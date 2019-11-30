Imports FontAwesome.Sharp

Public Class MainForm

    Friend Shared StringReferences As Dictionary(Of UInt32, String)
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

#Region "Main Form Core Actions"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        SettingsHandlers.SettingsCheck()
        OnlineVersion.CheckUpdate()
        LoadFontAwesomeIcons()
        FillCompressionMenu()
        ApplyFormSettings()
        FillViewSettings()
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

#End Region

#Region "Application Load"

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

#End Region


    Function HideTabs(ExcludedTabs As List(Of TabPage)) As DialogResult
        If IsNothing(ExcludedTabs) Then
            ExcludedTabs = New List(Of TabPage)
        End If
        For Each TempTabPage As TabPage In MainMenuTabControl.TabPages
            Select Case TempTabPage.Name
                Case HexView.Name
                    'Never Hide
                Case TextView.Name
                    'Never Hide
                Case MuscleView.Name
                    'this is not automatically removed except on startup
                    If Not MuscleViewStartupRemoved Then
                        MainMenuTabControl.TabPages.Remove(TempTabPage)
                        MuscleViewStartupRemoved = True
                    End If
                Case ExcludedTabs.Contains(TempTabPage)
                    'MessageBox.Show(TabControl1.SelectedTab.Text)
                    'MessageBox.Show(ExcludedTab.Text)
                    'Excluded
                    If MainMenuTabControl.SelectedTab.Text = TempTabPage.Text Then
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
                                Case PackageType.sdb
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
                    MainMenuTabControl.TabPages.Remove(TempTabPage)
            End Select
        Next
        Return DialogResult.OK
    End Function

    Sub FillCompressionMenu()
        'Tool Menu Compress in place
        If ApplicationHandlers.CheckBPEExe() Then
            BPECompressionToolStripMenuItem.Visible = True
        Else
            BPECompressionToolStripMenuItem.Visible = False
        End If
        If ApplicationHandlers.CheckIconicZlib() Then
            ZLIBCompressionToolStripMenuItem.Visible = True
        Else
            ZLIBCompressionToolStripMenuItem.Visible = False
        End If
        If ApplicationHandlers.CheckOodle6() OrElse ApplicationHandlers.CheckOodle7() Then
            OODLCompressionToolStripMenuItem.Visible = True
        Else
            OODLCompressionToolStripMenuItem.Visible = False
        End If
        My.Settings.Save()
    End Sub

#Region "Loading Icons"

    Sub LoadFontAwesomeIcons()
        If ApplicationHandlers.CheckFontAwesome() Then
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

#End Region

#Region "Drag Drop Functions"

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

#End Region

#Region "File view tool strip"

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

#End Region

#Region "Resize options"

    Sub ApplyFormSettings()
        MyBase.Size = My.Settings.SavedFormSize
        SplitFileMenuContainer.SplitterDistance = My.Settings.SavedSplitterDistance
    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitFileMenuContainer.SplitterMoved
        CurrentViewToolStripMenuItem.Width = SplitFileMenuContainer.SplitterDistance - 10
        If Not SplitFileMenuContainer.SplitterDistance = 253 Then
            My.Settings.SavedSplitterDistance = SplitFileMenuContainer.SplitterDistance
        End If
    End Sub

    Private Sub MainForm_ResizeEnd(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        My.Settings.SavedFormSize = MyBase.Size
    End Sub

#End Region

    'TO DO Add UpdateProgress to more functions, Flesh out with parameters

#Region "File Ref Notes"

    'Each File reference is &H190
    'The first File gives you the folder name of the parent HSPC file
    'Next you have individual files inside the SHDC
    'Then you have the Actual SHDC = Folder Information
    'File Name true names are at offset &H88
    'File Number Reference is at offset 8
    'like 30 in text is the 1E file

#End Region

    'There is the potential for better programming using data binding data grid views.
    'Option to make buttons disabled
    'disabling add - remove buttons https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/disable-buttons-in-a-button-column-in-the-datagrid
End Class