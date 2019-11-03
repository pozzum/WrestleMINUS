Imports System.Environment 'appdata
Imports System.IO   'Files

Module ApplicationHandlers

#Region "Finding WWE Exe"

    Function CheckGameExeFolder(Optional FromOptions As Boolean = False, Optional GameNumberIndex As Integer = 0)
        'Default exe is set to ""
        If FromOptions Then
            Dim GameExeOpenDialog As New OpenFileDialog With {
                .FileName = "WWE2KXX.exe",
                .Title = "Select WWE exe directory",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.ExeLocation = "" AndAlso File.Exists(My.Settings.ExeLocation) Then
                GameExeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.ExeLocation)
                GameExeOpenDialog.FileName = Path.GetFileName(My.Settings.ExeLocation)
            Else
                GameExeOpenDialog.InitialDirectory = Application.StartupPath
            End If
            If GameExeOpenDialog.ShowDialog() = DialogResult.OK Then
                If Path.GetExtension(GameExeOpenDialog.FileName).ToLower = ".exe" AndAlso
                Path.GetFileNameWithoutExtension(GameExeOpenDialog.FileName).ToLower.Contains("wwe") Then
                    My.Settings.ExeLocation = GameExeOpenDialog.FileName
                    Return True
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If Not My.Settings.ExeLocation = "" AndAlso
                 File.Exists(My.Settings.ExeLocation) Then
                Return True
            Else
                'no point in trying to auto detect
            End If
        End If
        Return False
    End Function

#End Region

#Region "File Compression Tools"

    Function CheckBPEExe(Optional FromOptions As Boolean = False)
        'Default exe is set to Not Installed
        If FromOptions Then
            Dim BPEExeOpenDialog As New OpenFileDialog With {
                .FileName = "bpe.exe",
                .Title = "Select bpe.exe",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.BPEExePath = "Not Installed" AndAlso File.Exists(My.Settings.BPEExePath) Then
                BPEExeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.BPEExePath)
                BPEExeOpenDialog.FileName = Path.GetFileName(My.Settings.BPEExePath)
            Else
                BPEExeOpenDialog.InitialDirectory = Application.StartupPath
            End If
            If BPEExeOpenDialog.ShowDialog() = DialogResult.OK Then
                If Path.GetFileName(BPEExeOpenDialog.FileName).ToLower = "bpe.exe" Then
                    My.Settings.BPEExePath = BPEExeOpenDialog.FileName
                    Return True
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If Not My.Settings.BPEExePath = "Not Installed" AndAlso
                 File.Exists(My.Settings.BPEExePath) Then
                Return True
            Else
                'Attempt to auto-locate the exe
                Dim ApplicationPath As String = Application.StartupPath & Path.DirectorySeparatorChar & "bpe.exe"
                Dim appDataPath As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
                GeneralTools.FolderCheck(appDataPath)
                appDataPath += Path.DirectorySeparatorChar & "bpe.exe"
                If File.Exists(ApplicationPath) Then
                    My.Settings.BPEExePath = ApplicationPath
                    Return True
                ElseIf File.Exists(appDataPath) Then
                    My.Settings.BPEExePath = appDataPath
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Function CheckUnrrbpe(Optional FromOptions As Boolean = False)
        'Default exe is set to Not Installed
        If FromOptions Then
            Dim UnrrbpeOpenDialog As New OpenFileDialog With {
                .FileName = "unrrbpe.exe",
                .Title = "Select unrrbpe.exe",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.UnrrbpePath = "Not Installed" AndAlso File.Exists(My.Settings.UnrrbpePath) Then
                UnrrbpeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.UnrrbpePath)
                UnrrbpeOpenDialog.FileName = Path.GetFileName(My.Settings.UnrrbpePath)
            Else
                UnrrbpeOpenDialog.InitialDirectory = Application.StartupPath
            End If
            If UnrrbpeOpenDialog.ShowDialog() = DialogResult.OK Then
                If Path.GetFileName(UnrrbpeOpenDialog.FileName).ToLower = "unrrbpe.exe" Then
                    My.Settings.UnrrbpePath = UnrrbpeOpenDialog.FileName
                    Return True
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If Not My.Settings.UnrrbpePath = "Not Installed" AndAlso
                 File.Exists(My.Settings.UnrrbpePath) Then
                Return True
            Else
                'Attempt to auto-locate the exe
                Dim ApplicationPath As String = Application.StartupPath & Path.DirectorySeparatorChar & "unrrbpe.exe"
                Dim appDataPath As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
                GeneralTools.FolderCheck(appDataPath)
                appDataPath += Path.DirectorySeparatorChar & "unrrbpe.exe"
                If File.Exists(ApplicationPath) Then
                    My.Settings.UnrrbpePath = ApplicationPath
                    Return True
                ElseIf File.Exists(appDataPath) Then
                    My.Settings.UnrrbpePath = appDataPath
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Function CheckIconicZlib(Optional FromOptions As Boolean = False) As Boolean
        'DLL Location is not Stored in the settings
        Dim NeededLocation As String = Application.StartupPath & Path.DirectorySeparatorChar & "Ionic.Zlib.dll"
        If File.Exists(NeededLocation) Then
            Return True
        Else
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "Ionic.Zlib.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation, NeededLocation, True)
                Return True
            Else
                If FromOptions Then
                    Dim ZLibOpenDialog As New OpenFileDialog With {
                        .FileName = "Ionic.Zlib.dll",
                        .Title = "Ionic.Zlib.dll",
                        .Filter = "Dynamic Link Library|*.dll|All files (*.*)|*.*",
                        .CheckFileExists = True}
                    If ZLibOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(ZLibOpenDialog.FileName).ToLower = "ionic.zlib.dll" Then
                            File.Copy(ZLibOpenDialog.FileName, NeededLocation)
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Function CheckOodle6(Optional FromOptions As Boolean = False) As Boolean
        'DLL Location is not Stored in the settings
        Dim NeededLocation As String = Application.StartupPath & Path.DirectorySeparatorChar & "oo2core_6_win64.dll"
        If File.Exists(NeededLocation) Then
            Return True
        Else
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "oo2core_6_win64.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation, NeededLocation, True)
                Return True
            Else
                If FromOptions Then
                    Dim OodleOpenDialog As New OpenFileDialog With {
                        .FileName = "oo2core_6_win64.dll",
                        .Title = "oo2core_6_win64.dll",
                        .Filter = "Dynamic Link Library|*.dll|All files (*.*)|*.*",
                        .CheckFileExists = True}
                    If OodleOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(OodleOpenDialog.FileName).ToLower = "oo2core_6_win64.dll" Then
                            File.Copy(OodleOpenDialog.FileName, NeededLocation)
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function

    Function CheckOodle7(Optional FromOptions As Boolean = False) As Boolean
        'DLL Location is not Stored in the settings
        Dim NeededLocation As String = Application.StartupPath & Path.DirectorySeparatorChar & "oo2core_7_win64.dll"
        If File.Exists(NeededLocation) Then
            Return True
        Else
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "oo2core_7_win64.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation, NeededLocation, True)
                Return True
            Else
                If FromOptions Then
                    Dim OodleOpenDialog As New OpenFileDialog With {
                        .FileName = "oo2core_7_win64.dll",
                        .Title = "oo2core_7_win64.dll",
                        .Filter = "Dynamic Link Library|*.dll|All files (*.*)|*.*",
                        .CheckFileExists = True}
                    If OodleOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(OodleOpenDialog.FileName).ToLower = "oo2core_7_win64.dll" Then
                            File.Copy(OodleOpenDialog.FileName, NeededLocation)
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If
                End If
            End If
        End If
        Return False
    End Function

#End Region

#Region "File Converters"

    Function CheckTexConvExe(Optional FromOptions As Boolean = False)
        'Default exe is set to Not Installed
        If FromOptions Then
            Dim TexConvToolOpenDialog As New OpenFileDialog With {
                .FileName = "texconv.exe",
                .Title = "Select texconv.exe",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.TexConvPath = "Not Installed" AndAlso File.Exists(My.Settings.TexConvPath) Then
                TexConvToolOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.TexConvPath)
                TexConvToolOpenDialog.FileName = Path.GetFileName(My.Settings.TexConvPath)
            Else
                TexConvToolOpenDialog.InitialDirectory = Application.StartupPath
            End If
            If TexConvToolOpenDialog.ShowDialog() = DialogResult.OK Then
                If Path.GetFileName(TexConvToolOpenDialog.FileName).ToLower = "texconv.exe" Then
                    My.Settings.TexConvPath = TexConvToolOpenDialog.FileName
                    Return True
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If Not My.Settings.TexConvPath = "Not Installed" AndAlso
                 File.Exists(My.Settings.TexConvPath) Then
                Return True
            Else
                'Attempt to auto-locate the exe
                Dim ApplicationPath As String = Application.StartupPath & Path.DirectorySeparatorChar & "texconv.exe"
                Dim appDataPath As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
                GeneralTools.FolderCheck(appDataPath)
                appDataPath += Path.DirectorySeparatorChar & "texconv.exe"
                If File.Exists(ApplicationPath) Then
                    My.Settings.TexConvPath = ApplicationPath
                    Return True
                ElseIf File.Exists(appDataPath) Then
                    My.Settings.TexConvPath = appDataPath
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Function CheckTexCrunchExe(Optional FromOptions As Boolean = False)
        'Default exe is set to Not Installed
        If FromOptions Then
            Dim TexCrunchExeOpenDialog As New OpenFileDialog With {
                .FileName = "crunch_x64.exe",
                .Title = "Select crunch_x64.exe",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.CrunchEXELocation = "Not Installed" AndAlso File.Exists(My.Settings.CrunchEXELocation) Then
                TexCrunchExeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.CrunchEXELocation)
                TexCrunchExeOpenDialog.FileName = Path.GetFileName(My.Settings.CrunchEXELocation)
            Else
                TexCrunchExeOpenDialog.InitialDirectory = Application.StartupPath
            End If
            If TexCrunchExeOpenDialog.ShowDialog() = DialogResult.OK Then
                If Path.GetFileName(TexCrunchExeOpenDialog.FileName).ToLower = "crunch_x64.exe" Then
                    My.Settings.CrunchEXELocation = TexCrunchExeOpenDialog.FileName
                    Return True
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If Not My.Settings.CrunchEXELocation = "Not Installed" AndAlso
                File.Exists(My.Settings.CrunchEXELocation) Then
                Return True
            Else
                'Attempt to auto-locate the exe
                Dim ApplicationPath As String = Application.StartupPath & Path.DirectorySeparatorChar & "crunch_x64.exe"
                Dim appDataPath As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
                GeneralTools.FolderCheck(appDataPath)
                appDataPath += Path.DirectorySeparatorChar & "crunch_x64.exe"
                If File.Exists(ApplicationPath) Then
                    My.Settings.CrunchEXELocation = ApplicationPath
                    Return True
                ElseIf File.Exists(appDataPath) Then
                    My.Settings.CrunchEXELocation = appDataPath
                    Return True
                End If
            End If
        End If
        Return False
    End Function

#End Region

#Region "File Openers"

    Function CheckRadVideo(Optional FromOptions As Boolean = False)
        'Default exe is set to Not Installed
        If FromOptions Then
            Dim RADVideoToolOpenDialog As New OpenFileDialog With {
                .FileName = "radvideo.exe",
                .Title = "Select radvideo.exe",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.RADVideoToolPath = "Not Installed" Then
                RADVideoToolOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.RADVideoToolPath)
            End If
            If RADVideoToolOpenDialog.ShowDialog = DialogResult.OK Then
                If Path.GetFileName(RADVideoToolOpenDialog.FileName) = "radvideo.exe" Then
                    My.Settings.RADVideoToolPath = RADVideoToolOpenDialog.FileName.Replace("radvideo.exe", "binkplay.exe")
                    Return True
                Else
                    MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                End If
            End If
        Else
            If My.Settings.RADVideoToolPath = "Not Installed" Then
                Dim AllDrives As DriveInfo() = DriveInfo.GetDrives()
                For Each Drive As DriveInfo In AllDrives
                    'MessageBox.Show(Drive.Name & "Program Files (x86)\RADVideo\radvideo.exe")
                    If (Drive.IsReady = True) Then
                        If File.Exists(Drive.Name & "Program Files (x86)\RADVideo\radvideo.exe") Then
                            My.Settings.RADVideoToolPath = Drive.Name & "Program Files (x86)\RADVideo\binkplay.exe"
                            Return True
                        ElseIf File.Exists(Drive.Name & "Program Files\RADVideo\radvideo.exe") Then
                            My.Settings.RADVideoToolPath = Drive.Name & "Program Files\RADVideo\binkplay.exe"
                            Return True
                        End If
                    End If
                Next
            Else
                If File.Exists(My.Settings.RADVideoToolPath) Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Function CheckDDSexe(Optional FromOptions As Boolean = False)
        'Default exe is set to Not Installed
        If FromOptions Then
            Dim DDSOpenExeOpenDialog As New OpenFileDialog With {
                .FileName = "*.exe",
                .Title = "Select exe for opening DDS files",
                .Filter = "Executable File|*.exe|All files (*.*)|*.*",
                .CheckFileExists = True}
            If Not My.Settings.DDSexeLocation = "Not Installed" Then
                DDSOpenExeOpenDialog.InitialDirectory = Path.GetDirectoryName(My.Settings.DDSexeLocation)
                DDSOpenExeOpenDialog.FileName = Path.GetFileName(My.Settings.DDSexeLocation)
            End If
            If DDSOpenExeOpenDialog.ShowDialog = DialogResult.OK Then
                My.Settings.DDSexeLocation = DDSOpenExeOpenDialog.FileName
                MainForm.OpenImageWithToolStripMenuItem.Text = "Open With " & Path.GetFileNameWithoutExtension(My.Settings.DDSexeLocation)
                Return True
            End If
        Else
            If Not My.Settings.DDSexeLocation = "Not Installed" Then
                If File.Exists(My.Settings.DDSexeLocation) Then
                    MainForm.OpenImageWithToolStripMenuItem.Text = "Open With " & Path.GetFileNameWithoutExtension(My.Settings.DDSexeLocation)
                    Return True
                Else
                End If
            End If
        End If
        MainForm.OpenImageWithToolStripMenuItem.Text = "Open With..."
        Return False
    End Function

    Function CheckFrosty(Optional FromOptions As Boolean = False) As Boolean
        'DLL Location is not Stored in the settings
        Dim NeededLocation As String = Application.StartupPath & Path.DirectorySeparatorChar & "FrostyYukes.dll"
        If File.Exists(NeededLocation) Then
            Return True
        Else
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "FrostyYukes.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation, NeededLocation, True)
                Return True
            Else
                If FromOptions Then
                    Dim FrostyYukesOpenDialog As New OpenFileDialog With {
                        .FileName = "FrostyYukes.dll",
                        .Title = "FrostyYukes.dll",
                        .Filter = "Dynamic Link Library|*.dll|All files (*.*)|*.*",
                        .CheckFileExists = True}
                    If FrostyYukesOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(FrostyYukesOpenDialog.FileName).ToLower = "frostyyukes.dll" Then
                            File.Copy(FrostyYukesOpenDialog.FileName, NeededLocation, True)
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If
                Else
                    MessageBox.Show("FrostyYukes Dll Not loaded")
                End If
            End If
        End If
        Return False
    End Function

#End Region

#Region "Optional Improvements"

    Function CheckFontAwesome(Optional FromOptions As Boolean = False) As Boolean
        'DLL Location is not Stored in the settings
        Dim NeededLocation As String = Application.StartupPath & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll"
        If File.Exists(NeededLocation) Then
            Return True
        Else
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "FontAwesome.Sharp.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation, NeededLocation, True)
                Return True
            Else
                If FromOptions Then
                    Dim FontAwesomeOpenDialog As New OpenFileDialog With {
                        .FileName = "FontAwesome.Sharp.dll",
                        .Title = "FontAwesome.Sharp.dll",
                        .Filter = "Dynamic Link Library|*.dll|All files (*.*)|*.*",
                        .CheckFileExists = True}
                    If FontAwesomeOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(FontAwesomeOpenDialog.FileName).ToLower = "fontawesome.sharp.dll" Then
                            File.Copy(FontAwesomeOpenDialog.FileName, NeededLocation, True)
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If
                Else
                    'MessageBox.Show("FontAwesome Dll Not loaded")
                End If
            End If
        End If
        Return False
    End Function

    Function CheckNewtonsoftJson(Optional FromOptions As Boolean = False) As Boolean
        'DLL Location is not Stored in the settings
        Dim NeededLocation As String = Application.StartupPath & Path.DirectorySeparatorChar & "Newtonsoft.Json.dll"
        If File.Exists(NeededLocation) Then
            Return True
        Else
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "Newtonsoft.Json.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation, NeededLocation, True)
                Return True
            Else
                If FromOptions Then
                    Dim JsonParserOpenDialog As New OpenFileDialog With {
                        .FileName = "Newtonsoft.Json.dll",
                        .Title = "Newtonsoft.Json.dll",
                        .Filter = "Dynamic Link Library|*.dll|All files (*.*)|*.*",
                        .CheckFileExists = True}
                    If JsonParserOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(JsonParserOpenDialog.FileName).ToLower = "newtonsoft.json.dll" Then
                            File.Copy(JsonParserOpenDialog.FileName, NeededLocation, True)
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        End If
                    End If
                Else
                    'MessageBox.Show("Json Parse Dll Not loaded")
                End If
            End If
        End If
        Return False
    End Function

#End Region


End Module