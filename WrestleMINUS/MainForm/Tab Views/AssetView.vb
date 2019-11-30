Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Public Class AssetFileInformation
        Public PacNumber As UInt32 = 0
        Public AttireNum As UInt32 = 0
        Public AudioNum As UInt32 = 0
        Public Check2 As UInt32 = 0
        Public MusicOffset As UInt32 = 0
        Public EVTOffset As UInt32 = 0
        Public MusicNum As UInt32 = 0
        Public TitantronNum As UInt32 = 0
        Public HeaderNum As UInt32 = 0
        Public WallNum As UInt32 = 0
        Public RampNum As UInt32 = 0
        Public WallRightNum As UInt32 = 0
        Public WallLeftNum As UInt32 = 0
        Public RawTronEnabled As Boolean = False
        Public SDTronEnabled As Boolean = False
        Public ClassicTronEnable As Boolean = False
        Public Check5 As UInt32 = 0
        Public Check6 As UInt32 = 0
    End Class

    Sub FillAssetFileView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAssetView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim AssetConvBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim AssetCount As Integer = BitConverter.ToInt32(AssetConvBytes, &HC)
        ProgressBar1.Maximum = AssetCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To AssetCount - 1
            Dim TempByteArray As Byte() = New Byte(&H40) {}
            Array.Copy(AssetConvBytes, &H18 + i * &H40, TempByteArray, 0, &H40)
            Dim TempAssetFile As AssetFileInformation = ParseBytesToAssetFileInformation(TempByteArray)
            Dim MUSFileName As String = ""
            If TempAssetFile.MusicOffset > 0 Then
                If Not TempAssetFile.MusicOffset = UInt32.MaxValue Then
                    Try
                        MUSFileName = Encoding.Default.GetString(AssetConvBytes, TempAssetFile.MusicOffset, &H11)
                        MUSFileName = MUSFileName.Substring(0, MUSFileName.IndexOf(Nothing))
                    Catch ex As Exception
                        MUSFileName = "ERROR"
                    End Try
                End If
            End If
            Dim EVTFileName As String = ""
            If TempAssetFile.EVTOffset > 0 Then
                If Not TempAssetFile.EVTOffset = UInt32.MaxValue Then
                    Try
                        'TO DO Fix name length inconsistencies
                        EVTFileName = Encoding.Default.GetString(AssetConvBytes, TempAssetFile.EVTOffset, &H11)
                        EVTFileName = EVTFileName.Substring(0, EVTFileName.IndexOf(Nothing))
                    Catch ex As Exception
                        EVTFileName = "ERROR"
                    End Try
                End If
            End If
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = TempAssetFile.PacNumber
            TempGridRow.Cells(1).Value = TempAssetFile.AttireNum
            TempGridRow.Cells(2).Value = TempAssetFile.AudioNum
            TempGridRow.Cells(3).Value = TempAssetFile.Check2
            TempGridRow.Cells(4).Value = TempAssetFile.MusicOffset
            TempGridRow.Cells(5).Value = TempAssetFile.EVTOffset
            TempGridRow.Cells(6).Value = TempAssetFile.MusicNum
            TempGridRow.Cells(7).Value = TempAssetFile.TitantronNum
            TempGridRow.Cells(8).Value = TempAssetFile.HeaderNum
            TempGridRow.Cells(9).Value = TempAssetFile.WallNum
            TempGridRow.Cells(10).Value = TempAssetFile.RampNum
            TempGridRow.Cells(11).Value = TempAssetFile.WallRightNum
            TempGridRow.Cells(12).Value = TempAssetFile.WallLeftNum
            TempGridRow.Cells(13).Value = TempAssetFile.RawTronEnabled
            TempGridRow.Cells(14).Value = TempAssetFile.SDTronEnabled
            TempGridRow.Cells(15).Value = TempAssetFile.ClassicTronEnable
            TempGridRow.Cells(16).Value = TempAssetFile.Check5
            TempGridRow.Cells(17).Value = TempAssetFile.Check6
            TempGridRow.Cells(18).Value = MUSFileName
            TempGridRow.Cells(19).Value = EVTFileName
            TempGridRow.HeaderCell.Value = i.ToString
            TempGridRow.Tag = TempAssetFile
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridAssetView.Rows.AddRange(WorkingCollection.ToArray)
    End Sub

    Function ParseBytesToAssetFileInformation(TestedByteArray As Byte()) As AssetFileInformation
        Return New AssetFileInformation With {
            .PacNumber = BitConverter.ToUInt32(TestedByteArray, 0),
            .AttireNum = BitConverter.ToUInt32(TestedByteArray, 4),
            .AudioNum = BitConverter.ToUInt32(TestedByteArray, 8),
            .Check2 = BitConverter.ToUInt32(TestedByteArray, 12),
            .MusicOffset = BitConverter.ToUInt32(TestedByteArray, 16),
            .EVTOffset = BitConverter.ToUInt32(TestedByteArray, 20),
            .MusicNum = BitConverter.ToUInt32(TestedByteArray, 24),
            .TitantronNum = BitConverter.ToUInt32(TestedByteArray, 28),
            .HeaderNum = BitConverter.ToUInt32(TestedByteArray, 32),
            .WallNum = BitConverter.ToUInt32(TestedByteArray, 36),
            .RampNum = BitConverter.ToUInt32(TestedByteArray, 40),
            .WallRightNum = BitConverter.ToUInt32(TestedByteArray, 44),
            .WallLeftNum = BitConverter.ToUInt32(TestedByteArray, 48),
            .RawTronEnabled = BitConverter.ToBoolean(TestedByteArray, 52),
            .SDTronEnabled = BitConverter.ToBoolean(TestedByteArray, 53),
            .ClassicTronEnable = BitConverter.ToBoolean(TestedByteArray, 54),
            .Check5 = BitConverter.ToUInt32(TestedByteArray, 56),
            .Check6 = BitConverter.ToUInt32(TestedByteArray, 60)}
    End Function

    Function GetBytesFromAssetFileDataGridRow(RequestedByteRow As DataGridViewRow) As Byte()
        Dim ReturnedBytes As Byte() = New Byte(&H40) {}
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(0).Value)), 0, ReturnedBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(1).Value)), 0, ReturnedBytes, 4, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(2).Value)), 0, ReturnedBytes, 8, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(3).Value)), 0, ReturnedBytes, 12, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(4).Value)), 0, ReturnedBytes, 16, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(5).Value)), 0, ReturnedBytes, 20, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(6).Value)), 0, ReturnedBytes, 24, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(7).Value)), 0, ReturnedBytes, 28, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(8).Value)), 0, ReturnedBytes, 32, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(9).Value)), 0, ReturnedBytes, 36, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(10).Value)), 0, ReturnedBytes, 40, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(11).Value)), 0, ReturnedBytes, 44, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(12).Value)), 0, ReturnedBytes, 48, 4)
        Array.Copy(BitConverter.GetBytes(CBool(RequestedByteRow.Cells(13).Value)), 0, ReturnedBytes, 52, 1)
        Array.Copy(BitConverter.GetBytes(CBool(RequestedByteRow.Cells(14).Value)), 0, ReturnedBytes, 53, 1)
        Array.Copy(BitConverter.GetBytes(CBool(RequestedByteRow.Cells(15).Value)), 0, ReturnedBytes, 54, 1)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(16).Value)), 0, ReturnedBytes, 56, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(RequestedByteRow.Cells(17).Value)), 0, ReturnedBytes, 60, 4)
        Return ReturnedBytes
    End Function

    Function AdjustFileNameOffsets() As Integer
        Dim CurrentIndex As UInt32 = &H18 + &H38 + (DataGridAssetView.Rows.Count) * &H40
        For i As Integer = 0 To DataGridAssetView.Rows.Count - 1
            'Updating Music File Information offsets first
            If Not DataGridAssetView.Rows(i).Cells(16).Value = "" Then
                DataGridAssetView.Rows(i).Cells(4).Value = CurrentIndex
                CurrentIndex += DataGridAssetView.Rows(i).Cells(16).Value.ToString.Length + 1
            End If
        Next
        For i As Integer = 0 To DataGridAssetView.Rows.Count - 1
            'Updating evt File Information offsets first
            If Not DataGridAssetView.Rows(i).Cells(17).Value = "" Then
                DataGridAssetView.Rows(i).Cells(5).Value = CurrentIndex
                CurrentIndex += DataGridAssetView.Rows(i).Cells(17).Value.ToString.Length + 1
            End If
        Next
        Return (CurrentIndex - (CurrentIndex Mod &H100) + &H100)
    End Function

    Private Sub DataGridAssetView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAssetView.CellEndEdit
        Dim StartingStrings As List(Of String) = New List(Of String)
        StartingStrings.AddRange({"ent", "cre", "tag", "MUS"})
        Dim MyCell As DataGridViewCell = DataGridAssetView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If e.ColumnIndex = 16 OrElse e.ColumnIndex = 17 Then
            'A File Name has been Added or Edited
            If MyCell.Value.ToString.Contains("_") Then
                Dim SplitUpString As String() = MyCell.Value.Split("_")
                If SplitUpString.Count > 3 AndAlso
                StartingStrings.Contains(SplitUpString(0)) Then
                    For i As Integer = 1 To SplitUpString.Count - 1
                        If Not IsNumeric(SplitUpString(i)) Then
                            MyCell.Value = OldValue
                        End If
                    Next
                ElseIf MyCell.Value = "" Then
                    'do nothing
                Else
                    MyCell.Value = OldValue
                End If
            Else
                MyCell.Value = OldValue
            End If
        ElseIf e.ColumnIndex = 4 OrElse e.ColumnIndex = 5 Then
            'this can only be edited by the program
        Else
            If Not IsNumeric(MyCell.Value) OrElse
           MyCell.Value < -1 Then
                MyCell.Value = CUInt(OldValue)
            Else
                Dim TempTest As ULong = CULng(MyCell.Value)
                If TempTest < UInt32.MaxValue Then
                    SavePending = True
                    SaveChangesAssetViewMenuItem.Visible = True
                    MyCell.Value = CUInt(MyCell.Value)
                Else
                    MyCell.Value = CUInt(OldValue)
                End If
            End If
        End If
    End Sub

    Private Sub DataGridAssetView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridAssetView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 20 Then 'add button
                'This function adds a duplicate row at index + 1, but index + 1 has to have true index updated as well
                Dim Duplicaterow As DataGridViewRow = DataGridAssetView.Rows(e.RowIndex).Clone
                For i As Integer = 0 To DataGridAssetView.Rows(e.RowIndex).Cells.Count - 1
                    Duplicaterow.Cells(i).Value = DataGridAssetView.Rows(e.RowIndex).Cells(i).Value
                Next
                DataGridAssetView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                SavePending = True
                SaveChangesAssetViewMenuItem.Visible = True
            ElseIf e.ColumnIndex = 21 Then 'Delete button
                DataGridAssetView.Rows.RemoveAt(e.RowIndex)
                SavePending = True
                SaveChangesAssetViewMenuItem.Visible = True
            Else
                'do nothing
            End If
        End If
    End Sub

    Private Sub SaveAssetViewChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesAssetViewMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildAssetArrayFile())
    End Sub

    Private Function BuildAssetArrayFile() As Byte()
        Dim ShowBytes As Byte() = FilePartHandlers.GetFilePartBytes(ReadNode.Tag)
        Dim ReturnedBytes As Byte() = New Byte(AdjustFileNameOffsets() - 1) {}
        Dim AssetCount As UInt32 = 0
        'File Header
        Array.Copy(New Byte() {&H56, &H4D, &H55, &H4D, &H10, &H0, &H0, &H0, &HE8, &H3, &H0, &H0, &H0, &H0, &H0, &H0, &H1, &H0, &H0, &H0, &H82, &H0, &H0, &H0}, 0, ReturnedBytes, 0, &H18)
        Array.Copy(BitConverter.GetBytes(CUInt(DataGridAssetView.Rows.Count)), 0, ReturnedBytes, &HC, 4)
        For i As Integer = 0 To DataGridAssetView.Rows.Count - 1
            Array.Copy(GetBytesFromAssetFileDataGridRow(DataGridAssetView.Rows(i)), 0, ReturnedBytes, &H18 + i * &H40, &H40)
            'Copying Music Names
            If DataGridAssetView.Rows(i).Cells(4).Value > 0 AndAlso DataGridAssetView.Rows(i).Cells(4).Value < (UInt32.MaxValue - 1) Then
                Array.Copy(Encoding.Default.GetBytes(DataGridAssetView.Rows(i).Cells(16).Value), 0,
                           ReturnedBytes, DataGridAssetView.Rows(i).Cells(4).Value,
                           DataGridAssetView.Rows(i).Cells(16).Value.ToString.Length)
            End If
            'Copying EVT Names
            If DataGridAssetView.Rows(i).Cells(5).Value > 0 AndAlso DataGridAssetView.Rows(i).Cells(5).Value < (UInt32.MaxValue - 1) Then
                Array.Copy(Encoding.Default.GetBytes(DataGridAssetView.Rows(i).Cells(17).Value), 0,
                           ReturnedBytes, DataGridAssetView.Rows(i).Cells(5).Value,
                           DataGridAssetView.Rows(i).Cells(17).Value.ToString.Length)
            End If
        Next
        Return ReturnedBytes
    End Function


End Class
