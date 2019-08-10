Public Class FileClassTemplate


#Region "Main Form Objects"
    Dim WithEvents DataGridDUMMYView As DataGridView
    'Make 1 column per information type + add and byte button columns if applicable
    Dim WithEvents SaveChangesDUMMYMenuItem As MenuItem
#End Region
#Region "DUMMY View"

    Public Class DUMMYInformation
        Public DUMMYReadOnly As UInt32 = 0
        Public DUMMYNum1 As UInt32 = 0
        Public DUMMYNum2 As UInt32 = 0
        Public DUMMYStringRef1 As UInt32 = 0
        Public DUMMYStringPrint1 As String = ""
        Public DUMMYWrestlerNumber1 As UInt32 = &H400
        Public DUMMYWrestlerName1 As String = ""
        Public DUMMYBool1 As Boolean = False
        Public DUMMYBool2 As Boolean = False
    End Class

    Dim DUMMYHeaderLength As UInt32 = &H10 'ADJUST
    Dim DUMMYPartSize As Integer = &H1E0 'ADJUST

    Sub FillTitleFileView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = MainForm.ClearandGetClone(DataGridDUMMYView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim TitleBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim TitleCount As Integer = BitConverter.ToInt32(TitleBytes, &H8)
        MainForm.ProgressBar1.Maximum = TitleCount - 1
        MainForm.ProgressBar1.Value = 0
        DataGridDUMMYView.Rows.Clear()
        'Adjusting Title Game Combo Box
        For i As Integer = 0 To TitleCount - 1
            Dim TempDUMMYBytes As Byte() = New Byte(DUMMYPartSize - 1) {}
            Array.Copy(TitleBytes, DUMMYHeaderLength + i * DUMMYPartSize, TempDUMMYBytes, 0, DUMMYPartSize)
            Dim TempDUMMYInformation As DUMMYInformation = ParseBytesToDUMMYInformation(TempDUMMYBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempDUMMYInformation.DUMMYReadOnly
            TempGridRow.Cells(0).Style = MainForm.ReadOnlyCellStyle
            TempGridRow.Cells(1).Value = TempDUMMYInformation.DUMMYNum1
            TempGridRow.Cells(2).Value = TempDUMMYInformation.DUMMYNum2
            TempGridRow.Cells(3).Value = Hex(TempDUMMYInformation.DUMMYStringRef1)
            TempGridRow.Cells(4).Value = TempDUMMYInformation.DUMMYStringPrint1
            TempGridRow.Cells(4).Style = MainForm.ReadOnlyCellStyle
            TempGridRow.Cells(5).Value = TempDUMMYInformation.DUMMYWrestlerNumber1
            TempGridRow.Cells(6).Value = TempDUMMYInformation.DUMMYWrestlerName1
            TempGridRow.Cells(6).Style = MainForm.ReadOnlyCellStyle
            TempGridRow.Cells(7).Value = TempDUMMYInformation.DUMMYBool1
            TempGridRow.Cells(8).Value = TempDUMMYInformation.DUMMYBool2
            'Assumed Add is 9 Delete is 10.  They do not need to be set per row
            TempGridRow.HeaderCell.Value = i.ToString
            TempGridRow.Tag = TempDUMMYInformation
            WorkingCollection.Add(TempGridRow)
            MainForm.ProgressBar1.Value = i
        Next
        DataGridDUMMYView.Rows.AddRange(WorkingCollection.ToArray)
        If MainForm.StringRead Then 'True
            If MainForm.PacsRead Then 'Strings and Pacs Read
                'Show String
                DataGridDUMMYView.Columns(4).Visible = True
                'Show Wrestler Names
                DataGridDUMMYView.Columns(6).Visible = True
            Else 'Strings Read Only
                'Show String
                DataGridDUMMYView.Columns(4).Visible = True
                'Hide Wrestler Names
                DataGridDUMMYView.Columns(6).Visible = False
            End If
        Else 'Pacs Read Only can't do anything so we don't check it
            'Hide String
            DataGridDUMMYView.Columns(4).Visible = True
            'Hide Wrestler Names
            DataGridDUMMYView.Columns(6).Visible = False
        End If
        'Adding titles won't do anything until we figure out other information
    End Sub

    Function ParseBytesToDUMMYInformation(TestedByteArray As Byte()) As DUMMYInformation
        Dim ReturnedDUMMYInfo As DUMMYInformation = New DUMMYInformation With {
           .DUMMYReadOnly = BitConverter.ToUInt32(TestedByteArray, 0),
           .DUMMYNum1 = BitConverter.ToUInt32(TestedByteArray, 4),
           .DUMMYNum2 = BitConverter.ToUInt32(TestedByteArray, 8),
           .DUMMYStringRef1 = BitConverter.ToUInt32(TestedByteArray, &HC),
           .DUMMYWrestlerNumber1 = BitConverter.ToUInt32(TestedByteArray, &H10),
           .DUMMYBool1 = BitConverter.ToUInt32(TestedByteArray, &H14),
           .DUMMYBool2 = BitConverter.ToUInt32(TestedByteArray, &H18)}
        If MainForm.StringRead Then
            ReturnedDUMMYInfo.DUMMYStringPrint1 = MainForm.StringReferences(ReturnedDUMMYInfo.DUMMYStringRef1)
            If MainForm.PacsRead Then
                ReturnedDUMMYInfo.DUMMYWrestlerName1 = MainForm.StringReferences(MainForm.PacNumbers(ReturnedDUMMYInfo.DUMMYWrestlerNumber1))
            End If
        End If
        Return ReturnedDUMMYInfo
    End Function

    Function GetBytesFromDUMMYInformationDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        Dim ReturnedBytes As Byte() = New Byte(DUMMYPartSize - 1) {}
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, 4, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(2).Value)), 0, ReturnedBytes, 8, 4)
        Array.Copy(BitConverter.GetBytes(CUInt("&H" & RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, &HC, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, &H10, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, &H14, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(8).Value)), 0, ReturnedBytes, &H18, 4)
        Return ReturnedBytes
    End Function

    Private Sub SaveChangesDUMMYMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesDUMMYMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(MainForm.ReadNode.Tag, BuildDUMMYFile())
    End Sub

    Function BuildDUMMYFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(MainForm.ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte() {}
        ReturnedBytes = New Byte(DUMMYHeaderLength + (DataGridDUMMYView.RowCount * DUMMYPartSize) - 1) {}
        'copy header bytes from existing file
        Array.Copy(ShowBytes, ReturnedBytes, DUMMYHeaderLength)
        'rewriting file count for header
        ReturnedBytes(8) = DataGridDUMMYView.RowCount
        For i As Integer = 0 To DataGridDUMMYView.RowCount - 1
            Dim TempBytes As Byte() = GetBytesFromDUMMYInformationDataGridRow(DataGridDUMMYView.Rows(i))
            Array.Copy(TempBytes, 0, ReturnedBytes, DUMMYHeaderLength + i * DUMMYPartSize, DUMMYPartSize)
        Next
        Return ReturnedBytes
    End Function

#End Region
End Class
