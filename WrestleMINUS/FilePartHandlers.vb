Imports System.IO   'Files
Imports System.Text 'Text Encoding

Public Class ExtendedFileProperties
    Public Name As String = ""
    Public FullFilePath As String = ""
    Public FileType As PackageType = PackageType.Unchecked
    Public Index As UInt64 = 0
    Public length As UInt64
    Public StoredData As Byte()
    Public SubFiles As List(Of ExtendedFileProperties)
    Public Parent As ExtendedFileProperties
End Class

Public Class FilePartHandlers

    Shared Function GetFilePartBytes(ByRef RequestedFileProperties As ExtendedFileProperties) As Byte()
        'blocking error if length = 0
        If Not RequestedFileProperties.length > 0 Then
            Return New Byte() {}
        End If
        Dim FileBytes As Byte()
        If RequestedFileProperties.length > Int32.MaxValue Then
            MessageBox.Show("File too long, cannot be processed at this time.")
            Return New Byte() {}
        Else
            FileBytes = New Byte(RequestedFileProperties.length - 1) {}
        End If
        If RequestedFileProperties.StoredData.Length > 0 Then
            Array.Copy(RequestedFileProperties.StoredData, CInt(RequestedFileProperties.Index), FileBytes, 0, CInt(FileBytes.Length))
        Else
            If Not File.Exists(RequestedFileProperties.FullFilePath) Then
                MessageBox.Show("File Not Found")
                Return New Byte() {}
            End If
            Try
                Dim ActiveReader As BinaryReader = New BinaryReader(File.Open(RequestedFileProperties.FullFilePath, FileMode.Open, FileAccess.Read))
                ActiveReader.BaseStream.Seek(RequestedFileProperties.Index, SeekOrigin.Begin)
                FileBytes = ActiveReader.ReadBytes(RequestedFileProperties.length)
                ActiveReader.Dispose()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
        Return FileBytes
    End Function

    Shared Sub GetAllSubItems(ByRef RequestedHostFile As ExtendedFileProperties)
        Dim filepath As String = RequestedHostFile.FullFilePath
        'TO DO Add Progress Reporting
        If Not IsNothing(RequestedHostFile.SubFiles) Then
            For Each TestedSubItem As ExtendedFileProperties In RequestedHostFile.SubFiles
                If PackageInformation.CheckExpandable(TestedSubItem.FileType) Then
                    PackageInformation.GetFileParts(TestedSubItem, True)
                End If
            Next
        End If
    End Sub

#Region "Extract Functions"

    Shared Function ExtractFilePart(FileRequested As ExtendedFileProperties, Savepath As String) As Boolean
        Try
            File.WriteAllBytes(Savepath, GetFilePartBytes(FileRequested))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Shared Function ExtractFilePartTo(RequestedFile As ExtendedFileProperties) As Boolean
        Dim FileExtention As String = ".bin"
        If My.Settings.UseDetailedFileNames Then
            FileExtention = "." & RequestedFile.FileType.ToString
        End If
        Dim ExtractSaveFileDialog As SaveFileDialog = New SaveFileDialog With {
            .InitialDirectory = Path.GetDirectoryName(RequestedFile.FullFilePath),
            .FileName = RequestedFile.Name & FileExtention
        }
        If ExtractSaveFileDialog.ShowDialog() = DialogResult.OK Then
            Return ExtractFilePart(RequestedFile, ExtractSaveFileDialog.FileName)
        Else
            Return False
        End If
    End Function

    Shared Function ExtractAllSubFiles(RequestedSeedFile As ExtendedFileProperties, Optional BaseFolder As String = "", Optional AdditonalFolders As String = "") As Boolean
        Dim Result As Boolean = True
        'crawls the node first so all of the files to be extracted are located
        GetAllSubItems(RequestedSeedFile)
        If BaseFolder = "" Then
            If RequestedSeedFile.FileType = PackageType.Folder Then
                'if a folder used the folder as the base folder
                BaseFolder = RequestedSeedFile.FullFilePath & Path.DirectorySeparatorChar
            Else
                'otherwise use the folder that the file is in with a new folder using that file name
                BaseFolder = Path.GetDirectoryName(RequestedSeedFile.FullFilePath) & Path.DirectorySeparatorChar &
                               Path.GetFileNameWithoutExtension(RequestedSeedFile.FullFilePath) & Path.DirectorySeparatorChar
            End If
        End If
        'Folder Check will make sure the folder exists
        GeneralTools.FolderCheck(BaseFolder & AdditonalFolders)
        For Each TemporarySubItem As ExtendedFileProperties In RequestedSeedFile.SubFiles
            'Folders aren't extractable but make new folders
            If Not TemporarySubItem.FileType = PackageType.Folder Then
                'if it's a file we don't want to copy it.
                If Not Path.GetFileName(TemporarySubItem.FullFilePath) = TemporarySubItem.Name Then
                    'Here we Extract the current Temporary Item prior to parsing sub items.
                    Dim FileExtention As String = ".bin"
                    If My.Settings.UseDetailedFileNames Then
                        FileExtention = "." & TemporarySubItem.FileType.ToString
                    End If
                    If Not ExtractFilePart(TemporarySubItem, BaseFolder & AdditonalFolders &
                                   TemporarySubItem.Name & FileExtention) Then
                        'we've had a failure somewhere in the chain
                        Result = False
                    End If
                End If
            End If
            If Not IsNothing(TemporarySubItem.SubFiles) Then
                If TemporarySubItem.SubFiles.Count > 0 Then
                    'If there are sub Items we want to create a new folder if needed and call Extract All Items for those Sub Items
                    Dim Folder As String = ""
                    'Here we want to check if it is an uncompress container for the settings options..
                    If TemporarySubItem.FileType = PackageType.OODL OrElse
                        TemporarySubItem.FileType = PackageType.ZLIB OrElse
                        TemporarySubItem.FileType = PackageType.BPE Then
                        If My.Settings.DecompresstoFolder Then
                            If TemporarySubItem.Name.Contains(".") Then
                                Folder = TemporarySubItem.Name.Substring(0, TemporarySubItem.Name.IndexOf(".")) & Path.DirectorySeparatorChar
                            Else
                                Folder = TemporarySubItem.Name.TrimEnd(" ") & Path.DirectorySeparatorChar
                            End If
                        End If
                        'if not folder add-on is nothing
                    Else
                        If TemporarySubItem.Name.Contains(".") Then
                            Folder = TemporarySubItem.Name.Substring(0, TemporarySubItem.Name.IndexOf(".")) & Path.DirectorySeparatorChar
                        Else
                            Folder = TemporarySubItem.Name.TrimEnd(" ") & Path.DirectorySeparatorChar
                        End If
                    End If
                    If Not ExtractAllSubFiles(TemporarySubItem, BaseFolder, AdditonalFolders & Folder) Then
                        Result = False
                    End If
                End If
            End If
        Next
        Return Result
    End Function

#End Region

#Region "Inject Functions"

    Shared Function InjectFileIntoFilePart(FileRequested As ExtendedFileProperties, Optional InjectionCompressionType As PackageType = PackageType.bin) As Boolean
        If PackageInformation.CheckInjectable(FileRequested.Parent.FileType) Then
            Dim injectopenfile As OpenFileDialog = New OpenFileDialog With {
            .FileName = FileRequested.Name & "." & FileRequested.FileType.ToString,
            .InitialDirectory = Path.GetDirectoryName(FileRequested.FullFilePath)}
            If injectopenfile.ShowDialog() = DialogResult.OK Then
                If File.Exists(injectopenfile.FileName) Then
                    Dim FileBytes As Byte() = File.ReadAllBytes(injectopenfile.FileName)
                    If FileRequested.FileType = PackageType.SHDC Then
                        ReDim Preserve FileBytes(FileBytes.Length + (FileBytes.Length Mod &H100) - 1)
                    End If

                    Select Case InjectionCompressionType
                        'The buttons are only shown if the program checks passed so we don't need to check them here
                        Case PackageType.BPE
                            Dim CompressedBytes As Byte() = PackUnpack.GetCompressedBPEBytes(FileBytes)
                            If IsNothing(CompressedBytes) Then
                                MessageBox.Show("Failure to get Compressed Bytes")
                                Return False
                            End If
                            FileBytes = CompressedBytes
                        Case PackageType.ZLIB
                            Dim CompressedBytes As Byte() = PackUnpack.GetCompressedZlibBytes(FileBytes)
                            If IsNothing(CompressedBytes) Then
                                MessageBox.Show("Failure to get Compressed Bytes")
                                Return False
                            End If
                            FileBytes = CompressedBytes
                        Case PackageType.OODL
                            Dim CompressedBytes As Byte() = PackUnpack.GetCompressedOodleBytes(FileBytes)
                            If IsNothing(CompressedBytes) Then
                                MessageBox.Show("Failure to get Compressed Bytes")
                                Return False
                            End If
                            FileBytes = CompressedBytes
                    End Select
                    'Here we have a check that will make sure we don't double compress files and if it passes a parent if it matches the compression type
                    If FileRequested.Parent.FileType = InjectionCompressionType Then
                        FileRequested = FileRequested.Parent
                    End If
                    If InjectBytesIntoFile(FileRequested, FileBytes) Then
                        MessageBox.Show("Injection Complete")
                        Return True
                    Else
                        MessageBox.Show("Injection Failed")
                        Return False
                    End If
                Else
                    MessageBox.Show("File Does Not Exist")
                End If
            End If
        Else
            MessageBox.Show("Not Yet Supported")
        End If
        Return False
    End Function

    'TO DO - Update Naming Convention
    'I think I need to add checking oodle parents and making sure it's injected properly..
    Shared Function InjectBytesIntoFile(FileRequested As ExtendedFileProperties, SentBytes As Byte()) As Boolean
        'Adding in a check if file is read only and if the file is missing
        If Not GeneralTools.CheckFileWriteable(FileRequested.FullFilePath) Then
            Return False
        End If
        If IsNothing(SentBytes) Then 'exits the function if no bytes are sent
            MessageBox.Show("No File Sent, Injection Failed!")
            Return False
        End If
        Dim SizeDifference As Long = SentBytes.Length - FileRequested.length 'negative for shorter
        'checking File Type match
        Dim TempCheck As PackageType = PackageInformation.CheckHeaderType(0, SentBytes, FileRequested.FullFilePath)
        If Not FileRequested.FileType = TempCheck Then
            If MessageBox.Show("File Type Mismatch!" & vbNewLine & "Continue?",
                            TempCheck.ToString & " Replacing " & FileRequested.FileType.ToString,
                            MessageBoxButtons.YesNo) = DialogResult.No Then
                Return False
            End If
        End If
        'Get Parent Node Bytes
        Dim ParentFileInformation As ExtendedFileProperties
        If IsNothing(FileRequested.Parent) Then
            ParentFileInformation = FileRequested
        Else
            ParentFileInformation = FileRequested.Parent
        End If
        'skipping pach directories
        Dim DirectoryIndex As Integer = -1
        If ParentFileInformation.FileType = PackageType.PachDirectory_4 OrElse
           ParentFileInformation.FileType = PackageType.PachDirectory_8 Then
            DirectoryIndex = ParentFileInformation.Parent.SubFiles.IndexOf(ParentFileInformation)
            MessageBox.Show("Directory Skipped" & vbNewLine & DirectoryIndex)
            ParentFileInformation = ParentFileInformation.Parent
        End If
        Dim ParentBytes As Byte() = GetFilePartBytes(ParentFileInformation)
        'adjust length if needed
        If ParentFileInformation.FileType = PackageType.HSPC OrElse
                ParentFileInformation.FileType = PackageType.EPK8 OrElse
                 ParentFileInformation.FileType = PackageType.EPAC Then
            If SizeDifference > 0 Then 'size is rounded to &h800 bytes for these types
                SizeDifference += (&H800 - SizeDifference Mod &H800)
            ElseIf SizeDifference < 0 Then ' if it is 0 it stays 0
                SizeDifference -= (&H800 - Math.Abs(SizeDifference) Mod &H800)
            End If
        End If
        'Create Byte Array of length
        Dim WrittenFileArray As Byte() = New Byte(ParentFileInformation.length + SizeDifference - 1) {}
        ' Write File Prior to new file
        Array.Copy(ParentBytes, 0, WrittenFileArray, 0, CInt(FileRequested.Index - ParentFileInformation.Index))
        'write new file
        Array.Copy(SentBytes, 0, WrittenFileArray, CInt(FileRequested.Index - ParentFileInformation.Index), SentBytes.Length)
        'write old file from after file part if there are any
        If ParentBytes.Length > (FileRequested.Index - ParentFileInformation.Index) + FileRequested.length Then 'there are bytes after the injected file
            Buffer.BlockCopy(ParentBytes,
                                 (FileRequested.Index - ParentFileInformation.Index) + FileRequested.length,
                                WrittenFileArray,
                                 (FileRequested.Index - ParentFileInformation.Index) + FileRequested.length + SizeDifference,
                                 ParentBytes.Length - ((FileRequested.Index - ParentFileInformation.Index) + FileRequested.length))
        End If
        'Get Node Location
        'TO DO Fix this for files that don't have a parent
        Dim SubFileLocation As Integer
        If Not IsNothing(FileRequested.Parent) Then
            SubFileLocation = FileRequested.Parent.SubFiles.IndexOf(FileRequested) '0 based
        Else
            SubFileLocation = 0
        End If
        'Adjust Headers
        Select Case ParentFileInformation.FileType
            Case PackageType.HSPC
                Dim FileCount As Integer = BitConverter.ToUInt32(WrittenFileArray, &H38)
                'adjust total file length TO DO Double Check This
                Array.Copy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - &H2800)), 0, WrittenFileArray, &H3C, 4)
                'Get the header length
                Dim FileNameLength As Integer = BitConverter.ToUInt32(WrittenFileArray, &H18)
                FileNameLength += -(FileNameLength Mod &H800) + &H1000
                For i As Integer = 0 To FileCount - 1
                    If i < SubFileLocation Then
                        'no change needed
                    ElseIf i = SubFileLocation Then
                        'Index Stays the same
                        Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length / &H100)), 0, WrittenFileArray, FileNameLength + i * &HC + &H4, 4)
                    Else 'size stays but index changes
                        Dim OldIndex As UInt32 = BitConverter.ToUInt32(WrittenFileArray, FileNameLength + i * &HC)
                        Dim TempIndex As UInt64 = OldIndex * &H800 + SizeDifference
                        Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, FileNameLength + i * &HC, 4)
                    End If
                Next
            Case PackageType.EPK8 OrElse PackageType.EPAC
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount As Integer = 0
                'DirectoryIndex
                Do While index < HeaderLength - 1
                    Dim DirectoryContainsCount As Integer = 0
                    If PackageType.EPAC Then
                        DirectoryContainsCount = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 3
                    ElseIf PackageType.EPK8 Then
                        DirectoryContainsCount = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 4
                    End If
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        If DirectoryCount < DirectoryIndex Then
                            'no change needed
                        ElseIf DirectoryCount = DirectoryIndex Then
                            If i < SubFileLocation Then
                                'no change needed
                            ElseIf i = SubFileLocation Then
                                'Index Stays the same
                                Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length / &H100)), 0, WrittenFileArray, &H800 + index + 12, 4)
                            Else ' i >
                                Dim TempIndex As UInt64 = BitConverter.ToUInt32(WrittenFileArray, &H800 + index + 8) * &H800 + SizeDifference
                                Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, &H800 + index + 8, 4)
                            End If
                        Else ' directory >
                            Dim TempIndex As UInt64 = BitConverter.ToUInt32(WrittenFileArray, &H800 + index + 8) * &H800 + SizeDifference
                            Array.Copy(BitConverter.GetBytes(CUInt(TempIndex / &H800)), 0, WrittenFileArray, &H800 + index + 8, 4)
                        End If
                        If PackageType.EPAC Then
                            index += &HC
                        ElseIf PackageType.EPK8 Then
                            index += &H10
                        End If
                    Next
                    DirectoryCount += 1
                Loop
            Case PackageType.SHDC
                Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(WrittenFileArray, &H18)
                Dim TempHeaderStart As Integer = BitConverter.ToUInt32(WrittenFileArray, &H1C)
                Dim TempHeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, &H20)
                If TempHeaderStart < TempHeaderCheck Then
                    TempHeaderStart = TempHeaderCheck + &H10 + &H40
                    If TempHeaderStart Mod &H10 > 0 Then
                        TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                    End If
                End If
                Dim PachPartsCount As Integer = (TempHeaderLength / &H10) '1 index
                For i As Integer = 0 To PachPartsCount - 1
                    Dim PartName As String = Hex(BitConverter.ToUInt32(WrittenFileArray, TempHeaderStart + (i * &H10)))
                    If PartName = "FFFFFFFF" Then
                        Continue For
                    End If
                    If i < SubFileLocation Then
                        'no change needed
                    ElseIf i = SubFileLocation Then
                        'Index Stays the same
                        Array.Copy(BitConverter.GetBytes(CULng(SentBytes.Length)), 0, WrittenFileArray, (TempHeaderStart + (i * &H10) + &H8), 8)
                    Else ' i > NodeLocation
                        'Size stays index changed
                        Dim OldIndex As UInt32 = BitConverter.ToUInt32(WrittenFileArray, TempHeaderStart + (i * &H10) + &H4)
                        Array.Copy(BitConverter.GetBytes(CUInt(OldIndex + SizeDifference)), 0, WrittenFileArray, (TempHeaderStart + (i * &H10) + &H4), 4)
                    End If
                Next
            Case PackageType.TextureLibrary
                'start with the header fixes and inject each part of the file
                Dim FileCount As Integer = WrittenFileArray(0)
                Dim BytesRevesed As Boolean = False
                If FileCount = 0 Then 'if this is 0 then we are dealing with a reverse byte system header.
                    FileCount = WrittenFileArray(3)
                    BytesRevesed = True
                End If
                'Texture Library has no total length to adjust
                For i As Integer = 0 To FileCount - 1
                    If i < SubFileLocation Then
                        'no change needed
                    ElseIf i = SubFileLocation Then
                        'Index Stays the same
                        If BytesRevesed Then
                            Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(SentBytes.Length))), 0, WrittenFileArray, i * &H20 + &H10 + &H14, 4)
                        Else
                            Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length)), 0, WrittenFileArray, i * &H20 + &H10 + &H14, 4)
                        End If
                    Else 'size stays but index changes
                        If BytesRevesed Then
                            Dim OldIndex As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(WrittenFileArray, i * &H20 + &H10 + &H18, 4), 0)
                            Dim TempIndex As UInt64 = OldIndex + SizeDifference
                            Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(TempIndex))), 0, WrittenFileArray, i * &H20 + &H10 + &H18, 4)
                        Else
                            Dim OldIndex As UInt32 = BitConverter.ToUInt64(WrittenFileArray, i * &H20 + &H10 + &H18)
                            Dim TempIndex As UInt64 = OldIndex + SizeDifference
                            Array.Copy(BitConverter.GetBytes(CLng(TempIndex)), 0, WrittenFileArray, i * &H20 + &H10 + &H18, 8)
                        End If
                    End If
                Next
            Case PackageType.YANMPack
                Dim HeaderLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(WrittenFileArray), 0) + &H20
                'Yanm length has to be adjusted for the new length
                Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(WrittenFileArray.Length))), 0, WrittenFileArray, 4, 4)
                Dim HeadIndex As Integer = 0
                Dim partcount As Integer = 0
                If HeaderLength > 0 Then
                    Do While HeadIndex < HeaderLength
                        If HeadIndex = 0 Then
                            HeadIndex = &H70
                        End If
                        If partcount < SubFileLocation Then
                        ElseIf partcount = SubFileLocation Then
                            'no change needed because there is no length indicator
                        Else
                            'so now we will need to update some file index information
                            Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(ParentFileInformation.SubFiles(partcount).Index + SizeDifference - HeaderLength))), 0, WrittenFileArray, HeadIndex + &H24, 4)
                        End If
                        partcount = partcount + 1
                        HeadIndex = HeadIndex + &H28
                    Loop
                End If
        End Select
        If ParentFileInformation.Index = 0 AndAlso
            ParentFileInformation.StoredData.Length = 0 Then
            'File to be Written
            Dim WrittenFile As String = FileRequested.FullFilePath
            If My.Settings.BackupInjections AndAlso GeneralTools.CheckFileWriteable(WrittenFile & ".bak", False) Then
                File.Copy(WrittenFile, WrittenFile & ".bak", True)
            End If
            File.WriteAllBytes(WrittenFile, WrittenFileArray)
            'Remove Save Pending Buttons when file written
            MainForm.SaveFileNoLongerPending()
            Dim TempName As String = ParentFileInformation.Name
            PackageInformation.GetFileParts(ParentFileInformation)
            MainForm.InformationLoaded = False
        Else
            'we must go higher
            FilePartHandlers.InjectBytesIntoFile(ParentFileInformation, WrittenFileArray)
        End If
        Return True
    End Function

#End Region

#Region "Rename Functions"

    Shared Function RenameFileorFilePart(ByRef FileRequested As ExtendedFileProperties) As Boolean
        Dim ReturnedResult As Boolean = False
        If FileRequested.FileType = PackageType.Folder Then
            If Directory.Exists(FileRequested.FullFilePath) Then
                Dim TextDialogInstance As New TextDialogPrompt With {
                    .OldFileName = FileRequested.Name,
                    .EditedFileName = FileRequested.FullFilePath,
                    .ContainerBeingEdited = PackageType.Folder}
                TextDialogInstance.ShowDialog()
                If TextDialogInstance.Result = DialogResult.OK Then
                    If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                        'Folder Editing, MoveAllFiles will be used
                        Dim NewFolderName As String = TextDialogInstance.EditedFileName
                        NewFolderName = PackageInformation.ValidateTruncation(NewFolderName, PackageType.Folder)
                        'here we have to check if the folder already exists
                        Dim NameMatched As Boolean = Directory.Exists(NewFolderName)
                        If Not NameMatched Then
                            GeneralTools.MoveAllItems(FileRequested.FullFilePath, NewFolderName)
                            Dim TempDI As DirectoryInfo = New DirectoryInfo(NewFolderName)
                            'we want to edit the file Requested with the new data
                            FileRequested = New ExtendedFileProperties With {
                                .Name = TempDI.Name,
                                .FullFilePath = TempDI.FullName,
                                .FileType = PackageType.Folder,
                                .Index = 0,
                                .length = 0,
                                .StoredData = New Byte() {}}
                            MessageBox.Show("Folder Moved")
                            ReturnedResult = True
                        Else
                            MessageBox.Show("Folder " & NewFolderName & " already exists.", "Rename Failed")
                        End If
                    End If
                End If
                TextDialogInstance.Dispose()
            Else
                MessageBox.Show("Folder " & FileRequested.FullFilePath & " Not Found")
            End If
            'We want to throw the returned answer here no matter the situation
            'This means that the menu will always be disposed of.
            Return ReturnedResult
        ElseIf FileRequested.Index = 0 AndAlso
            FileRequested.StoredData.Length = 0 Then
            'it should be a file
            If GeneralTools.CheckFileWriteable(FileRequested.FullFilePath) Then
                Dim TextDialogInstance As New TextDialogPrompt With {
                    .OldFileName = FileRequested.Name,
                    .EditedFileName = .OldFileName,
                    .ContainerBeingEdited = PackageType.EditingFileName}
                TextDialogInstance.ShowDialog()
                If TextDialogInstance.Result = DialogResult.OK Then
                    If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                        'Folder Editing, MoveAllFiles will be used
                        Dim NewFileName As String = TextDialogInstance.EditedFileName
                        Dim NewFullPath As String = Path.GetDirectoryName(FileRequested.FullFilePath) & Path.DirectorySeparatorChar & TextDialogInstance.EditedFileName
                        If Not File.Exists(NewFullPath) Then
                            File.Move(FileRequested.FullFilePath, NewFullPath)
                            Dim NewFI As FileInfo = New FileInfo(NewFullPath)
                            'We want to update the file requested information with new data
                            FileRequested = New ExtendedFileProperties With {
                                .Name = NewFI.Name,
                                .FullFilePath = NewFI.FullName,
                                .FileType = PackageType.Unchecked,
                                .Index = 0,
                                .length = FileLen(NewFI.FullName),
                                .StoredData = New Byte() {}}
                            ReturnedResult = True
                        Else
                            MessageBox.Show("File " & NewFullPath & " already exists.")
                        End If
                    End If
                End If
                TextDialogInstance.Dispose()
            Else
            End If
            'We want to throw the returned answer here no matter the situation
            'This means that the menu will always be disposed of.
            Return ReturnedResult
        End If
        'Adding in a check if file is read only and if the file is missing
        If Not GeneralTools.CheckFileWriteable(FileRequested.FullFilePath) Then
            Return False
        End If
        'We have passed where we could be renaming an actual file and gotten to the point of so we need the parent file properties
        Dim ParentFileProperties As ExtendedFileProperties = FileRequested.Parent
        Dim NewFilePartName As String = ""
        If PackageInformation.CheckInjectable(ParentFileProperties.FileType) Then 'Hopefully this can expand to all
            'Here we want TextDialogInstance
            Dim TextDialogInstance As New TextDialogPrompt With {
                .OldFileName = FileRequested.Name,
                .EditedFileName = .OldFileName,
                .ContainerBeingEdited = ParentFileProperties.FileType}
            TextDialogInstance.ShowDialog()
            If TextDialogInstance.Result = DialogResult.OK Then
                If Not TextDialogInstance.OldFileName = TextDialogInstance.EditedFileName Then 'no change
                    NewFilePartName = PackageInformation.ValidateTruncation(TextDialogInstance.EditedFileName, ParentFileProperties.FileType)
                    'here we have to check if it already has that same file name
                    Dim NameMatched As Boolean = False
                    For i As Integer = 0 To ParentFileProperties.SubFiles.Count - 1
                        If ParentFileProperties.SubFiles(i).Name = NewFilePartName Then
                            NameMatched = True
                        End If
                    Next
                    If NameMatched Then
                        MessageBox.Show("File Already Contains a file named " & NewFilePartName)
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                Return False
            End If
            TextDialogInstance.Dispose()
        Else
            MessageBox.Show("Not Yet Supported")
            Return False
        End If
        'Now we have generated the new name for the file part
        'Get Parent Node Bytes
        Dim ParentBytes As Byte() = FilePartHandlers.GetFilePartBytes(ParentFileProperties)
        Dim ParentNodeLocation As Integer = 0
        If ParentFileProperties.FileType = PackageType.PachDirectory_4 OrElse
            ParentFileProperties.FileType = PackageType.PachDirectory_8 Then
            'here we grab the
            ParentBytes = FilePartHandlers.GetFilePartBytes(ParentFileProperties)
            ParentNodeLocation = ParentFileProperties.Parent.SubFiles.IndexOf(ParentFileProperties)
        End If
        Dim WrittenFileArray As Byte() = New Byte(ParentBytes.Length - 1) {}
        ' Write File Prior to new file
        Array.Copy(ParentBytes, 0, WrittenFileArray, 0, ParentBytes.LongLength)
        Dim ContainedParts As Integer = ParentFileProperties.SubFiles.Count
        If ContainedParts > 1 Then
            If MessageBox.Show("Tool cannot yet sort.  Continue?", "Continue?", MessageBoxButtons.OKCancel) = DialogResult.Cancel Then
                Return Nothing
            End If
        End If
        'Get Node Location
        Dim FilePartLocation As Integer = ParentFileProperties.SubFiles.IndexOf(FileRequested) '0 based
        Select Case ParentFileProperties.FileType
            Case PackageType.HSPC
                'Likely only the 1 SHDC unlikely needs to be sorted
                Dim StringBytes As Byte() = New Byte(7) {}
                Dim HexBytes As Byte() = GeneralTools.HexStringToByte(NewFilePartName)
                'making the new string
                Array.Copy(HexBytes, 0, StringBytes, 0, HexBytes.Length)
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, &H800 + FilePartLocation * &H14, 8)
            Case PackageType.SHDC
                '8 char possible endian reverse
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim HexBytes As Byte() = GeneralTools.HexStringToByte(NewFilePartName)
                'making the new string
                Array.Copy(GeneralTools.EndianReverse(HexBytes), 0, StringBytes, 0, HexBytes.Length)
                Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(WrittenFileArray, &H18)
                Dim TempHeaderStart As Integer = BitConverter.ToUInt32(WrittenFileArray, &H1C)
                Dim TempHeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, &H20)
                If TempHeaderStart < TempHeaderCheck Then
                    TempHeaderStart = TempHeaderCheck + &H10 + &H40
                    If TempHeaderStart Mod &H10 > 0 Then
                        TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                    End If
                End If
                'TempHeaderStart + (i * &H10)
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, TempHeaderStart + FilePartLocation * &H10, 4)
            Case PackageType.EPK8
                'This would be renaming the pach container
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewFilePartName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'I need to figure out how to double check what container it is... and we can't like assume an earlier container only has 1 pach
                Dim EditedNameIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    If DirectoryCount = FilePartLocation Then
                        EditedNameIndex = &H800 + index
                        Exit Do
                    End If
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 4
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        index += &H10
                    Next
                    DirectoryCount += 1
                Loop
                'now that we have the proper index..
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 4)
            Case PackageType.EPAC
                'This would be renaming the pach container
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewFilePartName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'I need to figure out how to double check what container it is... and we can't like assume an earlier container only has 1 pach
                Dim EditedNamerIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    If DirectoryCount = FilePartLocation Then
                        EditedNamerIndex = &H800 + index
                        Exit Do
                    End If
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 3
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        index += &HC
                    Next
                    DirectoryCount += 1
                Loop
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNamerIndex, 4)
            Case PackageType.PachDirectory_8
                'a pach inside a epk8 pach directory
                Dim StringBytes As Byte() = New Byte(7) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewFilePartName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'We can't assume this is in the first container
                Dim EditedNameIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 4
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        If DirectoryCount = ParentNodeLocation AndAlso
                            i = FilePartLocation Then
                            EditedNameIndex = &H800 + index
                            Exit Do
                        End If
                        index += &H10
                    Next
                    DirectoryCount += 1
                Loop
                'now that we have the proper index..
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 8)
            Case PackageType.PachDirectory_4
                'this renames a pach inside a epk8 pach directory
                Dim StringBytes As Byte() = New Byte(3) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewFilePartName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                'We can't assume this is in the first container
                Dim EditedNameIndex As Integer = 0
                Dim HeaderLength As Integer = BitConverter.ToUInt32(WrittenFileArray, 4)
                Dim index As Integer = 0
                Dim DirectoryCount = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(WrittenFileArray, &H800 + index + 4) / 3
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        If DirectoryCount = ParentNodeLocation AndAlso
                            i = FilePartLocation Then
                            EditedNameIndex = &H800 + index
                            Exit Do
                        End If
                        index += &HC
                    Next
                    DirectoryCount += 1
                Loop
                Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 4)
            Case PackageType.TextureLibrary
                'Does not need to be sorted
                Dim StringBytes As Byte() = New Byte(15) {}
                'making the new string
                Array.Copy(Encoding.Default.GetBytes(NewFilePartName), 0, StringBytes, 0, NewFilePartName.Length)
                'copy the string to the file.
                Array.Copy(StringBytes, 0, WrittenFileArray, &H10 + FilePartLocation * &H20, 16)
            Case PackageType.YANMPack
                Dim HeaderLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(WrittenFileArray), 0) + &H20
                Dim HeadIndex As Integer = 0
                Dim partcount As Integer = 0
                'getting the part name
                Dim StringBytes As Byte() = New Byte(8) {}
                Dim UnEncodedBytes As Byte() = Encoding.Default.GetBytes(NewFilePartName)
                'making the new string
                Array.Copy(UnEncodedBytes, 0, StringBytes, 0, UnEncodedBytes.Length)
                Dim EditedNameIndex As Integer = 0
                If HeaderLength > 0 Then
                    Do While HeadIndex < HeaderLength
                        If HeadIndex = 0 Then
                            HeadIndex = &H70
                        End If
                        If partcount = FilePartLocation Then
                            EditedNameIndex = HeadIndex + 4
                        End If
                        partcount = partcount + 1
                        HeadIndex = HeadIndex + &H28
                    Loop
                    Array.Copy(StringBytes, 0, WrittenFileArray, EditedNameIndex, 8)
                End If
        End Select
        'now we want to correct the parent so we can inject properly
        'skipping pach directories
        If ParentFileProperties.Index = 0 AndAlso
            ParentFileProperties.StoredData.Length = 0 Then
            'File to be Written
            Dim WrittenFile As String = FileRequested.FullFilePath
            If My.Settings.BackupInjections AndAlso GeneralTools.CheckFileWriteable(WrittenFile & ".bak", False) Then
                File.Copy(WrittenFile, WrittenFile & ".bak", True)
            End If
            File.WriteAllBytes(WrittenFile, WrittenFileArray)
            Return True
        Else
            'we must go higher
            Return InjectBytesIntoFile(ParentFileProperties, WrittenFileArray)
        End If
    End Function

#End Region

#Region "Delete Functions"

    Shared Function DeleteFilesFromParent(FileRequested As ExtendedFileProperties) As Boolean
        If Not GeneralTools.CheckFileWriteable(FileRequested.FullFilePath) Then
            Return False
        End If
        'negative for shorter, this will have to be modified by if the header is shorter for the particular container type
        Dim SizeDifference As Long = -FileRequested.length
        'Get Parent Node Bytes
        Dim ParentFileInformation As ExtendedFileProperties
        If IsNothing(FileRequested.Parent) Then
            ParentFileInformation = FileRequested
        Else
            ParentFileInformation = FileRequested.Parent
        End If
        'Get Node Location
        Dim NodeLocation As Integer = ParentFileInformation.SubFiles.IndexOf(FileRequested)
        'skipping pach directories
        Dim DirectoryIndex As Integer = -1
        If ParentFileInformation.FileType = PackageType.PachDirectory_4 OrElse
           ParentFileInformation.FileType = PackageType.PachDirectory_8 Then
            DirectoryIndex = ParentFileInformation.Parent.SubFiles.IndexOf(ParentFileInformation)
            MessageBox.Show("Directory Skipped" & vbNewLine & DirectoryIndex)
            ParentFileInformation = ParentFileInformation.Parent
        End If
        Dim ParentBytes As Byte() = GetFilePartBytes(ParentFileInformation)
        'Adjusting the Size Difference to match the offset types
        If ParentFileInformation.FileType = PackageType.HSPC OrElse
                ParentFileInformation.FileType = PackageType.EPK8 OrElse
                 ParentFileInformation.FileType = PackageType.EPAC Then
            If SizeDifference > 0 Then 'size is rounded to &h800 bytes for these types
                SizeDifference += (&H800 - SizeDifference Mod &H800)
            ElseIf SizeDifference < 0 Then ' if it is 0 it stays 0
                SizeDifference -= (&H800 - Math.Abs(SizeDifference) Mod &H800)
            End If
        End If

#Region "Header Adjustments Lengths"

        'Here we want to add any header length changes based on a file being deleted..
        'Rounding should not need to happen because files have full length set in the item
        'WARNING THE ABOVE STATEMENT SHOULD BE CHECKED
        Dim HeaderLength As Integer = 0
        'store adjustment as a negative for foreshortening
        Dim HeaderAdjustment As Integer = 0
        Select Case ParentFileInformation.FileType
            Case PackageType.HSPC
                'we need the file name length to get the proper header length..
                Dim FileNameLength As Integer = BitConverter.ToUInt32(ParentBytes, &H18)
                FileNameLength += -(FileNameLength Mod &H800) + &H1000
                HeaderLength = BitConverter.ToUInt32(ParentBytes, FileNameLength) * &H800
                HeaderAdjustment = 0
            Case PackageType.EPK8
                'The Header is always &H4000 Long
                HeaderLength = &H4000
                HeaderAdjustment = 0
            Case PackageType.EPAC
                'The Header is always &H4000 Long
                HeaderLength = &H4000
                HeaderAdjustment = 0
            Case PackageType.SHDC
                Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(ParentBytes, &H18)
                Dim TempHeaderStart As Integer = BitConverter.ToUInt32(ParentBytes, &H1C)
                Dim TempHeaderLength As Integer = BitConverter.ToUInt32(ParentBytes, &H20)
                If TempHeaderStart < TempHeaderCheck Then
                    TempHeaderStart = TempHeaderCheck + &H10 + &H40
                    If TempHeaderStart Mod &H10 > 0 Then
                        TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                    End If
                End If
                HeaderLength = TempHeaderLength + TempHeaderStart
                'Header is &H10 per file so we need to trim that length.
                HeaderAdjustment = -(&H10 + &H14)
            Case PackageType.PACH
                HeaderLength = &H8 + (ParentFileInformation.SubFiles.Count * &HC)
                ' header just needs to have a &hC trim
                HeaderAdjustment = -&HC
            Case PackageType.PachDirectory_4
                'The Header is always &H4000 Long
                HeaderLength = &H4000
                HeaderAdjustment = 0
            Case PackageType.PachDirectory_8
                'The Header is always &H4000 Long
                HeaderLength = &H4000
                HeaderAdjustment = 0
            Case PackageType.TextureLibrary
                HeaderLength = &H10 + (ParentFileInformation.SubFiles.Count * &H20)
                ' we need trim &h20.
                HeaderAdjustment = -&H20
            Case PackageType.YANMPack
                HeaderLength = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes), 0) + &H20
                ' We need to check if the length has a floating 8 bytes, if so we trim &h30 otherwise we only trim &h20
                If HeaderLength Mod &H10 = 0 Then
                    HeaderLength = HeaderLength
                    HeaderAdjustment = -&H20
                ElseIf HeaderLength Mod &H10 = 8 Then
                    HeaderLength = HeaderLength + 8
                    HeaderAdjustment = -&H30
                End If
        End Select

#End Region

        'Create Byte Array of length
        Dim WrittenFileArray As Byte() = New Byte(ParentFileInformation.length + SizeDifference - 1 + HeaderAdjustment) {}
        'Write File Prior to new file
        'We need to start at the old header and place it at the end of the adjusted header
        Array.Copy(ParentBytes, HeaderLength, WrittenFileArray, HeaderLength + HeaderAdjustment, CInt(FileRequested.Index - ParentFileInformation.Index) - HeaderLength)
        'write old file from after file part if there are any
        If ParentBytes.Length > (FileRequested.Index - ParentFileInformation.Index) + FileRequested.length Then 'there are bytes after the injected file
            Buffer.BlockCopy(ParentBytes,
                                 (FileRequested.Index - ParentFileInformation.Index) + FileRequested.length,
                                WrittenFileArray,
                                 (FileRequested.Index - ParentFileInformation.Index) + FileRequested.length + SizeDifference + HeaderAdjustment,
                                 ParentBytes.Length - ((FileRequested.Index - ParentFileInformation.Index) + FileRequested.length))
        End If

        '0 based

#Region "Revise Header"

        'This has to be revised entirely.
        'we now need to copy from the Parent Byte array as we go..
        Select Case ParentFileInformation.FileType

#Region "Case PackageType.HSPC"

            Case PackageType.HSPC
                'Copy the start header, we will overwrite some of these bytes
                Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H40)
                'Adjust new File Name Length
                Dim FileNameLength As Integer = BitConverter.ToUInt32(ParentBytes, &H18)
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(FileNameLength - &H14)), 0, WrittenFileArray, &H18, 4)
                'Adjusting File Information Length
                Dim FileInformationLength As Integer = BitConverter.ToUInt32(ParentBytes, &H20)
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(FileInformationLength - &HC)), 0, WrittenFileArray, &H20, 4)
                'Adjust the File Length
                Dim TotalHeaderAndFooter As Integer = &H2800 + FileNameLength - (FileNameLength Mod &H800)
                Array.Copy(BitConverter.GetBytes(CUInt((WrittenFileArray.Length - TotalHeaderAndFooter))), 0, WrittenFileArray, &H3C, 4)
                'Adjusting the File Count
                Dim FileCount As Integer = BitConverter.ToUInt32(ParentBytes, &H38)
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(FileCount - 1)), 0, WrittenFileArray, &H38, 4)
                'Get File Name Start Point
                Dim FileNameStartPoint As Integer = &H800
                'Get File Information Start Point
                Dim FileInformationStartPoint As Integer = &H1000 + FileNameLength - (FileNameLength Mod &H800)
                'Now will adjust the file name header
                For i As Integer = 0 To FileCount - 1
                    If i < NodeLocation Then
                        'no change needed to the file name
                        Buffer.BlockCopy(ParentBytes, FileNameStartPoint + i * &H14, WrittenFileArray, FileNameStartPoint + i * &H14, &H14)
                        'We don't have to change the file information either
                        Buffer.BlockCopy(ParentBytes, FileInformationStartPoint + i * &HC, WrittenFileArray, FileInformationStartPoint + i * &HC, &HC)
                    ElseIf i = NodeLocation Then
                        'here we want to skip this and abandon these bytes
                    Else 'this is after the file is removed
                        'Here we keep the name but wee adjust the reference for the File Information Reference Offset
                        Buffer.BlockCopy(ParentBytes, FileNameStartPoint + i * &H14, WrittenFileArray, FileNameStartPoint + (i - 1) * &H14, &HC)
                        Buffer.BlockCopy(BitConverter.GetBytes(CUInt(i * &HC)), 0, WrittenFileArray, FileNameStartPoint + (i - 1) * &H14 + &HC, 4)
                        Buffer.BlockCopy(ParentBytes, FileNameStartPoint + i * &H14 + &H10, WrittenFileArray, FileNameStartPoint + (i - 1) * &H14 + &H10, 4)
                        'for the file information we need to adjust the starting offset
                        Dim TempIndex As UInteger = BitConverter.ToUInt32(ParentBytes, FileInformationStartPoint + i * &HC)
                        Buffer.BlockCopy(BitConverter.GetBytes(CUInt(TempIndex + (SizeDifference / &H800))),
                                         0, WrittenFileArray, FileInformationStartPoint + (i - 1) * &HC, 4)
                        'Length stays the same in the Reference Offset
                        Buffer.BlockCopy(ParentBytes, FileInformationStartPoint + i * &HC + 4, WrittenFileArray, FileInformationStartPoint + (i - 1) * &HC + 4, 8)
                    End If
                Next

#End Region

#Region "Case PackageType.EPK8"

            Case PackageType.EPK8
                'Check if Directory Index = -1 if it is then we are deleting a whole directory
                If DirectoryIndex = -1 Then
                    'We are deleting an entire directory
                    'Copy the start header, we will overwrite some of these bytes
                    Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H10)
                    'Header Length Adjustment
                    Dim SubHeaderLength As UInt32 = BitConverter.ToUInt32(ParentBytes, 4)
                    Dim index As Integer = 0
                    Dim CurrentDirectoryCount As Integer = 0
                    Dim ShortenedHeaderLength As UInt32 = SubHeaderLength
                    Do While index < SubHeaderLength - 1
                        Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(ParentBytes, &H800 + index + 4) / 4
                        If NodeLocation = CurrentDirectoryCount Then
                            ShortenedHeaderLength -= ((DirectoryContainsCount * &H10) + &HC)
                            Exit Do
                        Else
                            CurrentDirectoryCount += 1
                            index += &HC + DirectoryContainsCount * &H10
                        End If
                    Loop
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(ShortenedHeaderLength)), 0, WrittenFileArray, 4, 4)
                    'File Length Adjustment
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - &H4800)), 0, WrittenFileArray, 8, 4)
                    'Now we want to update the individual files now that the starting header has been adjusted
                    Dim ReadingIndex As Integer = 0
                    Dim PlacementIndex As Integer = 0
                    CurrentDirectoryCount = 0
                    Do While ReadingIndex < SubHeaderLength - 1
                        Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(ParentBytes, &H800 + ReadingIndex + 4) / 4
                        If CurrentDirectoryCount < NodeLocation Then
                            'here we just need to copy the files over
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC + DirectoryContainsCount * &H10)
                            ReadingIndex += &HC + DirectoryContainsCount * &H10
                            PlacementIndex += &HC + DirectoryContainsCount * &H10
                        ElseIf CurrentDirectoryCount = NodeLocation Then
                            'we want to skip these bytes and offset the Reading Index and Placement Index by the length
                            ReadingIndex += &HC + DirectoryContainsCount * &H10
                        Else
                            ' CurrentDirectoryCount > NodeLocation
                            'We need to copy over the file information but shorten the index based on the deleted directory
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC + DirectoryContainsCount * &H10)
                            ReadingIndex += &HC
                            PlacementIndex += &HC
                            For i As Integer = 0 To DirectoryContainsCount - 1
                                'copying over the name and length of the file
                                Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &H10)
                                'here we want to read the index so we can adjust it
                                Dim OldIndex As UInt32 = BitConverter.ToUInt32(ParentBytes, &H800 + ReadingIndex + 8) * &H800
                                OldIndex += SizeDifference
                                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(OldIndex / &H800)), 0, WrittenFileArray, PlacementIndex + &H800 + 8, 4)
                                ReadingIndex += &H10
                                PlacementIndex += &H10
                            Next
                        End If
                        CurrentDirectoryCount += 1
                    Loop
                Else
                    'We are deleting a file within a directory
                    'Copy the start header, we will overwrite some of these bytes
                    Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H10)
                    'Header Length Adjustment
                    Dim SubHeaderLength As UInt32 = BitConverter.ToUInt32(WrittenFileArray, 4)
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(SubHeaderLength - &H10)), 0, WrittenFileArray, 4, 4)
                    'File Length Adjustment
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - &H4800)), 0, WrittenFileArray, 8, 4)
                    'Now we want to update the individual files now that the starting header has been adjusted
                    Dim ReadingIndex As Integer = 0
                    Dim PlacementIndex As Integer = 0
                    Dim CurrentDirectoryCount As Integer = 0
                    Do While ReadingIndex < SubHeaderLength - 1
                        Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(ParentBytes, &H800 + ReadingIndex + 4) / 4
                        If CurrentDirectoryCount < DirectoryIndex Then
                            'We just need to copy the information since it did not change at all
                            Buffer.BlockCopy(ParentBytes, ReadingIndex, WrittenFileArray, PlacementIndex, &HC + DirectoryContainsCount * &H10)
                            ReadingIndex += &HC + DirectoryContainsCount * &H10
                            PlacementIndex += &HC + DirectoryContainsCount * &H10
                        ElseIf CurrentDirectoryCount = DirectoryIndex Then
                            'Here we want to copy the header so we have the name moved, we will overwrite the length
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                            'Here we want to adjust the directory length because we are removing a file from it.
                            Buffer.BlockCopy(BitConverter.GetBytes(CUShort((DirectoryContainsCount - 1) * 4)), 0, WrittenFileArray, &H800 + ReadingIndex + 4, 2)
                            'We want to skip these bytes and offset the Reading Index and Placement Index by the length
                            ReadingIndex += &HC
                            PlacementIndex += &HC
                            For i As Integer = 0 To DirectoryContainsCount - 1
                                If i < NodeLocation Then
                                    'Copy the information index do not have to be changed
                                    Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                                    ReadingIndex += &H10
                                    PlacementIndex += &H10
                                ElseIf i = NodeLocation Then
                                    'this is the File we are skipping so we want to adjust the reading index
                                    ReadingIndex += &H10
                                Else 'i > NodeLocation
                                    'copying over the name and length of the file
                                    Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &H10)
                                    'here we want to read the index so we can adjust it
                                    Dim OldIndex As UInt32 = BitConverter.ToUInt32(ParentBytes, &H800 + ReadingIndex + 8) * &H800
                                    OldIndex += SizeDifference
                                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(OldIndex / &H800)), 0, WrittenFileArray, PlacementIndex + &H800 + 8, 4)
                                    ReadingIndex += &H10
                                    PlacementIndex += &H10
                                End If
                            Next
                        Else 'CurrentDirectoryCount > DirectoryIndex
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC + DirectoryContainsCount * &H10)
                            ReadingIndex += &HC + DirectoryContainsCount * &H10
                            PlacementIndex += &HC + DirectoryContainsCount * &H10
                            For i As Integer = 0 To DirectoryContainsCount - 1
                                'copying over the name and length of the file
                                Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &H10)
                                'here we want to read the index so we can adjust it
                                Dim OldIndex As UInt32 = BitConverter.ToUInt32(ParentBytes, &H800 + ReadingIndex + 8) * &H800
                                OldIndex += SizeDifference
                                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(OldIndex / &H800)), 0, WrittenFileArray, PlacementIndex + &H800 + 8, 4)
                                ReadingIndex += &H10
                                PlacementIndex += &H10
                            Next
                        End If
                    Loop
                End If

#End Region

#Region "Case PackageType.EPAC"

            Case PackageType.EPAC
                'Check if Directory Index = -1 if it is then we are deleting a whole directory
                If DirectoryIndex = -1 Then
                    'We are deleting an entire directory
                    'Copy the start header, we will overwrite some of these bytes
                    Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H10)
                    'Header Length Adjustment
                    Dim SubHeaderLength As UInt32 = BitConverter.ToUInt32(ParentBytes, 4)
                    Dim index As Integer = 0
                    Dim CurrentDirectoryCount As Integer = 0
                    Dim ShortenedHeaderLength As UInt32 = SubHeaderLength
                    Do While index < SubHeaderLength - 1
                        Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(ParentBytes, &H800 + index + 4) / 3
                        If NodeLocation = CurrentDirectoryCount Then
                            ShortenedHeaderLength -= ((DirectoryContainsCount * &HC) + &HC)
                            Exit Do
                        Else
                            CurrentDirectoryCount += 1
                            index += &HC + DirectoryContainsCount * &HC
                        End If
                    Loop
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(ShortenedHeaderLength)), 0, WrittenFileArray, 4, 4)
                    'File Length Adjustment
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - &H4800)), 0, WrittenFileArray, 8, 4)
                    'Now we want to update the individual files now that the starting header has been adjusted
                    Dim ReadingIndex As Integer = 0
                    Dim PlacementIndex As Integer = 0
                    CurrentDirectoryCount = 0
                    Do While ReadingIndex < SubHeaderLength - 1
                        Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(ParentBytes, &H800 + ReadingIndex + 4) / 3
                        If CurrentDirectoryCount < NodeLocation Then
                            'here we just need to copy the files over
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC + DirectoryContainsCount * &HC)
                            ReadingIndex += &HC + DirectoryContainsCount * &HC
                            PlacementIndex += &HC + DirectoryContainsCount * &HC
                        ElseIf CurrentDirectoryCount = NodeLocation Then
                            'we want to skip these bytes and offset the Reading Index and Placement Index by the length
                            ReadingIndex += &HC + DirectoryContainsCount * &HC
                        Else
                            ' CurrentDirectoryCount > NodeLocation
                            'We need to copy over the file information but shorten the index based on the deleted directory
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC + DirectoryContainsCount * &HC)
                            ReadingIndex += &HC
                            PlacementIndex += &HC
                            For i As Integer = 0 To DirectoryContainsCount - 1
                                'copying over the name and length of the file
                                Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                                'here we want to read the index so we can adjust it
                                Dim OldIndex As UInt32 = BitConverter.ToUInt32(ParentBytes, &H800 + ReadingIndex + 4) * &H800
                                OldIndex += SizeDifference
                                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(OldIndex / &H800)), 0, WrittenFileArray, PlacementIndex + &H800 + 4, 4)
                                ReadingIndex += &HC
                                PlacementIndex += &HC
                            Next
                        End If
                        CurrentDirectoryCount += 1
                    Loop
                Else
                    'We are deleting a file within a directory
                    'Copy the start header, we will overwrite some of these bytes
                    Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H10)
                    'Header Length Adjustment
                    Dim SubHeaderLength As UInt32 = BitConverter.ToUInt32(WrittenFileArray, 4)
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(SubHeaderLength - &HC)), 0, WrittenFileArray, 4, 4)
                    'File Length Adjustment
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - &H4800)), 0, WrittenFileArray, 8, 4)
                    'Now we want to update the individual files now that the starting header has been adjusted
                    Dim ReadingIndex As Integer = 0
                    Dim PlacementIndex As Integer = 0
                    Dim CurrentDirectoryCount As Integer = 0
                    Do While ReadingIndex < SubHeaderLength - 1
                        Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(ParentBytes, &H800 + ReadingIndex + 4) / 3
                        If CurrentDirectoryCount < DirectoryIndex Then
                            'We just need to copy the information since it did not change at all
                            Buffer.BlockCopy(ParentBytes, ReadingIndex, WrittenFileArray, PlacementIndex, &HC + DirectoryContainsCount * &HC)
                            ReadingIndex += &HC + DirectoryContainsCount * &HC
                            PlacementIndex += &HC + DirectoryContainsCount * &HC
                        ElseIf CurrentDirectoryCount = DirectoryIndex Then
                            'Here we want to copy the header so we have the name moved, we will overwrite the length
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                            'Here we want to adjust the directory length because we are removing a file from it.
                            Buffer.BlockCopy(BitConverter.GetBytes(CUShort((DirectoryContainsCount - 1) * 4)), 0, WrittenFileArray, &H800 + ReadingIndex + 4, 2)
                            'We want to skip these bytes and offset the Reading Index and Placement Index by the length
                            ReadingIndex += &HC
                            PlacementIndex += &HC
                            For i As Integer = 0 To DirectoryContainsCount - 1
                                If i < NodeLocation Then
                                    'Copy the information index do not have to be changed
                                    Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                                    ReadingIndex += &HC
                                    PlacementIndex += &HC
                                ElseIf i = NodeLocation Then
                                    'this is the File we are skipping so we want to adjust the reading index
                                    ReadingIndex += &HC
                                Else 'i > NodeLocation
                                    'copying over the name and length of the file
                                    Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                                    'here we want to read the index so we can adjust it
                                    Dim OldIndex As UInt32 = BitConverter.ToUInt32(ParentBytes, &H800 + ReadingIndex + 4) * &H800
                                    OldIndex += SizeDifference
                                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(OldIndex / &H800)), 0, WrittenFileArray, PlacementIndex + &H800 + 4, 4)
                                    ReadingIndex += &HC
                                    PlacementIndex += &HC
                                End If
                            Next
                        Else 'CurrentDirectoryCount > DirectoryIndex
                            Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC + DirectoryContainsCount * &HC)
                            ReadingIndex += &HC + DirectoryContainsCount * &HC
                            PlacementIndex += &HC + DirectoryContainsCount * &HC
                            For i As Integer = 0 To DirectoryContainsCount - 1
                                'copying over the name and length of the file
                                Buffer.BlockCopy(ParentBytes, ReadingIndex + &H800, WrittenFileArray, PlacementIndex + &H800, &HC)
                                'here we want to read the index so we can adjust it
                                Dim OldIndex As UInt32 = BitConverter.ToUInt32(ParentBytes, &H800 + ReadingIndex + 8) * &H800
                                OldIndex += SizeDifference
                                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(OldIndex / &H800)), 0, WrittenFileArray, PlacementIndex + &H800 + 8, 4)
                                ReadingIndex += &HC
                                PlacementIndex += &HC
                            Next
                        End If
                    Loop
                End If

#End Region

#Region "Case PackageType.SHDC"

            Case PackageType.SHDC
                'Copying the Starting Container Header we will update parts of this as needed
                Array.Copy(ParentBytes, 0, WrittenFileArray, 0, &H40)
                'Here we need to get the file information and meta data header starts/lengths
                Dim MetaDataLength As Integer = BitConverter.ToUInt32(ParentBytes, &H18)
                Dim FileInformationStart As Integer = BitConverter.ToUInt32(ParentBytes, &H1C)
                Dim FileInformationLength As Integer = BitConverter.ToUInt32(ParentBytes, &H20)
                Dim MetaDataStart As Integer = &H40
                If FileInformationStart < MetaDataLength Then
                    FileInformationStart = MetaDataLength + &H10 + &H40
                    If FileInformationStart Mod &H10 > 0 Then
                        FileInformationStart = MetaDataLength + &H50
                        MetaDataStart = &H50
                    End If
                End If
                Dim PachPartsCount As Integer = (FileInformationLength / &H10) '1 index
                'Adjust meta data Length
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(MetaDataLength - &H14)), 0, WrittenFileArray, &H18, 4)
                'Adjust File Information Length
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(FileInformationLength - &H10)), 0, WrittenFileArray, &H20, 4)
                'Adjust File Count
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(PachPartsCount - 1)), 0, WrittenFileArray, &H38, 4)
                'Update Full File Length After Header
                Buffer.BlockCopy(BitConverter.GetBytes(CUInt(WrittenFileArray.Length - FileInformationStart - FileInformationLength)), 0, WrittenFileArray, &H3C, 4)
                'We need to adjust the header bytes slightly differently if is a newer SHDC format
                If MetaDataStart = &H50 Then
                    'the File Information Start bytes is something I don't know about really... RESEARCH NEEDED
                ElseIf MetaDataStart = &H40 Then
                    'We need to update the File Information Start bytes
                    Buffer.BlockCopy(BitConverter.GetBytes(CUInt(FileInformationStart - &H14)), 0, WrittenFileArray, &H1C, 4)
                End If
                'Here we can now copy over the Meta information and File Bytes
                For i As Integer = 0 To PachPartsCount - 1
                    If i < NodeLocation Then
                        'we need to update the file information to foreshorten the index based of the header reduction
                        'Here we will copy the "meta data" part
                        Array.Copy(ParentBytes, CUInt(MetaDataStart + i * &H14), WrittenFileArray, CUInt(MetaDataStart + i * &H14), &H14)
                        'We want to read the index subtract the header adjustment
                        Array.Copy(ParentBytes, FileInformationStart + i * &H10, WrittenFileArray, FileInformationStart + i * &H10 - &H14, &H10)
                        Dim SubItemIndex As Integer = BitConverter.ToUInt32(ParentBytes, FileInformationStart + (i * &H10) + &H4)
                        Buffer.BlockCopy(BitConverter.GetBytes(CUInt(SubItemIndex - &H24)), 0, WrittenFileArray, FileInformationStart + (i * &H10) + &H4 - &H14, 4)
                    ElseIf i = NodeLocation Then
                        'This is the file we are skipping over
                    Else 'this is after the file is removed
                        'We need to place the information 1 file earlier than it was initially.
                        'Here we will copy the "meta data" part
                        Array.Copy(ParentBytes, CUInt(MetaDataStart + i * &H14), WrittenFileArray, CUInt(MetaDataStart + (i - 1) * &H14), &H14)
                        'We want to read the index subtract the header adjustment
                        Array.Copy(ParentBytes, CUInt(FileInformationStart + i * &H10), WrittenFileArray, CUInt(FileInformationStart + (i - 1) * &H10 - &H14), &H10)
                        Dim SubItemIndex As Integer = BitConverter.ToUInt32(ParentBytes, FileInformationStart + (i * &H10) + &H4)
                        'we need to reduce the index by the deleted file length as well here
                        Buffer.BlockCopy(BitConverter.GetBytes(CUInt(SubItemIndex - &H24 + SizeDifference)), 0, WrittenFileArray, FileInformationStart + ((i - 1) * &H10 + &H4 - &H14), 4)
                    End If
                Next

#End Region

#Region "Case PackageType.TextureLibrary"

            Case PackageType.TextureLibrary
                'Code the injection here..
                'start with the header fixes and inject each part of the file
                Dim FileCount As Integer = ParentBytes(0)
                Dim BytesRevesed As Boolean = False
                If FileCount = 0 Then 'if this is 0 then we are dealing with a reverse byte system header.
                    FileCount = ParentBytes(3)
                    BytesRevesed = True
                End If
                'Texture Library has no total length to adjust
                'Copy the start header, we will overwrite some of these bytes
                Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H10)
                If BytesRevesed Then
                    WrittenFileArray(3) = FileCount - 1
                Else
                    WrittenFileArray(0) = FileCount - 1
                End If
                For i As Integer = 0 To FileCount - 1
                    If i < NodeLocation Then
                        'we need to take &h20 off the index because of the header change
                        Buffer.BlockCopy(ParentBytes, &H10 + i * &H20, WrittenFileArray, &H10 + i * &H20, &H20)
                        If BytesRevesed Then
                            Dim TempItemIndex As Integer = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, &H10 + i * &H20 + &H18), 0)
                            Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(TempItemIndex - &H20))), 0, WrittenFileArray, i * &H20 + &H10 + &H18, 4)
                        Else
                            Dim TempItemIndex As Integer = BitConverter.ToUInt32(ParentBytes, &H10 + i * &H20 + &H18)
                            Array.Copy(BitConverter.GetBytes(CUInt(TempItemIndex - &H20)), 0, WrittenFileArray, i * &H20 + &H10 + &H18, 4)
                        End If
                    ElseIf i = NodeLocation Then
                        'these are the bytes we skip
                    Else 'size stays but index changes
                        Buffer.BlockCopy(ParentBytes, &H10 + i * &H20, WrittenFileArray, &H10 + (i - 1) * &H20, &H20)
                        If BytesRevesed Then
                            Dim OldIndex As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, i * &H20 + &H10 + &H18, 4), 0)
                            Dim TempIndex As UInt64 = OldIndex + SizeDifference - &H20
                            Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(TempIndex))), 0, WrittenFileArray, (i - 1) * &H20 + &H10 + &H18, 4)
                        Else
                            Dim OldIndex As UInt32 = BitConverter.ToUInt64(ParentBytes, i * &H20 + &H10 + &H18)
                            Dim TempIndex As UInt64 = OldIndex + SizeDifference - &H20
                            Array.Copy(BitConverter.GetBytes(CLng(TempIndex)), 0, WrittenFileArray, (i - 1) * &H20 + &H10 + &H18, 8)
                        End If
                    End If
                Next

#End Region

#Region "Case PackageType.YANMPack"

            Case PackageType.YANMPack
                'Copy the start header, we will overwrite some of these bytes
                Buffer.BlockCopy(ParentBytes, 0, WrittenFileArray, 0, &H70)
                'First we need to reduce the Header Length by &h28 -
                Dim SubHeaderLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, 0), 0)
                Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(SubHeaderLength - &H28))), 0, WrittenFileArray, 0, 4)
                'Next bytes are the full file length sans header
                Dim YANMLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, 4), 0)
                Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(YANMLength + SizeDifference))), 0, WrittenFileArray, 4, 4)
                'Next is the header length which should be alway &h20 so we don't need to edit that.
                'After that is the full combined header length
                Dim CombinedHeaderLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, &HC), 0)
                Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(CombinedHeaderLength + HeaderAdjustment))), 0, WrittenFileArray, &HC, 4)
                'we have a lot of static bytes for the rest of the bytes of the header only changing byte is the contained bytes.
                Dim ContainedParts As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, &H48), 0)
                Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(ContainedParts - 1))), 0, WrittenFileArray, &H48, 4)
                For i As Integer = 0 To ContainedParts - 1
                    If i < NodeLocation Then
                        'We want to copy the bytes no adjustment needed
                        Buffer.BlockCopy(ParentBytes, &H70 + i * &H28, WrittenFileArray, &H70 + i * &H28, &H28)
                    ElseIf i = NodeLocation Then
                        'this is now the information we want to skip
                    Else
                        'first we want to copy the bytes over to the right location.
                        Buffer.BlockCopy(ParentBytes, &H70 + i * &H28, WrittenFileArray, &H70 + (i - 1) * &H28, &H28)
                        'so now we will need to update some file index information
                        Dim TempIndex As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(ParentBytes, &H70 + i * &H28 + &H24), 0)
                        Array.Copy(GeneralTools.EndianReverse(BitConverter.GetBytes(CUInt(TempIndex + SizeDifference))), 0, WrittenFileArray, &H70 + i * &H28 + &H24, 4)
                    End If
                Next
        End Select

#End Region

#End Region

        Dim ReturnTest As Boolean = True
        If ParentFileInformation.Index = 0 AndAlso
            ParentFileInformation.StoredData.Length = 0 Then
            'File to be Written
            'TO DO Sent this Written File code to a function as it is copied several times..
            Dim WrittenFile As String = FileRequested.FullFilePath
            If My.Settings.BackupInjections AndAlso GeneralTools.CheckFileWriteable(WrittenFile & ".bak", False) Then
                File.Copy(WrittenFile, WrittenFile & ".bak", True)
            End If
            File.WriteAllBytes(WrittenFile, WrittenFileArray)
            'Remove Save Pending Buttons when file written
            MainForm.SaveFileNoLongerPending()
        Else
            'we must go higher
            ReturnTest = InjectBytesIntoFile(ParentFileInformation, WrittenFileArray)
        End If
        Return ReturnTest
    End Function

#End Region

End Class