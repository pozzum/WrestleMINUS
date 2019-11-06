Imports System.Text.RegularExpressions
Imports System.Threading 'Multi-threading

Public Class OnlineVersion

    Shared AnnounceInternetVersion As Boolean = False

    Shared Sub CheckUpdate(Optional FromOptions As Boolean = False)
        AnnounceInternetVersion = FromOptions
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
                If AnnounceInternetVersion Then
                    MessageBox.Show("Site Not Found")
                End If
                Exit Sub
            End If
            'MessageBox.Show("Response Get")
            Const pattern As String = "Current Version: (?<version>\d.\d.\d.\d)"
            Dim TempMatch As Match = Regex.Match(Response, pattern)
            If TempMatch.Success Then
                Dim CurrentVersionString As String = TempMatch.Groups("version").Value
                Dim CurrentVer As Integer() = GeneralTools.NumberArrayFromVersionString(CurrentVersionString)
                Dim LocalVersionString As String = My.Application.Info.Version.ToString
                Dim LocalVer As Integer() = GeneralTools.NumberArrayFromVersionString(LocalVersionString)
                Dim SkippedVersionString As String = My.Settings.SkippedVersion.ToString
                Dim SkippedVer As Integer() = GeneralTools.NumberArrayFromVersionString(SkippedVersionString)
                'First compare the Skipped version Local
                Dim ChangeType As UpdateType = GetUpdateType(SkippedVer, LocalVer)
                If Not ChangeType = UpdateType.None Then
                    'Local Version is the same as skipped
                    ChangeType = GetUpdateType(CurrentVer, LocalVer)
                    If Not ChangeType = UpdateType.None Then
                        Dim result As Integer = MessageBox.Show("You have version " & LocalVersionString &
                                                            " and there is a new " & GetUpdateString(ChangeType) &
                                                            " version " & CurrentVersionString &
                                                            vbNewLine & "Would you like to update?",
                                                            "Version Update",
                                                            MessageBoxButtons.YesNo)
                        If result = DialogResult.Yes Then
                            OfferDownloadAd()
                        Else
                            'Ask if they want to skip this update
                            Dim SkipResult As Integer = MessageBox.Show("Would you like to skip this update in future?",
                                                            "Skip Version?",
                                                            MessageBoxButtons.YesNo)
                            If SkipResult = DialogResult.Yes Then
                                Dim TempVersion As Version = Nothing
                                If Version.TryParse(CurrentVersionString, TempVersion) Then
                                    My.Settings.SkippedVersion = TempVersion
                                End If
                            End If
                        End If
                    Else

                        If AnnounceInternetVersion Then
                            MessageBox.Show("No new update." & vbNewLine & "Online version: " & CurrentVersionString)
                        End If
                        'No new update
                    End If
                Else
                    'Skip is newer
                    'We want to compare the skipped version to online version
                    ChangeType = GetUpdateType(CurrentVer, SkippedVer)
                    If Not ChangeType = UpdateType.None Then
                        Dim result As Integer = MessageBox.Show("You have skipped version " & SkippedVersionString &
                                                            " and there is a new " & GetUpdateString(ChangeType) &
                                                            " version " & CurrentVersionString &
                                                            vbNewLine & "Would you like to update?",
                                                            "Version Update",
                                                            MessageBoxButtons.YesNo)
                        If result = DialogResult.Yes Then
                            OfferDownloadAd()
                        Else
                            'Ask if they want to skip this update
                            Dim SkipResult As Integer = MessageBox.Show("Would you like to skip this update in future?",
                                                            "Skip Version?",
                                                            MessageBoxButtons.YesNo)
                            If SkipResult = DialogResult.Yes Then
                                Dim TempVersion As Version = Nothing
                                If Version.TryParse(CurrentVersionString, TempVersion) Then
                                    My.Settings.SkippedVersion = TempVersion
                                End If
                            End If
                        End If
                    Else
                        If AnnounceInternetVersion Then
                            MessageBox.Show("Update Already Skipped." & vbNewLine & CurrentVersionString)
                        End If
                        'Update already Skipped
                    End If
                End If
            End If
        Catch ex As NullReferenceException
            If AnnounceInternetVersion Then
                MessageBox.Show(ex.Message)
            End If
        End Try
    End Sub

    Shared Sub OfferDownloadAd()
        Dim Adresult As Integer = MessageBox.Show("Would you be willing to view an ad?", "ads help support development", MessageBoxButtons.YesNo)
        If Adresult = DialogResult.Yes Then
            Process.Start("http://metastead.com/12819869/wrestleminus")
        Else
            Process.Start(PageAddress)
        End If
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

    Shared Function GetUpdateType(CheckedVersion As Integer(), LocalVersion As Integer()) As UpdateType
        If CheckedVersion(0) > LocalVersion(0) Then
            Return UpdateType.MajorUpdate
        ElseIf CheckedVersion(0) = LocalVersion(0) Then
            If CheckedVersion(1) > LocalVersion(1) Then
                Return UpdateType.MinorUpdate
            ElseIf CheckedVersion(1) = LocalVersion(1) Then
                If CheckedVersion(2) > LocalVersion(2) Then
                    Return UpdateType.MajorBugFix
                ElseIf CheckedVersion(2) = LocalVersion(2) Then
                    If CheckedVersion(3) > LocalVersion(3) Then
                        Return UpdateType.MinorBugFix
                    End If
                End If
            End If
        End If
        Return UpdateType.None
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