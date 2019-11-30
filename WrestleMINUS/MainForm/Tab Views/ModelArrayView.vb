Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Dim ObjArrayContainerCount As Integer = 0

    Sub FillObjectArrayView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjArrayView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim ObjArrayBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim ChairCount As Integer = BitConverter.ToInt32(ObjArrayBytes, &HC)
        Dim ParentStrings As String() = New String(ChairCount) {}
        ParentStrings(0) = "Base Object"
        ObjArrayContainerCount = 0
        For i As Integer = 0 To ChairCount - 1
            Dim ItemString As String = Encoding.ASCII.GetString(ObjArrayBytes, &H20 + ChairCount * 8 + i * &H30, &H10).Trim()
            Dim InternalItemCount As Int32 = BitConverter.ToInt32(ObjArrayBytes, &H48 + ChairCount * 8 + i * &H30)
            Dim StartIndex As Int32 = BitConverter.ToInt32(ObjArrayBytes, &H4C + ChairCount * 8 + i * &H30)
            TempGridRow = CloneRow.Clone()
            If InternalItemCount > 0 Then
                ObjArrayContainerCount += 1
                For j As Integer = StartIndex To StartIndex + InternalItemCount
                    ParentStrings(j) = ItemString
                Next
            End If
            TempGridRow.Cells(0).Value = i 'Absolute Index
            TempGridRow.Cells(1).Value = ParentStrings(i)  'Parent
            TempGridRow.Cells(2).Value = BitConverter.ToInt32(ObjArrayBytes, &H24 + i * 8) 'Number
            TempGridRow.Cells(3).Value = BitConverter.ToBoolean(ObjArrayBytes, &H20 + i * 8) 'Enabled
            If InternalItemCount > 0 Then
                TempGridRow.Cells(3).ReadOnly = True
            End If
            TempGridRow.Cells(4).Value = ItemString  'Object Name
            TempGridRow.Cells(5).Value = BitConverter.ToSingle(ObjArrayBytes, &H30 + ChairCount * 8 + i * &H30) 'X Float
            TempGridRow.Cells(6).Value = BitConverter.ToSingle(ObjArrayBytes, &H34 + ChairCount * 8 + i * &H30) 'Y Float
            TempGridRow.Cells(7).Value = BitConverter.ToSingle(ObjArrayBytes, &H38 + ChairCount * 8 + i * &H30) 'Z Float
            TempGridRow.Cells(8).Value = BitConverter.ToSingle(ObjArrayBytes, &H3C + ChairCount * 8 + i * &H30) 'RX Float
            TempGridRow.Cells(9).Value = BitConverter.ToSingle(ObjArrayBytes, &H40 + ChairCount * 8 + i * &H30) 'RY Float
            TempGridRow.Cells(10).Value = BitConverter.ToSingle(ObjArrayBytes, &H44 + ChairCount * 8 + i * &H30) 'RZ Float
            TempGridRow.Cells(11).Value = InternalItemCount 'Item Count
            TempGridRow.Cells(12).Value = StartIndex 'Start Index
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridObjArrayView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Sub LoadObjectArrayView(CSVString As String())
        'Line 0 should be the header lione and ignored
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjArrayView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        ObjArrayContainerCount = 0
        For i As Integer = 1 To CSVString.Count - 1
            TempGridRow = CloneRow.Clone()
            Dim CSVValues As String() = CSVString(i).Split(",")
            Dim InternalItemCount As Int32 = CSVValues(11)
            TempGridRow.Cells(0).Value = CSVValues(0)
            TempGridRow.Cells(1).Value = CSVValues(1)
            TempGridRow.Cells(2).Value = CSVValues(2) 'Number
            TempGridRow.Cells(3).Value = CSVValues(3) 'Enabled
            If InternalItemCount > 0 Then
                ObjArrayContainerCount += 1
                TempGridRow.Cells(3).ReadOnly = True
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
            End If
            TempGridRow.Cells(4).Value = CSVValues(4)  'Object Name
            TempGridRow.Cells(5).Value = CSVValues(5) 'X Float
            TempGridRow.Cells(6).Value = CSVValues(6) 'Y Float
            TempGridRow.Cells(7).Value = CSVValues(7) 'Z Float
            TempGridRow.Cells(8).Value = CSVValues(8) 'RX Float
            TempGridRow.Cells(9).Value = CSVValues(9) 'RY Float
            TempGridRow.Cells(10).Value = CSVValues(10) 'RZ Float
            TempGridRow.Cells(11).Value = InternalItemCount 'Item Count
            TempGridRow.Cells(12).Value = CSVValues(12) 'Start Index
            WorkingCollection.Add(TempGridRow)
        Next
        DataGridObjArrayView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridObjArrayView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjArrayView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridObjArrayView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            'Case 0 OrElse 1 'Index and Parent are Read Only
            Case 2 'Integer Object Number
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf Not (Math.Floor(MyCell.Value) = Math.Ceiling(MyCell.Value)) Then
                    MyCell.Value = OldValue
                End If
            'Case 3 Check Box does not have an invalid state to reset
            Case 4 'String for object name
                If MyCell.Value.ToString.Trim().ToString.Length > 16 Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = MyCell.Value.ToString.Trim()
                    MyCell.Value = MyCell.Value.ToString.Replace(" ", "")
                    'if it's a container object we need to update the relevent names on the datagrid..
                    If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                        'It is a container file and we need to update the lines
                        Dim StartIndex As Integer = CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(12).Value)
                        Dim EndingIndex As Integer = StartIndex + CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) - 1
                        For i As Integer = StartIndex To EndingIndex
                            DataGridObjArrayView.Rows(i).Cells(1).Value = MyCell.Value
                        Next
                    End If
                End If
            Case > 4 'Single Number Values
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                End If
                'Case 11 OrElse 12 'Item Count and Starting Index Should be Changed by User. Only by Add / Delete Buttons.
        End Select
        SaveChangesYOBJArrayMenuItem.Visible = True
    End Sub

    Private Sub DataGridObjArrayView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjArrayView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 13 Then 'add button
                If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                    'Container Object We need to add at least 1 Sub items
                    AddObjArrayContainer(e.RowIndex)
                Else
                    'Individual Item that we need to update within the container
                    AddObjArrayIndividualObject(e.RowIndex)
                End If
            ElseIf e.ColumnIndex = 14 Then 'Delete button
                If CInt(DataGridObjArrayView.Rows(e.RowIndex).Cells(11).Value) > 0 Then
                    'Container Object We also need to delete the sub objects
                    DeleteObjArrayContainer(e.RowIndex)
                Else
                    'Individual Item we will need to update the container object.
                    DeleteObjArrayIndividualObject(e.RowIndex)
                End If
            Else
                'do nothing
            End If
        End If
    End Sub

    Sub AddObjArrayContainer(index As Integer)
        'This function adds a new container object with 1 item at the location of index+1
        Dim Duplicaterow As DataGridViewRow = DataGridObjArrayView.Rows(index).Clone
        For i As Integer = 0 To DataGridObjArrayView.Rows(index).Cells.Count - 1
            Duplicaterow.Cells(i).Value = DataGridObjArrayView.Rows(index).Cells(i).Value
        Next
        'change Start Index and Container Count
        Duplicaterow.Cells(11).Value = 1
        Duplicaterow.Cells(12).Value = DataGridObjArrayView.Rows(index + 1).Cells(12).Value
        'Add +1 because we add a row before the command is actually called..
        Dim LastItemOfPreviousContainerToCopy As Integer = CInt(DataGridObjArrayView.Rows(index + 1).Cells(12).Value) + 1
        DataGridObjArrayView.Rows.Insert(index + 1, Duplicaterow)
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond or equal to the added object we need to +1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) >= index Then
                'if the index matches then the added item was added to the previous
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
            End If
        Next
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) + 1
        Next
        MessageBox.Show("Adding Sub Object " & LastItemOfPreviousContainerToCopy)
        AddObjArrayIndividualObject(LastItemOfPreviousContainerToCopy, True)
    End Sub

    Sub AddObjArrayIndividualObject(index As Integer, Optional NewContainer As Boolean = False)
        'This function adds a duplicate row at index + 1, but index + 1 has to have true index updated as well
        Dim Duplicaterow As DataGridViewRow = DataGridObjArrayView.Rows(index).Clone
        For i As Integer = 0 To DataGridObjArrayView.Rows(index).Cells.Count - 1
            Duplicaterow.Cells(i).Value = DataGridObjArrayView.Rows(index).Cells(i).Value
        Next
        DataGridObjArrayView.Rows.Insert(index + 1, Duplicaterow)
        'This function runs after the index is actually added
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) + 1
        Next
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond or equal to the added object we need to +1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) = index Then
                MessageBox.Show("Matched Index")
                'if the index matches then the added item was added to the previous container... or it's a new container
                If Not NewContainer Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
                ElseIf Not CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) = 1 Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + 1
                End If
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) >= index Then
                If Not NewContainer Then
                    'this means that this is the container that contains the added object so we need to increase the item count
                    DataGridObjArrayView.Rows(i).Cells(11).Value = CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) + 1
                End If
            End If
        Next
    End Sub

    Sub DeleteObjArrayContainer(index As Integer)
        'This function runs before the container is actually deleted
        If CInt(DataGridObjArrayView.Rows(index).Cells(11).Value) > 0 Then
            Dim LastContainedObject As Integer = (CInt(DataGridObjArrayView.Rows(index).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(index).Cells(11).Value) - 1)
            Dim FirstContainedObject As Integer = CInt(DataGridObjArrayView.Rows(index).Cells(12).Value)
            'MessageBox.Show(LastContainedObject & " to " & FirstContainedObject)
            For i As Integer = LastContainedObject To FirstContainedObject Step -1
                MessageBox.Show("Deleting Object " & i)
                DeleteObjArrayIndividualObject(i)
            Next
            'we want to skip the last 2 lines so we don't accidently delete 2 rows because the last for will call the delete container again
        Else
            MessageBox.Show("Deleting Container")
            For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
                'Update Absolute Index
                DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) - 1
            Next
            'Try To update containers
            For i As Integer = 0 To ObjArrayContainerCount
                'if the start index is beyond the deleted object  we need to -1 to the start index
                If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                    DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) - 1
                End If
            Next
            MessageBox.Show("Deleting Container Line")
            DataGridObjArrayView.Rows.RemoveAt(index)
            ObjArrayContainerCount -= 1
        End If
    End Sub

    Sub DeleteObjArrayIndividualObject(index As Integer)
        'This function runs before the index is actually deleted
        For i As Integer = index + 1 To DataGridObjArrayView.Rows.Count - 1
            'Update Absolute Index
            DataGridObjArrayView.Rows(i).Cells(0).Value = CInt(DataGridObjArrayView.Rows(i).Cells(0).Value) - 1
        Next
        Dim DeleteContainer As Boolean = False
        Dim ContainertoDelete As Integer = -1
        'Try to update containers
        For i As Integer = 0 To ObjArrayContainerCount
            'if the start index is beyond the deleted object  we need to -1 to the start index
            If CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) > index Then
                DataGridObjArrayView.Rows(i).Cells(12).Value = CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) - 1
            ElseIf CInt(DataGridObjArrayView.Rows(i).Cells(12).Value) + CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) > index Then
                'this means that this is the container that contains the deleted object so we need to reduce the idem count
                DataGridObjArrayView.Rows(i).Cells(11).Value = CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) - 1
                'if the updated container item count = 0 we will want to delete the container as well
                If CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) = 0 Then
                    DeleteContainer = True
                    ContainertoDelete = i
                End If
            End If
        Next
        DataGridObjArrayView.Rows.RemoveAt(index)
        If DeleteContainer Then DeleteObjArrayContainer(ContainertoDelete)
    End Sub

    Private Sub SaveYOBJArrayChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesYOBJArrayMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildYOBJArrayFile())
    End Sub

    Private Function BuildYOBJArrayFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(&H20 + ((&H30 + &H8) * DataGridObjArrayView.Rows.Count) - 1) {}
        Dim ContainerCount As UInt32 = 0
        Dim ObjectCount As UInt32 = 0
        For i As Integer = 0 To DataGridObjArrayView.Rows.Count - 1
            If CInt(DataGridObjArrayView.Rows(i).Cells(11).Value) > 0 Then
                ContainerCount += 1
            Else
                ObjectCount = DataGridObjArrayView.Rows.Count - ContainerCount
                Exit For
            End If
        Next
        'File Header
        Array.Copy(New Byte() {&HEA, &HA1, &H59, &H0, &H1, &H0, &H0, &H0, &H1, &H0, &H0, &H0}, 0, ReturnedBytes, 0, &HC)
        'total Containers + Objects
        Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows.Count)), 0, ReturnedBytes, &HC, 4)
        'Container Count
        Array.Copy(BitConverter.GetBytes(ContainerCount), 0, ReturnedBytes, &H10, 4)
        'Object Count
        Array.Copy(BitConverter.GetBytes(ObjectCount), 0, ReturnedBytes, &H14, 4)
        Dim DataIndex As UInt32 = &H20 + (DataGridObjArrayView.Rows.Count * 8)
        For i As Integer = 0 To DataGridObjArrayView.Rows.Count - 1
            'enabled First
            If DataGridObjArrayView.Rows(i).Cells(3).Value Then
                ReturnedBytes(&H20 + i * 8) = 1
            Else
                ReturnedBytes(&H20 + i * 8) = 0
            End If
            'Item Number
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, &H24 + i * 8, 4)
            'Name
            Dim ObjectName As String = DataGridObjArrayView.Rows(i).Cells(4).Value
            Dim StringBytes As Byte() = Encoding.ASCII.GetBytes(ObjectName)
            Array.Copy(StringBytes, 0, ReturnedBytes, DataIndex + i * &H30, StringBytes.Length)
            'X Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(5).Value)), 0, ReturnedBytes, DataIndex + &H10 + (i * &H30), 4)
            'Y Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(6).Value)), 0, ReturnedBytes, DataIndex + &H14 + (i * &H30), 4)
            'Z Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(7).Value)), 0, ReturnedBytes, DataIndex + &H18 + (i * &H30), 4)
            'RX Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(8).Value)), 0, ReturnedBytes, DataIndex + &H1C + (i * &H30), 4)
            'RY Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(9).Value)), 0, ReturnedBytes, DataIndex + &H20 + (i * &H30), 4)
            'RZ Float
            Array.Copy(BitConverter.GetBytes(CSng(DataGridObjArrayView.Rows(i).Cells(10).Value)), 0, ReturnedBytes, DataIndex + &H24 + (i * &H30), 4)
            'Item Count
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(11).Value)), 0, ReturnedBytes, DataIndex + &H28 + (i * &H30), 4)
            'Start Index
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridObjArrayView.Rows(i).Cells(12).Value)), 0, ReturnedBytes, DataIndex + &H2C + (i * &H30), 4)
        Next
        Return ReturnedBytes
    End Function

    Private Sub ExportYOBJArrayToCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportYOBJArrayToCSVToolStripMenuItem.Click
        Dim SaveCSVFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "Object Array.csv",
            .Filter = "Comma Separated Values|*.csv|All files (*.*)|*.*"}
        If SaveCSVFile.ShowDialog = DialogResult.OK Then
            Dim headers = (From header As DataGridViewColumn In DataGridObjArrayView.Columns.Cast(Of DataGridViewColumn)()
                           Select header.HeaderText).ToArray
            Dim rows As List(Of String) = New List(Of String)
            For Each temprow As DataGridViewRow In DataGridObjArrayView.Rows
                If Not temprow.IsNewRow Then
                    Dim Tempstring As String = ""
                    For Each tempcell As DataGridViewCell In temprow.Cells
                        If temprow.Cells.IndexOf(tempcell) = 0 Then
                            Tempstring = tempcell.Value
                        ElseIf tempcell.ValueType = GetType(CheckBox) Then
                            If tempcell.Value Then
                                Tempstring += ",1"
                            Else
                                Tempstring += ",0"
                            End If
                        Else
                            Tempstring += "," & tempcell.Value.ToString.Trim()
                        End If
                    Next
                    rows.Add(Tempstring)
                End If
            Next
            Using sw As New IO.StreamWriter(SaveCSVFile.FileName)
                sw.WriteLine(String.Join(",", headers))
                For Each r In rows
                    sw.WriteLine(r)
                Next
            End Using
            MessageBox.Show("File Saved")
        End If
    End Sub

    Private Sub ImportYOBJArrayFromCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportYOBJArrayFromCSVToolStripMenuItem.Click
        Dim OpenCSVFile As OpenFileDialog = New OpenFileDialog With {
            .FileName = "Object Array.csv",
            .Filter = "Comma Separated Values|*.csv|All files (*.*)|*.*"}
        If OpenCSVFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(OpenCSVFile.FileName) Then
                LoadObjectArrayView(File.ReadAllLines(OpenCSVFile.FileName))
                SaveChangesYOBJArrayMenuItem.Visible = True
            End If
        End If
    End Sub

End Class
