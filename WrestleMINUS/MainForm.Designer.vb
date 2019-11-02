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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.MenuStripMainForm = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadHomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebuildDefFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebuildDefCurrentHomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RebuildDefSelectFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BPECompressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BPEBatchCompressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BPESingleCompressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZLIBCompressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZLIBBatchCompressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZLIBSingleCompressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OODLCompressionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OODLBatchCompressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OODLSingleCompressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GenerateFileNameHashToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SupportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GitHubIssuesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitFileMenuContainer = New System.Windows.Forms.SplitContainer()
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
        Me.StringHexRefColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StringTextColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StringLengthColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddStringButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.DeleteStringButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripStringView = New System.Windows.Forms.MenuStrip()
        Me.StringCountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBoxSearch = New System.Windows.Forms.ToolStripTextBox()
        Me.SaveChangesStringMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SortStringsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportStringArrayToCSVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ArenaMiscAddButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ArenaMiscDelButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripMiscView = New System.Windows.Forms.MenuStrip()
        Me.MiscViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.SaveChangesMiscMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowView = New System.Windows.Forms.TabPage()
        Me.DataGridShowView = New System.Windows.Forms.DataGridView()
        Me.ShowViewStrName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewStringName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewSelectNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewNumSecond = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewA1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewA2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewB1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewB2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewB3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewC1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC7 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewC8 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ShowViewStage = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewD1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewD2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewD3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewD4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowView5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Filter = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewF1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewF2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewF3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewF4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewH1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.H2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewH3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewH4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewBar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewUnknown = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewI1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewI2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewI3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewLive = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewJ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ShowViewAddButton = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ShowViewDelete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripShowView = New System.Windows.Forms.MenuStrip()
        Me.ShowViewType = New System.Windows.Forms.ToolStripComboBox()
        Me.StringLoadedShowMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesShowMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NIBJView = New System.Windows.Forms.TabPage()
        Me.DataGridNIBJView = New System.Windows.Forms.DataGridView()
        Me.MenuStripNIBJView = New System.Windows.Forms.MenuStrip()
        Me.FileAttributesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesNIBJMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureView = New System.Windows.Forms.TabPage()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.ObjectView = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.ObjectMainViewPage = New System.Windows.Forms.TabPage()
        Me.DataGridObjectView = New System.Windows.Forms.DataGridView()
        Me.ObjectCountCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectHeaderLoad = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ObjectExportToObj = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ObjectVertexCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectRendered = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ObjectHeaderFiller = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectWeightNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUnknownIntA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVerHeaderCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVerticeOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectWeightsOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUVOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectNormalsOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectInternalNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectHeaderShader = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjecHeaderUnknownC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectMaterialIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParameterCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParameterOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectFaceOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUVCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUnknownD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUnknownE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUnknownF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUnknownG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectUnknownH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneViewPage = New System.Windows.Forms.TabPage()
        Me.DataGridObjectBoneView = New System.Windows.Forms.DataGridView()
        Me.ObjectBoneCountCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownC = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownE = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownG = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownJ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownK = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownL = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownM = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectBoneUnknownP = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectTextureViewPage = New System.Windows.Forms.TabPage()
        Me.ObjectTestureSplitContainer = New System.Windows.Forms.SplitContainer()
        Me.DataGridObjectTextureView = New System.Windows.Forms.DataGridView()
        Me.ObjectTextureCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectTextureCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridObjectShaderView = New System.Windows.Forms.DataGridView()
        Me.ObjectShaderCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectShaderCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectShaderType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectShaderB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertexViewPage = New System.Windows.Forms.TabPage()
        Me.DataGridObjectVertexView = New System.Windows.Forms.DataGridView()
        Me.ObjectVertCountCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertZ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertRX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertRY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertRZ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertWeight = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertU = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertV = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertNormal1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertNormal2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectVertNormal3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripObjectVertexView = New System.Windows.Forms.MenuStrip()
        Me.ShowNormalsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowWeightsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectFacesViewPage = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridObjectTriStripsView = New System.Windows.Forms.DataGridView()
        Me.ObjectTriStripNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectTriStripVerts = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectTriStripVertCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridObjectFacesView = New System.Windows.Forms.DataGridView()
        Me.ObjectFaceCurrentCountCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectFaceVertex1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectFaceVertex2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectFaceVertex3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParamViewPage = New System.Windows.Forms.TabPage()
        Me.DataGridObjectParamView = New System.Windows.Forms.DataGridView()
        Me.ObjectParamCountCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParamName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParamInt1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParamInt2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjectParamSingle = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripObjectView = New System.Windows.Forms.MenuStrip()
        Me.LoadedObjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjectEmoteListComboBox = New System.Windows.Forms.ToolStripComboBox()
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
        Me.ImportMasksFromTXTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportMaskstoTXTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesMaskMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.TekkenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TutorialVideoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DSImportScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DSSelectionScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DSExportScriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AznTutorialToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ObjArrayView = New System.Windows.Forms.TabPage()
        Me.DataGridObjArrayView = New System.Windows.Forms.DataGridView()
        Me.YobjArryIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ObjArrayParent = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Number = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArrEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ChairName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.X = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Y = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Z = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RX = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RY = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RZ = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ContainedYobjArray = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartIndexYobjArray = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Add_Obj_Array = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.Del_Obj_Array = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripObjectArrayView = New System.Windows.Forms.MenuStrip()
        Me.ImportYOBJArrayFromCSVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportYOBJArrayToCSVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesYOBJArrayMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AssetView = New System.Windows.Forms.TabPage()
        Me.DataGridAssetView = New System.Windows.Forms.DataGridView()
        Me.PacNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AttireNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AudioNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MUSOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EVTOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MusicID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitantronNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HeaderNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WallNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RampNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WallRightNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WallLeftNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RawTronEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SmackDownTronEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ClassicTronEnabled = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Check5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Check6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MUSFileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EVTFileName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddAsset = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.DeleteAsset = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripAssetView = New System.Windows.Forms.MenuStrip()
        Me.SaveChangesAssetViewMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TitleView = New System.Windows.Forms.TabPage()
        Me.DataGridTitleView = New System.Windows.Forms.DataGridView()
        Me.TitleEnabled = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PropRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleNameNum1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleNameNum1Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleNameNum2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleNameNum2Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleNameNum3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleNameNum3Full = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MyWWE1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MyWWE1Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MyWWE2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MyWWE2Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleUni1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UniTitle1Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleUni2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.UniTitle2Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleTemp1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Temp2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleFemale = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TitleTagTeam = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TitleCruiserweight = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UnlockNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TitleTemp4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripTitleView = New System.Windows.Forms.MenuStrip()
        Me.TitleGameComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.StringLoadedTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PacsLoadedTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesTitleMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SoundView = New System.Windows.Forms.TabPage()
        Me.DataGridSoundView = New System.Windows.Forms.DataGridView()
        Me.SoundContainerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SoundRefNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SoundHashRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SoundOffset = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SoundInfoAdd = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.SoundInfoDel = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripSoundView = New System.Windows.Forms.MenuStrip()
        Me.ToolStripSoundRefSearch = New System.Windows.Forms.ToolStripTextBox()
        Me.SaveChangesSoundMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemView = New System.Windows.Forms.TabPage()
        Me.DataGridMenuItemView = New System.Windows.Forms.DataGridView()
        Me.CAEEventID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEStringRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEStringPrint = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacNum1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacName1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacNum2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacName2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacNum3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacName3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacNum4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacName4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacNum5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPacName5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEDefaultWrestlerNum = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CAEPromo1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPromo2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPromo3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEPromo4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEBuffer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEUknown1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEUknown2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAELoackedtoPac = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PacNumExcluded = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAEDLCFlag = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.CAEMenuItemAdd = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.CAEMenuItemDelete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripMenuItemView = New System.Windows.Forms.MenuStrip()
        Me.StringLoadedCAEMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PacsLoadedCAEMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesCAEMenuItemMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnimationView = New System.Windows.Forms.TabPage()
        Me.DataGridAnimationView = New System.Windows.Forms.DataGridView()
        Me.AnimationBoneIndex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationStartingInt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationStartHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationHeaderLength = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationHeaderLengthHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationBoneType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationBoneTypeHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationOffsetA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationOffsetAHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationIntA = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationIntAHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationOffsetB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationOffsetBHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationIntB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationIntBHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationRemainingBytes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationStartingData = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationSecondaryData = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationXTrans = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnimationYTrans = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnimationZTrans = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnimationXRotation = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnimationYRotation = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnimationZRotation = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.AnimationFramesDec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationFramesHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationAnimationData = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AnimationAnimationDataParsed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripAnimationView = New System.Windows.Forms.MenuStrip()
        Me.AnimationShowHexToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnimationShowDebugToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesAnimationMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pof0View = New System.Windows.Forms.TabPage()
        Me.DataGridPof0View = New System.Windows.Forms.DataGridView()
        Me.Pof0ByteCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pof0RawHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pof0TranslateDec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pof0TranslateHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pof0ActiveOffsetDec = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pof0ActiveOffsetHex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pof0ReferenceData = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MenuStripPof0View = New System.Windows.Forms.MenuStrip()
        Me.WeaponPositionView = New System.Windows.Forms.TabPage()
        Me.DataGridWeaponPositionView = New System.Windows.Forms.DataGridView()
        Me.WeaponPositionCount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSettingNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSettingObjStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionByteArray = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionInt1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionInt2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionInt3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionInt4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSingle1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSingle2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSingle3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSingle4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSingle5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionSingle6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionShort1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionShort2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionShort3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionShort4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionIntSet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WeaponPositionAdd = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.WeaponPositionDelete = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.MenuStripWeapPos = New System.Windows.Forms.MenuStrip()
        Me.WeaponPositionTypeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveChangesWeaponPositionsMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ArcView = New System.Windows.Forms.TabPage()
        Me.DataGridArcView = New System.Windows.Forms.DataGridView()
        Me.MenuStripArcView = New System.Windows.Forms.MenuStrip()
        Me.MenuStripPictureView = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TreeViewContext = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenRADVideoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenImageWithToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractPartToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractAllInPlaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExtractAllToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectUncompressedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectBPEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectZLIBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InjectOODLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CrawlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeletePartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenamePartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileLocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.SelectNewHomeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStripMainForm.SuspendLayout()
        CType(Me.SplitFileMenuContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitFileMenuContainer.Panel1.SuspendLayout()
        Me.SplitFileMenuContainer.Panel2.SuspendLayout()
        Me.SplitFileMenuContainer.SuspendLayout()
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
        Me.TabControl2.SuspendLayout()
        Me.ObjectMainViewPage.SuspendLayout()
        CType(Me.DataGridObjectView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjectBoneViewPage.SuspendLayout()
        CType(Me.DataGridObjectBoneView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjectTextureViewPage.SuspendLayout()
        CType(Me.ObjectTestureSplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjectTestureSplitContainer.Panel1.SuspendLayout()
        Me.ObjectTestureSplitContainer.Panel2.SuspendLayout()
        Me.ObjectTestureSplitContainer.SuspendLayout()
        CType(Me.DataGridObjectTextureView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridObjectShaderView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjectVertexViewPage.SuspendLayout()
        CType(Me.DataGridObjectVertexView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripObjectVertexView.SuspendLayout()
        Me.ObjectFacesViewPage.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridObjectTriStripsView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridObjectFacesView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ObjectParamViewPage.SuspendLayout()
        CType(Me.DataGridObjectParamView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripObjectView.SuspendLayout()
        Me.AttireView.SuspendLayout()
        CType(Me.DataGridAttireView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripAttireView.SuspendLayout()
        Me.MuscleView.SuspendLayout()
        CType(Me.DataGridMuscleView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripMuscleView.SuspendLayout()
        Me.MaskView.SuspendLayout()
        CType(Me.DataGridMaskView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripMaskView.SuspendLayout()
        Me.ObjArrayView.SuspendLayout()
        CType(Me.DataGridObjArrayView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripObjectArrayView.SuspendLayout()
        Me.AssetView.SuspendLayout()
        CType(Me.DataGridAssetView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripAssetView.SuspendLayout()
        Me.TitleView.SuspendLayout()
        CType(Me.DataGridTitleView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripTitleView.SuspendLayout()
        Me.SoundView.SuspendLayout()
        CType(Me.DataGridSoundView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripSoundView.SuspendLayout()
        Me.MenuItemView.SuspendLayout()
        CType(Me.DataGridMenuItemView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripMenuItemView.SuspendLayout()
        Me.AnimationView.SuspendLayout()
        CType(Me.DataGridAnimationView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripAnimationView.SuspendLayout()
        Me.Pof0View.SuspendLayout()
        CType(Me.DataGridPof0View, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WeaponPositionView.SuspendLayout()
        CType(Me.DataGridWeaponPositionView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripWeapPos.SuspendLayout()
        Me.ArcView.SuspendLayout()
        CType(Me.DataGridArcView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStripPictureView.SuspendLayout()
        Me.TreeViewContext.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStripMainForm
        '
        Me.MenuStripMainForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStripMainForm.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripMainForm.Name = "MenuStripMainForm"
        Me.MenuStripMainForm.Size = New System.Drawing.Size(1484, 24)
        Me.MenuStripMainForm.TabIndex = 0
        Me.MenuStripMainForm.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadHomeToolStripMenuItem, Me.OpenToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'LoadHomeToolStripMenuItem
        '
        Me.LoadHomeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectNewHomeToolStripMenuItem})
        Me.LoadHomeToolStripMenuItem.Image = CType(resources.GetObject("LoadHomeToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LoadHomeToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.LoadHomeToolStripMenuItem.Name = "LoadHomeToolStripMenuItem"
        Me.LoadHomeToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.LoadHomeToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.LoadHomeToolStripMenuItem.Text = "&Load Home"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Image = CType(resources.GetObject("OpenToolStripMenuItem.Image"), System.Drawing.Image)
        Me.OpenToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.OpenToolStripMenuItem.Text = "&Open File / Folder"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.ShortcutKeyDisplayString = ""
        Me.OptionsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.OptionsToolStripMenuItem.Text = "&Options"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RebuildDefFileToolStripMenuItem, Me.BPECompressionToolStripMenuItem, Me.ZLIBCompressionToolStripMenuItem, Me.OODLCompressionToolStripMenuItem, Me.GenerateFileNameHashToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(46, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'RebuildDefFileToolStripMenuItem
        '
        Me.RebuildDefFileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RebuildDefCurrentHomeToolStripMenuItem, Me.RebuildDefSelectFolderToolStripMenuItem})
        Me.RebuildDefFileToolStripMenuItem.Name = "RebuildDefFileToolStripMenuItem"
        Me.RebuildDefFileToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.RebuildDefFileToolStripMenuItem.Text = "Rebuild Def File"
        '
        'RebuildDefCurrentHomeToolStripMenuItem
        '
        Me.RebuildDefCurrentHomeToolStripMenuItem.Name = "RebuildDefCurrentHomeToolStripMenuItem"
        Me.RebuildDefCurrentHomeToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.RebuildDefCurrentHomeToolStripMenuItem.Text = "For Current Home Folder"
        '
        'RebuildDefSelectFolderToolStripMenuItem
        '
        Me.RebuildDefSelectFolderToolStripMenuItem.Name = "RebuildDefSelectFolderToolStripMenuItem"
        Me.RebuildDefSelectFolderToolStripMenuItem.Size = New System.Drawing.Size(206, 22)
        Me.RebuildDefSelectFolderToolStripMenuItem.Text = "For WWE Exe..."
        '
        'BPECompressionToolStripMenuItem
        '
        Me.BPECompressionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BPEBatchCompressToolStripMenuItem, Me.BPESingleCompressToolStripMenuItem})
        Me.BPECompressionToolStripMenuItem.Name = "BPECompressionToolStripMenuItem"
        Me.BPECompressionToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.BPECompressionToolStripMenuItem.Text = "BPE Compression"
        '
        'BPEBatchCompressToolStripMenuItem
        '
        Me.BPEBatchCompressToolStripMenuItem.Name = "BPEBatchCompressToolStripMenuItem"
        Me.BPEBatchCompressToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.BPEBatchCompressToolStripMenuItem.Text = "Compress File(s) in Place"
        '
        'BPESingleCompressToolStripMenuItem
        '
        Me.BPESingleCompressToolStripMenuItem.Name = "BPESingleCompressToolStripMenuItem"
        Me.BPESingleCompressToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.BPESingleCompressToolStripMenuItem.Text = "Compress File to..."
        '
        'ZLIBCompressionToolStripMenuItem
        '
        Me.ZLIBCompressionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ZLIBBatchCompressToolStripMenuItem, Me.ZLIBSingleCompressToolStripMenuItem})
        Me.ZLIBCompressionToolStripMenuItem.Name = "ZLIBCompressionToolStripMenuItem"
        Me.ZLIBCompressionToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.ZLIBCompressionToolStripMenuItem.Text = "ZLIB Compression"
        '
        'ZLIBBatchCompressToolStripMenuItem
        '
        Me.ZLIBBatchCompressToolStripMenuItem.Name = "ZLIBBatchCompressToolStripMenuItem"
        Me.ZLIBBatchCompressToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ZLIBBatchCompressToolStripMenuItem.Text = "Compress File(s) in Place"
        '
        'ZLIBSingleCompressToolStripMenuItem
        '
        Me.ZLIBSingleCompressToolStripMenuItem.Name = "ZLIBSingleCompressToolStripMenuItem"
        Me.ZLIBSingleCompressToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.ZLIBSingleCompressToolStripMenuItem.Text = "Compress File to..."
        '
        'OODLCompressionToolStripMenuItem
        '
        Me.OODLCompressionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OODLBatchCompressToolStripMenuItem, Me.OODLSingleCompressToolStripMenuItem})
        Me.OODLCompressionToolStripMenuItem.Name = "OODLCompressionToolStripMenuItem"
        Me.OODLCompressionToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.OODLCompressionToolStripMenuItem.Text = "OODL Compression"
        '
        'OODLBatchCompressToolStripMenuItem
        '
        Me.OODLBatchCompressToolStripMenuItem.Name = "OODLBatchCompressToolStripMenuItem"
        Me.OODLBatchCompressToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.OODLBatchCompressToolStripMenuItem.Text = "Compress File(s) in Place"
        '
        'OODLSingleCompressToolStripMenuItem
        '
        Me.OODLSingleCompressToolStripMenuItem.Name = "OODLSingleCompressToolStripMenuItem"
        Me.OODLSingleCompressToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
        Me.OODLSingleCompressToolStripMenuItem.Text = "Compress File to..."
        '
        'GenerateFileNameHashToolStripMenuItem
        '
        Me.GenerateFileNameHashToolStripMenuItem.Name = "GenerateFileNameHashToolStripMenuItem"
        Me.GenerateFileNameHashToolStripMenuItem.Size = New System.Drawing.Size(207, 22)
        Me.GenerateFileNameHashToolStripMenuItem.Text = "Generate File Name Hash"
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
        'SplitFileMenuContainer
        '
        Me.SplitFileMenuContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitFileMenuContainer.Location = New System.Drawing.Point(0, 24)
        Me.SplitFileMenuContainer.Name = "SplitFileMenuContainer"
        '
        'SplitFileMenuContainer.Panel1
        '
        Me.SplitFileMenuContainer.Panel1.Controls.Add(Me.TreeView1)
        Me.SplitFileMenuContainer.Panel1.Controls.Add(Me.ProgressBar1)
        Me.SplitFileMenuContainer.Panel1.Controls.Add(Me.MenuStripTreeView)
        '
        'SplitFileMenuContainer.Panel2
        '
        Me.SplitFileMenuContainer.Panel2.Controls.Add(Me.TabControl1)
        Me.SplitFileMenuContainer.Size = New System.Drawing.Size(1484, 437)
        Me.SplitFileMenuContainer.SplitterDistance = 253
        Me.SplitFileMenuContainer.TabIndex = 1
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Location = New System.Drawing.Point(0, 44)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.Size = New System.Drawing.Size(253, 370)
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
        Me.ImageList1.Images.SetKeyName(24, "R.png")
        Me.ImageList1.Images.SetKeyName(25, "L.png")
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 414)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(253, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'MenuStripTreeView
        '
        Me.MenuStripTreeView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentViewToolStripMenuItem})
        Me.MenuStripTreeView.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripTreeView.Name = "MenuStripTreeView"
        Me.MenuStripTreeView.Size = New System.Drawing.Size(253, 44)
        Me.MenuStripTreeView.TabIndex = 1
        Me.MenuStripTreeView.Text = "MenuStrip2"
        '
        'CurrentViewToolStripMenuItem
        '
        Me.CurrentViewToolStripMenuItem.AutoSize = False
        Me.CurrentViewToolStripMenuItem.Enabled = False
        Me.CurrentViewToolStripMenuItem.Name = "CurrentViewToolStripMenuItem"
        Me.CurrentViewToolStripMenuItem.Size = New System.Drawing.Size(242, 40)
        Me.CurrentViewToolStripMenuItem.Text = "Current View:"
        Me.CurrentViewToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopLeft
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
        Me.TabControl1.Controls.Add(Me.SoundView)
        Me.TabControl1.Controls.Add(Me.MenuItemView)
        Me.TabControl1.Controls.Add(Me.AnimationView)
        Me.TabControl1.Controls.Add(Me.Pof0View)
        Me.TabControl1.Controls.Add(Me.WeaponPositionView)
        Me.TabControl1.Controls.Add(Me.ArcView)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1227, 437)
        Me.TabControl1.TabIndex = 1
        '
        'HexView
        '
        Me.HexView.Controls.Add(Me.Hex_Selected)
        Me.HexView.Controls.Add(Me.MenuStripHexView)
        Me.HexView.Location = New System.Drawing.Point(4, 22)
        Me.HexView.Name = "HexView"
        Me.HexView.Padding = New System.Windows.Forms.Padding(3)
        Me.HexView.Size = New System.Drawing.Size(1219, 411)
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
        Me.Hex_Selected.Size = New System.Drawing.Size(1213, 378)
        Me.Hex_Selected.TabIndex = 2
        Me.Hex_Selected.Text = ""
        '
        'MenuStripHexView
        '
        Me.MenuStripHexView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HexViewBitWidth, Me.HexViewFileName})
        Me.MenuStripHexView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripHexView.Name = "MenuStripHexView"
        Me.MenuStripHexView.Size = New System.Drawing.Size(1213, 27)
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
        Me.TextView.Size = New System.Drawing.Size(1219, 411)
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
        Me.Text_Selected.Size = New System.Drawing.Size(1213, 378)
        Me.Text_Selected.TabIndex = 2
        Me.Text_Selected.Text = ""
        '
        'MenuStripTextView
        '
        Me.MenuStripTextView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TextViewBitWidth, Me.TextViewFileName})
        Me.MenuStripTextView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripTextView.Name = "MenuStripTextView"
        Me.MenuStripTextView.Size = New System.Drawing.Size(1213, 27)
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
        Me.StringView.Size = New System.Drawing.Size(1219, 411)
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
        Me.DataGridStringView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.StringHexRefColumn, Me.StringTextColumn, Me.StringLengthColumn, Me.AddStringButton, Me.DeleteStringButton})
        Me.DataGridStringView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridStringView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridStringView.MultiSelect = False
        Me.DataGridStringView.Name = "DataGridStringView"
        Me.DataGridStringView.RowHeadersVisible = False
        Me.DataGridStringView.Size = New System.Drawing.Size(1213, 378)
        Me.DataGridStringView.TabIndex = 2
        '
        'StringHexRefColumn
        '
        Me.StringHexRefColumn.FillWeight = 25.0!
        Me.StringHexRefColumn.HeaderText = "HexRef"
        Me.StringHexRefColumn.MaxInputLength = 8
        Me.StringHexRefColumn.Name = "StringHexRefColumn"
        Me.StringHexRefColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'StringTextColumn
        '
        Me.StringTextColumn.HeaderText = "String Text"
        Me.StringTextColumn.MaxInputLength = 31
        Me.StringTextColumn.Name = "StringTextColumn"
        Me.StringTextColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'StringLengthColumn
        '
        Me.StringLengthColumn.FillWeight = 25.0!
        Me.StringLengthColumn.HeaderText = "Length"
        Me.StringLengthColumn.MaxInputLength = 10
        Me.StringLengthColumn.Name = "StringLengthColumn"
        Me.StringLengthColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'AddStringButton
        '
        Me.AddStringButton.FillWeight = 10.0!
        Me.AddStringButton.HeaderText = "Add"
        Me.AddStringButton.Name = "AddStringButton"
        Me.AddStringButton.Text = "Add"
        Me.AddStringButton.UseColumnTextForButtonValue = True
        '
        'DeleteStringButton
        '
        Me.DeleteStringButton.FillWeight = 10.0!
        Me.DeleteStringButton.HeaderText = "Delete"
        Me.DeleteStringButton.Name = "DeleteStringButton"
        Me.DeleteStringButton.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DeleteStringButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DeleteStringButton.Text = "Delete"
        Me.DeleteStringButton.UseColumnTextForButtonValue = True
        '
        'MenuStripStringView
        '
        Me.MenuStripStringView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StringCountToolStripMenuItem, Me.ToolStripTextBoxSearch, Me.SaveChangesStringMenuItem, Me.SortStringsToolStripMenuItem, Me.ExportStringArrayToCSVToolStripMenuItem})
        Me.MenuStripStringView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripStringView.Name = "MenuStripStringView"
        Me.MenuStripStringView.Size = New System.Drawing.Size(1213, 27)
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
        'SaveChangesStringMenuItem
        '
        Me.SaveChangesStringMenuItem.Name = "SaveChangesStringMenuItem"
        Me.SaveChangesStringMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveChangesStringMenuItem.Text = "Save Changes"
        Me.SaveChangesStringMenuItem.Visible = False
        '
        'SortStringsToolStripMenuItem
        '
        Me.SortStringsToolStripMenuItem.Name = "SortStringsToolStripMenuItem"
        Me.SortStringsToolStripMenuItem.Size = New System.Drawing.Size(79, 23)
        Me.SortStringsToolStripMenuItem.Text = "Sort Strings"
        Me.SortStringsToolStripMenuItem.Visible = False
        '
        'ExportStringArrayToCSVToolStripMenuItem
        '
        Me.ExportStringArrayToCSVToolStripMenuItem.Name = "ExportStringArrayToCSVToolStripMenuItem"
        Me.ExportStringArrayToCSVToolStripMenuItem.Size = New System.Drawing.Size(91, 23)
        Me.ExportStringArrayToCSVToolStripMenuItem.Text = "Export to CSV"
        '
        'MiscView
        '
        Me.MiscView.Controls.Add(Me.DataGridMiscView)
        Me.MiscView.Controls.Add(Me.MenuStripMiscView)
        Me.MiscView.Location = New System.Drawing.Point(4, 22)
        Me.MiscView.Name = "MiscView"
        Me.MiscView.Padding = New System.Windows.Forms.Padding(3)
        Me.MiscView.Size = New System.Drawing.Size(1219, 411)
        Me.MiscView.TabIndex = 3
        Me.MiscView.Text = "Misc View"
        Me.MiscView.UseVisualStyleBackColor = True
        '
        'DataGridMiscView
        '
        Me.DataGridMiscView.AllowUserToAddRows = False
        Me.DataGridMiscView.AllowUserToDeleteRows = False
        Me.DataGridMiscView.AllowUserToResizeRows = False
        Me.DataGridMiscView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader
        Me.DataGridMiscView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMiscView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_ArenaNumber, Me.Col_Stadium, Me.Col_Advertisement, Me.Col_CornerPost, Me.Col_LED_CornerPost, Me.Col_Rope, Me.Col_Apron, Me.Col_LED_Apron, Me.Col_Turnbuckle, Me.Col_Barricade, Me.Col_Fence, Me.Col_CeilingLighting, Me.Col_Spotlight, Me.Col_Stairs, Me.Col_CommentarySeat, Me.Col_RingMat, Me.Col_FloorMattress, Me.Col_Crowd, Me.Col_CrowdSeatsPlace, Me.Col_CrowdSeatsModel, Me.Col_IBL, Me.Col_Titantron, Me.Col_Minitron, Me.Col_Wall_L, Me.Col_Wall_R, Me.Col_Header, Me.Col_Floor, Me.Col_MiscObjects, Me.Col_LightingType, Me.Col_CornerPost_CM, Me.Col_Rope_CM, Me.Col_Apron_CM, Me.Col_Turnbuckle_CM, Me.Col_RingMat_CM, Me.Col_version, Me.ArenaMiscAddButton, Me.ArenaMiscDelButton})
        Me.DataGridMiscView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMiscView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridMiscView.Name = "DataGridMiscView"
        Me.DataGridMiscView.RowHeadersVisible = False
        Me.DataGridMiscView.RowHeadersWidth = 70
        Me.DataGridMiscView.Size = New System.Drawing.Size(1213, 378)
        Me.DataGridMiscView.TabIndex = 1
        '
        'Col_ArenaNumber
        '
        Me.Col_ArenaNumber.Frozen = True
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
        'ArenaMiscAddButton
        '
        Me.ArenaMiscAddButton.HeaderText = "Add"
        Me.ArenaMiscAddButton.Name = "ArenaMiscAddButton"
        Me.ArenaMiscAddButton.Text = "Add"
        Me.ArenaMiscAddButton.UseColumnTextForButtonValue = True
        Me.ArenaMiscAddButton.Width = 32
        '
        'ArenaMiscDelButton
        '
        Me.ArenaMiscDelButton.HeaderText = "Delete"
        Me.ArenaMiscDelButton.Name = "ArenaMiscDelButton"
        Me.ArenaMiscDelButton.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ArenaMiscDelButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ArenaMiscDelButton.Text = "Delete"
        Me.ArenaMiscDelButton.UseColumnTextForButtonValue = True
        Me.ArenaMiscDelButton.Width = 63
        '
        'MenuStripMiscView
        '
        Me.MenuStripMiscView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MiscViewType, Me.SaveChangesMiscMenuItem})
        Me.MenuStripMiscView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMiscView.Name = "MenuStripMiscView"
        Me.MenuStripMiscView.Size = New System.Drawing.Size(1213, 27)
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
        'SaveChangesMiscMenuItem
        '
        Me.SaveChangesMiscMenuItem.Name = "SaveChangesMiscMenuItem"
        Me.SaveChangesMiscMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveChangesMiscMenuItem.Text = "Save Changes"
        Me.SaveChangesMiscMenuItem.Visible = False
        '
        'ShowView
        '
        Me.ShowView.Controls.Add(Me.DataGridShowView)
        Me.ShowView.Controls.Add(Me.MenuStripShowView)
        Me.ShowView.Location = New System.Drawing.Point(4, 22)
        Me.ShowView.Name = "ShowView"
        Me.ShowView.Padding = New System.Windows.Forms.Padding(3)
        Me.ShowView.Size = New System.Drawing.Size(1219, 411)
        Me.ShowView.TabIndex = 4
        Me.ShowView.Text = "Show View"
        Me.ShowView.UseVisualStyleBackColor = True
        '
        'DataGridShowView
        '
        Me.DataGridShowView.AllowUserToAddRows = False
        Me.DataGridShowView.AllowUserToDeleteRows = False
        Me.DataGridShowView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridShowView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridShowView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ShowViewStrName, Me.ShowViewStringName, Me.ShowViewSelectNum, Me.ShowViewNumSecond, Me.ShowViewA1, Me.ShowViewA2, Me.ShowViewB1, Me.ShowViewB2, Me.ShowViewB3, Me.ShowViewC1, Me.ShowViewC2, Me.ShowViewC3, Me.ShowViewC4, Me.ShowViewC5, Me.ShowViewC6, Me.ShowViewC7, Me.ShowViewC8, Me.ShowViewStage, Me.ShowViewD1, Me.ShowViewD2, Me.ShowViewD3, Me.ShowViewD4, Me.ShowView5, Me.ShowViewRef, Me.Filter, Me.ShowViewF1, Me.ShowViewF2, Me.ShowViewF3, Me.ShowViewF4, Me.ShowViewH1, Me.H2, Me.ShowViewH3, Me.ShowViewH4, Me.ShowViewBar, Me.ShowViewUnknown, Me.ShowViewI1, Me.ShowViewI2, Me.ShowViewI3, Me.ShowViewLive, Me.ShowViewJ, Me.ShowViewAddButton, Me.ShowViewDelete})
        Me.DataGridShowView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridShowView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridShowView.Name = "DataGridShowView"
        Me.DataGridShowView.RowHeadersWidth = 60
        Me.DataGridShowView.Size = New System.Drawing.Size(1213, 378)
        Me.DataGridShowView.TabIndex = 2
        '
        'ShowViewStrName
        '
        Me.ShowViewStrName.HeaderText = "StrName"
        Me.ShowViewStrName.MaxInputLength = 8
        Me.ShowViewStrName.Name = "ShowViewStrName"
        Me.ShowViewStrName.Width = 73
        '
        'ShowViewStringName
        '
        Me.ShowViewStringName.HeaderText = "Name"
        Me.ShowViewStringName.Name = "ShowViewStringName"
        Me.ShowViewStringName.ReadOnly = True
        Me.ShowViewStringName.Width = 60
        '
        'ShowViewSelectNum
        '
        Me.ShowViewSelectNum.HeaderText = "SelectNum"
        Me.ShowViewSelectNum.MaxInputLength = 3
        Me.ShowViewSelectNum.Name = "ShowViewSelectNum"
        Me.ShowViewSelectNum.Width = 84
        '
        'ShowViewNumSecond
        '
        Me.ShowViewNumSecond.HeaderText = "SelectNum"
        Me.ShowViewNumSecond.MaxInputLength = 3
        Me.ShowViewNumSecond.Name = "ShowViewNumSecond"
        Me.ShowViewNumSecond.Width = 84
        '
        'ShowViewA1
        '
        Me.ShowViewA1.HeaderText = "A1"
        Me.ShowViewA1.MaxInputLength = 3
        Me.ShowViewA1.Name = "ShowViewA1"
        Me.ShowViewA1.Width = 45
        '
        'ShowViewA2
        '
        Me.ShowViewA2.HeaderText = "A2"
        Me.ShowViewA2.MaxInputLength = 3
        Me.ShowViewA2.Name = "ShowViewA2"
        Me.ShowViewA2.Width = 45
        '
        'ShowViewB1
        '
        Me.ShowViewB1.HeaderText = "B1"
        Me.ShowViewB1.MaxInputLength = 4
        Me.ShowViewB1.Name = "ShowViewB1"
        Me.ShowViewB1.Width = 45
        '
        'ShowViewB2
        '
        Me.ShowViewB2.HeaderText = "B2"
        Me.ShowViewB2.MaxInputLength = 4
        Me.ShowViewB2.Name = "ShowViewB2"
        Me.ShowViewB2.Width = 45
        '
        'ShowViewB3
        '
        Me.ShowViewB3.HeaderText = "B3"
        Me.ShowViewB3.MaxInputLength = 4
        Me.ShowViewB3.Name = "ShowViewB3"
        Me.ShowViewB3.Width = 45
        '
        'ShowViewC1
        '
        Me.ShowViewC1.HeaderText = "C1"
        Me.ShowViewC1.Name = "ShowViewC1"
        Me.ShowViewC1.Width = 26
        '
        'ShowViewC2
        '
        Me.ShowViewC2.HeaderText = "C2"
        Me.ShowViewC2.Name = "ShowViewC2"
        Me.ShowViewC2.Width = 26
        '
        'ShowViewC3
        '
        Me.ShowViewC3.HeaderText = "C3"
        Me.ShowViewC3.Name = "ShowViewC3"
        Me.ShowViewC3.Width = 26
        '
        'ShowViewC4
        '
        Me.ShowViewC4.HeaderText = "C4"
        Me.ShowViewC4.Name = "ShowViewC4"
        Me.ShowViewC4.Width = 26
        '
        'ShowViewC5
        '
        Me.ShowViewC5.HeaderText = "C5"
        Me.ShowViewC5.Name = "ShowViewC5"
        Me.ShowViewC5.Width = 26
        '
        'ShowViewC6
        '
        Me.ShowViewC6.HeaderText = "C6"
        Me.ShowViewC6.Name = "ShowViewC6"
        Me.ShowViewC6.Width = 26
        '
        'ShowViewC7
        '
        Me.ShowViewC7.HeaderText = "C7"
        Me.ShowViewC7.Name = "ShowViewC7"
        Me.ShowViewC7.Width = 26
        '
        'ShowViewC8
        '
        Me.ShowViewC8.HeaderText = "C8"
        Me.ShowViewC8.Name = "ShowViewC8"
        Me.ShowViewC8.Width = 26
        '
        'ShowViewStage
        '
        Me.ShowViewStage.HeaderText = "Stage"
        Me.ShowViewStage.MaxInputLength = 1
        Me.ShowViewStage.Name = "ShowViewStage"
        Me.ShowViewStage.Width = 60
        '
        'ShowViewD1
        '
        Me.ShowViewD1.HeaderText = "D1"
        Me.ShowViewD1.MaxInputLength = 1
        Me.ShowViewD1.Name = "ShowViewD1"
        Me.ShowViewD1.Width = 46
        '
        'ShowViewD2
        '
        Me.ShowViewD2.HeaderText = "D2"
        Me.ShowViewD2.MaxInputLength = 1
        Me.ShowViewD2.Name = "ShowViewD2"
        Me.ShowViewD2.Width = 46
        '
        'ShowViewD3
        '
        Me.ShowViewD3.HeaderText = "D3"
        Me.ShowViewD3.MaxInputLength = 1
        Me.ShowViewD3.Name = "ShowViewD3"
        Me.ShowViewD3.Width = 46
        '
        'ShowViewD4
        '
        Me.ShowViewD4.HeaderText = "D4"
        Me.ShowViewD4.MaxInputLength = 1
        Me.ShowViewD4.Name = "ShowViewD4"
        Me.ShowViewD4.Width = 46
        '
        'ShowView5
        '
        Me.ShowView5.HeaderText = "D5"
        Me.ShowView5.MaxInputLength = 1
        Me.ShowView5.Name = "ShowView5"
        Me.ShowView5.Width = 46
        '
        'ShowViewRef
        '
        Me.ShowViewRef.HeaderText = "Ref"
        Me.ShowViewRef.MaxInputLength = 1
        Me.ShowViewRef.Name = "ShowViewRef"
        Me.ShowViewRef.Width = 49
        '
        'Filter
        '
        Me.Filter.HeaderText = "Filter"
        Me.Filter.MaxInputLength = 12
        Me.Filter.Name = "Filter"
        Me.Filter.Width = 54
        '
        'ShowViewF1
        '
        Me.ShowViewF1.HeaderText = "F1"
        Me.ShowViewF1.MaxInputLength = 8
        Me.ShowViewF1.Name = "ShowViewF1"
        Me.ShowViewF1.Width = 44
        '
        'ShowViewF2
        '
        Me.ShowViewF2.HeaderText = "F2"
        Me.ShowViewF2.MaxInputLength = 2
        Me.ShowViewF2.Name = "ShowViewF2"
        Me.ShowViewF2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ShowViewF2.Width = 44
        '
        'ShowViewF3
        '
        Me.ShowViewF3.HeaderText = "F3"
        Me.ShowViewF3.MaxInputLength = 1
        Me.ShowViewF3.Name = "ShowViewF3"
        Me.ShowViewF3.Width = 44
        '
        'ShowViewF4
        '
        Me.ShowViewF4.HeaderText = "F4"
        Me.ShowViewF4.MaxInputLength = 1
        Me.ShowViewF4.Name = "ShowViewF4"
        Me.ShowViewF4.Width = 44
        '
        'ShowViewH1
        '
        Me.ShowViewH1.HeaderText = "H1"
        Me.ShowViewH1.MaxInputLength = 1
        Me.ShowViewH1.Name = "ShowViewH1"
        Me.ShowViewH1.Width = 46
        '
        'H2
        '
        Me.H2.HeaderText = "H2"
        Me.H2.MaxInputLength = 1
        Me.H2.Name = "H2"
        Me.H2.Width = 46
        '
        'ShowViewH3
        '
        Me.ShowViewH3.HeaderText = "H3"
        Me.ShowViewH3.MaxInputLength = 1
        Me.ShowViewH3.Name = "ShowViewH3"
        Me.ShowViewH3.Width = 46
        '
        'ShowViewH4
        '
        Me.ShowViewH4.HeaderText = "H4"
        Me.ShowViewH4.MaxInputLength = 1
        Me.ShowViewH4.Name = "ShowViewH4"
        Me.ShowViewH4.Width = 46
        '
        'ShowViewBar
        '
        Me.ShowViewBar.HeaderText = "Bar"
        Me.ShowViewBar.MaxInputLength = 1
        Me.ShowViewBar.Name = "ShowViewBar"
        Me.ShowViewBar.Width = 48
        '
        'ShowViewUnknown
        '
        Me.ShowViewUnknown.HeaderText = "Unkown"
        Me.ShowViewUnknown.MaxInputLength = 68
        Me.ShowViewUnknown.Name = "ShowViewUnknown"
        Me.ShowViewUnknown.Width = 72
        '
        'ShowViewI1
        '
        Me.ShowViewI1.HeaderText = "I1"
        Me.ShowViewI1.MaxInputLength = 1
        Me.ShowViewI1.Name = "ShowViewI1"
        Me.ShowViewI1.Width = 41
        '
        'ShowViewI2
        '
        Me.ShowViewI2.HeaderText = "I2"
        Me.ShowViewI2.MaxInputLength = 1
        Me.ShowViewI2.Name = "ShowViewI2"
        Me.ShowViewI2.Width = 41
        '
        'ShowViewI3
        '
        Me.ShowViewI3.HeaderText = "I3"
        Me.ShowViewI3.MaxInputLength = 2
        Me.ShowViewI3.Name = "ShowViewI3"
        Me.ShowViewI3.Width = 41
        '
        'ShowViewLive
        '
        Me.ShowViewLive.HeaderText = "Live"
        Me.ShowViewLive.MaxInputLength = 1
        Me.ShowViewLive.Name = "ShowViewLive"
        Me.ShowViewLive.Width = 52
        '
        'ShowViewJ
        '
        Me.ShowViewJ.HeaderText = "J"
        Me.ShowViewJ.MaxInputLength = 1
        Me.ShowViewJ.Name = "ShowViewJ"
        Me.ShowViewJ.Width = 37
        '
        'ShowViewAddButton
        '
        Me.ShowViewAddButton.HeaderText = "Add"
        Me.ShowViewAddButton.Name = "ShowViewAddButton"
        Me.ShowViewAddButton.Text = "Add"
        Me.ShowViewAddButton.UseColumnTextForButtonValue = True
        Me.ShowViewAddButton.Width = 32
        '
        'ShowViewDelete
        '
        Me.ShowViewDelete.HeaderText = "Delete"
        Me.ShowViewDelete.Name = "ShowViewDelete"
        Me.ShowViewDelete.Text = "Delete"
        Me.ShowViewDelete.UseColumnTextForButtonValue = True
        Me.ShowViewDelete.Width = 44
        '
        'MenuStripShowView
        '
        Me.MenuStripShowView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowViewType, Me.StringLoadedShowMenuItem, Me.SaveChangesShowMenuItem})
        Me.MenuStripShowView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripShowView.Name = "MenuStripShowView"
        Me.MenuStripShowView.Size = New System.Drawing.Size(1213, 27)
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
        'SaveChangesShowMenuItem
        '
        Me.SaveChangesShowMenuItem.Name = "SaveChangesShowMenuItem"
        Me.SaveChangesShowMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveChangesShowMenuItem.Text = "Save Changes"
        Me.SaveChangesShowMenuItem.Visible = False
        '
        'NIBJView
        '
        Me.NIBJView.Controls.Add(Me.DataGridNIBJView)
        Me.NIBJView.Controls.Add(Me.MenuStripNIBJView)
        Me.NIBJView.Location = New System.Drawing.Point(4, 22)
        Me.NIBJView.Name = "NIBJView"
        Me.NIBJView.Padding = New System.Windows.Forms.Padding(3)
        Me.NIBJView.Size = New System.Drawing.Size(1219, 411)
        Me.NIBJView.TabIndex = 5
        Me.NIBJView.Text = "NIBJView"
        Me.NIBJView.UseVisualStyleBackColor = True
        '
        'DataGridNIBJView
        '
        Me.DataGridNIBJView.AllowUserToAddRows = False
        Me.DataGridNIBJView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridNIBJView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridNIBJView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridNIBJView.MultiSelect = False
        Me.DataGridNIBJView.Name = "DataGridNIBJView"
        Me.DataGridNIBJView.RowHeadersWidth = 60
        Me.DataGridNIBJView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridNIBJView.TabIndex = 2
        '
        'MenuStripNIBJView
        '
        Me.MenuStripNIBJView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileAttributesToolStripMenuItem, Me.SaveChangesNIBJMenuItem})
        Me.MenuStripNIBJView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripNIBJView.Name = "MenuStripNIBJView"
        Me.MenuStripNIBJView.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripNIBJView.TabIndex = 0
        Me.MenuStripNIBJView.Text = "MenuStrip2"
        '
        'FileAttributesToolStripMenuItem
        '
        Me.FileAttributesToolStripMenuItem.Name = "FileAttributesToolStripMenuItem"
        Me.FileAttributesToolStripMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.FileAttributesToolStripMenuItem.Text = "File Attributes"
        '
        'SaveChangesNIBJMenuItem
        '
        Me.SaveChangesNIBJMenuItem.Name = "SaveChangesNIBJMenuItem"
        Me.SaveChangesNIBJMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesNIBJMenuItem.Text = "Save Changes"
        Me.SaveChangesNIBJMenuItem.Visible = False
        '
        'PictureView
        '
        Me.PictureView.Controls.Add(Me.PictureBox2)
        Me.PictureView.Location = New System.Drawing.Point(4, 22)
        Me.PictureView.Name = "PictureView"
        Me.PictureView.Size = New System.Drawing.Size(1219, 411)
        Me.PictureView.TabIndex = 8
        Me.PictureView.Text = "Picture View"
        Me.PictureView.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox2.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(1219, 411)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 2
        Me.PictureBox2.TabStop = False
        '
        'ObjectView
        '
        Me.ObjectView.Controls.Add(Me.TabControl2)
        Me.ObjectView.Controls.Add(Me.MenuStripObjectView)
        Me.ObjectView.Location = New System.Drawing.Point(4, 22)
        Me.ObjectView.Name = "ObjectView"
        Me.ObjectView.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectView.Size = New System.Drawing.Size(1219, 411)
        Me.ObjectView.TabIndex = 7
        Me.ObjectView.Text = "Object View"
        Me.ObjectView.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.ObjectMainViewPage)
        Me.TabControl2.Controls.Add(Me.ObjectBoneViewPage)
        Me.TabControl2.Controls.Add(Me.ObjectTextureViewPage)
        Me.TabControl2.Controls.Add(Me.ObjectVertexViewPage)
        Me.TabControl2.Controls.Add(Me.ObjectFacesViewPage)
        Me.TabControl2.Controls.Add(Me.ObjectParamViewPage)
        Me.TabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl2.Location = New System.Drawing.Point(3, 30)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(1213, 378)
        Me.TabControl2.TabIndex = 2
        '
        'ObjectMainViewPage
        '
        Me.ObjectMainViewPage.Controls.Add(Me.DataGridObjectView)
        Me.ObjectMainViewPage.Location = New System.Drawing.Point(4, 22)
        Me.ObjectMainViewPage.Name = "ObjectMainViewPage"
        Me.ObjectMainViewPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectMainViewPage.Size = New System.Drawing.Size(1205, 352)
        Me.ObjectMainViewPage.TabIndex = 0
        Me.ObjectMainViewPage.Text = "Main"
        Me.ObjectMainViewPage.UseVisualStyleBackColor = True
        '
        'DataGridObjectView
        '
        Me.DataGridObjectView.AllowUserToAddRows = False
        Me.DataGridObjectView.AllowUserToDeleteRows = False
        Me.DataGridObjectView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridObjectView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectCountCol, Me.ObjectHeaderLoad, Me.ObjectExportToObj, Me.ObjectVertexCount, Me.ObjectRendered, Me.ObjectHeaderFiller, Me.ObjectWeightNumber, Me.ObjectUnknownIntA, Me.ObjectVerHeaderCount, Me.ObjectVerticeOffset, Me.ObjectWeightsOffset, Me.ObjectUVOffset, Me.ObjectNormalsOffset, Me.ObjectInternalNum, Me.ObjectHeaderShader, Me.ObjecHeaderUnknownC, Me.ObjectMaterialIndex, Me.ObjectParameterCount, Me.ObjectParameterOffset, Me.ObjectFaceOffset, Me.ObjectUVCount, Me.ObjectUnknownD, Me.ObjectUnknownE, Me.ObjectUnknownF, Me.ObjectUnknownG, Me.ObjectUnknownH})
        Me.DataGridObjectView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectView.Location = New System.Drawing.Point(3, 3)
        Me.DataGridObjectView.Name = "DataGridObjectView"
        Me.DataGridObjectView.RowHeadersVisible = False
        Me.DataGridObjectView.Size = New System.Drawing.Size(1199, 346)
        Me.DataGridObjectView.TabIndex = 1
        '
        'ObjectCountCol
        '
        Me.ObjectCountCol.FillWeight = 25.0!
        Me.ObjectCountCol.HeaderText = "Count"
        Me.ObjectCountCol.Name = "ObjectCountCol"
        Me.ObjectCountCol.ReadOnly = True
        Me.ObjectCountCol.Width = 60
        '
        'ObjectHeaderLoad
        '
        Me.ObjectHeaderLoad.FillWeight = 25.0!
        Me.ObjectHeaderLoad.HeaderText = "Load"
        Me.ObjectHeaderLoad.Name = "ObjectHeaderLoad"
        Me.ObjectHeaderLoad.Text = "Load"
        Me.ObjectHeaderLoad.UseColumnTextForButtonValue = True
        Me.ObjectHeaderLoad.Width = 37
        '
        'ObjectExportToObj
        '
        Me.ObjectExportToObj.HeaderText = "Export Obj."
        Me.ObjectExportToObj.Name = "ObjectExportToObj"
        Me.ObjectExportToObj.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ObjectExportToObj.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ObjectExportToObj.Text = "Export"
        Me.ObjectExportToObj.UseColumnTextForButtonValue = True
        Me.ObjectExportToObj.Width = 78
        '
        'ObjectVertexCount
        '
        Me.ObjectVertexCount.FillWeight = 25.0!
        Me.ObjectVertexCount.HeaderText = "Vert Count"
        Me.ObjectVertexCount.Name = "ObjectVertexCount"
        Me.ObjectVertexCount.Width = 76
        '
        'ObjectRendered
        '
        Me.ObjectRendered.FillWeight = 30.0!
        Me.ObjectRendered.HeaderText = "Rendered"
        Me.ObjectRendered.Name = "ObjectRendered"
        Me.ObjectRendered.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ObjectRendered.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ObjectRendered.Width = 79
        '
        'ObjectHeaderFiller
        '
        Me.ObjectHeaderFiller.FillWeight = 30.0!
        Me.ObjectHeaderFiller.HeaderText = "Filler"
        Me.ObjectHeaderFiller.Name = "ObjectHeaderFiller"
        Me.ObjectHeaderFiller.Visible = False
        Me.ObjectHeaderFiller.Width = 53
        '
        'ObjectWeightNumber
        '
        Me.ObjectWeightNumber.FillWeight = 30.0!
        Me.ObjectWeightNumber.HeaderText = "Weights"
        Me.ObjectWeightNumber.Name = "ObjectWeightNumber"
        Me.ObjectWeightNumber.Width = 71
        '
        'ObjectUnknownIntA
        '
        Me.ObjectUnknownIntA.FillWeight = 30.0!
        Me.ObjectUnknownIntA.HeaderText = "IntA"
        Me.ObjectUnknownIntA.Name = "ObjectUnknownIntA"
        Me.ObjectUnknownIntA.Width = 51
        '
        'ObjectVerHeaderCount
        '
        Me.ObjectVerHeaderCount.FillWeight = 30.0!
        Me.ObjectVerHeaderCount.HeaderText = "Vert Head Count"
        Me.ObjectVerHeaderCount.Name = "ObjectVerHeaderCount"
        Me.ObjectVerHeaderCount.Width = 102
        '
        'ObjectVerticeOffset
        '
        Me.ObjectVerticeOffset.FillWeight = 50.0!
        Me.ObjectVerticeOffset.HeaderText = "Vertice Offset"
        Me.ObjectVerticeOffset.Name = "ObjectVerticeOffset"
        Me.ObjectVerticeOffset.Width = 88
        '
        'ObjectWeightsOffset
        '
        Me.ObjectWeightsOffset.FillWeight = 50.0!
        Me.ObjectWeightsOffset.HeaderText = "Weight Offset"
        Me.ObjectWeightsOffset.Name = "ObjectWeightsOffset"
        Me.ObjectWeightsOffset.Width = 89
        '
        'ObjectUVOffset
        '
        Me.ObjectUVOffset.FillWeight = 50.0!
        Me.ObjectUVOffset.HeaderText = "UV Offset"
        Me.ObjectUVOffset.Name = "ObjectUVOffset"
        Me.ObjectUVOffset.Width = 72
        '
        'ObjectNormalsOffset
        '
        Me.ObjectNormalsOffset.FillWeight = 50.0!
        Me.ObjectNormalsOffset.HeaderText = "Normals Offset"
        Me.ObjectNormalsOffset.Name = "ObjectNormalsOffset"
        Me.ObjectNormalsOffset.Width = 93
        '
        'ObjectInternalNum
        '
        Me.ObjectInternalNum.FillWeight = 25.0!
        Me.ObjectInternalNum.HeaderText = "Number"
        Me.ObjectInternalNum.Name = "ObjectInternalNum"
        Me.ObjectInternalNum.Width = 69
        '
        'ObjectHeaderShader
        '
        Me.ObjectHeaderShader.FillWeight = 30.0!
        Me.ObjectHeaderShader.HeaderText = "Shader"
        Me.ObjectHeaderShader.Name = "ObjectHeaderShader"
        Me.ObjectHeaderShader.Width = 66
        '
        'ObjecHeaderUnknownC
        '
        Me.ObjecHeaderUnknownC.FillWeight = 30.0!
        Me.ObjecHeaderUnknownC.HeaderText = "IntC"
        Me.ObjecHeaderUnknownC.Name = "ObjecHeaderUnknownC"
        Me.ObjecHeaderUnknownC.Width = 51
        '
        'ObjectMaterialIndex
        '
        Me.ObjectMaterialIndex.FillWeight = 30.0!
        Me.ObjectMaterialIndex.HeaderText = "Material"
        Me.ObjectMaterialIndex.Name = "ObjectMaterialIndex"
        Me.ObjectMaterialIndex.Width = 69
        '
        'ObjectParameterCount
        '
        Me.ObjectParameterCount.FillWeight = 30.0!
        Me.ObjectParameterCount.HeaderText = "Param Count"
        Me.ObjectParameterCount.Name = "ObjectParameterCount"
        Me.ObjectParameterCount.Width = 86
        '
        'ObjectParameterOffset
        '
        Me.ObjectParameterOffset.FillWeight = 30.0!
        Me.ObjectParameterOffset.HeaderText = "Param Offset"
        Me.ObjectParameterOffset.Name = "ObjectParameterOffset"
        Me.ObjectParameterOffset.Width = 86
        '
        'ObjectFaceOffset
        '
        Me.ObjectFaceOffset.FillWeight = 30.0!
        Me.ObjectFaceOffset.HeaderText = "Faces Offset"
        Me.ObjectFaceOffset.Name = "ObjectFaceOffset"
        Me.ObjectFaceOffset.Width = 85
        '
        'ObjectUVCount
        '
        Me.ObjectUVCount.FillWeight = 30.0!
        Me.ObjectUVCount.HeaderText = "UV Count"
        Me.ObjectUVCount.Name = "ObjectUVCount"
        Me.ObjectUVCount.Width = 72
        '
        'ObjectUnknownD
        '
        Me.ObjectUnknownD.FillWeight = 30.0!
        Me.ObjectUnknownD.HeaderText = "Unknown D"
        Me.ObjectUnknownD.Name = "ObjectUnknownD"
        Me.ObjectUnknownD.Width = 82
        '
        'ObjectUnknownE
        '
        Me.ObjectUnknownE.FillWeight = 30.0!
        Me.ObjectUnknownE.HeaderText = "Unknown E"
        Me.ObjectUnknownE.Name = "ObjectUnknownE"
        Me.ObjectUnknownE.Width = 81
        '
        'ObjectUnknownF
        '
        Me.ObjectUnknownF.FillWeight = 30.0!
        Me.ObjectUnknownF.HeaderText = "Unknown F"
        Me.ObjectUnknownF.Name = "ObjectUnknownF"
        Me.ObjectUnknownF.Width = 80
        '
        'ObjectUnknownG
        '
        Me.ObjectUnknownG.FillWeight = 30.0!
        Me.ObjectUnknownG.HeaderText = "Unknown G"
        Me.ObjectUnknownG.Name = "ObjectUnknownG"
        Me.ObjectUnknownG.Width = 82
        '
        'ObjectUnknownH
        '
        Me.ObjectUnknownH.FillWeight = 30.0!
        Me.ObjectUnknownH.HeaderText = "Unknown H"
        Me.ObjectUnknownH.Name = "ObjectUnknownH"
        Me.ObjectUnknownH.Width = 82
        '
        'ObjectBoneViewPage
        '
        Me.ObjectBoneViewPage.Controls.Add(Me.DataGridObjectBoneView)
        Me.ObjectBoneViewPage.Location = New System.Drawing.Point(4, 22)
        Me.ObjectBoneViewPage.Name = "ObjectBoneViewPage"
        Me.ObjectBoneViewPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectBoneViewPage.Size = New System.Drawing.Size(1205, 352)
        Me.ObjectBoneViewPage.TabIndex = 1
        Me.ObjectBoneViewPage.Text = "Bones"
        Me.ObjectBoneViewPage.UseVisualStyleBackColor = True
        '
        'DataGridObjectBoneView
        '
        Me.DataGridObjectBoneView.AllowUserToAddRows = False
        Me.DataGridObjectBoneView.AllowUserToDeleteRows = False
        Me.DataGridObjectBoneView.AllowUserToOrderColumns = True
        Me.DataGridObjectBoneView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectBoneView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectBoneView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectBoneCountCol, Me.ObjectBoneOrder, Me.ObjectBoneName, Me.ObjectBoneUnknownA, Me.ObjectBoneUnknownB, Me.ObjectBoneUnknownC, Me.ObjectBoneUnknownD, Me.ObjectBoneUnknownE, Me.ObjectBoneUnknownF, Me.ObjectBoneUnknownG, Me.ObjectBoneUnknownH, Me.ObjectBoneUnknownI, Me.ObjectBoneUnknownJ, Me.ObjectBoneUnknownK, Me.ObjectBoneUnknownL, Me.ObjectBoneUnknownM, Me.ObjectBoneUnknownN, Me.ObjectBoneUnknownO, Me.ObjectBoneUnknownP})
        Me.DataGridObjectBoneView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectBoneView.Location = New System.Drawing.Point(3, 3)
        Me.DataGridObjectBoneView.Name = "DataGridObjectBoneView"
        Me.DataGridObjectBoneView.RowHeadersVisible = False
        Me.DataGridObjectBoneView.Size = New System.Drawing.Size(1199, 346)
        Me.DataGridObjectBoneView.TabIndex = 0
        '
        'ObjectBoneCountCol
        '
        Me.ObjectBoneCountCol.HeaderText = "Count"
        Me.ObjectBoneCountCol.Name = "ObjectBoneCountCol"
        Me.ObjectBoneCountCol.ReadOnly = True
        '
        'ObjectBoneOrder
        '
        Me.ObjectBoneOrder.HeaderText = "Order"
        Me.ObjectBoneOrder.Name = "ObjectBoneOrder"
        '
        'ObjectBoneName
        '
        Me.ObjectBoneName.HeaderText = "Name"
        Me.ObjectBoneName.Name = "ObjectBoneName"
        '
        'ObjectBoneUnknownA
        '
        Me.ObjectBoneUnknownA.HeaderText = "A"
        Me.ObjectBoneUnknownA.Name = "ObjectBoneUnknownA"
        '
        'ObjectBoneUnknownB
        '
        Me.ObjectBoneUnknownB.HeaderText = "B"
        Me.ObjectBoneUnknownB.Name = "ObjectBoneUnknownB"
        '
        'ObjectBoneUnknownC
        '
        Me.ObjectBoneUnknownC.HeaderText = "C"
        Me.ObjectBoneUnknownC.Name = "ObjectBoneUnknownC"
        '
        'ObjectBoneUnknownD
        '
        Me.ObjectBoneUnknownD.HeaderText = "D"
        Me.ObjectBoneUnknownD.Name = "ObjectBoneUnknownD"
        '
        'ObjectBoneUnknownE
        '
        Me.ObjectBoneUnknownE.HeaderText = "E"
        Me.ObjectBoneUnknownE.Name = "ObjectBoneUnknownE"
        '
        'ObjectBoneUnknownF
        '
        Me.ObjectBoneUnknownF.HeaderText = "F"
        Me.ObjectBoneUnknownF.Name = "ObjectBoneUnknownF"
        '
        'ObjectBoneUnknownG
        '
        Me.ObjectBoneUnknownG.HeaderText = "G"
        Me.ObjectBoneUnknownG.Name = "ObjectBoneUnknownG"
        '
        'ObjectBoneUnknownH
        '
        Me.ObjectBoneUnknownH.HeaderText = "H"
        Me.ObjectBoneUnknownH.Name = "ObjectBoneUnknownH"
        '
        'ObjectBoneUnknownI
        '
        Me.ObjectBoneUnknownI.HeaderText = "I"
        Me.ObjectBoneUnknownI.Name = "ObjectBoneUnknownI"
        '
        'ObjectBoneUnknownJ
        '
        Me.ObjectBoneUnknownJ.HeaderText = "J"
        Me.ObjectBoneUnknownJ.Name = "ObjectBoneUnknownJ"
        '
        'ObjectBoneUnknownK
        '
        Me.ObjectBoneUnknownK.HeaderText = "K"
        Me.ObjectBoneUnknownK.Name = "ObjectBoneUnknownK"
        '
        'ObjectBoneUnknownL
        '
        Me.ObjectBoneUnknownL.HeaderText = "L"
        Me.ObjectBoneUnknownL.Name = "ObjectBoneUnknownL"
        '
        'ObjectBoneUnknownM
        '
        Me.ObjectBoneUnknownM.HeaderText = "M"
        Me.ObjectBoneUnknownM.Name = "ObjectBoneUnknownM"
        '
        'ObjectBoneUnknownN
        '
        Me.ObjectBoneUnknownN.HeaderText = "N"
        Me.ObjectBoneUnknownN.Name = "ObjectBoneUnknownN"
        '
        'ObjectBoneUnknownO
        '
        Me.ObjectBoneUnknownO.HeaderText = "O"
        Me.ObjectBoneUnknownO.Name = "ObjectBoneUnknownO"
        '
        'ObjectBoneUnknownP
        '
        Me.ObjectBoneUnknownP.HeaderText = "P"
        Me.ObjectBoneUnknownP.Name = "ObjectBoneUnknownP"
        '
        'ObjectTextureViewPage
        '
        Me.ObjectTextureViewPage.Controls.Add(Me.ObjectTestureSplitContainer)
        Me.ObjectTextureViewPage.Location = New System.Drawing.Point(4, 22)
        Me.ObjectTextureViewPage.Name = "ObjectTextureViewPage"
        Me.ObjectTextureViewPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectTextureViewPage.Size = New System.Drawing.Size(1205, 352)
        Me.ObjectTextureViewPage.TabIndex = 2
        Me.ObjectTextureViewPage.Text = "Textures"
        Me.ObjectTextureViewPage.UseVisualStyleBackColor = True
        '
        'ObjectTestureSplitContainer
        '
        Me.ObjectTestureSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ObjectTestureSplitContainer.Location = New System.Drawing.Point(3, 3)
        Me.ObjectTestureSplitContainer.Name = "ObjectTestureSplitContainer"
        '
        'ObjectTestureSplitContainer.Panel1
        '
        Me.ObjectTestureSplitContainer.Panel1.Controls.Add(Me.DataGridObjectTextureView)
        '
        'ObjectTestureSplitContainer.Panel2
        '
        Me.ObjectTestureSplitContainer.Panel2.Controls.Add(Me.DataGridObjectShaderView)
        Me.ObjectTestureSplitContainer.Size = New System.Drawing.Size(1199, 346)
        Me.ObjectTestureSplitContainer.SplitterDistance = 200
        Me.ObjectTestureSplitContainer.TabIndex = 0
        '
        'DataGridObjectTextureView
        '
        Me.DataGridObjectTextureView.AllowUserToAddRows = False
        Me.DataGridObjectTextureView.AllowUserToDeleteRows = False
        Me.DataGridObjectTextureView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectTextureView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectTextureView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectTextureCount, Me.ObjectTextureCol})
        Me.DataGridObjectTextureView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectTextureView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridObjectTextureView.Name = "DataGridObjectTextureView"
        Me.DataGridObjectTextureView.RowHeadersVisible = False
        Me.DataGridObjectTextureView.Size = New System.Drawing.Size(200, 346)
        Me.DataGridObjectTextureView.TabIndex = 0
        '
        'ObjectTextureCount
        '
        Me.ObjectTextureCount.FillWeight = 25.0!
        Me.ObjectTextureCount.HeaderText = "Count"
        Me.ObjectTextureCount.Name = "ObjectTextureCount"
        Me.ObjectTextureCount.ReadOnly = True
        '
        'ObjectTextureCol
        '
        Me.ObjectTextureCol.HeaderText = "Textures"
        Me.ObjectTextureCol.Name = "ObjectTextureCol"
        '
        'DataGridObjectShaderView
        '
        Me.DataGridObjectShaderView.AllowUserToAddRows = False
        Me.DataGridObjectShaderView.AllowUserToDeleteRows = False
        Me.DataGridObjectShaderView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectShaderView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectShaderView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectShaderCount, Me.ObjectShaderCol, Me.ObjectShaderType, Me.ObjectShaderB})
        Me.DataGridObjectShaderView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectShaderView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridObjectShaderView.Name = "DataGridObjectShaderView"
        Me.DataGridObjectShaderView.RowHeadersVisible = False
        Me.DataGridObjectShaderView.Size = New System.Drawing.Size(995, 346)
        Me.DataGridObjectShaderView.TabIndex = 0
        '
        'ObjectShaderCount
        '
        Me.ObjectShaderCount.FillWeight = 25.0!
        Me.ObjectShaderCount.HeaderText = "Count"
        Me.ObjectShaderCount.Name = "ObjectShaderCount"
        Me.ObjectShaderCount.ReadOnly = True
        '
        'ObjectShaderCol
        '
        Me.ObjectShaderCol.HeaderText = "Shader"
        Me.ObjectShaderCol.Name = "ObjectShaderCol"
        '
        'ObjectShaderType
        '
        Me.ObjectShaderType.HeaderText = "Type"
        Me.ObjectShaderType.Name = "ObjectShaderType"
        '
        'ObjectShaderB
        '
        Me.ObjectShaderB.HeaderText = "IntB"
        Me.ObjectShaderB.Name = "ObjectShaderB"
        '
        'ObjectVertexViewPage
        '
        Me.ObjectVertexViewPage.Controls.Add(Me.DataGridObjectVertexView)
        Me.ObjectVertexViewPage.Controls.Add(Me.MenuStripObjectVertexView)
        Me.ObjectVertexViewPage.Location = New System.Drawing.Point(4, 22)
        Me.ObjectVertexViewPage.Name = "ObjectVertexViewPage"
        Me.ObjectVertexViewPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectVertexViewPage.Size = New System.Drawing.Size(1205, 352)
        Me.ObjectVertexViewPage.TabIndex = 3
        Me.ObjectVertexViewPage.Text = "Vertices"
        Me.ObjectVertexViewPage.UseVisualStyleBackColor = True
        '
        'DataGridObjectVertexView
        '
        Me.DataGridObjectVertexView.AllowUserToAddRows = False
        Me.DataGridObjectVertexView.AllowUserToDeleteRows = False
        Me.DataGridObjectVertexView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectVertexView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectVertexView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectVertCountCol, Me.ObjectVertX, Me.ObjectVertY, Me.ObjectVertZ, Me.ObjectVertRX, Me.ObjectVertRY, Me.ObjectVertRZ, Me.ObjectVertWeight, Me.ObjectVertU, Me.ObjectVertV, Me.ObjectVertNormal1, Me.ObjectVertNormal2, Me.ObjectVertNormal3})
        Me.DataGridObjectVertexView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectVertexView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridObjectVertexView.Name = "DataGridObjectVertexView"
        Me.DataGridObjectVertexView.RowHeadersVisible = False
        Me.DataGridObjectVertexView.Size = New System.Drawing.Size(1199, 322)
        Me.DataGridObjectVertexView.TabIndex = 0
        '
        'ObjectVertCountCol
        '
        Me.ObjectVertCountCol.HeaderText = "Count"
        Me.ObjectVertCountCol.Name = "ObjectVertCountCol"
        Me.ObjectVertCountCol.ReadOnly = True
        '
        'ObjectVertX
        '
        Me.ObjectVertX.HeaderText = "X"
        Me.ObjectVertX.Name = "ObjectVertX"
        '
        'ObjectVertY
        '
        Me.ObjectVertY.HeaderText = "Y"
        Me.ObjectVertY.Name = "ObjectVertY"
        '
        'ObjectVertZ
        '
        Me.ObjectVertZ.HeaderText = "Z"
        Me.ObjectVertZ.Name = "ObjectVertZ"
        '
        'ObjectVertRX
        '
        Me.ObjectVertRX.HeaderText = "RX?"
        Me.ObjectVertRX.Name = "ObjectVertRX"
        '
        'ObjectVertRY
        '
        Me.ObjectVertRY.HeaderText = "RY?"
        Me.ObjectVertRY.Name = "ObjectVertRY"
        '
        'ObjectVertRZ
        '
        Me.ObjectVertRZ.HeaderText = "RZ?"
        Me.ObjectVertRZ.Name = "ObjectVertRZ"
        '
        'ObjectVertWeight
        '
        Me.ObjectVertWeight.HeaderText = "Weight"
        Me.ObjectVertWeight.Name = "ObjectVertWeight"
        Me.ObjectVertWeight.ReadOnly = True
        '
        'ObjectVertU
        '
        Me.ObjectVertU.HeaderText = "U"
        Me.ObjectVertU.Name = "ObjectVertU"
        '
        'ObjectVertV
        '
        Me.ObjectVertV.HeaderText = "V"
        Me.ObjectVertV.Name = "ObjectVertV"
        '
        'ObjectVertNormal1
        '
        Me.ObjectVertNormal1.HeaderText = "Normal 1"
        Me.ObjectVertNormal1.Name = "ObjectVertNormal1"
        '
        'ObjectVertNormal2
        '
        Me.ObjectVertNormal2.HeaderText = "Normal 2"
        Me.ObjectVertNormal2.Name = "ObjectVertNormal2"
        '
        'ObjectVertNormal3
        '
        Me.ObjectVertNormal3.HeaderText = "Normal 3"
        Me.ObjectVertNormal3.Name = "ObjectVertNormal3"
        '
        'MenuStripObjectVertexView
        '
        Me.MenuStripObjectVertexView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowNormalsToolStripMenuItem, Me.ShowWeightsToolStripMenuItem})
        Me.MenuStripObjectVertexView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripObjectVertexView.Name = "MenuStripObjectVertexView"
        Me.MenuStripObjectVertexView.Size = New System.Drawing.Size(1199, 24)
        Me.MenuStripObjectVertexView.TabIndex = 1
        Me.MenuStripObjectVertexView.Text = "MenuStrip1"
        '
        'ShowNormalsToolStripMenuItem
        '
        Me.ShowNormalsToolStripMenuItem.Name = "ShowNormalsToolStripMenuItem"
        Me.ShowNormalsToolStripMenuItem.Size = New System.Drawing.Size(111, 20)
        Me.ShowNormalsToolStripMenuItem.Text = "☑ Show Normals"
        '
        'ShowWeightsToolStripMenuItem
        '
        Me.ShowWeightsToolStripMenuItem.Name = "ShowWeightsToolStripMenuItem"
        Me.ShowWeightsToolStripMenuItem.Size = New System.Drawing.Size(109, 20)
        Me.ShowWeightsToolStripMenuItem.Text = "☑ Show Weights"
        '
        'ObjectFacesViewPage
        '
        Me.ObjectFacesViewPage.Controls.Add(Me.SplitContainer1)
        Me.ObjectFacesViewPage.Location = New System.Drawing.Point(4, 22)
        Me.ObjectFacesViewPage.Name = "ObjectFacesViewPage"
        Me.ObjectFacesViewPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectFacesViewPage.Size = New System.Drawing.Size(1205, 352)
        Me.ObjectFacesViewPage.TabIndex = 4
        Me.ObjectFacesViewPage.Text = "Faces"
        Me.ObjectFacesViewPage.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridObjectTriStripsView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridObjectFacesView)
        Me.SplitContainer1.Size = New System.Drawing.Size(1199, 346)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.TabIndex = 1
        '
        'DataGridObjectTriStripsView
        '
        Me.DataGridObjectTriStripsView.AllowUserToAddRows = False
        Me.DataGridObjectTriStripsView.AllowUserToDeleteRows = False
        Me.DataGridObjectTriStripsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectTriStripsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectTriStripsView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectTriStripNum, Me.ObjectTriStripVerts, Me.ObjectTriStripVertCount})
        Me.DataGridObjectTriStripsView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectTriStripsView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridObjectTriStripsView.Name = "DataGridObjectTriStripsView"
        Me.DataGridObjectTriStripsView.RowHeadersVisible = False
        Me.DataGridObjectTriStripsView.Size = New System.Drawing.Size(399, 346)
        Me.DataGridObjectTriStripsView.TabIndex = 1
        '
        'ObjectTriStripNum
        '
        Me.ObjectTriStripNum.FillWeight = 50.0!
        Me.ObjectTriStripNum.HeaderText = "Strip#"
        Me.ObjectTriStripNum.Name = "ObjectTriStripNum"
        '
        'ObjectTriStripVerts
        '
        Me.ObjectTriStripVerts.HeaderText = "Verts"
        Me.ObjectTriStripVerts.Name = "ObjectTriStripVerts"
        '
        'ObjectTriStripVertCount
        '
        Me.ObjectTriStripVertCount.HeaderText = "Count"
        Me.ObjectTriStripVertCount.Name = "ObjectTriStripVertCount"
        '
        'DataGridObjectFacesView
        '
        Me.DataGridObjectFacesView.AllowUserToAddRows = False
        Me.DataGridObjectFacesView.AllowUserToDeleteRows = False
        Me.DataGridObjectFacesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectFacesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectFacesView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectFaceCurrentCountCol, Me.ObjectFaceVertex1, Me.ObjectFaceVertex2, Me.ObjectFaceVertex3})
        Me.DataGridObjectFacesView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectFacesView.Location = New System.Drawing.Point(0, 0)
        Me.DataGridObjectFacesView.Name = "DataGridObjectFacesView"
        Me.DataGridObjectFacesView.RowHeadersVisible = False
        Me.DataGridObjectFacesView.Size = New System.Drawing.Size(796, 346)
        Me.DataGridObjectFacesView.TabIndex = 0
        '
        'ObjectFaceCurrentCountCol
        '
        Me.ObjectFaceCurrentCountCol.FillWeight = 50.0!
        Me.ObjectFaceCurrentCountCol.HeaderText = "Face #"
        Me.ObjectFaceCurrentCountCol.Name = "ObjectFaceCurrentCountCol"
        Me.ObjectFaceCurrentCountCol.ReadOnly = True
        '
        'ObjectFaceVertex1
        '
        Me.ObjectFaceVertex1.HeaderText = "Vert 1"
        Me.ObjectFaceVertex1.Name = "ObjectFaceVertex1"
        Me.ObjectFaceVertex1.ReadOnly = True
        '
        'ObjectFaceVertex2
        '
        Me.ObjectFaceVertex2.HeaderText = "Vert 2"
        Me.ObjectFaceVertex2.Name = "ObjectFaceVertex2"
        Me.ObjectFaceVertex2.ReadOnly = True
        '
        'ObjectFaceVertex3
        '
        Me.ObjectFaceVertex3.HeaderText = "Vert 3"
        Me.ObjectFaceVertex3.Name = "ObjectFaceVertex3"
        Me.ObjectFaceVertex3.ReadOnly = True
        '
        'ObjectParamViewPage
        '
        Me.ObjectParamViewPage.Controls.Add(Me.DataGridObjectParamView)
        Me.ObjectParamViewPage.Location = New System.Drawing.Point(4, 22)
        Me.ObjectParamViewPage.Name = "ObjectParamViewPage"
        Me.ObjectParamViewPage.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjectParamViewPage.Size = New System.Drawing.Size(1205, 352)
        Me.ObjectParamViewPage.TabIndex = 5
        Me.ObjectParamViewPage.Text = "Parameters"
        Me.ObjectParamViewPage.UseVisualStyleBackColor = True
        '
        'DataGridObjectParamView
        '
        Me.DataGridObjectParamView.AllowUserToAddRows = False
        Me.DataGridObjectParamView.AllowUserToDeleteRows = False
        Me.DataGridObjectParamView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridObjectParamView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjectParamView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ObjectParamCountCol, Me.ObjectParamName, Me.ObjectParamInt1, Me.ObjectParamInt2, Me.ObjectParamSingle})
        Me.DataGridObjectParamView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjectParamView.Location = New System.Drawing.Point(3, 3)
        Me.DataGridObjectParamView.Name = "DataGridObjectParamView"
        Me.DataGridObjectParamView.RowHeadersVisible = False
        Me.DataGridObjectParamView.Size = New System.Drawing.Size(1199, 346)
        Me.DataGridObjectParamView.TabIndex = 0
        '
        'ObjectParamCountCol
        '
        Me.ObjectParamCountCol.HeaderText = "Param #"
        Me.ObjectParamCountCol.Name = "ObjectParamCountCol"
        Me.ObjectParamCountCol.ReadOnly = True
        '
        'ObjectParamName
        '
        Me.ObjectParamName.HeaderText = "Name"
        Me.ObjectParamName.Name = "ObjectParamName"
        '
        'ObjectParamInt1
        '
        Me.ObjectParamInt1.HeaderText = "Int 1"
        Me.ObjectParamInt1.Name = "ObjectParamInt1"
        '
        'ObjectParamInt2
        '
        Me.ObjectParamInt2.HeaderText = "Int 2"
        Me.ObjectParamInt2.Name = "ObjectParamInt2"
        '
        'ObjectParamSingle
        '
        Me.ObjectParamSingle.HeaderText = "Percent?"
        Me.ObjectParamSingle.Name = "ObjectParamSingle"
        '
        'MenuStripObjectView
        '
        Me.MenuStripObjectView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadedObjectToolStripMenuItem, Me.ObjectEmoteListComboBox})
        Me.MenuStripObjectView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripObjectView.Name = "MenuStripObjectView"
        Me.MenuStripObjectView.Size = New System.Drawing.Size(1213, 27)
        Me.MenuStripObjectView.TabIndex = 0
        Me.MenuStripObjectView.Text = "MenuStrip1"
        '
        'LoadedObjectToolStripMenuItem
        '
        Me.LoadedObjectToolStripMenuItem.Name = "LoadedObjectToolStripMenuItem"
        Me.LoadedObjectToolStripMenuItem.Size = New System.Drawing.Size(102, 23)
        Me.LoadedObjectToolStripMenuItem.Text = "Loaded Object: "
        '
        'ObjectEmoteListComboBox
        '
        Me.ObjectEmoteListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ObjectEmoteListComboBox.Name = "ObjectEmoteListComboBox"
        Me.ObjectEmoteListComboBox.Size = New System.Drawing.Size(121, 23)
        '
        'AttireView
        '
        Me.AttireView.Controls.Add(Me.DataGridAttireView)
        Me.AttireView.Controls.Add(Me.MenuStripAttireView)
        Me.AttireView.Location = New System.Drawing.Point(4, 22)
        Me.AttireView.Name = "AttireView"
        Me.AttireView.Padding = New System.Windows.Forms.Padding(3)
        Me.AttireView.Size = New System.Drawing.Size(1219, 411)
        Me.AttireView.TabIndex = 9
        Me.AttireView.Text = "Attire View"
        Me.AttireView.UseVisualStyleBackColor = True
        '
        'DataGridAttireView
        '
        Me.DataGridAttireView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridAttireView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAttireView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Pach, Me.Count, Me.Attire0Ref, Me.Attire0String, Me.Attire0Enabled, Me.Attire0Manager, Me.Attire0Unlock, Me.Attire1Ref, Me.Attire1String, Me.Attire1Enabled, Me.Attire1Manager, Me.Attire1Unlock, Me.Attire2Ref, Me.Attire2String, Me.Attire2Enabled, Me.Attire2Manager, Me.Attire2Unlock, Me.Attire3Ref, Me.Attire3String, Me.Attire3Enabled, Me.Attire3Manager, Me.Attire3Unlock, Me.Attire4Ref, Me.Attire4String, Me.Attire4Enabled, Me.Attire4Manager, Me.Attire4Unlock, Me.Attire5Ref, Me.Attire5String, Me.Attire5Enabled, Me.Attire5Manager, Me.Attire5Unlock, Me.Attire6Ref, Me.Attire6String, Me.Attire6Enabled, Me.Attire6Manager, Me.Attire6Unlock, Me.Attire7Ref, Me.Attire7String, Me.Attire7Enabled, Me.Attire7Manager, Me.Attire7Unlock, Me.Attire8Ref, Me.Attire8String, Me.Attire8Enabled, Me.Attire8Manager, Me.Attire8Unlock, Me.Attire9Ref, Me.Attire9String, Me.Attire9Enabled, Me.Attire9Manager, Me.Attire9Unlock})
        Me.DataGridAttireView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAttireView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridAttireView.Name = "DataGridAttireView"
        Me.DataGridAttireView.Size = New System.Drawing.Size(1213, 381)
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
        Me.MenuStripAttireView.Size = New System.Drawing.Size(1213, 24)
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
        Me.MuscleView.Size = New System.Drawing.Size(1219, 411)
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
        Me.DataGridMuscleView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridMuscleView.TabIndex = 2
        '
        'MenuStripMuscleView
        '
        Me.MenuStripMuscleView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseToolStripMenuItem})
        Me.MenuStripMuscleView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMuscleView.Name = "MenuStripMuscleView"
        Me.MenuStripMuscleView.Size = New System.Drawing.Size(1213, 24)
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
        Me.MaskView.Size = New System.Drawing.Size(1219, 411)
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
        Me.DataGridMaskView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridMaskView.TabIndex = 2
        '
        'Mask_Name
        '
        Me.Mask_Name.HeaderText = "Mask_Name"
        Me.Mask_Name.Name = "Mask_Name"
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
        Me.Add_Mask.Text = "Add"
        Me.Add_Mask.UseColumnTextForButtonValue = True
        '
        'Del_Mask
        '
        Me.Del_Mask.HeaderText = "Delete"
        Me.Del_Mask.Name = "Del_Mask"
        Me.Del_Mask.Text = "Delete"
        Me.Del_Mask.UseColumnTextForButtonValue = True
        '
        'MenuStripMaskView
        '
        Me.MenuStripMaskView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportMasksFromTXTToolStripMenuItem, Me.ExportMaskstoTXTToolStripMenuItem, Me.SaveChangesMaskMenuItem, Me.ToolStripMenuItem2})
        Me.MenuStripMaskView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripMaskView.Name = "MenuStripMaskView"
        Me.MenuStripMaskView.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripMaskView.TabIndex = 0
        Me.MenuStripMaskView.Text = "MenuStrip2"
        '
        'ImportMasksFromTXTToolStripMenuItem
        '
        Me.ImportMasksFromTXTToolStripMenuItem.Name = "ImportMasksFromTXTToolStripMenuItem"
        Me.ImportMasksFromTXTToolStripMenuItem.Size = New System.Drawing.Size(142, 20)
        Me.ImportMasksFromTXTToolStripMenuItem.Text = "Import Masks from TXT"
        '
        'ExportMaskstoTXTToolStripMenuItem
        '
        Me.ExportMaskstoTXTToolStripMenuItem.Name = "ExportMaskstoTXTToolStripMenuItem"
        Me.ExportMaskstoTXTToolStripMenuItem.Size = New System.Drawing.Size(125, 20)
        Me.ExportMaskstoTXTToolStripMenuItem.Text = "Export Masks to TXT"
        '
        'SaveChangesMaskMenuItem
        '
        Me.SaveChangesMaskMenuItem.Name = "SaveChangesMaskMenuItem"
        Me.SaveChangesMaskMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesMaskMenuItem.Text = "Save Changes"
        Me.SaveChangesMaskMenuItem.Visible = False
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TekkenToolStripMenuItem, Me.AznTutorialToolStripMenuItem})
        Me.ToolStripMenuItem2.Image = CType(resources.GetObject("ToolStripMenuItem2.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(28, 20)
        '
        'TekkenToolStripMenuItem
        '
        Me.TekkenToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TutorialVideoToolStripMenuItem, Me.DSImportScriptToolStripMenuItem, Me.DSSelectionScriptToolStripMenuItem, Me.DSExportScriptToolStripMenuItem})
        Me.TekkenToolStripMenuItem.Name = "TekkenToolStripMenuItem"
        Me.TekkenToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.TekkenToolStripMenuItem.Text = "Tekken"
        '
        'TutorialVideoToolStripMenuItem
        '
        Me.TutorialVideoToolStripMenuItem.Name = "TutorialVideoToolStripMenuItem"
        Me.TutorialVideoToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.TutorialVideoToolStripMenuItem.Text = "Tutorial Video"
        '
        'DSImportScriptToolStripMenuItem
        '
        Me.DSImportScriptToolStripMenuItem.Name = "DSImportScriptToolStripMenuItem"
        Me.DSImportScriptToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DSImportScriptToolStripMenuItem.Text = "3DS Import Script"
        '
        'DSSelectionScriptToolStripMenuItem
        '
        Me.DSSelectionScriptToolStripMenuItem.Name = "DSSelectionScriptToolStripMenuItem"
        Me.DSSelectionScriptToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DSSelectionScriptToolStripMenuItem.Text = "3DS Selection Script"
        '
        'DSExportScriptToolStripMenuItem
        '
        Me.DSExportScriptToolStripMenuItem.Name = "DSExportScriptToolStripMenuItem"
        Me.DSExportScriptToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.DSExportScriptToolStripMenuItem.Text = "3DS Export Script"
        '
        'AznTutorialToolStripMenuItem
        '
        Me.AznTutorialToolStripMenuItem.Name = "AznTutorialToolStripMenuItem"
        Me.AznTutorialToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.AznTutorialToolStripMenuItem.Text = "Azn Tutorial"
        '
        'ObjArrayView
        '
        Me.ObjArrayView.Controls.Add(Me.DataGridObjArrayView)
        Me.ObjArrayView.Controls.Add(Me.MenuStripObjectArrayView)
        Me.ObjArrayView.Location = New System.Drawing.Point(4, 22)
        Me.ObjArrayView.Name = "ObjArrayView"
        Me.ObjArrayView.Padding = New System.Windows.Forms.Padding(3)
        Me.ObjArrayView.Size = New System.Drawing.Size(1219, 411)
        Me.ObjArrayView.TabIndex = 12
        Me.ObjArrayView.Text = "Object Array View"
        Me.ObjArrayView.UseVisualStyleBackColor = True
        '
        'DataGridObjArrayView
        '
        Me.DataGridObjArrayView.AllowUserToAddRows = False
        Me.DataGridObjArrayView.AllowUserToDeleteRows = False
        Me.DataGridObjArrayView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridObjArrayView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridObjArrayView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.YobjArryIndex, Me.ObjArrayParent, Me.Number, Me.ArrEnabled, Me.ChairName, Me.X, Me.Y, Me.Z, Me.RX, Me.RY, Me.RZ, Me.ContainedYobjArray, Me.StartIndexYobjArray, Me.Add_Obj_Array, Me.Del_Obj_Array})
        Me.DataGridObjArrayView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridObjArrayView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridObjArrayView.Name = "DataGridObjArrayView"
        Me.DataGridObjArrayView.RowHeadersVisible = False
        Me.DataGridObjArrayView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridObjArrayView.TabIndex = 1
        '
        'YobjArryIndex
        '
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.YobjArryIndex.DefaultCellStyle = DataGridViewCellStyle1
        Me.YobjArryIndex.HeaderText = "Index"
        Me.YobjArryIndex.Name = "YobjArryIndex"
        Me.YobjArryIndex.ReadOnly = True
        Me.YobjArryIndex.Width = 58
        '
        'ObjArrayParent
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ObjArrayParent.DefaultCellStyle = DataGridViewCellStyle2
        Me.ObjArrayParent.HeaderText = "Parent"
        Me.ObjArrayParent.MinimumWidth = 100
        Me.ObjArrayParent.Name = "ObjArrayParent"
        Me.ObjArrayParent.ReadOnly = True
        '
        'Number
        '
        Me.Number.HeaderText = "Number"
        Me.Number.Name = "Number"
        Me.Number.Width = 69
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
        Me.ChairName.MaxInputLength = 16
        Me.ChairName.MinimumWidth = 100
        Me.ChairName.Name = "ChairName"
        Me.ChairName.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ChairName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'X
        '
        Me.X.HeaderText = "X"
        Me.X.MinimumWidth = 60
        Me.X.Name = "X"
        Me.X.Width = 60
        '
        'Y
        '
        Me.Y.HeaderText = "Y"
        Me.Y.MinimumWidth = 60
        Me.Y.Name = "Y"
        Me.Y.Width = 60
        '
        'Z
        '
        Me.Z.HeaderText = "Z"
        Me.Z.MinimumWidth = 60
        Me.Z.Name = "Z"
        Me.Z.Width = 60
        '
        'RX
        '
        Me.RX.HeaderText = "RX"
        Me.RX.MinimumWidth = 60
        Me.RX.Name = "RX"
        Me.RX.Width = 60
        '
        'RY
        '
        Me.RY.HeaderText = "RY"
        Me.RY.MinimumWidth = 60
        Me.RY.Name = "RY"
        Me.RY.Width = 60
        '
        'RZ
        '
        Me.RZ.HeaderText = "RZ"
        Me.RZ.MinimumWidth = 60
        Me.RZ.Name = "RZ"
        Me.RZ.Width = 60
        '
        'ContainedYobjArray
        '
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ContainedYobjArray.DefaultCellStyle = DataGridViewCellStyle3
        Me.ContainedYobjArray.HeaderText = "Objects"
        Me.ContainedYobjArray.Name = "ContainedYobjArray"
        Me.ContainedYobjArray.ReadOnly = True
        Me.ContainedYobjArray.Width = 68
        '
        'StartIndexYobjArray
        '
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.StartIndexYobjArray.DefaultCellStyle = DataGridViewCellStyle4
        Me.StartIndexYobjArray.HeaderText = "Start Index"
        Me.StartIndexYobjArray.Name = "StartIndexYobjArray"
        Me.StartIndexYobjArray.ReadOnly = True
        Me.StartIndexYobjArray.Width = 83
        '
        'Add_Obj_Array
        '
        Me.Add_Obj_Array.HeaderText = "Add"
        Me.Add_Obj_Array.MinimumWidth = 100
        Me.Add_Obj_Array.Name = "Add_Obj_Array"
        Me.Add_Obj_Array.Text = "Add"
        Me.Add_Obj_Array.UseColumnTextForButtonValue = True
        '
        'Del_Obj_Array
        '
        Me.Del_Obj_Array.HeaderText = "Delete"
        Me.Del_Obj_Array.MinimumWidth = 100
        Me.Del_Obj_Array.Name = "Del_Obj_Array"
        Me.Del_Obj_Array.Text = "Delete"
        Me.Del_Obj_Array.UseColumnTextForButtonValue = True
        '
        'MenuStripObjectArrayView
        '
        Me.MenuStripObjectArrayView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportYOBJArrayFromCSVToolStripMenuItem, Me.ExportYOBJArrayToCSVToolStripMenuItem, Me.SaveChangesYOBJArrayMenuItem})
        Me.MenuStripObjectArrayView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripObjectArrayView.Name = "MenuStripObjectArrayView"
        Me.MenuStripObjectArrayView.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripObjectArrayView.TabIndex = 0
        Me.MenuStripObjectArrayView.Text = "MenuStrip2"
        '
        'ImportYOBJArrayFromCSVToolStripMenuItem
        '
        Me.ImportYOBJArrayFromCSVToolStripMenuItem.Name = "ImportYOBJArrayFromCSVToolStripMenuItem"
        Me.ImportYOBJArrayFromCSVToolStripMenuItem.Size = New System.Drawing.Size(108, 20)
        Me.ImportYOBJArrayFromCSVToolStripMenuItem.Text = "Import from CSV"
        '
        'ExportYOBJArrayToCSVToolStripMenuItem
        '
        Me.ExportYOBJArrayToCSVToolStripMenuItem.Name = "ExportYOBJArrayToCSVToolStripMenuItem"
        Me.ExportYOBJArrayToCSVToolStripMenuItem.Size = New System.Drawing.Size(91, 20)
        Me.ExportYOBJArrayToCSVToolStripMenuItem.Text = "Export to CSV"
        '
        'SaveChangesYOBJArrayMenuItem
        '
        Me.SaveChangesYOBJArrayMenuItem.Name = "SaveChangesYOBJArrayMenuItem"
        Me.SaveChangesYOBJArrayMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesYOBJArrayMenuItem.Text = "Save Changes"
        Me.SaveChangesYOBJArrayMenuItem.Visible = False
        '
        'AssetView
        '
        Me.AssetView.Controls.Add(Me.DataGridAssetView)
        Me.AssetView.Controls.Add(Me.MenuStripAssetView)
        Me.AssetView.Location = New System.Drawing.Point(4, 22)
        Me.AssetView.Name = "AssetView"
        Me.AssetView.Padding = New System.Windows.Forms.Padding(3)
        Me.AssetView.Size = New System.Drawing.Size(1219, 411)
        Me.AssetView.TabIndex = 13
        Me.AssetView.Text = "Asset View"
        Me.AssetView.UseVisualStyleBackColor = True
        '
        'DataGridAssetView
        '
        Me.DataGridAssetView.AllowUserToAddRows = False
        Me.DataGridAssetView.AllowUserToDeleteRows = False
        Me.DataGridAssetView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridAssetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAssetView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PacNumber, Me.AttireNum, Me.AudioNum, Me.Check2, Me.MUSOffset, Me.EVTOffset, Me.MusicID, Me.TitantronNum, Me.HeaderNum, Me.WallNum, Me.RampNum, Me.WallRightNum, Me.WallLeftNum, Me.RawTronEnabled, Me.SmackDownTronEnabled, Me.ClassicTronEnabled, Me.Check5, Me.Check6, Me.MUSFileName, Me.EVTFileName, Me.AddAsset, Me.DeleteAsset})
        Me.DataGridAssetView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAssetView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridAssetView.Name = "DataGridAssetView"
        Me.DataGridAssetView.RowHeadersVisible = False
        Me.DataGridAssetView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridAssetView.TabIndex = 0
        '
        'PacNumber
        '
        Me.PacNumber.HeaderText = "PacNumber"
        Me.PacNumber.MaxInputLength = 11
        Me.PacNumber.Name = "PacNumber"
        Me.PacNumber.Width = 88
        '
        'AttireNum
        '
        Me.AttireNum.HeaderText = "AttireNum"
        Me.AttireNum.MaxInputLength = 11
        Me.AttireNum.Name = "AttireNum"
        Me.AttireNum.Width = 78
        '
        'AudioNum
        '
        Me.AudioNum.HeaderText = "AudioNum"
        Me.AudioNum.MaxInputLength = 11
        Me.AudioNum.Name = "AudioNum"
        Me.AudioNum.Width = 81
        '
        'Check2
        '
        Me.Check2.HeaderText = "Check2"
        Me.Check2.MaxInputLength = 11
        Me.Check2.Name = "Check2"
        Me.Check2.Width = 69
        '
        'MUSOffset
        '
        Me.MUSOffset.HeaderText = "MUS Offset"
        Me.MUSOffset.MaxInputLength = 11
        Me.MUSOffset.Name = "MUSOffset"
        Me.MUSOffset.Width = 87
        '
        'EVTOffset
        '
        Me.EVTOffset.HeaderText = "EVT Offset"
        Me.EVTOffset.MaxInputLength = 11
        Me.EVTOffset.Name = "EVTOffset"
        Me.EVTOffset.Width = 84
        '
        'MusicID
        '
        Me.MusicID.HeaderText = "Music ID"
        Me.MusicID.MaxInputLength = 11
        Me.MusicID.Name = "MusicID"
        Me.MusicID.Width = 74
        '
        'TitantronNum
        '
        Me.TitantronNum.HeaderText = "Titantron Num"
        Me.TitantronNum.MaxInputLength = 11
        Me.TitantronNum.Name = "TitantronNum"
        Me.TitantronNum.Width = 99
        '
        'HeaderNum
        '
        Me.HeaderNum.HeaderText = "HeaderNum"
        Me.HeaderNum.MaxInputLength = 11
        Me.HeaderNum.Name = "HeaderNum"
        Me.HeaderNum.Width = 89
        '
        'WallNum
        '
        Me.WallNum.HeaderText = "WallNum"
        Me.WallNum.MaxInputLength = 11
        Me.WallNum.Name = "WallNum"
        Me.WallNum.Width = 75
        '
        'RampNum
        '
        Me.RampNum.HeaderText = "RampNum"
        Me.RampNum.MaxInputLength = 11
        Me.RampNum.Name = "RampNum"
        Me.RampNum.Width = 82
        '
        'WallRightNum
        '
        Me.WallRightNum.HeaderText = "WallRightNum"
        Me.WallRightNum.MaxInputLength = 11
        Me.WallRightNum.Name = "WallRightNum"
        '
        'WallLeftNum
        '
        Me.WallLeftNum.HeaderText = "WallLeftNum"
        Me.WallLeftNum.MaxInputLength = 11
        Me.WallLeftNum.Name = "WallLeftNum"
        Me.WallLeftNum.Width = 93
        '
        'RawTronEnabled
        '
        Me.RawTronEnabled.HeaderText = "Raw Tron"
        Me.RawTronEnabled.Name = "RawTronEnabled"
        Me.RawTronEnabled.Width = 60
        '
        'SmackDownTronEnabled
        '
        Me.SmackDownTronEnabled.HeaderText = "SD Tron"
        Me.SmackDownTronEnabled.Name = "SmackDownTronEnabled"
        Me.SmackDownTronEnabled.Width = 53
        '
        'ClassicTronEnabled
        '
        Me.ClassicTronEnabled.HeaderText = "Classic Tron"
        Me.ClassicTronEnabled.Name = "ClassicTronEnabled"
        Me.ClassicTronEnabled.Width = 71
        '
        'Check5
        '
        Me.Check5.HeaderText = "Check5"
        Me.Check5.MaxInputLength = 11
        Me.Check5.Name = "Check5"
        Me.Check5.Width = 69
        '
        'Check6
        '
        Me.Check6.HeaderText = "Check6"
        Me.Check6.MaxInputLength = 11
        Me.Check6.Name = "Check6"
        Me.Check6.Width = 69
        '
        'MUSFileName
        '
        Me.MUSFileName.HeaderText = "MUS Name"
        Me.MUSFileName.MaxInputLength = 16
        Me.MUSFileName.Name = "MUSFileName"
        Me.MUSFileName.Width = 87
        '
        'EVTFileName
        '
        Me.EVTFileName.HeaderText = "EVT Name"
        Me.EVTFileName.MaxInputLength = 16
        Me.EVTFileName.Name = "EVTFileName"
        Me.EVTFileName.Width = 84
        '
        'AddAsset
        '
        Me.AddAsset.HeaderText = "Add"
        Me.AddAsset.Name = "AddAsset"
        Me.AddAsset.Text = "Add"
        Me.AddAsset.UseColumnTextForButtonValue = True
        Me.AddAsset.Width = 32
        '
        'DeleteAsset
        '
        Me.DeleteAsset.HeaderText = "Delete"
        Me.DeleteAsset.Name = "DeleteAsset"
        Me.DeleteAsset.Text = "Delete"
        Me.DeleteAsset.UseColumnTextForButtonValue = True
        Me.DeleteAsset.Width = 44
        '
        'MenuStripAssetView
        '
        Me.MenuStripAssetView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveChangesAssetViewMenuItem})
        Me.MenuStripAssetView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripAssetView.Name = "MenuStripAssetView"
        Me.MenuStripAssetView.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripAssetView.TabIndex = 1
        Me.MenuStripAssetView.Text = "MenuStrip1"
        '
        'SaveChangesAssetViewMenuItem
        '
        Me.SaveChangesAssetViewMenuItem.Name = "SaveChangesAssetViewMenuItem"
        Me.SaveChangesAssetViewMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesAssetViewMenuItem.Text = "Save Changes"
        Me.SaveChangesAssetViewMenuItem.Visible = False
        '
        'TitleView
        '
        Me.TitleView.Controls.Add(Me.DataGridTitleView)
        Me.TitleView.Controls.Add(Me.MenuStripTitleView)
        Me.TitleView.Location = New System.Drawing.Point(4, 22)
        Me.TitleView.Name = "TitleView"
        Me.TitleView.Padding = New System.Windows.Forms.Padding(3)
        Me.TitleView.Size = New System.Drawing.Size(1219, 411)
        Me.TitleView.TabIndex = 14
        Me.TitleView.Text = "Title View"
        Me.TitleView.UseVisualStyleBackColor = True
        '
        'DataGridTitleView
        '
        Me.DataGridTitleView.AllowUserToAddRows = False
        Me.DataGridTitleView.AllowUserToDeleteRows = False
        Me.DataGridTitleView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridTitleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridTitleView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TitleEnabled, Me.PropRef, Me.MenuNumber, Me.TitleNameNum1, Me.TitleNameNum1Full, Me.TitleNameNum2, Me.TitleNameNum2Full, Me.TitleNameNum3, Me.TitleNameNum3Full, Me.MyWWE1, Me.MyWWE1Name, Me.MyWWE2, Me.MyWWE2Name, Me.TitleUni1, Me.UniTitle1Name, Me.TitleUni2, Me.UniTitle2Name, Me.TitleTemp1, Me.Temp2, Me.TitleFemale, Me.TitleTagTeam, Me.TitleCruiserweight, Me.UnlockNum, Me.TitleTemp4})
        Me.DataGridTitleView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridTitleView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridTitleView.Name = "DataGridTitleView"
        Me.DataGridTitleView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridTitleView.Size = New System.Drawing.Size(1213, 378)
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
        'TitleNameNum1
        '
        Me.TitleNameNum1.HeaderText = "Name1Ref"
        Me.TitleNameNum1.MaxInputLength = 8
        Me.TitleNameNum1.Name = "TitleNameNum1"
        Me.TitleNameNum1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleNameNum1.Width = 64
        '
        'TitleNameNum1Full
        '
        Me.TitleNameNum1Full.HeaderText = "Name1Full"
        Me.TitleNameNum1Full.Name = "TitleNameNum1Full"
        Me.TitleNameNum1Full.ReadOnly = True
        Me.TitleNameNum1Full.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleNameNum1Full.Width = 63
        '
        'TitleNameNum2
        '
        Me.TitleNameNum2.HeaderText = "Name2Ref"
        Me.TitleNameNum2.MaxInputLength = 8
        Me.TitleNameNum2.Name = "TitleNameNum2"
        Me.TitleNameNum2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleNameNum2.Width = 64
        '
        'TitleNameNum2Full
        '
        Me.TitleNameNum2Full.HeaderText = "Name2Full"
        Me.TitleNameNum2Full.Name = "TitleNameNum2Full"
        Me.TitleNameNum2Full.ReadOnly = True
        Me.TitleNameNum2Full.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleNameNum2Full.Width = 63
        '
        'TitleNameNum3
        '
        Me.TitleNameNum3.HeaderText = "Name3Ref"
        Me.TitleNameNum3.MaxInputLength = 8
        Me.TitleNameNum3.Name = "TitleNameNum3"
        Me.TitleNameNum3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleNameNum3.Width = 64
        '
        'TitleNameNum3Full
        '
        Me.TitleNameNum3Full.HeaderText = "Name3Full"
        Me.TitleNameNum3Full.Name = "TitleNameNum3Full"
        Me.TitleNameNum3Full.ReadOnly = True
        Me.TitleNameNum3Full.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleNameNum3Full.Width = 63
        '
        'MyWWE1
        '
        Me.MyWWE1.HeaderText = "MyWWE1"
        Me.MyWWE1.MaxInputLength = 4
        Me.MyWWE1.Name = "MyWWE1"
        Me.MyWWE1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MyWWE1.Width = 62
        '
        'MyWWE1Name
        '
        Me.MyWWE1Name.HeaderText = "MyWWE1Name"
        Me.MyWWE1Name.Name = "MyWWE1Name"
        Me.MyWWE1Name.ReadOnly = True
        Me.MyWWE1Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MyWWE1Name.Width = 90
        '
        'MyWWE2
        '
        Me.MyWWE2.HeaderText = "MyWWE2"
        Me.MyWWE2.MaxInputLength = 4
        Me.MyWWE2.Name = "MyWWE2"
        Me.MyWWE2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MyWWE2.Width = 62
        '
        'MyWWE2Name
        '
        Me.MyWWE2Name.HeaderText = "MyWWE2Name"
        Me.MyWWE2Name.Name = "MyWWE2Name"
        Me.MyWWE2Name.ReadOnly = True
        Me.MyWWE2Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MyWWE2Name.Width = 90
        '
        'TitleUni1
        '
        Me.TitleUni1.HeaderText = "Uni1"
        Me.TitleUni1.MaxInputLength = 4
        Me.TitleUni1.Name = "TitleUni1"
        Me.TitleUni1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleUni1.Width = 35
        '
        'UniTitle1Name
        '
        Me.UniTitle1Name.HeaderText = "UniTitle1Name"
        Me.UniTitle1Name.Name = "UniTitle1Name"
        Me.UniTitle1Name.ReadOnly = True
        Me.UniTitle1Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UniTitle1Name.Width = 83
        '
        'TitleUni2
        '
        Me.TitleUni2.HeaderText = "Uni2"
        Me.TitleUni2.MaxInputLength = 4
        Me.TitleUni2.Name = "TitleUni2"
        Me.TitleUni2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleUni2.Width = 35
        '
        'UniTitle2Name
        '
        Me.UniTitle2Name.HeaderText = "UniTitle2Name"
        Me.UniTitle2Name.Name = "UniTitle2Name"
        Me.UniTitle2Name.ReadOnly = True
        Me.UniTitle2Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UniTitle2Name.Width = 83
        '
        'TitleTemp1
        '
        Me.TitleTemp1.HeaderText = "Temp1"
        Me.TitleTemp1.MaxInputLength = 8
        Me.TitleTemp1.Name = "TitleTemp1"
        Me.TitleTemp1.Width = 65
        '
        'Temp2
        '
        Me.Temp2.HeaderText = "TitleType"
        Me.Temp2.MaxInputLength = 8
        Me.Temp2.Name = "Temp2"
        Me.Temp2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Temp2.Width = 57
        '
        'TitleFemale
        '
        Me.TitleFemale.HeaderText = "Female"
        Me.TitleFemale.Name = "TitleFemale"
        Me.TitleFemale.Width = 47
        '
        'TitleTagTeam
        '
        Me.TitleTagTeam.HeaderText = "TagTeam"
        Me.TitleTagTeam.Name = "TitleTagTeam"
        Me.TitleTagTeam.Width = 59
        '
        'TitleCruiserweight
        '
        Me.TitleCruiserweight.HeaderText = "Cruiser"
        Me.TitleCruiserweight.Name = "TitleCruiserweight"
        Me.TitleCruiserweight.Width = 45
        '
        'UnlockNum
        '
        Me.UnlockNum.HeaderText = "UnlockNum"
        Me.UnlockNum.MaxInputLength = 8
        Me.UnlockNum.Name = "UnlockNum"
        Me.UnlockNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.UnlockNum.Width = 69
        '
        'TitleTemp4
        '
        Me.TitleTemp4.HeaderText = "Temp4"
        Me.TitleTemp4.MaxInputLength = 8
        Me.TitleTemp4.Name = "TitleTemp4"
        Me.TitleTemp4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TitleTemp4.Width = 46
        '
        'MenuStripTitleView
        '
        Me.MenuStripTitleView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TitleGameComboBox, Me.StringLoadedTitleMenuItem, Me.PacsLoadedTitleMenuItem, Me.SaveChangesTitleMenuItem})
        Me.MenuStripTitleView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripTitleView.Name = "MenuStripTitleView"
        Me.MenuStripTitleView.Size = New System.Drawing.Size(1213, 27)
        Me.MenuStripTitleView.TabIndex = 0
        Me.MenuStripTitleView.Text = "MenuStrip3"
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
        'SoundView
        '
        Me.SoundView.Controls.Add(Me.DataGridSoundView)
        Me.SoundView.Controls.Add(Me.MenuStripSoundView)
        Me.SoundView.Location = New System.Drawing.Point(4, 22)
        Me.SoundView.Name = "SoundView"
        Me.SoundView.Padding = New System.Windows.Forms.Padding(3)
        Me.SoundView.Size = New System.Drawing.Size(1219, 411)
        Me.SoundView.TabIndex = 15
        Me.SoundView.Text = "Sound View"
        Me.SoundView.UseVisualStyleBackColor = True
        '
        'DataGridSoundView
        '
        Me.DataGridSoundView.AllowUserToAddRows = False
        Me.DataGridSoundView.AllowUserToDeleteRows = False
        Me.DataGridSoundView.AllowUserToResizeColumns = False
        Me.DataGridSoundView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridSoundView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridSoundView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SoundContainerNumber, Me.SoundRefNum, Me.SoundHashRef, Me.SoundOffset, Me.SoundInfoAdd, Me.SoundInfoDel})
        Me.DataGridSoundView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridSoundView.Location = New System.Drawing.Point(3, 30)
        Me.DataGridSoundView.Name = "DataGridSoundView"
        Me.DataGridSoundView.RowHeadersVisible = False
        Me.DataGridSoundView.Size = New System.Drawing.Size(1213, 378)
        Me.DataGridSoundView.TabIndex = 1
        '
        'SoundContainerNumber
        '
        Me.SoundContainerNumber.HeaderText = "Container"
        Me.SoundContainerNumber.Name = "SoundContainerNumber"
        Me.SoundContainerNumber.ReadOnly = True
        Me.SoundContainerNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SoundRefNum
        '
        Me.SoundRefNum.HeaderText = "Ref Num"
        Me.SoundRefNum.MaxInputLength = 11
        Me.SoundRefNum.Name = "SoundRefNum"
        Me.SoundRefNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SoundHashRef
        '
        Me.SoundHashRef.HeaderText = "Hash Bytes"
        Me.SoundHashRef.MaxInputLength = 8
        Me.SoundHashRef.Name = "SoundHashRef"
        Me.SoundHashRef.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SoundOffset
        '
        Me.SoundOffset.HeaderText = "Offset"
        Me.SoundOffset.Name = "SoundOffset"
        Me.SoundOffset.ReadOnly = True
        Me.SoundOffset.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'SoundInfoAdd
        '
        Me.SoundInfoAdd.HeaderText = "Add"
        Me.SoundInfoAdd.Name = "SoundInfoAdd"
        Me.SoundInfoAdd.Text = "Add"
        Me.SoundInfoAdd.UseColumnTextForButtonValue = True
        '
        'SoundInfoDel
        '
        Me.SoundInfoDel.HeaderText = "Delete"
        Me.SoundInfoDel.Name = "SoundInfoDel"
        Me.SoundInfoDel.Text = "Delete"
        Me.SoundInfoDel.UseColumnTextForButtonValue = True
        '
        'MenuStripSoundView
        '
        Me.MenuStripSoundView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSoundRefSearch, Me.SaveChangesSoundMenuItem})
        Me.MenuStripSoundView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripSoundView.Name = "MenuStripSoundView"
        Me.MenuStripSoundView.Size = New System.Drawing.Size(1213, 27)
        Me.MenuStripSoundView.TabIndex = 0
        Me.MenuStripSoundView.Text = "MenuStrip1"
        '
        'ToolStripSoundRefSearch
        '
        Me.ToolStripSoundRefSearch.Name = "ToolStripSoundRefSearch"
        Me.ToolStripSoundRefSearch.Size = New System.Drawing.Size(100, 23)
        Me.ToolStripSoundRefSearch.Text = "Search..."
        '
        'SaveChangesSoundMenuItem
        '
        Me.SaveChangesSoundMenuItem.Name = "SaveChangesSoundMenuItem"
        Me.SaveChangesSoundMenuItem.Size = New System.Drawing.Size(92, 23)
        Me.SaveChangesSoundMenuItem.Text = "Save Changes"
        Me.SaveChangesSoundMenuItem.Visible = False
        '
        'MenuItemView
        '
        Me.MenuItemView.Controls.Add(Me.DataGridMenuItemView)
        Me.MenuItemView.Controls.Add(Me.MenuStripMenuItemView)
        Me.MenuItemView.Location = New System.Drawing.Point(4, 22)
        Me.MenuItemView.Name = "MenuItemView"
        Me.MenuItemView.Size = New System.Drawing.Size(1219, 411)
        Me.MenuItemView.TabIndex = 16
        Me.MenuItemView.Text = "Menu Item View"
        Me.MenuItemView.UseVisualStyleBackColor = True
        '
        'DataGridMenuItemView
        '
        Me.DataGridMenuItemView.AllowUserToAddRows = False
        Me.DataGridMenuItemView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridMenuItemView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMenuItemView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CAEEventID, Me.CAEStringRef, Me.CAEStringPrint, Me.CAEPacNum1, Me.CAEPacName1, Me.CAEPacNum2, Me.CAEPacName2, Me.CAEPacNum3, Me.CAEPacName3, Me.CAEPacNum4, Me.CAEPacName4, Me.CAEPacNum5, Me.CAEPacName5, Me.CAEDefaultWrestlerNum, Me.CAEPromo1, Me.CAEPromo2, Me.CAEPromo3, Me.CAEPromo4, Me.CAEBuffer, Me.CAEUknown1, Me.CAEUknown2, Me.CAELoackedtoPac, Me.PacNumExcluded, Me.CAEDLCFlag, Me.CAEMenuItemAdd, Me.CAEMenuItemDelete})
        Me.DataGridMenuItemView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridMenuItemView.Location = New System.Drawing.Point(0, 24)
        Me.DataGridMenuItemView.Name = "DataGridMenuItemView"
        Me.DataGridMenuItemView.RowHeadersVisible = False
        Me.DataGridMenuItemView.Size = New System.Drawing.Size(1219, 387)
        Me.DataGridMenuItemView.TabIndex = 1
        '
        'CAEEventID
        '
        Me.CAEEventID.HeaderText = "Event ID"
        Me.CAEEventID.MaxInputLength = 10
        Me.CAEEventID.Name = "CAEEventID"
        '
        'CAEStringRef
        '
        Me.CAEStringRef.HeaderText = "String Ref"
        Me.CAEStringRef.MaxInputLength = 8
        Me.CAEStringRef.Name = "CAEStringRef"
        '
        'CAEStringPrint
        '
        Me.CAEStringPrint.HeaderText = "CAE Name"
        Me.CAEStringPrint.Name = "CAEStringPrint"
        Me.CAEStringPrint.ReadOnly = True
        '
        'CAEPacNum1
        '
        Me.CAEPacNum1.HeaderText = "Pac Num 1"
        Me.CAEPacNum1.MaxInputLength = 5
        Me.CAEPacNum1.Name = "CAEPacNum1"
        '
        'CAEPacName1
        '
        Me.CAEPacName1.HeaderText = "Pac Name 1"
        Me.CAEPacName1.Name = "CAEPacName1"
        '
        'CAEPacNum2
        '
        Me.CAEPacNum2.HeaderText = "Pac Num 2"
        Me.CAEPacNum2.MaxInputLength = 5
        Me.CAEPacNum2.Name = "CAEPacNum2"
        '
        'CAEPacName2
        '
        Me.CAEPacName2.HeaderText = "Pac Name 2"
        Me.CAEPacName2.Name = "CAEPacName2"
        '
        'CAEPacNum3
        '
        Me.CAEPacNum3.HeaderText = "Pac Num 3"
        Me.CAEPacNum3.MaxInputLength = 5
        Me.CAEPacNum3.Name = "CAEPacNum3"
        '
        'CAEPacName3
        '
        Me.CAEPacName3.HeaderText = "Pac Name 3"
        Me.CAEPacName3.Name = "CAEPacName3"
        '
        'CAEPacNum4
        '
        Me.CAEPacNum4.HeaderText = "Pac Num 4"
        Me.CAEPacNum4.MaxInputLength = 5
        Me.CAEPacNum4.Name = "CAEPacNum4"
        '
        'CAEPacName4
        '
        Me.CAEPacName4.HeaderText = "Pac Name 4"
        Me.CAEPacName4.Name = "CAEPacName4"
        '
        'CAEPacNum5
        '
        Me.CAEPacNum5.HeaderText = "Pac Num 5"
        Me.CAEPacNum5.MaxInputLength = 5
        Me.CAEPacNum5.Name = "CAEPacNum5"
        '
        'CAEPacName5
        '
        Me.CAEPacName5.HeaderText = "Pac Name 5"
        Me.CAEPacName5.Name = "CAEPacName5"
        '
        'CAEDefaultWrestlerNum
        '
        Me.CAEDefaultWrestlerNum.HeaderText = "Has Pac"
        Me.CAEDefaultWrestlerNum.Name = "CAEDefaultWrestlerNum"
        Me.CAEDefaultWrestlerNum.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CAEDefaultWrestlerNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'CAEPromo1
        '
        Me.CAEPromo1.HeaderText = "Promo 1"
        Me.CAEPromo1.MaxInputLength = 5
        Me.CAEPromo1.Name = "CAEPromo1"
        '
        'CAEPromo2
        '
        Me.CAEPromo2.HeaderText = "Promo 2"
        Me.CAEPromo2.MaxInputLength = 5
        Me.CAEPromo2.Name = "CAEPromo2"
        '
        'CAEPromo3
        '
        Me.CAEPromo3.HeaderText = "Promo 3"
        Me.CAEPromo3.MaxInputLength = 3
        Me.CAEPromo3.Name = "CAEPromo3"
        '
        'CAEPromo4
        '
        Me.CAEPromo4.HeaderText = "Promo 4"
        Me.CAEPromo4.MaxInputLength = 3
        Me.CAEPromo4.Name = "CAEPromo4"
        '
        'CAEBuffer
        '
        Me.CAEBuffer.HeaderText = "Buffer Bytes"
        Me.CAEBuffer.MaxInputLength = 5
        Me.CAEBuffer.Name = "CAEBuffer"
        '
        'CAEUknown1
        '
        Me.CAEUknown1.HeaderText = "Unkown 1"
        Me.CAEUknown1.MaxInputLength = 5
        Me.CAEUknown1.Name = "CAEUknown1"
        '
        'CAEUknown2
        '
        Me.CAEUknown2.HeaderText = "Unkown 2"
        Me.CAEUknown2.MaxInputLength = 5
        Me.CAEUknown2.Name = "CAEUknown2"
        '
        'CAELoackedtoPac
        '
        Me.CAELoackedtoPac.HeaderText = "Locked to Pac"
        Me.CAELoackedtoPac.MaxInputLength = 5
        Me.CAELoackedtoPac.Name = "CAELoackedtoPac"
        '
        'PacNumExcluded
        '
        Me.PacNumExcluded.HeaderText = "Excluded Pac"
        Me.PacNumExcluded.MaxInputLength = 5
        Me.PacNumExcluded.Name = "PacNumExcluded"
        '
        'CAEDLCFlag
        '
        Me.CAEDLCFlag.HeaderText = "DLC Flag"
        Me.CAEDLCFlag.Name = "CAEDLCFlag"
        Me.CAEDLCFlag.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CAEDLCFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'CAEMenuItemAdd
        '
        Me.CAEMenuItemAdd.HeaderText = "Add"
        Me.CAEMenuItemAdd.Name = "CAEMenuItemAdd"
        Me.CAEMenuItemAdd.Text = "Add"
        Me.CAEMenuItemAdd.UseColumnTextForButtonValue = True
        '
        'CAEMenuItemDelete
        '
        Me.CAEMenuItemDelete.HeaderText = "Delete"
        Me.CAEMenuItemDelete.Name = "CAEMenuItemDelete"
        Me.CAEMenuItemDelete.Text = "Delete"
        Me.CAEMenuItemDelete.UseColumnTextForButtonValue = True
        '
        'MenuStripMenuItemView
        '
        Me.MenuStripMenuItemView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StringLoadedCAEMenuItem, Me.PacsLoadedCAEMenuItem, Me.SaveChangesCAEMenuItemMenuItem})
        Me.MenuStripMenuItemView.Location = New System.Drawing.Point(0, 0)
        Me.MenuStripMenuItemView.Name = "MenuStripMenuItemView"
        Me.MenuStripMenuItemView.Size = New System.Drawing.Size(1219, 24)
        Me.MenuStripMenuItemView.TabIndex = 0
        Me.MenuStripMenuItemView.Text = "MenuStrip1"
        '
        'StringLoadedCAEMenuItem
        '
        Me.StringLoadedCAEMenuItem.Enabled = False
        Me.StringLoadedCAEMenuItem.Name = "StringLoadedCAEMenuItem"
        Me.StringLoadedCAEMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.StringLoadedCAEMenuItem.Text = "String Loaded:"
        '
        'PacsLoadedCAEMenuItem
        '
        Me.PacsLoadedCAEMenuItem.Enabled = False
        Me.PacsLoadedCAEMenuItem.Name = "PacsLoadedCAEMenuItem"
        Me.PacsLoadedCAEMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.PacsLoadedCAEMenuItem.Text = "Pacs Loaded:"
        '
        'SaveChangesCAEMenuItemMenuItem
        '
        Me.SaveChangesCAEMenuItemMenuItem.Name = "SaveChangesCAEMenuItemMenuItem"
        Me.SaveChangesCAEMenuItemMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesCAEMenuItemMenuItem.Text = "Save Changes"
        Me.SaveChangesCAEMenuItemMenuItem.Visible = False
        '
        'AnimationView
        '
        Me.AnimationView.Controls.Add(Me.DataGridAnimationView)
        Me.AnimationView.Controls.Add(Me.MenuStripAnimationView)
        Me.AnimationView.Location = New System.Drawing.Point(4, 22)
        Me.AnimationView.Name = "AnimationView"
        Me.AnimationView.Padding = New System.Windows.Forms.Padding(3)
        Me.AnimationView.Size = New System.Drawing.Size(1219, 411)
        Me.AnimationView.TabIndex = 17
        Me.AnimationView.Text = "Animation View"
        Me.AnimationView.UseVisualStyleBackColor = True
        '
        'DataGridAnimationView
        '
        Me.DataGridAnimationView.AllowUserToAddRows = False
        Me.DataGridAnimationView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAnimationView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AnimationBoneIndex, Me.AnimationStartingInt, Me.AnimationStartHex, Me.AnimationHeaderLength, Me.AnimationHeaderLengthHex, Me.AnimationBoneType, Me.AnimationBoneTypeHex, Me.AnimationOffsetA, Me.AnimationOffsetAHex, Me.AnimationIntA, Me.AnimationIntAHex, Me.AnimationOffsetB, Me.AnimationOffsetBHex, Me.AnimationIntB, Me.AnimationIntBHex, Me.AnimationRemainingBytes, Me.AnimationStartingData, Me.AnimationSecondaryData, Me.AnimationXTrans, Me.AnimationYTrans, Me.AnimationZTrans, Me.AnimationXRotation, Me.AnimationYRotation, Me.AnimationZRotation, Me.AnimationFramesDec, Me.AnimationFramesHex, Me.AnimationAnimationData, Me.AnimationAnimationDataParsed})
        Me.DataGridAnimationView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAnimationView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridAnimationView.Name = "DataGridAnimationView"
        Me.DataGridAnimationView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridAnimationView.TabIndex = 1
        '
        'AnimationBoneIndex
        '
        Me.AnimationBoneIndex.HeaderText = "Count"
        Me.AnimationBoneIndex.Name = "AnimationBoneIndex"
        Me.AnimationBoneIndex.ReadOnly = True
        Me.AnimationBoneIndex.Width = 50
        '
        'AnimationStartingInt
        '
        Me.AnimationStartingInt.HeaderText = "Start Int"
        Me.AnimationStartingInt.Name = "AnimationStartingInt"
        Me.AnimationStartingInt.Width = 69
        '
        'AnimationStartHex
        '
        Me.AnimationStartHex.HeaderText = "Start Hex"
        Me.AnimationStartHex.Name = "AnimationStartHex"
        Me.AnimationStartHex.Width = 76
        '
        'AnimationHeaderLength
        '
        Me.AnimationHeaderLength.HeaderText = "Head Length"
        Me.AnimationHeaderLength.Name = "AnimationHeaderLength"
        Me.AnimationHeaderLength.Width = 94
        '
        'AnimationHeaderLengthHex
        '
        Me.AnimationHeaderLengthHex.HeaderText = "Length Hex"
        Me.AnimationHeaderLengthHex.Name = "AnimationHeaderLengthHex"
        Me.AnimationHeaderLengthHex.Width = 87
        '
        'AnimationBoneType
        '
        Me.AnimationBoneType.HeaderText = "Bone Type"
        Me.AnimationBoneType.Name = "AnimationBoneType"
        Me.AnimationBoneType.Width = 84
        '
        'AnimationBoneTypeHex
        '
        Me.AnimationBoneTypeHex.HeaderText = "Bone as Hex"
        Me.AnimationBoneTypeHex.Name = "AnimationBoneTypeHex"
        Me.AnimationBoneTypeHex.Width = 93
        '
        'AnimationOffsetA
        '
        Me.AnimationOffsetA.HeaderText = "Offset A"
        Me.AnimationOffsetA.Name = "AnimationOffsetA"
        Me.AnimationOffsetA.Width = 70
        '
        'AnimationOffsetAHex
        '
        Me.AnimationOffsetAHex.HeaderText = "Offset A Hex"
        Me.AnimationOffsetAHex.Name = "AnimationOffsetAHex"
        Me.AnimationOffsetAHex.Width = 92
        '
        'AnimationIntA
        '
        Me.AnimationIntA.HeaderText = "Length A"
        Me.AnimationIntA.Name = "AnimationIntA"
        Me.AnimationIntA.Width = 54
        '
        'AnimationIntAHex
        '
        Me.AnimationIntAHex.HeaderText = "Length A Hex"
        Me.AnimationIntAHex.Name = "AnimationIntAHex"
        Me.AnimationIntAHex.Width = 76
        '
        'AnimationOffsetB
        '
        Me.AnimationOffsetB.HeaderText = "Offset B"
        Me.AnimationOffsetB.Name = "AnimationOffsetB"
        Me.AnimationOffsetB.Width = 70
        '
        'AnimationOffsetBHex
        '
        Me.AnimationOffsetBHex.HeaderText = "Offset B Hex"
        Me.AnimationOffsetBHex.Name = "AnimationOffsetBHex"
        Me.AnimationOffsetBHex.Width = 92
        '
        'AnimationIntB
        '
        Me.AnimationIntB.HeaderText = "Length B"
        Me.AnimationIntB.Name = "AnimationIntB"
        Me.AnimationIntB.Width = 54
        '
        'AnimationIntBHex
        '
        Me.AnimationIntBHex.HeaderText = "Length B Hex"
        Me.AnimationIntBHex.Name = "AnimationIntBHex"
        Me.AnimationIntBHex.Width = 76
        '
        'AnimationRemainingBytes
        '
        Me.AnimationRemainingBytes.HeaderText = "Remaining Bytes"
        Me.AnimationRemainingBytes.Name = "AnimationRemainingBytes"
        Me.AnimationRemainingBytes.Width = 102
        '
        'AnimationStartingData
        '
        Me.AnimationStartingData.HeaderText = "File Part A"
        Me.AnimationStartingData.Name = "AnimationStartingData"
        Me.AnimationStartingData.Width = 69
        '
        'AnimationSecondaryData
        '
        Me.AnimationSecondaryData.HeaderText = "File Part B"
        Me.AnimationSecondaryData.Name = "AnimationSecondaryData"
        '
        'AnimationXTrans
        '
        Me.AnimationXTrans.HeaderText = "X?"
        Me.AnimationXTrans.Name = "AnimationXTrans"
        Me.AnimationXTrans.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnimationXTrans.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnimationXTrans.Width = 50
        '
        'AnimationYTrans
        '
        Me.AnimationYTrans.HeaderText = "Y?"
        Me.AnimationYTrans.Name = "AnimationYTrans"
        Me.AnimationYTrans.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnimationYTrans.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnimationYTrans.Width = 50
        '
        'AnimationZTrans
        '
        Me.AnimationZTrans.HeaderText = "Z?"
        Me.AnimationZTrans.Name = "AnimationZTrans"
        Me.AnimationZTrans.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnimationZTrans.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnimationZTrans.Width = 50
        '
        'AnimationXRotation
        '
        Me.AnimationXRotation.HeaderText = "rX?"
        Me.AnimationXRotation.Name = "AnimationXRotation"
        Me.AnimationXRotation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnimationXRotation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnimationXRotation.Width = 50
        '
        'AnimationYRotation
        '
        Me.AnimationYRotation.HeaderText = "rY?"
        Me.AnimationYRotation.Name = "AnimationYRotation"
        Me.AnimationYRotation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnimationYRotation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnimationYRotation.Width = 50
        '
        'AnimationZRotation
        '
        Me.AnimationZRotation.HeaderText = "rZ?"
        Me.AnimationZRotation.Name = "AnimationZRotation"
        Me.AnimationZRotation.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.AnimationZRotation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.AnimationZRotation.Width = 50
        '
        'AnimationFramesDec
        '
        Me.AnimationFramesDec.HeaderText = "Frames Int"
        Me.AnimationFramesDec.Name = "AnimationFramesDec"
        '
        'AnimationFramesHex
        '
        Me.AnimationFramesHex.HeaderText = "Frames Hex"
        Me.AnimationFramesHex.Name = "AnimationFramesHex"
        '
        'AnimationAnimationData
        '
        Me.AnimationAnimationData.HeaderText = "Animation Bytes"
        Me.AnimationAnimationData.Name = "AnimationAnimationData"
        '
        'AnimationAnimationDataParsed
        '
        Me.AnimationAnimationDataParsed.HeaderText = "Parsed Data"
        Me.AnimationAnimationDataParsed.Name = "AnimationAnimationDataParsed"
        '
        'MenuStripAnimationView
        '
        Me.MenuStripAnimationView.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnimationShowHexToolStripMenuItem, Me.AnimationShowDebugToolStripMenuItem, Me.SaveChangesAnimationMenuItem})
        Me.MenuStripAnimationView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripAnimationView.Name = "MenuStripAnimationView"
        Me.MenuStripAnimationView.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripAnimationView.TabIndex = 0
        Me.MenuStripAnimationView.Text = "MenuStrip1"
        '
        'AnimationShowHexToolStripMenuItem
        '
        Me.AnimationShowHexToolStripMenuItem.Name = "AnimationShowHexToolStripMenuItem"
        Me.AnimationShowHexToolStripMenuItem.Size = New System.Drawing.Size(85, 20)
        Me.AnimationShowHexToolStripMenuItem.Text = "☒ Show Hex"
        '
        'AnimationShowDebugToolStripMenuItem
        '
        Me.AnimationShowDebugToolStripMenuItem.Name = "AnimationShowDebugToolStripMenuItem"
        Me.AnimationShowDebugToolStripMenuItem.Size = New System.Drawing.Size(99, 20)
        Me.AnimationShowDebugToolStripMenuItem.Text = "☐ Show Debug"
        '
        'SaveChangesAnimationMenuItem
        '
        Me.SaveChangesAnimationMenuItem.Name = "SaveChangesAnimationMenuItem"
        Me.SaveChangesAnimationMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesAnimationMenuItem.Text = "Save Changes"
        Me.SaveChangesAnimationMenuItem.Visible = False
        '
        'Pof0View
        '
        Me.Pof0View.Controls.Add(Me.DataGridPof0View)
        Me.Pof0View.Controls.Add(Me.MenuStripPof0View)
        Me.Pof0View.Location = New System.Drawing.Point(4, 22)
        Me.Pof0View.Name = "Pof0View"
        Me.Pof0View.Padding = New System.Windows.Forms.Padding(3)
        Me.Pof0View.Size = New System.Drawing.Size(1219, 411)
        Me.Pof0View.TabIndex = 18
        Me.Pof0View.Text = "Pof0 View"
        Me.Pof0View.UseVisualStyleBackColor = True
        '
        'DataGridPof0View
        '
        Me.DataGridPof0View.AllowUserToAddRows = False
        Me.DataGridPof0View.AllowUserToDeleteRows = False
        Me.DataGridPof0View.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridPof0View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridPof0View.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Pof0ByteCount, Me.Pof0RawHex, Me.Pof0TranslateDec, Me.Pof0TranslateHex, Me.Pof0ActiveOffsetDec, Me.Pof0ActiveOffsetHex, Me.Pof0ReferenceData})
        Me.DataGridPof0View.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridPof0View.Location = New System.Drawing.Point(3, 27)
        Me.DataGridPof0View.Name = "DataGridPof0View"
        Me.DataGridPof0View.RowHeadersVisible = False
        Me.DataGridPof0View.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridPof0View.TabIndex = 1
        '
        'Pof0ByteCount
        '
        Me.Pof0ByteCount.HeaderText = "Count"
        Me.Pof0ByteCount.Name = "Pof0ByteCount"
        Me.Pof0ByteCount.ReadOnly = True
        '
        'Pof0RawHex
        '
        Me.Pof0RawHex.HeaderText = "Raw Hex"
        Me.Pof0RawHex.Name = "Pof0RawHex"
        '
        'Pof0TranslateDec
        '
        Me.Pof0TranslateDec.HeaderText = "Dec Value"
        Me.Pof0TranslateDec.Name = "Pof0TranslateDec"
        '
        'Pof0TranslateHex
        '
        Me.Pof0TranslateHex.HeaderText = "Hex Value"
        Me.Pof0TranslateHex.Name = "Pof0TranslateHex"
        '
        'Pof0ActiveOffsetDec
        '
        Me.Pof0ActiveOffsetDec.HeaderText = "Offset Dec"
        Me.Pof0ActiveOffsetDec.Name = "Pof0ActiveOffsetDec"
        '
        'Pof0ActiveOffsetHex
        '
        Me.Pof0ActiveOffsetHex.HeaderText = "Offset Hex"
        Me.Pof0ActiveOffsetHex.Name = "Pof0ActiveOffsetHex"
        '
        'Pof0ReferenceData
        '
        Me.Pof0ReferenceData.FillWeight = 200.0!
        Me.Pof0ReferenceData.HeaderText = "Reference Data"
        Me.Pof0ReferenceData.Name = "Pof0ReferenceData"
        Me.Pof0ReferenceData.ReadOnly = True
        '
        'MenuStripPof0View
        '
        Me.MenuStripPof0View.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripPof0View.Name = "MenuStripPof0View"
        Me.MenuStripPof0View.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripPof0View.TabIndex = 0
        Me.MenuStripPof0View.Text = "MenuStrip1"
        '
        'WeaponPositionView
        '
        Me.WeaponPositionView.Controls.Add(Me.DataGridWeaponPositionView)
        Me.WeaponPositionView.Controls.Add(Me.MenuStripWeapPos)
        Me.WeaponPositionView.Location = New System.Drawing.Point(4, 22)
        Me.WeaponPositionView.Name = "WeaponPositionView"
        Me.WeaponPositionView.Padding = New System.Windows.Forms.Padding(3)
        Me.WeaponPositionView.Size = New System.Drawing.Size(1219, 411)
        Me.WeaponPositionView.TabIndex = 19
        Me.WeaponPositionView.Text = "Weap Pos View"
        Me.WeaponPositionView.UseVisualStyleBackColor = True
        '
        'DataGridWeaponPositionView
        '
        Me.DataGridWeaponPositionView.AllowUserToAddRows = False
        Me.DataGridWeaponPositionView.AllowUserToDeleteRows = False
        Me.DataGridWeaponPositionView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridWeaponPositionView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridWeaponPositionView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.WeaponPositionCount, Me.WeaponPositionSettingNum, Me.WeaponPositionSettingObjStart, Me.WeaponPositionByteArray, Me.WeaponPositionInt1, Me.WeaponPositionInt2, Me.WeaponPositionInt3, Me.WeaponPositionInt4, Me.WeaponPositionSingle1, Me.WeaponPositionSingle2, Me.WeaponPositionSingle3, Me.WeaponPositionSingle4, Me.WeaponPositionSingle5, Me.WeaponPositionSingle6, Me.WeaponPositionShort1, Me.WeaponPositionShort2, Me.WeaponPositionShort3, Me.WeaponPositionShort4, Me.WeaponPositionIntSet, Me.WeaponPositionAdd, Me.WeaponPositionDelete})
        Me.DataGridWeaponPositionView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridWeaponPositionView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridWeaponPositionView.Name = "DataGridWeaponPositionView"
        Me.DataGridWeaponPositionView.RowHeadersVisible = False
        Me.DataGridWeaponPositionView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridWeaponPositionView.TabIndex = 1
        '
        'WeaponPositionCount
        '
        Me.WeaponPositionCount.FillWeight = 20.0!
        Me.WeaponPositionCount.HeaderText = "Count"
        Me.WeaponPositionCount.MaxInputLength = 10
        Me.WeaponPositionCount.Name = "WeaponPositionCount"
        Me.WeaponPositionCount.ReadOnly = True
        '
        'WeaponPositionSettingNum
        '
        Me.WeaponPositionSettingNum.FillWeight = 20.0!
        Me.WeaponPositionSettingNum.HeaderText = "Setting Num"
        Me.WeaponPositionSettingNum.MaxInputLength = 10
        Me.WeaponPositionSettingNum.Name = "WeaponPositionSettingNum"
        '
        'WeaponPositionSettingObjStart
        '
        Me.WeaponPositionSettingObjStart.FillWeight = 20.0!
        Me.WeaponPositionSettingObjStart.HeaderText = "Object Num"
        Me.WeaponPositionSettingObjStart.MaxInputLength = 10
        Me.WeaponPositionSettingObjStart.Name = "WeaponPositionSettingObjStart"
        '
        'WeaponPositionByteArray
        '
        Me.WeaponPositionByteArray.HeaderText = "Byte Array"
        Me.WeaponPositionByteArray.Name = "WeaponPositionByteArray"
        Me.WeaponPositionByteArray.ReadOnly = True
        '
        'WeaponPositionInt1
        '
        Me.WeaponPositionInt1.FillWeight = 20.0!
        Me.WeaponPositionInt1.HeaderText = "Int1"
        Me.WeaponPositionInt1.MaxInputLength = 10
        Me.WeaponPositionInt1.Name = "WeaponPositionInt1"
        '
        'WeaponPositionInt2
        '
        Me.WeaponPositionInt2.FillWeight = 20.0!
        Me.WeaponPositionInt2.HeaderText = "Int2"
        Me.WeaponPositionInt2.MaxInputLength = 10
        Me.WeaponPositionInt2.Name = "WeaponPositionInt2"
        '
        'WeaponPositionInt3
        '
        Me.WeaponPositionInt3.FillWeight = 20.0!
        Me.WeaponPositionInt3.HeaderText = "Int3"
        Me.WeaponPositionInt3.MaxInputLength = 10
        Me.WeaponPositionInt3.Name = "WeaponPositionInt3"
        '
        'WeaponPositionInt4
        '
        Me.WeaponPositionInt4.FillWeight = 20.0!
        Me.WeaponPositionInt4.HeaderText = "Int4"
        Me.WeaponPositionInt4.MaxInputLength = 10
        Me.WeaponPositionInt4.Name = "WeaponPositionInt4"
        '
        'WeaponPositionSingle1
        '
        Me.WeaponPositionSingle1.FillWeight = 20.0!
        Me.WeaponPositionSingle1.HeaderText = "Float 1"
        Me.WeaponPositionSingle1.MaxInputLength = 12
        Me.WeaponPositionSingle1.Name = "WeaponPositionSingle1"
        '
        'WeaponPositionSingle2
        '
        Me.WeaponPositionSingle2.FillWeight = 20.0!
        Me.WeaponPositionSingle2.HeaderText = "Float 2"
        Me.WeaponPositionSingle2.MaxInputLength = 12
        Me.WeaponPositionSingle2.Name = "WeaponPositionSingle2"
        '
        'WeaponPositionSingle3
        '
        Me.WeaponPositionSingle3.FillWeight = 20.0!
        Me.WeaponPositionSingle3.HeaderText = "Float 3"
        Me.WeaponPositionSingle3.MaxInputLength = 12
        Me.WeaponPositionSingle3.Name = "WeaponPositionSingle3"
        '
        'WeaponPositionSingle4
        '
        Me.WeaponPositionSingle4.FillWeight = 20.0!
        Me.WeaponPositionSingle4.HeaderText = "Float 4"
        Me.WeaponPositionSingle4.MaxInputLength = 12
        Me.WeaponPositionSingle4.Name = "WeaponPositionSingle4"
        '
        'WeaponPositionSingle5
        '
        Me.WeaponPositionSingle5.FillWeight = 20.0!
        Me.WeaponPositionSingle5.HeaderText = "Float 5"
        Me.WeaponPositionSingle5.MaxInputLength = 12
        Me.WeaponPositionSingle5.Name = "WeaponPositionSingle5"
        '
        'WeaponPositionSingle6
        '
        Me.WeaponPositionSingle6.FillWeight = 20.0!
        Me.WeaponPositionSingle6.HeaderText = "Float 6"
        Me.WeaponPositionSingle6.MaxInputLength = 12
        Me.WeaponPositionSingle6.Name = "WeaponPositionSingle6"
        '
        'WeaponPositionShort1
        '
        Me.WeaponPositionShort1.FillWeight = 20.0!
        Me.WeaponPositionShort1.HeaderText = "Short 1"
        Me.WeaponPositionShort1.MaxInputLength = 5
        Me.WeaponPositionShort1.Name = "WeaponPositionShort1"
        '
        'WeaponPositionShort2
        '
        Me.WeaponPositionShort2.FillWeight = 20.0!
        Me.WeaponPositionShort2.HeaderText = "Short 2"
        Me.WeaponPositionShort2.MaxInputLength = 5
        Me.WeaponPositionShort2.Name = "WeaponPositionShort2"
        '
        'WeaponPositionShort3
        '
        Me.WeaponPositionShort3.FillWeight = 20.0!
        Me.WeaponPositionShort3.HeaderText = "Short 3"
        Me.WeaponPositionShort3.MaxInputLength = 5
        Me.WeaponPositionShort3.Name = "WeaponPositionShort3"
        '
        'WeaponPositionShort4
        '
        Me.WeaponPositionShort4.FillWeight = 20.0!
        Me.WeaponPositionShort4.HeaderText = "Short 4"
        Me.WeaponPositionShort4.MaxInputLength = 5
        Me.WeaponPositionShort4.Name = "WeaponPositionShort4"
        '
        'WeaponPositionIntSet
        '
        Me.WeaponPositionIntSet.FillWeight = 20.0!
        Me.WeaponPositionIntSet.HeaderText = "Int"
        Me.WeaponPositionIntSet.MaxInputLength = 10
        Me.WeaponPositionIntSet.Name = "WeaponPositionIntSet"
        '
        'WeaponPositionAdd
        '
        Me.WeaponPositionAdd.FillWeight = 20.0!
        Me.WeaponPositionAdd.HeaderText = "Add"
        Me.WeaponPositionAdd.Name = "WeaponPositionAdd"
        Me.WeaponPositionAdd.Text = "Add"
        Me.WeaponPositionAdd.UseColumnTextForButtonValue = True
        '
        'WeaponPositionDelete
        '
        Me.WeaponPositionDelete.FillWeight = 20.0!
        Me.WeaponPositionDelete.HeaderText = "Delete"
        Me.WeaponPositionDelete.Name = "WeaponPositionDelete"
        Me.WeaponPositionDelete.Text = "Delete"
        Me.WeaponPositionDelete.UseColumnTextForButtonValue = True
        '
        'MenuStripWeapPos
        '
        Me.MenuStripWeapPos.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WeaponPositionTypeToolStripMenuItem, Me.SaveChangesWeaponPositionsMenuItem})
        Me.MenuStripWeapPos.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripWeapPos.Name = "MenuStripWeapPos"
        Me.MenuStripWeapPos.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripWeapPos.TabIndex = 0
        Me.MenuStripWeapPos.Text = "MenuStrip1"
        '
        'WeaponPositionTypeToolStripMenuItem
        '
        Me.WeaponPositionTypeToolStripMenuItem.Name = "WeaponPositionTypeToolStripMenuItem"
        Me.WeaponPositionTypeToolStripMenuItem.Size = New System.Drawing.Size(95, 20)
        Me.WeaponPositionTypeToolStripMenuItem.Text = "Position Type: "
        '
        'SaveChangesWeaponPositionsMenuItem
        '
        Me.SaveChangesWeaponPositionsMenuItem.Name = "SaveChangesWeaponPositionsMenuItem"
        Me.SaveChangesWeaponPositionsMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.SaveChangesWeaponPositionsMenuItem.Text = "Save Changes"
        Me.SaveChangesWeaponPositionsMenuItem.Visible = False
        '
        'ArcView
        '
        Me.ArcView.Controls.Add(Me.DataGridArcView)
        Me.ArcView.Controls.Add(Me.MenuStripArcView)
        Me.ArcView.Location = New System.Drawing.Point(4, 22)
        Me.ArcView.Name = "ArcView"
        Me.ArcView.Padding = New System.Windows.Forms.Padding(3)
        Me.ArcView.Size = New System.Drawing.Size(1219, 411)
        Me.ArcView.TabIndex = 20
        Me.ArcView.Text = "Arc"
        Me.ArcView.UseVisualStyleBackColor = True
        '
        'DataGridArcView
        '
        Me.DataGridArcView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridArcView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridArcView.Location = New System.Drawing.Point(3, 27)
        Me.DataGridArcView.Name = "DataGridArcView"
        Me.DataGridArcView.Size = New System.Drawing.Size(1213, 381)
        Me.DataGridArcView.TabIndex = 1
        '
        'MenuStripArcView
        '
        Me.MenuStripArcView.Location = New System.Drawing.Point(3, 3)
        Me.MenuStripArcView.Name = "MenuStripArcView"
        Me.MenuStripArcView.Size = New System.Drawing.Size(1213, 24)
        Me.MenuStripArcView.TabIndex = 0
        Me.MenuStripArcView.Text = "MenuStrip1"
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
        'TreeViewContext
        '
        Me.TreeViewContext.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenRADVideoToolStripMenuItem, Me.OpenImageWithToolStripMenuItem, Me.ExtractToolStripMenuItem, Me.InjectToolStripMenuItem, Me.CrawlToolStripMenuItem, Me.DeleteFileToolStripMenuItem, Me.DeletePartToolStripMenuItem, Me.RenameFileToolStripMenuItem, Me.RenamePartToolStripMenuItem, Me.OpenFileLocationToolStripMenuItem})
        Me.TreeViewContext.Name = "TreeViewContext"
        Me.TreeViewContext.Size = New System.Drawing.Size(169, 224)
        '
        'OpenRADVideoToolStripMenuItem
        '
        Me.OpenRADVideoToolStripMenuItem.Name = "OpenRADVideoToolStripMenuItem"
        Me.OpenRADVideoToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.OpenRADVideoToolStripMenuItem.Text = "Open"
        '
        'OpenImageWithToolStripMenuItem
        '
        Me.OpenImageWithToolStripMenuItem.Name = "OpenImageWithToolStripMenuItem"
        Me.OpenImageWithToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.OpenImageWithToolStripMenuItem.Text = "Open With..."
        '
        'ExtractToolStripMenuItem
        '
        Me.ExtractToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExtractPartToToolStripMenuItem, Me.ExtractAllInPlaceToolStripMenuItem, Me.ExtractAllToToolStripMenuItem})
        Me.ExtractToolStripMenuItem.Name = "ExtractToolStripMenuItem"
        Me.ExtractToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.ExtractToolStripMenuItem.Text = "Extract"
        '
        'ExtractPartToToolStripMenuItem
        '
        Me.ExtractPartToToolStripMenuItem.Name = "ExtractPartToToolStripMenuItem"
        Me.ExtractPartToToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExtractPartToToolStripMenuItem.Text = "Extract Part To..."
        '
        'ExtractAllInPlaceToolStripMenuItem
        '
        Me.ExtractAllInPlaceToolStripMenuItem.Name = "ExtractAllInPlaceToolStripMenuItem"
        Me.ExtractAllInPlaceToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExtractAllInPlaceToolStripMenuItem.Text = "Extract All In Place"
        '
        'ExtractAllToToolStripMenuItem
        '
        Me.ExtractAllToToolStripMenuItem.Name = "ExtractAllToToolStripMenuItem"
        Me.ExtractAllToToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.ExtractAllToToolStripMenuItem.Text = "Extract All To..."
        '
        'InjectToolStripMenuItem
        '
        Me.InjectToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InjectUncompressedToolStripMenuItem, Me.InjectBPEToolStripMenuItem, Me.InjectZLIBToolStripMenuItem, Me.InjectOODLToolStripMenuItem})
        Me.InjectToolStripMenuItem.Name = "InjectToolStripMenuItem"
        Me.InjectToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.InjectToolStripMenuItem.Text = "Inject"
        '
        'InjectUncompressedToolStripMenuItem
        '
        Me.InjectUncompressedToolStripMenuItem.Name = "InjectUncompressedToolStripMenuItem"
        Me.InjectUncompressedToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.InjectUncompressedToolStripMenuItem.Text = "Inject Uncompressed"
        '
        'InjectBPEToolStripMenuItem
        '
        Me.InjectBPEToolStripMenuItem.Name = "InjectBPEToolStripMenuItem"
        Me.InjectBPEToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.InjectBPEToolStripMenuItem.Text = "Inject as BPE"
        '
        'InjectZLIBToolStripMenuItem
        '
        Me.InjectZLIBToolStripMenuItem.Name = "InjectZLIBToolStripMenuItem"
        Me.InjectZLIBToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.InjectZLIBToolStripMenuItem.Text = "Inject as ZLIB"
        '
        'InjectOODLToolStripMenuItem
        '
        Me.InjectOODLToolStripMenuItem.Name = "InjectOODLToolStripMenuItem"
        Me.InjectOODLToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.InjectOODLToolStripMenuItem.Text = "Inject as OODL"
        '
        'CrawlToolStripMenuItem
        '
        Me.CrawlToolStripMenuItem.Name = "CrawlToolStripMenuItem"
        Me.CrawlToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CrawlToolStripMenuItem.Text = "Crawl"
        '
        'DeleteFileToolStripMenuItem
        '
        Me.DeleteFileToolStripMenuItem.Name = "DeleteFileToolStripMenuItem"
        Me.DeleteFileToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.DeleteFileToolStripMenuItem.Text = "Delete"
        '
        'DeletePartToolStripMenuItem
        '
        Me.DeletePartToolStripMenuItem.Name = "DeletePartToolStripMenuItem"
        Me.DeletePartToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.DeletePartToolStripMenuItem.Text = "Delete"
        '
        'RenameFileToolStripMenuItem
        '
        Me.RenameFileToolStripMenuItem.Name = "RenameFileToolStripMenuItem"
        Me.RenameFileToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.RenameFileToolStripMenuItem.Text = "Rename"
        '
        'RenamePartToolStripMenuItem
        '
        Me.RenamePartToolStripMenuItem.Name = "RenamePartToolStripMenuItem"
        Me.RenamePartToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.RenamePartToolStripMenuItem.Text = "Rename"
        '
        'OpenFileLocationToolStripMenuItem
        '
        Me.OpenFileLocationToolStripMenuItem.Name = "OpenFileLocationToolStripMenuItem"
        Me.OpenFileLocationToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.OpenFileLocationToolStripMenuItem.Text = "Open file location"
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
        'SelectNewHomeToolStripMenuItem
        '
        Me.SelectNewHomeToolStripMenuItem.Name = "SelectNewHomeToolStripMenuItem"
        Me.SelectNewHomeToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.SelectNewHomeToolStripMenuItem.Text = "Select New Home"
        '
        'MainForm
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1484, 461)
        Me.Controls.Add(Me.SplitFileMenuContainer)
        Me.Controls.Add(Me.MenuStripMainForm)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStripMainForm
        Me.Name = "MainForm"
        Me.Text = "WrestleMINUS"
        Me.MenuStripMainForm.ResumeLayout(False)
        Me.MenuStripMainForm.PerformLayout()
        Me.SplitFileMenuContainer.Panel1.ResumeLayout(False)
        Me.SplitFileMenuContainer.Panel1.PerformLayout()
        Me.SplitFileMenuContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitFileMenuContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitFileMenuContainer.ResumeLayout(False)
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
        Me.ObjectView.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.ObjectMainViewPage.ResumeLayout(False)
        CType(Me.DataGridObjectView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjectBoneViewPage.ResumeLayout(False)
        CType(Me.DataGridObjectBoneView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjectTextureViewPage.ResumeLayout(False)
        Me.ObjectTestureSplitContainer.Panel1.ResumeLayout(False)
        Me.ObjectTestureSplitContainer.Panel2.ResumeLayout(False)
        CType(Me.ObjectTestureSplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjectTestureSplitContainer.ResumeLayout(False)
        CType(Me.DataGridObjectTextureView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridObjectShaderView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjectVertexViewPage.ResumeLayout(False)
        Me.ObjectVertexViewPage.PerformLayout()
        CType(Me.DataGridObjectVertexView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripObjectVertexView.ResumeLayout(False)
        Me.MenuStripObjectVertexView.PerformLayout()
        Me.ObjectFacesViewPage.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridObjectTriStripsView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridObjectFacesView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ObjectParamViewPage.ResumeLayout(False)
        CType(Me.DataGridObjectParamView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripObjectView.ResumeLayout(False)
        Me.MenuStripObjectView.PerformLayout()
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
        Me.MenuStripMaskView.ResumeLayout(False)
        Me.MenuStripMaskView.PerformLayout()
        Me.ObjArrayView.ResumeLayout(False)
        Me.ObjArrayView.PerformLayout()
        CType(Me.DataGridObjArrayView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripObjectArrayView.ResumeLayout(False)
        Me.MenuStripObjectArrayView.PerformLayout()
        Me.AssetView.ResumeLayout(False)
        Me.AssetView.PerformLayout()
        CType(Me.DataGridAssetView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripAssetView.ResumeLayout(False)
        Me.MenuStripAssetView.PerformLayout()
        Me.TitleView.ResumeLayout(False)
        Me.TitleView.PerformLayout()
        CType(Me.DataGridTitleView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripTitleView.ResumeLayout(False)
        Me.MenuStripTitleView.PerformLayout()
        Me.SoundView.ResumeLayout(False)
        Me.SoundView.PerformLayout()
        CType(Me.DataGridSoundView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripSoundView.ResumeLayout(False)
        Me.MenuStripSoundView.PerformLayout()
        Me.MenuItemView.ResumeLayout(False)
        Me.MenuItemView.PerformLayout()
        CType(Me.DataGridMenuItemView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripMenuItemView.ResumeLayout(False)
        Me.MenuStripMenuItemView.PerformLayout()
        Me.AnimationView.ResumeLayout(False)
        Me.AnimationView.PerformLayout()
        CType(Me.DataGridAnimationView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripAnimationView.ResumeLayout(False)
        Me.MenuStripAnimationView.PerformLayout()
        Me.Pof0View.ResumeLayout(False)
        Me.Pof0View.PerformLayout()
        CType(Me.DataGridPof0View, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WeaponPositionView.ResumeLayout(False)
        Me.WeaponPositionView.PerformLayout()
        CType(Me.DataGridWeaponPositionView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripWeapPos.ResumeLayout(False)
        Me.MenuStripWeapPos.PerformLayout()
        Me.ArcView.ResumeLayout(False)
        Me.ArcView.PerformLayout()
        CType(Me.DataGridArcView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStripPictureView.ResumeLayout(False)
        Me.MenuStripPictureView.PerformLayout()
        Me.TreeViewContext.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStripMainForm As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadHomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitFileMenuContainer As SplitContainer
    Friend WithEvents TreeView1 As TreeView
    Friend WithEvents MenuStripTreeView As MenuStrip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents HexView As TabPage
    Friend WithEvents TextView As TabPage
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
    Friend WithEvents FileAttributesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectViewPictureView As TabPage
    Friend WithEvents MenuStripPictureView As MenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TreeViewContext As ContextMenuStrip
    Friend WithEvents OpenRADVideoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectView As TabPage
    Friend WithEvents PictureView As TabPage
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
    Friend WithEvents MenuStripMaskView As MenuStrip
    Friend WithEvents ObjArrayView As TabPage
    Friend WithEvents MenuStripObjectArrayView As MenuStrip
    Friend WithEvents DataGridObjArrayView As DataGridView
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
    Friend WithEvents DataGridViewTextBoxColumn35 As DataGridViewTextBoxColumn
    Friend WithEvents CrawlToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AssetView As TabPage
    Friend WithEvents DataGridAssetView As DataGridView
    Friend WithEvents SaveChangesStringMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBoxSearch As ToolStripTextBox
    Friend WithEvents SaveChangesMiscMenuItem As ToolStripMenuItem
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
    Friend WithEvents MenuStripTitleView As MenuStrip
    Friend WithEvents StringLoadedTitleMenuItem As ToolStripMenuItem
    Friend WithEvents PacsLoadedTitleMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesTitleMenuItem As ToolStripMenuItem
    Friend WithEvents TitleGameComboBox As ToolStripComboBox
    Friend WithEvents SupportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GitHubIssuesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents OpenImageWithToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Hex_Selected As RichTextBox
    Friend WithEvents Text_Selected As RichTextBox
    Friend WithEvents SortStringsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StringLoadedShowMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesShowMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesNIBJMenuItem As ToolStripMenuItem
    Friend WithEvents ExportMaskstoTXTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportMasksFromTXTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesMaskMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents TekkenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TutorialVideoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DSImportScriptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DSSelectionScriptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DSExportScriptToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AznTutorialToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesYOBJArrayMenuItem As ToolStripMenuItem
    Friend WithEvents ExportYOBJArrayToCSVToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Mask_Name As DataGridViewTextBoxColumn
    Friend WithEvents Start_Face As DataGridViewTextBoxColumn
    Friend WithEvents End_Face As DataGridViewTextBoxColumn
    Friend WithEvents Add_Mask As DataGridViewButtonColumn
    Friend WithEvents Del_Mask As DataGridViewButtonColumn
    Friend WithEvents ImportYOBJArrayFromCSVToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents YobjArryIndex As DataGridViewTextBoxColumn
    Friend WithEvents ObjArrayParent As DataGridViewTextBoxColumn
    Friend WithEvents Number As DataGridViewTextBoxColumn
    Friend WithEvents ArrEnabled As DataGridViewCheckBoxColumn
    Friend WithEvents ChairName As DataGridViewTextBoxColumn
    Friend WithEvents X As DataGridViewTextBoxColumn
    Friend WithEvents Y As DataGridViewTextBoxColumn
    Friend WithEvents Z As DataGridViewTextBoxColumn
    Friend WithEvents RX As DataGridViewTextBoxColumn
    Friend WithEvents RY As DataGridViewTextBoxColumn
    Friend WithEvents RZ As DataGridViewTextBoxColumn
    Friend WithEvents ContainedYobjArray As DataGridViewTextBoxColumn
    Friend WithEvents StartIndexYobjArray As DataGridViewTextBoxColumn
    Friend WithEvents Add_Obj_Array As DataGridViewButtonColumn
    Friend WithEvents Del_Obj_Array As DataGridViewButtonColumn
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZLIBCompressionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OODLCompressionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BPECompressionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BPEBatchCompressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BPESingleCompressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZLIBBatchCompressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZLIBSingleCompressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OODLBatchCompressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OODLSingleCompressToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RenamePartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InjectUncompressedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InjectBPEToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InjectZLIBToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InjectOODLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtractToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtractPartToToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtractAllInPlaceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExtractAllToToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RenameFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GenerateFileNameHashToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeletePartToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RebuildDefFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RebuildDefCurrentHomeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RebuildDefSelectFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SoundView As TabPage
    Friend WithEvents DataGridSoundView As DataGridView
    Friend WithEvents MenuStripSoundView As MenuStrip
    Friend WithEvents SaveChangesSoundMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSoundRefSearch As ToolStripTextBox
    Friend WithEvents MenuStripAssetView As MenuStrip
    Friend WithEvents SaveChangesAssetViewMenuItem As ToolStripMenuItem
    Friend WithEvents PacNumber As DataGridViewTextBoxColumn
    Friend WithEvents AttireNum As DataGridViewTextBoxColumn
    Friend WithEvents AudioNum As DataGridViewTextBoxColumn
    Friend WithEvents Check2 As DataGridViewTextBoxColumn
    Friend WithEvents MUSOffset As DataGridViewTextBoxColumn
    Friend WithEvents EVTOffset As DataGridViewTextBoxColumn
    Friend WithEvents MusicID As DataGridViewTextBoxColumn
    Friend WithEvents TitantronNum As DataGridViewTextBoxColumn
    Friend WithEvents HeaderNum As DataGridViewTextBoxColumn
    Friend WithEvents WallNum As DataGridViewTextBoxColumn
    Friend WithEvents RampNum As DataGridViewTextBoxColumn
    Friend WithEvents WallRightNum As DataGridViewTextBoxColumn
    Friend WithEvents WallLeftNum As DataGridViewTextBoxColumn
    Friend WithEvents RawTronEnabled As DataGridViewCheckBoxColumn
    Friend WithEvents SmackDownTronEnabled As DataGridViewCheckBoxColumn
    Friend WithEvents ClassicTronEnabled As DataGridViewCheckBoxColumn
    Friend WithEvents Check5 As DataGridViewTextBoxColumn
    Friend WithEvents Check6 As DataGridViewTextBoxColumn
    Friend WithEvents MUSFileName As DataGridViewTextBoxColumn
    Friend WithEvents EVTFileName As DataGridViewTextBoxColumn
    Friend WithEvents AddAsset As DataGridViewButtonColumn
    Friend WithEvents DeleteAsset As DataGridViewButtonColumn
    Friend WithEvents TitleEnabled As DataGridViewTextBoxColumn
    Friend WithEvents PropRef As DataGridViewTextBoxColumn
    Friend WithEvents MenuNumber As DataGridViewTextBoxColumn
    Friend WithEvents TitleNameNum1 As DataGridViewTextBoxColumn
    Friend WithEvents TitleNameNum1Full As DataGridViewTextBoxColumn
    Friend WithEvents TitleNameNum2 As DataGridViewTextBoxColumn
    Friend WithEvents TitleNameNum2Full As DataGridViewTextBoxColumn
    Friend WithEvents TitleNameNum3 As DataGridViewTextBoxColumn
    Friend WithEvents TitleNameNum3Full As DataGridViewTextBoxColumn
    Friend WithEvents MyWWE1 As DataGridViewTextBoxColumn
    Friend WithEvents MyWWE1Name As DataGridViewTextBoxColumn
    Friend WithEvents MyWWE2 As DataGridViewTextBoxColumn
    Friend WithEvents MyWWE2Name As DataGridViewTextBoxColumn
    Friend WithEvents TitleUni1 As DataGridViewTextBoxColumn
    Friend WithEvents UniTitle1Name As DataGridViewTextBoxColumn
    Friend WithEvents TitleUni2 As DataGridViewTextBoxColumn
    Friend WithEvents UniTitle2Name As DataGridViewTextBoxColumn
    Friend WithEvents TitleTemp1 As DataGridViewTextBoxColumn
    Friend WithEvents Temp2 As DataGridViewTextBoxColumn
    Friend WithEvents TitleFemale As DataGridViewCheckBoxColumn
    Friend WithEvents TitleTagTeam As DataGridViewCheckBoxColumn
    Friend WithEvents TitleCruiserweight As DataGridViewCheckBoxColumn
    Friend WithEvents UnlockNum As DataGridViewTextBoxColumn
    Friend WithEvents TitleTemp4 As DataGridViewTextBoxColumn
    Friend WithEvents SoundContainerNumber As DataGridViewTextBoxColumn
    Friend WithEvents SoundRefNum As DataGridViewTextBoxColumn
    Friend WithEvents SoundHashRef As DataGridViewTextBoxColumn
    Friend WithEvents SoundOffset As DataGridViewTextBoxColumn
    Friend WithEvents SoundInfoAdd As DataGridViewButtonColumn
    Friend WithEvents SoundInfoDel As DataGridViewButtonColumn
    Friend WithEvents MenuItemView As TabPage
    Friend WithEvents DataGridMenuItemView As DataGridView
    Friend WithEvents MenuStripMenuItemView As MenuStrip
    Friend WithEvents SaveChangesCAEMenuItemMenuItem As ToolStripMenuItem
    Friend WithEvents StringLoadedCAEMenuItem As ToolStripMenuItem
    Friend WithEvents PacsLoadedCAEMenuItem As ToolStripMenuItem
    Friend WithEvents CAEEventID As DataGridViewTextBoxColumn
    Friend WithEvents CAEStringRef As DataGridViewTextBoxColumn
    Friend WithEvents CAEStringPrint As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacNum1 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacName1 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacNum2 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacName2 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacNum3 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacName3 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacNum4 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacName4 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacNum5 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPacName5 As DataGridViewTextBoxColumn
    Friend WithEvents CAEDefaultWrestlerNum As DataGridViewCheckBoxColumn
    Friend WithEvents CAEPromo1 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPromo2 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPromo3 As DataGridViewTextBoxColumn
    Friend WithEvents CAEPromo4 As DataGridViewTextBoxColumn
    Friend WithEvents CAEBuffer As DataGridViewTextBoxColumn
    Friend WithEvents CAEUknown1 As DataGridViewTextBoxColumn
    Friend WithEvents CAEUknown2 As DataGridViewTextBoxColumn
    Friend WithEvents CAELoackedtoPac As DataGridViewTextBoxColumn
    Friend WithEvents PacNumExcluded As DataGridViewTextBoxColumn
    Friend WithEvents CAEDLCFlag As DataGridViewCheckBoxColumn
    Friend WithEvents CAEMenuItemAdd As DataGridViewButtonColumn
    Friend WithEvents CAEMenuItemDelete As DataGridViewButtonColumn
    Friend WithEvents OpenFileLocationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnimationView As TabPage
    Friend WithEvents DataGridAnimationView As DataGridView
    Friend WithEvents MenuStripAnimationView As MenuStrip
    Friend WithEvents SaveChangesAnimationMenuItem As ToolStripMenuItem
    Friend WithEvents AnimationShowHexToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnimationShowDebugToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnimationBoneIndex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationStartingInt As DataGridViewTextBoxColumn
    Friend WithEvents AnimationStartHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationHeaderLength As DataGridViewTextBoxColumn
    Friend WithEvents AnimationHeaderLengthHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationBoneType As DataGridViewTextBoxColumn
    Friend WithEvents AnimationBoneTypeHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationOffsetA As DataGridViewTextBoxColumn
    Friend WithEvents AnimationOffsetAHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationIntA As DataGridViewTextBoxColumn
    Friend WithEvents AnimationIntAHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationOffsetB As DataGridViewTextBoxColumn
    Friend WithEvents AnimationOffsetBHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationIntB As DataGridViewTextBoxColumn
    Friend WithEvents AnimationIntBHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationRemainingBytes As DataGridViewTextBoxColumn
    Friend WithEvents AnimationStartingData As DataGridViewTextBoxColumn
    Friend WithEvents AnimationSecondaryData As DataGridViewTextBoxColumn
    Friend WithEvents AnimationXTrans As DataGridViewCheckBoxColumn
    Friend WithEvents AnimationYTrans As DataGridViewCheckBoxColumn
    Friend WithEvents AnimationZTrans As DataGridViewCheckBoxColumn
    Friend WithEvents AnimationXRotation As DataGridViewCheckBoxColumn
    Friend WithEvents AnimationYRotation As DataGridViewCheckBoxColumn
    Friend WithEvents AnimationZRotation As DataGridViewCheckBoxColumn
    Friend WithEvents AnimationFramesDec As DataGridViewTextBoxColumn
    Friend WithEvents AnimationFramesHex As DataGridViewTextBoxColumn
    Friend WithEvents AnimationAnimationData As DataGridViewTextBoxColumn
    Friend WithEvents AnimationAnimationDataParsed As DataGridViewTextBoxColumn
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
    Friend WithEvents ArenaMiscAddButton As DataGridViewButtonColumn
    Friend WithEvents ArenaMiscDelButton As DataGridViewButtonColumn
    Friend WithEvents ShowViewStrName As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewStringName As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewSelectNum As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewNumSecond As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewA1 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewA2 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewB1 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewB2 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewB3 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewC1 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC2 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC3 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC4 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC5 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC6 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC7 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewC8 As DataGridViewCheckBoxColumn
    Friend WithEvents ShowViewStage As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewD1 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewD2 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewD3 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewD4 As DataGridViewTextBoxColumn
    Friend WithEvents ShowView5 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewRef As DataGridViewTextBoxColumn
    Friend WithEvents Filter As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewF1 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewF2 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewF3 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewF4 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewH1 As DataGridViewTextBoxColumn
    Friend WithEvents H2 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewH3 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewH4 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewBar As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewUnknown As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewI1 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewI2 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewI3 As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewLive As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewJ As DataGridViewTextBoxColumn
    Friend WithEvents ShowViewAddButton As DataGridViewButtonColumn
    Friend WithEvents ShowViewDelete As DataGridViewButtonColumn
    Friend WithEvents Pof0View As TabPage
    Friend WithEvents DataGridPof0View As DataGridView
    Friend WithEvents MenuStripPof0View As MenuStrip
    Friend WithEvents WeaponPositionView As TabPage
    Friend WithEvents DataGridWeaponPositionView As DataGridView
    Friend WithEvents MenuStripWeapPos As MenuStrip
    Friend WithEvents WeaponPositionTypeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveChangesWeaponPositionsMenuItem As ToolStripMenuItem
    Friend WithEvents DataGridObjectView As DataGridView
    Friend WithEvents MenuStripObjectView As MenuStrip
    Friend WithEvents Pof0ByteCount As DataGridViewTextBoxColumn
    Friend WithEvents Pof0RawHex As DataGridViewTextBoxColumn
    Friend WithEvents Pof0TranslateDec As DataGridViewTextBoxColumn
    Friend WithEvents Pof0TranslateHex As DataGridViewTextBoxColumn
    Friend WithEvents Pof0ActiveOffsetDec As DataGridViewTextBoxColumn
    Friend WithEvents Pof0ActiveOffsetHex As DataGridViewTextBoxColumn
    Friend WithEvents Pof0ReferenceData As DataGridViewTextBoxColumn
    Friend WithEvents TabControl2 As TabControl
    Friend WithEvents ObjectMainViewPage As TabPage
    Friend WithEvents ObjectBoneViewPage As TabPage
    Friend WithEvents LoadedObjectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataGridObjectBoneView As DataGridView
    Friend WithEvents ObjectTextureViewPage As TabPage
    Friend WithEvents ObjectTestureSplitContainer As SplitContainer
    Friend WithEvents DataGridObjectTextureView As DataGridView
    Friend WithEvents DataGridObjectShaderView As DataGridView
    Friend WithEvents ObjectTextureCount As DataGridViewTextBoxColumn
    Friend WithEvents ObjectTextureCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectShaderCount As DataGridViewTextBoxColumn
    Friend WithEvents ObjectShaderCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectShaderType As DataGridViewTextBoxColumn
    Friend WithEvents ObjectShaderB As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneCountCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneOrder As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneName As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownA As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownB As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownC As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownD As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownE As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownF As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownG As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownH As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownI As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownJ As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownK As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownL As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownM As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownN As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownO As DataGridViewTextBoxColumn
    Friend WithEvents ObjectBoneUnknownP As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertexViewPage As TabPage
    Friend WithEvents DataGridObjectVertexView As DataGridView
    Friend WithEvents UnknownValue As DataGridViewTextBoxColumn
    Friend WithEvents ObjectEmoteListComboBox As ToolStripComboBox
    Friend WithEvents MenuStripObjectVertexView As MenuStrip
    Friend WithEvents ShowWeightsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectFacesViewPage As TabPage
    Friend WithEvents DataGridObjectFacesView As DataGridView
    Friend WithEvents ObjectFaceCurrentCountCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectFaceVertex1 As DataGridViewTextBoxColumn
    Friend WithEvents ObjectFaceVertex2 As DataGridViewTextBoxColumn
    Friend WithEvents ObjectFaceVertex3 As DataGridViewTextBoxColumn
    Friend WithEvents ShowNormalsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ObjectParamViewPage As TabPage
    Friend WithEvents DataGridObjectParamView As DataGridView
    Friend WithEvents ObjectParamCountCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectParamName As DataGridViewTextBoxColumn
    Friend WithEvents ObjectParamInt1 As DataGridViewTextBoxColumn
    Friend WithEvents ObjectParamInt2 As DataGridViewTextBoxColumn
    Friend WithEvents ObjectParamSingle As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionCount As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSettingNum As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSettingObjStart As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionByteArray As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionInt1 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionInt2 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionInt3 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionInt4 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSingle1 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSingle2 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSingle3 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSingle4 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSingle5 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionSingle6 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionShort1 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionShort2 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionShort3 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionShort4 As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionIntSet As DataGridViewTextBoxColumn
    Friend WithEvents WeaponPositionAdd As DataGridViewButtonColumn
    Friend WithEvents WeaponPositionDelete As DataGridViewButtonColumn
    Friend WithEvents StringHexRefColumn As DataGridViewTextBoxColumn
    Friend WithEvents StringTextColumn As DataGridViewTextBoxColumn
    Friend WithEvents StringLengthColumn As DataGridViewTextBoxColumn
    Friend WithEvents AddStringButton As DataGridViewButtonColumn
    Friend WithEvents DeleteStringButton As DataGridViewButtonColumn
    Friend WithEvents ObjectCountCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectHeaderLoad As DataGridViewButtonColumn
    Friend WithEvents ObjectExportToObj As DataGridViewButtonColumn
    Friend WithEvents ObjectVertexCount As DataGridViewTextBoxColumn
    Friend WithEvents ObjectRendered As DataGridViewCheckBoxColumn
    Friend WithEvents ObjectHeaderFiller As DataGridViewTextBoxColumn
    Friend WithEvents ObjectWeightNumber As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUnknownIntA As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVerHeaderCount As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVerticeOffset As DataGridViewTextBoxColumn
    Friend WithEvents ObjectWeightsOffset As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUVOffset As DataGridViewTextBoxColumn
    Friend WithEvents ObjectNormalsOffset As DataGridViewTextBoxColumn
    Friend WithEvents ObjectInternalNum As DataGridViewTextBoxColumn
    Friend WithEvents ObjectHeaderShader As DataGridViewTextBoxColumn
    Friend WithEvents ObjecHeaderUnknownC As DataGridViewTextBoxColumn
    Friend WithEvents ObjectMaterialIndex As DataGridViewTextBoxColumn
    Friend WithEvents ObjectParameterCount As DataGridViewTextBoxColumn
    Friend WithEvents ObjectParameterOffset As DataGridViewTextBoxColumn
    Friend WithEvents ObjectFaceOffset As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUVCount As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUnknownD As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUnknownE As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUnknownF As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUnknownG As DataGridViewTextBoxColumn
    Friend WithEvents ObjectUnknownH As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertCountCol As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertX As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertY As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertZ As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertRX As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertRY As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertRZ As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertWeight As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertU As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertV As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertNormal1 As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertNormal2 As DataGridViewTextBoxColumn
    Friend WithEvents ObjectVertNormal3 As DataGridViewTextBoxColumn
    Friend WithEvents ArcView As TabPage
    Friend WithEvents DataGridArcView As DataGridView
    Friend WithEvents MenuStripArcView As MenuStrip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DataGridObjectTriStripsView As DataGridView
    Friend WithEvents ObjectTriStripNum As DataGridViewTextBoxColumn
    Friend WithEvents ObjectTriStripVerts As DataGridViewTextBoxColumn
    Friend WithEvents ObjectTriStripVertCount As DataGridViewTextBoxColumn
    Friend WithEvents ExportStringArrayToCSVToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SelectNewHomeToolStripMenuItem As ToolStripMenuItem
End Class
