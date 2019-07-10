Imports System.IO   'Files
Public Class OptionsMenu
    Private Sub OptionsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        LoadSettings()
    End Sub
    Sub LoadSettings()
        TextBoxHome.Text = Path.GetDirectoryName(My.Settings.ExeLocation)
        TextBoxTexConv.Text = My.Settings.TexConvPath
        If Not File.Exists(My.Settings.TexConvPath) Then
            LabelTexConv.ForeColor = Color.Red
        Else
            LabelTexConv.ForeColor = Color.Black
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
        If Not MainForm.CheckIconicZlib() Then
            LabelZlib.Text = "Zlib DLL Loaded: False"
            LabelZlib.ForeColor = Color.Red
            ButtonSelectZlib.Visible = True
        Else
            LabelZlib.ForeColor = Color.Black
        End If
        If Not MainForm.CheckOodle() Then
            LabelOodle.Text = "Oodle DLL Loaded: False"
            LabelOodle.ForeColor = Color.Red
            ButtonOodleSelect.Visible = True
            ComboBoxCompLevel.Enabled = False
        Else
            LabelOodle.ForeColor = Color.Black
        End If
        ComboBoxCompLevel.Items.AddRange(System.Enum.GetNames(GetType(MainForm.OodleCompressionLevel)))
        ComboBoxCompLevel.SelectedIndex = My.Settings.OODLCompressionLevel
        CheckBoxLoadHome.Checked = My.Settings.LoadHomeOnLaunch
        CheckBoxBackup.Checked = My.Settings.BackupInjections
        CheckBoxDeleteTempBMP.Checked = My.Settings.DeleteTempBMP
        CheckBoxTreeNodeIcons.Checked = My.Settings.UseTreeIcons
        CheckBoxDetailedFileNames.Checked = My.Settings.UseDetailedFileNames
        CheckBoxExtractAllinPlace.Checked = My.Settings.DecompresstoFolder
        'extract all can only extract to folders with detailed file names
        If CheckBoxDetailedFileNames.Checked Then
            CheckBoxExtractAllinPlace.Enabled = True
        Else
            CheckBoxExtractAllinPlace.Checked = True
            CheckBoxExtractAllinPlace.Enabled = False
        End If
        MessageBox.Show(My.Settings.DecimalNameMinLength)
        TrackBarDecimalNameLength.Value = My.Settings.DecimalNameMinLength
        LabelDecimalNameLength.Text = "Decimal File Name Min Length: " & TrackBarDecimalNameLength.Value
        TrackBarHexLength.Value = My.Settings.HexViewLength
        CheckBoxOODLBypass.Checked = My.Settings.BypassOODLWarn
        updateviewlength()
    End Sub
    Private Sub ButtonSelectHome_Click(sender As Object, e As EventArgs) Handles ButtonSelectHome.Click
        MainForm.SelectHomeDirectory()
        LoadSettings()
    End Sub
    Private Sub ButtonSelectTexConv_Click(sender As Object, e As EventArgs) Handles ButtonSelectTexConv.Click
        MainForm.GetTexConvExe(True)
        LoadSettings()
    End Sub
    Private Sub ButtonDownloadRadVideo_Click(sender As Object, e As EventArgs) Handles ButtonDownloadRadVideo.Click
        Process.Start("http://www.radgametools.com/bnkdown.htm")
    End Sub
    Private Sub ButtonSelectRadVideo_Click(sender As Object, e As EventArgs) Handles ButtonSelectRadVideo.Click
        MainForm.GetRadVideo(True)
        LoadSettings()
    End Sub
    'I am banned from this site so I can't actually get it..
    Private Sub ButtonDownloadBPEExe_Click(sender As Object, e As EventArgs) Handles ButtonDownloadBPEExe.Click
        Process.Start("https://www.tapatalk.com/groups/legendsofmodding/bpe-compression-tool-t3741.html#p22164")
    End Sub
    Private Sub ButtonSelectBPEExe_Click(sender As Object, e As EventArgs) Handles ButtonSelectBPEExe.Click
        MainForm.CheckBPEExe(True)
        LoadSettings()
    End Sub
    Private Sub ButtonDownloadUnrrbpe_Click(sender As Object, e As EventArgs) Handles ButtonDownloadUnrrbpe.Click
        Process.Start("http://asmodean.reverse.net/pages/unrrbpe.html")
    End Sub
    Private Sub ButtonSelectUnrrbpe_Click(sender As Object, e As EventArgs) Handles ButtonSelectUnrrbpe.Click
        MainForm.CheckUnrrbpe(True)
        LoadSettings()
    End Sub
    Private Sub ButtonSelectDDSexe_Click(sender As Object, e As EventArgs) Handles ButtonSelectDDSexe.Click
        MainForm.CheckDDSexe(True)
        LoadSettings()
    End Sub
    Private Sub ButtonSelectZlib_Click(sender As Object, e As EventArgs) Handles ButtonSelectZlib.Click
        MainForm.CheckIconicZlib(True)
    End Sub
    Private Sub ButtonOodleSelect_Click(sender As Object, e As EventArgs) Handles ButtonOodleSelect.Click
        MainForm.CheckOodle(True)
    End Sub
    Private Sub ComboBoxCompLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCompLevel.SelectedIndexChanged
        My.Settings.OODLCompressionLevel = ComboBoxCompLevel.SelectedIndex
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
    Private Sub TrackBarDecimalNameLength_Scroll(sender As Object, e As EventArgs) Handles TrackBarDecimalNameLength.Scroll
        My.Settings.DecimalNameMinLength = TrackBarDecimalNameLength.Value
        LabelDecimalNameLength.Text = "Decimal File Name Min Length: " & TrackBarDecimalNameLength.Value
    End Sub
    Private Sub TrackBarHexLength_Scroll(sender As Object, e As EventArgs) Handles TrackBarHexLength.Scroll
        My.Settings.HexViewLength = TrackBarHexLength.Value
        updateviewlength()
    End Sub
    Sub updateviewlength()
        If TrackBarHexLength.Value > 1024 Then
            LabelHexLength.Text = "Hex/Text View Length: " & CInt(TrackBarHexLength.Value / 1024) & "MB"
        Else
            LabelHexLength.Text = "Hex/Text View Length: " & TrackBarHexLength.Value & "KB"
        End If
    End Sub
    Private Sub ButtonResetStrings_Click(sender As Object, e As EventArgs) Handles ButtonResetStrings.Click
        MainForm.StringReferences = New String(&HFFFFF) {}
        MainForm.StringReferences(0) = "String Not Read"
    End Sub
    Private Sub ButtonResetPacs_Click(sender As Object, e As EventArgs) Handles ButtonResetPacs.Click
        MainForm.PacNumbers = New Integer(1024) {}
        MainForm.PacNumbers(0) = -1
    End Sub
End Class