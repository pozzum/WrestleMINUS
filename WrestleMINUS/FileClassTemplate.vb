Public Class FileClassTemplate
#Region "Package Handling Code"
    'Add your item to the PackageType Enum
    'for the template we use DUMMY since DUMY is actually used in the UFC games
    '
    'DUMMY
    '
#Region "PackageInformation Code"
    'In order to properly handle the files the code needs at minimum a function added to CheckHeaderType
    'This would preferably a "Magic Number" at the start however that is not always possible
    'Several other checks have been implemented for different file types
    'here we simulate the files simulate 16th byte starts a DUMMY string
    '
    'Case Encoding.Default.GetChars(ByteArray, Index + &H10, 5) = "DUMMY"
    'Return PackageType.DUMMY
    '

    'We will always need to add GetImageIndex so the menu shows an image
    'We would show D for DUMMY
    'D is 14
    'If a Letter is not already collected you have to add it on the NodeIcons.txt
    '
    'Case PackageType.DUMMY
    'Return 14


    'If the file is self contained ignore the following region
#Region "Handling SubItems of containers"

    'First Function we need to add to is CheckExpandable
    'Add the item if it contains sub items.

    'If the file has sub items and we want to be able to inject into it we want to add it to the Injectable Command.
    'CheckInjectable

    'If the file is injectable we also want to spell out how the file names are formatted so we can rename them
    'ValidateTruncation
    'The following code is for a 4 Character Decimal Name 
    'Case PackageType.DUMMY
    'Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper

    'If the file is expandable we need to add the file to GetFileParts so it can generate the sub items.
    '
    'Dim FileCount As Integer = BitConverter.ToUInt32(FileBytes, &FileCountIndex)
    'If IsNothing(ParentFileProperties.SubFiles) Then
    '                    ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
    '                End If
    'End If
    'For i As Integer = 0 To FileCount - 1
    '   Dim FileName As String = Hex(BitConverter.ToUInt32(FileBytes, TempHeaderStart + (i * FileHeaderLengthPer) + FileNameOffset))
    '   FileName = FileName.PadLeft(My.Settings.DecimalNameMinLength, "0")
    '   Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
    '                    .Name = FileName,
    '                    .FullFilePath = ParentFileProperties.FullFilePath,
    '                    .Index = BitConverter.ToUInt32(FileBytes, FileNameLength + i * FileHeaderLengthPer + FileIndexOffset) + ParentFileProperties.Index,
    '                    .length = BitConverter.ToUInt32(FileBytes, FileNameLength + i * FileHeaderLengthPer + FileLengthOffset),
    '                    .StoredData = ParentFileProperties.StoredData,
    '                    .FileType = CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
    '                    .Parent = ParentFileProperties}
    '                ParentFileProperties.SubFiles.Add(ContainedFileProperties)
    'Next
#End Region


#End Region
#End Region

#Region "MainForm Code"

#Region "Main Form Objects"
    Dim WithEvents DataGridDUMMYView As DataGridView
    'Make 1 column per information type + add and byte button columns if applicable
    Dim WithEvents SaveChangesDUMMYMenuItem As MenuItem
    Dim WithEvents DUMMYView As TabPage
#End Region

#Region "Main Form Code"
    'HideTabs needs a shortcut to the byte array
    '    Case PackageType.DUMMY
    '    InjectedByte = BuildDUMMYFile()

    'TabControl1_Selecting needs to know what tab you select transitions to reading the file
    '   Case DUMMYView.Name
    '   FillDUMMYView(ReadNode)

    'GetTabType needs to return the tab that should read the file information
    '   Case PackageType.DUMMY
    '   Return DUMMYView

    'SHARED TAB Functions

    'StoreOldDataGridViewValue handles DataGridDUMMYView.CellEnter

    'SaveFileNoLongerPending()

    'SaveChangesDUMMYMenuItem.Visible = False
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

    ReadOnly DUMMYHeaderLength As UInt32 = &H10 'ADJUST
    ReadOnly DUMMYPartSize As Integer = &H1E0 'ADJUST

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
#End Region
End Class
