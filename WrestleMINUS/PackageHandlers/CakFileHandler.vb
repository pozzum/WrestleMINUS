Imports System.IO   'Files

Namespace PackageHandlers

    Public Class CakFileHandler

        Enum PackFlags
            Unknown = 0
            Obfuscated = &H4000UI
        End Enum

        Public Class BakedFileHeader
            Public Magic As UInteger
            Public Version As UShort
            Public Flags As PackFlags
            Public Code As UInteger()

            Public Function CheckFlags(SentFlag As PackFlags) As Boolean
                Return Not ((Flags And SentFlag) = 0)
            End Function

            Public Function FilesCount() As UInteger
                Return Code(0)
            End Function

            Public Function FolderFoldersCountsCount() As UInteger
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
                Flags = CType(FileReader.ReadUInt16(), PackFlags)
                Code = New UInteger(19) {}
                For i As Integer = 0 To 19
                    Code(0) = FileReader.ReadUInt32()
                Next
                If CheckFlags(PackFlags.Obfuscated) Then
                    DeobfuscateHeader(Hash)
                End If
            End Sub

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

        Dim FileName As String
        Dim FileNameHash As UInteger
        Dim Folders As List(Of PackFolderEntry) = New List(Of PackFolderEntry)
        Dim Files As List(Of PackFileEntry) = New List(Of PackFileEntry)
        Dim PackFileHeader As BakedFileHeader
        Dim FileStream As Stream

        Sub CakFile(IncomingStream As Stream, IncomingFileName As String)
            FileName = IncomingFileName
            FileStream = IncomingStream
            Dim LongHash As ULong = HashGenerator.GetFNVH1aHash(FileName.ToLower)
            FileNameHash = CUInt((LongHash >> 32) Xor LongHash)

            Using ActiveReader As BinaryReader = New BinaryReader(IncomingStream, Text.Encoding.Default, True)
                PackFileHeader = New BakedFileHeader()
                PackFileHeader.Serialize(ActiveReader, FileNameHash)

                'Folder Hashes
                ActiveReader.BaseStream.Position = PackFileHeader.FolderHashesOffset
                Dim FolderHashesBlock As Byte() = ActiveReader.ReadBytes(PackFileHeader.FolderHashesSize)
                If PackFileHeader.CheckFlags(PackFlags.Obfuscated) Then
                    FolderHashesBlock = DeobfuscateBlock(FolderHashesBlock, FileNameHash)
                End If

                'Files Hashes
                ActiveReader.BaseStream.Position = PackFileHeader.FileHashesOffset
                Dim FilesHashesBlock As Byte() = ActiveReader.ReadBytes(PackFileHeader.FileHashesSize)
                If PackFileHeader.CheckFlags(PackFlags.Obfuscated) Then
                    FilesHashesBlock = DeobfuscateBlock(FilesHashesBlock, FileNameHash)
                End If

                'Folders
                ActiveReader.BaseStream.Position = PackFileHeader.FoldersOffset
                Dim FoldersBlock As Byte() = ActiveReader.ReadBytes(PackFileHeader.FoldersSize)
                If PackFileHeader.CheckFlags(PackFlags.Obfuscated) Then
                    FoldersBlock = DeobfuscateBlock(FoldersBlock, FileNameHash)
                End If

                'Files
                ActiveReader.BaseStream.Position = PackFileHeader.FilesOffset
                Dim FilesBlock As Byte() = ActiveReader.ReadBytes(PackFileHeader.FilesSize)
                If PackFileHeader.CheckFlags(PackFlags.Obfuscated) Then
                    FilesBlock = DeobfuscateBlock(FilesBlock, FileNameHash)
                End If

                'Files
                ActiveReader.BaseStream.Position = PackFileHeader.StringsOffset
                Dim StringBlock As Byte() = ActiveReader.ReadBytes(PackFileHeader.StringsSize)
                If PackFileHeader.CheckFlags(PackFlags.Obfuscated) Then
                    StringBlock = DeobfuscateBlock(StringBlock, FileNameHash)
                End If
            End Using
        End Sub

        Function DeobfuscateBlock(IncomingBlock As Byte(), Hash As UInteger) As Byte()
            Dim OutgoingBlock As Byte() = New Byte(IncomingBlock.Length - 1) {}
            Dim Length As UInteger = IncomingBlock.Length
            Dim HashValue As UInteger = Hash

            Return OutgoingBlock
        End Function

    End Class

End Namespace