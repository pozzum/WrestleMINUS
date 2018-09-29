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
        Me.NIBJView = New System.Windows.Forms.TabPage()
        Me.DataGridNIBJView = New System.Windows.Forms.DataGridView()
        Me.MenuStripNIBJView = New System.Windows.Forms.MenuStrip()
        Me.NIBJViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.FileAttributesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureView = New System.Windows.Forms.TabPage()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.ObjectView = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AttireView = New System.Windows.Forms.TabPage()
        Me.DataGridAttireView = New System.Windows.Forms.DataGridView()
        Me.MenuStripAttireView = New System.Windows.Forms.MenuStrip()
        Me.StringLoadedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PacsLoadedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MuscleView = New System.Windows.Forms.TabPage()
        Me.DataGridMuscleView = New System.Windows.Forms.DataGridView()
        Me.MenuStripMuscleView = New System.Windows.Forms.MenuStrip()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaskView = New System.Windows.Forms.TabPage()
        Me.DataGridMaskView = New System.Windows.Forms.DataGridView()
        Me.Mask_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Start_Face = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.End_Face = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Add_Mask = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Del_Mask = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripMaskView = New System.Windows.Forms.MenuStrip()
        Me.ObjArrayView = New System.Windows.Forms.TabPage()
        Me.DataGridObjArrayView = New System.Windows.Forms.DataGridView()
        Me.ChairNum = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ChairName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArrX = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrY = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrZ = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrRX = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrRY = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrRZ = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrDec1 = New WrestleMINUS.NumericUpDownColumn()
        Me.ArrDec2 = New WrestleMINUS.NumericUpDownColumn()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.MenuStripPictureView = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TreeViewContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrawlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumericUpDownColumn1 = New WrestleMINUS.NumericUpDownColumn()
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumericUpDownColumn2 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn3 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn4 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn5 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn6 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn7 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn8 = New WrestleMINUS.NumericUpDownColumn()
        Me.NumericUpDownColumn9 = New WrestleMINUS.NumericUpDownColumn()
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
        CType(Me.DataGridNIBJView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripNIBJView.SuspendLayout()
        Me.PictureView.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjectView.SuspendLayout()
        Me.AttireView.SuspendLayout()
        CType(Me.DataGridAttireView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripAttireView.SuspendLayout()
        Me.MuscleView.SuspendLayout()
        CType(Me.DataGridMuscleView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripMuscleView.SuspendLayout()
        Me.MaskView.SuspendLayout()
        CType(Me.DataGridMaskView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjArrayView.SuspendLayout()
        CType(Me.DataGridObjArrayView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripPictureView.SuspendLayout()
        Me.TreeViewContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1184, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadHomeToolStripMenuItem, Me.OpenToolStripMenuItem, Me.ExitToolStripMenuItem})
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
        Me.ContentsToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ContentsToolStripMenuItem.Text = "&Contents"
        '
        'IndexToolStripMenuItem
        '
        Me.IndexToolStripMenuItem.Name = "IndexToolStripMenuItem"
        Me.IndexToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.IndexToolStripMenuItem.Text = "&Index"
        '
        'SearchToolStripMenuItem
        '
        Me.SearchToolStripMenuItem.Name = "SearchToolStripMenuItem"
        Me.SearchToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SearchToolStripMenuItem.Text = "&Search"
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(149, 6)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
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
        Me.SplitContainer1.Size = New System.Drawing.Size(1184, 450)
        Me.SplitContainer1.SplitterDistance = 196
        Me.SplitContainer1.TabIndex = 1
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 24)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.Size = New System.Drawing.Size(196, 403)
        Me.TreeView1.TabIndex = 0
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 427)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(196, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'MenuStripTreeView
        '
        Me.MenuStripTreeView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentViewToolStripMenuItem})
        Me.MenuStripTreeView.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripTreeView.Name = "MenuStripTreeView"
        Me.MenuStripTreeView.Size = New System.Drawing.Size(196, 24)
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
        Me.TabControl1.Controls.Add(Me.ObjectView)
        Me.TabControl1.Controls.Add(Me.AttireView)
        Me.TabControl1.Controls.Add(Me.MuscleView)
        Me.TabControl1.Controls.Add(Me.MaskView)
        Me.TabControl1.Controls.Add(Me.ObjArrayView)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(984, 450)
        Me.TabControl1.TabIndex = 1
        '
        'HexView
        '
        Me.HexView.Controls.Add(Me.Hex_Selected)
        Me.HexView.Controls.Add(Me.MenuStripHexView)
        Me.HexView.Location = New System.Drawing.Point(4, 22)
        Me.HexView.Name = "HexView"
        Me.HexView.Padding = New System.Windows.Forms.Padding(3)
        Me.HexView.Size = New System.Drawing.Size(976, 424)
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
        Me.Hex_Selected.Size = New System.Drawing.Size(970, 391)
        Me.Hex_Selected.TabIndex = 0
        Me.Hex_Selected.WordWrap = False
        '
        'MenuStripHexView
        '
        Me.MenuStripHexView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HexViewBitWidth, Me.HexViewFileName})
        Me.MenuStripHexView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripHexView.Name = "MenuStripHexView"
        Me.MenuStripHexView.Size = New System.Drawing.Size(970, 27)
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
        Me.TextView.Size = New System.Drawing.Size(976, 424)
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
        Me.Text_Selected.Size = New System.Drawing.Size(970, 391)
        Me.Text_Selected.TabIndex = 1
        Me.Text_Selected.WordWrap = False
        '
        'MenuStripTextView
        '
        Me.MenuStripTextView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextViewBitWidth, Me.TextViewFileName})
        Me.MenuStripTextView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripTextView.Name = "MenuStripTextView"
        Me.MenuStripTextView.Size = New System.Drawing.Size(970, 27)
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
        Me.StringView.Size = New System.Drawing.Size(976, 424)
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
        Me.DataGridStringView.Size = New System.Drawing.Size(970, 394)
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
        Me.MenuStripStringView.Size = New System.Drawing.Size(970, 24)
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
        Me.MiscView.Size = New System.Drawing.Size(976, 424)
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
        Me.DataGridMiscView.Size = New System.Drawing.Size(970, 391)
        Me.DataGridMiscView.TabIndex = 1
        '
        'MenuStripMiscView
        '
        Me.MenuStripMiscView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MiscViewType})
        Me.MenuStripMiscView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMiscView.Name = "MenuStripMiscView"
        Me.MenuStripMiscView.Size = New System.Drawing.Size(970, 27)
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
        Me.ShowView.Size = New System.Drawing.Size(976, 424)
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
        Me.DataGridShowView.Size = New System.Drawing.Size(970, 391)
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
        Me.MenuStripShowView.Size = New System.Drawing.Size(970, 27)
        Me.MenuStripShowView.TabIndex = 0
        Me.MenuStripShowView.Text = "MenuStrip2"
        '
        'ShowViewType
        '
        Me.ShowViewType.Items.AddRange(New Object() {"2K15", "2K16", "2K17", "2K18", "2K19"})
        Me.ShowViewType.Name = "ShowViewType"
        Me.ShowViewType.Size = New System.Drawing.Size(121, 23)
        '
        'NIBJView
        '
        Me.NIBJView.Controls.Add(Me.DataGridNIBJView)
        Me.NIBJView.Controls.Add(Me.MenuStripNIBJView)
        Me.NIBJView.Location = New System.Drawing.Point(4, 22)
        Me.NIBJView.Name = "NIBJView"
        Me.NIBJView.Padding = New System.Windows.Forms.Padding(3)
        Me.NIBJView.Size = New System.Drawing.Size(976, 424)
        Me.NIBJView.TabIndex = 5
        Me.NIBJView.Text = "NIBJView"
        Me.NIBJView.UseVisualStyleBackColor = True
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
        Me.DataGridNIBJView.Size = New System.Drawing.Size(970, 391)
        Me.DataGridNIBJView.TabIndex = 2
        '
        'MenuStripNIBJView
        '
        Me.MenuStripNIBJView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NIBJViewType, Me.FileAttributesToolStripMenuItem})
        Me.MenuStripNIBJView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripNIBJView.Name = "MenuStripNIBJView"
        Me.MenuStripNIBJView.Size = New System.Drawing.Size(970, 27)
        Me.MenuStripNIBJView.TabIndex = 0
        Me.MenuStripNIBJView.Text = "MenuStrip2"
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
        Me.PictureView.Controls.Add(Me.PictureBox2)
        Me.PictureView.Location = New System.Drawing.Point(4, 22)
        Me.PictureView.Name = "PictureView"
        Me.PictureView.Size = New System.Drawing.Size(976, 424)
        Me.PictureView.TabIndex = 8
        Me.PictureView.Text = "Picture View"
        Me.PictureView.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(976, 424)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'ObjectView
        '
        Me.ObjectView.Controls.Add(Me.Label1)
        Me.ObjectView.Location = New System.Drawing.Point(4, 22)
        Me.ObjectView.Name = "ObjectView"
        Me.ObjectView.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectView.Size = New System.Drawing.Size(976, 424)
        Me.ObjectView.TabIndex = 7
        Me.ObjectView.Text = "Object View"
        Me.ObjectView.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(970, 418)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "To Be Added :("
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AttireView
        '
        Me.AttireView.Controls.Add(Me.DataGridAttireView)
        Me.AttireView.Controls.Add(Me.MenuStripAttireView)
        Me.AttireView.Location = New System.Drawing.Point(4, 22)
        Me.AttireView.Name = "AttireView"
        Me.AttireView.Padding = New System.Windows.Forms.Padding(3)
        Me.AttireView.Size = New System.Drawing.Size(976, 424)
        Me.AttireView.TabIndex = 9
        Me.AttireView.Text = "Attire View"
        Me.AttireView.UseVisualStyleBackColor = True
        '
        'DataGridAttireView
        '
        Me.DataGridAttireView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridAttireView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAttireView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAttireView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridAttireView.Name = "DataGridAttireView"
        Me.DataGridAttireView.Size = New System.Drawing.Size(970, 394)
        Me.DataGridAttireView.TabIndex = 1
        '
        'MenuStripAttireView
        '
        Me.MenuStripAttireView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StringLoadedToolStripMenuItem, Me.PacsLoadedToolStripMenuItem})
        Me.MenuStripAttireView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripAttireView.Name = "MenuStripAttireView"
        Me.MenuStripAttireView.Size = New System.Drawing.Size(970, 24)
        Me.MenuStripAttireView.TabIndex = 0
        Me.MenuStripAttireView.Text = "MenuStrip2"
        '
        'StringLoadedToolStripMenuItem
        '
        Me.StringLoadedToolStripMenuItem.Enabled = False
        Me.StringLoadedToolStripMenuItem.Name = "StringLoadedToolStripMenuItem"
        Me.StringLoadedToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.StringLoadedToolStripMenuItem.Text = "String Loaded:"
        '
        'PacsLoadedToolStripMenuItem
        '
        Me.PacsLoadedToolStripMenuItem.Enabled = False
        Me.PacsLoadedToolStripMenuItem.Name = "PacsLoadedToolStripMenuItem"
        Me.PacsLoadedToolStripMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.PacsLoadedToolStripMenuItem.Text = "Pacs Loaded:"
        '
        'MuscleView
        '
        Me.MuscleView.Controls.Add(Me.DataGridMuscleView)
        Me.MuscleView.Controls.Add(Me.MenuStripMuscleView)
        Me.MuscleView.Location = New System.Drawing.Point(4, 22)
        Me.MuscleView.Name = "MuscleView"
        Me.MuscleView.Padding = New System.Windows.Forms.Padding(3)
        Me.MuscleView.Size = New System.Drawing.Size(976, 424)
        Me.MuscleView.TabIndex = 10
        Me.MuscleView.Text = "Muscle View"
        Me.MuscleView.UseVisualStyleBackColor = True
        '
        'DataGridMuscleView
        '
        Me.DataGridMuscleView.AllowUserToAddRows = False
        Me.DataGridMuscleView.AllowUserToDeleteRows = False
        Me.DataGridMuscleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMuscleView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMuscleView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridMuscleView.Name = "DataGridMuscleView"
        Me.DataGridMuscleView.RowHeadersVisible = False
        Me.DataGridMuscleView.Size = New System.Drawing.Size(970, 394)
        Me.DataGridMuscleView.TabIndex = 2
        '
        'MenuStripMuscleView
        '
        Me.MenuStripMuscleView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.MenuStripMuscleView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMuscleView.Name = "MenuStripMuscleView"
        Me.MenuStripMuscleView.Size = New System.Drawing.Size(970, 24)
        Me.MenuStripMuscleView.TabIndex = 0
        Me.MenuStripMuscleView.Text = "MenuStrip3"
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'MaskView
        '
        Me.MaskView.Controls.Add(Me.DataGridMaskView)
        Me.MaskView.Controls.Add(Me.MenuStripMaskView)
        Me.MaskView.Location = New System.Drawing.Point(4, 22)
        Me.MaskView.Name = "MaskView"
        Me.MaskView.Padding = New System.Windows.Forms.Padding(3)
        Me.MaskView.Size = New System.Drawing.Size(976, 424)
        Me.MaskView.TabIndex = 11
        Me.MaskView.Text = "Mask View"
        Me.MaskView.UseVisualStyleBackColor = True
        '
        'DataGridMaskView
        '
        Me.DataGridMaskView.AllowUserToAddRows = False
        Me.DataGridMaskView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMaskView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Mask_Name, Me.Start_Face, Me.End_Face, Me.Add_Mask, Me.Del_Mask})
        Me.DataGridMaskView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMaskView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridMaskView.Name = "DataGridMaskView"
        Me.DataGridMaskView.Size = New System.Drawing.Size(970, 394)
        Me.DataGridMaskView.TabIndex = 2
        '
        'Mask_Name
        '
        Me.Mask_Name.HeaderText = "Mask_Name"
        Me.Mask_Name.Name = "Mask_Name"
        Me.Mask_Name.ReadOnly = True
        '
        'Start_Face
        '
        Me.Start_Face.HeaderText = "Start Face"
        Me.Start_Face.Name = "Start_Face"
        '
        'End_Face
        '
        Me.End_Face.HeaderText = "End Face"
        Me.End_Face.Name = "End_Face"
        '
        'Add_Mask
        '
        Me.Add_Mask.HeaderText = "Add"
        Me.Add_Mask.Name = "Add_Mask"
        '
        'Del_Mask
        '
        Me.Del_Mask.HeaderText = "Delete"
        Me.Del_Mask.Name = "Del_Mask"
        '
        'MenuStripMaskView
        '
        Me.MenuStripMaskView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMaskView.Name = "MenuStripMaskView"
        Me.MenuStripMaskView.Size = New System.Drawing.Size(970, 24)
        Me.MenuStripMaskView.TabIndex = 0
        Me.MenuStripMaskView.Text = "MenuStrip2"
        '
        'ObjArrayView
        '
        Me.ObjArrayView.Controls.Add(Me.DataGridObjArrayView)
        Me.ObjArrayView.Controls.Add(Me.MenuStrip2)
        Me.ObjArrayView.Location = New System.Drawing.Point(4, 22)
        Me.ObjArrayView.Name = "ObjArrayView"
        Me.ObjArrayView.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjArrayView.Size = New System.Drawing.Size(976, 424)
        Me.ObjArrayView.TabIndex = 12
        Me.ObjArrayView.Text = "Object Array View"
        Me.ObjArrayView.UseVisualStyleBackColor = True
        '
        'DataGridObjArrayView
        '
        Me.DataGridObjArrayView.AllowUserToAddRows = False
        Me.DataGridObjArrayView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridObjArrayView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjArrayView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChairNum, Me.ArrEnabled, Me.ChairName, Me.ArrX, Me.ArrY, Me.ArrZ, Me.ArrRX, Me.ArrRY, Me.ArrRZ, Me.ArrDec1, Me.ArrDec2})
        Me.DataGridObjArrayView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjArrayView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridObjArrayView.Name = "DataGridObjArrayView"
        Me.DataGridObjArrayView.Size = New System.Drawing.Size(970, 394)
        Me.DataGridObjArrayView.TabIndex = 1
        '
        'ChairNum
        '
        Me.ChairNum.Frozen = True
        Me.ChairNum.HeaderText = "Number"
        Me.ChairNum.Name = "ChairNum"
        Me.ChairNum.ReadOnly = True
        Me.ChairNum.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChairNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ChairNum.Width = 69
        '
        'ArrEnabled
        '
        Me.ArrEnabled.HeaderText = "Enabled"
        Me.ArrEnabled.Name = "ArrEnabled"
        Me.ArrEnabled.Width = 52
        '
        'ChairName
        '
        Me.ChairName.HeaderText = "Name"
        Me.ChairName.Name = "ChairName"
        Me.ChairName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChairName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ChairName.Width = 41
        '
        'ArrX
        '
        Me.ArrX.HeaderText = "X"
        Me.ArrX.Name = "ArrX"
        Me.ArrX.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrX.Width = 20
        '
        'ArrY
        '
        Me.ArrY.HeaderText = "Y"
        Me.ArrY.Name = "ArrY"
        Me.ArrY.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrY.Width = 20
        '
        'ArrZ
        '
        Me.ArrZ.HeaderText = "Z"
        Me.ArrZ.Name = "ArrZ"
        Me.ArrZ.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrZ.Width = 20
        '
        'ArrRX
        '
        Me.ArrRX.HeaderText = "RX"
        Me.ArrRX.Name = "ArrRX"
        Me.ArrRX.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrRX.Width = 28
        '
        'ArrRY
        '
        Me.ArrRY.HeaderText = "RY"
        Me.ArrRY.Name = "ArrRY"
        Me.ArrRY.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrRY.Width = 28
        '
        'ArrRZ
        '
        Me.ArrRZ.HeaderText = "RZ"
        Me.ArrRZ.Name = "ArrRZ"
        Me.ArrRZ.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrRZ.Width = 28
        '
        'ArrDec1
        '
        Me.ArrDec1.HeaderText = "D1"
        Me.ArrDec1.Name = "ArrDec1"
        Me.ArrDec1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrDec1.Width = 27
        '
        'ArrDec2
        '
        Me.ArrDec2.HeaderText = "D2"
        Me.ArrDec2.Name = "ArrDec2"
        Me.ArrDec2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArrDec2.Width = 27
        '
        'MenuStrip2
        '
        Me.MenuStrip2.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(970, 24)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "MenuStrip2"
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
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(22, 20)
        Me.ToolStripMenuItem1.Text = " "
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Multiselect = True
        '
        'TreeViewContext
        '
        Me.TreeViewContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem1, Me.ExtractToolStripMenuItem, Me.InjectToolStripMenuItem, Me.CrawlToolStripMenuItem})
        Me.TreeViewContext.Name = "TreeViewContext"
        Me.TreeViewContext.Size = New System.Drawing.Size(110, 92)
        '
        'OpenToolStripMenuItem1
        '
        Me.OpenToolStripMenuItem1.Name = "OpenToolStripMenuItem1"
        Me.OpenToolStripMenuItem1.Size = New System.Drawing.Size(109, 22)
        Me.OpenToolStripMenuItem1.Text = "Open"
        '
        'ExtractToolStripMenuItem
        '
        Me.ExtractToolStripMenuItem.Name = "ExtractToolStripMenuItem"
        Me.ExtractToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.ExtractToolStripMenuItem.Text = "Extract"
        '
        'InjectToolStripMenuItem
        '
        Me.InjectToolStripMenuItem.Name = "InjectToolStripMenuItem"
        Me.InjectToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.InjectToolStripMenuItem.Text = "Inject"
        '
        'CrawlToolStripMenuItem
        '
        Me.CrawlToolStripMenuItem.Name = "CrawlToolStripMenuItem"
        Me.CrawlToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.CrawlToolStripMenuItem.Text = "Crawl"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.FillWeight = 25.0!
        Me.DataGridViewTextBoxColumn1.HeaderText = "HexRef"
        Me.DataGridViewTextBoxColumn1.MaxInputLength = 10
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 161
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "String Text"
        Me.DataGridViewTextBoxColumn2.MaxInputLength = 31
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 645
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.FillWeight = 25.0!
        Me.DataGridViewTextBoxColumn3.HeaderText = "Length"
        Me.DataGridViewTextBoxColumn3.MaxInputLength = 10
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 161
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "StrName"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 73
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "S1"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Width = 45
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "S2"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 45
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "S3"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Width = 45
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "S4"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 45
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "A1"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Width = 45
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "A2"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 45
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "B"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Width = 39
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "Stage"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 60
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "D1"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 46
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "D2"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Width = 46
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "Ref"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 49
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "Filter"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Width = 54
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.HeaderText = "F1"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Width = 44
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.HeaderText = "F2"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Width = 44
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.HeaderText = "G1"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 46
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.HeaderText = "G2"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.Width = 46
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.HeaderText = "H1"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.Width = 46
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.HeaderText = "H2"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.Width = 46
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.HeaderText = "H3"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.Width = 46
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.HeaderText = "H4"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.Width = 46
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.HeaderText = "Bar"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.Width = 48
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.HeaderText = "Unkown"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.Width = 72
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.HeaderText = "I1"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.Width = 41
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.HeaderText = "I2"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.Width = 41
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.HeaderText = "I3"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.Width = 41
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.HeaderText = "Live"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.Width = 52
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.HeaderText = "J"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.Width = 37
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.HeaderText = "Mask_Name"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.HeaderText = "Start Face"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.HeaderText = "End Face"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        '
        'NumericUpDownColumn1
        '
        Me.NumericUpDownColumn1.Frozen = True
        Me.NumericUpDownColumn1.HeaderText = "Number"
        Me.NumericUpDownColumn1.Name = "NumericUpDownColumn1"
        Me.NumericUpDownColumn1.ReadOnly = True
        Me.NumericUpDownColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.NumericUpDownColumn1.Width = 69
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn35.Width = 41
        '
        'NumericUpDownColumn2
        '
        Me.NumericUpDownColumn2.HeaderText = "X"
        Me.NumericUpDownColumn2.Name = "NumericUpDownColumn2"
        Me.NumericUpDownColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn2.Width = 20
        '
        'NumericUpDownColumn3
        '
        Me.NumericUpDownColumn3.HeaderText = "Y"
        Me.NumericUpDownColumn3.Name = "NumericUpDownColumn3"
        Me.NumericUpDownColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn3.Width = 20
        '
        'NumericUpDownColumn4
        '
        Me.NumericUpDownColumn4.HeaderText = "Z"
        Me.NumericUpDownColumn4.Name = "NumericUpDownColumn4"
        Me.NumericUpDownColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn4.Width = 20
        '
        'NumericUpDownColumn5
        '
        Me.NumericUpDownColumn5.HeaderText = "RX"
        Me.NumericUpDownColumn5.Name = "NumericUpDownColumn5"
        Me.NumericUpDownColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn5.Width = 28
        '
        'NumericUpDownColumn6
        '
        Me.NumericUpDownColumn6.HeaderText = "RY"
        Me.NumericUpDownColumn6.Name = "NumericUpDownColumn6"
        Me.NumericUpDownColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn6.Width = 28
        '
        'NumericUpDownColumn7
        '
        Me.NumericUpDownColumn7.HeaderText = "RZ"
        Me.NumericUpDownColumn7.Name = "NumericUpDownColumn7"
        Me.NumericUpDownColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn7.Width = 28
        '
        'NumericUpDownColumn8
        '
        Me.NumericUpDownColumn8.HeaderText = "D1"
        Me.NumericUpDownColumn8.Name = "NumericUpDownColumn8"
        Me.NumericUpDownColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn8.Width = 27
        '
        'NumericUpDownColumn9
        '
        Me.NumericUpDownColumn9.HeaderText = "D2"
        Me.NumericUpDownColumn9.Name = "NumericUpDownColumn9"
        Me.NumericUpDownColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.NumericUpDownColumn9.Width = 27
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 474)
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
        CType(Me.DataGridNIBJView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripNIBJView.ResumeLayout(False)
        Me.MenuStripNIBJView.PerformLayout()
        Me.PictureView.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjectView.ResumeLayout(False)
        Me.AttireView.ResumeLayout(False)
        Me.AttireView.PerformLayout()
        CType(Me.DataGridAttireView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripAttireView.ResumeLayout(False)
        Me.MenuStripAttireView.PerformLayout()
        Me.MuscleView.ResumeLayout(False)
        Me.MuscleView.PerformLayout()
        CType(Me.DataGridMuscleView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripMuscleView.ResumeLayout(False)
        Me.MenuStripMuscleView.PerformLayout()
        Me.MaskView.ResumeLayout(False)
        Me.MaskView.PerformLayout()
        CType(Me.DataGridMaskView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjArrayView.ResumeLayout(False)
        Me.ObjArrayView.PerformLayout()
        CType(Me.DataGridObjArrayView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripPictureView.ResumeLayout(False)
        Me.MenuStripPictureView.PerformLayout()
        Me.TreeViewContext.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadHomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
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
    Friend WithEvents ObjectViewPictureView As TabPage
    Friend WithEvents MenuStripPictureView As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TreeViewContext As ContextMenuStrip
    Friend WithEvents ExtractToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents OpenToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ObjectView As TabPage
    Friend WithEvents PictureView As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents AttireView As TabPage
    Friend WithEvents DataGridAttireView As DataGridView
    Friend WithEvents MenuStripAttireView As MenuStrip
    Friend WithEvents StringLoadedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PacsLoadedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MuscleView As TabPage
    Friend WithEvents DataGridMuscleView As DataGridView
    Friend WithEvents MenuStripMuscleView As MenuStrip
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MaskView As TabPage
    Friend WithEvents DataGridMaskView As DataGridView
    Friend WithEvents Mask_Name As DataGridViewTextBoxColumn
    Friend WithEvents Start_Face As DataGridViewTextBoxColumn
    Friend WithEvents End_Face As DataGridViewTextBoxColumn
    Friend WithEvents Add_Mask As DataGridViewButtonColumn
    Friend WithEvents Del_Mask As DataGridViewButtonColumn
    Friend WithEvents MenuStripMaskView As MenuStrip
    Friend WithEvents ObjArrayView As TabPage
    Friend WithEvents MenuStrip2 As MenuStrip
    Friend WithEvents DataGridObjArrayView As DataGridView
    Friend WithEvents ChairNum As NumericUpDownColumn
    Friend WithEvents ArrEnabled As DataGridViewCheckBoxColumn
    Friend WithEvents ChairName As DataGridViewTextBoxColumn
    Friend WithEvents ArrX As NumericUpDownColumn
    Friend WithEvents ArrY As NumericUpDownColumn
    Friend WithEvents ArrZ As NumericUpDownColumn
    Friend WithEvents ArrRX As NumericUpDownColumn
    Friend WithEvents ArrRY As NumericUpDownColumn
    Friend WithEvents ArrRZ As NumericUpDownColumn
    Friend WithEvents ArrDec1 As NumericUpDownColumn
    Friend WithEvents ArrDec2 As NumericUpDownColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn33 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As DataGridViewTextBoxColumn
    Friend WithEvents NumericUpDownColumn1 As NumericUpDownColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As DataGridViewTextBoxColumn
    Friend WithEvents NumericUpDownColumn2 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn3 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn4 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn5 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn6 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn7 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn8 As NumericUpDownColumn
    Friend WithEvents NumericUpDownColumn9 As NumericUpDownColumn
    Friend WithEvents InjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CrawlToolStripMenuItem As ToolStripMenuItem
End Class
