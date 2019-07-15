Imports System.IO   'Files

Public Class NodeProperties
    Public FullFilePath As String = ""
    Public FileType As PackageType = PackageType.Unchecked
    Public Index As UInt64 = 0
    Public length As UInt64
    Public StoredData As Byte()
End Class

Public Class NodeHandlers
    Shared Function GetNodeBytes(ByRef NodeTag As NodeProperties) As Byte()
        Dim FileBytes As Byte() = New Byte(NodeTag.length - 1) {}
        If NodeTag.StoredData.Length > 0 Then
            Array.Copy(NodeTag.StoredData, CInt(NodeTag.Index), FileBytes, 0, CInt(NodeTag.length))
        ElseIf NodeTag.length > 0 Then
            If Not File.Exists(NodeTag.FullFilePath) Then
                MessageBox.Show("File Not Found")
                Return New Byte() {}
            End If
            Try
                Dim ActiveReader As BinaryReader = New BinaryReader(File.Open(NodeTag.FullFilePath, FileMode.Open, FileAccess.Read))
                ActiveReader.BaseStream.Seek(NodeTag.Index, SeekOrigin.Begin)
                FileBytes = ActiveReader.ReadBytes(NodeTag.length)
                ActiveReader.Dispose()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        Else Return New Byte() {}
        End If
        Return FileBytes
    End Function
End Class
