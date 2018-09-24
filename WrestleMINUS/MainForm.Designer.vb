<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadHomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UndoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RedoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IndexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SearchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStripTreeView = New System.Windows.Forms.MenuStrip()
        Me.CurrentViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.HexView = New System.Windows.Forms.TabPage()
        Me.Hex_Selected = New System.Windows.Forms.TextBox()
        Me.MenuStripHexView = New System.Windows.Forms.MenuStrip()
        Me.HexViewBitWidth = New System.Windows.Forms.ToolStripComboBox()
        Me.HexViewFileName = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextView = New System.Windows.Forms.TabPage()
        Me.Text_Selected = New System.Windows.Forms.TextBox()
        Me.MenuStripTextView = New System.Windows.Forms.MenuStrip()
        Me.TextViewBitWidth = New System.Windows.Forms.ToolStripComboBox()
        Me.TextViewFileName = New System.Windows.Forms.ToolStripMenuItem()
        Me.StringView = New System.Windows.Forms.TabPage()
        Me.DataGridStringView = New System.Windows.Forms.DataGridView()
        Me.HexRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StringText = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripStringView = New System.Windows.Forms.MenuStrip()
        Me.StringCountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MiscView = New System.Windows.Forms.TabPage()
        Me.DataGridMiscView = New System.Windows.Forms.DataGridView()
        Me.MenuStripMiscView = New System.Windows.Forms.MenuStrip()
        Me.MiscViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.ShowView = New System.Windows.Forms.TabPage()
        Me.DataGridShowView = New System.Windows.Forms.DataGridView()
        Me.StrName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.S4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Stage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Crowd = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.E1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.E2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.E3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Filter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.G1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.G2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.H1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.H2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.H3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.H4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Bar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Unknown = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.I1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.I2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.I3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Live = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.J = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripShowView = New System.Windows.Forms.MenuStrip()
        Me.ShowViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.NIBJView = New System.Windows.Forms.TabPage()
        Me.MenuStripNIBJView = New System.Windows.Forms.MenuStrip()
        Me.DataGridNIBJView = New System.Windows.Forms.DataGridView()
        Me.NIBJViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.FileAttributesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureView = New System.Windows.Forms.TabPage()
        Me.MenuStripPictureView = New System.Windows.Forms.MenuStrip()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.MenuStripTreeView.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.HexView.SuspendLayout()
        Me.MenuStripHexView.SuspendLayout()
        Me.TextView.SuspendLayout()
        Me.MenuStripTextView.SuspendLayout()
        Me.StringView.SuspendLayout()
        CType(Me.DataGridStringView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripStringView.SuspendLayout()
        Me.MiscView.SuspendLayout()
        CType(Me.DataGridMiscView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripMiscView.SuspendLayout()
        Me.ShowView.SuspendLayout()
        CType(Me.DataGridShowView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripShowView.SuspendLayout()
        Me.NIBJView.SuspendLayout()
        Me.MenuStripNIBJView.SuspendLayout()
        CType(Me.DataGridNIBJView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PictureView.SuspendLayout()
        Me.MenuStripPictureView.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1011, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadHomeToolStripMenuItem, Me.OpenToolStripMenuItem, Me.toolStripSeparator, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem, Me.toolStripSeparator1, Me.PrintToolStripMenuItem, Me.PrintPreviewToolStripMenuItem, Me.toolStripSeparator2, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'LoadHomeToolStripMenuItem
        '
        Me.LoadHomeToolStripMenuItem.Image = CType(resources.GetObject("LoadHomeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LoadHomeToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LoadHomeToolStripMenuItem.Name = "LoadHomeToolStripMenuItem"
        Me.LoadHomeToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.LoadHomeToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.LoadHomeToolStripMenuItem.Text = "&Load Home"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.OpenToolStripMenuItem.Text = "&Open File"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(176, 6)
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Image = CType(resources.GetObject("SaveToolStripMenuItem.Image"), System.Drawing.Image)
        Me.SaveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &As"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(176, 6)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Image = CType(resources.GetObject("PrintToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.PrintToolStripMenuItem.Text = "&Print"
        '
        'PrintPreviewToolStripMenuItem
        '
        Me.PrintPreviewToolStripMenuItem.Image = CType(resources.GetObject("PrintPreviewToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PrintPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        Me.PrintPreviewToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.PrintPreviewToolStripMenuItem.Text = "Print Pre&view"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(176, 6)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(179, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UndoToolStripMenuItem, Me.RedoToolStripMenuItem, Me.toolStripSeparator3, Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.toolStripSeparator4, Me.SelectAllToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'UndoToolStripMenuItem
        '
        Me.UndoToolStripMenuItem.Name = "UndoToolStripMenuItem"
        Me.UndoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.UndoToolStripMenuItem.Text = "&Undo"
        '
        'RedoToolStripMenuItem
        '
        Me.RedoToolStripMenuItem.Name = "RedoToolStripMenuItem"
        Me.RedoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.RedoToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.RedoToolStripMenuItem.Text = "&Redo"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(141, 6)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Image = CType(resources.GetObject("CutToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.CutToolStripMenuItem.Text = "Cu&t"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Image = CType(resources.GetObject("CopyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CopyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Image = CType(resources.GetObject("PasteToolStripMenuItem.Image"), System.Drawing.Image)
        Me.PasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(141, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select &All"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CustomizeToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ToolsToolStripMenuItem.Text = "&Tools"
        '
        'CustomizeToolStripMenuItem
        '
        Me.CustomizeToolStripMenuItem.Name = "CustomizeToolStripMenuItem"
        Me.CustomizeToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.CustomizeToolStripMenuItem.Text = "&Customize"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(130, 22)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ContentsToolStripMenuItem, Me.IndexToolStripMenuItem, Me.SearchToolStripMenuItem, Me.toolStripSeparator5, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'ContentsToolStripMenuItem
        '
        Me.ContentsToolStripMenuItem.Name = "ContentsToolStripMenuItem"
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ContentsToolStripMenuItem.Text = "&Contents"
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.IndexToolStripMenuItem.Text = "&Index"
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.SearchToolStripMenuItem.Text = "&Search"
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(119, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ProgressBar1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.MenuStripTreeView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1011, 450)
        Me.SplitContainer1.SplitterDistance = 337
        Me.SplitContainer1.TabIndex = 1
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 24)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.Size = New System.Drawing.Size(337, 403)
        Me.TreeView1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 427)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(337, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'MenuStripTreeView
        '
        Me.MenuStripTreeView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentViewToolStripMenuItem})
        Me.MenuStripTreeView.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripTreeView.Name = "MenuStripTreeView"
        Me.MenuStripTreeView.Size = New System.Drawing.Size(337, 24)
        Me.MenuStripTreeView.TabIndex = 1
        Me.MenuStripTreeView.Text = "MenuStrip2"
        '
        'CurrentViewToolStripMenuItem
        '
        Me.CurrentViewToolStripMenuItem.Name = "CurrentViewToolStripMenuItem"
        Me.CurrentViewToolStripMenuItem.Size = New System.Drawing.Size(90, 20)
        Me.CurrentViewToolStripMenuItem.Text = "Current View:"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.HexView)
        Me.TabControl1.Controls.Add(Me.TextView)
        Me.TabControl1.Controls.Add(Me.StringView)
        Me.TabControl1.Controls.Add(Me.MiscView)
        Me.TabControl1.Controls.Add(Me.ShowView)
        Me.TabControl1.Controls.Add(Me.NIBJView)
        Me.TabControl1.Controls.Add(Me.PictureView)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(670, 450)
        Me.TabControl1.TabIndex = 1
        '
        'HexView
        '
        Me.HexView.Controls.Add(Me.Hex_Selected)
        Me.HexView.Controls.Add(Me.MenuStripHexView)
        Me.HexView.Location = New System.Drawing.Point(4, 22)
        Me.HexView.Name = "HexView"
        Me.HexView.Padding = New System.Windows.Forms.Padding(3)
        Me.HexView.Size = New System.Drawing.Size(662, 424)
        Me.HexView.TabIndex = 0
        Me.HexView.Text = "Hex View"
        Me.HexView.UseVisualStyleBackColor = True
        '
        'Hex_Selected
        '
        Me.Hex_Selected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Hex_Selected.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hex_Selected.Location = New System.Drawing.Point(3, 30)
        Me.Hex_Selected.Multiline = True
        Me.Hex_Selected.Name = "Hex_Selected"
        Me.Hex_Selected.ReadOnly = True
        Me.Hex_Selected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Hex_Selected.Size = New System.Drawing.Size(656, 391)
        Me.Hex_Selected.TabIndex = 0
        Me.Hex_Selected.WordWrap = False
        '
        'MenuStripHexView
        '
        Me.MenuStripHexView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HexViewBitWidth, Me.HexViewFileName})
        Me.MenuStripHexView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripHexView.Name = "MenuStripHexView"
        Me.MenuStripHexView.Size = New System.Drawing.Size(656, 27)
        Me.MenuStripHexView.TabIndex = 1
        Me.MenuStripHexView.Text = "MenuStrip3"
        '
        'HexViewBitWidth
        '
        Me.HexViewBitWidth.Items.AddRange(New Object() {"4", "8", "16", "32", "64"})
        Me.HexViewBitWidth.MaxLength = 3
        Me.HexViewBitWidth.Name = "HexViewBitWidth"
        Me.HexViewBitWidth.Size = New System.Drawing.Size(121, 23)
        '
        'HexViewFileName
        '
        Me.HexViewFileName.Enabled = False
        Me.HexViewFileName.Name = "HexViewFileName"
        Me.HexViewFileName.Size = New System.Drawing.Size(12, 23)
        '
        'TextView
        '
        Me.TextView.Controls.Add(Me.Text_Selected)
        Me.TextView.Controls.Add(Me.MenuStripTextView)
        Me.TextView.Location = New System.Drawing.Point(4, 22)
        Me.TextView.Name = "TextView"
        Me.TextView.Padding = New System.Windows.Forms.Padding(3)
        Me.TextView.Size = New System.Drawing.Size(662, 424)
        Me.TextView.TabIndex = 1
        Me.TextView.Text = "Text View"
        Me.TextView.UseVisualStyleBackColor = True
        '
        'Text_Selected
        '
        Me.Text_Selected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Text_Selected.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text_Selected.Location = New System.Drawing.Point(3, 30)
        Me.Text_Selected.Multiline = True
        Me.Text_Selected.Name = "Text_Selected"
        Me.Text_Selected.ReadOnly = True
        Me.Text_Selected.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Text_Selected.Size = New System.Drawing.Size(656, 391)
        Me.Text_Selected.TabIndex = 1
        Me.Text_Selected.WordWrap = False
        '
        'MenuStripTextView
        '
        Me.MenuStripTextView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextViewBitWidth, Me.TextViewFileName})
        Me.MenuStripTextView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripTextView.Name = "MenuStripTextView"
        Me.MenuStripTextView.Size = New System.Drawing.Size(656, 27)
        Me.MenuStripTextView.TabIndex = 0
        Me.MenuStripTextView.Text = "MenuStrip3"
        '
        'TextViewBitWidth
        '
        Me.TextViewBitWidth.Items.AddRange(New Object() {"4", "8", "16", "32", "64"})
        Me.TextViewBitWidth.MaxLength = 3
        Me.TextViewBitWidth.Name = "TextViewBitWidth"
        Me.TextViewBitWidth.Size = New System.Drawing.Size(121, 23)
        '
        'TextViewFileName
        '
        Me.TextViewFileName.Enabled = False
        Me.TextViewFileName.Name = "TextViewFileName"
        Me.TextViewFileName.Size = New System.Drawing.Size(12, 23)
        '
        'StringView
        '
        Me.StringView.Controls.Add(Me.DataGridStringView)
        Me.StringView.Controls.Add(Me.MenuStripStringView)
        Me.StringView.Location = New System.Drawing.Point(4, 22)
        Me.StringView.Name = "StringView"
        Me.StringView.Padding = New System.Windows.Forms.Padding(3)
        Me.StringView.Size = New System.Drawing.Size(662, 424)
        Me.StringView.TabIndex = 2
        Me.StringView.Text = "String Viewer"
        Me.StringView.UseVisualStyleBackColor = True
        '
        'DataGridStringView
        '
        Me.DataGridStringView.AllowUserToAddRows = False
        Me.DataGridStringView.AllowUserToDeleteRows = False
        Me.DataGridStringView.AllowUserToResizeRows = False
        Me.DataGridStringView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridStringView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridStringView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.HexRef, Me.StringText, Me.Length})
        Me.DataGridStringView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridStringView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridStringView.MultiSelect = False
        Me.DataGridStringView.Name = "DataGridStringView"
        Me.DataGridStringView.RowHeadersVisible = False
        Me.DataGridStringView.Size = New System.Drawing.Size(656, 394)
        Me.DataGridStringView.TabIndex = 2
        '
        'HexRef
        '
        Me.HexRef.FillWeight = 25.0!
        Me.HexRef.HeaderText = "HexRef"
        Me.HexRef.MaxInputLength = 10
        Me.HexRef.Name = "HexRef"
        Me.HexRef.ReadOnly = True
        Me.HexRef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'StringText
        '
        Me.StringText.HeaderText = "String Text"
        Me.StringText.MaxInputLength = 31
        Me.StringText.Name = "StringText"
        Me.StringText.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Length
        '
        Me.Length.FillWeight = 25.0!
        Me.Length.HeaderText = "Length"
        Me.Length.MaxInputLength = 10
        Me.Length.Name = "Length"
        Me.Length.ReadOnly = True
        Me.Length.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MenuStripStringView
        '
        Me.MenuStripStringView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StringCountToolStripMenuItem})
        Me.MenuStripStringView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripStringView.Name = "MenuStripStringView"
        Me.MenuStripStringView.Size = New System.Drawing.Size(656, 24)
        Me.MenuStripStringView.TabIndex = 0
        Me.MenuStripStringView.Text = "MenuStrip2"
        '
        'StringCountToolStripMenuItem
        '
        Me.StringCountToolStripMenuItem.Enabled = False
        Me.StringCountToolStripMenuItem.Name = "StringCountToolStripMenuItem"
        Me.StringCountToolStripMenuItem.Size = New System.Drawing.Size(89, 20)
        Me.StringCountToolStripMenuItem.Text = "String Count:"
        '
        'MiscView
        '
        Me.MiscView.Controls.Add(Me.DataGridMiscView)
        Me.MiscView.Controls.Add(Me.MenuStripMiscView)
        Me.MiscView.Location = New System.Drawing.Point(4, 22)
        Me.MiscView.Name = "MiscView"
        Me.MiscView.Padding = New System.Windows.Forms.Padding(3)
        Me.MiscView.Size = New System.Drawing.Size(662, 424)
        Me.MiscView.TabIndex = 3
        Me.MiscView.Text = "Misc View"
        Me.MiscView.UseVisualStyleBackColor = True
        '
        'DataGridMiscView
        '
        Me.DataGridMiscView.AllowUserToAddRows = False
        Me.DataGridMiscView.AllowUserToDeleteRows = False
        Me.DataGridMiscView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridMiscView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMiscView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMiscView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridMiscView.Name = "DataGridMiscView"
        Me.DataGridMiscView.Size = New System.Drawing.Size(656, 391)
        Me.DataGridMiscView.TabIndex = 1
        '
        'MenuStripMiscView
        '
        Me.MenuStripMiscView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MiscViewType})
        Me.MenuStripMiscView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMiscView.Name = "MenuStripMiscView"
        Me.MenuStripMiscView.Size = New System.Drawing.Size(656, 27)
        Me.MenuStripMiscView.TabIndex = 0
        Me.MenuStripMiscView.Text = "MenuStrip2"
        '
        'MiscViewType
        '
        Me.MiscViewType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.MiscViewType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.MiscViewType.Items.AddRange(New Object() {"2K15", "2K16", "2K17", "2K18", "2K19"})
        Me.MiscViewType.Name = "MiscViewType"
        Me.MiscViewType.Size = New System.Drawing.Size(121, 23)
        '
        'ShowView
        '
        Me.ShowView.Controls.Add(Me.DataGridShowView)
        Me.ShowView.Controls.Add(Me.MenuStripShowView)
        Me.ShowView.Location = New System.Drawing.Point(4, 22)
        Me.ShowView.Name = "ShowView"
        Me.ShowView.Padding = New System.Windows.Forms.Padding(3)
        Me.ShowView.Size = New System.Drawing.Size(662, 424)
        Me.ShowView.TabIndex = 4
        Me.ShowView.Text = "Show View"
        Me.ShowView.UseVisualStyleBackColor = True
        '
        'DataGridShowView
        '
        Me.DataGridShowView.AllowUserToAddRows = False
        Me.DataGridShowView.AllowUserToDeleteRows = False
        Me.DataGridShowView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridShowView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridShowView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.StrName, Me.S1, Me.S2, Me.S3, Me.S4, Me.A1, Me.A2, Me.B, Me.C1, Me.C2, Me.C3, Me.C4, Me.C5, Me.Stage, Me.D1, Me.D2, Me.Crowd, Me.E1, Me.E2, Me.E3, Me.Ref, Me.Filter, Me.F1, Me.F2, Me.G1, Me.G2, Me.H1, Me.H2, Me.H3, Me.H4, Me.Bar, Me.Unknown, Me.I1, Me.I2, Me.I3, Me.Live, Me.J})
        Me.DataGridShowView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridShowView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridShowView.Name = "DataGridShowView"
        Me.DataGridShowView.RowHeadersWidth = 60
        Me.DataGridShowView.Size = New System.Drawing.Size(656, 391)
        Me.DataGridShowView.TabIndex = 2
        '
        'StrName
        '
        Me.StrName.HeaderText = "StrName"
        Me.StrName.Name = "StrName"
        Me.StrName.Width = 73
        '
        'S1
        '
        Me.S1.HeaderText = "S1"
        Me.S1.Name = "S1"
        Me.S1.Width = 45
        '
        'S2
        '
        Me.S2.HeaderText = "S2"
        Me.S2.Name = "S2"
        Me.S2.Width = 45
        '
        'S3
        '
        Me.S3.HeaderText = "S3"
        Me.S3.Name = "S3"
        Me.S3.Width = 45
        '
        'S4
        '
        Me.S4.HeaderText = "S4"
        Me.S4.Name = "S4"
        Me.S4.Width = 45
        '
        'A1
        '
        Me.A1.HeaderText = "A1"
        Me.A1.Name = "A1"
        Me.A1.Width = 45
        '
        'A2
        '
        Me.A2.HeaderText = "A2"
        Me.A2.Name = "A2"
        Me.A2.Width = 45
        '
        'B
        '
        Me.B.HeaderText = "B"
        Me.B.Name = "B"
        Me.B.Width = 39
        '
        'C1
        '
        Me.C1.HeaderText = "C1"
        Me.C1.Name = "C1"
        Me.C1.Width = 26
        '
        'C2
        '
        Me.C2.HeaderText = "C2"
        Me.C2.Name = "C2"
        Me.C2.Width = 26
        '
        'C3
        '
        Me.C3.HeaderText = "C3"
        Me.C3.Name = "C3"
        Me.C3.Width = 26
        '
        'C4
        '
        Me.C4.HeaderText = "C4"
        Me.C4.Name = "C4"
        Me.C4.Width = 26
        '
        'C5
        '
        Me.C5.HeaderText = "C5"
        Me.C5.Name = "C5"
        Me.C5.Width = 26
        '
        'Stage
        '
        Me.Stage.HeaderText = "Stage"
        Me.Stage.Name = "Stage"
        Me.Stage.Width = 60
        '
        'D1
        '
        Me.D1.HeaderText = "D1"
        Me.D1.Name = "D1"
        Me.D1.Width = 46
        '
        'D2
        '
        Me.D2.HeaderText = "D2"
        Me.D2.Name = "D2"
        Me.D2.Width = 46
        '
        'Crowd
        '
        Me.Crowd.HeaderText = "Crowd"
        Me.Crowd.Name = "Crowd"
        Me.Crowd.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Crowd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Crowd.Width = 62
        '
        'E1
        '
        Me.E1.HeaderText = "E1"
        Me.E1.Name = "E1"
        Me.E1.Width = 26
        '
        'E2
        '
        Me.E2.HeaderText = "E2"
        Me.E2.Name = "E2"
        Me.E2.Width = 26
        '
        'E3
        '
        Me.E3.HeaderText = "E3"
        Me.E3.Name = "E3"
        Me.E3.Width = 26
        '
        'Ref
        '
        Me.Ref.HeaderText = "Ref"
        Me.Ref.Name = "Ref"
        Me.Ref.Width = 49
        '
        'Filter
        '
        Me.Filter.HeaderText = "Filter"
        Me.Filter.Name = "Filter"
        Me.Filter.Width = 54
        '
        'F1
        '
        Me.F1.HeaderText = "F1"
        Me.F1.Name = "F1"
        Me.F1.Width = 44
        '
        'F2
        '
        Me.F2.HeaderText = "F2"
        Me.F2.Name = "F2"
        Me.F2.Width = 44
        '
        'G1
        '
        Me.G1.HeaderText = "G1"
        Me.G1.Name = "G1"
        Me.G1.Width = 46
        '
        'G2
        '
        Me.G2.HeaderText = "G2"
        Me.G2.Name = "G2"
        Me.G2.Width = 46
        '
        'H1
        '
        Me.H1.HeaderText = "H1"
        Me.H1.Name = "H1"
        Me.H1.Width = 46
        '
        'H2
        '
        Me.H2.HeaderText = "H2"
        Me.H2.Name = "H2"
        Me.H2.Width = 46
        '
        'H3
        '
        Me.H3.HeaderText = "H3"
        Me.H3.Name = "H3"
        Me.H3.Width = 46
        '
        'H4
        '
        Me.H4.HeaderText = "H4"
        Me.H4.Name = "H4"
        Me.H4.Width = 46
        '
        'Bar
        '
        Me.Bar.HeaderText = "Bar"
        Me.Bar.Name = "Bar"
        Me.Bar.Width = 48
        '
        'Unknown
        '
        Me.Unknown.HeaderText = "Unkown"
        Me.Unknown.Name = "Unknown"
        Me.Unknown.Width = 72
        '
        'I1
        '
        Me.I1.HeaderText = "I1"
        Me.I1.Name = "I1"
        Me.I1.Width = 41
        '
        'I2
        '
        Me.I2.HeaderText = "I2"
        Me.I2.Name = "I2"
        Me.I2.Width = 41
        '
        'I3
        '
        Me.I3.HeaderText = "I3"
        Me.I3.Name = "I3"
        Me.I3.Width = 41
        '
        'Live
        '
        Me.Live.HeaderText = "Live"
        Me.Live.Name = "Live"
        Me.Live.Width = 52
        '
        'J
        '
        Me.J.HeaderText = "J"
        Me.J.Name = "J"
        Me.J.Width = 37
        '
        'MenuStripShowView
        '
        Me.MenuStripShowView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowViewType})
        Me.MenuStripShowView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripShowView.Name = "MenuStripShowView"
        Me.MenuStripShowView.Size = New System.Drawing.Size(656, 27)
        Me.MenuStripShowView.TabIndex = 0
        Me.MenuStripShowView.Text = "MenuStrip2"
        '
        'ShowViewType
        '
        Me.ShowViewType.Items.AddRange(New Object() {"2K15", "2K16", "2K17", "2K18", "2K19"})
        Me.ShowViewType.Name = "ShowViewType"
        Me.ShowViewType.Size = New System.Drawing.Size(121, 23)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'NIBJView
        '
        Me.NIBJView.Controls.Add(Me.DataGridNIBJView)
        Me.NIBJView.Controls.Add(Me.MenuStripNIBJView)
        Me.NIBJView.Location = New System.Drawing.Point(4, 22)
        Me.NIBJView.Name = "NIBJView"
        Me.NIBJView.Padding = New System.Windows.Forms.Padding(3)
        Me.NIBJView.Size = New System.Drawing.Size(662, 424)
        Me.NIBJView.TabIndex = 5
        Me.NIBJView.Text = "NIBJView"
        Me.NIBJView.UseVisualStyleBackColor = True
        '
        'MenuStripNIBJView
        '
        Me.MenuStripNIBJView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NIBJViewType, Me.FileAttributesToolStripMenuItem})
        Me.MenuStripNIBJView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripNIBJView.Name = "MenuStripNIBJView"
        Me.MenuStripNIBJView.Size = New System.Drawing.Size(656, 27)
        Me.MenuStripNIBJView.TabIndex = 0
        Me.MenuStripNIBJView.Text = "MenuStrip2"
        '
        'DataGridNIBJView
        '
        Me.DataGridNIBJView.AllowUserToAddRows = False
        Me.DataGridNIBJView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridNIBJView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridNIBJView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridNIBJView.MultiSelect = False
        Me.DataGridNIBJView.Name = "DataGridNIBJView"
        Me.DataGridNIBJView.RowHeadersWidth = 60
        Me.DataGridNIBJView.Size = New System.Drawing.Size(656, 391)
        Me.DataGridNIBJView.TabIndex = 2
        '
        'NIBJViewType
        '
        Me.NIBJViewType.Items.AddRange(New Object() {"2K15", "2K16", "2K17", "2K18", "2K19"})
        Me.NIBJViewType.Name = "NIBJViewType"
        Me.NIBJViewType.Size = New System.Drawing.Size(121, 23)
        '
        'FileAttributesToolStripMenuItem
        '
        Me.FileAttributesToolStripMenuItem.Name = "FileAttributesToolStripMenuItem"
        Me.FileAttributesToolStripMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.FileAttributesToolStripMenuItem.Text = "File Attributes"
        '
        'PictureView
        '
        Me.PictureView.Controls.Add(Me.PictureBox1)
        Me.PictureView.Controls.Add(Me.MenuStripPictureView)
        Me.PictureView.Location = New System.Drawing.Point(4, 22)
        Me.PictureView.Name = "PictureView"
        Me.PictureView.Padding = New System.Windows.Forms.Padding(3)
        Me.PictureView.Size = New System.Drawing.Size(662, 424)
        Me.PictureView.TabIndex = 6
        Me.PictureView.Text = "Picture View"
        Me.PictureView.UseVisualStyleBackColor = True
        '
        'MenuStripPictureView
        '
        Me.MenuStripPictureView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.MenuStripPictureView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripPictureView.Name = "MenuStripPictureView"
        Me.MenuStripPictureView.Size = New System.Drawing.Size(656, 24)
        Me.MenuStripPictureView.TabIndex = 0
        Me.MenuStripPictureView.Text = "MenuStrip2"
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(3, 27)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(656, 394)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(22, 20)
        Me.ToolStripMenuItem1.Text = " "
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
        Me.ShowIcon = False
        Me.Text = "WrestleMINUS"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.MenuStripTreeView.ResumeLayout(False)
        Me.MenuStripTreeView.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.HexView.ResumeLayout(False)
        Me.HexView.PerformLayout()
        Me.MenuStripHexView.ResumeLayout(False)
        Me.MenuStripHexView.PerformLayout()
        Me.TextView.ResumeLayout(False)
        Me.TextView.PerformLayout()
        Me.MenuStripTextView.ResumeLayout(False)
        Me.MenuStripTextView.PerformLayout()
        Me.StringView.ResumeLayout(False)
        Me.StringView.PerformLayout()
        CType(Me.DataGridStringView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripStringView.ResumeLayout(False)
        Me.MenuStripStringView.PerformLayout()
        Me.MiscView.ResumeLayout(False)
        Me.MiscView.PerformLayout()
        CType(Me.DataGridMiscView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripMiscView.ResumeLayout(False)
        Me.MenuStripMiscView.PerformLayout()
        Me.ShowView.ResumeLayout(False)
        Me.ShowView.PerformLayout()
        CType(Me.DataGridShowView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripShowView.ResumeLayout(False)
        Me.MenuStripShowView.PerformLayout()
        Me.NIBJView.ResumeLayout(False)
        Me.NIBJView.PerformLayout()
        Me.MenuStripNIBJView.ResumeLayout(False)
        Me.MenuStripNIBJView.PerformLayout()
        CType(Me.DataGridNIBJView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PictureView.ResumeLayout(False)
        Me.PictureView.PerformLayout()
        Me.MenuStripPictureView.ResumeLayout(False)
        Me.MenuStripPictureView.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadHomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator As ToolStripSeparator
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As ToolStripSeparator
    Friend WithEvents PrintToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UndoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RedoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator3 As ToolStripSeparator
    Friend WithEvents CutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As ToolStripSeparator
    Friend WithEvents SelectAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CustomizeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContentsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IndexToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SearchToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents MenuStripTreeView As MenuStrip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents HexView As TabPage
    Friend WithEvents TextView As TabPage
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents CurrentViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Hex_Selected As TextBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents MenuStripHexView As MenuStrip
    Friend WithEvents HexViewBitWidth As ToolStripComboBox
    Friend WithEvents HexViewFileName As ToolStripMenuItem
    Friend WithEvents Text_Selected As TextBox
    Friend WithEvents MenuStripTextView As MenuStrip
    Friend WithEvents TextViewBitWidth As ToolStripComboBox
    Friend WithEvents TextViewFileName As ToolStripMenuItem
    Friend WithEvents StringView As TabPage
    Friend WithEvents DataGridStringView As DataGridView
    Friend WithEvents HexRef As DataGridViewTextBoxColumn
    Friend WithEvents StringText As DataGridViewTextBoxColumn
    Friend WithEvents Length As DataGridViewTextBoxColumn
    Friend WithEvents MenuStripStringView As MenuStrip
    Friend WithEvents StringCountToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MiscView As TabPage
    Friend WithEvents MenuStripMiscView As MenuStrip
    Friend WithEvents MiscViewType As ToolStripComboBox
    Friend WithEvents DataGridMiscView As DataGridView
    Friend WithEvents ShowView As TabPage
    Friend WithEvents MenuStripShowView As MenuStrip
    Friend WithEvents ShowViewType As ToolStripComboBox
    Friend WithEvents DataGridShowView As DataGridView
    Friend WithEvents StrName As DataGridViewTextBoxColumn
    Friend WithEvents S1 As DataGridViewTextBoxColumn
    Friend WithEvents S2 As DataGridViewTextBoxColumn
    Friend WithEvents S3 As DataGridViewTextBoxColumn
    Friend WithEvents S4 As DataGridViewTextBoxColumn
    Friend WithEvents A1 As DataGridViewTextBoxColumn
    Friend WithEvents A2 As DataGridViewTextBoxColumn
    Friend WithEvents B As DataGridViewTextBoxColumn
    Friend WithEvents C1 As DataGridViewCheckBoxColumn
    Friend WithEvents C2 As DataGridViewCheckBoxColumn
    Friend WithEvents C3 As DataGridViewCheckBoxColumn
    Friend WithEvents C4 As DataGridViewCheckBoxColumn
    Friend WithEvents C5 As DataGridViewCheckBoxColumn
    Friend WithEvents Stage As DataGridViewTextBoxColumn
    Friend WithEvents D1 As DataGridViewTextBoxColumn
    Friend WithEvents D2 As DataGridViewTextBoxColumn
    Friend WithEvents Crowd As DataGridViewCheckBoxColumn
    Friend WithEvents E1 As DataGridViewCheckBoxColumn
    Friend WithEvents E2 As DataGridViewCheckBoxColumn
    Friend WithEvents E3 As DataGridViewCheckBoxColumn
    Friend WithEvents Ref As DataGridViewTextBoxColumn
    Friend WithEvents Filter As DataGridViewTextBoxColumn
    Friend WithEvents F1 As DataGridViewTextBoxColumn
    Friend WithEvents F2 As DataGridViewTextBoxColumn
    Friend WithEvents G1 As DataGridViewTextBoxColumn
    Friend WithEvents G2 As DataGridViewTextBoxColumn
    Friend WithEvents H1 As DataGridViewTextBoxColumn
    Friend WithEvents H2 As DataGridViewTextBoxColumn
    Friend WithEvents H3 As DataGridViewTextBoxColumn
    Friend WithEvents H4 As DataGridViewTextBoxColumn
    Friend WithEvents Bar As DataGridViewTextBoxColumn
    Friend WithEvents Unknown As DataGridViewTextBoxColumn
    Friend WithEvents I1 As DataGridViewTextBoxColumn
    Friend WithEvents I2 As DataGridViewTextBoxColumn
    Friend WithEvents I3 As DataGridViewTextBoxColumn
    Friend WithEvents Live As DataGridViewTextBoxColumn
    Friend WithEvents J As DataGridViewTextBoxColumn
    Friend WithEvents NIBJView As TabPage
    Friend WithEvents MenuStripNIBJView As MenuStrip
    Friend WithEvents DataGridNIBJView As DataGridView
    Friend WithEvents NIBJViewType As ToolStripComboBox
    Friend WithEvents FileAttributesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PictureView As TabPage
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MenuStripPictureView As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
End Class
