﻿Imports System.IO   'Files

Public Class OptionsMenu

#Region "Form Functions"

    Private Sub OptionsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        LoadSettings()
    End Sub

    Sub LoadSettings()
        TextBoxHome.Text = Path.GetDirectoryName(My.Settings.ExeLocation)
        LoadFileSelectTab()
        LoadOptionsTab()
        LoadAdvancedTab()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        My.Settings.OptionMenuSelectedTab = TabControl1.SelectedIndex
    End Sub

#End Region

#Region "File Select Tab"

    Sub LoadFileSelectTab()
        TextBoxTexConv.Text = My.Settings.TexConvPath
        If Not File.Exists(My.Settings.TexConvPath) Then
            LabelTexConv.ForeColor = Color.Red
        Else
            LabelTexConv.ForeColor = Color.Black
        End If
        TextBoxCrunchExe.Text = My.Settings.CrunchEXELocation
        If Not File.Exists(My.Settings.CrunchEXELocation) Then
            LabelCrunchExe.ForeColor = Color.Red
        Else
            LabelCrunchExe.ForeColor = Color.Black
        End If
        TextBoxRadVideo.Text = My.Settings.RADVideoToolPath
        If Not File.Exists(My.Settings.RADVideoToolPath) Then
            LabelRadVideo.ForeColor = Color.Red
        Else
            LabelRadVideo.ForeColor = Color.Black
        End If
        TextBoxBPEExe.Text = My.Settings.BPEExePath
        If Not File.Exists(My.Settings.BPEExePath) Then
            LabelBPEExe.ForeColor = Color.Red
        Else
            LabelBPEExe.ForeColor = Color.Black
        End If
        TextBoxUnrrbpe.Text = My.Settings.UnrrbpePath
        If Not File.Exists(My.Settings.UnrrbpePath) Then
            LabelUnrrbpe.ForeColor = Color.Red
        Else
            LabelUnrrbpe.ForeColor = Color.Black
        End If
        TextBoxDDSExe.Text = My.Settings.DDSexeLocation
        If Not File.Exists(My.Settings.DDSexeLocation) Then
            LabelDSSExe.ForeColor = Color.Red
        Else
            LabelDSSExe.ForeColor = Color.Black
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

    Private Sub ButtonDownloadDDSexe_Click(sender As Object, e As EventArgs) Handles ButtonDownloadDDSexe.Click
        Process.Start("https://www.getpaint.net/")
    End Sub

    Private Sub ButtonSelectUnrrbpe_Click(sender As Object, e As EventArgs) Handles ButtonSelectUnrrbpe.Click
        ApplicationHandlers.CheckUnrrbpe(True)
        LoadSettings()
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

#Region "Advanced Tab"

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

    Private Sub CheckBoxAutoDecompressCakFiles_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxShowCAkIntermediates.CheckedChanged
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

#End Region

#Region "DLL Tab"

    Sub LoadDLLTab()
        If Not ApplicationHandlers.CheckFontAwesome() Then
            LabelFontAwesome.Text = "FontAwe. Loaded: False"
            LabelFontAwesome.ForeColor = Color.Red
            ButtonSelectFontAwesome.Visible = True
        Else
            LabelFontAwesome.Text = "FontAwesome Loaded: True"
            LabelFontAwesome.ForeColor = Color.Black
            ButtonSelectFontAwesome.Visible = False
        End If
        If Not ApplicationHandlers.CheckIconicZlib() Then
            LabelZlib.Text = "Zlib DLL Loaded: False"
            LabelZlib.ForeColor = Color.Red
            ButtonSelectZlib.Visible = True
        Else
            LabelZlib.Text = "Zlib DLL Loaded: True"
            LabelZlib.ForeColor = Color.Black
            ButtonSelectZlib.Visible = False
        End If
        If Not ApplicationHandlers.CheckOodle6() Then
            LabelOodle_6.Text = "Oodle DLL Loaded: False"
            LabelOodle_6.ForeColor = Color.Red
            ButtonOodle6Select.Visible = True
            ComboBoxOodleCompressionLevel.Enabled = False
        Else
            LabelOodle_6.ForeColor = Color.Black
        End If
        If Not ApplicationHandlers.CheckOodle7() Then
            LabelOodle_7.Text = "Oodle DLL Loaded: False"
            LabelOodle_7.ForeColor = Color.Red
            ButtonOodle7Select.Visible = True
            ComboBoxOodleCompressionLevel.Enabled = False
        Else
            LabelOodle_7.ForeColor = Color.Black
        End If
        If Not ApplicationHandlers.CheckFrosty() Then
            LabelFrosty.Text = "FrustyYukes. Loaded: False"
            LabelFrosty.ForeColor = Color.Red
            ButtonFrostyYukesSelect.Visible = True
        Else
            LabelFrosty.Text = "FrustyYukes Loaded: True"
            LabelFrosty.ForeColor = Color.Black
            ButtonFrostyYukesSelect.Visible = False
        End If
    End Sub

    Private Sub ButtonSelectFontAwesome_Click(sender As Object, e As EventArgs) Handles ButtonSelectFontAwesome.Click
        ApplicationHandlers.CheckFontAwesome(True)
        LoadSettings()
    End Sub

    Private Sub ButtonSelectZlib_Click(sender As Object, e As EventArgs) Handles ButtonSelectZlib.Click
        ApplicationHandlers.CheckIconicZlib(True)
        LoadSettings()
    End Sub

    Private Sub ButtonOodle6Select_Click(sender As Object, e As EventArgs) Handles ButtonOodle6Select.Click
        ApplicationHandlers.CheckOodle6(True)
        LoadSettings()
    End Sub

    Private Sub ButtonOodle7Select_Click(sender As Object, e As EventArgs) Handles ButtonOodle7Select.Click
        ApplicationHandlers.CheckOodle7(True)
        LoadSettings()
    End Sub

    Private Sub ButtonFrostyYukesSelect_Click(sender As Object, e As EventArgs) Handles ButtonFrostyYukesSelect.Click
        ApplicationHandlers.CheckFrosty(True)
        LoadSettings()
    End Sub

    Private Sub ButtonNewtonJsonSelect_Click(sender As Object, e As EventArgs) Handles ButtonNewtonJsonSelect.Click
        ApplicationHandlers.CheckNewtonsoftJson(True)
        LoadSettings()
    End Sub

#End Region

End Class