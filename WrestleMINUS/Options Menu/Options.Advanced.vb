﻿Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class OptionsMenu

    Sub LoadAdvancedTab()
            CheckBoxShowSelectedNode.Checked = My.Settings.ShowSelectedNode
            CheckBoxSuppresHSPCFooters.Checked = My.Settings.SuppressHSPCFooters
            CheckBoxCreateCAkDef.Checked = My.Settings.CreateCAkDefFiles
            CheckBoxShowCAkIntermediates.Checked = My.Settings.ShowCAkIntermediates
            CheckBoxAppendDef.Checked = My.Settings.AppendDefFileRebuild
            CheckDisableModPref.Checked = My.Settings.DisableModPref
            CheckRelocateMods.Checked = My.Settings.RelocateModFolderMods
            UpdateDefOptions()
            ComboBoxOodleCompressionLevel.Items.AddRange(System.Enum.GetNames(GetType(PackUnpack.OodleCompressionLevel)))
            ComboBoxOodleCompressionLevel.SelectedIndex = My.Settings.OODLCompressionLevel
            TrackBarDecimalNameLength.Value = My.Settings.DecimalNameMinLength
            LabelDecimalNameLength.Text = "Decimal File Name Min Length: " & TrackBarDecimalNameLength.Value
            TrackBarHexLength.Value = My.Settings.HexViewLength
            TabControl1.SelectedIndex = My.Settings.OptionMenuSelectedTab
            UpdateViewLength()
        End Sub

        Private Sub CheckBoxShowSelectedNode_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowSelectedNode.CheckedChanged
            My.Settings.ShowSelectedNode = CheckBoxShowSelectedNode.Checked
            MainForm.ApplyCurrentViewOption()
        End Sub

        Private Sub CheckBoxSuppresHSPCFooters_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxSuppresHSPCFooters.CheckedChanged
            If Not My.Settings.SuppressHSPCFooters = CheckBoxSuppresHSPCFooters.Checked Then
                My.Settings.SuppressHSPCFooters = CheckBoxSuppresHSPCFooters.Checked
                If MessageBox.Show("Would you like to reload the file list?", "Refresh Files?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    MainForm.LoadInitalFilesToTree()
                End If
            End If
            My.Settings.SuppressHSPCFooters = CheckBoxSuppresHSPCFooters.Checked
        End Sub

        Private Sub CheckBoxCreateCAkDef_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCreateCAkDef.CheckedChanged
            My.Settings.CreateCAkDefFiles = CheckBoxCreateCAkDef.Checked
        End Sub

        Private Sub CheckBoxShowCAkIntermediates_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowCAkIntermediates.CheckedChanged
            My.Settings.ShowCAkIntermediates = CheckBoxShowCAkIntermediates.Checked
        End Sub

        Private Sub CheckBoxAppendDef_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxAppendDef.CheckedChanged
            My.Settings.AppendDefFileRebuild = CheckBoxAppendDef.Checked
            UpdateDefOptions()
        End Sub

        Private Sub CheckDisableModPref_CheckedChanged(sender As Object, e As EventArgs) Handles CheckDisableModPref.CheckedChanged
            My.Settings.DisableModPref = CheckDisableModPref.Checked
            UpdateDefOptions()
        End Sub

        Private Sub CheckRelocateMods_CheckedChanged(sender As Object, e As EventArgs) Handles CheckRelocateMods.CheckedChanged
            My.Settings.RelocateModFolderMods = CheckRelocateMods.Checked
            UpdateDefOptions()
        End Sub

        Sub UpdateDefOptions()
            If CheckBoxAppendDef.Checked Then
                CheckDisableModPref.Checked = False
                CheckDisableModPref.Enabled = False
                CheckRelocateMods.Checked = False
                CheckRelocateMods.Enabled = False
            Else 'false
                CheckDisableModPref.Enabled = True
                If CheckDisableModPref.Checked Then
                    CheckRelocateMods.Enabled = True
                Else
                    CheckRelocateMods.Checked = False
                    CheckRelocateMods.Enabled = False
                End If
            End If
        End Sub

        Private Sub ComboBoxCompLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxOodleCompressionLevel.SelectedIndexChanged
            My.Settings.OODLCompressionLevel = ComboBoxOodleCompressionLevel.SelectedIndex
        End Sub

        Private Sub TrackBarDecimalNameLength_Scroll(sender As Object, e As EventArgs) Handles TrackBarDecimalNameLength.Scroll
            My.Settings.DecimalNameMinLength = TrackBarDecimalNameLength.Value
            LabelDecimalNameLength.Text = "Decimal File Name Min Length: " & TrackBarDecimalNameLength.Value
        End Sub

        Private Sub TrackBarHexLength_Scroll(sender As Object, e As EventArgs) Handles TrackBarHexLength.Scroll
            My.Settings.HexViewLength = TrackBarHexLength.Value
            UpdateViewLength()
        End Sub

        Sub UpdateViewLength()
            If TrackBarHexLength.Value > 1024 Then
                LabelHexLength.Text = "Hex/Text View Length: " & CInt(TrackBarHexLength.Value / 1024) & "MB"
            Else
                LabelHexLength.Text = "Hex/Text View Length: " & TrackBarHexLength.Value & "KB"
            End If
        End Sub

        Private Sub ButtonResetStrings_Click(sender As Object, e As EventArgs) Handles ButtonResetStrings.Click
            MainForm.StringReferences = New Dictionary(Of UInt32, String) From {
            {0, "String Not Read"}
        }
        End Sub

        Private Sub ButtonResetFormSize_Click(sender As Object, e As EventArgs) Handles ButtonResetFormSize.Click
            My.Settings.SavedSplitterDistance = 253
            My.Settings.SavedFormSize = New Size(1500, 500)
            MainForm.ApplyFormSettings()
        End Sub

        Private Sub ButtonResetPacs_Click(sender As Object, e As EventArgs) Handles ButtonResetPacs.Click
            MainForm.PacNumbers = New Integer(1024) {}
            MainForm.PacNumbers(0) = -1
        End Sub


End Class