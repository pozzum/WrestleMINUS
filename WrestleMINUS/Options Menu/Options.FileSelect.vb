Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class OptionsMenu

#Region "File Select Tab"

    Sub LoadFileSelectTab()
        TextBoxHome.Text = IO.Path.GetDirectoryName(My.Settings.ExeLocation)
        If ApplicationHandlers.CheckGameExeFolder Then
            LabelHomeDirectory.ForeColor = Color.Black
        Else
            LabelHomeDirectory.ForeColor = Color.Red
        End If
        TextBoxTexConv.Text = My.Settings.TexConvPath
        If ApplicationHandlers.CheckTexConvExe Then
            LabelTexConv.ForeColor = Color.Black
        Else
            LabelTexConv.ForeColor = Color.Red
        End If
        TextBoxCrunchExe.Text = My.Settings.CrunchEXELocation
        If ApplicationHandlers.CheckTexCrunchExe() Then
            LabelCrunchExe.ForeColor = Color.Black
        Else
            LabelCrunchExe.ForeColor = Color.Red
        End If
        TextBoxRadVideo.Text = My.Settings.RADVideoToolPath
        If ApplicationHandlers.CheckRadVideo Then
            LabelRadVideo.ForeColor = Color.Black
        Else
            LabelRadVideo.ForeColor = Color.Red
        End If
        TextBoxBPEExe.Text = My.Settings.BPEExePath
        If ApplicationHandlers.CheckBPEExe Then
            LabelBPEExe.ForeColor = Color.Black
        Else
            LabelBPEExe.ForeColor = Color.Red
        End If
        TextBoxUnrrbpe.Text = My.Settings.UnrrbpePath
        If ApplicationHandlers.CheckUnrrbpe Then
            LabelUnrrbpe.ForeColor = Color.Black
        Else
            LabelUnrrbpe.ForeColor = Color.Red
        End If
        TextBoxDDSExe.Text = My.Settings.DDSexeLocation
        If ApplicationHandlers.CheckDDSexe Then
            LabelDSSExe.ForeColor = Color.Black
        Else
            LabelDSSExe.ForeColor = Color.Red
        End If
        LabelSkipVersion.Text = "Skipped Ver.: " & My.Settings.SkippedVersion.ToString
    End Sub

    Private Sub ButtonSelectHome_Click(sender As Object, e As EventArgs) Handles ButtonSelectHome.Click
        ApplicationHandlers.CheckGameExeFolder(True)
        LoadSettings()
    End Sub

    Private Sub ButtonSelectTexConv_Click(sender As Object, e As EventArgs) Handles ButtonSelectTexConv.Click
        ApplicationHandlers.CheckTexConvExe(True)
        LoadSettings()
    End Sub

    Private Sub ButtonSelectCrunchExe_Click(sender As Object, e As EventArgs) Handles ButtonSelectCrunchExe.Click
        ApplicationHandlers.CheckTexCrunchExe(True)
        LoadSettings()
    End Sub

    Private Sub ButtonDownloadRadVideo_Click(sender As Object, e As EventArgs) Handles ButtonDownloadRadVideo.Click
        Process.Start("http://www.radgametools.com/bnkdown.htm")
    End Sub

    Private Sub ButtonSelectRadVideo_Click(sender As Object, e As EventArgs) Handles ButtonSelectRadVideo.Click
        ApplicationHandlers.CheckRadVideo(True)
        LoadSettings()
    End Sub

    'I am banned from this site so I can't actually get it..
    Private Sub ButtonDownloadBPEExe_Click(sender As Object, e As EventArgs) Handles ButtonDownloadBPEExe.Click
        Process.Start("https://www.tapatalk.com/groups/legendsofmodding/bpe-compression-tool-t3741.html#p22164")
    End Sub

    Private Sub ButtonSelectBPEExe_Click(sender As Object, e As EventArgs) Handles ButtonSelectBPEExe.Click
        ApplicationHandlers.CheckBPEExe(True)
        LoadSettings()
    End Sub

    Private Sub ButtonDownloadUnrrbpe_Click(sender As Object, e As EventArgs) Handles ButtonDownloadUnrrbpe.Click
        Process.Start("http://asmodean.reverse.net/pages/unrrbpe.html")
    End Sub

    Private Sub ButtonSelectUnrrbpe_Click(sender As Object, e As EventArgs) Handles ButtonSelectUnrrbpe.Click
        ApplicationHandlers.CheckUnrrbpe(True)
        LoadSettings()
    End Sub

    Private Sub ButtonDownloadDDSexe_Click(sender As Object, e As EventArgs) Handles ButtonDownloadDDSexe.Click
        Process.Start("https://www.getpaint.net/")
    End Sub

    Private Sub ButtonSelectDDSexe_Click(sender As Object, e As EventArgs) Handles ButtonSelectDDSexe.Click
        ApplicationHandlers.CheckDDSexe(True)
        LoadSettings()
    End Sub

    Private Sub ButtonCheckUpdate_Click(sender As Object, e As EventArgs) Handles ButtonCheckUpdate.Click
        My.Settings.SkippedVersion = My.Application.Info.Version
        OnlineVersion.CheckUpdate(True)
        LoadSettings()
    End Sub

#End Region

End Class