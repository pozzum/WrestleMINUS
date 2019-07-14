Imports System.IO   'Files
Imports Ionic.Zlib  'zlib decompress

Public Class PackUnpack

#Region "Compression & Decompress"

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

#End Region

End Class