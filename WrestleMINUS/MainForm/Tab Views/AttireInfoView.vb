Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Sub FillAttireView(SelectedData As TreeNode)
        RemoveHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAttireView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim AttireBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim WrestlerCount As Integer = BitConverter.ToInt32(AttireBytes, &H8)
        Dim WrestlerPacs(WrestlerCount - 1) As Integer
        Dim AttireCount(WrestlerCount - 1) As Integer
        Dim AttireNames((WrestlerCount) * 10 - 1) As Integer
        Dim AttireEnabled((WrestlerCount) * 10 - 1) As Boolean
        Dim AttireManager((WrestlerCount) * 10 - 1) As Boolean
        Dim AttireUnlockNumber((WrestlerCount) * 10 - 1) As UInt32
        For i As Integer = 0 To WrestlerCount - 1
            WrestlerPacs(i) = BitConverter.ToUInt32(AttireBytes, &HC + &HA8 * i)
            AttireCount(i) = BitConverter.ToUInt32(AttireBytes, &H10 + &HA8 * i)
            For K As Integer = 0 To 9
                AttireNames(i * 10 + K) = BitConverter.ToUInt32(AttireBytes, &H14 + &HA8 * i + &H10 * K)
                AttireEnabled(i * 10 + K) = (AttireBytes(&H20 + &HA8 * i + &H10 * K) = &HFF)
                AttireManager(i * 10 + K) = (AttireBytes(&H20 + &HA8 * i + &H10 * K) = &H2)
                AttireUnlockNumber(i * 10 + K) = BitConverter.ToUInt32(AttireBytes, &H18 + &HA8 * i + &H10 * K)
            Next
        Next
        If StringRead Then 'True
            'Hide Strings
            DataGridAttireView.Columns(3).Visible = True
            DataGridAttireView.Columns(8).Visible = True
            DataGridAttireView.Columns(13).Visible = True
            DataGridAttireView.Columns(18).Visible = True
            DataGridAttireView.Columns(23).Visible = True
            DataGridAttireView.Columns(28).Visible = True
            DataGridAttireView.Columns(33).Visible = True
            DataGridAttireView.Columns(38).Visible = True
            DataGridAttireView.Columns(43).Visible = True
            DataGridAttireView.Columns(48).Visible = True
            If PacsRead Then 'Strings and Pacs Read
            Else 'Strings Read Only

            End If
        Else 'Pacs Read Only can't do much
            'Hide Strings
            DataGridAttireView.Columns(3).Visible = False
            DataGridAttireView.Columns(8).Visible = False
            DataGridAttireView.Columns(13).Visible = False
            DataGridAttireView.Columns(18).Visible = False
            DataGridAttireView.Columns(23).Visible = False
            DataGridAttireView.Columns(28).Visible = False
            DataGridAttireView.Columns(33).Visible = False
            DataGridAttireView.Columns(38).Visible = False
            DataGridAttireView.Columns(43).Visible = False
            DataGridAttireView.Columns(48).Visible = False
        End If
        ProgressBar1.Maximum = WrestlerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To WrestlerCount - 1
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = WrestlerPacs(i)
            TempGridRow.Cells(1).Value = AttireCount(i)
            TempGridRow.Cells(2).Value = Hex(AttireNames(i * 10 + 0))
            If StringRead Then
                TempGridRow.Cells(3).Value = StringReferences(AttireNames(i * 10 + 0))
            Else
                TempGridRow.Cells(3).Value = ""
            End If
            TempGridRow.Cells(4).Value = AttireEnabled(i * 10 + 0)
            TempGridRow.Cells(5).Value = AttireManager(i * 10 + 0)
            TempGridRow.Cells(6).Value = AttireUnlockNumber(i * 10 + 0)
            TempGridRow.Cells(7).Value = Hex(AttireNames(i * 10 + 1))
            If StringRead Then
                TempGridRow.Cells(8).Value = StringReferences(AttireNames(i * 10 + 1))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(9).Value = AttireEnabled(i * 10 + 1)
            TempGridRow.Cells(10).Value = AttireManager(i * 10 + 1)
            TempGridRow.Cells(11).Value = AttireUnlockNumber(i * 10 + 1)
            TempGridRow.Cells(12).Value = Hex(AttireNames(i * 10 + 2))
            If StringRead Then
                TempGridRow.Cells(13).Value = StringReferences(AttireNames(i * 10 + 2))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(14).Value = AttireEnabled(i * 10 + 2)
            TempGridRow.Cells(15).Value = AttireManager(i * 10 + 2)
            TempGridRow.Cells(16).Value = AttireUnlockNumber(i * 10 + 2)
            TempGridRow.Cells(17).Value = Hex(AttireNames(i * 10 + 3))
            If StringRead Then
                TempGridRow.Cells(18).Value = StringReferences(AttireNames(i * 10 + 3))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(19).Value = AttireEnabled(i * 10 + 3)
            TempGridRow.Cells(20).Value = AttireManager(i * 10 + 3)
            TempGridRow.Cells(21).Value = AttireUnlockNumber(i * 10 + 3)
            TempGridRow.Cells(22).Value = Hex(AttireNames(i * 10 + 4))
            If StringRead Then
                TempGridRow.Cells(23).Value = StringReferences(AttireNames(i * 10 + 4))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(24).Value = AttireEnabled(i * 10 + 4)
            TempGridRow.Cells(25).Value = AttireManager(i * 10 + 4)
            TempGridRow.Cells(26).Value = AttireUnlockNumber(i * 10 + 4)
            TempGridRow.Cells(27).Value = Hex(AttireNames(i * 10 + 5))
            If StringRead Then
                TempGridRow.Cells(28).Value = StringReferences(AttireNames(i * 10 + 5))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(29).Value = AttireEnabled(i * 10 + 5)
            TempGridRow.Cells(30).Value = AttireManager(i * 10 + 5)
            TempGridRow.Cells(31).Value = AttireUnlockNumber(i * 10 + 5)
            TempGridRow.Cells(32).Value = Hex(AttireNames(i * 10 + 6))
            If StringRead Then
                TempGridRow.Cells(33).Value = StringReferences(AttireNames(i * 10 + 6))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(34).Value = AttireEnabled(i * 10 + 6)
            TempGridRow.Cells(35).Value = AttireManager(i * 10 + 6)
            TempGridRow.Cells(36).Value = AttireUnlockNumber(i * 10 + 6)
            TempGridRow.Cells(37).Value = Hex(AttireNames(i * 10 + 7))
            If StringRead Then
                TempGridRow.Cells(38).Value = StringReferences(AttireNames(i * 10 + 7))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(39).Value = AttireEnabled(i * 10 + 7)
            TempGridRow.Cells(40).Value = AttireManager(i * 10 + 7)
            TempGridRow.Cells(41).Value = AttireUnlockNumber(i * 10 + 7)
            TempGridRow.Cells(42).Value = Hex(AttireNames(i * 10 + 8))
            If StringRead Then
                TempGridRow.Cells(43).Value = StringReferences(AttireNames(i * 10 + 8))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(44).Value = AttireEnabled(i * 10 + 8)
            TempGridRow.Cells(45).Value = AttireManager(i * 10 + 8)
            TempGridRow.Cells(46).Value = AttireUnlockNumber(i * 10 + 8)
            TempGridRow.Cells(47).Value = Hex(AttireNames(i * 10 + 9))
            If StringRead Then
                TempGridRow.Cells(48).Value = StringReferences(AttireNames(i * 10 + 9))
            Else
                TempGridRow.Cells(8).Value = ""
            End If
            TempGridRow.Cells(49).Value = AttireEnabled(i * 10 + 9)
            TempGridRow.Cells(50).Value = AttireManager(i * 10 + 9)
            TempGridRow.Cells(51).Value = AttireUnlockNumber(i * 10 + 9)
            If i > 99 Then
                TempGridRow.HeaderCell.Value = "UNREAD"
            Else
                TempGridRow.HeaderCell.Value = ""
            End If
            ProgressBar1.Value = i
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridAttireView.Rows.AddRange(WorkingCollection.ToArray())
        AddHandler DataGridAttireView.RowsAdded, AddressOf DataGridAttireView_RowsAdded
    End Sub

    Private Sub DataGridAttireView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAttireView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 0 Then 'Pac Number, Verify Number <= 1024
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
               CInt(MyCell.Value) > 1024 Then
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Attire Count, Verify Number <= 10
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
               CInt(MyCell.Value) > 10 Then
                MyCell.Value = OldValue
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 0 Then 'Attire Name
            If Not HexaDecimalHandlers.HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CUInt("&H" & MyCell.Value) > UInt32.MaxValue Then
                MyCell.Value = OldValue
            Else
                If StringReferences.ContainsKey(CUInt("&H" & MyCell.Value)) Then
                    DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & MyCell.Value))
                Else
                    DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = ""
                End If
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 1 Then 'Attire String Does Nothing
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 2 Then 'Enabled Changed
            If MyCell.Value Then 'if enabled checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = False 'unchecks manager
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 3 Then 'Manager Changed
            If MyCell.Value Then 'if manager checked
                DataGridAttireView.Rows(e.RowIndex).Cells(e.ColumnIndex - 1).Value = False 'unchecks enabled
            End If
        ElseIf ((e.ColumnIndex - 2) Mod 5) = 4 Then 'UnlockMode Program Only
        End If
        SavePending = True
        SaveChangesAttireMenuItem.Visible = True
    End Sub

    Private Sub DataGridAttireView_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)
        If DataGridAttireView.RowCount > 1 Then
            Dim NewRow As DataGridViewRow = DataGridAttireView.Rows(e.RowIndex - 1)
            NewRow.Cells(0).Value = 0
            NewRow.Cells(0).ValueType = GetType(System.Int32)
            NewRow.Cells(1).Value = 10
            For i As Integer = 0 To 9
                NewRow.Cells(2 + i * 5).Value = Hex(&H50A2 + i)
                If StringRead Then
                    NewRow.Cells(2 + i * 5 + 1).Value = StringReferences(&H50A2 + i)
                Else
                    NewRow.Cells(2 + i * 5 + 1).Value = ""
                End If
                NewRow.Cells(2 + i * 5 + 2).Value = True
                NewRow.Cells(2 + i * 5 + 3).Value = False
                NewRow.Cells(2 + i * 5 + 4).Value = UInt32.MaxValue
            Next
            If e.RowIndex > 99 Then
                NewRow.HeaderCell.Value = "UNREAD"
            Else
                NewRow.HeaderCell.Value = ""
            End If
        End If
    End Sub

    Private Sub DataGridAttireView_Sorted(sender As Object, e As EventArgs) Handles DataGridAttireView.Sorted
        If DataGridAttireView.RowCount > 100 Then
            For i As Integer = 0 To 99
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            Next
            For i As Integer = 100 To DataGridAttireView.RowCount - 1
                DataGridAttireView.Rows(i).HeaderCell.Value = "UNREAD"
            Next
        Else
            For i As Integer = 0 To DataGridAttireView.RowCount - 1
                DataGridAttireView.Rows(i).HeaderCell.Value = ""
            Next
        End If
    End Sub

    Private Sub SaveAttireChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesAttireMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildAttireFile())
    End Sub

    Private Function BuildAttireFile() As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&HC + ((DataGridAttireView.RowCount - 1) * &HA8) - 1) {}
        'COS
        ReturnedBytes(0) = &H43
        ReturnedBytes(1) = &H4F
        ReturnedBytes(2) = &H53
        ReturnedBytes(4) = &H1
        ReturnedBytes(8) = DataGridAttireView.RowCount - 1 ' subtract 1 because of the added
        For i As Integer = 0 To DataGridAttireView.RowCount - 2 '2 to skip the added row
            'PacNumber
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, &HC + i * &HA8, 4)
            'Attire Count
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(1).Value)), 0, ReturnedBytes, &HC + i * &HA8 + 4, 4)
            For K As Integer = 0 To 9
                Array.Copy(BitConverter.GetBytes(CUInt("&H" & (DataGridAttireView.Rows(i).Cells(2 + K * 5).Value.ToString))), 0, ReturnedBytes, &HC + i * &HA8 + K * &H10 + 8, 4)
                'Unlock Info
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridAttireView.Rows(i).Cells(6 + K * 5).Value.ToString)), 0, ReturnedBytes, &HC + i * &HA8 + K * &H10 + 12, 4)
                If DataGridAttireView.Rows(i).Cells(4 + K * 5).Value Then
                    ReturnedBytes(&HC + i * &HA8 + K * &H10 + 20) = &HFF
                ElseIf DataGridAttireView.Rows(i).Cells(5 + K * 5).Value Then
                    ReturnedBytes(&HC + i * &HA8 + K * &H10 + 20) = 2
                End If
            Next
        Next
        Return ReturnedBytes
    End Function


End Class
