Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Public Class CreateEntranceInformation
        Public EventID As UInt32 = 0
        Public PacNumber1 As UInt16 = 0
        Public PacName1 As String = ""
        Public PacNumber2 As UInt16 = 0
        Public PacName2 As String = ""
        Public PacNumber3 As UInt16 = 0
        Public PacName3 As String = ""
        Public PacNumber4 As UInt16 = 0
        Public PacName4 As String = ""
        Public PacNumber5 As UInt16 = 0
        Public PacName5 As String = ""
        Public PacDefaultRef As UInt16 = 0
        Public HasPacDefault As Boolean = False
        Public Promo1 As UInt16 = 0
        Public Promo2 As UInt16 = 0
        Public Promo3 As Byte = 0
        Public Promo4 As Byte = 0
        Public BufferBytes As UInt16 = 0
        Public StringReference As UInt32 = 0
        Public StringText As String = ""
        Public Unkown1 As UInt16 = 0
        Public Unkown2 As UInt16 = 0
        Public PacLockedNum As UInt16 = 0
        Public PacExcludedNum As UInt16 = 0
        Public DLCFlagNum As Boolean = 0
    End Class

    Sub FillMenuItemView(SelectedData As TreeNode)
        Dim CAEMenuBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim CAEMenuContainerCount As UInt16 = BitConverter.ToUInt16(CAEMenuBytes, 0)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMenuItemView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = CAEMenuContainerCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To CAEMenuContainerCount - 1
            Dim TempCAEMenuBytes As Byte() = New Byte(&H28 - 1) {}
            Array.Copy(CAEMenuBytes, &H4 + i * &H28, TempCAEMenuBytes, 0, &H28)
            Dim TempCAEMenuInformation As CreateEntranceInformation = ParseBytesToCAEInformation(TempCAEMenuBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempCAEMenuInformation.EventID
            TempGridRow.Cells(1).Value = Hex(TempCAEMenuInformation.StringReference)
            TempGridRow.Cells(2).Value = TempCAEMenuInformation.StringText
            TempGridRow.Cells(2).Style = ReadOnlyCellStyle
            'Pac Num 1
            TempGridRow.Cells(3).Value = TempCAEMenuInformation.PacNumber1
            TempGridRow.Cells(4).Value = TempCAEMenuInformation.PacName1
            TempGridRow.Cells(4).Style = ReadOnlyCellStyle
            TempGridRow.Cells(5).Value = TempCAEMenuInformation.PacNumber2
            TempGridRow.Cells(6).Value = TempCAEMenuInformation.PacName2
            TempGridRow.Cells(6).Style = ReadOnlyCellStyle
            TempGridRow.Cells(7).Value = TempCAEMenuInformation.PacNumber3
            TempGridRow.Cells(8).Value = TempCAEMenuInformation.PacName3
            TempGridRow.Cells(8).Style = ReadOnlyCellStyle
            TempGridRow.Cells(9).Value = TempCAEMenuInformation.PacNumber4
            TempGridRow.Cells(10).Value = TempCAEMenuInformation.PacName4
            TempGridRow.Cells(10).Style = ReadOnlyCellStyle
            TempGridRow.Cells(11).Value = TempCAEMenuInformation.PacNumber5
            TempGridRow.Cells(12).Value = TempCAEMenuInformation.PacName5
            TempGridRow.Cells(12).Style = ReadOnlyCellStyle
            'Has Pac
            TempGridRow.Cells(13).Value = TempCAEMenuInformation.HasPacDefault
            'Add Read Only Style in reverse
            If Not TempCAEMenuInformation.HasPacDefault Then
                TempGridRow.Cells(3).ReadOnly = True
                TempGridRow.Cells(3).Style = ReadOnlyCellStyle
            End If
            For J As Integer = 5 To 11 Step 2
                If TempGridRow.Cells(J - 2).Value = &HFFFF Then
                    TempGridRow.Cells(J).ReadOnly = True
                    TempGridRow.Cells(J).Style = ReadOnlyCellStyle
                End If
            Next
            'Promo Columns
            TempGridRow.Cells(14).Value = TempCAEMenuInformation.Promo1
            TempGridRow.Cells(15).Value = TempCAEMenuInformation.Promo2
            TempGridRow.Cells(16).Value = TempCAEMenuInformation.Promo3
            TempGridRow.Cells(17).Value = TempCAEMenuInformation.Promo4
            'Tests
            TempGridRow.Cells(18).Value = TempCAEMenuInformation.BufferBytes
            TempGridRow.Cells(19).Value = TempCAEMenuInformation.Unkown1
            TempGridRow.Cells(20).Value = TempCAEMenuInformation.Unkown2
            TempGridRow.Cells(21).Value = TempCAEMenuInformation.PacLockedNum
            TempGridRow.Cells(22).Value = TempCAEMenuInformation.PacExcludedNum
            TempGridRow.Cells(23).Value = TempCAEMenuInformation.DLCFlagNum
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridMenuItemView.Rows.AddRange(WorkingCollection.ToArray())
        'Here we want to hide some columns depending on what type of file we are working with.
        If DataGridMenuItemView.Rows(0).Cells(14).Value = &HFFFF Then
            'Not Promo
            For i As Integer = 3 To 13
                DataGridMenuItemView.Columns(i).Visible = True
            Next
            For i As Integer = 14 To 17
                DataGridMenuItemView.Columns(i).Visible = False
            Next
            For i As Integer = 18 To 22
                DataGridMenuItemView.Columns(i).Visible = True
            Next
        Else
            'Here is a promo file
            For i As Integer = 3 To 13
                DataGridMenuItemView.Columns(i).Visible = False
            Next
            For i As Integer = 14 To 17
                DataGridMenuItemView.Columns(i).Visible = True
            Next
            For i As Integer = 18 To 22
                DataGridMenuItemView.Columns(i).Visible = False
            Next
        End If
        'This will hide Columns if Pac Numbers or String Refs if they are not loaded.
        If StringRead Then 'True
            If PacsRead Then 'Strings and Pacs Read
                'Show String
                DataGridMenuItemView.Columns(2).Visible = True
                ''Show Wrestler Names
                DataGridMenuItemView.Columns(4).Visible = True
                DataGridMenuItemView.Columns(6).Visible = True
                DataGridMenuItemView.Columns(8).Visible = True
                DataGridMenuItemView.Columns(10).Visible = True
                DataGridMenuItemView.Columns(12).Visible = True
            Else 'Strings Read Only
                'Show String
                DataGridMenuItemView.Columns(2).Visible = True
                ''Hide Wrestler Names
                DataGridMenuItemView.Columns(4).Visible = False
                DataGridMenuItemView.Columns(6).Visible = False
                DataGridMenuItemView.Columns(8).Visible = False
                DataGridMenuItemView.Columns(10).Visible = False
                DataGridMenuItemView.Columns(12).Visible = False
            End If
        Else 'Pacs Read Only can't do anything so we don't check it
            ''Hide String
            DataGridMenuItemView.Columns(2).Visible = False
            'Hide Wrestler Names
            DataGridMenuItemView.Columns(4).Visible = False
            DataGridMenuItemView.Columns(6).Visible = False
            DataGridMenuItemView.Columns(8).Visible = False
            DataGridMenuItemView.Columns(10).Visible = False
            DataGridMenuItemView.Columns(12).Visible = False
        End If
        DataGridMenuItemView.Columns(18).Visible = False
    End Sub

    Function ParseBytesToCAEInformation(TestedByteArray As Byte()) As CreateEntranceInformation
        Dim ReturnedCAEInfo As CreateEntranceInformation = New CreateEntranceInformation With {
           .EventID = BitConverter.ToUInt32(TestedByteArray, 0),
           .PacNumber1 = BitConverter.ToUInt16(TestedByteArray, 4),
           .PacNumber2 = BitConverter.ToUInt16(TestedByteArray, 6),
           .PacNumber3 = BitConverter.ToUInt16(TestedByteArray, 8),
           .PacNumber4 = BitConverter.ToUInt16(TestedByteArray, &HA),
           .PacNumber5 = BitConverter.ToUInt16(TestedByteArray, &HC),
           .PacDefaultRef = BitConverter.ToUInt16(TestedByteArray, &HE),
           .Promo1 = BitConverter.ToUInt16(TestedByteArray, &H10),
           .Promo2 = BitConverter.ToUInt16(TestedByteArray, &H12),
           .Promo3 = TestedByteArray(&H14),
           .Promo4 = TestedByteArray(&H15),
           .BufferBytes = BitConverter.ToUInt16(TestedByteArray, &H16),
           .StringReference = BitConverter.ToUInt32(TestedByteArray, &H18),
           .Unkown1 = BitConverter.ToUInt16(TestedByteArray, &H1C),
           .Unkown2 = BitConverter.ToUInt16(TestedByteArray, &H1E),
           .PacLockedNum = BitConverter.ToUInt16(TestedByteArray, &H20),
           .PacExcludedNum = BitConverter.ToUInt16(TestedByteArray, &H22),
           .DLCFlagNum = BitConverter.ToBoolean(TestedByteArray, &H24)}
        If ReturnedCAEInfo.PacDefaultRef = 0 Then
            ReturnedCAEInfo.HasPacDefault = True
        Else
            ReturnedCAEInfo.HasPacDefault = False
        End If
        If StringRead Then 'True
            ReturnedCAEInfo.StringText = StringReferences(ReturnedCAEInfo.StringReference)
            If PacsRead Then
                'Strings and Pacs Read
                ReturnedCAEInfo.PacName1 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber1))
                ReturnedCAEInfo.PacName2 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber2))
                ReturnedCAEInfo.PacName3 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber3))
                ReturnedCAEInfo.PacName4 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber4))
                ReturnedCAEInfo.PacName5 = StringReferences(PacNumbers(ReturnedCAEInfo.PacNumber5))
            End If
        End If
        Return ReturnedCAEInfo
    End Function

    Function GetBytesFromCAEMenuInformationDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&H28 - 1) {}
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, &H18, 4)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, 4, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, 6, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, 8, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(9).Value)), 0, ReturnedBytes, &HA, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(11).Value)), 0, ReturnedBytes, &HC, 2)
        If RequestedByteRow.Cells(13).Value Then
            Array.Copy(BitConverter.GetBytes(CUShort(0)), 0, ReturnedBytes, &HE, 2)
        Else
            Array.Copy(BitConverter.GetBytes(CUShort(&HFFFF)), 0, ReturnedBytes, &HE, 2)
        End If
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(14).Value)), 0, ReturnedBytes, &H10, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(15).Value)), 0, ReturnedBytes, &H12, 2)
        ReturnedBytes(&H14) = CByte(RequestedByteRow.Cells(16).Value)
        ReturnedBytes(&H15) = CByte(RequestedByteRow.Cells(17).Value)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(18).Value)), 0, ReturnedBytes, &H16, 2)        '
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(19).Value)), 0, ReturnedBytes, &H1C, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(20).Value)), 0, ReturnedBytes, &H1E, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(21).Value)), 0, ReturnedBytes, &H20, 2)
        Array.Copy(BitConverter.GetBytes(CUShort(RequestedByteRow.Cells(22).Value)), 0, ReturnedBytes, &H22, 2)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(23).Value)), 0, ReturnedBytes, &H24, 4)
        Return ReturnedBytes
    End Function

    Private Sub DataGridMenuItemView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMenuItemView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        Select Case e.ColumnIndex
            Case 0 'Must be an integer < Uin32
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt32.MaxValue Then
                    MyCell.Value = OldValue
                Else MyCell.Value = CInt(MyCell.Value)
                End If
            Case 1 'must be Hex String
                If Not HexaDecimalHandlers.HexCheck(MyCell.Value) Then
                    MyCell.Value = OldValue
                Else
                    If StringRead Then
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(2).Value = StringReferences(CUInt("&H" & MyCell.Value))
                    End If
                End If
            Case 3, 5, 7, 9, 11
                'Pac Numbers
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    If CUShort(MyCell.Value) > &H400 Then
                        MyCell.Value = CUShort(&HFFFF)
                        'Make cell to the right Read Only
                        If Not e.ColumnIndex = 11 Then
                            For i As Integer = e.ColumnIndex + 2 To 11
                                DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Value = CUShort(&HFFFF)
                                DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Style = ReadOnlyCellStyle
                                DataGridMenuItemView.Rows(e.RowIndex).Cells(i).ReadOnly = True
                            Next
                        End If
                    Else
                        MyCell.Value = CUShort(MyCell.Value)
                        If StringRead And PacsRead Then
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(PacNumbers(CUShort(MyCell.Value)))
                        End If
                        'this is now a pac number meaning we need to enable the pac numbers to the right if there is one
                        If Not e.ColumnIndex = 11 Then
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).Value = CUShort(&HFFFF)
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).Style = New DataGridViewCellStyle()
                            DataGridMenuItemView.Rows(e.RowIndex).Cells(e.ColumnIndex + 2).ReadOnly = False
                        End If
                    End If
                End If
            Case 13
                'CheckBox change
                If MyCell.Value Then
                    'Pacs Enabled
                    DataGridMenuItemView.Rows(e.RowIndex).Cells(3).Style = New DataGridViewCellStyle()
                    DataGridMenuItemView.Rows(e.RowIndex).Cells(3).ReadOnly = False
                Else
                    'Pacs Disabled
                    For i As Integer = 3 To 11
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Value = CUShort(&HFFFF)
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Style = ReadOnlyCellStyle
                        DataGridMenuItemView.Rows(e.RowIndex).Cells(i).ReadOnly = True
                    Next
                End If
            Case 14, 15, 19, 20
                'promo uint16 & Unknown uin16
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CUShort(MyCell.Value)
                End If
            Case 16, 17
                'promo bytes
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > &HFF Then
                    MyCell.Value = OldValue
                Else
                    MyCell.Value = CByte(MyCell.Value)
                End If
            Case 18
                'Buffer Bytes
                MyCell.Value = CUShort(&HFFFF)
            Case 19, 20
                'Pac Numbers for lock defaults to 0
                If Not IsNumeric(MyCell.Value) Then
                    MyCell.Value = OldValue
                ElseIf CLng(MyCell.Value) < 0 OrElse
                CLng(MyCell.Value) > UInt16.MaxValue Then
                    MyCell.Value = OldValue
                Else
                    If CUShort(MyCell.Value) > &H400 Then
                        MyCell.Value = CUShort(0)
                    Else
                        MyCell.Value = CUShort(MyCell.Value)
                    End If
                End If
        End Select
        SavePending = True
        SaveChangesCAEMenuItemMenuItem.Visible = True
    End Sub

    Private Sub DataGridMenuItemView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMenuItemView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 24 Then 'add button
                'This function adds a duplicate row at index + 1
                Dim Duplicaterow As DataGridViewRow = DataGridMenuItemView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridMenuItemView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridMenuItemView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridMenuItemView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                SavePending = True
                SaveChangesCAEMenuItemMenuItem.Visible = True
            ElseIf e.ColumnIndex = 25 Then 'Delete button
                DataGridMenuItemView.Rows.RemoveAt(e.RowIndex)
                SavePending = True
                SaveChangesCAEMenuItemMenuItem.Visible = True
            Else
                'do nothing
            End If
        End If
    End Sub

    Private Sub SaveChangesCAEMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesCAEMenuItemMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildMenuItemFile())
    End Sub

    Function BuildMenuItemFile() As Byte()
        Dim CAEMenuBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(&H4 + (DataGridMenuItemView.RowCount * &H28) - 1) {}
        'Write the contained count
        Array.Copy(BitConverter.GetBytes(CUShort(DataGridMenuItemView.RowCount)), 0, ReturnedBytes, 0, 2)
        'now we need to get the second short which might be longer than the actual count
        Dim IncrementAmount As UInt16 = BitConverter.ToUInt16(CAEMenuBytes, 2) - BitConverter.ToUInt16(CAEMenuBytes, 0)
        Array.Copy(BitConverter.GetBytes(CUShort(DataGridMenuItemView.RowCount + IncrementAmount)), 0, ReturnedBytes, 2, 2)
        For i As Integer = 0 To DataGridMenuItemView.RowCount - 1
            Dim TempBytes As Byte() = GetBytesFromCAEMenuInformationDataGridRow(DataGridMenuItemView.Rows(i))
            Array.Copy(TempBytes, 0, ReturnedBytes, &H4 + i * &H28, &H28)
        Next
        Return ReturnedBytes
    End Function

End Class