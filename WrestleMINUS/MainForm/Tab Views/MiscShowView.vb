Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Sub FillShowView(SelectedData As TreeNode)
        '&h74 2K15 (&h4e), 2K16 (&h78) 2K17 (&h79),
        '&h78 2K18 (&h79)
        '&h7C 2K19 (&h99)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridShowView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim FileLength As Integer = ShowBytes.Length
        Dim GameTypeTest As UInt32 = BitConverter.ToInt32(ShowBytes, 8)
        Dim ShowSpacing As Integer = &H74
        Dim ShowCount As Integer = BitConverter.ToInt32(ShowBytes, 4)
        If GameTypeTest = &H19A Then '2K19
            ShowViewType.SelectedIndex = 4
            ShowSpacing = &H7C
        ElseIf GameTypeTest = &H12C Then '2K18-2K15
            If ShowCount = &H6F Then '2K18
                ShowViewType.SelectedIndex = 3
                ShowSpacing = &H78
            ElseIf ShowCount = &H69 Then '2K17
                ShowViewType.SelectedIndex = 2
                ShowSpacing = &H74
            ElseIf ShowCount = &H78 Then '2K16
                ShowViewType.SelectedIndex = 1
                ShowSpacing = &H74
            ElseIf ShowCount = &H4E Then '2K15
                ShowViewType.SelectedIndex = 0
                ShowSpacing = &H74
            Else
                MessageBox.Show("Unknown Game")
                Exit Sub
            End If
        Else
            MessageBox.Show("Unknown Game")
            Exit Sub
        End If
        Dim index As Integer = 0
        ProgressBar1.Maximum = FileLength
        ProgressBar1.Value = 0
        Dim current_poition As Long = &HC
        While current_poition < FileLength
            'TO DO Build out controls for games other than 2K19
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            Dim StringRef As UInt32 = BitConverter.ToUInt32(ShowBytes, current_poition)
            TempGridRow.Cells(0).Value = Hex(StringRef) ' Dim NameRef As String =
            If StringRead Then
                TempGridRow.Cells(1).Value = StringReferences(StringRef) 'Name as string =
            Else
                TempGridRow.Cells(1).Value = "" 'Name as string =
            End If
            TempGridRow.Cells(2).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 4) 'Dim SelectNum As int16 =
            TempGridRow.Cells(3).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 6) 'Dim SelectNumSecond As int16 =
            TempGridRow.Cells(4).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 8) 'Dim A1 As int16 =
            TempGridRow.Cells(5).Value = BitConverter.ToUInt16(ShowBytes, current_poition + 10) 'Dim A2 As int16 =
            TempGridRow.Cells(6).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 12)) 'Dim B1 As String =
            TempGridRow.Cells(7).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 14)) 'Dim B2 As String =
            TempGridRow.Cells(8).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 16)) 'Dim B3 As String =
            '7 Bytes 00
            TempGridRow.Cells(9).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 25) 'Dim C1 As Boolean =
            TempGridRow.Cells(10).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 26) ' Dim C2 As Boolean =
            TempGridRow.Cells(11).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 27) 'Dim C3 As Boolean =
            TempGridRow.Cells(12).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 28) ' Dim C4 As Boolean =
            TempGridRow.Cells(13).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 29) 'Dim C5 As Boolean =
            TempGridRow.Cells(14).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 30) 'Dim C6 As Boolean =
            TempGridRow.Cells(15).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 31) 'Dim C7 As Boolean =
            '2 Bytes 00
            TempGridRow.Cells(16).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 34) 'Dim C8 As Boolean =
            TempGridRow.Cells(17).Value = ShowBytes(current_poition + 35) 'Dim Stage As byte =
            '2 bytes 00
            TempGridRow.Cells(18).Value = ShowBytes(current_poition + 38) 'Dim D1 As byte =
            TempGridRow.Cells(19).Value = ShowBytes(current_poition + 39) 'Dim D2 As byte =
            TempGridRow.Cells(20).Value = ShowBytes(current_poition + 40) 'Dim D3 As byte =
            TempGridRow.Cells(21).Value = ShowBytes(current_poition + 41) 'Dim D4 As byte =
            '1 byte 00
            TempGridRow.Cells(22).Value = ShowBytes(current_poition + 43) 'Dim D5 As byte =
            TempGridRow.Cells(23).Value = ShowBytes(current_poition + 44) 'Dim Ref As byte =
            'Dim Filter As String
            TempGridRow.Cells(24).Value = Hex(ShowBytes(current_poition + 45)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 46)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 47)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 48)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 49)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 50)).PadLeft(2, "0")
            '3 Bytes FF
            '2 Bytes 00
            TempGridRow.Cells(25).Value = Hex(BitConverter.ToUInt32(ShowBytes, current_poition + 56)) 'Dim F1 As String
            TempGridRow.Cells(26).Value = Hex(ShowBytes(current_poition + 60)) 'Dim F2 As String &H5A or &H00
            '3 bytes 00
            TempGridRow.Cells(27).Value = ShowBytes(current_poition + 64) 'Dim F3 As byte
            '1 Byte 00
            TempGridRow.Cells(28).Value = ShowBytes(current_poition + 66) 'Dim F4 As byte Possibly Tron
            '1 Byte 00
            TempGridRow.Cells(29).Value = ShowBytes(current_poition + 68) 'Dim H1 As byte
            TempGridRow.Cells(30).Value = ShowBytes(current_poition + 69) 'Dim H2 As byte
            TempGridRow.Cells(31).Value = ShowBytes(current_poition + 70) 'Dim H3 As byte
            TempGridRow.Cells(32).Value = ShowBytes(current_poition + 71) 'Dim H4 As byte
            '1 byte 00
            TempGridRow.Cells(33).Value = ShowBytes(current_poition + 73) 'Dim Bar As byte
            Dim temparray As Byte() = New Byte(33) {}
            Buffer.BlockCopy(ShowBytes, current_poition + 74, temparray, 0, 34)
            TempGridRow.Cells(34).Value = (BitConverter.ToString(temparray).Replace("-", ""))
            '3 byte 70
            TempGridRow.Cells(35).Value = ShowBytes(current_poition + 111) 'Dim I1 As byte
            TempGridRow.Cells(36).Value = Hex(ShowBytes(current_poition + 112)) 'Dim I2 As String
            '1 byte 00
            TempGridRow.Cells(37).Value = Hex(ShowBytes(current_poition + 114))  'Dim I3 As byte
            '2 byte 00
            TempGridRow.Cells(38).Value = ShowBytes(current_poition + 117) 'Dim live As byte
            '2 byte 00
            TempGridRow.Cells(39).Value = ShowBytes(current_poition + 120) 'Dim J As byte
            '40 Add
            '41 Delete
            TempGridRow.HeaderCell.Value = index.ToString
            WorkingCollection.Add(TempGridRow)
            index += 1
            current_poition += ShowSpacing
            ProgressBar1.Value = current_poition
        End While
        DataGridShowView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridShowView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridShowView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridShowView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 2, 3, 4, 5,
                 17, 18, 19, 20, 21, 22, 23,
                 27, 28, 29, 30, 31, 32, 33,
                 35, 38, 39
                If Not IsNumeric(MyCell.Value) OrElse
                       MyCell.Value < 0 Then
                    MyCell.Value = OldValue
                Else
                    SavePending = True
                    SaveChangesShowMenuItem.Visible = True
                End If

            Case Else 'Hex Text Required
                '0, 6, 7, 8, 34, 36, 37
                If Not HexaDecimalHandlers.HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    If e.ColumnIndex = 24 Then 'Filter
                        MyCell.Value = MyCell.Value.ToString.PadRight(12, "0")
                    ElseIf e.ColumnIndex = 25 Then 'Filter
                        MyCell.Value = MyCell.Value.ToString.PadRight(8, "0")
                    ElseIf e.ColumnIndex = 34 Then
                        MyCell.Value = MyCell.Value.ToString.PadRight(68, "0")
                    End If
                    SavePending = True
                    SaveChangesShowMenuItem.Visible = True
                End If
        End Select
    End Sub

    Private Sub DataGridShowView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridShowView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If e.RowIndex >= 0 AndAlso
           e.ColumnIndex >= 0 AndAlso
            TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 40 Then 'add button
                Dim Duplicaterow As DataGridViewRow = DataGridShowView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridShowView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridShowView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridShowView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                SaveChangesShowMenuItem.Visible = True
                For i As Integer = e.RowIndex To DataGridShowView.Rows.Count - 1
                    DataGridShowView.Rows(i).HeaderCell.Value = i.ToString
                Next
            ElseIf e.ColumnIndex = 41 Then 'Delete button
                DataGridShowView.Rows.RemoveAt(e.RowIndex)
                SaveChangesShowMenuItem.Visible = True
                For i As Integer = e.RowIndex To DataGridShowView.Rows.Count - 1
                    DataGridShowView.Rows(i).HeaderCell.Value = i.ToString
                Next
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub

    Private Sub SaveShowChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesShowMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildShowFile())
    End Sub

    Private Function BuildShowFile() As Byte()
        Dim ShowLength As UInt16 = &H74
        Select Case ShowViewType.SelectedIndex
            Case 4
                ShowLength = &H7C
            Case 3
                ShowLength = &H78
        End Select
        Dim ReturnedBytes As Byte() = New Byte(&HC + ShowLength * DataGridShowView.RowCount - 1) {}
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        'copy the header byptes so they stay the same
        Array.Copy(ShowBytes, ReturnedBytes, &HC)
        ProgressBar1.Maximum = DataGridShowView.RowCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To DataGridShowView.RowCount - 1
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridShowView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 0, 4) 'String Ref
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 4, 2) 'Select 1
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(3).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 6, 2) 'Select 2
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(4).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 8, 2) 'A1
            Array.Copy(BitConverter.GetBytes(CUShort(DataGridShowView.Rows(i).Cells(5).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 10, 2) 'A2
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(6).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 12, 2) 'B1
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(7).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 14, 2) 'B2
            Array.Copy(BitConverter.GetBytes(CUShort("&h" & DataGridShowView.Rows(i).Cells(8).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 16, 2) 'B3
            If DataGridShowView.Rows(i).Cells(9).Value Then ReturnedBytes(&HC + i * ShowLength + 25) = 1 'C1
            If DataGridShowView.Rows(i).Cells(10).Value Then ReturnedBytes(&HC + i * ShowLength + 26) = 1 'C2
            If DataGridShowView.Rows(i).Cells(11).Value Then ReturnedBytes(&HC + i * ShowLength + 27) = 1 'C3
            If DataGridShowView.Rows(i).Cells(12).Value Then ReturnedBytes(&HC + i * ShowLength + 28) = 1 'C4
            If DataGridShowView.Rows(i).Cells(13).Value Then ReturnedBytes(&HC + i * ShowLength + 29) = 1 'C5
            If DataGridShowView.Rows(i).Cells(14).Value Then ReturnedBytes(&HC + i * ShowLength + 30) = 1 'C6
            If DataGridShowView.Rows(i).Cells(15).Value Then ReturnedBytes(&HC + i * ShowLength + 31) = 1 'C7
            If DataGridShowView.Rows(i).Cells(16).Value Then ReturnedBytes(&HC + i * ShowLength + 34) = 1 'C8
            ReturnedBytes(&HC + i * ShowLength + 35) = CByte(DataGridShowView.Rows(i).Cells(17).Value) 'Stage
            ReturnedBytes(&HC + i * ShowLength + 38) = CByte(DataGridShowView.Rows(i).Cells(18).Value) 'D1
            ReturnedBytes(&HC + i * ShowLength + 39) = CByte(DataGridShowView.Rows(i).Cells(19).Value) 'D2
            ReturnedBytes(&HC + i * ShowLength + 40) = CByte(DataGridShowView.Rows(i).Cells(20).Value) 'D3
            ReturnedBytes(&HC + i * ShowLength + 41) = CByte(DataGridShowView.Rows(i).Cells(21).Value) 'D4
            ReturnedBytes(&HC + i * ShowLength + 43) = CByte(DataGridShowView.Rows(i).Cells(22).Value) 'D5
            ReturnedBytes(&HC + i * ShowLength + 44) = CByte(DataGridShowView.Rows(i).Cells(23).Value) 'REF '24
            Array.Copy(HexaDecimalHandlers.HexStringToByte(DataGridShowView.Rows(i).Cells(24).Value), 0, ReturnedBytes, &HC + i * ShowLength + 45, 6) 'Filter
            ReturnedBytes(&HC + i * ShowLength + 51) = &HFF '3 Bytes FF
            ReturnedBytes(&HC + i * ShowLength + 52) = &HFF
            ReturnedBytes(&HC + i * ShowLength + 53) = &HFF
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridShowView.Rows(i).Cells(25).Value)), 0, ReturnedBytes, &HC + i * ShowLength + 56, 4) 'F1
            ReturnedBytes(&HC + i * ShowLength + 60) = CByte("&h" & DataGridShowView.Rows(i).Cells(26).Value) 'F2
            ReturnedBytes(&HC + i * ShowLength + 64) = CByte(DataGridShowView.Rows(i).Cells(27).Value) 'F3
            ReturnedBytes(&HC + i * ShowLength + 66) = CByte(DataGridShowView.Rows(i).Cells(28).Value) 'F4
            ReturnedBytes(&HC + i * ShowLength + 68) = CByte(DataGridShowView.Rows(i).Cells(29).Value) 'H1
            ReturnedBytes(&HC + i * ShowLength + 69) = CByte(DataGridShowView.Rows(i).Cells(30).Value) 'H2
            ReturnedBytes(&HC + i * ShowLength + 70) = CByte(DataGridShowView.Rows(i).Cells(31).Value) 'H3
            ReturnedBytes(&HC + i * ShowLength + 71) = CByte(DataGridShowView.Rows(i).Cells(32).Value) 'H4
            ReturnedBytes(&HC + i * ShowLength + 73) = CByte(DataGridShowView.Rows(i).Cells(33).Value) 'Bar
            Array.Copy(HexaDecimalHandlers.HexStringToByte(DataGridShowView.Rows(i).Cells(34).Value), 0, ReturnedBytes, &HC + i * ShowLength + 74, 34) 'Unkown
            ReturnedBytes(&HC + i * ShowLength + 108) = &H70 '3 byte 70
            ReturnedBytes(&HC + i * ShowLength + 109) = &H70
            ReturnedBytes(&HC + i * ShowLength + 110) = &H70
            ReturnedBytes(&HC + i * ShowLength + 111) = CByte(DataGridShowView.Rows(i).Cells(35).Value) 'I1
            ReturnedBytes(&HC + i * ShowLength + 112) = CByte("&h" & DataGridShowView.Rows(i).Cells(36).Value) 'I2
            ReturnedBytes(&HC + i * ShowLength + 114) = CByte("&h" & DataGridShowView.Rows(i).Cells(37).Value) 'I3
            ReturnedBytes(&HC + i * ShowLength + 117) = CByte(DataGridShowView.Rows(i).Cells(38).Value) 'live
            ReturnedBytes(&HC + i * ShowLength + 118) = &HFF
            ReturnedBytes(&HC + i * ShowLength + 120) = CByte(DataGridShowView.Rows(i).Cells(39).Value) 'J
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function

End Class
