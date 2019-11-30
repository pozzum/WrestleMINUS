Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If Not e.Button = MouseButtons.Right Then
            Exit Sub
        End If
        'Changing the node should remove the pending save unless it is canceled and then we shouldn't show the strip.
        If SavePending Then
            Exit Sub
        End If
        'so if a save is not pending then we want to change the selected node.
        TreeView1.SelectedNode = e.Node
        Dim NodeTag As ExtendedFileProperties = CType(TreeView1.SelectedNode.Tag, ExtendedFileProperties)
        Dim ParentNodeTag As ExtendedFileProperties = Nothing
        If IsNothing(TreeView1.SelectedNode.Parent) Then
            ParentNodeTag = Nothing
        Else
            ParentNodeTag = CType(TreeView1.SelectedNode.Parent.Tag, ExtendedFileProperties)
        End If
        'REFACTOR - Start All visible = false and create a Count Visible Function
        For i As Integer = 0 To TreeViewContext.Items.Count - 1
            TreeViewContext.Items(i).Tag = False
        Next
        'Hide Menu Options when not needed, and if no options are relevant do not show the context strip
        If NodeTag.FileType = PackageType.Folder Then
            RenameFileToolStripMenuItem.Tag = True
            DeleteFileToolStripMenuItem.Tag = True
            ExtractPartToToolStripMenuItem.Visible = False
            OpenFileLocationToolStripMenuItem.Tag = True
            OpenFileLocationToolStripMenuItem.Text = "Open folder location"
        Else
            If NodeTag.Index > 0 OrElse
                       NodeTag.StoredData.Length > 0 Then
                ExtractToolStripMenuItem.Tag = True
                ExtractPartToToolStripMenuItem.Visible = True
                If PackageInformation.CheckInjectable(NodeTag.FileType) Then
                    InjectBPEToolStripMenuItem.Visible = False
                    InjectZLIBToolStripMenuItem.Visible = False
                    InjectOODLToolStripMenuItem.Visible = False
                    If ParentNodeTag.FileType = PackageType.BPE Then
                        If ApplicationHandlers.CheckBPEExe() Then
                            InjectToolStripMenuItem.Tag = True
                            'InjectBPEToolStripMenuItem.Visible = True
                        End If
                    ElseIf ParentNodeTag.FileType = PackageType.ZLIB Then
                        If ApplicationHandlers.CheckIconicZlib() Then
                            InjectToolStripMenuItem.Tag = True
                            'InjectZLIBToolStripMenuItem.Visible = True
                        End If
                    ElseIf ParentNodeTag.FileType = PackageType.OODL Then
                        If ApplicationHandlers.CheckOodle6() Then
                            InjectToolStripMenuItem.Tag = True
                            'InjectOODLToolStripMenuItem.Visible = True
                        End If
                    Else
                        'We are working with a actual file part
                        InjectToolStripMenuItem.Tag = True
                        InjectUncompressedToolStripMenuItem.Tag = True
                        If PackageInformation.CheckRenameable(ParentNodeTag.FileType) Then
                            RenamePartToolStripMenuItem.Tag = True
                        End If

                        If PackageInformation.CheckDeleteable(ParentNodeTag.FileType) Then
                            If Not IsNothing(ParentNodeTag) Then
                                If TreeView1.SelectedNode.Parent.Nodes.Count > 1 Then
                                    DeletePartToolStripMenuItem.Tag = True
                                End If
                            End If
                        End If
                        If ApplicationHandlers.CheckBPEExe() Then
                            InjectBPEToolStripMenuItem.Visible = True
                        Else
                            InjectBPEToolStripMenuItem.Visible = False
                        End If
                        If ApplicationHandlers.CheckIconicZlib() Then
                            InjectZLIBToolStripMenuItem.Visible = True
                        Else
                            InjectZLIBToolStripMenuItem.Visible = False
                        End If
                        If ApplicationHandlers.CheckOodle6() OrElse ApplicationHandlers.CheckOodle7() Then
                            InjectOODLToolStripMenuItem.Visible = True
                        Else
                            InjectOODLToolStripMenuItem.Visible = False
                        End If
                    End If
                End If
            Else
                If Not NodeTag.FileType = PackageType.CakFolder Then
                    'We are working on a File, not a file part
                    RenameFileToolStripMenuItem.Tag = True
                    DeleteFileToolStripMenuItem.Tag = True
                    OpenFileLocationToolStripMenuItem.Tag = True
                    OpenFileLocationToolStripMenuItem.Text = "Open file location"
                End If
            End If
            'TO DO more add Open With Items Somehow
            'Hex Editor
            If NodeTag.FileType = PackageType.bk2 AndAlso My.Settings.RADVideoToolPath <> "Not Installed" Then
                OpenRADVideoToolStripMenuItem.Tag = True
            ElseIf NodeTag.FileType = PackageType.DDS Then
                OpenImageWithToolStripMenuItem.Tag = True
            End If
        End If
        If TreeView1.SelectedNode.GetNodeCount(False) > 0 Then
            CrawlToolStripMenuItem.Tag = True
            ExtractToolStripMenuItem.Tag = True
            ExtractAllInPlaceToolStripMenuItem.Visible = True
            ExtractAllToToolStripMenuItem.Visible = True
        Else
            ExtractAllInPlaceToolStripMenuItem.Visible = False
            ExtractAllToToolStripMenuItem.Visible = False
        End If
        For i As Integer = 0 To TreeViewContext.Items.Count - 1
            TreeViewContext.Items(i).Visible = TreeViewContext.Items(i).Tag
        Next
        If GeneralTools.CountVisibleToolStrip(TreeViewContext.Items) > 0 Then
            TreeViewContext.Show(TreeView1, New Point(e.X, e.Y))
        End If
    End Sub

#Region "Open Files"

    Private Sub OpenToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OpenRADVideoToolStripMenuItem.Click
        'Currently Only Opens Bink Files
        Dim TronBytes As Byte() = FilePartHandlers.GetFilePartBytes(TreeView1.SelectedNode.Tag)
        Dim filepath As String = Path.GetTempFileName
        File.WriteAllBytes(filepath, TronBytes)
        Process.Start(My.Settings.RADVideoToolPath, filepath)
    End Sub

    Private Sub OpenImageWithToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenImageWithToolStripMenuItem.Click
        If My.Settings.DDSexeLocation = "Not Installed" Then
            ApplicationHandlers.CheckDDSexe(True)
        Else
            'Currently Only Designed for DDS Files
            Dim DDSBytes As Byte() = FilePartHandlers.GetFilePartBytes(TreeView1.SelectedNode.Tag)
            Dim filepath As String = Path.GetTempPath & Guid.NewGuid().ToString() & ".dds"
            File.WriteAllBytes(filepath, DDSBytes)
            Process.Start(My.Settings.DDSexeLocation, filepath)
        End If
    End Sub

    'Possible Additions
    'Model Editor
    'Hex Editor

#End Region

#Region "Extract Options"

    Private Sub ExtractPartToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractPartToToolStripMenuItem.Click
        If FilePartHandlers.ExtractFilePartTo(TreeView1.SelectedNode.Tag) Then
            MessageBox.Show("Extraction Complete")
        Else
            MessageBox.Show("Extraction Failed")
        End If
    End Sub

    Private Sub ExtractAllInPlaceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractAllInPlaceToolStripMenuItem.Click
        If FilePartHandlers.ExtractAllSubFiles(TreeView1.SelectedNode.Tag) Then
            MessageBox.Show("Extraction Complete")
        Else
            MessageBox.Show("Extraction Failed")
        End If
    End Sub

    Private Sub ExtractAllToToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExtractAllToToolStripMenuItem.Click
        Dim SaveExtractAllDialog As SaveFileDialog = New SaveFileDialog With {
            .FileName = "Save Files Here",
            .InitialDirectory = Path.GetDirectoryName(TreeView1.SelectedNode.ToolTipText)}
        If SaveExtractAllDialog.ShowDialog() = DialogResult.OK Then
            If FilePartHandlers.ExtractAllSubFiles(TreeView1.SelectedNode.Tag,
                           (Path.GetDirectoryName(SaveExtractAllDialog.FileName) & Path.DirectorySeparatorChar)) Then
                MessageBox.Show("Extraction Complete")
            Else
                MessageBox.Show("Extraction Failed")
            End If
        End If
    End Sub

#End Region

#Region "Inject Options"

    Private Sub InjectUncompressedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectUncompressedToolStripMenuItem.Click
        If FilePartHandlers.InjectFileIntoFilePart(TreeView1.SelectedNode.Tag) Then
            RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        End If
    End Sub

    Private Sub InjectBPEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectBPEToolStripMenuItem.Click
        If FilePartHandlers.InjectFileIntoFilePart(TreeView1.SelectedNode.Tag, PackageType.BPE) Then
            RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        End If
    End Sub

    Private Sub InjectZLIBToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectZLIBToolStripMenuItem.Click
        If FilePartHandlers.InjectFileIntoFilePart(TreeView1.SelectedNode.Tag, PackageType.ZLIB) Then
            RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        End If
    End Sub

    Private Sub InjectOODLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectOODLToolStripMenuItem.Click
        If FilePartHandlers.InjectFileIntoFilePart(TreeView1.SelectedNode.Tag, PackageType.OODL) Then
            RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        End If
    End Sub

#End Region

#Region "Rename Options"

    Private Sub RenamePartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenamePartToolStripMenuItem.Click
        If FilePartHandlers.RenameFileorFilePart(TreeView1.SelectedNode.Tag) Then
            RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        End If
    End Sub

    Private Sub RenameFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameFileToolStripMenuItem.Click
        Dim WorkingFilePartProperties As ExtendedFileProperties = TreeView1.SelectedNode.Tag
        If FilePartHandlers.RenameFileorFilePart(WorkingFilePartProperties) Then
            RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode, WorkingFilePartProperties)
        End If
    End Sub

#End Region

    'Re-factored From here so far..

#Region "Delete Options"

    Private Sub DeleteFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteFileToolStripMenuItem.Click
        Dim filepath As String = TreeView1.SelectedNode.ToolTipText
        Dim SelectedNode As TreeNode = TreeView1.SelectedNode
        Dim NodeTag As ExtendedFileProperties = CType(TreeView1.SelectedNode.Tag, ExtendedFileProperties)
        If NodeTag.FileType = PackageType.Folder Then
            If Directory.Exists(filepath) Then
                If MessageBox.Show("Would you like to delete folder " & filepath & " ?", "Delete Folder?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    If File_FolderHandlers.DeleteAllItems(filepath) Then SelectedNode.Remove()
                End If
            Else
                MessageBox.Show("Folder " & filepath & " Not Found")
            End If
        Else 'it should be a file
            If File.Exists(filepath) Then
                If MessageBox.Show("Would you like to delete file " & Path.GetFileName(filepath) & " ?", "Delete File?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    If File_FolderHandlers.DeleteSafely(filepath) Then SelectedNode.Remove()
                End If
            Else
                MessageBox.Show("File " & Path.GetFileName(filepath) & " Not Found")
            End If
        End If
    End Sub

    Private Sub DeletePartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeletePartToolStripMenuItem.Click
        Dim ParrentNodeTag As ExtendedFileProperties = CType(TreeView1.SelectedNode.Parent.Tag, ExtendedFileProperties)
        If PackageInformation.CheckInjectable(ParrentNodeTag.FileType) Then 'Hopefully this can expand to all
            If MessageBox.Show("Would you like to delete " & TreeView1.SelectedNode.Text & " ?", "Delete Part", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                FilePartHandlers.DeleteFilesFromParent(TreeView1.SelectedNode.Tag)
                RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
            End If
        Else
            MessageBox.Show("Not Yet Supported")
        End If
    End Sub

#End Region

    Private Sub CrawlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CrawlToolStripMenuItem.Click
        TreeView1.SelectedNode.Nodes.Clear()
        'we update Extended File information inside the tag, so we only have to pass the node to update the information
        FilePartHandlers.GetAllSubItems(TreeView1.SelectedNode.Tag)
        UpdateNodeWithFileInformation(TreeView1.SelectedNode)
        TreeView1.SelectedNode.ExpandAll()
    End Sub

    Private Sub OpenFileLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenFileLocationToolStripMenuItem.Click
        Dim WorkingFilePartProperties As ExtendedFileProperties = TreeView1.SelectedNode.Tag
        If File.Exists(WorkingFilePartProperties.FullFilePath) Then
            Process.Start("explorer.exe", "/select," & WorkingFilePartProperties.FullFilePath)
        ElseIf Directory.Exists(WorkingFilePartProperties.FullFilePath) Then
            Process.Start("explorer.exe", "/select," & WorkingFilePartProperties.FullFilePath)
        End If
    End Sub

End Class
