Imports System.Environment 'appdata
Imports System.IO   'Files
Imports System.Runtime.Serialization.Formatters.Binary 'Binary Formatter

Public Class SettingsHandlers

    Shared Sub SettingsCheck()
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
        PackUnpack.CheckUnrrbpe()
        CheckDDSexe()
        If My.Settings.StringObject = "" Then
            Dim AppDataStringLocation As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\Strings.bin"
            If File.Exists(AppDataStringLocation) Then
                Dim fs As Stream = New FileStream(AppDataStringLocation, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                MainForm.StringReferences = CType(bf.Deserialize(fs), String())
                fs.Close()
            Else
                MainForm.StringReferences = New String(&HFFFFF) {}
                MainForm.StringReferences(0) = "String Not Read"
            End If
            My.Settings.StringObject = AppDataStringLocation
        Else
            If File.Exists(My.Settings.StringObject) Then
                Dim fs As Stream = New FileStream(My.Settings.StringObject, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                MainForm.StringReferences = CType(bf.Deserialize(fs), String())
                fs.Close()
            Else
                MainForm.StringReferences = New String(&HFFFFF) {}
                MainForm.StringReferences(0) = "String Not Read"
            End If
        End If
        If My.Settings.PacNumObject = "" Then
            Dim AppDataPacNumLocation As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\PacNums.bin"
            If File.Exists(AppDataPacNumLocation) Then
                Dim fs As Stream = New FileStream(AppDataPacNumLocation, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                MainForm.PacNumbers = CType(bf.Deserialize(fs), Integer())
                fs.Close()
            Else
                MainForm.PacNumbers = New Integer(1024) {}
                MainForm.PacNumbers(0) = -1
            End If
            My.Settings.PacNumObject = AppDataPacNumLocation
        Else
            If File.Exists(My.Settings.PacNumObject) Then
                Dim fs As Stream = New FileStream(My.Settings.PacNumObject, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                MainForm.PacNumbers = CType(bf.Deserialize(fs), Integer())
                fs.Close()
            Else
                MainForm.PacNumbers = New Integer(1024) {}
                MainForm.PacNumbers(0) = -1
            End If
        End If
        If My.Settings.TruncateDecimalNames Then
            My.Settings.DecimalNameMinLength = 8
            My.Settings.TruncateDecimalNames = False
        End If
        My.Settings.Save()
    End Sub

    Shared Function SaveSettingsFiles() As DialogResult
        'WARNING MAIN FORM REFERENCES
        Try
            'Here is the saving of the String File so we don't store it in the settings file and bloat the loading and closing.
            If File.Exists(My.Settings.StringObject) = True Then
                File.Delete(My.Settings.StringObject)
            End If
            Dim StringFileStream As Stream = New FileStream(My.Settings.StringObject, FileMode.Create)
            Dim StringBinaryFormatter As BinaryFormatter = New BinaryFormatter()
            StringBinaryFormatter.Serialize(StringFileStream, MainForm.StringReferences)
            StringFileStream.Close()
            If File.Exists(My.Settings.PacNumObject) = True Then
                File.Delete(My.Settings.PacNumObject)
            End If
            Dim PacNumFileStream As Stream = New FileStream(My.Settings.PacNumObject, FileMode.Create)
            Dim PacNumBinaryFormatter As BinaryFormatter = New BinaryFormatter()
            PacNumBinaryFormatter.Serialize(PacNumFileStream, MainForm.PacNumbers)
            PacNumFileStream.Close()
            Return DialogResult.OK
        Catch ex As Exception
            Return MessageBox.Show("Error Savings Settings Files" & vbNewLine &
                            ex.Message & vbNewLine & "Continue?", "Settings Error", MessageBoxButtons.OKCancel)
        End Try
    End Function

#Region "Finding Files & Folders"

    Shared Sub SelectHomeDirectory()
        Dim TempFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "WWE2KXX.exe",
            .Title = "Select WWE exe directory"}
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

    'REFACTOR - Make Get Functions like Check Functions where they return a true or false

    Shared Sub GetTexConvExe(Optional fromoptions As Boolean = False)
        Dim convertpath As String = Path.GetDirectoryName(Application.ExecutablePath) &
                      Path.DirectorySeparatorChar & "texconv.exe"
        Dim appDataPath As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        GeneralTools.FolderCheck(appDataPath)
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

    Shared Sub GetRadVideo(Optional FromOptions As Boolean = False)
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

    Shared Sub CheckDDSexe(Optional FromOptions As Boolean = False)
        If FromOptions Then
            Dim DDSOpenExeOpenDialog As New OpenFileDialog With {.FileName = "*.exe", .Title = "Select exe for opening DDS files"}
            If Not My.Settings.DDSexeLocation = "Not Installed" Then
                DDSOpenExeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.DDSexeLocation)
            End If
            If DDSOpenExeOpenDialog.ShowDialog = DialogResult.OK Then
                My.Settings.DDSexeLocation = DDSOpenExeOpenDialog.FileName
            Else
                'WARNING MAIN FORM REF
                MainForm.OpenImageWithToolStripMenuItem.Text = "Open With..."
            End If
        ElseIf File.Exists(My.Settings.DDSexeLocation) Then
            MainForm.OpenImageWithToolStripMenuItem.Text = "Open With " & Path.GetFileNameWithoutExtension(My.Settings.DDSexeLocation)
        End If
    End Sub

    Shared Function CheckFontAwesome(Optional FromOptions As Boolean = False) As Boolean
        If Not File.Exists(Application.StartupPath & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll") Then
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation,
                      Path.GetDirectoryName(Application.ExecutablePath) & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll", True)
                Return True
            Else
                If Not FromOptions Then
                    MessageBox.Show("FontAwesome Dll Not loaded")
                    Return False
                Else
                    Dim FontAwesomeOpenDialog As New OpenFileDialog With {.FileName = "FontAwesome.Sharp.dll", .Title = "FontAwesome.Sharp.dll"}
                    If FontAwesomeOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(FontAwesomeOpenDialog.FileName) = "FontAwesome.Sharp.dll" Then
                            File.Copy(FontAwesomeOpenDialog.FileName, Application.StartupPath &
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

End Class