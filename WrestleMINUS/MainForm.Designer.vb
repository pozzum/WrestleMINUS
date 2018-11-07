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
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GitHubIssuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.MenuStripTreeView = New System.Windows.Forms.MenuStrip()
        Me.CurrentViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.HexView = New System.Windows.Forms.TabPage()
        Me.Hex_Selected = New System.Windows.Forms.RichTextBox()
        Me.MenuStripHexView = New System.Windows.Forms.MenuStrip()
        Me.HexViewBitWidth = New System.Windows.Forms.ToolStripComboBox()
        Me.HexViewFileName = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextView = New System.Windows.Forms.TabPage()
        Me.Text_Selected = New System.Windows.Forms.RichTextBox()
        Me.MenuStripTextView = New System.Windows.Forms.MenuStrip()
        Me.TextViewBitWidth = New System.Windows.Forms.ToolStripComboBox()
        Me.TextViewFileName = New System.Windows.Forms.ToolStripMenuItem()
        Me.StringView = New System.Windows.Forms.TabPage()
        Me.DataGridStringView = New System.Windows.Forms.DataGridView()
        Me.HexRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StringText = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Length = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddString = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.DeleteString = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripStringView = New System.Windows.Forms.MenuStrip()
        Me.StringCountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBoxSearch = New System.Windows.Forms.ToolStripTextBox()
        Me.SaveStringChangesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SortStringsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MiscView = New System.Windows.Forms.TabPage()
        Me.DataGridMiscView = New System.Windows.Forms.DataGridView()
        Me.Col_ArenaNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Stadium = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Advertisement = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CornerPost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_LED_CornerPost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Rope = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Apron = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_LED_Apron = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Turnbuckle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Barricade = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Fence = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CeilingLighting = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Spotlight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Stairs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CommentarySeat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_RingMat = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_FloorMattress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Crowd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CrowdSeatsPlace = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CrowdSeatsModel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_IBL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Titantron = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Minitron = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Wall_L = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Wall_R = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Header = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Floor = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_MiscObjects = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_LightingType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_CornerPost_CM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Rope_CM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Apron_CM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Turnbuckle_CM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_RingMat_CM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_version = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripMiscView = New System.Windows.Forms.MenuStrip()
        Me.MiscViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.SaveMiscChangesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowView = New System.Windows.Forms.TabPage()
        Me.DataGridShowView = New System.Windows.Forms.DataGridView()
        Me.MenuStripShowView = New System.Windows.Forms.MenuStrip()
        Me.ShowViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.StringLoadedShowMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.Pach = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Count = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire0Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire0String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire0Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire0Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire0Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire1Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire1String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire1Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire1Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire1Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire2Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire2String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire2Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire2Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire2Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire3Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire3String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire3Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire3Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire3Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire4Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire4String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire4Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire4Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire4Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire5Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire5String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire5Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire5Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire5Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire6Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire6String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire6Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire6Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire6Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire7Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire7String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire7Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire7Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire7Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire8Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire8String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire8Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire8Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire8Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire9Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire9String = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Attire9Enabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire9Manager = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Attire9Unlock = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripAttireView = New System.Windows.Forms.MenuStrip()
        Me.StringLoadedAttireMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PacsLoadedAttireMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesAttireMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ArrEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ChairName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStrip2 = New System.Windows.Forms.MenuStrip()
        Me.AssetView = New System.Windows.Forms.TabPage()
        Me.DataGridAssetView = New System.Windows.Forms.DataGridView()
        Me.PacNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AttireNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AudioNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitantronNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MiniNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HeaderNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WallNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RampNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WallRightNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WallLeftNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleView = New System.Windows.Forms.TabPage()
        Me.DataGridTitleView = New System.Windows.Forms.DataGridView()
        Me.TitleEnabled = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PropRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name1Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name2Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Name3Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MyWWE1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MyWWE2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Uni1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Uni2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Temp1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Temp2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Female = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TagTeam = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Cruiserweight = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UnlockNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Temp4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStrip3 = New System.Windows.Forms.MenuStrip()
        Me.TitleGameComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.StringLoadedTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PacsLoadedTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStripPictureView = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TreeViewContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenWithToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractAllInPlaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractAllToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrawlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SaveExtractAllDialog = New System.Windows.Forms.SaveFileDialog()
        Me.SaveShowChangesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StrName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowStringName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowSelectNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SelectNumSecond = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.A2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.B3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C7 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.C8 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Stage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.D5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Filter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.F4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.AssetView.SuspendLayout()
        CType(Me.DataGridAssetView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TitleView.SuspendLayout()
        CType(Me.DataGridTitleView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip3.SuspendLayout()
        Me.MenuStripPictureView.SuspendLayout()
        Me.TreeViewContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.HelpToolStripMenuItem})
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
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.SupportToolStripMenuItem, Me.GitHubIssuesToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'SupportToolStripMenuItem
        '
        Me.SupportToolStripMenuItem.Name = "SupportToolStripMenuItem"
        Me.SupportToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.SupportToolStripMenuItem.Text = "ST Thread"
        '
        'GitHubIssuesToolStripMenuItem
        '
        Me.GitHubIssuesToolStripMenuItem.Name = "GitHubIssuesToolStripMenuItem"
        Me.GitHubIssuesToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.GitHubIssuesToolStripMenuItem.Text = "GitHub Issues"
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
        Me.SplitContainer1.SplitterDistance = 202
        Me.SplitContainer1.TabIndex = 1
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 24)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.Size = New System.Drawing.Size(202, 403)
        Me.TreeView1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Qu.png")
        Me.ImageList1.Images.SetKeyName(1, "F.png")
        Me.ImageList1.Images.SetKeyName(2, "H.png")
        Me.ImageList1.Images.SetKeyName(3, "8.png")
        Me.ImageList1.Images.SetKeyName(4, "E.png")
        Me.ImageList1.Images.SetKeyName(5, "S.png")
        Me.ImageList1.Images.SetKeyName(6, "P.png")
        Me.ImageList1.Images.SetKeyName(7, "B.png")
        Me.ImageList1.Images.SetKeyName(8, "Z.png")
        Me.ImageList1.Images.SetKeyName(9, "O.png")
        Me.ImageList1.Images.SetKeyName(10, "T.png")
        Me.ImageList1.Images.SetKeyName(11, "A.png")
        Me.ImageList1.Images.SetKeyName(12, "Y.png")
        Me.ImageList1.Images.SetKeyName(13, "AT.png")
        Me.ImageList1.Images.SetKeyName(14, "D.png")
        Me.ImageList1.Images.SetKeyName(15, "M.png")
        Me.ImageList1.Images.SetKeyName(16, "G.png")
        Me.ImageList1.Images.SetKeyName(17, "N.png")
        Me.ImageList1.Images.SetKeyName(18, "2.png")
        Me.ImageList1.Images.SetKeyName(19, "C.png")
        Me.ImageList1.Images.SetKeyName(20, "U.png")
        Me.ImageList1.Images.SetKeyName(21, "K.png")
        Me.ImageList1.Images.SetKeyName(22, "V.png")
        Me.ImageList1.Images.SetKeyName(23, "W.png")
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 427)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(202, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'MenuStripTreeView
        '
        Me.MenuStripTreeView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentViewToolStripMenuItem})
        Me.MenuStripTreeView.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripTreeView.Name = "MenuStripTreeView"
        Me.MenuStripTreeView.Size = New System.Drawing.Size(202, 24)
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
        Me.TabControl1.Controls.Add(Me.AssetView)
        Me.TabControl1.Controls.Add(Me.TitleView)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(978, 450)
        Me.TabControl1.TabIndex = 1
        '
        'HexView
        '
        Me.HexView.Controls.Add(Me.Hex_Selected)
        Me.HexView.Controls.Add(Me.MenuStripHexView)
        Me.HexView.Location = New System.Drawing.Point(4, 22)
        Me.HexView.Name = "HexView"
        Me.HexView.Padding = New System.Windows.Forms.Padding(3)
        Me.HexView.Size = New System.Drawing.Size(970, 424)
        Me.HexView.TabIndex = 0
        Me.HexView.Text = "Hex View"
        Me.HexView.UseVisualStyleBackColor = True
        '
        'Hex_Selected
        '
        Me.Hex_Selected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Hex_Selected.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Hex_Selected.Location = New System.Drawing.Point(3, 30)
        Me.Hex_Selected.Name = "Hex_Selected"
        Me.Hex_Selected.ReadOnly = True
        Me.Hex_Selected.Size = New System.Drawing.Size(964, 391)
        Me.Hex_Selected.TabIndex = 2
        Me.Hex_Selected.Text = ""
        '
        'MenuStripHexView
        '
        Me.MenuStripHexView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HexViewBitWidth, Me.HexViewFileName})
        Me.MenuStripHexView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripHexView.Name = "MenuStripHexView"
        Me.MenuStripHexView.Size = New System.Drawing.Size(964, 27)
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
        Me.TextView.Size = New System.Drawing.Size(970, 424)
        Me.TextView.TabIndex = 1
        Me.TextView.Text = "Text View"
        Me.TextView.UseVisualStyleBackColor = True
        '
        'Text_Selected
        '
        Me.Text_Selected.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Text_Selected.Font = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text_Selected.Location = New System.Drawing.Point(3, 30)
        Me.Text_Selected.Name = "Text_Selected"
        Me.Text_Selected.ReadOnly = True
        Me.Text_Selected.Size = New System.Drawing.Size(964, 391)
        Me.Text_Selected.TabIndex = 2
        Me.Text_Selected.Text = ""
        '
        'MenuStripTextView
        '
        Me.MenuStripTextView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextViewBitWidth, Me.TextViewFileName})
        Me.MenuStripTextView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripTextView.Name = "MenuStripTextView"
        Me.MenuStripTextView.Size = New System.Drawing.Size(964, 27)
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
        Me.StringView.Size = New System.Drawing.Size(970, 424)
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
        Me.DataGridStringView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.HexRef, Me.StringText, Me.Length, Me.AddString, Me.DeleteString})
        Me.DataGridStringView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridStringView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridStringView.MultiSelect = False
        Me.DataGridStringView.Name = "DataGridStringView"
        Me.DataGridStringView.RowHeadersVisible = False
        Me.DataGridStringView.Size = New System.Drawing.Size(964, 391)
        Me.DataGridStringView.TabIndex = 2
        '
        'HexRef
        '
        Me.HexRef.FillWeight = 25.0!
        Me.HexRef.HeaderText = "HexRef"
        Me.HexRef.MaxInputLength = 8
        Me.HexRef.Name = "HexRef"
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
        Me.Length.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'AddString
        '
        Me.AddString.FillWeight = 10.0!
        Me.AddString.HeaderText = "Add"
        Me.AddString.Name = "AddString"
        '
        'DeleteString
        '
        Me.DeleteString.FillWeight = 10.0!
        Me.DeleteString.HeaderText = "Delete"
        Me.DeleteString.Name = "DeleteString"
        Me.DeleteString.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DeleteString.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'MenuStripStringView
        '
        Me.MenuStripStringView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StringCountToolStripMenuItem, Me.ToolStripTextBoxSearch, Me.SaveStringChangesToolStripMenuItem, Me.SortStringsToolStripMenuItem})
        Me.MenuStripStringView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripStringView.Name = "MenuStripStringView"
        Me.MenuStripStringView.Size = New System.Drawing.Size(964, 27)
        Me.MenuStripStringView.TabIndex = 0
        Me.MenuStripStringView.Text = "MenuStrip2"
        '
        'StringCountToolStripMenuItem
        '
        Me.StringCountToolStripMenuItem.Enabled = False
        Me.StringCountToolStripMenuItem.Name = "StringCountToolStripMenuItem"
        Me.StringCountToolStripMenuItem.Size = New System.Drawing.Size(89, 23)
        Me.StringCountToolStripMenuItem.Text = "String Count:"
        '
        'ToolStripTextBoxSearch
        '
        Me.ToolStripTextBoxSearch.Name = "ToolStripTextBoxSearch"
        Me.ToolStripTextBoxSearch.Size = New System.Drawing.Size(100, 23)
        Me.ToolStripTextBoxSearch.Text = "Search..."
        '
        'SaveStringChangesToolStripMenuItem
        '
        Me.SaveStringChangesToolStripMenuItem.Name = "SaveStringChangesToolStripMenuItem"
        Me.SaveStringChangesToolStripMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveStringChangesToolStripMenuItem.Text = "Save Changes"
        Me.SaveStringChangesToolStripMenuItem.Visible = False
        '
        'SortStringsToolStripMenuItem
        '
        Me.SortStringsToolStripMenuItem.Name = "SortStringsToolStripMenuItem"
        Me.SortStringsToolStripMenuItem.Size = New System.Drawing.Size(79, 23)
        Me.SortStringsToolStripMenuItem.Text = "Sort Strings"
        Me.SortStringsToolStripMenuItem.Visible = False
        '
        'MiscView
        '
        Me.MiscView.Controls.Add(Me.DataGridMiscView)
        Me.MiscView.Controls.Add(Me.MenuStripMiscView)
        Me.MiscView.Location = New System.Drawing.Point(4, 22)
        Me.MiscView.Name = "MiscView"
        Me.MiscView.Padding = New System.Windows.Forms.Padding(3)
        Me.MiscView.Size = New System.Drawing.Size(970, 424)
        Me.MiscView.TabIndex = 3
        Me.MiscView.Text = "Misc View"
        Me.MiscView.UseVisualStyleBackColor = True
        '
        'DataGridMiscView
        '
        Me.DataGridMiscView.AllowUserToAddRows = False
        Me.DataGridMiscView.AllowUserToDeleteRows = False
        Me.DataGridMiscView.AllowUserToResizeRows = False
        Me.DataGridMiscView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridMiscView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMiscView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_ArenaNumber, Me.Col_Stadium, Me.Col_Advertisement, Me.Col_CornerPost, Me.Col_LED_CornerPost, Me.Col_Rope, Me.Col_Apron, Me.Col_LED_Apron, Me.Col_Turnbuckle, Me.Col_Barricade, Me.Col_Fence, Me.Col_CeilingLighting, Me.Col_Spotlight, Me.Col_Stairs, Me.Col_CommentarySeat, Me.Col_RingMat, Me.Col_FloorMattress, Me.Col_Crowd, Me.Col_CrowdSeatsPlace, Me.Col_CrowdSeatsModel, Me.Col_IBL, Me.Col_Titantron, Me.Col_Minitron, Me.Col_Wall_L, Me.Col_Wall_R, Me.Col_Header, Me.Col_Floor, Me.Col_MiscObjects, Me.Col_LightingType, Me.Col_CornerPost_CM, Me.Col_Rope_CM, Me.Col_Apron_CM, Me.Col_Turnbuckle_CM, Me.Col_RingMat_CM, Me.Col_version})
        Me.DataGridMiscView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMiscView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridMiscView.Name = "DataGridMiscView"
        Me.DataGridMiscView.RowHeadersVisible = False
        Me.DataGridMiscView.RowHeadersWidth = 70
        Me.DataGridMiscView.Size = New System.Drawing.Size(964, 391)
        Me.DataGridMiscView.TabIndex = 1
        '
        'Col_ArenaNumber
        '
        Me.Col_ArenaNumber.HeaderText = "ArenaNum"
        Me.Col_ArenaNumber.MaxInputLength = 5
        Me.Col_ArenaNumber.Name = "Col_ArenaNumber"
        Me.Col_ArenaNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_ArenaNumber.Width = 63
        '
        'Col_Stadium
        '
        Me.Col_Stadium.HeaderText = "Stadium"
        Me.Col_Stadium.MaxInputLength = 3
        Me.Col_Stadium.Name = "Col_Stadium"
        Me.Col_Stadium.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Stadium.Width = 51
        '
        'Col_Advertisement
        '
        Me.Col_Advertisement.HeaderText = "Advert"
        Me.Col_Advertisement.MaxInputLength = 3
        Me.Col_Advertisement.Name = "Col_Advertisement"
        Me.Col_Advertisement.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Advertisement.Width = 44
        '
        'Col_CornerPost
        '
        Me.Col_CornerPost.HeaderText = "CornerPost"
        Me.Col_CornerPost.MaxInputLength = 3
        Me.Col_CornerPost.Name = "Col_CornerPost"
        Me.Col_CornerPost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_CornerPost.Width = 65
        '
        'Col_LED_CornerPost
        '
        Me.Col_LED_CornerPost.HeaderText = "LEDCorner"
        Me.Col_LED_CornerPost.MaxInputLength = 3
        Me.Col_LED_CornerPost.Name = "Col_LED_CornerPost"
        Me.Col_LED_CornerPost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_LED_CornerPost.Width = 65
        '
        'Col_Rope
        '
        Me.Col_Rope.HeaderText = "Rope"
        Me.Col_Rope.MaxInputLength = 3
        Me.Col_Rope.Name = "Col_Rope"
        Me.Col_Rope.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Rope.Width = 39
        '
        'Col_Apron
        '
        Me.Col_Apron.HeaderText = "Apron"
        Me.Col_Apron.MaxInputLength = 3
        Me.Col_Apron.Name = "Col_Apron"
        Me.Col_Apron.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Apron.Width = 41
        '
        'Col_LED_Apron
        '
        Me.Col_LED_Apron.HeaderText = "LEDApron"
        Me.Col_LED_Apron.MaxInputLength = 3
        Me.Col_LED_Apron.Name = "Col_LED_Apron"
        Me.Col_LED_Apron.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_LED_Apron.Width = 62
        '
        'Col_Turnbuckle
        '
        Me.Col_Turnbuckle.HeaderText = "Turnbuckle"
        Me.Col_Turnbuckle.MaxInputLength = 3
        Me.Col_Turnbuckle.Name = "Col_Turnbuckle"
        Me.Col_Turnbuckle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Turnbuckle.Width = 67
        '
        'Col_Barricade
        '
        Me.Col_Barricade.HeaderText = "Barricade"
        Me.Col_Barricade.MaxInputLength = 3
        Me.Col_Barricade.Name = "Col_Barricade"
        Me.Col_Barricade.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Barricade.Width = 58
        '
        'Col_Fence
        '
        Me.Col_Fence.HeaderText = "Fence"
        Me.Col_Fence.MaxInputLength = 3
        Me.Col_Fence.Name = "Col_Fence"
        Me.Col_Fence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Fence.Width = 43
        '
        'Col_CeilingLighting
        '
        Me.Col_CeilingLighting.HeaderText = "CLight"
        Me.Col_CeilingLighting.MaxInputLength = 3
        Me.Col_CeilingLighting.Name = "Col_CeilingLighting"
        Me.Col_CeilingLighting.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_CeilingLighting.Width = 43
        '
        'Col_Spotlight
        '
        Me.Col_Spotlight.HeaderText = "Spotlight"
        Me.Col_Spotlight.MaxInputLength = 3
        Me.Col_Spotlight.Name = "Col_Spotlight"
        Me.Col_Spotlight.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Spotlight.Width = 54
        '
        'Col_Stairs
        '
        Me.Col_Stairs.HeaderText = "Stairs"
        Me.Col_Stairs.MaxInputLength = 3
        Me.Col_Stairs.Name = "Col_Stairs"
        Me.Col_Stairs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Stairs.Width = 39
        '
        'Col_CommentarySeat
        '
        Me.Col_CommentarySeat.HeaderText = "ComSeat"
        Me.Col_CommentarySeat.MaxInputLength = 3
        Me.Col_CommentarySeat.Name = "Col_CommentarySeat"
        Me.Col_CommentarySeat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_CommentarySeat.Width = 56
        '
        'Col_RingMat
        '
        Me.Col_RingMat.HeaderText = "RingMat"
        Me.Col_RingMat.MaxInputLength = 3
        Me.Col_RingMat.Name = "Col_RingMat"
        Me.Col_RingMat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_RingMat.Width = 53
        '
        'Col_FloorMattress
        '
        Me.Col_FloorMattress.HeaderText = "FloorMat"
        Me.Col_FloorMattress.MaxInputLength = 3
        Me.Col_FloorMattress.Name = "Col_FloorMattress"
        Me.Col_FloorMattress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_FloorMattress.Width = 54
        '
        'Col_Crowd
        '
        Me.Col_Crowd.HeaderText = "Crowd"
        Me.Col_Crowd.MaxInputLength = 3
        Me.Col_Crowd.Name = "Col_Crowd"
        Me.Col_Crowd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Crowd.Width = 43
        '
        'Col_CrowdSeatsPlace
        '
        Me.Col_CrowdSeatsPlace.HeaderText = "CrSeatsPlace"
        Me.Col_CrowdSeatsPlace.MaxInputLength = 3
        Me.Col_CrowdSeatsPlace.Name = "Col_CrowdSeatsPlace"
        Me.Col_CrowdSeatsPlace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_CrowdSeatsPlace.Width = 77
        '
        'Col_CrowdSeatsModel
        '
        Me.Col_CrowdSeatsModel.HeaderText = "CrSeatsModel"
        Me.Col_CrowdSeatsModel.MaxInputLength = 3
        Me.Col_CrowdSeatsModel.Name = "Col_CrowdSeatsModel"
        Me.Col_CrowdSeatsModel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_CrowdSeatsModel.Width = 79
        '
        'Col_IBL
        '
        Me.Col_IBL.HeaderText = "IBL"
        Me.Col_IBL.MaxInputLength = 3
        Me.Col_IBL.Name = "Col_IBL"
        Me.Col_IBL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_IBL.Width = 29
        '
        'Col_Titantron
        '
        Me.Col_Titantron.HeaderText = "Titantron"
        Me.Col_Titantron.MaxInputLength = 3
        Me.Col_Titantron.Name = "Col_Titantron"
        Me.Col_Titantron.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Titantron.Width = 55
        '
        'Col_Minitron
        '
        Me.Col_Minitron.HeaderText = "Minitron"
        Me.Col_Minitron.MaxInputLength = 3
        Me.Col_Minitron.Name = "Col_Minitron"
        Me.Col_Minitron.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Minitron.Width = 50
        '
        'Col_Wall_L
        '
        Me.Col_Wall_L.HeaderText = "Wall_L"
        Me.Col_Wall_L.MaxInputLength = 3
        Me.Col_Wall_L.Name = "Col_Wall_L"
        Me.Col_Wall_L.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Wall_L.Width = 46
        '
        'Col_Wall_R
        '
        Me.Col_Wall_R.HeaderText = "Wall_R"
        Me.Col_Wall_R.MaxInputLength = 3
        Me.Col_Wall_R.Name = "Col_Wall_R"
        Me.Col_Wall_R.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Wall_R.Width = 48
        '
        'Col_Header
        '
        Me.Col_Header.HeaderText = "Header"
        Me.Col_Header.MaxInputLength = 3
        Me.Col_Header.Name = "Col_Header"
        Me.Col_Header.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Header.Width = 48
        '
        'Col_Floor
        '
        Me.Col_Floor.HeaderText = "Floor"
        Me.Col_Floor.MaxInputLength = 3
        Me.Col_Floor.Name = "Col_Floor"
        Me.Col_Floor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Floor.Width = 36
        '
        'Col_MiscObjects
        '
        Me.Col_MiscObjects.HeaderText = "MiscO"
        Me.Col_MiscObjects.MaxInputLength = 3
        Me.Col_MiscObjects.Name = "Col_MiscObjects"
        Me.Col_MiscObjects.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_MiscObjects.Width = 43
        '
        'Col_LightingType
        '
        Me.Col_LightingType.HeaderText = "Lighting"
        Me.Col_LightingType.MaxInputLength = 3
        Me.Col_LightingType.Name = "Col_LightingType"
        Me.Col_LightingType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_LightingType.Width = 50
        '
        'Col_CornerPost_CM
        '
        Me.Col_CornerPost_CM.HeaderText = "CornerPost_CM"
        Me.Col_CornerPost_CM.MaxInputLength = 3
        Me.Col_CornerPost_CM.Name = "Col_CornerPost_CM"
        Me.Col_CornerPost_CM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_CornerPost_CM.Width = 87
        '
        'Col_Rope_CM
        '
        Me.Col_Rope_CM.HeaderText = "Rope_CM"
        Me.Col_Rope_CM.MaxInputLength = 3
        Me.Col_Rope_CM.Name = "Col_Rope_CM"
        Me.Col_Rope_CM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Rope_CM.Width = 61
        '
        'Col_Apron_CM
        '
        Me.Col_Apron_CM.HeaderText = "Apron_CM"
        Me.Col_Apron_CM.MaxInputLength = 3
        Me.Col_Apron_CM.Name = "Col_Apron_CM"
        Me.Col_Apron_CM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Apron_CM.Width = 63
        '
        'Col_Turnbuckle_CM
        '
        Me.Col_Turnbuckle_CM.HeaderText = "Buckle_CM"
        Me.Col_Turnbuckle_CM.MaxInputLength = 3
        Me.Col_Turnbuckle_CM.Name = "Col_Turnbuckle_CM"
        Me.Col_Turnbuckle_CM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_Turnbuckle_CM.Width = 68
        '
        'Col_RingMat_CM
        '
        Me.Col_RingMat_CM.HeaderText = "RingMat_CM"
        Me.Col_RingMat_CM.MaxInputLength = 3
        Me.Col_RingMat_CM.Name = "Col_RingMat_CM"
        Me.Col_RingMat_CM.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_RingMat_CM.Width = 75
        '
        'Col_version
        '
        Me.Col_version.HeaderText = "version"
        Me.Col_version.MaxInputLength = 3
        Me.Col_version.Name = "Col_version"
        Me.Col_version.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Col_version.Width = 47
        '
        'MenuStripMiscView
        '
        Me.MenuStripMiscView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MiscViewType, Me.SaveMiscChangesToolStripMenuItem})
        Me.MenuStripMiscView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMiscView.Name = "MenuStripMiscView"
        Me.MenuStripMiscView.Size = New System.Drawing.Size(964, 27)
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
        'SaveMiscChangesToolStripMenuItem
        '
        Me.SaveMiscChangesToolStripMenuItem.Name = "SaveMiscChangesToolStripMenuItem"
        Me.SaveMiscChangesToolStripMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveMiscChangesToolStripMenuItem.Text = "Save Changes"
        Me.SaveMiscChangesToolStripMenuItem.Visible = False
        '
        'ShowView
        '
        Me.ShowView.Controls.Add(Me.DataGridShowView)
        Me.ShowView.Controls.Add(Me.MenuStripShowView)
        Me.ShowView.Location = New System.Drawing.Point(4, 22)
        Me.ShowView.Name = "ShowView"
        Me.ShowView.Padding = New System.Windows.Forms.Padding(3)
        Me.ShowView.Size = New System.Drawing.Size(970, 424)
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
        Me.DataGridShowView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.StrName, Me.ShowStringName, Me.ShowSelectNum, Me.SelectNumSecond, Me.A1, Me.A2, Me.B1, Me.B2, Me.B3, Me.C1, Me.C2, Me.C3, Me.C4, Me.C5, Me.C6, Me.C7, Me.C8, Me.Stage, Me.D1, Me.D2, Me.D3, Me.D4, Me.D5, Me.Ref, Me.Filter, Me.F1, Me.F2, Me.F3, Me.F4, Me.H1, Me.H2, Me.H3, Me.H4, Me.Bar, Me.Unknown, Me.I1, Me.I2, Me.I3, Me.Live, Me.J})
        Me.DataGridShowView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridShowView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridShowView.Name = "DataGridShowView"
        Me.DataGridShowView.RowHeadersWidth = 60
        Me.DataGridShowView.Size = New System.Drawing.Size(964, 391)
        Me.DataGridShowView.TabIndex = 2
        '
        'MenuStripShowView
        '
        Me.MenuStripShowView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowViewType, Me.StringLoadedShowMenuItem, Me.SaveShowChangesToolStripMenuItem})
        Me.MenuStripShowView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripShowView.Name = "MenuStripShowView"
        Me.MenuStripShowView.Size = New System.Drawing.Size(964, 27)
        Me.MenuStripShowView.TabIndex = 0
        Me.MenuStripShowView.Text = "MenuStrip2"
        '
        'ShowViewType
        '
        Me.ShowViewType.Enabled = False
        Me.ShowViewType.Items.AddRange(New Object() {"2K15", "2K16", "2K17", "2K18", "2K19"})
        Me.ShowViewType.Name = "ShowViewType"
        Me.ShowViewType.Size = New System.Drawing.Size(121, 23)
        '
        'StringLoadedShowMenuItem
        '
        Me.StringLoadedShowMenuItem.Name = "StringLoadedShowMenuItem"
        Me.StringLoadedShowMenuItem.Size = New System.Drawing.Size(95, 23)
        Me.StringLoadedShowMenuItem.Text = "String Loaded:"
        '
        'NIBJView
        '
        Me.NIBJView.Controls.Add(Me.DataGridNIBJView)
        Me.NIBJView.Controls.Add(Me.MenuStripNIBJView)
        Me.NIBJView.Location = New System.Drawing.Point(4, 22)
        Me.NIBJView.Name = "NIBJView"
        Me.NIBJView.Padding = New System.Windows.Forms.Padding(3)
        Me.NIBJView.Size = New System.Drawing.Size(970, 424)
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
        Me.DataGridNIBJView.Size = New System.Drawing.Size(964, 391)
        Me.DataGridNIBJView.TabIndex = 2
        '
        'MenuStripNIBJView
        '
        Me.MenuStripNIBJView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NIBJViewType, Me.FileAttributesToolStripMenuItem})
        Me.MenuStripNIBJView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripNIBJView.Name = "MenuStripNIBJView"
        Me.MenuStripNIBJView.Size = New System.Drawing.Size(964, 27)
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
        Me.PictureView.Size = New System.Drawing.Size(970, 424)
        Me.PictureView.TabIndex = 8
        Me.PictureView.Text = "Picture View"
        Me.PictureView.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(970, 424)
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
        Me.ObjectView.Size = New System.Drawing.Size(970, 424)
        Me.ObjectView.TabIndex = 7
        Me.ObjectView.Text = "Object View"
        Me.ObjectView.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(964, 418)
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
        Me.AttireView.Size = New System.Drawing.Size(970, 424)
        Me.AttireView.TabIndex = 9
        Me.AttireView.Text = "Attire View"
        Me.AttireView.UseVisualStyleBackColor = True
        '
        'DataGridAttireView
        '
        Me.DataGridAttireView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridAttireView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAttireView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Pach, Me.Count, Me.Attire0Ref, Me.Attire0String, Me.Attire0Enabled, Me.Attire0Manager, Me.Attire0Unlock, Me.Attire1Ref, Me.Attire1String, Me.Attire1Enabled, Me.Attire1Manager, Me.Attire1Unlock, Me.Attire2Ref, Me.Attire2String, Me.Attire2Enabled, Me.Attire2Manager, Me.Attire2Unlock, Me.Attire3Ref, Me.Attire3String, Me.Attire3Enabled, Me.Attire3Manager, Me.Attire3Unlock, Me.Attire4Ref, Me.Attire4String, Me.Attire4Enabled, Me.Attire4Manager, Me.Attire4Unlock, Me.Attire5Ref, Me.Attire5String, Me.Attire5Enabled, Me.Attire5Manager, Me.Attire5Unlock, Me.Attire6Ref, Me.Attire6String, Me.Attire6Enabled, Me.Attire6Manager, Me.Attire6Unlock, Me.Attire7Ref, Me.Attire7String, Me.Attire7Enabled, Me.Attire7Manager, Me.Attire7Unlock, Me.Attire8Ref, Me.Attire8String, Me.Attire8Enabled, Me.Attire8Manager, Me.Attire8Unlock, Me.Attire9Ref, Me.Attire9String, Me.Attire9Enabled, Me.Attire9Manager, Me.Attire9Unlock})
        Me.DataGridAttireView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAttireView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridAttireView.Name = "DataGridAttireView"
        Me.DataGridAttireView.Size = New System.Drawing.Size(964, 394)
        Me.DataGridAttireView.TabIndex = 1
        '
        'Pach
        '
        Me.Pach.HeaderText = "Pach"
        Me.Pach.MaxInputLength = 4
        Me.Pach.Name = "Pach"
        Me.Pach.Width = 57
        '
        'Count
        '
        Me.Count.HeaderText = "Count"
        Me.Count.MaxInputLength = 2
        Me.Count.Name = "Count"
        Me.Count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Count.Width = 41
        '
        'Attire0Ref
        '
        Me.Attire0Ref.HeaderText = "Default Attire"
        Me.Attire0Ref.MaxInputLength = 4
        Me.Attire0Ref.Name = "Attire0Ref"
        Me.Attire0Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire0Ref.Width = 74
        '
        'Attire0String
        '
        Me.Attire0String.HeaderText = "Name"
        Me.Attire0String.Name = "Attire0String"
        Me.Attire0String.ReadOnly = True
        Me.Attire0String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire0String.Width = 41
        '
        'Attire0Enabled
        '
        Me.Attire0Enabled.HeaderText = "Enabled"
        Me.Attire0Enabled.Name = "Attire0Enabled"
        Me.Attire0Enabled.Width = 52
        '
        'Attire0Manager
        '
        Me.Attire0Manager.HeaderText = "Manager"
        Me.Attire0Manager.Name = "Attire0Manager"
        Me.Attire0Manager.Width = 55
        '
        'Attire0Unlock
        '
        Me.Attire0Unlock.HeaderText = "Unlock"
        Me.Attire0Unlock.Name = "Attire0Unlock"
        Me.Attire0Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire0Unlock.Visible = False
        Me.Attire0Unlock.Width = 47
        '
        'Attire1Ref
        '
        Me.Attire1Ref.HeaderText = "Attire 1"
        Me.Attire1Ref.MaxInputLength = 4
        Me.Attire1Ref.Name = "Attire1Ref"
        Me.Attire1Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire1Ref.Width = 46
        '
        'Attire1String
        '
        Me.Attire1String.HeaderText = "Name"
        Me.Attire1String.Name = "Attire1String"
        Me.Attire1String.ReadOnly = True
        Me.Attire1String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire1String.Width = 41
        '
        'Attire1Enabled
        '
        Me.Attire1Enabled.HeaderText = "Enabled"
        Me.Attire1Enabled.Name = "Attire1Enabled"
        Me.Attire1Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire1Enabled.Width = 52
        '
        'Attire1Manager
        '
        Me.Attire1Manager.HeaderText = "Manager"
        Me.Attire1Manager.Name = "Attire1Manager"
        Me.Attire1Manager.Width = 55
        '
        'Attire1Unlock
        '
        Me.Attire1Unlock.HeaderText = "Unlock"
        Me.Attire1Unlock.Name = "Attire1Unlock"
        Me.Attire1Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire1Unlock.Visible = False
        Me.Attire1Unlock.Width = 47
        '
        'Attire2Ref
        '
        Me.Attire2Ref.HeaderText = "Attire 2"
        Me.Attire2Ref.MaxInputLength = 4
        Me.Attire2Ref.Name = "Attire2Ref"
        Me.Attire2Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire2Ref.Width = 46
        '
        'Attire2String
        '
        Me.Attire2String.HeaderText = "Name"
        Me.Attire2String.Name = "Attire2String"
        Me.Attire2String.ReadOnly = True
        Me.Attire2String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire2String.Width = 41
        '
        'Attire2Enabled
        '
        Me.Attire2Enabled.HeaderText = "Enabled"
        Me.Attire2Enabled.Name = "Attire2Enabled"
        Me.Attire2Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire2Enabled.Width = 52
        '
        'Attire2Manager
        '
        Me.Attire2Manager.HeaderText = "Manager"
        Me.Attire2Manager.Name = "Attire2Manager"
        Me.Attire2Manager.Width = 55
        '
        'Attire2Unlock
        '
        Me.Attire2Unlock.HeaderText = "Unlock"
        Me.Attire2Unlock.Name = "Attire2Unlock"
        Me.Attire2Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire2Unlock.Visible = False
        Me.Attire2Unlock.Width = 47
        '
        'Attire3Ref
        '
        Me.Attire3Ref.HeaderText = "Attire 3"
        Me.Attire3Ref.MaxInputLength = 4
        Me.Attire3Ref.Name = "Attire3Ref"
        Me.Attire3Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire3Ref.Width = 46
        '
        'Attire3String
        '
        Me.Attire3String.HeaderText = "Name"
        Me.Attire3String.Name = "Attire3String"
        Me.Attire3String.ReadOnly = True
        Me.Attire3String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire3String.Width = 41
        '
        'Attire3Enabled
        '
        Me.Attire3Enabled.HeaderText = "Enabled"
        Me.Attire3Enabled.Name = "Attire3Enabled"
        Me.Attire3Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire3Enabled.Width = 52
        '
        'Attire3Manager
        '
        Me.Attire3Manager.HeaderText = "Manager"
        Me.Attire3Manager.Name = "Attire3Manager"
        Me.Attire3Manager.Width = 55
        '
        'Attire3Unlock
        '
        Me.Attire3Unlock.HeaderText = "Unlock"
        Me.Attire3Unlock.Name = "Attire3Unlock"
        Me.Attire3Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire3Unlock.Visible = False
        Me.Attire3Unlock.Width = 47
        '
        'Attire4Ref
        '
        Me.Attire4Ref.HeaderText = "Attire 4"
        Me.Attire4Ref.MaxInputLength = 4
        Me.Attire4Ref.Name = "Attire4Ref"
        Me.Attire4Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire4Ref.Width = 46
        '
        'Attire4String
        '
        Me.Attire4String.HeaderText = "Name"
        Me.Attire4String.Name = "Attire4String"
        Me.Attire4String.ReadOnly = True
        Me.Attire4String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire4String.Width = 41
        '
        'Attire4Enabled
        '
        Me.Attire4Enabled.HeaderText = "Enabled"
        Me.Attire4Enabled.Name = "Attire4Enabled"
        Me.Attire4Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire4Enabled.Width = 52
        '
        'Attire4Manager
        '
        Me.Attire4Manager.HeaderText = "Manager"
        Me.Attire4Manager.Name = "Attire4Manager"
        Me.Attire4Manager.Width = 55
        '
        'Attire4Unlock
        '
        Me.Attire4Unlock.HeaderText = "Unlock"
        Me.Attire4Unlock.Name = "Attire4Unlock"
        Me.Attire4Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire4Unlock.Visible = False
        Me.Attire4Unlock.Width = 47
        '
        'Attire5Ref
        '
        Me.Attire5Ref.HeaderText = "Attire 5"
        Me.Attire5Ref.MaxInputLength = 4
        Me.Attire5Ref.Name = "Attire5Ref"
        Me.Attire5Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire5Ref.Width = 46
        '
        'Attire5String
        '
        Me.Attire5String.HeaderText = "Name"
        Me.Attire5String.Name = "Attire5String"
        Me.Attire5String.ReadOnly = True
        Me.Attire5String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire5String.Width = 41
        '
        'Attire5Enabled
        '
        Me.Attire5Enabled.HeaderText = "Enabled"
        Me.Attire5Enabled.Name = "Attire5Enabled"
        Me.Attire5Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire5Enabled.Width = 52
        '
        'Attire5Manager
        '
        Me.Attire5Manager.HeaderText = "Manager"
        Me.Attire5Manager.Name = "Attire5Manager"
        Me.Attire5Manager.Width = 55
        '
        'Attire5Unlock
        '
        Me.Attire5Unlock.HeaderText = "Unlock"
        Me.Attire5Unlock.Name = "Attire5Unlock"
        Me.Attire5Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire5Unlock.Visible = False
        Me.Attire5Unlock.Width = 47
        '
        'Attire6Ref
        '
        Me.Attire6Ref.HeaderText = "Attire 6"
        Me.Attire6Ref.MaxInputLength = 4
        Me.Attire6Ref.Name = "Attire6Ref"
        Me.Attire6Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire6Ref.Width = 46
        '
        'Attire6String
        '
        Me.Attire6String.HeaderText = "Name"
        Me.Attire6String.Name = "Attire6String"
        Me.Attire6String.ReadOnly = True
        Me.Attire6String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire6String.Width = 41
        '
        'Attire6Enabled
        '
        Me.Attire6Enabled.HeaderText = "Enabled"
        Me.Attire6Enabled.Name = "Attire6Enabled"
        Me.Attire6Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire6Enabled.Width = 52
        '
        'Attire6Manager
        '
        Me.Attire6Manager.HeaderText = "Manager"
        Me.Attire6Manager.Name = "Attire6Manager"
        Me.Attire6Manager.Width = 55
        '
        'Attire6Unlock
        '
        Me.Attire6Unlock.HeaderText = "Unlock"
        Me.Attire6Unlock.Name = "Attire6Unlock"
        Me.Attire6Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire6Unlock.Visible = False
        Me.Attire6Unlock.Width = 47
        '
        'Attire7Ref
        '
        Me.Attire7Ref.HeaderText = "Attire 7"
        Me.Attire7Ref.MaxInputLength = 4
        Me.Attire7Ref.Name = "Attire7Ref"
        Me.Attire7Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire7Ref.Width = 46
        '
        'Attire7String
        '
        Me.Attire7String.HeaderText = "Name"
        Me.Attire7String.Name = "Attire7String"
        Me.Attire7String.ReadOnly = True
        Me.Attire7String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire7String.Width = 41
        '
        'Attire7Enabled
        '
        Me.Attire7Enabled.HeaderText = "Enabled"
        Me.Attire7Enabled.Name = "Attire7Enabled"
        Me.Attire7Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire7Enabled.Width = 52
        '
        'Attire7Manager
        '
        Me.Attire7Manager.HeaderText = "Manager"
        Me.Attire7Manager.Name = "Attire7Manager"
        Me.Attire7Manager.Width = 55
        '
        'Attire7Unlock
        '
        Me.Attire7Unlock.HeaderText = "Unlock"
        Me.Attire7Unlock.Name = "Attire7Unlock"
        Me.Attire7Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire7Unlock.Visible = False
        Me.Attire7Unlock.Width = 47
        '
        'Attire8Ref
        '
        Me.Attire8Ref.HeaderText = "Attire 8"
        Me.Attire8Ref.MaxInputLength = 4
        Me.Attire8Ref.Name = "Attire8Ref"
        Me.Attire8Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire8Ref.Width = 46
        '
        'Attire8String
        '
        Me.Attire8String.HeaderText = "Name"
        Me.Attire8String.Name = "Attire8String"
        Me.Attire8String.ReadOnly = True
        Me.Attire8String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire8String.Width = 41
        '
        'Attire8Enabled
        '
        Me.Attire8Enabled.HeaderText = "Enabled"
        Me.Attire8Enabled.Name = "Attire8Enabled"
        Me.Attire8Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire8Enabled.Width = 52
        '
        'Attire8Manager
        '
        Me.Attire8Manager.HeaderText = "Manager"
        Me.Attire8Manager.Name = "Attire8Manager"
        Me.Attire8Manager.Width = 55
        '
        'Attire8Unlock
        '
        Me.Attire8Unlock.HeaderText = "Unlock"
        Me.Attire8Unlock.Name = "Attire8Unlock"
        Me.Attire8Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire8Unlock.Visible = False
        Me.Attire8Unlock.Width = 47
        '
        'Attire9Ref
        '
        Me.Attire9Ref.HeaderText = "Attire 9"
        Me.Attire9Ref.MaxInputLength = 4
        Me.Attire9Ref.Name = "Attire9Ref"
        Me.Attire9Ref.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire9Ref.Width = 46
        '
        'Attire9String
        '
        Me.Attire9String.HeaderText = "Name"
        Me.Attire9String.Name = "Attire9String"
        Me.Attire9String.ReadOnly = True
        Me.Attire9String.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire9String.Width = 41
        '
        'Attire9Enabled
        '
        Me.Attire9Enabled.HeaderText = "Enabled"
        Me.Attire9Enabled.Name = "Attire9Enabled"
        Me.Attire9Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Attire9Enabled.Width = 52
        '
        'Attire9Manager
        '
        Me.Attire9Manager.HeaderText = "Manager"
        Me.Attire9Manager.Name = "Attire9Manager"
        Me.Attire9Manager.Width = 55
        '
        'Attire9Unlock
        '
        Me.Attire9Unlock.HeaderText = "Unlock"
        Me.Attire9Unlock.Name = "Attire9Unlock"
        Me.Attire9Unlock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Attire9Unlock.Visible = False
        Me.Attire9Unlock.Width = 47
        '
        'MenuStripAttireView
        '
        Me.MenuStripAttireView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StringLoadedAttireMenuItem, Me.PacsLoadedAttireMenuItem, Me.SaveChangesAttireMenuItem})
        Me.MenuStripAttireView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripAttireView.Name = "MenuStripAttireView"
        Me.MenuStripAttireView.Size = New System.Drawing.Size(964, 24)
        Me.MenuStripAttireView.TabIndex = 0
        Me.MenuStripAttireView.Text = "MenuStrip2"
        '
        'StringLoadedAttireMenuItem
        '
        Me.StringLoadedAttireMenuItem.Enabled = False
        Me.StringLoadedAttireMenuItem.Name = "StringLoadedAttireMenuItem"
        Me.StringLoadedAttireMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.StringLoadedAttireMenuItem.Text = "String Loaded:"
        '
        'PacsLoadedAttireMenuItem
        '
        Me.PacsLoadedAttireMenuItem.Enabled = False
        Me.PacsLoadedAttireMenuItem.Name = "PacsLoadedAttireMenuItem"
        Me.PacsLoadedAttireMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.PacsLoadedAttireMenuItem.Text = "Pacs Loaded:"
        '
        'SaveChangesAttireMenuItem
        '
        Me.SaveChangesAttireMenuItem.Name = "SaveChangesAttireMenuItem"
        Me.SaveChangesAttireMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesAttireMenuItem.Text = "Save Changes"
        Me.SaveChangesAttireMenuItem.Visible = False
        '
        'MuscleView
        '
        Me.MuscleView.Controls.Add(Me.DataGridMuscleView)
        Me.MuscleView.Controls.Add(Me.MenuStripMuscleView)
        Me.MuscleView.Location = New System.Drawing.Point(4, 22)
        Me.MuscleView.Name = "MuscleView"
        Me.MuscleView.Padding = New System.Windows.Forms.Padding(3)
        Me.MuscleView.Size = New System.Drawing.Size(970, 424)
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
        Me.DataGridMuscleView.Size = New System.Drawing.Size(964, 394)
        Me.DataGridMuscleView.TabIndex = 2
        '
        'MenuStripMuscleView
        '
        Me.MenuStripMuscleView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.MenuStripMuscleView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMuscleView.Name = "MenuStripMuscleView"
        Me.MenuStripMuscleView.Size = New System.Drawing.Size(964, 24)
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
        Me.MaskView.Size = New System.Drawing.Size(970, 424)
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
        Me.DataGridMaskView.Size = New System.Drawing.Size(964, 394)
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
        Me.MenuStripMaskView.Size = New System.Drawing.Size(964, 24)
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
        Me.ObjArrayView.Size = New System.Drawing.Size(970, 424)
        Me.ObjArrayView.TabIndex = 12
        Me.ObjArrayView.Text = "Object Array View"
        Me.ObjArrayView.UseVisualStyleBackColor = True
        '
        'DataGridObjArrayView
        '
        Me.DataGridObjArrayView.AllowUserToAddRows = False
        Me.DataGridObjArrayView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridObjArrayView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjArrayView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ArrEnabled, Me.ChairName})
        Me.DataGridObjArrayView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjArrayView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridObjArrayView.Name = "DataGridObjArrayView"
        Me.DataGridObjArrayView.Size = New System.Drawing.Size(964, 394)
        Me.DataGridObjArrayView.TabIndex = 1
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
        'MenuStrip2
        '
        Me.MenuStrip2.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip2.Name = "MenuStrip2"
        Me.MenuStrip2.Size = New System.Drawing.Size(964, 24)
        Me.MenuStrip2.TabIndex = 0
        Me.MenuStrip2.Text = "MenuStrip2"
        '
        'AssetView
        '
        Me.AssetView.Controls.Add(Me.DataGridAssetView)
        Me.AssetView.Location = New System.Drawing.Point(4, 22)
        Me.AssetView.Name = "AssetView"
        Me.AssetView.Padding = New System.Windows.Forms.Padding(3)
        Me.AssetView.Size = New System.Drawing.Size(970, 424)
        Me.AssetView.TabIndex = 13
        Me.AssetView.Text = "Asset View"
        Me.AssetView.UseVisualStyleBackColor = True
        '
        'DataGridAssetView
        '
        Me.DataGridAssetView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridAssetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAssetView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PacNumber, Me.AttireNum, Me.AudioNum, Me.Check2, Me.Check3, Me.FileOffset, Me.TitantronNum, Me.MiniNum, Me.HeaderNum, Me.WallNum, Me.RampNum, Me.WallRightNum, Me.WallLeftNum, Me.Check4, Me.Check5, Me.Check6, Me.FileName})
        Me.DataGridAssetView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAssetView.Location = New System.Drawing.Point(3, 3)
        Me.DataGridAssetView.Name = "DataGridAssetView"
        Me.DataGridAssetView.Size = New System.Drawing.Size(964, 418)
        Me.DataGridAssetView.TabIndex = 0
        '
        'PacNumber
        '
        Me.PacNumber.HeaderText = "PacNumber"
        Me.PacNumber.Name = "PacNumber"
        Me.PacNumber.Width = 88
        '
        'AttireNum
        '
        Me.AttireNum.HeaderText = "AttireNum"
        Me.AttireNum.Name = "AttireNum"
        Me.AttireNum.Width = 78
        '
        'AudioNum
        '
        Me.AudioNum.HeaderText = "AudioNum"
        Me.AudioNum.Name = "AudioNum"
        Me.AudioNum.Width = 81
        '
        'Check2
        '
        Me.Check2.HeaderText = "Check2"
        Me.Check2.Name = "Check2"
        Me.Check2.Width = 69
        '
        'Check3
        '
        Me.Check3.HeaderText = "Check3"
        Me.Check3.Name = "Check3"
        Me.Check3.Width = 69
        '
        'FileOffset
        '
        Me.FileOffset.HeaderText = "FileOffset"
        Me.FileOffset.Name = "FileOffset"
        Me.FileOffset.Width = 76
        '
        'TitantronNum
        '
        Me.TitantronNum.HeaderText = "TitantronNum"
        Me.TitantronNum.Name = "TitantronNum"
        Me.TitantronNum.Width = 96
        '
        'MiniNum
        '
        Me.MiniNum.HeaderText = "MiniNum"
        Me.MiniNum.Name = "MiniNum"
        Me.MiniNum.Width = 73
        '
        'HeaderNum
        '
        Me.HeaderNum.HeaderText = "HeaderNum"
        Me.HeaderNum.Name = "HeaderNum"
        Me.HeaderNum.Width = 89
        '
        'WallNum
        '
        Me.WallNum.HeaderText = "WallNum"
        Me.WallNum.Name = "WallNum"
        Me.WallNum.Width = 75
        '
        'RampNum
        '
        Me.RampNum.HeaderText = "RampNum"
        Me.RampNum.Name = "RampNum"
        Me.RampNum.Width = 82
        '
        'WallRightNum
        '
        Me.WallRightNum.HeaderText = "WallRightNum"
        Me.WallRightNum.Name = "WallRightNum"
        '
        'WallLeftNum
        '
        Me.WallLeftNum.HeaderText = "WallLeftNum"
        Me.WallLeftNum.Name = "WallLeftNum"
        Me.WallLeftNum.Width = 93
        '
        'Check4
        '
        Me.Check4.HeaderText = "Check4"
        Me.Check4.Name = "Check4"
        Me.Check4.Width = 69
        '
        'Check5
        '
        Me.Check5.HeaderText = "Check5"
        Me.Check5.Name = "Check5"
        Me.Check5.Width = 69
        '
        'Check6
        '
        Me.Check6.HeaderText = "Check6"
        Me.Check6.Name = "Check6"
        Me.Check6.Width = 69
        '
        'FileName
        '
        Me.FileName.HeaderText = "FileName"
        Me.FileName.Name = "FileName"
        Me.FileName.Width = 76
        '
        'TitleView
        '
        Me.TitleView.Controls.Add(Me.DataGridTitleView)
        Me.TitleView.Controls.Add(Me.MenuStrip3)
        Me.TitleView.Location = New System.Drawing.Point(4, 22)
        Me.TitleView.Name = "TitleView"
        Me.TitleView.Padding = New System.Windows.Forms.Padding(3)
        Me.TitleView.Size = New System.Drawing.Size(970, 424)
        Me.TitleView.TabIndex = 14
        Me.TitleView.Text = "Title View"
        Me.TitleView.UseVisualStyleBackColor = True
        '
        'DataGridTitleView
        '
        Me.DataGridTitleView.AllowUserToAddRows = False
        Me.DataGridTitleView.AllowUserToDeleteRows = False
        Me.DataGridTitleView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridTitleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridTitleView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TitleEnabled, Me.PropRef, Me.MenuNumber, Me.Name1, Me.Name1Full, Me.Name2, Me.Name2Full, Me.Name3, Me.Name3Full, Me.MyWWE1, Me.MyWWE2, Me.Uni1, Me.Uni2, Me.Temp1, Me.Temp2, Me.Female, Me.TagTeam, Me.Cruiserweight, Me.UnlockNum, Me.Temp4})
        Me.DataGridTitleView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridTitleView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridTitleView.Name = "DataGridTitleView"
        Me.DataGridTitleView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridTitleView.Size = New System.Drawing.Size(964, 391)
        Me.DataGridTitleView.TabIndex = 1
        '
        'TitleEnabled
        '
        Me.TitleEnabled.HeaderText = "Enabled"
        Me.TitleEnabled.MaxInputLength = 1
        Me.TitleEnabled.Name = "TitleEnabled"
        Me.TitleEnabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleEnabled.Width = 52
        '
        'PropRef
        '
        Me.PropRef.HeaderText = "PropRef"
        Me.PropRef.MaxInputLength = 4
        Me.PropRef.Name = "PropRef"
        Me.PropRef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PropRef.Width = 52
        '
        'MenuNumber
        '
        Me.MenuNumber.HeaderText = "MenuNum"
        Me.MenuNumber.Name = "MenuNumber"
        Me.MenuNumber.Width = 81
        '
        'Name1
        '
        Me.Name1.HeaderText = "Name1"
        Me.Name1.MaxInputLength = 8
        Me.Name1.Name = "Name1"
        Me.Name1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Name1.Width = 47
        '
        'Name1Full
        '
        Me.Name1Full.HeaderText = "Name1Full"
        Me.Name1Full.Name = "Name1Full"
        Me.Name1Full.ReadOnly = True
        Me.Name1Full.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Name1Full.Width = 63
        '
        'Name2
        '
        Me.Name2.HeaderText = "Name2"
        Me.Name2.MaxInputLength = 8
        Me.Name2.Name = "Name2"
        Me.Name2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Name2.Width = 47
        '
        'Name2Full
        '
        Me.Name2Full.HeaderText = "Name2Full"
        Me.Name2Full.Name = "Name2Full"
        Me.Name2Full.ReadOnly = True
        Me.Name2Full.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Name2Full.Width = 63
        '
        'Name3
        '
        Me.Name3.HeaderText = "Name3"
        Me.Name3.MaxInputLength = 8
        Me.Name3.Name = "Name3"
        Me.Name3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Name3.Width = 47
        '
        'Name3Full
        '
        Me.Name3Full.HeaderText = "Name3Full"
        Me.Name3Full.Name = "Name3Full"
        Me.Name3Full.ReadOnly = True
        Me.Name3Full.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Name3Full.Width = 63
        '
        'MyWWE1
        '
        Me.MyWWE1.HeaderText = "MyWWE1"
        Me.MyWWE1.MaxInputLength = 4
        Me.MyWWE1.Name = "MyWWE1"
        Me.MyWWE1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MyWWE1.Width = 62
        '
        'MyWWE2
        '
        Me.MyWWE2.HeaderText = "MyWWE2"
        Me.MyWWE2.MaxInputLength = 4
        Me.MyWWE2.Name = "MyWWE2"
        Me.MyWWE2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MyWWE2.Width = 62
        '
        'Uni1
        '
        Me.Uni1.HeaderText = "Uni1"
        Me.Uni1.MaxInputLength = 4
        Me.Uni1.Name = "Uni1"
        Me.Uni1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Uni1.Width = 35
        '
        'Uni2
        '
        Me.Uni2.HeaderText = "Uni2"
        Me.Uni2.MaxInputLength = 4
        Me.Uni2.Name = "Uni2"
        Me.Uni2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Uni2.Width = 35
        '
        'Temp1
        '
        Me.Temp1.HeaderText = "Temp1"
        Me.Temp1.MaxInputLength = 8
        Me.Temp1.Name = "Temp1"
        Me.Temp1.Width = 65
        '
        'Temp2
        '
        Me.Temp2.HeaderText = "Temp2"
        Me.Temp2.MaxInputLength = 8
        Me.Temp2.Name = "Temp2"
        Me.Temp2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Temp2.Width = 46
        '
        'Female
        '
        Me.Female.HeaderText = "Female"
        Me.Female.Name = "Female"
        Me.Female.Width = 47
        '
        'TagTeam
        '
        Me.TagTeam.HeaderText = "TagTeam"
        Me.TagTeam.Name = "TagTeam"
        Me.TagTeam.Width = 59
        '
        'Cruiserweight
        '
        Me.Cruiserweight.HeaderText = "Cruiser"
        Me.Cruiserweight.Name = "Cruiserweight"
        Me.Cruiserweight.Width = 45
        '
        'UnlockNum
        '
        Me.UnlockNum.HeaderText = "UnlockNum"
        Me.UnlockNum.MaxInputLength = 8
        Me.UnlockNum.Name = "UnlockNum"
        Me.UnlockNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UnlockNum.Width = 69
        '
        'Temp4
        '
        Me.Temp4.HeaderText = "Temp4"
        Me.Temp4.MaxInputLength = 8
        Me.Temp4.Name = "Temp4"
        Me.Temp4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Temp4.Width = 46
        '
        'MenuStrip3
        '
        Me.MenuStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TitleGameComboBox, Me.StringLoadedTitleMenuItem, Me.PacsLoadedTitleMenuItem, Me.SaveChangesTitleMenuItem})
        Me.MenuStrip3.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip3.Name = "MenuStrip3"
        Me.MenuStrip3.Size = New System.Drawing.Size(964, 27)
        Me.MenuStrip3.TabIndex = 0
        Me.MenuStrip3.Text = "MenuStrip3"
        '
        'TitleGameComboBox
        '
        Me.TitleGameComboBox.Enabled = False
        Me.TitleGameComboBox.Items.AddRange(New Object() {"2K15", "2K16", "2K17", "2K18", "2K19"})
        Me.TitleGameComboBox.Name = "TitleGameComboBox"
        Me.TitleGameComboBox.Size = New System.Drawing.Size(121, 23)
        '
        'StringLoadedTitleMenuItem
        '
        Me.StringLoadedTitleMenuItem.Enabled = False
        Me.StringLoadedTitleMenuItem.Name = "StringLoadedTitleMenuItem"
        Me.StringLoadedTitleMenuItem.Size = New System.Drawing.Size(95, 23)
        Me.StringLoadedTitleMenuItem.Text = "String Loaded:"
        '
        'PacsLoadedTitleMenuItem
        '
        Me.PacsLoadedTitleMenuItem.Enabled = False
        Me.PacsLoadedTitleMenuItem.Name = "PacsLoadedTitleMenuItem"
        Me.PacsLoadedTitleMenuItem.Size = New System.Drawing.Size(88, 23)
        Me.PacsLoadedTitleMenuItem.Text = "Pacs Loaded:"
        '
        'SaveChangesTitleMenuItem
        '
        Me.SaveChangesTitleMenuItem.Name = "SaveChangesTitleMenuItem"
        Me.SaveChangesTitleMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveChangesTitleMenuItem.Text = "Save Changes"
        Me.SaveChangesTitleMenuItem.Visible = False
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
        Me.OpenFileDialog1.FileName = "*.*"
        Me.OpenFileDialog1.Multiselect = True
        '
        'TreeViewContext
        '
        Me.TreeViewContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem1, Me.OpenWithToolStripMenuItem, Me.ExtractToolStripMenuItem, Me.ExtractAllInPlaceToolStripMenuItem, Me.ExtractAllToToolStripMenuItem, Me.InjectToolStripMenuItem, Me.CrawlToolStripMenuItem})
        Me.TreeViewContext.Name = "TreeViewContext"
        Me.TreeViewContext.Size = New System.Drawing.Size(171, 158)
        '
        'OpenToolStripMenuItem1
        '
        Me.OpenToolStripMenuItem1.Name = "OpenToolStripMenuItem1"
        Me.OpenToolStripMenuItem1.Size = New System.Drawing.Size(170, 22)
        Me.OpenToolStripMenuItem1.Text = "Open"
        '
        'OpenWithToolStripMenuItem
        '
        Me.OpenWithToolStripMenuItem.Name = "OpenWithToolStripMenuItem"
        Me.OpenWithToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.OpenWithToolStripMenuItem.Text = "Open With..."
        '
        'ExtractToolStripMenuItem
        '
        Me.ExtractToolStripMenuItem.Name = "ExtractToolStripMenuItem"
        Me.ExtractToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ExtractToolStripMenuItem.Text = "Extract"
        '
        'ExtractAllInPlaceToolStripMenuItem
        '
        Me.ExtractAllInPlaceToolStripMenuItem.Name = "ExtractAllInPlaceToolStripMenuItem"
        Me.ExtractAllInPlaceToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ExtractAllInPlaceToolStripMenuItem.Text = "Extract All In Place"
        '
        'ExtractAllToToolStripMenuItem
        '
        Me.ExtractAllToToolStripMenuItem.Name = "ExtractAllToToolStripMenuItem"
        Me.ExtractAllToToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.ExtractAllToToolStripMenuItem.Text = "Extract All To..."
        '
        'InjectToolStripMenuItem
        '
        Me.InjectToolStripMenuItem.Name = "InjectToolStripMenuItem"
        Me.InjectToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.InjectToolStripMenuItem.Text = "Inject"
        '
        'CrawlToolStripMenuItem
        '
        Me.CrawlToolStripMenuItem.Name = "CrawlToolStripMenuItem"
        Me.CrawlToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
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
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn35.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn35.Width = 41
        '
        'SaveExtractAllDialog
        '
        Me.SaveExtractAllDialog.FileName = "Save Files Here"
        '
        'SaveShowChangesToolStripMenuItem
        '
        Me.SaveShowChangesToolStripMenuItem.Name = "SaveShowChangesToolStripMenuItem"
        Me.SaveShowChangesToolStripMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveShowChangesToolStripMenuItem.Text = "Save Changes"
        Me.SaveShowChangesToolStripMenuItem.Visible = False
        '
        'StrName
        '
        Me.StrName.HeaderText = "StrName"
        Me.StrName.MaxInputLength = 8
        Me.StrName.Name = "StrName"
        Me.StrName.Width = 73
        '
        'ShowStringName
        '
        Me.ShowStringName.HeaderText = "Name"
        Me.ShowStringName.Name = "ShowStringName"
        Me.ShowStringName.ReadOnly = True
        Me.ShowStringName.Width = 60
        '
        'ShowSelectNum
        '
        Me.ShowSelectNum.HeaderText = "SelectNum"
        Me.ShowSelectNum.MaxInputLength = 3
        Me.ShowSelectNum.Name = "ShowSelectNum"
        Me.ShowSelectNum.Width = 84
        '
        'SelectNumSecond
        '
        Me.SelectNumSecond.HeaderText = "SelectNum"
        Me.SelectNumSecond.MaxInputLength = 3
        Me.SelectNumSecond.Name = "SelectNumSecond"
        Me.SelectNumSecond.Width = 84
        '
        'A1
        '
        Me.A1.HeaderText = "A1"
        Me.A1.MaxInputLength = 3
        Me.A1.Name = "A1"
        Me.A1.Width = 45
        '
        'A2
        '
        Me.A2.HeaderText = "A2"
        Me.A2.MaxInputLength = 3
        Me.A2.Name = "A2"
        Me.A2.Width = 45
        '
        'B1
        '
        Me.B1.HeaderText = "B1"
        Me.B1.MaxInputLength = 4
        Me.B1.Name = "B1"
        Me.B1.Width = 45
        '
        'B2
        '
        Me.B2.HeaderText = "B2"
        Me.B2.MaxInputLength = 4
        Me.B2.Name = "B2"
        Me.B2.Width = 45
        '
        'B3
        '
        Me.B3.HeaderText = "B3"
        Me.B3.MaxInputLength = 4
        Me.B3.Name = "B3"
        Me.B3.Width = 45
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
        'C6
        '
        Me.C6.HeaderText = "C6"
        Me.C6.Name = "C6"
        Me.C6.Width = 26
        '
        'C7
        '
        Me.C7.HeaderText = "C7"
        Me.C7.Name = "C7"
        Me.C7.Width = 26
        '
        'C8
        '
        Me.C8.HeaderText = "C8"
        Me.C8.Name = "C8"
        Me.C8.Width = 26
        '
        'Stage
        '
        Me.Stage.HeaderText = "Stage"
        Me.Stage.MaxInputLength = 1
        Me.Stage.Name = "Stage"
        Me.Stage.Width = 60
        '
        'D1
        '
        Me.D1.HeaderText = "D1"
        Me.D1.MaxInputLength = 1
        Me.D1.Name = "D1"
        Me.D1.Width = 46
        '
        'D2
        '
        Me.D2.HeaderText = "D2"
        Me.D2.MaxInputLength = 1
        Me.D2.Name = "D2"
        Me.D2.Width = 46
        '
        'D3
        '
        Me.D3.HeaderText = "D3"
        Me.D3.MaxInputLength = 1
        Me.D3.Name = "D3"
        Me.D3.Width = 46
        '
        'D4
        '
        Me.D4.HeaderText = "D4"
        Me.D4.MaxInputLength = 1
        Me.D4.Name = "D4"
        Me.D4.Width = 46
        '
        'D5
        '
        Me.D5.HeaderText = "D5"
        Me.D5.MaxInputLength = 1
        Me.D5.Name = "D5"
        Me.D5.Width = 46
        '
        'Ref
        '
        Me.Ref.HeaderText = "Ref"
        Me.Ref.MaxInputLength = 1
        Me.Ref.Name = "Ref"
        Me.Ref.Width = 49
        '
        'Filter
        '
        Me.Filter.HeaderText = "Filter"
        Me.Filter.MaxInputLength = 12
        Me.Filter.Name = "Filter"
        Me.Filter.Width = 54
        '
        'F1
        '
        Me.F1.HeaderText = "F1"
        Me.F1.MaxInputLength = 8
        Me.F1.Name = "F1"
        Me.F1.Width = 44
        '
        'F2
        '
        Me.F2.HeaderText = "F2"
        Me.F2.MaxInputLength = 2
        Me.F2.Name = "F2"
        Me.F2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.F2.Width = 44
        '
        'F3
        '
        Me.F3.HeaderText = "F3"
        Me.F3.MaxInputLength = 1
        Me.F3.Name = "F3"
        Me.F3.Width = 44
        '
        'F4
        '
        Me.F4.HeaderText = "F4"
        Me.F4.MaxInputLength = 1
        Me.F4.Name = "F4"
        Me.F4.Width = 44
        '
        'H1
        '
        Me.H1.HeaderText = "H1"
        Me.H1.MaxInputLength = 1
        Me.H1.Name = "H1"
        Me.H1.Width = 46
        '
        'H2
        '
        Me.H2.HeaderText = "H2"
        Me.H2.MaxInputLength = 1
        Me.H2.Name = "H2"
        Me.H2.Width = 46
        '
        'H3
        '
        Me.H3.HeaderText = "H3"
        Me.H3.MaxInputLength = 1
        Me.H3.Name = "H3"
        Me.H3.Width = 46
        '
        'H4
        '
        Me.H4.HeaderText = "H4"
        Me.H4.MaxInputLength = 1
        Me.H4.Name = "H4"
        Me.H4.Width = 46
        '
        'Bar
        '
        Me.Bar.HeaderText = "Bar"
        Me.Bar.MaxInputLength = 1
        Me.Bar.Name = "Bar"
        Me.Bar.Width = 48
        '
        'Unknown
        '
        Me.Unknown.HeaderText = "Unkown"
        Me.Unknown.MaxInputLength = 68
        Me.Unknown.Name = "Unknown"
        Me.Unknown.Width = 72
        '
        'I1
        '
        Me.I1.HeaderText = "I1"
        Me.I1.MaxInputLength = 1
        Me.I1.Name = "I1"
        Me.I1.Width = 41
        '
        'I2
        '
        Me.I2.HeaderText = "I2"
        Me.I2.MaxInputLength = 1
        Me.I2.Name = "I2"
        Me.I2.Width = 41
        '
        'I3
        '
        Me.I3.HeaderText = "I3"
        Me.I3.MaxInputLength = 2
        Me.I3.Name = "I3"
        Me.I3.Width = 41
        '
        'Live
        '
        Me.Live.HeaderText = "Live"
        Me.Live.MaxInputLength = 1
        Me.Live.Name = "Live"
        Me.Live.Width = 52
        '
        'J
        '
        Me.J.HeaderText = "J"
        Me.J.MaxInputLength = 1
        Me.J.Name = "J"
        Me.J.Width = 37
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainForm"
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
        Me.AssetView.ResumeLayout(False)
        CType(Me.DataGridAssetView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TitleView.ResumeLayout(False)
        Me.TitleView.PerformLayout()
        CType(Me.DataGridTitleView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip3.ResumeLayout(False)
        Me.MenuStrip3.PerformLayout()
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
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
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
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents MenuStripHexView As MenuStrip
    Friend WithEvents HexViewBitWidth As ToolStripComboBox
    Friend WithEvents HexViewFileName As ToolStripMenuItem
    Friend WithEvents MenuStripTextView As MenuStrip
    Friend WithEvents TextViewBitWidth As ToolStripComboBox
    Friend WithEvents TextViewFileName As ToolStripMenuItem
    Friend WithEvents StringView As TabPage
    Friend WithEvents DataGridStringView As DataGridView
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
    Friend WithEvents OpenToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ObjectView As TabPage
    Friend WithEvents PictureView As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents AttireView As TabPage
    Friend WithEvents DataGridAttireView As DataGridView
    Friend WithEvents MenuStripAttireView As MenuStrip
    Friend WithEvents StringLoadedAttireMenuItem As ToolStripMenuItem
    Friend WithEvents PacsLoadedAttireMenuItem As ToolStripMenuItem
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
    Friend WithEvents ExtractAllInPlaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtractAllToToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveExtractAllDialog As SaveFileDialog
    Friend WithEvents AssetView As TabPage
    Friend WithEvents DataGridAssetView As DataGridView
    Friend WithEvents PacNumber As DataGridViewTextBoxColumn
    Friend WithEvents AttireNum As DataGridViewTextBoxColumn
    Friend WithEvents AudioNum As DataGridViewTextBoxColumn
    Friend WithEvents Check2 As DataGridViewTextBoxColumn
    Friend WithEvents Check3 As DataGridViewTextBoxColumn
    Friend WithEvents FileOffset As DataGridViewTextBoxColumn
    Friend WithEvents TitantronNum As DataGridViewTextBoxColumn
    Friend WithEvents MiniNum As DataGridViewTextBoxColumn
    Friend WithEvents HeaderNum As DataGridViewTextBoxColumn
    Friend WithEvents WallNum As DataGridViewTextBoxColumn
    Friend WithEvents RampNum As DataGridViewTextBoxColumn
    Friend WithEvents WallRightNum As DataGridViewTextBoxColumn
    Friend WithEvents WallLeftNum As DataGridViewTextBoxColumn
    Friend WithEvents Check4 As DataGridViewTextBoxColumn
    Friend WithEvents Check5 As DataGridViewTextBoxColumn
    Friend WithEvents Check6 As DataGridViewTextBoxColumn
    Friend WithEvents FileName As DataGridViewTextBoxColumn
    Friend WithEvents SaveStringChangesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBoxSearch As ToolStripTextBox
    Friend WithEvents SaveMiscChangesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesAttireMenuItem As ToolStripMenuItem
    Friend WithEvents Pach As DataGridViewTextBoxColumn
    Friend WithEvents Count As DataGridViewTextBoxColumn
    Friend WithEvents Attire0Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire0String As DataGridViewTextBoxColumn
    Friend WithEvents Attire0Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire0Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire0Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire1Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire1String As DataGridViewTextBoxColumn
    Friend WithEvents Attire1Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire1Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire1Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire2Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire2String As DataGridViewTextBoxColumn
    Friend WithEvents Attire2Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire2Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire2Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire3Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire3String As DataGridViewTextBoxColumn
    Friend WithEvents Attire3Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire3Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire3Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire4Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire4String As DataGridViewTextBoxColumn
    Friend WithEvents Attire4Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire4Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire4Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire5Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire5String As DataGridViewTextBoxColumn
    Friend WithEvents Attire5Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire5Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire5Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire6Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire6String As DataGridViewTextBoxColumn
    Friend WithEvents Attire6Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire6Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire6Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire7Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire7String As DataGridViewTextBoxColumn
    Friend WithEvents Attire7Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire7Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire7Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire8Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire8String As DataGridViewTextBoxColumn
    Friend WithEvents Attire8Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire8Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire8Unlock As DataGridViewTextBoxColumn
    Friend WithEvents Attire9Ref As DataGridViewTextBoxColumn
    Friend WithEvents Attire9String As DataGridViewTextBoxColumn
    Friend WithEvents Attire9Enabled As DataGridViewCheckBoxColumn
    Friend WithEvents Attire9Manager As DataGridViewCheckBoxColumn
    Friend WithEvents Attire9Unlock As DataGridViewTextBoxColumn
    Friend WithEvents TitleView As TabPage
    Friend WithEvents DataGridTitleView As DataGridView
    Friend WithEvents MenuStrip3 As MenuStrip
    Friend WithEvents StringLoadedTitleMenuItem As ToolStripMenuItem
    Friend WithEvents PacsLoadedTitleMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesTitleMenuItem As ToolStripMenuItem
    Friend WithEvents TitleGameComboBox As ToolStripComboBox
    Friend WithEvents TitleEnabled As DataGridViewTextBoxColumn
    Friend WithEvents PropRef As DataGridViewTextBoxColumn
    Friend WithEvents MenuNumber As DataGridViewTextBoxColumn
    Friend WithEvents Name1 As DataGridViewTextBoxColumn
    Friend WithEvents Name1Full As DataGridViewTextBoxColumn
    Friend WithEvents Name2 As DataGridViewTextBoxColumn
    Friend WithEvents Name2Full As DataGridViewTextBoxColumn
    Friend WithEvents Name3 As DataGridViewTextBoxColumn
    Friend WithEvents Name3Full As DataGridViewTextBoxColumn
    Friend WithEvents MyWWE1 As DataGridViewTextBoxColumn
    Friend WithEvents MyWWE2 As DataGridViewTextBoxColumn
    Friend WithEvents Uni1 As DataGridViewTextBoxColumn
    Friend WithEvents Uni2 As DataGridViewTextBoxColumn
    Friend WithEvents Temp1 As DataGridViewTextBoxColumn
    Friend WithEvents Temp2 As DataGridViewTextBoxColumn
    Friend WithEvents Female As DataGridViewCheckBoxColumn
    Friend WithEvents TagTeam As DataGridViewCheckBoxColumn
    Friend WithEvents Cruiserweight As DataGridViewCheckBoxColumn
    Friend WithEvents UnlockNum As DataGridViewTextBoxColumn
    Friend WithEvents Temp4 As DataGridViewTextBoxColumn
    Friend WithEvents SupportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GitHubIssuesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents OpenWithToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Hex_Selected As RichTextBox
    Friend WithEvents Text_Selected As RichTextBox
    Friend WithEvents SortStringsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HexRef As DataGridViewTextBoxColumn
    Friend WithEvents StringText As DataGridViewTextBoxColumn
    Friend WithEvents Length As DataGridViewTextBoxColumn
    Friend WithEvents AddString As DataGridViewButtonColumn
    Friend WithEvents DeleteString As DataGridViewButtonColumn
    Friend WithEvents Col_ArenaNumber As DataGridViewTextBoxColumn
    Friend WithEvents Col_Stadium As DataGridViewTextBoxColumn
    Friend WithEvents Col_Advertisement As DataGridViewTextBoxColumn
    Friend WithEvents Col_CornerPost As DataGridViewTextBoxColumn
    Friend WithEvents Col_LED_CornerPost As DataGridViewTextBoxColumn
    Friend WithEvents Col_Rope As DataGridViewTextBoxColumn
    Friend WithEvents Col_Apron As DataGridViewTextBoxColumn
    Friend WithEvents Col_LED_Apron As DataGridViewTextBoxColumn
    Friend WithEvents Col_Turnbuckle As DataGridViewTextBoxColumn
    Friend WithEvents Col_Barricade As DataGridViewTextBoxColumn
    Friend WithEvents Col_Fence As DataGridViewTextBoxColumn
    Friend WithEvents Col_CeilingLighting As DataGridViewTextBoxColumn
    Friend WithEvents Col_Spotlight As DataGridViewTextBoxColumn
    Friend WithEvents Col_Stairs As DataGridViewTextBoxColumn
    Friend WithEvents Col_CommentarySeat As DataGridViewTextBoxColumn
    Friend WithEvents Col_RingMat As DataGridViewTextBoxColumn
    Friend WithEvents Col_FloorMattress As DataGridViewTextBoxColumn
    Friend WithEvents Col_Crowd As DataGridViewTextBoxColumn
    Friend WithEvents Col_CrowdSeatsPlace As DataGridViewTextBoxColumn
    Friend WithEvents Col_CrowdSeatsModel As DataGridViewTextBoxColumn
    Friend WithEvents Col_IBL As DataGridViewTextBoxColumn
    Friend WithEvents Col_Titantron As DataGridViewTextBoxColumn
    Friend WithEvents Col_Minitron As DataGridViewTextBoxColumn
    Friend WithEvents Col_Wall_L As DataGridViewTextBoxColumn
    Friend WithEvents Col_Wall_R As DataGridViewTextBoxColumn
    Friend WithEvents Col_Header As DataGridViewTextBoxColumn
    Friend WithEvents Col_Floor As DataGridViewTextBoxColumn
    Friend WithEvents Col_MiscObjects As DataGridViewTextBoxColumn
    Friend WithEvents Col_LightingType As DataGridViewTextBoxColumn
    Friend WithEvents Col_CornerPost_CM As DataGridViewTextBoxColumn
    Friend WithEvents Col_Rope_CM As DataGridViewTextBoxColumn
    Friend WithEvents Col_Apron_CM As DataGridViewTextBoxColumn
    Friend WithEvents Col_Turnbuckle_CM As DataGridViewTextBoxColumn
    Friend WithEvents Col_RingMat_CM As DataGridViewTextBoxColumn
    Friend WithEvents Col_version As DataGridViewTextBoxColumn
    Friend WithEvents StringLoadedShowMenuItem As ToolStripMenuItem
    Friend WithEvents SaveShowChangesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StrName As DataGridViewTextBoxColumn
    Friend WithEvents ShowStringName As DataGridViewTextBoxColumn
    Friend WithEvents ShowSelectNum As DataGridViewTextBoxColumn
    Friend WithEvents SelectNumSecond As DataGridViewTextBoxColumn
    Friend WithEvents A1 As DataGridViewTextBoxColumn
    Friend WithEvents A2 As DataGridViewTextBoxColumn
    Friend WithEvents B1 As DataGridViewTextBoxColumn
    Friend WithEvents B2 As DataGridViewTextBoxColumn
    Friend WithEvents B3 As DataGridViewTextBoxColumn
    Friend WithEvents C1 As DataGridViewCheckBoxColumn
    Friend WithEvents C2 As DataGridViewCheckBoxColumn
    Friend WithEvents C3 As DataGridViewCheckBoxColumn
    Friend WithEvents C4 As DataGridViewCheckBoxColumn
    Friend WithEvents C5 As DataGridViewCheckBoxColumn
    Friend WithEvents C6 As DataGridViewCheckBoxColumn
    Friend WithEvents C7 As DataGridViewCheckBoxColumn
    Friend WithEvents C8 As DataGridViewCheckBoxColumn
    Friend WithEvents Stage As DataGridViewTextBoxColumn
    Friend WithEvents D1 As DataGridViewTextBoxColumn
    Friend WithEvents D2 As DataGridViewTextBoxColumn
    Friend WithEvents D3 As DataGridViewTextBoxColumn
    Friend WithEvents D4 As DataGridViewTextBoxColumn
    Friend WithEvents D5 As DataGridViewTextBoxColumn
    Friend WithEvents Ref As DataGridViewTextBoxColumn
    Friend WithEvents Filter As DataGridViewTextBoxColumn
    Friend WithEvents F1 As DataGridViewTextBoxColumn
    Friend WithEvents F2 As DataGridViewTextBoxColumn
    Friend WithEvents F3 As DataGridViewTextBoxColumn
    Friend WithEvents F4 As DataGridViewTextBoxColumn
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
End Class
