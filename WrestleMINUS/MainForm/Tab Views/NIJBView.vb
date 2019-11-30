Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    'TO DO Build out other NIBJ Types
    Sub FillNIBJView(SelectedData As TreeNode)
        DataGridNIBJView.Rows.Clear()
        DataGridNIBJView.Columns.Clear()
        Dim NodeTag As ExtendedFileProperties = CType(SelectedData.Tag, ExtendedFileProperties)
        Dim NIJBBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim HeaderByte As Integer = BitConverter.ToInt32(NIJBBytes, &H4) '64 for Parm Light
        Dim LightCount As Integer = BitConverter.ToInt32(NIJBBytes, &H8)
        Dim ShowCount As Integer = BitConverter.ToInt32(NIJBBytes, &HC)
        Select Case HeaderByte
            Case &H64 'parameter file
                Dim Folder As String = Encoding.Default.GetChars(NIJBBytes, &H10, &H10)
                Dim Properties As String = Encoding.Default.GetChars(NIJBBytes, &H20, &H10)
                FileAttributesToolStripMenuItem.Text = Folder & " > " & Properties
                For i As Integer = 0 To LightCount - 1
                    Dim TempNewColumn As DataGridTextBoxColumn = New DataGridTextBoxColumn()
                    DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H30 + i * &H20, &H10))
                Next
                Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridNIBJView)
                Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
                ProgressBar1.Maximum = ShowCount - 1
                ProgressBar1.Value = 0
                For i As Integer = 0 To ShowCount - 1
                    DataGridNIBJView.Rows.Add()
                    For j As Integer = 0 To LightCount - 1
                        DataGridNIBJView(j, i).Value = Strings.Right(Hex(BitConverter.ToInt32(NIJBBytes, &H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadRight(8, "0"), 8)
                        DataGridNIBJView.Rows(i).Cells(j).Style.BackColor = ColorTranslator.FromHtml("#" & (DataGridNIBJView(j, i).Value.ToString.Substring(2, 6)))
                        Dim FontColor As String = Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 2)).PadLeft(2, "0") &
                            Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 1)).PadLeft(2, "0") &
                            Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadLeft(2, "0")
                        DataGridNIBJView.Rows(i).Cells(j).Style.ForeColor = ColorTranslator.FromHtml("#" & FontColor)
                    Next
                    DataGridNIBJView.Rows(i).HeaderCell.Value = i.ToString
                    ProgressBar1.Value = i
                Next
            Case &H67
                FileAttributesToolStripMenuItem.Text = "Unreadable Type"
                If LightCount = 0 Then
                    DataGridNIBJView.Columns.Add("Color0", "Color0")
                Else
                    For i As Integer = 0 To LightCount - 1
                        DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H10 + i * &H40, &H10))
                    Next
                End If
            Case &H6A
                FileAttributesToolStripMenuItem.Text = "Unreadable Type"
                If LightCount = 0 Then
                    DataGridNIBJView.Columns.Add("Color0", "Color0")
                End If
        End Select
    End Sub

    Private Sub DataGridNIBJView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridNIBJView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridNIBJView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If Not HexaDecimalHandlers.HexCheck(MyCell.Value) OrElse
            MyCell.Value.ToString.Length > 8 Then
            MyCell.Value = OldValue
        Else
            If MyCell.Value.ToString.Length < 8 Then
                MyCell.Value = MyCell.Value.ToString.PadRight(8, "0")
            End If
            MyCell.Style.BackColor = ColorTranslator.FromHtml("#" & (MyCell.Value.ToString.Substring(2, 6)))
            Dim FontColor As Color = ColorTranslator.FromHtml("#" & Hex(&HFF - MyCell.Style.BackColor.R).PadLeft(2, "0") &
                                                              Hex(&HFF - MyCell.Style.BackColor.G).PadLeft(2, "0") &
                                                              Hex(&HFF - MyCell.Style.BackColor.B).PadLeft(2, "0"))
            MyCell.Style.ForeColor = FontColor
            SavePending = True
            SaveChangesNIBJMenuItem.Visible = True
        End If
    End Sub

    Private Sub SaveNIBJChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesNIBJMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildNIBJFile())
    End Sub

    Private Function BuildNIBJFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(ShowBytes.Length - 1) {}
        Dim LightCount As Integer = DataGridNIBJView.ColumnCount
        Dim ShowCount As Integer = DataGridNIBJView.RowCount
        Array.Copy(ShowBytes, ReturnedBytes, &H30 + LightCount * &H20)
        For i As Integer = 0 To LightCount - 1
            For j As Integer = 0 To ShowCount - 1
                Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridNIBJView.Rows(j).Cells(i).Value)), 0,
                ReturnedBytes, &H30 + (LightCount * &H20) + (i * ShowCount * 4) + (j * 4), 4)
            Next
        Next
        Return ReturnedBytes
    End Function

End Class
