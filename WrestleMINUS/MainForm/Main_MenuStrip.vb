Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
#Region "File Sub Menu"

    Private Sub LoadHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadHomeToolStripMenuItem.Click
        LoadHome()
    End Sub

    Private Sub SelectNewHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectNewHomeToolStripMenuItem.Click
        ApplicationHandlers.CheckGameExeFolder(True)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OpenFileOrFolderDialog As New OpenFileDialog With {
            .CheckFileExists = False,
            .FileName = "Select Items",
            .Multiselect = True,
            .ValidateNames = False}
        If OpenFileOrFolderDialog.ShowDialog = DialogResult.OK Then
            Dim DialogSelection As String() = OpenFileOrFolderDialog.FileNames
            For i As Integer = 0 To DialogSelection.Count - 1
                DialogSelection(i) = DialogSelection(i).Replace("Select Items", "")
            Next '"Select Items"
            SelectedFiles = DialogSelection
            LoadInitalFilesToTree()
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        OptionsMenu.Show()
    End Sub

#End Region

#Region "Tool Tool-strip"

    Private Sub BPEBatchCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BPEBatchCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Multiselect = True,
            .FileName = "File(s) to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim SelectedFiles As String() = CompressOpenFileDialog.FileNames
            For i As Integer = 0 To SelectedFiles.Count - 1
                PackUnpack.CompressBPEToFile(SelectedFiles(i))
            Next
            MessageBox.Show("Compression Complete")
        End If
    End Sub

    Private Sub BPESingleCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BPESingleCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "File to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim CompressSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
                .InitialDirectory = Path.GetDirectoryName(CompressOpenFileDialog.FileName),
                .FileName = Path.GetFileNameWithoutExtension(CompressOpenFileDialog.FileName) & ".bpe",
                .Title = "Save File Location"}
            If CompressSaveFileDialog.ShowDialog() = DialogResult.OK Then
                If PackUnpack.CompressBPEToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
                    MessageBox.Show("Compression Complete")
                End If
            End If
        End If
    End Sub

    Private Sub ZLIBBatchCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZLIBBatchCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Multiselect = True,
            .FileName = "File(s) to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim SelectedFiles As String() = CompressOpenFileDialog.FileNames
            For i As Integer = 0 To SelectedFiles.Count - 1
                PackUnpack.CompressZLIBToFile(SelectedFiles(i))
            Next
            MessageBox.Show("Compression Complete")
        End If
    End Sub

    Private Sub ZLIBSingleCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZLIBSingleCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "File to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim CompressSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
                .InitialDirectory = Path.GetDirectoryName(CompressOpenFileDialog.FileName),
                .FileName = Path.GetFileNameWithoutExtension(CompressOpenFileDialog.FileName) & ".zlib",
                .Title = "Save File Location"}
            If CompressSaveFileDialog.ShowDialog() = DialogResult.OK Then
                If PackUnpack.CompressZLIBToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
                    MessageBox.Show("Compression Complete")
                End If
            End If
        End If
    End Sub

    Private Sub OODLBatchCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OODLBatchCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .Multiselect = True,
            .FileName = "File(s) to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim SelectedFiles As String() = CompressOpenFileDialog.FileNames
            For i As Integer = 0 To SelectedFiles.Count - 1
                PackUnpack.CompressOODL_6ToFile(SelectedFiles(i))
            Next
            MessageBox.Show("Compression Complete")
        End If
    End Sub

    Private Sub OODLSingleCompressToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OODLSingleCompressToolStripMenuItem.Click
        Dim CompressOpenFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "File to Compress"}
        If CompressOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Dim CompressSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
                .InitialDirectory = Path.GetDirectoryName(CompressOpenFileDialog.FileName),
                .FileName = Path.GetFileNameWithoutExtension(CompressOpenFileDialog.FileName) & ".oodl",
                .Title = "Save File Location"}
            If CompressSaveFileDialog.ShowDialog() = DialogResult.OK Then
                If PackUnpack.CompressOODL_6ToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
                    MessageBox.Show("Compression Complete")
                End If
            End If
        End If
    End Sub

    Private Sub GenerateFileNameHashToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GenerateFileNameHashToolStripMenuItem.Click
        Dim TextDialogInstance As New TextDialogPrompt With {
                .ContainerBeingEdited = PackageType.bin,
                .OldFileName = "hash"}
        TextDialogInstance.ShowDialog()
        If TextDialogInstance.Result = DialogResult.OK Then
            If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                Dim NewFileName As String = TextDialogInstance.EditedFileName
                Dim HashedName As String = HashGenerator.GetMd5Hash(NewFileName, True)
                MessageBox.Show(HashedName, "File Hash")
            End If
        End If
        TextDialogInstance.Dispose()
    End Sub

    Private Sub RebuildDefCurrentHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebuildDefCurrentHomeToolStripMenuItem.Click
        DefBuilder.RebuildDef(My.Settings.ExeLocation, True, My.Settings.AppendDefFileRebuild, My.Settings.DisableModPref, My.Settings.RelocateModFolderMods)
    End Sub

    Private Sub RebuildDefSelectFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RebuildDefSelectFolderToolStripMenuItem.Click
        Dim TempFileDialog As OpenFileDialog = New OpenFileDialog With {
            .FileName = "WWE2KXX.exe",
            .Title = "Select WWE exe directory"}
        If Directory.Exists("C:\Steam\steamapps\common\") Then
            TempFileDialog.InitialDirectory = "C:\Steam\steamapps\common\"
        End If
        TempFileDialog.ShowDialog()
        If File.Exists(TempFileDialog.FileName) AndAlso
            Path.GetExtension(TempFileDialog.FileName).ToLower = ".exe" Then
            DefBuilder.RebuildDef(TempFileDialog.FileName, True, My.Settings.AppendDefFileRebuild, My.Settings.DisableModPref, My.Settings.RelocateModFolderMods)
        Else
            MessageBox.Show("File Selection Failure")
        End If
    End Sub

#End Region

#Region "HelpToolStrip"

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Process.Start("https://pozzum.github.io/WrestleMINUS/")
    End Sub

    Private Sub SupportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupportToolStripMenuItem.Click
        Process.Start("https://smacktalks.org/forums/topic/70048-wrestleminus/")
    End Sub

    Private Sub GitHubIssuesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GitHubIssuesToolStripMenuItem.Click
        Process.Start("https://github.com/pozzum/WrestleMINUS/issues")
    End Sub

#End Region
End Class
