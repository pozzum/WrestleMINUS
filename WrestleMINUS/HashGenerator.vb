Imports System.Security.Cryptography 'MD5
Imports System.Text 'Text Encoding 
Public Class HashGenerator
    Shared Function GetMd5Hash(ByVal HashedString As String, Optional PartialHash As Boolean = False) As String
        ' Initializes a hash object : md5
        Dim Hash = MD5.Create
        ' We declare a variable that will be an array of bytes (bytes)
        Dim HashValue() As Byte
        Try
            'Convert the input string to a byte array And compute the hash.
            HashValue = Hash.ComputeHash(Encoding.UTF8.GetBytes(HashedString))
            ' The Byte array Is converted to hexadecimal for easy reading
            Dim HashHex = PrintByteArray(HashValue, PartialHash)
            ' Return the hash
            Return HashHex
        Catch
            MessageBox.Show("Error occurred! Try Again.")
        End Try
        'if file open fails
        Return Nothing
    End Function
    ' One parses the array Of bytes (bytes) And converts Each Byte (Byte) To hexadecimal
    Public Shared Function PrintByteArray(ByVal Array() As Byte, Optional PartialHash As Boolean = False) As String
        Dim HexValue As String = ""
        ' The Byte array (bytes)
        Dim i As Integer
        If PartialHash Then
            For i = 11 To 8 Step -1
                ' Each byte Is converted to hexadecimal
                HexValue += Array(i).ToString("X2")
            Next i
        Else
            For i = 0 To Array.Length - 1
                ' Each byte Is converted to hexadecimal
                HexValue += Array(i).ToString("X2")
            Next i
        End If
        ' Return the string to upper
        Return HexValue.ToUpper
    End Function
End Class
