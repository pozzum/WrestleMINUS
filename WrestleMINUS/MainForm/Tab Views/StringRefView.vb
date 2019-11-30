Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    Public Class StringReferenceData
        Public StartOffset As UInt32
        Public Length As UInt32
        Public Reference As UInt32
        Public StringBytes As Byte()
        Public Readout As String
    End Class

    Sub FillStringView(SelectedData As TreeNode)
        Dim Testing As String = ""
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridStringView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Try
            Dim NodeTag As ExtendedFileProperties = CType(SelectedData.Tag, ExtendedFileProperties)
            Dim StringBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
            Dim FormattedStringList As List(Of StringReferenceData) = New List(Of StringReferenceData)
            If NodeTag.FileType = PackageType.StringFile Then
                FormattedStringList = ParsePacStringBytesToStringReferenceList(StringBytes)
            ElseIf NodeTag.FileType = PackageType.sdb Then
                FormattedStringList = ParseSDBBytesToStringReferenceList(StringBytes)
            End If
            StringCountToolStripMenuItem.Text = "String Count: " & FormattedStringList.Count
            ProgressBar1.Maximum = FormattedStringList.Count - 1
            ProgressBar1.Value = 0
            StringReferences = New Dictionary(Of UInteger, String)
            For j As UInt32 = 0 To FormattedStringList.Count - 1
                Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
                TempGridRow.Cells(DataGridStringView.Columns.IndexOf(StringHexRefColumn)).Value = Hex(FormattedStringList(j).Reference) 'StringHexRefColumn
                TempGridRow.Cells(DataGridStringView.Columns.IndexOf(StringTextColumn)).Value = FormattedStringList(j).Readout
                TempGridRow.Cells(DataGridStringView.Columns.IndexOf(StringLengthColumn)).Value = FormattedStringList(j).Length
                TempGridRow.Tag = FormattedStringList(j)
                StringReferences.Add(FormattedStringList(j).Reference, FormattedStringList(j).Readout)
                WorkingCollection.Add(TempGridRow)
                ProgressBar1.Value = j
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & Testing)
        End Try
        DataGridStringView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Function ParsePacStringBytesToStringReferenceList(TestedByteArray As Byte()) As List(Of StringReferenceData)
        Dim ReturnedList As List(Of StringReferenceData) = New List(Of StringReferenceData)
        Dim StringCount As Integer = BitConverter.ToInt32(TestedByteArray, 4)
        For j As Integer = 0 To StringCount - 1
            Dim TempStringReference As StringReferenceData = New StringReferenceData With {
                .StartOffset = BitConverter.ToUInt32(TestedByteArray, 8 + j * 12 + 0),
                .Length = BitConverter.ToUInt32(TestedByteArray, 8 + j * 12 + 4),
                .Reference = BitConverter.ToUInt32(TestedByteArray, 8 + j * 12 + 8),
                .StringBytes = New Byte(.Length - 1) {}}
            Array.Copy(TestedByteArray, TempStringReference.StartOffset, TempStringReference.StringBytes, 0, TempStringReference.Length)
            'Trim all 00 chars so the strings don't end abruptly in future manipulation
            TempStringReference.Readout = Encoding.Default.GetString(TempStringReference.StringBytes).TrimEnd(Chr(0))
            ReturnedList.Add(TempStringReference)
        Next
        Return ReturnedList
    End Function

    Function ParseSDBBytesToStringReferenceList(TestedByteArray As Byte()) As List(Of StringReferenceData)
        Dim ReturnedList As List(Of StringReferenceData) = New List(Of StringReferenceData)
        Dim StringCount As Integer = BitConverter.ToInt32(TestedByteArray, 4)
        For j As Integer = 0 To StringCount - 1
            Dim TempStringReference As StringReferenceData = New StringReferenceData With {
                .StartOffset = BitConverter.ToUInt32(TestedByteArray, 8 + j * 12 + 0),
                .Length = BitConverter.ToUInt32(TestedByteArray, 8 + j * 12 + 4),
                .Reference = BitConverter.ToUInt32(TestedByteArray, 8 + j * 12 + 8),
                .StringBytes = New Byte(.Length - 1) {}}
            Array.Copy(TestedByteArray, TempStringReference.StartOffset, TempStringReference.StringBytes, 0, TempStringReference.Length)
            'Trim all 00 chars so the strings don't end abruptly in future manipulation
            TempStringReference.Readout = Encoding.Default.GetString(TempStringReference.StringBytes).TrimEnd(Chr(0))
            ReturnedList.Add(TempStringReference)
        Next
        Return ReturnedList
    End Function

    Sub SortStringView()
        Dim TempColumn As DataGridViewColumn = New DataGridViewTextBoxColumn With {.Name = "TempCol", .Visible = False}
        DataGridStringView.Columns.Add(TempColumn)
        Dim ColNum As Integer = DataGridStringView.Columns.IndexOf(TempColumn)
        For Each TempRow As DataGridViewRow In DataGridStringView.Rows
            Dim TempNumber As UInt32 = CUInt("&H" & TempRow.Cells(DataGridStringView.Columns.IndexOf(StringHexRefColumn)).Value.ToString)
            TempRow.Cells(ColNum).Value = TempNumber
        Next
        DataGridStringView.Sort(DataGridStringView.Columns(ColNum), System.ComponentModel.ListSortDirection.Ascending)
        DataGridStringView.Columns.Remove(TempColumn)
        SortStringsToolStripMenuItem.Visible = False
    End Sub

    Function CheckDuplicateStrings() As Boolean 'returns True for a dupe row
        Dim BuiltList As List(Of Integer) = New List(Of Integer)
        For i As Integer = 0 To DataGridStringView.Rows.Count - 1
            Dim TempNumber As UInt32 = CUInt("&H" & DataGridStringView.Rows(i).Cells(DataGridStringView.Columns.IndexOf(StringHexRefColumn)).Value.ToString)
            If BuiltList.Contains(TempNumber) Then
                MessageBox.Show("Duplicate string ID found at row " & i)
                Return True
            Else
                BuiltList.Add(TempNumber)
            End If
        Next
        Return False
    End Function

    Private Sub SaveChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesStringMenuItem.Click
        SortStringView()
        If Not CheckDuplicateStrings() Then
            FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildStringFile())
        End If
    End Sub

    Private Sub SortStringsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SortStringsToolStripMenuItem.Click
        SortStringView()
    End Sub

    Private Sub ExportStringArrayToCSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportStringArrayToCSVToolStripMenuItem.Click
        Dim SaveCSVFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "StringFile.csv",
            .Filter = "Comma Separated Values|*.csv|All files (*.*)|*.*"}
        If SaveCSVFile.ShowDialog = DialogResult.OK Then
            Dim headers = (From header As DataGridViewColumn In DataGridStringView.Columns.Cast(Of DataGridViewColumn)()
                           Select header.HeaderText).ToArray
            Dim rows As List(Of String) = New List(Of String)
            For Each temprow As DataGridViewRow In DataGridStringView.Rows
                Dim NoLineBreaks As String = temprow.Cells(1).Value.ToString.Replace(ChrW(&HA), ChrW(&HB6))
                Dim Tempstring As String = ""
                Tempstring = temprow.Cells(0).Value & vbTab &
                NoLineBreaks & vbTab & temprow.Cells(2).Value
                rows.Add(Tempstring)
            Next
            Using sw As New IO.StreamWriter(SaveCSVFile.FileName)
                sw.WriteLine("HexRef" & vbTab & "String Text" & vbTab & "Length")
                For Each r In rows
                    sw.WriteLine(r)
                Next
            End Using
            MessageBox.Show("File Saved")
        End If
    End Sub

    Dim OldLength As Integer
    Dim StringLengthTheSame As Boolean = False

    Private Sub DataGridStringView_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellEnter
        OldValue = sender.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
        If e.ColumnIndex = DataGridStringView.Columns.IndexOf(StringHexRefColumn) Then 'Hex Text Reference Editing
            'nothing additional
        ElseIf e.ColumnIndex = DataGridStringView.Columns.IndexOf(StringTextColumn) Then
            'additional checks
            OldLength = DataGridStringView.Rows(e.RowIndex).Cells(DataGridStringView.Columns.IndexOf(StringLengthColumn)).Value
            StringLengthTheSame = (OldValue.Length + 1 = OldLength) 'If length is not the same then it might be a super string
        ElseIf e.ColumnIndex = DataGridStringView.Columns.IndexOf(StringLengthColumn) Then
            'nothing additional
        End If
    End Sub

    Private Sub DataGridStringView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridStringView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = DataGridStringView.Columns.IndexOf(StringHexRefColumn) Then 'Hex value
            If Not HexaDecimalHandlers.HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            Else
                SortStringsToolStripMenuItem.Visible = True
                SaveChangesStringMenuItem.Visible = True
            End If
        ElseIf e.ColumnIndex = DataGridStringView.Columns.IndexOf(StringTextColumn) Then 'string text
            If Not MyCell.Value = OldValue Then
                If StringLengthTheSame Then
                    DataGridStringView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value.length + 1
                Else
                    If MessageBox.Show("String currently contains extra characters." & vbNewLine &
                                      "Would you like to maintain the length", "Potential Super String Detected!",
                                       MessageBoxButtons.YesNo) = DialogResult.No Then
                        DataGridStringView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value.length + 1
                    Else 'check if new string is too long
                        If DataGridStringView.Rows(e.RowIndex).Cells(2).Value < MyCell.Value.length + 1 Then
                            MessageBox.Show("String is too long, string will be truncated.")
                            MyCell.Value = MyCell.Value.ToString.Substring(0, DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
                        End If
                    End If
                End If
                DataGridStringView.Rows(e.RowIndex).Tag = Encoding.Default.GetBytes(MyCell.Value + Chr(0)) 'Stores the new string as bytes in the tag
                'this redim keeps the right length for a super string type string where there are several 0 chars at the end
                ReDim Preserve DataGridStringView.Rows(e.RowIndex).Tag(DataGridStringView.Rows(e.RowIndex).Cells(2).Value - 1)
                SavePending = True
                SaveChangesStringMenuItem.Visible = True
            End If
        ElseIf e.ColumnIndex = DataGridStringView.Columns.IndexOf(StringLengthColumn) Then 'Adjusting Length only
            If Not IsNumeric(MyCell.Value) OrElse
               MyCell.Value < 1 Then
                MyCell.Value = OldValue
            Else
                If MyCell.Value < DataGridStringView.Rows(e.RowIndex).Cells(1).Value.length + 1 Then
                    MessageBox.Show("String is too long, string will be truncated.")
                    DataGridStringView.Rows(e.RowIndex).Cells(1).Value =
                                DataGridStringView.Rows(e.RowIndex).Cells(1).Value.ToString.Substring(0, MyCell.Value - 1) 'Truncates the String
                    DataGridStringView.Rows(e.RowIndex).Tag = Encoding.Default.GetBytes(DataGridStringView.Rows(e.RowIndex).Cells(1).Value + Chr(0)) 'Stores the new string as bytes in the tag
                Else 'Redim the tag to expand it
                    ReDim Preserve DataGridStringView.Rows(e.RowIndex).Tag(MyCell.Value - 1)
                End If
                SavePending = True
                SaveChangesStringMenuItem.Visible = True
            End If
        End If
    End Sub

    Private Sub DataGridStringView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridStringView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = DataGridStringView.Columns.IndexOf(AddStringButton) Then 'add button
                Dim Duplicaterow As DataGridViewRow = DataGridStringView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridStringView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridStringView.Rows(e.RowIndex).Cells(i).Value
                Next
                Duplicaterow.Tag = DataGridStringView.Rows(e.RowIndex).Tag
                DataGridStringView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                'TO DO we can add in a check here if the next string ref is already taken
                'TO DO Disabled DataGridView Buttons
            ElseIf e.ColumnIndex = DataGridStringView.Columns.IndexOf(DeleteStringButton) Then 'Delete button
                DataGridStringView.Rows.RemoveAt(e.RowIndex)
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub

    Private Sub ToolStripTextBoxSearch_Enter(sender As Object, e As EventArgs) Handles ToolStripTextBoxSearch.Enter
        If ToolStripTextBoxSearch.Text = "Search..." Then
            ToolStripTextBoxSearch.Text = ""
        End If
    End Sub

    Private Sub ToolStripTextBoxSearch_Leave(sender As Object, e As EventArgs) Handles ToolStripTextBoxSearch.Leave
        If ToolStripTextBoxSearch.Text = "" Then
            ToolStripTextBoxSearch.Text = "Search..."
        End If
        Dim TemporaryCollection As DataGridViewRow() = New DataGridViewRow(DataGridStringView.Rows.Count - 1) {}
        DataGridStringView.Rows.CopyTo(TemporaryCollection, 0)
        DataGridStringView.Rows.Clear()
        ProgressBar1.Maximum = TemporaryCollection.Count - 1
        ProgressBar1.Value = 0
        If ToolStripTextBoxSearch.Text = "" OrElse
            ToolStripTextBoxSearch.Text = "Search..." Then
            For i As Integer = 0 To TemporaryCollection.Count - 1
                TemporaryCollection(i).Visible = True
                ProgressBar1.Value = i
            Next
        Else
            For i As Integer = 0 To TemporaryCollection.Count - 1
                If TemporaryCollection(i).Cells(1).Value.ToString.ToLower.Contains(ToolStripTextBoxSearch.Text.ToLower) Then
                    TemporaryCollection(i).Visible = True
                Else
                    TemporaryCollection(i).Visible = False
                End If
                ProgressBar1.Value = i
            Next
        End If
        DataGridStringView.Rows.AddRange(TemporaryCollection.ToArray)
    End Sub

    Private Function BuildStringFile() As Byte()
        Dim StringCount As Integer = DataGridStringView.RowCount
        Dim StringSum As Integer = 0
        For i As Integer = 0 To StringCount - 1
            StringSum += DataGridStringView.Rows(i).Cells(2).Value
        Next
        'First get the string count and make a header
        Dim ReturnedBytes As Byte() = New Byte(&H8 + StringCount * &HC + StringSum - 1) {} 'String Sum adds on an extra 1 for the last string from my tests
        'Building the header 0000 , string count
        Array.Copy(BitConverter.GetBytes(CUInt(StringCount)), 0, ReturnedBytes, 4, 4)
        ProgressBar1.Maximum = StringCount - 1
        ProgressBar1.Value = 0
        Dim index As UInt32 = &H8 + StringCount * &HC
        For i As Integer = 0 To StringCount - 1
            'index of string won't change
            Array.Copy(BitConverter.GetBytes(index), 0, ReturnedBytes, 8 + i * 12 + 0, 4)
            'String Length will be equal to cell 3 (2 in 0 index)
            Array.Copy(BitConverter.GetBytes(CUInt(DataGridStringView.Rows(i).Cells(2).Value)), 0, ReturnedBytes, 8 + i * 12 + 4, 4)
            'Reference is set from the first cell
            Array.Copy(BitConverter.GetBytes(CUInt("&h" & DataGridStringView.Rows(i).Cells(0).Value)), 0, ReturnedBytes, 8 + i * 12 + 8, 4)
            'now we have to copy the string from the tag storage
            Dim TempArray As Byte() = Encoding.Default.GetBytes(DataGridStringView.Rows(i).Cells(1).Value) 'DataGridStringView.Rows(i).Tag ' Casting the Tag so the array handles it properly
            Array.Copy(TempArray, 0, ReturnedBytes, index, TempArray.Length) 'CUInt(DataGridStringView.Rows(i).Cells(2).Value))
            'Now add the length to the index
            index += CUInt(DataGridStringView.Rows(i).Cells(2).Value)
            ProgressBar1.Value = i
        Next
        Return ReturnedBytes
    End Function

End Class
