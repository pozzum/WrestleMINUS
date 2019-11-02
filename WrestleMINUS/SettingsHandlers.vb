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
            ApplicationHandlers.CheckGameExeFolder(True)
        End If
        If My.Settings.TexConvPath = "" Then 'Locate the texture conversion exe
            ApplicationHandlers.CheckTexConvExe()
        End If
        If My.Settings.CrunchEXELocation = "" Then 'Locate the texture crunch exe
            ApplicationHandlers.CheckTexCrunchExe()
        End If
        If My.Settings.RADVideoToolPath = "" Then
            ApplicationHandlers.CheckRadVideo()
        End If
        ApplicationHandlers.CheckUnrrbpe()
        ApplicationHandlers.CheckDDSexe()
        If My.Settings.StringObject = "" Then
            Dim AppDataStringLocation As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS\Strings.bin"
            If File.Exists(AppDataStringLocation) Then
                Try
                    Dim fs As Stream = New FileStream(AppDataStringLocation, FileMode.Open)
                    Dim bf As BinaryFormatter = New BinaryFormatter()
                    MainForm.StringReferences = CType(bf.Deserialize(fs), Dictionary(Of UInt32, String))
                    fs.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    MainForm.StringReferences = New Dictionary(Of UInt32, String) From {
                        {0, "String Not Read"}
                    }
                End Try
            Else
                MainForm.StringReferences = New Dictionary(Of UInt32, String) From {
                        {0, "String Not Read"}
                    }
            End If
            My.Settings.StringObject = AppDataStringLocation
        Else
            If File.Exists(My.Settings.StringObject) Then
                Try
                    Dim fs As Stream = New FileStream(My.Settings.StringObject, FileMode.Open)
                    Dim bf As BinaryFormatter = New BinaryFormatter()
                    MainForm.StringReferences = CType(bf.Deserialize(fs), Dictionary(Of UInt32, String))
                    fs.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    MainForm.StringReferences = New Dictionary(Of UInt32, String) From {
                        {0, "String Not Read"}
                    }
                End Try
            Else
                MainForm.StringReferences = New Dictionary(Of UInt32, String) From {
                        {0, "String Not Read"}
                    }
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
        If IsNothing(My.Settings.SkippedVersion) Then
            My.Settings.SkippedVersion = My.Application.Info.Version
        ElseIf My.Settings.SkippedVersion.ToString = "0.0" Then
            My.Settings.SkippedVersion = My.Application.Info.Version
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

End Class