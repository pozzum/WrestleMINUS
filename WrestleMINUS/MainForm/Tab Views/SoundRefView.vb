Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm


    Dim SoundContainerCount As UInt32 = 0

    Sub FillSoundRefFileView(SelectedData As TreeNode)
        Dim SoundRefBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        SoundContainerCount = BitConverter.ToUInt32(SoundRefBytes, &HC)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridSoundView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SoundContainerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SoundContainerCount - 1
            Dim ContainerName As Integer = BitConverter.ToInt32(SoundRefBytes, &H10 + i * &HC + 0)
            Dim ContainerIndex As Integer = BitConverter.ToInt32(SoundRefBytes, &H10 + i * &HC + 4)
            Dim ContainerSubCount As Integer = BitConverter.ToInt16(SoundRefBytes, &H10 + i * &HC + 8)
            If ContainerSubCount = 0 Then
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = ContainerName
                TempGridRow.Cells(0).Style = ReadOnlyCellStyle
                TempGridRow.Cells(1).Value = ""
                TempGridRow.Cells(2).Value = ""
                TempGridRow.Cells(3).Value = Hex(&H10 + SoundContainerCount * &HC + ContainerIndex)
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
                WorkingCollection.Add(TempGridRow)
            Else
                For J As Integer = 0 To ContainerSubCount - 1
                    Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                    TempGridRow.Cells(0).Value = ContainerName
                    TempGridRow.Cells(0).Style = ReadOnlyCellStyle
                    'reference number
                    TempGridRow.Cells(1).Value = BitConverter.ToUInt32(SoundRefBytes, &H10 + SoundContainerCount * &HC + ContainerIndex + J * 8)
                    'Hex Text
                    TempGridRow.Cells(2).Value = Hex(BitConverter.ToUInt32(SoundRefBytes, &H10 + SoundContainerCount * &HC + ContainerIndex + J * 8 + 4))
                    TempGridRow.Cells(3).Value = Hex(&H10 + SoundContainerCount * &HC + ContainerIndex + J * 8)
                    TempGridRow.Cells(3).Style = ReadOnlyCellStyle
                    WorkingCollection.Add(TempGridRow)
                Next
            End If
            ProgressBar1.Value = i
        Next
        'FullSoundCollection.AddRange(WorkingCollection.ToArray())
        DataGridSoundView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridSoundView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridSoundView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridSoundView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 1 'Must be an integer
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > Int32.MaxValue Then
                    MyCell.Value = OldValue
                Else MyCell.Value = CInt(MyCell.Value)
                End If
            Case 2 'must be Hex String
                If Not HexaDecimalHandlers.HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                End If
        End Select
        SavePending = True
        SaveChangesSoundMenuItem.Visible = True
    End Sub

    Private Sub DataGridSoundView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridSoundView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 4 Then 'add button
                'This function adds a duplicate row at index + 1
                Dim Duplicaterow As DataGridViewRow = DataGridSoundView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridSoundView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridSoundView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridSoundView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                For i As Integer = e.RowIndex + 1 To DataGridSoundView.Rows.Count - 1
                    DataGridSoundView.Rows(i).Cells(3).Value = Hex(CUInt("&h" & DataGridSoundView.Rows(i).Cells(3).Value) + 8)
                Next
                SavePending = True
                SaveChangesSoundMenuItem.Visible = True
            ElseIf e.ColumnIndex = 5 Then 'Delete button
                'we don't want to delete it if there are no other objects in the container.
                If Not (LastIteminContainerCheck(e.RowIndex)) Then
                    DataGridSoundView.Rows.RemoveAt(e.RowIndex)
                    For i As Integer = e.RowIndex To DataGridSoundView.Rows.Count - 1
                        DataGridSoundView.Rows(i).Cells(3).Value = Hex(CUInt("&H" & DataGridSoundView.Rows(i).Cells(3).Value) - 8)
                    Next
                    SavePending = True
                    SaveChangesSoundMenuItem.Visible = True
                End If
            Else
                'do nothing
            End If
        End If
    End Sub

    Function LastIteminContainerCheck(IndexonMenu As Integer) As Boolean
        If IndexonMenu > 0 Then
            If DataGridSoundView.Rows(IndexonMenu).Cells(0).Value = DataGridSoundView.Rows(IndexonMenu - 1).Cells(0).Value Then
                Return False
            End If
        End If
        If IndexonMenu < DataGridSoundView.Rows.Count Then
            If DataGridSoundView.Rows(IndexonMenu).Cells(0).Value = DataGridSoundView.Rows(IndexonMenu + 1).Cells(0).Value Then
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub SaveChangesSoundMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesSoundMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildSoundRefFile())
    End Sub

    Function BuildSoundRefFile() As Byte()
        Dim SoundInformationBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(&H10 + SoundContainerCount * &HC + DataGridSoundView.Rows.Count * 8 - 1) {}
        Dim ContainerNumber As Int32 = -1
        Dim ContainerCount As Int32 = -1 'use as a 0 index
        Dim ContainerStart As Int32 = 0
        Dim ObjectCount As UInt32 = 0
        Dim EmptyRows As UInt32 = 0
        'First we are going to Build the header
        ReturnedBytes(0) = &H67
        ReturnedBytes(4) = &H10
        ReturnedBytes(&HC) = SoundContainerCount
        'Next we want to build all of the header containers
        For i As Integer = 0 To DataGridSoundView.Rows.Count - 1
            If CInt(DataGridSoundView.Rows(i).Cells(0).Value) = ContainerNumber Then
                ObjectCount += 1
            Else
                'We have to capture the new container
                If ContainerCount = -1 Then
                    'Just make the new container we don't need to write it
                    ContainerNumber = CInt(DataGridSoundView.Rows(i).Cells(0).Value)
                    ContainerCount = 0
                    ContainerStart = i - EmptyRows
                    ObjectCount = 1
                Else
                    'we want to write the header array
                    Array.Copy(BitConverter.GetBytes(CUInt(ContainerNumber)), 0, ReturnedBytes, &H10 + ContainerCount * &HC, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(ContainerStart * 8)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 4, 4)
                    Array.Copy(BitConverter.GetBytes(CUInt(ObjectCount)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 8, 4)
                    Array.Copy(New Byte() {&HF0}, 0, ReturnedBytes, &H10 + ContainerCount * &HC + &HB, 1)
                    ContainerNumber = CInt(DataGridSoundView.Rows(i).Cells(0).Value)
                    'MessageBox.Show(ContainerNumber)
                    ContainerCount += 1
                    ContainerStart = i - EmptyRows
                    ObjectCount = 0
                    If Not DataGridSoundView.Rows(i).Cells(1).Value.ToString = "" AndAlso Not DataGridSoundView.Rows(i).Cells(2).Value.ToString = "" Then
                        ObjectCount = 1
                    Else
                        EmptyRows += 1
                    End If
                End If
            End If
            'Now we want to write the bytes of the array since we store and update the offset
            If Not DataGridSoundView.Rows(i).Cells(1).Value.ToString = "" Then
                If Not DataGridSoundView.Rows(i).Cells(2).Value.ToString = "" Then
                    Array.Copy(BitConverter.GetBytes(CUInt(DataGridSoundView.Rows(i).Cells(1).Value)), 0, ReturnedBytes, CUInt("&H" & DataGridSoundView.Rows(i).Cells(3).Value.ToString), 4)
                    Array.Copy(HexaDecimalHandlers.HexStringToByte(DataGridSoundView.Rows(i).Cells(2).Value.ToString.PadLeft(8, "0"), True), 0, ReturnedBytes, CUInt("&H" & DataGridSoundView.Rows(i).Cells(3).Value.ToString) + 4, 4)
                End If
            End If
        Next
        'Now we want to add the last container bytes
        Array.Copy(BitConverter.GetBytes(CUInt(ContainerNumber)), 0, ReturnedBytes, &H10 + ContainerCount * &HC, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(ContainerStart * 8)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 4, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(ObjectCount)), 0, ReturnedBytes, &H10 + ContainerCount * &HC + 8, 4)
        Array.Copy(New Byte() {&HF0}, 0, ReturnedBytes, &H10 + ContainerCount * &HC + &HB, 1)
        'now we want to trim the empty row count of rows from the end
        ReDim Preserve ReturnedBytes(&H10 + SoundContainerCount * &HC + DataGridSoundView.Rows.Count * 8 - 1 - EmptyRows * 8)
        Array.Copy(BitConverter.GetBytes(CUInt(ReturnedBytes.Length - &H10)), 0, ReturnedBytes, 8, 4)
        Return ReturnedBytes
    End Function

#Region "Sound Search Bar"

    Private Sub ToolStripSoundRefSearch_Enter(sender As Object, e As EventArgs) Handles ToolStripSoundRefSearch.Enter
        If ToolStripSoundRefSearch.Text = "Search..." Then
            ToolStripSoundRefSearch.Text = ""
        End If
    End Sub

    Private Sub ToolStripSoundRefSearch_Leave(sender As Object, e As EventArgs) Handles ToolStripSoundRefSearch.Leave
        'FullSoundCollection = DataGridSoundView.Rows
        If ToolStripSoundRefSearch.Text = "" Then
            ToolStripSoundRefSearch.Text = "Search..."
        End If
        Dim TemporaryCollection As DataGridViewRow() = New DataGridViewRow(DataGridSoundView.Rows.Count - 1) {}
        DataGridSoundView.Rows.CopyTo(TemporaryCollection, 0)
        DataGridSoundView.Rows.Clear()
        ProgressBar1.Maximum = TemporaryCollection.Count - 1
        ProgressBar1.Value = 0
        If ToolStripSoundRefSearch.Text = "" OrElse
            ToolStripSoundRefSearch.Text = "Search..." Then
            For i As Integer = 0 To TemporaryCollection.Count - 1
                TemporaryCollection(i).Visible = True
                ProgressBar1.Value = i
            Next
        Else
            For i As Integer = 0 To TemporaryCollection.Count - 1
                If TemporaryCollection(i).Cells(1).Value.ToString.ToLower.Contains(ToolStripSoundRefSearch.Text.ToLower) Then
                    TemporaryCollection(i).Visible = True
                Else
                    TemporaryCollection(i).Visible = False
                End If
                ProgressBar1.Value = i
            Next
        End If
        DataGridSoundView.Rows.AddRange(TemporaryCollection.ToArray)
    End Sub

#End Region

End Class
