Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    Sub FillMaskView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMaskView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim MaskBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim MaskHeader As Integer = BitConverter.ToInt32(MaskBytes, &H4) ' should be C
        If Not MaskHeader = &HC Then
            MessageBox.Show("Unknown error with CE header")
            Exit Sub
        End If
        Dim ActiveIndex As Long = MaskHeader
        Dim ContainerCount As Integer = BitConverter.ToInt32(MaskBytes, &H8) ' Should be 2
        If ContainerCount = 0 Then
            MessageBox.Show("CE contains no masks")
            Exit Sub
        End If
        Dim CurrentMaskName As String
        '-
        'First we have to process each container, these are the M_Head and M_Body containers
        '-
        For i As Integer = 0 To ContainerCount - 1 'process each mask individuallt head then body
            Dim MaskHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex) ' should start as offset &hC, should also be &H4c
            CurrentMaskName = System.Text.Encoding.ASCII.GetString(MaskBytes, ActiveIndex + &H8, 6)
            'BitConverter.ToString(mask_array, active_offset + &H8)
            Dim MaskLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H4) ' should be &h4c if mask masks no objects
            Dim MaskedParts As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &H48)
            If Not MaskedParts = 0 Then
                '-
                'Now if the container contains masked parts we have to start processing those masked parts
                '-
                ActiveIndex += MaskHeaderLength
                For j As Integer = 0 To MaskedParts - 1
                    Dim MaskedPartHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                    If Not MaskedPartHeaderLength = &H10 Then
                        MessageBox.Show("Error with Mask Header")
                        Exit Sub
                    End If
                    Dim TotalPartHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                    Dim PartNumber As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 8)
                    Dim MasksOnPart As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &HC) 'should be 1 always from what I've seen
                    If MasksOnPart = 0 Then
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_1"
                        TempGridRow.Cells(1).Value = "nil"
                        TempGridRow.Cells(2).Value = "nil"
                        WorkingCollection.Add(TempGridRow)
                        ActiveIndex += TotalPartHeaderLength
                    Else
                        '-
                        'For some reason they seem to have the capability of containing more than 1 set of mask arrays so this is just an extra layer of precaution
                        'This next for should only run once for every CE I've looked at
                        '-
                        ActiveIndex += MaskedPartHeaderLength
                        For k As Integer = 0 To MasksOnPart - 1
                            Dim TrueMaskHeaderLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                            If Not TrueMaskHeaderLength = &H10 Then
                                MessageBox.Show("Error with Mask Header")
                                Exit Sub
                            End If
                            Dim TrueMaskTotalLength As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                            Dim TrueMaskNumber As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + 8)
                            Dim TrueMaskCount As Integer = BitConverter.ToInt32(MaskBytes, ActiveIndex + &HC)
                            If MasksOnPart = 0 Then
                                TempGridRow = CloneRow.Clone()
                                TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_" & TrueMaskNumber
                                TempGridRow.Cells(1).Value = "nil"
                                TempGridRow.Cells(2).Value = "nil"
                                WorkingCollection.Add(TempGridRow)
                                ActiveIndex += TrueMaskTotalLength
                            Else
                                '-
                                'Now to handle the true sets of masks each set is 8 bytes
                                '-
                                ActiveIndex += TrueMaskHeaderLength
                                For L As Integer = 0 To TrueMaskCount - 1
                                    'I only want the first line to have the part name
                                    If L = 0 Then
                                        TempGridRow = CloneRow.Clone()
                                        TempGridRow.Cells(0).Value = CurrentMaskName & "_" & PartNumber & "_" & TrueMaskNumber
                                        TempGridRow.Cells(1).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                                        TempGridRow.Cells(2).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                                        WorkingCollection.Add(TempGridRow)
                                        ActiveIndex += 8
                                    Else
                                        TempGridRow = CloneRow.Clone()
                                        TempGridRow.Cells(0).Value = ""
                                        TempGridRow.Cells(1).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex)
                                        TempGridRow.Cells(2).Value = BitConverter.ToInt32(MaskBytes, ActiveIndex + 4)
                                        WorkingCollection.Add(TempGridRow)
                                        ActiveIndex += 8
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            Else
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = CurrentMaskName & "_0_0"
                TempGridRow.Cells(1).Value = "nil"
                TempGridRow.Cells(2).Value = "nil"
                WorkingCollection.Add(TempGridRow)
                ActiveIndex += MaskLength
            End If
        Next
        DataGridMaskView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub FillMaskDataGrid(ByVal Source As String)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMaskView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
        Dim NewFaceStringArray As String() = File.ReadAllLines(Source)
        For i As Integer = 0 To NewFaceStringArray.Count - 1 Step 2
            'MessageBox.Show(New_Faces(i))
            Dim Temp_Name As String
            Dim Temp_Object_Number As Integer = NewFaceStringArray(i).Substring(6, 1)
            If Temp_Object_Number = 0 Then
                Temp_Name = "M_Head_0_0"
            Else
                Temp_Name = "M_Body_" & Temp_Object_Number - 1 & "_0"
            End If
            Dim Face_Numbers As String() = NewFaceStringArray(i + 1).Split(",")
            Dim Individual_Faces As List(Of Integer) = New List(Of Integer)
            For Each temp_face_num As String In Face_Numbers
                Individual_Faces.Add(Integer.Parse(temp_face_num))
            Next
            Individual_Faces.Sort()
            Dim Name_Added As Boolean = False
            Dim Base_Number As Integer = Individual_Faces(0) - 1
            For J As Integer = 1 To Individual_Faces.Count - 1
                If Not Individual_Faces(J) = Individual_Faces(J - 1) + 1 Then
                    If Name_Added = False Then
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = Temp_Name
                        TempGridRow.Cells(1).Value = Base_Number
                        TempGridRow.Cells(2).Value = (Individual_Faces(J - 1) - 1)
                        WorkingCollection.Add(TempGridRow)
                        Base_Number = Individual_Faces(J) - 1
                        Name_Added = True
                    Else
                        TempGridRow = CloneRow.Clone()
                        TempGridRow.Cells(0).Value = ""
                        TempGridRow.Cells(1).Value = Base_Number
                        TempGridRow.Cells(2).Value = (Individual_Faces(J - 1) - 1)
                        WorkingCollection.Add(TempGridRow)
                        Base_Number = Individual_Faces(J) - 1
                    End If
                End If
            Next
            If Name_Added = False Then
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = Temp_Name
                TempGridRow.Cells(1).Value = Base_Number
                TempGridRow.Cells(2).Value = (Individual_Faces(Individual_Faces.Count - 1) - 1)
                WorkingCollection.Add(TempGridRow)
                Name_Added = True
            Else
                TempGridRow = CloneRow.Clone()
                TempGridRow.Cells(0).Value = ""
                TempGridRow.Cells(1).Value = Base_Number
                TempGridRow.Cells(2).Value = (Individual_Faces(Individual_Faces.Count - 1) - 1)
                WorkingCollection.Add(TempGridRow)
            End If
        Next
        DataGridMaskView.Rows.AddRange(WorkingCollection.ToArray())
        SaveChangesMaskMenuItem.Visible = True
    End Sub

    Private Sub ImportMasksFromTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportMasksFromTXTToolStripMenuItem.Click
        Dim OpenTxtFile As OpenFileDialog = New OpenFileDialog With {
            .FileName = "CE_mask_list.txt",
            .Filter = "Text File|*.txt|All files (*.*)|*.*"}
        If OpenTxtFile.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If File.Exists(OpenTxtFile.FileName) Then
                FillMaskDataGrid(OpenTxtFile.FileName)
            End If
        End If
    End Sub

    Private Sub ExportMaskstoTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMaskstoTXTToolStripMenuItem.Click
        Dim SaveTxtFile As SaveFileDialog = New SaveFileDialog With {
            .FileName = "CE_mask_list.txt",
            .Filter = "Text File|*.txt|All files (*.*)|*.*"}
        If DataGridMaskView.SelectedRows.Count = 0 Then
            If SaveTxtFile.ShowDialog = DialogResult.OK Then
                Dim CurrentBody As String = "New"
                Dim FaceArray As List(Of Integer) = New List(Of Integer)
                Using sw As New IO.StreamWriter(SaveTxtFile.FileName)
                    For i As Integer = 0 To DataGridMaskView.RowCount - 1
                        If DataGridMaskView.Rows(i).Cells(0).Value = "" Then 'Just some mask numbers
                            For j As Integer = CInt(DataGridMaskView.Rows(i).Cells(1).Value) To CInt(DataGridMaskView.Rows(i).Cells(2).Value)
                                FaceArray.Add(j + 1)
                            Next
                        Else 'New Body Part
                            If CurrentBody <> "New" Then
                                'We Have to Write the existing Mask to the File
                                sw.WriteLine(CurrentBody)
                                FaceArray.Sort()
                                sw.WriteLine(String.Join(",", FaceArray))
                            End If
                            If Not DataGridMaskView.Rows(i).Cells(1).Value.ToString = "nil" Then
                                FaceArray = New List(Of Integer)
                                For j As Integer = CInt(DataGridMaskView.Rows(i).Cells(1).Value) To CInt(DataGridMaskView.Rows(i).Cells(2).Value)
                                    FaceArray.Add(j + 1)
                                Next
                                If DataGridMaskView.Rows(i).Cells(0).Value.contains("Body") Then
                                    CurrentBody = "Object" & (CInt(DataGridMaskView.Rows(i).Cells(0).Value.ToString().Substring(7, 1)) + 1).ToString
                                Else
                                    CurrentBody = "Object" & (CInt(DataGridMaskView.Rows(i).Cells(0).Value.ToString().Substring(7, 1))).ToString
                                End If
                            End If
                        End If
                    Next
                End Using
                MessageBox.Show("File Saved")
            End If
        Else
            If SaveTxtFile.ShowDialog = DialogResult.OK Then
                Dim FaceArray As List(Of Integer) = New List(Of Integer)
                For Each temprow As DataGridViewRow In DataGridMaskView.SelectedRows
                    For i As Integer = CInt(temprow.Cells(1).Value) To CInt(temprow.Cells(2).Value)
                        FaceArray.Add(i + 1)
                    Next
                Next
                'sorts all the faces before putting it in the array
                FaceArray.Sort()
                '3ds max script provided by tekken
                Using sw As New IO.StreamWriter(SaveTxtFile.FileName)
                    sw.WriteLine(String.Join(",", FaceArray))
                End Using
                MessageBox.Show("File Saved")
            End If
        End If
    End Sub

#Region "Save Mask Functions"

    Private Sub SaveMaskChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesMaskMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildMaskFile())
        SaveChangesMaskMenuItem.Visible = False
    End Sub

    'actively used when making a save
    Dim CurrentDataGridRow As Integer = 0

    Dim CurrentContainer As String = ""
    Dim ContainerByteArray As Byte() = New Byte() {}
    Dim ContainerCount As Integer = 0
    Dim CurrentBodyPart As Integer = -1
    Dim BodyPartArray As Byte() = New Byte() {}
    Dim BodyPartCount As Integer = 0
    Dim CurrentMask As Integer = -1
    Dim SingleMaskByteArray As Byte() = New Byte() {}
    Dim SingleArrayCount As Integer = 0
    Dim NumberByteArray As Byte() = New Byte() {}

    Private Function BuildMaskFile() As Byte()
        For Each row As DataGridViewRow In DataGridMaskView.Rows
            If row.Cells(1).Value.ToString = "nil" OrElse row.Cells(2).Value.ToString = "nil" Then
                MessageBox.Show("Please delete any nil rows before saving")
            End If
        Next
        Dim total_file As Byte() = New Byte() {}
        CurrentDataGridRow = 1
        ' row 0 must be manually processed as the starting point
        Dim SplitRowText As String() = DataGridMaskView(0, 0).Value.ToString().Split("_")
        CurrentContainer = SplitRowText(0) & "_" & SplitRowText(1)
        ContainerByteArray = New Byte(0) {}
        ContainerCount = 0
        CurrentBodyPart = CInt(SplitRowText(2))
        BodyPartArray = New Byte(0) {}
        BodyPartCount = 0
        CurrentMask = CInt(SplitRowText(3))
        SingleMaskByteArray = New Byte(0) {}
        SingleArrayCount = 0
        NumberByteArray = New Byte(7) {}
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, 0).Value)), 0, NumberByteArray, 0, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, 0).Value)), 0, NumberByteArray, 4, 4)
        Do While CurrentDataGridRow < DataGridMaskView.RowCount
            If DataGridMaskView(0, CurrentDataGridRow).Value.ToString.Length = 0 Then
                'MessageBox.Show("Adding")
                AddMoreNumbersToByteArray()
            Else
                SplitRowText = DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")
                If Not CurrentContainer = SplitRowText(0) & "_" & SplitRowText(1) Then
                    'putting the number arrays into a mask container
                    FormMaskByteArray()
                    'Make a new part together and adding it to any other parts
                    FormBodyByteArray()
                    'Now putting the container together
                    FormContainerByteArray()
                Else
                    If Not CurrentBodyPart = CInt(SplitRowText(2)) Then
                        'putting the number arrays into a mask container
                        FormMaskByteArray()
                        'Make a new part together and adding it to any other parts
                        FormBodyByteArray()
                    Else
                        If Not CurrentMask = CInt(SplitRowText(3)) Then
                            'putting the number arrays into a mask container
                            FormMaskByteArray()
                        Else
                            'This shouldn't happen, but this means it's the same part and it should just add the numbers
                            AddMoreNumbersToByteArray()
                        End If
                    End If
                End If
            End If
            CurrentDataGridRow += 1
        Loop
        'finish last package at this point
        'putting the number arrays into a mask container
        FormMaskByteArray()
        'Make a new part together and adding it to any other parts
        FormBodyByteArray()
        'Now putting the container together
        FormContainerByteArray()
        'Add the final header and then add containers to file
        total_file = New Byte(ContainerByteArray.Length + &HC - 2) {}
        'adding ceader length
        total_file(&H4) = &HC
        'adding container count
        total_file(&H8) = ContainerCount
        'copying final containers to array before writing to file
        Buffer.BlockCopy(ContainerByteArray, 0, total_file, &HC, ContainerByteArray.Length - 1)
        Return total_file
    End Function

    Sub AddMoreNumbersToByteArray()
        Dim old_length As Integer = NumberByteArray.Length
        ReDim Preserve NumberByteArray(old_length + 7)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, CurrentDataGridRow).Value)), 0, NumberByteArray, old_length, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, CurrentDataGridRow).Value)), 0, NumberByteArray, old_length + 4, 4)
    End Sub

    Sub MakeNewNumberByteArray()
        ReDim NumberByteArray(7)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(1, CurrentDataGridRow).Value)), 0, NumberByteArray, 0, 4)
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(DataGridMaskView(2, CurrentDataGridRow).Value)), 0, NumberByteArray, 4, 4)
    End Sub

    Private Sub FormMaskByteArray()
        Dim old_length As Integer = SingleMaskByteArray.Length - 1
        ReDim Preserve SingleMaskByteArray((SingleMaskByteArray.Length) + (NumberByteArray.Length) + &H10 - 1)
        'length of header
        SingleMaskByteArray(old_length) = &H10
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt((NumberByteArray.Length) + &H10)), 0, SingleMaskByteArray, old_length + &H4, 4)
        'putting the mask number which should be 0
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(CurrentMask)), 0, SingleMaskByteArray, old_length + &H8, 4)
        'getting the count of numbers
        Buffer.BlockCopy(BitConverter.GetBytes(CInt((NumberByteArray.Length - 1) / 8)), 0, SingleMaskByteArray, old_length + &HC, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(NumberByteArray, 0, SingleMaskByteArray, old_length + &H10, NumberByteArray.Length)
        SingleArrayCount += 1
        ' MessageBox.Show(DataGridView1(0, current_row).Value.ToString() & vbNewLine &
        'DataGridView1(0, current_row).Value.ToString().Length)
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentMask = CInt(DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")(3))
            MakeNewNumberByteArray()
        End If
        'Make New Number Array for new mask
    End Sub

    Sub MakeNewMaskByteArray()
        SingleMaskByteArray = New Byte(0) {}
    End Sub

    Sub FormBodyByteArray()
        Dim old_length As Integer = BodyPartArray.Length - 1
        ReDim Preserve BodyPartArray(BodyPartArray.Length + SingleMaskByteArray.Length + &H10 - 2)
        'length of header
        BodyPartArray(old_length) = &H10
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(SingleMaskByteArray.Length + &H10 - 1)), 0, BodyPartArray, old_length + &H4, 4)
        'putting the body part number
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(CurrentBodyPart)), 0, BodyPartArray, old_length + &H8, 4)
        'getting the count of masks
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(SingleArrayCount)), 0, BodyPartArray, old_length + &HC, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(SingleMaskByteArray, 0, BodyPartArray, old_length + &H10, SingleMaskByteArray.Length)
        '
        BodyPartCount += 1
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentBodyPart = CInt(DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Split("_")(2))
        End If
        MakeNewMaskByteArray()
        SingleArrayCount = 0
    End Sub

    Sub MakeNewBodyPartByteArray()
        BodyPartArray = New Byte(0) {}
    End Sub

    Sub FormContainerByteArray()
        Dim old_length As Integer = ContainerByteArray.Length - 1
        ReDim Preserve ContainerByteArray(ContainerByteArray.Length + BodyPartArray.Length + &H4C - 2)
        'length of header
        ContainerByteArray(old_length) = &H4C
        'length of total plus header
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(BodyPartArray.Length + &H4C - 1)), 0, ContainerByteArray, old_length + &H4, 4)
        'putting in the container name
        Buffer.BlockCopy(System.Text.Encoding.ASCII.GetBytes(CurrentContainer), 0, ContainerByteArray, old_length + &H8, System.Text.Encoding.ASCII.GetBytes(CurrentContainer).Length)
        'adding the count of body parts
        Buffer.BlockCopy(BitConverter.GetBytes(CInt(BodyPartCount)), 0, ContainerByteArray, old_length + &H48, 4)
        'copying the number array into the mask
        Buffer.BlockCopy(BodyPartArray, 0, ContainerByteArray, old_length + &H4C, BodyPartArray.Length)
        '
        ContainerCount += 1
        If CurrentDataGridRow < DataGridMaskView.RowCount Then
            CurrentContainer = DataGridMaskView(0, CurrentDataGridRow).Value.ToString().Substring(0, 6)
        End If
        MakeNewBodyPartByteArray()
        BodyPartCount = 0
    End Sub

#End Region

#Region "Tutorial Links"

    Private Sub TutorialVideoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TutorialVideoToolStripMenuItem.Click
        Try
            Process.Start("https://www.youtube.com/watch?v=e7WNIh-WYB4")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DSImportScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSImportScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3FYJ")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DSSelectionScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSSelectionScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3FZt")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DSExportScriptToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DSExportScriptToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/3Fd2")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub AznTutorialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AznTutorialToolStripMenuItem.Click
        Try
            Process.Start("http://velociterium.com/4t4q")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

#End Region

#Region "DataGridView Actions"

    Private Sub DataGridMaskView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            If e.ColumnIndex = 3 Then
                'add row of mask
                Dim temprow As DataGridViewRow = DataGridMaskView.Rows(e.RowIndex).Clone()
                temprow.Cells(0).Value = ""
                temprow.Cells(1).Value = DataGridMaskView.Rows(e.RowIndex).Cells(1).Value
                temprow.Cells(2).Value = DataGridMaskView.Rows(e.RowIndex).Cells(2).Value
                DataGridMaskView.Rows.Insert(e.RowIndex + 1, temprow)
                SavePending = True
                SaveChangesMaskMenuItem.Visible = True
            ElseIf e.ColumnIndex = 4 Then
                'delete row of mask
                DataGridMaskView.Rows.Remove(DataGridMaskView.Rows(e.RowIndex))
                SavePending = True
                SaveChangesMaskMenuItem.Visible = True
            Else
                'do nothing because a not button column at this time.
            End If
        End If
    End Sub

    Private Sub DataGridMaskView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMaskView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMaskView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Dim NewValue As String = MyCell.Value
        If e.ColumnIndex = 0 Then 'Mask Name must be of a format M_Head_x_x
            Dim SplitUpString As String() = NewValue.Split("_")
            If SplitUpString.Count = 4 AndAlso
               SplitUpString(0) = "M" Then
                If SplitUpString(1) = "Head" OrElse
                   SplitUpString(1) = "Body" Then
                    If Not IsNumeric(SplitUpString(2)) Then
                        MyCell.Value = OldValue
                    Else
                        If Not IsNumeric(SplitUpString(3)) Then
                            MyCell.Value = OldValue
                        End If
                    End If
                Else
                    MyCell.Value = OldValue
                End If
            Else
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 1 Then 'Start Face
            If Not IsNumeric(NewValue) Then
                MyCell.Value = OldValue
            ElseIf CInt(NewValue) < 0 Then
                MyCell.Value = OldValue
            Else
                Dim EndFace As String = DataGridMaskView.Rows(e.RowIndex).Cells(2).Value
                If Not IsNumeric(EndFace) Then ' End Face is nil and we need to make it a number
                    DataGridMaskView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value
                ElseIf CInt(NewValue) > CInt(EndFace) Then 'if we are increasing the start face we need to increas the end face
                    DataGridMaskView.Rows(e.RowIndex).Cells(2).Value = MyCell.Value
                End If
            End If
        ElseIf e.ColumnIndex = 2 Then 'End Face
            If Not IsNumeric(NewValue) Then
                MyCell.Value = OldValue
            ElseIf CInt(NewValue) < 0 Then
                MyCell.Value = OldValue
            Else
                Dim StartFace As String = DataGridMaskView.Rows(e.RowIndex).Cells(1).Value
                If Not IsNumeric(StartFace) Then ' End Face is nil and we need to make it a number
                    DataGridMaskView.Rows(e.RowIndex).Cells(1).Value = MyCell.Value
                ElseIf CInt(NewValue) < CInt(StartFace) Then 'if we are decreasing the end face we need to decrease the start face
                    DataGridMaskView.Rows(e.RowIndex).Cells(1).Value = MyCell.Value
                End If
            End If
        End If
        SavePending = True
        SaveChangesMaskMenuItem.Visible = True
    End Sub


#End Region
End Class