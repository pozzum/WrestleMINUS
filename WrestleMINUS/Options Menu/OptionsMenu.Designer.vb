<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsMenu))
        Me.LabelHomeDirectory = New System.Windows.Forms.Label()
        Me.TextBoxHome = New System.Windows.Forms.TextBox()
        Me.ButtonSelectHome = New System.Windows.Forms.Button()
        Me.LabelTexConv = New System.Windows.Forms.Label()
        Me.TextBoxTexConv = New System.Windows.Forms.TextBox()
        Me.ButtonSelectTexConv = New System.Windows.Forms.Button()
        Me.LabelRadVideo = New System.Windows.Forms.Label()
        Me.TextBoxRadVideo = New System.Windows.Forms.TextBox()
        Me.ButtonSelectRadVideo = New System.Windows.Forms.Button()
        Me.LabelUnrrbpe = New System.Windows.Forms.Label()
        Me.TextBoxUnrrbpe = New System.Windows.Forms.TextBox()
        Me.ButtonSelectUnrrbpe = New System.Windows.Forms.Button()
        Me.ButtonDownloadUnrrbpe = New System.Windows.Forms.Button()
        Me.ButtonDownloadRadVideo = New System.Windows.Forms.Button()
        Me.CheckBoxLoadHome = New System.Windows.Forms.CheckBox()
        Me.CheckBoxBackup = New System.Windows.Forms.CheckBox()
        Me.CheckBoxDeleteTempBMP = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTreeNodeIcons = New System.Windows.Forms.CheckBox()
        Me.ButtonDownloadDDSexe = New System.Windows.Forms.Button()
        Me.ButtonSelectDDSexe = New System.Windows.Forms.Button()
        Me.TextBoxDDSExe = New System.Windows.Forms.TextBox()
        Me.LabelDSSExe = New System.Windows.Forms.Label()
        Me.CheckBoxDetailedFileNames = New System.Windows.Forms.CheckBox()
        Me.CheckBoxOODLBypass = New System.Windows.Forms.CheckBox()
        Me.ButtonDownloadBPEExe = New System.Windows.Forms.Button()
        Me.ButtonSelectBPEExe = New System.Windows.Forms.Button()
        Me.TextBoxBPEExe = New System.Windows.Forms.TextBox()
        Me.LabelBPEExe = New System.Windows.Forms.Label()
        Me.CheckBoxExtractAllinPlace = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CheckBoxRecycleDeletedFiles = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageExeTools = New System.Windows.Forms.TabPage()
        Me.ButtonSelectCrunchExe = New System.Windows.Forms.Button()
        Me.TextBoxCrunchExe = New System.Windows.Forms.TextBox()
        Me.LabelCrunchExe = New System.Windows.Forms.Label()
        Me.LabelSkipVersion = New System.Windows.Forms.Label()
        Me.ButtonCheckUpdate = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.CheckBoxShowCAkIntermediates = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCreateCAkDef = New System.Windows.Forms.CheckBox()
        Me.CheckBoxSuppresHSPCFooters = New System.Windows.Forms.CheckBox()
        Me.ButtonResetFormSize = New System.Windows.Forms.Button()
        Me.CheckBoxShowSelectedNode = New System.Windows.Forms.CheckBox()
        Me.ButtonResetPacs = New System.Windows.Forms.Button()
        Me.ButtonResetStrings = New System.Windows.Forms.Button()
        Me.LabelDecimalNameLength = New System.Windows.Forms.Label()
        Me.LabelHexLength = New System.Windows.Forms.Label()
        Me.TrackBarHexLength = New System.Windows.Forms.TrackBar()
        Me.TrackBarDecimalNameLength = New System.Windows.Forms.TrackBar()
        Me.LabelOodleCompLevel = New System.Windows.Forms.Label()
        Me.ComboBoxOodleCompressionLevel = New System.Windows.Forms.ComboBox()
        Me.CheckBoxAppendDef = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckRelocateMods = New System.Windows.Forms.CheckBox()
        Me.CheckDisableModPref = New System.Windows.Forms.CheckBox()
        Me.TabPageDLLCheck = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ButtonNewtonJsonSelect = New System.Windows.Forms.Button()
        Me.LabelFrosty = New System.Windows.Forms.Label()
        Me.ButtonFrostyYukesSelect = New System.Windows.Forms.Button()
        Me.ButtonOodle7Select = New System.Windows.Forms.Button()
        Me.LabelOodle_7 = New System.Windows.Forms.Label()
        Me.ButtonSelectFontAwesome = New System.Windows.Forms.Button()
        Me.LabelFontAwesome = New System.Windows.Forms.Label()
        Me.ButtonOodle6Select = New System.Windows.Forms.Button()
        Me.ButtonSelectZlib = New System.Windows.Forms.Button()
        Me.LabelOodle_6 = New System.Windows.Forms.Label()
        Me.LabelZlib = New System.Windows.Forms.Label()
        Me.CheckBoxRebuildCak = New System.Windows.Forms.CheckBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPageExeTools.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.TrackBarHexLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarDecimalNameLength, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageDLLCheck.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelHomeDirectory
        '
        Me.LabelHomeDirectory.AutoSize = True
        Me.LabelHomeDirectory.Location = New System.Drawing.Point(8, 6)
        Me.LabelHomeDirectory.Name = "LabelHomeDirectory"
        Me.LabelHomeDirectory.Size = New System.Drawing.Size(80, 13)
        Me.LabelHomeDirectory.TabIndex = 0
        Me.LabelHomeDirectory.Text = "Home Directory"
        '
        'TextBoxHome
        '
        Me.TextBoxHome.Location = New System.Drawing.Point(8, 22)
        Me.TextBoxHome.Name = "TextBoxHome"
        Me.TextBoxHome.ReadOnly = True
        Me.TextBoxHome.Size = New System.Drawing.Size(229, 20)
        Me.TextBoxHome.TabIndex = 1
        '
        'ButtonSelectHome
        '
        Me.ButtonSelectHome.Location = New System.Drawing.Point(243, 20)
        Me.ButtonSelectHome.Name = "ButtonSelectHome"
        Me.ButtonSelectHome.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectHome.TabIndex = 2
        Me.ButtonSelectHome.Text = "..."
        Me.ButtonSelectHome.UseVisualStyleBackColor = True
        '
        'LabelTexConv
        '
        Me.LabelTexConv.AutoSize = True
        Me.LabelTexConv.Location = New System.Drawing.Point(8, 45)
        Me.LabelTexConv.Name = "LabelTexConv"
        Me.LabelTexConv.Size = New System.Drawing.Size(115, 13)
        Me.LabelTexConv.TabIndex = 3
        Me.LabelTexConv.Text = "TexConv Exe Location"
        '
        'TextBoxTexConv
        '
        Me.TextBoxTexConv.Location = New System.Drawing.Point(8, 61)
        Me.TextBoxTexConv.Name = "TextBoxTexConv"
        Me.TextBoxTexConv.ReadOnly = True
        Me.TextBoxTexConv.Size = New System.Drawing.Size(229, 20)
        Me.TextBoxTexConv.TabIndex = 4
        '
        'ButtonSelectTexConv
        '
        Me.ButtonSelectTexConv.Location = New System.Drawing.Point(243, 59)
        Me.ButtonSelectTexConv.Name = "ButtonSelectTexConv"
        Me.ButtonSelectTexConv.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectTexConv.TabIndex = 5
        Me.ButtonSelectTexConv.Text = "..."
        Me.ButtonSelectTexConv.UseVisualStyleBackColor = True
        '
        'LabelRadVideo
        '
        Me.LabelRadVideo.AutoSize = True
        Me.LabelRadVideo.Location = New System.Drawing.Point(8, 123)
        Me.LabelRadVideo.Name = "LabelRadVideo"
        Me.LabelRadVideo.Size = New System.Drawing.Size(119, 13)
        Me.LabelRadVideo.TabIndex = 6
        Me.LabelRadVideo.Text = "RadVideo Exe Location"
        '
        'TextBoxRadVideo
        '
        Me.TextBoxRadVideo.Location = New System.Drawing.Point(8, 139)
        Me.TextBoxRadVideo.Name = "TextBoxRadVideo"
        Me.TextBoxRadVideo.ReadOnly = True
        Me.TextBoxRadVideo.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxRadVideo.TabIndex = 7
        '
        'ButtonSelectRadVideo
        '
        Me.ButtonSelectRadVideo.Location = New System.Drawing.Point(243, 137)
        Me.ButtonSelectRadVideo.Name = "ButtonSelectRadVideo"
        Me.ButtonSelectRadVideo.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectRadVideo.TabIndex = 8
        Me.ButtonSelectRadVideo.Text = "..."
        Me.ButtonSelectRadVideo.UseVisualStyleBackColor = True
        '
        'LabelUnrrbpe
        '
        Me.LabelUnrrbpe.AutoSize = True
        Me.LabelUnrrbpe.Location = New System.Drawing.Point(8, 201)
        Me.LabelUnrrbpe.Name = "LabelUnrrbpe"
        Me.LabelUnrrbpe.Size = New System.Drawing.Size(110, 13)
        Me.LabelUnrrbpe.TabIndex = 9
        Me.LabelUnrrbpe.Text = "Unrrbpe Exe Location"
        '
        'TextBoxUnrrbpe
        '
        Me.TextBoxUnrrbpe.Location = New System.Drawing.Point(8, 217)
        Me.TextBoxUnrrbpe.Name = "TextBoxUnrrbpe"
        Me.TextBoxUnrrbpe.ReadOnly = True
        Me.TextBoxUnrrbpe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxUnrrbpe.TabIndex = 10
        '
        'ButtonSelectUnrrbpe
        '
        Me.ButtonSelectUnrrbpe.Location = New System.Drawing.Point(243, 215)
        Me.ButtonSelectUnrrbpe.Name = "ButtonSelectUnrrbpe"
        Me.ButtonSelectUnrrbpe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectUnrrbpe.TabIndex = 11
        Me.ButtonSelectUnrrbpe.Text = "..."
        Me.ButtonSelectUnrrbpe.UseVisualStyleBackColor = True
        '
        'ButtonDownloadUnrrbpe
        '
        Me.ButtonDownloadUnrrbpe.Location = New System.Drawing.Point(207, 215)
        Me.ButtonDownloadUnrrbpe.Name = "ButtonDownloadUnrrbpe"
        Me.ButtonDownloadUnrrbpe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadUnrrbpe.TabIndex = 12
        Me.ButtonDownloadUnrrbpe.Text = "DL"
        Me.ButtonDownloadUnrrbpe.UseVisualStyleBackColor = True
        '
        'ButtonDownloadRadVideo
        '
        Me.ButtonDownloadRadVideo.Location = New System.Drawing.Point(207, 137)
        Me.ButtonDownloadRadVideo.Name = "ButtonDownloadRadVideo"
        Me.ButtonDownloadRadVideo.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadRadVideo.TabIndex = 13
        Me.ButtonDownloadRadVideo.Text = "DL"
        Me.ButtonDownloadRadVideo.UseVisualStyleBackColor = True
        '
        'CheckBoxLoadHome
        '
        Me.CheckBoxLoadHome.AutoSize = True
        Me.CheckBoxLoadHome.Location = New System.Drawing.Point(8, 6)
        Me.CheckBoxLoadHome.Name = "CheckBoxLoadHome"
        Me.CheckBoxLoadHome.Size = New System.Drawing.Size(135, 17)
        Me.CheckBoxLoadHome.TabIndex = 18
        Me.CheckBoxLoadHome.Text = "Load Home on Launch"
        Me.CheckBoxLoadHome.UseVisualStyleBackColor = True
        '
        'CheckBoxBackup
        '
        Me.CheckBoxBackup.AutoSize = True
        Me.CheckBoxBackup.Location = New System.Drawing.Point(8, 29)
        Me.CheckBoxBackup.Name = "CheckBoxBackup"
        Me.CheckBoxBackup.Size = New System.Drawing.Size(196, 17)
        Me.CheckBoxBackup.TabIndex = 19
        Me.CheckBoxBackup.Text = "Create Backup Files When Injecting"
        Me.CheckBoxBackup.UseVisualStyleBackColor = True
        '
        'CheckBoxDeleteTempBMP
        '
        Me.CheckBoxDeleteTempBMP.AutoSize = True
        Me.CheckBoxDeleteTempBMP.Location = New System.Drawing.Point(8, 52)
        Me.CheckBoxDeleteTempBMP.Name = "CheckBoxDeleteTempBMP"
        Me.CheckBoxDeleteTempBMP.Size = New System.Drawing.Size(172, 17)
        Me.CheckBoxDeleteTempBMP.TabIndex = 25
        Me.CheckBoxDeleteTempBMP.Text = "Delete Temp BMP Files on Exit"
        Me.CheckBoxDeleteTempBMP.UseVisualStyleBackColor = True
        '
        'CheckBoxTreeNodeIcons
        '
        Me.CheckBoxTreeNodeIcons.AutoSize = True
        Me.CheckBoxTreeNodeIcons.Location = New System.Drawing.Point(8, 98)
        Me.CheckBoxTreeNodeIcons.Name = "CheckBoxTreeNodeIcons"
        Me.CheckBoxTreeNodeIcons.Size = New System.Drawing.Size(125, 17)
        Me.CheckBoxTreeNodeIcons.TabIndex = 26
        Me.CheckBoxTreeNodeIcons.Text = "Use TreeNode Icons"
        Me.CheckBoxTreeNodeIcons.UseVisualStyleBackColor = True
        '
        'ButtonDownloadDDSexe
        '
        Me.ButtonDownloadDDSexe.Location = New System.Drawing.Point(207, 254)
        Me.ButtonDownloadDDSexe.Name = "ButtonDownloadDDSexe"
        Me.ButtonDownloadDDSexe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadDDSexe.TabIndex = 30
        Me.ButtonDownloadDDSexe.Text = "DL"
        Me.ButtonDownloadDDSexe.UseVisualStyleBackColor = True
        '
        'ButtonSelectDDSexe
        '
        Me.ButtonSelectDDSexe.Location = New System.Drawing.Point(243, 254)
        Me.ButtonSelectDDSexe.Name = "ButtonSelectDDSexe"
        Me.ButtonSelectDDSexe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectDDSexe.TabIndex = 29
        Me.ButtonSelectDDSexe.Text = "..."
        Me.ButtonSelectDDSexe.UseVisualStyleBackColor = True
        '
        'TextBoxDDSExe
        '
        Me.TextBoxDDSExe.Location = New System.Drawing.Point(8, 256)
        Me.TextBoxDDSExe.Name = "TextBoxDDSExe"
        Me.TextBoxDDSExe.ReadOnly = True
        Me.TextBoxDDSExe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxDDSExe.TabIndex = 28
        '
        'LabelDSSExe
        '
        Me.LabelDSSExe.AutoSize = True
        Me.LabelDSSExe.Location = New System.Drawing.Point(8, 240)
        Me.LabelDSSExe.Name = "LabelDSSExe"
        Me.LabelDSSExe.Size = New System.Drawing.Size(138, 13)
        Me.LabelDSSExe.TabIndex = 27
        Me.LabelDSSExe.Text = "DDS Opening Exe Location"
        '
        'CheckBoxDetailedFileNames
        '
        Me.CheckBoxDetailedFileNames.AutoSize = True
        Me.CheckBoxDetailedFileNames.Location = New System.Drawing.Point(8, 121)
        Me.CheckBoxDetailedFileNames.Name = "CheckBoxDetailedFileNames"
        Me.CheckBoxDetailedFileNames.Size = New System.Drawing.Size(180, 17)
        Me.CheckBoxDetailedFileNames.TabIndex = 31
        Me.CheckBoxDetailedFileNames.Text = "Extract With Detailed Extensions"
        Me.CheckBoxDetailedFileNames.UseVisualStyleBackColor = True
        '
        'CheckBoxOODLBypass
        '
        Me.CheckBoxOODLBypass.AutoSize = True
        Me.CheckBoxOODLBypass.Location = New System.Drawing.Point(8, 167)
        Me.CheckBoxOODLBypass.Name = "CheckBoxOODLBypass"
        Me.CheckBoxOODLBypass.Size = New System.Drawing.Size(139, 17)
        Me.CheckBoxOODLBypass.TabIndex = 32
        Me.CheckBoxOODLBypass.Text = "Bypass Oodle Warnings"
        Me.CheckBoxOODLBypass.UseVisualStyleBackColor = True
        '
        'ButtonDownloadBPEExe
        '
        Me.ButtonDownloadBPEExe.Location = New System.Drawing.Point(207, 176)
        Me.ButtonDownloadBPEExe.Name = "ButtonDownloadBPEExe"
        Me.ButtonDownloadBPEExe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadBPEExe.TabIndex = 38
        Me.ButtonDownloadBPEExe.Text = "DL"
        Me.ButtonDownloadBPEExe.UseVisualStyleBackColor = True
        '
        'ButtonSelectBPEExe
        '
        Me.ButtonSelectBPEExe.Location = New System.Drawing.Point(243, 176)
        Me.ButtonSelectBPEExe.Name = "ButtonSelectBPEExe"
        Me.ButtonSelectBPEExe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectBPEExe.TabIndex = 37
        Me.ButtonSelectBPEExe.Text = "..."
        Me.ButtonSelectBPEExe.UseVisualStyleBackColor = True
        '
        'TextBoxBPEExe
        '
        Me.TextBoxBPEExe.Location = New System.Drawing.Point(8, 178)
        Me.TextBoxBPEExe.Name = "TextBoxBPEExe"
        Me.TextBoxBPEExe.ReadOnly = True
        Me.TextBoxBPEExe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxBPEExe.TabIndex = 36
        '
        'LabelBPEExe
        '
        Me.LabelBPEExe.AutoSize = True
        Me.LabelBPEExe.Location = New System.Drawing.Point(8, 162)
        Me.LabelBPEExe.Name = "LabelBPEExe"
        Me.LabelBPEExe.Size = New System.Drawing.Size(93, 13)
        Me.LabelBPEExe.TabIndex = 35
        Me.LabelBPEExe.Text = "BPE Exe Location"
        '
        'CheckBoxExtractAllinPlace
        '
        Me.CheckBoxExtractAllinPlace.AutoSize = True
        Me.CheckBoxExtractAllinPlace.Location = New System.Drawing.Point(8, 144)
        Me.CheckBoxExtractAllinPlace.Name = "CheckBoxExtractAllinPlace"
        Me.CheckBoxExtractAllinPlace.Size = New System.Drawing.Size(220, 17)
        Me.CheckBoxExtractAllinPlace.TabIndex = 39
        Me.CheckBoxExtractAllinPlace.Text = "Extract All Decompresses to New Folders"
        Me.CheckBoxExtractAllinPlace.UseVisualStyleBackColor = True
        '
        'CheckBoxRecycleDeletedFiles
        '
        Me.CheckBoxRecycleDeletedFiles.AutoSize = True
        Me.CheckBoxRecycleDeletedFiles.Location = New System.Drawing.Point(8, 75)
        Me.CheckBoxRecycleDeletedFiles.Name = "CheckBoxRecycleDeletedFiles"
        Me.CheckBoxRecycleDeletedFiles.Size = New System.Drawing.Size(129, 17)
        Me.CheckBoxRecycleDeletedFiles.TabIndex = 45
        Me.CheckBoxRecycleDeletedFiles.Text = "Recycle Deleted Files"
        Me.CheckBoxRecycleDeletedFiles.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageExeTools)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPageDLLCheck)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(284, 386)
        Me.TabControl1.TabIndex = 46
        '
        'TabPageExeTools
        '
        Me.TabPageExeTools.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectCrunchExe)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxCrunchExe)
        Me.TabPageExeTools.Controls.Add(Me.LabelCrunchExe)
        Me.TabPageExeTools.Controls.Add(Me.LabelSkipVersion)
        Me.TabPageExeTools.Controls.Add(Me.ButtonCheckUpdate)
        Me.TabPageExeTools.Controls.Add(Me.LabelHomeDirectory)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxHome)
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectHome)
        Me.TabPageExeTools.Controls.Add(Me.LabelTexConv)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxTexConv)
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectTexConv)
        Me.TabPageExeTools.Controls.Add(Me.ButtonDownloadBPEExe)
        Me.TabPageExeTools.Controls.Add(Me.LabelRadVideo)
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectBPEExe)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxRadVideo)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxBPEExe)
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectRadVideo)
        Me.TabPageExeTools.Controls.Add(Me.LabelBPEExe)
        Me.TabPageExeTools.Controls.Add(Me.ButtonDownloadRadVideo)
        Me.TabPageExeTools.Controls.Add(Me.LabelUnrrbpe)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxUnrrbpe)
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectUnrrbpe)
        Me.TabPageExeTools.Controls.Add(Me.ButtonDownloadDDSexe)
        Me.TabPageExeTools.Controls.Add(Me.ButtonDownloadUnrrbpe)
        Me.TabPageExeTools.Controls.Add(Me.ButtonSelectDDSexe)
        Me.TabPageExeTools.Controls.Add(Me.LabelDSSExe)
        Me.TabPageExeTools.Controls.Add(Me.TextBoxDDSExe)
        Me.TabPageExeTools.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExeTools.Name = "TabPageExeTools"
        Me.TabPageExeTools.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExeTools.Size = New System.Drawing.Size(276, 360)
        Me.TabPageExeTools.TabIndex = 0
        Me.TabPageExeTools.Text = "File Select"
        '
        'ButtonSelectCrunchExe
        '
        Me.ButtonSelectCrunchExe.Location = New System.Drawing.Point(243, 98)
        Me.ButtonSelectCrunchExe.Name = "ButtonSelectCrunchExe"
        Me.ButtonSelectCrunchExe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectCrunchExe.TabIndex = 58
        Me.ButtonSelectCrunchExe.Text = "..."
        Me.ButtonSelectCrunchExe.UseVisualStyleBackColor = True
        '
        'TextBoxCrunchExe
        '
        Me.TextBoxCrunchExe.Location = New System.Drawing.Point(8, 100)
        Me.TextBoxCrunchExe.Name = "TextBoxCrunchExe"
        Me.TextBoxCrunchExe.ReadOnly = True
        Me.TextBoxCrunchExe.Size = New System.Drawing.Size(229, 20)
        Me.TextBoxCrunchExe.TabIndex = 57
        '
        'LabelCrunchExe
        '
        Me.LabelCrunchExe.AutoSize = True
        Me.LabelCrunchExe.Location = New System.Drawing.Point(8, 84)
        Me.LabelCrunchExe.Name = "LabelCrunchExe"
        Me.LabelCrunchExe.Size = New System.Drawing.Size(106, 13)
        Me.LabelCrunchExe.TabIndex = 56
        Me.LabelCrunchExe.Text = "Crunch Exe Location"
        '
        'LabelSkipVersion
        '
        Me.LabelSkipVersion.AutoSize = True
        Me.LabelSkipVersion.Location = New System.Drawing.Point(8, 285)
        Me.LabelSkipVersion.Name = "LabelSkipVersion"
        Me.LabelSkipVersion.Size = New System.Drawing.Size(71, 13)
        Me.LabelSkipVersion.TabIndex = 53
        Me.LabelSkipVersion.Text = "Skipped Ver.:"
        '
        'ButtonCheckUpdate
        '
        Me.ButtonCheckUpdate.Location = New System.Drawing.Point(143, 280)
        Me.ButtonCheckUpdate.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonCheckUpdate.Name = "ButtonCheckUpdate"
        Me.ButtonCheckUpdate.Size = New System.Drawing.Size(125, 23)
        Me.ButtonCheckUpdate.TabIndex = 52
        Me.ButtonCheckUpdate.Text = "Check For Update"
        Me.ButtonCheckUpdate.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.CheckBoxRebuildCak)
        Me.TabPage2.Controls.Add(Me.CheckBoxRecycleDeletedFiles)
        Me.TabPage2.Controls.Add(Me.CheckBoxLoadHome)
        Me.TabPage2.Controls.Add(Me.CheckBoxBackup)
        Me.TabPage2.Controls.Add(Me.CheckBoxDeleteTempBMP)
        Me.TabPage2.Controls.Add(Me.CheckBoxTreeNodeIcons)
        Me.TabPage2.Controls.Add(Me.CheckBoxExtractAllinPlace)
        Me.TabPage2.Controls.Add(Me.CheckBoxDetailedFileNames)
        Me.TabPage2.Controls.Add(Me.CheckBoxOODLBypass)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(276, 360)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Options"
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage3.Controls.Add(Me.CheckBoxShowCAkIntermediates)
        Me.TabPage3.Controls.Add(Me.CheckBoxCreateCAkDef)
        Me.TabPage3.Controls.Add(Me.CheckBoxSuppresHSPCFooters)
        Me.TabPage3.Controls.Add(Me.ButtonResetFormSize)
        Me.TabPage3.Controls.Add(Me.CheckBoxShowSelectedNode)
        Me.TabPage3.Controls.Add(Me.ButtonResetPacs)
        Me.TabPage3.Controls.Add(Me.ButtonResetStrings)
        Me.TabPage3.Controls.Add(Me.LabelDecimalNameLength)
        Me.TabPage3.Controls.Add(Me.LabelHexLength)
        Me.TabPage3.Controls.Add(Me.TrackBarHexLength)
        Me.TabPage3.Controls.Add(Me.TrackBarDecimalNameLength)
        Me.TabPage3.Controls.Add(Me.LabelOodleCompLevel)
        Me.TabPage3.Controls.Add(Me.ComboBoxOodleCompressionLevel)
        Me.TabPage3.Controls.Add(Me.CheckBoxAppendDef)
        Me.TabPage3.Controls.Add(Me.Label2)
        Me.TabPage3.Controls.Add(Me.CheckRelocateMods)
        Me.TabPage3.Controls.Add(Me.CheckDisableModPref)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(276, 360)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Advanced"
        '
        'CheckBoxShowCAkIntermediates
        '
        Me.CheckBoxShowCAkIntermediates.AutoSize = True
        Me.CheckBoxShowCAkIntermediates.Location = New System.Drawing.Point(6, 75)
        Me.CheckBoxShowCAkIntermediates.Name = "CheckBoxShowCAkIntermediates"
        Me.CheckBoxShowCAkIntermediates.Size = New System.Drawing.Size(164, 17)
        Me.CheckBoxShowCAkIntermediates.TabIndex = 53
        Me.CheckBoxShowCAkIntermediates.Text = "Show Intermediate CAk Parts"
        Me.CheckBoxShowCAkIntermediates.UseVisualStyleBackColor = True
        '
        'CheckBoxCreateCAkDef
        '
        Me.CheckBoxCreateCAkDef.AutoSize = True
        Me.CheckBoxCreateCAkDef.Location = New System.Drawing.Point(6, 52)
        Me.CheckBoxCreateCAkDef.Name = "CheckBoxCreateCAkDef"
        Me.CheckBoxCreateCAkDef.Size = New System.Drawing.Size(164, 17)
        Me.CheckBoxCreateCAkDef.TabIndex = 52
        Me.CheckBoxCreateCAkDef.Text = "Create CAk def Files on read."
        Me.CheckBoxCreateCAkDef.UseVisualStyleBackColor = True
        '
        'CheckBoxSuppresHSPCFooters
        '
        Me.CheckBoxSuppresHSPCFooters.AutoSize = True
        Me.CheckBoxSuppresHSPCFooters.Location = New System.Drawing.Point(6, 29)
        Me.CheckBoxSuppresHSPCFooters.Name = "CheckBoxSuppresHSPCFooters"
        Me.CheckBoxSuppresHSPCFooters.Size = New System.Drawing.Size(159, 17)
        Me.CheckBoxSuppresHSPCFooters.TabIndex = 51
        Me.CheckBoxSuppresHSPCFooters.Text = "Suppress HSPC Footer Files"
        Me.CheckBoxSuppresHSPCFooters.UseVisualStyleBackColor = True
        '
        'ButtonResetFormSize
        '
        Me.ButtonResetFormSize.Location = New System.Drawing.Point(88, 322)
        Me.ButtonResetFormSize.Name = "ButtonResetFormSize"
        Me.ButtonResetFormSize.Size = New System.Drawing.Size(100, 24)
        Me.ButtonResetFormSize.TabIndex = 50
        Me.ButtonResetFormSize.Text = "Reset Form Size"
        Me.ButtonResetFormSize.UseVisualStyleBackColor = True
        '
        'CheckBoxShowSelectedNode
        '
        Me.CheckBoxShowSelectedNode.AutoSize = True
        Me.CheckBoxShowSelectedNode.Location = New System.Drawing.Point(6, 6)
        Me.CheckBoxShowSelectedNode.Name = "CheckBoxShowSelectedNode"
        Me.CheckBoxShowSelectedNode.Size = New System.Drawing.Size(205, 17)
        Me.CheckBoxShowSelectedNode.TabIndex = 49
        Me.CheckBoxShowSelectedNode.Text = "Show Selected Node on Current View"
        Me.CheckBoxShowSelectedNode.UseVisualStyleBackColor = True
        '
        'ButtonResetPacs
        '
        Me.ButtonResetPacs.Location = New System.Drawing.Point(190, 322)
        Me.ButtonResetPacs.Name = "ButtonResetPacs"
        Me.ButtonResetPacs.Size = New System.Drawing.Size(80, 24)
        Me.ButtonResetPacs.TabIndex = 48
        Me.ButtonResetPacs.Text = "Reset Pacs"
        Me.ButtonResetPacs.UseVisualStyleBackColor = True
        '
        'ButtonResetStrings
        '
        Me.ButtonResetStrings.Location = New System.Drawing.Point(6, 322)
        Me.ButtonResetStrings.Name = "ButtonResetStrings"
        Me.ButtonResetStrings.Size = New System.Drawing.Size(80, 24)
        Me.ButtonResetStrings.TabIndex = 47
        Me.ButtonResetStrings.Text = "Reset Strings"
        Me.ButtonResetStrings.UseVisualStyleBackColor = True
        '
        'LabelDecimalNameLength
        '
        Me.LabelDecimalNameLength.AutoSize = True
        Me.LabelDecimalNameLength.Location = New System.Drawing.Point(6, 207)
        Me.LabelDecimalNameLength.Name = "LabelDecimalNameLength"
        Me.LabelDecimalNameLength.Size = New System.Drawing.Size(163, 13)
        Me.LabelDecimalNameLength.TabIndex = 46
        Me.LabelDecimalNameLength.Text = "Decimal File Name Min Length: 0"
        '
        'LabelHexLength
        '
        Me.LabelHexLength.AutoSize = True
        Me.LabelHexLength.Location = New System.Drawing.Point(6, 266)
        Me.LabelHexLength.Name = "LabelHexLength"
        Me.LabelHexLength.Size = New System.Drawing.Size(140, 13)
        Me.LabelHexLength.TabIndex = 44
        Me.LabelHexLength.Text = "Hex/Text View Length: 1KB"
        '
        'TrackBarHexLength
        '
        Me.TrackBarHexLength.LargeChange = 100
        Me.TrackBarHexLength.Location = New System.Drawing.Point(6, 282)
        Me.TrackBarHexLength.Maximum = 500
        Me.TrackBarHexLength.Minimum = 1
        Me.TrackBarHexLength.Name = "TrackBarHexLength"
        Me.TrackBarHexLength.Size = New System.Drawing.Size(260, 45)
        Me.TrackBarHexLength.SmallChange = 10
        Me.TrackBarHexLength.TabIndex = 43
        Me.TrackBarHexLength.TickFrequency = 50
        Me.TrackBarHexLength.Value = 1
        '
        'TrackBarDecimalNameLength
        '
        Me.TrackBarDecimalNameLength.LargeChange = 2
        Me.TrackBarDecimalNameLength.Location = New System.Drawing.Point(6, 233)
        Me.TrackBarDecimalNameLength.Maximum = 8
        Me.TrackBarDecimalNameLength.Name = "TrackBarDecimalNameLength"
        Me.TrackBarDecimalNameLength.Size = New System.Drawing.Size(260, 45)
        Me.TrackBarDecimalNameLength.TabIndex = 45
        Me.TrackBarDecimalNameLength.TickFrequency = 2
        '
        'LabelOodleCompLevel
        '
        Me.LabelOodleCompLevel.AutoSize = True
        Me.LabelOodleCompLevel.Location = New System.Drawing.Point(6, 182)
        Me.LabelOodleCompLevel.Name = "LabelOodleCompLevel"
        Me.LabelOodleCompLevel.Size = New System.Drawing.Size(130, 13)
        Me.LabelOodleCompLevel.TabIndex = 36
        Me.LabelOodleCompLevel.Text = "Oodle Compression Level:"
        '
        'ComboBoxOodleCompressionLevel
        '
        Me.ComboBoxOodleCompressionLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBoxOodleCompressionLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxOodleCompressionLevel.FormattingEnabled = True
        Me.ComboBoxOodleCompressionLevel.Location = New System.Drawing.Point(141, 179)
        Me.ComboBoxOodleCompressionLevel.Name = "ComboBoxOodleCompressionLevel"
        Me.ComboBoxOodleCompressionLevel.Size = New System.Drawing.Size(125, 21)
        Me.ComboBoxOodleCompressionLevel.TabIndex = 35
        '
        'CheckBoxAppendDef
        '
        Me.CheckBoxAppendDef.AutoSize = True
        Me.CheckBoxAppendDef.Location = New System.Drawing.Point(6, 111)
        Me.CheckBoxAppendDef.Name = "CheckBoxAppendDef"
        Me.CheckBoxAppendDef.Size = New System.Drawing.Size(141, 17)
        Me.CheckBoxAppendDef.TabIndex = 4
        Me.CheckBoxAppendDef.Text = "Append Def File Rebuild"
        Me.CheckBoxAppendDef.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 95)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Def Rebuild Options:"
        '
        'CheckRelocateMods
        '
        Me.CheckRelocateMods.AutoSize = True
        Me.CheckRelocateMods.Location = New System.Drawing.Point(6, 157)
        Me.CheckRelocateMods.Name = "CheckRelocateMods"
        Me.CheckRelocateMods.Size = New System.Drawing.Size(236, 17)
        Me.CheckRelocateMods.TabIndex = 2
        Me.CheckRelocateMods.Text = "Move Mods from Mod Folder to Home Folder"
        Me.CheckRelocateMods.UseVisualStyleBackColor = True
        '
        'CheckDisableModPref
        '
        Me.CheckDisableModPref.AutoSize = True
        Me.CheckDisableModPref.Location = New System.Drawing.Point(6, 134)
        Me.CheckDisableModPref.Name = "CheckDisableModPref"
        Me.CheckDisableModPref.Size = New System.Drawing.Size(172, 17)
        Me.CheckDisableModPref.TabIndex = 1
        Me.CheckDisableModPref.Text = "Disable Mod Folder Preference"
        Me.CheckDisableModPref.UseVisualStyleBackColor = True
        '
        'TabPageDLLCheck
        '
        Me.TabPageDLLCheck.BackColor = System.Drawing.SystemColors.Control
        Me.TabPageDLLCheck.Controls.Add(Me.Label3)
        Me.TabPageDLLCheck.Controls.Add(Me.ButtonNewtonJsonSelect)
        Me.TabPageDLLCheck.Controls.Add(Me.LabelFrosty)
        Me.TabPageDLLCheck.Controls.Add(Me.ButtonFrostyYukesSelect)
        Me.TabPageDLLCheck.Controls.Add(Me.ButtonOodle7Select)
        Me.TabPageDLLCheck.Controls.Add(Me.LabelOodle_7)
        Me.TabPageDLLCheck.Controls.Add(Me.ButtonSelectFontAwesome)
        Me.TabPageDLLCheck.Controls.Add(Me.LabelFontAwesome)
        Me.TabPageDLLCheck.Controls.Add(Me.ButtonOodle6Select)
        Me.TabPageDLLCheck.Controls.Add(Me.ButtonSelectZlib)
        Me.TabPageDLLCheck.Controls.Add(Me.LabelOodle_6)
        Me.TabPageDLLCheck.Controls.Add(Me.LabelZlib)
        Me.TabPageDLLCheck.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDLLCheck.Name = "TabPageDLLCheck"
        Me.TabPageDLLCheck.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageDLLCheck.Size = New System.Drawing.Size(276, 360)
        Me.TabPageDLLCheck.TabIndex = 3
        Me.TabPageDLLCheck.Text = "DLL"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(127, 13)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "Newt..Json Loaded: True"
        '
        'ButtonNewtonJsonSelect
        '
        Me.ButtonNewtonJsonSelect.Location = New System.Drawing.Point(143, 131)
        Me.ButtonNewtonJsonSelect.Name = "ButtonNewtonJsonSelect"
        Me.ButtonNewtonJsonSelect.Size = New System.Drawing.Size(125, 23)
        Me.ButtonNewtonJsonSelect.TabIndex = 71
        Me.ButtonNewtonJsonSelect.Text = "Locate Json.dll"
        Me.ButtonNewtonJsonSelect.UseVisualStyleBackColor = True
        Me.ButtonNewtonJsonSelect.Visible = False
        '
        'LabelFrosty
        '
        Me.LabelFrosty.AutoSize = True
        Me.LabelFrosty.Location = New System.Drawing.Point(8, 111)
        Me.LabelFrosty.Name = "LabelFrosty"
        Me.LabelFrosty.Size = New System.Drawing.Size(132, 13)
        Me.LabelFrosty.TabIndex = 70
        Me.LabelFrosty.Text = "FrostyYukes Loaded: True"
        '
        'ButtonFrostyYukesSelect
        '
        Me.ButtonFrostyYukesSelect.Location = New System.Drawing.Point(143, 106)
        Me.ButtonFrostyYukesSelect.Name = "ButtonFrostyYukesSelect"
        Me.ButtonFrostyYukesSelect.Size = New System.Drawing.Size(125, 23)
        Me.ButtonFrostyYukesSelect.TabIndex = 69
        Me.ButtonFrostyYukesSelect.Text = "Locate FrostyYukes"
        Me.ButtonFrostyYukesSelect.UseVisualStyleBackColor = True
        Me.ButtonFrostyYukesSelect.Visible = False
        '
        'ButtonOodle7Select
        '
        Me.ButtonOodle7Select.Location = New System.Drawing.Point(143, 81)
        Me.ButtonOodle7Select.Name = "ButtonOodle7Select"
        Me.ButtonOodle7Select.Size = New System.Drawing.Size(125, 23)
        Me.ButtonOodle7Select.TabIndex = 68
        Me.ButtonOodle7Select.Text = "Locate oo2core_7"
        Me.ButtonOodle7Select.UseVisualStyleBackColor = True
        Me.ButtonOodle7Select.Visible = False
        '
        'LabelOodle_7
        '
        Me.LabelOodle_7.AutoSize = True
        Me.LabelOodle_7.Location = New System.Drawing.Point(8, 86)
        Me.LabelOodle_7.Name = "LabelOodle_7"
        Me.LabelOodle_7.Size = New System.Drawing.Size(111, 13)
        Me.LabelOodle_7.TabIndex = 67
        Me.LabelOodle_7.Text = "Oodle 7 Loaded: True"
        '
        'ButtonSelectFontAwesome
        '
        Me.ButtonSelectFontAwesome.Location = New System.Drawing.Point(143, 6)
        Me.ButtonSelectFontAwesome.Name = "ButtonSelectFontAwesome"
        Me.ButtonSelectFontAwesome.Size = New System.Drawing.Size(125, 23)
        Me.ButtonSelectFontAwesome.TabIndex = 66
        Me.ButtonSelectFontAwesome.Text = "Locate FontAwesome"
        Me.ButtonSelectFontAwesome.UseVisualStyleBackColor = True
        Me.ButtonSelectFontAwesome.Visible = False
        '
        'LabelFontAwesome
        '
        Me.LabelFontAwesome.AutoSize = True
        Me.LabelFontAwesome.Location = New System.Drawing.Point(8, 11)
        Me.LabelFontAwesome.Name = "LabelFontAwesome"
        Me.LabelFontAwesome.Size = New System.Drawing.Size(122, 13)
        Me.LabelFontAwesome.TabIndex = 65
        Me.LabelFontAwesome.Text = "FontAwe. Loaded: False"
        '
        'ButtonOodle6Select
        '
        Me.ButtonOodle6Select.Location = New System.Drawing.Point(143, 56)
        Me.ButtonOodle6Select.Name = "ButtonOodle6Select"
        Me.ButtonOodle6Select.Size = New System.Drawing.Size(125, 23)
        Me.ButtonOodle6Select.TabIndex = 64
        Me.ButtonOodle6Select.Text = "Locate oo2core_6"
        Me.ButtonOodle6Select.UseVisualStyleBackColor = True
        Me.ButtonOodle6Select.Visible = False
        '
        'ButtonSelectZlib
        '
        Me.ButtonSelectZlib.Location = New System.Drawing.Point(143, 31)
        Me.ButtonSelectZlib.Name = "ButtonSelectZlib"
        Me.ButtonSelectZlib.Size = New System.Drawing.Size(125, 23)
        Me.ButtonSelectZlib.TabIndex = 63
        Me.ButtonSelectZlib.Text = "Locate Iconic.Zlib"
        Me.ButtonSelectZlib.UseVisualStyleBackColor = True
        Me.ButtonSelectZlib.Visible = False
        '
        'LabelOodle_6
        '
        Me.LabelOodle_6.AutoSize = True
        Me.LabelOodle_6.Location = New System.Drawing.Point(8, 61)
        Me.LabelOodle_6.Name = "LabelOodle_6"
        Me.LabelOodle_6.Size = New System.Drawing.Size(111, 13)
        Me.LabelOodle_6.TabIndex = 62
        Me.LabelOodle_6.Text = "Oodle 6 Loaded: True"
        '
        'LabelZlib
        '
        Me.LabelZlib.AutoSize = True
        Me.LabelZlib.Location = New System.Drawing.Point(8, 36)
        Me.LabelZlib.Name = "LabelZlib"
        Me.LabelZlib.Size = New System.Drawing.Size(114, 13)
        Me.LabelZlib.TabIndex = 61
        Me.LabelZlib.Text = "Zlib DLL Loaded: True"
        '
        'CheckBoxRebuildCak
        '
        Me.CheckBoxRebuildCak.AutoSize = True
        Me.CheckBoxRebuildCak.Location = New System.Drawing.Point(8, 190)
        Me.CheckBoxRebuildCak.Name = "CheckBoxRebuildCak"
        Me.CheckBoxRebuildCak.Size = New System.Drawing.Size(109, 17)
        Me.CheckBoxRebuildCak.TabIndex = 46
        Me.CheckBoxRebuildCak.Text = "Rebuild CAk Files"
        Me.CheckBoxRebuildCak.UseVisualStyleBackColor = True
        '
        'OptionsMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(284, 386)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Options Menu"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageExeTools.ResumeLayout(False)
        Me.TabPageExeTools.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.TrackBarHexLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarDecimalNameLength, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageDLLCheck.ResumeLayout(False)
        Me.TabPageDLLCheck.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents LabelHomeDirectory As Label
    Friend WithEvents TextBoxHome As TextBox
    Friend WithEvents ButtonSelectHome As Button
    Friend WithEvents LabelTexConv As Label
    Friend WithEvents TextBoxTexConv As TextBox
    Friend WithEvents ButtonSelectTexConv As Button
    Friend WithEvents LabelRadVideo As Label
    Friend WithEvents TextBoxRadVideo As TextBox
    Friend WithEvents ButtonSelectRadVideo As Button
    Friend WithEvents LabelUnrrbpe As Label
    Friend WithEvents TextBoxUnrrbpe As TextBox
    Friend WithEvents ButtonSelectUnrrbpe As Button
    Friend WithEvents ButtonDownloadUnrrbpe As Button
    Friend WithEvents ButtonDownloadRadVideo As Button
    Friend WithEvents CheckBoxLoadHome As CheckBox
    Friend WithEvents CheckBoxBackup As CheckBox
    Friend WithEvents CheckBoxDeleteTempBMP As CheckBox
    Friend WithEvents CheckBoxTreeNodeIcons As CheckBox
    Friend WithEvents ButtonDownloadDDSexe As Button
    Friend WithEvents ButtonSelectDDSexe As Button
    Friend WithEvents TextBoxDDSExe As TextBox
    Friend WithEvents LabelDSSExe As Label
    Friend WithEvents CheckBoxDetailedFileNames As CheckBox
    Friend WithEvents CheckBoxOODLBypass As CheckBox
    Friend WithEvents ButtonDownloadBPEExe As Button
    Friend WithEvents ButtonSelectBPEExe As Button
    Friend WithEvents TextBoxBPEExe As TextBox
    Friend WithEvents LabelBPEExe As Label
    Friend WithEvents CheckBoxExtractAllinPlace As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents CheckBoxRecycleDeletedFiles As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageExeTools As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents CheckBoxAppendDef As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckRelocateMods As CheckBox
    Friend WithEvents CheckDisableModPref As CheckBox
    Friend WithEvents LabelOodleCompLevel As Label
    Friend WithEvents ComboBoxOodleCompressionLevel As ComboBox
    Friend WithEvents ButtonResetPacs As Button
    Friend WithEvents ButtonResetStrings As Button
    Friend WithEvents LabelDecimalNameLength As Label
    Friend WithEvents LabelHexLength As Label
    Friend WithEvents TrackBarHexLength As TrackBar
    Friend WithEvents TrackBarDecimalNameLength As TrackBar
    Friend WithEvents CheckBoxShowSelectedNode As CheckBox
    Friend WithEvents ButtonResetFormSize As Button
    Friend WithEvents LabelSkipVersion As Label
    Friend WithEvents ButtonCheckUpdate As Button
    Friend WithEvents CheckBoxSuppresHSPCFooters As CheckBox
    Friend WithEvents CheckBoxCreateCAkDef As CheckBox
    Friend WithEvents CheckBoxShowCAkIntermediates As CheckBox
    Friend WithEvents ButtonSelectCrunchExe As Button
    Friend WithEvents TextBoxCrunchExe As TextBox
    Friend WithEvents LabelCrunchExe As Label
    Friend WithEvents TabPageDLLCheck As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents ButtonNewtonJsonSelect As Button
    Friend WithEvents LabelFrosty As Label
    Friend WithEvents ButtonFrostyYukesSelect As Button
    Friend WithEvents ButtonOodle7Select As Button
    Friend WithEvents LabelOodle_7 As Label
    Friend WithEvents ButtonSelectFontAwesome As Button
    Friend WithEvents LabelFontAwesome As Label
    Friend WithEvents ButtonOodle6Select As Button
    Friend WithEvents ButtonSelectZlib As Button
    Friend WithEvents LabelOodle_6 As Label
    Friend WithEvents LabelZlib As Label
    Friend WithEvents CheckBoxRebuildCak As CheckBox
End Class
