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
            TestType = PackageType.CakBaked OrElse
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
            TestType = PackageType.YANMPack OrElse
            TestType = PackageType.big OrElse
            TestType = PackageType.Cak OrElse
            TestType = PackageType.tex OrElse
            TestType = PackageType.Crn Then
            Return True
        End If
        Return False
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

    Shared Function CheckDeleteable(TestType As PackageType) As Boolean
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

    Shared Function CheckRenameable(TestType As PackageType) As Boolean
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
            Case PackageType.CakFolder
                Return 1
            Case PackageType.FileRefBin
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
            Case PackageType.big
                Return 7
            Case PackageType.ZLIB
                Return 8
            Case PackageType.OODL
                Return 9
            Case PackageType.OODL7
                Return 9
            Case PackageType.TextureLibrary
                Return 10
            Case PackageType.TPL
                Return 10
            Case PackageType.TronFile
                Return 10
            Case PackageType.Arc
                Return 11
            Case PackageType.YANMPack
                Return 11
            Case PackageType.YANM
                Return 11
            Case PackageType.OFOP
                Return 11
            Case PackageType.acts
                Return 11
            Case PackageType.evd
                Return 11
            Case PackageType.mprms
                Return 11
            Case PackageType.YOBJ
                Return 12
            Case PackageType.YOBJArray
                Return 12
            Case PackageType.mdl
                Return 12
            Case PackageType.StringFile
                Return 13
            Case PackageType.sdb
                Return 13
            Case PackageType.DDS
                Return 14
            Case PackageType.tex
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
            Case PackageType.Cak
                Return 19
            Case PackageType.Crn
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
            Case PackageType.WeaponPosition
                Return 23
            Case PackageType.TextureReference
                Return 24
            Case PackageType.LSD
                Return 25
            Case PackageType.LSD_BIN
                Return 25
            Case PackageType.CakBaked
                Return 0
            Case PackageType.Unchecked
                Return 0
            Case Else
                Return 0
        End Select
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
            Case "ARCH"
                Return PackageType.Arc
            Case "FDIR"
                Return PackageType.Cak
            Case "TEX!"
                Return PackageType.tex
            Case "MDL!"
                Return PackageType.mdl
            Case "MTLs"
                Return PackageType.mtls
            Case "MSKI"
                Return PackageType.mskinfo
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
                    Case FirstFour.Substring(0, 1).Contains("g") AndAlso
                            ByteArray(Index + 1) = 0
                        Return PackageType.SoundReference
                    Case FirstFour.Contains("WP") AndAlso
                        (FirstFour.Contains("AR") OrElse
                        FirstFour.Contains("BS") OrElse
                        FirstFour.Contains("RU") OrElse
                        FirstFour.Contains("ST") OrElse
                        FirstFour.Contains("EA"))
                        Return PackageType.WeaponPosition
                    Case FirstFour.Contains("EB")
                        Return PackageType.big
                    Case Else
                        'if we do not have a header text to guide us we have some additional text checks that are consistent.
                        'We want to Check for 2K20 Files that have the Header Type at byte 8
                        'First we want to check for oodl 7 at byte &H10
                        'THIS IS PRODUCING ERRORS WITH SOME PROJECT FILES
                        If ByteArray.Length > &H20 + Index Then
                            Dim OODL7Check As String = Encoding.Default.GetChars(ByteArray, Index + &H10, 4)
                            If OODL7Check = "OODL" Then
                                Return PackageType.OODL7
                            End If
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
                        If ByteArray.Length > &H30 + Index Then
                            Select Case True
                                Case (Encoding.Default.GetChars(ByteArray, Index + 8, 1) = "/" AndAlso Encoding.Default.GetChars(ByteArray, Index + &HC, 2).Contains("/"))
                                    Return PackageType.FileRefBin
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
                                Case FileNamePath.ToLower.Contains(".sdb")
                                    Dim NumberCheck As UInt32 = BitConverter.ToUInt32(ByteArray, Index + 0)
                                    If NumberCheck = 0 Then
                                        Return PackageType.sdb
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
                Return PackageType.bin
        End Select
    End Function

    Shared Function CheckCakContainerForFileType(Index As Long, ByVal ByteArray As Byte()) As PackageType
        If ByteArray.Length > &H10 + Index Then
            Dim FileExtention As String = Encoding.Default.GetChars(ByteArray, Index + &H8, 4)
            Select Case FileExtention
                Case "JSFB"
                    Return PackageType.jsfb
                Case "TEX!"
                    Return PackageType.tex
                Case "MDL!"
                    Return PackageType.mdl
                Case "MSKI"
                    Return PackageType.mskinfo
                Case "MTLs"
                    Return PackageType.mtls
                Case "MKRS"
                    Return PackageType.other_ywa
                Case "ACTS"
                    Return PackageType.acts
                Case "MPRM"
                    Return PackageType.mprms
                Case "EVD!"
                    Return PackageType.evd
                Case Else
                    Return PackageType.bin
            End Select
        Else
            Return PackageType.bin
        End If
    End Function

    Shared Function ChecktexForCRN(Index As Long, ByVal ByteArray As Byte()) As PackageType
        If ByteArray.Length > &H30 + Index Then
            'GeneralTools.EndianReverse(
            If Not BitConverter.ToUInt32(ByteArray, Index + 4) = 9 Then
                MessageBox.Show("Potential Tex Header Issue")
            End If
            Dim FileExtention As String = Encoding.Default.GetChars(ByteArray, Index + &H28, 4)
            Select Case FileExtention
                Case "CRN!"
                    Return PackageType.Crn
                Case Else
                    Return PackageType.bin
            End Select
        Else
            Return PackageType.bin
        End If
    End Function

    Shared Function CheckjsfbForFileType(Index As Long, ByVal ByteArray As Byte(), Optional FileNamePath As String = "") As PackageType
        If ByteArray.Length > &H10 + Index Then
            Dim HeaderText As String = Encoding.Default.GetChars(ByteArray, Index + &H8, 4)
            Select Case HeaderText
                Case "Supe"
                    Return PackageType.TronFile
                Case "Entr"
                    Return PackageType.EntrData
                Case Else
                    Return PackageType.bin
            End Select
        Else
            Return PackageType.bin
        End If

    End Function

    'Function can be split to an unchecked and a function returning a list of file information
    Shared Sub GetFileParts(ByRef ParentFileProperties As ExtendedFileProperties,
                            Optional Crawl As Boolean = False,
                            Optional TriggerProgress As Boolean = False)
        'Here we are using the ref of parent file so we can edit the sub files.
        Dim FileBytes As Byte() = FilePartHandlers.GetFilePartBytes(ParentFileProperties, CInt((Int32.MaxValue / 32)))
        'marking space limit explicitly to suppress the error message
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
                    If ContainedFileProperties.Index > FileNameLength Then
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Else
                        ContainedFileProperties.FileType = PackageType.bin
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    End If
                Next
                'Adding Coding to read the ending Zlib File
                If Not My.Settings.SuppressHSPCFooters Then
                    Dim ZlibOffset As Integer = BitConverter.ToUInt32(FileBytes, &H3C) + &H2000
                    Dim EndingZlibFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = "FileNames",
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .Index = ZlibOffset,
                            .length = &H800,
                            .StoredData = ParentFileProperties.StoredData,
                            .FileType = CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                            .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(EndingZlibFileProperties)
                End If
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
                Dim HeaderTypeCheck As Integer = BitConverter.ToUInt32(FileBytes, &HC) '01 = no spacer - 10 = &h10 spacer 00 08 super large spacers
                Dim FileInformationLength As Integer = BitConverter.ToUInt32(FileBytes, &H20)
                Dim FileInformationIndex As Integer
                If HeaderTypeCheck = 1 Then
                    FileInformationIndex = BitConverter.ToUInt32(FileBytes, &H1C)
                ElseIf HeaderTypeCheck = &H10 Then
                    Dim TempNumber As Integer = BitConverter.ToUInt32(FileBytes, &H18)
                    If (TempNumber Mod &H10) > 0 Then
                        TempNumber += &H10 - (TempNumber Mod &H10)
                    End If
                    'MessageBox.Show(Hex(TempNumber))
                    FileInformationIndex = TempNumber + &H40 + &H10
                ElseIf HeaderTypeCheck = &H800 Then
                    FileInformationIndex = BitConverter.ToUInt32(FileBytes, &H1C) * &H800
                End If
                Dim PachPartsCount As Integer = (FileInformationLength / &H10) '1 index
                If PachPartsCount > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To PachPartsCount - 1
                    Try
                        Dim PartName As String = Hex(BitConverter.ToUInt32(FileBytes, FileInformationIndex + (i * &H10)))
                        'MessageBox.Show(PartName)
                        If PartName = "FFFFFFFF" Then
                            Continue For
                        End If
                        PartName = PartName.PadLeft(My.Settings.DecimalNameMinLength, "0")
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = PartName,
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .Index = BitConverter.ToUInt32(FileBytes, FileInformationIndex + (i * &H10) + &H4) + ParentFileProperties.Index,
                            .length = BitConverter.ToUInt64(FileBytes, FileInformationIndex + (i * &H10) + &H8),
                            .StoredData = ParentFileProperties.StoredData,
                            .FileType = PackageInformation.CheckHeaderType(.Index - ParentFileProperties.Index, FileBytes, ParentFileProperties.FullFilePath),
                            .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message & vbNewLine &
                                        "Object Number: " & i & vbNewLine &
                                        "Header Start {hex}: " & Hex(FileInformationIndex))
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
                If ApplicationHandlers.CheckIconicZlib() Then
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
                If ApplicationHandlers.CheckUnrrbpe() Then
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
                    'here we exit the sub so an empty file isn't crawled if the tree is being crawled
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
                If ApplicationHandlers.CheckOodle6() Then
                    UncompressedBytes = PackUnpack.GetUncompressedOodle_6Bytes(FileBytes)
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
                    'here we exit the sub so an empty file isn't crawled if the tree is being crawled
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
            Case PackageType.OODL7
                If Not IsNothing(ParentFileProperties.SubFiles) Then
                    'MessageBox.Show(ParentFileProperties.Name & " Already Decompressed")
                    Exit Select
                Else
                    ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                End If
                Dim UncompressedBytes As Byte() = Nothing
                If ApplicationHandlers.CheckOodle7() Then
                    UncompressedBytes = PackUnpack.GetUncompressedOodle_7Bytes(FileBytes)
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
                    'here we exit the sub so an empty file isn't crawled if the tree is being crawled
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
                If FileBytes.Length > &H10 Then
                    Dim HeaderLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, &HC), 0)
                    Dim YANMLength As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, 4), 0)
                    Dim HeadIndex As Integer = 0
                    Dim partcount As Integer = 0
                    If HeaderLength > 0 Then
                        If IsNothing(ParentFileProperties.SubFiles) Then
                            ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                        End If
                    End If
                    If YANMLength > 0 Then
                        Do While HeadIndex < (HeaderLength - &H20)
                            If HeadIndex = 0 Then
                                HeadIndex = &H70
                            End If
                            'getting the part name
                            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                                .Name = Encoding.ASCII.GetString(FileBytes, HeadIndex + 4, 8),
                                .FullFilePath = ParentFileProperties.FullFilePath,
                                .Index = (BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, HeadIndex + &H24), 0)) + HeaderLength + ParentFileProperties.Index,
                                .length = BitConverter.ToUInt16(GeneralTools.EndianReverse(FileBytes, HeadIndex + 2, 2), 0) * &H20,
                                .FileType = PackageType.YANM,
                                .StoredData = ParentFileProperties.StoredData,
                                .Parent = ParentFileProperties}
                            ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                            partcount = partcount + 1
                            HeadIndex = HeadIndex + &H28
                        Loop
                    Else
                        Dim DummyFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                                .Name = "DUMMY",
                                .FullFilePath = ParentFileProperties.FullFilePath,
                                .Index = 0,
                                .length = 0,
                                .FileType = PackageType.bin,
                                .StoredData = ParentFileProperties.StoredData,
                                .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(DummyFileProperties)
                    End If
                End If

#Region "To be Built"

            Case PackageType.YOBJ

            Case PackageType.big
                Dim FileCount As UInt32 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, 4, 4), 0)
                Dim OffsetHeaderLength As UInt64 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, &HC, 4), 0)
                MessageBox.Show(OffsetHeaderLength.ToString)
                If FileCount > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To FileCount - 1
                    Dim CurrentItemIndex As UInt64 = BitConverter.ToUInt32(GeneralTools.EndianReverse(FileBytes, i * &H10 + &H30, 4), 0) * &H10
                    Dim CurrentItemLength As UInt64 = BitConverter.ToUInt64(GeneralTools.EndianReverse(FileBytes, i * &H10 + &H30 + 4, 8), 0)
                    Dim ImageNameLength As UInt16 = FileBytes(&H14)
                    Dim ImageName As String = Encoding.Default.GetChars(FileBytes, OffsetHeaderLength + i * ImageNameLength + 2, ImageNameLength)
                    ImageName = ImageName.TrimEnd(Chr(0))
                    'Here we need to get the File length and index in case the bytes are reversed
                    Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                        .Name = ImageName,
                        .FullFilePath = ParentFileProperties.FullFilePath,
                        .length = CurrentItemLength,
                        .Index = CurrentItemIndex,
                        .StoredData = ParentFileProperties.StoredData,
                        .FileType = PackageType.bin,
                        .Parent = ParentFileProperties}
                    ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                Next

#End Region

#Region "2K20 Cak Files"

            Case PackageType.Cak
                Dim ListOfFiles As List(Of String) = PackUnpack.GetBakedCakFileList(ParentFileProperties.FullFilePath)
                If ListOfFiles.Count > 0 Then
                    If IsNothing(ParentFileProperties.SubFiles) Then
                        ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                    End If
                End If
                For i As Integer = 0 To ListOfFiles.Count - 1
                    ParentFileProperties = BuildCakSubNodes(ParentFileProperties, ListOfFiles(i))
                Next

            Case PackageType.CakBaked
                If IsNothing(ParentFileProperties.SubFiles) Then
                    ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                End If
                If CheckHeaderType(0, FileBytes, ParentFileProperties.FullFilePath) = PackageType.OODL7 Then
                    ParentFileProperties.FileType = PackageType.OODL7
                    Dim UncompressedBytes As Byte() = Nothing
                    If ApplicationHandlers.CheckOodle7() Then
                        UncompressedBytes = PackUnpack.GetUncompressedOodle_7Bytes(FileBytes)
                    End If
                    If IsNothing(UncompressedBytes) Then
                        If My.Settings.ShowCAkIntermediates Then
                            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = ParentFileProperties.Name & " UNCOMPRESS",
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .VirtualFilePath = ParentFileProperties.VirtualFilePath,
                            .Index = 0,
                            .length = 0,
                            .StoredData = UncompressedBytes,
                            .FileType = PackageType.bin,
                            .Parent = ParentFileProperties}
                            ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                        Else
                            ParentFileProperties.FileType = PackageType.bin
                        End If
                        'here we exit the sub so an empty file isn't crawled if the tree is being crawled
                        Exit Sub
                    Else
                        If My.Settings.ShowCAkIntermediates Then
                            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                                .Name = ParentFileProperties.Name & " UNCOMPRESS",
                                .FullFilePath = ParentFileProperties.FullFilePath,
                                .VirtualFilePath = ParentFileProperties.VirtualFilePath,
                                .Index = 0,
                                .length = UncompressedBytes.Length,
                                .StoredData = UncompressedBytes,
                                .FileType = CheckCakContainerForFileType(ParentFileProperties.Index, FileBytes),
                                .Parent = ParentFileProperties}
                            If ContainedFileProperties.FileType = PackageType.jsfb Then
                                ContainedFileProperties.FileType = CheckjsfbForFileType(0, UncompressedBytes, ParentFileProperties.VirtualFilePath)
                            End If
                            ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                        Else
                            'Instead of making a new node we want to update the parent
                            'ParentFileProperties.Name
                            'ParentFileProperties.FullFilePath
                            'ParentFileProperties.VirtualFilePath
                            ParentFileProperties.FileType = CheckCakContainerForFileType(ParentFileProperties.Index, FileBytes)
                            ParentFileProperties.Index = 0
                            ParentFileProperties.length = UncompressedBytes.Length
                            ParentFileProperties.StoredData = UncompressedBytes
                            'ParentFileProperties.Parent
                            GetFileParts(ParentFileProperties, Crawl)
                            Exit Sub
                        End If
                    End If
                Else
                    If My.Settings.ShowCAkIntermediates Then
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                               .Name = ParentFileProperties.Name & " EXTRACT",
                               .FullFilePath = ParentFileProperties.FullFilePath,
                               .VirtualFilePath = ParentFileProperties.VirtualFilePath,
                               .Index = &H18,
                               .length = FileBytes.Length - &H18,
                               .StoredData = FileBytes,
                               .FileType = CheckCakContainerForFileType(ParentFileProperties.Index, FileBytes),
                               .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Else
                        ParentFileProperties.FileType = CheckCakContainerForFileType(ParentFileProperties.Index, FileBytes)
                        ParentFileProperties.Index = &H18
                        ParentFileProperties.length = FileBytes.Length - &H18
                        ParentFileProperties.StoredData = FileBytes
                        'ParentFileProperties.Parent
                        GetFileParts(ParentFileProperties, Crawl, TriggerProgress)
                        Exit Sub
                    End If
                End If
            Case PackageType.tex
                If ChecktexForCRN(0, FileBytes) = PackageType.Crn Then
                    If My.Settings.ShowCAkIntermediates Then
                        If IsNothing(ParentFileProperties.SubFiles) Then
                            ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                        End If
                        Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                               .Name = Path.GetFileNameWithoutExtension(ParentFileProperties.Name) & ".crn",
                               .FullFilePath = ParentFileProperties.FullFilePath,
                               .VirtualFilePath = ParentFileProperties.VirtualFilePath,
                               .Index = &H2C,
                               .length = BitConverter.ToUInt32(FileBytes, 0 + &H24),
                               .StoredData = FileBytes,
                               .FileType = PackageType.Crn,
                               .Parent = ParentFileProperties}
                        ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                    Else
                        'Instead of making a new node we want to update the parent
                        ParentFileProperties.Name = Path.GetFileNameWithoutExtension(ParentFileProperties.Name) & ".crn"
                        'ParentFileProperties.FullFilePath
                        'ParentFileProperties.VirtualFilePath
                        ParentFileProperties.FileType = PackageType.Crn
                        ParentFileProperties.Index = &H2C
                        ParentFileProperties.length = BitConverter.ToUInt32(FileBytes, 0 + &H24)
                        ParentFileProperties.StoredData = FileBytes
                        'ParentFileProperties.Parent
                        GetFileParts(ParentFileProperties, Crawl, TriggerProgress)
                        Exit Sub
                    End If
                Else
                    ParentFileProperties.FileType = PackageType.bin
                End If
            Case PackageType.Crn
                Dim UncrunchedBytes As Byte() = Nothing
                If ApplicationHandlers.CheckTexCrunchExe() Then
                    'GeneralTools.BreakFunction()
                    'Try
                    Dim TempName As String = Path.GetTempFileName
                    FileSystem.Rename(TempName, TempName + ".crn")
                    TempName += ".crn"
                    File.WriteAllBytes(TempName, FileBytes)
                    Dim CrunchExeProcess As New ProcessStartInfo(My.Settings.CrunchEXELocation) With
                        {.Arguments = TempName,
                        .WindowStyle = ProcessWindowStyle.Hidden}
                    Process.Start(CrunchExeProcess).WaitForExit()
                    Dim TempCRN As String = Path.GetDirectoryName(My.Settings.CrunchEXELocation) &
                                                Path.DirectorySeparatorChar &
                                                Path.GetFileNameWithoutExtension(TempName) & ".dds"
                    'Dim TempDDSLocal As String = Application.StartupPath & Path.DirectorySeparatorChar &
                    '                            Path.GetFileNameWithoutExtension(TempName) & ".dds"
                    'If File.Exists(TempDDSLocal) Then
                    '        File.Copy(TempDDSLocal, TempDDS, True)
                    '        File.Delete(TempDDSLocal)
                    '    End If
                    MainForm.CreatedImages.Add(TempCRN)
                    File.Delete(TempName)
                    UncrunchedBytes = File.ReadAllBytes(TempCRN)
                    'Catch ex As Exception
                    '    MessageBox.Show("Error Un-crunching Texture" & vbNewLine & ex.Message)
                    'End Try
                    If IsNothing(UncrunchedBytes) Then
                        If My.Settings.ShowCAkIntermediates Then
                            If IsNothing(ParentFileProperties.SubFiles) Then
                                ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                            End If
                            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                            .Name = Path.GetFileNameWithoutExtension(ParentFileProperties.Name) & ".dds",
                            .FullFilePath = ParentFileProperties.FullFilePath,
                            .VirtualFilePath = ParentFileProperties.VirtualFilePath,
                            .Index = 0,
                            .length = 0,
                            .StoredData = UncrunchedBytes,
                            .FileType = PackageType.DDS,
                            .Parent = ParentFileProperties}
                            ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                        Else
                            ParentFileProperties.FileType = PackageType.bin
                        End If
                        'here we exit the sub so an empty file isn't crawled if the tree is being crawled
                        Exit Sub
                    Else
                        If My.Settings.ShowCAkIntermediates Then
                            If IsNothing(ParentFileProperties.SubFiles) Then
                                ParentFileProperties.SubFiles = New List(Of ExtendedFileProperties)
                            End If
                            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                                   .Name = Path.GetFileNameWithoutExtension(ParentFileProperties.Name) & ".dds",
                                   .FullFilePath = ParentFileProperties.FullFilePath,
                                   .VirtualFilePath = ParentFileProperties.VirtualFilePath,
                                   .Index = 0,
                                   .length = UncrunchedBytes.Length,
                                   .StoredData = UncrunchedBytes,
                                   .FileType = PackageType.DDS,
                                   .Parent = ParentFileProperties}
                            ParentFileProperties.SubFiles.Add(ContainedFileProperties)
                        Else
                            'Instead of making a new node we want to update the parent
                            ParentFileProperties.Name = Path.GetFileNameWithoutExtension(ParentFileProperties.Name) & ".dds"
                            'ParentFileProperties.FullFilePath
                            'ParentFileProperties.VirtualFilePath
                            ParentFileProperties.FileType = PackageType.DDS
                            ParentFileProperties.Index = 0
                            ParentFileProperties.length = UncrunchedBytes.Length
                            ParentFileProperties.StoredData = UncrunchedBytes
                            'ParentFileProperties.Parent
                        End If
                    End If
                Else
                    ParentFileProperties.FileType = PackageType.bin
                End If

#End Region

            Case Else
                'This is a control measure to help against infinite select try to expand loops.
                If CheckExpandable(ParentFileProperties.FileType) Then
                    ParentFileProperties.FileType = PackageType.bin
                End If

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

    Shared Function BuildCakSubNodes(ParentFile As ExtendedFileProperties, FullVirtualPath As String, Optional RemainingPath As String = "") As ExtendedFileProperties
        If RemainingPath = "" Then
            RemainingPath = FullVirtualPath
        End If
        'MessageBox.Show(RemainingPath)
        If IsNothing(ParentFile.SubFiles) Then
            ParentFile.SubFiles = New List(Of ExtendedFileProperties)
        End If
        If RemainingPath.Contains("/") Then
            Dim ActiveFolder As String = RemainingPath.Substring(0, RemainingPath.IndexOf("/"))
            If Not ParentFile.SubFiles.Count = 0 Then
                For i As Integer = 0 To ParentFile.SubFiles.Count - 1
                    If ParentFile.SubFiles(i).Name = ActiveFolder Then
                        ParentFile.SubFiles(i) = BuildCakSubNodes(ParentFile.SubFiles(i), FullVirtualPath, RemainingPath.Substring(RemainingPath.IndexOf("/") + 1))
                        Return ParentFile
                    End If
                Next
            End If
            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                .Name = ActiveFolder,
                .FullFilePath = ParentFile.FullFilePath,
                .VirtualFilePath = FullVirtualPath,'Here we need to Retain the Full Path so it can be extracted with the tool
                .length = 0,
                .Index = 0,
                .StoredData = ParentFile.StoredData,
                .FileType = PackageType.CakFolder,
                .Parent = ParentFile}
            'MessageBox.Show(ContainedFileProperties.Name)
            ContainedFileProperties = BuildCakSubNodes(ContainedFileProperties, FullVirtualPath, RemainingPath.Substring(RemainingPath.IndexOf("/") + 1))
            ParentFile.SubFiles.Add(ContainedFileProperties)
            Return ParentFile
        Else
            Dim ContainedFileProperties As ExtendedFileProperties = New ExtendedFileProperties With {
                .Name = RemainingPath,
                .FullFilePath = ParentFile.FullFilePath,
                .VirtualFilePath = FullVirtualPath,'Here we need to Retain the Full Path so it can be extracted with the tool
                .length = 0,
                .Index = 0,
                .StoredData = ParentFile.StoredData,
                .FileType = PackageType.CakBaked,
                .Parent = ParentFile}
            ParentFile.SubFiles.Add(ContainedFileProperties)
            Return ParentFile
        End If
    End Function

#End Region

End Class