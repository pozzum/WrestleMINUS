Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    'TO DO Request Direct Editing of Hex Edit View
    Sub AddHexText(SelectedNode As TreeNode)
        If File.Exists(SelectedNode.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If HexViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(HexViewBitWidth.Text)
            Else
                bitwidth = CInt(HexViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As ExtendedFileProperties = CType(SelectedNode.Tag, ExtendedFileProperties)
            Dim Filebytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedNode.Tag, CInt(&H1000 * My.Settings.HexViewLength))
            Dim ByteString As String = ""
            If Filebytes.Length < (&H1000 * My.Settings.HexViewLength) Then
                ByteString = (BitConverter.ToString(Filebytes, 0, Filebytes.Length).Replace("-", " "))
            Else
                ByteString = (BitConverter.ToString(Filebytes, 0, (&H1000 * My.Settings.HexViewLength)).Replace("-", " "))
            End If
            Dim builder As New StringBuilder(ByteString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 3)
            For i As Int32 = startIndex To (bitwidth * 3) Step -(bitwidth * 3)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Hex_Selected.Text = builder.ToString()
        End If
    End Sub

    Private Sub HexViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles HexViewBitWidth.TextChanged
        If HexViewBitWidth.Text = "" Then
            'Do Nothing
            Exit Sub
        ElseIf Not IsNumeric(HexViewBitWidth.Text) Then
            HexViewBitWidth.Text = OldValue
            Exit Sub
        ElseIf CInt(HexViewBitWidth.Text) > 0 Then
            If HexViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = HexViewBitWidth.SelectedIndex
                TextViewBitWidth.SelectedIndex = HexViewBitWidth.SelectedIndex
            Else
                TextViewBitWidth.SelectedIndex = -1
                TextViewBitWidth.Text = HexViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddHexText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

End Class
