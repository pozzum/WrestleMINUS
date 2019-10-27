Imports System.IO   'Files

Public Class GeneralTools

#Region "File Based General Tools"

    'Here are App-separate commands use as required.
    Shared Sub FolderCheck(FolderPath As String)
        If Directory.Exists(FolderPath) = False Then
            Directory.CreateDirectory(FolderPath)
        End If
    End Sub

    Shared Function GenerateFoldersFromVirtualPath(VirtualFileNamePath As String)
        Dim ReturnedFilepath As String = VirtualFileNamePath.Replace("/", "\")
        FolderCheck(Path.GetDirectoryName(ReturnedFilepath))
        Return ReturnedFilepath
    End Function

    Shared Sub MoveAllItems(ByVal fromPath As String, ByVal toPath As String)
        My.Computer.FileSystem.CopyDirectory(fromPath, toPath, True)
        'Here we want to not recycle the files since the files are moved
        My.Computer.FileSystem.DeleteDirectory(fromPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

    Shared Function DeleteAllItems(ByVal Path As String) As Boolean
        Try
            MakeFolderWriteable(Path)
            'FileIO Properties sends the items to recycling bin rather than deleting Permanently
            If My.Settings.RecycleDeletedFiles Then
                'we need to recursive recycle the files
                MessageBox.Show("Recycle")
                My.Computer.FileSystem.DeleteDirectory(Path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Else
                My.Computer.FileSystem.DeleteDirectory(Path, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Shared Function DeleteSafely(ByVal Path As String) As Boolean
        Try
            If My.Settings.RecycleDeletedFiles Then
                My.Computer.FileSystem.DeleteFile(Path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.SendToRecycleBin)
            Else
                My.Computer.FileSystem.DeleteFile(Path, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Public Shared Function CheckFileWriteable(FilePath As String, Optional FileMustExist As Boolean = True) As Boolean
        If Not File.Exists(FilePath) Then
            If FileMustExist Then
                MessageBox.Show(FilePath & vbNewLine & "Not Found")
                Return False
            Else
                Return True
            End If
        End If
        Dim attributes As FileAttributes = File.GetAttributes(FilePath)
        If (attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly Then
            If MessageBox.Show(FilePath & " Is Read Only!" & vbNewLine & "Would you like to make it writable?", "Make File Writeable?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly)
                File.SetAttributes(FilePath, attributes)
            Else
                Return False
            End If
        End If
        Return True
    End Function

    Shared Sub MakeFolderWriteable(DirectoryPath As String)
        Dim subdirectories As String() = Directory.GetDirectories(DirectoryPath)
        For Each dir As String In subdirectories
            MakeFolderWriteable(dir)
        Next
        Dim Files As String() = Directory.GetFiles(DirectoryPath)
        For Each SoloFile As String In Files
            File.SetAttributes(SoloFile, FileAttributes.Normal)
        Next
    End Sub

    'This is required to remove read only
    Shared Function RemoveAttribute(ByVal attributes As FileAttributes, ByVal attributesToRemove As FileAttributes) As FileAttributes
        Return attributes And Not attributesToRemove
    End Function

#End Region

#Region "Hex or Byte Based General Tools"

    Shared Function EndianReverse(Source As Byte(), Optional Index As Integer = 0, Optional Length As Integer = 4) As Byte()
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

    Shared Function HexCheck(StringtoCheck As String) As Boolean
        If Not IsNothing(StringtoCheck) Then
            Dim HexTest As Boolean = StringtoCheck.All(Function(c) "0123456789abcdefABCDEF".Contains(c))
            Return HexTest
        Else
            Return False
        End If
    End Function

    Shared Function HexStringToByte(HexString As String, Optional Reverse As Boolean = False) As Byte()
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

#End Region

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