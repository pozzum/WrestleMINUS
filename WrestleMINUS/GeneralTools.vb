Imports System.IO   'Files

Public Class GeneralTools

    'Here are App-seperate commands use as required.
    Shared Sub FolderCheck(FolderPath As String)
        If Directory.Exists(FolderPath) = False Then
            Directory.CreateDirectory(FolderPath)
        End If
    End Sub

    Shared Sub MoveAllItems(ByVal fromPath As String, ByVal toPath As String)
        My.Computer.FileSystem.CopyDirectory(fromPath, toPath, True)
        My.Computer.FileSystem.DeleteDirectory(fromPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

    Shared Function EndianReverse(Source As Byte(), Optional Index As Integer = 0, Optional Length As Integer = 4) As Byte()
        Dim ReturnedArray As Byte() = New Byte(Length - 1) {}
        Array.Copy(Source, Index, ReturnedArray, 0, Length)
        Array.Reverse(ReturnedArray)
        Return ReturnedArray
    End Function

    Shared Function HexCheck(StringtoCheck As String) As Boolean
        If Not IsNothing(StringtoCheck) Then
            Dim HexTest As Boolean = StringtoCheck.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            Return HexTest
        Else
            Return False
        End If
    End Function

    Shared Function HexStringToByte(HexString As String) As Byte()
        Dim NumberofCharacters As Integer = HexString.Length
        Dim ReturnedBytes As Byte() = New Byte(NumberofCharacters / 2 - 1) {}
        For i As Integer = 0 To NumberofCharacters - 1 Step 2
            ReturnedBytes(i / 2) = Convert.ToByte(HexString.Substring(i, 2), 16)
        Next
        Return ReturnedBytes
    End Function

End Class