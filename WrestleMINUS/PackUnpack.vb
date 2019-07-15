﻿'http://www.codeplex.com/DotNetZip
Imports System.Environment 'appdata
Imports System.IO   'Files
Imports Ionic.Zlib  'zlib decompress

Public Class PackUnpack

#Region "BPE Compression"

    Shared Function CheckBPEExe(Optional FromOptions As Boolean = False)
        Dim AppDataStorage As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        If FromOptions = False Then
            If File.Exists(AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe") Then
                My.Settings.BPEExePath = AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe"
                Return True
            Else
                If MessageBox.Show("Would you like to navigate to ""bpe.exe""" & vbNewLine &
                    "this would be in any ""Pac Editor"" install folder.",
                       "BPE Compressor",
                       MessageBoxButtons.YesNo) = DialogResult.No Then
                    MessageBox.Show("Download link can be found in the options menu.")
                    My.Settings.BPEExePath = "Not Installed"
                    Return False
                End If
            End If
        End If
        If My.Settings.BPEExePath = "" Then
            My.Settings.BPEExePath = "Not Installed"
        End If
        Dim BPEExeOpenDialog As New OpenFileDialog With {.FileName = "bpe.exe", .Title = "Select bpe.exe"}
        If BPEExeOpenDialog.ShowDialog = DialogResult.OK Then
            If Path.GetFileName(BPEExeOpenDialog.FileName) = "bpe.exe" Then
                If Not Path.GetDirectoryName(BPEExeOpenDialog.FileName) = AppDataStorage Then
                    If MessageBox.Show("Would you like create a copy of this to the appdata?",
                                       "Copy File?",
                                       MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        GeneralTools.FolderCheck(AppDataStorage)
                        File.Copy(BPEExeOpenDialog.FileName, AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe", True)
                        My.Settings.BPEExePath = AppDataStorage & Path.DirectorySeparatorChar & "bpe.exe"
                        Return True
                    Else
                        My.Settings.BPEExePath = BPEExeOpenDialog.FileName
                        Return True
                    End If
                Else
                    My.Settings.BPEExePath = BPEExeOpenDialog.FileName
                    Return True
                End If
            Else
                MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                Return False
            End If
        End If
        If My.Settings.BPEExePath = "Not Installed" Then
            Return False
        Else
            Return True
        End If
    End Function

    Shared Function CheckUnrrbpe(Optional FromOptions As Boolean = False)
        Dim AppDataStorage As String = GetFolderPath(SpecialFolder.ApplicationData) & "\Pozzum\WrestleMINUS"
        If FromOptions = False Then
            If File.Exists(AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe") Then
                My.Settings.UnrrbpePath = AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe"
                Return True
            Else
                If MessageBox.Show("Would you like to navigate to ""unrrbpe.exe""" & vbNewLine &
                    "this would be in any ""X-Packer"" install folder.",
                       "BPE Decompresser",
                       MessageBoxButtons.YesNo) = DialogResult.No Then
                    MessageBox.Show("Download link can be found in the options menu.")
                    My.Settings.UnrrbpePath = "Not Installed"
                    Return False
                End If
            End If
        End If
        If My.Settings.UnrrbpePath = "" Then
            My.Settings.UnrrbpePath = "Not Installed"
        End If
        Dim UnrrbpeOpenDialog As New OpenFileDialog With {.FileName = "unrrbpe.exe", .Title = "Select unrrbpe.exe"}
        If UnrrbpeOpenDialog.ShowDialog = DialogResult.OK Then
            If Path.GetFileName(UnrrbpeOpenDialog.FileName) = "unrrbpe.exe" Then
                If Not Path.GetDirectoryName(UnrrbpeOpenDialog.FileName) = AppDataStorage Then
                    If MessageBox.Show("Would you like create a copy of this to the appdata?",
                                       "Copy File?",
                                       MessageBoxButtons.YesNo) = DialogResult.Yes Then
                        GeneralTools.FolderCheck(AppDataStorage)
                        File.Copy(UnrrbpeOpenDialog.FileName, AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe", True)
                        My.Settings.UnrrbpePath = AppDataStorage & Path.DirectorySeparatorChar & "unrrbpe.exe"
                        Return True
                    Else
                        My.Settings.UnrrbpePath = UnrrbpeOpenDialog.FileName
                        Return True
                    End If
                Else
                    My.Settings.UnrrbpePath = UnrrbpeOpenDialog.FileName
                    Return True
                End If
            Else
                MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                Return False
            End If
        End If
        If My.Settings.UnrrbpePath = "Not Installed" Then
            Return False
        Else
            Return True
        End If
    End Function

    Shared Function GetUncompressedBPEBytes(SentBytes As Byte())
        Dim UncompressedLength As Integer = BitConverter.ToUInt32(SentBytes, &HC)
        Dim UncompressedBytes As Byte() = New Byte(UncompressedLength) {}
        Try
            Dim TempInput As String = Path.GetTempFileName
            Dim TempOutput As String = Path.GetTempFileName
            File.WriteAllBytes(TempInput, SentBytes)
            Process.Start(My.Settings.UnrrbpePath, TempInput & " " & TempOutput).WaitForExit()
            UncompressedBytes = File.ReadAllBytes(TempOutput)
            File.Delete(TempInput)
            File.Delete(TempOutput)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Return UncompressedBytes
    End Function

    Shared Function GetCompressedBPEBytes(SentBytes As Byte())
        Dim CompressedBuffer As Byte() = New Byte(SentBytes.LongLength - 1) {}
        Try
            Dim TempBPEExe As String = Path.GetDirectoryName(Application.ExecutablePath) & Path.DirectorySeparatorChar & Path.GetFileName(My.Settings.BPEExePath)
            Dim TempFile1 As String = Path.GetTempFileName
            Dim TempInput As String = Path.GetDirectoryName(TempBPEExe) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(TempFile1)
            Dim TempOutput As String = TempInput & ".bpe"
            File.Copy(My.Settings.BPEExePath, TempBPEExe, True)
            File.WriteAllBytes(TempInput, SentBytes)
            Dim Info As ProcessStartInfo = New ProcessStartInfo("cmd.exe")
            Info.Arguments = "/C """ & TempBPEExe & """ " & Path.GetFileName(TempInput) & " " & Path.GetFileName(TempOutput)
            Process.Start(Info).WaitForExit()
            CompressedBuffer = File.ReadAllBytes(TempOutput)
            If Not Path.GetDirectoryName(Application.ExecutablePath) = Path.GetDirectoryName(My.Settings.BPEExePath) Then
                File.Delete(TempBPEExe)
            End If
            File.Delete(TempInput)
            File.Delete(TempOutput)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        'BreakFunction(CompressedBuffer)
        Return CompressedBuffer
    End Function

    Shared Function CompressBPEToFile(FiletoRead As String, Optional FiletoWrite As String = "")
        If File.Exists(FiletoRead) Then
            Dim FileBytes As Byte() = File.ReadAllBytes(FiletoRead)
            Dim ExistingFileType As PackageType = PackageHandlers.CheckHeaderType(0, FileBytes, FiletoRead)
            If ExistingFileType = PackageType.BPE Then
                MessageBox.Show("File is already a BPE")
            Else
                Dim BytesToCompress As Byte() = Nothing
                If ExistingFileType = PackageType.OODL Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as OODL" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedOodleBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                ElseIf ExistingFileType = PackageType.ZLIB Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as ZLIB" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedZlibBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                Else
                    BytesToCompress = FileBytes
                End If
                Dim CompressedBytes As Byte() = Nothing
                CompressedBytes = GetCompressedBPEBytes(BytesToCompress)
                If IsNothing(CompressedBytes) Then
                    MessageBox.Show("Failure to get compressed bytes for " & Path.GetFileName(FiletoRead))
                    Return False
                Else
                    'Write bytes to file
                    Dim SaveLocation As String = ""
                    If FiletoWrite = "" Then
                        SaveLocation = Path.GetDirectoryName(FiletoRead) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(FiletoRead) & ".bpe"
                        If File.Exists(SaveLocation) Then
                            'We need to Backup the file in this case... this really shouldn't happen because bpe is skipped
                            File.Move(SaveLocation, SaveLocation.Replace(".bpe", ".bpe.bak"))
                        End If
                    Else
                        SaveLocation = FiletoWrite
                    End If
                    File.WriteAllBytes(SaveLocation, CompressedBytes)
                    Return True
                End If
            End If
        Else
            MessageBox.Show("File " & Path.GetFileName(FiletoRead) & " Does Not Exist")
        End If
        Return False
    End Function

#End Region

#Region "Zlib Compression"

    Shared Function CheckIconicZlib(Optional FromOptions As Boolean = False) As Boolean
        If Not File.Exists(Application.StartupPath & Path.DirectorySeparatorChar & "Ionic.Zlib.dll") Then
            If Not FromOptions Then
                MessageBox.Show("Ionic.Zlib Dll Not loaded")
                Return False
            Else
                Dim ZlibOpenDialog As New OpenFileDialog With {.FileName = "Ionic.Zlib.dll", .Title = "Ionic.Zlib.dll"}
                If ZlibOpenDialog.ShowDialog = DialogResult.OK Then
                    If Path.GetFileName(ZlibOpenDialog.FileName) = "Ionic.Zlib.dll" Then
                        File.Copy(ZlibOpenDialog.FileName, Application.StartupPath & Path.DirectorySeparatorChar & "Ionic.Zlib.dll")
                        Return True
                    Else
                        MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                        Return False
                    End If
                Else
                    Return False
                End If
            End If
        Else
            Return True
        End If
    End Function

    Shared Function GetUncompressedZlibBytes(SentBytes As Byte())
        Dim CompressedLength As UInt32 = BitConverter.ToUInt32(SentBytes, 8)
        Dim UncompressedLength As UInt32 = BitConverter.ToUInt32(SentBytes, 12)
        Dim input As Byte() = New Byte(CompressedLength - 1) {}
        Array.Copy(SentBytes, 16, input, 0, CInt(SentBytes.Length - 16))
        Dim output As Byte() = New Byte(UncompressedLength - 1) {}
        Try
            output = ZlibStream.UncompressBuffer(input)
        Catch ex As Exception
            Return Nothing
        End Try
        Return output
    End Function

    Shared Function GetCompressedZlibBytes(SentBytes As Byte())
        Dim CompressedBuffer As Byte() = New Byte(SentBytes.LongLength - 1) {}
        Try
            Using MemStream As MemoryStream = New MemoryStream
                Using Compressor As ZlibStream = New ZlibStream(MemStream, CompressionMode.Compress, CompressionLevel.BestCompression)
                    Compressor.Write(SentBytes, 0, SentBytes.Length)
                End Using
                CompressedBuffer = MemStream.ToArray()
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Dim ZlibFileBytes As Byte() = New Byte(CompressedBuffer.Length - 1 + &H10) {}
        Dim HeaderArray As Byte() = {&H5A, &H4C, &H49, &H42, &H30, &H12, &H0, &H0}
        Array.Copy(HeaderArray, 0, ZlibFileBytes, 0, &H8)
        Array.Copy(BitConverter.GetBytes(CUInt(CompressedBuffer.Length)), 0, ZlibFileBytes, &H8, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length)), 0, ZlibFileBytes, &HC, 4)
        Array.Copy(CompressedBuffer, 0, ZlibFileBytes, &H10, CompressedBuffer.Length)
        'BreakFunction(ZlibFileBytes)
        Return ZlibFileBytes
    End Function

    Shared Function CompressZLIBToFile(FiletoRead As String, Optional FiletoWrite As String = "")
        If File.Exists(FiletoRead) Then
            Dim FileBytes As Byte() = File.ReadAllBytes(FiletoRead)
            Dim ExistingFileType As PackageType = PackageHandlers.CheckHeaderType(0, FileBytes, FiletoRead)
            If ExistingFileType = PackageType.ZLIB Then
                MessageBox.Show("File is already a ZLIB")
            Else
                Dim BytesToCompress As Byte() = Nothing
                If ExistingFileType = PackageType.OODL Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as OODL" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedOodleBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                ElseIf ExistingFileType = PackageType.BPE Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as BPE" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedBPEBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                Else
                    BytesToCompress = FileBytes
                End If
                Dim CompressedBytes As Byte() = Nothing
                CompressedBytes = GetCompressedZlibBytes(BytesToCompress)
                If IsNothing(CompressedBytes) Then
                    MessageBox.Show("Failure to get compressed bytes for " & Path.GetFileName(FiletoRead))
                    Return False
                Else
                    'Write bytes to file
                    Dim SaveLocation As String = ""
                    If FiletoWrite = "" Then
                        SaveLocation = Path.GetDirectoryName(FiletoRead) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(FiletoRead) & ".zlib"
                        If File.Exists(SaveLocation) Then
                            'We need to Backup the file in this case... this really shouldn't happen because bpe is skipped
                            File.Move(SaveLocation, SaveLocation.Replace(".zlib", ".zlib.bak"))
                        End If
                    Else
                        SaveLocation = FiletoWrite
                    End If
                    File.WriteAllBytes(SaveLocation, CompressedBytes)
                    Return True
                End If
            End If
        Else
            MessageBox.Show("File " & Path.GetFileName(FiletoRead) & " Does Not Exist")
        End If
        Return False
    End Function

#End Region

#Region "Oodle Compression"

    Public Enum OodleFormat As UInt32
        LZH
        LZHLW
        LZNIB
        None
        LZB16
        LZBLW
        LZA
        LZNA
        Kraken
        Mermaid
        BitKnit
        Selkie
        Akkorokamui
    End Enum

    Public Enum OodleCompressionLevel As UInt32
        None
        SuperFast
        VeryFast
        Fast
        Normal
        Optimal1
        Optimal2
        Optimal3
        Optimal4
        Optimal5
    End Enum

    Public Declare Function OodleLZ_Decompress Lib "oo2core_6_win64" (InputBuffer As Byte(), bufferSize As Long, OutputBuffer As Byte(), outputBufferSize As Long,
            a As UInt32, b As UInt32, c As ULong, d As UInt32, e As UInt32, f As UInt32, g As UInt32, h As UInt32, i As UInt32, threadModule As UInt32) As Integer
    Public Declare Function OodleLZ_Compress Lib "oo2core_6_win64" (format As OodleFormat, buffer As Byte(), bufferSize As Long, outputBuffer As Byte(), level As OodleCompressionLevel, a As UInt32, b As UInt32, b As UInt32, d As ULong, threadModule As UInt32) As Integer

    Shared Function CheckOodle(Optional FromOptions As Boolean = False) As Boolean
        If Not File.Exists(Application.StartupPath & Path.DirectorySeparatorChar & "oo2core_6_win64.dll") Then
            Dim TestLocation As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar & "oo2core_6_win64.dll"
            If File.Exists(TestLocation) Then
                File.Copy(TestLocation,
                      Path.GetDirectoryName(Application.ExecutablePath) & Path.DirectorySeparatorChar & "oo2core_6_win64.dll", True)
                Return True
            Else
                If Not FromOptions Then
                    MessageBox.Show("Oodle Dll Not loaded")
                    Return False
                Else
                    Dim OodleOpenDialog As New OpenFileDialog With {.FileName = "oo2core_6_win64.dll", .Title = "oo2core_6_win64.dll"}
                    If OodleOpenDialog.ShowDialog = DialogResult.OK Then
                        If Path.GetFileName(OodleOpenDialog.FileName) = "oo2core_6_win64.dll" Then
                            File.Copy(OodleOpenDialog.FileName, Application.StartupPath &
                                      Path.DirectorySeparatorChar & "oo2core_6_win64.dll")
                            Return True
                        Else
                            MessageBox.Show("File selected is incorrect, you can reselect in the options menu")
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If
            End If
        Else
            Return True
        End If
    End Function

    'U32 compsize = (U32)OodleLZ_Compress(g_compressor, rawbufbase+dataoffset, bytesread, compbuf, (OodleLZ_CompressionLevel)g_level, &compressoptions, rawbufbase+dataoffset-contextsize, NULL, g_scratchmemory, g_scratchmemsize);
    Shared Function GetUncompressedOodleBytes(SentBytes As Byte())
        Dim CompressedLength As Long = BitConverter.ToUInt32(SentBytes, &H14)
        If Not CompressedLength = SentBytes.Length - &H18 Then
            If My.Settings.BypassOODLWarn Then
                CompressedLength = SentBytes.Length - &H18
            Else
                Dim result = MessageBox.Show("OODL Compression Length Mis-Match" & vbNewLine &
                              "Auto-detect compressed length?", "OODL Header Issue", MessageBoxButtons.YesNoCancel)
                If result = DialogResult.Cancel Then
                    Return Nothing
                ElseIf result = DialogResult.Yes Then
                    CompressedLength = SentBytes.Length - &H18
                End If
            End If
        End If
        Dim CompressedBytes As Byte() = New Byte(CompressedLength - 1) {}
        Dim LengthDiff As Int32 = SentBytes.Length - CompressedBytes.Length
        Array.Copy(SentBytes, LengthDiff, CompressedBytes, 0, CInt(CompressedBytes.Length))
        Dim UncompressedLength As Long = BitConverter.ToUInt32(SentBytes, &H10)
        Dim UncompressedBytes As Byte() = New Byte(UncompressedLength - 1) {}
        Try
            OodleLZ_Decompress(CompressedBytes, CompressedLength, UncompressedBytes, UncompressedLength,
                               0, 0, 0, 0, 0, 0, 0, 0, 0, 3)
        Catch ex As Exception
            Return Nothing
        End Try
        Return UncompressedBytes
    End Function

    Shared Function GetCompressedOodleBytes(SentBytes As Byte())
        Dim CompressedBuffer As Byte() = New Byte(SentBytes.LongLength - 1) {}
        Dim CompressedLength As Long = 0
        Try
            CompressedLength = OodleLZ_Compress(OodleFormat.Kraken, SentBytes, SentBytes.LongLength, CompressedBuffer, My.Settings.OODLCompressionLevel, 0, 0, 0, 0, 3)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        End Try
        Dim OodleFileBytes As Byte() = New Byte(CompressedLength - 1 + &H18) {}
        Dim HeaderArray As Byte() = {&H4F, &H4F, &H44, &H4C, &H0, &H0, &H0, &H0, &H30, &H0, &H6, &H2E, &H4B, &H52, &H4B, &H4E}
        Array.Copy(HeaderArray, 0, OodleFileBytes, 0, &H10) 'OODL Header '30 00 06 2E Bytes Might be relevant information
        Array.Copy(BitConverter.GetBytes(CUInt(SentBytes.Length)), 0, OodleFileBytes, &H10, 4)
        Array.Copy(BitConverter.GetBytes(CUInt(CompressedLength)), 0, OodleFileBytes, &H14, 4)
        Array.Copy(CompressedBuffer, 0, OodleFileBytes, &H18, CompressedLength)
        Return OodleFileBytes
    End Function

    Shared Function CompressOODLToFile(FiletoRead As String, Optional FiletoWrite As String = "")
        If File.Exists(FiletoRead) Then
            Dim FileBytes As Byte() = File.ReadAllBytes(FiletoRead)
            Dim ExistingFileType As PackageType = PackageHandlers.CheckHeaderType(0, FileBytes, FiletoRead)
            If ExistingFileType = PackageType.OODL Then
                MessageBox.Show("File is already a ZLIB")
            Else
                Dim BytesToCompress As Byte() = Nothing
                If ExistingFileType = PackageType.ZLIB Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as ZLIB" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedZlibBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                ElseIf ExistingFileType = PackageType.BPE Then
                    Dim Result As DialogResult = MessageBox.Show(Path.GetFileName(FiletoRead) & " is already compressed as BPE" & vbNewLine & "Decompress before compression?",
                                                                 "File Already Compressed", MessageBoxButtons.YesNoCancel)
                    If Result = DialogResult.Yes Then
                        BytesToCompress = GetUncompressedBPEBytes(FileBytes)
                    ElseIf Result = DialogResult.No Then
                        BytesToCompress = FileBytes
                    ElseIf Result = DialogResult.Cancel Then
                        Return False
                    End If
                Else
                    BytesToCompress = FileBytes
                End If
                Dim CompressedBytes As Byte() = Nothing
                CompressedBytes = GetCompressedOodleBytes(BytesToCompress)
                If IsNothing(CompressedBytes) Then
                    MessageBox.Show("Failure to get compressed bytes for " & Path.GetFileName(FiletoRead))
                    Return False
                Else
                    'Write bytes to file
                    Dim SaveLocation As String = ""
                    If FiletoWrite = "" Then
                        SaveLocation = Path.GetDirectoryName(FiletoRead) & Path.DirectorySeparatorChar & Path.GetFileNameWithoutExtension(FiletoRead) & ".oodl"
                        If File.Exists(SaveLocation) Then
                            'We need to Backup the file in this case... this really shouldn't happen because bpe is skipped
                            File.Move(SaveLocation, SaveLocation.Replace(".oodl", ".oodl.bak"))
                        End If
                    Else
                        SaveLocation = FiletoWrite
                    End If
                    File.WriteAllBytes(SaveLocation, CompressedBytes)
                    Return True
                End If
            End If
        Else
            MessageBox.Show("File " & Path.GetFileName(FiletoRead) & " Does Not Exist")
        End If
        Return False
    End Function

#End Region

End Class