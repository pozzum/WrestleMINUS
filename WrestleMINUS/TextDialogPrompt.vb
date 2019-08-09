Imports System.IO
Imports System.Text.RegularExpressions 'Text Replace

Public Class TextDialogPrompt
    Public Result As DialogResult = DialogResult.Cancel
    Public OldFileName As String = ""
    Public EditedFileName As String = ""
    Public ContainerBeingEdited As PackageType = PackageType.bin
    Public CurrentRestriction As RestrictionTypes = RestrictionTypes.None

    Public Enum RestrictionTypes
        None
        Hex
        IntegerType
        AlphaNumWSpaces
        AlphaNumNoSpaces
        FileName
        Folder
    End Enum

    Private Sub TextDialogPrompt_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If OldFileName = "hash" Then
            LabelOldFileHeader.Text = "Generating Name Hash:"
        ElseIf ContainerBeingEdited = PackageType.Folder Then
            If Path.GetFileName(OldFileName) = "" Then
                LabelOldFileHeader.Text = "Renaming Folder: " & Path.GetFileName(Path.GetDirectoryName(OldFileName))
            Else
                LabelOldFileHeader.Text = "Renaming Folder: " & Path.GetFileName(OldFileName)
            End If
        ElseIf ContainerBeingEdited = PackageType.EditingFileName Then
            LabelOldFileHeader.Text = "Renaming File: " & OldFileName
        Else
            LabelOldFileHeader.Text = "Renaming File Part: " & OldFileName
        End If
        TextBoxEditedName.Text = EditedFileName
        ApplyRestrictions(ContainerBeingEdited)
        ResizeMenu()
    End Sub

    Sub ApplyRestrictions(ContainerType As PackageType)
        Select Case ContainerType
            Case PackageType.bin
                CurrentRestriction = RestrictionTypes.None
                TextBoxEditedName.MaxLength = 255
            Case PackageType.HSPC
                CurrentRestriction = RestrictionTypes.Hex
                TextBoxEditedName.MaxLength = 16
            Case PackageType.SHDC
                CurrentRestriction = RestrictionTypes.Hex
                TextBoxEditedName.MaxLength = 8
            Case PackageType.EPK8
                'Should contain a directory first??
                CurrentRestriction = RestrictionTypes.AlphaNumWSpaces
                TextBoxEditedName.MaxLength = 4
            Case PackageType.EPAC
                'Should contain a directory first??
                CurrentRestriction = RestrictionTypes.AlphaNumWSpaces
                TextBoxEditedName.MaxLength = 4
            Case PackageType.PachDirectory_4
                CurrentRestriction = RestrictionTypes.AlphaNumWSpaces
                TextBoxEditedName.MaxLength = 4
            Case PackageType.PachDirectory_8
                CurrentRestriction = RestrictionTypes.AlphaNumWSpaces
                TextBoxEditedName.MaxLength = 8
            Case PackageType.TextureLibrary
                CurrentRestriction = RestrictionTypes.AlphaNumNoSpaces
                TextBoxEditedName.MaxLength = 16
            Case PackageType.EditingFileName
                CurrentRestriction = RestrictionTypes.FileName
                TextBoxEditedName.MaxLength = 255
            Case PackageType.Folder
                CurrentRestriction = RestrictionTypes.Folder
                TextBoxEditedName.MaxLength = 255
            Case Else
                CurrentRestriction = RestrictionTypes.None
                TextBoxEditedName.MaxLength = 255
        End Select
        '    ParrentNodeTag.FileType = PackageType.PachDirectory OrElse
        '    ParrentNodeTag.FileType = PackageType.TextureLibrary
    End Sub

    Sub ResizeMenu()
        Dim MinSize As Integer = CalculateLabelWidth(LabelOldFileHeader)
        If MinSize < Me.Width Then
            MinSize = Me.Width
        End If
        'labels can just get the width set
        LabelOldFileHeader.Width = MinSize
        TextBoxEditedName.Width = MinSize
        'buttons have to be adjusted
        '6 pixels in the middle and 12 on the left 156
        Dim ButtonWidth As Integer = (MinSize - 6) / 2
        ButtonAcceptChange.Width = ButtonWidth
        ButtonCancelChange.Width = ButtonWidth
        ButtonCancelChange.Location = New Point(12 + MinSize - ButtonWidth, ButtonCancelChange.Location.Y)
        '20 pixels on either side..
        Me.Width = MinSize + 40
    End Sub

    Function CalculateLabelWidth(TestedLabel As Label)
        Dim TextSize As Size = TextRenderer.MeasureText(TestedLabel.Text, TestedLabel.Font)
        Dim TextWidth As Integer = TextSize.Width
        Return TextWidth + TestedLabel.Padding.Horizontal
    End Function

    Private Sub TextBoxEditedName_TextChanged(sender As Object, e As EventArgs) Handles TextBoxEditedName.TextChanged
        Dim SentTextBox As TextBox = CType(sender, TextBox)
        Dim CursorPosition As Integer = SentTextBox.SelectionStart
        Select Case CurrentRestriction
            Case RestrictionTypes.Hex
                SentTextBox.Text = Regex.Replace(SentTextBox.Text, "[^0-9a-fA-F]", "").ToUpper
            Case RestrictionTypes.IntegerType
                SentTextBox.Text = Regex.Replace(SentTextBox.Text, "[^\d]", "")
            Case RestrictionTypes.AlphaNumWSpaces
                SentTextBox.Text = Regex.Replace(SentTextBox.Text, "[^0-9a-zA-Z_ ]", "")
            Case RestrictionTypes.AlphaNumNoSpaces
                SentTextBox.Text = Regex.Replace(SentTextBox.Text, "[^0-9a-zA-Z_]", "")
            Case RestrictionTypes.FileName
                SentTextBox.Text = Regex.Replace(SentTextBox.Text, "[^\w\-. ]", "")
            Case RestrictionTypes.Folder
                SentTextBox.Text = Regex.Replace(SentTextBox.Text, "[^\w\-\:.\\ ]", "")
        End Select
        SentTextBox.SelectionStart = CursorPosition
        EditedFileName = SentTextBox.Text
    End Sub

    Function CheckValidName(TestedNewName As String) As Boolean
        Dim InitialLength As Integer = TestedNewName.Length
        Select Case CurrentRestriction
            Case RestrictionTypes.Hex
                TestedNewName = Regex.Replace(TestedNewName, "[^0-9a-fA-F]", "").ToUpper
            Case RestrictionTypes.IntegerType
                TestedNewName = Regex.Replace(TestedNewName, "[^\d]", "")
            Case RestrictionTypes.AlphaNumWSpaces
                TestedNewName = Regex.Replace(TestedNewName, "[^0-9a-zA-Z_ ]", "")
            Case RestrictionTypes.AlphaNumNoSpaces
                TestedNewName = Regex.Replace(TestedNewName, "[^0-9a-zA-Z_]", "")
            Case RestrictionTypes.FileName
                TestedNewName = Regex.Replace(TestedNewName, "[^\w\-. ]", "")
        End Select
        Return (InitialLength = TestedNewName.Length)
    End Function

    Private Sub ButtonAcceptChange_Click(sender As Object, e As EventArgs) Handles ButtonAcceptChange.Click
        If CheckValidName(TextBoxEditedName.Text) Then
            Result = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Error Validating New Name")
        End If
    End Sub

    Private Sub ButtonCancelChange_Click(sender As Object, e As EventArgs) Handles ButtonCancelChange.Click
        Result = DialogResult.Cancel
        Me.Close()
    End Sub

End Class