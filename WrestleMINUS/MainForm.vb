Imports System.IO   'Files
Imports System.Text 'Text Encoding 
Imports Ionic.Zlib  'zlib decompress
'http://www.codeplex.com/DotNetZip

'http://forum.xentax.com/viewtopic.php?f=32&t=9972
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Environment 'appdata
Imports System.Threading 'Multithreading
Imports System.Runtime.Serialization.Formatters.Binary 'Binary Formatter
Imports Newtonsoft.Json
Imports System.Text.RegularExpressions 'Regular Expressions for text matches
Imports FontAwesome.Sharp
Public Class MainForm
#Region "Compression & Decompress"
    Public Enum OodleFormat As UInt32
        LZH
        LZHLW
        LZNIB
        None
        LZB16
        LZBLW
        LZA
        LZNA
        Kraken
        Mermaid
        BitKnit
        Selkie
        Akkorokamui
    End Enum
    Public Enum OodleCompressionLevel As UInt32
        None
        SuperFast
        VeryFast
        Fast
        Normal
        Optimal1
        Optimal2
        Optimal3
        Optimal4
        Optimal5
    End Enum
    Private Declare Function OodleLZ_Decompress Lib "oo2core_6_win64" (InputBuffer As Byte(), bufferSize As Long, OutputBuffer As Byte(), outputBufferSize As Long,
            a As UInt32, b As UInt32, c As ULong, d As UInt32, e As UInt32, f As UInt32, g As UInt32, h As UInt32, i As UInt32, threadModule As UInt32) As Integer
    Private Declare Function OodleLZ_Compress Lib "oo2core_6_win64" (format As OodleFormat, buffer As Byte(), bufferSize As Long, outputBuffer As Byte(), level As OodleCompressionLevel, a As UInt32, b As UInt32, b As UInt32, d As ULong, threadModule As UInt32) As Integer
    'U32 compsize = (U32)OodleLZ_Compress(g_compressor, rawbufbase+dataoffset, bytesread, compbuf, (OodleLZ_CompressionLevel)g_level, &compressoptions, rawbufbase+dataoffset-contextsize, NULL, g_scratchmemory, g_scratchmemsize);
    Function GetUncompressedOodleBytes(SentBytes As Byte())
        Dim CompressedLength As Long = BitConverter.ToUInt32(SentBytes, &H14)
        If Not CompressedLength = SentBytes.Length - &H18 Then
            If My.Settings.BypassOODLWarn Then
                CompressedLength = SentBytes.Length - &H18
            Else
                Dim result = MessageBox.Show("OODL Compression Length Mis-Match" & vbNewLine &
                              "Auto-detect compressed length?", "OODL Header Issue", MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Cancel Then
                    Return Nothing
                ElseIf result = DialogResult.Yes Then
                    CompressedLength = SentBytes.Length - &H18
                End If
            End If
        End If
        Dim CompressedBytes As Byte() = New Byte(CompressedLength - 1) {}
        Dim LengthDiff As Int32 = SentBytes.Length - CompressedBytes.Length
        Array.Copy(SentBytes, LengthDiff, CompressedBytes, 0, CInt(CompressedBytes.Length))
        Dim UncompressedLength As Long = BitConverter.ToUInt32(SentBytes, &H10)
        Dim UncompressedBytes As Byte() = New Byte(UncompressedLength - 1) {}
        Try
            OodleLZ_Decompress(CompressedBytes, CompressedLength, UncompressedBytes, UncompressedLength,
                               0, 0, 0, 0, 0, 0, 0, 0, 0, 3)
        Catch ex As Exception
            Return Nothing
        End Try
        Return UncompressedBytes
    End Function
    Function GetCompressedOodleBytes(SentBytes As Byte())
        Dim CompressedBuffer As Byte() = New Byte(SentBytes.LongLength - 1) {}
        Dim CompressedLength As Long = 0
        Try
            CompressedLength = OodleLZ_Compress(OodleFormat.Kraken, SentBytes, SentBytes.LongLength, CompressedBuffer, My.Settings.OODLCompressionLevel, 0, 0, 0, 0, 3)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Dim OodleFileBytes As Byte() = New Byte(CompressedLength - 1 + &H18) {}
        Dim HeaderArray As Byte() = {&H4F, &H4F, &H44, &H4C, &H0, &H0, &H0, &H0, &H30, &H0, &H6, &H2E, &H4B, &H52, &H4B, &H4E}
        Array.Copy(HeaderArray, 0, OodleFileBytes, 0, &H10) 'OODL Header '30 00 06 2E Bytes Might be relevant information
        Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length)), 0, OodleFileBytes, &H10, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(CompressedLength)), 0, OodleFileBytes, &H14, 4)
        Array.Copy(CompressedBuffer, 0, OodleFileBytes, &H18, CompressedLength)
        Return OodleFileBytes
    End Function
    Function GetUncompressedZlibBytes(SentBytes As Byte())
        Dim CompressedLength As UInt32 = BitConverter.ToUInt32(SentBytes, 8)
        Dim UncompressedLength As UInt32 = BitConverter.ToUInt32(SentBytes, 12)
        Dim input As Byte() = New Byte(CompressedLength - 1) {}
        Array.Copy(SentBytes, 16, input, 0, CInt(SentBytes.Length - 16))
        Dim output As Byte() = New Byte(UncompressedLength - 1) {}
        Try
            output = ZlibStream.UncompressBuffer(input)
        Catch ex As Exception
            Return Nothing
        End Try
        Return output
    End Function
    Function GetCompressedZlibBytes(SentBytes As Byte())
        Dim CompressedBuffer As Byte() = New Byte(SentBytes.LongLength - 1) {}
        Try
            Using MemStream As MemoryStream = New MemoryStream
                Using Compressor As ZlibStream = New ZlibStream(MemStream, CompressionMode.Compress, CompressionLevel.BestCompression)
                    Compressor.Write(SentBytes, 0, SentBytes.Length)
                End Using
                CompressedBuffer = MemStream.ToArray()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Dim ZlibFileBytes As Byte() = New Byte(CompressedBuffer.Length - 1 + &H10) {}
        Dim HeaderArray As Byte() = {&H5A, &H4C, &H49, &H42, &H30, &H12, &H0, &H0}
        Array.Copy(HeaderArray, 0, ZlibFileBytes, 0, &H8)
        Array.Copy(BitConverter.GetBytes(CUInt(CompressedBuffer.Length)), 0, ZlibFileBytes, &H8, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length)), 0, ZlibFileBytes, &HC, 4)
        Array.Copy(CompressedBuffer, 0, ZlibFileBytes, &H10, CompressedBuffer.Length)
        'BreakFunction(ZlibFileBytes)
        Return ZlibFileBytes
    End Function
    Function GetUncompressedBPEBytes(SentBytes As Byte())
        Dim UncompressedLength As Integer = BitConverter.ToUInt32(SentBytes, &HC)
        Dim UncompressedBytes As Byte() = New Byte(UncompressedLength) {}
        Try
            Dim TempInput As String = Path.GetTempFileName
            Dim TempOutput As String = Path.GetTempFileName
            File.WriteAllBytes(TempInput, SentBytes)
            Process.Start(My.Settings.UnrrbpePath, TempInput & " " & TempOutput).WaitForExit()
            UncompressedBytes = File.ReadAllBytes(TempOutput)
            File.Delete(TempInput)
            File.Delete(TempOutput)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Return UncompressedBytes
    End Function
    Function GetCompressedBPEBytes(SentBytes As Byte())
        Dim CompressedBuffer As Byte() = New Byte(SentBytes.LongLength - 1) {}
        Try
            Dim TempBPEExe As String = Path.GetDirectoryName(Application.ExecutablePath) & Path.DirectorySeparatorChar & Path.GetFileName(My.Settings.BPEExePath)
            Dim TempFile1 As String = Path.GetTempFileName
            Dim TempInput As String = Path.GetDirectoryName(TempBPEExe) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(TempFile1)
            Dim TempOutput As String = TempInput & ".bpe"
            File.Copy(My.Settings.BPEExePath, TempBPEExe, True)
            File.WriteAllBytes(TempInput, SentBytes)
            Dim Info As ProcessStartInfo = New ProcessStartInfo("cmd.exe")
            Info.Arguments = "/C """ & TempBPEExe & """ " & Path.GetFileName(TempInput) & " " & Path.GetFileName(TempOutput)
            Process.Start(Info).WaitForExit()
            CompressedBuffer = File.ReadAllBytes(TempOutput)
            If Not Path.GetDirectoryName(Application.ExecutablePath) = Path.GetDirectoryName(My.Settings.BPEExePath) Then
                File.Delete(TempBPEExe)
            End If
            File.Delete(TempInput)
            File.Delete(TempOutput)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        'BreakFunction(CompressedBuffer)
        Return CompressedBuffer
    End Function
    Sub BreakFunction(Optional TestedBytes As Byte() = Nothing)

    End Sub
#End Region
    Friend Shared StringReferences() As String
    Friend Shared PacNumbers() As Integer
    Dim SelectedFiles() As String
    'Injection Properties used across multiple forms
    Dim SavePending As Boolean = False
    Dim ReadNode As TreeNode
    Dim OldValue
    Dim InformationLoaded As Boolean = False
#Region "Main Form Functions"
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        LoadFontAwesomeIcons()
        CheckUpdate()
        SettingsCheck()
        HideTabs(Nothing)
        CreatedImages = New List(Of String)
    End Sub
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If CheckCommands() Then
            LoadParameters()
        ElseIf My.Settings.LoadHomeOnLaunch Then
            LoadHome()
        End If
    End Sub
    Sub LoadFontAwesomeIcons()
        If CheckFontAwesome() Then
            LoadHomeToolStripMenuItem.Image = IconChar.Home.ToBitmap(16, Color.Black)
            OpenToolStripMenuItem.Image = IconChar.FolderOpen.ToBitmap(16, Color.Black)
            ExitToolStripMenuItem.Image = IconChar.WindowClose.ToBitmap(16, Color.Black)
            BPECompressionToolStripMenuItem.Image = IconChar.CompressArrowsAlt.ToBitmap(16, Color.Black)
            ZLIBCompressionToolStripMenuItem.Image = IconChar.CompressArrowsAlt.ToBitmap(16, Color.Black)
            OODLCompressionToolStripMenuItem.Image = IconChar.CompressArrowsAlt.ToBitmap(16, Color.Black)
            'https://fontawesome.com/cheatsheet?from=io
        End If
    End Sub
    Sub SettingsCheck()
        If My.Settings.UpgradeRequired = True Then
            My.Settings.Upgrade()
            My.Settings.UpgradeRequired = False
            My.Settings.Save()
        End If
        'My.Settings.Reset()
        If My.Settings.ExeLocation = "" Then
            SelectHomeDirectory()
        End If
        If My.Settings.TexConvPath = "" Then 'Locate the texture conversion exe
            GetTexConvExe()
        End If
        If My.Settings.RADVideoToolPath = "" Then
            GetRadVideo()
        End If
        CheckUnrrbpe()
        CheckDDSexe()
        'Tool Menu Compress in place
        Dim CompressOptions As Integer = ToolsToolStripMenuItem.DropDownItems.Count
        If CheckBPEExe() Then
            BPECompressionToolStripMenuItem.Visible = True
        Else
            BPECompressionToolStripMenuItem.Visible = False
            CompressOptions -= 1
        End If
        If CheckIconicZlib() Then
            ZLIBCompressionToolStripMenuItem.Visible = True
        Else
            ZLIBCompressionToolStripMenuItem.Visible = False
            CompressOptions -= 1
        End If
        If CheckOodle() Then
            OODLCompressionToolStripMenuItem.Visible = True
        Else
            OODLCompressionToolStripMenuItem.Visible = False
            CompressOptions -= 1
        End If
        If CompressOptions = 0 Then
            ToolsToolStripMenuItem.Visible = False
        End If
        HexViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        TextViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
        ShowViewType.SelectedIndex = My.Settings.ShowModeIndex
        If My.Settings.StringObject = "" Then
            Dim AppDataStringLocation As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\Strings.bin"
            If File.Exists(AppDataStringLocation) Then
                Dim fs As Stream = New FileStream(AppDataStringLocation, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                StringReferences = CType(bf.Deserialize(fs), String())
                fs.Close()
            Else
                StringReferences = New String(&HFFFFF) {}
                StringReferences(0) = "String Not Read"
            End If
            My.Settings.StringObject = AppDataStringLocation
        Else
            If File.Exists(My.Settings.StringObject) Then
                Dim fs As Stream = New FileStream(My.Settings.StringObject, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                StringReferences = CType(bf.Deserialize(fs), String())
                fs.Close()
            Else
                StringReferences = New String(&HFFFFF) {}
                StringReferences(0) = "String Not Read"
            End If
        End If
        If My.Settings.PacNumObject = "" Then
            Dim AppDataPacNumLocation As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\PacNums.bin"
            If File.Exists(AppDataPacNumLocation) Then
                Dim fs As Stream = New FileStream(AppDataPacNumLocation, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                PacNumbers = CType(bf.Deserialize(fs), Integer())
                fs.Close()
            Else
                PacNumbers = New Integer(1024) {}
                PacNumbers(0) = -1
            End If
            My.Settings.PacNumObject = AppDataPacNumLocation
        Else
            If File.Exists(My.Settings.PacNumObject) Then
                Dim fs As Stream = New FileStream(My.Settings.PacNumObject, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                PacNumbers = CType(bf.Deserialize(fs), Integer())
                fs.Close()
            Else
                PacNumbers = New Integer(1024) {}
                PacNumbers(0) = -1
            End If
        End If
        If My.Settings.TruncateDecimalNames Then
            My.Settings.DecimalNameMinLength = 8
            My.Settings.TruncateDecimalNames = False
        End If
        My.Settings.Save()
    End Sub
    Dim MuscleViewStartupRemoved As Boolean = False
    Function HideTabs(ExcludedTab As TabPage) As DialogResult
        If IsNothing(ExcludedTab) Then
            ExcludedTab = New TabPage()
        End If
        For Each TempTabPage As TabPage In TabControl1.TabPages
            Select Case TempTabPage.Name
                Case HexView.Name
                    'Never Hide
                Case TextView.Name
                    'Never Hide
                Case MuscleView.Name
                    'this is not automatically removed except on startup
                    If Not MuscleViewStartupRemoved Then
                        TabControl1.TabPages.Remove(TempTabPage)
                        MuscleViewStartupRemoved = True
                    End If
                Case ExcludedTab.Name
                    'Excluded
                Case Else
                    'Here we will add the save pending check and file injection
                    If SavePending Then
                        Dim Result As Integer = MessageBox.Show("File save is pending, would you like to save?", "Save Pending", MessageBoxButtons.YesNoCancel)
                        If Result = DialogResult.Cancel Then
                            'This exits the command so we don't hide any tabs, and returns a cancel to the form closing command.
                            Return DialogResult.Cancel
                        ElseIf Result = DialogResult.Yes Then
                            Dim InjectedByte As Byte() = New Byte() {}
                            Select Case CType(ReadNode.Tag, NodeProperties).FileType
                                Case PackageType.StringFile
                                    InjectedByte = BuildStringFile()
                                Case PackageType.ArenaInfo
                                    InjectedByte = BuildMiscFile()
                                Case PackageType.ShowInfo
                                    InjectedByte = BuildShowFile()
                                Case PackageType.CostumeFile
                                    InjectedByte = BuildAttireFile()
                                Case PackageType.TitleFile
                                    InjectedByte = BuildTitleFile()
                            End Select
                            InjectIntoNode(ReadNode, InjectedByte)
                        Else 'Dialog Result No Save Canceled
                            SaveFileNoLongerPending()
                        End If
                    End If
                    TabControl1.TabPages.Remove(TempTabPage)
            End Select
        Next
        Return DialogResult.OK
    End Function
    Sub CheckUpdate()
        Dim checkUpdateThread = New Thread(AddressOf OnlineVersion.CheckUpdate)
        checkUpdateThread.SetApartmentState(ApartmentState.STA)
        checkUpdateThread.Start()
    End Sub
#Region "Options Menu Location Functions"
    Public Sub SelectHomeDirectory()
        Dim TempFileDialog As OpenFileDialog = New OpenFileDialog
        TempFileDialog.FileName = "WWE2KXX.exe"
        TempFileDialog.Title = "Select WWE exe directory"
        If Directory.Exists("C:\Steam\steamapps\common\") Then
            TempFileDialog.InitialDirectory = "C:\Steam\steamapps\common\"
        End If
        TempFileDialog.ShowDialog()
        If File.Exists(TempFileDialog.FileName) AndAlso
            Path.GetExtension(TempFileDialog.FileName).ToLower = ".exe" Then
            My.Settings.ExeLocation = TempFileDialog.FileName
        Else
            If My.Settings.ExeLocation = "" Then
                MessageBox.Show("Loading home disabled")
                My.Settings.ExeLocation = ""
                My.Settings.LoadHomeOnLaunch = False
            End If
        End If
    End Sub
    Public Sub GetTexConvExe(Optional fromoptions As Boolean = False)
        Dim convertpath As String = Path.GetDirectoryName(Application.ExecutablePath) &
                      Path.DirectorySeparatorChar & "texconv.exe"
        Dim appDataPath As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        FolderCheck(appDataPath)
        appDataPath += Path.DirectorySeparatorChar & "texconv.exe"
        If fromoptions Then
            Dim TexConvToolOpenDialog As New OpenFileDialog With {.FileName = "texconv.exe", .Title = "Select texconv.exe"}
            If My.Settings.TexConvPath = "" Then
                TexConvToolOpenDialog.InitialDirectory = Application.StartupPath
            Else
                Path.GetDirectoryName(My.Settings.TexConvPath)
            End If
            If TexConvToolOpenDialog.ShowDialog = DialogResult.OK Then
                If Path.GetFileName(TexConvToolOpenDialog.FileName) = "texconv.exe" Then
                    My.Settings.TexConvPath = TexConvToolOpenDialog.FileName
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If File.Exists(convertpath) Then
                If MessageBox.Show("Would you like to move the texture conversion exe to Appdata?" & vbNewLine & "(Recommended)", "Move tool to appdata?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    File.Move(convertpath, appDataPath)
                    My.Settings.TexConvPath = appDataPath
                Else
                    My.Settings.TexConvPath = convertpath
                End If
            ElseIf File.Exists(appDataPath) Then
                My.Settings.TexConvPath = appDataPath
            Else
                If MessageBox.Show("texconv.exe not found!" & vbNewLine &
                                "Would you like to navigate to ""texconv.exe"" ", "Find texconv.exe", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    Dim TexConvToolOpenDialog As New OpenFileDialog With {.FileName = "texconv.exe", .Title = "Select texconv.exe"}
                    If TexConvToolOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(TexConvToolOpenDialog.FileName) = "texconv.exe" Then
                            My.Settings.TexConvPath = TexConvToolOpenDialog.FileName
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If


                Else
                    MessageBox.Show("Picture view will raise errors")
                End If
            End If
        End If
    End Sub
    Public Sub GetRadVideo(Optional FromOptions As Boolean = False)
        If My.Settings.RADVideoToolPath = "" Then
            My.Settings.RADVideoToolPath = "Not Installed"
            Dim AllDrives As DriveInfo() = DriveInfo.GetDrives()
            For Each Drive As DriveInfo In AllDrives
                'MessageBox.Show(Drive.Name & "Program Files (x86)\RADVideo\radvideo.exe")
                If (Drive.IsReady = True) Then
                    If File.Exists(Drive.Name & "Program Files (x86)\RADVideo\radvideo.exe") Then
                        My.Settings.RADVideoToolPath = Drive.Name & "Program Files (x86)\RADVideo\binkplay.exe"
                        Exit For
                    ElseIf File.Exists(Drive.Name & "Program Files\RADVideo\radvideo.exe") Then
                        My.Settings.RADVideoToolPath = Drive.Name & "Program Files\RADVideo\binkplay.exe"
                        Exit For
                    End If
                End If
            Next
        End If
        If My.Settings.RADVideoToolPath = "Not Installed" OrElse FromOptions Then
            Dim RADVideoToolOpenDialog As New OpenFileDialog With {.FileName = "radvideo.exe", .Title = "Select radvideo.exe"}
            If Not My.Settings.RADVideoToolPath = "Not Installed" Then
                RADVideoToolOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.RADVideoToolPath)
            End If
            If RADVideoToolOpenDialog.ShowDialog = DialogResult.OK Then
                If Path.GetFileName(RADVideoToolOpenDialog.FileName) = "radvideo.exe" Then
                    My.Settings.RADVideoToolPath = RADVideoToolOpenDialog.FileName.Replace("radvideo.exe", "binkplay.exe")
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        End If
    End Sub
    Public Function CheckBPEExe(Optional FromOptions As Boolean = False)
        Dim AppDataStorage As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        If FromOptions = False Then
            If File.Exists(AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe") Then
                My.Settings.BPEExePath = AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe"
                Return True
            Else
                If MessageBox.Show("Would you like to navigate to ""bpe.exe""" & vbNewLine &
                    "this would be in any ""Pac Editor"" install folder.",
                       "BPE Compressor",
                       MessageBoxButtons.YesNo) = DialogResult.No Then
                    MessageBox.Show("Download link can be found in the options menu.")
                    My.Settings.BPEExePath = "Not Installed"
                    Return False
                End If
            End If
        End If
        If My.Settings.BPEExePath = "" Then
            My.Settings.BPEExePath = "Not Installed"
        End If
        Dim BPEExeOpenDialog As New OpenFileDialog With {.FileName = "bpe.exe", .Title = "Select bpe.exe"}
        If BPEExeOpenDialog.ShowDialog = DialogResult.OK Then
            If Path.GetFileName(BPEExeOpenDialog.FileName) = "bpe.exe" Then
                If Not Path.GetDirectoryName(BPEExeOpenDialog.FileName) = AppDataStorage Then
                    If MessageBox.Show("Would you like create a copy of this to the appdata?",
                                       "Copy File?",
                                       MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        FolderCheck(AppDataStorage)
                        File.Copy(BPEExeOpenDialog.FileName, AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe", True)
                        My.Settings.BPEExePath = AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe"
                        Return True
                    Else
                        My.Settings.BPEExePath = BPEExeOpenDialog.FileName
                        Return True
                    End If
                Else
                    My.Settings.BPEExePath = BPEExeOpenDialog.FileName
                    Return True
                End If
            Else
                MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                Return False
            End If
        End If
        If My.Settings.BPEExePath = "Not Installed" Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CheckUnrrbpe(Optional FromOptions As Boolean = False)
        Dim AppDataStorage As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        If FromOptions = False Then
            If File.Exists(AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe") Then
                My.Settings.UnrrbpePath = AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe"
                Return True
            Else
                If MessageBox.Show("Would you like to navigate to ""unrrbpe.exe""" & vbNewLine &
                    "this would be in any ""X-Packer"" install folder.",
                       "BPE Decompresser",
                       MessageBoxButtons.YesNo) = DialogResult.No Then
                    MessageBox.Show("Download link can be found in the options menu.")
                    My.Settings.UnrrbpePath = "Not Installed"
                    Return False
                End If
            End If
        End If
        If My.Settings.UnrrbpePath = "" Then
            My.Settings.UnrrbpePath = "Not Installed"
        End If
        Dim UnrrbpeOpenDialog As New OpenFileDialog With {.FileName = "unrrbpe.exe", .Title = "Select unrrbpe.exe"}
        If UnrrbpeOpenDialog.ShowDialog = DialogResult.OK Then
            If Path.GetFileName(UnrrbpeOpenDialog.FileName) = "unrrbpe.exe" Then
                If Not Path.GetDirectoryName(UnrrbpeOpenDialog.FileName) = AppDataStorage Then
                    If MessageBox.Show("Would you like create a copy of this to the appdata?",
                                       "Copy File?",
                                       MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        FolderCheck(AppDataStorage)
                        File.Copy(UnrrbpeOpenDialog.FileName, AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe", True)
                        My.Settings.UnrrbpePath = AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe"
                        Return True
                    Else
                        My.Settings.UnrrbpePath = UnrrbpeOpenDialog.FileName
                        Return True
                    End If
                Else
                    My.Settings.UnrrbpePath = UnrrbpeOpenDialog.FileName
                    Return True
                End If
            Else
                MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                Return False
            End If
        End If
        If My.Settings.UnrrbpePath = "Not Installed" Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Sub CheckDDSexe(Optional FromOptions As Boolean = False)
        If FromOptions Then
            Dim DDSOpenExeOpenDialog As New OpenFileDialog With {.FileName = "*.exe", .Title = "Select exe for opening DDS files"}
            If Not My.Settings.DDSexeLocation = "Not Installed" Then
                DDSOpenExeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.DDSexeLocation)
            End If
            If DDSOpenExeOpenDialog.ShowDialog = DialogResult.OK Then
                My.Settings.DDSexeLocation = DDSOpenExeOpenDialog.FileName
            Else
                OpenWithToolStripMenuItem.Text = "Open With..."
            End If
        ElseIf File.Exists(My.Settings.DDSexeLocation) Then
            OpenWithToolStripMenuItem.Text = "Open With " & Path.GetFileNameWithoutExtension(My.Settings.DDSexeLocation)
        End If
    End Sub
    Public Function CheckIconicZlib(Optional FromOptions As Boolean = False) As Boolean
        If Not File.Exists(Application.StartupPath & Path.DirectorySeparatorChar & "Ionic.Zlib.dll") Then
            If Not FromOptions Then
                MessageBox.Show("Ionic.Zlib Dll Not loaded")
                Return False
            Else
                Dim ZlibOpenDialog As New OpenFileDialog With {.FileName = "Ionic.Zlib.dll", .Title = "Ionic.Zlib.dll"}
                If ZlibOpenDialog.ShowDialog = DialogResult.OK Then
                    If Path.GetFileName(ZlibOpenDialog.FileName) = "Ionic.Zlib.dll" Then
                        File.Copy(ZlibOpenDialog.FileName, Application.StartupPath & Path.DirectorySeparatorChar & "Ionic.Zlib.dll")
                        Return True
                    Else
                        MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Else
            Return True
        End If
    End Function
    Public Function CheckOodle(Optional FromOptions As Boolean = False) As Boolean
        If Not File.Exists(Application.StartupPath & Path.DirectorySeparatorChar & "oo2core_6_win64.dll") Then
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "oo2core_6_win64.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation,
                      Path.GetDirectoryName(Application.ExecutablePath) & Path.DirectorySeparatorChar & "oo2core_6_win64.dll", True)
                Return True
            Else
                If Not FromOptions Then
                    MessageBox.Show("Oodle Dll Not loaded")
                    Return False
                Else
                    Dim OodleOpenDialog As New OpenFileDialog With {.FileName = "oo2core_6_win64.dll", .Title = "oo2core_6_win64.dll"}
                    If OodleOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(OodleOpenDialog.FileName) = "oo2core_6_win64.dll" Then
                            File.Copy(OodleOpenDialog.FileName, Application.StartupPath &
                                      Path.DirectorySeparatorChar & "oo2core_6_win64.dll")
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If
            End If
        Else
            Return True
        End If
    End Function
    Public Function CheckFontAwesome(Optional FromOptions As Boolean = False) As Boolean
        If Not File.Exists(Application.StartupPath & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll") Then
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation,
                      Path.GetDirectoryName(Application.ExecutablePath) & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll", True)
                Return True
            Else
                If Not FromOptions Then
                    MessageBox.Show("Oodle Dll Not loaded")
                    Return False
                Else
                    Dim OodleOpenDialog As New OpenFileDialog With {.FileName = "FontAwesome.Sharp.dll", .Title = "FontAwesome.Sharp.dll"}
                    If OodleOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(OodleOpenDialog.FileName) = "FontAwesome.Sharp.dll" Then
                            File.Copy(OodleOpenDialog.FileName, Application.StartupPath &
                                      Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll")
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If
            End If
        Else
            Return True
        End If
    End Function
#End Region
    Shared Function CheckCommands() As Boolean
        Dim args As String() = Environment.GetCommandLineArgs()
        Dim parameters As Boolean = False
        If args.Length > 1 Then
            parameters = True
            MainForm.SelectedFiles = New String(args.Length - 2) {}
            For i As Integer = 1 To args.Length - 1
                MainForm.SelectedFiles(i - 1) = args(i)
            Next
        End If
        Return parameters
    End Function
    Shared Sub LoadIcons()
        If My.Settings.UseTreeIcons Then
            MainForm.TreeView1.ImageList = MainForm.ImageList1
        Else
            MainForm.TreeView1.ImageList = Nothing
        End If
    End Sub
    Private Sub MainForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter,
                                                                                Hex_Selected.DragEnter,
                                                                                Text_Selected.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop,
                                                                                Hex_Selected.DragDrop,
                                                                                Text_Selected.DragDrop
        Dim s() As String = e.Data.GetData("FileDrop", False)
        SelectedFiles = s
        LoadParameters()
        'Dim i As Integer
        'For i = 0 To s.Length - 1
        'ListBox1.Items.Add(s(i))
        'Next i
    End Sub
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If My.Settings.DeleteTempBMP Then
            DeleteTempImages()
            'Code from xhp-creations commented out for future if Delete Temp does not properly wor
            'Dim TempFolder As String = Path.GetDirectoryName(My.Settings.TexConvPath) &
            'Path.DirectorySeparatorChar
            'Try
            'For Each f In Directory.GetFiles(TempFolder, "*.BMP", SearchOption.AllDirectories)
            'File.Delete(f)
            'Next
            'Catch ex As UnauthorizedAccessException
            'End Try
        End If
        'Close all tabs so save check is in just one place.
        If HideTabs(Nothing) = DialogResult.Cancel Then
            e.Cancel = True
        End If
        'Seperating command out to allow for error handling to exit closing form command
        If SaveSettingsFiles() = DialogResult.Cancel Then
            e.Cancel = True
        End If
    End Sub
    Function SaveSettingsFiles() As DialogResult
        Try
            'Here is the saving of the String File so we don't store it in the settings file and bloat the loading and closing.
            If File.Exists(My.Settings.StringObject) = True Then
                File.Delete(My.Settings.StringObject)
            End If
            Dim StringFileStream As Stream = New FileStream(My.Settings.StringObject, FileMode.Create)
            Dim StringBinaryFormatter As BinaryFormatter = New BinaryFormatter()
            StringBinaryFormatter.Serialize(StringFileStream, StringReferences)
            StringFileStream.Close()
            If File.Exists(My.Settings.PacNumObject) = True Then
                File.Delete(My.Settings.PacNumObject)
            End If
            Dim PacNumFileStream As Stream = New FileStream(My.Settings.PacNumObject, FileMode.Create)
            Dim PacNumBinaryFormatter As BinaryFormatter = New BinaryFormatter()
            PacNumBinaryFormatter.Serialize(PacNumFileStream, PacNumbers)
            PacNumFileStream.Close()
            Return DialogResult.OK
        Catch ex As Exception
            Return MessageBox.Show("Error Savings Settings Files" & vbNewLine &
                            ex.Message & vbNewLine & "Continue?", "Settings Error", MessageBoxButtons.OKCancel)
        End Try
    End Function
#End Region
#Region "General Tools"
    'Here are App-seperate commands use as required.
    Private Sub FolderCheck(FolderPath As String)
        If Directory.Exists(FolderPath) = False Then
            Directory.CreateDirectory(FolderPath)
        End If
    End Sub
    Private Sub MoveAllItems(ByVal fromPath As String, ByVal toPath As String)
        My.Computer.FileSystem.CopyDirectory(fromPath, toPath, True)
        My.Computer.FileSystem.DeleteDirectory(fromPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub
    Private Function EndianReverse(Source As Byte(), Optional Index As Integer = 0, Optional Length As Integer = 4) As Byte()
        Dim ReturnedArray As Byte() = New Byte(Length - 1) {}
        Array.Copy(Source, Index, ReturnedArray, 0, Length)
        Array.Reverse(ReturnedArray)
        Return ReturnedArray
    End Function
    Function HexCheck(StringtoCheck As String) As Boolean
        If Not IsNothing(StringtoCheck) Then
            Dim HexTest As Boolean = StringtoCheck.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            Return HexTest
        Else
            Return False
        End If
    End Function
    Function HexStringToByte(HexString As String) As Byte()
        Dim NumberofCharacters As Integer = HexString.Length
        Dim ReturnedBytes As Byte() = New Byte(NumberofCharacters / 2 - 1) {}
        For i As Integer = 0 To NumberofCharacters - 1 Step 2
            ReturnedBytes(i / 2) = Convert.ToByte(HexString.Substring(i, 2), 16)
        Next
        Return ReturnedBytes
    End Function
#End Region
#Region "File Handlers"
    Public Class NodeProperties
        Public FileType As PackageType = PackageType.Unchecked
        Public Index As UInt64 = 0
        Public length As UInt64
        Public StoredData As Byte()
    End Class
    Public Enum PackageType
        Unchecked
        bin
        Folder
        HSPC
        EPK8
        EPAC
        PACH
        SHDC
        PachDirectory_4
        PachDirectory_8
        ZLIB
        BPE
        OODL
        TextureLibrary
        YANMPack
        YOBJ
        StringFile
        DDS
        ArenaInfo
        ShowInfo
        NIBJ
        bk2
        CostumeFile
        MuscleFile
        MaskFile
        YOBJArray
        OFOP
        YANM
        VMUM
        TitleFile
        'here are additional ufc type files that we will have no handling for.  Hoever we want to properly cover them for adding in future.
        UFC_MAP
        UFC_HKX
        UFC_PAC
        UFC_TXT
        UFC_CVX
        UFC_BIN
        'Day of Recconing GV File Types added as needed
        TPL
        DUMY
        'Sending the text editor notice that we are editing a file name
        EditingFileName
    End Enum
    Dim ActiveFile As String = ""
    Dim LibraryContainsList As String() = New String() {"dds",
        "ymx",
        "tex",
        "map",
        "hkx",
        "pac",
        "txt",
        "cvx",
        "bin",
        "tpl"} ' list adds files from ufc undesputed.
    Sub CheckFile(ByRef HostNode As TreeNode, Optional Crawl As Boolean = False)
        'TO DO Tempnode generation can be simplified.
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(HostNode.Tag, NodeProperties)
        ActiveFile = HostNode.ToolTipText
        Dim FileBytes As Byte() = GetNodeBytes(HostNode)
        Select Case NodeTag.FileType
            Case PackageType.Unchecked
                NodeTag.FileType = CheckHeaderType(0, FileBytes)
                NodeTag.StoredData = New Byte() {}
                HostNode.ImageIndex = GetImageIndex(NodeTag.FileType)
                HostNode.SelectedImageIndex = HostNode.ImageIndex
                HostNode.Tag = NodeTag
                CheckFile(HostNode, Crawl)
                Exit Sub 'Skips the crawler at the bottom and duping the host node update
#Region "Primary Container Types {PAC}"
            Case PackageType.HSPC
                Dim FileCount As Integer = BitConverter.ToUInt32(FileBytes, &H38)
                Dim FileNameLength As Integer = BitConverter.ToUInt32(FileBytes, &H18)
                FileNameLength += -(FileNameLength Mod &H800) + &H1000
                For i As Integer = 0 To FileCount - 1
                    'This will be the full length hex name always so TruncateDecimalNames is unneeded
                    Dim FileName As String = BitConverter.ToString(FileBytes, &H800 + i * &H14, 8).ToUpper.Replace("-", "")
                    Dim TempNode As TreeNode = New TreeNode(FileName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .Index = BitConverter.ToUInt32(FileBytes, FileNameLength + i * &HC) * &H800 + NodeTag.Index,
                        .length = BitConverter.ToUInt32(FileBytes, FileNameLength + i * &HC + &H4) * &H100,
                        .StoredData = NodeTag.StoredData,
                        .FileType = CheckHeaderType(.Index - NodeTag.Index, FileBytes)}
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    TempNode.Tag = TempNodeProps
                    HostNode.Nodes.Add(TempNode)
                Next
            Case PackageType.EPK8
                Dim HeaderLength As Integer = BitConverter.ToUInt32(FileBytes, 4)
                Dim index As Integer = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4) 'we cant trim spaces because it would mess with reinjection if the name was edited
                    Dim DirectoryTreeNode As TreeNode = New TreeNode(DirectoryName, 6, 6)
                    DirectoryTreeNode.ToolTipText = HostNode.ToolTipText
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(FileBytes, &H800 + index + 4) / 4
                    Dim DirectoryNodeProps As NodeProperties = New NodeProperties With {
                        .StoredData = NodeTag.StoredData,
                        .FileType = PackageType.PachDirectory_8}
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        Dim PachName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 8)
                        Dim TempNode As TreeNode = New TreeNode(PachName)
                        TempNode.ToolTipText = HostNode.ToolTipText
                        Dim TempNodeProps As NodeProperties = New NodeProperties With {
                            .Index = BitConverter.ToUInt32(FileBytes, &H800 + index + 8) * &H800 + &H4000 + NodeTag.Index,
                            .length = BitConverter.ToUInt32(FileBytes, &H800 + index + 12) * &H100,
                            .StoredData = NodeTag.StoredData,
                            .FileType = CheckHeaderType(.Index - NodeTag.Index, FileBytes)}
                        If i = 0 Then
                            DirectoryNodeProps.Index = TempNodeProps.Index
                        End If
                        DirectoryNodeProps.length += TempNodeProps.length
                        index += &H10
                        TempNode.Tag = TempNodeProps
                        TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                        TempNode.SelectedImageIndex = TempNode.ImageIndex
                        DirectoryTreeNode.Nodes.Add(TempNode)
                    Next
                    DirectoryTreeNode.Tag = DirectoryNodeProps
                    DirectoryTreeNode.ImageIndex = GetImageIndex(DirectoryNodeProps.FileType)
                    DirectoryTreeNode.SelectedImageIndex = DirectoryTreeNode.ImageIndex
                    HostNode.Nodes.Add(DirectoryTreeNode)
                Loop
            Case PackageType.EPAC
                Dim HeaderLength As Integer = BitConverter.ToUInt32(FileBytes, 4)
                Dim index As Integer = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4) 'we cant trim spaces because it would mess with reinjection if the name was edited
                    Dim DirectoryTreeNode As TreeNode = New TreeNode(DirectoryName, 6, 6)
                    DirectoryTreeNode.ToolTipText = HostNode.ToolTipText
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(FileBytes, &H800 + index + 4) / 3
                    Dim DirectoryNodeProps As NodeProperties = New NodeProperties With {
                        .StoredData = NodeTag.StoredData,
                        .FileType = PackageType.PachDirectory_4}
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        Dim PachName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4)
                        Dim TempNode As TreeNode = New TreeNode(PachName)
                        TempNode.ToolTipText = HostNode.ToolTipText
                        Dim TempNodeProps As NodeProperties = New NodeProperties With {
                            .Index = BitConverter.ToUInt32(FileBytes, &H800 + index + 4) * &H800 + &H4000 + NodeTag.Index,
                            .length = BitConverter.ToUInt32(FileBytes, &H800 + index + 8) * &H100,
                            .StoredData = NodeTag.StoredData,
                            .FileType = CheckHeaderType(.Index - NodeTag.Index, FileBytes)}
                        If i = 0 Then
                            DirectoryNodeProps.Index = TempNodeProps.Index
                        End If
                        DirectoryNodeProps.length += TempNodeProps.length
                        index += &HC
                        TempNode.Tag = TempNodeProps
                        TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                        TempNode.SelectedImageIndex = TempNode.ImageIndex
                        DirectoryTreeNode.Nodes.Add(TempNode)
                    Next
                    DirectoryTreeNode.Tag = DirectoryNodeProps
                    DirectoryTreeNode.ImageIndex = GetImageIndex(DirectoryNodeProps.FileType)
                    DirectoryTreeNode.SelectedImageIndex = DirectoryTreeNode.ImageIndex
                    HostNode.Nodes.Add(DirectoryTreeNode)
                Loop
#End Region
#Region "Secondary Container Types {PACH}"
            Case PackageType.SHDC
                Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(FileBytes, &H18)
                Dim TempHeaderStart As Integer = BitConverter.ToUInt32(FileBytes, &H1C)
                Dim TempHeaderLength As Integer = BitConverter.ToUInt32(FileBytes, &H20)
                If TempHeaderStart < TempHeaderCheck Then
                    TempHeaderStart = TempHeaderCheck + &H10 + &H40
                    If TempHeaderStart Mod &H10 > 0 Then
                        TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                    End If
                End If
                Dim PachPartsCount As Integer = (TempHeaderLength / &H10) '1 index
                For i As Integer = 0 To PachPartsCount - 1
                    Try
                        Dim PartName As String = Hex(BitConverter.ToUInt32(FileBytes, TempHeaderStart + (i * &H10)))
                        'MessageBox.Show(PartName)
                        If PartName = "FFFFFFFF" Then
                            Continue For
                        End If
                        PartName = PartName.PadLeft(My.Settings.DecimalNameMinLength, "0")
                        Dim TempNode As TreeNode = New TreeNode(PartName)
                        TempNode.ToolTipText = HostNode.ToolTipText
                        Dim TempNodeProps As NodeProperties = New NodeProperties With {
                            .Index = BitConverter.ToUInt32(FileBytes, TempHeaderStart + (i * &H10) + &H4) + NodeTag.Index,
                            .length = BitConverter.ToUInt64(FileBytes, TempHeaderStart + (i * &H10) + &H8),
                            .StoredData = NodeTag.StoredData,
                            .FileType = CheckHeaderType(.Index - NodeTag.Index, FileBytes)}
                        TempNode.Tag = TempNodeProps
                        TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                        TempNode.SelectedImageIndex = TempNode.ImageIndex
                        HostNode.Nodes.Add(TempNode)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message & vbNewLine &
                                        "Object Number: " & i & vbNewLine &
                                        "Header Start {hex}: " & Hex(TempHeaderStart))
                    End Try
                Next
            Case PackageType.PACH
                Dim Partcount As Integer = BitConverter.ToUInt32(FileBytes, 4)
                For i As Integer = 0 To Partcount - 1
                    Dim PartName As String = Hex(BitConverter.ToUInt32(FileBytes, &H8 + (i * &HC)))
                    PartName = PartName.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0")
                    Dim TempNode As TreeNode = New TreeNode(PartName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .Index = BitConverter.ToUInt32(FileBytes, &HC + (i * &HC)) + &H8 + Partcount * &HC + NodeTag.Index,
                        .length = BitConverter.ToUInt32(FileBytes, &H10 + (i * &HC)),
                        .StoredData = NodeTag.StoredData,
                        .FileType = CheckHeaderType(.Index - NodeTag.Index, FileBytes)}
                    TempNode.Tag = TempNodeProps
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    HostNode.Nodes.Add(TempNode)
                Next
            Case PackageType.DUMY
                Dim HeaderLength As Integer = BitConverter.ToUInt32(EndianReverse(FileBytes, 4), 0)
                Dim PartName As String = Hex(0)
                PartName = PartName.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0")
                Dim TempNode As TreeNode = New TreeNode(PartName)
                TempNode.ToolTipText = HostNode.ToolTipText
                Dim TempNodeProps As NodeProperties = New NodeProperties With {
                    .Index = &H8 + HeaderLength,
                    .length = FileBytes.Length - .Index,
                    .StoredData = NodeTag.StoredData,
                    .FileType = CheckHeaderType(.Index - NodeTag.Index, FileBytes)}
                TempNode.Tag = TempNodeProps
                TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                TempNode.SelectedImageIndex = TempNode.ImageIndex
                HostNode.Nodes.Add(TempNode)
#End Region
#Region "Compression Types"
            Case PackageType.ZLIB
                ' Checking to make sure the node isn't already decompressed..
                If HostNode.Nodes.Count > 0 Then
                    'MessageBox.Show(HostNode.Text & " Already Decompressed")
                    Exit Select
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If CheckIconicZlib() Then
                    UncompressedBytes = GetUncompressedZlibBytes(FileBytes)
                End If
                If IsNothing(UncompressedBytes) Then
                    NodeTag.FileType = PackageType.bin
                    HostNode.Tag = NodeTag
                    Exit Sub
                Else
                    Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .Index = 0,
                        .length = UncompressedBytes.Length,
                        .StoredData = UncompressedBytes,
                        .FileType = CheckHeaderType(.Index, UncompressedBytes)}
                    TempNode.Tag = TempNodeProps
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    HostNode.Nodes.Add(TempNode)
                End If
            Case PackageType.BPE
                ' Checking to make sure the node isn't already decompressed..
                If HostNode.Nodes.Count > 0 Then
                    'MessageBox.Show(HostNode.Text & " Already Decompressed")
                    Exit Select
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If CheckUnrrbpe() Then
                    UncompressedBytes = GetUncompressedBPEBytes(FileBytes)
                End If
                If IsNothing(UncompressedBytes) Then
                    NodeTag.FileType = PackageType.bin
                    HostNode.Tag = NodeTag
                    Exit Sub
                Else
                    Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .Index = 0,
                        .length = UncompressedBytes.Length,
                        .StoredData = UncompressedBytes,
                        .FileType = CheckHeaderType(.Index, UncompressedBytes)}
                    TempNode.Tag = TempNodeProps
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    HostNode.Nodes.Add(TempNode)
                End If
            Case PackageType.OODL
                ' Checking to make sure the node isn't already decompressed..
                If HostNode.Nodes.Count > 0 Then
                    'MessageBox.Show(HostNode.Text & " Already Decompressed")
                    Exit Select
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If CheckOodle() Then
                    UncompressedBytes = GetUncompressedOodleBytes(FileBytes)
                End If
                If IsNothing(UncompressedBytes) Then
                    NodeTag.FileType = PackageType.bin
                    HostNode.Tag = NodeTag
                    Exit Sub
                Else
                    Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .Index = 0,
                        .length = UncompressedBytes.Length,
                        .StoredData = UncompressedBytes,
                        .FileType = CheckHeaderType(.Index, UncompressedBytes)}
                    TempNode.Tag = TempNodeProps
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    HostNode.Nodes.Add(TempNode)
                End If
#End Region
#Region "Library Types"
            Case PackageType.TextureLibrary
                Dim TextureCount As Integer = FileBytes(0)
                Dim BytesRevesed As Boolean = False
                If TextureCount = 0 Then 'if this is 0 then we are dealing with a reverse byte system header.
                    TextureCount = FileBytes(3)
                    BytesRevesed = True
                End If
                For i As Integer = 0 To TextureCount - 1
                    Dim ImageName As String = Encoding.Default.GetChars(FileBytes, i * &H20 + &H10, &H10)
                    ImageName = ImageName.TrimEnd(Chr(0))
                    'Adding File Type Check
                    Dim FileTypeName As String = Encoding.Default.GetChars(FileBytes, i * &H20 + &H20, 3)
                    FileTypeName = FileTypeName.ToLower
                    Dim FileTypeContained As PackageType = PackageType.bin
                    Select Case FileTypeName
                        Case "dds"
                            FileTypeContained = PackageType.DDS
                        Case "ymx"
                            FileTypeContained = PackageType.YOBJ
                        Case "tex"
                            FileTypeContained = PackageType.TextureLibrary
                        Case "map"
                            FileTypeContained = PackageType.UFC_MAP
                        Case "hkx"
                            FileTypeContained = PackageType.UFC_HKX
                        Case "pac"
                            FileTypeContained = PackageType.UFC_PAC
                        Case "txt"
                            FileTypeContained = PackageType.UFC_TXT
                        Case "cvx"
                            FileTypeContained = PackageType.UFC_CVX
                        Case "bin"
                            FileTypeContained = PackageType.UFC_BIN
                        Case "tpl"
                            FileTypeContained = PackageType.TPL
                        Case Else
                            'MessageBox.Show("Missing Type " & FileTypeName)
                    End Select
                    'Here we need to get the File length and index incase the bytes are revesed
                    Dim CurrentItemLength As UInt32 = 0
                    Dim CurrentItemIndex As UInt32 = 0
                    If BytesRevesed Then
                        CurrentItemLength = BitConverter.ToUInt32(EndianReverse(FileBytes, i * &H20 + &H10 + &H14, 4), 0)
                        CurrentItemIndex = BitConverter.ToUInt32(EndianReverse(FileBytes, i * &H20 + &H10 + &H18, 4), 0) + HostNode.Index
                    Else
                        CurrentItemLength = BitConverter.ToUInt32(FileBytes, i * &H20 + &H10 + &H14)
                        CurrentItemIndex = BitConverter.ToUInt64(FileBytes, i * &H20 + &H10 + &H18) + HostNode.Index
                    End If
                    Dim TempNode As TreeNode = New TreeNode(ImageName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .length = CurrentItemLength,
                        .Index = CurrentItemIndex,
                        .StoredData = NodeTag.StoredData,
                        .FileType = FileTypeContained}
                    TempNode.Tag = TempNodeProps
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    HostNode.Nodes.Add(TempNode)
                Next
            Case PackageType.YANMPack
                Dim HeaderLength As UInt32 = BitConverter.ToUInt32(EndianReverse(FileBytes), 0) + &H20
                'MessageBox.Show(HeaderLength)
                Dim YANMLength As UInt32 = BitConverter.ToUInt32(EndianReverse(FileBytes, 3), 0)
                'MessageBox.Show(YANMLength)
                Dim HeadIndex As Integer = 0
                Dim partcount As Integer = 0
                Do While HeadIndex < HeaderLength
                    If HeadIndex = 0 Then
                        HeadIndex = &H70
                    End If
                    'getting the part name
                    Dim PartName As String = Encoding.ASCII.GetString(FileBytes, HeadIndex + 4, 8)
                    Dim TempNode As TreeNode = New TreeNode(PartName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties With {
                        .Index = (BitConverter.ToUInt32(EndianReverse(FileBytes, HeadIndex + &H24), 0)) + HeaderLength + HostNode.Index,
                        .FileType = PackageType.YANM,
                        .StoredData = NodeTag.StoredData}
                    If HeadIndex + &H20 + &H28 < HeaderLength Then
                        TempNodeProps.length = (BitConverter.ToUInt32(EndianReverse(FileBytes, HeadIndex + &H24 + &H28), 0)) + HeaderLength - TempNodeProps.Index
                    Else
                        TempNodeProps.length = YANMLength - TempNodeProps.Index + HeaderLength
                    End If
                    'add to list box
                    TempNode.Tag = TempNodeProps
                    TempNode.ImageIndex = GetImageIndex(TempNodeProps.FileType)
                    TempNode.SelectedImageIndex = TempNode.ImageIndex
                    HostNode.Nodes.Add(TempNode)
                    partcount = partcount + 1
                    HeadIndex = HeadIndex + &H28
                Loop
#Region "To be Built"
            Case PackageType.YOBJ
#End Region
#End Region
#Region "Stand Alone Files"
                'Case PackageType.StringFile
                'Case PackageType.bin
                'Case PackageType.DDS
                'Case PackageType.YANM
                'Case PackageType.ArenaInfo
                'Case PackageType.ShowInfo
                'Case PackageType.NIBJ
                'Case PackageType.bk2
                'Case PackageType.CostumeFile
                'Case PackageType.MuscleFile
                'Case PackageType.MaskFile
                'Case PackageType.YOBJArray
                'Case PackageType.OFOP
                'Case PackageType.YANM
                'Case PackageType.VMUM
#End Region
        End Select
        If Crawl Then
            For Each ChildNode As TreeNode In HostNode.Nodes
                Dim ChildNodeProps As NodeProperties = New NodeProperties
                ChildNodeProps = CType(ChildNode.Tag, NodeProperties)
                ProgressBar1.Maximum += 1
                ProgressBar1.Value += 1
                If Expandable(ChildNodeProps.FileType) Then
                    CheckFile(ChildNode, Crawl)
                End If
            Next
        End If
        HostNode.ImageIndex = GetImageIndex(NodeTag.FileType)
        HostNode.SelectedImageIndex = HostNode.ImageIndex
        HostNode.Tag = NodeTag
    End Sub
    Function CheckHeaderType(Index As Long, ByVal ByteArray As Byte()) As PackageType
        'To be split into 2 seperate functions once all processes are added
        Dim FirstFour As String
        'Make sure the file has bytes
        If ByteArray.Length = 0 Then
            Return PackageType.bin
        End If
        If Index > ByteArray.Length Then
            Return PackageType.bin
        End If
        FirstFour = Encoding.Default.GetChars(ByteArray, Index, 4)
        Select Case FirstFour
            Case "HSPC"
                Return PackageType.HSPC
            Case "SHDC"
                Return PackageType.SHDC
            Case "EPK8"
                Return PackageType.EPK8
            Case "PACH"
                Return PackageType.PACH
            Case "EPAC"
                Return PackageType.EPAC
            Case "ZLIB"
                Return PackageType.ZLIB
            Case "YOBJ"
                Return PackageType.YOBJ
            Case "JBOY"
                Return PackageType.YOBJ
            Case "NIBJ"
                Return PackageType.NIBJ
            Case "0FOP"
                Return PackageType.OFOP
            Case "YANM"
                Return PackageType.YANM
            Case "OODL"
                Return PackageType.OODL
            Case "VMUM"
                Return PackageType.VMUM
            Case "DUMY"
                Return PackageType.DUMY
            Case Else
                'if we don't have a perfect 4 match we go to check the 3 character matches
                Select Case True
                    Case FirstFour.Contains("STG")
                        Return PackageType.ShowInfo
                    Case FirstFour.Contains("DDS")
                        Return PackageType.DDS
                    Case FirstFour.Contains("KB2")
                        Return PackageType.bk2
                    Case FirstFour.Contains("COS")
                        Return PackageType.CostumeFile
                    Case FirstFour.Contains("BPE")
                        Return PackageType.BPE
                    Case FirstFour.Contains("ê¡Y")
                        Return PackageType.YOBJArray
                    Case FirstFour.Contains(" ¯0")
                        Return PackageType.TPL
                    Case Else
                        'if we do not have a header text to guide us we have some additional text checks that are consistent.
                        'some of these checks don't 100% require this many bytes, but none should functionally have that little byte length
                        If ByteArray.Length > &H30 Then
                            Select Case True
                                Case Encoding.Default.GetChars(ByteArray, Index + &H10, 4) = "aren"
                                    Return PackageType.ArenaInfo
                                Case Encoding.Default.GetChars(ByteArray, Index + &H14, 6) = "M_Head"
                                    Return PackageType.MaskFile
                                Case Encoding.Default.GetChars(ByteArray, Index + &H14, 6) = "M_Body"
                                    Return PackageType.MaskFile
                                Case BitConverter.ToUInt32(ByteArray, Index + &H14) = 2004 'this is an unsafe check.  It just works for all known games so far.
                                    Return PackageType.TitleFile
                                Case Encoding.Default.GetChars(ByteArray, Index + &H18, 3) = "yM_"
                                    Return PackageType.MuscleFile
                                Case LibraryContainsList.Contains(Encoding.Default.GetChars(ByteArray, Index + &H20, 3)) 'ufc games can have additional file types  create a short list of 3 char codes to test
                                    Return PackageType.TextureLibrary
                                Case Encoding.Default.GetChars(ByteArray, Index + &H24, 4) = "root"
                                    Return PackageType.YANMPack
                                Case ActiveFile.ToLower.Contains("string")
                                    Dim NumberCheck As UInt32 = BitConverter.ToUInt32(ByteArray, Index + 0)
                                    If NumberCheck = 0 Then
                                        Return PackageType.StringFile
                                    Else
                                        Return PackageType.bin
                                    End If
                                Case Else
                                    Return PackageType.bin
                            End Select
                        Else
                            Return PackageType.bin
                        End If
                End Select
        End Select
    End Function
    Function Expandable(TestType As PackageType) As Boolean
        If TestType = PackageType.Unchecked OrElse
           TestType = PackageType.Folder OrElse
           TestType = PackageType.HSPC OrElse
           TestType = PackageType.EPK8 OrElse
           TestType = PackageType.EPAC OrElse
           TestType = PackageType.SHDC OrElse
           TestType = PackageType.PACH OrElse
            TestType = PackageType.BPE OrElse
           TestType = PackageType.ZLIB OrElse
           TestType = PackageType.OODL OrElse
           TestType = PackageType.PachDirectory_4 OrElse
           TestType = PackageType.PachDirectory_8 OrElse
           TestType = PackageType.TextureLibrary OrElse
           TestType = PackageType.YANMPack OrElse
           TestType = PackageType.YOBJ Then
            Return True
        End If
        Return False
    End Function
    Function GetNodeBytes(ByRef HostNode As TreeNode) As Byte()
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(HostNode.Tag, NodeProperties)
        Dim FileBytes As Byte() = New Byte(NodeTag.length - 1) {}
        If NodeTag.StoredData.Length > 0 Then
            Array.Copy(NodeTag.StoredData, CInt(NodeTag.Index), FileBytes, 0, CInt(NodeTag.length))
        ElseIf NodeTag.length > 0 Then
            If Not File.Exists(HostNode.ToolTipText) Then
                MessageBox.Show("File Not Found")
                Return New Byte() {}
            End If
            Try
                Dim ActiveReader As BinaryReader = New BinaryReader(File.Open(HostNode.ToolTipText, FileMode.Open, FileAccess.Read))
                ActiveReader.BaseStream.Seek(NodeTag.Index, SeekOrigin.Begin)
                FileBytes = ActiveReader.ReadBytes(NodeTag.length)
                ActiveReader.Dispose()
                ' MessageBox.Show(NodeTag.Index & vbNewLine & NodeTag.length)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else Return New Byte() {}
        End If
        Return FileBytes
    End Function
    Function GetImageIndex(SentType As PackageType) As UInt32
        Select Case SentType
            Case PackageType.Folder
                Return 1
            Case PackageType.HSPC
                Return 2
            Case PackageType.EPK8
                Return 3
            Case PackageType.EPAC
                Return 4
            Case PackageType.SHDC
                Return 5
            Case PackageType.PACH
                Return 6
            Case PackageType.PachDirectory_4
                Return 6
            Case PackageType.PachDirectory_8
                Return 6
            Case PackageType.BPE
                Return 7
            Case PackageType.ZLIB
                Return 8
            Case PackageType.OODL
                Return 9
            Case PackageType.TextureLibrary
                Return 10
            Case PackageType.TPL
                Return 10
            Case PackageType.YANMPack
                Return 11
            Case PackageType.YANM
                Return 11
            Case PackageType.OFOP
                Return 11
            Case PackageType.YOBJ
                Return 12
            Case PackageType.YOBJArray
                Return 12
            Case PackageType.StringFile
                Return 13
            Case PackageType.DDS
                Return 14
            Case PackageType.DUMY
                Return 14
            Case PackageType.ArenaInfo
                Return 15
            Case PackageType.ShowInfo
                Return 16
            Case PackageType.NIBJ
                Return 17
            Case PackageType.bk2
                Return 18
            Case PackageType.CostumeFile
                Return 19
            Case PackageType.MuscleFile
                Return 20
            Case PackageType.UFC_PAC
                Return 20
            Case PackageType.UFC_BIN
                Return 20
            Case PackageType.UFC_CVX
                Return 20
            Case PackageType.UFC_HKX
                Return 20
            Case PackageType.UFC_MAP
                Return 20
            Case PackageType.UFC_TXT
                Return 20
            Case PackageType.MaskFile
                Return 21
            Case PackageType.VMUM
                Return 22
            Case PackageType.TitleFile
                Return 23
            Case Else
                Return 0
        End Select

    End Function
#End Region
#Region "Menu Strip"
    Private Sub LoadHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadHomeToolStripMenuItem.Click
        LoadHome()
    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileOrFolderDialog.ShowDialog = DialogResult.OK Then
            Dim DialogSelection As String() = OpenFileOrFolderDialog.FileNames
            For i As Integer = 0 To DialogSelection.Count - 1
                DialogSelection(i) = DialogSelection(i).Replace("Select Items", "")
            Next '"Select Items"
            SelectedFiles = DialogSelection
            LoadParameters()
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub
    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        OptionsMenu.Show() '
    End Sub
#Region "Tool Toolstrip"
#Region "Tool Compression Toolstrip"
    Private Sub BPEBatchCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BPEBatchCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Multiselect = True,
            .FileName = "File(s) to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim SelectedFiles As String() = CompressOpenFileDialog.FileNames
            For i As Integer = 0 To SelectedFiles.Count - 1
                CompressBPEToFile(SelectedFiles(i))
            Next
            MessageBox.Show("Compression Complete")
        End If
    End Sub
    Private Sub BPESingleCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BPESingleCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "File to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim CompressSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
                .InitialDirectory = Path.GetDirectoryName(CompressOpenFileDialog.FileName),
                .FileName = Path.GetFileNameWithoutExtension(CompressOpenFileDialog.FileName) & ".bpe",
                .Title = "Save File Locaiton"}
            If CompressSaveFileDialog.ShowDialog() = DialogResult.OK Then
                If CompressBPEToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
                    MessageBox.Show("Compression Complete")
                End If
            End If
        End If
    End Sub
    Function CompressBPEToFile(FiletoRead As String, Optional FiletoWrite As String = "")
        If File.Exists(FiletoRead) Then
            Dim FileBytes As Byte() = File.ReadAllBytes(FiletoRead)
            Dim ExistingFileType As PackageType = CheckHeaderType(0, FileBytes)
            If ExistingFileType = PackageType.BPE Then
                MessageBox.Show("File is already a BPE")
            Else
                Dim BytesToCompress As Byte() = Nothing
                If ExistingFileType = PackageType.OODL Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as OODL" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedOodleBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                ElseIf ExistingFileType = PackageType.ZLIB Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as ZLIB" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedZlibBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                Else
                    BytesToCompress = FileBytes
                End If
                Dim CompressedBytes As Byte() = Nothing
                CompressedBytes = GetCompressedBPEBytes(BytesToCompress)
                If IsNothing(CompressedBytes) Then
                    MessageBox.Show("Failure to get compressed bytes for " & Path.GetFileName(FiletoRead))
                    Return False
                Else
                    'Write bytes to file
                    Dim SaveLocation As String = ""
                    If FiletoWrite = "" Then
                        SaveLocation = Path.GetDirectoryName(FiletoRead) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(FiletoRead) & ".bpe"
                        If File.Exists(SaveLocation) Then
                            'We need to Backup the file in this case... this really shouldn't happen because bpe is skipped
                            File.Move(SaveLocation, SaveLocation.Replace(".bpe", ".bpe.bak"))
                        End If
                    Else
                        SaveLocation = FiletoWrite
                    End If
                    File.WriteAllBytes(SaveLocation, CompressedBytes)
                    Return True
                End If
            End If
        Else
            MessageBox.Show("File " & Path.GetFileName(FiletoRead) & " Does Not Exist")
        End If
        Return False
    End Function
    Private Sub ZLIBBatchCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZLIBBatchCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Multiselect = True,
            .FileName = "File(s) to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim SelectedFiles As String() = CompressOpenFileDialog.FileNames
            For i As Integer = 0 To SelectedFiles.Count - 1
                CompressZLIBToFile(SelectedFiles(i))
            Next
            MessageBox.Show("Compression Complete")
        End If
    End Sub
    Private Sub ZLIBSingleCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZLIBSingleCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "File to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim CompressSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
                .InitialDirectory = Path.GetDirectoryName(CompressOpenFileDialog.FileName),
                .FileName = Path.GetFileNameWithoutExtension(CompressOpenFileDialog.FileName) & ".zlib",
                .Title = "Save File Locaiton"}
            If CompressSaveFileDialog.ShowDialog() = DialogResult.OK Then
                If CompressZLIBToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
                    MessageBox.Show("Compression Complete")
                End If
            End If
        End If
    End Sub
    Function CompressZLIBToFile(FiletoRead As String, Optional FiletoWrite As String = "")
        If File.Exists(FiletoRead) Then
            Dim FileBytes As Byte() = File.ReadAllBytes(FiletoRead)
            Dim ExistingFileType As PackageType = CheckHeaderType(0, FileBytes)
            If ExistingFileType = PackageType.ZLIB Then
                MessageBox.Show("File is already a ZLIB")
            Else
                Dim BytesToCompress As Byte() = Nothing
                If ExistingFileType = PackageType.OODL Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as OODL" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedOodleBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                ElseIf ExistingFileType = PackageType.BPE Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as BPE" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedBPEBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                Else
                    BytesToCompress = FileBytes
                End If
                Dim CompressedBytes As Byte() = Nothing
                CompressedBytes = GetCompressedZlibBytes(BytesToCompress)
                If IsNothing(CompressedBytes) Then
                    MessageBox.Show("Failure to get compressed bytes for " & Path.GetFileName(FiletoRead))
                    Return False
                Else
                    'Write bytes to file
                    Dim SaveLocation As String = ""
                    If FiletoWrite = "" Then
                        SaveLocation = Path.GetDirectoryName(FiletoRead) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(FiletoRead) & ".zlib"
                        If File.Exists(SaveLocation) Then
                            'We need to Backup the file in this case... this really shouldn't happen because bpe is skipped
                            File.Move(SaveLocation, SaveLocation.Replace(".zlib", ".zlib.bak"))
                        End If
                    Else
                        SaveLocation = FiletoWrite
                    End If
                    File.WriteAllBytes(SaveLocation, CompressedBytes)
                    Return True
                End If
            End If
        Else
            MessageBox.Show("File " & Path.GetFileName(FiletoRead) & " Does Not Exist")
        End If
        Return False
    End Function
    Private Sub OODLBatchCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OODLBatchCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Multiselect = True,
            .FileName = "File(s) to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim SelectedFiles As String() = CompressOpenFileDialog.FileNames
            For i As Integer = 0 To SelectedFiles.Count - 1
                CompressOODLToFile(SelectedFiles(i))
            Next
            MessageBox.Show("Compression Complete")
        End If
    End Sub
    Private Sub OODLSingleCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OODLSingleCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "File to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim CompressSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
                .InitialDirectory = Path.GetDirectoryName(CompressOpenFileDialog.FileName),
                .FileName = Path.GetFileNameWithoutExtension(CompressOpenFileDialog.FileName) & ".oodl",
                .Title = "Save File Locaiton"}
            If CompressSaveFileDialog.ShowDialog() = DialogResult.OK Then
                If CompressOODLToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
                    MessageBox.Show("Compression Complete")
                End If
            End If
        End If
    End Sub
    Function CompressOODLToFile(FiletoRead As String, Optional FiletoWrite As String = "")
        If File.Exists(FiletoRead) Then
            Dim FileBytes As Byte() = File.ReadAllBytes(FiletoRead)
            Dim ExistingFileType As PackageType = CheckHeaderType(0, FileBytes)
            If ExistingFileType = PackageType.OODL Then
                MessageBox.Show("File is already a ZLIB")
            Else
                Dim BytesToCompress As Byte() = Nothing
                If ExistingFileType = PackageType.ZLIB Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as ZLIB" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedZlibBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                ElseIf ExistingFileType = PackageType.BPE Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as BPE" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedBPEBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                Else
                    BytesToCompress = FileBytes
                End If
                Dim CompressedBytes As Byte() = Nothing
                CompressedBytes = GetCompressedOodleBytes(BytesToCompress)
                If IsNothing(CompressedBytes) Then
                    MessageBox.Show("Failure to get compressed bytes for " & Path.GetFileName(FiletoRead))
                    Return False
                Else
                    'Write bytes to file
                    Dim SaveLocation As String = ""
                    If FiletoWrite = "" Then
                        SaveLocation = Path.GetDirectoryName(FiletoRead) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(FiletoRead) & ".oodl"
                        If File.Exists(SaveLocation) Then
                            'We need to Backup the file in this case... this really shouldn't happen because bpe is skipped
                            File.Move(SaveLocation, SaveLocation.Replace(".oodl", ".oodl.bak"))
                        End If
                    Else
                        SaveLocation = FiletoWrite
                    End If
                    File.WriteAllBytes(SaveLocation, CompressedBytes)
                    Return True
                End If
            End If
        Else
            MessageBox.Show("File " & Path.GetFileName(FiletoRead) & " Does Not Exist")
        End If
        Return False
    End Function
#End Region
    Private Sub GenerateFileNameHashToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateFileNameHashToolStripMenuItem.Click
        Dim TextDialogInstance As New TextDialogPrompt With {
                .ContainerBeingEdited = PackageType.bin,
                .OldFileName = "hash"}
        TextDialogInstance.ShowDialog()
        If TextDialogInstance.Result = DialogResult.OK Then
            If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                Dim NewFileName As String = TextDialogInstance.EditedFileName
                Dim HashedName As String = HashGenerator.GetMd5Hash(NewFileName, True)
                MessageBox.Show(HashedName, "File Hash")
            End If
        End If
        TextDialogInstance.Dispose()
    End Sub
#End Region
#Region "HelpToolStrip"
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Process.Start("https://pozzum.github.io/WrestleMINUS/")
    End Sub
    Private Sub SupportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupportToolStripMenuItem.Click
        Process.Start("https://smacktalks.org/forums/topic/70048-wrestleminus/")
    End Sub
    Private Sub GitHubIssuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GitHubIssuesToolStripMenuItem.Click
        Process.Start("https://github.com/pozzum/WrestleMINUS/issues")
    End Sub
#End Region
#End Region
    'Some Issue with Menus not properly populating.. when you first select Text View..
#Region "TreeView Population"
    Sub LoadParameters()
        InformationLoaded = False
        TreeView1.Nodes.Clear()
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = SelectedFiles.Length
        If SelectedFiles.Length = 1 Then
            CurrentViewToolStripMenuItem.Text = "Current View: " & Path.GetFileName(SelectedFiles(0))
        Else
            CurrentViewToolStripMenuItem.Text = "Current View: Multiple Files"
        End If
        For i As Integer = 0 To SelectedFiles.Length - 1
            If Not SelectedFiles(i) = "" Then
                If File.Exists(SelectedFiles(i)) Then
                    Dim NewFI As FileInfo = New FileInfo(SelectedFiles(i))
                    Dim TempNode As TreeNode = TreeView1.Nodes.Add(NewFI.Name)
                    TempNode.ToolTipText = NewFI.FullName
                    TempNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                        .Index = 0,
                        .length = FileLen(NewFI.FullName),
                        .StoredData = New Byte() {}}
                    TempNode.ImageIndex = 0
                    TempNode.SelectedImageIndex = 0
                    CheckFile(TempNode)
                    ProgressBar1.Value += 1
                ElseIf Directory.Exists(SelectedFiles(i)) Then
                    Dim TempDI As DirectoryInfo = New DirectoryInfo(SelectedFiles(i))
                    Dim TempNode As TreeNode = TreeView1.Nodes.Add(TempDI.Name)
                    TempNode.ToolTipText = TempDI.FullName
                    TempNode.Tag = New NodeProperties With {.FileType = PackageType.Folder,
                            .Index = 0,
                            .length = 0,
                            .StoredData = New Byte(0) {}}
                    TempNode.ImageIndex = 1
                    TempNode.SelectedImageIndex = 1
                    LoadSubDirectories(SelectedFiles(i), TempNode)
                    LoadFiles(SelectedFiles(i), TempNode)
                End If
            End If
        Next
    End Sub
    Sub LoadHome()
        InformationLoaded = False
        If Not My.Settings.ExeLocation = "" Then
            TreeView1.Nodes.Clear()
            ProgressBar1.Value = 0
            Dim HomeDirectory As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar
            CurrentViewToolStripMenuItem.Text = "Current View: " & HomeDirectory
            Dim HomeDI As DirectoryInfo = New DirectoryInfo(HomeDirectory)
            ProgressBar1.Maximum = Directory.GetFiles(HomeDirectory, "*.*", SearchOption.AllDirectories).Length +
                                    Directory.GetDirectories(HomeDirectory, "**", SearchOption.AllDirectories).Length
            Dim TempNode As TreeNode = TreeView1.Nodes.Add(HomeDI.Name)
            TempNode.ToolTipText = HomeDI.FullName
            TempNode.Tag = New NodeProperties With {.FileType = PackageType.Folder,
                    .Index = 0,
                    .length = 0,
                    .StoredData = New Byte(0) {}}
            TempNode.ImageIndex = 1
            TempNode.SelectedImageIndex = 1
            'testing having folders first
            LoadSubDirectories(HomeDirectory, TempNode)
            LoadFiles(HomeDirectory, TempNode)
        Else
            If MessageBox.Show("No Home Directory Selected." & vbNewLine &
                             "Select Home Now?", "Select Home?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                SelectHomeDirectory()
            End If
        End If
    End Sub
    Sub LoadSubDirectories(DirectoryPath As String, ParentNode As TreeNode)
        Dim ListofSubDirectores() As String = Directory.GetDirectories(DirectoryPath)
        For Each SubDirectory As String In ListofSubDirectores
            Dim NewDI As DirectoryInfo = New DirectoryInfo(SubDirectory)
            Dim TempNode As TreeNode = ParentNode.Nodes.Add(NewDI.Name)
            TempNode.ToolTipText = NewDI.FullName
            TempNode.Tag = New NodeProperties With {.FileType = PackageType.Folder,
                .Index = 0,
                .length = 0,
                .StoredData = New Byte(0) {}
            }
            TempNode.ImageIndex = 1
            TempNode.SelectedImageIndex = 1
            LoadSubDirectories(SubDirectory, TempNode)
            LoadFiles(SubDirectory, TempNode)
            UpdateProgress()
        Next
    End Sub
    Sub LoadFiles(DirectoryPath As String, ParentNode As TreeNode)
        Dim ListofFiles() As String = Directory.GetFiles(DirectoryPath)
        For Each FilePath As String In ListofFiles
            Dim NewFI As FileInfo = New FileInfo(FilePath)
            Dim TempNode As TreeNode = ParentNode.Nodes.Add(NewFI.Name)
            TempNode.ToolTipText = NewFI.FullName
            TempNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                .Index = 0,
                .length = FileLen(NewFI.FullName),
                .StoredData = New Byte() {}}
            TempNode.ImageIndex = 0
            TempNode.SelectedImageIndex = 0
            UpdateProgress()
        Next
    End Sub
    Sub UpdateProgress()
        If ProgressBar1.Value < ProgressBar1.Maximum Then
            ProgressBar1.Value += 1
            Dim Percent As Integer = CInt((ProgressBar1.Value / ProgressBar1.Maximum) * 100)
            ProgressBar1.CreateGraphics().DrawString(Percent.ToString() + "%",
                                                     New Font("Arial", 8.25, FontStyle.Regular),
                                                     Brushes.Black,
                                                     New PointF(ProgressBar1.Width / 2 - 10,
                                                                ProgressBar1.Height / 2 - 7))
        End If
    End Sub
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If File.Exists(e.Node.ToolTipText.ToString()) Then
            If Expandable(CType(e.Node.Tag, NodeProperties).FileType) Then
                If e.Node.Nodes.Count = 0 Then
                    CheckFile(e.Node)
                End If
            End If
            Dim PageLoaded As TabPage = GetTabType(CType(e.Node.Tag, NodeProperties).FileType)
            If HideTabs(PageLoaded) = DialogResult.OK Then
                ReadNode = e.Node
                HexViewFileName.Text = TreeView1.SelectedNode.Text
                AddHexText(TreeView1.SelectedNode)
                TextViewFileName.Text = TreeView1.SelectedNode.Text
                AddText(TreeView1.SelectedNode)
                If Not PageLoaded Is Nothing Then
                    LoadTab(PageLoaded)
                End If
            Else
                TreeView1.SelectedNode = ReadNode
            End If
        End If
    End Sub
    'moving functions from on tree view to on tab select to reduce load times during tree movement on keyboard
    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If InformationLoaded = False Then
            InformationLoaded = True
            Select Case e.TabPage.Name
                Case StringView.Name
                    FillStringView(ReadNode)
                    InformationLoaded = False
                Case MiscView.Name
                    FillMiscView(ReadNode)
                Case ShowView.Name
                    FillShowView(ReadNode)
                Case NIBJView.Name
                    FillNIBJView(ReadNode)
                Case PictureView.Name
                    LoadPictureView(ReadNode)
                Case AttireView.Name
                    LoadAttireView(ReadNode)
                Case MuscleView.Name
                    LoadMuscleView(ReadNode)
                Case MaskView.Name
                    LoadMaskView(ReadNode)
                Case ObjArrayView.Name
                    LoadObjectArrayView(ReadNode)
                Case AssetView.Name
                    LoadAssetFileView(ReadNode)
                Case TitleView.Name
                    LoadTitleFileView(ReadNode)
            End Select
        End If
    End Sub
    Function GetTabType(SelectedType As PackageType) As TabPage
        Select Case SelectedType
            Case PackageType.StringFile
                Return StringView
            Case PackageType.ArenaInfo
                Return MiscView
            Case PackageType.ShowInfo
                Return ShowView
            Case PackageType.NIBJ
                Return NIBJView
            Case PackageType.DDS
                Return PictureView
            Case PackageType.YOBJ
                Return ObjectView
            Case PackageType.CostumeFile
                Return AttireView
            Case PackageType.MaskFile
                Return MaskView
            Case PackageType.MuscleFile
                Return MuscleView
            Case PackageType.YOBJArray
                Return ObjArrayView
            Case PackageType.VMUM
                Return AssetView
            Case PackageType.TitleFile
                Return TitleView
            Case Else
                Return Nothing
        End Select
    End Function
    Sub LoadTab(NewTab As TabPage)
        If Not TabControl1.TabPages.Contains(NewTab) Then
            TabControl1.TabPages.Add(NewTab)
            InformationLoaded = False
            If NewTab.Name = MuscleView.Name Then
                LoadMuscleView(ReadNode)
            End If
        ElseIf NewTab.Name = MuscleView.Name Then
            LoadMuscleView(ReadNode)
        End If
    End Sub
#End Region
#Region "Context Menu Strip"
    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If Not e.Button = MouseButtons.Right Then
            Exit Sub
        End If
        TreeView1.SelectedNode = e.Node
        If SavePending Then 'Changing the node should remove the pending save unless it is canceled and then we shouldn't show the strip.
            Exit Sub
        End If
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParentNodeTag As NodeProperties = Nothing
        If IsNothing(TreeView1.SelectedNode.Parent) Then
            ParentNodeTag = Nothing
        Else
            ParentNodeTag = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        End If
        Dim ShownOptions As Integer = TreeViewContext.Items.Count
        'Hide Menu Options when not needed, and if no options are relevent do not show the context strip
        If NodeTag.FileType = PackageType.Folder Then
            ExtractPartToToolStripMenuItem.Visible = False
            InjectToolStripMenuItem.Visible = False
            InjectUncompressedToolStripMenuItem.Visible = False
            InjectBPEToolStripMenuItem.Visible = False
            InjectZLIBToolStripMenuItem.Visible = False
            InjectOODLToolStripMenuItem.Visible = False
            OpenToolStripMenuItem1.Visible = False
            OpenWithToolStripMenuItem.Visible = False
            RenamePartToolStripMenuItem.Visible = False
            RenameFileToolStripMenuItem.Visible = True
            ShownOptions -= 4
        Else
            If NodeTag.Index > 0 OrElse
                       NodeTag.StoredData.Length > 0 Then
                ExtractToolStripMenuItem.Visible = True
                ExtractPartToToolStripMenuItem.Visible = True 'injection is still in progess
                If ParentNodeTag.FileType = PackageType.BPE Then
                    InjectToolStripMenuItem.Visible = False
                    InjectUncompressedToolStripMenuItem.Visible = False
                    InjectZLIBToolStripMenuItem.Visible = False
                    InjectOODLToolStripMenuItem.Visible = False
                    RenamePartToolStripMenuItem.Visible = False
                    RenameFileToolStripMenuItem.Visible = False
                    ShownOptions -= 3
                    If My.Settings.BPEExePath <> "Not Installed" Then
                        InjectBPEToolStripMenuItem.Visible = True
                    Else
                        InjectBPEToolStripMenuItem.Visible = True
                        ShownOptions -= 1
                    End If
                ElseIf ParentNodeTag.FileType = PackageType.ZLIB Then
                    InjectToolStripMenuItem.Visible = False
                    InjectUncompressedToolStripMenuItem.Visible = False
                    InjectBPEToolStripMenuItem.Visible = False
                    InjectZLIBToolStripMenuItem.Visible = True
                    InjectOODLToolStripMenuItem.Visible = False
                    RenamePartToolStripMenuItem.Visible = False
                    RenameFileToolStripMenuItem.Visible = False
                    ShownOptions -= 3
                ElseIf ParentNodeTag.FileType = PackageType.OODL Then
                    InjectToolStripMenuItem.Visible = False
                    InjectUncompressedToolStripMenuItem.Visible = False
                    InjectBPEToolStripMenuItem.Visible = False
                    InjectZLIBToolStripMenuItem.Visible = False
                    RenamePartToolStripMenuItem.Visible = False
                    RenameFileToolStripMenuItem.Visible = False
                    ShownOptions -= 3
                    If CheckOodle() Then
                        InjectOODLToolStripMenuItem.Visible = True
                    Else
                        InjectOODLToolStripMenuItem.Visible = False
                        ShownOptions -= 1
                    End If
                Else
                    InjectToolStripMenuItem.Visible = True
                    InjectUncompressedToolStripMenuItem.Visible = True
                    InjectZLIBToolStripMenuItem.Visible = True
                    RenamePartToolStripMenuItem.Visible = True
                    RenameFileToolStripMenuItem.Visible = False
                    If My.Settings.BPEExePath <> "Not Installed" Then
                        InjectBPEToolStripMenuItem.Visible = True
                    Else
                        InjectBPEToolStripMenuItem.Visible = True
                        ShownOptions -= 1
                    End If
                    If CheckOodle() Then
                        InjectOODLToolStripMenuItem.Visible = True
                    Else
                        InjectOODLToolStripMenuItem.Visible = False
                        ShownOptions -= 1
                    End If
                End If
            Else
                ExtractToolStripMenuItem.Visible = False
                ExtractPartToToolStripMenuItem.Visible = False
                InjectToolStripMenuItem.Visible = False
                InjectUncompressedToolStripMenuItem.Visible = False
                InjectBPEToolStripMenuItem.Visible = False
                InjectZLIBToolStripMenuItem.Visible = False
                InjectOODLToolStripMenuItem.Visible = False
                RenamePartToolStripMenuItem.Visible = False
                RenameFileToolStripMenuItem.Visible = True
                ShownOptions -= 3
            End If
            'TO DO add Open With Items Somehow
            If NodeTag.FileType = PackageType.bk2 AndAlso My.Settings.RADVideoToolPath <> "Not Installed" Then
                OpenToolStripMenuItem1.Visible = True
                OpenWithToolStripMenuItem.Visible = False
                ShownOptions -= 1
            ElseIf NodeTag.FileType = PackageType.DDS Then
                OpenToolStripMenuItem1.Visible = False
                OpenWithToolStripMenuItem.Visible = True
                ShownOptions -= 1
            Else
                OpenToolStripMenuItem1.Visible = False
                OpenWithToolStripMenuItem.Visible = False
                ShownOptions -= 2
            End If
        End If
        If TreeView1.SelectedNode.GetNodeCount(False) > 0 Then
            CrawlToolStripMenuItem.Visible = True
            ExtractToolStripMenuItem.Visible = True
            ExtractAllInPlaceToolStripMenuItem.Visible = True
            ExtractAllToToolStripMenuItem.Visible = True
        Else
            CrawlToolStripMenuItem.Visible = False
            'this should not be removed from this
            'ExtractToolStripMenuItem.Visible = False
            ExtractAllInPlaceToolStripMenuItem.Visible = False
            ExtractAllToToolStripMenuItem.Visible = False
            ShownOptions -= 1
        End If
        If ShownOptions > 0 Then
            TreeViewContext.Show(TreeView1, New Point(e.X, e.Y))
        End If
    End Sub
    Private Sub ExtractPartToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractPartToToolStripMenuItem.Click
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ExtractSaveFileDialog As SaveFileDialog = New SaveFileDialog()
        ExtractSaveFileDialog.InitialDirectory = Path.GetDirectoryName(TreeView1.SelectedNode.ToolTipText)
        Dim FileExtention As String = ".bin"
        If My.Settings.UseDetailedFileNames Then
            FileExtention = "." & NodeTag.FileType.ToString
        End If
        ExtractSaveFileDialog.FileName = TreeView1.SelectedNode.Text & FileExtention
        If ExtractSaveFileDialog.ShowDialog() = DialogResult.OK Then
            ExtractNode(TreeView1.SelectedNode, ExtractSaveFileDialog.FileName)
        End If
    End Sub
    Private Sub OpenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem1.Click
        'Currently Only Opens Bink Files
        Dim TronBytes As Byte() = GetNodeBytes(TreeView1.SelectedNode)
        Dim filepath As String = Path.GetTempFileName
        File.WriteAllBytes(filepath, TronBytes)
        Process.Start(My.Settings.RADVideoToolPath, filepath)
    End Sub
    Private Sub OpenWithToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenWithToolStripMenuItem.Click
        If My.Settings.DDSexeLocation = "Not Installed" Then
            CheckDDSexe(True)
        Else
            'Currently Only Designed for DDS Files
            Dim DDSBytes As Byte() = GetNodeBytes(TreeView1.SelectedNode)
            Dim filepath As String = Path.GetTempPath & Guid.NewGuid().ToString() & ".dds"
            File.WriteAllBytes(filepath, DDSBytes)
            Process.Start(My.Settings.DDSexeLocation, filepath)
        End If
    End Sub
    Private Sub CrawlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrawlToolStripMenuItem.Click
        Crawlnode(TreeView1.SelectedNode) 'Crawls All of the Nodes and then expands the node
        TreeView1.SelectedNode.ExpandAll()
    End Sub
    'Handles ExtractPartToToolStripMenuItem.Click
    Private Sub ExtractAllInPlaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractAllInPlaceToolStripMenuItem.Click
        Crawlnode(TreeView1.SelectedNode) 'crawls the node first so all of the files to be extracted are located
        If CType(TreeView1.SelectedNode.Tag, NodeProperties).FileType = PackageType.Folder Then 'if a folder used the folder
            ExtractAllNode(TreeView1.SelectedNode,
                           TreeView1.SelectedNode.ToolTipText &
                           Path.DirectorySeparatorChar)
        Else 'otherwise use the folder that the file is in with a new folder using that file name
            ExtractAllNode(TreeView1.SelectedNode,
                           Path.GetDirectoryName(TreeView1.SelectedNode.ToolTipText) & Path.DirectorySeparatorChar &
                           Path.GetFileNameWithoutExtension(TreeView1.SelectedNode.ToolTipText) & Path.DirectorySeparatorChar)
        End If
    End Sub
    Private Sub ExtractAllToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractAllToToolStripMenuItem.Click
        SaveExtractAllDialog.InitialDirectory = Path.GetDirectoryName(TreeView1.SelectedNode.ToolTipText)
        If SaveExtractAllDialog.ShowDialog() = DialogResult.OK Then
            Crawlnode(TreeView1.SelectedNode)
            ExtractAllNode(TreeView1.SelectedNode,
                           (Path.GetDirectoryName(SaveExtractAllDialog.FileName) & Path.DirectorySeparatorChar))
        End If
    End Sub
    Private Sub InjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectUncompressedToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParrentNodeTag As NodeProperties = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        If ParrentNodeTag.FileType = PackageType.HSPC OrElse
            ParrentNodeTag.FileType = PackageType.SHDC OrElse
            ParrentNodeTag.FileType = PackageType.EPK8 OrElse
            ParrentNodeTag.FileType = PackageType.EPAC OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_8 OrElse
            ParrentNodeTag.FileType = PackageType.TextureLibrary Then 'Hopefully this can expand to all
            Dim injectopenfile As OpenFileDialog = New OpenFileDialog With {
            .FileName = TreeView1.SelectedNode.Text & "." & NodeTag.FileType.ToString,
            .InitialDirectory = Path.GetDirectoryName(filepath)}
            If injectopenfile.ShowDialog() = DialogResult.OK Then
                If File.Exists(injectopenfile.FileName) Then
                    Dim FileBytes As Byte() = File.ReadAllBytes(injectopenfile.FileName)
                    If NodeTag.FileType = PackageType.SHDC Then
                        ReDim Preserve FileBytes(FileBytes.Length + (FileBytes.Length Mod &H100) - 1)
                    End If
                    InjectIntoNode(TreeView1.SelectedNode, FileBytes)
                Else
                    MessageBox.Show("File Does Not Exist")
                End If
            End If
        Else
            MessageBox.Show("Not Yet Supported")
        End If
    End Sub
    Private Sub InjectBPEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectBPEToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParrentNodeTag As NodeProperties = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        If ParrentNodeTag.FileType = PackageType.HSPC OrElse
            ParrentNodeTag.FileType = PackageType.SHDC OrElse
            ParrentNodeTag.FileType = PackageType.EPK8 OrElse
            ParrentNodeTag.FileType = PackageType.EPAC OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_8 OrElse
            ParrentNodeTag.FileType = PackageType.TextureLibrary Then 'Hopefully this can expand to all
            Dim injectopenfile As OpenFileDialog = New OpenFileDialog With {
            .FileName = TreeView1.SelectedNode.Text & "." & NodeTag.FileType.ToString,
            .InitialDirectory = Path.GetDirectoryName(filepath)}
            If injectopenfile.ShowDialog() = DialogResult.OK Then
                If File.Exists(injectopenfile.FileName) Then
                    Dim FileBytes As Byte() = File.ReadAllBytes(injectopenfile.FileName)
                    Dim CompressedBytes As Byte() = Nothing
                    CompressedBytes = GetCompressedBPEBytes(FileBytes)
                    If IsNothing(CompressedBytes) Then
                        MessageBox.Show("Failure to get Compressed Bytes")
                        Exit Sub
                    End If
                    If ParrentNodeTag.FileType = PackageType.BPE Then 'Hopefully this can expand to all
                        InjectIntoNode(TreeView1.SelectedNode.Parent, CompressedBytes)
                    Else
                        InjectIntoNode(TreeView1.SelectedNode, CompressedBytes)
                    End If
                Else
                    MessageBox.Show("File Does Not Exist")
                End If
            End If
        Else
            MessageBox.Show("Not Yet Supported")
        End If
    End Sub
    Private Sub InjectZLIBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectZLIBToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParrentNodeTag As NodeProperties = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        If ParrentNodeTag.FileType = PackageType.HSPC OrElse
            ParrentNodeTag.FileType = PackageType.SHDC OrElse
            ParrentNodeTag.FileType = PackageType.EPK8 OrElse
            ParrentNodeTag.FileType = PackageType.EPAC OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_8 OrElse
            ParrentNodeTag.FileType = PackageType.TextureLibrary Then 'Hopefully this can expand to all
            Dim injectopenfile As OpenFileDialog = New OpenFileDialog With {
            .FileName = TreeView1.SelectedNode.Text & "." & NodeTag.FileType.ToString,
            .InitialDirectory = Path.GetDirectoryName(filepath)}
            If injectopenfile.ShowDialog() = DialogResult.OK Then
                If File.Exists(injectopenfile.FileName) Then
                    Dim FileBytes As Byte() = File.ReadAllBytes(injectopenfile.FileName)
                    Dim CompressedBytes As Byte() = Nothing
                    CompressedBytes = GetCompressedZlibBytes(FileBytes)
                    If IsNothing(CompressedBytes) Then
                        MessageBox.Show("Failure to get Compressed Bytes")
                        Exit Sub
                    End If
                    If ParrentNodeTag.FileType = PackageType.ZLIB Then 'Hopefully this can expand to all
                        InjectIntoNode(TreeView1.SelectedNode.Parent, CompressedBytes)
                    Else
                        InjectIntoNode(TreeView1.SelectedNode, CompressedBytes)
                    End If
                Else
                    MessageBox.Show("File Does Not Exist")
                End If
            End If
        Else
            MessageBox.Show("Not Yet Supported")
        End If
    End Sub
    Private Sub InjectOODLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectOODLToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParrentNodeTag As NodeProperties = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        If ParrentNodeTag.FileType = PackageType.HSPC OrElse
            ParrentNodeTag.FileType = PackageType.SHDC OrElse
            ParrentNodeTag.FileType = PackageType.EPK8 OrElse
            ParrentNodeTag.FileType = PackageType.EPAC OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_8 OrElse
            ParrentNodeTag.FileType = PackageType.TextureLibrary Then 'Hopefully this can expand to all
            Dim injectopenfile As OpenFileDialog = New OpenFileDialog With {
            .FileName = TreeView1.SelectedNode.Text & "." & NodeTag.FileType.ToString,
            .InitialDirectory = Path.GetDirectoryName(filepath)}
            If injectopenfile.ShowDialog() = DialogResult.OK Then
                If File.Exists(injectopenfile.FileName) Then
                    Dim FileBytes As Byte() = File.ReadAllBytes(injectopenfile.FileName)
                    Dim CompressedBytes As Byte() = Nothing
                    CompressedBytes = GetCompressedOodleBytes(FileBytes)
                    If IsNothing(CompressedBytes) Then
                        MessageBox.Show("Failure to get Compressed Bytes")
                        Exit Sub
                    End If
                    If ParrentNodeTag.FileType = PackageType.OODL Then 'Hopefully this can expand to all
                        InjectIntoNode(TreeView1.SelectedNode.Parent, CompressedBytes)
                    Else
                        InjectIntoNode(TreeView1.SelectedNode, CompressedBytes)
                    End If
                Else
                    MessageBox.Show("File Does Not Exist")
                End If
            End If
        Else
            MessageBox.Show("Not Yet Supported")
        End If
    End Sub
    Sub Crawlnode(Basenode As TreeNode)
        Dim filepath As String = Basenode.ToolTipText
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = Basenode.GetNodeCount(False)
        For Each TestedNode As TreeNode In Basenode.Nodes
            If Expandable(CType(TestedNode.Tag, NodeProperties).FileType) Then
                CheckFile(TestedNode, True)
            End If
            ProgressBar1.Value += 1
        Next
        ProgressBar1.Value = ProgressBar1.Maximum
    End Sub
    Private Sub RenamePartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenamePartToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParrentNodeTag As NodeProperties = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        If ParrentNodeTag.FileType = PackageType.HSPC OrElse
            ParrentNodeTag.FileType = PackageType.SHDC OrElse
            ParrentNodeTag.FileType = PackageType.EPK8 OrElse
            ParrentNodeTag.FileType = PackageType.EPAC OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory_8 OrElse
            ParrentNodeTag.FileType = PackageType.TextureLibrary Then 'Hopefully this can expand to all
            'Here we want TextDialogInstance
            Dim TextDialogInstance As New TextDialogPrompt With {
                .OldFileName = TreeView1.SelectedNode.Text,
                .EditedFileName = .OldFileName,
                .ContainerBeingEdited = ParrentNodeTag.FileType}
            TextDialogInstance.ShowDialog()
            If TextDialogInstance.Result = DialogResult.OK Then
                If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                    Dim NewFileName As String = TextDialogInstance.EditedFileName
                    NewFileName = ValidateTruncation(NewFileName, ParrentNodeTag.FileType)
                    'MessageBox.Show("Changed Name to " & NewFileName)
                    'here we have to check if it already has that same file name
                    Dim NodeSet As TreeNodeCollection = TreeView1.SelectedNode.Parent.Nodes
                    Dim NameMatched As Boolean = False
                    For i As Integer = 0 To NodeSet.Count - 1
                        'MessageBox.Show(NodeSet(i).Text)
                        If NodeSet(i).Text = NewFileName Then
                            NameMatched = True
                        End If
                    Next
                    If Not NameMatched Then
                        RenameNode(TreeView1.SelectedNode, NewFileName)
                        MessageBox.Show("Part Renamed")
                    Else
                        MessageBox.Show("File Already Contains a file named " & NewFileName)
                    End If
                End If
            End If
            TextDialogInstance.Dispose()
        Else
            MessageBox.Show("Not Yet Supported")
        End If
    End Sub
    'If File.Exists(SelectedFiles(i)) Then
    'Dim NewFI As FileInfo = New FileInfo(SelectedFiles(i))
    'Dim TempNode As TreeNode = TreeView1.Nodes.Add(NewFI.Name)
    '                TempNode.ToolTipText = NewFI.FullName
    '                TempNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
    '                    .Index = 0,
    '                    .length = FileLen(NewFI.FullName),
    '                    .StoredData = New Byte() {}}
    '                TempNode.ImageIndex = 0
    '                TempNode.SelectedImageIndex = 0
    '                CheckFile(TempNode)
    '                ProgressBar1.Value += 1
    '            ElseIf Directory.Exists(SelectedFiles(i)) Then
    'Dim TempDI As DirectoryInfo = New DirectoryInfo(SelectedFiles(i))
    'Dim TempNode As TreeNode = TreeView1.Nodes.Add(TempDI.Name)
    '                TempNode.ToolTipText = TempDI.FullName
    '                TempNode.Tag = New NodeProperties With {.FileType = PackageType.Folder,
    '                        .Index = 0,
    '                        .length = 0,
    '                        .StoredData = New Byte(0) {}}
    '                TempNode.ImageIndex = 1
    '                TempNode.SelectedImageIndex = 1
    '                LoadSubDirectories(SelectedFiles(i), TempNode)
    '                LoadFiles(SelectedFiles(i), TempNode)
    '            End If
    Private Sub RenameFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameFileToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        Dim NodeTag As NodeProperties = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        If NodeTag.FileType = PackageType.Folder Then
            If Directory.Exists(filepath) Then
                Dim TextDialogInstance As New TextDialogPrompt With {
                    .OldFileName = TreeView1.SelectedNode.ToolTipText,
                    .EditedFileName = filepath,
                    .ContainerBeingEdited = PackageType.Folder}
                TextDialogInstance.ShowDialog()
                If TextDialogInstance.Result = DialogResult.OK Then
                    If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                        'Folder Editing, MoveAllFiles will be used
                        Dim NewFolderName As String = TextDialogInstance.EditedFileName
                        NewFolderName = ValidateTruncation(NewFolderName, PackageType.Folder)
                        'here we have to check if the folder already exists
                        Dim NameMatched As Boolean = Directory.Exists(NewFolderName)
                        If Not NameMatched Then
                            MoveAllItems(filepath, NewFolderName)
                            MessageBox.Show("Folder Moved")
                            Dim TempDI As DirectoryInfo = New DirectoryInfo(NewFolderName)
                            'resettreebranch
                            SelectedNode.Nodes.Clear()
                            SelectedNode.Text = TempDI.Name
                            SelectedNode.ToolTipText = TempDI.FullName
                            SelectedNode.Tag = New NodeProperties With {.FileType = PackageType.Folder,
                                .Index = 0,
                                .length = 0,
                                .StoredData = New Byte() {}}
                            SelectedNode.ImageIndex = 1
                            SelectedNode.SelectedImageIndex = 1
                            LoadSubDirectories(TempDI.FullName, SelectedNode)
                            LoadFiles(TempDI.FullName, SelectedNode)
                            InformationLoaded = False
                        Else
                            MessageBox.Show("Folder " & NewFolderName & " already exists.", "Rename Failed")
                        End If
                    End If
                End If
                TextDialogInstance.Dispose()
            Else
                    MessageBox.Show("Folder " & filepath & " Not Found")
            End If
        Else 'it should be a file
            If File.Exists(filepath) Then
                Dim TextDialogInstance As New TextDialogPrompt With {
                    .OldFileName = Path.GetFileName(filepath),
                    .EditedFileName = Path.GetFileName(filepath),
                    .ContainerBeingEdited = PackageType.EditingFileName}
                TextDialogInstance.ShowDialog()
                If TextDialogInstance.Result = DialogResult.OK Then
                    If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                        'Folder Editing, MoveAllFiles will be used
                        Dim NewFileName As String = TextDialogInstance.EditedFileName
                        Dim NewFullPath As String = Path.GetDirectoryName(filepath) & Path.DirectorySeparatorChar & TextDialogInstance.EditedFileName
                        If Not File.Exists(NewFullPath) Then
                            File.Move(filepath, NewFullPath)
                            Dim NewFI As FileInfo = New FileInfo(NewFullPath)
                            SelectedNode.Nodes.Clear()
                            SelectedNode.Text = NewFI.Name
                            SelectedNode.ToolTipText = NewFI.FullName
                            SelectedNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                            .Index = 0,
                            .length = FileLen(NewFI.FullName),
                            .StoredData = New Byte() {}}
                            SelectedNode.ImageIndex = 0
                            SelectedNode.SelectedImageIndex = 0
                            CheckFile(SelectedNode)
                        Else
                            MessageBox.Show("File " & NewFullPath & " already exists.")
                        End If
                    End If
                    End If
                TextDialogInstance.Dispose()
            Else
                MessageBox.Show("File " & Path.GetFileName(filepath) & " Not Found")
            End If
        End If
    End Sub
    Sub ExtractNode(Sentnode As TreeNode, Savepath As String)
        File.WriteAllBytes(Savepath, GetNodeBytes(Sentnode))
    End Sub
    Sub ExtractAllNode(CurrentNode As TreeNode, BaseFolder As String, Optional AdditonalFolders As String = "")
        FolderCheck(BaseFolder & AdditonalFolders)
        For Each temporarynode As TreeNode In CurrentNode.Nodes
            Dim NodeTag As NodeProperties = CType(temporarynode.Tag, NodeProperties)
            If Not NodeTag.FileType = PackageType.Folder Then 'Folders aren't extractable but make new folders
                If Not Path.GetFileName(temporarynode.ToolTipText) = temporarynode.Text Then 'if it's a file we don't want to copy it.
                    Dim FileExtention As String = ".bin"
                    If My.Settings.UseDetailedFileNames Then
                        FileExtention = "." & NodeTag.FileType.ToString
                    End If
                    ExtractNode(temporarynode, BaseFolder & AdditonalFolders &
                                   temporarynode.Text & FileExtention)
                End If
            End If
            If temporarynode.GetNodeCount(False) > 0 Then
                Dim Folder As String = ""
                'File.Getfilenamewithoutextention...?
                'Here we want to check if it is an uncompress containre for the settings options..
                If NodeTag.FileType = PackageType.OODL OrElse
                    NodeTag.FileType = PackageType.ZLIB OrElse
                    NodeTag.FileType = PackageType.BPE Then
                    If My.Settings.DecompresstoFolder Then
                        If temporarynode.Text.Contains(".") Then
                            Folder = temporarynode.Text.Substring(0, temporarynode.Text.IndexOf(".")) & Path.DirectorySeparatorChar
                        Else
                            Folder = temporarynode.Text.TrimEnd(" ") & Path.DirectorySeparatorChar
                        End If
                    End If
                    'if not folder add-on is nothing
                Else
                    If temporarynode.Text.Contains(".") Then
                        Folder = temporarynode.Text.Substring(0, temporarynode.Text.IndexOf(".")) & Path.DirectorySeparatorChar
                    Else
                        Folder = temporarynode.Text.TrimEnd(" ") & Path.DirectorySeparatorChar
                    End If
                End If
                ExtractAllNode(temporarynode, BaseFolder, AdditonalFolders & Folder)
            End If
        Next
    End Sub
    'First Pass but I feel like this can still be improved...
    'I think I need to add checking oodl parents and making sure it's injected properly..
    Function InjectIntoNode(Sentnode As TreeNode, SentBytes As Byte()) As Boolean
        'Adding in a check if file is read only and if the file is missing
        If Not CheckFileWriteable(Sentnode.ToolTipText) Then
            Return False
        End If
        If IsNothing(SentBytes) Then 'exits the function if no bytes are sent
            MessageBox.Show("No File Sent, Injection Failed!")
            Return False
        End If
        Dim NodeTag As NodeProperties = CType(Sentnode.Tag, NodeProperties)
        Dim SizeDifference As Long = SentBytes.Length - NodeTag.length 'negative for shorter
        'checking File Type match
        Dim TempCheck As PackageType = CheckHeaderType(0, SentBytes)
        If Not NodeTag.FileType = TempCheck Then
            If MessageBox.Show("File Type Mismatch!" & vbNewLine & "Continue?",
                            TempCheck.ToString & " Replacing " & NodeTag.FileType.ToString,
                            MessageBoxButtons.YesNo) = DialogResult.No Then
                Return False
            End If
        End If
        'Get Parent Node Bytes
        Dim ParentNode As TreeNode
        If IsNothing(Sentnode.Parent) Then
            ParentNode = Sentnode
        Else
            ParentNode = Sentnode.Parent
        End If
        Dim ParentNodeTag As NodeProperties = CType(ParentNode.Tag, NodeProperties)
        'skipping pachdirectories
        Dim DirectoryIndex As Integer = -1
        If ParentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
           ParentNodeTag.FileType = PackageType.PachDirectory_8 Then
            DirectoryIndex = ParentNode.Index
            MessageBox.Show("Directory Skipped" & vbNewLine & DirectoryIndex)
            ParentNode = ParentNode.Parent
            ParentNodeTag = CType(ParentNode.Tag, NodeProperties)
        End If
        Dim ParentBytes As Byte() = GetNodeBytes(ParentNode)
        'adjust length if needed
        If ParentNodeTag.FileType = PackageType.HSPC OrElse
                ParentNodeTag.FileType = PackageType.EPK8 OrElse
                 ParentNodeTag.FileType = PackageType.EPAC Then
            If SizeDifference > 0 Then 'size is rounded to &h800 bytes for these types
                SizeDifference += (&H800 - SizeDifference Mod &H800)
            ElseIf SizeDifference < 0 Then ' if it is 0 it stays 0
                SizeDifference -= (&H800 - Math.Abs(SizeDifference) Mod &H800)
            End If
        End If
        'Create Byte Array of length
        Dim WrittenFileArray As Byte() = New Byte(ParentNodeTag.length + SizeDifference - 1) {}
        ' Write File Prior to new file
        Array.Copy(ParentBytes, 0, WrittenFileArray, 0, CInt(NodeTag.Index - ParentNodeTag.Index))
        'write new file
        Array.Copy(SentBytes, 0, WrittenFileArray, CInt(NodeTag.Index - ParentNodeTag.Index), SentBytes.Length)
        'write old file from after file part if there are any
        If ParentBytes.Length > (NodeTag.Index - ParentNodeTag.Index) + NodeTag.length Then 'there are bytes after the injected file
            Buffer.BlockCopy(ParentBytes,
                                 (NodeTag.Index - ParentNodeTag.Index) + NodeTag.length,
                                WrittenFileArray,
                                 (NodeTag.Index - ParentNodeTag.Index) + NodeTag.length + SizeDifference,
                                 ParentBytes.Length - ((NodeTag.Index - ParentNodeTag.Index) + NodeTag.length))
        End If
        'Get Node Location
        Dim NodeLocation As Integer = Sentnode.Index '0 based
        'Adjust Headers
        If ParentNodeTag.FileType = PackageType.HSPC Then
            Dim FileCount As Integer = BitConverter.ToUInt32(WrittenFileArray, &H38)
            'adjust total file length TO DO Double Check This
            Array.Copy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - &H2800)), 0, WrittenFileArray, &H3C, 4)
            'Get the header length
            Dim FileNameLength As Integer = BitConverter.ToUInt32(WrittenFileArray, &H18)
            FileNameLength += -(FileNameLength Mod &H800) + &H1000
            For i As Integer = 0 To FileCount - 1
                If i < NodeLocation Then
                    'no change needed
                ElseIf i = NodeLocation Then
                    'Index Stays the same
                    Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length / &H100)), 0, WrittenFileArray, FileNameLength + i * &HC + &H4, 4)
                Else 'size stays but index changes
                    Dim OldIndex As UInt32 = BitConverter.ToUInt32(WrittenFileArray, FileNameLength + i * &HC)
                    Dim TempIndex As UInt64 = OldIndex * &H800 + SizeDifference
                    Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, FileNameLength + i * &HC, 4)
                End If
            Next
        ElseIf ParentNodeTag.FileType = PackageType.EPK8 OrElse
           ParentNodeTag.FileType = PackageType.EPAC Then
            Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
            Dim index As Integer = 0
            Dim DirectoryCount As Integer = 0
            'DirectoryIndex
            Do While index < HeaderLength - 1
                Dim DirectoryContainsCount As Integer = 0
                If PackageType.EPAC Then
                    DirectoryContainsCount = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 3
                ElseIf PackageType.EPK8 Then
                    DirectoryContainsCount = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 4
                End If
                index += &HC
                For i As Integer = 0 To DirectoryContainsCount - 1
                    If DirectoryCount < DirectoryIndex Then
                        'no change needed
                    ElseIf DirectoryCount = DirectoryIndex Then
                        If i < NodeLocation Then
                            'no change needed
                        ElseIf i = NodeLocation Then
                            'Index Stays the same
                            Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length / &H100)), 0, WrittenFileArray, &H800 + index + 12, 4)
                        Else ' i > 
                            Dim TempIndex As UInt64 = BitConverter.ToUInt32(WrittenFileArray, &H800 + index + 8) * &H800 + SizeDifference
                            Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, &H800 + index + 8, 4)
                        End If
                    Else ' directory > 
                        Dim TempIndex As UInt64 = BitConverter.ToUInt32(WrittenFileArray, &H800 + index + 8) * &H800 + SizeDifference
                        Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, &H800 + index + 8, 4)
                    End If
                    If PackageType.EPAC Then
                        index += &HC
                    ElseIf PackageType.EPK8 Then
                        index += &H10
                    End If
                Next
                DirectoryCount += 1
            Loop
        ElseIf ParentNodeTag.FileType = PackageType.SHDC Then
            Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(WrittenFileArray, &H18)
            Dim TempHeaderStart As Integer = BitConverter.ToUInt32(WrittenFileArray, &H1C)
            Dim TempHeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, &H20)
            If TempHeaderStart < TempHeaderCheck Then
                TempHeaderStart = TempHeaderCheck + &H10 + &H40
                If TempHeaderStart Mod &H10 > 0 Then
                    TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                End If
            End If
            Dim PachPartsCount As Integer = (TempHeaderLength / &H10) '1 index
            For i As Integer = 0 To PachPartsCount - 1
                Dim PartName As String = Hex(BitConverter.ToUInt32(WrittenFileArray, TempHeaderStart + (i * &H10)))
                'MessageBox.Show(PartName)
                If PartName = "FFFFFFFF" Then
                    Continue For
                End If
                If i < NodeLocation Then
                    'no change needed
                ElseIf i = NodeLocation Then
                    'Index Stays the same
                    Array.Copy(BitConverter.GetBytes(CULng(SentBytes.Length)), 0, WrittenFileArray, (TempHeaderStart + (i * &H10) + &H8), 8)
                Else ' i > NodeLocation
                    'Size stays index changed
                    Dim OldIndex As UInt32 = BitConverter.ToUInt32(WrittenFileArray, TempHeaderStart + (i * &H10) + &H4)
                    Array.Copy(BitConverter.GetBytes(CUInt(OldIndex + SizeDifference)), 0, WrittenFileArray, (TempHeaderStart + (i * &H10) + &H4), 4)
                End If
            Next
        ElseIf ParentNodeTag.FileType = PackageType.TextureLibrary Then
            'Code the injection here..
            'start with the header fixes and inject each part of the file
            Dim FileCount As Integer = WrittenFileArray(0)
            Dim BytesRevesed As Boolean = False
            If FileCount = 0 Then 'if this is 0 then we are dealing with a reverse byte system header.
                FileCount = WrittenFileArray(3)
                BytesRevesed = True
            End If
            'Texture Library has no total length to adjust
            For i As Integer = 0 To FileCount - 1
                If i < NodeLocation Then
                    'no change needed
                ElseIf i = NodeLocation Then
                    'Index Stays the same
                    If BytesRevesed Then
                        Array.Copy(EndianReverse(BitConverter.GetBytes(CUInt(SentBytes.Length))), 0, WrittenFileArray, i * &H20 + &H10 + &H14, 4)
                    Else
                        Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length)), 0, WrittenFileArray, i * &H20 + &H10 + &H14, 4)
                    End If
                Else 'size stays but index changes
                    If BytesRevesed Then
                        Dim OldIndex As UInt32 = BitConverter.ToUInt32(EndianReverse(WrittenFileArray, i * &H20 + &H10 + &H18, 4), 0)
                        Dim TempIndex As UInt64 = OldIndex + SizeDifference
                        Array.Copy(EndianReverse(BitConverter.GetBytes(CUInt(TempIndex))), 0, WrittenFileArray, i * &H20 + &H10 + &H18, 4)
                    Else
                        Dim OldIndex As UInt32 = BitConverter.ToUInt64(WrittenFileArray, i * &H20 + &H10 + &H18)
                        Dim TempIndex As UInt64 = OldIndex + SizeDifference
                        Array.Copy(BitConverter.GetBytes(CLng(TempIndex)), 0, WrittenFileArray, i * &H20 + &H10 + &H18, 8)
                    End If
                End If
            Next
        End If
        If ParentNodeTag.Index = 0 AndAlso
            ParentNodeTag.StoredData.Length = 0 Then
            'File to be Written
            Dim WrittenFile As String = Sentnode.ToolTipText
            If My.Settings.BackupInjections AndAlso CheckFileWriteable(WrittenFile & ".bak", False) Then
                File.Copy(WrittenFile, WrittenFile & ".bak", True)
            End If
            File.WriteAllBytes(WrittenFile, WrittenFileArray)
            'Remove Save Pending Buttons when file written
            SaveFileNoLongerPending()
            'resettreebranch
            Dim TempName As String = ParentNode.Text
            ParentNode.Nodes.Clear()
            ParentNode.Text = TempName
            ParentNode.ToolTipText = WrittenFile
            ParentNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                   .Index = 0,
                    .length = WrittenFileArray.Length,
                    .StoredData = New Byte() {}}
            'fixes for rebuilding the same file over and over
            CheckFile(ParentNode)
            TabControl1.SelectedIndex = 0
            TreeView1.SelectedNode = ParentNode
            ReadNode = ParentNode
            InformationLoaded = False
        Else
            'we must go higher
            InjectIntoNode(ParentNode, WrittenFileArray)
        End If
        Return True
    End Function
    Public Shared Function CheckFileWriteable(FilePath As String, Optional FileMustExist As Boolean = True)
        If Not File.Exists(FilePath) Then
            If FileMustExist Then
                MessageBox.Show(FilePath & vbNewLine & "Not Found")
                Return False
            Else
                Return True
            End If
        End If
        Dim attributes As FileAttributes = File.GetAttributes(FilePath)
        If (attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
            If MessageBox.Show(FilePath & " Is Read Only!" & vbNewLine & "Would you like to make it writable?", "Make File Writeable?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly)
                File.SetAttributes(FilePath, attributes)
            Else
                Return False
            End If
        End If
        Return True
    End Function
    'This is required to remove read only
    Private Shared Function RemoveAttribute(ByVal attributes As FileAttributes, ByVal attributesToRemove As FileAttributes) As FileAttributes
        Return attributes And Not attributesToRemove
    End Function
    Function ValidateTruncation(TestedString As String, ContainerType As PackageType) As String
        Select Case ContainerType
            Case PackageType.HSPC
                Return TestedString.PadLeft(16, "0").ToUpper
            Case PackageType.SHDC
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.EPK8
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.EPAC
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.PachDirectory_4
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.PachDirectory_8
                Return TestedString.PadLeft(My.Settings.DecimalNameMinLength, "0").ToUpper
            Case PackageType.TextureLibrary
                Return TestedString.Trim(" ")
            Case PackageType.Folder
                Return TestedString.Trim(" ")
            Case PackageType.EditingFileName
                Return TestedString.Trim(" ")
            Case Else
                Return TestedString
        End Select
    End Function
    Sub RenameNode(Sentnode As TreeNode, NewName As String)
        'Adding in a check if file is read only and if the file is missing
        If Not CheckFileWriteable(Sentnode.ToolTipText) Then
            Exit Sub
        End If
        Dim NodeTag As NodeProperties = CType(Sentnode.Tag, NodeProperties)
        'Get Parent Node Bytes
        Dim ParentNode As TreeNode
        If IsNothing(Sentnode.Parent) Then
            ParentNode = Sentnode
        Else
            ParentNode = Sentnode.Parent
        End If
        Dim ParentNodeTag As NodeProperties = CType(ParentNode.Tag, NodeProperties)
        Dim ParentBytes As Byte() = GetNodeBytes(ParentNode)
        Dim ParentNodeLocation As Integer = 0
        If ParentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
            ParentNodeTag.FileType = PackageType.PachDirectory_8 Then
            'here we grab the 
            ParentBytes = GetNodeBytes(ParentNode.Parent)
            ParentNodeLocation = ParentNode.Index
        End If
        Dim WrittenFileArray As Byte() = New Byte(ParentBytes.Length - 1) {}
        ' Write File Prior to new file
        Array.Copy(ParentBytes, 0, WrittenFileArray, 0, ParentBytes.LongLength)
        Dim ContainedParts As Integer = ParentNode.GetNodeCount(False)
        If ContainedParts > 1 Then
            If MessageBox.Show("Tool cannot yet sort.  Continue?", "Continue?", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                Exit Sub
            End If
        End If
        'Get Node Location
        Dim NodeLocation As Integer = Sentnode.Index '0 based
        Select Case ParentNodeTag.FileType
            Case PackageType.HSPC
                'Likely only the 1 SHDC unlikely needs to be sorted
                Dim StringBytes As Byte() = New Byte(7) {}
                Dim HexBytes As Byte() = HexStringToByte(NewName)
                'making the new string
                Array.Copy(HexBytes, 0, StringBytes, 0, HexBytes.Length)
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, &H800 + NodeLocation * &H14, 8)
            Case PackageType.SHDC
                '8 char possible endian reverse
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim HexBytes As Byte() = HexStringToByte(NewName)
                'making the new string
                Array.Copy(EndianReverse(HexBytes), 0, StringBytes, 0, HexBytes.Length)
                Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(WrittenFileArray, &H18)
                Dim TempHeaderStart As Integer = BitConverter.ToUInt32(WrittenFileArray, &H1C)
                Dim TempHeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, &H20)
                If TempHeaderStart < TempHeaderCheck Then
                    TempHeaderStart = TempHeaderCheck + &H10 + &H40
                    If TempHeaderStart Mod &H10 > 0 Then
                        TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                    End If
                End If
                'TempHeaderStart + (i * &H10)
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, TempHeaderStart + NodeLocation * &H10, 4)
            Case PackageType.EPK8
                'This would be renaming the pach container
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'I need to figure out how to double check what container it is... and we can't like assume an earlier container only has 1 pach
                Dim EditedNameIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    If DirectoryCount = NodeLocation Then
                        EditedNameIndex = &H800 + index
                        Exit Do
                    End If
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 4
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        index += &H10
                    Next
                    DirectoryCount += 1
                Loop
                'now that we have the proper index..
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 4)
            Case PackageType.EPAC
                'This would be renaming the pach container
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'I need to figure out how to double check what container it is... and we can't like assume an earlier container only has 1 pach
                Dim EditedNamerIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    If DirectoryCount = NodeLocation Then
                        EditedNamerIndex = &H800 + index
                        Exit Do
                    End If
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 3
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        index += &HC
                    Next
                    DirectoryCount += 1
                Loop
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNamerIndex, 4)
            Case PackageType.PachDirectory_8
                'a pach inside a epk8 pach directory
                Dim StringBytes As Byte() = New Byte(7) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'We can't assume this is in the first container
                Dim EditedNameIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 4
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        If DirectoryCount = ParentNodeLocation AndAlso
                            i = NodeLocation Then
                            EditedNameIndex = &H800 + index
                            Exit Do
                        End If
                        index += &H10
                    Next
                    DirectoryCount += 1
                Loop
                'now that we have the proper index..
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 8)
            Case PackageType.PachDirectory_4
                'this renames a pach inside a epk8 pach directory
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'We can't assume this is in the first container
                Dim EditedNameIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 3
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        If DirectoryCount = ParentNodeLocation AndAlso
                            i = NodeLocation Then
                            EditedNameIndex = &H800 + index
                            Exit Do
                        End If
                        index += &HC
                    Next
                    DirectoryCount += 1
                Loop
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 4)
            Case PackageType.TextureLibrary
                'Does not need to be sorted
                Dim StringBytes As Byte() = New Byte(15) {}
                'making the new string
                Array.Copy(Encoding.Default.GetBytes(NewName), 0, StringBytes, 0, NewName.Length)
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, &H10 + NodeLocation * &H20, 16)
        End Select
        'now we want to correct the parent so we can inject properly
        'skipping pachdirectories
        Dim DirectoryIndex As Integer = -1
        If ParentNodeTag.FileType = PackageType.PachDirectory_4 OrElse
           ParentNodeTag.FileType = PackageType.PachDirectory_8 Then
            DirectoryIndex = ParentNode.Index
            MessageBox.Show("Directory Skipped" & vbNewLine & DirectoryIndex)
            ParentNode = ParentNode.Parent
            ParentNodeTag = CType(Sentnode.Parent.Parent.Tag, NodeProperties)
        End If
        If ParentNodeTag.Index = 0 AndAlso
            ParentNodeTag.StoredData.Length = 0 Then
            'File to be Written
            Dim WrittenFile As String = Sentnode.ToolTipText
            If My.Settings.BackupInjections AndAlso CheckFileWriteable(WrittenFile & ".bak", False) Then
                File.Copy(WrittenFile, WrittenFile & ".bak", True)
            End If
            File.WriteAllBytes(WrittenFile, WrittenFileArray)
            'Remove Save Pending Buttons when file written
            SaveFileNoLongerPending()
            'resettreebranch
            Dim TempName As String = ParentNode.Text
            ParentNode.Nodes.Clear()
            ParentNode.Text = TempName
            ParentNode.ToolTipText = WrittenFile
            ParentNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                   .Index = 0,
                    .length = WrittenFileArray.Length,
                    .StoredData = New Byte() {}}
            'fixes for rebuilding the same file over and over
            CheckFile(ParentNode)
            TreeView1.SelectedNode = ParentNode
            TabControl1.SelectedIndex = 0
            ReadNode = ParentNode
            InformationLoaded = False
        Else
            'we must go higher
            InjectIntoNode(ParentNode, WrittenFileArray)
        End If
    End Sub
#End Region
#Region "View Controls"
#Region "Multi-View Controls"
    'Commands that should be generic to be used across multiple tabs.
    Private Sub StoreOldComboBoxValue(sender As Object, e As EventArgs) Handles HexViewBitWidth.Enter,
                                                                        TextViewBitWidth.Enter
        OldValue = sender.text
    End Sub
    Private Sub StoreOldDataGridViewValue(sender As DataGridView, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellEnter,
                                                                                                    DataGridShowView.CellEnter,
                                                                                                    DataGridNIBJView.CellEnter,
                                                                                                    DataGridAttireView.CellEnter,
                                                                                                    DataGridObjArrayView.CellEnter
        OldValue = sender.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub
    Sub SaveFileNoLongerPending()
        ReadNode = Nothing
        SavePending = False
        SaveStringChangesToolStripMenuItem.Visible = False
        SaveMiscChangesToolStripMenuItem.Visible = False
        SaveShowChangesToolStripMenuItem.Visible = False
    End Sub
    Function ClearandGetClone(SentDataGrid) As DataGridViewRow
        SentDataGrid.Rows.Clear()
        SentDataGrid.Rows.Add()
        Dim CloneRow As DataGridViewRow = SentDataGrid.Rows(0).Clone()
        SentDataGrid.Rows.Clear()
        Return CloneRow
    End Function
    Dim ReadOnlyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle With {
        .BackColor = SystemColors.Control,
        .ForeColor = SystemColors.ControlText}
#End Region
    'TO DO Request Direct Editing of Hex Edit View
#Region "Hex View Controls"
    Sub AddHexText(SelectedNode As TreeNode)
        If File.Exists(SelectedNode.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If HexViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(HexViewBitWidth.Text)
            Else
                bitwidth = CInt(HexViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As NodeProperties = CType(SelectedNode.Tag, NodeProperties)
            Dim Filebytes As Byte() = GetNodeBytes(SelectedNode)
            Dim ByteString As String = ""
            If Filebytes.Length < (&H1000 * My.Settings.HexViewLength) Then
                ByteString = (BitConverter.ToString(Filebytes, 0, Filebytes.Length).Replace("-", " "))
            Else
                ByteString = (BitConverter.ToString(Filebytes, 0, (&H1000 * My.Settings.HexViewLength)).Replace("-", " "))
            End If
            Dim builder As New StringBuilder(ByteString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 3)
            For i As Int32 = startIndex To (bitwidth * 3) Step -(bitwidth * 3)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Hex_Selected.Text = builder.ToString()
        End If
    End Sub
    Private Sub HexViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles HexViewBitWidth.TextChanged
        If HexViewBitWidth.Text = "" Then
            'Do Nothing 
            Exit Sub
        ElseIf Not IsNumeric(HexViewBitWidth.Text) Then
            HexViewBitWidth.Text = OldValue
            Exit Sub
        ElseIf CInt(HexViewBitWidth.Text) > 0 Then
            If HexViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = HexViewBitWidth.SelectedIndex
                TextViewBitWidth.SelectedIndex = HexViewBitWidth.SelectedIndex
            Else
                TextViewBitWidth.SelectedIndex = -1
                TextViewBitWidth.Text = HexViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddHexText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

#End Region
#Region "Text View Controls"
    Sub AddText(SelectedNode As TreeNode)
        If File.Exists(SelectedNode.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If TextViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(TextViewBitWidth.Text)
            Else
                bitwidth = CInt(TextViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As NodeProperties = CType(SelectedNode.Tag, NodeProperties)
            Dim Filebytes As Byte() = GetNodeBytes(SelectedNode)
            Dim TextString As String = ""
            If NodeTag.length < (&H1000 * My.Settings.HexViewLength) Then
                TextString = New String(".", NodeTag.length)
            Else
                TextString = New String(".", (&H1000 * My.Settings.HexViewLength))
            End If
            Dim FirstBuilder As New StringBuilder(TextString)
            For i As Integer = 0 To TextString.Length - 1
                If Filebytes(i) > 31 AndAlso (Filebytes(i) < 257) Then
                    FirstBuilder(i) = Encoding.Default.GetChars(Filebytes, i, 1)(0)
                End If
            Next
            Dim builder As New StringBuilder(FirstBuilder.ToString().Replace(vbCr, ".").Replace(vbLf, "."))
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 1)
            For i As Int32 = startIndex To (bitwidth * 1) Step -(bitwidth * 1)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Text_Selected.Text = builder.ToString()
        End If
    End Sub
    Private Sub TextViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles TextViewBitWidth.TextChanged
        If TextViewBitWidth.Text = "" Then
            'Do Nothing 
            Exit Sub
        ElseIf Not IsNumeric(TextViewBitWidth.Text) Then
            TextViewBitWidth.Text = OldValue
            Exit Sub
        ElseIf CInt(TextViewBitWidth.Text) > 0 Then
            If TextViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = TextViewBitWidth.SelectedIndex
                HexViewBitWidth.SelectedIndex = TextViewBitWidth.SelectedIndex
            Else
                HexViewBitWidth.SelectedIndex = -1
                HexViewBitWidth.Text = TextViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

#End Region
#Region "String View Controls"
    Sub FillStringView(SelectedData As TreeNode)
        Dim Testing As String = ""
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridStringView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Try
            Dim NodeTag As NodeProperties = CType(SelectedData.Tag, NodeProperties)
            Dim StringBytes As Byte() = GetNodeBytes(SelectedData)
            Dim StringCount As Integer = BitConverter.ToInt32(StringBytes, 4)
            StringCountToolStripMenuItem.Text = "String Count: " & StringCount
            ProgressBar1.Maximum = StringCount - 1
            ProgressBar1.Value = 0
            'Get Data On the Pach parts
            Dim StringFileOffset(Int16.MaxValue) As Integer
            Dim StringFileLength(Int16.MaxValue) As Integer
            Dim StringFileReference(Int16.MaxValue) As Integer
            For j As Integer = 0 To StringCount - 1
                StringFileOffset(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 0)
                StringFileLength(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 4)
                StringFileReference(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 8)
                Testing = StringFileReference(j)
                'Trim all 00 chars so the strings don't end abrubtly in future manipulation
                Dim TempStringBytes As Byte() = New Byte(StringFileLength(j) - 1) {}
                Array.Copy(StringBytes, StringFileOffset(j), TempStringBytes, 0, StringFileLength(j))
                StringReferences(StringFileReference(j)) = Encoding.Default.GetString(TempStringBytes).TrimEnd(Chr(0))
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = Hex(StringFileReference(j))
                TempGridRow.Cells(1).Value = StringReferences(StringFileReference(j))
                TempGridRow.Cells(2).Value = StringFileLength(j).ToString
                TempGridRow.Cells(3).Value = "Add"
                TempGridRow.Cells(4).Value = "Remove"
                TempGridRow.Tag = TempStringBytes
                WorkingCollection.Add(TempGridRow)
                ProgressBar1.Value = j
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & Testing)
        End Try
        DataGridStringView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub
    Sub SortStringView()
        Dim TempColumn As DataGridViewColumn = New DataGridViewTextBoxColumn With {.Name = "TempCol", .Visible = False}
        DataGridStringView.Columns.Add(TempColumn)
        Dim ColNum As Integer = DataGridStringView.Columns.IndexOf(TempColumn)
        For Each TempRow As DataGridViewRow In DataGridStringView.Rows
            Dim TempNumber As UInt32 = CUInt("&H" & TempRow.Cells(0).Value.ToString)
            TempRow.Cells(ColNum).Value = TempNumber
        Next
        DataGridStringView.Sort(DataGridStringView.Columns(ColNum), System.ComponentModel.ListSortDirection.Ascending)
        DataGridStringView.Columns.Remove(TempColumn)
        SortStringsToolStripMenuItem.Visible = False
    End Sub
    Function CheckDuplicateStrings() As Boolean 'returns True for a dupe row
        Dim BuiltList As List(Of Integer) = New List(Of Integer)
        For i As Integer = 0 To DataGridStringView.Rows.Count - 1
            Dim TempNumber As UInt32 = CUInt("&H" & DataGridStringView.Rows(i).Cells(0).Value.ToString)
            If BuiltList.Contains(TempNumber) Then
                MessageBox.Show("Duplicate string ID found at row " & i)
                Return True
            Else
                BuiltList.Add(TempNumber)
            End If
        Next
        Return False
    End Function
    Private Sub SaveChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveStringChangesToolStripMenuItem.Click
        SortStringView()
        If Not CheckDuplicateStrings() Then
            InjectIntoNode(ReadNode, BuildStringFile())
        End If
    End Sub
    Private Sub SortStringsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortStringsToolStripMenuItem.Click
        SortStringView()
    End Sub
    Dim OldLength As Integer
    Dim LengthTheSame As Boolean = False
    Private Sub DataGridStringView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellEnter
        If e.ColumnIndex = 0 Then 'Hex Text Reference Editing
            OldValue = DataGridStringView.Rows(e.RowIndex).Cells(0).Value
        ElseIf e.ColumnIndex = 1 Then
            OldValue = DataGridStringView.Rows(e.RowIndex).Cells(1).Value
            OldLength = DataGridStringView.Rows(e.RowIndex).Cells(2).Value
            LengthTheSame = (OldValue.Length + 1 = OldLength) 'If length is not the same then it might be a super string
        ElseIf e.ColumnIndex = 2 Then
            OldValue = DataGridStringView.Rows(e.RowIndex).Cells(2).Value
        End If
    End Sub
    Private Sub DataGridStringView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridStringView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 0 Then 'Hexvalue
            If Not HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            Else
                SortStringsToolStripMenuItem.Visible = True
                SaveStringChangesToolStripMenuItem.Visible = True
            End If
        ElseIf e.ColumnIndex = 1 Then 'string text
            If LengthTheSame Then
                DataGridStringView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value.length + 1
            Else
                If MessageBox.Show("String currently contains extra characters." & vbNewLine &
                                  "Would you like to maintain the length", "Potential Super String Detected!",
                                   MessageBoxButtons.YesNo) = DialogResult.No Then
                    DataGridStringView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value.length + 1
                Else 'check if new string is too long
                    If DataGridStringView.Rows(e.RowIndex).Cells(2).Value < MyCell.Value.length + 1 Then
                        MessageBox.Show("String is too long, string will be truncated.")
                        MyCell.Value = MyCell.Value.ToString.Substring(0, DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
                    End If
                End If
            End If
            DataGridStringView.Rows(e.RowIndex).Tag = Encoding.Default.GetBytes(MyCell.Value + Chr(0)) 'Stores the new string as bytes in the tag
            'this redim keeps the right length for a super string type string where there are several 0 chars at the end
            ReDim Preserve DataGridStringView.Rows(e.RowIndex).Tag(DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
            SavePending = True
            SaveStringChangesToolStripMenuItem.Visible = True
        ElseIf e.ColumnIndex = 2 Then 'Adjusting Length only
            If Not IsNumeric(MyCell.Value) OrElse
               MyCell.Value < 1 Then
                MyCell.Value = OldValue
            Else
                If MyCell.Value < DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1 Then
                    MessageBox.Show("String is too long, string will be truncated.")
                    DataGridStringView.Rows(e.RowIndex).Cells(1).Value =
                                DataGridStringView.Rows(e.RowIndex).Cells(1).Value.ToString.Substring(0, MyCell.Value - 1) 'Truncates the String
                    DataGridStringView.Rows(e.RowIndex).Tag = Encoding.Default.GetBytes(DataGridStringView.Rows(e.RowIndex).Cells(1).Value + Chr(0)) 'Stores the new string as bytes in the tag
                Else 'Redim the tag to expand it
                    ReDim Preserve DataGridStringView.Rows(e.RowIndex).Tag(MyCell.Value - 1)
                End If
                SavePending = True
                SaveStringChangesToolStripMenuItem.Visible = True
            End If

        End If
    End Sub
    Private Sub DataGridStringView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 3 Then 'add button
                Dim Duplicaterow As DataGridViewRow = DataGridStringView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridStringView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridStringView.Rows(e.RowIndex).Cells(i).Value
                Next
                Duplicaterow.Tag = DataGridStringView.Rows(e.RowIndex).Tag
                DataGridStringView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
            ElseIf e.ColumnIndex = 4 Then 'Delete button
                DataGridStringView.Rows.RemoveAt(e.RowIndex)
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub
    Private Sub ToolStripTextBoxSearch_Enter(sender As Object, e As EventArgs) Handles ToolStripTextBoxSearch.Enter
        If ToolStripTextBoxSearch.Text = "Search..." Then
            ToolStripTextBoxSearch.Text = ""
        End If
    End Sub
    Private Sub ToolStripTextBoxSearch_Leave(sender As Object, e As EventArgs) Handles ToolStripTextBoxSearch.Leave
        If ToolStripTextBoxSearch.Text = "" Then
            ToolStripTextBoxSearch.Text = "Search..."
        End If
        Dim TemporaryCollection As DataGridViewRow() = New DataGridViewRow(DataGridStringView.Rows.Count - 1) {}
        DataGridStringView.Rows.CopyTo(TemporaryCollection, 0)
        DataGridStringView.Rows.Clear()
        ProgressBar1.Maximum = TemporaryCollection.Count - 1
        ProgressBar1.Value = 0
        If ToolStripTextBoxSearch.Text = "" OrElse
            ToolStripTextBoxSearch.Text = "Search..." Then
            For i As Integer = 0 To TemporaryCollection.Count - 1
                TemporaryCollection(i).Visible = True
                ProgressBar1.Value = i
            Next
        Else
            For i As Integer = 0 To TemporaryCollection.Count - 1
                If TemporaryCollection(i).Cells(1).Value.ToString.ToLower.Contains(ToolStripTextBoxSearch.Text.ToLower) Then
                    TemporaryCollection(i).Visible = True
                Else
                    TemporaryCollection(i).Visible = False
                End If
                ProgressBar1.Value = i
            Next
        End If
        DataGridStringView.Rows.AddRange(TemporaryCollection.ToArray)
    End Sub
    Private Function BuildStringFile() As Byte()
        Dim StringCount As Integer = DataGridStringView.RowCount
        Dim StringSum As Integer = 0
        For i As Integer = 0 To StringCount - 1
            StringSum += DataGridStringView.Rows(i).Cells(2).Value
        Next
        'First get the string count and make a header
        Dim ReturnedBytes As Byte() = New Byte(&H8 + StringCount * &HC + StringSum - 1) {} 'String Sum adds on an extra 1 for the last string from my tests
        'Building the header 0000 , string count
        Array.Copy(BitConverter.GetBytes(CUInt(StringCount)), 0, ReturnedBytes, 4, 4)
        ProgressBar1.Maximum = StringCount - 1
        ProgressBar1.Value = 0
        Dim index As UInt32 = &H8 + StringCount * &HC
        For i As Integer = 0 To StringCount - 1
            'index of string won't change
            Array.Copy(BitConverter.GetBytes(index), 0, ReturnedBytes, 8 + i * 12 + 0, 4)
            'String Length will be equal to cell 3 (2 in 0 index)
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridStringView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, 8 + i * 12 + 4, 4)
            'Reference is set from the first cell
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridStringView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, 8 + i * 12 + 8, 4)
            'now we have to copy the string from the tag storage
            Dim TempArray As Byte() = Encoding.Default.GetBytes(DataGridStringView.Rows(i).Cells(1).Value) 'DataGridStringView.Rows(i).Tag ' Casting the Tag so the array handles it properly
            Array.Copy(TempArray, 0, ReturnedBytes, index, TempArray.Length) 'CUInt(DataGridStringView.Rows(i).Cells(2).Value))
            'Now add the lengthto the index
            index += CUInt(DataGridStringView.Rows(i).Cells(2).Value)
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function
#End Region
#Region "Misc View Controls"
    Public Class ArenaInformation
        Public Stadium As Integer = -2
        Public Advertisement As Integer = -2
        Public CornerPost As Integer = -2
        Public LED_CornerPost As Integer = -2
        Public Rope As Integer = -2
        Public Apron As Integer = -2
        Public LED_Apron As Integer = -2
        Public Turnbuckle As Integer = -2
        Public Barricade As Integer = -2
        Public Fence As Integer = -2
        Public CeilingLighting As Integer = -2
        Public Spotlight As Integer = -2
        Public Stairs As Integer = -2
        Public CommentarySeat As Integer = -2
        Public RingMat As Integer = -2
        Public FloorMattress As Integer = -2
        Public Crowd As Integer = -2
        Public CrowdSeatsPlace As Integer = -2
        Public CrowdSeatsModel As Integer = -2
        Public IBL As Integer = -2
        Public Titantron As Integer = -2
        Public Minitron As Integer = -2
        Public Wall_L As Integer = -2
        Public Wall_R As Integer = -2
        Public Header As Integer = -2
        Public Floor As Integer = -2
        Public MiscObjects As Integer = -2
        Public LightingType As Integer = -2
        Public CornerPost_CM As Integer = -2
        Public Rope_CM As Integer = -2
        Public Apron_CM As Integer = -2
        Public Turnbuckle_CM As Integer = -2
        Public RingMat_CM As Integer = -2
        Public version As String = "1.0"
    End Class
    Sub FillMiscView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMiscView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim MiscBytes As Byte() = GetNodeBytes(SelectedData)
        Dim ArenaCount As Integer = BitConverter.ToInt32(MiscBytes, 0)
        ProgressBar1.Maximum = ArenaCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To ArenaCount - 1
            Dim DByteList As List(Of Boolean) = New List(Of Boolean)
            Dim ArenaNum As String = Encoding.ASCII.GetString(MiscBytes, 25 + i * 32, 5)
            Dim TempIndex As Integer = BitConverter.ToInt32(MiscBytes, 40 + i * 32)
            Dim TempLength As Integer = BitConverter.ToInt32(MiscBytes, 36 + i * 32)
            Dim ArenaArray As Byte() = New Byte(TempLength - 1) {}
            Array.Copy(MiscBytes, TempIndex, ArenaArray, 0, TempLength)
            Dim ArenaJson As String = Encoding.ASCII.GetString(ArenaArray)
            Dim TempArena As ArenaInformation = ParseJsonToArena(ArenaJson)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = ArenaNum
            TempGridRow.Cells(1).Value = TempArena.Stadium
            TempGridRow.Cells(2).Value = TempArena.Advertisement
            TempGridRow.Cells(3).Value = TempArena.CornerPost
            TempGridRow.Cells(4).Value = TempArena.LED_CornerPost
            TempGridRow.Cells(5).Value = TempArena.Rope
            TempGridRow.Cells(6).Value = TempArena.Apron
            TempGridRow.Cells(7).Value = TempArena.LED_Apron
            TempGridRow.Cells(8).Value = TempArena.Turnbuckle
            TempGridRow.Cells(9).Value = TempArena.Barricade
            TempGridRow.Cells(10).Value = TempArena.Fence
            TempGridRow.Cells(11).Value = TempArena.CeilingLighting
            TempGridRow.Cells(12).Value = TempArena.Spotlight
            TempGridRow.Cells(13).Value = TempArena.Stairs
            TempGridRow.Cells(14).Value = TempArena.CommentarySeat
            TempGridRow.Cells(15).Value = TempArena.RingMat
            TempGridRow.Cells(16).Value = TempArena.FloorMattress
            TempGridRow.Cells(17).Value = TempArena.Crowd
            TempGridRow.Cells(18).Value = TempArena.CrowdSeatsPlace
            TempGridRow.Cells(19).Value = TempArena.CrowdSeatsModel
            TempGridRow.Cells(20).Value = TempArena.IBL
            TempGridRow.Cells(21).Value = TempArena.Titantron
            TempGridRow.Cells(22).Value = TempArena.Minitron
            TempGridRow.Cells(23).Value = TempArena.Wall_L
            TempGridRow.Cells(24).Value = TempArena.Wall_R
            TempGridRow.Cells(25).Value = TempArena.Header
            TempGridRow.Cells(26).Value = TempArena.Floor
            TempGridRow.Cells(27).Value = TempArena.MiscObjects
            TempGridRow.Cells(28).Value = TempArena.LightingType
            TempGridRow.Cells(29).Value = TempArena.CornerPost_CM
            TempGridRow.Cells(30).Value = TempArena.Rope_CM
            TempGridRow.Cells(31).Value = TempArena.Apron_CM
            TempGridRow.Cells(32).Value = TempArena.Turnbuckle_CM
            TempGridRow.Cells(33).Value = TempArena.RingMat_CM
            TempGridRow.Cells(34).Value = TempArena.version
            'Build the D byte list for error handling
            For K As Integer = 0 To ArenaArray.Length - 1
                If ArenaArray(K) = &HA Then
                    If ArenaArray(K - 1) = &HD Then
                        DByteList.Add(True)
                    Else
                        DByteList.Add(False)
                    End If
                End If
            Next
            TempGridRow.Tag = DByteList
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridMiscView.Rows.AddRange(WorkingCollection.ToArray)
        GetMiscColumns(MiscViewType.SelectedIndex)
    End Sub
    Function ParseJsonToArena(ArenaJson As String) As ArenaInformation
        Dim reader As Newtonsoft.Json.JsonTextReader = New JsonTextReader(New StringReader(ArenaJson))
        reader.Read() 'Skips the first null value
        Dim ReturnedArena As ArenaInformation = New ArenaInformation
        While reader.Read
            Dim TemporaryArenaPart As String = reader.Value
            reader.Read()
            Select Case TemporaryArenaPart
                Case "Stadium"
                    ReturnedArena.Stadium = reader.Value
                Case "Advertisement"
                    ReturnedArena.Advertisement = reader.Value
                Case "CornerPost"
                    ReturnedArena.CornerPost = reader.Value
                Case "LED_CornerPost"
                    ReturnedArena.LED_CornerPost = reader.Value
                Case "Rope"
                    ReturnedArena.Rope = reader.Value
                Case "Apron"
                    ReturnedArena.Apron = reader.Value
                Case "LED_Apron"
                    ReturnedArena.LED_Apron = reader.Value
                Case "Turnbuckle"
                    ReturnedArena.Turnbuckle = reader.Value
                Case "Barricade"
                    ReturnedArena.Barricade = reader.Value
                Case "Fence"
                    ReturnedArena.Fence = reader.Value
                Case "CeilingLighting"
                    ReturnedArena.CeilingLighting = reader.Value
                Case "Spotlight"
                    ReturnedArena.Spotlight = reader.Value
                Case "Stairs"
                    ReturnedArena.Stairs = reader.Value
                Case "CommentarySeat"
                    ReturnedArena.CommentarySeat = reader.Value
                Case "RingMat"
                    ReturnedArena.RingMat = reader.Value
                Case "FloorMattress"
                    ReturnedArena.FloorMattress = reader.Value
                Case "Crowd"
                    ReturnedArena.Crowd = reader.Value
                Case "CrowdSeatsPlace"
                    ReturnedArena.CrowdSeatsPlace = reader.Value
                Case "CrowdSeatsModel"
                    ReturnedArena.CrowdSeatsModel = reader.Value
                Case "IBL"
                    ReturnedArena.IBL = reader.Value
                Case "Titantron"
                    ReturnedArena.Titantron = reader.Value
                Case "Minitron"
                    ReturnedArena.Minitron = reader.Value
                Case "Wall_L"
                    ReturnedArena.Wall_L = reader.Value
                Case "Wall_R"
                    ReturnedArena.Wall_R = reader.Value
                Case "Header"
                    ReturnedArena.Header = reader.Value
                Case "Floor"
                    ReturnedArena.Floor = reader.Value
                Case "MiscObjects"
                    ReturnedArena.MiscObjects = reader.Value
                Case "LightingType"
                    ReturnedArena.LightingType = reader.Value
                Case "CornerPost_CM"
                    ReturnedArena.CornerPost_CM = reader.Value
                Case "Rope_CM"
                    ReturnedArena.Rope_CM = reader.Value
                Case "Apron_CM"
                    ReturnedArena.Apron_CM = reader.Value
                Case "Turnbuckle_CM"
                    ReturnedArena.Turnbuckle_CM = reader.Value
                Case "RingMat_CM"
                    ReturnedArena.RingMat_CM = reader.Value
                Case "version"
                    ReturnedArena.version = reader.Value
                Case ""
                    'Skip because null type
                Case Else
                    MessageBox.Show("Unknown Obejct: " & TemporaryArenaPart)
            End Select
        End While
        Return ReturnedArena
    End Function
    Sub GetMiscColumns(MenuIndex As Integer)
        If Not MenuIndex > 0 Then '2K16 and Beyond
            DataGridMiscView.Columns("Col_LED_Apron").Visible = False
            DataGridMiscView.Columns("Col_Titantron").Visible = False
            DataGridMiscView.Columns("Col_Minitron").Visible = False
            DataGridMiscView.Columns("Col_Wall_L").Visible = False
            DataGridMiscView.Columns("Col_Wall_R").Visible = False
            DataGridMiscView.Columns("Col_Header").Visible = False
            DataGridMiscView.Columns("Col_Floor").Visible = False
            DataGridMiscView.Columns("Col_MiscObjects").Visible = False
        Else
            DataGridMiscView.Columns("Col_LED_Apron").Visible = True
            DataGridMiscView.Columns("Col_Titantron").Visible = True
            DataGridMiscView.Columns("Col_Minitron").Visible = True
            DataGridMiscView.Columns("Col_Wall_L").Visible = True
            DataGridMiscView.Columns("Col_Wall_R").Visible = True
            DataGridMiscView.Columns("Col_Header").Visible = True
            DataGridMiscView.Columns("Col_Floor").Visible = True
            DataGridMiscView.Columns("Col_MiscObjects").Visible = True
            If Not MenuIndex > 2 Then '2K18 and Beyond
                DataGridMiscView.Columns("Col_LED_CornerPost").Visible = False
                DataGridMiscView.Columns("Col_LightingType").Visible = False
            Else
                DataGridMiscView.Columns("Col_LED_CornerPost").Visible = True
                DataGridMiscView.Columns("Col_LightingType").Visible = True
                If Not MenuIndex > 3 Then '2K19 and Beyond
                    DataGridMiscView.Columns("Col_CrowdSeatsPlace").Visible = False
                    DataGridMiscView.Columns("Col_CrowdSeatsModel").Visible = False
                    DataGridMiscView.Columns("Col_CornerPost_CM").Visible = False
                    DataGridMiscView.Columns("Col_Rope_CM").Visible = False
                    DataGridMiscView.Columns("Col_Apron_CM").Visible = False
                    DataGridMiscView.Columns("Col_Turnbuckle_CM").Visible = False
                    DataGridMiscView.Columns("Col_RingMat_CM").Visible = False
                Else
                    DataGridMiscView.Columns("Col_CrowdSeatsPlace").Visible = True
                    DataGridMiscView.Columns("Col_CrowdSeatsModel").Visible = True
                    DataGridMiscView.Columns("Col_CornerPost_CM").Visible = True
                    DataGridMiscView.Columns("Col_Rope_CM").Visible = True
                    DataGridMiscView.Columns("Col_Apron_CM").Visible = True
                    DataGridMiscView.Columns("Col_Turnbuckle_CM").Visible = True
                    DataGridMiscView.Columns("Col_RingMat_CM").Visible = True
                End If
            End If
        End If
    End Sub
    Private Sub MiscViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MiscViewType.SelectedIndexChanged
        'Locked to 2K19 for now
        If Not MiscViewType.SelectedIndex = 4 Then
            MiscViewType.SelectedIndex = 4
            Exit Sub
        End If
        If Not My.Settings.MiscModeIndex = MiscViewType.SelectedIndex Then 'checks if the index is actually changed, or just being reverted
            If SavePending Then
                If MessageBox.Show("Any changes will be lost", "Continue format change?", MessageBoxButtons.YesNo) = DialogResult.No Then
                    MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
                    Exit Sub
                End If
            End If
            My.Settings.MiscModeIndex = MiscViewType.SelectedIndex
            If TreeView1.SelectedNode IsNot Nothing Then
                FillMiscView(TreeView1.SelectedNode)
            End If
        End If
    End Sub
    Private Sub DataGridMiscView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMiscView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If OldValue = -2 Then
            MyCell.Value = OldValue
            Exit Sub
        End If
        If Not IsNumeric(MyCell.Value) OrElse
               MyCell.Value < -1 Then
            MyCell.Value = OldValue
        Else
            SavePending = True
            SaveMiscChangesToolStripMenuItem.Visible = True
        End If
    End Sub
    Private Sub SaveMiscChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMiscChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildMiscFile())
    End Sub
    Private Function BuildMiscFile() As Byte()
        Dim Active_Offset As Integer = &H10 + &H20 * DataGridMiscView.RowCount
        Dim Temp_Array As Byte() = New Byte(&H20000) {}
        Temp_Array(0) = DataGridMiscView.RowCount
        Temp_Array(5) = 1
        Temp_Array(&HC) = &H10
        ProgressBar1.Maximum = DataGridMiscView.RowCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To DataGridMiscView.RowCount - 1
            'Making the header 
            Dim Part_Head_Array As Byte() = New Byte(&H20) {}
            'Adding the text parts.
            Dim ArenaInfoBytes As Byte() = Encoding.ASCII.GetBytes("arenaInfo" & DataGridMiscView.Rows(i).Cells(0).Value)
            Buffer.BlockCopy(ArenaInfoBytes, 0, Part_Head_Array, 0, ArenaInfoBytes.Length)
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("jsn"), 0, Part_Head_Array, &H10, 3)
            'Build the Arena String
            Dim Part_String As String = BuildJsonArenaFile(i)
            'if there is a builder error this will exit the function without writing the file.
            If Part_String = "" Then
                Return Nothing
            End If
            'Add the Length to the Header
            Buffer.BlockCopy(BitConverter.GetBytes(Part_String.Length), 0, Part_Head_Array, &H14, 4)
            'Add the offset to the header
            Buffer.BlockCopy(BitConverter.GetBytes(Active_Offset), 0, Part_Head_Array, &H18, 4)
            'injecting the header
            Buffer.BlockCopy(Part_Head_Array, 0, Temp_Array, &H10 + &H20 * i, &H20)
            'Adding the ArenaInfo to the ararry
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(Part_String), 0, Temp_Array, Active_Offset, Part_String.Length)
            'Updating the Active Offset
            Active_Offset += Part_String.Length
            If Part_String.Length Mod 16 > 0 Then
                Active_Offset += 16 - Part_String.Length Mod 16
            End If
            ProgressBar1.Value = i
        Next
        ReDim Preserve Temp_Array(Active_Offset - 1)
        'Dim Final_Array As Byte() = New Byte(Active_Offset - 1) {}
        'Buffer.BlockCopy(Temp_Array, 0, Final_Array, 0, Active_Offset)
        Return Temp_Array
    End Function
    Function JsonWriterArenaFile(Index As Integer) As String
        'TO DO TEST 2K18 - 2K15
        'This is to be tested for 2k18 - 2k15 before allowing it in a release
        Dim TempStringBuilder As StringBuilder = New StringBuilder()
        Dim TempStringWriter As StringWriter = New StringWriter(TempStringBuilder)
        Using ActualWriter As JsonWriter = New JsonTextWriter(TempStringWriter)
            ActualWriter.Formatting = Formatting.Indented
            ActualWriter.WriteStartObject()
            ActualWriter.WritePropertyName("Stadium")
            ActualWriter.WriteValue(DataGridMiscView(1, Index).Value)
            ActualWriter.WritePropertyName("Advertisement")
            ActualWriter.WriteValue(DataGridMiscView(2, Index).Value)
            ActualWriter.WritePropertyName("CornerPost")
            ActualWriter.WriteValue(DataGridMiscView(3, Index).Value)
            ActualWriter.WritePropertyName("LED_CornerPost")
            ActualWriter.WriteValue(DataGridMiscView(4, Index).Value)
            ActualWriter.WritePropertyName("Rope")
            ActualWriter.WriteValue(DataGridMiscView(5, Index).Value)
            ActualWriter.WritePropertyName("Apron")
            ActualWriter.WriteValue(DataGridMiscView(6, Index).Value)
            ActualWriter.WritePropertyName("LED_Apron")
            ActualWriter.WriteValue(DataGridMiscView(7, Index).Value)
            ActualWriter.WritePropertyName("Turnbuckle")
            ActualWriter.WriteValue(DataGridMiscView(8, Index).Value)
            ActualWriter.WritePropertyName("Barricade")
            ActualWriter.WriteValue(DataGridMiscView(9, Index).Value)
            ActualWriter.WritePropertyName("Fence")
            ActualWriter.WriteValue(DataGridMiscView(10, Index).Value)
            ActualWriter.WritePropertyName("CeilingLighting")
            ActualWriter.WriteValue(DataGridMiscView(11, Index).Value)
            ActualWriter.WritePropertyName("Spotlight")
            ActualWriter.WriteValue(DataGridMiscView(12, Index).Value)
            ActualWriter.WritePropertyName("Stairs")
            ActualWriter.WriteValue(DataGridMiscView(13, Index).Value)
            ActualWriter.WritePropertyName("RingMat")
            ActualWriter.WriteValue(DataGridMiscView(14, Index).Value)
            ActualWriter.WritePropertyName("FloorMattress")
            ActualWriter.WriteValue(DataGridMiscView(15, Index).Value)
            ActualWriter.WritePropertyName("Crowd")
            ActualWriter.WriteValue(DataGridMiscView(16, Index).Value)
            ActualWriter.WritePropertyName("CrowdSeatsPlace")
            ActualWriter.WriteValue(DataGridMiscView(17, Index).Value)
            ActualWriter.WritePropertyName("CrowdSeatsModel")
            ActualWriter.WriteValue(DataGridMiscView(18, Index).Value)
            ActualWriter.WritePropertyName("IBL")
            ActualWriter.WriteValue(DataGridMiscView(19, Index).Value)
            ActualWriter.WritePropertyName("Titantron")
            ActualWriter.WriteValue(DataGridMiscView(20, Index).Value)
            ActualWriter.WritePropertyName("Minitron")
            ActualWriter.WriteValue(DataGridMiscView(21, Index).Value)
            ActualWriter.WritePropertyName("Wall_L")
            ActualWriter.WriteValue(DataGridMiscView(22, Index).Value)
            ActualWriter.WritePropertyName("Wall_R")
            ActualWriter.WriteValue(DataGridMiscView(23, Index).Value)
            ActualWriter.WritePropertyName("Header")
            ActualWriter.WriteValue(DataGridMiscView(24, Index).Value)
            ActualWriter.WritePropertyName("Floor")
            ActualWriter.WriteValue(DataGridMiscView(25, Index).Value)
            ActualWriter.WritePropertyName("MiscObjects")
            ActualWriter.WriteValue(DataGridMiscView(26, Index).Value)
            ActualWriter.WritePropertyName("LightingType")
            ActualWriter.WriteValue(DataGridMiscView(27, Index).Value)
            ActualWriter.WritePropertyName("CornerPost_CM")
            ActualWriter.WriteValue(DataGridMiscView(28, Index).Value)
            ActualWriter.WritePropertyName("Rope_CM")
            ActualWriter.WriteValue(DataGridMiscView(29, Index).Value)
            ActualWriter.WritePropertyName("Apron_CM")
            ActualWriter.WriteValue(DataGridMiscView(30, Index).Value)
            ActualWriter.WritePropertyName("Turnbuckle_CM")
            ActualWriter.WriteValue(DataGridMiscView(31, Index).Value)
            ActualWriter.WritePropertyName("RingMat_CM")
            ActualWriter.WriteValue(DataGridMiscView(32, Index).Value)
            ActualWriter.WritePropertyName("version")
            ActualWriter.WriteValue(DataGridMiscView(33, Index).Value)
            ActualWriter.WriteEndObject()
        End Using
        Return TempStringBuilder.ToString()
    End Function
    Function BuildJsonArenaFile(index As Integer) As String
        Try
            Dim DListAray As List(Of Boolean) = DataGridMiscView.Rows(index).Tag
            ' Chr(&H7B) = { Chr(&HD) = Carriage return Chr(&HA) = Line feed
            Dim TempItemCount As Integer = 0
            Dim Temp_String As String = Chr(&H7B)
            If DListAray(TempItemCount + 0) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Stadium"":" & DataGridMiscView(1, index).Value.ToString & ","
            If DListAray(TempItemCount + 1) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Advertisement"":" & DataGridMiscView(2, index).Value.ToString & ","
            If DListAray(TempItemCount + 2) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CornerPost"":" & DataGridMiscView(3, index).Value.ToString & ","
            If Not DataGridMiscView(4, index).Value = -2 Then
                If DListAray(TempItemCount + 3) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LED_CornerPost"":" & DataGridMiscView(4, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If DListAray(TempItemCount + 3) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Rope"":" & DataGridMiscView(5, index).Value.ToString & ","
            If DListAray(TempItemCount + 4) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Apron"":" & DataGridMiscView(6, index).Value.ToString & ","
            If Not DataGridMiscView(7, index).Value = -2 Then
                If DListAray(TempItemCount + 5) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LED_Apron"":" & DataGridMiscView(7, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If DListAray(TempItemCount + 5) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Turnbuckle"":" & DataGridMiscView(8, index).Value.ToString & ","
            If DListAray(TempItemCount + 6) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Barricade"":" & DataGridMiscView(9, index).Value.ToString & ","
            If DListAray(TempItemCount + 7) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Fence"":" & DataGridMiscView(10, index).Value.ToString & ","
            If DListAray(TempItemCount + 8) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CeilingLighting"":" & DataGridMiscView(11, index).Value.ToString & ","
            If DListAray(TempItemCount + 9) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Spotlight"":" & DataGridMiscView(12, index).Value.ToString & ","
            If DListAray(TempItemCount + 10) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Stairs"":" & DataGridMiscView(13, index).Value.ToString & ","
            If DListAray(TempItemCount + 11) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CommentarySeat"":" & DataGridMiscView(14, index).Value.ToString & ","
            If DListAray(TempItemCount + 12) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """RingMat"":" & DataGridMiscView(15, index).Value.ToString & ","
            If DListAray(TempItemCount + 13) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """FloorMattress"":" & DataGridMiscView(16, index).Value.ToString & ","
            If DListAray(TempItemCount + 14) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Crowd"":" & DataGridMiscView(17, index).Value.ToString & ","
            If Not DataGridMiscView(18, index).Value = -2 Then
                If DListAray(TempItemCount + 15) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CrowdSeatsPlace"":" & DataGridMiscView(18, index).Value.ToString & ","
                If DListAray(TempItemCount + 16) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CrowdSeatsModel"":" & DataGridMiscView(19, index).Value.ToString & ","
                TempItemCount += 2
            End If
            If DListAray(TempItemCount + 15) Then Temp_String += Chr(&HD)
            Temp_String = Temp_String & Chr(&HA) & "    " & """IBL"":" & DataGridMiscView(20, index).Value.ToString & ","
            If DListAray(TempItemCount + 16) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Titantron"":" & DataGridMiscView(21, index).Value.ToString & ","
            If DListAray(TempItemCount + 17) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Minitron"":" & DataGridMiscView(22, index).Value.ToString & ","
            If DListAray(TempItemCount + 18) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Wall_L"":" & DataGridMiscView(23, index).Value.ToString & ","
            If DListAray(TempItemCount + 19) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Wall_R"":" & DataGridMiscView(24, index).Value.ToString & ","
            If DListAray(TempItemCount + 20) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Header"":" & DataGridMiscView(25, index).Value.ToString & ","
            If DListAray(TempItemCount + 21) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Floor"":" & DataGridMiscView(26, index).Value.ToString & ","
            If DListAray(TempItemCount + 22) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """MiscObjects"":" & DataGridMiscView(27, index).Value.ToString & ","
            If Not DataGridMiscView(28, index).Value.ToString = -2 Then
                If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LightingType"":" & DataGridMiscView(28, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If Not DataGridMiscView(29, index).Value.ToString = -2 Then
                If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CornerPost_CM"":" & DataGridMiscView(29, index).Value.ToString & ","
                If DListAray(TempItemCount + 24) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Rope_CM"":" & DataGridMiscView(30, index).Value.ToString & ","
                If DListAray(TempItemCount + 25) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Apron_CM"":" & DataGridMiscView(31, index).Value.ToString & ","
                If DListAray(TempItemCount + 26) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Turnbuckle_CM"":" & DataGridMiscView(32, index).Value.ToString & ","
                If DListAray(TempItemCount + 27) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """RingMat_CM"":" & DataGridMiscView(33, index).Value.ToString & ","
                TempItemCount += 5
            End If
            If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """version"":" & """" & DataGridMiscView(34, index).Value.ToString & """"
            If DListAray(TempItemCount + 24) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & Chr(&H7D)
            If DListAray(TempItemCount + 25) Then Temp_String += Chr(&HD) 'DListAray(TempItemCount + 17)
            Temp_String += Chr(&HA)
            Return Temp_String
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function
#End Region
#Region "Show View Controls"
    Sub FillShowView(SelectedData As TreeNode)
        '&h74 2K15 (&h4e), 2K16 (&h78) 2K17 (&h79),
        '&h78 2K18 (&h79)
        '&h7C 2K19 (&h99)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridShowView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim StringRead As Boolean = False
        If Not StringReferences(0) = "String Not Read" Then
            StringRead = True
        End If
        StringLoadedShowMenuItem.Text = "String Loaded: " & StringRead.ToString
        Dim ShowBytes As Byte() = GetNodeBytes(SelectedData)
        Dim FileLength As Integer = ShowBytes.Length
        Dim GameTypeTest As UInt32 = BitConverter.ToInt32(ShowBytes, 8)
        Dim ShowSpacing As Integer = &H74
        Dim ShowCount As Integer = BitConverter.ToInt32(ShowBytes, 4)
        If GameTypeTest = &H19A Then '2K19
            ShowViewType.SelectedIndex = 4
            ShowSpacing = &H7C
        ElseIf GameTypeTest = &H12C Then '2K18-2K15
            If ShowCount = &H6F Then '2K18
                ShowViewType.SelectedIndex = 3
                ShowSpacing = &H78
            ElseIf ShowCount = &H69 Then '2K17
                ShowViewType.SelectedIndex = 2
                ShowSpacing = &H74
            ElseIf ShowCount = &H78 Then '2K16
                ShowViewType.SelectedIndex = 1
                ShowSpacing = &H74
            ElseIf ShowCount = &H4E Then '2K15
                ShowViewType.SelectedIndex = 0
                ShowSpacing = &H74
            Else
                MessageBox.Show("Unknown Game")
                Exit Sub
            End If
        Else
            MessageBox.Show("Unknown Game")
            Exit Sub
        End If
        Dim index As Integer = 0
        ProgressBar1.Maximum = FileLength
        ProgressBar1.Value = 0
        Dim current_poition As Long = &HC
        While current_poition < FileLength
            'TO DO Build out controls for games other than 2K19
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            Dim StringRef As UInt32 = BitConverter.ToUInt32(ShowBytes, current_poition)
            TempGridRow.Cells(0).Value = Hex(StringRef) ' Dim NameRef As String = 
            TempGridRow.Cells(1).Value = StringReferences(StringRef) 'Name as string =
            TempGridRow.Cells(2).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 4) 'Dim SelectNum As int16 =
            TempGridRow.Cells(3).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 6) 'Dim SelectNumSecond As int16 = 
            TempGridRow.Cells(4).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 8) 'Dim A1 As int16 =
            TempGridRow.Cells(5).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 10) 'Dim A2 As int16 = 
            TempGridRow.Cells(6).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 12)) 'Dim B1 As String = 
            TempGridRow.Cells(7).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 14)) 'Dim B2 As String = 
            TempGridRow.Cells(8).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 16)) 'Dim B3 As String = 
            '7 Bytes 00
            TempGridRow.Cells(9).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 25) 'Dim C1 As Boolean = 
            TempGridRow.Cells(10).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 26) ' Dim C2 As Boolean = 
            TempGridRow.Cells(11).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 27) 'Dim C3 As Boolean = 
            TempGridRow.Cells(12).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 28) ' Dim C4 As Boolean = 
            TempGridRow.Cells(13).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 29) 'Dim C5 As Boolean = 
            TempGridRow.Cells(14).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 30) 'Dim C6 As Boolean = 
            TempGridRow.Cells(15).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 31) 'Dim C7 As Boolean = 
            '2 Bytes 00
            TempGridRow.Cells(16).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 34) 'Dim C8 As Boolean = 
            TempGridRow.Cells(17).Value = ShowBytes(current_poition + 35) 'Dim Stage As byte = 
            '2 bytes 00
            TempGridRow.Cells(18).Value = ShowBytes(current_poition + 38) 'Dim D1 As byte = 
            TempGridRow.Cells(19).Value = ShowBytes(current_poition + 39) 'Dim D2 As byte = 
            TempGridRow.Cells(20).Value = ShowBytes(current_poition + 40) 'Dim D3 As byte = 
            TempGridRow.Cells(21).Value = ShowBytes(current_poition + 41) 'Dim D4 As byte = 
            '1 byte 00
            TempGridRow.Cells(22).Value = ShowBytes(current_poition + 43) 'Dim D5 As byte = 
            TempGridRow.Cells(23).Value = ShowBytes(current_poition + 44) 'Dim Ref As byte =
            'Dim Filter As String
            TempGridRow.Cells(24).Value = Hex(ShowBytes(current_poition + 45)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 46)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 47)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 48)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 49)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 50)).PadLeft(2, "0")
            '3 Bytes FF
            '2 Bytes 00
            TempGridRow.Cells(25).Value = Hex(BitConverter.ToUInt32(ShowBytes, current_poition + 56)) 'Dim F1 As String
            TempGridRow.Cells(26).Value = Hex(ShowBytes(current_poition + 60)) 'Dim F2 As String &H5A or &H00
            '3 bytes 00
            TempGridRow.Cells(27).Value = ShowBytes(current_poition + 64) 'Dim F3 As byte
            '1 Byte 00
            TempGridRow.Cells(28).Value = ShowBytes(current_poition + 66) 'Dim F4 As byte Possibly Tron
            '1 Byte 00
            TempGridRow.Cells(29).Value = ShowBytes(current_poition + 68) 'Dim H1 As byte
            TempGridRow.Cells(30).Value = ShowBytes(current_poition + 69) 'Dim H2 As byte
            TempGridRow.Cells(31).Value = ShowBytes(current_poition + 70) 'Dim H3 As byte
            TempGridRow.Cells(32).Value = ShowBytes(current_poition + 71) 'Dim H4 As byte
            '1 byte 00
            TempGridRow.Cells(33).Value = ShowBytes(current_poition + 73) 'Dim Bar As byte
            Dim temparray As Byte() = New Byte(33) {}
            Buffer.BlockCopy(ShowBytes, current_poition + 74, temparray, 0, 34)
            TempGridRow.Cells(34).Value = (BitConverter.ToString(temparray).Replace("-", ""))
            '3 byte 70
            TempGridRow.Cells(35).Value = ShowBytes(current_poition + 111) 'Dim I1 As byte
            TempGridRow.Cells(36).Value = Hex(ShowBytes(current_poition + 112)) 'Dim I2 As String
            '1 byte 00
            TempGridRow.Cells(37).Value = Hex(ShowBytes(current_poition + 114))  'Dim I3 As byte
            '2 byte 00
            TempGridRow.Cells(38).Value = ShowBytes(current_poition + 117) 'Dim live As byte
            '2 byte 00
            TempGridRow.Cells(39).Value = ShowBytes(current_poition + 120) 'Dim J As byte
            TempGridRow.HeaderCell.Value = index.ToString
            WorkingCollection.Add(TempGridRow)
            index += 1
            current_poition += ShowSpacing
            ProgressBar1.Value = current_poition
        End While
        DataGridShowView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub
    Private Sub DataGridShowView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridShowView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridShowView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 2, 3, 4, 5,
                 17, 18, 19, 20, 21, 22, 23,
                 27, 28, 29, 30, 31, 32, 33,
                 35, 38, 39
                If Not IsNumeric(MyCell.Value) OrElse
                       MyCell.Value < 0 Then
                    MyCell.Value = OldValue
                Else
                    SavePending = True
                    SaveShowChangesToolStripMenuItem.Visible = True
                End If

            Case Else 'Hex Text Required
                '0, 6, 7, 8, 34, 36, 37
                If Not HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    If e.ColumnIndex = 24 Then 'Filter
                        MyCell.Value = MyCell.Value.ToString.PadRight(12, "0")
                    ElseIf e.ColumnIndex = 25 Then 'Filter
                        MyCell.Value = MyCell.Value.ToString.PadRight(8, "0")
                    ElseIf e.ColumnIndex = 34 Then
                        MyCell.Value = MyCell.Value.ToString.PadRight(68, "0")
                    End If
                    SavePending = True
                    SaveShowChangesToolStripMenuItem.Visible = True
                End If
        End Select
    End Sub
    Private Sub SaveShowChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveShowChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildShowFile())
    End Sub
    Private Function BuildShowFile() As Byte()
        Dim ShowLength As UInt16 = &H74
        Select Case ShowViewType.SelectedIndex
            Case 4
                ShowLength = &H7C
            Case 3
                ShowLength = &H78
        End Select
        Dim ReturnedBytes As Byte() = New Byte(&HC + ShowLength * DataGridShowView.RowCount - 1) {}
        Dim ShowBytes As Byte() = GetNodeBytes(ReadNode)
        'copy the header byptes so they stay the same
        Array.Copy(ShowBytes, ReturnedBytes, &HC)
        ProgressBar1.Maximum = DataGridShowView.RowCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To DataGridShowView.RowCount - 1
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridShowView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 0, 4) 'String Ref
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 4, 2) 'Select 1
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(3).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 6, 2) 'Select 2
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(4).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 8, 2) 'A1
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(5).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 10, 2) 'A2
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(6).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 12, 2) 'B1
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(7).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 14, 2) 'B2
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(8).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 16, 2) 'B3
            If DataGridShowView.Rows(i).Cells(9).Value Then ReturnedBytes(&HC + i * ShowLength + 25) = 1 'C1
            If DataGridShowView.Rows(i).Cells(10).Value Then ReturnedBytes(&HC + i * ShowLength + 26) = 1 'C2
            If DataGridShowView.Rows(i).Cells(11).Value Then ReturnedBytes(&HC + i * ShowLength + 27) = 1 'C3
            If DataGridShowView.Rows(i).Cells(12).Value Then ReturnedBytes(&HC + i * ShowLength + 28) = 1 'C4
            If DataGridShowView.Rows(i).Cells(13).Value Then ReturnedBytes(&HC + i * ShowLength + 29) = 1 'C5
            If DataGridShowView.Rows(i).Cells(14).Value Then ReturnedBytes(&HC + i * ShowLength + 30) = 1 'C6
            If DataGridShowView.Rows(i).Cells(15).Value Then ReturnedBytes(&HC + i * ShowLength + 31) = 1 'C7
            If DataGridShowView.Rows(i).Cells(16).Value Then ReturnedBytes(&HC + i * ShowLength + 34) = 1 'C8
            ReturnedBytes(&HC + i * ShowLength + 35) = CByte(DataGridShowView.Rows(i).Cells(17).Value) 'Stage
            ReturnedBytes(&HC + i * ShowLength + 38) = CByte(DataGridShowView.Rows(i).Cells(18).Value) 'D1
            ReturnedBytes(&HC + i * ShowLength + 39) = CByte(DataGridShowView.Rows(i).Cells(19).Value) 'D2
            ReturnedBytes(&HC + i * ShowLength + 40) = CByte(DataGridShowView.Rows(i).Cells(20).Value) 'D3
            ReturnedBytes(&HC + i * ShowLength + 41) = CByte(DataGridShowView.Rows(i).Cells(21).Value) 'D4
            ReturnedBytes(&HC + i * ShowLength + 43) = CByte(DataGridShowView.Rows(i).Cells(22).Value) 'D5
            ReturnedBytes(&HC + i * ShowLength + 44) = CByte(DataGridShowView.Rows(i).Cells(23).Value) 'REF '24
            Array.Copy(HexStringToByte(DataGridShowView.Rows(i).Cells(24).Value), 0, ReturnedBytes, &HC + i * ShowLength + 45, 6) 'Filter
            ReturnedBytes(&HC + i * ShowLength + 51) = &HFF '3 Bytes FF
            ReturnedBytes(&HC + i * ShowLength + 52) = &HFF
            ReturnedBytes(&HC + i * ShowLength + 53) = &HFF
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridShowView.Rows(i).Cells(25).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 56, 4) 'F1
            ReturnedBytes(&HC + i * ShowLength + 60) = CByte("&h" & DataGridShowView.Rows(i).Cells(26).Value) 'F2
            ReturnedBytes(&HC + i * ShowLength + 64) = CByte(DataGridShowView.Rows(i).Cells(27).Value) 'F3
            ReturnedBytes(&HC + i * ShowLength + 66) = CByte(DataGridShowView.Rows(i).Cells(28).Value) 'F4
            ReturnedBytes(&HC + i * ShowLength + 68) = CByte(DataGridShowView.Rows(i).Cells(29).Value) 'H1
            ReturnedBytes(&HC + i * ShowLength + 69) = CByte(DataGridShowView.Rows(i).Cells(30).Value) 'H2
            ReturnedBytes(&HC + i * ShowLength + 70) = CByte(DataGridShowView.Rows(i).Cells(31).Value) 'H3
            ReturnedBytes(&HC + i * ShowLength + 71) = CByte(DataGridShowView.Rows(i).Cells(32).Value) 'H4
            ReturnedBytes(&HC + i * ShowLength + 73) = CByte(DataGridShowView.Rows(i).Cells(33).Value) 'Bar
            Array.Copy(HexStringToByte(DataGridShowView.Rows(i).Cells(34).Value), 0, ReturnedBytes, &HC + i * ShowLength + 74, 34) 'Unkown
            ReturnedBytes(&HC + i * ShowLength + 108) = &H70 '3 byte 70
            ReturnedBytes(&HC + i * ShowLength + 109) = &H70
            ReturnedBytes(&HC + i * ShowLength + 110) = &H70
            ReturnedBytes(&HC + i * ShowLength + 111) = CByte(DataGridShowView.Rows(i).Cells(35).Value) 'I1
            ReturnedBytes(&HC + i * ShowLength + 112) = CByte("&h" & DataGridShowView.Rows(i).Cells(36).Value) 'I2
            ReturnedBytes(&HC + i * ShowLength + 114) = CByte("&h" & DataGridShowView.Rows(i).Cells(37).Value) 'I3
            ReturnedBytes(&HC + i * ShowLength + 117) = CByte(DataGridShowView.Rows(i).Cells(38).Value) 'live
            ReturnedBytes(&HC + i * ShowLength + 118) = &HFF
            ReturnedBytes(&HC + i * ShowLength + 120) = CByte(DataGridShowView.Rows(i).Cells(39).Value) 'J
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function
#End Region
#Region "NIJB View Controls"
    'TO DO Build out other NIBJ Types
    Sub FillNIBJView(SelectedData As TreeNode)
        DataGridNIBJView.Rows.Clear()
        DataGridNIBJView.Columns.Clear()
        Dim NodeTag As NodeProperties = CType(SelectedData.Tag, NodeProperties)
        Dim NIJBBytes As Byte() = GetNodeBytes(SelectedData)
        Dim HeaderByte As Integer = BitConverter.ToInt32(NIJBBytes, &H4) '64 for Parm Light
        Dim LightCount As Integer = BitConverter.ToInt32(NIJBBytes, &H8)
        Dim ShowCount As Integer = BitConverter.ToInt32(NIJBBytes, &HC)
        Select Case HeaderByte
            Case &H64 'parameter file
                Dim Folder As String = Encoding.Default.GetChars(NIJBBytes, &H10, &H10)
                Dim Properties As String = Encoding.Default.GetChars(NIJBBytes, &H20, &H10)
                FileAttributesToolStripMenuItem.Text = Folder & " > " & Properties
                For i As Integer = 0 To LightCount - 1
                    Dim TempNewColumn As DataGridTextBoxColumn = New DataGridTextBoxColumn()
                    DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H30 + i * &H20, &H10))
                Next
                Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridNIBJView)
                Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
                ProgressBar1.Maximum = ShowCount - 1
                ProgressBar1.Value = 0
                For i As Integer = 0 To ShowCount - 1
                    DataGridNIBJView.Rows.Add()
                    For j As Integer = 0 To LightCount - 1
                        DataGridNIBJView(j, i).Value = Strings.Right(Hex(BitConverter.ToInt32(NIJBBytes, &H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadRight(8, "0"), 8)
                        DataGridNIBJView.Rows(i).Cells(j).Style.BackColor = ColorTranslator.FromHtml("#" & (DataGridNIBJView(j, i).Value.ToString.Substring(2, 6)))
                        Dim FontColor As String = Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 2)).PadLeft(2, "0") &
                            Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 1)).PadLeft(2, "0") &
                            Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadLeft(2, "0")
                        DataGridNIBJView.Rows(i).Cells(j).Style.ForeColor = ColorTranslator.FromHtml("#" & FontColor)
                    Next
                    DataGridNIBJView.Rows(i).HeaderCell.Value = i.ToString
                    ProgressBar1.Value = i
                Next
            Case &H67
                FileAttributesToolStripMenuItem.Text = "Unreadable Type"
                If LightCount = 0 Then
                    DataGridNIBJView.Columns.Add("Color0", "Color0")
                Else
                    For i As Integer = 0 To LightCount - 1
                        DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H10 + i * &H40, &H10))
                    Next
                End If
            Case &H6A
                FileAttributesToolStripMenuItem.Text = "Unreadable Type"
                If LightCount = 0 Then
                    DataGridNIBJView.Columns.Add("Color0", "Color0")
                End If
        End Select
    End Sub
    Private Sub DataGridNIBJView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridNIBJView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridNIBJView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If Not HexCheck(MyCell.Value) OrElse
            MyCell.Value.ToString.Length > 8 Then
            MyCell.Value = OldValue
        Else
            If MyCell.Value.ToString.Length < 8 Then
                MyCell.Value = MyCell.Value.ToString.PadRight(8, "0")
            End If
            MyCell.Style.BackColor = ColorTranslator.FromHtml("#" & (MyCell.Value.ToString.Substring(2, 6)))
            Dim FontColor As Color = ColorTranslator.FromHtml("#" & Hex(&HFF - MyCell.Style.BackColor.R).PadLeft(2, "0") &
                                                              Hex(&HFF - MyCell.Style.BackColor.G).PadLeft(2, "0") &
                                                              Hex(&HFF - MyCell.Style.BackColor.B).PadLeft(2, "0"))
            MyCell.Style.ForeColor = FontColor
            SavePending = True
            SaveNIBJChangesToolStripMenuItem.Visible = True
        End If
    End Sub
    Private Sub SaveNIBJChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveNIBJChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildNIBJFile())
    End Sub
    Private Function BuildNIBJFile() As Byte()
        Dim ShowBytes As Byte() = GetNodeBytes(ReadNode)
        Dim ReturnedBytes As Byte() = New Byte(ShowBytes.Length - 1) {}
        Dim LightCount As Integer = DataGridNIBJView.ColumnCount
        Dim ShowCount As Integer = DataGridNIBJView.RowCount
        Array.Copy(ShowBytes, ReturnedBytes, &H30 + LightCount * &H20)
        For i As Integer = 0 To LightCount - 1
            For j As Integer = 0 To ShowCount - 1
                Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridNIBJView.Rows(j).Cells(i).Value)), 0,
                ReturnedBytes, &H30 + (LightCount * &H20) + (i * ShowCount * 4) + (j * 4), 4)
            Next
        Next
        Return ReturnedBytes
    End Function
#End Region
#Region "Picture View Controls"
    Dim CreatedImages As List(Of String)
    Sub LoadPictureView(SelectedData As TreeNode)
        Dim PictureBytes As Byte() = GetNodeBytes(SelectedData)
        Dim ImageStream As MemoryStream = New MemoryStream(PictureBytes)
        Dim TempName As String = Path.GetTempFileName
        FileSystem.Rename(TempName, TempName + ".dds")
        TempName += ".dds"
        File.WriteAllBytes(TempName, PictureBytes)
        Process.Start(My.Settings.TexConvPath, " -ft bmp " & TempName).WaitForExit()
        Dim TempBMP As String = Path.GetDirectoryName(My.Settings.TexConvPath) &
                                Path.DirectorySeparatorChar &
                                Path.GetFileNameWithoutExtension(TempName) & ".BMP"
        Dim TempBMPLocal As String = Application.StartupPath & Path.DirectorySeparatorChar &
                                Path.GetFileNameWithoutExtension(TempName) & ".BMP"
        If File.Exists(TempBMPLocal) Then
            File.Copy(TempBMPLocal, TempBMP, True)
            File.Delete(TempBMPLocal)
        End If
        If File.Exists(TempBMP) Then
            Dim tempimage As Image
            Using TempObject = New Bitmap(TempBMP)
                tempimage = New Bitmap(TempObject)
            End Using
            PictureBox2.Image = tempimage
        Else
            MessageBox.Show("Error creating bitmap image.")
        End If
        CreatedImages.Add(TempBMP)
        File.Delete(TempName)
    End Sub
    Sub DeleteTempImages()
        PictureBox2.Image = Nothing
        For Each CurrentImage As String In CreatedImages
            Try
                If File.Exists(CurrentImage) Then
                    File.Delete(CurrentImage)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Next
        CreatedImages.Clear()
    End Sub
#End Region
#Region "Attire Editor View"
    Sub LoadAttireView(SelectedData As TreeNode)
        RemoveHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAttireView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim StringRead As Boolean = False
        If Not StringReferences(0) = "String Not Read" Then
            StringRead = True
        End If
        StringLoadedAttireMenuItem.Text = "String Loaded: " & StringRead.ToString
        Dim PacsRead As Boolean = False
        If Not PacNumbers(0) = -1 Then
            PacsRead = True
        End If
        PacsLoadedAttireMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
        Dim AttireBytes As Byte() = GetNodeBytes(SelectedData)
        Dim WrestlerCount As Integer = BitConverter.ToInt32(AttireBytes, &H8)
        Dim WrestlerPacs(WrestlerCount - 1) As Integer
        Dim AttireCount(WrestlerCount - 1) As Integer
        Dim AttireNames((WrestlerCount) * 10 - 1) As Integer
        Dim AttireEnabled((WrestlerCount) * 10 - 1) As Boolean
        Dim AttireManager((WrestlerCount) * 10 - 1) As Boolean
        Dim AttireUnlockNumber((WrestlerCount) * 10 - 1) As UInt32
        For i As Integer = 0 To WrestlerCount - 1
            WrestlerPacs(i) = BitConverter.ToUInt32(AttireBytes, &HC + &HA8 * i)
            AttireCount(i) = BitConverter.ToUInt32(AttireBytes, &H10 + &HA8 * i)
            For K As Integer = 0 To 9
                AttireNames(i * 10 + K) = BitConverter.ToUInt32(AttireBytes, &H14 + &HA8 * i + &H10 * K)
                AttireEnabled(i * 10 + K) = (AttireBytes(&H20 + &HA8 * i + &H10 * K) = &HFF)
                AttireManager(i * 10 + K) = (AttireBytes(&H20 + &HA8 * i + &H10 * K) = &H2)
                AttireUnlockNumber(i * 10 + K) = BitConverter.ToUInt32(AttireBytes, &H18 + &HA8 * i + &H10 * K)
            Next
        Next
        If StringRead Then 'True
            'Hide Strings
            DataGridAttireView.Columns(3).Visible = True
            DataGridAttireView.Columns(8).Visible = True
            DataGridAttireView.Columns(13).Visible = True
            DataGridAttireView.Columns(18).Visible = True
            DataGridAttireView.Columns(23).Visible = True
            DataGridAttireView.Columns(28).Visible = True
            DataGridAttireView.Columns(33).Visible = True
            DataGridAttireView.Columns(38).Visible = True
            DataGridAttireView.Columns(43).Visible = True
            DataGridAttireView.Columns(48).Visible = True
            If PacsRead Then 'Strings and Pacs Read
            Else 'Strings Read Only

            End If
        Else 'Pacs Read Only can't do much
            'Hide Strings
            DataGridAttireView.Columns(3).Visible = False
            DataGridAttireView.Columns(8).Visible = False
            DataGridAttireView.Columns(13).Visible = False
            DataGridAttireView.Columns(18).Visible = False
            DataGridAttireView.Columns(23).Visible = False
            DataGridAttireView.Columns(28).Visible = False
            DataGridAttireView.Columns(33).Visible = False
            DataGridAttireView.Columns(38).Visible = False
            DataGridAttireView.Columns(43).Visible = False
            DataGridAttireView.Columns(48).Visible = False
        End If
        ProgressBar1.Maximum = WrestlerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To WrestlerCount - 1
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = WrestlerPacs(i)
            TempGridRow.Cells(1).Value = AttireCount(i)
            TempGridRow.Cells(2).Value = Hex(AttireNames(i * 10 + 0))
            TempGridRow.Cells(3).Value = StringReferences(AttireNames(i * 10 + 0))
            TempGridRow.Cells(4).Value = AttireEnabled(i * 10 + 0)
            TempGridRow.Cells(5).Value = AttireManager(i * 10 + 0)
            TempGridRow.Cells(6).Value = AttireUnlockNumber(i * 10 + 0)
            TempGridRow.Cells(7).Value = Hex(AttireNames(i * 10 + 1))
            TempGridRow.Cells(8).Value = StringReferences(AttireNames(i * 10 + 1))
            TempGridRow.Cells(9).Value = AttireEnabled(i * 10 + 1)
            TempGridRow.Cells(10).Value = AttireManager(i * 10 + 1)
            TempGridRow.Cells(11).Value = AttireUnlockNumber(i * 10 + 1)
            TempGridRow.Cells(12).Value = Hex(AttireNames(i * 10 + 2))
            TempGridRow.Cells(13).Value = StringReferences(AttireNames(i * 10 + 2))
            TempGridRow.Cells(14).Value = AttireEnabled(i * 10 + 2)
            TempGridRow.Cells(15).Value = AttireManager(i * 10 + 2)
            TempGridRow.Cells(16).Value = AttireUnlockNumber(i * 10 + 2)
            TempGridRow.Cells(17).Value = Hex(AttireNames(i * 10 + 3))
            TempGridRow.Cells(18).Value = StringReferences(AttireNames(i * 10 + 3))
            TempGridRow.Cells(19).Value = AttireEnabled(i * 10 + 3)
            TempGridRow.Cells(20).Value = AttireManager(i * 10 + 3)
            TempGridRow.Cells(21).Value = AttireUnlockNumber(i * 10 + 3)
            TempGridRow.Cells(22).Value = Hex(AttireNames(i * 10 + 4))
            TempGridRow.Cells(23).Value = StringReferences(AttireNames(i * 10 + 4))
            TempGridRow.Cells(24).Value = AttireEnabled(i * 10 + 4)
            TempGridRow.Cells(25).Value = AttireManager(i * 10 + 4)
            TempGridRow.Cells(26).Value = AttireUnlockNumber(i * 10 + 4)
            TempGridRow.Cells(27).Value = Hex(AttireNames(i * 10 + 5))
            TempGridRow.Cells(28).Value = StringReferences(AttireNames(i * 10 + 5))
            TempGridRow.Cells(29).Value = AttireEnabled(i * 10 + 5)
            TempGridRow.Cells(30).Value = AttireManager(i * 10 + 5)
            TempGridRow.Cells(31).Value = AttireUnlockNumber(i * 10 + 5)
            TempGridRow.Cells(32).Value = Hex(AttireNames(i * 10 + 6))
            TempGridRow.Cells(33).Value = StringReferences(AttireNames(i * 10 + 6))
            TempGridRow.Cells(34).Value = AttireEnabled(i * 10 + 6)
            TempGridRow.Cells(35).Value = AttireManager(i * 10 + 6)
            TempGridRow.Cells(36).Value = AttireUnlockNumber(i * 10 + 6)
            TempGridRow.Cells(37).Value = Hex(AttireNames(i * 10 + 7))
            TempGridRow.Cells(38).Value = StringReferences(AttireNames(i * 10 + 7))
            TempGridRow.Cells(39).Value = AttireEnabled(i * 10 + 7)
            TempGridRow.Cells(40).Value = AttireManager(i * 10 + 7)
            TempGridRow.Cells(41).Value = AttireUnlockNumber(i * 10 + 7)
            TempGridRow.Cells(42).Value = Hex(AttireNames(i * 10 + 8))
            TempGridRow.Cells(43).Value = StringReferences(AttireNames(i * 10 + 8))
            TempGridRow.Cells(44).Value = AttireEnabled(i * 10 + 8)
            TempGridRow.Cells(45).Value = AttireManager(i * 10 + 8)
            TempGridRow.Cells(46).Value = AttireUnlockNumber(i * 10 + 8)
            TempGridRow.Cells(47).Value = Hex(AttireNames(i * 10 + 9))
            TempGridRow.Cells(48).Value = StringReferences(AttireNames(i * 10 + 9))
            TempGridRow.Cells(49).Value = AttireEnabled(i * 10 + 9)
            TempGridRow.Cells(50).Value = AttireManager(i * 10 + 9)
            TempGridRow.Cells(51).Value = AttireUnlockNumber(i * 10 + 9)
            If i > 99 Then
                TempGridRow.HeaderCell.Value = "UNREAD"
            Else
                TempGridRow.HeaderCell.Value = ""
            End If
            ProgressBar1.Value = i
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridAttireView.Rows.AddRange(WorkingCollection.ToArray())
        AddHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
    End Sub
    Private Sub DataGridAttireView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAttireView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 0 Then 'Pac Number, Verify Number <= 1024
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
               CInt(MyCell.Value) > 1024 Then
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Attire Count, Verify Number <= 10
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
               CInt(MyCell.Value) > 10 Then
                MyCell.Value = OldValue
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 0 Then 'Attire Name
            If Not HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf StringReferences(CUInt("&H" & MyCell.Value)) > &HFFFFF Then
                MyCell.Value = OldValue
            Else
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & MyCell.Value))
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 1 Then 'Attire String Does Nothing
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 2 Then 'Enabled Changed
            If MyCell.Value Then 'if enabled checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = False 'unchecks manager
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 3 Then 'Manager Changed
            If MyCell.Value Then 'if manager checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value = False 'unchecks enabled
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 4 Then 'UnlockMode Program Only
        End If
        SavePending = True
        SaveChangesAttireMenuItem.Visible = True
    End Sub
    Private Sub DataGridAttireView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If DataGridAttireView.RowCount > 1 Then
            Dim NewRow As DataGridViewRow = DataGridAttireView.Rows(e.RowIndex - 1)
            NewRow.Cells(0).Value = 0
            NewRow.Cells(0).ValueType = GetType(System.Int32)
            NewRow.Cells(1).Value = 10
            For i As Integer = 0 To 9
                NewRow.Cells(2 + i * 5).Value = Hex(&H50A2 + i)
                NewRow.Cells(2 + i * 5 + 1).Value = StringReferences(&H50A2 + i)
                NewRow.Cells(2 + i * 5 + 2).Value = True
                NewRow.Cells(2 + i * 5 + 3).Value = False
                NewRow.Cells(2 + i * 5 + 4).Value = UInt32.MaxValue
            Next
            If e.RowIndex > 99 Then
                NewRow.HeaderCell.Value = "UNREAD"
            Else
                NewRow.HeaderCell.Value = ""
            End If
        End If
    End Sub
    Private Sub DataGridAttireView_Sorted(sender As Object, e As EventArgs) Handles DataGridAttireView.Sorted
        If DataGridAttireView.RowCount > 100 Then
            For i As Integer = 0 To 99
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            Next
            For i As Integer = 100 To DataGridAttireView.RowCount - 1
                DataGridAttireView.Rows(i).HeaderCell.Value = "UNREAD"
            Next
        Else
            For i As Integer = 0 To DataGridAttireView.RowCount - 1
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            Next
        End If
    End Sub
    Private Sub SaveAttireChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesAttireMenuItem.Click
        InjectIntoNode(ReadNode, BuildAttireFile())
    End Sub
    Private Function BuildAttireFile() As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&HC + ((DataGridAttireView.RowCount - 1) * &HA8) - 1) {}
        'COS
        ReturnedBytes(0) = &H43
        ReturnedBytes(1) = &H4F
        ReturnedBytes(2) = &H53
        ReturnedBytes(4) = &H1
        ReturnedBytes(8) = DataGridAttireView.RowCount - 1 ' subtract 1 because of the added
        For i As Integer = 0 To DataGridAttireView.RowCount - 2 '2 to skip the added row
            'PacNumber
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &HC + i * &HA8, 4)
            'Attire Count
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(1).Value)), 0, ReturnedBytes, &HC + i * &HA8 + 4, 4)
            For K As Integer = 0 To 9
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridAttireView.Rows(i).Cells(2 + K * 5).Value.ToString))), 0, ReturnedBytes, &HC + i * &HA8 + K * &H10 + 8, 4)
                'Unlock Info
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(6 + K * 5).Value.ToString)), 0, ReturnedBytes, &HC + i * &HA8 + K * &H10 + 12, 4)
                If DataGridAttireView.Rows(i).Cells(4 + K * 5).Value Then
                    ReturnedBytes(&HC + i * &HA8 + K * &H10 + 20) = &HFF
                ElseIf DataGridAttireView.Rows(i).Cells(5 + K * 5).Value Then
                    ReturnedBytes(&HC + i * &HA8 + K * &H10 + 20) = 2
                End If
            Next
        Next
        Return ReturnedBytes
    End Function
#End Region
#Region "Muscle View Controls"
    'TO DO Streamline intergration with Object models whenever Object viewer is actually built.
    Sub LoadMuscleView(SelectedData As TreeNode)
        Dim MuscleBytes As Byte() = GetNodeBytes(SelectedData)
        If DataGridMuscleView.ColumnCount = 0 Then
            DataGridMuscleView.Columns.Add("Name", "Name")
            DataGridMuscleView.Columns.Add("Number1", Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        Else
            DataGridMuscleView.Columns.Add("Number" & DataGridMuscleView.ColumnCount, Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        End If
        Dim MuscleCount As Integer = BitConverter.ToInt32(MuscleBytes, &HC)
        Dim ActiveIndex As Long = &H14
        For i As Integer = 0 To MuscleCount - 1
            Dim MuscleName As String = Encoding.ASCII.GetString(MuscleBytes, ActiveIndex + 4, &H20)
            ActiveIndex = ActiveIndex + BitConverter.ToInt32(MuscleBytes, ActiveIndex)
            If DataGridMuscleView.ColumnCount = 2 Then
                DataGridMuscleView.Rows.Add(MuscleName, i)
            Else
                If GetMuscleRow(MuscleName) = -1 Then
                    DataGridMuscleView.Rows.Add(MuscleName)
                    DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, DataGridMuscleView.RowCount - 1).Value = i
                Else
                    DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, GetMuscleRow(MuscleName)).Value = i
                End If
            End If
        Next
    End Sub
    Function GetMuscleRow(MuscleName As String)
        For i As Integer = 0 To DataGridMuscleView.RowCount - 1
            If DataGridMuscleView(0, i).Value = MuscleName Then
                Return i
            End If
        Next
        Return -1
    End Function
    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If TabControl1.TabPages.Contains(MuscleView) Then
            TabControl1.SelectedIndex = 0
            TabControl1.TabPages.Remove(MuscleView)
        End If
    End Sub
#End Region
#Region "Mask View Controls"
    Sub LoadMaskView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMaskView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim MaskBytes As Byte() = GetNodeBytes(SelectedData)
        Dim MaskHeader As Integer = BitConverter.ToInt32(MaskBytes, &H4) ' should be C
        If Not MaskHeader = &HC Then
            MessageBox.Show("Unkown error with CE header")
            Exit Sub
        End If
        Dim ActiveIndex As Long = MaskHeader
        Dim ContainerCount As Integer = BitConverter.ToInt32(MaskBytes, &H8) ' Should be 2
        If ContainerCount = 0 Then
            MessageBox.Show("CE contains no masks")
            Exit Sub
        End If
        Dim CurrentMaskName As String
        '-
        'First we have to process each container, these are the M_Head and M_Body containers
        '-
        For i As Integer = 0 To ContainerCount - 1 'process each mask individuallt head then body
            Dim MaskHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex) ' should start as offset &hC, should also be &H4c
            CurrentMaskName = System.Text.Encoding.ASCII.GetString(MaskBytes, ActiveIndex + &H8, 6)
            'BitConverter.ToString(mask_array, active_offset + &H8)
            Dim MaskLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H4) ' should be &h4c if mask masks no objects
            Dim MaskedParts As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H48)
            If Not MaskedParts = 0 Then
                '-
                'Now if the container contains masked parts we have to start processing those masked parts
                '-
                ActiveIndex += MaskHeaderLength
                For j As Integer = 0 To MaskedParts - 1
                    Dim MaskedPartHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                    If Not MaskedPartHeaderLength = &H10 Then
                        MessageBox.Show("Error with Mask Header")
                        Exit Sub
                    End If
                    Dim TotalPartHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                    Dim PartNumber As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 8)
                    Dim MasksOnPart As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &HC) 'should be 1 always from what I've seen
                    If MasksOnPart = 0 Then
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_1"
                        TempGridRow.Cells(1).Value = "nil"
                        TempGridRow.Cells(2).Value = "nil"
                        WorkingCollection.Add(TempGridRow)
                        ActiveIndex += TotalPartHeaderLength
                    Else
                        '-
                        'For some reason they seem to have the capability of containing more than 1 set of mask arrays so this is just an extra layer of precaution
                        'This next for should only run once for every CE I've looked at
                        '-
                        ActiveIndex += MaskedPartHeaderLength
                        For k As Integer = 0 To MasksOnPart - 1
                            Dim TrueMaskHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                            If Not TrueMaskHeaderLength = &H10 Then
                                MessageBox.Show("Error with Mask Header")
                                Exit Sub
                            End If
                            Dim TrueMaskTotalLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                            Dim TrueMaskNumber As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 8)
                            Dim TrueMaskCount As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &HC)
                            If MasksOnPart = 0 Then
                                TempGridRow = CloneRow.Clone()
                                TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_" & TrueMaskNumber
                                TempGridRow.Cells(1).Value = "nil"
                                TempGridRow.Cells(2).Value = "nil"
                                WorkingCollection.Add(TempGridRow)
                                ActiveIndex += TrueMaskTotalLength
                            Else
                                '-
                                'Now to handle the true sets of masks each set is 8 bytes
                                '-
                                ActiveIndex += TrueMaskHeaderLength
                                For L As Integer = 0 To TrueMaskCount - 1
                                    'I only want the first line to have the part name
                                    If L = 0 Then
                                        TempGridRow = CloneRow.Clone()
                                        TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_" & TrueMaskNumber
                                        TempGridRow.Cells(1).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                                        TempGridRow.Cells(2).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                                        WorkingCollection.Add(TempGridRow)
                                        ActiveIndex += 8
                                    Else
                                        TempGridRow = CloneRow.Clone()
                                        TempGridRow.Cells(0).Value = ""
                                        TempGridRow.Cells(1).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                                        TempGridRow.Cells(2).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                                        WorkingCollection.Add(TempGridRow)
                                        ActiveIndex += 8
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            Else
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = CurrentMaskName & "_0_0"
                TempGridRow.Cells(1).Value = "nil"
                TempGridRow.Cells(2).Value = "nil"
                WorkingCollection.Add(TempGridRow)
                ActiveIndex += MaskLength
            End If
        Next
        DataGridMaskView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub
    Private Sub FillMaskDataGrid(ByVal Source As String)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMaskView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim NewFaceStringArray As String() = File.ReadAllLines(Source)
        For i As Integer = 0 To NewFaceStringArray.Count - 1 Step 2
            'MessageBox.Show(New_Faces(i))
            Dim Temp_Name As String
            Dim Temp_Object_Number As Integer = NewFaceStringArray(i).Substring(6, 1)
            If Temp_Object_Number = 0 Then
                Temp_Name = "M_Head_0_0"
            Else
                Temp_Name = "M_Body_" & Temp_Object_Number - 1 & "_0"
            End If
            Dim Face_Numbers As String() = NewFaceStringArray(i + 1).Split(",")
            Dim Individual_Faces As List(Of Integer) = New List(Of Integer)
            For Each temp_face_num As String In Face_Numbers
                Individual_Faces.Add(Integer.Parse(temp_face_num))
            Next
            Individual_Faces.Sort()
            Dim Name_Added As Boolean = False
            Dim Base_Number As Integer = Individual_Faces(0) - 1
            For J As Integer = 1 To Individual_Faces.Count - 1
                If Not Individual_Faces(J) = Individual_Faces(J - 1) + 1 Then
                    If Name_Added = False Then
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = Temp_Name
                        TempGridRow.Cells(1).Value = Base_Number
                        TempGridRow.Cells(2).Value = (Individual_Faces(J - 1) - 1)
                        WorkingCollection.Add(TempGridRow)
                        Base_Number = Individual_Faces(J) - 1
                        Name_Added = True
                    Else
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = ""
                        TempGridRow.Cells(1).Value = Base_Number
                        TempGridRow.Cells(2).Value = (Individual_Faces(J - 1) - 1)
                        WorkingCollection.Add(TempGridRow)
                        Base_Number = Individual_Faces(J) - 1
                    End If
                End If
            Next
            If Name_Added = False Then
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = Temp_Name
                TempGridRow.Cells(1).Value = Base_Number
                TempGridRow.Cells(2).Value = (Individual_Faces(Individual_Faces.Count - 1) - 1)
                WorkingCollection.Add(TempGridRow)
                Name_Added = True
            Else
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = ""
                TempGridRow.Cells(1).Value = Base_Number
                TempGridRow.Cells(2).Value = (Individual_Faces(Individual_Faces.Count - 1) - 1)
                WorkingCollection.Add(TempGridRow)
            End If
        Next
        DataGridMaskView.Rows.AddRange(WorkingCollection.ToArray())
        SaveMaskChangesToolStripMenuItem.Visible = True
    End Sub
    Private Sub ImportMasksFromTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportMasksFromTXTToolStripMenuItem.Click
        Dim OpenTxtFile As OpenFileDialog = New OpenFileDialog With {
            .FileName = "CE_mask_list.txt",
            .Filter = "Text File|*.txt|All files (*.*)|*.*"}
        If OpenTxtFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(OpenTxtFile.FileName) Then
                FillMaskDataGrid(OpenTxtFile.FileName)
            End If
        End If
    End Sub
    Private Sub ExportMaskstoTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMaskstoTXTToolStripMenuItem.Click
        Dim SaveTxtFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "CE_mask_list.txt",
            .Filter = "Text File|*.txt|All files (*.*)|*.*"}
        If DataGridMaskView.SelectedRows.Count = 0 Then
            If SaveTxtFile.ShowDialog = DialogResult.OK Then
                Dim CurrentBody As String = "New"
                Dim FaceArray As List(Of Integer) = New List(Of Integer)
                Using sw As New IO.StreamWriter(SaveTxtFile.FileName)
                    For i As Integer = 0 To DataGridMaskView.RowCount - 1
                        If DataGridMaskView.Rows(i).Cells(0).Value = "" Then 'Just some mask numbers
                            For j As Integer = CInt(DataGridMaskView.Rows(i).Cells(1).Value) To CInt(DataGridMaskView.Rows(i).Cells(2).Value)
                                FaceArray.Add(j + 1)
                            Next
                        Else 'New Body Part
                            If CurrentBody <> "New" Then
                                'We Have to Write the existing Mask to the File
                                sw.WriteLine(CurrentBody)
                                FaceArray.Sort()
                                sw.WriteLine(String.Join(",", FaceArray))
                            End If
                            If Not DataGridMaskView.Rows(i).Cells(1).Value.ToString = "nil" Then
                                FaceArray = New List(Of Integer)
                                For j As Integer = CInt(DataGridMaskView.Rows(i).Cells(1).Value) To CInt(DataGridMaskView.Rows(i).Cells(2).Value)
                                    FaceArray.Add(j + 1)
                                Next
                                If DataGridMaskView.Rows(i).Cells(0).Value.contains("Body") Then
                                    CurrentBody = "Object" & (CInt(DataGridMaskView.Rows(i).Cells(0).Value.ToString().Substring(7, 1)) + 1).ToString
                                Else
                                    CurrentBody = "Object" & (CInt(DataGridMaskView.Rows(i).Cells(0).Value.ToString().Substring(7, 1))).ToString
                                End If
                            End If
                        End If
                    Next
                End Using
                MessageBox.Show("File Saved")
            End If
        Else
            If SaveTxtFile.ShowDialog = DialogResult.OK Then
                Dim FaceArray As List(Of Integer) = New List(Of Integer)
                For Each temprow As DataGridViewRow In DataGridMaskView.SelectedRows
                    For i As Integer = CInt(temprow.Cells(1).Value) To CInt(temprow.Cells(2).Value)
                        FaceArray.Add(i + 1)
                    Next
                Next
                'sorts all the faces before putting it in the array
                FaceArray.Sort()
                '3ds max script provided by tekken
                Using sw As New IO.StreamWriter(SaveTxtFile.FileName)
                    sw.WriteLine(String.Join(",", FaceArray))
                End Using
                MessageBox.Show("File Saved")
            End If
        End If
    End Sub
#Region "Save Mask Functions"
    Private Sub SaveMaskChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMaskChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildMaskFile())
        SaveMaskChangesToolStripMenuItem.Visible = False
    End Sub
    'actively used when making a save
    Dim CurrentDataGridRow As Integer = 0
    Dim CurrentContainer As String = ""
    Dim ContainerByteArray As Byte() = New Byte() {}
    Dim ContainerCount As Integer = 0
    Dim CurrentBodyPart As Integer = -1
    Dim BodyPartArray As Byte() = New Byte() {}
    Dim BodyPartCount As Integer = 0
    Dim CurrentMask As Integer = -1
    Dim SingleMaskByteArray As Byte() = New Byte() {}
    Dim SingleArrayCount As Integer = 0
    Dim NumberByteArray As Byte() = New Byte() {}
    Private Function BuildMaskFile() As Byte()
        For Each row As DataGridViewRow In DataGridMaskView.Rows
            If row.Cells(1).Value.ToString = "nil" OrElse row.Cells(2).Value.ToString = "nil" Then
                MessageBox.Show("Please delete any nil rows before saving")
            End If
        Next
        Dim total_file As Byte() = New Byte() {}
        CurrentDataGridRow = 1
        ' row 0 must be manually processed as the starting point
        Dim SplitRowText As String() = DataGridMaskView(0, 0).Value.ToString().Split("_")
        CurrentContainer = SplitRowText(0) & "_" & SplitRowText(1)
        ContainerByteArray = New Byte(0) {}
        ContainerCount = 0
        CurrentBodyPart = CInt(SplitRowText(2))
        BodyPartArray = New Byte(0) {}
        BodyPartCount = 0
        CurrentMask = CInt(SplitRowText(3))
        SingleMaskByteArray = New Byte(0) {}
        SingleArrayCount = 0
        NumberByteArray = New Byte(7) {}
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, 0).Value)), 0, NumberByteArray, 0, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, 0).Value)), 0, NumberByteArray, 4, 4)
        Do While CurrentDataGridRow < DataGridMaskView.RowCount
            If DataGridMaskView(0, CurrentDataGridRow).Value.ToString.Length = 0 Then
                'MessageBox.Show("Adding")
                AddMoreNumbersToByteArray()
            Else
                SplitRowText = DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")
                If Not CurrentContainer = SplitRowText(0) & "_" & SplitRowText(1) Then
                    'putting the number arrays into a mask container
                    FormMaskByteArray()
                    'Make a new part together and adding it to any other parts
                    FormBodyByteArray()
                    'Now putting the container together
                    FormContainerByteArray()
                Else
                    If Not CurrentBodyPart = CInt(SplitRowText(2)) Then
                        'putting the number arrays into a mask container
                        FormMaskByteArray()
                        'Make a new part together and adding it to any other parts
                        FormBodyByteArray()
                    Else
                        If Not CurrentMask = CInt(SplitRowText(3)) Then
                            'putting the number arrays into a mask container
                            FormMaskByteArray()
                        Else
                            'This shouldn't happen, but this means it's the same part and it should just add the numbers
                            AddMoreNumbersToByteArray()
                        End If
                    End If
                End If
            End If
            CurrentDataGridRow += 1
        Loop
        'finish last package at this point
        'putting the number arrays into a mask container
        FormMaskByteArray()
        'Make a new part together and adding it to any other parts
        FormBodyByteArray()
        'Now putting the container together
        FormContainerByteArray()
        'Add the final header and then add containers to file
        total_file = New Byte(ContainerByteArray.Length + &HC - 2) {}
        'adding ceader length
        total_file(&H4) = &HC
        'adding container count
        total_file(&H8) = ContainerCount
        'copying final containers to array before writing to file
        Buffer.BlockCopy(ContainerByteArray, 0, total_file, &HC, ContainerByteArray.Length - 1)
        Return total_file
    End Function
    Sub AddMoreNumbersToByteArray()
        Dim old_length As Integer = NumberByteArray.Length
        ReDim Preserve NumberByteArray(old_length + 7)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, CurrentDataGridRow).Value)), 0, NumberByteArray, old_length, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, CurrentDataGridRow).Value)), 0, NumberByteArray, old_length + 4, 4)
    End Sub
    Sub MakeNewNumberByteArray()
        ReDim NumberByteArray(7)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, CurrentDataGridRow).Value)), 0, NumberByteArray, 0, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, CurrentDataGridRow).Value)), 0, NumberByteArray, 4, 4)
    End Sub
    Private Sub FormMaskByteArray()
        Dim old_length As Integer = SingleMaskByteArray.Length - 1
        ReDim Preserve SingleMaskByteArray((SingleMaskByteArray.Length) + (NumberByteArray.Length) + &H10 - 1)
        'length of header
        SingleMaskByteArray(old_length) = &H10
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt((NumberByteArray.Length) + &H10)), 0, SingleMaskByteArray, old_length + &H4, 4)
        'putting the mask number which should be 0
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(CurrentMask)), 0, SingleMaskByteArray, old_length + &H8, 4)
        'getting the count of numbers
        Buffer.BlockCopy(BitConverter.GetBytes(CInt((NumberByteArray.Length - 1) / 8)), 0, SingleMaskByteArray, old_length + &HC, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(NumberByteArray, 0, SingleMaskByteArray, old_length + &H10, NumberByteArray.Length)
        SingleArrayCount += 1
        ' MessageBox.Show(DataGridView1(0, current_row).Value.ToString() & vbNewLine &
        'DataGridView1(0, current_row).Value.ToString().Length)
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentMask = CInt(DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")(3))
            MakeNewNumberByteArray()
        End If
        'Make New Number Array for new mask
    End Sub
    Sub MakeNewMaskByteArray()
        SingleMaskByteArray = New Byte(0) {}
    End Sub
    Sub FormBodyByteArray()
        Dim old_length As Integer = BodyPartArray.Length - 1
        ReDim Preserve BodyPartArray(BodyPartArray.Length + SingleMaskByteArray.Length + &H10 - 2)
        'length of header
        BodyPartArray(old_length) = &H10
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(SingleMaskByteArray.Length + &H10 - 1)), 0, BodyPartArray, old_length + &H4, 4)
        'putting the body part number
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(CurrentBodyPart)), 0, BodyPartArray, old_length + &H8, 4)
        'getting the count of masks
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(SingleArrayCount)), 0, BodyPartArray, old_length + &HC, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(SingleMaskByteArray, 0, BodyPartArray, old_length + &H10, SingleMaskByteArray.Length)
        '
        BodyPartCount += 1
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentBodyPart = CInt(DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")(2))
        End If
        MakeNewMaskByteArray()
        SingleArrayCount = 0
    End Sub
    Sub MakeNewBodyPartByteArray()
        BodyPartArray = New Byte(0) {}
    End Sub
    Sub FormContainerByteArray()
        Dim old_length As Integer = ContainerByteArray.Length - 1
        ReDim Preserve ContainerByteArray(ContainerByteArray.Length + BodyPartArray.Length + &H4C - 2)
        'length of header
        ContainerByteArray(old_length) = &H4C
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(BodyPartArray.Length + &H4C - 1)), 0, ContainerByteArray, old_length + &H4, 4)
        'putting in the container name
        Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(CurrentContainer), 0, ContainerByteArray, old_length + &H8, System.Text.Encoding.ASCII.GetBytes(CurrentContainer).Length)
        'adding the count of body parts
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(BodyPartCount)), 0, ContainerByteArray, old_length + &H48, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(BodyPartArray, 0, ContainerByteArray, old_length + &H4C, BodyPartArray.Length)
        '
        ContainerCount += 1
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentContainer = DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Substring(0, 6)
        End If
        MakeNewBodyPartByteArray()
        BodyPartCount = 0
    End Sub
#End Region
#Region "Tutorial Links"
    Private Sub TutorialVideoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialVideoToolStripMenuItem.Click
        Try
            Process.Start("https://www.youtube.com/watch?v=e7WNIh-WYB4")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DSImportScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSImportScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3FYJ")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DSSelectionScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSSelectionScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3FZt")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub DSExportScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSExportScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3Fd2")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AznTutorialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AznTutorialToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/4t4q")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region
#Region "DataGridView Actions"
    Private Sub DataGridMaskView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 3 Then
                'add row of mask
                Dim temprow As DataGridViewRow = DataGridMaskView.Rows(e.RowIndex).Clone()
                temprow.Cells(0).Value = ""
                temprow.Cells(1).Value = DataGridMaskView.Rows(e.RowIndex).Cells(1).Value
                temprow.Cells(2).Value = DataGridMaskView.Rows(e.RowIndex).Cells(2).Value
                DataGridMaskView.Rows.Insert(e.RowIndex + 1, temprow)
                SavePending = True
                SaveMaskChangesToolStripMenuItem.Visible = True
            ElseIf e.ColumnIndex = 4 Then
                'delete row of mask
                DataGridMaskView.Rows.Remove(DataGridMaskView.Rows(e.RowIndex))
                SavePending = True
                SaveMaskChangesToolStripMenuItem.Visible = True
            Else
                'do nothing because a not button column at this time.
            End If
        End If
    End Sub
    Private Sub DataGridMaskView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellEnter
        If e.ColumnIndex = 0 OrElse
           e.ColumnIndex = 1 OrElse
           e.ColumnIndex = 2 Then 'Hex Text Reference Editing
            OldValue = DataGridMaskView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        End If
    End Sub
    Private Sub DataGridMaskView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMaskView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Dim NewValue As String = MyCell.Value
        If e.ColumnIndex = 0 Then 'Mask Name must be of a format M_Head_x_x
            Dim SplitUpString As String() = NewValue.Split("_")
            If SplitUpString.Count = 4 AndAlso
               SplitUpString(0) = "M" Then
                If SplitUpString(1) = "Head" OrElse
                   SplitUpString(1) = "Body" Then
                    If Not IsNumeric(SplitUpString(2)) Then
                        MyCell.Value = OldValue
                    Else
                        If Not IsNumeric(SplitUpString(3)) Then
                            MyCell.Value = OldValue
                        End If
                    End If
                Else
                    MyCell.Value = OldValue
                End If
            Else
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Start Face
            If Not IsNumeric(NewValue) Then
                MyCell.Value = OldValue
            ElseIf CInt(NewValue) < 0 Then
                MyCell.Value = OldValue
            Else
                Dim EndFace As String = DataGridMaskView.Rows(e.RowIndex).Cells(2).Value
                If Not IsNumeric(EndFace) Then ' End Face is nil and we need to make it a number
                    DataGridMaskView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value
                ElseIf CInt(NewValue) > CInt(EndFace) Then 'if we are increasing the start face we need to increas the end face
                    DataGridMaskView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then 'End Face
            If Not IsNumeric(NewValue) Then
                MyCell.Value = OldValue
            ElseIf CInt(NewValue) < 0 Then
                MyCell.Value = OldValue
            Else
                Dim StartFace As String = DataGridMaskView.Rows(e.RowIndex).Cells(1).Value
                If Not IsNumeric(StartFace) Then ' End Face is nil and we need to make it a number
                    DataGridMaskView.Rows(e.RowIndex).Cells(1).Value = MyCell.Value
                ElseIf CInt(NewValue) < CInt(StartFace) Then 'if we are decreasing the end face we need to decrease the start face
                    DataGridMaskView.Rows(e.RowIndex).Cells(1).Value = MyCell.Value
                End If
            End If
        End If
        SavePending = True
        SaveMaskChangesToolStripMenuItem.Visible = True
    End Sub
#End Region
#End Region
#Region "Object Array Controls"
    Dim ObjArrayContainerCount As Integer = 0
    Sub LoadObjectArrayView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjArrayView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim ObjArrayBytes As Byte() = GetNodeBytes(SelectedData)
        Dim ChairCount As Integer = BitConverter.ToInt32(ObjArrayBytes, &HC)
        Dim ParentStrings As String() = New String(ChairCount) {}
        ParentStrings(0) = "Base Object"
        ObjArrayContainerCount = 0
        For i As Integer = 0 To ChairCount - 1
            Dim ItemString As String = Encoding.ASCII.GetString(ObjArrayBytes, &H20 + ChairCount * 8 + i * &H30, &H10).Trim()
            Dim InternalItemCount As Int32 = BitConverter.ToInt32(ObjArrayBytes, &H48 + ChairCount * 8 + i * &H30)
            Dim StartIndex As Int32 = BitConverter.ToInt32(ObjArrayBytes, &H4C + ChairCount * 8 + i * &H30)
            TempGridRow = CloneRow.Clone()
            If InternalItemCount > 0 Then
                ObjArrayContainerCount += 1
                For j As Integer = StartIndex To StartIndex + InternalItemCount
                    ParentStrings(j) = ItemString
                Next
            End If
            TempGridRow.Cells(0).Value = i 'Absolute Index
            TempGridRow.Cells(1).Value = ParentStrings(i)  'Parent
            TempGridRow.Cells(2).Value = BitConverter.ToInt32(ObjArrayBytes, &H24 + i * 8) 'Number
            TempGridRow.Cells(3).Value = BitConverter.ToBoolean(ObjArrayBytes, &H20 + i * 8) 'Enabled
            If InternalItemCount > 0 Then
                TempGridRow.Cells(3).ReadOnly = True
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
            End If
            TempGridRow.Cells(4).Value = ItemString  'Object Name
            TempGridRow.Cells(5).Value = BitConverter.ToSingle(ObjArrayBytes, &H30 + ChairCount * 8 + i * &H30) 'X Float
            TempGridRow.Cells(6).Value = BitConverter.ToSingle(ObjArrayBytes, &H34 + ChairCount * 8 + i * &H30) 'Y Float
            TempGridRow.Cells(7).Value = BitConverter.ToSingle(ObjArrayBytes, &H38 + ChairCount * 8 + i * &H30) 'Z Float
            TempGridRow.Cells(8).Value = BitConverter.ToSingle(ObjArrayBytes, &H3C + ChairCount * 8 + i * &H30) 'RX Float
            TempGridRow.Cells(9).Value = BitConverter.ToSingle(ObjArrayBytes, &H40 + ChairCount * 8 + i * &H30) 'RY Float
            TempGridRow.Cells(10).Value = BitConverter.ToSingle(ObjArrayBytes, &H44 + ChairCount * 8 + i * &H30) 'RZ Float
            TempGridRow.Cells(11).Value = InternalItemCount 'Item Count
            TempGridRow.Cells(12).Value = StartIndex 'Start Index
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridObjArrayView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub
    Sub LoadObjectArrayView(CSVString As String())
        'Line 0 should be the header lione and ignored
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjArrayView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        ObjArrayContainerCount = 0
        For i As Integer = 1 To CSVString.Count - 1
            TempGridRow = CloneRow.Clone()
            Dim CSVValues As String() = CSVString(i).Split(",")
            Dim InternalItemCount As Int32 = CSVValues(11)
            TempGridRow.Cells(0).Value = CSVValues(0)
            TempGridRow.Cells(1).Value = CSVValues(1)
            TempGridRow.Cells(2).Value = CSVValues(2) 'Number
            TempGridRow.Cells(3).Value = CSVValues(3) 'Enabled
            If InternalItemCount > 0 Then
                ObjArrayContainerCount += 1
                TempGridRow.Cells(3).ReadOnly = True
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
            End If
            TempGridRow.Cells(4).Value = CSVValues(4)  'Object Name
            TempGridRow.Cells(5).Value = CSVValues(5) 'X Float
            TempGridRow.Cells(6).Value = CSVValues(6) 'Y Float
            TempGridRow.Cells(7).Value = CSVValues(7) 'Z Float
            TempGridRow.Cells(8).Value = CSVValues(8) 'RX Float
            TempGridRow.Cells(9).Value = CSVValues(9) 'RY Float
            TempGridRow.Cells(10).Value = CSVValues(10) 'RZ Float
            TempGridRow.Cells(11).Value = InternalItemCount 'Item Count
            TempGridRow.Cells(12).Value = CSVValues(12) 'Start Index
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridObjArrayView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub
    Private Sub DataGridObjArrayView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjArrayView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridObjArrayView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            'Case 0 OrElse 1 'Index and Parent are Read Only
            Case 2 'Integer Object Number 
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf Not (Math.Floor(MyCell.Value) = Math.Ceiling(MyCell.Value)) Then
                    MyCell.Value = OldValue
                End If
            'Case 3 Check Box does not have an invalid state to reset
            Case 4 'String for object name
                If MyCell.Value.ToString.Trim().ToString.Length > 16 Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = MyCell.Value.ToString.Trim()
                    MyCell.Value = MyCell.Value.ToString.Replace(" ", "")
                    'if it's a container object we need to update the relevent names on the datagrid..
                    If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                        'It is a container file and we need to update the lines
                        Dim StartIndex As Integer = CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(12).Value)
                        Dim EndingIndex As Integer = StartIndex + CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) - 1
                        For i As Integer = StartIndex To EndingIndex
                            DataGridObjArrayView.Rows(i).Cells(1).Value = MyCell.Value
                        Next
                    End If
                End If
            Case > 4 'Single Number Values
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                End If
                'Case 11 OrElse 12 'Item Count and Starting Index Should be Changed by User. Only by Add / Delete Buttons.
        End Select
        SaveYOBJArrayChangesToolStripMenuItem.Visible = True
    End Sub
    Private Sub DataGridObjArrayView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjArrayView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 13 Then 'add button
                If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                    'Container Object We need to add at least 1 Sub items
                    AddObjArrayContainer(e.RowIndex)
                Else
                    'Individual Item that we need to update within the container
                    AddObjArrayIndividualObject(e.RowIndex)
                End If
            ElseIf e.ColumnIndex = 14 Then 'Delete button
                If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                    'Container Object We also need to delete the sub objects
                    DeleteObjArrayContainer(e.RowIndex)
                Else
                    'Individual Item we will need to update the container object.
                    DeleteObjArrayIndividualObject(e.RowIndex)
                End If
            Else
                'do nothing
            End If
        End If
    End Sub
    Sub AddObjArrayContainer(index As Integer)
        'This function adds a new container object with 1 item at the location of index+1
        Dim Duplicaterow As DataGridViewRow = DataGridObjArrayView.Rows(index).Clone
        For i As Integer = 0 To DataGridObjArrayView.Rows(index).Cells.Count - 1
            Duplicaterow.Cells(i).Value = DataGridObjArrayView.Rows(index).Cells(i).Value
        Next
        'change Start Index and Container Count
        Duplicaterow.Cells(11).Value = 1
        Duplicaterow.Cells(12).Value = DataGridObjArrayView.Rows(index + 1).Cells(12).Value
        'Add +1 because we add a row before the command is actually called..
        Dim LastItemOfPreviousContainerToCopy As Integer = CInt(DataGridObjArrayView.Rows(index + 1).Cells(12).Value) + 1
        DataGridObjArrayView.Rows.Insert(index + 1, Duplicaterow)
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond or equal to the added object we need to +1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) >= index Then
                'if the index matches then the added item was added to the previous 
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
            End If
        Next
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) + 1
        Next
        MessageBox.Show("Adding Sub Object " & LastItemOfPreviousContainerToCopy)
        AddObjArrayIndividualObject(LastItemOfPreviousContainerToCopy, True)
    End Sub
    Sub AddObjArrayIndividualObject(index As Integer, Optional NewContainer As Boolean = False)
        'This function adds a duplicate row at index + 1, but index + 1 has to have true index updated as well
        Dim Duplicaterow As DataGridViewRow = DataGridObjArrayView.Rows(index).Clone
        For i As Integer = 0 To DataGridObjArrayView.Rows(index).Cells.Count - 1
            Duplicaterow.Cells(i).Value = DataGridObjArrayView.Rows(index).Cells(i).Value
        Next
        DataGridObjArrayView.Rows.Insert(index + 1, Duplicaterow)
        'This function runs after the index is actually added
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) + 1
        Next
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond or equal to the added object we need to +1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) = index Then
                MessageBox.Show("Matched Index")
                'if the index matches then the added item was added to the previous container... or it's a new container
                If Not NewContainer Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
                ElseIf Not CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) = 1 Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
                End If
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) >= index Then
                If Not NewContainer Then
                    'this means that this is the container that contains the added object so we need to increase the item count
                    DataGridObjArrayView.Rows(i).Cells(11).Value = CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) + 1
                End If
            End If
        Next
    End Sub
    Sub DeleteObjArrayContainer(index As Integer)
        'This function runs before the container is actually deleted
        If CInt(DataGridObjArrayView.Rows(index).Cells(11).Value) > 0 Then
            Dim LastContainedObject As Integer = (CInt(DataGridObjArrayView.Rows(index).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(index).Cells(11).Value) - 1)
            Dim FirstContainedObject As Integer = CInt(DataGridObjArrayView.Rows(index).Cells(12).Value)
            'MessageBox.Show(LastContainedObject & " to " & FirstContainedObject)
            For i As Integer = LastContainedObject To FirstContainedObject Step -1
                MessageBox.Show("Deleting Object " & i)
                DeleteObjArrayIndividualObject(i)
            Next
            'we want to skip the last 2 lines so we don't accidently delete 2 rows because the last for will call the delete container again
        Else
            MessageBox.Show("Deleting Container")
            For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
                'Update Absolute Index
                DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) - 1
            Next
            'Try To update containers
            For i As Integer = 0 To ObjArrayContainerCount
                'if the start index is beyond the deleted object  we need to -1 to the start index
                If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) - 1
                End If
            Next
            MessageBox.Show("Deleting Container Line")
            DataGridObjArrayView.Rows.RemoveAt(index)
            ObjArrayContainerCount -= 1
        End If
    End Sub
    Sub DeleteObjArrayIndividualObject(index As Integer)
        'This function runs before the index is actually deleted
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) - 1
        Next
        Dim DeleteContainer As Boolean = False
        Dim ContainertoDelete As Integer = -1
        'Try to update containers
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond the deleted object  we need to -1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) - 1
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) > index Then
                'this means that this is the container that contains the deleted object so we need to reduce the idem count
                DataGridObjArrayView.Rows(i).Cells(11).Value = CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) - 1
                'if the updated container item count = 0 we will want to delete the container as well
                If CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) = 0 Then
                    DeleteContainer = True
                    ContainertoDelete = i
                End If
            End If
        Next
        DataGridObjArrayView.Rows.RemoveAt(index)
        If DeleteContainer Then DeleteObjArrayContainer(ContainertoDelete)
    End Sub
    Private Sub SaveYOBJArrayChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveYOBJArrayChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildYOBJArrayFile())
    End Sub
    Private Function BuildYOBJArrayFile() As Byte()
        Dim ShowBytes As Byte() = GetNodeBytes(ReadNode)
        Dim ReturnedBytes As Byte() = New Byte(&H20 + ((&H30 + &H8) * DataGridObjArrayView.Rows.Count) - 1) {}
        Dim ContainerCount As UInt32 = 0
        Dim ObjectCount As UInt32 = 0
        For i As Integer = 0 To DataGridObjArrayView.Rows.Count - 1
            If CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) > 0 Then
                ContainerCount += 1
            Else
                ObjectCount = DataGridObjArrayView.Rows.Count - ContainerCount
                Exit For
            End If
        Next
        'File Header
        Array.Copy(New Byte() {&HEA, &HA1, &H59, &H0, &H1, &H0, &H0, &H0, &H1, &H0, &H0, &H0}, 0, ReturnedBytes, 0, &HC)
        'total Containers + Objects
        Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows.Count)), 0, ReturnedBytes, &HC, 4)
        'Container Count
        Array.Copy(BitConverter.GetBytes(ContainerCount), 0, ReturnedBytes, &H10, 4)
        'Object Count
        Array.Copy(BitConverter.GetBytes(ObjectCount), 0, ReturnedBytes, &H14, 4)
        Dim DataIndex As UInt32 = &H20 + (DataGridObjArrayView.Rows.Count * 8)
        For i As Integer = 0 To DataGridObjArrayView.Rows.Count - 1
            'enabled First
            If DataGridObjArrayView.Rows(i).Cells(3).Value Then
                ReturnedBytes(&H20 + i * 8) = 1
            Else
                ReturnedBytes(&H20 + i * 8) = 0
            End If
            'Item Number
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &H24 + i * 8, 4)
            'Name
            Dim ObjectName As String = DataGridObjArrayView.Rows(i).Cells(4).Value
            Dim StringBytes As Byte() = Encoding.ASCII.GetBytes(ObjectName)
            Array.Copy(StringBytes, 0, ReturnedBytes, DataIndex + i * &H30, StringBytes.Length)
            'X Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(5).Value)), 0, ReturnedBytes, DataIndex + &H10 + (i * &H30), 4)
            'Y Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(6).Value)), 0, ReturnedBytes, DataIndex + &H14 + (i * &H30), 4)
            'Z Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(7).Value)), 0, ReturnedBytes, DataIndex + &H18 + (i * &H30), 4)
            'RX Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(8).Value)), 0, ReturnedBytes, DataIndex + &H1C + (i * &H30), 4)
            'RY Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(9).Value)), 0, ReturnedBytes, DataIndex + &H20 + (i * &H30), 4)
            'RZ Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(10).Value)), 0, ReturnedBytes, DataIndex + &H24 + (i * &H30), 4)
            'Item Count
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(11).Value)), 0, ReturnedBytes, DataIndex + &H28 + (i * &H30), 4)
            'Start Index
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(12).Value)), 0, ReturnedBytes, DataIndex + &H2C + (i * &H30), 4)
        Next
        Return ReturnedBytes
    End Function
    Private Sub ExportYOBJArrayToCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportYOBJArrayToCSVToolStripMenuItem.Click
        Dim SaveCSVFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "Object Array.csv",
            .Filter = "Comma Seperated Values|*.csv|All files (*.*)|*.*"}
        If SaveCSVFile.ShowDialog = DialogResult.OK Then
            Dim headers = (From header As DataGridViewColumn In DataGridObjArrayView.Columns.Cast(Of DataGridViewColumn)()
                           Select header.HeaderText).ToArray
            Dim rows As List(Of String) = New List(Of String)
            For Each temprow As DataGridViewRow In DataGridObjArrayView.Rows
                If Not temprow.IsNewRow Then
                    Dim Tempstring As String = ""
                    For Each tempcell As DataGridViewCell In temprow.Cells
                        If temprow.Cells.IndexOf(tempcell) = 0 Then
                            Tempstring = tempcell.Value
                        ElseIf tempcell.ValueType = GetType(CheckBox) Then
                            If tempcell.Value Then
                                Tempstring += ",1"
                            Else
                                Tempstring += ",0"
                            End If
                        Else
                            Tempstring += "," & tempcell.Value.ToString.Trim()
                        End If
                    Next
                    rows.Add(Tempstring)
                End If
            Next
            Using sw As New IO.StreamWriter(SaveCSVFile.FileName)
                sw.WriteLine(String.Join(",", headers))
                For Each r In rows
                    sw.WriteLine(r)
                Next
            End Using
            MessageBox.Show("File Saved")
        End If
    End Sub
    Private Sub ImportYOBJArrayFromCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportYOBJArrayFromCSVToolStripMenuItem.Click
        Dim OpenCSVFile As OpenFileDialog = New OpenFileDialog With {
            .FileName = "Object Array.csv",
            .Filter = "Comma Seperated Values|*.csv|All files (*.*)|*.*"}
        If OpenCSVFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(OpenCSVFile.FileName) Then
                LoadObjectArrayView(File.ReadAllLines(OpenCSVFile.FileName))
                SaveYOBJArrayChangesToolStripMenuItem.Visible = True
            End If
        End If
    End Sub
#End Region
    'Left off refactoring here
    'Forms to Use CellEndEdit instead of Cell value changed as it fixes a lot of stupid handling issues.
    'TO DO Increase speed of datagrid population with collections
    'These is the potential for better programming using data binding datagrid views.
#Region "Asset View Controls"
    'TO Add Saving to this menu
    Sub LoadAssetFileView(SelectedData As TreeNode)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(SelectedData.Tag, NodeProperties)
        Dim AssetConvBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            AssetConvBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), AssetConvBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            AssetConvBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), AssetConvBytes, 0, CInt(NodeTag.length))
        End If
        Dim AssetCount As UInt32 = BitConverter.ToUInt32(AssetConvBytes, &HC)
        Dim index As UInt32 = &H18
        For i As Integer = 0 To AssetCount - 2
            Dim PacNumber As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40)
            Dim AttireNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 4)
            Dim AudioNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 8)
            Dim Check2 As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 12)
            Dim Check3 As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 16)
            Dim FileOffset As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 20)
            Dim TitantronNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 24)
            Dim MiniNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 28)
            Dim HeaderNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 32)
            Dim WallNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 36)
            Dim RampNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 40)
            Dim WallRightNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 44)
            Dim WallLeftNum As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 48)
            Dim Check4 As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 52)
            Dim Check5 As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 56)
            Dim Check6 As UInt32 = BitConverter.ToUInt32(AssetConvBytes, index + i * &H40 + 60)
            Dim FileName As String = ""
            If FileOffset > 0 Then
                Try
                    FileName = Encoding.Default.GetString(AssetConvBytes, FileOffset, &HC)
                Catch ex As Exception
                    FileName = "ERROR"
                End Try
            End If
            DataGridAssetView.Rows.Add(PacNumber, AttireNum, AudioNum, Check2, Check3, FileOffset, TitantronNum, MiniNum,
                                        HeaderNum, WallNum, RampNum, WallRightNum, WallLeftNum, Check4, Check5, Check6, FileName)
        Next
    End Sub
#End Region
    'TO DO... Combine some of these redundent Close view functions...
#Region "Title View"
    Dim TitleHeaderBytes As Byte()
    Sub CloseTitleView()
        If SavePending Then
            If MessageBox.Show("Changes have not yet been saved.  Would you like to save them now?", "Save Changes?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                InjectIntoNode(ReadNode, BuildTitleFile())
            End If
            SaveChangesTitleMenuItem.Visible = False
            SavePending = False
        End If
        ReadNode = Nothing
        TabControl1.TabPages.Remove(TitleView)
    End Sub
    Sub LoadTitleFileView(SelectedData As TreeNode)
        ReadNode = SelectedData
        DataGridTitleView.Rows.Clear()
        Dim StringRead As Boolean = False
        If Not StringReferences(0) = "String Not Read" Then
            StringRead = True
        End If
        StringLoadedTitleMenuItem.Text = "String Loaded: " & StringRead.ToString
        Dim PacsRead As Boolean = False
        If Not PacNumbers(0) = -1 Then
            PacsRead = True
        End If
        PacsLoadedTitleMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(SelectedData.Tag, NodeProperties)
        Dim TitleBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            TitleBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), TitleBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            TitleBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), TitleBytes, 0, CInt(NodeTag.length))
        End If
        'creating header to copy to 
        TitleHeaderBytes = New Byte(&H10) {}
        Array.Copy(TitleBytes, TitleHeaderBytes, &H10)
        Dim TitleCount As UInt32 = BitConverter.ToUInt32(TitleBytes, &H8)
        Dim index As UInt32 = &H10
        Dim TitleSize As Integer = NodeTag.length / TitleCount
        If TitleSize = 480 Then '480 1E0 2K19 Titles
            TitleGameComboBox.SelectedIndex = 4
        End If
        For i As Integer = 0 To TitleCount - 1
            Dim Enabled As UInt32 = BitConverter.ToUInt32(TitleBytes, index)
            Dim PropID As UInt32 = BitConverter.ToUInt32(TitleBytes, index + 4)
            Dim MenuNumber As UInt32 = BitConverter.ToUInt32(TitleBytes, index + 8)
            Dim NameRef1 As UInt32 = BitConverter.ToUInt32(TitleBytes, index + &HC)
            Dim NameRef2 As UInt32 = BitConverter.ToUInt32(TitleBytes, index + &H10)
            Dim NameRef3 As UInt32 = BitConverter.ToUInt32(TitleBytes, index + &H14)
            Dim WWEDefault1 As UInt32 = &H400 'default value for no champ
            Dim WWEDefault2 As UInt32 = &H400
            Dim UniDefault1 As UInt32 = &H400
            Dim UniDefault2 As UInt32 = &H400
            Dim Temp1 As UInt32 = 0
            Dim Temp2 As UInt32 = 0
            Dim UnlockNum As UInt32 = 0
            Dim Temp4 As UInt32 = 0
            Dim Female As Boolean = False
            Dim Tag As Boolean = False
            Dim Cuiserweight As Boolean = False
            If TitleSize = 480 Then '480 1E0 2K19 Titles

                '&h180 00 bytes
                '&h18 FF bytes
                '&h10 00 bytes
                WWEDefault1 = BitConverter.ToUInt32(TitleBytes, index + &H1C0)
                WWEDefault2 = BitConverter.ToUInt32(TitleBytes, index + &H1C4)
                UniDefault1 = BitConverter.ToUInt32(TitleBytes, index + &H1C8)
                UniDefault2 = BitConverter.ToUInt32(TitleBytes, index + &H1CC)
                Temp1 = BitConverter.ToUInt32(TitleBytes, index + &H1D0)
                Temp2 = BitConverter.ToUInt32(TitleBytes, index + &H1D4) 'contains Gender and tag information
                UnlockNum = BitConverter.ToUInt32(TitleBytes, index + &H1D8)
                Temp4 = BitConverter.ToUInt32(TitleBytes, index + &H1DC)
                'Addrow
                index += 480
            ElseIf TitleSize = 188 Then '188 BC 2K18
            End If
            Dim NameString1 As String = ""
            Dim NameString2 As String = ""
            Dim NameString3 As String = ""
            If StringRead Then
                NameString1 = StringReferences(NameRef1)
                NameString2 = StringReferences(NameRef2)
                NameString3 = StringReferences(NameRef3)
            End If
            'REad Temp2 and translate to Bools
            Dim TitleType As Integer = Temp2 Mod 8
            If TitleType >= 4 Then
                Cuiserweight = True
            End If
            TitleType = TitleType Mod 4
            If TitleType >= 2 Then
                Tag = True
            End If
            TitleType = TitleType Mod 2
            If TitleType >= 1 Then
                Female = True
            End If
            DataGridTitleView.Rows.Add(Enabled, PropID, MenuNumber, Hex(NameRef1), NameString1, Hex(NameRef2), NameString2, Hex(NameRef3), NameString3,
                                        WWEDefault1, WWEDefault2, UniDefault1, UniDefault2, Hex(Temp1), Hex(Temp2), Female, Tag, Cuiserweight, Hex(UnlockNum), Hex(Temp4))
            DataGridTitleView.Rows(i).HeaderCell.Value = i.ToString
            'TO DO set to collection to write faster
        Next
        If StringRead Then 'True
            If PacsRead Then 'Strings and Pacs Read
            Else 'Strings Read Only

            End If
        Else 'Pacs Read Only can't do much
            'Hide Strings
            DataGridTitleView.Columns(3).Visible = False
            DataGridTitleView.Columns(5).Visible = False
            DataGridTitleView.Columns(7).Visible = False
        End If
        AddHandler DataGridTitleView.CellValueChanged, AddressOf DataGridTitleView_CellValueChanged
        AddHandler DataGridTitleView.CellEnter, AddressOf DataGridTitleView_CellEnter
        'AddHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
        'Addint titles won't do anything until we figure out other information
    End Sub
    Private Sub DataGridTitleView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 0 Then 'Enabled Make sure between 0 & 6 inclusive
            If Not IsNumeric(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 OrElse
               CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 6 Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Attire Count, Verify Number <= 9999
            If Not IsNumeric(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 OrElse
               CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 9999 Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
        ElseIf e.ColumnIndex = 2 Then 'Attire Count, Verify Number <= 9999
            If Not IsNumeric(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 OrElse
               CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 9999 Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
        ElseIf e.ColumnIndex = 3 OrElse e.ColumnIndex = 5 OrElse e.ColumnIndex = 7 Then 'Attire Name
            Dim HexString As String = DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim hexcheck As Boolean = HexString.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            If Not hexcheck Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf StringReferences(CUInt("&H" & HexString)) > &HFFFFF Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            Else
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & HexString))
            End If
        ElseIf e.ColumnIndex > 8 AndAlso e.ColumnIndex < 13 Then 'Enabled Make sure between 0 & 1024 inclusive
            If Not IsNumeric(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 OrElse
                   CInt(DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 1024 Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
        ElseIf e.ColumnIndex = 13 OrElse e.ColumnIndex = 14 OrElse e.ColumnIndex = 18 OrElse e.ColumnIndex = 19 Then
            Dim HexString As String = DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim hexcheck As Boolean = HexString.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            If Not hexcheck Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
        End If
        SavePending = True
        SaveChangesTitleMenuItem.Visible = True
    End Sub
    Private Sub DataGridTitleView_CellEnter(sender As Object, e As DataGridViewCellEventArgs)
        OldValue = DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub
    Private Sub SaveTitleChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesTitleMenuItem.Click
        InjectIntoNode(ReadNode, BuildTitleFile())
        ReadNode = Nothing
        SaveChangesTitleMenuItem.Visible = False
        SavePending = False
    End Sub
    Function BuildTitleFile() As Byte()
        Dim ReturnedBytes As Byte() = New Byte() {}
        If TitleGameComboBox.SelectedIndex = 4 Then
            ReturnedBytes = New Byte(&H10 + ((DataGridTitleView.RowCount) * &H1E0) - 1) {}
            'copy first 10 bytes from existing file
            Array.Copy(TitleHeaderBytes, ReturnedBytes, &H10)
            'rewriting for the heck of it
            ReturnedBytes(8) = DataGridTitleView.RowCount
            For i As Integer = 0 To DataGridTitleView.RowCount - 1 '2 because because of a hidden row
                'Enabled Num
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &H10 + i * &H1E0, 4)
                'Prop Number
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(1).Value)), 0, ReturnedBytes, &H10 + i * &H1E0 + 4, 4)
                'Menu Number
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &H10 + i * &H1E0 + 8, 4)
                'Title Names
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridTitleView.Rows(i).Cells(3).Value))), 0, ReturnedBytes, &H10 + i * &H1E0 + &HC, 4)
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridTitleView.Rows(i).Cells(5).Value))), 0, ReturnedBytes, &H10 + i * &H1E0 + &H10, 4)
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridTitleView.Rows(i).Cells(7).Value))), 0, ReturnedBytes, &H10 + i * &H1E0 + &H14, 4)
                'Add in extra FF bytes
                Dim FBytes As Byte() = {&HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF,
                           &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF,
                           &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF}
                Array.Copy(FBytes, 0, ReturnedBytes, &H10 + i * &H1E0 + &H198, &H18)
                'Default Champs
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(9).Value)), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1C0, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(10).Value)), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1C4, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(11).Value)), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1C8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridTitleView.Rows(i).Cells(12).Value)), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1CC, 4)
                'Other Information
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridTitleView.Rows(i).Cells(13).Value))), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1D0, 4)
                Dim TempTitleType As UInt32 = CUInt("&H" & (DataGridTitleView.Rows(i).Cells(14).Value))
                TempTitleType -= TempTitleType Mod 8
                If DataGridTitleView.Rows(i).Cells(17).Value Then
                    TempTitleType += 4
                End If
                If DataGridTitleView.Rows(i).Cells(16).Value Then
                    TempTitleType += 2
                End If
                If DataGridTitleView.Rows(i).Cells(15).Value Then
                    TempTitleType += 1
                End If
                Array.Copy(BitConverter.GetBytes(TempTitleType), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1D4, 4)
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridTitleView.Rows(i).Cells(18).Value))), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1D8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridTitleView.Rows(i).Cells(19).Value))), 0, ReturnedBytes, &H10 + i * &H1E0 + &H1DC, 4)
            Next
        End If
        Return ReturnedBytes
    End Function
#End Region
#End Region
End Class
