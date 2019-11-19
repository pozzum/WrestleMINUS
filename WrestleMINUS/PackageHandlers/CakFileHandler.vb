Imports System.IO   'Files
Imports System.Text 'Text Encoding

Namespace PackageHandlers

    Public Class CakFileHandler : Implements IDisposable

#Region "Specifying Classes"

        Enum CakPackFlags
            Unknown = 0
            NonObfuscated = &H4000US
        End Enum

        Public Class BakedFileHeader
            Public Magic As UInteger
            Public Version As UShort
            Public Flags As CakPackFlags
            Public Code As UInteger()

            Public Function FilesCount() As UInteger
                Return Code(0)
            End Function

            Public Function FoldersCount() As UInteger
                Return Code(1)
            End Function

            Public Function FolderHashesSize() As UInteger
                Return Code(2)
            End Function

            Public Function FolderHashesCrc() As UInteger
                Return Code(3)
            End Function

            Public Function FolderHashesOffset() As UInteger
                Return Code(4)
            End Function

            Public Function FileHashesSize() As UInteger
                Return Code(5)
            End Function

            Public Function FileHashesCrc() As UInteger
                Return Code(6)
            End Function

            Public Function FileHashesOffset() As UInteger
                Return Code(7)
            End Function

            Public Function FilesSize() As UInteger
                Return Code(8)
            End Function

            Public Function FilesCrc() As UInteger
                Return Code(9)
            End Function

            Public Function FilesOffset() As UInteger
                Return Code(10)
            End Function

            Public Function FoldersSize() As UInteger
                Return Code(11)
            End Function

            Public Function FoldersCrc() As UInteger
                Return Code(12)
            End Function

            Public Function FoldersOffset() As UInteger
                Return Code(13)
            End Function

            Public Function StringsSize() As UInteger
                Return Code(14)
            End Function

            Public Function StringsCrc() As UInteger
                Return Code(15)
            End Function

            Public Function StringsOffset() As UInteger
                Return Code(16)
            End Function

            'Code(17)
            'Code(18)
            Public Function TotalSize() As UInteger
                Return Code(19)
            End Function

            Sub Serialize(FileReader As BinaryReader, Hash As UInteger)
                Magic = FileReader.ReadUInt32()
                Version = FileReader.ReadUInt16()
                Flags = CType(FileReader.ReadUInt16(), CakPackFlags)
                Code = New UInteger(19) {}
                For i As Integer = 0 To 19
                    Code(i) = FileReader.ReadUInt32()
                Next
                If Not Flags = CakPackFlags.NonObfuscated Then
                    DeobfuscateHeader(Hash)
                End If
            End Sub

            'This can technically just get sent to deobfuscate block first....

            Sub DeobfuscateHeader(Hash As UInteger)
                Code(0) = Code(0) Xor Hash
                Dim var0 As UInteger = Hash Xor Code(0)
                Code(1) = Code(1) Xor var0
                Dim var1 As UInteger = Code(1) Xor var0
                Code(2) = Code(2) Xor var1
                Dim var2 As UInteger = Code(2) Xor var1
                Code(3) = Code(3) Xor var2
                Dim var3 As UInteger = Code(3) Xor var2
                Code(4) = Code(4) Xor var3
                Dim var4 As UInteger = Code(4) Xor var3
                Code(5) = Code(5) Xor var4
                Dim var5 As UInteger = Code(5) Xor var4
                Code(6) = Code(6) Xor var5
                Dim var6 As UInteger = Code(6) Xor var5
                Code(7) = Code(7) Xor var6
                Dim var7 As UInteger = Code(7) Xor var6
                Code(8) = Code(8) Xor var7
                Dim var8 As UInteger = Code(8) Xor var7
                Code(9) = Code(9) Xor var8
                Dim var9 As UInteger = Code(9) Xor var8
                Code(10) = Code(10) Xor var9
                Dim var10 As UInteger = Code(10) Xor var9
                Code(11) = Code(11) Xor var10
                Dim var11 As UInteger = Code(11) Xor var10
                Code(12) = Code(12) Xor var11
                Dim var12 As UInteger = Code(12) Xor var11
                Code(13) = Code(13) Xor var12
                Dim var13 As UInteger = Code(13) Xor var12
                Code(14) = Code(14) Xor var13
                Dim var14 As UInteger = Code(14) Xor var13
                Code(15) = Code(15) Xor var14
                Dim var15 As UInteger = Code(15) Xor var14
                Code(16) = Code(16) Xor var15
                Dim var16 As UInteger = Code(16) Xor var15
                Code(17) = Code(17) Xor var16
                Dim var17 As UInteger = Code(17) Xor var16
                Code(18) = Code(18) Xor var17
                Dim var18 As UInteger = Code(18) Xor var17
                Code(19) = Code(19) Xor var18
            End Sub

        End Class

        Public Class PackFolderEntry
            Public Unk As ULong
            Public Name As String
            Public FolderIndices As List(Of Integer)
            Public FileIndices As List(Of Integer)
            Public Hash As ULong
        End Class

        Public Class PackFileEntry
            Public Name As String
            Public Unk1 As UInteger
            Public Crc As UInteger
            Public Size As UInteger
            Public Offset As ULong
            Public Type As String
            Public Hash As ULong
        End Class

#End Region

        Dim FileName As String
        Dim FileNameHash As UInteger

        Dim Folders As List(Of PackFolderEntry) = New List(Of PackFolderEntry)
        Dim Files As List(Of PackFileEntry) = New List(Of PackFileEntry)
        Dim FileMapping As Dictionary(Of ULong, Integer) = New Dictionary(Of ULong, Integer)

        Dim PackFileHeader As BakedFileHeader
        Shared FileStream As Stream

        Dim FolderHashesBlock As Byte()
        Dim FilesHashesBlock As Byte()
        Dim FoldersBlock As Byte()
        Dim FilesBlock As Byte()
        Dim StringBlock As Byte()

        Sub Initialize(IncomingStream As Stream, IncomingFileName As String)
            FileName = IncomingFileName
            FileStream = IncomingStream
            Dim LongHash As ULong = HashGenerator.GetFNVH1aHash(FileName.ToLower)
            Dim TempHash As UInteger = (LongHash >> 32)
            FileNameHash = CUInt(((TempHash Xor LongHash) << 32) >> 32)

            Using ActiveReader As BinaryReader = New BinaryReader(FileStream, Text.Encoding.Default, False)
                PackFileHeader = New BakedFileHeader()
                PackFileHeader.Serialize(ActiveReader, FileNameHash)

                'Folder Hashes
                ActiveReader.BaseStream.Position = PackFileHeader.FolderHashesOffset
                FolderHashesBlock = ActiveReader.ReadBytes(PackFileHeader.FolderHashesSize)
                If Not PackFileHeader.Flags = CakPackFlags.NonObfuscated Then
                    'Making a temp Value allows inspection of the transition.
                    FolderHashesBlock = DeobfuscateBlock(FolderHashesBlock, FileNameHash)
                End If

                'Files Hashes
                ActiveReader.BaseStream.Position = PackFileHeader.FileHashesOffset
                FilesHashesBlock = ActiveReader.ReadBytes(PackFileHeader.FileHashesSize)
                If Not PackFileHeader.Flags = CakPackFlags.NonObfuscated Then
                    FilesHashesBlock = DeobfuscateBlock(FilesHashesBlock, FileNameHash)
                End If

                'Folders
                ActiveReader.BaseStream.Position = PackFileHeader.FoldersOffset
                FoldersBlock = ActiveReader.ReadBytes(PackFileHeader.FoldersSize)
                If Not PackFileHeader.Flags = CakPackFlags.NonObfuscated Then
                    FoldersBlock = DeobfuscateBlock(FoldersBlock, FileNameHash)
                End If

                'Files
                ActiveReader.BaseStream.Position = PackFileHeader.FilesOffset
                FilesBlock = ActiveReader.ReadBytes(PackFileHeader.FilesSize)
                If Not PackFileHeader.Flags = CakPackFlags.NonObfuscated Then
                    FilesBlock = DeobfuscateBlock(FilesBlock, FileNameHash)
                End If

                'Strings
                ActiveReader.BaseStream.Position = PackFileHeader.StringsOffset
                StringBlock = ActiveReader.ReadBytes(PackFileHeader.StringsSize)
                If Not PackFileHeader.Flags = CakPackFlags.NonObfuscated Then
                    StringBlock = DeobfuscateBlock(StringBlock, FileNameHash)
                End If

                Using FolderReader As BinaryReader = New BinaryReader(New MemoryStream(FoldersBlock))
                    For i As UInteger = 0 To PackFileHeader.FoldersCount - 1
                        Dim CurrentEntry As PackFolderEntry = New PackFolderEntry With {
                            .FileIndices = New List(Of Integer),
                            .FolderIndices = New List(Of Integer),
                            .Unk = FolderReader.ReadUInt64()}
                        Dim StringOffset As UInteger = FolderReader.ReadUInt32()
                        Dim FolderCount As UInteger = FolderReader.ReadUInt32()
                        Dim FileCount As UInteger = FolderReader.ReadUInt32()
                        If FolderCount > 0 Then
                            For j As UInteger = 0 To FolderCount - 1
                                CurrentEntry.FolderIndices.Add(FolderReader.ReadInt32)
                            Next
                        End If
                        If FileCount > 0 Then
                            For j As UInteger = 0 To FileCount - 1
                                CurrentEntry.FileIndices.Add(FolderReader.ReadInt32)
                            Next
                        End If
                        Dim TempName As String = ""
                        TempName = Encoding.Default.GetChars(StringBlock, StringOffset, 255)
                        CurrentEntry.Name = TempName.Substring(0, TempName.IndexOf(vbNullChar))
                        Folders.Add(CurrentEntry)
                    Next
                End Using

                Using FolderHashReader As BinaryReader = New BinaryReader(New MemoryStream(FolderHashesBlock))
                    For i As Integer = 0 To PackFileHeader.FoldersCount - 1
                        Dim NameHash As ULong = FolderHashReader.ReadUInt64()
                        Dim Index As Integer = FolderHashReader.ReadInt32()
                        Folders(Index).Hash = NameHash
                    Next
                End Using

                Using FilesReader As BinaryReader = New BinaryReader(New MemoryStream(FilesBlock))
                    For i As UInteger = 0 To PackFileHeader.FilesCount - 1
                        Dim StringOffset As UInteger = FilesReader.ReadUInt32()
                        Dim CurrentEntry As PackFileEntry = New PackFileEntry With {
                           .Unk1 = FilesReader.ReadUInt32(),
                           .Crc = FilesReader.ReadUInt32(),
                           .Size = FilesReader.ReadUInt32(),
                           .Offset = FilesReader.ReadInt64()}
                        CurrentEntry.Type = Encoding.Default.GetChars(FilesReader.ReadBytes(4))
                        Dim TempName As String = ""
                        Dim Readlength As Integer = 255
                        If (StringBlock.Length - StringOffset) < 255 Then
                            Readlength = (StringBlock.Length - StringOffset)
                        End If
                        TempName = Encoding.Default.GetChars(StringBlock, StringOffset, Readlength)
                        CurrentEntry.Name = TempName.Substring(0, TempName.IndexOf(vbNullChar))
                        Files.Add(CurrentEntry)
                    Next
                End Using

                Using FileHashesReader As BinaryReader = New BinaryReader(New MemoryStream(FilesHashesBlock))
                    For i As UInteger = 0 To PackFileHeader.FilesCount - 1
                        Dim NameHash As ULong = FileHashesReader.ReadUInt64()
                        Dim Index As Integer = FileHashesReader.ReadInt32()
                        If Not NameHash = 0 Then
                            Files(Index).Hash = NameHash
                            FileMapping.Add(NameHash, Index)
                        End If
                    Next
                End Using
            End Using
        End Sub

#Region "Block Manipulation"

        Function DeobfuscateBlock(IncomingBlock As Byte(), Hash As UInteger) As Byte()
            Dim OutgoingBlock As Byte() = New Byte(IncomingBlock.Length - 1) {}
            Dim ActiveOffset As UInteger = 0
            Dim ActiveHash As UInteger = Hash
            If Math.Floor(IncomingBlock.Length / 8) > 0 Then
                For i As Integer = 0 To Math.Floor(IncomingBlock.Length / 8) - 1
                    Dim FirstUint As UInteger = BitConverter.ToUInt32(IncomingBlock, i * 8)
                    FirstUint = FirstUint Xor ActiveHash
                    ActiveHash = FirstUint Xor ActiveHash
                    Array.Copy(BitConverter.GetBytes(FirstUint), 0, OutgoingBlock, i * 8, 4)
                    ActiveOffset += 4
                    'duplicating
                    Dim SecondUint As UInteger = BitConverter.ToUInt32(IncomingBlock, i * 8 + 4)
                    SecondUint = SecondUint Xor ActiveHash
                    ActiveHash = SecondUint Xor ActiveHash
                    Array.Copy(BitConverter.GetBytes(SecondUint), 0, OutgoingBlock, i * 8 + 4, 4)
                    ActiveOffset += 4
                Next
            End If
            If Math.Floor(IncomingBlock.Length Mod 8) > 0 Then
                'GeneralTools.BreakFunction()
                'ActiveOffset = IncomingBlock.Length - ((IncomingBlock.Length Mod 8) * 8)
                For i As Integer = 0 To (IncomingBlock.Length Mod 8) - 1
                    Dim TempByte As Byte = IncomingBlock(ActiveOffset + i)
                    TempByte = BitConverter.GetBytes(CULng(TempByte Xor ActiveHash))(0)
                    ActiveHash = BitConverter.GetBytes(CULng(TempByte Xor ActiveHash))(0)
                    OutgoingBlock(ActiveOffset + i) = TempByte
                Next
            End If
            Return OutgoingBlock
        End Function

        Function ObfuscateBlock(IncomingBlock As Byte(), Hash As UInteger) As Byte()
            Dim OutgoingBlock As Byte() = New Byte(IncomingBlock.Length - 1) {}
            Dim ActiveOffset As UInteger = 0
            Dim ActiveHash As UInteger = Hash
            If Math.Floor(IncomingBlock.Length / 8) > 0 Then
                For i As Integer = 0 To Math.Floor(IncomingBlock.Length / 8) - 1
                    Dim FirstUint As UInteger = BitConverter.ToUInt32(IncomingBlock, i * 8)
                    FirstUint = FirstUint Xor ActiveHash
                    ActiveHash = FirstUint Xor ActiveHash
                    Array.Copy(BitConverter.GetBytes(FirstUint), 0, OutgoingBlock, i * 8, 4)
                    ActiveOffset += 4
                    'duplicating
                    Dim SecondUint As UInteger = BitConverter.ToUInt32(IncomingBlock, i * 8 + 4)
                    SecondUint = SecondUint Xor ActiveHash
                    ActiveHash = SecondUint Xor ActiveHash
                    Array.Copy(BitConverter.GetBytes(SecondUint), 0, OutgoingBlock, i * 8 + 4, 4)
                    ActiveOffset += 4
                Next
            End If
            If Math.Floor(IncomingBlock.Length Mod 8) > 0 Then
                'GeneralTools.BreakFunction()
                'ActiveOffset = IncomingBlock.Length - ((IncomingBlock.Length Mod 8) * 8)
                For i As Integer = 0 To (IncomingBlock.Length Mod 8) - 1
                    Dim TempByte As Byte = IncomingBlock(ActiveOffset + i)
                    TempByte = BitConverter.GetBytes(CULng(TempByte Xor ActiveHash))(0)
                    ActiveHash = BitConverter.GetBytes(CULng(TempByte Xor ActiveHash))(0)
                    OutgoingBlock(ActiveOffset + i) = TempByte
                Next
            End If
            Return OutgoingBlock
        End Function

#End Region

#Region "External Commands"

        Function GetFileStringList() As List(Of String)
            Dim ReturnedList As List(Of String) = New List(Of String)
            If Not IsNothing(Files) AndAlso Files.Count > 0 Then
                For i As Integer = 0 To Files.Count - 1
                    ReturnedList.Add(Files(i).Name)
                Next
            End If
            Return ReturnedList
        End Function

        Function GetFileEntryFromString(TestedString As String) As PackFileEntry
            If Not IsNothing(Files) AndAlso Files.Count > 0 Then
                For i As Integer = 0 To Files.Count - 1
                    If Files(i).Name = TestedString Then
                        Return Files(i)
                    End If
                Next
            End If
            Return Nothing
        End Function

        Function GetFileEntryList() As List(Of PackFileEntry)
            Return Files
        End Function

        Function LoadFromFile(filename As String) As CakFileHandler
            Dim File As FileInfo = New FileInfo(filename)
            Dim ReturnedCakFileHandler As CakFileHandler = New CakFileHandler
            Using TempFileStream As FileStream = New FileStream(File.FullName, FileMode.Open, FileAccess.Read)
                ReturnedCakFileHandler.Initialize(TempFileStream, File.Name)
            End Using
            Return ReturnedCakFileHandler
        End Function

        Function GetIndividualFile(filename As String, VirtualPath As String) As Byte()
            Dim ReturnedBytes As Byte() = New Byte() {}
            Dim File As FileInfo = New FileInfo(filename)
            Dim TempFileHandler As CakFileHandler = New CakFileHandler
            Using TempFileStream As FileStream = New FileStream(File.FullName, FileMode.Open, FileAccess.Read)
                TempFileHandler.Initialize(TempFileStream, File.Name)
            End Using
            Dim TestedPackEntry As PackFileEntry = TempFileHandler.GetFileEntryFromString(VirtualPath)
            If Not IsNothing(TestedPackEntry) Then
                ReturnedBytes = GetFileEntryBytes(TempFileHandler, TestedPackEntry, New FileStream(File.FullName, FileMode.Open, FileAccess.Read))
            End If
            Return ReturnedBytes
        End Function

        Function GetFileEntryBytes(CompleteFileHandle As CakFileHandler, RequestedFileEntry As PackFileEntry, IndividualFileStream As FileStream) As Byte()
            Dim ReturnedBytes As Byte() = New Byte() {}
            Using ActiveReader As BinaryReader = New BinaryReader(IndividualFileStream, Encoding.Default, False)
                ActiveReader.BaseStream.Position = RequestedFileEntry.Offset
                Dim BufferBlock As Byte() = ActiveReader.ReadBytes(CInt(RequestedFileEntry.Size))
                If Not CompleteFileHandle.PackFileHeader.Flags = CakPackFlags.NonObfuscated Then
                    BufferBlock = DeobfuscateBlock(BufferBlock, CompleteFileHandle.FileNameHash)
                End If
                ReturnedBytes = BufferBlock
            End Using
            Return ReturnedBytes
        End Function

        Function BuildPlainFromHandler(SaveLocation As String, CompleteHandler As CakFileHandler, ReadFromLocation As String) As Boolean
            Dim File As FileInfo = New FileInfo(ReadFromLocation)
            Dim FileWritten As FileInfo = New FileInfo(SaveLocation)
            Using CleanWriter As BinaryWriter = New BinaryWriter(FileWritten.OpenWrite())
                CleanWriter.Write(CompleteHandler.PackFileHeader.Magic)
                CleanWriter.Write(CompleteHandler.PackFileHeader.Version)
                CleanWriter.Write(CUShort(CakPackFlags.NonObfuscated))
                For i As Integer = 0 To (CompleteHandler.PackFileHeader.Code.Length - 1)
                    CleanWriter.Write(CompleteHandler.PackFileHeader.Code(i))
                Next
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FolderHashesOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FolderHashesOffset
                    CleanWriter.Write(CompleteHandler.FolderHashesBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FileHashesOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FileHashesOffset
                    CleanWriter.Write(CompleteHandler.FilesHashesBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FilesOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FilesOffset
                    CleanWriter.Write(CompleteHandler.FilesBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FoldersOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FoldersOffset
                    CleanWriter.Write(CompleteHandler.FoldersBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.StringsOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.StringsOffset
                    CleanWriter.Write(CompleteHandler.StringBlock)
                    'Else
                    '    Return False
                End If
                'Header Write Complete.. Now to write Files

                For i As Integer = 0 To Files.Count - 1
                    If Files(i).Offset > CleanWriter.BaseStream.Length Then
                        CleanWriter.Seek(0, SeekOrigin.End)
                        For J As Integer = 0 To Files(i).Offset - CleanWriter.BaseStream.Length
                            CleanWriter.Write(New Byte() {0})
                        Next
                    Else
                        CleanWriter.BaseStream.Position = Files(i).Offset
                        CleanWriter.Write(GetFileEntryBytes(CompleteHandler, Files(i), New FileStream(File.FullName, FileMode.Open, FileAccess.Read)))
                    End If
                Next

            End Using
            Return True
        End Function

        Function BuildCodedFromHandler(SaveLocation As String, CompleteHandler As CakFileHandler, ReadFromLocation As String) As Boolean
            Dim File As FileInfo = New FileInfo(ReadFromLocation)
            Dim FileWritten As FileInfo = New FileInfo(SaveLocation)
            Using CleanWriter As BinaryWriter = New BinaryWriter(FileWritten.OpenWrite())
                CleanWriter.Write(CompleteHandler.PackFileHeader.Magic)
                CleanWriter.Write(CompleteHandler.PackFileHeader.Version)
                CleanWriter.Write(CUShort(CakPackFlags.NonObfuscated))
                For i As Integer = 0 To (CompleteHandler.PackFileHeader.Code.Length - 1)
                    CleanWriter.Write(CompleteHandler.PackFileHeader.Code(i))
                Next
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FolderHashesOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FolderHashesOffset
                    CleanWriter.Write(CompleteHandler.FolderHashesBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FileHashesOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FileHashesOffset
                    CleanWriter.Write(CompleteHandler.FilesHashesBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FilesOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FilesOffset
                    CleanWriter.Write(CompleteHandler.FilesBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.FoldersOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.FoldersOffset
                    CleanWriter.Write(CompleteHandler.FoldersBlock)
                    'Else
                    '    Return False
                End If
                If CleanWriter.BaseStream.Length() >= CompleteHandler.PackFileHeader.StringsOffset Then
                    CleanWriter.BaseStream.Position = CompleteHandler.PackFileHeader.StringsOffset
                    CleanWriter.Write(CompleteHandler.StringBlock)
                    'Else
                    '    Return False
                End If
                'Header Write Complete.. Now to write Files

                For i As Integer = 0 To Files.Count - 1
                    If Files(i).Offset > CleanWriter.BaseStream.Length Then
                        CleanWriter.Seek(0, SeekOrigin.End)
                        For J As Integer = 0 To Files(i).Offset - CleanWriter.BaseStream.Length
                            CleanWriter.Write(New Byte() {0})
                        Next
                    Else
                        CleanWriter.BaseStream.Position = Files(i).Offset
                        CleanWriter.Write(GetFileEntryBytes(CompleteHandler, Files(i), New FileStream(File.FullName, FileMode.Open, FileAccess.Read)))
                    End If
                Next

            End Using
            Return True
        End Function

#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    FileStream.Close()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            ' TODO: uncomment the following line if Finalize() is overridden above.
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region


    End Class

End Namespace