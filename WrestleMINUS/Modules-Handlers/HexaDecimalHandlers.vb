Module HexaDecimalHandlers

    Function EndianReverse(Source As Byte(), Optional Index As Integer = 0, Optional Length As Integer = 4) As Byte()
        If (Length + Index) > Source.Length Then
            Length = (Source.Length - Index)
        End If
        If Length > 0 Then
            Dim ReturnedArray As Byte() = New Byte(Length - 1) {}
            Array.Copy(Source, Index, ReturnedArray, 0, Length)
            Array.Reverse(ReturnedArray)
            Return ReturnedArray
        Else
            Return Nothing
        End If
    End Function

    Function HexCheck(StringtoCheck As String) As Boolean
        If Not IsNothing(StringtoCheck) Then
            Dim HexTest As Boolean = StringtoCheck.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            Return HexTest
        Else
            Return False
        End If
    End Function

    Function HexStringToByte(HexString As String, Optional Reverse As Boolean = False) As Byte()
        Dim NumberofCharacters As Integer = HexString.Length
        Dim ReturnedBytes As Byte() = New Byte(NumberofCharacters / 2 - 1) {}
        For i As Integer = 0 To NumberofCharacters - 1 Step 2
            ReturnedBytes(i / 2) = Convert.ToByte(HexString.Substring(i, 2), 16)
        Next
        If Reverse Then
            ReturnedBytes = EndianReverse(ReturnedBytes, 0, ReturnedBytes.Length)
        End If
        Return ReturnedBytes
    End Function

End Module