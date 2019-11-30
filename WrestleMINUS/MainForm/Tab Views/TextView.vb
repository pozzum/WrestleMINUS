Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Sub AddText(SelectedNode As TreeNode)
        If File.Exists(SelectedNode.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If TextViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(TextViewBitWidth.Text)
            Else
                bitwidth = CInt(TextViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As ExtendedFileProperties = CType(SelectedNode.Tag, ExtendedFileProperties)
            Dim Filebytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedNode.Tag, CInt(&H1000 * My.Settings.HexViewLength))
            If Filebytes.Length > 0 Then
                Dim TextString As String = ""
                If Filebytes.Length < (&H1000 * My.Settings.HexViewLength) Then
                    TextString = New String(".", Filebytes.Length)
                Else
                    TextString = New String(".", (&H1000 * My.Settings.HexViewLength))
                End If
                Dim FirstBuilder As New StringBuilder(TextString)
                For i As Integer = 0 To TextString.Length - 1
                    If Filebytes(i) > 31 AndAlso (Filebytes(i) < 257) Then
                        FirstBuilder(i) = Encoding.Default.GetChars(Filebytes, i, 1)(0)
                    End If
                Next
                Dim builder As New StringBuilder(FirstBuilder.ToString().Replace(vbCr, ".").Replace(vbLf, "."))
                Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 1)
                For i As Int32 = startIndex To (bitwidth * 1) Step -(bitwidth * 1)
                    builder.Insert(i, vbCr & vbLf)
                Next i
                Text_Selected.Text = builder.ToString()
            Else
                Text_Selected.Text = ""
            End If
        End If
    End Sub

    Private Sub TextViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles TextViewBitWidth.TextChanged
        If TextViewBitWidth.Text = "" Then
            'Do Nothing
            Exit Sub
        ElseIf Not IsNumeric(TextViewBitWidth.Text) Then
            TextViewBitWidth.Text = OldValue
            Exit Sub
        ElseIf CInt(TextViewBitWidth.Text) > 0 Then
            If TextViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = TextViewBitWidth.SelectedIndex
                HexViewBitWidth.SelectedIndex = TextViewBitWidth.SelectedIndex
            Else
                HexViewBitWidth.SelectedIndex = -1
                HexViewBitWidth.Text = TextViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

End Class
