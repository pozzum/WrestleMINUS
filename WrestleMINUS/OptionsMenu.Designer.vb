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
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Home Directory"
        '
        'TextBoxHome
        '
        Me.TextBoxHome.Location = New System.Drawing.Point(12, 25)
        Me.TextBoxHome.Name = "TextBoxHome"
        Me.TextBoxHome.ReadOnly = True
        Me.TextBoxHome.Size = New System.Drawing.Size(229, 20)
        Me.TextBoxHome.TabIndex = 1
        '
        'ButtonSelectHome
        '
        Me.ButtonSelectHome.Location = New System.Drawing.Point(247, 23)
        Me.ButtonSelectHome.Name = "ButtonSelectHome"
        Me.ButtonSelectHome.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectHome.TabIndex = 2
        Me.ButtonSelectHome.Text = "..."
        Me.ButtonSelectHome.UseVisualStyleBackColor = True
        '
        'LabelTexConv
        '
        Me.LabelTexConv.AutoSize = True
        Me.LabelTexConv.Location = New System.Drawing.Point(12, 48)
        Me.LabelTexConv.Name = "LabelTexConv"
        Me.LabelTexConv.Size = New System.Drawing.Size(115, 13)
        Me.LabelTexConv.TabIndex = 3
        Me.LabelTexConv.Text = "TexConv Exe Location"
        '
        'TextBoxTexConv
        '
        Me.TextBoxTexConv.Location = New System.Drawing.Point(12, 64)
        Me.TextBoxTexConv.Name = "TextBoxTexConv"
        Me.TextBoxTexConv.ReadOnly = True
        Me.TextBoxTexConv.Size = New System.Drawing.Size(229, 20)
        Me.TextBoxTexConv.TabIndex = 4
        '
        'ButtonSelectTexConv
        '
        Me.ButtonSelectTexConv.Location = New System.Drawing.Point(247, 62)
        Me.ButtonSelectTexConv.Name = "ButtonSelectTexConv"
        Me.ButtonSelectTexConv.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectTexConv.TabIndex = 5
        Me.ButtonSelectTexConv.Text = "..."
        Me.ButtonSelectTexConv.UseVisualStyleBackColor = True
        '
        'LabelRadVideo
        '
        Me.LabelRadVideo.AutoSize = True
        Me.LabelRadVideo.Location = New System.Drawing.Point(12, 87)
        Me.LabelRadVideo.Name = "LabelRadVideo"
        Me.LabelRadVideo.Size = New System.Drawing.Size(119, 13)
        Me.LabelRadVideo.TabIndex = 6
        Me.LabelRadVideo.Text = "RadVideo Exe Location"
        '
        'TextBoxRadVideo
        '
        Me.TextBoxRadVideo.Location = New System.Drawing.Point(12, 103)
        Me.TextBoxRadVideo.Name = "TextBoxRadVideo"
        Me.TextBoxRadVideo.ReadOnly = True
        Me.TextBoxRadVideo.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxRadVideo.TabIndex = 7
        '
        'ButtonSelectRadVideo
        '
        Me.ButtonSelectRadVideo.Location = New System.Drawing.Point(247, 101)
        Me.ButtonSelectRadVideo.Name = "ButtonSelectRadVideo"
        Me.ButtonSelectRadVideo.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectRadVideo.TabIndex = 8
        Me.ButtonSelectRadVideo.Text = "..."
        Me.ButtonSelectRadVideo.UseVisualStyleBackColor = True
        '
        'LabelUnrrbpe
        '
        Me.LabelUnrrbpe.AutoSize = True
        Me.LabelUnrrbpe.Location = New System.Drawing.Point(12, 126)
        Me.LabelUnrrbpe.Name = "LabelUnrrbpe"
        Me.LabelUnrrbpe.Size = New System.Drawing.Size(110, 13)
        Me.LabelUnrrbpe.TabIndex = 9
        Me.LabelUnrrbpe.Text = "Unrrbpe Exe Location"
        '
        'TextBoxUnrrbpe
        '
        Me.TextBoxUnrrbpe.Location = New System.Drawing.Point(12, 142)
        Me.TextBoxUnrrbpe.Name = "TextBoxUnrrbpe"
        Me.TextBoxUnrrbpe.ReadOnly = True
        Me.TextBoxUnrrbpe.Size = New System.Drawing.Size(193, 20)
        Me.TextBoxUnrrbpe.TabIndex = 10
        '
        'ButtonSelectUnrrbpe
        '
        Me.ButtonSelectUnrrbpe.Location = New System.Drawing.Point(247, 140)
        Me.ButtonSelectUnrrbpe.Name = "ButtonSelectUnrrbpe"
        Me.ButtonSelectUnrrbpe.Size = New System.Drawing.Size(25, 23)
        Me.ButtonSelectUnrrbpe.TabIndex = 11
        Me.ButtonSelectUnrrbpe.Text = "..."
        Me.ButtonSelectUnrrbpe.UseVisualStyleBackColor = True
        '
        'ButtonDownloadUnrrbpe
        '
        Me.ButtonDownloadUnrrbpe.Location = New System.Drawing.Point(211, 140)
        Me.ButtonDownloadUnrrbpe.Name = "ButtonDownloadUnrrbpe"
        Me.ButtonDownloadUnrrbpe.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadUnrrbpe.TabIndex = 12
        Me.ButtonDownloadUnrrbpe.Text = "DL"
        Me.ButtonDownloadUnrrbpe.UseVisualStyleBackColor = True
        '
        'ButtonDownloadRadVideo
        '
        Me.ButtonDownloadRadVideo.Location = New System.Drawing.Point(211, 101)
        Me.ButtonDownloadRadVideo.Name = "ButtonDownloadRadVideo"
        Me.ButtonDownloadRadVideo.Size = New System.Drawing.Size(30, 23)
        Me.ButtonDownloadRadVideo.TabIndex = 13
        Me.ButtonDownloadRadVideo.Text = "DL"
        Me.ButtonDownloadRadVideo.UseVisualStyleBackColor = True
        '
        'LabelZlib
        '
        Me.LabelZlib.AutoSize = True
        Me.LabelZlib.Location = New System.Drawing.Point(12, 174)
        Me.LabelZlib.Name = "LabelZlib"
        Me.LabelZlib.Size = New System.Drawing.Size(114, 13)
        Me.LabelZlib.TabIndex = 14
        Me.LabelZlib.Text = "Zlib DLL Loaded: True"
        '
        'LabelOodle
        '
        Me.LabelOodle.AutoSize = True
        Me.LabelOodle.Location = New System.Drawing.Point(12, 203)
        Me.LabelOodle.Name = "LabelOodle"
        Me.LabelOodle.Size = New System.Drawing.Size(125, 13)
        Me.LabelOodle.TabIndex = 15
        Me.LabelOodle.Text = "Oodle DLL Loaded: True"
        '
        'ButtonSelectZlib
        '
        Me.ButtonSelectZlib.Location = New System.Drawing.Point(147, 169)
        Me.ButtonSelectZlib.Name = "ButtonSelectZlib"
        Me.ButtonSelectZlib.Size = New System.Drawing.Size(125, 23)
        Me.ButtonSelectZlib.TabIndex = 16
        Me.ButtonSelectZlib.Text = "Locate Iconic.Zlib"
        Me.ButtonSelectZlib.UseVisualStyleBackColor = True
        Me.ButtonSelectZlib.Visible = False
        '
        'ButtonOodleSelect
        '
        Me.ButtonOodleSelect.Location = New System.Drawing.Point(147, 198)
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
        Me.CheckBoxLoadHome.Location = New System.Drawing.Point(15, 232)
        Me.CheckBoxLoadHome.Name = "CheckBoxLoadHome"
        Me.CheckBoxLoadHome.Size = New System.Drawing.Size(135, 17)
        Me.CheckBoxLoadHome.TabIndex = 18
        Me.CheckBoxLoadHome.Text = "Load Home on Launch"
        Me.CheckBoxLoadHome.UseVisualStyleBackColor = True
        '
        'OptionsMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.CheckBoxLoadHome)
        Me.Controls.Add(Me.ButtonOodleSelect)
        Me.Controls.Add(Me.ButtonSelectZlib)
        Me.Controls.Add(Me.LabelOodle)
        Me.Controls.Add(Me.LabelZlib)
        Me.Controls.Add(Me.ButtonDownloadRadVideo)
        Me.Controls.Add(Me.ButtonDownloadUnrrbpe)
        Me.Controls.Add(Me.ButtonSelectUnrrbpe)
        Me.Controls.Add(Me.TextBoxUnrrbpe)
        Me.Controls.Add(Me.LabelUnrrbpe)
        Me.Controls.Add(Me.ButtonSelectRadVideo)
        Me.Controls.Add(Me.TextBoxRadVideo)
        Me.Controls.Add(Me.LabelRadVideo)
        Me.Controls.Add(Me.ButtonSelectTexConv)
        Me.Controls.Add(Me.TextBoxTexConv)
        Me.Controls.Add(Me.LabelTexConv)
        Me.Controls.Add(Me.ButtonSelectHome)
        Me.Controls.Add(Me.TextBoxHome)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "OptionsMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options Menu"
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
End Class
