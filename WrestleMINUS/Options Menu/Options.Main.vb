Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class OptionsMenu

#Region "Options Tab"

    Sub LoadOptionsTab()
        CheckBoxLoadHome.Checked = My.Settings.LoadHomeOnLaunch
        CheckBoxBackup.Checked = My.Settings.BackupInjections
        CheckBoxDeleteTempBMP.Checked = My.Settings.DeleteTempBMP
        CheckBoxRecycleDeletedFiles.Checked = My.Settings.RecycleDeletedFiles
        CheckBoxTreeNodeIcons.Checked = My.Settings.UseTreeIcons
        CheckBoxDetailedFileNames.Checked = My.Settings.UseDetailedFileNames
        CheckBoxExtractAllinPlace.Checked = My.Settings.DecompresstoFolder
        CheckBoxOODLBypass.Checked = My.Settings.BypassOODLWarn
        'extract all can only extract to folders with detailed file names
        If CheckBoxDetailedFileNames.Checked Then
            CheckBoxExtractAllinPlace.Enabled = True
        Else
            CheckBoxExtractAllinPlace.Checked = True
            CheckBoxExtractAllinPlace.Enabled = False
        End If
    End Sub

    Private Sub CheckBoxLoadHome_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxLoadHome.CheckedChanged
        My.Settings.LoadHomeOnLaunch = CheckBoxLoadHome.Checked
    End Sub

    Private Sub CheckBoxBackup_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxBackup.CheckedChanged
        My.Settings.BackupInjections = CheckBoxBackup.Checked
    End Sub

    Private Sub CheckBoxDeleteTempBMP_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDeleteTempBMP.CheckedChanged
        My.Settings.DeleteTempBMP = CheckBoxDeleteTempBMP.Checked
    End Sub

    Private Sub CheckBoxRecycleDeletedFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxRecycleDeletedFiles.CheckedChanged
        My.Settings.RecycleDeletedFiles = CheckBoxRecycleDeletedFiles.Checked
    End Sub

    Private Sub CheckBoxTreeNodeIcons_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxTreeNodeIcons.CheckedChanged
        My.Settings.UseTreeIcons = CheckBoxTreeNodeIcons.Checked
        MainForm.LoadIcons()
    End Sub

    Private Sub CheckBoxDetailedFileNames_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxDetailedFileNames.CheckedChanged
        My.Settings.UseDetailedFileNames = CheckBoxDetailedFileNames.Checked
        If CheckBoxDetailedFileNames.Checked Then
            CheckBoxExtractAllinPlace.Enabled = True
        Else
            CheckBoxExtractAllinPlace.Checked = True
            CheckBoxExtractAllinPlace.Enabled = False
        End If
    End Sub

    Private Sub CheckBoxExtractAllinPlace_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxExtractAllinPlace.CheckedChanged
        My.Settings.DecompresstoFolder = CheckBoxExtractAllinPlace.Checked
    End Sub

    Private Sub CheckBoxOODLBypass_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxOODLBypass.CheckedChanged
        My.Settings.BypassOODLWarn = CheckBoxOODLBypass.Checked
    End Sub

#End Region

End Class