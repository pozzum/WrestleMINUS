Imports System.IO   'Files
Imports System.Text 'Binary Formatter
Imports Newtonsoft.Json

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Public Class ArenaInformation
        Public Stadium As Integer = -2
        Public Advertisement As Integer = -2
        Public CornerPost As Integer = -2
        Public LED_CornerPost As Integer = -2
        Public Rope As Integer = -2
        Public Apron As Integer = -2
        Public LED_Apron As Integer = -2
        Public Turnbuckle As Integer = -2
        Public Barricade As Integer = -2
        Public Fence As Integer = -2
        Public CeilingLighting As Integer = -2
        Public Spotlight As Integer = -2
        Public Stairs As Integer = -2
        Public CommentarySeat As Integer = -2
        Public RingMat As Integer = -2
        Public FloorMattress As Integer = -2
        Public Crowd As Integer = -2
        Public CrowdSeatsPlace As Integer = -2
        Public CrowdSeatsModel As Integer = -2
        Public IBL As Integer = -2
        Public Titantron As Integer = -2
        Public Minitron As Integer = -2
        Public Wall_L As Integer = -2
        Public Wall_R As Integer = -2
        Public Header As Integer = -2
        Public Floor As Integer = -2
        Public MiscObjects As Integer = -2
        Public LightingType As Integer = -2
        Public CornerPost_CM As Integer = -2
        Public Rope_CM As Integer = -2
        Public Apron_CM As Integer = -2
        Public Turnbuckle_CM As Integer = -2
        Public RingMat_CM As Integer = -2
        Public version As String = "1.0"
    End Class

    Sub FillMiscView(SelectedData As TreeNode)
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridMiscView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        Dim MiscBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim ArenaCount As Integer = BitConverter.ToInt32(MiscBytes, 0)
        ProgressBar1.Maximum = ArenaCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To ArenaCount - 1
            Dim DByteList As List(Of Boolean) = New List(Of Boolean)
            Dim ArenaNum As String = Encoding.ASCII.GetString(MiscBytes, 25 + i * 32, 5)
            Dim TempIndex As Integer = BitConverter.ToInt32(MiscBytes, 40 + i * 32)
            Dim TempLength As Integer = BitConverter.ToInt32(MiscBytes, 36 + i * 32)
            Dim ArenaArray As Byte() = New Byte(TempLength - 1) {}
            Array.Copy(MiscBytes, TempIndex, ArenaArray, 0, TempLength)
            Dim ArenaJson As String = Encoding.ASCII.GetString(ArenaArray)
            Dim TempArena As ArenaInformation = ParseJsonToArena(ArenaJson)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = ArenaNum
            TempGridRow.Cells(1).Value = TempArena.Stadium
            TempGridRow.Cells(2).Value = TempArena.Advertisement
            TempGridRow.Cells(3).Value = TempArena.CornerPost
            TempGridRow.Cells(4).Value = TempArena.LED_CornerPost
            TempGridRow.Cells(5).Value = TempArena.Rope
            TempGridRow.Cells(6).Value = TempArena.Apron
            TempGridRow.Cells(7).Value = TempArena.LED_Apron
            TempGridRow.Cells(8).Value = TempArena.Turnbuckle
            TempGridRow.Cells(9).Value = TempArena.Barricade
            TempGridRow.Cells(10).Value = TempArena.Fence
            TempGridRow.Cells(11).Value = TempArena.CeilingLighting
            TempGridRow.Cells(12).Value = TempArena.Spotlight
            TempGridRow.Cells(13).Value = TempArena.Stairs
            TempGridRow.Cells(14).Value = TempArena.CommentarySeat
            TempGridRow.Cells(15).Value = TempArena.RingMat
            TempGridRow.Cells(16).Value = TempArena.FloorMattress
            TempGridRow.Cells(17).Value = TempArena.Crowd
            TempGridRow.Cells(18).Value = TempArena.CrowdSeatsPlace
            TempGridRow.Cells(19).Value = TempArena.CrowdSeatsModel
            TempGridRow.Cells(20).Value = TempArena.IBL
            TempGridRow.Cells(21).Value = TempArena.Titantron
            TempGridRow.Cells(22).Value = TempArena.Minitron
            TempGridRow.Cells(23).Value = TempArena.Wall_L
            TempGridRow.Cells(24).Value = TempArena.Wall_R
            TempGridRow.Cells(25).Value = TempArena.Header
            TempGridRow.Cells(26).Value = TempArena.Floor
            TempGridRow.Cells(27).Value = TempArena.MiscObjects
            TempGridRow.Cells(28).Value = TempArena.LightingType
            TempGridRow.Cells(29).Value = TempArena.CornerPost_CM
            TempGridRow.Cells(30).Value = TempArena.Rope_CM
            TempGridRow.Cells(31).Value = TempArena.Apron_CM
            TempGridRow.Cells(32).Value = TempArena.Turnbuckle_CM
            TempGridRow.Cells(33).Value = TempArena.RingMat_CM
            TempGridRow.Cells(34).Value = TempArena.version
            'Build the D byte list for error handling
            TempGridRow.Cells(35).Style = ReadOnlyCellStyle
            '35 = Add
            '36 = Delete
            For K As Integer = 0 To ArenaArray.Length - 1
                If ArenaArray(K) = &HA Then
                    If ArenaArray(K - 1) = &HD Then
                        DByteList.Add(True)
                    Else
                        DByteList.Add(False)
                    End If
                End If
            Next
            TempGridRow.Tag = DByteList
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridMiscView.Rows.AddRange(WorkingCollection.ToArray)
        GetMiscColumns(MiscViewType.SelectedIndex)
    End Sub

    Function ParseJsonToArena(ArenaJson As String) As ArenaInformation
        Dim reader As Newtonsoft.Json.JsonTextReader = New JsonTextReader(New StringReader(ArenaJson))
        reader.Read() 'Skips the first null value
        Dim ReturnedArena As ArenaInformation = New ArenaInformation
        While reader.Read
            Dim TemporaryArenaPart As String = reader.Value
            reader.Read()
            Select Case TemporaryArenaPart
                Case "Stadium"
                    ReturnedArena.Stadium = reader.Value
                Case "Advertisement"
                    ReturnedArena.Advertisement = reader.Value
                Case "CornerPost"
                    ReturnedArena.CornerPost = reader.Value
                Case "LED_CornerPost"
                    ReturnedArena.LED_CornerPost = reader.Value
                Case "Rope"
                    ReturnedArena.Rope = reader.Value
                Case "Apron"
                    ReturnedArena.Apron = reader.Value
                Case "LED_Apron"
                    ReturnedArena.LED_Apron = reader.Value
                Case "Turnbuckle"
                    ReturnedArena.Turnbuckle = reader.Value
                Case "Barricade"
                    ReturnedArena.Barricade = reader.Value
                Case "Fence"
                    ReturnedArena.Fence = reader.Value
                Case "CeilingLighting"
                    ReturnedArena.CeilingLighting = reader.Value
                Case "Spotlight"
                    ReturnedArena.Spotlight = reader.Value
                Case "Stairs"
                    ReturnedArena.Stairs = reader.Value
                Case "CommentarySeat"
                    ReturnedArena.CommentarySeat = reader.Value
                Case "RingMat"
                    ReturnedArena.RingMat = reader.Value
                Case "FloorMattress"
                    ReturnedArena.FloorMattress = reader.Value
                Case "Crowd"
                    ReturnedArena.Crowd = reader.Value
                Case "CrowdSeatsPlace"
                    ReturnedArena.CrowdSeatsPlace = reader.Value
                Case "CrowdSeatsModel"
                    ReturnedArena.CrowdSeatsModel = reader.Value
                Case "IBL"
                    ReturnedArena.IBL = reader.Value
                Case "Titantron"
                    ReturnedArena.Titantron = reader.Value
                Case "Minitron"
                    ReturnedArena.Minitron = reader.Value
                Case "Wall_L"
                    ReturnedArena.Wall_L = reader.Value
                Case "Wall_R"
                    ReturnedArena.Wall_R = reader.Value
                Case "Header"
                    ReturnedArena.Header = reader.Value
                Case "Floor"
                    ReturnedArena.Floor = reader.Value
                Case "MiscObjects"
                    ReturnedArena.MiscObjects = reader.Value
                Case "LightingType"
                    ReturnedArena.LightingType = reader.Value
                Case "CornerPost_CM"
                    ReturnedArena.CornerPost_CM = reader.Value
                Case "Rope_CM"
                    ReturnedArena.Rope_CM = reader.Value
                Case "Apron_CM"
                    ReturnedArena.Apron_CM = reader.Value
                Case "Turnbuckle_CM"
                    ReturnedArena.Turnbuckle_CM = reader.Value
                Case "RingMat_CM"
                    ReturnedArena.RingMat_CM = reader.Value
                Case "version"
                    ReturnedArena.version = reader.Value
                Case ""
                    'Skip because null type
                Case Else
                    MessageBox.Show("Unknown Obejct: " & TemporaryArenaPart)
            End Select
        End While
        Return ReturnedArena
    End Function

    Sub GetMiscColumns(MenuIndex As Integer)
        If Not MenuIndex > 0 Then '2K16 and Beyond
            DataGridMiscView.Columns("Col_LED_Apron").Visible = False
            DataGridMiscView.Columns("Col_Titantron").Visible = False
            DataGridMiscView.Columns("Col_Minitron").Visible = False
            DataGridMiscView.Columns("Col_Wall_L").Visible = False
            DataGridMiscView.Columns("Col_Wall_R").Visible = False
            DataGridMiscView.Columns("Col_Header").Visible = False
            DataGridMiscView.Columns("Col_Floor").Visible = False
            DataGridMiscView.Columns("Col_MiscObjects").Visible = False
        Else
            DataGridMiscView.Columns("Col_LED_Apron").Visible = True
            DataGridMiscView.Columns("Col_Titantron").Visible = True
            DataGridMiscView.Columns("Col_Minitron").Visible = True
            DataGridMiscView.Columns("Col_Wall_L").Visible = True
            DataGridMiscView.Columns("Col_Wall_R").Visible = True
            DataGridMiscView.Columns("Col_Header").Visible = True
            DataGridMiscView.Columns("Col_Floor").Visible = True
            DataGridMiscView.Columns("Col_MiscObjects").Visible = True
            If Not MenuIndex > 2 Then '2K18 and Beyond
                DataGridMiscView.Columns("Col_LED_CornerPost").Visible = False
                DataGridMiscView.Columns("Col_LightingType").Visible = False
            Else
                DataGridMiscView.Columns("Col_LED_CornerPost").Visible = True
                DataGridMiscView.Columns("Col_LightingType").Visible = True
                If Not MenuIndex > 3 Then '2K19 and Beyond
                    DataGridMiscView.Columns("Col_CrowdSeatsPlace").Visible = False
                    DataGridMiscView.Columns("Col_CrowdSeatsModel").Visible = False
                    DataGridMiscView.Columns("Col_CornerPost_CM").Visible = False
                    DataGridMiscView.Columns("Col_Rope_CM").Visible = False
                    DataGridMiscView.Columns("Col_Apron_CM").Visible = False
                    DataGridMiscView.Columns("Col_Turnbuckle_CM").Visible = False
                    DataGridMiscView.Columns("Col_RingMat_CM").Visible = False
                Else
                    DataGridMiscView.Columns("Col_CrowdSeatsPlace").Visible = True
                    DataGridMiscView.Columns("Col_CrowdSeatsModel").Visible = True
                    DataGridMiscView.Columns("Col_CornerPost_CM").Visible = True
                    DataGridMiscView.Columns("Col_Rope_CM").Visible = True
                    DataGridMiscView.Columns("Col_Apron_CM").Visible = True
                    DataGridMiscView.Columns("Col_Turnbuckle_CM").Visible = True
                    DataGridMiscView.Columns("Col_RingMat_CM").Visible = True
                End If
            End If
        End If
    End Sub

    Private Sub MiscViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MiscViewType.SelectedIndexChanged
        'Locked to 2K19 for now
        If Not MiscViewType.SelectedIndex = 4 Then
            MiscViewType.SelectedIndex = 4
            Exit Sub
        End If
        If Not My.Settings.MiscModeIndex = MiscViewType.SelectedIndex Then 'checks if the index is actually changed, or just being reverted
            If SavePending Then
                If MessageBox.Show("Any changes will be lost", "Continue format change?", MessageBoxButtons.YesNo) = DialogResult.No Then
                    MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
                    Exit Sub
                End If
            End If
            My.Settings.MiscModeIndex = MiscViewType.SelectedIndex
            If TreeView1.SelectedNode IsNot Nothing Then
                FillMiscView(TreeView1.SelectedNode)
            End If
        End If
    End Sub

    Private Sub DataGridMiscView_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellEndEdit
        Dim MyCell As DataGridViewCell = DataGridMiscView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        If OldValue = -2 Then
            MyCell.Value = OldValue
            Exit Sub
        End If
        If Not IsNumeric(MyCell.Value) OrElse
               MyCell.Value < -1 Then
            MyCell.Value = OldValue
        Else
            SavePending = True
            SaveChangesMiscMenuItem.Visible = True
        End If
    End Sub

    Private Sub DataGridMiscView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn AndAlso
           e.RowIndex >= 0 Then
            If e.ColumnIndex = 35 Then 'add button
                If Not DataGridMiscView.Rows(e.RowIndex).Cells(0).Value + 1 = DataGridMiscView.Rows(e.RowIndex + 1).Cells(0).Value Then
                    Dim Duplicaterow As DataGridViewRow = DataGridMiscView.Rows(e.RowIndex).Clone
                    For i As Integer = 0 To DataGridMiscView.Rows(e.RowIndex).Cells.Count - 1
                        Duplicaterow.Cells(i).Value = DataGridMiscView.Rows(e.RowIndex).Cells(i).Value
                    Next
                    Duplicaterow.Cells(0).Value = (Duplicaterow.Cells(0).Value + 1).ToString.PadLeft(5, "0")
                    Duplicaterow.Tag = DataGridMiscView.Rows(e.RowIndex).Tag
                    DataGridMiscView.Rows.Insert(e.RowIndex + 1, Duplicaterow)
                    SaveChangesMiscMenuItem.Visible = True
                Else
                    MessageBox.Show("Cannot Create New Arena Here")
                End If
            ElseIf e.ColumnIndex = 36 Then 'Delete button
                DataGridMiscView.Rows.RemoveAt(e.RowIndex)
                SaveChangesMiscMenuItem.Visible = True
            Else
                'do nothing
            End If
            'do nothing
        End If
    End Sub

    Private Sub SaveMiscChangesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveChangesMiscMenuItem.Click
        FilePartHandlers.InjectBytesIntoFile(ReadNode.Tag, BuildMiscFile())
    End Sub

    Private Function BuildMiscFile() As Byte()
        Dim Active_Offset As Integer = &H10 + &H20 * DataGridMiscView.RowCount
        Dim Temp_Array As Byte() = New Byte(&H20000) {}
        Temp_Array(0) = DataGridMiscView.RowCount
        Temp_Array(5) = 1
        Temp_Array(&HC) = &H10
        ProgressBar1.Maximum = DataGridMiscView.RowCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To DataGridMiscView.RowCount - 1
            'Making the header
            Dim Part_Head_Array As Byte() = New Byte(&H20) {}
            'Adding the text parts.
            Dim ArenaInfoBytes As Byte() = Encoding.ASCII.GetBytes("arenaInfo" & DataGridMiscView.Rows(i).Cells(0).Value)
            Buffer.BlockCopy(ArenaInfoBytes, 0, Part_Head_Array, 0, ArenaInfoBytes.Length)
            Buffer.BlockCopy(Encoding.ASCII.GetBytes("jsn"), 0, Part_Head_Array, &H10, 3)
            'Build the Arena String
            Dim Part_String As String = BuildJsonArenaFile(i)
            'if there is a builder error this will exit the function without writing the file.
            If Part_String = "" Then
                Return Nothing
            End If
            'Add the Length to the Header
            Buffer.BlockCopy(BitConverter.GetBytes(Part_String.Length), 0, Part_Head_Array, &H14, 4)
            'Add the offset to the header
            Buffer.BlockCopy(BitConverter.GetBytes(Active_Offset), 0, Part_Head_Array, &H18, 4)
            'injecting the header
            Buffer.BlockCopy(Part_Head_Array, 0, Temp_Array, &H10 + &H20 * i, &H20)
            'Adding the ArenaInfo to the ararry
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(Part_String), 0, Temp_Array, Active_Offset, Part_String.Length)
            'Updating the Active Offset
            Active_Offset += Part_String.Length
            If Part_String.Length Mod 16 > 0 Then
                Active_Offset += 16 - Part_String.Length Mod 16
            End If
            ProgressBar1.Value = i
        Next
        ReDim Preserve Temp_Array(Active_Offset - 1)
        'Dim Final_Array As Byte() = New Byte(Active_Offset - 1) {}
        'Buffer.BlockCopy(Temp_Array, 0, Final_Array, 0, Active_Offset)
        Return Temp_Array
    End Function

    Function JsonWriterArenaFile(Index As Integer) As String
        'TO DO TEST 2K18 - 2K15
        'This is to be tested for 2k18 - 2k15 before allowing it in a release
        Dim TempStringBuilder As StringBuilder = New StringBuilder()
        Dim TempStringWriter As StringWriter = New StringWriter(TempStringBuilder)
        Using ActualWriter As JsonWriter = New JsonTextWriter(TempStringWriter)
            ActualWriter.Formatting = Formatting.Indented
            ActualWriter.WriteStartObject()
            ActualWriter.WritePropertyName("Stadium")
            ActualWriter.WriteValue(DataGridMiscView(1, Index).Value)
            ActualWriter.WritePropertyName("Advertisement")
            ActualWriter.WriteValue(DataGridMiscView(2, Index).Value)
            ActualWriter.WritePropertyName("CornerPost")
            ActualWriter.WriteValue(DataGridMiscView(3, Index).Value)
            ActualWriter.WritePropertyName("LED_CornerPost")
            ActualWriter.WriteValue(DataGridMiscView(4, Index).Value)
            ActualWriter.WritePropertyName("Rope")
            ActualWriter.WriteValue(DataGridMiscView(5, Index).Value)
            ActualWriter.WritePropertyName("Apron")
            ActualWriter.WriteValue(DataGridMiscView(6, Index).Value)
            ActualWriter.WritePropertyName("LED_Apron")
            ActualWriter.WriteValue(DataGridMiscView(7, Index).Value)
            ActualWriter.WritePropertyName("Turnbuckle")
            ActualWriter.WriteValue(DataGridMiscView(8, Index).Value)
            ActualWriter.WritePropertyName("Barricade")
            ActualWriter.WriteValue(DataGridMiscView(9, Index).Value)
            ActualWriter.WritePropertyName("Fence")
            ActualWriter.WriteValue(DataGridMiscView(10, Index).Value)
            ActualWriter.WritePropertyName("CeilingLighting")
            ActualWriter.WriteValue(DataGridMiscView(11, Index).Value)
            ActualWriter.WritePropertyName("Spotlight")
            ActualWriter.WriteValue(DataGridMiscView(12, Index).Value)
            ActualWriter.WritePropertyName("Stairs")
            ActualWriter.WriteValue(DataGridMiscView(13, Index).Value)
            ActualWriter.WritePropertyName("RingMat")
            ActualWriter.WriteValue(DataGridMiscView(14, Index).Value)
            ActualWriter.WritePropertyName("FloorMattress")
            ActualWriter.WriteValue(DataGridMiscView(15, Index).Value)
            ActualWriter.WritePropertyName("Crowd")
            ActualWriter.WriteValue(DataGridMiscView(16, Index).Value)
            ActualWriter.WritePropertyName("CrowdSeatsPlace")
            ActualWriter.WriteValue(DataGridMiscView(17, Index).Value)
            ActualWriter.WritePropertyName("CrowdSeatsModel")
            ActualWriter.WriteValue(DataGridMiscView(18, Index).Value)
            ActualWriter.WritePropertyName("IBL")
            ActualWriter.WriteValue(DataGridMiscView(19, Index).Value)
            ActualWriter.WritePropertyName("Titantron")
            ActualWriter.WriteValue(DataGridMiscView(20, Index).Value)
            ActualWriter.WritePropertyName("Minitron")
            ActualWriter.WriteValue(DataGridMiscView(21, Index).Value)
            ActualWriter.WritePropertyName("Wall_L")
            ActualWriter.WriteValue(DataGridMiscView(22, Index).Value)
            ActualWriter.WritePropertyName("Wall_R")
            ActualWriter.WriteValue(DataGridMiscView(23, Index).Value)
            ActualWriter.WritePropertyName("Header")
            ActualWriter.WriteValue(DataGridMiscView(24, Index).Value)
            ActualWriter.WritePropertyName("Floor")
            ActualWriter.WriteValue(DataGridMiscView(25, Index).Value)
            ActualWriter.WritePropertyName("MiscObjects")
            ActualWriter.WriteValue(DataGridMiscView(26, Index).Value)
            ActualWriter.WritePropertyName("LightingType")
            ActualWriter.WriteValue(DataGridMiscView(27, Index).Value)
            ActualWriter.WritePropertyName("CornerPost_CM")
            ActualWriter.WriteValue(DataGridMiscView(28, Index).Value)
            ActualWriter.WritePropertyName("Rope_CM")
            ActualWriter.WriteValue(DataGridMiscView(29, Index).Value)
            ActualWriter.WritePropertyName("Apron_CM")
            ActualWriter.WriteValue(DataGridMiscView(30, Index).Value)
            ActualWriter.WritePropertyName("Turnbuckle_CM")
            ActualWriter.WriteValue(DataGridMiscView(31, Index).Value)
            ActualWriter.WritePropertyName("RingMat_CM")
            ActualWriter.WriteValue(DataGridMiscView(32, Index).Value)
            ActualWriter.WritePropertyName("version")
            ActualWriter.WriteValue(DataGridMiscView(33, Index).Value)
            ActualWriter.WriteEndObject()
        End Using
        Return TempStringBuilder.ToString()
    End Function

    Function BuildJsonArenaFile(index As Integer) As String
        Try
            Dim DListAray As List(Of Boolean) = DataGridMiscView.Rows(index).Tag
            ' Chr(&H7B) = { Chr(&HD) = Carriage return Chr(&HA) = Line feed
            Dim TempItemCount As Integer = 0
            Dim Temp_String As String = Chr(&H7B)
            If DListAray(TempItemCount + 0) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Stadium"":" & DataGridMiscView(1, index).Value.ToString & ","
            If DListAray(TempItemCount + 1) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Advertisement"":" & DataGridMiscView(2, index).Value.ToString & ","
            If DListAray(TempItemCount + 2) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CornerPost"":" & DataGridMiscView(3, index).Value.ToString & ","
            If Not DataGridMiscView(4, index).Value = -2 Then
                If DListAray(TempItemCount + 3) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LED_CornerPost"":" & DataGridMiscView(4, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If DListAray(TempItemCount + 3) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Rope"":" & DataGridMiscView(5, index).Value.ToString & ","
            If DListAray(TempItemCount + 4) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Apron"":" & DataGridMiscView(6, index).Value.ToString & ","
            If Not DataGridMiscView(7, index).Value = -2 Then
                If DListAray(TempItemCount + 5) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LED_Apron"":" & DataGridMiscView(7, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If DListAray(TempItemCount + 5) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Turnbuckle"":" & DataGridMiscView(8, index).Value.ToString & ","
            If DListAray(TempItemCount + 6) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Barricade"":" & DataGridMiscView(9, index).Value.ToString & ","
            If DListAray(TempItemCount + 7) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Fence"":" & DataGridMiscView(10, index).Value.ToString & ","
            If DListAray(TempItemCount + 8) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CeilingLighting"":" & DataGridMiscView(11, index).Value.ToString & ","
            If DListAray(TempItemCount + 9) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Spotlight"":" & DataGridMiscView(12, index).Value.ToString & ","
            If DListAray(TempItemCount + 10) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Stairs"":" & DataGridMiscView(13, index).Value.ToString & ","
            If DListAray(TempItemCount + 11) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """CommentarySeat"":" & DataGridMiscView(14, index).Value.ToString & ","
            If DListAray(TempItemCount + 12) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """RingMat"":" & DataGridMiscView(15, index).Value.ToString & ","
            If DListAray(TempItemCount + 13) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """FloorMattress"":" & DataGridMiscView(16, index).Value.ToString & ","
            If DListAray(TempItemCount + 14) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Crowd"":" & DataGridMiscView(17, index).Value.ToString & ","
            If Not DataGridMiscView(18, index).Value = -2 Then
                If DListAray(TempItemCount + 15) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CrowdSeatsPlace"":" & DataGridMiscView(18, index).Value.ToString & ","
                If DListAray(TempItemCount + 16) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CrowdSeatsModel"":" & DataGridMiscView(19, index).Value.ToString & ","
                TempItemCount += 2
            End If
            If DListAray(TempItemCount + 15) Then Temp_String += Chr(&HD)
            Temp_String = Temp_String & Chr(&HA) & "    " & """IBL"":" & DataGridMiscView(20, index).Value.ToString & ","
            If DListAray(TempItemCount + 16) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Titantron"":" & DataGridMiscView(21, index).Value.ToString & ","
            If DListAray(TempItemCount + 17) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Minitron"":" & DataGridMiscView(22, index).Value.ToString & ","
            If DListAray(TempItemCount + 18) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Wall_L"":" & DataGridMiscView(23, index).Value.ToString & ","
            If DListAray(TempItemCount + 19) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Wall_R"":" & DataGridMiscView(24, index).Value.ToString & ","
            If DListAray(TempItemCount + 20) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Header"":" & DataGridMiscView(25, index).Value.ToString & ","
            If DListAray(TempItemCount + 21) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """Floor"":" & DataGridMiscView(26, index).Value.ToString & ","
            If DListAray(TempItemCount + 22) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """MiscObjects"":" & DataGridMiscView(27, index).Value.ToString & ","
            If Not DataGridMiscView(28, index).Value.ToString = -2 Then
                If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """LightingType"":" & DataGridMiscView(28, index).Value.ToString & ","
                TempItemCount += 1
            End If
            If Not DataGridMiscView(29, index).Value.ToString = -2 Then
                If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """CornerPost_CM"":" & DataGridMiscView(29, index).Value.ToString & ","
                If DListAray(TempItemCount + 24) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Rope_CM"":" & DataGridMiscView(30, index).Value.ToString & ","
                If DListAray(TempItemCount + 25) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Apron_CM"":" & DataGridMiscView(31, index).Value.ToString & ","
                If DListAray(TempItemCount + 26) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """Turnbuckle_CM"":" & DataGridMiscView(32, index).Value.ToString & ","
                If DListAray(TempItemCount + 27) Then Temp_String += Chr(&HD)
                Temp_String += Chr(&HA) & "    " & """RingMat_CM"":" & DataGridMiscView(33, index).Value.ToString & ","
                TempItemCount += 5
            End If
            If DListAray(TempItemCount + 23) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & "    " & """version"":" & """" & DataGridMiscView(34, index).Value.ToString & """"
            If DListAray(TempItemCount + 24) Then Temp_String += Chr(&HD)
            Temp_String += Chr(&HA) & Chr(&H7D)
            If DListAray(TempItemCount + 25) Then Temp_String += Chr(&HD) 'DListAray(TempItemCount + 17)
            Temp_String += Chr(&HA)
            Return Temp_String
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        End Try
    End Function

End Class
