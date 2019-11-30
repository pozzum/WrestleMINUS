Imports System.Text 'Binary Formatter
Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    'TO DO you can create format for the settings to be a class object once more information is known.

    Public Enum WeaponPositionType
        Unknown
        Arena
        Base
        Rules
        Settings
        Equipment
    End Enum

    Dim CurrentPoistionType As WeaponPositionType = WeaponPositionType.Unknown

    Sub FillWeaponPositionView(SelectedData As TreeNode)
        Dim WeaponPositionBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        'We need to show what type of position file it is
        Dim PartialWeaponHeaderString As String = New String(Encoding.Default.GetChars(WeaponPositionBytes, 2, 2))
        If PartialWeaponHeaderString = "AR" Then
            CurrentPoistionType = WeaponPositionType.Arena
        ElseIf PartialWeaponHeaderString = "BS" Then
            CurrentPoistionType = WeaponPositionType.Base
        ElseIf PartialWeaponHeaderString = "RU" Then
            CurrentPoistionType = WeaponPositionType.Rules
        ElseIf PartialWeaponHeaderString = "ST" Then
            CurrentPoistionType = WeaponPositionType.Settings
        ElseIf PartialWeaponHeaderString = "EA" Then
            CurrentPoistionType = WeaponPositionType.Equipment
        End If
        WeaponPositionTypeToolStripMenuItem.Text = "Position Type: " & CurrentPoistionType.ToString
        Dim WeaponCount As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, 8)
        Dim WeaponPotisionLEngth As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, 12)
        Dim WeaponLineLength As UInt32 = WeaponPotisionLEngth / WeaponCount
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridWeaponPositionView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = WeaponCount - 1
        ProgressBar1.Value = 0
        If CurrentPoistionType = WeaponPositionType.Settings Then
            Dim CurrentIndex As UInt32 = &H10
            For i As Integer = 0 To WeaponCount - 1
                Dim CurrentSettingNum As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex)
                Dim SubItemCount As UInt32 = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex + 4)
                CurrentIndex += 8
                For J As Integer = 0 To SubItemCount - 1
                    Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value = i
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Style = ReadOnlyCellStyle
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Value = (BitConverter.ToString(WeaponPositionBytes, CurrentIndex, &H28).Replace("-", " "))
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Style = ReadOnlyCellStyle
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Value = CurrentSettingNum
                    If Not J = 0 Then
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Style = ReadOnlyCellStyle
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True
                    End If
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart)).Value = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H4)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H8)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &HC)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H10)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H14)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)).Value = BitConverter.ToSingle(WeaponPositionBytes, CurrentIndex + &H18)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H1C)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H1E)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H20)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)).Value = BitConverter.ToUInt16(WeaponPositionBytes, CurrentIndex + &H22)
                    TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)).Value = BitConverter.ToUInt32(WeaponPositionBytes, CurrentIndex + &H24)
                    WorkingCollection.Add(TempGridRow)
                    CurrentIndex += &H28
                Next
                ProgressBar1.Value = i
            Next
            DataGridWeaponPositionView.Rows.AddRange(WorkingCollection.ToArray())
        Else
            For i As Integer = 0 To WeaponCount - 1
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value = i
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Style = ReadOnlyCellStyle
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Value = (BitConverter.ToString(WeaponPositionBytes, &H10 + i * WeaponLineLength, WeaponLineLength).Replace("-", " "))
                TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionByteArray)).Style = ReadOnlyCellStyle
                Select Case CurrentPoistionType
                    Case WeaponPositionType.Arena
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 8)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt4)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 12)
                    Case WeaponPositionType.Base
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                    Case WeaponPositionType.Rules
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                    Case WeaponPositionType.Equipment
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value = BitConverter.ToUInt32(WeaponPositionBytes, &H10 + i * WeaponLineLength + 4)
                        TempGridRow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(WeaponPositionBytes, &H10 + i * WeaponLineLength + 8), 0)
                End Select
                WorkingCollection.Add(TempGridRow)
                ProgressBar1.Value = i
            Next
            DataGridWeaponPositionView.Rows.AddRange(WorkingCollection.ToArray())
        End If
        GetWeaponPositionViewDisplayedColumns()
    End Sub

    Sub GetWeaponPositionViewDisplayedColumns()
        WeaponPositionSettingObjStart.Visible = False
        WeaponPositionByteArray.Visible = False
        WeaponPositionInt1.Visible = False
        WeaponPositionInt2.Visible = False
        WeaponPositionInt3.Visible = False
        WeaponPositionInt4.Visible = False
        WeaponPositionSettingNum.Visible = False
        WeaponPositionSingle1.Visible = False
        WeaponPositionSingle2.Visible = False
        WeaponPositionSingle3.Visible = False
        WeaponPositionSingle4.Visible = False
        WeaponPositionSingle5.Visible = False
        WeaponPositionSingle6.Visible = False
        WeaponPositionShort1.Visible = False
        WeaponPositionShort2.Visible = False
        WeaponPositionShort3.Visible = False
        WeaponPositionShort4.Visible = False
        WeaponPositionIntSet.Visible = False
        Select Case CurrentPoistionType
            Case WeaponPositionType.Arena
                WeaponPositionInt1.Visible = True
                WeaponPositionInt2.Visible = True
                WeaponPositionInt3.Visible = True
                WeaponPositionInt4.Visible = True
            Case WeaponPositionType.Base, WeaponPositionType.Rules
                WeaponPositionInt1.Visible = True
                WeaponPositionInt2.Visible = True
            Case WeaponPositionType.Settings
                WeaponPositionSettingObjStart.Visible = True
                WeaponPositionSettingNum.Visible = True
                WeaponPositionSingle1.Visible = True
                WeaponPositionSingle2.Visible = True
                WeaponPositionSingle3.Visible = True
                WeaponPositionSingle4.Visible = True
                WeaponPositionSingle5.Visible = True
                WeaponPositionSingle6.Visible = True
                WeaponPositionShort1.Visible = True
                WeaponPositionShort2.Visible = True
                WeaponPositionShort3.Visible = True
                WeaponPositionShort4.Visible = True
                WeaponPositionIntSet.Visible = True
            Case WeaponPositionType.Equipment
                WeaponPositionInt1.Visible = True
                WeaponPositionInt2.Visible = True
                WeaponPositionInt3.Visible = True
            Case Else
                WeaponPositionByteArray.Visible = True
        End Select
    End Sub

    Private Sub DataGridWeaponPositionView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridWeaponPositionView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridWeaponPositionView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt4),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)
                'Int32 columns
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                    CLng(MyCell.Value) > UInt32.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CInt(MyCell.Value)
                End If
            Case DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)
                'float columns
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CSng(MyCell.Value)
                End If
            Case DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3),
                     DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)
                'Int16 columns
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                    CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CShort(MyCell.Value)
                End If

        End Select
        If Not MyCell.Value = OldValue Then
            SavePending = True
            SaveChangesWeaponPositionsMenuItem.Visible = True
        End If
    End Sub

    Private Sub DataGridWeaponPositionView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridWeaponPositionView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
               e.RowIndex >= 0 Then
            If e.ColumnIndex = DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionAdd) Then 'add button
                'We need to add a read only filter over items that are added to a setting
                'This function adds a duplicate row at index + 1
                Dim Duplicaterow As DataGridViewRow = DataGridWeaponPositionView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridWeaponPositionView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridWeaponPositionView.Rows(e.RowIndex).Cells(i).Value
                Next
                Duplicaterow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True
                Duplicaterow.Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Style = ReadOnlyCellStyle
                DataGridWeaponPositionView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                If Not CurrentPoistionType = WeaponPositionType.Settings Then
                    For i As Integer = e.RowIndex + 1 To DataGridWeaponPositionView.Rows.Count - 1
                        DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value =
                            CInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value + 1)
                    Next
                End If
                SavePending = True
                SaveChangesWeaponPositionsMenuItem.Visible = True
            ElseIf e.ColumnIndex = DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionDelete) Then 'Delete button
                'We need to add a read only removal filter over items that are added to a setting
                If CurrentPoistionType = WeaponPositionType.Settings Then
                    If DataGridWeaponPositionView.Rows(e.RowIndex).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True Then
                        DataGridWeaponPositionView.Rows.RemoveAt(e.RowIndex)
                    Else
                        DataGridWeaponPositionView.Rows.RemoveAt(e.RowIndex)
                        DataGridWeaponPositionView.Rows(e.RowIndex).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = False
                        DataGridWeaponPositionView.Rows(e.RowIndex).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Style = DefaultCellStyle
                    End If
                Else
                    DataGridWeaponPositionView.Rows.RemoveAt(e.RowIndex)
                    For i As Integer = e.RowIndex To DataGridWeaponPositionView.Rows.Count - 1
                        DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value =
                            CInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionCount)).Value - 1)
                    Next
                End If
                SavePending = True
                SaveChangesWeaponPositionsMenuItem.Visible = True
            Else
                'do nothing
            End If
        End If
    End Sub

    Private Sub SaveChangesWeaponPositionsMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesWeaponPositionsMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildWeaponPositionFile())
    End Sub

    Private Function BuildWeaponPositionFile() As Byte()
        Dim ReturnedBytes As Byte() = New Byte() {}
        Select Case CurrentPoistionType
            Case WeaponPositionType.Arena
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H10) - 1) {} '57 50 41 52 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H41, &H52, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H10)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 4, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 8, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt4)).Value)), 0, ReturnedBytes, &H10 + i * &H10 + 12, 4)
                Next
            Case WeaponPositionType.Base
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H8) - 1) {} '57 50 42 53 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H42, &H53, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H8)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 4, 4)
                Next
            Case WeaponPositionType.Rules
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H8) - 1) {} '57 50 52 55 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H52, &H55, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H8)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &H8 + 4, 4)
                Next
            Case WeaponPositionType.Settings
                Dim ContainedObjects As UInt32 = GetWeaponSettingContainers()
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &H28 + ContainedObjects * 8) - 1) {} '57 50 53 54 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H53, &H54, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(ContainedObjects)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &H28 + ContainedObjects * 8)), 0, ReturnedBytes, 12, 4)
                'Write Items
                Dim CurrentIndex As UInt32 = &H10
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    If DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = False Then
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).Value)), 0, ReturnedBytes, CurrentIndex + 0, 4)
                        Array.Copy(BitConverter.GetBytes(CUInt(GetContainedObjectsInSettingContainer(i))), 0, ReturnedBytes, CurrentIndex + 4, 4)
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart)).Value)), 0, ReturnedBytes, CurrentIndex + 0 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1)).Value)), 0, ReturnedBytes, CurrentIndex + &H4 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2)).Value)), 0, ReturnedBytes, CurrentIndex + &H8 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3)).Value)), 0, ReturnedBytes, CurrentIndex + &HC + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4)).Value)), 0, ReturnedBytes, CurrentIndex + &H10 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5)).Value)), 0, ReturnedBytes, CurrentIndex + &H14 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)).Value)), 0, ReturnedBytes, CurrentIndex + &H18 + 8, 4)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1)).Value)), 0, ReturnedBytes, CurrentIndex + &H1C + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2)).Value)), 0, ReturnedBytes, CurrentIndex + &H1E + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3)).Value)), 0, ReturnedBytes, CurrentIndex + &H20 + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)).Value)), 0, ReturnedBytes, CurrentIndex + &H22 + 8, 2)
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)).Value)), 0, ReturnedBytes, CurrentIndex + &H24 + 8, 4)
                        CurrentIndex += 8 + &H28
                    Else
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingObjStart)).Value)), 0, ReturnedBytes, CurrentIndex + 0, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle1)).Value)), 0, ReturnedBytes, CurrentIndex + &H4, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle2)).Value)), 0, ReturnedBytes, CurrentIndex + &H8, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle3)).Value)), 0, ReturnedBytes, CurrentIndex + &HC, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle4)).Value)), 0, ReturnedBytes, CurrentIndex + &H10, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle5)).Value)), 0, ReturnedBytes, CurrentIndex + &H14, 4)
                        Array.Copy(BitConverter.GetBytes(CSng(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSingle6)).Value)), 0, ReturnedBytes, CurrentIndex + &H18, 4)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort1)).Value)), 0, ReturnedBytes, CurrentIndex + &H1C, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort2)).Value)), 0, ReturnedBytes, CurrentIndex + &H1E, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort3)).Value)), 0, ReturnedBytes, CurrentIndex + &H20, 2)
                        Array.Copy(BitConverter.GetBytes(CUShort(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionShort4)).Value)), 0, ReturnedBytes, CurrentIndex + &H22, 2)
                        Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionIntSet)).Value)), 0, ReturnedBytes, CurrentIndex + &H24, 4)
                        CurrentIndex += &H28
                    End If
                Next
            Case WeaponPositionType.Equipment
                ReturnedBytes = New Byte(&H10 + (DataGridWeaponPositionView.RowCount * &HC) - 1) {} '57 50 45 41 0A
                'Copy Header Start
                Array.Copy(New Byte() {&H57, &H50, &H45, &H41, &HA}, ReturnedBytes, 5)
                'Finish Header
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount)), 0, ReturnedBytes, 8, 4)
                Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.RowCount * &HC)), 0, ReturnedBytes, 12, 4)
                'Write Items
                For i As Integer = 0 To DataGridWeaponPositionView.RowCount - 1
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt1)).Value)), 0, ReturnedBytes, &H10 + i * &HC + 0, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt2)).Value)), 0, ReturnedBytes, &H10 + i * &HC + 4, 4)
                    Array.Copy(HexaDecimalHandlers.EndianReverse(BitConverter.GetBytes(CUInt(DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionInt3)).Value))), 0, ReturnedBytes, &H10 + i * &HC + 8, 4)
                Next
        End Select
        Return ReturnedBytes
    End Function

    Private Function GetWeaponSettingContainers() As UInt32
        Dim ReturnedInt As UInt32 = 0
        For i As Integer = 0 To DataGridWeaponPositionView.Rows.Count - 1
            If DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = False Then
                ReturnedInt += 1
            End If
        Next
        Return ReturnedInt
    End Function

    Private Function GetContainedObjectsInSettingContainer(FirstRow As UInt32)
        Dim ReturnedInt As UInt32 = 1
        For i As Integer = FirstRow + 1 To DataGridWeaponPositionView.Rows.Count - 1
            If DataGridWeaponPositionView.Rows(i).Cells(DataGridWeaponPositionView.Columns.IndexOf(WeaponPositionSettingNum)).ReadOnly = True Then
                ReturnedInt += 1
            Else
                Exit For
            End If
        Next
        Return ReturnedInt
    End Function

End Class
