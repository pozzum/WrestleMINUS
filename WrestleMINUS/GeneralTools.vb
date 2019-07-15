Imports System.IO   'Files

Public Class GeneralTools

#Region "File Based General Tools"

    'Here are App-seperate commands use as required.
    Shared Sub FolderCheck(FolderPath As String)
        If Directory.Exists(FolderPath) = False Then
            Directory.CreateDirectory(FolderPath)
        End If
    End Sub

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
                'we need to recursevley recycle the files
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
#End Region
End Class