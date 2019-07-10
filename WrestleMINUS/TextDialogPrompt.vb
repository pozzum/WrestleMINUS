Imports WrestleMINUS.MainForm
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
    End Enum
    Private Sub TextDialogPrompt_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LabelOldFileHeader.Text = "Renaming File: " & OldFileName
        TextBoxEditedName.Text = EditedFileName
        ApplyRestrictions(ContainerBeingEdited)
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
            Case Else
                CurrentRestriction = RestrictionTypes.None
                TextBoxEditedName.MaxLength = 255
        End Select
        '    ParrentNodeTag.FileType = PackageType.PachDirectory OrElse
        '    ParrentNodeTag.FileType = PackageType.TextureLibrary
    End Sub
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