<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TextDialogPrompt
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
        Me.LabelOldFileHeader = New System.Windows.Forms.Label()
        Me.TextBoxEditedName = New System.Windows.Forms.TextBox()
        Me.ButtonAcceptChange = New System.Windows.Forms.Button()
        Me.ButtonCancelChange = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LabelOldFileHeader
        '
        Me.LabelOldFileHeader.Location = New System.Drawing.Point(12, 9)
        Me.LabelOldFileHeader.Name = "LabelOldFileHeader"
        Me.LabelOldFileHeader.Size = New System.Drawing.Size(156, 23)
        Me.LabelOldFileHeader.TabIndex = 0
        Me.LabelOldFileHeader.Text = "Renaming File: ..."
        Me.LabelOldFileHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'TextBoxEditedName
        '
        Me.TextBoxEditedName.Location = New System.Drawing.Point(12, 25)
        Me.TextBoxEditedName.Name = "TextBoxEditedName"
        Me.TextBoxEditedName.Size = New System.Drawing.Size(156, 20)
        Me.TextBoxEditedName.TabIndex = 1
        '
        'ButtonAcceptChange
        '
        Me.ButtonAcceptChange.Location = New System.Drawing.Point(12, 51)
        Me.ButtonAcceptChange.Name = "ButtonAcceptChange"
        Me.ButtonAcceptChange.Size = New System.Drawing.Size(75, 23)
        Me.ButtonAcceptChange.TabIndex = 2
        Me.ButtonAcceptChange.Text = "Ok"
        Me.ButtonAcceptChange.UseVisualStyleBackColor = True
        '
        'ButtonCancelChange
        '
        Me.ButtonCancelChange.Location = New System.Drawing.Point(93, 51)
        Me.ButtonCancelChange.Name = "ButtonCancelChange"
        Me.ButtonCancelChange.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancelChange.TabIndex = 3
        Me.ButtonCancelChange.Text = "Cancel"
        Me.ButtonCancelChange.UseVisualStyleBackColor = True
        '
        'TextDialogPrompt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(180, 81)
        Me.Controls.Add(Me.ButtonCancelChange)
        Me.Controls.Add(Me.ButtonAcceptChange)
        Me.Controls.Add(Me.TextBoxEditedName)
        Me.Controls.Add(Me.LabelOldFileHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TextDialogPrompt"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Input Text"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelOldFileHeader As Label
    Friend WithEvents TextBoxEditedName As TextBox
    Friend WithEvents ButtonAcceptChange As Button
    Friend WithEvents ButtonCancelChange As Button
End Class
