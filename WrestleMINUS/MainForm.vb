Imports System.IO   'Files
Imports System.Text 'Binary Formatter
Imports FontAwesome.Sharp
Imports Newtonsoft.Json

Public Class MainForm

    Friend Shared StringReferences() As String
    Friend Shared StringRead As Boolean = False
    Friend Shared PacNumbers() As Integer
    Friend Shared PacsRead As Boolean = False
    Dim SelectedFiles() As String

    'Injection Properties used across multiple forms
    Friend Shared SavePending As Boolean = False

    Friend Shared ReadNode As TreeNode
    Friend Shared OldValue
    Public Shared InformationLoaded As Boolean = False
    Shared CurrentViewText As String = ""

#Region "Main Form Loading Functions"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        SettingsHandlers.SettingsCheck()
        OnlineVersion.CheckUpdate()
        LoadFontAwesomeIcons()
        FillCompressionMenu()
        ApplyFormSettings()
        ApplyCurrentViewOption()
        HideTabs(Nothing)
        CreatedImages = New List(Of String)
    End Sub

    Private Sub MainForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If CheckCommands() Then
            LoadInitalFilesToTree()
        ElseIf My.Settings.LoadHomeOnLaunch Then
            LoadHome()
        End If
    End Sub

    Private Sub MainForm_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter,
                                                                                Hex_Selected.DragEnter,
                                                                                Text_Selected.DragEnter
        If (e.Data.GetDataPresent(DataFormats.FileDrop)) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop,
                                                                                Hex_Selected.DragDrop,
                                                                                Text_Selected.DragDrop
        Dim s() As String = e.Data.GetData("FileDrop", False)
        SelectedFiles = s
        LoadInitalFilesToTree()
        'Dim i As Integer
        'For i = 0 To s.Length - 1
        'ListBox1.Items.Add(s(i))
        'Next i
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If My.Settings.DeleteTempBMP Then
            DeleteTempImages()
        End If
        'Close all tabs so save check is in just one place.
        If HideTabs(Nothing) = DialogResult.Cancel Then
            e.Cancel = True
        End If
        'Separating command out to allow for error handling to exit closing form command
        If SettingsHandlers.SaveSettingsFiles() = DialogResult.Cancel Then
            e.Cancel = True
        End If
    End Sub

    Shared Function CheckCommands() As Boolean
        Dim args As String() = Environment.GetCommandLineArgs()
        Dim parameters As Boolean = False
        If args.Length > 1 Then
            parameters = True
            MainForm.SelectedFiles = New String(args.Length - 2) {}
            For i As Integer = 1 To args.Length - 1
                MainForm.SelectedFiles(i - 1) = args(i)
            Next
        End If
        Return parameters
    End Function

    Dim MuscleViewStartupRemoved As Boolean = False

    Function HideTabs(ExcludedTabs As List(Of TabPage)) As DialogResult
        If IsNothing(ExcludedTabs) Then
            ExcludedTabs = New List(Of TabPage)
        End If
        For Each TempTabPage As TabPage In TabControl1.TabPages
            Select Case TempTabPage.Name
                Case HexView.Name
                    'Never Hide
                Case TextView.Name
                    'Never Hide
                Case MuscleView.Name
                    'this is not automatically removed except on startup
                    If Not MuscleViewStartupRemoved Then
                        TabControl1.TabPages.Remove(TempTabPage)
                        MuscleViewStartupRemoved = True
                    End If
                Case ExcludedTabs.Contains(TempTabPage)
                    'MessageBox.Show(TabControl1.SelectedTab.Text)
                    'MessageBox.Show(ExcludedTab.Text)
                    'Excluded
                    If TabControl1.SelectedTab.Text = TempTabPage.Text Then
                        'MessageBox.Show("Rebuild Table")
                        'Here we should be able to reset the tab.
                        ReadNode = TreeView1.SelectedNode
                        If My.Settings.ShowSelectedNode Then
                            CurrentViewToolStripMenuItem.Text = CurrentViewText & vbNewLine & "Current Selected Node: " & ReadNode.Text
                        End If
                        FillTabDataGrid(TempTabPage)
                    End If
                Case Else
                    'Here we will add the save pending check and file injection
                    If SavePending Then
                        Dim Result As Integer = MessageBox.Show("File save is pending, would you like to save?", "Save Pending", MessageBoxButtons.YesNoCancel)
                        If Result = DialogResult.Cancel Then
                            'This exits the command so we don't hide any tabs, and returns a cancel to the form closing command.
                            Return DialogResult.Cancel
                        ElseIf Result = DialogResult.Yes Then
                            Dim InjectedByte As Byte() = New Byte() {}
                            Select Case CType(ReadNode.Tag, ExtendedFileProperties).FileType
                                Case PackageType.StringFile
                                    InjectedByte = BuildStringFile()
                                Case PackageType.ArenaInfo
                                    InjectedByte = BuildMiscFile()
                                Case PackageType.ShowInfo
                                    InjectedByte = BuildShowFile()
                                Case PackageType.CostumeFile
                                    InjectedByte = BuildAttireFile()
                                Case PackageType.TitleFile
                                    InjectedByte = BuildTitleFile()
                                Case PackageType.VMUM
                                    InjectedByte = BuildAssetArrayFile()
                                Case PackageType.TitleFile
                                    InjectedByte = BuildTitleFile()
                                Case PackageType.SoundReference
                                    InjectedByte = BuildSoundRefFile()
                                Case PackageType.LSD
                                    InjectedByte = BuildMenuItemFile()
                                Case PackageType.WeaponPosition
                                    InjectedByte = BuildWeaponPositionFile()
                            End Select
                            FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, InjectedByte)
                        Else 'Dialog Result No Save Canceled
                            SaveFileNoLongerPending()
                        End If
                    End If
                    TabControl1.TabPages.Remove(TempTabPage)
            End Select
        Next
        Return DialogResult.OK
    End Function

    Sub LoadFontAwesomeIcons()
        If SettingsHandlers.CheckFontAwesome() Then
            LoadHomeToolStripMenuItem.Image = IconChar.Home.ToBitmap(16, Color.Black)
            OpenToolStripMenuItem.Image = IconChar.FolderOpen.ToBitmap(16, Color.Black)
            ExitToolStripMenuItem.Image = IconChar.WindowClose.ToBitmap(16, Color.Black)
            BPECompressionToolStripMenuItem.Image = IconChar.CompressArrowsAlt.ToBitmap(16, Color.Black)
            ZLIBCompressionToolStripMenuItem.Image = IconChar.CompressArrowsAlt.ToBitmap(16, Color.Black)
            OODLCompressionToolStripMenuItem.Image = IconChar.CompressArrowsAlt.ToBitmap(16, Color.Black)
            RebuildDefFileToolStripMenuItem.Image = IconChar.Wrench.ToBitmap(16, Color.Black)
            'https://fontawesome.com/cheatsheet?from=io
        End If
    End Sub

    Shared Sub LoadIcons()
        If My.Settings.UseTreeIcons Then
            MainForm.TreeView1.ImageList = MainForm.ImageList1
        Else
            MainForm.TreeView1.ImageList = Nothing
        End If
    End Sub

    Sub FillCompressionMenu()
        'Tool Menu Compress in place
        If PackUnpack.CheckBPEExe() Then
            BPECompressionToolStripMenuItem.Visible = True
        Else
            BPECompressionToolStripMenuItem.Visible = False
        End If
        If PackUnpack.CheckIconicZlib() Then
            ZLIBCompressionToolStripMenuItem.Visible = True
        Else
            ZLIBCompressionToolStripMenuItem.Visible = False
        End If
        If PackUnpack.CheckOodle() Then
            OODLCompressionToolStripMenuItem.Visible = True
        Else
            OODLCompressionToolStripMenuItem.Visible = False
        End If
        My.Settings.Save()
    End Sub

    Sub ApplyFormSettings()
        MyBase.Size = My.Settings.SavedFormSize
        SplitFileMenuContainer.SplitterDistance = My.Settings.SavedSplitterDistance
        HexViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        TextViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
        ShowViewType.SelectedIndex = My.Settings.ShowModeIndex
        If Not StringReferences(0) = "String Not Read" Then
            StringRead = True
        End If
        StringLoadedShowMenuItem.Text = "String Loaded: " & StringRead.ToString
        StringLoadedAttireMenuItem.Text = "String Loaded: " & StringRead.ToString
        StringLoadedTitleMenuItem.Text = "String Loaded: " & StringRead.ToString
        StringLoadedCAEMenuItem.Text = "String Loaded: " & StringRead.ToString
        If Not PacNumbers(0) = -1 Then
            PacsRead = True
        End If
        PacsLoadedAttireMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
        PacsLoadedTitleMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
        PacsLoadedCAEMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
    End Sub

    Shared Sub ApplyCurrentViewOption()
        If My.Settings.ShowSelectedNode Then
            MainForm.CurrentViewToolStripMenuItem.Height = 35
            If IsNothing(ReadNode) Then
                MainForm.CurrentViewToolStripMenuItem.Text = CurrentViewText
            Else
                MainForm.CurrentViewToolStripMenuItem.Text = CurrentViewText & vbNewLine & "Current Selected Node: " & MainForm.ReadNode.Text
            End If
        Else
            MainForm.CurrentViewToolStripMenuItem.Height = 20
            MainForm.CurrentViewToolStripMenuItem.Text = CurrentViewText
        End If
    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitFileMenuContainer.SplitterMoved
        CurrentViewToolStripMenuItem.Width = SplitFileMenuContainer.SplitterDistance - 10
        If Not SplitFileMenuContainer.SplitterDistance = 253 Then
            My.Settings.SavedSplitterDistance = SplitFileMenuContainer.SplitterDistance
        End If
    End Sub

    Private Sub MainForm_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        'If Not MyBase.Size.Height = 500 Then
        '    If Not MyBase.Size.Width = 1500 Then
        My.Settings.SavedFormSize = MyBase.Size
        '    End If
        'End If
    End Sub

#End Region

#Region "Menu Strip"

#Region "File Sub Menu"

    Private Sub LoadHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadHomeToolStripMenuItem.Click
        LoadHome()
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
                PackUnpack.CompressOODLToFile(SelectedFiles(i))
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
                If PackUnpack.CompressOODLToFile(CompressOpenFileDialog.FileName(), CompressSaveFileDialog.FileName()) Then
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

#End Region

    'TO DO Add UpdateProgress to more functions, Flesh out with parameters

#Region "TreeView Population"

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
                SettingsHandlers.SelectHomeDirectory()
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

#End Region

#Region "Tab Controls"

    'moving functions from on tree view to on tab select to reduce load times during tree movement on keyboard
    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl1.Selecting
        If e.TabPage.Name = StringView.Name Then
            'separating this out fixes a Load string issue.
            FillStringView(ReadNode)
        Else
            If InformationLoaded = False Then
                InformationLoaded = True
                FillTabDataGrid(e.TabPage)
            End If
        End If
    End Sub

    Private Sub FillTabDataGrid(SelectedTab As TabPage)
        Select Case SelectedTab.Name
            Case MiscView.Name
                FillMiscView(ReadNode)
            Case ShowView.Name
                FillShowView(ReadNode)
            Case NIBJView.Name
                FillNIBJView(ReadNode)
            Case PictureView.Name
                FillPictureView(ReadNode)
            Case ObjectView.Name
                FillObjectViews(ReadNode)
            Case AttireView.Name
                FillAttireView(ReadNode)
            Case MuscleView.Name
                FillMuscleView(ReadNode)
            Case MaskView.Name
                FillMaskView(ReadNode)
            Case ObjArrayView.Name
                FillObjectArrayView(ReadNode)
            Case AssetView.Name
                FillAssetFileView(ReadNode)
            Case TitleView.Name
                FillTitleFileView(ReadNode)
            Case SoundView.Name
                FillSoundRefFileView(ReadNode)
            Case MenuItemView.Name
                FillMenuItemView(ReadNode)
            Case AnimationView.Name
                FillAnimationView(ReadNode)
                FillPof0View(ReadNode)
            Case Pof0View.Name
                FillPof0View(ReadNode)
                InformationLoaded = False
            Case WeaponPositionView.Name
                FillWeaponPositionView(ReadNode)
        End Select
    End Sub

    Function GetTabTypes(SelectedType As PackageType) As List(Of TabPage)
        Dim ReturnedList As List(Of TabPage) = New List(Of TabPage)
        Select Case SelectedType
            Case PackageType.StringFile
                ReturnedList.Add(StringView)
            Case PackageType.ArenaInfo
                ReturnedList.Add(MiscView)
            Case PackageType.ShowInfo
                ReturnedList.Add(ShowView)
            Case PackageType.NIBJ
                ReturnedList.Add(NIBJView)
            Case PackageType.DDS
                ReturnedList.Add(PictureView)
            Case PackageType.YOBJ
                ReturnedList.Add(ObjectView)
                ReturnedList.Add(Pof0View)
            Case PackageType.CostumeFile
                ReturnedList.Add(AttireView)
            Case PackageType.MaskFile
                ReturnedList.Add(MaskView)
            Case PackageType.MuscleFile
                ReturnedList.Add(MuscleView)
            Case PackageType.YOBJArray
                ReturnedList.Add(ObjArrayView)
            Case PackageType.VMUM
                ReturnedList.Add(AssetView)
            Case PackageType.TitleFile
                ReturnedList.Add(TitleView)
            Case PackageType.SoundReference
                ReturnedList.Add(SoundView)
            Case PackageType.LSD
                ReturnedList.Add(MenuItemView)
            Case PackageType.YANM
                ReturnedList.Add(AnimationView)
                ReturnedList.Add(Pof0View)
            Case PackageType.WeaponPosition
                ReturnedList.Add(WeaponPositionView)
            Case Else
                'Nothing
        End Select
        Return ReturnedList
    End Function

    Sub LoadTabs(NewTabList As List(Of TabPage))
        For Each TempNewTab As TabPage In NewTabList
            If Not TabControl1.TabPages.Contains(TempNewTab) Then
                TabControl1.TabPages.Add(TempNewTab)
                InformationLoaded = False
                If TempNewTab.Name = MuscleView.Name Then
                    FillMuscleView(ReadNode)
                End If
            ElseIf TempNewTab.Name = MuscleView.Name Then
                FillMuscleView(ReadNode)
            End If
        Next
    End Sub

#End Region

#Region "Context Menu Strip"

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
                InjectBPEToolStripMenuItem.Visible = False
                InjectZLIBToolStripMenuItem.Visible = False
                InjectOODLToolStripMenuItem.Visible = False
                If ParentNodeTag.FileType = PackageType.BPE Then
                    If PackUnpack.CheckBPEExe() Then
                        InjectToolStripMenuItem.Tag = True
                        'InjectBPEToolStripMenuItem.Visible = True
                    End If
                ElseIf ParentNodeTag.FileType = PackageType.ZLIB Then
                    If PackUnpack.CheckIconicZlib() Then
                        InjectToolStripMenuItem.Tag = True
                        'InjectZLIBToolStripMenuItem.Visible = True
                    End If
                ElseIf ParentNodeTag.FileType = PackageType.OODL Then
                    If PackUnpack.CheckOodle() Then
                        InjectToolStripMenuItem.Tag = True
                        'InjectOODLToolStripMenuItem.Visible = True
                    End If
                Else
                    'We are working with a actual file part
                    InjectToolStripMenuItem.Tag = True
                    InjectUncompressedToolStripMenuItem.Tag = True
                    RenamePartToolStripMenuItem.Tag = True
                    If Not IsNothing(ParentNodeTag) Then
                        If TreeView1.SelectedNode.Parent.Nodes.Count > 1 Then
                            DeletePartToolStripMenuItem.Tag = True
                        End If
                    End If
                    If PackUnpack.CheckBPEExe() Then
                        InjectBPEToolStripMenuItem.Visible = True
                    Else
                        InjectBPEToolStripMenuItem.Visible = False
                    End If
                    If PackUnpack.CheckIconicZlib() Then
                        InjectZLIBToolStripMenuItem.Visible = True
                    Else
                        InjectZLIBToolStripMenuItem.Visible = False
                    End If
                    If PackUnpack.CheckOodle() Then
                        InjectOODLToolStripMenuItem.Visible = True
                    Else
                        InjectOODLToolStripMenuItem.Visible = False
                    End If
                End If
            Else
                'We are working on a File, not a file part
                RenameFileToolStripMenuItem.Tag = True
                DeleteFileToolStripMenuItem.Tag = True
                OpenFileLocationToolStripMenuItem.Tag = True
                OpenFileLocationToolStripMenuItem.Text = "Open file location"
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
            SettingsHandlers.CheckDDSexe(True)
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
                    If GeneralTools.DeleteAllItems(filepath) Then SelectedNode.Remove()
                End If
            Else
                MessageBox.Show("Folder " & filepath & " Not Found")
            End If
        Else 'it should be a file
            If File.Exists(filepath) Then
                If MessageBox.Show("Would you like to delete file " & Path.GetFileName(filepath) & " ?", "Delete File?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    If GeneralTools.DeleteSafely(filepath) Then SelectedNode.Remove()
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

#End Region

#Region "View Controls"

#Region "Multi-View Controls"

    'Commands that should be generic to be used across multiple tabs.
    Private Sub StoreOldComboBoxValue(sender As Object, e As EventArgs) Handles HexViewBitWidth.Enter,
                                                                        TextViewBitWidth.Enter
        OldValue = sender.text
    End Sub

    Private Sub StoreOldDataGridViewValue(sender As DataGridView, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellEnter,
                                                                                                    DataGridShowView.CellEnter,
                                                                                                    DataGridNIBJView.CellEnter,
                                                                                                    DataGridAttireView.CellEnter,
                                                                                                    DataGridMaskView.CellEnter,
                                                                                                    DataGridObjArrayView.CellEnter,
                                                                                                    DataGridAssetView.CellEnter,
                                                                                                    DataGridTitleView.CellEnter,
                                                                                                    DataGridSoundView.CellEnter,
                                                                                                    DataGridMenuItemView.CellEnter,
                                                                                                    DataGridWeaponPositionView.CellEnter
        OldValue = sender.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub

    Sub SaveFileNoLongerPending()
        'Read node should auto reset if the menu gets regenerated..
        'if this causes issues we will need to call a regen node somehow.
        SavePending = False
        SaveChangesStringMenuItem.Visible = False
        SaveChangesMiscMenuItem.Visible = False
        SaveChangesShowMenuItem.Visible = False
        SaveChangesNIBJMenuItem.Visible = False
        SaveChangesAttireMenuItem.Visible = False
        SaveChangesMaskMenuItem.Visible = False
        SaveChangesYOBJArrayMenuItem.Visible = False
        SaveChangesAssetViewMenuItem.Visible = False
        SaveChangesTitleMenuItem.Visible = False
        SaveChangesSoundMenuItem.Visible = False
        SaveChangesCAEMenuItemMenuItem.Visible = False
        RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        'TO DO Update this to include all save buttons
    End Sub

    Function ClearandGetClone(SentDataGrid) As DataGridViewRow
        SentDataGrid.Rows.Clear()
        SentDataGrid.Rows.Add()
        Dim CloneRow As DataGridViewRow = SentDataGrid.Rows(0).Clone()
        SentDataGrid.Rows.Clear()
        Return CloneRow
    End Function

    Public ReadOnlyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle With {
        .BackColor = SystemColors.Control,
        .ForeColor = SystemColors.ControlText}

    Public DefaultCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle With {
        .BackColor = SystemColors.Window,
        .ForeColor = SystemColors.WindowText}
#End Region

    'TO DO Request Direct Editing of Hex Edit View

#Region "Hex View Controls"

    Sub AddHexText(SelectedNode As TreeNode)
        If File.Exists(SelectedNode.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If HexViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(HexViewBitWidth.Text)
            Else
                bitwidth = CInt(HexViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As ExtendedFileProperties = CType(SelectedNode.Tag, ExtendedFileProperties)
            Dim Filebytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedNode.Tag)
            Dim ByteString As String = ""
            If Filebytes.Length < (&H1000 * My.Settings.HexViewLength) Then
                ByteString = (BitConverter.ToString(Filebytes, 0, Filebytes.Length).Replace("-", " "))
            Else
                ByteString = (BitConverter.ToString(Filebytes, 0, (&H1000 * My.Settings.HexViewLength)).Replace("-", " "))
            End If
            Dim builder As New StringBuilder(ByteString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 3)
            For i As Int32 = startIndex To (bitwidth * 3) Step -(bitwidth * 3)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Hex_Selected.Text = builder.ToString()
        End If
    End Sub

    Private Sub HexViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles HexViewBitWidth.TextChanged
        If HexViewBitWidth.Text = "" Then
            'Do Nothing
            Exit Sub
        ElseIf Not IsNumeric(HexViewBitWidth.Text) Then
            HexViewBitWidth.Text = OldValue
            Exit Sub
        ElseIf CInt(HexViewBitWidth.Text) > 0 Then
            If HexViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = HexViewBitWidth.SelectedIndex
                TextViewBitWidth.SelectedIndex = HexViewBitWidth.SelectedIndex
            Else
                TextViewBitWidth.SelectedIndex = -1
                TextViewBitWidth.Text = HexViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddHexText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

#End Region

#Region "Text View Controls"

    Sub AddText(SelectedNode As TreeNode)
        If File.Exists(SelectedNode.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If TextViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(TextViewBitWidth.Text)
            Else
                bitwidth = CInt(TextViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As ExtendedFileProperties = CType(SelectedNode.Tag, ExtendedFileProperties)
            Dim Filebytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedNode.Tag)
            If Filebytes.Length > 0 Then
                Dim TextString As String = ""
                If NodeTag.length < (&H1000 * My.Settings.HexViewLength) Then
                    TextString = New String(".", NodeTag.length)
                Else
                    TextString = New String(".", (&H1000 * My.Settings.HexViewLength))
                End If
                Dim FirstBuilder As New StringBuilder(TextString)
                For i As Integer = 0 To TextString.Length - 1
                    If Filebytes(i) > 31 AndAlso (Filebytes(i) < 257) Then
                        FirstBuilder(i) = Encoding.Default.GetChars(Filebytes, i, 1)(0)
                    End If
                Next
                Dim builder As New StringBuilder(FirstBuilder.ToString().Replace(vbCr, ".").Replace(vbLf, "."))
                Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 1)
                For i As Int32 = startIndex To (bitwidth * 1) Step -(bitwidth * 1)
                    builder.Insert(i, vbCr & vbLf)
                Next i
                Text_Selected.Text = builder.ToString()
            Else
                Text_Selected.Text = ""
            End If
        End If
    End Sub

    Private Sub TextViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles TextViewBitWidth.TextChanged
        If TextViewBitWidth.Text = "" Then
            'Do Nothing
            Exit Sub
        ElseIf Not IsNumeric(TextViewBitWidth.Text) Then
            TextViewBitWidth.Text = OldValue
            Exit Sub
        ElseIf CInt(TextViewBitWidth.Text) > 0 Then
            If TextViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = TextViewBitWidth.SelectedIndex
                HexViewBitWidth.SelectedIndex = TextViewBitWidth.SelectedIndex
            Else
                HexViewBitWidth.SelectedIndex = -1
                HexViewBitWidth.Text = TextViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

#End Region

#Region "String View Controls"

    Sub FillStringView(SelectedData As TreeNode)
        Dim Testing As String = ""
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridStringView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Try
            Dim NodeTag As ExtendedFileProperties = CType(SelectedData.Tag, ExtendedFileProperties)
            Dim StringBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
            Dim StringCount As Integer = BitConverter.ToInt32(StringBytes, 4)
            StringCountToolStripMenuItem.Text = "String Count: " & StringCount
            ProgressBar1.Maximum = StringCount - 1
            ProgressBar1.Value = 0
            'Get Data On the Pach parts
            Dim StringFileOffset(Int16.MaxValue) As Integer
            Dim StringFileLength(Int16.MaxValue) As Integer
            Dim StringFileReference(Int16.MaxValue) As Integer
            For j As Integer = 0 To StringCount - 1
                StringFileOffset(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 0)
                StringFileLength(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 4)
                StringFileReference(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 8)
                Testing = StringFileReference(j)
                'Trim all 00 chars so the strings don't end abrubtly in future manipulation
                Dim TempStringBytes As Byte() = New Byte(StringFileLength(j) - 1) {}
                Array.Copy(StringBytes, StringFileOffset(j), TempStringBytes, 0, StringFileLength(j))
                StringReferences(StringFileReference(j)) = Encoding.Default.GetString(TempStringBytes).TrimEnd(Chr(0))
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = Hex(StringFileReference(j))
                TempGridRow.Cells(1).Value = StringReferences(StringFileReference(j))
                TempGridRow.Cells(2).Value = StringFileLength(j).ToString
                TempGridRow.Cells(3).Value = "Add"
                TempGridRow.Cells(4).Value = "Remove"
                TempGridRow.Tag = TempStringBytes
                WorkingCollection.Add(TempGridRow)
                ProgressBar1.Value = j
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & Testing)
        End Try
        DataGridStringView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Sub SortStringView()
        Dim TempColumn As DataGridViewColumn = New DataGridViewTextBoxColumn With {.Name = "TempCol", .Visible = False}
        DataGridStringView.Columns.Add(TempColumn)
        Dim ColNum As Integer = DataGridStringView.Columns.IndexOf(TempColumn)
        For Each TempRow As DataGridViewRow In DataGridStringView.Rows
            Dim TempNumber As UInt32 = CUInt("&H" & TempRow.Cells(0).Value.ToString)
            TempRow.Cells(ColNum).Value = TempNumber
        Next
        DataGridStringView.Sort(DataGridStringView.Columns(ColNum), System.ComponentModel.ListSortDirection.Ascending)
        DataGridStringView.Columns.Remove(TempColumn)
        SortStringsToolStripMenuItem.Visible = False
    End Sub

    Function CheckDuplicateStrings() As Boolean 'returns True for a dupe row
        Dim BuiltList As List(Of Integer) = New List(Of Integer)
        For i As Integer = 0 To DataGridStringView.Rows.Count - 1
            Dim TempNumber As UInt32 = CUInt("&H" & DataGridStringView.Rows(i).Cells(0).Value.ToString)
            If BuiltList.Contains(TempNumber) Then
                MessageBox.Show("Duplicate string ID found at row " & i)
                Return True
            Else
                BuiltList.Add(TempNumber)
            End If
        Next
        Return False
    End Function

    Private Sub SaveChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesStringMenuItem.Click
        SortStringView()
        If Not CheckDuplicateStrings() Then
            FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildStringFile())
        End If
    End Sub

    Private Sub SortStringsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortStringsToolStripMenuItem.Click
        SortStringView()
    End Sub

    Dim OldLength As Integer
    Dim LengthTheSame As Boolean = False

    Private Sub DataGridStringView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellEnter
        If e.ColumnIndex = 0 Then 'Hex Text Reference Editing
            OldValue = DataGridStringView.Rows(e.RowIndex).Cells(0).Value
        ElseIf e.ColumnIndex = 1 Then
            OldValue = DataGridStringView.Rows(e.RowIndex).Cells(1).Value
            OldLength = DataGridStringView.Rows(e.RowIndex).Cells(2).Value
            LengthTheSame = (OldValue.Length + 1 = OldLength) 'If length is not the same then it might be a super string
        ElseIf e.ColumnIndex = 2 Then
            OldValue = DataGridStringView.Rows(e.RowIndex).Cells(2).Value
        End If
    End Sub

    Private Sub DataGridStringView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridStringView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 0 Then 'Hex value
            If Not GeneralTools.HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            Else
                SortStringsToolStripMenuItem.Visible = True
                SaveChangesStringMenuItem.Visible = True
            End If
        ElseIf e.ColumnIndex = 1 Then 'string text
            If Not MyCell.Value = OldValue Then
                If LengthTheSame Then
                    DataGridStringView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value.length + 1
                Else
                    If MessageBox.Show("String currently contains extra characters." & vbNewLine &
                                      "Would you like to maintain the length", "Potential Super String Detected!",
                                       MessageBoxButtons.YesNo) = DialogResult.No Then
                        DataGridStringView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value.length + 1
                    Else 'check if new string is too long
                        If DataGridStringView.Rows(e.RowIndex).Cells(2).Value < MyCell.Value.length + 1 Then
                            MessageBox.Show("String is too long, string will be truncated.")
                            MyCell.Value = MyCell.Value.ToString.Substring(0, DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
                        End If
                    End If
                End If
                DataGridStringView.Rows(e.RowIndex).Tag = Encoding.Default.GetBytes(MyCell.Value + Chr(0)) 'Stores the new string as bytes in the tag
                'this redim keeps the right length for a super string type string where there are several 0 chars at the end
                ReDim Preserve DataGridStringView.Rows(e.RowIndex).Tag(DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
                SavePending = True
                SaveChangesStringMenuItem.Visible = True
            End If
        ElseIf e.ColumnIndex = 2 Then 'Adjusting Length only
            If Not IsNumeric(MyCell.Value) OrElse
               MyCell.Value < 1 Then
                MyCell.Value = OldValue
            Else
                If MyCell.Value < DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1 Then
                    MessageBox.Show("String is too long, string will be truncated.")
                    DataGridStringView.Rows(e.RowIndex).Cells(1).Value =
                                DataGridStringView.Rows(e.RowIndex).Cells(1).Value.ToString.Substring(0, MyCell.Value - 1) 'Truncates the String
                    DataGridStringView.Rows(e.RowIndex).Tag = Encoding.Default.GetBytes(DataGridStringView.Rows(e.RowIndex).Cells(1).Value + Chr(0)) 'Stores the new string as bytes in the tag
                Else 'Redim the tag to expand it
                    ReDim Preserve DataGridStringView.Rows(e.RowIndex).Tag(MyCell.Value - 1)
                End If
                SavePending = True
                SaveChangesStringMenuItem.Visible = True
            End If
        End If
    End Sub

    Private Sub DataGridStringView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 3 Then 'add button
                Dim Duplicaterow As DataGridViewRow = DataGridStringView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridStringView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridStringView.Rows(e.RowIndex).Cells(i).Value
                Next
                Duplicaterow.Tag = DataGridStringView.Rows(e.RowIndex).Tag
                DataGridStringView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
            ElseIf e.ColumnIndex = 4 Then 'Delete button
                DataGridStringView.Rows.RemoveAt(e.RowIndex)
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub

    Private Sub ToolStripTextBoxSearch_Enter(sender As Object, e As EventArgs) Handles ToolStripTextBoxSearch.Enter
        If ToolStripTextBoxSearch.Text = "Search..." Then
            ToolStripTextBoxSearch.Text = ""
        End If
    End Sub

    Private Sub ToolStripTextBoxSearch_Leave(sender As Object, e As EventArgs) Handles ToolStripTextBoxSearch.Leave
        If ToolStripTextBoxSearch.Text = "" Then
            ToolStripTextBoxSearch.Text = "Search..."
        End If
        Dim TemporaryCollection As DataGridViewRow() = New DataGridViewRow(DataGridStringView.Rows.Count - 1) {}
        DataGridStringView.Rows.CopyTo(TemporaryCollection, 0)
        DataGridStringView.Rows.Clear()
        ProgressBar1.Maximum = TemporaryCollection.Count - 1
        ProgressBar1.Value = 0
        If ToolStripTextBoxSearch.Text = "" OrElse
            ToolStripTextBoxSearch.Text = "Search..." Then
            For i As Integer = 0 To TemporaryCollection.Count - 1
                TemporaryCollection(i).Visible = True
                ProgressBar1.Value = i
            Next
        Else
            For i As Integer = 0 To TemporaryCollection.Count - 1
                If TemporaryCollection(i).Cells(1).Value.ToString.ToLower.Contains(ToolStripTextBoxSearch.Text.ToLower) Then
                    TemporaryCollection(i).Visible = True
                Else
                    TemporaryCollection(i).Visible = False
                End If
                ProgressBar1.Value = i
            Next
        End If
        DataGridStringView.Rows.AddRange(TemporaryCollection.ToArray)
    End Sub

    Private Function BuildStringFile() As Byte()
        Dim StringCount As Integer = DataGridStringView.RowCount
        Dim StringSum As Integer = 0
        For i As Integer = 0 To StringCount - 1
            StringSum += DataGridStringView.Rows(i).Cells(2).Value
        Next
        'First get the string count and make a header
        Dim ReturnedBytes As Byte() = New Byte(&H8 + StringCount * &HC + StringSum - 1) {} 'String Sum adds on an extra 1 for the last string from my tests
        'Building the header 0000 , string count
        Array.Copy(BitConverter.GetBytes(CUInt(StringCount)), 0, ReturnedBytes, 4, 4)
        ProgressBar1.Maximum = StringCount - 1
        ProgressBar1.Value = 0
        Dim index As UInt32 = &H8 + StringCount * &HC
        For i As Integer = 0 To StringCount - 1
            'index of string won't change
            Array.Copy(BitConverter.GetBytes(index), 0, ReturnedBytes, 8 + i * 12 + 0, 4)
            'String Length will be equal to cell 3 (2 in 0 index)
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridStringView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, 8 + i * 12 + 4, 4)
            'Reference is set from the first cell
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridStringView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, 8 + i * 12 + 8, 4)
            'now we have to copy the string from the tag storage
            Dim TempArray As Byte() = Encoding.Default.GetBytes(DataGridStringView.Rows(i).Cells(1).Value) 'DataGridStringView.Rows(i).Tag ' Casting the Tag so the array handles it properly
            Array.Copy(TempArray, 0, ReturnedBytes, index, TempArray.Length) 'CUInt(DataGridStringView.Rows(i).Cells(2).Value))
            'Now add the length to the index
            index += CUInt(DataGridStringView.Rows(i).Cells(2).Value)
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function

#End Region

#Region "Misc View Controls"

    Public Class ArenaInformation
        Public Stadium As Integer = -2
        Public Advertisement As Integer = -2
        Public CornerPost As Integer = -2
        Public LED_CornerPost As Integer = -2
        Public Rope As Integer = -2
        Public Apron As Integer = -2
        Public LED_Apron As Integer = -2
        Public Turnbuckle As Integer = -2
        Public Barricade As Integer = -2
        Public Fence As Integer = -2
        Public CeilingLighting As Integer = -2
        Public Spotlight As Integer = -2
        Public Stairs As Integer = -2
        Public CommentarySeat As Integer = -2
        Public RingMat As Integer = -2
        Public FloorMattress As Integer = -2
        Public Crowd As Integer = -2
        Public CrowdSeatsPlace As Integer = -2
        Public CrowdSeatsModel As Integer = -2
        Public IBL As Integer = -2
        Public Titantron As Integer = -2
        Public Minitron As Integer = -2
        Public Wall_L As Integer = -2
        Public Wall_R As Integer = -2
        Public Header As Integer = -2
        Public Floor As Integer = -2
        Public MiscObjects As Integer = -2
        Public LightingType As Integer = -2
        Public CornerPost_CM As Integer = -2
        Public Rope_CM As Integer = -2
        Public Apron_CM As Integer = -2
        Public Turnbuckle_CM As Integer = -2
        Public RingMat_CM As Integer = -2
        Public version As String = "1.0"
    End Class

    Sub FillMiscView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMiscView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim MiscBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim ArenaCount As Integer = BitConverter.ToInt32(MiscBytes, 0)
        ProgressBar1.Maximum = ArenaCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To ArenaCount - 1
            Dim DByteList As List(Of Boolean) = New List(Of Boolean)
            Dim ArenaNum As String = Encoding.ASCII.GetString(MiscBytes, 25 + i * 32, 5)
            Dim TempIndex As Integer = BitConverter.ToInt32(MiscBytes, 40 + i * 32)
            Dim TempLength As Integer = BitConverter.ToInt32(MiscBytes, 36 + i * 32)
            Dim ArenaArray As Byte() = New Byte(TempLength - 1) {}
            Array.Copy(MiscBytes, TempIndex, ArenaArray, 0, TempLength)
            Dim ArenaJson As String = Encoding.ASCII.GetString(ArenaArray)
            Dim TempArena As ArenaInformation = ParseJsonToArena(ArenaJson)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = ArenaNum
            TempGridRow.Cells(1).Value = TempArena.Stadium
            TempGridRow.Cells(2).Value = TempArena.Advertisement
            TempGridRow.Cells(3).Value = TempArena.CornerPost
            TempGridRow.Cells(4).Value = TempArena.LED_CornerPost
            TempGridRow.Cells(5).Value = TempArena.Rope
            TempGridRow.Cells(6).Value = TempArena.Apron
            TempGridRow.Cells(7).Value = TempArena.LED_Apron
            TempGridRow.Cells(8).Value = TempArena.Turnbuckle
            TempGridRow.Cells(9).Value = TempArena.Barricade
            TempGridRow.Cells(10).Value = TempArena.Fence
            TempGridRow.Cells(11).Value = TempArena.CeilingLighting
            TempGridRow.Cells(12).Value = TempArena.Spotlight
            TempGridRow.Cells(13).Value = TempArena.Stairs
            TempGridRow.Cells(14).Value = TempArena.CommentarySeat
            TempGridRow.Cells(15).Value = TempArena.RingMat
            TempGridRow.Cells(16).Value = TempArena.FloorMattress
            TempGridRow.Cells(17).Value = TempArena.Crowd
            TempGridRow.Cells(18).Value = TempArena.CrowdSeatsPlace
            TempGridRow.Cells(19).Value = TempArena.CrowdSeatsModel
            TempGridRow.Cells(20).Value = TempArena.IBL
            TempGridRow.Cells(21).Value = TempArena.Titantron
            TempGridRow.Cells(22).Value = TempArena.Minitron
            TempGridRow.Cells(23).Value = TempArena.Wall_L
            TempGridRow.Cells(24).Value = TempArena.Wall_R
            TempGridRow.Cells(25).Value = TempArena.Header
            TempGridRow.Cells(26).Value = TempArena.Floor
            TempGridRow.Cells(27).Value = TempArena.MiscObjects
            TempGridRow.Cells(28).Value = TempArena.LightingType
            TempGridRow.Cells(29).Value = TempArena.CornerPost_CM
            TempGridRow.Cells(30).Value = TempArena.Rope_CM
            TempGridRow.Cells(31).Value = TempArena.Apron_CM
            TempGridRow.Cells(32).Value = TempArena.Turnbuckle_CM
            TempGridRow.Cells(33).Value = TempArena.RingMat_CM
            TempGridRow.Cells(34).Value = TempArena.version
            'Build the D byte list for error handling
            TempGridRow.Cells(35).Style = ReadOnlyCellStyle
            '35 = Add
            '36 = Delete
            For K As Integer = 0 To ArenaArray.Length - 1
                If ArenaArray(K) = &HA Then
                    If ArenaArray(K - 1) = &HD Then
                        DByteList.Add(True)
                    Else
                        DByteList.Add(False)
                    End If
                End If
            Next
            TempGridRow.Tag = DByteList
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridMiscView.Rows.AddRange(WorkingCollection.ToArray)
        GetMiscColumns(MiscViewType.SelectedIndex)
    End Sub

    Function ParseJsonToArena(ArenaJson As String) As ArenaInformation
        Dim reader As Newtonsoft.Json.JsonTextReader = New JsonTextReader(New StringReader(ArenaJson))
        reader.Read() 'Skips the first null value
        Dim ReturnedArena As ArenaInformation = New ArenaInformation
        While reader.Read
            Dim TemporaryArenaPart As String = reader.Value
            reader.Read()
            Select Case TemporaryArenaPart
                Case "Stadium"
                    ReturnedArena.Stadium = reader.Value
                Case "Advertisement"
                    ReturnedArena.Advertisement = reader.Value
                Case "CornerPost"
                    ReturnedArena.CornerPost = reader.Value
                Case "LED_CornerPost"
                    ReturnedArena.LED_CornerPost = reader.Value
                Case "Rope"
                    ReturnedArena.Rope = reader.Value
                Case "Apron"
                    ReturnedArena.Apron = reader.Value
                Case "LED_Apron"
                    ReturnedArena.LED_Apron = reader.Value
                Case "Turnbuckle"
                    ReturnedArena.Turnbuckle = reader.Value
                Case "Barricade"
                    ReturnedArena.Barricade = reader.Value
                Case "Fence"
                    ReturnedArena.Fence = reader.Value
                Case "CeilingLighting"
                    ReturnedArena.CeilingLighting = reader.Value
                Case "Spotlight"
                    ReturnedArena.Spotlight = reader.Value
                Case "Stairs"
                    ReturnedArena.Stairs = reader.Value
                Case "CommentarySeat"
                    ReturnedArena.CommentarySeat = reader.Value
                Case "RingMat"
                    ReturnedArena.RingMat = reader.Value
                Case "FloorMattress"
                    ReturnedArena.FloorMattress = reader.Value
                Case "Crowd"
                    ReturnedArena.Crowd = reader.Value
                Case "CrowdSeatsPlace"
                    ReturnedArena.CrowdSeatsPlace = reader.Value
                Case "CrowdSeatsModel"
                    ReturnedArena.CrowdSeatsModel = reader.Value
                Case "IBL"
                    ReturnedArena.IBL = reader.Value
                Case "Titantron"
                    ReturnedArena.Titantron = reader.Value
                Case "Minitron"
                    ReturnedArena.Minitron = reader.Value
                Case "Wall_L"
                    ReturnedArena.Wall_L = reader.Value
                Case "Wall_R"
                    ReturnedArena.Wall_R = reader.Value
                Case "Header"
                    ReturnedArena.Header = reader.Value
                Case "Floor"
                    ReturnedArena.Floor = reader.Value
                Case "MiscObjects"
                    ReturnedArena.MiscObjects = reader.Value
                Case "LightingType"
                    ReturnedArena.LightingType = reader.Value
                Case "CornerPost_CM"
                    ReturnedArena.CornerPost_CM = reader.Value
                Case "Rope_CM"
                    ReturnedArena.Rope_CM = reader.Value
                Case "Apron_CM"
                    ReturnedArena.Apron_CM = reader.Value
                Case "Turnbuckle_CM"
                    ReturnedArena.Turnbuckle_CM = reader.Value
                Case "RingMat_CM"
                    ReturnedArena.RingMat_CM = reader.Value
                Case "version"
                    ReturnedArena.version = reader.Value
                Case ""
                    'Skip because null type
                Case Else
                    MessageBox.Show("Unknown Obejct: " & TemporaryArenaPart)
            End Select
        End While
        Return ReturnedArena
    End Function

    Sub GetMiscColumns(MenuIndex As Integer)
        If Not MenuIndex > 0 Then '2K16 and Beyond
            DataGridMiscView.Columns("Col_LED_Apron").Visible = False
            DataGridMiscView.Columns("Col_Titantron").Visible = False
            DataGridMiscView.Columns("Col_Minitron").Visible = False
            DataGridMiscView.Columns("Col_Wall_L").Visible = False
            DataGridMiscView.Columns("Col_Wall_R").Visible = False
            DataGridMiscView.Columns("Col_Header").Visible = False
            DataGridMiscView.Columns("Col_Floor").Visible = False
            DataGridMiscView.Columns("Col_MiscObjects").Visible = False
        Else
            DataGridMiscView.Columns("Col_LED_Apron").Visible = True
            DataGridMiscView.Columns("Col_Titantron").Visible = True
            DataGridMiscView.Columns("Col_Minitron").Visible = True
            DataGridMiscView.Columns("Col_Wall_L").Visible = True
            DataGridMiscView.Columns("Col_Wall_R").Visible = True
            DataGridMiscView.Columns("Col_Header").Visible = True
            DataGridMiscView.Columns("Col_Floor").Visible = True
            DataGridMiscView.Columns("Col_MiscObjects").Visible = True
            If Not MenuIndex > 2 Then '2K18 and Beyond
                DataGridMiscView.Columns("Col_LED_CornerPost").Visible = False
                DataGridMiscView.Columns("Col_LightingType").Visible = False
            Else
                DataGridMiscView.Columns("Col_LED_CornerPost").Visible = True
                DataGridMiscView.Columns("Col_LightingType").Visible = True
                If Not MenuIndex > 3 Then '2K19 and Beyond
                    DataGridMiscView.Columns("Col_CrowdSeatsPlace").Visible = False
                    DataGridMiscView.Columns("Col_CrowdSeatsModel").Visible = False
                    DataGridMiscView.Columns("Col_CornerPost_CM").Visible = False
                    DataGridMiscView.Columns("Col_Rope_CM").Visible = False
                    DataGridMiscView.Columns("Col_Apron_CM").Visible = False
                    DataGridMiscView.Columns("Col_Turnbuckle_CM").Visible = False
                    DataGridMiscView.Columns("Col_RingMat_CM").Visible = False
                Else
                    DataGridMiscView.Columns("Col_CrowdSeatsPlace").Visible = True
                    DataGridMiscView.Columns("Col_CrowdSeatsModel").Visible = True
                    DataGridMiscView.Columns("Col_CornerPost_CM").Visible = True
                    DataGridMiscView.Columns("Col_Rope_CM").Visible = True
                    DataGridMiscView.Columns("Col_Apron_CM").Visible = True
                    DataGridMiscView.Columns("Col_Turnbuckle_CM").Visible = True
                    DataGridMiscView.Columns("Col_RingMat_CM").Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub MiscViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MiscViewType.SelectedIndexChanged
        'Locked to 2K19 for now
        If Not MiscViewType.SelectedIndex = 4 Then
            MiscViewType.SelectedIndex = 4
            Exit Sub
        End If
        If Not My.Settings.MiscModeIndex = MiscViewType.SelectedIndex Then 'checks if the index is actually changed, or just being reverted
            If SavePending Then
                If MessageBox.Show("Any changes will be lost", "Continue format change?", MessageBoxButtons.YesNo) = DialogResult.No Then
                    MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
                    Exit Sub
                End If
            End If
            My.Settings.MiscModeIndex = MiscViewType.SelectedIndex
            If TreeView1.SelectedNode IsNot Nothing Then
                FillMiscView(TreeView1.SelectedNode)
            End If
        End If
    End Sub

    Private Sub DataGridMiscView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMiscView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If OldValue = -2 Then
            MyCell.Value = OldValue
            Exit Sub
        End If
        If Not IsNumeric(MyCell.Value) OrElse
               MyCell.Value < -1 Then
            MyCell.Value = OldValue
        Else
            SavePending = True
            SaveChangesMiscMenuItem.Visible = True
        End If
    End Sub

    Private Sub DataGridMiscView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 35 Then 'add button
                If Not DataGridMiscView.Rows(e.RowIndex).Cells(0).Value + 1 = DataGridMiscView.Rows(e.RowIndex + 1).Cells(0).Value Then
                    Dim Duplicaterow As DataGridViewRow = DataGridMiscView.Rows(e.RowIndex).Clone
                    For i As Integer = 0 To DataGridMiscView.Rows(e.RowIndex).Cells.Count - 1
                        Duplicaterow.Cells(i).Value = DataGridMiscView.Rows(e.RowIndex).Cells(i).Value
                    Next
                    Duplicaterow.Cells(0).Value = (Duplicaterow.Cells(0).Value + 1).ToString.PadLeft(5, "0")
                    Duplicaterow.Tag = DataGridMiscView.Rows(e.RowIndex).Tag
                    DataGridMiscView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                    SaveChangesMiscMenuItem.Visible = True
                Else
                    MessageBox.Show("Cannot Create New Arena Here")
                End If
            ElseIf e.ColumnIndex = 36 Then 'Delete button
                DataGridMiscView.Rows.RemoveAt(e.RowIndex)
                SaveChangesMiscMenuItem.Visible = True
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub

    Private Sub SaveMiscChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesMiscMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildMiscFile())
    End Sub

    Private Function BuildMiscFile() As Byte()
        Dim Active_Offset As Integer = &H10 + &H20 * DataGridMiscView.RowCount
        Dim Temp_Array As Byte() = New Byte(&H20000) {}
        Temp_Array(0) = DataGridMiscView.RowCount
        Temp_Array(5) = 1
        Temp_Array(&HC) = &H10
        ProgressBar1.Maximum = DataGridMiscView.RowCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To DataGridMiscView.RowCount - 1
            'Making the header
            Dim Part_Head_Array As Byte() = New Byte(&H20) {}
            'Adding the text parts.
            Dim ArenaInfoBytes As Byte() = Encoding.ASCII.GetBytes("arenaInfo" & DataGridMiscView.Rows(i).Cells(0).Value)
            Buffer.BlockCopy(ArenaInfoBytes, 0, Part_Head_Array, 0, ArenaInfoBytes.Length)
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("jsn"), 0, Part_Head_Array, &H10, 3)
            'Build the Arena String
            Dim Part_String As String = BuildJsonArenaFile(i)
            'if there is a builder error this will exit the function without writing the file.
            If Part_String = "" Then
                Return Nothing
            End If
            'Add the Length to the Header
            Buffer.BlockCopy(BitConverter.GetBytes(Part_String.Length), 0, Part_Head_Array, &H14, 4)
            'Add the offset to the header
            Buffer.BlockCopy(BitConverter.GetBytes(Active_Offset), 0, Part_Head_Array, &H18, 4)
            'injecting the header
            Buffer.BlockCopy(Part_Head_Array, 0, Temp_Array, &H10 + &H20 * i, &H20)
            'Adding the ArenaInfo to the ararry
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(Part_String), 0, Temp_Array, Active_Offset, Part_String.Length)
            'Updating the Active Offset
            Active_Offset += Part_String.Length
            If Part_String.Length Mod 16 > 0 Then
                Active_Offset += 16 - Part_String.Length Mod 16
            End If
            ProgressBar1.Value = i
        Next
        ReDim Preserve Temp_Array(Active_Offset - 1)
        'Dim Final_Array As Byte() = New Byte(Active_Offset - 1) {}
        'Buffer.BlockCopy(Temp_Array, 0, Final_Array, 0, Active_Offset)
        Return Temp_Array
    End Function

    Function JsonWriterArenaFile(Index As Integer) As String
        'TO DO TEST 2K18 - 2K15
        'This is to be tested for 2k18 - 2k15 before allowing it in a release
        Dim TempStringBuilder As StringBuilder = New StringBuilder()
        Dim TempStringWriter As StringWriter = New StringWriter(TempStringBuilder)
        Using ActualWriter As JsonWriter = New JsonTextWriter(TempStringWriter)
            ActualWriter.Formatting = Formatting.Indented
            ActualWriter.WriteStartObject()
            ActualWriter.WritePropertyName("Stadium")
            ActualWriter.WriteValue(DataGridMiscView(1, Index).Value)
            ActualWriter.WritePropertyName("Advertisement")
            ActualWriter.WriteValue(DataGridMiscView(2, Index).Value)
            ActualWriter.WritePropertyName("CornerPost")
            ActualWriter.WriteValue(DataGridMiscView(3, Index).Value)
            ActualWriter.WritePropertyName("LED_CornerPost")
            ActualWriter.WriteValue(DataGridMiscView(4, Index).Value)
            ActualWriter.WritePropertyName("Rope")
            ActualWriter.WriteValue(DataGridMiscView(5, Index).Value)
            ActualWriter.WritePropertyName("Apron")
            ActualWriter.WriteValue(DataGridMiscView(6, Index).Value)
            ActualWriter.WritePropertyName("LED_Apron")
            ActualWriter.WriteValue(DataGridMiscView(7, Index).Value)
            ActualWriter.WritePropertyName("Turnbuckle")
            ActualWriter.WriteValue(DataGridMiscView(8, Index).Value)
            ActualWriter.WritePropertyName("Barricade")
            ActualWriter.WriteValue(DataGridMiscView(9, Index).Value)
            ActualWriter.WritePropertyName("Fence")
            ActualWriter.WriteValue(DataGridMiscView(10, Index).Value)
            ActualWriter.WritePropertyName("CeilingLighting")
            ActualWriter.WriteValue(DataGridMiscView(11, Index).Value)
            ActualWriter.WritePropertyName("Spotlight")
            ActualWriter.WriteValue(DataGridMiscView(12, Index).Value)
            ActualWriter.WritePropertyName("Stairs")
            ActualWriter.WriteValue(DataGridMiscView(13, Index).Value)
            ActualWriter.WritePropertyName("RingMat")
            ActualWriter.WriteValue(DataGridMiscView(14, Index).Value)
            ActualWriter.WritePropertyName("FloorMattress")
            ActualWriter.WriteValue(DataGridMiscView(15, Index).Value)
            ActualWriter.WritePropertyName("Crowd")
            ActualWriter.WriteValue(DataGridMiscView(16, Index).Value)
            ActualWriter.WritePropertyName("CrowdSeatsPlace")
            ActualWriter.WriteValue(DataGridMiscView(17, Index).Value)
            ActualWriter.WritePropertyName("CrowdSeatsModel")
            ActualWriter.WriteValue(DataGridMiscView(18, Index).Value)
            ActualWriter.WritePropertyName("IBL")
            ActualWriter.WriteValue(DataGridMiscView(19, Index).Value)
            ActualWriter.WritePropertyName("Titantron")
            ActualWriter.WriteValue(DataGridMiscView(20, Index).Value)
            ActualWriter.WritePropertyName("Minitron")
            ActualWriter.WriteValue(DataGridMiscView(21, Index).Value)
            ActualWriter.WritePropertyName("Wall_L")
            ActualWriter.WriteValue(DataGridMiscView(22, Index).Value)
            ActualWriter.WritePropertyName("Wall_R")
            ActualWriter.WriteValue(DataGridMiscView(23, Index).Value)
            ActualWriter.WritePropertyName("Header")
            ActualWriter.WriteValue(DataGridMiscView(24, Index).Value)
            ActualWriter.WritePropertyName("Floor")
            ActualWriter.WriteValue(DataGridMiscView(25, Index).Value)
            ActualWriter.WritePropertyName("MiscObjects")
            ActualWriter.WriteValue(DataGridMiscView(26, Index).Value)
            ActualWriter.WritePropertyName("LightingType")
            ActualWriter.WriteValue(DataGridMiscView(27, Index).Value)
            ActualWriter.WritePropertyName("CornerPost_CM")
            ActualWriter.WriteValue(DataGridMiscView(28, Index).Value)
            ActualWriter.WritePropertyName("Rope_CM")
            ActualWriter.WriteValue(DataGridMiscView(29, Index).Value)
            ActualWriter.WritePropertyName("Apron_CM")
            ActualWriter.WriteValue(DataGridMiscView(30, Index).Value)
            ActualWriter.WritePropertyName("Turnbuckle_CM")
            ActualWriter.WriteValue(DataGridMiscView(31, Index).Value)
            ActualWriter.WritePropertyName("RingMat_CM")
            ActualWriter.WriteValue(DataGridMiscView(32, Index).Value)
            ActualWriter.WritePropertyName("version")
            ActualWriter.WriteValue(DataGridMiscView(33, Index).Value)
            ActualWriter.WriteEndObject()
        End Using
        Return TempStringBuilder.ToString()
    End Function

    Function BuildJsonArenaFile(index As Integer) As String
        Try
            Dim DListAray As List(Of Boolean) = DataGridMiscView.Rows(index).Tag
            ' Chr(&H7B) = { Chr(&HD) = Carriage return Chr(&HA) = Line feed
            Dim TempItemCount As Integer = 0
            Dim Temp_String As String = Chr(&H7B)
            If DListAray(TempItemCount + 0) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Stadium"":" & DataGridMiscView(1, index).Value.ToString & ","
            If DListAray(TempItemCount + 1) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Advertisement"":" & DataGridMiscView(2, index).Value.ToString & ","
            If DListAray(TempItemCount + 2) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CornerPost"":" & DataGridMiscView(3, index).Value.ToString & ","
            If Not DataGridMiscView(4, index).Value = -2 Then
                If DListAray(TempItemCount + 3) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LED_CornerPost"":" & DataGridMiscView(4, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If DListAray(TempItemCount + 3) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Rope"":" & DataGridMiscView(5, index).Value.ToString & ","
            If DListAray(TempItemCount + 4) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Apron"":" & DataGridMiscView(6, index).Value.ToString & ","
            If Not DataGridMiscView(7, index).Value = -2 Then
                If DListAray(TempItemCount + 5) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LED_Apron"":" & DataGridMiscView(7, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If DListAray(TempItemCount + 5) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Turnbuckle"":" & DataGridMiscView(8, index).Value.ToString & ","
            If DListAray(TempItemCount + 6) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Barricade"":" & DataGridMiscView(9, index).Value.ToString & ","
            If DListAray(TempItemCount + 7) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Fence"":" & DataGridMiscView(10, index).Value.ToString & ","
            If DListAray(TempItemCount + 8) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CeilingLighting"":" & DataGridMiscView(11, index).Value.ToString & ","
            If DListAray(TempItemCount + 9) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Spotlight"":" & DataGridMiscView(12, index).Value.ToString & ","
            If DListAray(TempItemCount + 10) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Stairs"":" & DataGridMiscView(13, index).Value.ToString & ","
            If DListAray(TempItemCount + 11) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CommentarySeat"":" & DataGridMiscView(14, index).Value.ToString & ","
            If DListAray(TempItemCount + 12) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """RingMat"":" & DataGridMiscView(15, index).Value.ToString & ","
            If DListAray(TempItemCount + 13) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """FloorMattress"":" & DataGridMiscView(16, index).Value.ToString & ","
            If DListAray(TempItemCount + 14) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Crowd"":" & DataGridMiscView(17, index).Value.ToString & ","
            If Not DataGridMiscView(18, index).Value = -2 Then
                If DListAray(TempItemCount + 15) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CrowdSeatsPlace"":" & DataGridMiscView(18, index).Value.ToString & ","
                If DListAray(TempItemCount + 16) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CrowdSeatsModel"":" & DataGridMiscView(19, index).Value.ToString & ","
                TempItemCount += 2
            End If
            If DListAray(TempItemCount + 15) Then Temp_String += Chr(&HD)
            Temp_String = Temp_String & Chr(&HA) & "    " & """IBL"":" & DataGridMiscView(20, index).Value.ToString & ","
            If DListAray(TempItemCount + 16) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Titantron"":" & DataGridMiscView(21, index).Value.ToString & ","
            If DListAray(TempItemCount + 17) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Minitron"":" & DataGridMiscView(22, index).Value.ToString & ","
            If DListAray(TempItemCount + 18) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Wall_L"":" & DataGridMiscView(23, index).Value.ToString & ","
            If DListAray(TempItemCount + 19) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Wall_R"":" & DataGridMiscView(24, index).Value.ToString & ","
            If DListAray(TempItemCount + 20) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Header"":" & DataGridMiscView(25, index).Value.ToString & ","
            If DListAray(TempItemCount + 21) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Floor"":" & DataGridMiscView(26, index).Value.ToString & ","
            If DListAray(TempItemCount + 22) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """MiscObjects"":" & DataGridMiscView(27, index).Value.ToString & ","
            If Not DataGridMiscView(28, index).Value.ToString = -2 Then
                If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LightingType"":" & DataGridMiscView(28, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If Not DataGridMiscView(29, index).Value.ToString = -2 Then
                If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CornerPost_CM"":" & DataGridMiscView(29, index).Value.ToString & ","
                If DListAray(TempItemCount + 24) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Rope_CM"":" & DataGridMiscView(30, index).Value.ToString & ","
                If DListAray(TempItemCount + 25) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Apron_CM"":" & DataGridMiscView(31, index).Value.ToString & ","
                If DListAray(TempItemCount + 26) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Turnbuckle_CM"":" & DataGridMiscView(32, index).Value.ToString & ","
                If DListAray(TempItemCount + 27) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """RingMat_CM"":" & DataGridMiscView(33, index).Value.ToString & ","
                TempItemCount += 5
            End If
            If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """version"":" & """" & DataGridMiscView(34, index).Value.ToString & """"
            If DListAray(TempItemCount + 24) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & Chr(&H7D)
            If DListAray(TempItemCount + 25) Then Temp_String += Chr(&HD) 'DListAray(TempItemCount + 17)
            Temp_String += Chr(&HA)
            Return Temp_String
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function

#End Region

#Region "Show View Controls"

    Sub FillShowView(SelectedData As TreeNode)
        '&h74 2K15 (&h4e), 2K16 (&h78) 2K17 (&h79),
        '&h78 2K18 (&h79)
        '&h7C 2K19 (&h99)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridShowView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim FileLength As Integer = ShowBytes.Length
        Dim GameTypeTest As UInt32 = BitConverter.ToInt32(ShowBytes, 8)
        Dim ShowSpacing As Integer = &H74
        Dim ShowCount As Integer = BitConverter.ToInt32(ShowBytes, 4)
        If GameTypeTest = &H19A Then '2K19
            ShowViewType.SelectedIndex = 4
            ShowSpacing = &H7C
        ElseIf GameTypeTest = &H12C Then '2K18-2K15
            If ShowCount = &H6F Then '2K18
                ShowViewType.SelectedIndex = 3
                ShowSpacing = &H78
            ElseIf ShowCount = &H69 Then '2K17
                ShowViewType.SelectedIndex = 2
                ShowSpacing = &H74
            ElseIf ShowCount = &H78 Then '2K16
                ShowViewType.SelectedIndex = 1
                ShowSpacing = &H74
            ElseIf ShowCount = &H4E Then '2K15
                ShowViewType.SelectedIndex = 0
                ShowSpacing = &H74
            Else
                MessageBox.Show("Unknown Game")
                Exit Sub
            End If
        Else
            MessageBox.Show("Unknown Game")
            Exit Sub
        End If
        Dim index As Integer = 0
        ProgressBar1.Maximum = FileLength
        ProgressBar1.Value = 0
        Dim current_poition As Long = &HC
        While current_poition < FileLength
            'TO DO Build out controls for games other than 2K19
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            Dim StringRef As UInt32 = BitConverter.ToUInt32(ShowBytes, current_poition)
            TempGridRow.Cells(0).Value = Hex(StringRef) ' Dim NameRef As String =
            TempGridRow.Cells(1).Value = StringReferences(StringRef) 'Name as string =
            TempGridRow.Cells(2).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 4) 'Dim SelectNum As int16 =
            TempGridRow.Cells(3).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 6) 'Dim SelectNumSecond As int16 =
            TempGridRow.Cells(4).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 8) 'Dim A1 As int16 =
            TempGridRow.Cells(5).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 10) 'Dim A2 As int16 =
            TempGridRow.Cells(6).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 12)) 'Dim B1 As String =
            TempGridRow.Cells(7).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 14)) 'Dim B2 As String =
            TempGridRow.Cells(8).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 16)) 'Dim B3 As String =
            '7 Bytes 00
            TempGridRow.Cells(9).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 25) 'Dim C1 As Boolean =
            TempGridRow.Cells(10).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 26) ' Dim C2 As Boolean =
            TempGridRow.Cells(11).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 27) 'Dim C3 As Boolean =
            TempGridRow.Cells(12).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 28) ' Dim C4 As Boolean =
            TempGridRow.Cells(13).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 29) 'Dim C5 As Boolean =
            TempGridRow.Cells(14).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 30) 'Dim C6 As Boolean =
            TempGridRow.Cells(15).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 31) 'Dim C7 As Boolean =
            '2 Bytes 00
            TempGridRow.Cells(16).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 34) 'Dim C8 As Boolean =
            TempGridRow.Cells(17).Value = ShowBytes(current_poition + 35) 'Dim Stage As byte =
            '2 bytes 00
            TempGridRow.Cells(18).Value = ShowBytes(current_poition + 38) 'Dim D1 As byte =
            TempGridRow.Cells(19).Value = ShowBytes(current_poition + 39) 'Dim D2 As byte =
            TempGridRow.Cells(20).Value = ShowBytes(current_poition + 40) 'Dim D3 As byte =
            TempGridRow.Cells(21).Value = ShowBytes(current_poition + 41) 'Dim D4 As byte =
            '1 byte 00
            TempGridRow.Cells(22).Value = ShowBytes(current_poition + 43) 'Dim D5 As byte =
            TempGridRow.Cells(23).Value = ShowBytes(current_poition + 44) 'Dim Ref As byte =
            'Dim Filter As String
            TempGridRow.Cells(24).Value = Hex(ShowBytes(current_poition + 45)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 46)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 47)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 48)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 49)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 50)).PadLeft(2, "0")
            '3 Bytes FF
            '2 Bytes 00
            TempGridRow.Cells(25).Value = Hex(BitConverter.ToUInt32(ShowBytes, current_poition + 56)) 'Dim F1 As String
            TempGridRow.Cells(26).Value = Hex(ShowBytes(current_poition + 60)) 'Dim F2 As String &H5A or &H00
            '3 bytes 00
            TempGridRow.Cells(27).Value = ShowBytes(current_poition + 64) 'Dim F3 As byte
            '1 Byte 00
            TempGridRow.Cells(28).Value = ShowBytes(current_poition + 66) 'Dim F4 As byte Possibly Tron
            '1 Byte 00
            TempGridRow.Cells(29).Value = ShowBytes(current_poition + 68) 'Dim H1 As byte
            TempGridRow.Cells(30).Value = ShowBytes(current_poition + 69) 'Dim H2 As byte
            TempGridRow.Cells(31).Value = ShowBytes(current_poition + 70) 'Dim H3 As byte
            TempGridRow.Cells(32).Value = ShowBytes(current_poition + 71) 'Dim H4 As byte
            '1 byte 00
            TempGridRow.Cells(33).Value = ShowBytes(current_poition + 73) 'Dim Bar As byte
            Dim temparray As Byte() = New Byte(33) {}
            Buffer.BlockCopy(ShowBytes, current_poition + 74, temparray, 0, 34)
            TempGridRow.Cells(34).Value = (BitConverter.ToString(temparray).Replace("-", ""))
            '3 byte 70
            TempGridRow.Cells(35).Value = ShowBytes(current_poition + 111) 'Dim I1 As byte
            TempGridRow.Cells(36).Value = Hex(ShowBytes(current_poition + 112)) 'Dim I2 As String
            '1 byte 00
            TempGridRow.Cells(37).Value = Hex(ShowBytes(current_poition + 114))  'Dim I3 As byte
            '2 byte 00
            TempGridRow.Cells(38).Value = ShowBytes(current_poition + 117) 'Dim live As byte
            '2 byte 00
            TempGridRow.Cells(39).Value = ShowBytes(current_poition + 120) 'Dim J As byte
            '40 Add
            '41 Delete
            TempGridRow.HeaderCell.Value = index.ToString
            WorkingCollection.Add(TempGridRow)
            index += 1
            current_poition += ShowSpacing
            ProgressBar1.Value = current_poition
        End While
        DataGridShowView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridShowView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridShowView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridShowView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 2, 3, 4, 5,
                 17, 18, 19, 20, 21, 22, 23,
                 27, 28, 29, 30, 31, 32, 33,
                 35, 38, 39
                If Not IsNumeric(MyCell.Value) OrElse
                       MyCell.Value < 0 Then
                    MyCell.Value = OldValue
                Else
                    SavePending = True
                    SaveChangesShowMenuItem.Visible = True
                End If

            Case Else 'Hex Text Required
                '0, 6, 7, 8, 34, 36, 37
                If Not GeneralTools.HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    If e.ColumnIndex = 24 Then 'Filter
                        MyCell.Value = MyCell.Value.ToString.PadRight(12, "0")
                    ElseIf e.ColumnIndex = 25 Then 'Filter
                        MyCell.Value = MyCell.Value.ToString.PadRight(8, "0")
                    ElseIf e.ColumnIndex = 34 Then
                        MyCell.Value = MyCell.Value.ToString.PadRight(68, "0")
                    End If
                    SavePending = True
                    SaveChangesShowMenuItem.Visible = True
                End If
        End Select
    End Sub

    Private Sub DataGridShowView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridShowView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If e.RowIndex >= 0 AndAlso
           e.ColumnIndex >= 0 AndAlso
            TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 40 Then 'add button
                Dim Duplicaterow As DataGridViewRow = DataGridShowView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridShowView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridShowView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridShowView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                SaveChangesShowMenuItem.Visible = True
                For i As Integer = e.RowIndex To DataGridShowView.Rows.Count - 1
                    DataGridShowView.Rows(i).HeaderCell.Value = i.ToString
                Next
            ElseIf e.ColumnIndex = 41 Then 'Delete button
                DataGridShowView.Rows.RemoveAt(e.RowIndex)
                SaveChangesShowMenuItem.Visible = True
                For i As Integer = e.RowIndex To DataGridShowView.Rows.Count - 1
                    DataGridShowView.Rows(i).HeaderCell.Value = i.ToString
                Next
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub

    Private Sub SaveShowChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesShowMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildShowFile())
    End Sub

    Private Function BuildShowFile() As Byte()
        Dim ShowLength As UInt16 = &H74
        Select Case ShowViewType.SelectedIndex
            Case 4
                ShowLength = &H7C
            Case 3
                ShowLength = &H78
        End Select
        Dim ReturnedBytes As Byte() = New Byte(&HC + ShowLength * DataGridShowView.RowCount - 1) {}
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        'copy the header byptes so they stay the same
        Array.Copy(ShowBytes, ReturnedBytes, &HC)
        ProgressBar1.Maximum = DataGridShowView.RowCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To DataGridShowView.RowCount - 1
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridShowView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 0, 4) 'String Ref
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 4, 2) 'Select 1
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(3).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 6, 2) 'Select 2
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(4).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 8, 2) 'A1
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(5).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 10, 2) 'A2
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(6).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 12, 2) 'B1
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(7).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 14, 2) 'B2
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(8).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 16, 2) 'B3
            If DataGridShowView.Rows(i).Cells(9).Value Then ReturnedBytes(&HC + i * ShowLength + 25) = 1 'C1
            If DataGridShowView.Rows(i).Cells(10).Value Then ReturnedBytes(&HC + i * ShowLength + 26) = 1 'C2
            If DataGridShowView.Rows(i).Cells(11).Value Then ReturnedBytes(&HC + i * ShowLength + 27) = 1 'C3
            If DataGridShowView.Rows(i).Cells(12).Value Then ReturnedBytes(&HC + i * ShowLength + 28) = 1 'C4
            If DataGridShowView.Rows(i).Cells(13).Value Then ReturnedBytes(&HC + i * ShowLength + 29) = 1 'C5
            If DataGridShowView.Rows(i).Cells(14).Value Then ReturnedBytes(&HC + i * ShowLength + 30) = 1 'C6
            If DataGridShowView.Rows(i).Cells(15).Value Then ReturnedBytes(&HC + i * ShowLength + 31) = 1 'C7
            If DataGridShowView.Rows(i).Cells(16).Value Then ReturnedBytes(&HC + i * ShowLength + 34) = 1 'C8
            ReturnedBytes(&HC + i * ShowLength + 35) = CByte(DataGridShowView.Rows(i).Cells(17).Value) 'Stage
            ReturnedBytes(&HC + i * ShowLength + 38) = CByte(DataGridShowView.Rows(i).Cells(18).Value) 'D1
            ReturnedBytes(&HC + i * ShowLength + 39) = CByte(DataGridShowView.Rows(i).Cells(19).Value) 'D2
            ReturnedBytes(&HC + i * ShowLength + 40) = CByte(DataGridShowView.Rows(i).Cells(20).Value) 'D3
            ReturnedBytes(&HC + i * ShowLength + 41) = CByte(DataGridShowView.Rows(i).Cells(21).Value) 'D4
            ReturnedBytes(&HC + i * ShowLength + 43) = CByte(DataGridShowView.Rows(i).Cells(22).Value) 'D5
            ReturnedBytes(&HC + i * ShowLength + 44) = CByte(DataGridShowView.Rows(i).Cells(23).Value) 'REF '24
            Array.Copy(GeneralTools.HexStringToByte(DataGridShowView.Rows(i).Cells(24).Value), 0, ReturnedBytes, &HC + i * ShowLength + 45, 6) 'Filter
            ReturnedBytes(&HC + i * ShowLength + 51) = &HFF '3 Bytes FF
            ReturnedBytes(&HC + i * ShowLength + 52) = &HFF
            ReturnedBytes(&HC + i * ShowLength + 53) = &HFF
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridShowView.Rows(i).Cells(25).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 56, 4) 'F1
            ReturnedBytes(&HC + i * ShowLength + 60) = CByte("&h" & DataGridShowView.Rows(i).Cells(26).Value) 'F2
            ReturnedBytes(&HC + i * ShowLength + 64) = CByte(DataGridShowView.Rows(i).Cells(27).Value) 'F3
            ReturnedBytes(&HC + i * ShowLength + 66) = CByte(DataGridShowView.Rows(i).Cells(28).Value) 'F4
            ReturnedBytes(&HC + i * ShowLength + 68) = CByte(DataGridShowView.Rows(i).Cells(29).Value) 'H1
            ReturnedBytes(&HC + i * ShowLength + 69) = CByte(DataGridShowView.Rows(i).Cells(30).Value) 'H2
            ReturnedBytes(&HC + i * ShowLength + 70) = CByte(DataGridShowView.Rows(i).Cells(31).Value) 'H3
            ReturnedBytes(&HC + i * ShowLength + 71) = CByte(DataGridShowView.Rows(i).Cells(32).Value) 'H4
            ReturnedBytes(&HC + i * ShowLength + 73) = CByte(DataGridShowView.Rows(i).Cells(33).Value) 'Bar
            Array.Copy(GeneralTools.HexStringToByte(DataGridShowView.Rows(i).Cells(34).Value), 0, ReturnedBytes, &HC + i * ShowLength + 74, 34) 'Unkown
            ReturnedBytes(&HC + i * ShowLength + 108) = &H70 '3 byte 70
            ReturnedBytes(&HC + i * ShowLength + 109) = &H70
            ReturnedBytes(&HC + i * ShowLength + 110) = &H70
            ReturnedBytes(&HC + i * ShowLength + 111) = CByte(DataGridShowView.Rows(i).Cells(35).Value) 'I1
            ReturnedBytes(&HC + i * ShowLength + 112) = CByte("&h" & DataGridShowView.Rows(i).Cells(36).Value) 'I2
            ReturnedBytes(&HC + i * ShowLength + 114) = CByte("&h" & DataGridShowView.Rows(i).Cells(37).Value) 'I3
            ReturnedBytes(&HC + i * ShowLength + 117) = CByte(DataGridShowView.Rows(i).Cells(38).Value) 'live
            ReturnedBytes(&HC + i * ShowLength + 118) = &HFF
            ReturnedBytes(&HC + i * ShowLength + 120) = CByte(DataGridShowView.Rows(i).Cells(39).Value) 'J
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function

#End Region

#Region "NIJB View Controls"

    'TO DO Build out other NIBJ Types
    Sub FillNIBJView(SelectedData As TreeNode)
        DataGridNIBJView.Rows.Clear()
        DataGridNIBJView.Columns.Clear()
        Dim NodeTag As ExtendedFileProperties = CType(SelectedData.Tag, ExtendedFileProperties)
        Dim NIJBBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim HeaderByte As Integer = BitConverter.ToInt32(NIJBBytes, &H4) '64 for Parm Light
        Dim LightCount As Integer = BitConverter.ToInt32(NIJBBytes, &H8)
        Dim ShowCount As Integer = BitConverter.ToInt32(NIJBBytes, &HC)
        Select Case HeaderByte
            Case &H64 'parameter file
                Dim Folder As String = Encoding.Default.GetChars(NIJBBytes, &H10, &H10)
                Dim Properties As String = Encoding.Default.GetChars(NIJBBytes, &H20, &H10)
                FileAttributesToolStripMenuItem.Text = Folder & " > " & Properties
                For i As Integer = 0 To LightCount - 1
                    Dim TempNewColumn As DataGridTextBoxColumn = New DataGridTextBoxColumn()
                    DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H30 + i * &H20, &H10))
                Next
                Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridNIBJView)
                Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
                ProgressBar1.Maximum = ShowCount - 1
                ProgressBar1.Value = 0
                For i As Integer = 0 To ShowCount - 1
                    DataGridNIBJView.Rows.Add()
                    For j As Integer = 0 To LightCount - 1
                        DataGridNIBJView(j, i).Value = Strings.Right(Hex(BitConverter.ToInt32(NIJBBytes, &H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadRight(8, "0"), 8)
                        DataGridNIBJView.Rows(i).Cells(j).Style.BackColor = ColorTranslator.FromHtml("#" & (DataGridNIBJView(j, i).Value.ToString.Substring(2, 6)))
                        Dim FontColor As String = Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 2)).PadLeft(2, "0") &
                            Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 1)).PadLeft(2, "0") &
                            Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadLeft(2, "0")
                        DataGridNIBJView.Rows(i).Cells(j).Style.ForeColor = ColorTranslator.FromHtml("#" & FontColor)
                    Next
                    DataGridNIBJView.Rows(i).HeaderCell.Value = i.ToString
                    ProgressBar1.Value = i
                Next
            Case &H67
                FileAttributesToolStripMenuItem.Text = "Unreadable Type"
                If LightCount = 0 Then
                    DataGridNIBJView.Columns.Add("Color0", "Color0")
                Else
                    For i As Integer = 0 To LightCount - 1
                        DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H10 + i * &H40, &H10))
                    Next
                End If
            Case &H6A
                FileAttributesToolStripMenuItem.Text = "Unreadable Type"
                If LightCount = 0 Then
                    DataGridNIBJView.Columns.Add("Color0", "Color0")
                End If
        End Select
    End Sub

    Private Sub DataGridNIBJView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridNIBJView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridNIBJView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If Not GeneralTools.HexCheck(MyCell.Value) OrElse
            MyCell.Value.ToString.Length > 8 Then
            MyCell.Value = OldValue
        Else
            If MyCell.Value.ToString.Length < 8 Then
                MyCell.Value = MyCell.Value.ToString.PadRight(8, "0")
            End If
            MyCell.Style.BackColor = ColorTranslator.FromHtml("#" & (MyCell.Value.ToString.Substring(2, 6)))
            Dim FontColor As Color = ColorTranslator.FromHtml("#" & Hex(&HFF - MyCell.Style.BackColor.R).PadLeft(2, "0") &
                                                              Hex(&HFF - MyCell.Style.BackColor.G).PadLeft(2, "0") &
                                                              Hex(&HFF - MyCell.Style.BackColor.B).PadLeft(2, "0"))
            MyCell.Style.ForeColor = FontColor
            SavePending = True
            SaveChangesNIBJMenuItem.Visible = True
        End If
    End Sub

    Private Sub SaveNIBJChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesNIBJMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildNIBJFile())
    End Sub

    Private Function BuildNIBJFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(ShowBytes.Length - 1) {}
        Dim LightCount As Integer = DataGridNIBJView.ColumnCount
        Dim ShowCount As Integer = DataGridNIBJView.RowCount
        Array.Copy(ShowBytes, ReturnedBytes, &H30 + LightCount * &H20)
        For i As Integer = 0 To LightCount - 1
            For j As Integer = 0 To ShowCount - 1
                Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridNIBJView.Rows(j).Cells(i).Value)), 0,
                ReturnedBytes, &H30 + (LightCount * &H20) + (i * ShowCount * 4) + (j * 4), 4)
            Next
        Next
        Return ReturnedBytes
    End Function

#End Region

#Region "Picture View Controls"

    Dim CreatedImages As List(Of String)

    Sub FillPictureView(SelectedData As TreeNode)
        Dim PictureBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim ImageStream As MemoryStream = New MemoryStream(PictureBytes)
        Dim TempName As String = Path.GetTempFileName
        FileSystem.Rename(TempName, TempName + ".dds")
        TempName += ".dds"
        File.WriteAllBytes(TempName, PictureBytes)
        Process.Start(My.Settings.TexConvPath, " -ft bmp " & TempName).WaitForExit()
        Dim TempBMP As String = Path.GetDirectoryName(My.Settings.TexConvPath) &
                                Path.DirectorySeparatorChar &
                                Path.GetFileNameWithoutExtension(TempName) & ".BMP"
        Dim TempBMPLocal As String = Application.StartupPath & Path.DirectorySeparatorChar &
                                Path.GetFileNameWithoutExtension(TempName) & ".BMP"
        If File.Exists(TempBMPLocal) Then
            File.Copy(TempBMPLocal, TempBMP, True)
            File.Delete(TempBMPLocal)
        End If
        If File.Exists(TempBMP) Then
            Dim tempimage As Image
            Using TempObject = New Bitmap(TempBMP)
                tempimage = New Bitmap(TempObject)
            End Using
            PictureBox2.Image = tempimage
        Else
            MessageBox.Show("Error creating bitmap image.")
        End If
        CreatedImages.Add(TempBMP)
        File.Delete(TempName)
    End Sub

    Sub DeleteTempImages()
        PictureBox2.Image = Nothing
        For Each CurrentImage As String In CreatedImages
            Try
                If File.Exists(CurrentImage) Then
                    File.Delete(CurrentImage)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Next
        CreatedImages.Clear()
    End Sub

#End Region

#Region "Object View"

    Dim CompleteYobjBytes As Byte()
    Dim SelectedObjHeader As ObjectHeaderInformation

    Sub FillObjectViews(SelectedData As TreeNode)
        CompleteYobjBytes = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        FillObjectHeaderView()
        FillObjectBoneView()
        FillObjectTextureView()
        FillObjectShaderView()
        FillObjectEmoteComboBox()
        FillSubObjectViews(DataGridObjectView.Rows(0).Tag)
    End Sub
    'CompleteYobjBytes Header
    '0x0 JBOY / YOBJ
    '0x4 Offset to Pof0 (Yobj Size)
    '0x8 &H10
    '0xC Length of Yobj Size
    '0x10 Note 07 or 03?
    '0x14 Filler?
    '0x18 SubItemCount
    '0x1C HeaderStartOffset
    '0x20 BoneCount
    '0x24 TextureCount
    '0x28 BonesStartOffset
    '0x2C TextureStartOffset
    '0x30 ShaderStartOffset
    '0x34 ShaderCount
    '0x38 Filler? 00
    '0x3C Filler? 00
    '0x40 Filler? 00
    '0x44 Filler? 00
    '0x48 EmoteNameCount
    '0x4C EmoteNameStartOffset
    '0x50 BoneStructureCount
    '0x54 BoneStructureStartOffset

#Region "Header Read"

    Public Class ObjectHeaderInformation
        Public IndexCount As UInt32 = 0
        Public VertCount As UInt32 = 0
        Public Rendered As Boolean = False
        Public FillerBytes As Byte() = New Byte() {}
        Public FillerString As String = ""
        Public WeightCount As UInt32 = 0
        Public UnknownA As UInt32 = 0
        Public VertHeaderCount As UInt32 = 0
        Public OffsetVertex As UInt32 = 0
        Public OffsetWeight As UInt32 = 0
        Public OffsetUV As UInt32 = 0
        Public OffsetNormals As UInt32 = 0
        Public UnknownB As UInt32 = 0
        Public ShaderBytes As Byte() = New Byte() {}
        Public ShaderString As String = ""
        Public RemainingBytes As Byte() = New Byte() {}
        Public RemainingString As String = ""
        Public UnknownC As UInt32 = 0
        Public MaterialIndex As UInt32 = 0
        Public CountParameter As UInt32 = 0
        Public OffsetParameter As UInt32 = 0
        Public OffsetFaces As UInt32 = 0
        Public CountUV As UInt32 = 0
        Public UnknownD As UInt32 = 0
        Public UnknownE As UInt32 = 0
        Public UnknownF As UInt32 = 0
        Public UnknownG As UInt32 = 0
        Public UnknownH As UInt32 = 0
    End Class

    Dim DefaultFill As String = "00 00 00 00 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF"

    Sub FillObjectHeaderView()
        'We need to show what type of position file it is
        Dim SubItemCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H18, 4), 0)
        Dim HeaderStartOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H1C, 4), 0) + 8
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjectView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SubItemCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SubItemCount - 1
            Dim TempObjectBytes As Byte() = New Byte(&HB8 - 1) {}
            Array.Copy(CompleteYobjBytes, HeaderStartOffset + i * &HB8, TempObjectBytes, 0, &HB8)
            Dim TempObjectHeaderInformation As ObjectHeaderInformation = ParseBytesToObjectHeaderInformation(TempObjectBytes)
            TempObjectHeaderInformation.IndexCount = i
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectVertexCount)).Value = TempObjectHeaderInformation.VertCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectRendered)).Value = TempObjectHeaderInformation.Rendered
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectHeaderFiller)).Value = TempObjectHeaderInformation.FillerString
            If TempObjectHeaderInformation.FillerString = DefaultFill Then
                TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectHeaderFiller)).Style = ReadOnlyCellStyle
            End If
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectWeightNumber)).Value = TempObjectHeaderInformation.WeightCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownIntA)).Value = TempObjectHeaderInformation.UnknownA
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectVerHeaderCount)).Value = TempObjectHeaderInformation.VertHeaderCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectVerticeOffset)).Value = Hex(TempObjectHeaderInformation.OffsetVertex)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectWeightsOffset)).Value = Hex(TempObjectHeaderInformation.OffsetWeight)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUVOffset)).Value = Hex(TempObjectHeaderInformation.OffsetUV)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectNormalsOffset)).Value = Hex(TempObjectHeaderInformation.OffsetNormals)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectInternalNum)).Value = TempObjectHeaderInformation.UnknownB
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectHeaderShader)).Value = TempObjectHeaderInformation.ShaderString
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjecHeaderUnknownC)).Value = TempObjectHeaderInformation.UnknownC
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectMaterialIndex)).Value = TempObjectHeaderInformation.MaterialIndex
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectParameterCount)).Value = TempObjectHeaderInformation.CountParameter
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectParameterOffset)).Value = Hex(TempObjectHeaderInformation.OffsetParameter)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectFaceOffset)).Value = Hex(TempObjectHeaderInformation.OffsetFaces)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUVCount)).Value = TempObjectHeaderInformation.CountUV
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownD)).Value = TempObjectHeaderInformation.UnknownD
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownE)).Value = TempObjectHeaderInformation.UnknownE
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownF)).Value = Hex(TempObjectHeaderInformation.UnknownF)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownG)).Value = Hex(TempObjectHeaderInformation.UnknownG)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownH)).Value = Hex(TempObjectHeaderInformation.UnknownH)
            TempGridRow.Tag = TempObjectHeaderInformation
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Function ParseBytesToObjectHeaderInformation(TestedByteArray As Byte()) As ObjectHeaderInformation
        Dim TempFillerBytes As Byte() = New Byte(&H54 - 1) {}
        Array.Copy(TestedByteArray, 8, TempFillerBytes, 0, &H54)
        Dim TempShaderBytes As Byte() = New Byte(&H10 - 1) {}
        Array.Copy(TestedByteArray, &H7C, TempShaderBytes, 0, &H10)
        Dim ReturnedObjecInfo As ObjectHeaderInformation = New ObjectHeaderInformation With {
           .VertCount = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, 0, 4), 0),
           .Rendered = BitConverter.ToBoolean(TestedByteArray, 4 + 3),
           .FillerBytes = TempFillerBytes,
           .FillerString = BitConverter.ToString(TempFillerBytes, 0).Replace("-", " "),
           .WeightCount = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H5C, 4), 0),
           .UnknownA = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H60, 4), 0),
           .VertHeaderCount = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H64, 4), 0),
           .OffsetVertex = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H68, 4), 0),
           .OffsetWeight = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H6C, 4), 0),
           .OffsetUV = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H70, 4), 0),
           .OffsetNormals = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H74, 4), 0),
           .UnknownB = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H78, 4), 0),
           .ShaderBytes = TempShaderBytes,
           .ShaderString = Encoding.Default.GetString(TempShaderBytes).TrimEnd(Chr(0)),
           .UnknownC = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H8C, 4), 0),
           .MaterialIndex = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H90, 4), 0),
           .CountParameter = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H94, 4), 0),
           .OffsetParameter = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H98, 4), 0),
           .OffsetFaces = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H9C, 4), 0),
           .CountUV = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &HA0, 4), 0),
           .UnknownD = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &HA4, 4), 0),
           .UnknownE = BitConverter.ToUInt32(TestedByteArray, &HA8),
           .UnknownF = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &HAC, 4), 0),
           .UnknownG = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &HB0, 4), 0),
           .UnknownH = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &HB4, 4), 0)
           }
        Return ReturnedObjecInfo
    End Function

#Region "Bone Information"

    Public Class ObjectBoneInformation
        Public IndexCount As UInt32 = 0
        Public StructureOrder As UInt32 = 0
        Public NameBytes As Byte() = New Byte() {}
        Public Name As String = ""
        Public UnknownA As UInt32 = 0
        Public UnknownB As UInt32 = 0
        Public UnknownC As UInt32 = 0
        Public UnknownD As UInt32 = 0
        Public UnknownE As UInt32 = 0
        Public UnknownF As UInt32 = 0
        Public UnknownG As UInt32 = 0
        Public UnknownH As UInt32 = 0
        Public UnknownI As UInt32 = 0
        Public UnknownJ As UInt32 = 0
        Public UnknownK As UInt32 = 0
        Public UnknownL As UInt32 = 0
        Public UnknownM As UInt32 = 0
        Public UnknownN As Single = 0
        Public UnknownO As Single = 0
        Public UnknownP As Single = 0
    End Class

    Sub FillObjectBoneView()
        Dim BoneCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H20, 4), 0)
        Dim BonesStartOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H28, 4), 0) + 8
        'Bones List
        Dim ClonedBoneRow As DataGridViewRow = ClearandGetClone(DataGridObjectBoneView)
        Dim WorkingBoneCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = BoneCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To BoneCount - 1
            Dim TempObjectBoneBytes As Byte() = New Byte(&H50 - 1) {}
            Array.Copy(CompleteYobjBytes, BonesStartOffset + i * &H50, TempObjectBoneBytes, 0, &H50)
            Dim TempObjectBoneInformation As ObjectBoneInformation = ParseBytestoObjectBoneInformation(TempObjectBoneBytes)
            TempObjectBoneInformation.IndexCount = i
            Dim TempBoneGridRow As DataGridViewRow = ClonedBoneRow.Clone()
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneCountCol)).Value = i
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneCountCol)).Style = ReadOnlyCellStyle
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneName)).Value = TempObjectBoneInformation.Name
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownA)).Value = TempObjectBoneInformation.UnknownA
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownB)).Value = TempObjectBoneInformation.UnknownB
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownC)).Value = TempObjectBoneInformation.UnknownC
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownD)).Value = TempObjectBoneInformation.UnknownD
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownE)).Value = TempObjectBoneInformation.UnknownE
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownF)).Value = TempObjectBoneInformation.UnknownF
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownG)).Value = TempObjectBoneInformation.UnknownG
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownH)).Value = TempObjectBoneInformation.UnknownH
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownI)).Value = TempObjectBoneInformation.UnknownI
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownJ)).Value = TempObjectBoneInformation.UnknownJ
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownK)).Value = TempObjectBoneInformation.UnknownK
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownL)).Value = TempObjectBoneInformation.UnknownL
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownM)).Value = TempObjectBoneInformation.UnknownM
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownN)).Value = TempObjectBoneInformation.UnknownN
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownO)).Value = TempObjectBoneInformation.UnknownO
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownP)).Value = TempObjectBoneInformation.UnknownP
            TempBoneGridRow.Tag = TempObjectBoneInformation
            WorkingBoneCollection.Add(TempBoneGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectBoneView.Rows.AddRange(WorkingBoneCollection.ToArray())
        Dim BoneStructureCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H50, 4), 0)
        Dim BoneStructureStartOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H54, 4), 0) + 8
        'Bone int List
        Dim RevisedBoneStructureCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, BoneStructureStartOffset, 4), 0)
        ProgressBar1.Maximum = RevisedBoneStructureCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To RevisedBoneStructureCount - 1
            Dim TemporaryBoneNumber As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, BoneStructureStartOffset + 8 + i * 4, 4), 0)
            DataGridObjectBoneView.Rows(TemporaryBoneNumber).Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneOrder)).Value = i + 1
            DataGridObjectBoneView.Rows(TemporaryBoneNumber).Tag.StructureOrder = i + 1
        Next
    End Sub

    Function ParseBytestoObjectBoneInformation(TestedByteArray As Byte()) As ObjectBoneInformation
        Dim TempBoneName As Byte() = New Byte(&H10 - 1) {}
        Array.Copy(TestedByteArray, 0, TempBoneName, 0, &H10)
        Dim ReturnedBoneInfo As ObjectBoneInformation = New ObjectBoneInformation With {
            .NameBytes = TempBoneName,
            .Name = Encoding.Default.GetString(TempBoneName).TrimEnd(Chr(0)),
            .UnknownA = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H10, 4), 0),
            .UnknownB = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H14, 4), 0),
            .UnknownC = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H18, 4), 0),
            .UnknownD = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H1C, 4), 0),
            .UnknownE = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H20, 4), 0),
            .UnknownF = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H24, 4), 0),
            .UnknownG = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H28, 4), 0),
            .UnknownH = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H2C, 4), 0),
            .UnknownI = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H30, 4), 0),
            .UnknownJ = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H34, 4), 0),
            .UnknownK = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H38, 4), 0),
            .UnknownL = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H3C, 4), 0),
            .UnknownM = BitConverter.ToUInt32(TestedByteArray, &H40),
            .UnknownN = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &H44, 4), 0),
            .UnknownO = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &H48, 4), 0),
            .UnknownP = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &H4C, 4), 0)}
        Return ReturnedBoneInfo
    End Function

#End Region

#Region "Texture & Shader Info"

    Sub FillObjectTextureView()
        Dim TextureCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H24, 4), 0)
        Dim TextureStartOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H2C, 4), 0) + 8
        'Texture List
        Dim ClonedTextureRow As DataGridViewRow = ClearandGetClone(DataGridObjectTextureView)
        Dim WorkingTextureCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = TextureCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To TextureCount - 1
            Dim TempObjectTextureBytes As Byte() = New Byte(&H10 - 1) {}
            Array.Copy(CompleteYobjBytes, TextureStartOffset + i * &H10, TempObjectTextureBytes, 0, &H10)
            Dim TempTextureGridRow As DataGridViewRow = ClonedTextureRow.Clone()
            TempTextureGridRow.Cells(DataGridObjectTextureView.Columns.IndexOf(ObjectTextureCount)).Value = i
            TempTextureGridRow.Cells(DataGridObjectTextureView.Columns.IndexOf(ObjectTextureCount)).Style = ReadOnlyCellStyle
            TempTextureGridRow.Cells(DataGridObjectTextureView.Columns.IndexOf(ObjectTextureCol)).Value = Encoding.Default.GetString(TempObjectTextureBytes).TrimEnd(Chr(0))
            WorkingTextureCollection.Add(TempTextureGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectTextureView.Rows.AddRange(WorkingTextureCollection.ToArray())
    End Sub

    Sub FillObjectShaderView()
        Dim ShaderCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H34, 4), 0)
        Dim ShaderStartOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H30, 4), 0) + 8
        'Shader List
        Dim ClonedShaderRow As DataGridViewRow = ClearandGetClone(DataGridObjectShaderView)
        Dim WorkingShaderCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = ShaderCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To ShaderCount - 1
            Dim TempObjectShaderBytes As Byte() = New Byte(&H20 - 1) {}
            Array.Copy(CompleteYobjBytes, ShaderStartOffset + i * &H20, TempObjectShaderBytes, 0, &H20)
            Dim TempShaderGridRow As DataGridViewRow = ClonedShaderRow.Clone()
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderCount)).Value = i
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderCount)).Style = ReadOnlyCellStyle
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderCol)).Value = Encoding.Default.GetString(TempObjectShaderBytes, 0, &H10).TrimEnd(Chr(0))
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderType)).Value = BitConverter.ToUInt32(GeneralTools.EndianReverse(TempObjectShaderBytes, &H10, 4), 0)
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderB)).Value = BitConverter.ToUInt32(GeneralTools.EndianReverse(TempObjectShaderBytes, &H18, 4), 0)
            WorkingShaderCollection.Add(TempShaderGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectShaderView.Rows.AddRange(WorkingShaderCollection.ToArray())
    End Sub

#End Region

#Region "Emote Selector"
    Sub FillObjectEmoteComboBox()
        Dim EmoteNameCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H48, 4), 0)
        Dim EmoteNameStartOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, &H4C, 4), 0) + 8
        ObjectEmoteListComboBox.Items.Clear()
        If EmoteNameCount > 0 Then
            Dim EmoteList As List(Of String) = New List(Of String)
            EmoteList.Add("Neutral")
            ProgressBar1.Maximum = EmoteNameCount - 1
            ProgressBar1.Value = 0
            For i As Integer = 0 To EmoteNameCount - 1
                EmoteList.Add(Encoding.Default.GetString(CompleteYobjBytes, EmoteNameStartOffset + i * &H10, &H10).TrimEnd(Chr(0)))
            Next
            ObjectEmoteListComboBox.Items.AddRange(EmoteList.ToArray())
            ObjectEmoteListComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub ObjectEmoteListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ObjectEmoteListComboBox.SelectedIndexChanged
        If Not IsNothing(SelectedObjHeader) Then
            If Not SelectedObjHeader.VertHeaderCount = 1 Then
                FillVertexView()
            End If
        End If
    End Sub
#End Region

#End Region

#Region "Individual Object"

    Sub FillSubObjectViews(SelectedObject As ObjectHeaderInformation)
        SelectedObjHeader = SelectedObject
        LoadedObjectToolStripMenuItem.Text = "Loaded Object: " & SelectedObjHeader.IndexCount
        FillVertexView()
        FillWeightsColumns()
        FillNormalColumns()
        GetVertexViewDisplayedColumns()
        FillUVsView()
        FillFacesView()
        FillParamsView()
    End Sub
#End Region

#Region "Vertex View Info"

#Region "Main Vertex Data"
    Public Class ObjectVertex
        Public IndexCount As UInt32 = 0
        Public X As Single = 0
        Public Y As Single = 0
        Public Z As Single = 0
        Public RX As Single = 0
        Public RY As Single = 0
        Public RZ As Single = 0
        Public UnknownFooter As Single = 0
    End Class

    Sub FillVertexView()
        'Clearing Previously generated as  weights
        If DataGridObjectVertexView.Columns.Count > DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 Then
            For i As Integer = DataGridObjectVertexView.Columns.Count - 1 To DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 Step -1
                DataGridObjectVertexView.Columns.RemoveAt(i)
            Next
        End If
        'Vertices
        Dim ClonedVertexRow As DataGridViewRow = ClearandGetClone(DataGridObjectVertexView)
        Dim WorkingVertexCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SelectedObjHeader.VertCount - 1
        ProgressBar1.Value = 0
        Dim FirstObjectIndex As UInt32 = 0
        If SelectedObjHeader.VertHeaderCount = 1 Then
            FirstObjectIndex = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetVertex + 8, 4), 0) + 8
        Else
            FirstObjectIndex = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetVertex + 8 + ObjectEmoteListComboBox.SelectedIndex * 4, 4), 0) + 8
        End If
        For i As Integer = 0 To SelectedObjHeader.VertCount - 1
            Dim TempObjectVertexBytes As Byte() = New Byte(&H1C - 1) {}
            Array.Copy(CompleteYobjBytes, FirstObjectIndex + i * &H1C, TempObjectVertexBytes, 0, &H1C)
            Dim TempObjectVertex As ObjectVertex = ParseBytestoObjectObjectVertex(TempObjectVertexBytes)
            TempObjectVertex.IndexCount = i + 1
            Dim TempVertexGridRow As DataGridViewRow = ClonedVertexRow.Clone()
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertCountCol)).Value = TempObjectVertex.IndexCount
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertCountCol)).Style = ReadOnlyCellStyle
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertX)).Value = TempObjectVertex.X
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertY)).Value = TempObjectVertex.Y
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertZ)).Value = TempObjectVertex.Z
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertRX)).Value = TempObjectVertex.RX
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertRY)).Value = TempObjectVertex.RY
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertRZ)).Value = TempObjectVertex.RZ
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertUnknown)).Value = TempObjectVertex.UnknownFooter
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertUnknown)).Style = ReadOnlyCellStyle
            TempVertexGridRow.Tag = TempObjectVertex
            WorkingVertexCollection.Add(TempVertexGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectVertexView.Rows.AddRange(WorkingVertexCollection.ToArray())
    End Sub

    Function ParseBytestoObjectObjectVertex(TestedByteArray As Byte()) As ObjectVertex
        Dim ReturnedVertexInfo As ObjectVertex = New ObjectVertex With {
            .X = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, 0, 4), 0),
            .Z = -BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, 4, 4), 0),
            .Y = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, 8, 4), 0),
            .RX = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &HC, 4), 0),
            .RZ = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &H10, 4), 0),
            .RY = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &H14, 4), 0),
            .UnknownFooter = BitConverter.ToSingle(GeneralTools.EndianReverse(TestedByteArray, &H18, 4), 0)}
        Return ReturnedVertexInfo
    End Function

#End Region

#Region "Weights Info"

    Sub FillWeightsColumns()
        'For the weights I want to try adding columns at the end of the vertex data grid
        For i As Integer = 0 To SelectedObjHeader.WeightCount - 1
            DataGridObjectVertexView.Columns.Add("VertWeightNum" & i, "Weight Num " & i)
            DataGridObjectVertexView.Columns.Add("VertWeightSingle" & i, "Weight Single " & i)
        Next

        ProgressBar1.Maximum = SelectedObjHeader.VertCount - 1
        ProgressBar1.Value = 0
        Dim StartingColumnNumber As Integer = DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3)
        For iCurrentVert As Integer = 0 To SelectedObjHeader.VertCount - 1
            For jCurrentWeight As Integer = 0 To SelectedObjHeader.WeightCount - 1
                Dim StartingOffset As UInt32 = SelectedObjHeader.OffsetWeight + 8 + (iCurrentVert * 8 * SelectedObjHeader.WeightCount) + (jCurrentWeight * 8)
                DataGridObjectVertexView.Rows(iCurrentVert).Cells(StartingColumnNumber + 1 + jCurrentWeight * 2).Value =
                    BitConverter.ToUInt32(CompleteYobjBytes, StartingOffset)
                DataGridObjectVertexView.Rows(iCurrentVert).Cells(StartingColumnNumber + 2 + jCurrentWeight * 2).Value =
                    BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, StartingOffset + 4, 4), 0)
            Next
            ProgressBar1.Value = iCurrentVert
        Next

        '8 weights # per vert

    End Sub



    Private Sub ShowWeightsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowWeightsToolStripMenuItem.Click
        If ShowWeightsToolStripMenuItem.Text.Contains("☑") Then
            ShowWeightsToolStripMenuItem.Text = "☐ Show Weights"
        ElseIf ShowWeightsToolStripMenuItem.Text.Contains("☐") Then
            '☐
            ShowWeightsToolStripMenuItem.Text = "☑ Show Weights"
        End If
        GetVertexViewDisplayedColumns()
    End Sub

#End Region

#Region "Normals Columns"

    Private Sub FillNormalColumns()
        ProgressBar1.Maximum = SelectedObjHeader.VertCount - 1
        ProgressBar1.Value = 0
        For iCurrentVert As Integer = 0 To SelectedObjHeader.VertCount - 1
            DataGridObjectVertexView.Rows(iCurrentVert).Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal1)).Value =
                    BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetNormals + 8 + iCurrentVert * &HC, 4), 0)
            DataGridObjectVertexView.Rows(iCurrentVert).Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal2)).Value =
                    BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetNormals + 8 + iCurrentVert * &HC + 4, 4), 0)
            DataGridObjectVertexView.Rows(iCurrentVert).Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3)).Value =
                    BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetNormals + 8 + iCurrentVert * &HC + 8, 4), 0)
            ProgressBar1.Value = iCurrentVert
        Next
    End Sub

    Private Sub ShowNormalsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowNormalsToolStripMenuItem.Click
        If ShowNormalsToolStripMenuItem.Text.Contains("☑") Then
            ShowNormalsToolStripMenuItem.Text = "☐ Show Normals"
        ElseIf ShowNormalsToolStripMenuItem.Text.Contains("☐") Then
            '☐
            ShowNormalsToolStripMenuItem.Text = "☑ Show Normals"
        End If
        GetVertexViewDisplayedColumns()
    End Sub
#End Region

    Sub GetVertexViewDisplayedColumns()
        For i As Integer = 0 To DataGridObjectVertexView.Columns.GetColumnCount(DataGridViewElementStates.None) - 1
            DataGridObjectVertexView.Columns(i).Visible = True
        Next
        If ShowWeightsToolStripMenuItem.Text.Contains("☑") Then
        Else
            For i As Integer = DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 To DataGridObjectVertexView.Columns.Count - 1
                DataGridObjectVertexView.Columns(i).Visible = False
            Next
        End If
        If ShowNormalsToolStripMenuItem.Text.Contains("☑") Then
        Else
            ObjectVertNormal1.Visible = False
            ObjectVertNormal2.Visible = False
            ObjectVertNormal3.Visible = False
        End If
    End Sub

#End Region

#Region "UV View"

    Sub FillUVsView()
        Dim ClonedRow As DataGridViewRow = ClearandGetClone(DataGridObjectUVView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SelectedObjHeader.CountUV - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SelectedObjHeader.CountUV - 1
            Dim TempGridRow As DataGridViewRow = ClonedRow.Clone()
            TempGridRow.Cells(DataGridObjectUVView.Columns.IndexOf(ObjectUVCurrentCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectUVView.Columns.IndexOf(ObjectUVCurrentCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectUVView.Columns.IndexOf(ObjectUVColumn1)).Value = BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetUV + 8 + i * 8, 4), 0)
            TempGridRow.Cells(DataGridObjectUVView.Columns.IndexOf(ObjectUVColumn2)).Value = BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetUV + 8 + 4 + i * 8, 4), 0)
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectUVView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

#End Region

#Region "Faces View"

    Sub FillFacesView()
        'DataGridObjectFacesView
        Dim ClonedRow As DataGridViewRow = ClearandGetClone(DataGridObjectFacesView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim PossibleFaceCount As Int32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetFaces + 8 + 4, 4), 0)
        ' MessageBox.Show(Hex(PossibleFaceCount))
        ProgressBar1.Maximum = PossibleFaceCount - 3
        ProgressBar1.Value = 0
        For i As Integer = 0 To PossibleFaceCount - 3
            Dim TempGridRow As DataGridViewRow = ClonedRow.Clone()
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceCurrentCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceCurrentCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceVertex1)).Value = BitConverter.ToUInt16(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetFaces + 8 + 12 + i * 2, 2), 0) + 1
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceVertex2)).Value = BitConverter.ToUInt16(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetFaces + 8 + 12 + i * 2 + 2, 2), 0) + 1
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceVertex3)).Value = BitConverter.ToUInt16(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetFaces + 8 + 12 + i * 2 + 4, 2), 0) + 1
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectFacesView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

#End Region

#Region "Parameters View"

    Sub FillParamsView()
        'DataGridObjectFacesView
        Dim ClonedRow As DataGridViewRow = ClearandGetClone(DataGridObjectParamView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SelectedObjHeader.CountParameter - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SelectedObjHeader.CountParameter - 1
            Dim CurrentParamOffset As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(CompleteYobjBytes, SelectedObjHeader.OffsetParameter + 8 + i * 4, 4), 0)
            Dim TempGridRow As DataGridViewRow = ClonedRow.Clone()
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamName)).Value = Encoding.Default.GetString(CompleteYobjBytes, CurrentParamOffset + 8, &H10)
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamInt1)).Value = BitConverter.ToUInt16(GeneralTools.EndianReverse(CompleteYobjBytes, CurrentParamOffset + 8 + &H10, 2), 0)
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamInt2)).Value = BitConverter.ToUInt16(GeneralTools.EndianReverse(CompleteYobjBytes, CurrentParamOffset + 8 + &H12, 2), 0)
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamSingle)).Value = BitConverter.ToSingle(GeneralTools.EndianReverse(CompleteYobjBytes, CurrentParamOffset + 8 + &H14, 4), 0)
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectParamView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

#End Region

#End Region

#Region "Attire Editor View"

    Sub FillAttireView(SelectedData As TreeNode)
        RemoveHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAttireView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim AttireBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim WrestlerCount As Integer = BitConverter.ToInt32(AttireBytes, &H8)
        Dim WrestlerPacs(WrestlerCount - 1) As Integer
        Dim AttireCount(WrestlerCount - 1) As Integer
        Dim AttireNames((WrestlerCount) * 10 - 1) As Integer
        Dim AttireEnabled((WrestlerCount) * 10 - 1) As Boolean
        Dim AttireManager((WrestlerCount) * 10 - 1) As Boolean
        Dim AttireUnlockNumber((WrestlerCount) * 10 - 1) As UInt32
        For i As Integer = 0 To WrestlerCount - 1
            WrestlerPacs(i) = BitConverter.ToUInt32(AttireBytes, &HC + &HA8 * i)
            AttireCount(i) = BitConverter.ToUInt32(AttireBytes, &H10 + &HA8 * i)
            For K As Integer = 0 To 9
                AttireNames(i * 10 + K) = BitConverter.ToUInt32(AttireBytes, &H14 + &HA8 * i + &H10 * K)
                AttireEnabled(i * 10 + K) = (AttireBytes(&H20 + &HA8 * i + &H10 * K) = &HFF)
                AttireManager(i * 10 + K) = (AttireBytes(&H20 + &HA8 * i + &H10 * K) = &H2)
                AttireUnlockNumber(i * 10 + K) = BitConverter.ToUInt32(AttireBytes, &H18 + &HA8 * i + &H10 * K)
            Next
        Next
        If StringRead Then 'True
            'Hide Strings
            DataGridAttireView.Columns(3).Visible = True
            DataGridAttireView.Columns(8).Visible = True
            DataGridAttireView.Columns(13).Visible = True
            DataGridAttireView.Columns(18).Visible = True
            DataGridAttireView.Columns(23).Visible = True
            DataGridAttireView.Columns(28).Visible = True
            DataGridAttireView.Columns(33).Visible = True
            DataGridAttireView.Columns(38).Visible = True
            DataGridAttireView.Columns(43).Visible = True
            DataGridAttireView.Columns(48).Visible = True
            If PacsRead Then 'Strings and Pacs Read
            Else 'Strings Read Only

            End If
        Else 'Pacs Read Only can't do much
            'Hide Strings
            DataGridAttireView.Columns(3).Visible = False
            DataGridAttireView.Columns(8).Visible = False
            DataGridAttireView.Columns(13).Visible = False
            DataGridAttireView.Columns(18).Visible = False
            DataGridAttireView.Columns(23).Visible = False
            DataGridAttireView.Columns(28).Visible = False
            DataGridAttireView.Columns(33).Visible = False
            DataGridAttireView.Columns(38).Visible = False
            DataGridAttireView.Columns(43).Visible = False
            DataGridAttireView.Columns(48).Visible = False
        End If
        ProgressBar1.Maximum = WrestlerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To WrestlerCount - 1
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = WrestlerPacs(i)
            TempGridRow.Cells(1).Value = AttireCount(i)
            TempGridRow.Cells(2).Value = Hex(AttireNames(i * 10 + 0))
            TempGridRow.Cells(3).Value = StringReferences(AttireNames(i * 10 + 0))
            TempGridRow.Cells(4).Value = AttireEnabled(i * 10 + 0)
            TempGridRow.Cells(5).Value = AttireManager(i * 10 + 0)
            TempGridRow.Cells(6).Value = AttireUnlockNumber(i * 10 + 0)
            TempGridRow.Cells(7).Value = Hex(AttireNames(i * 10 + 1))
            TempGridRow.Cells(8).Value = StringReferences(AttireNames(i * 10 + 1))
            TempGridRow.Cells(9).Value = AttireEnabled(i * 10 + 1)
            TempGridRow.Cells(10).Value = AttireManager(i * 10 + 1)
            TempGridRow.Cells(11).Value = AttireUnlockNumber(i * 10 + 1)
            TempGridRow.Cells(12).Value = Hex(AttireNames(i * 10 + 2))
            TempGridRow.Cells(13).Value = StringReferences(AttireNames(i * 10 + 2))
            TempGridRow.Cells(14).Value = AttireEnabled(i * 10 + 2)
            TempGridRow.Cells(15).Value = AttireManager(i * 10 + 2)
            TempGridRow.Cells(16).Value = AttireUnlockNumber(i * 10 + 2)
            TempGridRow.Cells(17).Value = Hex(AttireNames(i * 10 + 3))
            TempGridRow.Cells(18).Value = StringReferences(AttireNames(i * 10 + 3))
            TempGridRow.Cells(19).Value = AttireEnabled(i * 10 + 3)
            TempGridRow.Cells(20).Value = AttireManager(i * 10 + 3)
            TempGridRow.Cells(21).Value = AttireUnlockNumber(i * 10 + 3)
            TempGridRow.Cells(22).Value = Hex(AttireNames(i * 10 + 4))
            TempGridRow.Cells(23).Value = StringReferences(AttireNames(i * 10 + 4))
            TempGridRow.Cells(24).Value = AttireEnabled(i * 10 + 4)
            TempGridRow.Cells(25).Value = AttireManager(i * 10 + 4)
            TempGridRow.Cells(26).Value = AttireUnlockNumber(i * 10 + 4)
            TempGridRow.Cells(27).Value = Hex(AttireNames(i * 10 + 5))
            TempGridRow.Cells(28).Value = StringReferences(AttireNames(i * 10 + 5))
            TempGridRow.Cells(29).Value = AttireEnabled(i * 10 + 5)
            TempGridRow.Cells(30).Value = AttireManager(i * 10 + 5)
            TempGridRow.Cells(31).Value = AttireUnlockNumber(i * 10 + 5)
            TempGridRow.Cells(32).Value = Hex(AttireNames(i * 10 + 6))
            TempGridRow.Cells(33).Value = StringReferences(AttireNames(i * 10 + 6))
            TempGridRow.Cells(34).Value = AttireEnabled(i * 10 + 6)
            TempGridRow.Cells(35).Value = AttireManager(i * 10 + 6)
            TempGridRow.Cells(36).Value = AttireUnlockNumber(i * 10 + 6)
            TempGridRow.Cells(37).Value = Hex(AttireNames(i * 10 + 7))
            TempGridRow.Cells(38).Value = StringReferences(AttireNames(i * 10 + 7))
            TempGridRow.Cells(39).Value = AttireEnabled(i * 10 + 7)
            TempGridRow.Cells(40).Value = AttireManager(i * 10 + 7)
            TempGridRow.Cells(41).Value = AttireUnlockNumber(i * 10 + 7)
            TempGridRow.Cells(42).Value = Hex(AttireNames(i * 10 + 8))
            TempGridRow.Cells(43).Value = StringReferences(AttireNames(i * 10 + 8))
            TempGridRow.Cells(44).Value = AttireEnabled(i * 10 + 8)
            TempGridRow.Cells(45).Value = AttireManager(i * 10 + 8)
            TempGridRow.Cells(46).Value = AttireUnlockNumber(i * 10 + 8)
            TempGridRow.Cells(47).Value = Hex(AttireNames(i * 10 + 9))
            TempGridRow.Cells(48).Value = StringReferences(AttireNames(i * 10 + 9))
            TempGridRow.Cells(49).Value = AttireEnabled(i * 10 + 9)
            TempGridRow.Cells(50).Value = AttireManager(i * 10 + 9)
            TempGridRow.Cells(51).Value = AttireUnlockNumber(i * 10 + 9)
            If i > 99 Then
                TempGridRow.HeaderCell.Value = "UNREAD"
            Else
                TempGridRow.HeaderCell.Value = ""
            End If
            ProgressBar1.Value = i
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridAttireView.Rows.AddRange(WorkingCollection.ToArray())
        AddHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
    End Sub

    Private Sub DataGridAttireView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAttireView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 0 Then 'Pac Number, Verify Number <= 1024
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
               CInt(MyCell.Value) > 1024 Then
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Attire Count, Verify Number <= 10
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
               CInt(MyCell.Value) > 10 Then
                MyCell.Value = OldValue
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 0 Then 'Attire Name
            If Not GeneralTools.HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf StringReferences(CUInt("&H" & MyCell.Value)) > &HFFFFF Then
                MyCell.Value = OldValue
            Else
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & MyCell.Value))
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 1 Then 'Attire String Does Nothing
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 2 Then 'Enabled Changed
            If MyCell.Value Then 'if enabled checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = False 'unchecks manager
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 3 Then 'Manager Changed
            If MyCell.Value Then 'if manager checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value = False 'unchecks enabled
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 4 Then 'UnlockMode Program Only
        End If
        SavePending = True
        SaveChangesAttireMenuItem.Visible = True
    End Sub

    Private Sub DataGridAttireView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If DataGridAttireView.RowCount > 1 Then
            Dim NewRow As DataGridViewRow = DataGridAttireView.Rows(e.RowIndex - 1)
            NewRow.Cells(0).Value = 0
            NewRow.Cells(0).ValueType = GetType(System.Int32)
            NewRow.Cells(1).Value = 10
            For i As Integer = 0 To 9
                NewRow.Cells(2 + i * 5).Value = Hex(&H50A2 + i)
                NewRow.Cells(2 + i * 5 + 1).Value = StringReferences(&H50A2 + i)
                NewRow.Cells(2 + i * 5 + 2).Value = True
                NewRow.Cells(2 + i * 5 + 3).Value = False
                NewRow.Cells(2 + i * 5 + 4).Value = UInt32.MaxValue
            Next
            If e.RowIndex > 99 Then
                NewRow.HeaderCell.Value = "UNREAD"
            Else
                NewRow.HeaderCell.Value = ""
            End If
        End If
    End Sub

    Private Sub DataGridAttireView_Sorted(sender As Object, e As EventArgs) Handles DataGridAttireView.Sorted
        If DataGridAttireView.RowCount > 100 Then
            For i As Integer = 0 To 99
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            Next
            For i As Integer = 100 To DataGridAttireView.RowCount - 1
                DataGridAttireView.Rows(i).HeaderCell.Value = "UNREAD"
            Next
        Else
            For i As Integer = 0 To DataGridAttireView.RowCount - 1
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            Next
        End If
    End Sub

    Private Sub SaveAttireChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesAttireMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildAttireFile())
    End Sub

    Private Function BuildAttireFile() As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&HC + ((DataGridAttireView.RowCount - 1) * &HA8) - 1) {}
        'COS
        ReturnedBytes(0) = &H43
        ReturnedBytes(1) = &H4F
        ReturnedBytes(2) = &H53
        ReturnedBytes(4) = &H1
        ReturnedBytes(8) = DataGridAttireView.RowCount - 1 ' subtract 1 because of the added
        For i As Integer = 0 To DataGridAttireView.RowCount - 2 '2 to skip the added row
            'PacNumber
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &HC + i * &HA8, 4)
            'Attire Count
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(1).Value)), 0, ReturnedBytes, &HC + i * &HA8 + 4, 4)
            For K As Integer = 0 To 9
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridAttireView.Rows(i).Cells(2 + K * 5).Value.ToString))), 0, ReturnedBytes, &HC + i * &HA8 + K * &H10 + 8, 4)
                'Unlock Info
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(6 + K * 5).Value.ToString)), 0, ReturnedBytes, &HC + i * &HA8 + K * &H10 + 12, 4)
                If DataGridAttireView.Rows(i).Cells(4 + K * 5).Value Then
                    ReturnedBytes(&HC + i * &HA8 + K * &H10 + 20) = &HFF
                ElseIf DataGridAttireView.Rows(i).Cells(5 + K * 5).Value Then
                    ReturnedBytes(&HC + i * &HA8 + K * &H10 + 20) = 2
                End If
            Next
        Next
        Return ReturnedBytes
    End Function

#End Region

#Region "Muscle View Controls"

    'TO DO Streamline integration with Object models whenever Object viewer is actually built.
    Sub FillMuscleView(SelectedData As TreeNode)
        Dim MuscleBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        If DataGridMuscleView.ColumnCount = 0 Then
            DataGridMuscleView.Columns.Add("Name", "Name")
            DataGridMuscleView.Columns.Add("Number1", Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        Else
            DataGridMuscleView.Columns.Add("Number" & DataGridMuscleView.ColumnCount, Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        End If
        Dim MuscleCount As Integer = BitConverter.ToInt32(MuscleBytes, &HC)
        Dim ActiveIndex As Long = &H14
        For i As Integer = 0 To MuscleCount - 1
            Dim MuscleName As String = Encoding.ASCII.GetString(MuscleBytes, ActiveIndex + 4, &H20)
            ActiveIndex = ActiveIndex + BitConverter.ToInt32(MuscleBytes, ActiveIndex)
            If DataGridMuscleView.ColumnCount = 2 Then
                DataGridMuscleView.Rows.Add(MuscleName, i)
            Else
                If GetMuscleRow(MuscleName) = -1 Then
                    DataGridMuscleView.Rows.Add(MuscleName)
                    DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, DataGridMuscleView.RowCount - 1).Value = i
                Else
                    DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, GetMuscleRow(MuscleName)).Value = i
                End If
            End If
        Next
    End Sub

    Function GetMuscleRow(MuscleName As String)
        For i As Integer = 0 To DataGridMuscleView.RowCount - 1
            If DataGridMuscleView(0, i).Value = MuscleName Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If TabControl1.TabPages.Contains(MuscleView) Then
            TabControl1.SelectedIndex = 0
            TabControl1.TabPages.Remove(MuscleView)
        End If
    End Sub

#End Region

#Region "Mask View Controls"

    Sub FillMaskView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMaskView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim MaskBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim MaskHeader As Integer = BitConverter.ToInt32(MaskBytes, &H4) ' should be C
        If Not MaskHeader = &HC Then
            MessageBox.Show("Unknown error with CE header")
            Exit Sub
        End If
        Dim ActiveIndex As Long = MaskHeader
        Dim ContainerCount As Integer = BitConverter.ToInt32(MaskBytes, &H8) ' Should be 2
        If ContainerCount = 0 Then
            MessageBox.Show("CE contains no masks")
            Exit Sub
        End If
        Dim CurrentMaskName As String
        '-
        'First we have to process each container, these are the M_Head and M_Body containers
        '-
        For i As Integer = 0 To ContainerCount - 1 'process each mask individuallt head then body
            Dim MaskHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex) ' should start as offset &hC, should also be &H4c
            CurrentMaskName = System.Text.Encoding.ASCII.GetString(MaskBytes, ActiveIndex + &H8, 6)
            'BitConverter.ToString(mask_array, active_offset + &H8)
            Dim MaskLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H4) ' should be &h4c if mask masks no objects
            Dim MaskedParts As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H48)
            If Not MaskedParts = 0 Then
                '-
                'Now if the container contains masked parts we have to start processing those masked parts
                '-
                ActiveIndex += MaskHeaderLength
                For j As Integer = 0 To MaskedParts - 1
                    Dim MaskedPartHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                    If Not MaskedPartHeaderLength = &H10 Then
                        MessageBox.Show("Error with Mask Header")
                        Exit Sub
                    End If
                    Dim TotalPartHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                    Dim PartNumber As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 8)
                    Dim MasksOnPart As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &HC) 'should be 1 always from what I've seen
                    If MasksOnPart = 0 Then
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_1"
                        TempGridRow.Cells(1).Value = "nil"
                        TempGridRow.Cells(2).Value = "nil"
                        WorkingCollection.Add(TempGridRow)
                        ActiveIndex += TotalPartHeaderLength
                    Else
                        '-
                        'For some reason they seem to have the capability of containing more than 1 set of mask arrays so this is just an extra layer of precaution
                        'This next for should only run once for every CE I've looked at
                        '-
                        ActiveIndex += MaskedPartHeaderLength
                        For k As Integer = 0 To MasksOnPart - 1
                            Dim TrueMaskHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                            If Not TrueMaskHeaderLength = &H10 Then
                                MessageBox.Show("Error with Mask Header")
                                Exit Sub
                            End If
                            Dim TrueMaskTotalLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                            Dim TrueMaskNumber As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 8)
                            Dim TrueMaskCount As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &HC)
                            If MasksOnPart = 0 Then
                                TempGridRow = CloneRow.Clone()
                                TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_" & TrueMaskNumber
                                TempGridRow.Cells(1).Value = "nil"
                                TempGridRow.Cells(2).Value = "nil"
                                WorkingCollection.Add(TempGridRow)
                                ActiveIndex += TrueMaskTotalLength
                            Else
                                '-
                                'Now to handle the true sets of masks each set is 8 bytes
                                '-
                                ActiveIndex += TrueMaskHeaderLength
                                For L As Integer = 0 To TrueMaskCount - 1
                                    'I only want the first line to have the part name
                                    If L = 0 Then
                                        TempGridRow = CloneRow.Clone()
                                        TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_" & TrueMaskNumber
                                        TempGridRow.Cells(1).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                                        TempGridRow.Cells(2).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                                        WorkingCollection.Add(TempGridRow)
                                        ActiveIndex += 8
                                    Else
                                        TempGridRow = CloneRow.Clone()
                                        TempGridRow.Cells(0).Value = ""
                                        TempGridRow.Cells(1).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                                        TempGridRow.Cells(2).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                                        WorkingCollection.Add(TempGridRow)
                                        ActiveIndex += 8
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            Else
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = CurrentMaskName & "_0_0"
                TempGridRow.Cells(1).Value = "nil"
                TempGridRow.Cells(2).Value = "nil"
                WorkingCollection.Add(TempGridRow)
                ActiveIndex += MaskLength
            End If
        Next
        DataGridMaskView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub FillMaskDataGrid(ByVal Source As String)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMaskView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim NewFaceStringArray As String() = File.ReadAllLines(Source)
        For i As Integer = 0 To NewFaceStringArray.Count - 1 Step 2
            'MessageBox.Show(New_Faces(i))
            Dim Temp_Name As String
            Dim Temp_Object_Number As Integer = NewFaceStringArray(i).Substring(6, 1)
            If Temp_Object_Number = 0 Then
                Temp_Name = "M_Head_0_0"
            Else
                Temp_Name = "M_Body_" & Temp_Object_Number - 1 & "_0"
            End If
            Dim Face_Numbers As String() = NewFaceStringArray(i + 1).Split(",")
            Dim Individual_Faces As List(Of Integer) = New List(Of Integer)
            For Each temp_face_num As String In Face_Numbers
                Individual_Faces.Add(Integer.Parse(temp_face_num))
            Next
            Individual_Faces.Sort()
            Dim Name_Added As Boolean = False
            Dim Base_Number As Integer = Individual_Faces(0) - 1
            For J As Integer = 1 To Individual_Faces.Count - 1
                If Not Individual_Faces(J) = Individual_Faces(J - 1) + 1 Then
                    If Name_Added = False Then
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = Temp_Name
                        TempGridRow.Cells(1).Value = Base_Number
                        TempGridRow.Cells(2).Value = (Individual_Faces(J - 1) - 1)
                        WorkingCollection.Add(TempGridRow)
                        Base_Number = Individual_Faces(J) - 1
                        Name_Added = True
                    Else
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = ""
                        TempGridRow.Cells(1).Value = Base_Number
                        TempGridRow.Cells(2).Value = (Individual_Faces(J - 1) - 1)
                        WorkingCollection.Add(TempGridRow)
                        Base_Number = Individual_Faces(J) - 1
                    End If
                End If
            Next
            If Name_Added = False Then
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = Temp_Name
                TempGridRow.Cells(1).Value = Base_Number
                TempGridRow.Cells(2).Value = (Individual_Faces(Individual_Faces.Count - 1) - 1)
                WorkingCollection.Add(TempGridRow)
                Name_Added = True
            Else
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = ""
                TempGridRow.Cells(1).Value = Base_Number
                TempGridRow.Cells(2).Value = (Individual_Faces(Individual_Faces.Count - 1) - 1)
                WorkingCollection.Add(TempGridRow)
            End If
        Next
        DataGridMaskView.Rows.AddRange(WorkingCollection.ToArray())
        SaveChangesMaskMenuItem.Visible = True
    End Sub

    Private Sub ImportMasksFromTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportMasksFromTXTToolStripMenuItem.Click
        Dim OpenTxtFile As OpenFileDialog = New OpenFileDialog With {
            .FileName = "CE_mask_list.txt",
            .Filter = "Text File|*.txt|All files (*.*)|*.*"}
        If OpenTxtFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(OpenTxtFile.FileName) Then
                FillMaskDataGrid(OpenTxtFile.FileName)
            End If
        End If
    End Sub

    Private Sub ExportMaskstoTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMaskstoTXTToolStripMenuItem.Click
        Dim SaveTxtFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "CE_mask_list.txt",
            .Filter = "Text File|*.txt|All files (*.*)|*.*"}
        If DataGridMaskView.SelectedRows.Count = 0 Then
            If SaveTxtFile.ShowDialog = DialogResult.OK Then
                Dim CurrentBody As String = "New"
                Dim FaceArray As List(Of Integer) = New List(Of Integer)
                Using sw As New IO.StreamWriter(SaveTxtFile.FileName)
                    For i As Integer = 0 To DataGridMaskView.RowCount - 1
                        If DataGridMaskView.Rows(i).Cells(0).Value = "" Then 'Just some mask numbers
                            For j As Integer = CInt(DataGridMaskView.Rows(i).Cells(1).Value) To CInt(DataGridMaskView.Rows(i).Cells(2).Value)
                                FaceArray.Add(j + 1)
                            Next
                        Else 'New Body Part
                            If CurrentBody <> "New" Then
                                'We Have to Write the existing Mask to the File
                                sw.WriteLine(CurrentBody)
                                FaceArray.Sort()
                                sw.WriteLine(String.Join(",", FaceArray))
                            End If
                            If Not DataGridMaskView.Rows(i).Cells(1).Value.ToString = "nil" Then
                                FaceArray = New List(Of Integer)
                                For j As Integer = CInt(DataGridMaskView.Rows(i).Cells(1).Value) To CInt(DataGridMaskView.Rows(i).Cells(2).Value)
                                    FaceArray.Add(j + 1)
                                Next
                                If DataGridMaskView.Rows(i).Cells(0).Value.contains("Body") Then
                                    CurrentBody = "Object" & (CInt(DataGridMaskView.Rows(i).Cells(0).Value.ToString().Substring(7, 1)) + 1).ToString
                                Else
                                    CurrentBody = "Object" & (CInt(DataGridMaskView.Rows(i).Cells(0).Value.ToString().Substring(7, 1))).ToString
                                End If
                            End If
                        End If
                    Next
                End Using
                MessageBox.Show("File Saved")
            End If
        Else
            If SaveTxtFile.ShowDialog = DialogResult.OK Then
                Dim FaceArray As List(Of Integer) = New List(Of Integer)
                For Each temprow As DataGridViewRow In DataGridMaskView.SelectedRows
                    For i As Integer = CInt(temprow.Cells(1).Value) To CInt(temprow.Cells(2).Value)
                        FaceArray.Add(i + 1)
                    Next
                Next
                'sorts all the faces before putting it in the array
                FaceArray.Sort()
                '3ds max script provided by tekken
                Using sw As New IO.StreamWriter(SaveTxtFile.FileName)
                    sw.WriteLine(String.Join(",", FaceArray))
                End Using
                MessageBox.Show("File Saved")
            End If
        End If
    End Sub

#Region "Save Mask Functions"

    Private Sub SaveMaskChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesMaskMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildMaskFile())
        SaveChangesMaskMenuItem.Visible = False
    End Sub

    'actively used when making a save
    Dim CurrentDataGridRow As Integer = 0

    Dim CurrentContainer As String = ""
    Dim ContainerByteArray As Byte() = New Byte() {}
    Dim ContainerCount As Integer = 0
    Dim CurrentBodyPart As Integer = -1
    Dim BodyPartArray As Byte() = New Byte() {}
    Dim BodyPartCount As Integer = 0
    Dim CurrentMask As Integer = -1
    Dim SingleMaskByteArray As Byte() = New Byte() {}
    Dim SingleArrayCount As Integer = 0
    Dim NumberByteArray As Byte() = New Byte() {}

    Private Function BuildMaskFile() As Byte()
        For Each row As DataGridViewRow In DataGridMaskView.Rows
            If row.Cells(1).Value.ToString = "nil" OrElse row.Cells(2).Value.ToString = "nil" Then
                MessageBox.Show("Please delete any nil rows before saving")
            End If
        Next
        Dim total_file As Byte() = New Byte() {}
        CurrentDataGridRow = 1
        ' row 0 must be manually processed as the starting point
        Dim SplitRowText As String() = DataGridMaskView(0, 0).Value.ToString().Split("_")
        CurrentContainer = SplitRowText(0) & "_" & SplitRowText(1)
        ContainerByteArray = New Byte(0) {}
        ContainerCount = 0
        CurrentBodyPart = CInt(SplitRowText(2))
        BodyPartArray = New Byte(0) {}
        BodyPartCount = 0
        CurrentMask = CInt(SplitRowText(3))
        SingleMaskByteArray = New Byte(0) {}
        SingleArrayCount = 0
        NumberByteArray = New Byte(7) {}
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, 0).Value)), 0, NumberByteArray, 0, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, 0).Value)), 0, NumberByteArray, 4, 4)
        Do While CurrentDataGridRow < DataGridMaskView.RowCount
            If DataGridMaskView(0, CurrentDataGridRow).Value.ToString.Length = 0 Then
                'MessageBox.Show("Adding")
                AddMoreNumbersToByteArray()
            Else
                SplitRowText = DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")
                If Not CurrentContainer = SplitRowText(0) & "_" & SplitRowText(1) Then
                    'putting the number arrays into a mask container
                    FormMaskByteArray()
                    'Make a new part together and adding it to any other parts
                    FormBodyByteArray()
                    'Now putting the container together
                    FormContainerByteArray()
                Else
                    If Not CurrentBodyPart = CInt(SplitRowText(2)) Then
                        'putting the number arrays into a mask container
                        FormMaskByteArray()
                        'Make a new part together and adding it to any other parts
                        FormBodyByteArray()
                    Else
                        If Not CurrentMask = CInt(SplitRowText(3)) Then
                            'putting the number arrays into a mask container
                            FormMaskByteArray()
                        Else
                            'This shouldn't happen, but this means it's the same part and it should just add the numbers
                            AddMoreNumbersToByteArray()
                        End If
                    End If
                End If
            End If
            CurrentDataGridRow += 1
        Loop
        'finish last package at this point
        'putting the number arrays into a mask container
        FormMaskByteArray()
        'Make a new part together and adding it to any other parts
        FormBodyByteArray()
        'Now putting the container together
        FormContainerByteArray()
        'Add the final header and then add containers to file
        total_file = New Byte(ContainerByteArray.Length + &HC - 2) {}
        'adding ceader length
        total_file(&H4) = &HC
        'adding container count
        total_file(&H8) = ContainerCount
        'copying final containers to array before writing to file
        Buffer.BlockCopy(ContainerByteArray, 0, total_file, &HC, ContainerByteArray.Length - 1)
        Return total_file
    End Function

    Sub AddMoreNumbersToByteArray()
        Dim old_length As Integer = NumberByteArray.Length
        ReDim Preserve NumberByteArray(old_length + 7)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, CurrentDataGridRow).Value)), 0, NumberByteArray, old_length, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, CurrentDataGridRow).Value)), 0, NumberByteArray, old_length + 4, 4)
    End Sub

    Sub MakeNewNumberByteArray()
        ReDim NumberByteArray(7)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, CurrentDataGridRow).Value)), 0, NumberByteArray, 0, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, CurrentDataGridRow).Value)), 0, NumberByteArray, 4, 4)
    End Sub

    Private Sub FormMaskByteArray()
        Dim old_length As Integer = SingleMaskByteArray.Length - 1
        ReDim Preserve SingleMaskByteArray((SingleMaskByteArray.Length) + (NumberByteArray.Length) + &H10 - 1)
        'length of header
        SingleMaskByteArray(old_length) = &H10
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt((NumberByteArray.Length) + &H10)), 0, SingleMaskByteArray, old_length + &H4, 4)
        'putting the mask number which should be 0
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(CurrentMask)), 0, SingleMaskByteArray, old_length + &H8, 4)
        'getting the count of numbers
        Buffer.BlockCopy(BitConverter.GetBytes(CInt((NumberByteArray.Length - 1) / 8)), 0, SingleMaskByteArray, old_length + &HC, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(NumberByteArray, 0, SingleMaskByteArray, old_length + &H10, NumberByteArray.Length)
        SingleArrayCount += 1
        ' MessageBox.Show(DataGridView1(0, current_row).Value.ToString() & vbNewLine &
        'DataGridView1(0, current_row).Value.ToString().Length)
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentMask = CInt(DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")(3))
            MakeNewNumberByteArray()
        End If
        'Make New Number Array for new mask
    End Sub

    Sub MakeNewMaskByteArray()
        SingleMaskByteArray = New Byte(0) {}
    End Sub

    Sub FormBodyByteArray()
        Dim old_length As Integer = BodyPartArray.Length - 1
        ReDim Preserve BodyPartArray(BodyPartArray.Length + SingleMaskByteArray.Length + &H10 - 2)
        'length of header
        BodyPartArray(old_length) = &H10
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(SingleMaskByteArray.Length + &H10 - 1)), 0, BodyPartArray, old_length + &H4, 4)
        'putting the body part number
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(CurrentBodyPart)), 0, BodyPartArray, old_length + &H8, 4)
        'getting the count of masks
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(SingleArrayCount)), 0, BodyPartArray, old_length + &HC, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(SingleMaskByteArray, 0, BodyPartArray, old_length + &H10, SingleMaskByteArray.Length)
        '
        BodyPartCount += 1
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentBodyPart = CInt(DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")(2))
        End If
        MakeNewMaskByteArray()
        SingleArrayCount = 0
    End Sub

    Sub MakeNewBodyPartByteArray()
        BodyPartArray = New Byte(0) {}
    End Sub

    Sub FormContainerByteArray()
        Dim old_length As Integer = ContainerByteArray.Length - 1
        ReDim Preserve ContainerByteArray(ContainerByteArray.Length + BodyPartArray.Length + &H4C - 2)
        'length of header
        ContainerByteArray(old_length) = &H4C
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(BodyPartArray.Length + &H4C - 1)), 0, ContainerByteArray, old_length + &H4, 4)
        'putting in the container name
        Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(CurrentContainer), 0, ContainerByteArray, old_length + &H8, System.Text.Encoding.ASCII.GetBytes(CurrentContainer).Length)
        'adding the count of body parts
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(BodyPartCount)), 0, ContainerByteArray, old_length + &H48, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(BodyPartArray, 0, ContainerByteArray, old_length + &H4C, BodyPartArray.Length)
        '
        ContainerCount += 1
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentContainer = DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Substring(0, 6)
        End If
        MakeNewBodyPartByteArray()
        BodyPartCount = 0
    End Sub

#End Region

#Region "Tutorial Links"

    Private Sub TutorialVideoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialVideoToolStripMenuItem.Click
        Try
            Process.Start("https://www.youtube.com/watch?v=e7WNIh-WYB4")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DSImportScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSImportScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3FYJ")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DSSelectionScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSSelectionScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3FZt")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DSExportScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSExportScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3Fd2")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AznTutorialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AznTutorialToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/4t4q")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "DataGridView Actions"

    Private Sub DataGridMaskView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 3 Then
                'add row of mask
                Dim temprow As DataGridViewRow = DataGridMaskView.Rows(e.RowIndex).Clone()
                temprow.Cells(0).Value = ""
                temprow.Cells(1).Value = DataGridMaskView.Rows(e.RowIndex).Cells(1).Value
                temprow.Cells(2).Value = DataGridMaskView.Rows(e.RowIndex).Cells(2).Value
                DataGridMaskView.Rows.Insert(e.RowIndex + 1, temprow)
                SavePending = True
                SaveChangesMaskMenuItem.Visible = True
            ElseIf e.ColumnIndex = 4 Then
                'delete row of mask
                DataGridMaskView.Rows.Remove(DataGridMaskView.Rows(e.RowIndex))
                SavePending = True
                SaveChangesMaskMenuItem.Visible = True
            Else
                'do nothing because a not button column at this time.
            End If
        End If
    End Sub

    Private Sub DataGridMaskView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMaskView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Dim NewValue As String = MyCell.Value
        If e.ColumnIndex = 0 Then 'Mask Name must be of a format M_Head_x_x
            Dim SplitUpString As String() = NewValue.Split("_")
            If SplitUpString.Count = 4 AndAlso
               SplitUpString(0) = "M" Then
                If SplitUpString(1) = "Head" OrElse
                   SplitUpString(1) = "Body" Then
                    If Not IsNumeric(SplitUpString(2)) Then
                        MyCell.Value = OldValue
                    Else
                        If Not IsNumeric(SplitUpString(3)) Then
                            MyCell.Value = OldValue
                        End If
                    End If
                Else
                    MyCell.Value = OldValue
                End If
            Else
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Start Face
            If Not IsNumeric(NewValue) Then
                MyCell.Value = OldValue
            ElseIf CInt(NewValue) < 0 Then
                MyCell.Value = OldValue
            Else
                Dim EndFace As String = DataGridMaskView.Rows(e.RowIndex).Cells(2).Value
                If Not IsNumeric(EndFace) Then ' End Face is nil and we need to make it a number
                    DataGridMaskView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value
                ElseIf CInt(NewValue) > CInt(EndFace) Then 'if we are increasing the start face we need to increas the end face
                    DataGridMaskView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then 'End Face
            If Not IsNumeric(NewValue) Then
                MyCell.Value = OldValue
            ElseIf CInt(NewValue) < 0 Then
                MyCell.Value = OldValue
            Else
                Dim StartFace As String = DataGridMaskView.Rows(e.RowIndex).Cells(1).Value
                If Not IsNumeric(StartFace) Then ' End Face is nil and we need to make it a number
                    DataGridMaskView.Rows(e.RowIndex).Cells(1).Value = MyCell.Value
                ElseIf CInt(NewValue) < CInt(StartFace) Then 'if we are decreasing the end face we need to decrease the start face
                    DataGridMaskView.Rows(e.RowIndex).Cells(1).Value = MyCell.Value
                End If
            End If
        End If
        SavePending = True
        SaveChangesMaskMenuItem.Visible = True
    End Sub

#End Region

#End Region

#Region "Object Array Controls"

    Dim ObjArrayContainerCount As Integer = 0

    Sub FillObjectArrayView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjArrayView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim ObjArrayBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim ChairCount As Integer = BitConverter.ToInt32(ObjArrayBytes, &HC)
        Dim ParentStrings As String() = New String(ChairCount) {}
        ParentStrings(0) = "Base Object"
        ObjArrayContainerCount = 0
        For i As Integer = 0 To ChairCount - 1
            Dim ItemString As String = Encoding.ASCII.GetString(ObjArrayBytes, &H20 + ChairCount * 8 + i * &H30, &H10).Trim()
            Dim InternalItemCount As Int32 = BitConverter.ToInt32(ObjArrayBytes, &H48 + ChairCount * 8 + i * &H30)
            Dim StartIndex As Int32 = BitConverter.ToInt32(ObjArrayBytes, &H4C + ChairCount * 8 + i * &H30)
            TempGridRow = CloneRow.Clone()
            If InternalItemCount > 0 Then
                ObjArrayContainerCount += 1
                For j As Integer = StartIndex To StartIndex + InternalItemCount
                    ParentStrings(j) = ItemString
                Next
            End If
            TempGridRow.Cells(0).Value = i 'Absolute Index
            TempGridRow.Cells(1).Value = ParentStrings(i)  'Parent
            TempGridRow.Cells(2).Value = BitConverter.ToInt32(ObjArrayBytes, &H24 + i * 8) 'Number
            TempGridRow.Cells(3).Value = BitConverter.ToBoolean(ObjArrayBytes, &H20 + i * 8) 'Enabled
            If InternalItemCount > 0 Then
                TempGridRow.Cells(3).ReadOnly = True
            End If
            TempGridRow.Cells(4).Value = ItemString  'Object Name
            TempGridRow.Cells(5).Value = BitConverter.ToSingle(ObjArrayBytes, &H30 + ChairCount * 8 + i * &H30) 'X Float
            TempGridRow.Cells(6).Value = BitConverter.ToSingle(ObjArrayBytes, &H34 + ChairCount * 8 + i * &H30) 'Y Float
            TempGridRow.Cells(7).Value = BitConverter.ToSingle(ObjArrayBytes, &H38 + ChairCount * 8 + i * &H30) 'Z Float
            TempGridRow.Cells(8).Value = BitConverter.ToSingle(ObjArrayBytes, &H3C + ChairCount * 8 + i * &H30) 'RX Float
            TempGridRow.Cells(9).Value = BitConverter.ToSingle(ObjArrayBytes, &H40 + ChairCount * 8 + i * &H30) 'RY Float
            TempGridRow.Cells(10).Value = BitConverter.ToSingle(ObjArrayBytes, &H44 + ChairCount * 8 + i * &H30) 'RZ Float
            TempGridRow.Cells(11).Value = InternalItemCount 'Item Count
            TempGridRow.Cells(12).Value = StartIndex 'Start Index
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridObjArrayView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Sub LoadObjectArrayView(CSVString As String())
        'Line 0 should be the header lione and ignored
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjArrayView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        ObjArrayContainerCount = 0
        For i As Integer = 1 To CSVString.Count - 1
            TempGridRow = CloneRow.Clone()
            Dim CSVValues As String() = CSVString(i).Split(",")
            Dim InternalItemCount As Int32 = CSVValues(11)
            TempGridRow.Cells(0).Value = CSVValues(0)
            TempGridRow.Cells(1).Value = CSVValues(1)
            TempGridRow.Cells(2).Value = CSVValues(2) 'Number
            TempGridRow.Cells(3).Value = CSVValues(3) 'Enabled
            If InternalItemCount > 0 Then
                ObjArrayContainerCount += 1
                TempGridRow.Cells(3).ReadOnly = True
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
            End If
            TempGridRow.Cells(4).Value = CSVValues(4)  'Object Name
            TempGridRow.Cells(5).Value = CSVValues(5) 'X Float
            TempGridRow.Cells(6).Value = CSVValues(6) 'Y Float
            TempGridRow.Cells(7).Value = CSVValues(7) 'Z Float
            TempGridRow.Cells(8).Value = CSVValues(8) 'RX Float
            TempGridRow.Cells(9).Value = CSVValues(9) 'RY Float
            TempGridRow.Cells(10).Value = CSVValues(10) 'RZ Float
            TempGridRow.Cells(11).Value = InternalItemCount 'Item Count
            TempGridRow.Cells(12).Value = CSVValues(12) 'Start Index
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridObjArrayView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridObjArrayView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjArrayView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridObjArrayView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            'Case 0 OrElse 1 'Index and Parent are Read Only
            Case 2 'Integer Object Number
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf Not (Math.Floor(MyCell.Value) = Math.Ceiling(MyCell.Value)) Then
                    MyCell.Value = OldValue
                End If
            'Case 3 Check Box does not have an invalid state to reset
            Case 4 'String for object name
                If MyCell.Value.ToString.Trim().ToString.Length > 16 Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = MyCell.Value.ToString.Trim()
                    MyCell.Value = MyCell.Value.ToString.Replace(" ", "")
                    'if it's a container object we need to update the relevent names on the datagrid..
                    If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                        'It is a container file and we need to update the lines
                        Dim StartIndex As Integer = CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(12).Value)
                        Dim EndingIndex As Integer = StartIndex + CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) - 1
                        For i As Integer = StartIndex To EndingIndex
                            DataGridObjArrayView.Rows(i).Cells(1).Value = MyCell.Value
                        Next
                    End If
                End If
            Case > 4 'Single Number Values
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                End If
                'Case 11 OrElse 12 'Item Count and Starting Index Should be Changed by User. Only by Add / Delete Buttons.
        End Select
        SaveChangesYOBJArrayMenuItem.Visible = True
    End Sub

    Private Sub DataGridObjArrayView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjArrayView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 13 Then 'add button
                If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                    'Container Object We need to add at least 1 Sub items
                    AddObjArrayContainer(e.RowIndex)
                Else
                    'Individual Item that we need to update within the container
                    AddObjArrayIndividualObject(e.RowIndex)
                End If
            ElseIf e.ColumnIndex = 14 Then 'Delete button
                If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                    'Container Object We also need to delete the sub objects
                    DeleteObjArrayContainer(e.RowIndex)
                Else
                    'Individual Item we will need to update the container object.
                    DeleteObjArrayIndividualObject(e.RowIndex)
                End If
            Else
                'do nothing
            End If
        End If
    End Sub

    Sub AddObjArrayContainer(index As Integer)
        'This function adds a new container object with 1 item at the location of index+1
        Dim Duplicaterow As DataGridViewRow = DataGridObjArrayView.Rows(index).Clone
        For i As Integer = 0 To DataGridObjArrayView.Rows(index).Cells.Count - 1
            Duplicaterow.Cells(i).Value = DataGridObjArrayView.Rows(index).Cells(i).Value
        Next
        'change Start Index and Container Count
        Duplicaterow.Cells(11).Value = 1
        Duplicaterow.Cells(12).Value = DataGridObjArrayView.Rows(index + 1).Cells(12).Value
        'Add +1 because we add a row before the command is actually called..
        Dim LastItemOfPreviousContainerToCopy As Integer = CInt(DataGridObjArrayView.Rows(index + 1).Cells(12).Value) + 1
        DataGridObjArrayView.Rows.Insert(index + 1, Duplicaterow)
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond or equal to the added object we need to +1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) >= index Then
                'if the index matches then the added item was added to the previous
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
            End If
        Next
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) + 1
        Next
        MessageBox.Show("Adding Sub Object " & LastItemOfPreviousContainerToCopy)
        AddObjArrayIndividualObject(LastItemOfPreviousContainerToCopy, True)
    End Sub

    Sub AddObjArrayIndividualObject(index As Integer, Optional NewContainer As Boolean = False)
        'This function adds a duplicate row at index + 1, but index + 1 has to have true index updated as well
        Dim Duplicaterow As DataGridViewRow = DataGridObjArrayView.Rows(index).Clone
        For i As Integer = 0 To DataGridObjArrayView.Rows(index).Cells.Count - 1
            Duplicaterow.Cells(i).Value = DataGridObjArrayView.Rows(index).Cells(i).Value
        Next
        DataGridObjArrayView.Rows.Insert(index + 1, Duplicaterow)
        'This function runs after the index is actually added
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) + 1
        Next
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond or equal to the added object we need to +1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) = index Then
                MessageBox.Show("Matched Index")
                'if the index matches then the added item was added to the previous container... or it's a new container
                If Not NewContainer Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
                ElseIf Not CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) = 1 Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
                End If
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) >= index Then
                If Not NewContainer Then
                    'this means that this is the container that contains the added object so we need to increase the item count
                    DataGridObjArrayView.Rows(i).Cells(11).Value = CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) + 1
                End If
            End If
        Next
    End Sub

    Sub DeleteObjArrayContainer(index As Integer)
        'This function runs before the container is actually deleted
        If CInt(DataGridObjArrayView.Rows(index).Cells(11).Value) > 0 Then
            Dim LastContainedObject As Integer = (CInt(DataGridObjArrayView.Rows(index).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(index).Cells(11).Value) - 1)
            Dim FirstContainedObject As Integer = CInt(DataGridObjArrayView.Rows(index).Cells(12).Value)
            'MessageBox.Show(LastContainedObject & " to " & FirstContainedObject)
            For i As Integer = LastContainedObject To FirstContainedObject Step -1
                MessageBox.Show("Deleting Object " & i)
                DeleteObjArrayIndividualObject(i)
            Next
            'we want to skip the last 2 lines so we don't accidently delete 2 rows because the last for will call the delete container again
        Else
            MessageBox.Show("Deleting Container")
            For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
                'Update Absolute Index
                DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) - 1
            Next
            'Try To update containers
            For i As Integer = 0 To ObjArrayContainerCount
                'if the start index is beyond the deleted object  we need to -1 to the start index
                If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) - 1
                End If
            Next
            MessageBox.Show("Deleting Container Line")
            DataGridObjArrayView.Rows.RemoveAt(index)
            ObjArrayContainerCount -= 1
        End If
    End Sub

    Sub DeleteObjArrayIndividualObject(index As Integer)
        'This function runs before the index is actually deleted
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) - 1
        Next
        Dim DeleteContainer As Boolean = False
        Dim ContainertoDelete As Integer = -1
        'Try to update containers
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond the deleted object  we need to -1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) - 1
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) > index Then
                'this means that this is the container that contains the deleted object so we need to reduce the idem count
                DataGridObjArrayView.Rows(i).Cells(11).Value = CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) - 1
                'if the updated container item count = 0 we will want to delete the container as well
                If CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) = 0 Then
                    DeleteContainer = True
                    ContainertoDelete = i
                End If
            End If
        Next
        DataGridObjArrayView.Rows.RemoveAt(index)
        If DeleteContainer Then DeleteObjArrayContainer(ContainertoDelete)
    End Sub

    Private Sub SaveYOBJArrayChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesYOBJArrayMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildYOBJArrayFile())
    End Sub

    Private Function BuildYOBJArrayFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(&H20 + ((&H30 + &H8) * DataGridObjArrayView.Rows.Count) - 1) {}
        Dim ContainerCount As UInt32 = 0
        Dim ObjectCount As UInt32 = 0
        For i As Integer = 0 To DataGridObjArrayView.Rows.Count - 1
            If CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) > 0 Then
                ContainerCount += 1
            Else
                ObjectCount = DataGridObjArrayView.Rows.Count - ContainerCount
                Exit For
            End If
        Next
        'File Header
        Array.Copy(New Byte() {&HEA, &HA1, &H59, &H0, &H1, &H0, &H0, &H0, &H1, &H0, &H0, &H0}, 0, ReturnedBytes, 0, &HC)
        'total Containers + Objects
        Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows.Count)), 0, ReturnedBytes, &HC, 4)
        'Container Count
        Array.Copy(BitConverter.GetBytes(ContainerCount), 0, ReturnedBytes, &H10, 4)
        'Object Count
        Array.Copy(BitConverter.GetBytes(ObjectCount), 0, ReturnedBytes, &H14, 4)
        Dim DataIndex As UInt32 = &H20 + (DataGridObjArrayView.Rows.Count * 8)
        For i As Integer = 0 To DataGridObjArrayView.Rows.Count - 1
            'enabled First
            If DataGridObjArrayView.Rows(i).Cells(3).Value Then
                ReturnedBytes(&H20 + i * 8) = 1
            Else
                ReturnedBytes(&H20 + i * 8) = 0
            End If
            'Item Number
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &H24 + i * 8, 4)
            'Name
            Dim ObjectName As String = DataGridObjArrayView.Rows(i).Cells(4).Value
            Dim StringBytes As Byte() = Encoding.ASCII.GetBytes(ObjectName)
            Array.Copy(StringBytes, 0, ReturnedBytes, DataIndex + i * &H30, StringBytes.Length)
            'X Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(5).Value)), 0, ReturnedBytes, DataIndex + &H10 + (i * &H30), 4)
            'Y Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(6).Value)), 0, ReturnedBytes, DataIndex + &H14 + (i * &H30), 4)
            'Z Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(7).Value)), 0, ReturnedBytes, DataIndex + &H18 + (i * &H30), 4)
            'RX Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(8).Value)), 0, ReturnedBytes, DataIndex + &H1C + (i * &H30), 4)
            'RY Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(9).Value)), 0, ReturnedBytes, DataIndex + &H20 + (i * &H30), 4)
            'RZ Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(10).Value)), 0, ReturnedBytes, DataIndex + &H24 + (i * &H30), 4)
            'Item Count
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(11).Value)), 0, ReturnedBytes, DataIndex + &H28 + (i * &H30), 4)
            'Start Index
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(12).Value)), 0, ReturnedBytes, DataIndex + &H2C + (i * &H30), 4)
        Next
        Return ReturnedBytes
    End Function

    Private Sub ExportYOBJArrayToCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportYOBJArrayToCSVToolStripMenuItem.Click
        Dim SaveCSVFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "Object Array.csv",
            .Filter = "Comma Seperated Values|*.csv|All files (*.*)|*.*"}
        If SaveCSVFile.ShowDialog = DialogResult.OK Then
            Dim headers = (From header As DataGridViewColumn In DataGridObjArrayView.Columns.Cast(Of DataGridViewColumn)()
                           Select header.HeaderText).ToArray
            Dim rows As List(Of String) = New List(Of String)
            For Each temprow As DataGridViewRow In DataGridObjArrayView.Rows
                If Not temprow.IsNewRow Then
                    Dim Tempstring As String = ""
                    For Each tempcell As DataGridViewCell In temprow.Cells
                        If temprow.Cells.IndexOf(tempcell) = 0 Then
                            Tempstring = tempcell.Value
                        ElseIf tempcell.ValueType = GetType(CheckBox) Then
                            If tempcell.Value Then
                                Tempstring += ",1"
                            Else
                                Tempstring += ",0"
                            End If
                        Else
                            Tempstring += "," & tempcell.Value.ToString.Trim()
                        End If
                    Next
                    rows.Add(Tempstring)
                End If
            Next
            Using sw As New IO.StreamWriter(SaveCSVFile.FileName)
                sw.WriteLine(String.Join(",", headers))
                For Each r In rows
                    sw.WriteLine(r)
                Next
            End Using
            MessageBox.Show("File Saved")
        End If
    End Sub

    Private Sub ImportYOBJArrayFromCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportYOBJArrayFromCSVToolStripMenuItem.Click
        Dim OpenCSVFile As OpenFileDialog = New OpenFileDialog With {
            .FileName = "Object Array.csv",
            .Filter = "Comma Seperated Values|*.csv|All files (*.*)|*.*"}
        If OpenCSVFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(OpenCSVFile.FileName) Then
                LoadObjectArrayView(File.ReadAllLines(OpenCSVFile.FileName))
                SaveChangesYOBJArrayMenuItem.Visible = True
            End If
        End If
    End Sub

#End Region

#Region "Asset View Controls"

    Public Class AssetFileInformation
        Public PacNumber As UInt32 = 0
        Public AttireNum As UInt32 = 0
        Public AudioNum As UInt32 = 0
        Public Check2 As UInt32 = 0
        Public MusicOffset As UInt32 = 0
        Public EVTOffset As UInt32 = 0
        Public MusicNum As UInt32 = 0
        Public TitantronNum As UInt32 = 0
        Public HeaderNum As UInt32 = 0
        Public WallNum As UInt32 = 0
        Public RampNum As UInt32 = 0
        Public WallRightNum As UInt32 = 0
        Public WallLeftNum As UInt32 = 0
        Public RawTronEnabled As Boolean = False
        Public SDTronEnabled As Boolean = False
        Public ClassicTronEnable As Boolean = False
        Public Check5 As UInt32 = 0
        Public Check6 As UInt32 = 0
    End Class

    Sub FillAssetFileView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAssetView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim AssetConvBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim AssetCount As Integer = BitConverter.ToInt32(AssetConvBytes, &HC)
        ProgressBar1.Maximum = AssetCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To AssetCount - 1
            Dim TempByteArray As Byte() = New Byte(&H40) {}
            Array.Copy(AssetConvBytes, &H18 + i * &H40, TempByteArray, 0, &H40)
            Dim TempAssetFile As AssetFileInformation = ParseBytesToAssetFileInformation(TempByteArray)
            Dim MUSFileName As String = ""
            If TempAssetFile.MusicOffset > 0 Then
                If Not TempAssetFile.MusicOffset = UInt32.MaxValue Then
                    Try
                        MUSFileName = Encoding.Default.GetString(AssetConvBytes, TempAssetFile.MusicOffset, &H11)
                        MUSFileName = MUSFileName.Substring(0, MUSFileName.IndexOf(Nothing))
                    Catch ex As Exception
                        MUSFileName = "ERROR"
                    End Try
                End If
            End If
            Dim EVTFileName As String = ""
            If TempAssetFile.EVTOffset > 0 Then
                If Not TempAssetFile.EVTOffset = UInt32.MaxValue Then
                    Try
                        'TO DO Fix name length inconsistencies
                        EVTFileName = Encoding.Default.GetString(AssetConvBytes, TempAssetFile.EVTOffset, &H11)
                        EVTFileName = EVTFileName.Substring(0, EVTFileName.IndexOf(Nothing))
                    Catch ex As Exception
                        EVTFileName = "ERROR"
                    End Try
                End If
            End If
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempAssetFile.PacNumber
            TempGridRow.Cells(1).Value = TempAssetFile.AttireNum
            TempGridRow.Cells(2).Value = TempAssetFile.AudioNum
            TempGridRow.Cells(3).Value = TempAssetFile.Check2
            TempGridRow.Cells(4).Value = TempAssetFile.MusicOffset
            TempGridRow.Cells(5).Value = TempAssetFile.EVTOffset
            TempGridRow.Cells(6).Value = TempAssetFile.MusicNum
            TempGridRow.Cells(7).Value = TempAssetFile.TitantronNum
            TempGridRow.Cells(8).Value = TempAssetFile.HeaderNum
            TempGridRow.Cells(9).Value = TempAssetFile.WallNum
            TempGridRow.Cells(10).Value = TempAssetFile.RampNum
            TempGridRow.Cells(11).Value = TempAssetFile.WallRightNum
            TempGridRow.Cells(12).Value = TempAssetFile.WallLeftNum
            TempGridRow.Cells(13).Value = TempAssetFile.RawTronEnabled
            TempGridRow.Cells(14).Value = TempAssetFile.SDTronEnabled
            TempGridRow.Cells(15).Value = TempAssetFile.ClassicTronEnable
            TempGridRow.Cells(16).Value = TempAssetFile.Check5
            TempGridRow.Cells(17).Value = TempAssetFile.Check6
            TempGridRow.Cells(18).Value = MUSFileName
            TempGridRow.Cells(19).Value = EVTFileName
            TempGridRow.HeaderCell.Value = i.ToString
            TempGridRow.Tag = TempAssetFile
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridAssetView.Rows.AddRange(WorkingCollection.ToArray)
    End Sub

    Function ParseBytesToAssetFileInformation(TestedByteArray As Byte()) As AssetFileInformation
        Return New AssetFileInformation With {
            .PacNumber = BitConverter.ToUInt32(TestedByteArray, 0),
            .AttireNum = BitConverter.ToUInt32(TestedByteArray, 4),
            .AudioNum = BitConverter.ToUInt32(TestedByteArray, 8),
            .Check2 = BitConverter.ToUInt32(TestedByteArray, 12),
            .MusicOffset = BitConverter.ToUInt32(TestedByteArray, 16),
            .EVTOffset = BitConverter.ToUInt32(TestedByteArray, 20),
            .MusicNum = BitConverter.ToUInt32(TestedByteArray, 24),
            .TitantronNum = BitConverter.ToUInt32(TestedByteArray, 28),
            .HeaderNum = BitConverter.ToUInt32(TestedByteArray, 32),
            .WallNum = BitConverter.ToUInt32(TestedByteArray, 36),
            .RampNum = BitConverter.ToUInt32(TestedByteArray, 40),
            .WallRightNum = BitConverter.ToUInt32(TestedByteArray, 44),
            .WallLeftNum = BitConverter.ToUInt32(TestedByteArray, 48),
            .RawTronEnabled = BitConverter.ToBoolean(TestedByteArray, 52),
            .SDTronEnabled = BitConverter.ToBoolean(TestedByteArray, 53),
            .ClassicTronEnable = BitConverter.ToBoolean(TestedByteArray, 54),
            .Check5 = BitConverter.ToUInt32(TestedByteArray, 56),
            .Check6 = BitConverter.ToUInt32(TestedByteArray, 60)}
    End Function

    Function GetBytesFromAssetFileDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&H40) {}
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, 4, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(2).Value)), 0, ReturnedBytes, 8, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, 12, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(4).Value)), 0, ReturnedBytes, 16, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, 20, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(6).Value)), 0, ReturnedBytes, 24, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, 28, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(8).Value)), 0, ReturnedBytes, 32, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(9).Value)), 0, ReturnedBytes, 36, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(10).Value)), 0, ReturnedBytes, 40, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(11).Value)), 0, ReturnedBytes, 44, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(12).Value)), 0, ReturnedBytes, 48, 4)
        Array.Copy(BitConverter.GetBytes(CBool(RequestedByteRow.Cells(13).Value)), 0, ReturnedBytes, 52, 1)
        Array.Copy(BitConverter.GetBytes(CBool(RequestedByteRow.Cells(14).Value)), 0, ReturnedBytes, 53, 1)
        Array.Copy(BitConverter.GetBytes(CBool(RequestedByteRow.Cells(15).Value)), 0, ReturnedBytes, 54, 1)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(16).Value)), 0, ReturnedBytes, 56, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(17).Value)), 0, ReturnedBytes, 60, 4)
        Return ReturnedBytes
    End Function

    Function AdjustFileNameOffsets() As Integer
        Dim CurrentIndex As UInt32 = &H18 + &H38 + (DataGridAssetView.Rows.Count) * &H40
        For i As Integer = 0 To DataGridAssetView.Rows.Count - 1
            'Updating Music File Information offsets first
            If Not DataGridAssetView.Rows(i).Cells(16).Value = "" Then
                DataGridAssetView.Rows(i).Cells(4).Value = CurrentIndex
                CurrentIndex += DataGridAssetView.Rows(i).Cells(16).Value.ToString.Length + 1
            End If
        Next
        For i As Integer = 0 To DataGridAssetView.Rows.Count - 1
            'Updating evt File Information offsets first
            If Not DataGridAssetView.Rows(i).Cells(17).Value = "" Then
                DataGridAssetView.Rows(i).Cells(5).Value = CurrentIndex
                CurrentIndex += DataGridAssetView.Rows(i).Cells(17).Value.ToString.Length + 1
            End If
        Next
        Return (CurrentIndex - (CurrentIndex Mod &H100) + &H100)
    End Function

    Private Sub DataGridAssetView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAssetView.CellEndEdit
        Dim StartingStrings As List(Of String) = New List(Of String)
        StartingStrings.AddRange({"ent", "cre", "tag", "MUS"})
        Dim MyCell As DataGridViewCell = DataGridAssetView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 16 OrElse e.ColumnIndex = 17 Then
            'A File Name has been Added or Edited
            If MyCell.Value.ToString.Contains("_") Then
                Dim SplitUpString As String() = MyCell.Value.Split("_")
                If SplitUpString.Count > 3 AndAlso
                StartingStrings.Contains(SplitUpString(0)) Then
                    For i As Integer = 1 To SplitUpString.Count - 1
                        If Not IsNumeric(SplitUpString(i)) Then
                            MyCell.Value = OldValue
                        End If
                    Next
                ElseIf MyCell.Value = "" Then
                    'do nothing
                Else
                    MyCell.Value = OldValue
                End If
            Else
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 4 OrElse e.ColumnIndex = 5 Then
            'this can only be edited by the program
        Else
            If Not IsNumeric(MyCell.Value) OrElse
           MyCell.Value < -1 Then
                MyCell.Value = CUInt(OldValue)
            Else
                Dim TempTest As ULong = CULng(MyCell.Value)
                If TempTest < UInt32.MaxValue Then
                    SavePending = True
                    SaveChangesAssetViewMenuItem.Visible = True
                    MyCell.Value = CUInt(MyCell.Value)
                Else
                    MyCell.Value = CUInt(OldValue)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridAssetView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAssetView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 20 Then 'add button
                'This function adds a duplicate row at index + 1, but index + 1 has to have true index updated as well
                Dim Duplicaterow As DataGridViewRow = DataGridAssetView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridAssetView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridAssetView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridAssetView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                SavePending = True
                SaveChangesAssetViewMenuItem.Visible = True
            ElseIf e.ColumnIndex = 21 Then 'Delete button
                DataGridAssetView.Rows.RemoveAt(e.RowIndex)
                SavePending = True
                SaveChangesAssetViewMenuItem.Visible = True
            Else
                'do nothing
            End If
        End If
    End Sub

    Private Sub SaveAssetViewChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesAssetViewMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildAssetArrayFile())
    End Sub

    Private Function BuildAssetArrayFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(AdjustFileNameOffsets() - 1) {}
        Dim AssetCount As UInt32 = 0
        'File Header
        Array.Copy(New Byte() {&H56, &H4D, &H55, &H4D, &H10, &H0, &H0, &H0, &HE8, &H3, &H0, &H0, &H0, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H82, &H0, &H0, &H0}, 0, ReturnedBytes, 0, &H18)
        Array.Copy(BitConverter.GetBytes(CUInt(DataGridAssetView.Rows.Count)), 0, ReturnedBytes, &HC, 4)
        For i As Integer = 0 To DataGridAssetView.Rows.Count - 1
            Array.Copy(GetBytesFromAssetFileDataGridRow(DataGridAssetView.Rows(i)), 0, ReturnedBytes, &H18 + i * &H40, &H40)
            'Copying Music Names
            If DataGridAssetView.Rows(i).Cells(4).Value > 0 AndAlso DataGridAssetView.Rows(i).Cells(4).Value < (UInt32.MaxValue - 1) Then
                Array.Copy(Encoding.Default.GetBytes(DataGridAssetView.Rows(i).Cells(16).Value), 0,
                           ReturnedBytes, DataGridAssetView.Rows(i).Cells(4).Value,
                           DataGridAssetView.Rows(i).Cells(16).Value.ToString.Length)
            End If
            'Copying EVT Names
            If DataGridAssetView.Rows(i).Cells(5).Value > 0 AndAlso DataGridAssetView.Rows(i).Cells(5).Value < (UInt32.MaxValue - 1) Then
                Array.Copy(Encoding.Default.GetBytes(DataGridAssetView.Rows(i).Cells(17).Value), 0,
                           ReturnedBytes, DataGridAssetView.Rows(i).Cells(5).Value,
                           DataGridAssetView.Rows(i).Cells(17).Value.ToString.Length)
            End If
        Next
        Return ReturnedBytes
    End Function

#End Region

#Region "Title View"

    Public Class TitleInformation
        Public Enabled As UInt32 = 0
        Public PropID As UInt32 = 0
        Public MenuNumber As UInt32 = 0
        Public NameRef1 As UInt32 = 0
        Public NameString1 As String = ""
        Public NameRef2 As UInt32 = 0
        Public NameString2 As String = ""
        Public NameRef3 As UInt32 = 0
        Public NameString3 As String = ""
        Public WWEDefault1 As UInt32 = &H400 'default value for no champ
        Public WWEDefault1Name As String = ""
        Public WWEDefault2 As UInt32 = &H400
        Public WWEDefault2Name As String = ""
        Public UniDefault1 As UInt32 = &H400
        Public UniDefault1Name As String = ""
        Public UniDefault2 As UInt32 = &H400
        Public UniDefault2Name As String = ""
        Public Temp1 As UInt32 = 0
        Public TitleTypeByte As UInt32 = 0
        Public Female As Boolean = False
        Public TagTeam As Boolean = False
        Public Cuiserweight As Boolean = False
        Public UnlockNum As UInt32 = 0
        Public Temp4 As UInt32 = 0
    End Class

    Sub FillTitleFileView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridTitleView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TitleBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim TitleCount As Integer = BitConverter.ToInt32(TitleBytes, &H8)
        ProgressBar1.Maximum = TitleCount - 1
        ProgressBar1.Value = 0
        DataGridTitleView.Rows.Clear()
        'Adjusting Title Game Combo Box
        Dim TitleSize As Integer = (TitleBytes.Length - &H10) / TitleCount
        If TitleSize = 480 Then '480 1E0 2K19 Titles
            TitleGameComboBox.SelectedIndex = 4
        End If
        For i As Integer = 0 To TitleCount - 1
            Dim TempTitleBytes As Byte() = New Byte(TitleSize - 1) {}
            Array.Copy(TitleBytes, &H10 + i * TitleSize, TempTitleBytes, 0, TitleSize)
            Dim TempTitleInformation As TitleInformation = ParseBytesToTitleInformation(TempTitleBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempTitleInformation.Enabled
            TempGridRow.Cells(1).Value = TempTitleInformation.PropID
            TempGridRow.Cells(2).Value = TempTitleInformation.MenuNumber
            TempGridRow.Cells(3).Value = Hex(TempTitleInformation.NameRef1)
            TempGridRow.Cells(4).Value = TempTitleInformation.NameString1
            TempGridRow.Cells(4).Style = ReadOnlyCellStyle
            TempGridRow.Cells(5).Value = Hex(TempTitleInformation.NameRef2)
            TempGridRow.Cells(6).Value = TempTitleInformation.NameString2
            TempGridRow.Cells(6).Style = ReadOnlyCellStyle
            TempGridRow.Cells(7).Value = Hex(TempTitleInformation.NameRef3)
            TempGridRow.Cells(8).Value = TempTitleInformation.NameString3
            TempGridRow.Cells(8).Style = ReadOnlyCellStyle
            TempGridRow.Cells(9).Value = TempTitleInformation.WWEDefault1
            TempGridRow.Cells(10).Value = TempTitleInformation.WWEDefault1Name
            TempGridRow.Cells(10).Style = ReadOnlyCellStyle
            TempGridRow.Cells(11).Value = TempTitleInformation.WWEDefault2
            TempGridRow.Cells(12).Value = TempTitleInformation.WWEDefault2Name
            TempGridRow.Cells(12).Style = ReadOnlyCellStyle
            TempGridRow.Cells(13).Value = TempTitleInformation.UniDefault1
            TempGridRow.Cells(14).Value = TempTitleInformation.UniDefault1Name
            TempGridRow.Cells(14).Style = ReadOnlyCellStyle
            TempGridRow.Cells(15).Value = TempTitleInformation.UniDefault2
            TempGridRow.Cells(16).Value = TempTitleInformation.UniDefault2Name
            TempGridRow.Cells(16).Style = ReadOnlyCellStyle
            TempGridRow.Cells(17).Value = TempTitleInformation.Temp1
            TempGridRow.Cells(18).Value = TempTitleInformation.TitleTypeByte
            TempGridRow.Cells(19).Value = TempTitleInformation.Female
            TempGridRow.Cells(20).Value = TempTitleInformation.TagTeam
            TempGridRow.Cells(21).Value = TempTitleInformation.Cuiserweight
            TempGridRow.Cells(22).Value = TempTitleInformation.UnlockNum
            TempGridRow.Cells(23).Value = TempTitleInformation.Temp4
            TempGridRow.HeaderCell.Value = i.ToString
            TempGridRow.Tag = TempTitleInformation
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridTitleView.Rows.AddRange(WorkingCollection.ToArray)
        If StringRead Then 'True
            If PacsRead Then 'Strings and Pacs Read
                'Show String
                DataGridTitleView.Columns(4).Visible = True
                DataGridTitleView.Columns(6).Visible = True
                DataGridTitleView.Columns(8).Visible = True
                'Show Wrestler Names
                DataGridTitleView.Columns(10).Visible = True
                DataGridTitleView.Columns(12).Visible = True
                DataGridTitleView.Columns(14).Visible = True
                DataGridTitleView.Columns(16).Visible = True
            Else 'Strings Read Only
                'Show String
                DataGridTitleView.Columns(4).Visible = True
                DataGridTitleView.Columns(6).Visible = True
                DataGridTitleView.Columns(8).Visible = True
                'Hide Wrestler Names
                DataGridTitleView.Columns(10).Visible = False
                DataGridTitleView.Columns(12).Visible = False
                DataGridTitleView.Columns(14).Visible = False
                DataGridTitleView.Columns(16).Visible = False
            End If
        Else 'Pacs Read Only can't do anything so we don't check it
            'Hide String
            DataGridTitleView.Columns(4).Visible = False
            DataGridTitleView.Columns(6).Visible = False
            DataGridTitleView.Columns(8).Visible = False
            'Hide Wrestler Names
            DataGridTitleView.Columns(10).Visible = False
            DataGridTitleView.Columns(12).Visible = False
            DataGridTitleView.Columns(14).Visible = False
            DataGridTitleView.Columns(16).Visible = False
        End If
        'Adding titles won't do anything until we figure out other information
    End Sub

    Function ParseBytesToTitleInformation(TestedByteArray As Byte()) As TitleInformation
        Dim ReturnedTitleInfo As TitleInformation = New TitleInformation With {
           .Enabled = BitConverter.ToUInt32(TestedByteArray, 0),
           .PropID = BitConverter.ToUInt32(TestedByteArray, 4),
           .MenuNumber = BitConverter.ToUInt32(TestedByteArray, 8),
           .NameRef1 = BitConverter.ToUInt32(TestedByteArray, &HC),
           .NameRef2 = BitConverter.ToUInt32(TestedByteArray, &H10),
           .NameRef3 = BitConverter.ToUInt32(TestedByteArray, &H14)}
        If TestedByteArray.Length = 480 Then '480 1E0 2K19 Titles
            '&h180 00 bytes
            '&h18 FF bytes
            '&h10 00 bytes
            ReturnedTitleInfo.WWEDefault1 = BitConverter.ToUInt32(TestedByteArray, &H1C0)
            ReturnedTitleInfo.WWEDefault2 = BitConverter.ToUInt32(TestedByteArray, &H1C4)
            ReturnedTitleInfo.UniDefault1 = BitConverter.ToUInt32(TestedByteArray, &H1C8)
            ReturnedTitleInfo.UniDefault2 = BitConverter.ToUInt32(TestedByteArray, &H1CC)
            ReturnedTitleInfo.Temp1 = BitConverter.ToUInt32(TestedByteArray, &H1D0)
            ReturnedTitleInfo.TitleTypeByte = BitConverter.ToUInt32(TestedByteArray, &H1D4) 'contains Gender and tag information
            ReturnedTitleInfo.UnlockNum = BitConverter.ToUInt32(TestedByteArray, &H1D8)
            ReturnedTitleInfo.Temp4 = BitConverter.ToUInt32(TestedByteArray, &H1DC)
            If StringRead Then
                ReturnedTitleInfo.NameString1 = StringReferences(ReturnedTitleInfo.NameRef1)
                ReturnedTitleInfo.NameString2 = StringReferences(ReturnedTitleInfo.NameRef2)
                ReturnedTitleInfo.NameString3 = StringReferences(ReturnedTitleInfo.NameRef3)
                If PacsRead Then
                    ReturnedTitleInfo.WWEDefault1Name = StringReferences(PacNumbers(ReturnedTitleInfo.WWEDefault1))
                    ReturnedTitleInfo.WWEDefault2Name = StringReferences(PacNumbers(ReturnedTitleInfo.WWEDefault2))
                    ReturnedTitleInfo.UniDefault1Name = StringReferences(PacNumbers(ReturnedTitleInfo.UniDefault1))
                    ReturnedTitleInfo.UniDefault2Name = StringReferences(PacNumbers(ReturnedTitleInfo.UniDefault2))
                End If
            End If
        End If
        'Read TitleTypeByte and translate to Title SEttings
        Dim TitleType As Integer = ReturnedTitleInfo.TitleTypeByte Mod 8
        If TitleType >= 4 Then
            ReturnedTitleInfo.Cuiserweight = True
        End If
        TitleType = TitleType Mod 4
        If TitleType >= 2 Then
            ReturnedTitleInfo.TagTeam = True
        End If
        TitleType = TitleType Mod 2
        If TitleType >= 1 Then
            ReturnedTitleInfo.Female = True
        End If
        Return ReturnedTitleInfo
    End Function

    Function GetBytesFromTitleInformationDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        If TitleGameComboBox.SelectedIndex = 4 Then
            Dim ReturnedBytes As Byte() = New Byte(&H1E0 - 1) {}
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, 4, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(2).Value)), 0, ReturnedBytes, 8, 4)
            Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, &HC, 4)
            Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, &H10, 4)
            Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, &H14, 4)
            Array.Copy(New Byte() {
                       &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF,
                       &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF,
                       &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF}, 0, ReturnedBytes, &H198, &H18)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(9).Value)), 0, ReturnedBytes, &H1C0, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(11).Value)), 0, ReturnedBytes, &H1C4, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(13).Value)), 0, ReturnedBytes, &H1C8, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(15).Value)), 0, ReturnedBytes, &H1CC, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(17).Value)), 0, ReturnedBytes, &H1D0, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(18).Value)), 0, ReturnedBytes, &H1D4, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(22).Value)), 0, ReturnedBytes, &H1D8, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(23).Value)), 0, ReturnedBytes, &H1DC, 4)
            Return ReturnedBytes
        Else
            MessageBox.Show("Build Failure")
            Return Nothing
        End If
    End Function

    Private Sub DataGridTitleView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridTitleView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex < 3 Then
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            Else
                If e.ColumnIndex = 0 Then
                    'Enabled Make sure between 0 & 6 inclusive
                    If CInt(MyCell.Value) = 0 OrElse
                    CInt(MyCell.Value) > 6 Then
                        MyCell.Value = OldValue
                    End If
                ElseIf e.ColumnIndex = 1 Then
                    'Prop Number, Verify Number <= 9999
                    If CInt(MyCell.Value) < 0 OrElse
                    CInt(MyCell.Value) > 9999 Then
                        MyCell.Value = OldValue
                    End If
                ElseIf e.ColumnIndex = 2 Then
                    'Menu Number, Verify Number <= 1024
                    If CInt(MyCell.Value) < 0 OrElse
                    CInt(MyCell.Value) > 1024 Then
                        MyCell.Value = OldValue
                    End If
                End If
            End If
        ElseIf e.ColumnIndex < 9 Then
            'String Reference Information
            If Not GeneralTools.HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CUInt("&H" & MyCell.Value) > StringReferences.LongCount Then
                MyCell.Value = OldValue
            Else
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & MyCell.Value))
            End If
        ElseIf e.ColumnIndex > 8 AndAlso e.ColumnIndex < 17 Then
            'Wrestler reference Number Make sure between 0 & 1024 inclusive
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
                   CInt(MyCell.Value) > 1024 Then
                MyCell.Value = OldValue
            Else
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(PacNumbers(CUInt(MyCell.Value)))
            End If
        ElseIf e.ColumnIndex < 19 Then
            'Temp1 and Title temp byte
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
                   CInt(MyCell.Value) > 9999 Then
                MyCell.Value = OldValue
            ElseIf e.ColumnIndex = 18 Then
                Dim TitleType As Integer = CInt(MyCell.Value) Mod 8
                If TitleType >= 4 Then
                    'Cruiser-weight
                    DataGridTitleView.Rows(e.RowIndex).Cells(21).Value = True
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(21).Value = False
                End If
                TitleType = TitleType Mod 4
                If TitleType >= 2 Then
                    'Tag Title
                    DataGridTitleView.Rows(e.RowIndex).Cells(20).Value = True
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(20).Value = False
                End If
                TitleType = TitleType Mod 2
                If TitleType >= 1 Then
                    'Female Title
                    DataGridTitleView.Rows(e.RowIndex).Cells(19).Value = True
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(19).Value = False
                End If
            End If
        ElseIf e.ColumnIndex < 22 Then
            Dim OldTitleType As Integer = CInt(DataGridTitleView.Rows(e.RowIndex).Cells(18).Value)
            If e.ColumnIndex = 19 Then 'Female Check box
                If DataGridTitleView.Rows(e.RowIndex).Cells(19).Value Then
                    If (OldTitleType Mod 2) >= 1 Then
                        'do nothing already Female
                    Else
                        OldTitleType += 1
                    End If
                Else
                    If (OldTitleType Mod 2) >= 1 Then
                        OldTitleType = OldTitleType - 1
                    Else
                        'do nothing already Not Female
                    End If
                End If
            ElseIf e.ColumnIndex = 20 Then 'Tag Team Check box
                If DataGridTitleView.Rows(e.RowIndex).Cells(20).Value Then
                    If (OldTitleType Mod 4) >= 2 Then
                        'do nothing already Tag Team
                    Else
                        OldTitleType += 2
                    End If
                Else
                    If (OldTitleType Mod 4) >= 2 Then
                        OldTitleType = OldTitleType - 2
                    Else
                        'do nothing already Not Tag Team
                    End If
                End If
            ElseIf e.ColumnIndex = 21 Then 'Cruiser-weight Check box
                If DataGridTitleView.Rows(e.RowIndex).Cells(21).Value Then
                    If (OldTitleType Mod 8) >= 4 Then
                        'do nothing already Cruise-wight
                    Else
                        OldTitleType += 4
                    End If
                Else
                    If (OldTitleType Mod 8) >= 4 Then
                        OldTitleType = OldTitleType - 4
                    Else
                        'do nothing already Not Cruise-wight
                    End If
                End If
            End If
            DataGridTitleView.Rows(e.RowIndex).Cells(18).Value = OldTitleType
        ElseIf e.ColumnIndex > 21 Then
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CLng(MyCell.Value) < 0 OrElse
                    CLng(MyCell.Value) > UInt32.MaxValue Then
                MyCell.Value = OldValue
            End If
        End If
        SavePending = True
        SaveChangesTitleMenuItem.Visible = True
    End Sub

    Private Sub SaveTitleChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesTitleMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildTitleFile())
    End Sub

    Function BuildTitleFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte() {}
        If TitleGameComboBox.SelectedIndex = 4 Then
            '2K19
            ReturnedBytes = New Byte(&H10 + (DataGridTitleView.RowCount * &H1E0) - 1) {}
            'copy first 10 bytes from existing file
            Array.Copy(ShowBytes, ReturnedBytes, &H10)
            'rewriting for the heck of it
            ReturnedBytes(8) = DataGridTitleView.RowCount
            For i As Integer = 0 To DataGridTitleView.RowCount - 1
                Dim TempBytes As Byte() = GetBytesFromTitleInformationDataGridRow(DataGridTitleView.Rows(i))
                Array.Copy(TempBytes, 0, ReturnedBytes, &H10 + i * TempBytes.Length, TempBytes.Length)
            Next
        End If
        Return ReturnedBytes
    End Function

#End Region

#Region "Sound Ref View"

    Dim SoundContainerCount As UInt32 = 0

    Sub FillSoundRefFileView(SelectedData As TreeNode)
        Dim SoundRefBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        SoundContainerCount = BitConverter.ToUInt32(SoundRefBytes, &HC)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridSoundView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SoundContainerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SoundContainerCount - 1
            Dim ContainerName As Integer = BitConverter.ToInt32(SoundRefBytes, &H10 + i * &HC + 0)
            Dim ContainerIndex As Integer = BitConverter.ToInt32(SoundRefBytes, &H10 + i * &HC + 4)
            Dim ContainerSubCount As Integer = BitConverter.ToInt16(SoundRefBytes, &H10 + i * &HC + 8)
            If ContainerSubCount = 0 Then
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = ContainerName
                TempGridRow.Cells(0).Style = ReadOnlyCellStyle
                TempGridRow.Cells(1).Value = ""
                TempGridRow.Cells(2).Value = ""
                TempGridRow.Cells(3).Value = Hex(&H10 + SoundContainerCount * &HC + ContainerIndex)
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
                WorkingCollection.Add(TempGridRow)
            Else
                For J As Integer = 0 To ContainerSubCount - 1
                    Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                    TempGridRow.Cells(0).Value = ContainerName
                    TempGridRow.Cells(0).Style = ReadOnlyCellStyle
                    'reference number
                    TempGridRow.Cells(1).Value = BitConverter.ToUInt32(SoundRefBytes, &H10 + SoundContainerCount * &HC + ContainerIndex + J * 8)
                    'Hex Text
                    TempGridRow.Cells(2).Value = Hex(BitConverter.ToUInt32(SoundRefBytes, &H10 + SoundContainerCount * &HC + ContainerIndex + J * 8 + 4))
                    TempGridRow.Cells(3).Value = Hex(&H10 + SoundContainerCount * &HC + ContainerIndex + J * 8)
                    TempGridRow.Cells(3).Style = ReadOnlyCellStyle
                    WorkingCollection.Add(TempGridRow)
                Next
            End If
            ProgressBar1.Value = i
        Next
        'FullSoundCollection.AddRange(WorkingCollection.ToArray())
        DataGridSoundView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridSoundView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridSoundView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridSoundView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 1 'Must be an integer
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > Int32.MaxValue Then
                    MyCell.Value = OldValue
                Else MyCell.Value = CInt(MyCell.Value)
                End If
            Case 2 'must be Hex String
                If Not GeneralTools.HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                End If
        End Select
        SavePending = True
        SaveChangesSoundMenuItem.Visible = True
    End Sub

    Private Sub DataGridSoundView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridSoundView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 4 Then 'add button
                'This function adds a duplicate row at index + 1
                Dim Duplicaterow As DataGridViewRow = DataGridSoundView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridSoundView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridSoundView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridSoundView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                For i As Integer = e.RowIndex + 1 To DataGridSoundView.Rows.Count - 1
                    DataGridSoundView.Rows(i).Cells(3).Value = Hex(CUInt("&h" & DataGridSoundView.Rows(i).Cells(3).Value) + 8)
                Next
                SavePending = True
                SaveChangesSoundMenuItem.Visible = True
            ElseIf e.ColumnIndex = 5 Then 'Delete button
                'we don't want to delete it if there are no other objects in the container.
                If Not (LastIteminContainerCheck(e.RowIndex)) Then
                    DataGridSoundView.Rows.RemoveAt(e.RowIndex)
                    For i As Integer = e.RowIndex To DataGridSoundView.Rows.Count - 1
                        DataGridSoundView.Rows(i).Cells(3).Value = Hex(CUInt("&H" & DataGridSoundView.Rows(i).Cells(3).Value) - 8)
                    Next
                    SavePending = True
                    SaveChangesSoundMenuItem.Visible = True
                End If
            Else
                'do nothing
            End If
        End If
    End Sub

    Function LastIteminContainerCheck(IndexonMenu As Integer) As Boolean
        If IndexonMenu > 0 Then
            If DataGridSoundView.Rows(IndexonMenu).Cells(0).Value = DataGridSoundView.Rows(IndexonMenu - 1).Cells(0).Value Then
                Return False
            End If
        End If
        If IndexonMenu < DataGridSoundView.Rows.Count Then
            If DataGridSoundView.Rows(IndexonMenu).Cells(0).Value = DataGridSoundView.Rows(IndexonMenu + 1).Cells(0).Value Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub SaveChangesSoundMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesSoundMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildSoundRefFile())
    End Sub

    Function BuildSoundRefFile() As Byte()
        Dim SoundInformationBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(&H10 + SoundContainerCount * &HC + DataGridSoundView.Rows.Count * 8 - 1) {}
        Dim ContainerNumber As Int32 = -1
        Dim ContainerCount As Int32 = -1 'use as a 0 index
        Dim ContainerStart As Int32 = 0
        Dim ObjectCount As UInt32 = 0
        Dim EmptyRows As UInt32 = 0
        'First we are going to Build the header
        ReturnedBytes(0) = &H67
        ReturnedBytes(4) = &H10
        ReturnedBytes(&HC) = SoundContainerCount
        'Next we want to build all of the header containers
        For i As Integer = 0 To DataGridSoundView.Rows.Count - 1
            If CInt(DataGridSoundView.Rows(i).Cells(0).Value) = ContainerNumber Then
                ObjectCount += 1
            Else
                'We have to capture the new container
                If ContainerCount = -1 Then
                    'Just make the new container we don't need to write it
                    ContainerNumber = CInt(DataGridSoundView.Rows(i).Cells(0).Value)
                    ContainerCount = 0
                    ContainerStart = i - EmptyRows
                    ObjectCount = 1
                Else
                    'we want to write the header array
                    Array.Copy(BitConverter.GetBytes(CUInt(ContainerNumber)), 0, ReturnedBytes, &H10 + ContainerCount * &HC, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(ContainerStart * 8)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 4, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(ObjectCount)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 8, 4)
                    Array.Copy(New Byte() {&HF0}, 0, ReturnedBytes, &H10 + ContainerCount * &HC + &HB, 1)
                    ContainerNumber = CInt(DataGridSoundView.Rows(i).Cells(0).Value)
                    'MessageBox.Show(ContainerNumber)
                    ContainerCount += 1
                    ContainerStart = i - EmptyRows
                    ObjectCount = 0
                    If Not DataGridSoundView.Rows(i).Cells(1).Value.ToString = "" AndAlso Not DataGridSoundView.Rows(i).Cells(2).Value.ToString = "" Then
                        ObjectCount = 1
                    Else
                        EmptyRows += 1
                    End If
                End If
            End If
            'Now we want to write the bytes of the array since we store and update the offset
            If Not DataGridSoundView.Rows(i).Cells(1).Value.ToString = "" Then
                If Not DataGridSoundView.Rows(i).Cells(2).Value.ToString = "" Then
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridSoundView.Rows(i).Cells(1).Value)), 0, ReturnedBytes, CUInt("&H" & DataGridSoundView.Rows(i).Cells(3).Value.ToString), 4)
                    Array.Copy(GeneralTools.HexStringToByte(DataGridSoundView.Rows(i).Cells(2).Value.ToString.PadLeft(8, "0"), True), 0, ReturnedBytes, CUInt("&H" & DataGridSoundView.Rows(i).Cells(3).Value.ToString) + 4, 4)
                End If
            End If
        Next
        'Now we want to add the last container bytes
        Array.Copy(BitConverter.GetBytes(CUInt(ContainerNumber)), 0, ReturnedBytes, &H10 + ContainerCount * &HC, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(ContainerStart * 8)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 4, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(ObjectCount)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 8, 4)
        Array.Copy(New Byte() {&HF0}, 0, ReturnedBytes, &H10 + ContainerCount * &HC + &HB, 1)
        'now we want to trim the empty row count of rows from the end
        ReDim Preserve ReturnedBytes(&H10 + SoundContainerCount * &HC + DataGridSoundView.Rows.Count * 8 - 1 - EmptyRows * 8)
        Array.Copy(BitConverter.GetBytes(CUInt(ReturnedBytes.Length - &H10)), 0, ReturnedBytes, 8, 4)
        Return ReturnedBytes
    End Function

#Region "Sound Search Bar"

    Private Sub ToolStripSoundRefSearch_Enter(sender As Object, e As EventArgs) Handles ToolStripSoundRefSearch.Enter
        If ToolStripSoundRefSearch.Text = "Search..." Then
            ToolStripSoundRefSearch.Text = ""
        End If
    End Sub

    Private Sub ToolStripSoundRefSearch_Leave(sender As Object, e As EventArgs) Handles ToolStripSoundRefSearch.Leave
        'FullSoundCollection = DataGridSoundView.Rows
        If ToolStripSoundRefSearch.Text = "" Then
            ToolStripSoundRefSearch.Text = "Search..."
        End If
        Dim TemporaryCollection As DataGridViewRow() = New DataGridViewRow(DataGridSoundView.Rows.Count - 1) {}
        DataGridSoundView.Rows.CopyTo(TemporaryCollection, 0)
        DataGridSoundView.Rows.Clear()
        ProgressBar1.Maximum = TemporaryCollection.Count - 1
        ProgressBar1.Value = 0
        If ToolStripSoundRefSearch.Text = "" OrElse
            ToolStripSoundRefSearch.Text = "Search..." Then
            For i As Integer = 0 To TemporaryCollection.Count - 1
                TemporaryCollection(i).Visible = True
                ProgressBar1.Value = i
            Next
        Else
            For i As Integer = 0 To TemporaryCollection.Count - 1
                If TemporaryCollection(i).Cells(1).Value.ToString.ToLower.Contains(ToolStripSoundRefSearch.Text.ToLower) Then
                    TemporaryCollection(i).Visible = True
                Else
                    TemporaryCollection(i).Visible = False
                End If
                ProgressBar1.Value = i
            Next
        End If
        DataGridSoundView.Rows.AddRange(TemporaryCollection.ToArray)
    End Sub

#End Region

#End Region

#Region "CAE Menu Item View"

    Public Class CreateEntranceInformation
        Public EventID As UInt32 = 0
        Public PacNumber1 As UInt16 = 0
        Public PacName1 As String = ""
        Public PacNumber2 As UInt16 = 0
        Public PacName2 As String = ""
        Public PacNumber3 As UInt16 = 0
        Public PacName3 As String = ""
        Public PacNumber4 As UInt16 = 0
        Public PacName4 As String = ""
        Public PacNumber5 As UInt16 = 0
        Public PacName5 As String = ""
        Public PacDefaultRef As UInt16 = 0
        Public HasPacDefault As Boolean = False
        Public Promo1 As UInt16 = 0
        Public Promo2 As UInt16 = 0
        Public Promo3 As Byte = 0
        Public Promo4 As Byte = 0
        Public BufferBytes As UInt16 = 0
        Public StringReference As UInt32 = 0
        Public StringText As String = ""
        Public Unkown1 As UInt16 = 0
        Public Unkown2 As UInt16 = 0
        Public PacLockedNum As UInt16 = 0
        Public PacExcludedNum As UInt16 = 0
        Public DLCFlagNum As Boolean = 0
    End Class

    Sub FillMenuItemView(SelectedData As TreeNode)
        Dim CAEMenuBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim CAEMenuContainerCount As UInt16 = BitConverter.ToUInt16(CAEMenuBytes, 0)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMenuItemView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = CAEMenuContainerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To CAEMenuContainerCount - 1
            Dim TempCAEMenuBytes As Byte() = New Byte(&H28 - 1) {}
            Array.Copy(CAEMenuBytes, &H4 + i * &H28, TempCAEMenuBytes, 0, &H28)
            Dim TempCAEMenuInformation As CreateEntranceInformation = ParseBytesToCAEInformation(TempCAEMenuBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempCAEMenuInformation.EventID
            TempGridRow.Cells(1).Value = Hex(TempCAEMenuInformation.StringReference)
            TempGridRow.Cells(2).Value = TempCAEMenuInformation.StringText
            TempGridRow.Cells(2).Style = ReadOnlyCellStyle
            'Pac Num 1
            TempGridRow.Cells(3).Value = TempCAEMenuInformation.PacNumber1
            TempGridRow.Cells(4).Value = TempCAEMenuInformation.PacName1
            TempGridRow.Cells(4).Style = ReadOnlyCellStyle
            TempGridRow.Cells(5).Value = TempCAEMenuInformation.PacNumber2
            TempGridRow.Cells(6).Value = TempCAEMenuInformation.PacName2
            TempGridRow.Cells(6).Style = ReadOnlyCellStyle
            TempGridRow.Cells(7).Value = TempCAEMenuInformation.PacNumber3
            TempGridRow.Cells(8).Value = TempCAEMenuInformation.PacName3
            TempGridRow.Cells(8).Style = ReadOnlyCellStyle
            TempGridRow.Cells(9).Value = TempCAEMenuInformation.PacNumber4
            TempGridRow.Cells(10).Value = TempCAEMenuInformation.PacName4
            TempGridRow.Cells(10).Style = ReadOnlyCellStyle
            TempGridRow.Cells(11).Value = TempCAEMenuInformation.PacNumber5
            TempGridRow.Cells(12).Value = TempCAEMenuInformation.PacName5
            TempGridRow.Cells(12).Style = ReadOnlyCellStyle
            'Has Pac
            TempGridRow.Cells(13).Value = TempCAEMenuInformation.HasPacDefault
            'Add Read Only Style in reverse
            If Not TempCAEMenuInformation.HasPacDefault Then
                TempGridRow.Cells(3).ReadOnly = True
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
            End If
            For J As Integer = 5 To 11 Step 2
                If TempGridRow.Cells(J - 2).Value = &HFFFF Then
                    TempGridRow.Cells(J).ReadOnly = True
                    TempGridRow.Cells(J).Style = ReadOnlyCellStyle
                End If
            Next
            'Promo Columns
            TempGridRow.Cells(14).Value = TempCAEMenuInformation.Promo1
            TempGridRow.Cells(15).Value = TempCAEMenuInformation.Promo2
            TempGridRow.Cells(16).Value = TempCAEMenuInformation.Promo3
            TempGridRow.Cells(17).Value = TempCAEMenuInformation.Promo4
            'Tests
            TempGridRow.Cells(18).Value = TempCAEMenuInformation.BufferBytes
            TempGridRow.Cells(19).Value = TempCAEMenuInformation.Unkown1
            TempGridRow.Cells(20).Value = TempCAEMenuInformation.Unkown2
            TempGridRow.Cells(21).Value = TempCAEMenuInformation.PacLockedNum
            TempGridRow.Cells(22).Value = TempCAEMenuInformation.PacExcludedNum
            TempGridRow.Cells(23).Value = TempCAEMenuInformation.DLCFlagNum
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridMenuItemView.Rows.AddRange(WorkingCollection.ToArray())
        'Here we want to hide some columns depending on what type of file we are working with.
        If DataGridMenuItemView.Rows(0).Cells(14).Value = &HFFFF Then
            'Not Promo
            For i As Integer = 3 To 13
                DataGridMenuItemView.Columns(i).Visible = True
            Next
            For i As Integer = 14 To 17
                DataGridMenuItemView.Columns(i).Visible = False
            Next
            For i As Integer = 18 To 22
                DataGridMenuItemView.Columns(i).Visible = True
            Next
        Else
            'Here is a promo file
            For i As Integer = 3 To 13
                DataGridMenuItemView.Columns(i).Visible = False
            Next
            For i As Integer = 14 To 17
                DataGridMenuItemView.Columns(i).Visible = True
            Next
            For i As Integer = 18 To 22
                DataGridMenuItemView.Columns(i).Visible = False
            Next
        End If
        'This will hide Columns if Pac Numbers or String Refs if they are not loaded.
        If StringRead Then 'True
            If PacsRead Then 'Strings and Pacs Read
                'Show String
                DataGridMenuItemView.Columns(2).Visible = True
                ''Show Wrestler Names
                DataGridMenuItemView.Columns(4).Visible = True
                DataGridMenuItemView.Columns(6).Visible = True
                DataGridMenuItemView.Columns(8).Visible = True
                DataGridMenuItemView.Columns(10).Visible = True
                DataGridMenuItemView.Columns(12).Visible = True
            Else 'Strings Read Only
                'Show String
                DataGridMenuItemView.Columns(2).Visible = True
                ''Hide Wrestler Names
                DataGridMenuItemView.Columns(4).Visible = False
                DataGridMenuItemView.Columns(6).Visible = False
                DataGridMenuItemView.Columns(8).Visible = False
                DataGridMenuItemView.Columns(10).Visible = False
                DataGridMenuItemView.Columns(12).Visible = False
            End If
        Else 'Pacs Read Only can't do anything so we don't check it
            ''Hide String
            DataGridMenuItemView.Columns(2).Visible = False
            'Hide Wrestler Names
            DataGridMenuItemView.Columns(4).Visible = False
            DataGridMenuItemView.Columns(6).Visible = False
            DataGridMenuItemView.Columns(8).Visible = False
            DataGridMenuItemView.Columns(10).Visible = False
            DataGridMenuItemView.Columns(12).Visible = False
        End If
        DataGridMenuItemView.Columns(18).Visible = False
    End Sub

    Function ParseBytesToCAEInformation(TestedByteArray As Byte()) As CreateEntranceInformation
        Dim ReturnedCAEInfo As CreateEntranceInformation = New CreateEntranceInformation With {
           .EventID = BitConverter.ToUInt32(TestedByteArray, 0),
           .PacNumber1 = BitConverter.ToUInt16(TestedByteArray, 4),
           .PacNumber2 = BitConverter.ToUInt16(TestedByteArray, 6),
           .PacNumber3 = BitConverter.ToUInt16(TestedByteArray, 8),
           .PacNumber4 = BitConverter.ToUInt16(TestedByteArray, &HA),
           .PacNumber5 = BitConverter.ToUInt16(TestedByteArray, &HC),
           .PacDefaultRef = BitConverter.ToUInt16(TestedByteArray, &HE),
           .Promo1 = BitConverter.ToUInt16(TestedByteArray, &H10),
           .Promo2 = BitConverter.ToUInt16(TestedByteArray, &H12),
           .Promo3 = TestedByteArray(&H14),
           .Promo4 = TestedByteArray(&H15),
           .BufferBytes = BitConverter.ToUInt16(TestedByteArray, &H16),
           .StringReference = BitConverter.ToUInt32(TestedByteArray, &H18),
           .Unkown1 = BitConverter.ToUInt16(TestedByteArray, &H1C),
           .Unkown2 = BitConverter.ToUInt16(TestedByteArray, &H1E),
           .PacLockedNum = BitConverter.ToUInt16(TestedByteArray, &H20),
           .PacExcludedNum = BitConverter.ToUInt16(TestedByteArray, &H22),
           .DLCFlagNum = BitConverter.ToBoolean(TestedByteArray, &H24)}
        If ReturnedCAEInfo.PacDefaultRef = 0 Then
            ReturnedCAEInfo.HasPacDefault = True
        Else
            ReturnedCAEInfo.HasPacDefault = False
        End If
        If StringRead Then 'True
            ReturnedCAEInfo.StringText = StringReferences(ReturnedCAEInfo.StringReference)
            If PacsRead Then
                'Strings and Pacs Read
                ReturnedCAEInfo.PacName1 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber1))
                ReturnedCAEInfo.PacName2 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber2))
                ReturnedCAEInfo.PacName3 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber3))
                ReturnedCAEInfo.PacName4 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber4))
                ReturnedCAEInfo.PacName5 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber5))
            End If
        End If
        Return ReturnedCAEInfo
    End Function

    Function GetBytesFromCAEMenuInformationDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&H28 - 1) {}
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, &H18, 4)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, 4, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, 6, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, 8, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(9).Value)), 0, ReturnedBytes, &HA, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(11).Value)), 0, ReturnedBytes, &HC, 2)
        If RequestedByteRow.Cells(13).Value Then
            Array.Copy(BitConverter.GetBytes(CUShort(0)), 0, ReturnedBytes, &HE, 2)
        Else
            Array.Copy(BitConverter.GetBytes(CUShort(&HFFFF)), 0, ReturnedBytes, &HE, 2)
        End If
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(14).Value)), 0, ReturnedBytes, &H10, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(15).Value)), 0, ReturnedBytes, &H12, 2)
        ReturnedBytes(&H14) = CByte(RequestedByteRow.Cells(16).Value)
        ReturnedBytes(&H15) = CByte(RequestedByteRow.Cells(17).Value)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(18).Value)), 0, ReturnedBytes, &H16, 2)        '
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(19).Value)), 0, ReturnedBytes, &H1C, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(20).Value)), 0, ReturnedBytes, &H1E, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(21).Value)), 0, ReturnedBytes, &H20, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(22).Value)), 0, ReturnedBytes, &H22, 2)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(23).Value)), 0, ReturnedBytes, &H24, 4)
        Return ReturnedBytes
    End Function

    Private Sub DataGridMenuItemView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMenuItemView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 0 'Must be an integer < Uin32
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt32.MaxValue Then
                    MyCell.Value = OldValue
                Else MyCell.Value = CInt(MyCell.Value)
                End If
            Case 1 'must be Hex String
                If Not GeneralTools.HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    If StringRead Then
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(2).Value = StringReferences(CUInt("&H" & MyCell.Value))
                    End If
                End If
            Case 3, 5, 7, 9, 11
                'Pac Numbers
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    If CUShort(MyCell.Value) > &H400 Then
                        MyCell.Value = CUShort(&HFFFF)
                        'Make cell to the right Read Only
                        If Not e.ColumnIndex = 11 Then
                            For i As Integer = e.ColumnIndex + 2 To 11
                                DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Value = CUShort(&HFFFF)
                                DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Style = ReadOnlyCellStyle
                                DataGridMenuItemView.Rows(e.RowIndex).Cells(i).ReadOnly = True
                            Next
                        End If
                    Else
                        MyCell.Value = CUShort(MyCell.Value)
                        If StringRead And PacsRead Then
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(PacNumbers(CUShort(MyCell.Value)))
                        End If
                        'this is now a pac number meaning we need to enable the pac numbers to the right if there is one
                        If Not e.ColumnIndex = 11 Then
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).Value = CUShort(&HFFFF)
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).Style = New DataGridViewCellStyle()
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).ReadOnly = False
                        End If
                    End If
                End If
            Case 13
                'CheckBox change
                If MyCell.Value Then
                    'Pacs Enabled
                    DataGridMenuItemView.Rows(e.RowIndex).Cells(3).Style = New DataGridViewCellStyle()
                    DataGridMenuItemView.Rows(e.RowIndex).Cells(3).ReadOnly = False
                Else
                    'Pacs Disabled
                    For i As Integer = 3 To 11
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Value = CUShort(&HFFFF)
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Style = ReadOnlyCellStyle
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(i).ReadOnly = True
                    Next
                End If
            Case 14, 15, 19, 20
                'promo uint16 & Unknown uin16
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CUShort(MyCell.Value)
                End If
            Case 16, 17
                'promo bytes
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > &HFF Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CByte(MyCell.Value)
                End If
            Case 18
                'Buffer Bytes
                MyCell.Value = CUShort(&HFFFF)
            Case 19, 20
                'Pac Numbers for lock defaults to 0
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    If CUShort(MyCell.Value) > &H400 Then
                        MyCell.Value = CUShort(0)
                    Else
                        MyCell.Value = CUShort(MyCell.Value)
                    End If
                End If
        End Select
        SavePending = True
        SaveChangesCAEMenuItemMenuItem.Visible = True
    End Sub

    Private Sub DataGridMenuItemView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMenuItemView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 24 Then 'add button
                'This function adds a duplicate row at index + 1
                Dim Duplicaterow As DataGridViewRow = DataGridMenuItemView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridMenuItemView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridMenuItemView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                SavePending = True
                SaveChangesCAEMenuItemMenuItem.Visible = True
            ElseIf e.ColumnIndex = 25 Then 'Delete button
                DataGridMenuItemView.Rows.RemoveAt(e.RowIndex)
                SavePending = True
                SaveChangesCAEMenuItemMenuItem.Visible = True
            Else
                'do nothing
            End If
        End If
    End Sub

    Private Sub SaveChangesCAEMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesCAEMenuItemMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildMenuItemFile())
    End Sub

    Function BuildMenuItemFile() As Byte()
        Dim CAEMenuBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(&H4 + (DataGridMenuItemView.RowCount * &H28) - 1) {}
        'Write the contained count
        Array.Copy(BitConverter.GetBytes(CUShort(DataGridMenuItemView.RowCount)), 0, ReturnedBytes, 0, 2)
        'now we need to get the second short which might be longer than the actual count
        Dim IncrementAmount As UInt16 = BitConverter.ToUInt16(CAEMenuBytes, 2) - BitConverter.ToUInt16(CAEMenuBytes, 0)
        Array.Copy(BitConverter.GetBytes(CUShort(DataGridMenuItemView.RowCount + IncrementAmount)), 0, ReturnedBytes, 2, 2)
        For i As Integer = 0 To DataGridMenuItemView.RowCount - 1
            Dim TempBytes As Byte() = GetBytesFromCAEMenuInformationDataGridRow(DataGridMenuItemView.Rows(i))
            Array.Copy(TempBytes, 0, ReturnedBytes, &H4 + i * &H28, &H28)
        Next
        Return ReturnedBytes
    End Function

#End Region

#Region "Animation View"

    Public Enum YANMBone As Integer
        Unknown = 0
        vector = &HC5879E5
        J_Hips = &HF91A7A26
        Ch_rotation = &HDAECC2E
        J_Spine1 = &H3D185308
        J_Head = &HF9D973BE
        J_Neck = &HF91972B1
        J_Clavicle_L = &HD153E363
        J_Shoulder_L = &HA46A2F43
        J_Elbow_L = &H6742F8AA
        J_Wrist_L = &HD4C2705A
        J_MiddleF0_L = &HB10CEB4C
        J_MiddleF1_L = &HB10CAB4C
        J_MiddleF2_L = &HB10C6B4C
        J_MiddleF3_L = &HB10C2B4C
        J_Spine2
        J_Chest
        J_Jaw
        J_Tongue1
        J_Tongue2
        J_Tongue3
        J_Tongue4
        J_Eye_L
        J_Eye_R
        H_Neck_tw
        J_IndexF0_L
        J_IndexF1_L
        J_IndexF2_L
        J_IndexF3_L
        J_PinkyF0_L
        J_PinkyF1_L
        J_PinkyF2_L
        J_PinkyF3_L
        J_RingF0_L
        J_RingF1_L
        J_RingF2_L
        J_RingF3_L
        J_ThumbF1_L
        J_ThumbF2_L
        J_ThumbF3_L
        H_Elbow_L_tw01
        H_Elbow_L_tw02
        H_Delt_L_OS01
        H_Ebw_In_L
        H_Ebw_Out_L
        H_TrapBase_L
        J_Clavicle_R
        J_Shoulder_R
        J_Elbow_R
        J_Wrist_R
        J_MiddleF0_R
        J_MiddleF1_R
        J_MiddleF2_R
        J_MiddleF3_R
        J_IndexF0_R
        J_IndexF1_R
        J_IndexF2_R
        J_IndexF3_R
        J_PinkyF0_R
        J_PinkyF1_R
        J_PinkyF2_R
        J_PinkyF3_R
        J_RingF0_R
        J_RingF1_R
        J_RingF2_R
        J_RingF3_R
        J_ThumbF1_R
        J_ThumbF2_R
        J_ThumbF3_R
        H_Elbow_R_tw01
        H_Elbow_R_tw02
        H_Ebw_In_R
        H_Ebw_Out_R
        H_Delt_R_OS01
        H_TrapBase_R
        J_Leg_L
        J_Knee_L
        J_Foot_L
        J_Toe_L
        H_Hip_L_OS1
        H_Kn_L_OS02
        H_Kn_L_OS01
        J_Leg_R
        J_Knee_R
        J_Foot_R
        J_Toe_R
        H_Kn_R_OS02
        H_Kn_R_OS01
        H_Hip_R_OS1
        H_Leg_Vol_C_R
    End Enum

    Public Class AnimationHeaderInformation
        Public StartingID As UInt16 = 0
        Public HeaderLength As UInt16 = 0
        Public BoneType As Int32 = 0
        Public BoneParsed As YANMBone = YANMBone.Unknown
        Public AnimationPartAIndex As UInt32 = 0
        Public AnimationPartALength As UInt32 = 0
        Public AnimationPartBIndex As UInt32 = 0
        Public AnimationPartBLength As UInt32 = 0
        Public RemainingBytes As Byte() = New Byte() {}
        Public RemByteString As String = ""
        Public AnimationPartABytes As Byte() = New Byte() {}
        Public AnimationPartAString As String = ""
        Public AnimationPartBBytes As Byte() = New Byte() {}
        Public AnimationPartBString As String = ""
        Public XTranslation As Boolean = False
        Public YTranslation As Boolean = False
        Public ZTranslation As Boolean = False
        Public XRotation As Boolean = False
        Public YRotation As Boolean = False
        Public ZRotation As Boolean = False
        Public FrameCount As UInt32 = 0
        Public FinalAnimationString As String = ""
        Public FinalAnimationParsed As String = ""
    End Class

    Sub FillAnimationView(SelectedData As TreeNode)
        Dim AnimationBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim AnimationBoneCount As UInt16 = BitConverter.ToUInt16(GeneralTools.EndianReverse(AnimationBytes, &HA, 2), 0)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAnimationView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = AnimationBoneCount - 2
        ProgressBar1.Value = 0
        Dim RollingIndex As UInt32 = &H10
        For i As Integer = 0 To AnimationBoneCount - 2
            Dim TempPartLength As UInt16 = BitConverter.ToUInt16(GeneralTools.EndianReverse(AnimationBytes, RollingIndex + 2, 2), 0)
            Dim TempAnimationBytes As Byte() = New Byte(TempPartLength - 1) {}
            Array.Copy(AnimationBytes, RollingIndex, TempAnimationBytes, 0, TempPartLength)
            RollingIndex += TempPartLength
            If Not TempPartLength = &H30 Then
                If Not TempPartLength = &H18 Then
                    MessageBox.Show(Hex(TempPartLength))
                End If
            End If
            Dim TempAnimationInformation As AnimationHeaderInformation = ParseBytesToAnimationHeaderInformation(TempAnimationBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = i
            TempGridRow.Cells(1).Value = TempAnimationInformation.StartingID
            TempGridRow.Cells(2).Value = Hex(TempAnimationInformation.StartingID)
            TempGridRow.Cells(3).Value = TempAnimationInformation.HeaderLength
            TempGridRow.Cells(4).Value = Hex(TempAnimationInformation.HeaderLength)
            'Try
            TempGridRow.Cells(5).Value = CType(TempAnimationInformation.BoneType, YANMBone).ToString
            'Catch ex As Exception
            'TempGridRow.Cells(5).Value = TempAnimationInformation.BoneType
            'End Try
            TempGridRow.Cells(6).Value = Hex(TempAnimationInformation.BoneType).PadLeft(8, "0")
            TempGridRow.Cells(7).Value = TempAnimationInformation.AnimationPartAIndex
            TempGridRow.Cells(8).Value = Hex(TempAnimationInformation.AnimationPartAIndex)
            TempGridRow.Cells(9).Value = TempAnimationInformation.AnimationPartALength * 8
            TempGridRow.Cells(10).Value = Hex(TempAnimationInformation.AnimationPartALength * 8)
            TempGridRow.Cells(11).Value = TempAnimationInformation.AnimationPartBIndex
            TempGridRow.Cells(12).Value = Hex(TempAnimationInformation.AnimationPartBIndex)
            TempGridRow.Cells(13).Value = TempAnimationInformation.AnimationPartBLength * 8
            TempGridRow.Cells(14).Value = Hex(TempAnimationInformation.AnimationPartBLength * 8)
            TempGridRow.Cells(15).Value = TempAnimationInformation.RemByteString
            'Getting Animation Information from the main file
            TempAnimationInformation.AnimationPartABytes = New Byte(TempAnimationInformation.AnimationPartALength * 8 - 1) {}
            Array.Copy(AnimationBytes, TempAnimationInformation.AnimationPartAIndex + 8, TempAnimationInformation.AnimationPartABytes, 0, TempAnimationInformation.AnimationPartALength * 8)
            TempAnimationInformation.AnimationPartBBytes = New Byte(TempAnimationInformation.AnimationPartBLength * 8 - 1) {}
            Array.Copy(AnimationBytes, TempAnimationInformation.AnimationPartBIndex + 8, TempAnimationInformation.AnimationPartBBytes, 0, TempAnimationInformation.AnimationPartBLength * 8)
            FinishAnimationParse(TempAnimationInformation)
            'Applying the additional information to the additional columns
            TempGridRow.Cells(16).Value = TempAnimationInformation.AnimationPartAString
            TempGridRow.Cells(17).Value = TempAnimationInformation.AnimationPartBString
            TempGridRow.Cells(18).Value = TempAnimationInformation.XTranslation
            TempGridRow.Cells(19).Value = TempAnimationInformation.YTranslation
            TempGridRow.Cells(20).Value = TempAnimationInformation.ZTranslation
            TempGridRow.Cells(21).Value = TempAnimationInformation.XRotation
            TempGridRow.Cells(22).Value = TempAnimationInformation.YRotation
            TempGridRow.Cells(23).Value = TempAnimationInformation.ZRotation
            TempGridRow.Cells(24).Value = TempAnimationInformation.FrameCount
            TempGridRow.Cells(25).Value = Hex(TempAnimationInformation.FrameCount)
            TempGridRow.Cells(26).Value = TempAnimationInformation.FinalAnimationString
            TempGridRow.Cells(27).Value = TempAnimationInformation.FinalAnimationParsed
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        GetAnimationViewDisplayedColumns()
        DataGridAnimationView.Rows.AddRange(WorkingCollection.ToArray())
        For i As Integer = 0 To DataGridAnimationView.Rows.Count - 1
            DataGridAnimationView.Rows(i).HeaderCell.Value = i + 1
        Next

    End Sub

    Function ParseBytesToAnimationHeaderInformation(TestedByteArray As Byte()) As AnimationHeaderInformation
        Dim ReturnedAnimationInfo As AnimationHeaderInformation = New AnimationHeaderInformation With {
           .StartingID = BitConverter.ToUInt16(GeneralTools.EndianReverse(TestedByteArray, 0, 2), 0),
           .HeaderLength = BitConverter.ToUInt16(GeneralTools.EndianReverse(TestedByteArray, 2, 2), 0),
           .BoneType = BitConverter.ToInt32(GeneralTools.EndianReverse(TestedByteArray, 4), 0),
           .AnimationPartAIndex = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, 8), 0),
           .AnimationPartALength = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &HC), 0),
           .AnimationPartBIndex = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H10), 0),
           .AnimationPartBLength = BitConverter.ToUInt32(GeneralTools.EndianReverse(TestedByteArray, &H14), 0)}
        If TestedByteArray.Length > &H18 Then
            ReturnedAnimationInfo.RemainingBytes = New Byte(&H18 - 1) {}
            Array.Copy(TestedByteArray, &H18, ReturnedAnimationInfo.RemainingBytes, 0, &H18)
            ReturnedAnimationInfo.RemByteString = (BitConverter.ToString(ReturnedAnimationInfo.RemainingBytes).Replace("-", " "))
        Else
            ReturnedAnimationInfo.RemainingBytes = New Byte() {}
            ReturnedAnimationInfo.RemByteString = ""

        End If
        Return ReturnedAnimationInfo
    End Function

    Sub FinishAnimationParse(ByRef PartialAnimationInformation As AnimationHeaderInformation)
        PartialAnimationInformation.AnimationPartAString = BitConverter.ToString(PartialAnimationInformation.AnimationPartABytes).Replace("-", " ")
        PartialAnimationInformation.AnimationPartAString = GeneralTools.TruncateString(PartialAnimationInformation.AnimationPartAString, 32000)
        PartialAnimationInformation.AnimationPartBString = BitConverter.ToString(PartialAnimationInformation.AnimationPartBBytes).Replace("-", " ")
        PartialAnimationInformation.AnimationPartBString = GeneralTools.TruncateString(PartialAnimationInformation.AnimationPartBString, 32000)
        If PartialAnimationInformation.StartingID < 1000 Then
            'A is the header to parse
            'here is a check to parse the other byte header if frames = 0
            'Check boxes X - Y - Z Transitional
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 0) = 1 Then
                PartialAnimationInformation.XTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 0) = &HFF00 Then
                PartialAnimationInformation.XTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 0)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 2) = 1 Then
                PartialAnimationInformation.YTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 2) = &HFF00 Then
                PartialAnimationInformation.YTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 2)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 4) = 1 Then
                PartialAnimationInformation.ZTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 4) = &HFF00 Then
                PartialAnimationInformation.ZTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 4)))
            End If

            'Check boxes X - Y - Z Rotational
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 8) = 1 Then
                PartialAnimationInformation.XRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 8) = &HFF00 Then
                PartialAnimationInformation.XRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 8)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 10) = 1 Then
                PartialAnimationInformation.YRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 10) = &HFF00 Then
                PartialAnimationInformation.YRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 10)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 12) = 1 Then
                PartialAnimationInformation.ZRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 12) = &HFF00 Then
                PartialAnimationInformation.ZRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 12)))
            End If
            PartialAnimationInformation.FrameCount = BitConverter.ToUInt16(GeneralTools.EndianReverse(PartialAnimationInformation.AnimationPartABytes, 14, 2), 0)
            PartialAnimationInformation.FinalAnimationString = PartialAnimationInformation.AnimationPartBString
            If PartialAnimationInformation.StartingID = 772 Then
                'Dim TestSubFrames As Integer = 0
                For i As Integer = 0 To PartialAnimationInformation.AnimationPartBLength Step 8
                    'PartialAnimationInformation.FinalAnimationParsed += BitConverter.ToDouble(GeneralTools.EndianReverse(PartialAnimationInformation.AnimationPartBBytes, i, 8), 0).ToString() & ", "
                    'TestSubFrames += PartialAnimationInformation.AnimationPartBBytes(i + 7)
                    For j As Integer = 0 To 5
                        If PartialAnimationInformation.AnimationPartBBytes(i + j) > 128 Then
                            PartialAnimationInformation.FinalAnimationParsed += (-(&HFF - PartialAnimationInformation.AnimationPartBBytes(i + j))).ToString & ", "
                        Else
                            PartialAnimationInformation.FinalAnimationParsed += (PartialAnimationInformation.AnimationPartBBytes(i + j)).ToString & ", "
                        End If
                    Next
                    PartialAnimationInformation.FinalAnimationParsed += vbNewLine
                Next
                'PartialAnimationInformation.FinalAnimationParsed = TestSubFrames
            ElseIf PartialAnimationInformation.StartingID = 516 Then
                Dim TestedFrames As Integer = 0
                For i As Integer = 16 To PartialAnimationInformation.AnimationPartBLength Step 20
                    TestedFrames += BitConverter.ToInt16(GeneralTools.EndianReverse(PartialAnimationInformation.AnimationPartBBytes, i + 4, 2), 0)
                Next
                PartialAnimationInformation.FinalAnimationParsed = TestedFrames
            End If
        Else
            'B is our header to parse
            'Check boxes X - Y - Z Transitional
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 0) = 1 Then
                PartialAnimationInformation.XTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 0) = &HFF00 Then
                PartialAnimationInformation.XTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 0)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 2) = 1 Then
                PartialAnimationInformation.YTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 2) = &HFF00 Then
                PartialAnimationInformation.YTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 2)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 4) = 1 Then
                PartialAnimationInformation.ZTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 4) = &HFF00 Then
                PartialAnimationInformation.ZTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 4)))
            End If

            'Check boxes X - Y - Z Rotational
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 8) = 1 Then
                PartialAnimationInformation.XRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 8) = &HFF00 Then
                PartialAnimationInformation.XRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 8)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 10) = 1 Then
                PartialAnimationInformation.YRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 10) = &HFF00 Then
                PartialAnimationInformation.YRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 10)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 12) = 1 Then
                PartialAnimationInformation.ZRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 12) = &HFF00 Then
                PartialAnimationInformation.ZRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 12)))
            End If
            PartialAnimationInformation.FrameCount = BitConverter.ToUInt16(GeneralTools.EndianReverse(PartialAnimationInformation.AnimationPartBBytes, 14, 2), 0)
            PartialAnimationInformation.FinalAnimationString = PartialAnimationInformation.AnimationPartAString
            If PartialAnimationInformation.StartingID = 772 Then
                'Dim TestSubFrames As Integer = 0
                For i As Integer = 0 To PartialAnimationInformation.AnimationPartALength Step 8
                    'PartialAnimationInformation.FinalAnimationParsed += BitConverter.ToDouble(GeneralTools.EndianReverse(PartialAnimationInformation.AnimationPartABytes, i, 8), 0).ToString() & ", "
                    'TestSubFrames += PartialAnimationInformation.AnimationPartABytes(i + 7)
                    For j As Integer = 0 To 5
                        If PartialAnimationInformation.AnimationPartABytes(i + j) > 128 Then
                            PartialAnimationInformation.FinalAnimationParsed += (-(&HFF - PartialAnimationInformation.AnimationPartABytes(i + j))).ToString & ", "
                        Else
                            PartialAnimationInformation.FinalAnimationParsed += (PartialAnimationInformation.AnimationPartABytes(i + j)).ToString & ", "
                        End If
                    Next
                    PartialAnimationInformation.FinalAnimationParsed += vbNewLine
                Next
                'PartialAnimationInformation.FinalAnimationParsed = TestSubFrames
            ElseIf PartialAnimationInformation.StartingID = 516 Then
                Dim TestedFrames As Integer = 0
                For i As Integer = 16 To PartialAnimationInformation.AnimationPartALength Step 20
                    TestedFrames += BitConverter.ToInt16(GeneralTools.EndianReverse(PartialAnimationInformation.AnimationPartABytes, i + 4, 2), 0)
                Next
                PartialAnimationInformation.FinalAnimationParsed = TestedFrames
            End If
        End If

        '772 = 8 byte chunks
    End Sub

    Sub GetAnimationViewDisplayedColumns()
        For i As Integer = 0 To DataGridAnimationView.Columns.GetColumnCount(DataGridViewElementStates.None) - 1
            DataGridAnimationView.Columns(i).Visible = True
        Next
        If AnimationShowDebugToolStripMenuItem.Text.Contains("☑") Then
            If AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
                'Show Hex Columns and Hide Decimal Columns
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
                'Show Hex Columns and Also Show Decimal Columns
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☒") Then
                'Hide Hex Columns and Show Decimal Columns
            End If
        Else '☐
            DataGridAnimationView.Columns(3).Visible = False
            DataGridAnimationView.Columns(4).Visible = False
            DataGridAnimationView.Columns(7).Visible = False
            DataGridAnimationView.Columns(8).Visible = False
            DataGridAnimationView.Columns(9).Visible = False
            DataGridAnimationView.Columns(10).Visible = False
            DataGridAnimationView.Columns(11).Visible = False
            DataGridAnimationView.Columns(12).Visible = False
            DataGridAnimationView.Columns(13).Visible = False
            DataGridAnimationView.Columns(14).Visible = False
            DataGridAnimationView.Columns(16).Visible = False
            DataGridAnimationView.Columns(17).Visible = False
            If AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
                'Show Hex Columns and Hide Decimal Columns
                DataGridAnimationView.Columns(1).Visible = False
                DataGridAnimationView.Columns(24).Visible = False
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☐") Then
                'Show Hex Columns and Also Show Decimal Columns
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☒") Then
                'Hide Hex Columns and Show Decimal Columns
                DataGridAnimationView.Columns(2).Visible = False
                DataGridAnimationView.Columns(25).Visible = False
            End If
        End If
    End Sub

    Private Sub AnimationShowHexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnimationShowHexToolStripMenuItem.Click
        If AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
            AnimationShowHexToolStripMenuItem.Text = "☐ Show Hex"
        ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☐") Then
            '☐
            AnimationShowHexToolStripMenuItem.Text = "☒ Show Hex"
            '☒
        ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☒") Then
            AnimationShowHexToolStripMenuItem.Text = "☑ Show Hex"
        End If
        GetAnimationViewDisplayedColumns()
    End Sub

    Private Sub AnimationShowDebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnimationShowDebugToolStripMenuItem.Click
        If AnimationShowDebugToolStripMenuItem.Text.Contains("☑") Then
            AnimationShowDebugToolStripMenuItem.Text = "☐ Show Debug"
        ElseIf AnimationShowDebugToolStripMenuItem.Text.Contains("☐") Then
            '☐
            AnimationShowDebugToolStripMenuItem.Text = "☑ Show Debug"
        End If
        GetAnimationViewDisplayedColumns()
    End Sub

#End Region

#Region "Pof0 View"

    Sub FillPof0View(SelectedData As TreeNode)
        Dim ParentFileBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        'After the Parent File we want the Pof0 array
        Dim Pof0Index As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentFileBytes, 4, 4), 0) + 8
        Dim Pof0Length As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentFileBytes, Pof0Index + 4, 4), 0)
        Dim Pof0Bytes As Byte() = New Byte(Pof0Length - 1) {}
        Array.Copy(ParentFileBytes, Pof0Index + 8, Pof0Bytes, 0, Pof0Length)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridPof0View)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = Pof0Length
        ProgressBar1.Value = 0
        Dim Pof0CurrentRead As UInt32 = 0
        Dim ParentFileCurrentOffset As UInt32 = 8
        Do While Pof0CurrentRead < Pof0Length
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            'Parse if the current Pof0 Byte is a 1 byte or 2 byte reference.
            TempGridRow.Cells(0).Value = Hex(Pof0CurrentRead)
            TempGridRow.Cells(0).Style = ReadOnlyCellStyle
            If Pof0Bytes(Pof0CurrentRead) >= &HC0 Then
                '4 Byte ref
                TempGridRow.Cells(1).Value = Hex(BitConverter.ToUInt32(GeneralTools.EndianReverse(Pof0Bytes, Pof0CurrentRead, 4), 0))
                Dim TranslatedLength As UInt32 = 0
                TranslatedLength = (BitConverter.ToUInt32(GeneralTools.EndianReverse(Pof0Bytes, Pof0CurrentRead, 4), 0) - BitConverter.ToUInt32({0, 0, 0, &HC0}, 0)) * 4
                TempGridRow.Cells(2).Value = TranslatedLength
                TempGridRow.Cells(3).Value = Hex(TranslatedLength)
                ParentFileCurrentOffset += TranslatedLength
                TempGridRow.Cells(4).Value = ParentFileCurrentOffset
                TempGridRow.Cells(5).Value = Hex(ParentFileCurrentOffset)
                TempGridRow.Cells(6).Value = Hex(BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentFileBytes, ParentFileCurrentOffset, 4), 0))
                TempGridRow.Cells(6).Style = ReadOnlyCellStyle
                Pof0CurrentRead += 3
            ElseIf Pof0Bytes(Pof0CurrentRead) >= &H80 Then
                '2 Byte ref
                TempGridRow.Cells(1).Value = Hex(BitConverter.ToUInt16(GeneralTools.EndianReverse(Pof0Bytes, Pof0CurrentRead, 2), 0))
                Dim TranslatedLength As UInt32 = 0
                TranslatedLength = (BitConverter.ToUInt16(GeneralTools.EndianReverse(Pof0Bytes, Pof0CurrentRead, 2), 0) - &H8000) * 4
                TempGridRow.Cells(2).Value = TranslatedLength
                TempGridRow.Cells(3).Value = Hex(TranslatedLength)
                ParentFileCurrentOffset += TranslatedLength
                TempGridRow.Cells(4).Value = ParentFileCurrentOffset
                TempGridRow.Cells(5).Value = Hex(ParentFileCurrentOffset)
                TempGridRow.Cells(6).Value = Hex(BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentFileBytes, ParentFileCurrentOffset, 4), 0))
                TempGridRow.Cells(6).Style = ReadOnlyCellStyle
                Pof0CurrentRead += 1
            ElseIf Pof0Bytes(Pof0CurrentRead) >= &H40 Then
                '1 byte ref
                TempGridRow.Cells(1).Value = Hex(Pof0Bytes(Pof0CurrentRead))
                Dim TranslatedLength As UInt16 = (Pof0Bytes(Pof0CurrentRead) - &H40) * 4
                TempGridRow.Cells(2).Value = TranslatedLength
                TempGridRow.Cells(3).Value = Hex(TranslatedLength)
                ParentFileCurrentOffset += TranslatedLength
                TempGridRow.Cells(4).Value = ParentFileCurrentOffset
                TempGridRow.Cells(5).Value = Hex(ParentFileCurrentOffset)
                TempGridRow.Cells(6).Value = Hex(BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentFileBytes, ParentFileCurrentOffset, 4), 0))
                TempGridRow.Cells(6).Style = ReadOnlyCellStyle
            ElseIf Pof0Bytes(Pof0CurrentRead) = 0 Then
                'there is no added offset so we shouldn't add a line
                Pof0CurrentRead += 1
                Continue Do
            Else
                'Shouldn't happen whut
                TempGridRow.Cells(1).Value = Hex(Pof0Bytes(Pof0CurrentRead))
                TempGridRow.Cells(2).Value = "ERROR"
            End If
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = Pof0CurrentRead
            Pof0CurrentRead += 1
        Loop
        ProgressBar1.Value = Pof0Length
        DataGridPof0View.Rows.AddRange(WorkingCollection.ToArray())
        For i As Integer = 0 To DataGridPof0View.Rows.Count - 1
            DataGridPof0View.Rows(i).HeaderCell.Value = i + 1
        Next
    End Sub

#End Region

#Region "Initial Position File"
    'TO DO you can create format for the settings to be a class object once more information is known.

    Public Enum WeaponPositionType
        Unknown
        Arena
        Base
        Rules
        Settings
        Equipment
    End Enum

    Dim CurrentPoistionType As WeaponPositionType = WeaponPositionType.Unknown

    Sub FillWeaponPositionView(SelectedData As TreeNode)
        Dim WeaponPositionBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        'We need to show what type of position file it is
        Dim PartialWeaponHeaderString As String = New String(Encoding.Default.GetChars(WeaponPositionBytes, 2, 2))
        If PartialWeaponHeaderString = "AR" Then
            CurrentPoistionType = WeaponPositionType.Arena
        ElseIf PartialWeaponHeaderString = "BS" Then
            CurrentPoistionType = WeaponPositionType.Base
        ElseIf PartialWeaponHeaderString = "RU" Then
            CurrentPoistionType = WeaponPositionType.Rules
        ElseIf PartialWeaponHeaderString = "ST" Then
            CurrentPoistionType = WeaponPositionType.Settings
        ElseIf PartialWeaponHeaderString = "EA" Then
            CurrentPoistionType = WeaponPositionType.Equipment
        End If
        WeaponPositionTypeToolStripMenuItem.Text = "Position Type: " & CurrentPoistionType.ToString
        Dim WeaponCount As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, 8)
        Dim WeaponPotisionLEngth As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, 12)
        Dim WeaponLineLength As UInt32 = WeaponPotisionLEngth / WeaponCount
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridWeaponPositionView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = WeaponCount - 1
        ProgressBar1.Value = 0
        If CurrentPoistionType = WeaponPositionType.Settings Then
            Dim CurrentIndex As UInt32 = &H10
            For i As Integer = 0 To WeaponCount - 1
                Dim CurrentSettingNum As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex)
                Dim SubItemCount As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex + 4)
                CurrentIndex += 8
                For J As Integer = 0 To SubItemCount - 1
                    Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value = i
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Style = ReadOnlyCellStyle
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Value = (BitConverter.ToString(WeaponPositionBytes, CurrentIndex, &H28).Replace("-", " "))
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Style = ReadOnlyCellStyle
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Value = CurrentSettingNum
                    If Not J = 0 Then
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Style = ReadOnlyCellStyle
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True
                    End If
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart)).Value = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H4)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H8)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &HC)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H10)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H14)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H18)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H1C)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H1E)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H20)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H22)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)).Value = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex + &H24)
                    WorkingCollection.Add(TempGridRow)
                    CurrentIndex += &H28
                Next
                ProgressBar1.Value = i
            Next
            DataGridWeaponPositionView.Rows.AddRange(WorkingCollection.ToArray())
        Else
            For i As Integer = 0 To WeaponCount - 1
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value = i
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Style = ReadOnlyCellStyle
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Value = (BitConverter.ToString(WeaponPositionBytes, &H10 + i * WeaponLineLength, WeaponLineLength).Replace("-", " "))
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Style = ReadOnlyCellStyle
                Select Case CurrentPoistionType
                    Case WeaponPositionType.Arena
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 8)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt4)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 12)
                    Case WeaponPositionType.Base
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                    Case WeaponPositionType.Rules
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                    Case WeaponPositionType.Equipment
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value = BitConverter.ToUInt32(GeneralTools.EndianReverse(WeaponPositionBytes, &H10 + i * WeaponLineLength + 8), 0)
                End Select
                WorkingCollection.Add(TempGridRow)
                ProgressBar1.Value = i
            Next
            DataGridWeaponPositionView.Rows.AddRange(WorkingCollection.ToArray())
        End If
        GetWeaponPositionViewDisplayedColumns()
    End Sub

    Sub GetWeaponPositionViewDisplayedColumns()
        WeaponPositionSettingObjStart.Visible = False
        WeaponPositionByteArray.Visible = False
        WeaponPositionInt1.Visible = False
        WeaponPositionInt2.Visible = False
        WeaponPositionInt3.Visible = False
        WeaponPositionInt4.Visible = False
        WeaponPositionSettingNum.Visible = False
        WeaponPositionSingle1.Visible = False
        WeaponPositionSingle2.Visible = False
        WeaponPositionSingle3.Visible = False
        WeaponPositionSingle4.Visible = False
        WeaponPositionSingle5.Visible = False
        WeaponPositionSingle6.Visible = False
        WeaponPositionShort1.Visible = False
        WeaponPositionShort2.Visible = False
        WeaponPositionShort3.Visible = False
        WeaponPositionShort4.Visible = False
        WeaponPositionIntSet.Visible = False
        Select Case CurrentPoistionType
            Case WeaponPositionType.Arena
                WeaponPositionInt1.Visible = True
                WeaponPositionInt2.Visible = True
                WeaponPositionInt3.Visible = True
                WeaponPositionInt4.Visible = True
            Case WeaponPositionType.Base, WeaponPositionType.Rules
                WeaponPositionInt1.Visible = True
                WeaponPositionInt2.Visible = True
            Case WeaponPositionType.Settings
                WeaponPositionSettingObjStart.Visible = True
                WeaponPositionSettingNum.Visible = True
                WeaponPositionSingle1.Visible = True
                WeaponPositionSingle2.Visible = True
                WeaponPositionSingle3.Visible = True
                WeaponPositionSingle4.Visible = True
                WeaponPositionSingle5.Visible = True
                WeaponPositionSingle6.Visible = True
                WeaponPositionShort1.Visible = True
                WeaponPositionShort2.Visible = True
                WeaponPositionShort3.Visible = True
                WeaponPositionShort4.Visible = True
                WeaponPositionIntSet.Visible = True
            Case WeaponPositionType.Equipment
                WeaponPositionInt1.Visible = True
                WeaponPositionInt2.Visible = True
                WeaponPositionInt3.Visible = True
            Case Else
                WeaponPositionByteArray.Visible = True
        End Select
    End Sub

    Private Sub DataGridWeaponPositionView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridWeaponPositionView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridWeaponPositionView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt4),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)
                'Int32 columns
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt32.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CInt(MyCell.Value)
                End If
            Case DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)
                'float columns
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CSng(MyCell.Value)
                End If
            Case DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3),
                 DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)
                'Int16 columns
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CShort(MyCell.Value)
                End If

        End Select
        If Not MyCell.Value = OldValue Then
            SavePending = True
            SaveChangesWeaponPositionsMenuItem.Visible = True
        End If
    End Sub

    Private Sub DataGridWeaponPositionView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridWeaponPositionView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionAdd) Then 'add button
                'We need to add a read only filter over items that are added to a setting
                'This function adds a duplicate row at index + 1
                Dim Duplicaterow As DataGridViewRow = DataGridWeaponPositionView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridWeaponPositionView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridWeaponPositionView.Rows(e.RowIndex).Cells(i).Value
                Next
                Duplicaterow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True
                Duplicaterow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Style = ReadOnlyCellStyle
                DataGridWeaponPositionView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                If Not CurrentPoistionType = WeaponPositionType.Settings Then
                    For i As Integer = e.RowIndex + 1 To DataGridWeaponPositionView.Rows.Count - 1
                        DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value =
                        CInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value + 1)
                    Next
                End If
                SavePending = True
                SaveChangesWeaponPositionsMenuItem.Visible = True
            ElseIf e.ColumnIndex = DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionDelete) Then 'Delete button
                'We need to add a read only removal filter over items that are added to a setting
                If CurrentPoistionType = WeaponPositionType.Settings Then
                    If DataGridWeaponPositionView.Rows(e.RowIndex).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True Then
                        DataGridWeaponPositionView.Rows.RemoveAt(e.RowIndex)
                    Else
                        DataGridWeaponPositionView.Rows.RemoveAt(e.RowIndex)
                        DataGridWeaponPositionView.Rows(e.RowIndex).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = False
                        DataGridWeaponPositionView.Rows(e.RowIndex).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Style = DefaultCellStyle
                    End If
                Else
                    DataGridWeaponPositionView.Rows.RemoveAt(e.RowIndex)
                    For i As Integer = e.RowIndex To DataGridWeaponPositionView.Rows.Count - 1
                        DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value =
                        CInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value - 1)
                    Next
                End If
                SavePending = True
                SaveChangesWeaponPositionsMenuItem.Visible = True
            Else
                'do nothing
            End If
        End If
    End Sub

    Private Sub SaveChangesWeaponPositionsMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesWeaponPositionsMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildWeaponPositionFile())
    End Sub

    Private Function BuildWeaponPositionFile() As Byte()
        Dim ReturnedBytes As Byte() = New Byte() {}
        Select Case CurrentPoistionType
            Case WeaponPositionType.Arena
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H10) - 1) {} '57 50 41 52 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H41, &H52, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H10)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 4, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 8, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt4)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 12, 4)
                Next
            Case WeaponPositionType.Base
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H8) - 1) {} '57 50 42 53 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H42, &H53, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H8)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 4, 4)
                Next
            Case WeaponPositionType.Rules
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H8) - 1) {} '57 50 52 55 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H52, &H55, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H8)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 4, 4)
                Next
            Case WeaponPositionType.Settings
                Dim ContainedObjects As UInt32 = GetWeaponSettingContainers()
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H28 + ContainedObjects * 8) - 1) {} '57 50 53 54 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H53, &H54, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(ContainedObjects)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H28 + ContainedObjects * 8)), 0, ReturnedBytes, 12, 4)
                'Write Items
                Dim CurrentIndex As UInt32 = &H10
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    If DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = False Then
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Value)), 0, ReturnedBytes, CurrentIndex + 0, 4)
                        Array.Copy(BitConverter.GetBytes(CUInt(GetContainedObjectsInSettingContainer(i))), 0, ReturnedBytes, CurrentIndex + 4, 4)
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart)).Value)), 0, ReturnedBytes, CurrentIndex + 0 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1)).Value)), 0, ReturnedBytes, CurrentIndex + &H4 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2)).Value)), 0, ReturnedBytes, CurrentIndex + &H8 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3)).Value)), 0, ReturnedBytes, CurrentIndex + &HC + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4)).Value)), 0, ReturnedBytes, CurrentIndex + &H10 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5)).Value)), 0, ReturnedBytes, CurrentIndex + &H14 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)).Value)), 0, ReturnedBytes, CurrentIndex + &H18 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1)).Value)), 0, ReturnedBytes, CurrentIndex + &H1C + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2)).Value)), 0, ReturnedBytes, CurrentIndex + &H1E + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3)).Value)), 0, ReturnedBytes, CurrentIndex + &H20 + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)).Value)), 0, ReturnedBytes, CurrentIndex + &H22 + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)).Value)), 0, ReturnedBytes, CurrentIndex + &H24 + 8, 4)
                        CurrentIndex += 8 + &H28
                    Else
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart)).Value)), 0, ReturnedBytes, CurrentIndex + 0, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1)).Value)), 0, ReturnedBytes, CurrentIndex + &H4, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2)).Value)), 0, ReturnedBytes, CurrentIndex + &H8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3)).Value)), 0, ReturnedBytes, CurrentIndex + &HC, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4)).Value)), 0, ReturnedBytes, CurrentIndex + &H10, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5)).Value)), 0, ReturnedBytes, CurrentIndex + &H14, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)).Value)), 0, ReturnedBytes, CurrentIndex + &H18, 4)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1)).Value)), 0, ReturnedBytes, CurrentIndex + &H1C, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2)).Value)), 0, ReturnedBytes, CurrentIndex + &H1E, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3)).Value)), 0, ReturnedBytes, CurrentIndex + &H20, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)).Value)), 0, ReturnedBytes, CurrentIndex + &H22, 2)
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)).Value)), 0, ReturnedBytes, CurrentIndex + &H24, 4)
                        CurrentIndex += &H28
                    End If
                Next
            Case WeaponPositionType.Equipment
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &HC) - 1) {} '57 50 45 41 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H45, &H41, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &HC)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &HC + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &HC + 4, 4)
                    Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value))), 0, ReturnedBytes, &H10 + i * &HC + 8, 4)
                Next
        End Select
        Return ReturnedBytes
    End Function

    Private Function GetWeaponSettingContainers() As UInt32
        Dim ReturnedInt As UInt32 = 0
        For i As Integer = 0 To DataGridWeaponPositionView.Rows.Count - 1
            If DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = False Then
                ReturnedInt += 1
            End If
        Next
        Return ReturnedInt
    End Function

    Private Function GetContainedObjectsInSettingContainer(FirstRow As UInt32)
        Dim ReturnedInt As UInt32 = 1
        For i As Integer = FirstRow + 1 To DataGridWeaponPositionView.Rows.Count - 1
            If DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True Then
                ReturnedInt += 1
            Else
                Exit For
            End If
        Next
        Return ReturnedInt
    End Function

#End Region

#Region "File Ref Notes"

    'Each File reference is &H190
    'The first File gives you the folder name of the parent HSPC file
    'Next you have individual files inside the SHDC
    'Then you have the Actual SHDC = Folder Information
    'File Name true names are at offset &H88
    'File Number Reference is at offset 8
    'like 30 in text is the 1E file

#End Region

#End Region

    'There is the potential for better programming using data binding data grid views.
    'Option to make buttons disabled
    'disabling add - remove buttons https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/disable-buttons-in-a-button-column-in-the-datagrid
End Class