Imports System.Text.RegularExpressions
Imports System.Threading 'Multithreading

Public Class OnlineVersion

    Shared Sub CheckUpdate()
        Dim checkUpdateThread = New Thread(AddressOf CheckInternetVersion)
        checkUpdateThread.SetApartmentState(ApartmentState.STA)
        checkUpdateThread.Start()
    End Sub

    'uses smrando's updater as a base https://github.com/Dessyreqt/smrandomizer/blob/master/SuperMetroidRandomizer/Net/RandomizerVersion.cs
    Public Enum UpdateType
        None
        MinorBugFix
        MajorBugFix
        MinorUpdate
        MajorUpdate
    End Enum

    Shared ReadOnly PageAddress As String = "https://pozzum.github.io/WrestleMINUS/"

    'Dim LocalVersionSplit() As Integer = CInt(Split(LocalVersion, "."))
    Shared Sub CheckInternetVersion()
        Try
            Dim Response = GetResponse(PageAddress)
            If String.IsNullOrWhiteSpace(Response) Then
                'do nothing
            End If
            'MessageBox.Show("Response Get")
            Const pattern As String = "Current Version: (?<version>\d.\d.\d.\d)"
            Dim TempMatch As Match = Regex.Match(Response, pattern)
            If TempMatch.Success Then
                Dim CurrentVersion As String = TempMatch.Groups("version").Value
                Dim CurrentVersionTemp() As String = Split(CurrentVersion, ".")
                Dim CurrentVersionInt(3) As Integer
                Dim LocalVersion As String = My.Application.Info.Version.ToString
                Dim LocalVersionTemp() As String = Split(LocalVersion, ".")
                Dim LocalVersionInt(3) As Integer
                Dim SkippedVersion As String = My.Settings.SkippedVersion.ToString
                Dim SkippedVersionTemp() As String = Split(SkippedVersion, ".")
                Dim SkippedVersionInt(3) As Integer
                For s As Integer = 0 To 3
                    CurrentVersionInt(s) = CInt(CurrentVersionTemp(s))
                    LocalVersionInt(s) = CInt(LocalVersionTemp(s))
                    SkippedVersionInt(s) = CInt(SkippedVersionTemp(s))
                Next
                'First compare the Skipped version Local
                Dim ChangeType As UpdateType = GetUpdateType(SkippedVersionInt, LocalVersionInt)
                If Not ChangeType = UpdateType.None Then
                    'Local Version is the same as skipped
                    ChangeType = GetUpdateType(CurrentVersionInt, LocalVersionInt)
                    If Not ChangeType = UpdateType.None Then
                        Dim result As Integer = MessageBox.Show("You have version " & LocalVersion &
                                                            " and there is a new " & GetUpdateString(ChangeType) &
                                                            " version " & CurrentVersion &
                                                            vbNewLine & "Would you like to update?",
                                                            "Version Update",
                                                            MessageBoxButtons.YesNo)
                        If result = DialogResult.Yes Then
                            Dim Adresult As Integer = MessageBox.Show("Would you be willing to view an ad?", "ads help support development", MessageBoxButtons.YesNo)
                            If Adresult = DialogResult.Yes Then
                                Process.Start("http://metastead.com/12819869/wrestleminus")
                            Else
                                Process.Start(PageAddress)
                            End If
                        Else
                            'Ask if they want to skip this update
                            Dim SkipResult As Integer = MessageBox.Show("Would you like to skip this update in future?",
                                                            "Skip Version?",
                                                            MessageBoxButtons.YesNo)
                            If SkipResult = DialogResult.Yes Then
                                Dim TempVersion As Version = Nothing
                                If Version.TryParse(CurrentVersion, TempVersion) Then
                                    My.Settings.SkippedVersion = TempVersion
                                End If
                            End If
                        End If
                    Else
                        'No new update
                    End If
                Else
                    'Skip is newer
                    'We want to compare the skipped version to online version
                    ChangeType = GetUpdateType(CurrentVersionInt, LocalVersionInt)
                    If Not ChangeType = UpdateType.None Then
                        Dim result As Integer = MessageBox.Show("You have skipped version " & SkippedVersion &
                                                            " and there is a new " & GetUpdateString(ChangeType) &
                                                            " version " & CurrentVersion &
                                                            vbNewLine & "Would you like to update?",
                                                            "Version Update",
                                                            MessageBoxButtons.YesNo)
                        If result = DialogResult.Yes Then
                            Dim Adresult As Integer = MessageBox.Show("Would you be willing to view an ad?", "ads help support development", MessageBoxButtons.YesNo)
                            If Adresult = DialogResult.Yes Then
                                Process.Start("http://metastead.com/12819869/wrestleminus")
                            Else
                                Process.Start(PageAddress)
                            End If
                        Else
                            'Ask if they want to skip this update
                            Dim SkipResult As Integer = MessageBox.Show("Would you like to skip this update in future?",
                                                            "Skip Version?",
                                                            MessageBoxButtons.YesNo)
                            If SkipResult = DialogResult.Yes Then
                                Dim TempVersion As Version = Nothing
                                If Version.TryParse(CurrentVersion, TempVersion) Then
                                    My.Settings.SkippedVersion = TempVersion
                                End If
                            End If
                        End If
                    Else
                        'Update already Skipped
                    End If
                End If
            End If
        Catch ex As NullReferenceException
            'MessageBox.Show(ex.Message)
        End Try
    End Sub

    Shared Function GetResponse(Address As String)
        If Not Address.Contains("pozzum.github.io/WrestleMINUS/") Then
            Return ""
        End If
        Dim TempBrowser As WebBrowser = New WebBrowser With {
            .ScrollBarsEnabled = False,
            .ScriptErrorsSuppressed = True}
        TempBrowser.Navigate(Address)
        While (TempBrowser.ReadyState = WebBrowserReadyState.Complete) = False
            Application.DoEvents()
        End While
        Return TempBrowser.Document.Body.InnerHtml
    End Function

    Shared Function GetUpdateType(CheckedVersion, LocalVersion) As UpdateType
        If CheckedVersion(0) > LocalVersion(0) Then
            Return UpdateType.MajorUpdate
        ElseIf CheckedVersion(0) < LocalVersion(0) Then
            Return UpdateType.None
        ElseIf CheckedVersion(1) > LocalVersion(1) Then
            Return UpdateType.MinorUpdate
        ElseIf CheckedVersion(1) < LocalVersion(1) Then
            Return UpdateType.None
        ElseIf CheckedVersion(2) > LocalVersion(2) Then
            Return UpdateType.MajorBugFix
        ElseIf CheckedVersion(2) < LocalVersion(2) Then
            Return UpdateType.None
        ElseIf CheckedVersion(3) > LocalVersion(3) Then
            Return UpdateType.MinorBugFix
        Else
            Return UpdateType.None
        End If

    End Function

    Shared Function GetUpdateString(ChangeType As UpdateType)
        If ChangeType = UpdateType.MajorUpdate Then
            Return "major update"
        ElseIf ChangeType = UpdateType.MinorUpdate Then
            Return "minor update"
        ElseIf ChangeType = UpdateType.MajorBugFix Then
            Return "major bugfix"
        ElseIf ChangeType = UpdateType.MinorBugFix Then
            Return "minor bugfix"
        Else
            Return ""
        End If
    End Function

End Class