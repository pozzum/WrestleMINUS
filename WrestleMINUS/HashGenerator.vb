Imports System.Security.Cryptography 'MD5
Imports System.Text 'Text Encoding

Module HashGenerator

    Function GetFNVH1aHash(StringtoBeHashed As String) As UInteger

        Const fnv_offset As ULong = &HCBF29CE484222325UL
        Const fnv_prime As UInteger = &H1000193UI
        Dim hash As ULong = fnv_offset

        For i As Integer = 0 To StringtoBeHashed.Length - 1
            hash = hash Xor CByte(AscW(StringtoBeHashed(i)))
            hash = (hash * fnv_prime) And UInteger.MaxValue
        Next

        Return hash
    End Function

    Function GetMd5Hash(ByVal HashedString As String, Optional PartialHash As Boolean = False) As String
        ' Initializes a hash object : md5
        Dim Hash = MD5.Create
        ' We declare a variable that will be an array of bytes (bytes)
        Dim HashValue() As Byte
        Try
            'Convert the input string to a byte array And compute the hash.
            HashValue = Hash.ComputeHash(Encoding.UTF8.GetBytes(HashedString))
            ' The Byte array Is converted to hexadecimal for easy reading
            Dim HashHex = PrintHashArray(HashValue, PartialHash)
            ' Return the hash
            Return HashHex
        Catch
            MessageBox.Show("Error occurred! Try Again.")
        End Try
        'if file open fails
        Return Nothing
    End Function

    ' One parses the array Of bytes (bytes) And converts Each Byte (Byte) To hexadecimal
    Function PrintHashArray(ByVal Array() As Byte, Optional PartialHash As Boolean = False) As String
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

End Module