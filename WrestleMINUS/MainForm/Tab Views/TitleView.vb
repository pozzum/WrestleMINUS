Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Public Class TitleInformation
        Public Enabled As UInt32 = 0
        Public PropID As UInt32 = 0
        Public MenuNumber As UInt32 = 0
        Public NameRef1 As UInt32 = 0
        Public NameString1 As String = ""
        Public NameRef2 As UInt32 = 0
        Public NameString2 As String = ""
        Public NameRef3 As UInt32 = 0
        Public NameString3 As String = ""
        Public WWEDefault1 As UInt32 = &H400 'default value for no champ
        Public WWEDefault1Name As String = ""
        Public WWEDefault2 As UInt32 = &H400
        Public WWEDefault2Name As String = ""
        Public UniDefault1 As UInt32 = &H400
        Public UniDefault1Name As String = ""
        Public UniDefault2 As UInt32 = &H400
        Public UniDefault2Name As String = ""
        Public Temp1 As UInt32 = 0
        Public TitleTypeByte As UInt32 = 0
        Public Female As Boolean = False
        Public TagTeam As Boolean = False
        Public Cuiserweight As Boolean = False
        Public UnlockNum As UInt32 = 0
        Public Temp4 As UInt32 = 0
    End Class

    Sub FillTitleFileView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridTitleView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TitleBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim TitleCount As Integer = BitConverter.ToInt32(TitleBytes, &H8)
        ProgressBar1.Maximum = TitleCount - 1
        ProgressBar1.Value = 0
        DataGridTitleView.Rows.Clear()
        'Adjusting Title Game Combo Box
        Dim TitleSize As Integer = (TitleBytes.Length - &H10) / TitleCount
        If TitleSize = 480 Then '480 1E0 2K19 Titles
            TitleGameComboBox.SelectedIndex = 4
        End If
        For i As Integer = 0 To TitleCount - 1
            Dim TempTitleBytes As Byte() = New Byte(TitleSize - 1) {}
            Array.Copy(TitleBytes, &H10 + i * TitleSize, TempTitleBytes, 0, TitleSize)
            Dim TempTitleInformation As TitleInformation = ParseBytesToTitleInformation(TempTitleBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempTitleInformation.Enabled
            TempGridRow.Cells(1).Value = TempTitleInformation.PropID
            TempGridRow.Cells(2).Value = TempTitleInformation.MenuNumber
            TempGridRow.Cells(3).Value = Hex(TempTitleInformation.NameRef1)
            TempGridRow.Cells(4).Value = TempTitleInformation.NameString1
            TempGridRow.Cells(4).Style = ReadOnlyCellStyle
            TempGridRow.Cells(5).Value = Hex(TempTitleInformation.NameRef2)
            TempGridRow.Cells(6).Value = TempTitleInformation.NameString2
            TempGridRow.Cells(6).Style = ReadOnlyCellStyle
            TempGridRow.Cells(7).Value = Hex(TempTitleInformation.NameRef3)
            TempGridRow.Cells(8).Value = TempTitleInformation.NameString3
            TempGridRow.Cells(8).Style = ReadOnlyCellStyle
            TempGridRow.Cells(9).Value = TempTitleInformation.WWEDefault1
            TempGridRow.Cells(10).Value = TempTitleInformation.WWEDefault1Name
            TempGridRow.Cells(10).Style = ReadOnlyCellStyle
            TempGridRow.Cells(11).Value = TempTitleInformation.WWEDefault2
            TempGridRow.Cells(12).Value = TempTitleInformation.WWEDefault2Name
            TempGridRow.Cells(12).Style = ReadOnlyCellStyle
            TempGridRow.Cells(13).Value = TempTitleInformation.UniDefault1
            TempGridRow.Cells(14).Value = TempTitleInformation.UniDefault1Name
            TempGridRow.Cells(14).Style = ReadOnlyCellStyle
            TempGridRow.Cells(15).Value = TempTitleInformation.UniDefault2
            TempGridRow.Cells(16).Value = TempTitleInformation.UniDefault2Name
            TempGridRow.Cells(16).Style = ReadOnlyCellStyle
            TempGridRow.Cells(17).Value = TempTitleInformation.Temp1
            TempGridRow.Cells(18).Value = TempTitleInformation.TitleTypeByte
            TempGridRow.Cells(19).Value = TempTitleInformation.Female
            TempGridRow.Cells(20).Value = TempTitleInformation.TagTeam
            TempGridRow.Cells(21).Value = TempTitleInformation.Cuiserweight
            TempGridRow.Cells(22).Value = TempTitleInformation.UnlockNum
            TempGridRow.Cells(23).Value = TempTitleInformation.Temp4
            TempGridRow.HeaderCell.Value = i.ToString
            TempGridRow.Tag = TempTitleInformation
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridTitleView.Rows.AddRange(WorkingCollection.ToArray)
        If StringRead Then 'True
            If PacsRead Then 'Strings and Pacs Read
                'Show String
                DataGridTitleView.Columns(4).Visible = True
                DataGridTitleView.Columns(6).Visible = True
                DataGridTitleView.Columns(8).Visible = True
                'Show Wrestler Names
                DataGridTitleView.Columns(10).Visible = True
                DataGridTitleView.Columns(12).Visible = True
                DataGridTitleView.Columns(14).Visible = True
                DataGridTitleView.Columns(16).Visible = True
            Else 'Strings Read Only
                'Show String
                DataGridTitleView.Columns(4).Visible = True
                DataGridTitleView.Columns(6).Visible = True
                DataGridTitleView.Columns(8).Visible = True
                'Hide Wrestler Names
                DataGridTitleView.Columns(10).Visible = False
                DataGridTitleView.Columns(12).Visible = False
                DataGridTitleView.Columns(14).Visible = False
                DataGridTitleView.Columns(16).Visible = False
            End If
        Else 'Pacs Read Only can't do anything so we don't check it
            'Hide String
            DataGridTitleView.Columns(4).Visible = False
            DataGridTitleView.Columns(6).Visible = False
            DataGridTitleView.Columns(8).Visible = False
            'Hide Wrestler Names
            DataGridTitleView.Columns(10).Visible = False
            DataGridTitleView.Columns(12).Visible = False
            DataGridTitleView.Columns(14).Visible = False
            DataGridTitleView.Columns(16).Visible = False
        End If
        'Adding titles won't do anything until we figure out other information
    End Sub

    Function ParseBytesToTitleInformation(TestedByteArray As Byte()) As TitleInformation
        Dim ReturnedTitleInfo As TitleInformation = New TitleInformation With {
           .Enabled = BitConverter.ToUInt32(TestedByteArray, 0),
           .PropID = BitConverter.ToUInt32(TestedByteArray, 4),
           .MenuNumber = BitConverter.ToUInt32(TestedByteArray, 8),
           .NameRef1 = BitConverter.ToUInt32(TestedByteArray, &HC),
           .NameRef2 = BitConverter.ToUInt32(TestedByteArray, &H10),
           .NameRef3 = BitConverter.ToUInt32(TestedByteArray, &H14)}
        If TestedByteArray.Length = 480 Then '480 1E0 2K19 Titles
            '&h180 00 bytes
            '&h18 FF bytes
            '&h10 00 bytes
            ReturnedTitleInfo.WWEDefault1 = BitConverter.ToUInt32(TestedByteArray, &H1C0)
            ReturnedTitleInfo.WWEDefault2 = BitConverter.ToUInt32(TestedByteArray, &H1C4)
            ReturnedTitleInfo.UniDefault1 = BitConverter.ToUInt32(TestedByteArray, &H1C8)
            ReturnedTitleInfo.UniDefault2 = BitConverter.ToUInt32(TestedByteArray, &H1CC)
            ReturnedTitleInfo.Temp1 = BitConverter.ToUInt32(TestedByteArray, &H1D0)
            ReturnedTitleInfo.TitleTypeByte = BitConverter.ToUInt32(TestedByteArray, &H1D4) 'contains Gender and tag information
            ReturnedTitleInfo.UnlockNum = BitConverter.ToUInt32(TestedByteArray, &H1D8)
            ReturnedTitleInfo.Temp4 = BitConverter.ToUInt32(TestedByteArray, &H1DC)
            If StringRead Then
                ReturnedTitleInfo.NameString1 = StringReferences(ReturnedTitleInfo.NameRef1)
                ReturnedTitleInfo.NameString2 = StringReferences(ReturnedTitleInfo.NameRef2)
                ReturnedTitleInfo.NameString3 = StringReferences(ReturnedTitleInfo.NameRef3)
                If PacsRead Then
                    ReturnedTitleInfo.WWEDefault1Name = StringReferences(PacNumbers(ReturnedTitleInfo.WWEDefault1))
                    ReturnedTitleInfo.WWEDefault2Name = StringReferences(PacNumbers(ReturnedTitleInfo.WWEDefault2))
                    ReturnedTitleInfo.UniDefault1Name = StringReferences(PacNumbers(ReturnedTitleInfo.UniDefault1))
                    ReturnedTitleInfo.UniDefault2Name = StringReferences(PacNumbers(ReturnedTitleInfo.UniDefault2))
                End If
            End If
        End If
        'Read TitleTypeByte and translate to Title SEttings
        Dim TitleType As Integer = ReturnedTitleInfo.TitleTypeByte Mod 8
        If TitleType >= 4 Then
            ReturnedTitleInfo.Cuiserweight = True
        End If
        TitleType = TitleType Mod 4
        If TitleType >= 2 Then
            ReturnedTitleInfo.TagTeam = True
        End If
        TitleType = TitleType Mod 2
        If TitleType >= 1 Then
            ReturnedTitleInfo.Female = True
        End If
        Return ReturnedTitleInfo
    End Function

    Function GetBytesFromTitleInformationDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        If TitleGameComboBox.SelectedIndex = 4 Then
            Dim ReturnedBytes As Byte() = New Byte(&H1E0 - 1) {}
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, 4, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(2).Value)), 0, ReturnedBytes, 8, 4)
            Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, &HC, 4)
            Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, &H10, 4)
            Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, &H14, 4)
            Array.Copy(New Byte() {
                       &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF,
                       &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF,
                       &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF}, 0, ReturnedBytes, &H198, &H18)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(9).Value)), 0, ReturnedBytes, &H1C0, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(11).Value)), 0, ReturnedBytes, &H1C4, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(13).Value)), 0, ReturnedBytes, &H1C8, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(15).Value)), 0, ReturnedBytes, &H1CC, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(17).Value)), 0, ReturnedBytes, &H1D0, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(18).Value)), 0, ReturnedBytes, &H1D4, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(22).Value)), 0, ReturnedBytes, &H1D8, 4)
            Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(23).Value)), 0, ReturnedBytes, &H1DC, 4)
            Return ReturnedBytes
        Else
            MessageBox.Show("Build Failure")
            Return Nothing
        End If
    End Function

    Private Sub DataGridTitleView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridTitleView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex < 3 Then
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            Else
                If e.ColumnIndex = 0 Then
                    'Enabled Make sure between 0 & 6 inclusive
                    If CInt(MyCell.Value) = 0 OrElse
                    CInt(MyCell.Value) > 6 Then
                        MyCell.Value = OldValue
                    End If
                ElseIf e.ColumnIndex = 1 Then
                    'Prop Number, Verify Number <= 9999
                    If CInt(MyCell.Value) < 0 OrElse
                    CInt(MyCell.Value) > 9999 Then
                        MyCell.Value = OldValue
                    End If
                ElseIf e.ColumnIndex = 2 Then
                    'Menu Number, Verify Number <= 1024
                    If CInt(MyCell.Value) < 0 OrElse
                    CInt(MyCell.Value) > 1024 Then
                        MyCell.Value = OldValue
                    End If
                End If
            End If
        ElseIf e.ColumnIndex < 9 Then
            'String Reference Information
            If Not HexaDecimalHandlers.HexCheck(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CUInt("&H" & MyCell.Value) > UInt32.MaxValue Then
                MyCell.Value = OldValue
            Else
                If StringReferences.ContainsKey(CUInt("&H" & MyCell.Value)) Then
                    DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(CUInt("&H" & MyCell.Value))
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = ""
                End If
            End If
        ElseIf e.ColumnIndex > 8 AndAlso e.ColumnIndex < 17 Then
            'Wrestler reference Number Make sure between 0 & 1024 inclusive
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
                   CInt(MyCell.Value) > 1024 Then
                MyCell.Value = OldValue
            Else
                DataGridTitleView.Rows(e.RowIndex).Cells(e.ColumnIndex + 1).Value = StringReferences(PacNumbers(CUInt(MyCell.Value)))
            End If
        ElseIf e.ColumnIndex < 19 Then
            'Temp1 and Title temp byte
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CInt(MyCell.Value) < 0 OrElse
                   CInt(MyCell.Value) > 9999 Then
                MyCell.Value = OldValue
            ElseIf e.ColumnIndex = 18 Then
                Dim TitleType As Integer = CInt(MyCell.Value) Mod 8
                If TitleType >= 4 Then
                    'Cruiser-weight
                    DataGridTitleView.Rows(e.RowIndex).Cells(21).Value = True
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(21).Value = False
                End If
                TitleType = TitleType Mod 4
                If TitleType >= 2 Then
                    'Tag Title
                    DataGridTitleView.Rows(e.RowIndex).Cells(20).Value = True
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(20).Value = False
                End If
                TitleType = TitleType Mod 2
                If TitleType >= 1 Then
                    'Female Title
                    DataGridTitleView.Rows(e.RowIndex).Cells(19).Value = True
                Else
                    DataGridTitleView.Rows(e.RowIndex).Cells(19).Value = False
                End If
            End If
        ElseIf e.ColumnIndex < 22 Then
            Dim OldTitleType As Integer = CInt(DataGridTitleView.Rows(e.RowIndex).Cells(18).Value)
            If e.ColumnIndex = 19 Then 'Female Check box
                If DataGridTitleView.Rows(e.RowIndex).Cells(19).Value Then
                    If (OldTitleType Mod 2) >= 1 Then
                        'do nothing already Female
                    Else
                        OldTitleType += 1
                    End If
                Else
                    If (OldTitleType Mod 2) >= 1 Then
                        OldTitleType = OldTitleType - 1
                    Else
                        'do nothing already Not Female
                    End If
                End If
            ElseIf e.ColumnIndex = 20 Then 'Tag Team Check box
                If DataGridTitleView.Rows(e.RowIndex).Cells(20).Value Then
                    If (OldTitleType Mod 4) >= 2 Then
                        'do nothing already Tag Team
                    Else
                        OldTitleType += 2
                    End If
                Else
                    If (OldTitleType Mod 4) >= 2 Then
                        OldTitleType = OldTitleType - 2
                    Else
                        'do nothing already Not Tag Team
                    End If
                End If
            ElseIf e.ColumnIndex = 21 Then 'Cruiser-weight Check box
                If DataGridTitleView.Rows(e.RowIndex).Cells(21).Value Then
                    If (OldTitleType Mod 8) >= 4 Then
                        'do nothing already Cruise-wight
                    Else
                        OldTitleType += 4
                    End If
                Else
                    If (OldTitleType Mod 8) >= 4 Then
                        OldTitleType = OldTitleType - 4
                    Else
                        'do nothing already Not Cruise-wight
                    End If
                End If
            End If
            DataGridTitleView.Rows(e.RowIndex).Cells(18).Value = OldTitleType
        ElseIf e.ColumnIndex > 21 Then
            If Not IsNumeric(MyCell.Value) Then
                MyCell.Value = OldValue
            ElseIf CLng(MyCell.Value) < 0 OrElse
                    CLng(MyCell.Value) > UInt32.MaxValue Then
                MyCell.Value = OldValue
            End If
        End If
        SavePending = True
        SaveChangesTitleMenuItem.Visible = True
    End Sub

    Private Sub SaveTitleChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesTitleMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildTitleFile())
    End Sub

    Function BuildTitleFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte() {}
        If TitleGameComboBox.SelectedIndex = 4 Then
            '2K19
            ReturnedBytes = New Byte(&H10 + (DataGridTitleView.RowCount * &H1E0) - 1) {}
            'copy first 10 bytes from existing file
            Array.Copy(ShowBytes, ReturnedBytes, &H10)
            'rewriting for the heck of it
            ReturnedBytes(8) = DataGridTitleView.RowCount
            For i As Integer = 0 To DataGridTitleView.RowCount - 1
                Dim TempBytes As Byte() = GetBytesFromTitleInformationDataGridRow(DataGridTitleView.Rows(i))
                Array.Copy(TempBytes, 0, ReturnedBytes, &H10 + i * TempBytes.Length, TempBytes.Length)
            Next
        End If
        Return ReturnedBytes
    End Function

End Class
