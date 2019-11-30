Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Dim MuscleViewStartupRemoved As Boolean = False

    Sub FillViewSettings()
        HexViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        TextViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
        ShowViewType.SelectedIndex = My.Settings.ShowModeIndex
        Dim TestPair As KeyValuePair(Of UInt32, String) = New KeyValuePair(Of UInteger, String)(0, "String Not Read")
        If Not StringReferences.Contains(TestPair) Then
            StringRead = True
        End If
        StringLoadedShowMenuItem.Text = "String Loaded: " & StringRead.ToString
        StringLoadedAttireMenuItem.Text = "String Loaded: " & StringRead.ToString
        StringLoadedTitleMenuItem.Text = "String Loaded: " & StringRead.ToString
        StringLoadedCAEMenuItem.Text = "String Loaded: " & StringRead.ToString
        If Not PacNumbers(0) = -1 Then
            PacsRead = True
        End If
        PacsLoadedAttireMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
        PacsLoadedTitleMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
        PacsLoadedCAEMenuItem.Text = "Pacs Loaded: " & PacsRead.ToString
    End Sub

    'Commands that should be generic to be used across multiple tabs.
    Private Sub StoreOldComboBoxValue(sender As Object, e As EventArgs) Handles HexViewBitWidth.Enter,
                                                                        TextViewBitWidth.Enter
        OldValue = sender.text
    End Sub

    Private Sub StoreOldDataGridViewValue(sender As DataGridView, e As DataGridViewCellEventArgs) Handles DataGridMiscView.CellEnter,
                                                                                                    DataGridShowView.CellEnter,
                                                                                                    DataGridNIBJView.CellEnter,
                                                                                                    DataGridAttireView.CellEnter,
                                                                                                    DataGridMaskView.CellEnter,
                                                                                                    DataGridObjArrayView.CellEnter,
                                                                                                    DataGridAssetView.CellEnter,
                                                                                                    DataGridTitleView.CellEnter,
                                                                                                    DataGridSoundView.CellEnter,
                                                                                                    DataGridMenuItemView.CellEnter,
                                                                                                    DataGridWeaponPositionView.CellEnter
        OldValue = sender.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
    End Sub

    Sub SaveFileNoLongerPending()
        'Read node should auto reset if the menu gets regenerated..
        'if this causes issues we will need to call a regen node somehow.
        SavePending = False
        SaveChangesStringMenuItem.Visible = False
        SaveChangesMiscMenuItem.Visible = False
        SaveChangesShowMenuItem.Visible = False
        SaveChangesNIBJMenuItem.Visible = False
        SaveChangesAttireMenuItem.Visible = False
        SaveChangesMaskMenuItem.Visible = False
        SaveChangesYOBJArrayMenuItem.Visible = False
        SaveChangesAssetViewMenuItem.Visible = False
        SaveChangesTitleMenuItem.Visible = False
        SaveChangesSoundMenuItem.Visible = False
        SaveChangesCAEMenuItemMenuItem.Visible = False
        RebuildNodeFromUpdatedFiles(TreeView1.SelectedNode)
        'TO DO Update this to include all save buttons
    End Sub

    Function ClearandGetClone(SentDataGrid) As DataGridViewRow
        SentDataGrid.Rows.Clear()
        SentDataGrid.Rows.Add()
        Dim CloneRow As DataGridViewRow = SentDataGrid.Rows(0).Clone()
        SentDataGrid.Rows.Clear()
        Return CloneRow
    End Function

    Public ReadOnlyCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle With {
        .BackColor = SystemColors.Control,
        .ForeColor = SystemColors.ControlText}

    Public DefaultCellStyle As DataGridViewCellStyle = New DataGridViewCellStyle With {
        .BackColor = SystemColors.Window,
        .ForeColor = SystemColors.WindowText}

End Class
