Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    'TO DO Streamline integration with Object models whenever Object viewer is actually built.
    Sub FillMuscleView(SelectedData As TreeNode)
        Dim MuscleBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        If DataGridMuscleView.ColumnCount = 0 Then
            DataGridMuscleView.Columns.Add("Name", "Name")
            DataGridMuscleView.Columns.Add("Number1", Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        Else
            DataGridMuscleView.Columns.Add("Number" & DataGridMuscleView.ColumnCount, Path.GetFileNameWithoutExtension(SelectedData.ToolTipText))
        End If
        Dim MuscleCount As Integer = BitConverter.ToInt32(MuscleBytes, &HC)
        Dim ActiveIndex As Long = &H14
        For i As Integer = 0 To MuscleCount - 1
            Dim MuscleName As String = Encoding.ASCII.GetString(MuscleBytes, ActiveIndex + 4, &H20)
            ActiveIndex = ActiveIndex + BitConverter.ToInt32(MuscleBytes, ActiveIndex)
            If DataGridMuscleView.ColumnCount = 2 Then
                DataGridMuscleView.Rows.Add(MuscleName, i)
            Else
                If GetMuscleRow(MuscleName) = -1 Then
                    DataGridMuscleView.Rows.Add(MuscleName)
                    DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, DataGridMuscleView.RowCount - 1).Value = i
                Else
                    DataGridMuscleView(DataGridMuscleView.ColumnCount - 1, GetMuscleRow(MuscleName)).Value = i
                End If
            End If
        Next
    End Sub

    Function GetMuscleRow(MuscleName As String)
        For i As Integer = 0 To DataGridMuscleView.RowCount - 1
            If DataGridMuscleView(0, i).Value = MuscleName Then
                Return i
            End If
        Next
        Return -1
    End Function

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If MainMenuTabControl.TabPages.Contains(MuscleView) Then
            MainMenuTabControl.SelectedIndex = 0
            MainMenuTabControl.TabPages.Remove(MuscleView)
        End If
    End Sub

End Class