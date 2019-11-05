Imports System.IO   'Files

Module File_FolderHandlers

    'Here are App-separate commands use as required.
    Sub FolderMake(FolderPath As String)
        If Directory.Exists(FolderPath) = False Then
            Directory.CreateDirectory(FolderPath)
        End If
    End Sub

    Function FolderMakeFromVirtualPath(VirtualFileNamePath As String)
        Dim ReturnedFilepath As String = VirtualFileNamePath.Replace("/", "\")
        FolderMake(Path.GetDirectoryName(ReturnedFilepath))
        Return ReturnedFilepath
    End Function

    Sub MoveAllItems(ByVal fromPath As String, ByVal toPath As String)
        My.Computer.FileSystem.CopyDirectory(fromPath, toPath, True)
        'Here we want to not recycle the files since the files are moved
        My.Computer.FileSystem.DeleteDirectory(fromPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
    End Sub

    Function DeleteAllItems(ByVal Path As String) As Boolean
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

    Function DeleteSafely(ByVal Path As String) As Boolean
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

    Function CheckFileWriteable(FilePath As String, Optional FileMustExist As Boolean = True) As Boolean
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
            If MessageBox.Show(FilePath & " Is Read Only!" & vbNewLine & "Would you like to make it writable?", "Make File Write-able?", MessageBoxButtons.OKCancel) = DialogResult.OK Then
                attributes = RemoveAttribute(attributes, FileAttributes.ReadOnly)
                File.SetAttributes(FilePath, attributes)
            Else
                Return False
            End If
        End If
        Return True
    End Function

    Sub MakeFolderWriteable(DirectoryPath As String)
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
    Function RemoveAttribute(ByVal attributes As FileAttributes, ByVal attributesToRemove As FileAttributes) As FileAttributes
        Return attributes And Not attributesToRemove
    End Function

    'https://stackoverflow.com/questions/50744/wait-until-file-is-unlocked-in-net
    Function WaitForFile(FullFilePath As String) As Boolean
        Dim NumberofTries As Integer = 0
        Do While NumberofTries < 10
            Try
                Using TestStream As FileStream = New FileStream(FullFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 100)
                    TestStream.ReadByte()
                    Return True
                End Using
            Catch ex As Exception

                NumberofTries += 1
                Threading.Thread.Sleep(500)
            End Try
        Loop
        MessageBox.Show(FullFilePath & vbNewLine & "Cannot be read after " & NumberofTries & " attempts.")
        Return False
    End Function

End Module