Imports System.IO   'Files

Public Class GeneralTools

#Region "Other Object Based General Tools"

    Shared Function CountVisibleToolStrip(SentCollection As ToolStripItemCollection) As Integer
        'if the tool-strip is not yet rendered visible will always trigger as false
        'so I have personally implemented using the tag system like I do with the nodes.
        Dim ReturnCount As Integer = 0
        For i As Integer = 0 To SentCollection.Count - 1
            If SentCollection(i).Tag Then
                ReturnCount += 1
            End If
        Next
        Return ReturnCount
    End Function

    Shared Function BreakFunction(Optional InspectedObject As Object = Nothing)
        'This is an easy way to add breaks wherever needed
        Return Nothing
    End Function

#End Region

#Region "String General Tools"

    Shared Function CountCharOccuranceInString(TestedString As String, SearchedCharacter As Char) As Integer
        Dim Count As Integer = 0
        For Each c As Char In TestedString
            If c = SearchedCharacter Then
                Count += 1
            End If
        Next
        Return Count
    End Function

    Shared Function TruncateString(TestedString As String, MaximumStength As Integer) As String
        ' If argument is too big, return the original string.
        ' ... Otherwise take a substring from the string's start index.
        If MaximumStength > TestedString.Length Then
            Return TestedString
        Else
            Return TestedString.Substring(0, MaximumStength)
        End If
    End Function

#End Region

End Class