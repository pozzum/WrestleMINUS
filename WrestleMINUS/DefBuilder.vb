Imports System.IO   'Files

Public Class DefBuilder

    Shared Function RebuildDef(GameExeLocation As String,
                               Optional AnnounceFinish As Boolean = False,
                               Optional AppendtoFile As Boolean = False,
                               Optional DisableModFolderPreference As Boolean = False,
                               Optional RelocateModFolderFiles As Boolean = False)
        Dim DefFileLocation As String = Path.GetDirectoryName(GameExeLocation) & "\Chunk0.def"
        If GeneralTools.CheckFileWriteable(DefFileLocation, True) Then
            If AppendtoFile Then
                AnnounceFinish = True
                'TO DO This Code path needs to add settings handlers for the mod folder options
                Dim DefFile As System.IO.StreamWriter
                Dim OldDef As String = IO.File.ReadAllText(DefFileLocation)
                DefFile = My.Computer.FileSystem.OpenTextFileWriter(DefFileLocation, True)
                'DefFile.WriteLine(";PAC_LIST_ARC_EPAC")
                For Each TempFile In Directory.GetFiles(Path.GetDirectoryName(GameExeLocation), "*", IO.SearchOption.AllDirectories)
                    If Path.GetExtension(TempFile) = ".pac" Then
                        TempFile = TempFile.Replace((Path.GetDirectoryName(GameExeLocation) & "\"), "")
                        If OldDef.Contains(TempFile) = False Then
                            DefFile.WriteLine(TempFile) '
                        End If
                    End If
                Next
                DefFile.Close()
            Else
                IO.File.Create(DefFileLocation).Dispose()
                Dim DefFile As System.IO.StreamWriter
                DefFile = My.Computer.FileSystem.OpenTextFileWriter(DefFileLocation, True)
                DefFile.WriteLine(";PAC_LIST_ARC_EPAC")
                Dim FileList As List(Of String) = New List(Of String)
                Dim ModsFolderList As List(Of String) = New List(Of String)
                For Each TempFile In Directory.GetFiles(Path.GetDirectoryName(GameExeLocation), "*", IO.SearchOption.AllDirectories)
                    If Path.GetExtension(TempFile) = ".pac" Then
                        TempFile = TempFile.Replace((Path.GetDirectoryName(GameExeLocation) & "\"), "")
                        If TempFile.ToLower.Substring(0, 4) = "mods" Then
                            ModsFolderList.Add(TempFile)
                            'it is in the mods folder and we have to check if there is an existing file to replace
                        Else
                            FileList.Add(TempFile)
                        End If
                    End If
                Next
                Dim ExePath As String = (Path.GetDirectoryName(GameExeLocation) & "\")
                If DisableModFolderPreference Then
                    If RelocateModFolderFiles Then
                        For j As Integer = 0 To ModsFolderList.Count - 1
                            Dim TempFileName As String = Path.GetFileName(ModsFolderList(j))
                            'MessageBox.Show(TempFileName)
                            For i As Integer = 0 To FileList.Count - 1
                                If Path.GetFileName(FileList(i)) = TempFileName Then
                                    IO.File.Move(ExePath & ModsFolderList(j), ExePath & FileList(i))
                                    Continue For
                                End If
                            Next
                        Next
                    Else
                        FileList.AddRange(ModsFolderList)
                    End If
                Else
                    For j As Integer = 0 To ModsFolderList.Count - 1
                        Dim TempFileName As String = Path.GetFileName(ModsFolderList(j))
                        'MessageBox.Show(TempFileName)
                        For i As Integer = 0 To FileList.Count - 1
                            If Path.GetFileName(FileList(i)) = TempFileName Then
                                'MessageBox.Show(TempFileName)
                                FileList(i) = ModsFolderList(j)
                                Continue For
                            End If
                        Next
                    Next
                End If

                For i As Integer = 0 To FileList.Count - 1
                    DefFile.WriteLine(FileList(i))
                Next
                DefFile.Close()
            End If
            If AnnounceFinish Then
                MessageBox.Show("Def File Rebuilt")
            End If
            Return True
        Else
            Return False
        End If
    End Function

End Class