Imports System.Environment 'appdata
Imports System.IO   'Files
Imports System.Runtime.Serialization.Formatters.Binary 'Binary Formatter

Module SettingsHandlers

    Sub SettingsCheck()
        If My.Settings.UpgradeRequired = True Then
            My.Settings.Upgrade()
            My.Settings.UpgradeRequired = False
            My.Settings.Save()
        End If
        If My.Settings.ExeLocation = "" Then
            ApplicationHandlers.CheckGameExeFolder(True)
            'exes
            ApplicationHandlers.CheckBPEExe()
            ApplicationHandlers.CheckUnrrbpe()
            ApplicationHandlers.CheckTexConvExe()
            ApplicationHandlers.CheckTexCrunchExe()
            ApplicationHandlers.CheckRadVideo()
            ApplicationHandlers.CheckDDSexe()
            'dlls
            ApplicationHandlers.CheckIconicZlib()
            ApplicationHandlers.CheckOodle6()
            ApplicationHandlers.CheckOodle7()
            ApplicationHandlers.CheckFontAwesome()
            ApplicationHandlers.CheckNewtonsoftJson()
        End If

        If My.Settings.StringObjectLocation = "" Then
            My.Settings.StringObjectLocation = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\Strings.bin"
        End If
        If File.Exists(My.Settings.StringObjectLocation) Then
            Try
                Dim fs As Stream = New FileStream(My.Settings.StringObjectLocation, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                MainForm.StringReferences = CType(bf.Deserialize(fs), Dictionary(Of UInt32, String))
                fs.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                ResetStringReferences()
            End Try
        Else
            ResetStringReferences()
        End If
        If My.Settings.PacNumObjectLocation = "" Then
            My.Settings.PacNumObjectLocation = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\PacNums.bin"
        End If
        If File.Exists(My.Settings.PacNumObjectLocation) Then
            Try
                Dim fs As Stream = New FileStream(My.Settings.PacNumObjectLocation, FileMode.Open)
                Dim bf As BinaryFormatter = New BinaryFormatter()
                MainForm.PacNumbers = CType(bf.Deserialize(fs), Integer())
                fs.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                ResetPacNumbers()
            End Try
        Else
            ResetPacNumbers()
        End If
        If IsNothing(My.Settings.SkippedVersion) Then
            My.Settings.SkippedVersion = My.Application.Info.Version
        ElseIf My.Settings.SkippedVersion.ToString = "0.0" Then
            My.Settings.SkippedVersion = My.Application.Info.Version
        End If
        My.Settings.Save()
    End Sub

    Sub ResetStringReferences()
        MainForm.StringReferences = New Dictionary(Of UInt32, String) From {
                    {0, "String Not Read"}
                }
    End Sub

    Sub ResetPacNumbers()
        MainForm.PacNumbers = New Integer(1024) {}
        MainForm.PacNumbers(0) = -1
    End Sub

    Function SaveSettingsFiles() As DialogResult
        'WARNING MAIN FORM REFERENCES
        Try
            'Here is the saving of the String File so we don't store it in the settings file and bloat the loading and closing.
            If Not IsNothing(MainForm.StringReferences) Then
                If File.Exists(My.Settings.StringObjectLocation) = True Then
                    File.Delete(My.Settings.StringObjectLocation)
                End If
                Dim StringFileStream As Stream = New FileStream(My.Settings.StringObjectLocation, FileMode.Create)
                Dim StringBinaryFormatter As BinaryFormatter = New BinaryFormatter()
                StringBinaryFormatter.Serialize(StringFileStream, MainForm.StringReferences)
                StringFileStream.Close()
            End If
            If Not IsNothing(MainForm.PacNumbers) Then
                If File.Exists(My.Settings.PacNumObjectLocation) = True Then
                    File.Delete(My.Settings.PacNumObjectLocation)
                End If
                Dim PacNumFileStream As Stream = New FileStream(My.Settings.PacNumObjectLocation, FileMode.Create)
                Dim PacNumBinaryFormatter As BinaryFormatter = New BinaryFormatter()
                PacNumBinaryFormatter.Serialize(PacNumFileStream, MainForm.PacNumbers)
                PacNumFileStream.Close()
            End If
            Return DialogResult.OK
        Catch ex As Exception
            Return MessageBox.Show("Error Savings Settings Files" & vbNewLine &
                            ex.Message & vbNewLine & "Continue?", "Settings Error", MessageBoxButtons.OKCancel)
        End Try
    End Function

End Module