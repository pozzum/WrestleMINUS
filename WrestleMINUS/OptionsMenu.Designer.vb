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
        Me.Label1 = New System.Windows.Forms.Label()
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
        Me.LabelZlib = New System.Windows.Forms.Label()
        Me.LabelOodle = New System.Windows.Forms.Label()
        Me.ButtonSelectZlib = New System.Windows.Forms.Button()
        Me.ButtonOodleSelect = New System.Windows.Forms.Button()
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
        Me.LabelFontAwesome = New System.Windows.Forms.Label()
        Me.ButtonSelectFontAwesome = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CheckBoxRecycleDeletedFiles = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
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
        Me.ButtonCheckUpdate = New System.Windows.Forms.Button()
        Me.LabelSkipVersion = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.TrackBarHexLength, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBarDecimalNameLength, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Home Directory"
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
        Me.LabelRadVideo.Location = New System.Drawing.Point(8, 84)
        Me.LabelRadVideo.Name = "LabelRadVideo"
        Me.LabelRadVideo.Size = New System.Drawing.Size(119, 13)
        Me.LabelRadVideo.TabIndex = 6
        Me.LabelRadVideo.Text = "RadVideo Exe Location"
        '
        'TextBoxRadVideo
        '
        Me.TextBoxRadVideo.Location = New System.Drawing.Point(8, 100)
        Me.TextBoxRadVideo.Name = "TextBoxRadVideo"
        Me.TextBoxRadVideo.ReadOnly = True
        Me.TextBoxRadVideo.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxRadVideo.TabIndex = 7
        '
        'ButtonSelectRadVideo
        '
        Me.ButtonSelectRadVideo.Location = New System.Drawing.Point(243, 98)
        Me.ButtonSelectRadVideo.Name = "ButtonSelectRadVideo"
        Me.ButtonSelectRadVideo.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectRadVideo.TabIndex = 8
        Me.ButtonSelectRadVideo.Text = "..."
        Me.ButtonSelectRadVideo.UseVisualStyleBackColor = True
        '
        'LabelUnrrbpe
        '
        Me.LabelUnrrbpe.AutoSize = True
        Me.LabelUnrrbpe.Location = New System.Drawing.Point(8, 162)
        Me.LabelUnrrbpe.Name = "LabelUnrrbpe"
        Me.LabelUnrrbpe.Size = New System.Drawing.Size(110, 13)
        Me.LabelUnrrbpe.TabIndex = 9
        Me.LabelUnrrbpe.Text = "Unrrbpe Exe Location"
        '
        'TextBoxUnrrbpe
        '
        Me.TextBoxUnrrbpe.Location = New System.Drawing.Point(8, 178)
        Me.TextBoxUnrrbpe.Name = "TextBoxUnrrbpe"
        Me.TextBoxUnrrbpe.ReadOnly = True
        Me.TextBoxUnrrbpe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxUnrrbpe.TabIndex = 10
        '
        'ButtonSelectUnrrbpe
        '
        Me.ButtonSelectUnrrbpe.Location = New System.Drawing.Point(243, 176)
        Me.ButtonSelectUnrrbpe.Name = "ButtonSelectUnrrbpe"
        Me.ButtonSelectUnrrbpe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectUnrrbpe.TabIndex = 11
        Me.ButtonSelectUnrrbpe.Text = "..."
        Me.ButtonSelectUnrrbpe.UseVisualStyleBackColor = True
        '
        'ButtonDownloadUnrrbpe
        '
        Me.ButtonDownloadUnrrbpe.Location = New System.Drawing.Point(207, 176)
        Me.ButtonDownloadUnrrbpe.Name = "ButtonDownloadUnrrbpe"
        Me.ButtonDownloadUnrrbpe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadUnrrbpe.TabIndex = 12
        Me.ButtonDownloadUnrrbpe.Text = "DL"
        Me.ButtonDownloadUnrrbpe.UseVisualStyleBackColor = True
        '
        'ButtonDownloadRadVideo
        '
        Me.ButtonDownloadRadVideo.Location = New System.Drawing.Point(207, 98)
        Me.ButtonDownloadRadVideo.Name = "ButtonDownloadRadVideo"
        Me.ButtonDownloadRadVideo.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadRadVideo.TabIndex = 13
        Me.ButtonDownloadRadVideo.Text = "DL"
        Me.ButtonDownloadRadVideo.UseVisualStyleBackColor = True
        '
        'LabelZlib
        '
        Me.LabelZlib.AutoSize = True
        Me.LabelZlib.Location = New System.Drawing.Point(8, 274)
        Me.LabelZlib.Name = "LabelZlib"
        Me.LabelZlib.Size = New System.Drawing.Size(114, 13)
        Me.LabelZlib.TabIndex = 14
        Me.LabelZlib.Text = "Zlib DLL Loaded: True"
        '
        'LabelOodle
        '
        Me.LabelOodle.AutoSize = True
        Me.LabelOodle.Location = New System.Drawing.Point(8, 301)
        Me.LabelOodle.Name = "LabelOodle"
        Me.LabelOodle.Size = New System.Drawing.Size(125, 13)
        Me.LabelOodle.TabIndex = 15
        Me.LabelOodle.Text = "Oodle DLL Loaded: True"
        '
        'ButtonSelectZlib
        '
        Me.ButtonSelectZlib.Location = New System.Drawing.Point(143, 269)
        Me.ButtonSelectZlib.Name = "ButtonSelectZlib"
        Me.ButtonSelectZlib.Size = New System.Drawing.Size(125, 23)
        Me.ButtonSelectZlib.TabIndex = 16
        Me.ButtonSelectZlib.Text = "Locate Iconic.Zlib"
        Me.ButtonSelectZlib.UseVisualStyleBackColor = True
        Me.ButtonSelectZlib.Visible = False
        '
        'ButtonOodleSelect
        '
        Me.ButtonOodleSelect.Location = New System.Drawing.Point(143, 296)
        Me.ButtonOodleSelect.Name = "ButtonOodleSelect"
        Me.ButtonOodleSelect.Size = New System.Drawing.Size(125, 23)
        Me.ButtonOodleSelect.TabIndex = 17
        Me.ButtonOodleSelect.Text = "Locate oo2core"
        Me.ButtonOodleSelect.UseVisualStyleBackColor = True
        Me.ButtonOodleSelect.Visible = False
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
        Me.ButtonDownloadDDSexe.Location = New System.Drawing.Point(207, 215)
        Me.ButtonDownloadDDSexe.Name = "ButtonDownloadDDSexe"
        Me.ButtonDownloadDDSexe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadDDSexe.TabIndex = 30
        Me.ButtonDownloadDDSexe.Text = "DL"
        Me.ButtonDownloadDDSexe.UseVisualStyleBackColor = True
        '
        'ButtonSelectDDSexe
        '
        Me.ButtonSelectDDSexe.Location = New System.Drawing.Point(243, 215)
        Me.ButtonSelectDDSexe.Name = "ButtonSelectDDSexe"
        Me.ButtonSelectDDSexe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectDDSexe.TabIndex = 29
        Me.ButtonSelectDDSexe.Text = "..."
        Me.ButtonSelectDDSexe.UseVisualStyleBackColor = True
        '
        'TextBoxDDSExe
        '
        Me.TextBoxDDSExe.Location = New System.Drawing.Point(8, 217)
        Me.TextBoxDDSExe.Name = "TextBoxDDSExe"
        Me.TextBoxDDSExe.ReadOnly = True
        Me.TextBoxDDSExe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxDDSExe.TabIndex = 28
        '
        'LabelDSSExe
        '
        Me.LabelDSSExe.AutoSize = True
        Me.LabelDSSExe.Location = New System.Drawing.Point(8, 201)
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
        Me.ButtonDownloadBPEExe.Location = New System.Drawing.Point(207, 137)
        Me.ButtonDownloadBPEExe.Name = "ButtonDownloadBPEExe"
        Me.ButtonDownloadBPEExe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadBPEExe.TabIndex = 38
        Me.ButtonDownloadBPEExe.Text = "DL"
        Me.ButtonDownloadBPEExe.UseVisualStyleBackColor = True
        '
        'ButtonSelectBPEExe
        '
        Me.ButtonSelectBPEExe.Location = New System.Drawing.Point(243, 137)
        Me.ButtonSelectBPEExe.Name = "ButtonSelectBPEExe"
        Me.ButtonSelectBPEExe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectBPEExe.TabIndex = 37
        Me.ButtonSelectBPEExe.Text = "..."
        Me.ButtonSelectBPEExe.UseVisualStyleBackColor = True
        '
        'TextBoxBPEExe
        '
        Me.TextBoxBPEExe.Location = New System.Drawing.Point(8, 139)
        Me.TextBoxBPEExe.Name = "TextBoxBPEExe"
        Me.TextBoxBPEExe.ReadOnly = True
        Me.TextBoxBPEExe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxBPEExe.TabIndex = 36
        '
        'LabelBPEExe
        '
        Me.LabelBPEExe.AutoSize = True
        Me.LabelBPEExe.Location = New System.Drawing.Point(8, 123)
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
        'LabelFontAwesome
        '
        Me.LabelFontAwesome.AutoSize = True
        Me.LabelFontAwesome.Location = New System.Drawing.Point(8, 247)
        Me.LabelFontAwesome.Name = "LabelFontAwesome"
        Me.LabelFontAwesome.Size = New System.Drawing.Size(122, 13)
        Me.LabelFontAwesome.TabIndex = 43
        Me.LabelFontAwesome.Text = "FontAwe. Loaded: False"
        '
        'ButtonSelectFontAwesome
        '
        Me.ButtonSelectFontAwesome.Location = New System.Drawing.Point(143, 242)
        Me.ButtonSelectFontAwesome.Name = "ButtonSelectFontAwesome"
        Me.ButtonSelectFontAwesome.Size = New System.Drawing.Size(125, 23)
        Me.ButtonSelectFontAwesome.TabIndex = 44
        Me.ButtonSelectFontAwesome.Text = "Locate FontAwesome"
        Me.ButtonSelectFontAwesome.UseVisualStyleBackColor = True
        Me.ButtonSelectFontAwesome.Visible = False
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
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(284, 386)
        Me.TabControl1.TabIndex = 46
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.LabelSkipVersion)
        Me.TabPage1.Controls.Add(Me.ButtonCheckUpdate)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.ButtonSelectFontAwesome)
        Me.TabPage1.Controls.Add(Me.TextBoxHome)
        Me.TabPage1.Controls.Add(Me.LabelFontAwesome)
        Me.TabPage1.Controls.Add(Me.ButtonSelectHome)
        Me.TabPage1.Controls.Add(Me.LabelTexConv)
        Me.TabPage1.Controls.Add(Me.TextBoxTexConv)
        Me.TabPage1.Controls.Add(Me.ButtonSelectTexConv)
        Me.TabPage1.Controls.Add(Me.ButtonDownloadBPEExe)
        Me.TabPage1.Controls.Add(Me.ButtonOodleSelect)
        Me.TabPage1.Controls.Add(Me.LabelRadVideo)
        Me.TabPage1.Controls.Add(Me.ButtonSelectZlib)
        Me.TabPage1.Controls.Add(Me.ButtonSelectBPEExe)
        Me.TabPage1.Controls.Add(Me.LabelOodle)
        Me.TabPage1.Controls.Add(Me.TextBoxRadVideo)
        Me.TabPage1.Controls.Add(Me.LabelZlib)
        Me.TabPage1.Controls.Add(Me.TextBoxBPEExe)
        Me.TabPage1.Controls.Add(Me.ButtonSelectRadVideo)
        Me.TabPage1.Controls.Add(Me.LabelBPEExe)
        Me.TabPage1.Controls.Add(Me.ButtonDownloadRadVideo)
        Me.TabPage1.Controls.Add(Me.LabelUnrrbpe)
        Me.TabPage1.Controls.Add(Me.TextBoxUnrrbpe)
        Me.TabPage1.Controls.Add(Me.ButtonSelectUnrrbpe)
        Me.TabPage1.Controls.Add(Me.ButtonDownloadDDSexe)
        Me.TabPage1.Controls.Add(Me.ButtonDownloadUnrrbpe)
        Me.TabPage1.Controls.Add(Me.ButtonSelectDDSexe)
        Me.TabPage1.Controls.Add(Me.LabelDSSExe)
        Me.TabPage1.Controls.Add(Me.TextBoxDDSExe)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(276, 360)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "File Select"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
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
        'ButtonResetFormSize
        '
        Me.ButtonResetFormSize.Location = New System.Drawing.Point(88, 253)
        Me.ButtonResetFormSize.Name = "ButtonResetFormSize"
        Me.ButtonResetFormSize.Size = New System.Drawing.Size(100, 23)
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
        Me.ButtonResetPacs.Location = New System.Drawing.Point(190, 253)
        Me.ButtonResetPacs.Name = "ButtonResetPacs"
        Me.ButtonResetPacs.Size = New System.Drawing.Size(80, 23)
        Me.ButtonResetPacs.TabIndex = 48
        Me.ButtonResetPacs.Text = "Reset Pacs"
        Me.ButtonResetPacs.UseVisualStyleBackColor = True
        '
        'ButtonResetStrings
        '
        Me.ButtonResetStrings.Location = New System.Drawing.Point(6, 253)
        Me.ButtonResetStrings.Name = "ButtonResetStrings"
        Me.ButtonResetStrings.Size = New System.Drawing.Size(80, 23)
        Me.ButtonResetStrings.TabIndex = 47
        Me.ButtonResetStrings.Text = "Reset Strings"
        Me.ButtonResetStrings.UseVisualStyleBackColor = True
        '
        'LabelDecimalNameLength
        '
        Me.LabelDecimalNameLength.AutoSize = True
        Me.LabelDecimalNameLength.Location = New System.Drawing.Point(6, 138)
        Me.LabelDecimalNameLength.Name = "LabelDecimalNameLength"
        Me.LabelDecimalNameLength.Size = New System.Drawing.Size(163, 13)
        Me.LabelDecimalNameLength.TabIndex = 46
        Me.LabelDecimalNameLength.Text = "Decimal File Name Min Length: 0"
        '
        'LabelHexLength
        '
        Me.LabelHexLength.AutoSize = True
        Me.LabelHexLength.Location = New System.Drawing.Point(6, 197)
        Me.LabelHexLength.Name = "LabelHexLength"
        Me.LabelHexLength.Size = New System.Drawing.Size(140, 13)
        Me.LabelHexLength.TabIndex = 44
        Me.LabelHexLength.Text = "Hex/Text View Length: 1KB"
        '
        'TrackBarHexLength
        '
        Me.TrackBarHexLength.LargeChange = 100
        Me.TrackBarHexLength.Location = New System.Drawing.Point(6, 213)
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
        Me.TrackBarDecimalNameLength.Location = New System.Drawing.Point(6, 164)
        Me.TrackBarDecimalNameLength.Maximum = 8
        Me.TrackBarDecimalNameLength.Name = "TrackBarDecimalNameLength"
        Me.TrackBarDecimalNameLength.Size = New System.Drawing.Size(260, 45)
        Me.TrackBarDecimalNameLength.TabIndex = 45
        Me.TrackBarDecimalNameLength.TickFrequency = 2
        '
        'LabelOodleCompLevel
        '
        Me.LabelOodleCompLevel.AutoSize = True
        Me.LabelOodleCompLevel.Location = New System.Drawing.Point(6, 113)
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
        Me.ComboBoxOodleCompressionLevel.Location = New System.Drawing.Point(141, 110)
        Me.ComboBoxOodleCompressionLevel.Name = "ComboBoxOodleCompressionLevel"
        Me.ComboBoxOodleCompressionLevel.Size = New System.Drawing.Size(125, 21)
        Me.ComboBoxOodleCompressionLevel.TabIndex = 35
        '
        'CheckBoxAppendDef
        '
        Me.CheckBoxAppendDef.AutoSize = True
        Me.CheckBoxAppendDef.Location = New System.Drawing.Point(6, 41)
        Me.CheckBoxAppendDef.Name = "CheckBoxAppendDef"
        Me.CheckBoxAppendDef.Size = New System.Drawing.Size(141, 17)
        Me.CheckBoxAppendDef.TabIndex = 4
        Me.CheckBoxAppendDef.Text = "Append Def File Rebuild"
        Me.CheckBoxAppendDef.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Def Rebuild Options:"
        '
        'CheckRelocateMods
        '
        Me.CheckRelocateMods.AutoSize = True
        Me.CheckRelocateMods.Location = New System.Drawing.Point(6, 87)
        Me.CheckRelocateMods.Name = "CheckRelocateMods"
        Me.CheckRelocateMods.Size = New System.Drawing.Size(236, 17)
        Me.CheckRelocateMods.TabIndex = 2
        Me.CheckRelocateMods.Text = "Move Mods from Mod Folder to Home Folder"
        Me.CheckRelocateMods.UseVisualStyleBackColor = True
        '
        'CheckDisableModPref
        '
        Me.CheckDisableModPref.AutoSize = True
        Me.CheckDisableModPref.Location = New System.Drawing.Point(6, 64)
        Me.CheckDisableModPref.Name = "CheckDisableModPref"
        Me.CheckDisableModPref.Size = New System.Drawing.Size(172, 17)
        Me.CheckDisableModPref.TabIndex = 1
        Me.CheckDisableModPref.Text = "Disable Mod Folder Preference"
        Me.CheckDisableModPref.UseVisualStyleBackColor = True
        '
        'ButtonCheckUpdate
        '
        Me.ButtonCheckUpdate.Location = New System.Drawing.Point(143, 323)
        Me.ButtonCheckUpdate.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonCheckUpdate.Name = "ButtonCheckUpdate"
        Me.ButtonCheckUpdate.Size = New System.Drawing.Size(125, 23)
        Me.ButtonCheckUpdate.TabIndex = 52
        Me.ButtonCheckUpdate.Text = "Check For Update"
        Me.ButtonCheckUpdate.UseVisualStyleBackColor = True
        '
        'LabelSkipVersion
        '
        Me.LabelSkipVersion.AutoSize = True
        Me.LabelSkipVersion.Location = New System.Drawing.Point(8, 328)
        Me.LabelSkipVersion.Name = "LabelSkipVersion"
        Me.LabelSkipVersion.Size = New System.Drawing.Size(71, 13)
        Me.LabelSkipVersion.TabIndex = 53
        Me.LabelSkipVersion.Text = "Skipped Ver.:"
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
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.TrackBarHexLength, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBarDecimalNameLength, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
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
    Friend WithEvents LabelZlib As Label
    Friend WithEvents LabelOodle As Label
    Friend WithEvents ButtonSelectZlib As Button
    Friend WithEvents ButtonOodleSelect As Button
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
    Friend WithEvents LabelFontAwesome As Label
    Friend WithEvents ButtonSelectFontAwesome As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents CheckBoxRecycleDeletedFiles As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
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
End Class
