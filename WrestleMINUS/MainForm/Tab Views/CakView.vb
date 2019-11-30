Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Sub FillCAkFileView(SelectedData As TreeNode)
        Dim TempCakFile As PackageHandlers.CakFileHandler = New PackageHandlers.CakFileHandler
        TempCakFile = TempCakFile.LoadFromFile(SelectedData.Tag.FullFilePath)
        'Now we have read the file we can get the header bytes
        Dim TempHeader As PackageHandlers.CakFileHandler.BakedFileHeader = TempCakFile.GetHeaderInformation()
        DataGridCAkHeaderView.Rows.Add("Folder Hashes", TempHeader.FolderHashesOffset, TempHeader.FolderHashesSize, TempHeader.FolderHashesCrc, TempHeader.FoldersCount)
        DataGridCAkHeaderView.Rows.Add("File Hashes", TempHeader.FileHashesOffset, TempHeader.FileHashesSize, TempHeader.FileHashesCrc, TempHeader.FilesCount)
        DataGridCAkHeaderView.Rows.Add("Files", TempHeader.FilesOffset, TempHeader.FilesSize, TempHeader.FilesCrc, TempHeader.FilesCount)
        DataGridCAkHeaderView.Rows.Add("Folders", TempHeader.FoldersOffset, TempHeader.FoldersSize, TempHeader.FoldersCrc, TempHeader.FoldersCount)
        DataGridCAkHeaderView.Rows.Add("Strings", TempHeader.StringsOffset, TempHeader.StringsSize, TempHeader.StringsCrc, "")
        'Folder Views
        'DataGridCAkFolderView
        Dim ContainedFolderList As List(Of PackageHandlers.CakFileHandler.PackFolderEntry) = TempCakFile.GetFolderInformation()
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridCAkFolderView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = ContainedFolderList.Count - 1
        For i As Integer = 0 To ContainedFolderList.Count - 1
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(DataGridCAkFolderView.Columns.IndexOf(CAkFolderIndexColumn)).Value = i
            TempGridRow.Cells(DataGridCAkFolderView.Columns.IndexOf(CAkFolderNameColumn)).Value = ContainedFolderList(i).Name
            TempGridRow.Cells(DataGridCAkFolderView.Columns.IndexOf(CAkFolderUnkColumn)).Value = ContainedFolderList(i).Unk
            TempGridRow.Cells(DataGridCAkFolderView.Columns.IndexOf(CAkFolderHashColumn)).Value = Hex(ContainedFolderList(i).Hash)
            TempGridRow.Cells(DataGridCAkFolderView.Columns.IndexOf(CAkFolderFolderCountColumn)).Value = ContainedFolderList(i).FolderIndices.Count
            TempGridRow.Cells(DataGridCAkFolderView.Columns.IndexOf(CAkFolderFileCountColumn)).Value = ContainedFolderList(i).FileIndices.Count
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridCAkFolderView.Rows.AddRange(WorkingCollection.ToArray())
        'DataGridCAkFilesView
        Dim ContainedFileList As List(Of PackageHandlers.CakFileHandler.PackFileEntry) = TempCakFile.GetFileInformation()
        Dim CloneFileRow As DataGridViewRow = ClearandGetClone(DataGridCAkFilesView)
        Dim WorkingFileCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = ContainedFileList.Count - 1
        For i As Integer = 0 To ContainedFileList.Count - 1
            Dim TempGridRow As DataGridViewRow = CloneFileRow.Clone()
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileIndexColumn)).Value = i
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileNameColumn)).Value = ContainedFileList(i).Name
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileUnkColumn)).Value = ContainedFileList(i).FolderIndex
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileHashColumn)).Value = Hex(ContainedFileList(i).Hash)
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileOffsetColumn)).Value = ContainedFileList(i).Offset
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileSizeColumn)).Value = ContainedFileList(i).Size
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileCrcColumn)).Value = ContainedFileList(i).Crc
            TempGridRow.Cells(DataGridCAkFilesView.Columns.IndexOf(CAkFileTypeColumn)).Value = ContainedFileList(i).Type
            WorkingFileCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridCAkFilesView.Rows.AddRange(WorkingFileCollection.ToArray())
    End Sub

End Class
