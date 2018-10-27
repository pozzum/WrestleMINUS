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
Public Class MainForm
    Private Declare Function OodleLZ_Decompress Lib "oo2core_6_win64" (InputBuffer As Byte(), bufferSize As Long, OutputBuffer As Byte(), outputBufferSize As Long,
            a As UInt32, b As UInt32, c As ULong, d As UInt32, e As UInt32, f As UInt32, g As UInt32, h As UInt32, i As UInt32, threadModule As UInt32) As Integer
#Region "Main Form Functions"
    Friend Shared StringReferences() As String
    Friend Shared PacNumbers() As Integer
    Dim SelectedFiles() As String
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        CheckUpdate()
        SettingsCheck()
        HideTabs()
        CreatedImages = New List(Of String)
    End Sub
    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If CheckCommands() Then
            LoadParameters()
        ElseIf My.Settings.LoadHomeOnLaunch Then
            LoadHome()
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
        If My.Settings.UnrrbpePath = "" Then
            GetUnrrbpe()
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
        CheckIconicZlib()
        CheckOodle()
        My.Settings.Save()
    End Sub
    Sub HideTabs()
        If TabControl1.TabPages.Contains(StringView) Then
            TabControl1.TabPages.Remove(StringView)
        End If
        If TabControl1.TabPages.Contains(MiscView) Then
            TabControl1.TabPages.Remove(MiscView)
        End If
        If TabControl1.TabPages.Contains(ShowView) Then
            TabControl1.TabPages.Remove(ShowView)
        End If
        If TabControl1.TabPages.Contains(NIBJView) Then
            TabControl1.TabPages.Remove(NIBJView)
        End If
        If TabControl1.TabPages.Contains(PictureView) Then
            TabControl1.TabPages.Remove(PictureView)
        End If
        If TabControl1.TabPages.Contains(ObjectView) Then
            TabControl1.TabPages.Remove(ObjectView)
        End If
        If TabControl1.TabPages.Contains(AttireView) Then
            TabControl1.TabPages.Remove(AttireView)
        End If
        If TabControl1.TabPages.Contains(MuscleView) Then
            TabControl1.TabPages.Remove(MuscleView)
        End If
        If TabControl1.TabPages.Contains(MaskView) Then
            TabControl1.TabPages.Remove(MaskView)
        End If
        If TabControl1.TabPages.Contains(ObjArrayView) Then
            TabControl1.TabPages.Remove(ObjArrayView)
        End If
        If TabControl1.TabPages.Contains(AssetView) Then
            TabControl1.TabPages.Remove(AssetView)
        End If
    End Sub
    Sub CheckUpdate()
        Dim checkUpdateThread = New Thread(AddressOf OnlineVersion.CheckUpdate)
        checkUpdateThread.SetApartmentState(ApartmentState.STA)
        checkUpdateThread.Start()
    End Sub
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
                If MessageBox.Show("Home Directory Required", "Retry?", MessageBoxButtons.RetryCancel) = DialogResult.Retry Then
                    SelectHomeDirectory()
                Else
                    Application.Exit()
                End If

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
    Public Sub GetUnrrbpe(Optional FromOptions As Boolean = False)
        Dim AppDataStorage As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        If FromOptions = False Then
            If File.Exists(AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe") Then
                My.Settings.UnrrbpePath = AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe"
                Exit Sub
            Else
                If MessageBox.Show("Would you like to navigate to ""unrrbpe.exe""" & vbNewLine &
                    "this would be in any ""X-Packer"" install folder.",
                       "BPE Decompresser",
                       MessageBoxButtons.YesNo) = DialogResult.No Then
                Else
                    MessageBox.Show("Download link can be found in the options menu.")
                    My.Settings.UnrrbpePath = "Not Installed"
                    Exit Sub
                End If
            End If
        End If
        My.Settings.UnrrbpePath = "Not Installed"
        Dim UnrrbpeOpenDialog As New OpenFileDialog With {.FileName = "unrrbpe.exe", .Title = "Select unrrbpe.exe"}
        If UnrrbpeOpenDialog.ShowDialog = DialogResult.OK Then
            If Path.GetFileName(UnrrbpeOpenDialog.FileName) = "unrrbpe.exe" Then
                If MessageBox.Show("Would you like create a copy of this to the appdata?",
                                   "Copy File?",
                                   MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    FolderCheck(AppDataStorage)
                    File.Copy(UnrrbpeOpenDialog.FileName, AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe")
                    My.Settings.UnrrbpePath = AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe"
                Else
                    My.Settings.UnrrbpePath = UnrrbpeOpenDialog.FileName
                End If
            Else
                MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
            End If
        End If

    End Sub
    Public Function CheckIconicZlib(Optional FromOptions As Boolean = False)
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
    Public Function CheckOodle(Optional FromOptions As Boolean = False)
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
    Private Sub MainForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
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
        If SavePending Then
            Dim Result As Integer = MessageBox.Show("File save is pending, would you like to save?", "Save Pending", MessageBoxButtons.YesNoCancel)
            If Result = DialogResult.Cancel Then
                e.Cancel = True
            ElseIf Result = DialogResult.Yes Then
                If TabControl1.TabPages.Contains(StringView) Then
                    InjectIntoNode(ReadNode, BuildStringFile())
                End If
                If TabControl1.TabPages.Contains(MiscView) Then
                    InjectIntoNode(ReadNode, BuildMiscFile())
                End If
                If TabControl1.TabPages.Contains(AttireView) Then
                    InjectIntoNode(ReadNode, BuildAttireFile())
                End If
            End If
        End If
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
        'TO DO Add Check for file edits pending dialog box.
    End Sub
#End Region
#Region "General Tools"
    Private Sub FolderCheck(FolderPath As String)
        If Directory.Exists(FolderPath) = False Then
            Directory.CreateDirectory(FolderPath)
        End If
    End Sub
    Private Sub MoveAllItems(ByVal fromPath As String, ByVal toPath As String)
        My.Computer.FileSystem.CopyDirectory(fromPath, toPath, True)
        My.Computer.FileSystem.DeleteDirectory(fromPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub
    Private Function EndianReverse(Source As Byte(), Optional Index As Integer = 0, Optional Length As Integer = 4)
        Dim ReturnedArray As Byte() = New Byte(Length - 1) {}
        Array.Copy(Source, Index, ReturnedArray, 0, Length)
        Array.Reverse(ReturnedArray)
        Return ReturnedArray
    End Function
#End Region
#Region "File Handlers"
    Public Class NodeProperties
        Public FileType As PackageType = PackageType.Unchecked
        Public Index As UInt64
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
        PachDirectory
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
    End Enum
    Dim ActiveReader As BinaryReader
    Dim ActiveFile As String = ""
    Sub CheckFile(ByRef HostNode As TreeNode, Optional Crawl As Boolean = False)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(HostNode.Tag, NodeProperties)
        ActiveFile = HostNode.ToolTipText
        If NodeTag.FileType = PackageType.Folder Then
            If Crawl Then
                For Each TempNode As TreeNode In HostNode.Nodes
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps = CType(TempNode.Tag, NodeProperties)
                    ProgressBar1.Maximum += 1
                    ProgressBar1.Value += 1
                    If Expandable(TempNodeProps.FileType) Then
                        CheckFile(TempNode, Crawl)
                    End If
                Next
            Else
                'You should not be here.
                Exit Sub
            End If
        End If
        If Not File.Exists(ActiveFile) Then
            MessageBox.Show("File Not Found")
            Exit Sub
        End If
        Dim FileBytes As Byte() = New Byte(NodeTag.length - 1) {}
        If NodeTag.StoredData.Length > 0 Then
            Array.Copy(NodeTag.StoredData, CInt(NodeTag.Index), FileBytes, 0, CInt(NodeTag.length))
        Else
            Try
                ActiveReader = New BinaryReader(File.Open(ActiveFile, FileMode.Open, FileAccess.Read))
                ActiveReader.BaseStream.Seek(NodeTag.Index, SeekOrigin.Begin)
                FileBytes = ActiveReader.ReadBytes(NodeTag.length)
                ActiveReader.Close()
                ' MessageBox.Show(NodeTag.Index & vbNewLine & NodeTag.length)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
        'TO DO Add better memory array handler that will probably remove like 100 linees of code...
        'If Not SkipReader Then
        'ActiveReader = New BinaryReader(File.Open(HostNode.ToolTipText, FileMode.Open, FileAccess.Read))
        'End If
        Select Case NodeTag.FileType
            Case PackageType.Unchecked
                NodeTag.FileType = CheckHeaderType(0, FileBytes)
                NodeTag.StoredData = New Byte() {}
                HostNode.Tag = NodeTag
                CheckFile(HostNode, Crawl)
#Region "Primary Container Types {PAC}"
            Case PackageType.HSPC
                Dim FileCount As Integer = BitConverter.ToUInt32(FileBytes, &H38)
                Dim FileNameLength As Integer = BitConverter.ToUInt32(FileBytes, &H18)
                FileNameLength += -(FileNameLength Mod &H800) + &H1000
                For i As Integer = 0 To FileCount - 1
                    Dim FileName As String = BitConverter.ToString(FileBytes, &H800 + i * &H14, 8).ToUpper.Replace("-", "")
                    Dim TempNode As TreeNode = New TreeNode(FileName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.Index = BitConverter.ToUInt32(FileBytes, FileNameLength + i * &HC) * &H800
                    TempNodeProps.length = BitConverter.ToUInt32(FileBytes, FileNameLength + i * &HC + &H4) * &H100
                    TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, FileBytes)
                    TempNodeProps.Index += NodeTag.Index
                    TempNodeProps.StoredData = NodeTag.StoredData
                    TempNode.Tag = TempNodeProps
                    If Crawl Then
                        ProgressBar1.Maximum += 1
                        ProgressBar1.Value += 1
                        If Expandable(TempNodeProps.FileType) Then
                            CheckFile(TempNode, Crawl)
                        End If
                    End If
                    HostNode.Nodes.Add(TempNode)
                Next
            Case PackageType.EPK8
                Dim HeaderLength As Integer = BitConverter.ToUInt32(FileBytes, 4)
                Dim index As Integer = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4)
                    Dim DirectoryTreeNode As TreeNode = New TreeNode(DirectoryName)
                    DirectoryTreeNode.ToolTipText = HostNode.ToolTipText
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(FileBytes, &H800 + index + 4) / 4
                    Dim DirectoryNodeProps As NodeProperties = New NodeProperties
                    DirectoryNodeProps.StoredData = NodeTag.StoredData
                    DirectoryNodeProps.FileType = PackageType.PachDirectory
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        Dim PachName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 8)
                        Dim TempNode As TreeNode = New TreeNode(PachName)
                        TempNode.ToolTipText = HostNode.ToolTipText
                        Dim TempNodeProps As NodeProperties = New NodeProperties
                        TempNodeProps.Index = BitConverter.ToUInt32(FileBytes, &H800 + index + 8) * &H800 + &H4000
                        If i = 0 Then
                            DirectoryNodeProps.Index = TempNodeProps.Index
                        End If
                        TempNodeProps.length = BitConverter.ToUInt32(FileBytes, &H800 + index + 12) * &H100
                        DirectoryNodeProps.length += TempNodeProps.length
                        TempNodeProps.StoredData = NodeTag.StoredData
                        TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, FileBytes)
                        TempNodeProps.Index += NodeTag.Index
                        index += &H10
                        TempNode.Tag = TempNodeProps
                        If Crawl Then
                            ProgressBar1.Maximum += 1
                            ProgressBar1.Value += 1
                            If Expandable(TempNodeProps.FileType) Then
                                CheckFile(TempNode, Crawl)
                            End If
                        End If
                        DirectoryTreeNode.Nodes.Add(TempNode)
                    Next
                    DirectoryTreeNode.Tag = DirectoryNodeProps
                    If Crawl Then
                        ProgressBar1.Maximum += 1
                        ProgressBar1.Value += 1
                    End If
                    HostNode.Nodes.Add(DirectoryTreeNode)
                Loop
            Case PackageType.EPAC
                Dim HeaderLength As Integer = BitConverter.ToUInt32(FileBytes, 4)
                Dim index As Integer = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4)
                    Dim DirectoryTreeNode As TreeNode = New TreeNode(DirectoryName)
                    DirectoryTreeNode.ToolTipText = HostNode.ToolTipText
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(FileBytes, &H800 + index + 4) / 3
                    Dim DirectoryNodeProps As NodeProperties = New NodeProperties
                    DirectoryNodeProps.StoredData = NodeTag.StoredData
                    DirectoryNodeProps.FileType = PackageType.PachDirectory
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        Dim PachName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4)
                        Dim TempNode As TreeNode = New TreeNode(PachName)
                        TempNode.ToolTipText = HostNode.ToolTipText
                        Dim TempNodeProps As NodeProperties = New NodeProperties
                        TempNodeProps.Index = BitConverter.ToUInt32(FileBytes, &H800 + index + 4) * &H800 + &H4000
                        If i = 0 Then
                            DirectoryNodeProps.Index = TempNodeProps.Index
                        End If
                        TempNodeProps.length = BitConverter.ToUInt32(FileBytes, &H800 + index + 8) * &H100
                        DirectoryNodeProps.length += TempNodeProps.length
                        TempNodeProps.StoredData = NodeTag.StoredData
                        TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, FileBytes)
                        TempNodeProps.Index += NodeTag.Index
                        index += &HC
                        TempNode.Tag = TempNodeProps
                        If Crawl Then
                            ProgressBar1.Maximum += 1
                            ProgressBar1.Value += 1
                            If Expandable(TempNodeProps.FileType) Then
                                CheckFile(TempNode, Crawl)
                            End If
                        End If
                        DirectoryTreeNode.Nodes.Add(TempNode)
                    Next
                    DirectoryTreeNode.Tag = DirectoryNodeProps
                    If Crawl Then
                        ProgressBar1.Maximum += 1
                        ProgressBar1.Value += 1
                    End If
                    HostNode.Nodes.Add(DirectoryTreeNode)
                Loop
#End Region
#Region "Primary Container Types {PACH}"
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
                        Dim TempNode As TreeNode = New TreeNode(PartName)
                        TempNode.ToolTipText = HostNode.ToolTipText
                        Dim TempNodeProps As NodeProperties = New NodeProperties
                        TempNodeProps.Index = BitConverter.ToUInt32(FileBytes, TempHeaderStart + (i * &H10) + &H4)
                        TempNodeProps.length = BitConverter.ToUInt64(FileBytes, TempHeaderStart + (i * &H10) + &H8)
                        TempNodeProps.StoredData = NodeTag.StoredData
                        TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, FileBytes)
                        TempNodeProps.Index += NodeTag.Index
                        TempNode.Tag = TempNodeProps
                        If Crawl Then
                            ProgressBar1.Maximum += 1
                            ProgressBar1.Value += 1
                            If Expandable(TempNodeProps.FileType) Then
                                CheckFile(TempNode, Crawl)
                            End If
                        End If
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
                    Dim TempNode As TreeNode = New TreeNode(PartName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.Index = BitConverter.ToUInt32(FileBytes, &HC + (i * &HC)) + &H8 + Partcount * &HC
                    TempNodeProps.length = BitConverter.ToUInt32(FileBytes, &H10 + (i * &HC))
                    TempNodeProps.StoredData = NodeTag.StoredData
                    TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, FileBytes)
                    TempNodeProps.Index += NodeTag.Index
                    TempNode.Tag = TempNodeProps
                    If Crawl Then
                        ProgressBar1.Maximum += 1
                        ProgressBar1.Value += 1
                        If Expandable(TempNodeProps.FileType) Then
                            CheckFile(TempNode, Crawl)
                        End If
                    End If
                    HostNode.Nodes.Add(TempNode)
                Next

#End Region
#Region "Compression Types"
            Case PackageType.ZLIB
                Dim CompressedLength As UInt32 = BitConverter.ToUInt32(FileBytes, 8)
                Dim UncompressedLength As UInt32 = BitConverter.ToUInt32(FileBytes, 12)
                Dim input As Byte() = New Byte(CompressedLength - 1) {}
                Array.Copy(FileBytes, 16, input, 0, CInt(NodeTag.length - 16))
                Dim output As Byte() = New Byte(UncompressedLength - 1) {}
                Try
                    output = ZlibStream.UncompressBuffer(input)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    NodeTag.FileType = PackageType.bin
                    HostNode.Tag = NodeTag
                    Exit Sub
                End Try
                Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
                TempNode.ToolTipText = HostNode.ToolTipText
                Dim TempNodeProps As NodeProperties = New NodeProperties
                TempNodeProps.Index = 0
                TempNodeProps.length = output.Length
                TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, output)
                'MessageBox.Show(output.Length)
                TempNodeProps.StoredData = output
                TempNode.Tag = TempNodeProps
                If Crawl Then
                    ProgressBar1.Maximum += 1
                    ProgressBar1.Value += 1
                    If Expandable(TempNodeProps.FileType) Then
                        CheckFile(TempNode, Crawl)
                    End If
                End If
                HostNode.Nodes.Add(TempNode)
            Case PackageType.BPE
                Dim UncompressedLength As Integer = BitConverter.ToUInt32(FileBytes, &HC)
                Dim UncompressedBytes As Byte() = New Byte(UncompressedLength) {}
                Try
                    Dim TempInput As String = Path.GetTempFileName
                    Dim TempOutput As String = Path.GetTempFileName
                    File.WriteAllBytes(TempInput, FileBytes)
                    Process.Start(My.Settings.UnrrbpePath, TempInput & " " & TempOutput).WaitForExit()
                    UncompressedBytes = File.ReadAllBytes(TempOutput)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    NodeTag.FileType = PackageType.bin
                    HostNode.Tag = NodeTag
                    Exit Sub
                End Try
                Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
                TempNode.ToolTipText = HostNode.ToolTipText
                Dim TempNodeProps As NodeProperties = New NodeProperties
                TempNodeProps.Index = 0
                TempNodeProps.length = UncompressedBytes.Length
                TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, UncompressedBytes)
                'MessageBox.Show(output.Length)
                TempNodeProps.StoredData = UncompressedBytes
                TempNode.Tag = TempNodeProps
                If Crawl Then
                    ProgressBar1.Maximum += 1
                    ProgressBar1.Value += 1
                    If Expandable(TempNodeProps.FileType) Then
                        CheckFile(TempNode, Crawl)
                    End If
                End If
                HostNode.Nodes.Add(TempNode)
            Case PackageType.OODL
                Dim CompressedLength As Long = BitConverter.ToUInt32(FileBytes, &H14)
                Dim CompressedBytes As Byte() = New Byte(CompressedLength - 1) {}
                Dim LengthDiff As Int32 = FileBytes.Length - CompressedBytes.Length
                Array.Copy(FileBytes, LengthDiff, CompressedBytes, 0, CInt(CompressedBytes.Length))
                Dim UncompressedLength As Long = BitConverter.ToUInt32(FileBytes, &H10)
                Dim UncompressedBytes As Byte() = New Byte(UncompressedLength - 1) {}
                Try
                    OodleLZ_Decompress(CompressedBytes, CompressedLength, UncompressedBytes, UncompressedLength,
                                       0, 0, 0, 0, 0, 0, 0, 0, 0, 3)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    NodeTag.FileType = PackageType.bin
                    HostNode.Tag = NodeTag
                    Exit Sub
                End Try
                Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
                TempNode.ToolTipText = HostNode.ToolTipText
                Dim TempNodeProps As NodeProperties = New NodeProperties
                TempNodeProps.Index = 0
                TempNodeProps.length = UncompressedBytes.Length
                TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, UncompressedBytes)
                'MessageBox.Show(output.Length)
                TempNodeProps.StoredData = UncompressedBytes
                TempNode.Tag = TempNodeProps
                If Crawl Then
                    ProgressBar1.Maximum += 1
                    ProgressBar1.Value += 1
                    If Expandable(TempNodeProps.FileType) Then
                        CheckFile(TempNode, Crawl)
                    End If
                End If
                HostNode.Nodes.Add(TempNode)
#End Region
#Region "Library Types"
            Case PackageType.TextureLibrary
                Dim TextureCount As Integer = FileBytes(0)
                For i As Integer = 0 To TextureCount - 1
                    Dim ImageName As String = Encoding.Default.GetChars(FileBytes, i * &H20 + &H10, &H10)
                    Dim TempNode As TreeNode = New TreeNode(ImageName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.FileType = PackageType.DDS
                    TempNodeProps.length = BitConverter.ToUInt32(FileBytes, i * &H20 + &H10 + &H14)
                    TempNodeProps.Index = BitConverter.ToUInt64(FileBytes, i * &H20 + &H10 + &H18)
                    TempNodeProps.StoredData = NodeTag.StoredData
                    TempNode.Tag = TempNodeProps
                    If Crawl Then
                        ProgressBar1.Maximum += 1
                        ProgressBar1.Value += 1
                        If Expandable(TempNodeProps.FileType) Then
                            CheckFile(TempNode, Crawl)
                        End If
                    End If
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
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.FileType = PackageType.YANM
                    'getting the start offset
                    TempNodeProps.Index = (BitConverter.ToUInt32(EndianReverse(FileBytes, HeadIndex + &H24), 0)) + HeaderLength + HostNode.Index
                    'Get the file Length
                    If HeadIndex + &H20 + &H28 < HeaderLength Then
                        TempNodeProps.length = (BitConverter.ToUInt32(EndianReverse(FileBytes, HeadIndex + &H24 + &H28), 0)) + HeaderLength - TempNodeProps.Index
                    Else
                        TempNodeProps.length = YANMLength - TempNodeProps.Index + HeaderLength
                    End If
                    MessageBox.Show(TempNodeProps.length)
                    TempNodeProps.StoredData = NodeTag.StoredData
                    'add to list box
                    TempNode.Tag = TempNodeProps
                    If Crawl Then
                        ProgressBar1.Maximum += 1
                        ProgressBar1.Value += 1
                        If Expandable(TempNodeProps.FileType) Then
                            CheckFile(TempNode, Crawl)
                        End If
                    End If
                    HostNode.Nodes.Add(TempNode)
                    partcount = partcount + 1
                    HeadIndex = HeadIndex + &H28
                Loop
#Region "To be Built"
            Case PackageType.YOBJ
#End Region
#End Region

#Region "Stand Alone Files"
            Case PackageType.StringFile
            Case PackageType.bin
            Case PackageType.DDS
            Case PackageType.YANM
            Case PackageType.ArenaInfo
            Case PackageType.ShowInfo
            Case PackageType.NIBJ
            Case PackageType.bk2
            Case PackageType.CostumeFile
            Case PackageType.MuscleFile
            Case PackageType.MaskFile
            Case PackageType.YOBJArray
            Case PackageType.OFOP
            Case PackageType.YANM
            Case PackageType.VMUM
#End Region
        End Select
        HostNode.Tag = NodeTag
    End Sub
    Function CheckHeaderType(Index As Long, ByVal ByteArray As Byte())
        'To be split into 2 seperate functions once all processes are added
        Dim FirstFour As String
        'Make sure the file has bytes
        If ByteArray.Length = 0 Then
            Return PackageType.bin
        End If
        If Index > ByteArray.Length Then
            Return PackageType.bin
        End If
        'If ByteArray Is Nothing Then
        'ActiveReader.BaseStream.Seek(Index, SeekOrigin.Begin)
        'Try
        'FirstFour = ActiveReader.ReadChars(4)
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'Return PackageType.Unknown
        'End Try
        'Else
        FirstFour = Encoding.Default.GetChars(ByteArray, Index, 4)
        'End If
        If FirstFour = "HSPC" Then
            Return PackageType.HSPC
        ElseIf FirstFour = "SHDC" Then
            Return PackageType.SHDC
        ElseIf FirstFour = "EPK8" Then
            Return PackageType.EPK8
        ElseIf FirstFour = "PACH" Then
            Return PackageType.PACH
        ElseIf FirstFour = "EPAC" Then
            Return PackageType.EPAC
        ElseIf FirstFour = "ZLIB" Then
            Return PackageType.ZLIB
        ElseIf FirstFour = "YOBJ" OrElse
            FirstFour = "JBOY" Then
            Return PackageType.YOBJ
        ElseIf FirstFour = "NIBJ" Then
            Return PackageType.NIBJ
        ElseIf FirstFour = "0FOP" Then
            Return PackageType.OFOP
        ElseIf FirstFour = "YANM" Then
            Return PackageType.YANM
        ElseIf FirstFour = "OODL" Then
            Return PackageType.OODL
        ElseIf FirstFour = "VMUM" Then
            Return PackageType.VMUM
        ElseIf FirstFour.Contains("STG") Then
            Return PackageType.ShowInfo
        ElseIf FirstFour.Contains("DDS") Then
            Return PackageType.DDS 'KB2
        ElseIf FirstFour.Contains("KB2") Then
            Return PackageType.bk2
        ElseIf FirstFour.Contains("COS") Then
            Return PackageType.CostumeFile
        ElseIf FirstFour.Contains("BPE") Then
            Return PackageType.BPE
        ElseIf FirstFour.Contains("ê¡Y") Then
            Return PackageType.YOBJArray
        Else
            Dim ArenaCheck As String = ""
            If ByteArray.Length > &H14 Then
                ArenaCheck = Encoding.Default.GetChars(ByteArray, Index + &H10, 4)
            End If
            If ArenaCheck = "aren" Then
                Return PackageType.ArenaInfo
            End If
            'mask file check
            Dim MaskCheck As String = ""
            Dim TitleCheck As Integer = 0
            If ByteArray.Length > &H20 Then
                MaskCheck = Encoding.Default.GetChars(ByteArray, Index + &H14, 6)
                TitleCheck = BitConverter.ToUInt32(ByteArray, Index + &H14)
            End If
            If MaskCheck = "M_Head" OrElse
               MaskCheck = "M_Body" Then
                Return PackageType.MaskFile
            End If
            If TitleCheck = 2004 Then 'this is an unsafe check.  It just works for all known games so far.
                Return PackageType.TitleFile
            End If
            'muscle file check
            Dim MuscleCheck As String = ""
            If ByteArray.Length > &H20 Then
                MuscleCheck = Encoding.Default.GetChars(ByteArray, Index + &H18, 3)
            End If
            If MuscleCheck = "yM_" Then
                Return PackageType.MuscleFile
            End If
            'dds check
            Dim DDSCheck As String = ""
            If ByteArray.Length > &H24 Then
                DDSCheck = Encoding.Default.GetChars(ByteArray, Index + &H20, 3)
            End If
            If DDSCheck.ToLower = "dds" Then
                Return PackageType.TextureLibrary
            End If
            'dds check
            Dim YANMCheck As String = ""
            If ByteArray.Length > &H30 Then
                YANMCheck = Encoding.Default.GetChars(ByteArray, Index + &H24, 4)
            End If
            If YANMCheck.ToLower = "root" Then
                Return PackageType.YANMPack
            End If
            'String File Test
            If ActiveFile.ToLower.Contains("string") Then
                Dim StringTest As UInt32
                StringTest = BitConverter.ToUInt32(ByteArray, Index + 0)
                If StringTest = 0 Then
                    Return PackageType.StringFile
                End If
            End If
            Return PackageType.bin
        End If
    End Function
    Function Expandable(TestType As PackageType) As Boolean
        If TestType = PackageType.Unchecked OrElse
           TestType = PackageType.Folder OrElse
           TestType = PackageType.HSPC OrElse
           TestType = PackageType.EPK8 OrElse
           TestType = PackageType.EPAC OrElse
           TestType = PackageType.SHDC OrElse
           TestType = PackageType.PachDirectory OrElse
           TestType = PackageType.BPE OrElse
           TestType = PackageType.ZLIB OrElse
           TestType = PackageType.OODL OrElse
           TestType = PackageType.TextureLibrary OrElse
           TestType = PackageType.YANMPack OrElse
           TestType = PackageType.YOBJ Then
            Return True
        End If
        Return False
    End Function
#End Region
#Region "Menu Strip"

    Private Sub LoadHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadHomeToolStripMenuItem.Click
        LoadHome()
    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            SelectedFiles = OpenFileDialog1.FileNames
            LoadParameters()
        End If
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub
    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        OptionsMenu.Show()
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Help.ShowHelp(Nothing, "https://pozzum.github.io/WrestleMINUS/")
    End Sub
#End Region
#Region "TreeView Population"
    Sub LoadParameters()
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
                    TempNode.StateImageIndex = 1
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
                    TempNode.StateImageIndex = 0
                    LoadSubDirectories(SelectedFiles(i), TempNode)
                    LoadFiles(SelectedFiles(i), TempNode)
                End If
            End If
        Next
    End Sub
    Sub LoadHome()
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
        TempNode.StateImageIndex = 0
        'testing having folders first
        LoadSubDirectories(HomeDirectory, TempNode)
        LoadFiles(HomeDirectory, TempNode)
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
            TempNode.StateImageIndex = 0
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
            TempNode.StateImageIndex = 1
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
            'Application.DoEvents()
        End If
    End Sub
    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If File.Exists(e.Node.ToolTipText.ToString()) Then
            If Not CType(e.Node.Tag, NodeProperties).FileType = PackageType.bin Then
                If e.Node.Nodes.Count = 0 Then
                    CheckFile(e.Node)
                    ActiveReader.Close()
                End If
            End If
            HexViewFileName.Text = TreeView1.SelectedNode.Text
            AddHexText(TreeView1.SelectedNode)
            TextViewFileName.Text = TreeView1.SelectedNode.Text
            AddText(TreeView1.SelectedNode)
            'TO DO I'll change this to a match case at some point I swear
            'TO DO add to an extenral function so I can create a crawler.
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.StringFile Then
                'StringView
                If Not TabControl1.TabPages.Contains(StringView) Then
                    TabControl1.TabPages.Add(StringView)
                End If
                'FillStringView(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(StringView) Then
                    CloseStringView()
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.ArenaInfo Then
                'StringView
                If Not TabControl1.TabPages.Contains(MiscView) Then
                    TabControl1.TabPages.Add(MiscView)
                End If
                FillMiscView(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(MiscView) Then
                    CloseMiscView()
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.ShowInfo Then
                'StringView
                If Not TabControl1.TabPages.Contains(ShowView) Then
                    TabControl1.TabPages.Add(ShowView)
                End If
                FillShowView(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(ShowView) Then
                    TabControl1.TabPages.Remove(ShowView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.NIBJ Then
                'StringView
                If Not TabControl1.TabPages.Contains(NIBJView) Then
                    TabControl1.TabPages.Add(NIBJView)
                End If
                FillNIBJView(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(NIBJView) Then
                    TabControl1.TabPages.Remove(NIBJView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.DDS Then
                'StringView
                If Not TabControl1.TabPages.Contains(PictureView) Then
                    TabControl1.TabPages.Add(PictureView)
                End If
                If TabControl1.SelectedIndex = TabControl1.TabPages.IndexOf(PictureView) Then
                    LoadPicture(TreeView1.SelectedNode)
                End If
            Else
                    If TabControl1.TabPages.Contains(PictureView) Then
                    TabControl1.TabPages.Remove(PictureView)
                    'If My.Settings.DeleteTempBMP Then
                    'DeleteTempImages()
                    'End If
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.YOBJ Then
                If Not TabControl1.TabPages.Contains(ObjectView) Then
                    TabControl1.TabPages.Add(ObjectView)
                End If
                'LoadPicture(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(ObjectView) Then
                    TabControl1.TabPages.Remove(ObjectView)

                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.CostumeFile Then
                If Not TabControl1.TabPages.Contains(AttireView) Then
                    TabControl1.TabPages.Add(AttireView)
                End If
                LoadAttires(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(AttireView) Then
                    CloseAttireView()
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.MuscleFile Then
                If Not TabControl1.TabPages.Contains(MuscleView) Then
                    TabControl1.TabPages.Add(MuscleView)
                End If
                LoadMuscles(TreeView1.SelectedNode)
            Else
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.MaskFile Then
                If Not TabControl1.TabPages.Contains(MaskView) Then
                    TabControl1.TabPages.Add(MaskView)
                End If
                LoadMask(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(MaskView) Then
                    TabControl1.TabPages.Remove(MaskView)
                End If
            End If
            'ObjArrayView
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.YOBJArray Then
                If Not TabControl1.TabPages.Contains(ObjArrayView) Then
                    TabControl1.TabPages.Add(ObjArrayView)
                End If
                LoadObjectArray(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(ObjArrayView) Then
                    TabControl1.TabPages.Remove(ObjArrayView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.VMUM Then
                If Not TabControl1.TabPages.Contains(AssetView) Then
                    TabControl1.TabPages.Add(AssetView)
                End If
                LoadAssetFile(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(AssetView) Then
                    TabControl1.TabPages.Remove(AssetView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.TitleFile Then
                If Not TabControl1.TabPages.Contains(TitleView) Then
                    TabControl1.TabPages.Add(TitleView)
                End If
                LoadTitleFile(TreeView1.SelectedNode)
            Else
                If TabControl1.TabPages.Contains(TitleView) Then
                    TabControl1.TabPages.Remove(TitleView)
                End If
            End If
        End If
    End Sub
    'moving functions from on tree view to on tab select to reduce load times during tree movement on keyboard
    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPageIndex = TabControl1.TabPages.IndexOf(PictureView) Then
            LoadPicture(TreeView1.SelectedNode)
        ElseIf e.TabPageIndex = TabControl1.TabPages.IndexOf(StringView) Then
            FillStringView(TreeView1.SelectedNode)
        End If
    End Sub
#End Region
#Region "Context Menu Strip"
    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If Not e.Button = MouseButtons.Right Then
            Exit Sub
        End If
        TreeView1.SelectedNode = e.Node
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(e.Node.Tag, NodeProperties).FileType
        NodeTag.Index = CType(e.Node.Tag, NodeProperties).Index
        NodeTag.length = CType(e.Node.Tag, NodeProperties).length
        NodeTag.StoredData = CType(e.Node.Tag, NodeProperties).StoredData
        Dim ShownOptions As Integer = 6
        If NodeTag.FileType = PackageType.Folder Then
            ExtractToolStripMenuItem.Visible = False
            InjectToolStripMenuItem.Visible = False
            OpenToolStripMenuItem1.Visible = False
            ShownOptions -= 3
        Else
            If NodeTag.Index > 0 OrElse
                   NodeTag.StoredData.Length > 0 Then
                ExtractToolStripMenuItem.Visible = True
                InjectToolStripMenuItem.Visible = True
            Else
                ExtractToolStripMenuItem.Visible = False
                InjectToolStripMenuItem.Visible = False
                ShownOptions -= 2
            End If
            If NodeTag.FileType = PackageType.bk2 AndAlso My.Settings.RADVideoToolPath <> "Not Installed" Then
                OpenToolStripMenuItem1.Visible = True
            Else
                OpenToolStripMenuItem1.Visible = False
                ShownOptions -= 1
            End If
        End If
        If TreeView1.SelectedNode.GetNodeCount(False) > 0 Then
            CrawlToolStripMenuItem.Visible = True
            ExtractAllInPlaceToolStripMenuItem.Visible = True
            ExtractAllToToolStripMenuItem.Visible = True
        Else
            CrawlToolStripMenuItem.Visible = False
            ExtractAllInPlaceToolStripMenuItem.Visible = False
            ExtractAllToToolStripMenuItem.Visible = False
            ShownOptions -= 3
        End If
        If ShownOptions > 0 Then
            TreeViewContext.Show(TreeView1, New Point(e.X, e.Y))
        End If
    End Sub
    Private Sub ExtractToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        SaveFileDialog1.InitialDirectory = Path.GetDirectoryName(filepath)
        SaveFileDialog1.FileName = TreeView1.SelectedNode.Text & "." & NodeTag.FileType.ToString
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            extractnode(TreeView1.SelectedNode, SaveFileDialog1.FileName)
        End If
    End Sub
    Private Sub OpenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem1.Click
        'Currently Only Opens Bink Files
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(TreeView1.SelectedNode.Tag, NodeProperties).FileType
        NodeTag.Index = CType(TreeView1.SelectedNode.Tag, NodeProperties).Index
        NodeTag.length = CType(TreeView1.SelectedNode.Tag, NodeProperties).length
        NodeTag.StoredData = CType(TreeView1.SelectedNode.Tag, NodeProperties).StoredData

        Dim TronBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            TronBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), TronBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(filepath)
            TronBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), TronBytes, 0, CInt(NodeTag.length))
        End If
        filepath = Path.GetTempFileName

        File.WriteAllBytes(filepath, TronBytes)

        Process.Start(My.Settings.RADVideoToolPath, filepath)
    End Sub
    Private Sub CrawlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrawlToolStripMenuItem.Click
        Crawlnode(TreeView1.SelectedNode)
        TreeView1.SelectedNode.ExpandAll()
    End Sub
    Private Sub ExtractAllInPlaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractAllInPlaceToolStripMenuItem.Click
        Crawlnode(TreeView1.SelectedNode)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        If NodeTag.FileType = PackageType.Folder Then
            ExtractFolder = TreeView1.SelectedNode.ToolTipText & Path.DirectorySeparatorChar
        Else
            ExtractFolder = Path.GetDirectoryName(TreeView1.SelectedNode.ToolTipText) & Path.DirectorySeparatorChar
        End If
        ExtractAllNode(TreeView1.SelectedNode)
    End Sub
    Private Sub ExtractAllToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractAllToToolStripMenuItem.Click
        SaveExtractAllDialog.InitialDirectory = Path.GetDirectoryName(TreeView1.SelectedNode.ToolTipText)
        If SaveExtractAllDialog.ShowDialog() = DialogResult.OK Then
            Crawlnode(TreeView1.SelectedNode)
            Dim NodeTag As NodeProperties = New NodeProperties
            NodeTag = CType(TreeView1.SelectedNode.Tag, NodeProperties)
            ExtractFolder = Path.GetDirectoryName(SaveExtractAllDialog.FileName) & Path.DirectorySeparatorChar
            ExtractAllNode(TreeView1.SelectedNode)
        End If
    End Sub
    Private Sub InjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(TreeView1.SelectedNode.Tag, NodeProperties)
        Dim ParrentNodeTag As NodeProperties = New NodeProperties
        ParrentNodeTag = CType(TreeView1.SelectedNode.Parent.Tag, NodeProperties)
        If ParrentNodeTag.FileType = PackageType.HSPC OrElse
            ParrentNodeTag.FileType = PackageType.SHDC OrElse
            ParrentNodeTag.FileType = PackageType.EPK8 OrElse
            ParrentNodeTag.FileType = PackageType.EPAC OrElse
            ParrentNodeTag.FileType = PackageType.PachDirectory Then
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
    Sub Crawlnode(Basenode As TreeNode)
        Dim filepath As String = Basenode.ToolTipText
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = Basenode.GetNodeCount(False)
        For Each TestedNode As TreeNode In Basenode.Nodes
            Dim NodeTag As NodeProperties = New NodeProperties
            NodeTag = CType(TestedNode.Tag, NodeProperties)
            If Expandable(NodeTag.FileType) Then
                CheckFile(TestedNode, True)
            End If
            ProgressBar1.Value += 1
        Next
        ProgressBar1.Value = ProgressBar1.Maximum
    End Sub
    Sub extractnode(Sentnode As TreeNode, Savepath As String)
        Dim filepath As String = Sentnode.ToolTipText
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(Sentnode.Tag, NodeProperties)
        Dim ExtractByte As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            ExtractByte = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), ExtractByte, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(filepath)
            ExtractByte = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), ExtractByte, 0, CInt(NodeTag.length))
        End If
        File.WriteAllBytes(Savepath, ExtractByte)
    End Sub
    Dim ExtractFolder As String
    Sub ExtractAllNode(CurrentNode As TreeNode, Optional AdditonalFolders As String = "")
        For Each temporarynode As TreeNode In CurrentNode.Nodes
            Dim NodeTag As NodeProperties = New NodeProperties
            NodeTag = CType(temporarynode.Tag, NodeProperties)
            If Not NodeTag.FileType = PackageType.Folder Then
                If Not Path.GetFileName(temporarynode.ToolTipText) = temporarynode.Text Then
                    extractnode(temporarynode, ExtractFolder & AdditonalFolders &
                                   temporarynode.Text & "." & NodeTag.FileType.ToString)
                End If
            End If
            If temporarynode.GetNodeCount(False) > 0 Then
                Dim Folder As String
                If temporarynode.Text.Contains(".") Then
                    Folder = temporarynode.Text.Substring(0, temporarynode.Text.IndexOf("."))
                Else
                    Folder = temporarynode.Text
                End If
                FolderCheck(ExtractFolder & AdditonalFolders & Folder)
                ExtractAllNode(temporarynode, AdditonalFolders & Folder & Path.DirectorySeparatorChar)
            End If
        Next
    End Sub
    Function InjectIntoNode(Sentnode As TreeNode, SentBytes As Byte()) As Boolean
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(Sentnode.Tag, NodeProperties)
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
        Dim ParentNodeTag As NodeProperties = New NodeProperties
        Dim ParentNode As TreeNode = New TreeNode
        If Not IsNothing(Sentnode.Parent) Then
            ParentNode = Sentnode.Parent
            ParentNodeTag = CType(Sentnode.Parent.Tag, NodeProperties)
        Else
            ParentNode = Sentnode
            ParentNodeTag.StoredData = New Byte() {}
            ParentNodeTag.length = 0
        End If
        If Not ParentNodeTag.length = 0 Then
            'skipping pachdirectories
            Dim DirectoryIndex As Integer = -1
            If ParentNodeTag.FileType = PackageType.PachDirectory Then
                DirectoryIndex = Sentnode.Parent.Index
                MessageBox.Show("Directory Skipped" & vbNewLine & DirectoryIndex)
                ParentNodeTag = CType(Sentnode.Parent.Parent.Tag, NodeProperties)
            End If
            'MessageBox.Show(ParentNodeTag.FileType.ToString)
            Dim ParentBytes As Byte() = New Byte(ParentNodeTag.length) {}
            If ParentNodeTag.StoredData.Length > 0 Then
                Array.Copy(ParentNodeTag.StoredData, CInt(ParentNodeTag.Index), ParentBytes, 0, CInt(ParentNodeTag.length))
            Else
                Try
                    ActiveReader = New BinaryReader(File.Open(ActiveFile, FileMode.Open, FileAccess.Read))
                    ActiveReader.BaseStream.Seek(ParentNodeTag.Index, SeekOrigin.Begin)
                    ParentBytes = ActiveReader.ReadBytes(ParentNodeTag.length)
                    ActiveReader.Close()
                    ' MessageBox.Show(NodeTag.Index & vbNewLine & NodeTag.length)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            End If
            'adjust length if needed
            If ParentNodeTag.FileType = PackageType.HSPC OrElse
                ParentNodeTag.FileType = PackageType.EPK8 OrElse
                 ParentNodeTag.FileType = PackageType.EPAC Then
                If SizeDifference > 0 Then
                    SizeDifference += (&H800 - SizeDifference Mod &H800)
                Else
                    SizeDifference -= (&H800 - Math.Abs(SizeDifference) Mod &H800)
                End If
            End If
            'Create Byte Array of length
            Dim WrittenFileArray As Byte() = New Byte(ParentNodeTag.length + SizeDifference) {}
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
                'adjust total file length
                Array.Copy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length / &H10)), 0, WrittenFileArray, &H3C, 4)
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
                                Dim OldIndex As UInt32 = BitConverter.ToUInt32(WrittenFileArray, &H800 + index + 8)
                                MessageBox.Show(Hex(OldIndex))
                                Dim TempIndex As UInt64 = OldIndex * &H800 + SizeDifference
                                MessageBox.Show(Hex(TempIndex) / &H800)
                                Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, &H800 + index + 8, 4)
                            End If
                        Else ' directory > 
                            Dim OldIndex As UInt32 = BitConverter.ToUInt32(WrittenFileArray, &H800 + index + 8)
                            MessageBox.Show(Hex(OldIndex))
                            Dim TempIndex As UInt64 = OldIndex * &H800 + SizeDifference
                            MessageBox.Show(Hex(TempIndex / &H800))
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
            End If

            If ParentNodeTag.Index = 0 AndAlso
            ParentNodeTag.StoredData.Length = 0 Then
                'File to be Written
                Dim WrittenFile As String = Sentnode.ToolTipText
                If My.Settings.BackupInjections Then
                    File.Copy(WrittenFile, WrittenFile & ".bak", True)
                End If
                File.WriteAllBytes(WrittenFile, WrittenFileArray)
                'resettreebranch
                Dim TempName As String = ParentNode.Text
                ParentNode.Nodes.Clear()
                ParentNode.Text = TempName
                ParentNode.ToolTipText = WrittenFile
                ParentNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                    .Index = 0,
                    .length = WrittenFileArray.Length,
                    .StoredData = New Byte() {}}
                CheckFile(ParentNode)
            Else
                'we must go higher
                InjectIntoNode(Sentnode.Parent, WrittenFileArray)
            End If
        Else
            Dim WrittenFile As String = Sentnode.ToolTipText
            If My.Settings.BackupInjections Then
                File.Copy(WrittenFile, WrittenFile & ".bak", True)
            End If
            'TO DO ADD handle for injecting uncompressed bytes into compressed current as there is some issue.
            File.WriteAllBytes(WrittenFile, SentBytes)
        End If
        Return True
    End Function
#End Region
#Region "Hex View Controls"
    Sub AddHexText(SelectedFilePath As TreeNode)
        If File.Exists(SelectedFilePath.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If HexViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(HexViewBitWidth.Text)
            Else
                bitwidth = CInt(HexViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As NodeProperties = CType(SelectedFilePath.Tag, NodeProperties)
            Dim Filebytes As Byte()
            If NodeTag.StoredData.Length > 0 Then
                Filebytes = NodeTag.StoredData
            Else
                Filebytes = File.ReadAllBytes(SelectedFilePath.ToolTipText)
            End If
            Dim HexLength As Long = 0
            If NodeTag.length = 0 Then
                HexLength = Filebytes.Length
            Else
                HexLength = NodeTag.length
            End If
            Dim ByteString As String = ""
            Try
                If HexLength < (&H1000 * My.Settings.HexViewLength) Then
                    ByteString = (BitConverter.ToString(Filebytes, CInt(NodeTag.Index), HexLength).Replace("-", " "))
                Else
                    ByteString = (BitConverter.ToString(Filebytes, CInt(NodeTag.Index), (&H1000 * My.Settings.HexViewLength)).Replace("-", " "))
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            'SelectedFilePath = SelectedFilePath.Replace(vbCr, "").Replace(vbLf, "")
            Dim builder As New StringBuilder(ByteString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 3)
            For i As Int32 = startIndex To (bitwidth * 3) Step -(bitwidth * 3)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Hex_Selected.Text = builder.ToString()
        End If
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles HexViewBitWidth.TextChanged
        If HexViewBitWidth.Text.Length > 0 AndAlso
           CInt(HexViewBitWidth.Text) > 0 Then
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
    Sub AddText(SelectedFilePath As TreeNode)
        If File.Exists(SelectedFilePath.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If TextViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(TextViewBitWidth.Text)
            Else
                bitwidth = CInt(TextViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As NodeProperties = CType(SelectedFilePath.Tag, NodeProperties)
            Dim Filebytes As Byte()
            If NodeTag.StoredData.Length > 0 Then
                Filebytes = NodeTag.StoredData
            Else
                Filebytes = File.ReadAllBytes(SelectedFilePath.ToolTipText)
            End If
            Dim TextString As String = ""
            Dim HexLength As Long = 0
            If NodeTag.length = 0 Then
                HexLength = Filebytes.Length
            Else
                HexLength = NodeTag.length
            End If
            If HexLength < (&H1000 * My.Settings.HexViewLength) Then
                TextString = New String(".", HexLength)
            Else
                TextString = New String(".", (&H1000 * My.Settings.HexViewLength))
            End If
            Dim FirstBuilder As New StringBuilder(TextString)
            For i As Integer = 0 To TextString.Length - 1
                If Filebytes(i + NodeTag.Index) > 31 AndAlso (Filebytes(i + NodeTag.Index) < 257) Then
                    FirstBuilder(i) = Encoding.Default.GetChars(Filebytes, i + NodeTag.Index, 1)(0)
                End If
            Next
            TextString = FirstBuilder.ToString()
            TextString = TextString.Replace(vbCr, ".").Replace(vbLf, ".")
            Dim builder As New StringBuilder(TextString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 1)
            For i As Int32 = startIndex To (bitwidth * 1) Step -(bitwidth * 1)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Text_Selected.Text = builder.ToString()
        End If
    End Sub
    Private Sub TextViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles TextViewBitWidth.TextChanged
        If TextViewBitWidth.Text.Length > 0 AndAlso
           CInt(TextViewBitWidth.Text) > 0 Then
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
    'Injection Properties used across multiple forms
    Dim SavePending As Boolean = False
    Dim ReadNode As TreeNode
    Dim SizeChange As Integer
    Dim OldValue
#Region "String View Controls"
    Sub CloseStringView()
        If SavePending Then
            If MessageBox.Show("Changes have not yet been saved.  Would you like to save them now?", "Save Changes?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                InjectIntoNode(ReadNode, BuildStringFile())
            End If
            SaveStringChangesToolStripMenuItem.Visible = False
            SavePending = False
        End If
        ReadNode = Nothing
        TabControl1.TabPages.Remove(StringView)
    End Sub
    Dim StringFileOffset() As Integer
    Dim StringFileLength() As Integer
    Dim StringFileReference() As Integer
    Dim StringCount As Integer
    Sub FillStringView(SelectedData As TreeNode)
        ReadNode = SelectedData
        SizeChange = 0
        Dim Testing As String = ""
        DataGridStringView.Rows.Clear()
        DataGridStringView.Rows.Add()
        Dim CloneRow As DataGridViewRow = DataGridStringView.Rows(0).Clone()
        DataGridStringView.Rows.Clear()
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Try
            Dim NodeTag As NodeProperties = CType(SelectedData.Tag, NodeProperties)
            Dim StringBytes As Byte()
            If NodeTag.StoredData.Length > 0 Then
                StringBytes = NodeTag.StoredData
            Else
                Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
                StringBytes = New Byte(NodeTag.length - 1) {}
                Array.Copy(FileBytes, CInt(NodeTag.Index), StringBytes, 0, CInt(NodeTag.length))
            End If
            StringCount = BitConverter.ToInt32(StringBytes, 4)
            StringCountToolStripMenuItem.Text = "String Count: " & StringCount
            ProgressBar1.Maximum = StringCount - 1
            ProgressBar1.Value = 0
            'Get Data On the Pach parts
            ReDim StringFileOffset(Int16.MaxValue)
            ReDim StringFileLength(Int16.MaxValue)
            ReDim StringFileReference(Int16.MaxValue)
            For j As Integer = 0 To StringCount - 1
                StringFileOffset(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 0)
                StringFileLength(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 4)
                StringFileReference(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 8)
                Testing = StringFileReference(j)
                'Trim all 00 chars so the strings don't end abrubtly in future manipulation
                StringReferences(StringFileReference(j)) = Encoding.Default.GetString(StringBytes, StringFileOffset(j), StringFileLength(j)).TrimEnd(Chr(0))
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = Hex(StringFileReference(j))
                TempGridRow.Cells(1).Value = StringReferences(StringFileReference(j))
                TempGridRow.Cells(2).Value = StringFileLength(j).ToString
                TempGridRow.Cells(3).Value = False
                WorkingCollection.Add(TempGridRow)
                ProgressBar1.Value = j
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & Testing)
        End Try
        DataGridStringView.Rows.AddRange(WorkingCollection.ToArray())
        AddHandler DataGridStringView.CellValueChanged, AddressOf DataGridStringView_CellValueChanged
        AddHandler DataGridStringView.CellEnter, AddressOf DataGridStringView_CellEnter
    End Sub
    Private Sub SaveChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveStringChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildStringFile())
        ReadNode = Nothing
        SaveStringChangesToolStripMenuItem.Visible = False
        SavePending = False
    End Sub
    Dim OldString As String = ""
    Dim OldLength As Integer
    Dim LengthTheSame As Boolean = False
    Dim ChangePending As Boolean = False

    Private Sub DataGridStringView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If ChangePending Then
            ChangePending = False
            Exit Sub
        End If
        If e.ColumnIndex = 1 Then
            If LengthTheSame Then
                SizeChange += (DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1) -
                    DataGridStringView.Rows(e.RowIndex).Cells(2).Value
                DataGridStringView.Rows(e.RowIndex).Cells(2).Value = DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1
            Else
                If MessageBox.Show("String currently contains extra characters." & vbNewLine &
                                  "Would you like to maintain the length",
                                   "Potential Super String Detected!",
                                   MessageBoxButtons.YesNo) = DialogResult.No Then
                    ChangePending = True
                    SizeChange += (DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1) -
                                    DataGridStringView.Rows(e.RowIndex).Cells(2).Value
                    DataGridStringView.Rows(e.RowIndex).Cells(2).Value = DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1
                Else 'check if new string is too long
                    If DataGridStringView.Rows(e.RowIndex).Cells(2).Value < DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1 Then
                        MessageBox.Show("String is too long, string will be truncated.")
                        ChangePending = True
                        DataGridStringView.Rows(e.RowIndex).Cells(1).Value =
                            DataGridStringView.Rows(e.RowIndex).Cells(1).Value.ToString.Substring(0, DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
                    End If
                End If
            End If
            SavePending = True
            SaveStringChangesToolStripMenuItem.Visible = True
            DataGridStringView.Rows(e.RowIndex).Cells(3).Value = True
        ElseIf e.ColumnIndex = 2 Then
            If DataGridStringView.Rows(e.RowIndex).Cells(2).Value < DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1 Then
                MessageBox.Show("String is too long, string will be truncated.")
                DataGridStringView.Rows(e.RowIndex).Cells(1).Value =
                            DataGridStringView.Rows(e.RowIndex).Cells(1).Value.ToString.Substring(0, DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
            End If
            SizeChange += DataGridStringView.Rows(e.RowIndex).Cells(2).Value - OldLength
            SavePending = True
            SaveStringChangesToolStripMenuItem.Visible = True
            DataGridStringView.Rows(e.RowIndex).Cells(3).Value = True
        End If
    End Sub
    Private Sub DataGridStringView_CellEnter(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 1 Then
            OldString = DataGridStringView.Rows(e.RowIndex).Cells(1).Value
            OldLength = DataGridStringView.Rows(e.RowIndex).Cells(2).Value
            LengthTheSame = (OldString.Length + 1 = OldLength)
            'If Not LengthDif Then
            'MessageBox.Show("Super String")
            'End If
        ElseIf e.ColumnIndex = 2 Then
            OldLength = DataGridStringView.Rows(e.RowIndex).Cells(2).Value
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
        ProgressBar1.Maximum = StringCount - 1
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
        Dim NodeTag As NodeProperties = CType(ReadNode.Tag, NodeProperties)
        Dim StringBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            StringBytes = NodeTag.StoredData
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(ReadNode.ToolTipText)
            StringBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), StringBytes, 0, CInt(NodeTag.length))
        End If
        'First get the string count and make a header
        MessageBox.Show(SizeChange)
        Dim ReturnedBytes As Byte() = New Byte(ReadNode.Tag.length + SizeChange) {}
        Array.Copy(BitConverter.GetBytes(CUInt(StringCount)), 0, ReturnedBytes, 4, 4)

        ProgressBar1.Maximum = StringCount - 1
        ProgressBar1.Value = 0
        Dim CurrentChange As Integer = 0
        For i As Integer = 0 To StringCount - 1
            If DataGridStringView.Rows(i).Cells(3).Value = True Then
                'index of string won't change
                Array.Copy(BitConverter.GetBytes(StringFileOffset(i) + CurrentChange), 0, ReturnedBytes, 8 + i * 12 + 0, 4)
                'String Length will be equal to cell 3 (2 in 0 index)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridStringView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, 8 + i * 12 + 4, 4)
                'Reference Stays the same
                Array.Copy(BitConverter.GetBytes(StringFileReference(i)), 0, ReturnedBytes, 8 + i * 12 + 8, 4)
                'now we have to build the new string as bytes and inject it including a buffer so we need to make a temp byte array
                Dim TempBytes As Byte() = Encoding.Default.GetBytes(DataGridStringView.Rows(i).Cells(1).Value.ToString)
                ReDim Preserve TempBytes(DataGridStringView.Rows(i).Cells(2).Value)
                Array.Copy(TempBytes, 0, ReturnedBytes, StringFileOffset(i) + CurrentChange, TempBytes.Length)
                'Now add the length difference to the Current Change Value
                CurrentChange += CUInt(DataGridStringView.Rows(i).Cells(2).Value) - StringFileLength(i)
                'Subtracts for shorter strings and adds for longer strings
            Else
                Array.Copy(BitConverter.GetBytes(StringFileOffset(i) + CurrentChange), 0, ReturnedBytes, 8 + i * 12 + 0, 4)
                Array.Copy(BitConverter.GetBytes(StringFileLength(i)), 0, ReturnedBytes, 8 + i * 12 + 4, 4)
                Array.Copy(BitConverter.GetBytes(StringFileReference(i)), 0, ReturnedBytes, 8 + i * 12 + 8, 4)
                'now copy the string bytes over since they didn't change
                Array.Copy(StringBytes, StringFileOffset(i), ReturnedBytes, StringFileOffset(i) + CurrentChange, StringFileLength(i))
            End If
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function
#End Region
    'TO DO Increase speed of datagrid population with collections
#Region "Misc View Controls"
    Sub CloseMiscView()
        If SavePending Then
            If MessageBox.Show("Changes have not yet been saved.  Would you like to save them now?", "Save Changes?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                InjectIntoNode(ReadNode, BuildMiscFile())
            End If
            SaveMiscChangesToolStripMenuItem.Visible = False
            SavePending = False
        End If
        ReadNode = Nothing
        RemoveHandler DataGridMiscView.CellValueChanged, AddressOf DataGridMiscView_CellValueChanged
        TabControl1.TabPages.Remove(MiscView)
    End Sub
    Sub FillMiscView(SelectedData As TreeNode)
        RemoveHandler DataGridMiscView.CellValueChanged, AddressOf DataGridMiscView_CellValueChanged
        ReadNode = SelectedData
        SizeChange = 0
        DataGridMiscView.Rows.Clear()
        DataGridMiscView.Columns.Clear()
        Dim GameType As Integer = MiscViewType.SelectedIndex
        GetMiscColumns(GameType)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim MiscBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            MiscBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), MiscBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            MiscBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), MiscBytes, 0, CInt(NodeTag.length))
        End If
        Dim ArenaCount As Integer = BitConverter.ToInt32(MiscBytes, 0)
        For i As Integer = 0 To ArenaCount - 1
            Dim ArenaNum As String = Encoding.ASCII.GetString(MiscBytes, 25 + i * 32, 5)
            Dim TempIndex As Integer = BitConverter.ToInt32(MiscBytes, 40 + i * 32)
            Dim TempLength As Integer = BitConverter.ToInt32(MiscBytes, 36 + i * 32)
            Dim ArenaJson As String = Encoding.ASCII.GetString(MiscBytes, TempIndex, TempLength)
            'MessageBox.Show(Arena_String)
            Dim Stadium As String = ArenaJson.Substring(ArenaJson.IndexOf("Stadium") + 9,
                                                        ArenaJson.IndexOf(",", ArenaJson.IndexOf("Stadium") + 9) - ArenaJson.IndexOf("Stadium") - 9)
            Dim Advert As String = ArenaJson.Substring(ArenaJson.IndexOf("Advertisement") + 15,
                                                       ArenaJson.IndexOf(",", ArenaJson.IndexOf("Advertisement") + 15) - ArenaJson.IndexOf("Advertisement") - 15)
            Dim CornerPost As String = ArenaJson.Substring(ArenaJson.IndexOf("CornerPost") + 12,
                                                           ArenaJson.IndexOf(",", ArenaJson.IndexOf("CornerPost") + 12) - ArenaJson.IndexOf("CornerPost") - 12)
            Dim TempLEDCorner As Integer = ArenaJson.IndexOf("LED_CornerPost")
            Dim LEDCorner As String = "-1"
            If TempLEDCorner = -1 Then
            Else
                LEDCorner = ArenaJson.Substring(ArenaJson.IndexOf("LED_CornerPost") + 16, ArenaJson.IndexOf(",", ArenaJson.IndexOf("LED_CornerPost") + 16) - ArenaJson.IndexOf("LED_CornerPost") - 16)
            End If
            Dim Rope As String = ArenaJson.Substring(ArenaJson.IndexOf("Rope") + 6,
                                                     ArenaJson.IndexOf(",", ArenaJson.IndexOf("Rope") + 6) - ArenaJson.IndexOf("Rope") - 6)
            Dim Apron As String = ArenaJson.Substring(ArenaJson.IndexOf("Apron") + 7,
                                                      ArenaJson.IndexOf(",", ArenaJson.IndexOf("Apron") + 7) - ArenaJson.IndexOf("Apron") - 7)
            Dim TempLEDApron As Integer = ArenaJson.IndexOf("LED_Apron")
            Dim LEDApron As String = "-1"
            If TempLEDApron = -1 Then
            Else
                LEDApron = ArenaJson.Substring(ArenaJson.IndexOf("LED_Apron") + 11,
                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("LED_Apron") + 11) - ArenaJson.IndexOf("LED_Apron") - 11)
            End If
            Dim Turnbuckle As String = ArenaJson.Substring(ArenaJson.IndexOf("Turnbuckle") + 12,
                                                           ArenaJson.IndexOf(",", ArenaJson.IndexOf("Turnbuckle") + 12) - ArenaJson.IndexOf("Turnbuckle") - 12)
            Dim Barricade As String = ArenaJson.Substring(ArenaJson.IndexOf("Barricade") + 11,
                                                          ArenaJson.IndexOf(",", ArenaJson.IndexOf("Barricade") + 11) - ArenaJson.IndexOf("Barricade") - 11)
            Dim Fence As String = ArenaJson.Substring(ArenaJson.IndexOf("Fence") + 7,
                                                      ArenaJson.IndexOf(",", ArenaJson.IndexOf("Fence") + 7) - ArenaJson.IndexOf("Fence") - 7)
            Dim CeilingLight As String = ArenaJson.Substring(ArenaJson.IndexOf("CeilingLighting") + 17,
                                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("CeilingLighting") + 17) - ArenaJson.IndexOf("CeilingLighting") - 17)
            Dim SpotLight As String = ArenaJson.Substring(ArenaJson.IndexOf("Spotlight") + 11,
                                                          ArenaJson.IndexOf(",", ArenaJson.IndexOf("Spotlight") + 11) - ArenaJson.IndexOf("Spotlight") - 11)
            Dim Stairs As String = ArenaJson.Substring(ArenaJson.IndexOf("Stairs") + 8,
                                                       ArenaJson.IndexOf(",", ArenaJson.IndexOf("Stairs") + 8) - ArenaJson.IndexOf("Stairs") - 8)
            Dim CommentarySeat As String = ArenaJson.Substring(ArenaJson.IndexOf("CommentarySeat") + 16,
                                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("CommentarySeat") + 16) - ArenaJson.IndexOf("CommentarySeat") - 16)
            Dim RingMat As String = ArenaJson.Substring(ArenaJson.IndexOf("RingMat") + 9,
                                                        ArenaJson.IndexOf(",", ArenaJson.IndexOf("RingMat") + 9) - ArenaJson.IndexOf("RingMat") - 9)
            Dim FloorMat As String = ArenaJson.Substring(ArenaJson.IndexOf("FloorMattress") + 15,
                                                         ArenaJson.IndexOf(",", ArenaJson.IndexOf("FloorMattress") + 15) - ArenaJson.IndexOf("FloorMattress") - 15)
            Dim Crowd As String = ArenaJson.Substring(ArenaJson.IndexOf("Crowd") + 7,
                                                      ArenaJson.IndexOf(",", ArenaJson.IndexOf("Crowd") + 7) - ArenaJson.IndexOf("Crowd") - 7)
            Dim TempCrowdSeatPlan As Integer = ArenaJson.IndexOf("Titantron")
            Dim CrowdSeatPlan As String = "-1"
            If Not TempCrowdSeatPlan = -1 Then
                CrowdSeatPlan = ArenaJson.Substring(ArenaJson.IndexOf("CrowdSeatsPlace") + 17,
                                                ArenaJson.IndexOf(",", ArenaJson.IndexOf("CrowdSeatsPlace") + 17) - ArenaJson.IndexOf("CrowdSeatsPlace") - 17)
            End If
            Dim TempCrowdSeatModel As Integer = ArenaJson.IndexOf("Titantron")
            Dim CrowdSeatModel As String = "-1"
            If Not TempCrowdSeatModel = -1 Then
                CrowdSeatModel = ArenaJson.Substring(ArenaJson.IndexOf("CrowdSeatsModel") + 17,
                                                ArenaJson.IndexOf(",", ArenaJson.IndexOf("CrowdSeatsModel") + 17) - ArenaJson.IndexOf("CrowdSeatsModel") - 17)
            End If
            Dim IBL As String = ArenaJson.Substring(ArenaJson.IndexOf("IBL") + 5,
                                                    ArenaJson.IndexOf(",", ArenaJson.IndexOf("IBL") + 5) - ArenaJson.IndexOf("IBL") - 5)
            Dim TempTitantron As Integer = ArenaJson.IndexOf("Titantron")
            Dim Titantron As String = "-1"
            If Not TempTitantron = -1 Then
                Titantron = ArenaJson.Substring(ArenaJson.IndexOf("Titantron") + 11,
                                                ArenaJson.IndexOf(",", ArenaJson.IndexOf("Titantron") + 11) - ArenaJson.IndexOf("Titantron") - 11)
            End If
            Dim TempMinitron As Integer = ArenaJson.IndexOf("Minitron")
            Dim Minitron As String = "-1"
            If TempMinitron = -1 Then
            Else
                Minitron = ArenaJson.Substring(ArenaJson.IndexOf("Minitron") + 10,
                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("Minitron") + 10) - ArenaJson.IndexOf("Minitron") - 10)
            End If
            Dim TempWall_L As Integer = ArenaJson.IndexOf("Wall_L")
            Dim Wall_L As String = "-1"
            If TempWall_L = -1 Then
            Else
                Wall_L = ArenaJson.Substring(ArenaJson.IndexOf("Wall_L") + 8,
                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("Wall_L") + 8) - ArenaJson.IndexOf("Wall_L") - 8)
            End If
            Dim TempWall_R As Integer = ArenaJson.IndexOf("Wall_R")
            Dim Wall_R As String = "-1"
            If TempWall_R = -1 Then
            Else
                Wall_R = ArenaJson.Substring(ArenaJson.IndexOf("Wall_R") + 8,
                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("Wall_R") + 8) - ArenaJson.IndexOf("Wall_R") - 8)
            End If
            Dim TempHeader As Integer = ArenaJson.IndexOf("Header")
            Dim Header As String = "-1"
            If TempHeader = -1 Then
            Else
                Header = ArenaJson.Substring(ArenaJson.IndexOf("Header") + 8,
                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("Header") + 8) - ArenaJson.IndexOf("Header") - 8)
            End If
            Dim TempFloor As Integer = ArenaJson.IndexOf("Floor""")
            Dim Floor As String = "-1"
            If TempFloor = -1 Then
            Else
                Floor = ArenaJson.Substring(ArenaJson.IndexOf("Floor""") + 7,
                                            ArenaJson.IndexOf(",", ArenaJson.IndexOf("Floor""") + 7) - ArenaJson.IndexOf("Floor""") - 7)
            End If
            Dim TempMiscObject As Integer = ArenaJson.IndexOf("Floor""")
            Dim MiscObject As String = "-1"
            If TempMiscObject = -1 Then
            Else
                MiscObject = ArenaJson.Substring(ArenaJson.IndexOf("MiscObjects") + 13,
                                                 ArenaJson.IndexOf(",", ArenaJson.IndexOf("MiscObjects") + 13) - ArenaJson.IndexOf("MiscObjects") - 13)
            End If
            Dim TempLightType As Integer = ArenaJson.IndexOf("LightingType")
            Dim LightingType As String = "-1"
            If TempLightType = -1 Then
            Else
                LightingType = ArenaJson.Substring(ArenaJson.IndexOf("LightingType") + 14,
                                                   ArenaJson.IndexOf(",", ArenaJson.IndexOf("LightingType") + 14) - ArenaJson.IndexOf("LightingType") - 14)
            End If
            Dim TempCornerCM As Integer = ArenaJson.IndexOf("CornerPost_CM")
            Dim CornerPost_CM As String = "0"
            If TempCornerCM = -1 Then
            Else
                CornerPost_CM = ArenaJson.Substring(ArenaJson.IndexOf("CornerPost_CM") + 15,
                                                    ArenaJson.IndexOf(",", ArenaJson.IndexOf("CornerPost_CM") + 15) - ArenaJson.IndexOf("CornerPost_CM") - 15)
            End If
            Dim TempRopeCM As Integer = ArenaJson.IndexOf("Rope_CM")
            Dim Rope_CM As String = "0"
            If TempRopeCM = -1 Then
            Else
                Rope_CM = ArenaJson.Substring(ArenaJson.IndexOf("Rope_CM") + 9,
                                              ArenaJson.IndexOf(",", ArenaJson.IndexOf("Rope_CM") + 9) - ArenaJson.IndexOf("Rope_CM") - 9)
            End If
            Dim TempApronCM As Integer = ArenaJson.IndexOf("Apron_CM")
            Dim Apron_CM As String = "0"
            If TempApronCM = -1 Then
            Else
                Apron_CM = ArenaJson.Substring(ArenaJson.IndexOf("Apron_CM") + 10,
                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("Apron_CM") + 10) - ArenaJson.IndexOf("Apron_CM") - 10)
            End If
            Dim TempTurnCM As Integer = ArenaJson.IndexOf("Turnbuckle_CM")
            Dim Turnbuckle_CM As String = "0"
            If TempTurnCM = -1 Then
            Else
                Turnbuckle_CM = ArenaJson.Substring(ArenaJson.IndexOf("Turnbuckle_CM") + 15,
                                                    ArenaJson.IndexOf(",", ArenaJson.IndexOf("Turnbuckle_CM") + 15) - ArenaJson.IndexOf("Turnbuckle_CM") - 15)
            End If
            Dim TempRinmMatCM As Integer = ArenaJson.IndexOf("Turnbuckle_CM")
            Dim RingMat_CM As String = "0"
            If TempRinmMatCM = -1 Then
            Else
                RingMat_CM = ArenaJson.Substring(ArenaJson.IndexOf("RingMat_CM") + 12,
                                                 ArenaJson.IndexOf(",", ArenaJson.IndexOf("RingMat_CM") + 12) - ArenaJson.IndexOf("RingMat_CM") - 12)
            End If
            Dim version As String = ArenaJson.Substring(ArenaJson.IndexOf("version") + 9,
                                                        ArenaJson.IndexOf("}", ArenaJson.IndexOf("version") + 9) - ArenaJson.IndexOf("version") - 9)
            'MessageBox.Show(temparena)
            'If GameType = 0 Then '2K15
            'DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, Rope, Apron, Turnbuckle, Barricade, Fence,
            'CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, version)
            'ElseIf GameType = 1 Then '2K16
            'DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
            '                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
            '                       Wall_R, Header, Floor, MiscObject, version)
            'ElseIf GameType = 2 Then '2K17 
            '   DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
            '  CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
            ' Wall_R, Header, Floor, MiscObject, version)
            'If GameType < 4 Then '2K18
            'DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, LEDCorner, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
            'CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
            'Wall_R, Header, Floor, MiscObject, LightingType, CornerPost_CM, Rope_CM, Apron_CM, Turnbuckle_CM, RingMat_CM, 100) 'version)
            'Else '2K19
            DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, LEDCorner, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
                                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, CrowdSeatPlan, CrowdSeatModel, IBL, Titantron, Minitron, Wall_L,
                                       Wall_R, Header, Floor, MiscObject, LightingType, CornerPost_CM, Rope_CM, Apron_CM, Turnbuckle_CM, RingMat_CM, version)
            'End If
            DataGridMiscView.Rows.Item(i).HeaderCell.Value = ArenaNum
        Next
        AddHandler DataGridMiscView.CellValueChanged, AddressOf DataGridMiscView_CellValueChanged
    End Sub
    Sub GetMiscColumns(MenuIndex As Integer)
        Dim ArenaParts As DataGridViewColumnCollection = DataGridMiscView.Columns
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Stadium",
                       .Name = "Stadium"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Advert",
                       .Name = "Advert"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CornerPost",
                       .Name = "CornerPost"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "LEDCorner",
                       .Name = "LEDCorner"})
        If Not MenuIndex > 2 Then '2K18 and Beyond
            ArenaParts(3).Visible = False
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Rope",
                       .Name = "Rope"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Apron",
                       .Name = "Apron"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "LEDApron",
                       .Name = "LEDApron"})
        If Not MenuIndex > 0 Then '2K18 and Beyond
            ArenaParts(6).Visible = False
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Turnbuckle",
                       .Name = "Turnbuckle"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Barricade",
                       .Name = "Barricade"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Fence",
                       .Name = "Fence"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CeilingL",
                       .Name = "CeilingL"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "SpotL",
                       .Name = "SpotL"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Stairs",
                       .Name = "Stairs"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "ComSeat",
                       .Name = "ComSeat"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "RingMat",
                       .Name = "RingMat"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "FloorMat",
                       .Name = "FloorMat"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Crowd",
                       .Name = "Crowd"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CSPlace",
                       .Name = "CSPlace"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CSModel",
                       .Name = "CSModel"})
        If Not MenuIndex > 3 Then '2K19 and Beyond
            ArenaParts(17).Visible = False
            ArenaParts(18).Visible = False
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "IBL",
                       .Name = "IBL"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                         .HeaderText = "Titantron",
                         .Name = "Titantron"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Minitron",
                                   .Name = "Minitron"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Wall_L",
                                  .Name = "Wall_L"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Wall_R",
                                  .Name = "Wall_R"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                  .HeaderText = "Header",
                                   .Name = "Header"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Floor",
                                   .Name = "Floor"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "MiscO",
                                   .Name = "MiscO"})
        If Not MenuIndex > 0 Then '2K16 and Beyond
            ArenaParts(20).Visible = False
            ArenaParts(21).Visible = False
            ArenaParts(22).Visible = False
            ArenaParts(23).Visible = False
            ArenaParts(24).Visible = False
            ArenaParts(25).Visible = False
            ArenaParts(26).Visible = False
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                          .HeaderText = "LightT",
                          .Name = "LightT"})
        If Not MenuIndex > 2 Then '2K18 and Beyond
            ArenaParts(27).Visible = False
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                   .HeaderText = "CornerCM",
                   .Name = "CornerCM"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "RopeCM",
                       .Name = "RopeCM"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                   .HeaderText = "ApronCM",
                   .Name = "ApronCM"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                          .HeaderText = "TurnbCM",
                           .Name = "TurnbCM"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                      .HeaderText = "RingMatCM",
                       .Name = "RingMatCM"})
        If Not MenuIndex > 2 Then '2K18 and Beyond
            ArenaParts(28).Visible = False
            ArenaParts(29).Visible = False
            ArenaParts(30).Visible = False
            ArenaParts(31).Visible = False
            ArenaParts(32).Visible = False
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Version",
                       .Name = "Version"})
    End Sub
    Private Sub MiscViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MiscViewType.SelectedIndexChanged
        If SavePending Then
            MessageBox.Show("Any changes will be lost", "Continue format change?", MessageBoxButtons.YesNo)
        End If
        My.Settings.MiscModeIndex = MiscViewType.SelectedIndex
        If TreeView1.SelectedNode IsNot Nothing Then
            FillMiscView(TreeView1.SelectedNode)
        End If
    End Sub
    Private Sub DataGridMiscView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        SavePending = True
        SaveMiscChangesToolStripMenuItem.Visible = True
    End Sub
    Private Sub SaveChangesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SaveMiscChangesToolStripMenuItem.Click
        InjectIntoNode(ReadNode, BuildMiscFile())
        ReadNode = Nothing
        SaveMiscChangesToolStripMenuItem.Visible = False
        SavePending = False
    End Sub
    Private Function BuildMiscFile() As Byte()
        Dim Active_Offset As Integer
        Dim Temp_Array As Byte() = New Byte(&H20000) {}
        Temp_Array(0) = DataGridMiscView.RowCount
        Temp_Array(5) = 1
        Temp_Array(&HC) = &H10
        For i As Integer = 0 To DataGridMiscView.RowCount - 1
            If i = 0 Then
                Active_Offset = &H10 + &H20 * DataGridMiscView.RowCount
            End If
            'Making the header 
            Dim Part_Head_Array As Byte() = New Byte(&H20) {}
            'Adding the text parts.
            Dim ArenaInfoBytes As Byte() = Encoding.ASCII.GetBytes("arenaInfo" & DataGridMiscView.Rows.Item(i).HeaderCell.Value)
            Buffer.BlockCopy(ArenaInfoBytes, 0, Part_Head_Array, 0, ArenaInfoBytes.Length)
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("jsn"), 0, Part_Head_Array, &H10, 3)
            'Build the Arena String
            Dim Part_String As String = buildarena(i)
            'MessageBox.Show(Part_String)
            'Add the Length to the Header
            Dim Length_Array As Byte() = System.BitConverter.GetBytes(Part_String.Length)
            'Array.Reverse(Length_Array, 0, Length_Array.Length)
            Buffer.BlockCopy(Length_Array, 0, Part_Head_Array, &H14, 4)
            'Add the offset to the header
            Dim Offset_Array As Byte() = System.BitConverter.GetBytes(Active_Offset)
            'Array.Reverse(Offset_Array, 0, Offset_Array.Length)
            Buffer.BlockCopy(Offset_Array, 0, Part_Head_Array, &H18, 4)
            'injecting the header
            Buffer.BlockCopy(Part_Head_Array, 0, Temp_Array, &H10 + &H20 * i, &H20)
            'Adding the ArenaInfo to the ararry
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(Part_String), 0, Temp_Array, Active_Offset, Part_String.Length)
            'Updating the Active Offset
            Active_Offset = Active_Offset + Part_String.Length + (16 - Part_String.Length Mod 16)
        Next
        Dim Final_Array As Byte() = New Byte(Active_Offset - 1) {}
        Buffer.BlockCopy(Temp_Array, 0, Final_Array, 0, Active_Offset)
        Return Final_Array
    End Function
    Function buildarena(index As Integer) As String
        ' Chr(&H7B) = { Chr(&HD) = Carriage return Chr(&HA) = Line feed
        Dim Temp_String As String = Chr(&H7B) & Chr(&HD) & Chr(&HA) & "    " & """Stadium"":" & DataGridMiscView(0, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Advertisement"":" & DataGridMiscView(1, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """CornerPost"":" & DataGridMiscView(2, index).Value.ToString & ","
        If MiscViewType.SelectedIndex > 2 Then '2K18 and Beyond
            Temp_String = Temp_String & Chr(&HA) & "    " & """LED_CornerPost"":" & DataGridMiscView(3, index).Value.ToString & ","
        End If
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Rope"":" & DataGridMiscView(4, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Apron"":" & DataGridMiscView(5, index).Value.ToString & ","
        If MiscViewType.SelectedIndex > 0 Then '2K16 and Beyond
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """LED_Apron"":" & DataGridMiscView(6, index).Value.ToString & ","
        End If
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Turnbuckle"":" & DataGridMiscView(7, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Barricade"":" & DataGridMiscView(8, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Fence"":" & DataGridMiscView(9, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """CeilingLighting"":" & DataGridMiscView(10, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Spotlight"":" & DataGridMiscView(11, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Stairs"":" & DataGridMiscView(12, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """CommentarySeat"":" & DataGridMiscView(13, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """RingMat"":" & DataGridMiscView(14, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """FloorMattress"":" & DataGridMiscView(15, index).Value.ToString & ","
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Crowd"":" & DataGridMiscView(16, index).Value.ToString & ","
        If MiscViewType.SelectedIndex > 3 Then '2K19 and Beyond
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """CrowdSeatsPlace"":" & DataGridMiscView(17, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """CrowdSeatsModel"":" & DataGridMiscView(18, index).Value.ToString & ","
        End If
        Temp_String = Temp_String & Chr(&HA) & "    " & """IBL"":" & DataGridMiscView(19, index).Value.ToString & ","
        If MiscViewType.SelectedIndex > 0 Then '2K16 and Beyond
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Titantron"":" & DataGridMiscView(20, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Minitron"":" & DataGridMiscView(21, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Wall_L"":" & DataGridMiscView(22, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Wall_R"":" & DataGridMiscView(23, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Header"":" & DataGridMiscView(24, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Floor"":" & DataGridMiscView(25, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """MiscObjects"":" & DataGridMiscView(26, index).Value.ToString & ","
        End If
        If MiscViewType.SelectedIndex > 2 Then '2K18 and Beyond
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """LightingType"":" & DataGridMiscView(27, index).Value.ToString & ","
        End If
        If MiscViewType.SelectedIndex > 2 Then '2K18 and Beyond
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """CornerPost_CM"":" & DataGridMiscView(28, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Rope_CM"":" & DataGridMiscView(29, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Apron_CM"":" & DataGridMiscView(30, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """Turnbuckle_CM"":" & DataGridMiscView(31, index).Value.ToString & ","
            Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """RingMat_CM"":" & DataGridMiscView(32, index).Value.ToString & ","
        End If
        Temp_String = Temp_String & Chr(&HD) & Chr(&HA) & "    " & """version"":" & DataGridMiscView(33, index).Value.ToString
        Temp_String = Temp_String & Chr(&HA) & Chr(&H7D) & Chr(&HA) & Chr(&H0)
        Return Temp_String
    End Function
#End Region
    'Build Injector
#Region "Show View Controls"
    Sub FillShowView(SelectedData As TreeNode)
        DataGridShowView.Rows.Clear()
        Dim GameType As Integer = ShowViewType.SelectedIndex
        'GetMiscColumns(GameType)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim ShowBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            ShowBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), ShowBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            ShowBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), ShowBytes, 0, CInt(NodeTag.length))
        End If
        Dim FileLength As Integer = ShowBytes.Length
        Dim index As Integer = 0
        Dim current_poition As Long = &HC
        While current_poition < FileLength
            DataGridShowView.Rows.Add()
            DataGridShowView(0, index).Value = Hex(BitConverter.ToInt32(ShowBytes, current_poition)) ' Dim NameRef As String = 
            DataGridShowView(1, index).Value = Hex(ShowBytes(current_poition + 4)) 'Dim S1 As String =
            DataGridShowView(2, index).Value = Hex(ShowBytes(current_poition + 5)) 'Dim S2 As String =
            DataGridShowView(3, index).Value = Hex(ShowBytes(current_poition + 6)) 'Dim S3 As String = 
            DataGridShowView(4, index).Value = Hex(ShowBytes(current_poition + 7)) ' Dim S4 As String = 
            DataGridShowView(5, index).Value = Hex(ShowBytes(current_poition + 8)) 'Dim A1 As String =
            DataGridShowView(6, index).Value = Hex(ShowBytes(current_poition + 10)) 'Dim A2 As String = 
            DataGridShowView(7, index).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 12)) 'Dim B As String = 
            DataGridShowView(8, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 24) 'Dim C1 As Boolean = 
            DataGridShowView(9, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 25) ' Dim C2 As Boolean = 
            DataGridShowView(10, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 26) 'Dim C3 As Boolean = 
            DataGridShowView(11, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 27) ' Dim C4 As Boolean = 
            DataGridShowView(12, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 30) 'Dim C5 As Boolean = 
            DataGridShowView(13, index).Value = Hex(ShowBytes(current_poition + 31)) 'Dim Stage As String = 
            DataGridShowView(14, index).Value = Hex(ShowBytes(current_poition + 34)) 'Dim D1 As String = 
            DataGridShowView(15, index).Value = Hex(ShowBytes(current_poition + 35)) 'Dim D2 As String = 
            DataGridShowView(16, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 36) 'Dim Crowd As Boolean = 
            DataGridShowView(17, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 37) ' Dim E1 As Boolean = 
            DataGridShowView(18, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 38) 'Dim E2 As Boolean = 
            DataGridShowView(19, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 39) ' Dim E3 As Boolean = 
            DataGridShowView(20, index).Value = Hex(ShowBytes(current_poition + 40)) 'Dim Ref As String
            'Dim Filter As String
            DataGridShowView(21, index).Value = Hex(ShowBytes(current_poition + 41)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 42)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 43)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 44)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 45)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 46)).PadLeft(2, "0")
            'Dim F1 As String
            DataGridShowView(22, index).Value = Hex(BitConverter.ToInt32(ShowBytes, current_poition + 52))
            'DataGridView1(22, index).Value = Hex(show_array(current_poition + 52)).PadLeft(2, "0") &
            'Hex(show_array(current_poition + 53)).PadLeft(2, "0") &
            'Hex(show_array(current_poition + 54)).PadLeft(2, "0") &
            'Hex(show_array(current_poition + 55)).PadLeft(2, "0")
            DataGridShowView(23, index).Value = Hex(ShowBytes(current_poition + 56)) 'Dim F2 As String
            DataGridShowView(24, index).Value = Hex(ShowBytes(current_poition + 60)) 'Dim G1 As String
            DataGridShowView(25, index).Value = Hex(ShowBytes(current_poition + 62)) 'Dim G2 As String
            DataGridShowView(26, index).Value = Hex(ShowBytes(current_poition + 64)) 'Dim H1 As String
            DataGridShowView(27, index).Value = Hex(ShowBytes(current_poition + 65)) 'Dim H2 As String
            DataGridShowView(28, index).Value = Hex(ShowBytes(current_poition + 66)) 'Dim H3 As String
            DataGridShowView(29, index).Value = Hex(ShowBytes(current_poition + 67)) 'Dim H4 As String
            DataGridShowView(30, index).Value = Hex(ShowBytes(current_poition + 69)) 'Dim Bar As String
            'Dim Unknown As String
            Dim temparray As Byte() = New Byte(33) {}
            Buffer.BlockCopy(ShowBytes, current_poition + 70, temparray, 0, 34)
            DataGridShowView(31, index).Value = (BitConverter.ToString(temparray).Replace("-", ""))
            DataGridShowView(32, index).Value = Hex(ShowBytes(current_poition + 107)) 'Dim I1 As String
            DataGridShowView(33, index).Value = Hex(ShowBytes(current_poition + 108)) 'Dim I2 As String
            DataGridShowView(34, index).Value = Hex(ShowBytes(current_poition + 110))  'Dim I3 As String
            DataGridShowView(35, index).Value = Hex(ShowBytes(current_poition + 113)) 'Dim live As String
            DataGridShowView(36, index).Value = Hex(ShowBytes(current_poition + 116)) 'Dim J As String
            'DataGridView1.Rows.Add(NameRef, S1, S2, S3, S4, A1, A2, B, C1, C2, C3, C4, C5, Stage, D1, D2, Crowd, E1, E2, E3)
            DataGridShowView.Rows(index).HeaderCell.Value = index.ToString
            index += 1
            '&h7C
            current_poition += &H7C
        End While
    End Sub

#End Region
#Region "NIJB View Controls"
    Sub FillNIBJView(SelectedData As TreeNode)
        DataGridNIBJView.Rows.Clear()
        DataGridNIBJView.Columns.Clear()
        Dim GameType As Integer = NIBJViewType.SelectedIndex
        'GetNIJBColumns(GameType)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim NIJBBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            NIJBBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), NIJBBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            NIJBBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), NIJBBytes, 0, CInt(NodeTag.length))
        End If
        Dim LightCount As Integer = BitConverter.ToInt32(NIJBBytes, &H8)
        Dim ShowCount As Integer = BitConverter.ToInt32(NIJBBytes, &HC)
        Dim Folder As String = Encoding.Default.GetChars(NIJBBytes, &H10, &H10)
        Dim Properties As String = Encoding.Default.GetChars(NIJBBytes, &H20, &H10)
        FileAttributesToolStripMenuItem.Text = Folder & " > " & Properties
        For i As Integer = 0 To LightCount - 1
            DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H30 + i * &H20, &H10))
        Next
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
        Next
    End Sub
#End Region
#Region "Picture View Controls"
    Dim CreatedImages As List(Of String)
    Sub LoadPicture(SelectedData As TreeNode)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim PictureBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            PictureBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), PictureBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            PictureBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), PictureBytes, 0, CInt(NodeTag.length))
        End If
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
    'Attire Editor Built
#Region "Attire Editor View"
    Sub CloseAttireView()
        If SavePending Then
            If MessageBox.Show("Changes have not yet been saved.  Would you like to save them now?", "Save Changes?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                InjectIntoNode(ReadNode, BuildAttireFile())
            End If
            SaveChangesAttireMenuItem.Visible = False
            SavePending = False
        End If
        ReadNode = Nothing
        TabControl1.TabPages.Remove(AttireView)
    End Sub
    Sub LoadAttires(SelectedData As TreeNode)
        ReadNode = SelectedData
        DataGridAttireView.Rows.Clear()
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
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim AttireBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            AttireBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), AttireBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            AttireBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), AttireBytes, 0, CInt(NodeTag.length))
        End If
        Dim WrestlerCount As Integer = BitConverter.ToInt32(AttireBytes, &H8)
        Dim AttireCol As DataGridViewColumnCollection = DataGridAttireView.Columns
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
        For i As Integer = 0 To WrestlerCount - 1
            DataGridAttireView.Rows.Add(WrestlerPacs(i), AttireCount(i),
                                       Hex(AttireNames(i * 10 + 0)), StringReferences(AttireNames(i * 10 + 0)), AttireEnabled(i * 10 + 0), AttireManager(i * 10 + 0), AttireUnlockNumber(i * 10 + 0),
                                        Hex(AttireNames(i * 10 + 1)), StringReferences(AttireNames(i * 10 + 1)), AttireEnabled(i * 10 + 1), AttireManager(i * 10 + 1), AttireUnlockNumber(i * 10 + 1),
                                        Hex(AttireNames(i * 10 + 2)), StringReferences(AttireNames(i * 10 + 2)), AttireEnabled(i * 10 + 2), AttireManager(i * 10 + 2), AttireUnlockNumber(i * 10 + 2),
                                        Hex(AttireNames(i * 10 + 3)), StringReferences(AttireNames(i * 10 + 3)), AttireEnabled(i * 10 + 3), AttireManager(i * 10 + 3), AttireUnlockNumber(i * 10 + 3),
                                        Hex(AttireNames(i * 10 + 4)), StringReferences(AttireNames(i * 10 + 4)), AttireEnabled(i * 10 + 4), AttireManager(i * 10 + 4), AttireUnlockNumber(i * 10 + 4),
                                        Hex(AttireNames(i * 10 + 5)), StringReferences(AttireNames(i * 10 + 5)), AttireEnabled(i * 10 + 5), AttireManager(i * 10 + 5), AttireUnlockNumber(i * 10 + 5),
                                        Hex(AttireNames(i * 10 + 6)), StringReferences(AttireNames(i * 10 + 6)), AttireEnabled(i * 10 + 6), AttireManager(i * 10 + 6), AttireUnlockNumber(i * 10 + 6),
                                        Hex(AttireNames(i * 10 + 7)), StringReferences(AttireNames(i * 10 + 7)), AttireEnabled(i * 10 + 7), AttireManager(i * 10 + 7), AttireUnlockNumber(i * 10 + 7),
                                        Hex(AttireNames(i * 10 + 8)), StringReferences(AttireNames(i * 10 + 8)), AttireEnabled(i * 10 + 8), AttireManager(i * 10 + 8), AttireUnlockNumber(i * 10 + 8),
                                        Hex(AttireNames(i * 10 + 9)), StringReferences(AttireNames(i * 10 + 9)), AttireEnabled(i * 10 + 9), AttireManager(i * 10 + 9), AttireUnlockNumber(i * 10 + 9))
            If i > 99 Then
                DataGridAttireView.Rows(i).HeaderCell.Value = "UNREAD"
            Else
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            End If
        Next
        AddHandler DataGridAttireView.CellValueChanged, AddressOf DataGridAttireView_CellValueChanged
        AddHandler DataGridAttireView.CellEnter, AddressOf DataGridAttireView_CellEnter
        AddHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
    End Sub

    Private Sub DataGridAttireView_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs)
        If e.ColumnIndex = 0 Then 'Pac Number, Verify Number <= 1024
            If Not IsNumeric(DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf CInt(DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 OrElse
               CInt(DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 1024 Then
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Attire Count, Verify Number <= 10
            If Not IsNumeric(DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf CInt(DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 OrElse
               CInt(DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 10 Then
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            End If
            'ADD IN Disable Attires past number
            'If DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value < 10 Then
            'For i As Integer = DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value To 9
            'DataGridAttireView.Rows(e.RowIndex).Cells(1 + i * 5).ReadOnly = True
            'Next
            'End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 0 Then 'Attire Name
            Dim HexString As String = DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim hexcheck As Boolean = HexString.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            If Not hexcheck Then
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            ElseIf StringReferences(CUInt("&H" & HexString)) > &HFFFFF Then
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = OldValue
            Else
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & HexString))
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 1 Then 'Attire String Does Nothing
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 2 Then 'Enabled Changed
            If DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then 'if enabled checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = False 'unchecks manager
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 3 Then 'Manager Changed
            If DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value Then 'if manager checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value = False 'unchecks enabled
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 4 Then 'UnlockMode Program Only
        End If
        SavePending = True
        SaveChangesAttireMenuItem.Visible = True
    End Sub
    Private Sub DataGridAttireView_CellEnter(sender As Object, e As DataGridViewCellEventArgs)
        OldValue = DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub
    Private Sub DataGridAttireView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
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
        SaveChangesAttireMenuItem.Visible = False
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
    Sub LoadMuscles(SelectedData As TreeNode)

        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim MuscleBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            MuscleBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), MuscleBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            MuscleBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), MuscleBytes, 0, CInt(NodeTag.length))
        End If

        If DataGridMuscleView.ColumnCount = 0 Then
            DataGridMuscleView.Columns.Add("Name", "Name")
            DataGridMuscleView.Columns.Add("Number1", Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        Else
            DataGridMuscleView.Columns.Add("Number" & DataGridMuscleView.ColumnCount, Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        End If
        'MessageBox.Show(fileLength)
        Dim MuscleCount As Integer = BitConverter.ToInt32(MuscleBytes, &HC)
        Dim ActiveIndex As Long = &H14
        For i As Integer = 0 To MuscleCount - 1
            Dim MuscleName As String = Encoding.ASCII.GetString(MuscleBytes, ActiveIndex + 4, &H20)
            ActiveIndex = ActiveIndex + BitConverter.ToInt32(MuscleBytes, ActiveIndex)
            If DataGridMuscleView.ColumnCount = 2 Then
                DataGridMuscleView.Rows.Add(MuscleName, i)
            Else
                'MessageBox.Show(getrow(MuscleName))
                DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, GetMuscleRow(MuscleName)).Value = i
            End If
            'DataGridView1.Rows.Item(i - 1).HeaderCell.Value = i
        Next

    End Sub
    Function GetMuscleRow(MuscleName As String)
        For i As Integer = 0 To DataGridMuscleView.RowCount - 1
            'MessageBox.Show(i)
            'MessageBox.Show(DataGridView1(0, i).Value)
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
    Sub LoadMask(SelectedData As TreeNode)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim MaskBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            MaskBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), MaskBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            MaskBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), MaskBytes, 0, CInt(NodeTag.length))
        End If
        DataGridMaskView.Rows.Clear()
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
        Dim current_mask As String
        '-
        'First we have to process each container, these are the M_Head and M_Body containers
        '-
        For i As Integer = 0 To ContainerCount - 1 'process each mask individuallt head then body
            Dim mask_header_length As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex) ' should start as offset &hC, should also be &H4c
            current_mask = System.Text.Encoding.ASCII.GetString(MaskBytes, ActiveIndex + &H8, 6)
            'BitConverter.ToString(mask_array, active_offset + &H8)
            Dim MaskLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H4) ' should be &h4c if mask masks no objects
            Dim MaskedParts As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H48)
            If Not MaskedParts = 0 Then
                '-
                'Now if the container contains masked parts we have to start processing those masked parts
                '-
                ActiveIndex += mask_header_length
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
                        DataGridMaskView.Rows.Add(current_mask & "_" & PartNumber & "_1", "nil", "nil")
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
                                DataGridMaskView.Rows.Add(current_mask & "_" & PartNumber & "_" & TrueMaskNumber, "nil", "nil")
                                ActiveIndex += TrueMaskTotalLength
                            Else
                                '-
                                'Now to handle the true sets of masks each set is 8 bytes
                                '-
                                ActiveIndex += TrueMaskHeaderLength
                                For L As Integer = 0 To TrueMaskCount - 1
                                    'I only want the first line to have the part name
                                    If L = 0 Then
                                        DataGridMaskView.Rows.Add(current_mask & "_" & PartNumber & "_" & TrueMaskNumber,
                                                               BitConverter.ToInt32(MaskBytes, ActiveIndex),
                                                               BitConverter.ToInt32(MaskBytes, ActiveIndex + 4))
                                        ActiveIndex += 8
                                    Else
                                        DataGridMaskView.Rows.Add("",
                                                                BitConverter.ToInt32(MaskBytes, ActiveIndex),
                                                                BitConverter.ToInt32(MaskBytes, ActiveIndex + 4))
                                        ActiveIndex += 8
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            Else
                DataGridMaskView.Rows.Add(current_mask & "_0_0", "nil", "nil")
                ActiveIndex += MaskLength
            End If
        Next
    End Sub
#End Region
#Region "Object Array Controls"
    Sub LoadObjectArray(SelectedData As TreeNode)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag = CType(SelectedData.Tag, NodeProperties)
        Dim ObjArrayBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            ObjArrayBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), ObjArrayBytes, 0, CInt(NodeTag.length))
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            ObjArrayBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, CInt(NodeTag.Index), ObjArrayBytes, 0, CInt(NodeTag.length))
        End If
        Dim ChairCount As Integer = BitConverter.ToInt32(ObjArrayBytes, &HC)
        DataGridObjArrayView.Rows.Clear()
        For i As Integer = 0 To ChairCount - 1
            DataGridObjArrayView.Rows.Add(
            BitConverter.ToInt32(ObjArrayBytes, &H24 + i * 8), 'Number
            BitConverter.ToBoolean(ObjArrayBytes, &H20 + i * 8), 'Enabled
            Encoding.ASCII.GetString(ObjArrayBytes, &H20 + ChairCount * 8 + i * &H30, &H10), 'Chair Name
            BitConverter.ToSingle(ObjArrayBytes, &H30 + ChairCount * 8 + i * &H30), 'X Float
            BitConverter.ToSingle(ObjArrayBytes, &H34 + ChairCount * 8 + i * &H30), 'Y Float
            BitConverter.ToSingle(ObjArrayBytes, &H38 + ChairCount * 8 + i * &H30), 'Z Float
            BitConverter.ToSingle(ObjArrayBytes, &H3C + ChairCount * 8 + i * &H30), 'RX Float
            BitConverter.ToSingle(ObjArrayBytes, &H40 + ChairCount * 8 + i * &H30), 'RY Float
            BitConverter.ToSingle(ObjArrayBytes, &H44 + ChairCount * 8 + i * &H30), 'RZ Float
            BitConverter.ToInt32(ObjArrayBytes, &H48 + ChairCount * 8 + i * &H30), 'D1 Decimal
            BitConverter.ToInt32(ObjArrayBytes, &H4C + ChairCount * 8 + i * &H30)) 'D2 Decimal 
        Next
    End Sub

#End Region
#Region "Asset View Controls"
    Sub LoadAssetFile(SelectedData As TreeNode)
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
    Sub LoadTitleFile(SelectedData As TreeNode)
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
End Class
