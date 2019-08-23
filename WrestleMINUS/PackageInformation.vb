Imports System.IO   'Files
Imports System.Text 'Text Encoding

Public Class PackageInformation

#Region "Package Properties Functions"

    Shared ReadOnly LibraryContainsList As String() = New String() {"dds",
    "ymx",
    "tex",
    "map",
    "hkx",
    "pac",
    "txt",
    "cvx",
    "bin",
    "tpl"} ' list adds files from ufc undisputed.

    Shared Function CheckExpandable(TestType As PackageType) As Boolean
        If TestType = PackageType.Unchecked OrElse
           TestType = PackageType.Folder OrElse
           TestType = PackageType.HSPC OrElse
           TestType = PackageType.EPK8 OrElse
           TestType = PackageType.EPAC OrElse
           TestType = PackageType.SHDC OrElse
           TestType = PackageType.PACH OrElse
            TestType = PackageType.BPE OrElse
           TestType = PackageType.ZLIB OrElse
           TestType = PackageType.OODL OrElse
           TestType = PackageType.PachDirectory_4 OrElse
           TestType = PackageType.PachDirectory_8 OrElse
           TestType = PackageType.TextureLibrary OrElse
           TestType = PackageType.YANMPack Then
            Return True
        End If
        Return False
    End Function

    Shared Function GetImageIndex(SentType As PackageType) As UInt32
        Select Case SentType
            Case PackageType.Folder
                Return 1
            Case PackageType.HSPC
                Return 2
            Case PackageType.EPK8
                Return 3
            Case PackageType.EPAC
                Return 4
            Case PackageType.SHDC
                Return 5
            Case PackageType.SoundReference
                Return 5
            Case PackageType.PACH
                Return 6
            Case PackageType.PachDirectory_4
                Return 6
            Case PackageType.PachDirectory_8
                Return 6
            Case PackageType.BPE
                Return 7
            Case PackageType.ZLIB
                Return 8
            Case PackageType.OODL
                Return 9
            Case PackageType.TextureLibrary
                Return 10
            Case PackageType.TPL
                Return 10
            Case PackageType.YANMPack
                Return 11
            Case PackageType.YANM
                Return 11
            Case PackageType.OFOP
                Return 11
            Case PackageType.YOBJ
                Return 12
            Case PackageType.YOBJArray
                Return 12
            Case PackageType.StringFile
                Return 13
            Case PackageType.DDS
                Return 14
            Case PackageType.DUMY
                Return 14
            Case PackageType.ArenaInfo
                Return 15
            Case PackageType.ShowInfo
                Return 16
            Case PackageType.NIBJ
                Return 17
            Case PackageType.bk2
                Return 18
            Case PackageType.CostumeFile
                Return 19
            Case PackageType.MuscleFile
                Return 20
            Case PackageType.UFC_PAC
                Return 20
            Case PackageType.UFC_BIN
                Return 20
            Case PackageType.UFC_CVX
                Return 20
            Case PackageType.UFC_HKX
                Return 20
            Case PackageType.UFC_MAP
                Return 20
            Case PackageType.UFC_TXT
                Return 20
            Case PackageType.MaskFile
                Return 21
            Case PackageType.VMUM
                Return 22
            Case PackageType.TitleFile
                Return 23
            Case PackageType.TextureReference
                Return 24
            Case PackageType.LSD
                Return 25
            Case PackageType.LSD_BIN
                Return 25
            Case Else
                Return 0
        End Select
    End Function

    Shared Function CheckInjectable(TestType As PackageType) As Boolean
        'Hopefully this can expand to all
        If TestType = PackageType.HSPC OrElse
           TestType = PackageType.EPK8 OrElse
           TestType = PackageType.EPAC OrElse
           TestType = PackageType.SHDC OrElse
           TestType = PackageType.PACH OrElse
           TestType = PackageType.PachDirectory_4 OrElse
           TestType = PackageType.PachDirectory_8 OrElse
           TestType = PackageType.TextureLibrary OrElse
           TestType = PackageType.ZLIB OrElse
           TestType = PackageType.OODL OrElse
           TestType = PackageType.BPE OrElse
           TestType = PackageType.YANMPack Then
            Return True
        End If
        Return False
    End Function

    Shared Function ValidateTruncation(TestedString As String, ContainerType As PackageType) As String
        Select Case ContainerType
            Case PackageType.HSPC
                Return TestedString.PadLeft(16, "0").ToUpper
            Case PackageType.SHDC
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.EPK8
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.EPAC
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.PachDirectory_4
                Return TestedString.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0").ToUpper
            Case PackageType.PachDirectory_8
                Return TestedString.PadLeft(My.Settings.DecimalNameMinLength, "0").ToUpper
            Case PackageType.TextureLibrary
                Return TestedString.Trim(" ")
            Case PackageType.Folder
                Return TestedString.Trim(" ")
            Case PackageType.EditingFileName
                Return TestedString.Trim(" ")
            Case Else
                Return TestedString
        End Select
    End Function

#End Region

#Region "File Reading / Processing"

    Shared Function CheckHeaderType(Index As Long, ByVal ByteArray As Byte(), Optional FileNamePath As String = "") As PackageType
        'To be split into 2 separate functions once all processes are added
        Dim FirstFour As String
        'Make sure the file has bytes
        If ByteArray.Length = 0 Then
            Return PackageType.bin
        End If
        If Index > ByteArray.Length Then
            Return PackageType.bin
        End If
        FirstFour = Encoding.Default.GetChars(ByteArray, Index, 4)
        Select Case FirstFour
            Case "HSPC"
                Return PackageType.HSPC
            Case "SHDC"
                Return PackageType.SHDC
            Case "EPK8"
                Return PackageType.EPK8
            Case "PACH"
                Return PackageType.PACH
            Case "EPAC"
                Return PackageType.EPAC
            Case "ZLIB"
                Return PackageType.ZLIB
            Case "YOBJ"
                Return PackageType.YOBJ
            Case "JBOY"
                Return PackageType.YOBJ
            Case "NIBJ"
                Return PackageType.NIBJ
            Case "0FOP"
                Return PackageType.OFOP
            Case "YANM"
                Return PackageType.YANM
            Case "OODL"
                Return PackageType.OODL
            Case "VMUM"
                Return PackageType.VMUM
            Case "DUMY"
                Return PackageType.DUMY
            Case "OneT"
                Return PackageType.TextureReference
            Case Else
                'if we don't have a perfect 4 match we go to check the 3 character matches
                Select Case True
                    Case FirstFour.Contains("STG")
                        Return PackageType.ShowInfo
                    Case FirstFour.Contains("DDS")
                        Return PackageType.DDS
                    Case FirstFour.Contains("KB2")
                        Return PackageType.bk2
                    Case FirstFour.Contains("COS")
                        Return PackageType.CostumeFile
                    Case FirstFour.Contains("BPE")
                        Return PackageType.BPE
                    Case FirstFour.Contains("ê¡Y")
                        Return PackageType.YOBJArray
                    Case FirstFour.Contains(" ¯0")
                        Return PackageType.TPL
                    Case FirstFour.Substring(0, 1).Contains("g")
                        Return PackageType.SoundReference
                    Case Else
                        'if we do not have a header text to guide us we have some additional text checks that are consistent.
                        If ByteArray.Length > &H20 Then
                            Dim ShortNumberCheck As UInt32 = BitConverter.ToUInt16(ByteArray, Index + 0)
                            If ShortNumberCheck > 0 Then
                                If New String(Encoding.Default.GetChars(ByteArray, Index + 4, &HC)) = "¾ï¾ï¾ï¾ï¾ï¾ï" Then
                                    Return PackageType.LSD_BIN
                                ElseIf New String(Encoding.Default.GetChars(ByteArray, Index + 8, &HC)) = "ÿÿÿÿÿÿÿÿÿÿÿÿ" Then
                                    Return PackageType.LSD
                                ElseIf New String(Encoding.Default.GetChars(ByteArray, Index + &H14, 8)) = "ÿÿÿÿÿÿÿÿ" Then
                                    Return PackageType.LSD
                                End If
                            End If
                        End If
                            'some of these checks don't 100% require this many bytes, but none should functionally have that little byte length
                            If ByteArray.Length > &H30 Then
                            Select Case True
                                Case Encoding.Default.GetChars(ByteArray, Index + &H10, 4) = "aren"
                                    Return PackageType.ArenaInfo
                                Case Encoding.Default.GetChars(ByteArray, Index + &H14, 6) = "M_Head"
                                    Return PackageType.MaskFile
                                Case Encoding.Default.GetChars(ByteArray, Index + &H14, 6) = "M_Body"
                                    Return PackageType.MaskFile
                                Case BitConverter.ToUInt32(ByteArray, Index + &H14) = 2004 'this is an unsafe check.  It just works for all known games so far.
                                    Return PackageType.TitleFile
                                Case Encoding.Default.GetChars(ByteArray, Index + &H18, 3) = "yM_"
                                    Return PackageType.MuscleFile
                                Case LibraryContainsList.Contains(Encoding.Default.GetChars(ByteArray, Index + &H20, 3)) 'ufc games can have additional file types  create a short list of 3 char codes to test
                                    Return PackageType.TextureLibrary
                                Case Encoding.Default.GetChars(ByteArray, Index + &H24, 4) = "root"
                                    Return PackageType.YANMPack
                                Case FileNamePath.ToLower.Contains("string")
                                    Dim NumberCheck As UInt32 = BitConverter.ToUInt32(ByteArray, Index + 0)
                                    If NumberCheck = 0 Then
                                        Return PackageType.StringFile
                                    Else
                                        Return PackageType.bin
                                    End If
                                Case Else
                                    Return PackageType.bin
                            End Select
                        Else
                            Return PackageType.bin
                        End If
                End Select
        End Select
    End Function

    'Function can be split to an unchecked and a function returning a list of file information
    Shared Sub GetFileParts(ByRef ParentFileProperties As ExtendedFileProperties,
                            Optional Crawl As Boolean = False,
                            Optional TriggerProgress As Boolean = False)
        'Here we are using the ref of parent file so we can edit the sub files.
        Dim FileBytes As Byte() = FilePartHandlers.GetFilePartBytes(ParentFileProperties)
        Select Case ParentFileProperties.FileType
            Case PackageType.Folder
                'TO DO Add in a progress trigger back to the MainForm.
                Dim ListofSubDirectores() As String = Directory.GetDirectories(ParentFileProperties.FullFilePath)
                If ListofSubDirectores.Count > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                    For Each SubDirectory As String In ListofSubDirectores
                        Dim NewDI As DirectoryInfo = New DirectoryInfo(SubDirectory)
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = NewDI.Name,
                            .FullFilePath = NewDI.FullName,
                            .FileType = PackageType.Folder,
                            .Index = 0,
                            .length = 0,
                            .StoredData = New Byte() {},
                            .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Next
                End If
                If Not IsNothing(ParentFileProperties.SubFiles) Then
                    'crawl the sub directories.  Crawling the files.. greatly increases time loading
                    For i As Integer = 0 To ParentFileProperties.SubFiles.Count - 1
                        GetFileParts(ParentFileProperties.SubFiles(i), Crawl)
                    Next
                End If
                Dim ListofFiles() As String = Directory.GetFiles(ParentFileProperties.FullFilePath)
                If ListofFiles.Count > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                    For Each FilePath As String In ListofFiles
                        Dim NewFI As FileInfo = New FileInfo(FilePath)
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = NewFI.Name,
                            .FullFilePath = NewFI.FullName,
                            .FileType = PackageType.Unchecked,
                            .Index = 0,
                            .length = FileLen(NewFI.FullName),
                            .StoredData = New Byte() {},
                            .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Next
                End If
            Case PackageType.Unchecked
                ParentFileProperties.FileType = CheckHeaderType(0, FileBytes, ParentFileProperties.FullFilePath)
                ParentFileProperties.StoredData = New Byte() {}
                GetFileParts(ParentFileProperties, Crawl)
                Exit Sub 'Skips the crawler at the bottom and duping the host node update

#Region "Primary Container Types {PAC}"

            Case PackageType.HSPC
                Dim FileCount As Integer = BitConverter.ToUInt32(FileBytes, &H38)
                Dim FileNameLength As Integer = BitConverter.ToUInt32(FileBytes, &H18)
                FileNameLength += -(FileNameLength Mod &H800) + &H1000
                If FileCount > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To FileCount - 1
                    'This will be the full length hex name always so TruncateDecimalNames is unneeded
                    Dim FileName As String = BitConverter.ToString(FileBytes, &H800 + i * &H14, 8).ToUpper.Replace("-", "")
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = FileName,
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = BitConverter.ToUInt32(FileBytes, FileNameLength + i * &HC) * &H800 + ParentFileProperties.Index,
                        .length = BitConverter.ToUInt32(FileBytes, FileNameLength + i * &HC + &H4) * &H100,
                        .StoredData = ParentFileProperties.StoredData,
                        .FileType = CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                Next
            Case PackageType.EPK8
                Dim HeaderLength As Integer = BitConverter.ToUInt32(FileBytes, 4)
                If HeaderLength > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                Dim index As Integer = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4) 'we cant trim spaces because it would mess with re-injection if the name was edited
                    Dim DirectoryFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = DirectoryName,
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .StoredData = ParentFileProperties.StoredData,
                        .FileType = PackageType.PachDirectory_8,
                        .SubFiles = New List(Of ExtendedFileProperties),
                        .Parent = ParentFileProperties}
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(FileBytes, &H800 + index + 4) / 4
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        Dim PachName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 8)
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = PachName,
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .Index = BitConverter.ToUInt32(FileBytes, &H800 + index + 8) * &H800 + &H4000 + ParentFileProperties.Index,
                            .length = BitConverter.ToUInt32(FileBytes, &H800 + index + 12) * &H100,
                            .StoredData = ParentFileProperties.StoredData,
                            .FileType = PackageInformation.CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                            .Parent = DirectoryFileProperties}
                        If i = 0 Then
                            DirectoryFileProperties.Index = ContainedFileProperties.Index
                        End If
                        DirectoryFileProperties.length += ContainedFileProperties.length
                        index += &H10
                        DirectoryFileProperties.SubFiles.Add(ContainedFileProperties)
                    Next
                    ParentFileProperties.SubFiles.Add(DirectoryFileProperties)
                Loop
            Case PackageType.EPAC
                Dim HeaderLength As Integer = BitConverter.ToUInt32(FileBytes, 4)
                If HeaderLength > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                Dim index As Integer = 0
                Do While index < HeaderLength - 1
                    Dim DirectoryName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4) 'we cant trim spaces because it would mess with re-injection if the name was edited
                    Dim DirectoryFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = DirectoryName,
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .StoredData = ParentFileProperties.StoredData,
                        .FileType = PackageType.PachDirectory_4,
                        .SubFiles = New List(Of ExtendedFileProperties),
                        .Parent = ParentFileProperties}
                    Dim DirectoryContainsCount As Integer = BitConverter.ToUInt16(FileBytes, &H800 + index + 4) / 3
                    index += &HC
                    For i As Integer = 0 To DirectoryContainsCount - 1
                        Dim PachName As String = Encoding.Default.GetChars(FileBytes, &H800 + index, 4)
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = PachName,
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .Index = BitConverter.ToUInt32(FileBytes, &H800 + index + 4) * &H800 + &H4000 + ParentFileProperties.Index,
                            .length = BitConverter.ToUInt32(FileBytes, &H800 + index + 8) * &H100,
                            .StoredData = ParentFileProperties.StoredData,
                            .FileType = PackageInformation.CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                            .Parent = DirectoryFileProperties}
                        If i = 0 Then
                            DirectoryFileProperties.Index = ContainedFileProperties.Index
                        End If
                        DirectoryFileProperties.length += ContainedFileProperties.length
                        index += &HC
                        DirectoryFileProperties.SubFiles.Add(ContainedFileProperties)
                    Next
                    ParentFileProperties.SubFiles.Add(DirectoryFileProperties)
                Loop

#End Region

#Region "Secondary Container Types {PACH}"

            Case PackageType.SHDC
                Dim TempHeaderCheck As Integer = BitConverter.ToUInt32(FileBytes, &H18)
                Dim TempHeaderStart As Integer = BitConverter.ToUInt32(FileBytes, &H1C)
                Dim TempHeaderLength As Integer = BitConverter.ToUInt32(FileBytes, &H20)
                If TempHeaderStart < TempHeaderCheck Then
                    TempHeaderStart = TempHeaderCheck + &H10 + &H40
                    If TempHeaderStart Mod &H10 > 0 Then
                        TempHeaderStart = TempHeaderStart + &H10 - (TempHeaderStart Mod &H10)
                    End If
                End If
                Dim PachPartsCount As Integer = (TempHeaderLength / &H10) '1 index
                If PachPartsCount > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To PachPartsCount - 1
                    Try
                        Dim PartName As String = Hex(BitConverter.ToUInt32(FileBytes, TempHeaderStart + (i * &H10)))
                        'MessageBox.Show(PartName)
                        If PartName = "FFFFFFFF" Then
                            Continue For
                        End If
                        PartName = PartName.PadLeft(My.Settings.DecimalNameMinLength, "0")
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = PartName,
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .Index = BitConverter.ToUInt32(FileBytes, TempHeaderStart + (i * &H10) + &H4) + ParentFileProperties.Index,
                            .length = BitConverter.ToUInt64(FileBytes, TempHeaderStart + (i * &H10) + &H8),
                            .StoredData = ParentFileProperties.StoredData,
                            .FileType = PackageInformation.CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                            .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message & vbNewLine &
                                        "Object Number: " & i & vbNewLine &
                                        "Header Start {hex}: " & Hex(TempHeaderStart))
                    End Try
                Next
            Case PackageType.PACH
                Dim Partcount As Integer = BitConverter.ToUInt32(FileBytes, 4)
                If Partcount > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To Partcount - 1
                    Dim PartName As String = Hex(BitConverter.ToUInt32(FileBytes, &H8 + (i * &HC)))
                    PartName = PartName.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0")
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = PartName,
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = BitConverter.ToUInt32(FileBytes, &HC + (i * &HC)) + &H8 + Partcount * &HC + ParentFileProperties.Index,
                        .length = BitConverter.ToUInt32(FileBytes, &H10 + (i * &HC)),
                        .StoredData = ParentFileProperties.StoredData,
                        .FileType = CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                Next
            Case PackageType.DUMY
                Dim HeaderLength As Integer = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, 4), 0)
                If HeaderLength > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                Dim PartName As String = Hex(0)
                PartName = PartName.PadLeft(Math.Min(4, My.Settings.DecimalNameMinLength), "0")
                Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                    .Name = PartName,
                    .FullFilePath = ParentFileProperties.FullFilePath,
                    .Index = &H8 + HeaderLength,
                    .length = FileBytes.Length - .Index,
                    .StoredData = ParentFileProperties.StoredData,
                    .FileType = CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                    .Parent = ParentFileProperties}
                ParentFileProperties.SubFiles.Add(ContainedFileProperties)

#End Region

#Region "Compression Types"

            Case PackageType.ZLIB
                ' Checking to make sure the node isn't already decompressed..
                If Not IsNothing(ParentFileProperties.SubFiles) Then
                    'MessageBox.Show(ParentFileProperties.Name & " Already Decompressed")
                    Exit Select
                Else
                    ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If PackUnpack.CheckIconicZlib() Then
                    UncompressedBytes = PackUnpack.GetUncompressedZlibBytes(FileBytes)
                End If
                If IsNothing(UncompressedBytes) Then
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ParentFileProperties.Name & " UNCOMPRESS",
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = 0,
                        .length = 0,
                        .StoredData = UncompressedBytes,
                        .FileType = PackageType.bin,
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Exit Sub
                Else
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ParentFileProperties.Name & " UNCOMPRESS",
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = 0,
                        .length = UncompressedBytes.Length,
                        .StoredData = UncompressedBytes,
                        .FileType = CheckHeaderType(.Index, UncompressedBytes, ParentFileProperties.FullFilePath),
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                End If
            Case PackageType.BPE
                ' Checking to make sure the node isn't already decompressed..
                If Not IsNothing(ParentFileProperties.SubFiles) Then
                    'MessageBox.Show(ParentFileProperties.Name & " Already Decompressed")
                    Exit Select
                Else
                    ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If PackUnpack.CheckUnrrbpe() Then
                    UncompressedBytes = PackUnpack.GetUncompressedBPEBytes(FileBytes)
                End If
                If IsNothing(UncompressedBytes) Then
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ParentFileProperties.Name & " UNCOMPRESS",
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = 0,
                        .length = 0,
                        .StoredData = UncompressedBytes,
                        .FileType = PackageType.bin,
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Exit Sub
                Else
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ParentFileProperties.Name & " UNCOMPRESS",
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = 0,
                        .length = UncompressedBytes.Length,
                        .StoredData = UncompressedBytes,
                        .FileType = CheckHeaderType(.Index, UncompressedBytes, ParentFileProperties.FullFilePath),
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                End If
            Case PackageType.OODL
                ' Checking to make sure the node isn't already decompressed..
                If Not IsNothing(ParentFileProperties.SubFiles) Then
                    'MessageBox.Show(ParentFileProperties.Name & " Already Decompressed")
                    Exit Select
                Else
                    ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If PackUnpack.CheckOodle() Then
                    UncompressedBytes = PackUnpack.GetUncompressedOodleBytes(FileBytes)
                End If
                If IsNothing(UncompressedBytes) Then
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ParentFileProperties.Name & " UNCOMPRESS",
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = 0,
                        .length = 0,
                        .StoredData = UncompressedBytes,
                        .FileType = PackageType.bin,
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Exit Sub
                Else
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ParentFileProperties.Name & " UNCOMPRESS",
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = 0,
                        .length = UncompressedBytes.Length,
                        .StoredData = UncompressedBytes,
                        .FileType = CheckHeaderType(.Index, UncompressedBytes, ParentFileProperties.FullFilePath),
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                End If

#End Region

#Region "Library Types"

            Case PackageType.TextureLibrary
                Dim TextureCount As Integer = FileBytes(0)
                Dim BytesRevesed As Boolean = False
                If TextureCount = 0 Then 'if this is 0 then we are dealing with a reverse byte system header.
                    TextureCount = FileBytes(3)
                    BytesRevesed = True
                End If
                If TextureCount > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To TextureCount - 1
                    Dim ImageName As String = Encoding.Default.GetChars(FileBytes, i * &H20 + &H10, &H10)
                    ImageName = ImageName.TrimEnd(Chr(0))
                    'Adding File Type Check
                    Dim FileTypeName As String = Encoding.Default.GetChars(FileBytes, i * &H20 + &H20, 3)
                    FileTypeName = FileTypeName.ToLower
                    Dim FileTypeContained As PackageType = PackageType.bin
                    Select Case FileTypeName
                        Case "dds"
                            FileTypeContained = PackageType.DDS
                        Case "ymx"
                            FileTypeContained = PackageType.YOBJ
                        Case "tex"
                            FileTypeContained = PackageType.TextureLibrary
                        Case "map"
                            FileTypeContained = PackageType.UFC_MAP
                        Case "hkx"
                            FileTypeContained = PackageType.UFC_HKX
                        Case "pac"
                            FileTypeContained = PackageType.UFC_PAC
                        Case "txt"
                            FileTypeContained = PackageType.UFC_TXT
                        Case "cvx"
                            FileTypeContained = PackageType.UFC_CVX
                        Case "bin"
                            FileTypeContained = PackageType.UFC_BIN
                        Case "tpl"
                            FileTypeContained = PackageType.TPL
                        Case Else
                            'MessageBox.Show("Missing Type " & FileTypeName)
                    End Select
                    'Here we need to get the File length and index in case the bytes are reversed
                    Dim CurrentItemLength As UInt32 = 0
                    Dim CurrentItemIndex As UInt32 = 0
                    If BytesRevesed Then
                        CurrentItemLength = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, i * &H20 + &H10 + &H14, 4), 0)
                        CurrentItemIndex = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, i * &H20 + &H10 + &H18, 4), 0) + ParentFileProperties.Index
                    Else
                        CurrentItemLength = BitConverter.ToUInt32(FileBytes, i * &H20 + &H10 + &H14)
                        CurrentItemIndex = BitConverter.ToUInt64(FileBytes, i * &H20 + &H10 + &H18) + ParentFileProperties.Index
                    End If
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ImageName,
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .length = CurrentItemLength,
                        .Index = CurrentItemIndex,
                        .StoredData = ParentFileProperties.StoredData,
                        .FileType = FileTypeContained,
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                Next
            Case PackageType.YANMPack
                Dim HeaderLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes), 0) + &H20
                Dim YANMLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, 4), 0)
                Dim HeadIndex As Integer = 0
                Dim partcount As Integer = 0
                If HeaderLength > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                Do While HeadIndex < HeaderLength
                    If HeadIndex = 0 Then
                        HeadIndex = &H70
                    End If
                    'getting the part name
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = Encoding.ASCII.GetString(FileBytes, HeadIndex + 4, 8),
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .Index = (BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, HeadIndex + &H24), 0)) + HeaderLength + ParentFileProperties.Index,
                        .FileType = PackageType.YANM,
                        .StoredData = ParentFileProperties.StoredData,
                        .Parent = ParentFileProperties}
                    If HeadIndex + &H20 + &H28 < HeaderLength Then
                        ContainedFileProperties.length = (BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, HeadIndex + &H24 + &H28), 0)) + HeaderLength - ContainedFileProperties.Index
                    Else
                        ContainedFileProperties.length = YANMLength - ContainedFileProperties.Index + HeaderLength
                    End If
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    partcount = partcount + 1
                    HeadIndex = HeadIndex + &H28
                Loop

#Region "To be Built"

            Case PackageType.YOBJ

#End Region

#End Region

#Region "Stand Alone Files"

                'Case PackageType.StringFile
                'Case PackageType.bin
                'Case PackageType.DDS
                'Case PackageType.YANM
                'Case PackageType.ArenaInfo
                'Case PackageType.ShowInfo
                'Case PackageType.NIBJ
                'Case PackageType.bk2
                'Case PackageType.CostumeFile
                'Case PackageType.MuscleFile
                'Case PackageType.MaskFile
                'Case PackageType.YOBJArray
                'Case PackageType.OFOP
                'Case PackageType.YANM
                'Case PackageType.VMUM

#End Region

        End Select
        If Crawl Then
            If Not IsNothing(ParentFileProperties.SubFiles) Then
                For Each ContainedFileProperties As ExtendedFileProperties In ParentFileProperties.SubFiles
                    MainForm.ProgressBar1.Maximum += 1
                    MainForm.ProgressBar1.Value += 1
                    If PackageInformation.CheckExpandable(ContainedFileProperties.FileType) Then
                        GetFileParts(ContainedFileProperties, Crawl)
                    End If
                Next
            End If
        End If
    End Sub

#End Region

End Class