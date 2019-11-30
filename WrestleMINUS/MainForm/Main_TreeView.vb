Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    Function GenerateNodeFromFile(SentFileProperties As ExtendedFileProperties, Optional ExitingNode As TreeNode = Nothing) As TreeNode
        Dim TempNode As TreeNode = New TreeNode(SentFileProperties.Name) With {
            .ImageIndex = PackageInformation.GetImageIndex(SentFileProperties.FileType),
            .SelectedImageIndex = .ImageIndex,
            .Tag = SentFileProperties,
            .ToolTipText = SentFileProperties.FullFilePath}
        If Not IsNothing(SentFileProperties.SubFiles) Then
            'if the file has sub items we want to send the node with any sub nodes already populated to reduce loops
            For i As Integer = 0 To SentFileProperties.SubFiles.Count - 1
                TempNode.Nodes.Add(GenerateNodeFromFile(SentFileProperties.SubFiles(i)))
            Next
        End If
        Return TempNode
    End Function

    Sub UpdateNodeWithFileInformation(ExitingNode As TreeNode)
        If IsNothing(ExitingNode.Parent) Then
            Dim StoredIndex As Integer = ExitingNode.Index
            TreeView1.Nodes.Insert(ExitingNode.Index + 1, GenerateNodeFromFile(ExitingNode.Tag))
            ExitingNode.Remove()
            TreeView1.SelectedNode = TreeView1.Nodes(StoredIndex)
        Else
            TreeView1.SelectedNode.Parent.Nodes.Insert(ExitingNode.Index + 1, GenerateNodeFromFile(ExitingNode.Tag))
            ExitingNode.Remove()
        End If
    End Sub

    Function RebuildNodeFromUpdatedFiles(EditedFile As TreeNode, Optional NewExtendedFileProperties As ExtendedFileProperties = Nothing)
        Dim SentNodeTag As ExtendedFileProperties = CType(EditedFile.Tag, ExtendedFileProperties)
        'First we want to crawl up to the File Base Node
        Do While EditedFile.Parent IsNot Nothing
            If SentNodeTag.Index = 0 AndAlso
                    SentNodeTag.StoredData.Length = 0 Then
                'we have the right node and node tag to exit the Do While
                Exit Do
            Else
                EditedFile = EditedFile.Parent
                SentNodeTag = CType(EditedFile.Tag, ExtendedFileProperties)
            End If
        Loop
        'MessageBox.Show(EditedFile.Text)
        'Next we want to check if that file has a parent (folder)
        Dim ParentNode As TreeNode = EditedFile.Parent
        Dim ParentIndex As Integer = EditedFile.Index
        'MessageBox.Show(ParentIndex)
        'We need to double check if the new extended file properties resulted in a new folder or file name.
        Dim NewFI As FileInfo
        If Not IsNothing(NewExtendedFileProperties) AndAlso
            Not SentNodeTag.FullFilePath = NewExtendedFileProperties.FullFilePath Then
            NewFI = New FileInfo(NewExtendedFileProperties.FullFilePath)
        Else
            NewFI = New FileInfo(SentNodeTag.FullFilePath)
        End If
        'Create a new File Properties

        Dim UpdatedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                    .Name = NewFI.Name,
                    .FullFilePath = NewFI.FullName,
                    .FileType = PackageType.Unchecked,
                    .Index = 0,
                    .length = FileLen(NewFI.FullName),
                    .StoredData = New Byte() {}}
        'Now We want to now delete the odl node and make a new one
        EditedFile.Remove()
        'we want to get the sub files to be consistent
        PackageInformation.GetFileParts(UpdatedFileProperties)
        'the Generate node function and put it where the old one was
        Dim NewNode As TreeNode = GenerateNodeFromFile(UpdatedFileProperties)
        If IsNothing(ParentNode) Then
            TreeView1.Nodes.Insert(ParentIndex, NewNode)
        Else
            ParentNode.Nodes.Insert(ParentIndex, NewNode)
        End If
        TreeView1.SelectedNode = NewNode
        'make information loaded as false
        InformationLoaded = False
        Return True
    End Function

    Dim ProgressBarFont As Font = New Font("Arial", 8.25, FontStyle.Regular)

    'TO DO Add this to more locations
    Sub UpdateProgress()
        If ProgressBar1.Value < ProgressBar1.Maximum Then
            ProgressBar1.Value += 1
            Dim Percent As Integer = CInt((ProgressBar1.Value / ProgressBar1.Maximum) * 100)
            ProgressBar1.CreateGraphics().DrawString(Percent.ToString() + "%",
                                                     ProgressBarFont,
                                                     Brushes.Black,
                                                     New PointF(ProgressBar1.Width / 2 - 10,
                                                                ProgressBar1.Height / 2 - 7))
        End If
    End Sub

    Sub LoadInitalFilesToTree()
        InformationLoaded = False
        TreeView1.Nodes.Clear()
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = SelectedFiles.Length
        If SelectedFiles.Length = 1 Then
            CurrentViewText = "Current View: " & Path.GetFileName(SelectedFiles(0))
        Else
            CurrentViewText = "Current View: Multiple Files"
        End If
        CurrentViewToolStripMenuItem.Text = CurrentViewText
        For i As Integer = 0 To SelectedFiles.Length - 1
            If Not SelectedFiles(i) = "" Then
                If File.Exists(SelectedFiles(i)) Then
                    Dim NewFI As FileInfo = New FileInfo(SelectedFiles(i))
                    'Dim TempNode As TreeNode = TreeView1.Nodes.Add(NewFI.Name)
                    Dim InitalFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = NewFI.Name,
                        .FullFilePath = NewFI.FullName,
                        .FileType = PackageType.Unchecked,
                        .Index = 0,
                        .length = FileLen(NewFI.FullName),
                        .StoredData = New Byte() {}}
                    PackageInformation.GetFileParts(InitalFileProperties)
                    TreeView1.Nodes.Add(GenerateNodeFromFile(InitalFileProperties))
                    ProgressBar1.Value += 1
                ElseIf Directory.Exists(SelectedFiles(i)) Then
                    Dim TempDI As DirectoryInfo = New DirectoryInfo(SelectedFiles(i))
                    Dim InitalFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = TempDI.Name,
                        .FullFilePath = TempDI.FullName,
                        .FileType = PackageType.Folder,
                        .Index = 0,
                        .length = 0,
                        .StoredData = New Byte() {}}
                    PackageInformation.GetFileParts(InitalFileProperties)
                    TreeView1.Nodes.Add(GenerateNodeFromFile(InitalFileProperties))
                    ProgressBar1.Value += 1
                End If
            End If
        Next
    End Sub

    Sub LoadHome()
        InformationLoaded = False
        If Not My.Settings.ExeLocation = "" Then
            TreeView1.Nodes.Clear()
            ProgressBar1.Value = 0
            Dim HomeDirectory As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar
            CurrentViewText = "Current View: " & HomeDirectory
            CurrentViewToolStripMenuItem.Text = CurrentViewText
            Dim HomeDI As DirectoryInfo = New DirectoryInfo(HomeDirectory)
            Dim InitalFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = HomeDI.Name,
                        .FullFilePath = HomeDI.FullName,
                        .FileType = PackageType.Folder,
                        .Index = 0,
                        .length = 0,
                        .StoredData = New Byte() {}}
            ProgressBar1.Maximum = Directory.GetFiles(HomeDirectory, "*.*", SearchOption.AllDirectories).Length +
                                    Directory.GetDirectories(HomeDirectory, "**", SearchOption.AllDirectories).Length
            PackageInformation.GetFileParts(InitalFileProperties, False, True)
            TreeView1.Nodes.Add(GenerateNodeFromFile(InitalFileProperties))
        Else
            If MessageBox.Show("No Home Directory Selected." & vbNewLine &
                             "Select Home Now?", "Select Home?", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                ApplicationHandlers.CheckGameExeFolder(True)
            End If
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        Dim NodeFileProperties As ExtendedFileProperties = CType(e.Node.Tag, ExtendedFileProperties)
        If File.Exists(NodeFileProperties.FullFilePath) Then
            If PackageInformation.CheckExpandable(NodeFileProperties.FileType) Then
                If e.Node.Nodes.Count = 0 Then
                    'we want to pass the actual tag if we are editing it like Get file parts does.
                    PackageInformation.GetFileParts(NodeFileProperties)
                    UpdateNodeWithFileInformation(e.Node)
                End If
            End If
            Dim PagesToLoad As List(Of TabPage) = GetTabTypes(NodeFileProperties.FileType)
            If HideTabs(PagesToLoad) = DialogResult.OK Then
                ReadNode = e.Node
                If My.Settings.ShowSelectedNode Then
                    CurrentViewToolStripMenuItem.Text = CurrentViewText & vbNewLine & "Current Selected Node: " & ReadNode.Text
                End If
                HexViewFileName.Text = TreeView1.SelectedNode.Text
                AddHexText(TreeView1.SelectedNode)
                TextViewFileName.Text = TreeView1.SelectedNode.Text
                AddText(TreeView1.SelectedNode)
                If Not PagesToLoad Is Nothing Then
                    LoadTabs(PagesToLoad)
                End If
            Else
                TreeView1.SelectedNode = ReadNode
            End If
        End If
    End Sub

End Class
