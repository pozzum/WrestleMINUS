Imports System.IO   'Files
Imports System.Text 'Text Encoding 
Imports Ionic.Zlib  'zlib decompress
Imports Pfim        'https://github.com/nickbabcock/Pfim
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices



Public Class MainForm

#Region "Main Form Functions"

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.UpgradeRequired = True Then
            My.Settings.Upgrade()
            My.Settings.UpgradeRequired = False
            My.Settings.Save()
        End If
        If My.Settings.ExeLocation = "" Then
            SelectHomeDirectory()
        End If
        HexViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        TextViewBitWidth.SelectedIndex = My.Settings.BitWidthIndex
        MiscViewType.SelectedIndex = My.Settings.MiscModeIndex
        ShowViewType.SelectedIndex = My.Settings.ShowModeIndex
        If TabControl1.TabPages.Contains(StringView) Then
            TabControl1.TabPages.Remove(StringView)
        End If
        If TabControl1.TabPages.Contains(MiscView) Then
            TabControl1.TabPages.Remove(MiscView)
        End If
        If TabControl1.TabPages.Contains(ShowView) Then
            TabControl1.TabPages.Remove(ShowView)
        End If
        If TabControl1.TabPages.Contains(NIBJView) Then
            TabControl1.TabPages.Remove(NIBJView)
        End If
        If CheckCommands() Then
            LoadParameters()
        Else
            LoadHome()
        End If
    End Sub
    Private Sub SelectHomeDirectory()
        Dim TempFileDialog As OpenFileDialog = New OpenFileDialog
        TempFileDialog.FileName = "WWE2KXX.exe"
        TempFileDialog.Title = "Select WWE exe directory"
        If Directory.Exists("C:\Steam\steamapps\common\") Then
            TempFileDialog.InitialDirectory = "C:\Steam\steamapps\common\"
        End If
        TempFileDialog.ShowDialog()
        If File.Exists(TempFileDialog.FileName) AndAlso
            Path.GetExtension(TempFileDialog.FileName).ToLower = ".exe" Then
            My.Settings.ExeLocation = TempFileDialog.FileName
        Else
            If My.Settings.ExeLocation = "" Then
                SelectHomeDirectory()
            End If
        End If
    End Sub
    Shared Function CheckCommands()

        Dim args As String() = Environment.GetCommandLineArgs()
        Dim parameters As Boolean = False
        For i As Integer = 0 To args.Length - 1
            If args(i).StartsWith("-pofo") Then
                parameters = True
            ElseIf args(i).StartsWith("-moveset") Then
                parameters = True
            End If
        Next
        Return parameters
    End Function
    Private Sub MainForm_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop

    End Sub
#End Region
#Region "File Handlers"
    Public Class NodeProperties
        Public FileType As PackageType = PackageType.Unchecked
        Public Index As Long
        Public length As Long
        Public StoredData As Byte()
    End Class
    Public Enum PackageType
        Unchecked
        Unknown
        Folder
        HSPC
        SHDC
        EPK8
        EPAC
        PACH
        ZLIB
        TextureLibrary
        StringFile
        YOBJ
        DDS
        PachDirectory
        ArenaInfo
        ShowInfo
        NIBJ
    End Enum
    Dim ActiveReader As BinaryReader
    Dim ActiveFile As String = ""
    Sub CheckFile(ByRef HostNode As TreeNode, Optional SkipReader As Boolean = False)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(HostNode.Tag, NodeProperties).FileType
        NodeTag.Index = CType(HostNode.Tag, NodeProperties).Index
        NodeTag.length = CType(HostNode.Tag, NodeProperties).length
        NodeTag.StoredData = CType(HostNode.Tag, NodeProperties).StoredData
        If Not SkipReader Then
            ActiveReader = New BinaryReader(File.Open(HostNode.ToolTipText, FileMode.Open, FileAccess.Read))
        End If
        ActiveFile = HostNode.ToolTipText
        If NodeTag.FileType = PackageType.Unchecked Then 'we need to start the reader if the file exists
            If File.Exists(HostNode.ToolTipText) Then
                NodeTag.Index = 0
                NodeTag.length = ActiveReader.BaseStream.Length
                NodeTag.FileType = CheckHeaderType(0)
                NodeTag.StoredData = New Byte() {}
                HostNode.Tag = NodeTag
                CheckFile(HostNode, True)
            End If
        ElseIf NodeTag.FileType = PackageType.HSPC Then
            ActiveReader.BaseStream.Seek(&H38, SeekOrigin.Begin)
            Dim FileCount As Integer = ActiveReader.ReadUInt32()
            For i As Integer = 0 To FileCount - 1
                ActiveReader.BaseStream.Seek(&H800, SeekOrigin.Begin)
                ActiveReader.BaseStream.Seek(i * &H14, SeekOrigin.Current)
                Dim FileName As String = Hex(ActiveReader.ReadUInt64).ToUpper
                Dim TempNode As TreeNode = New TreeNode(FileName)
                TempNode.ToolTipText = HostNode.ToolTipText
                ActiveReader.BaseStream.Seek(&H1000, SeekOrigin.Begin)
                ActiveReader.BaseStream.Seek(i * &HC, SeekOrigin.Current)
                Dim TempNodeProps As NodeProperties = New NodeProperties
                TempNodeProps.Index = (ActiveReader.ReadUInt32() * &H800)
                TempNodeProps.length = (ActiveReader.ReadUInt32() * &H100)
                TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index)
                TempNodeProps.StoredData = New Byte() {}
                TempNode.Tag = TempNodeProps
                'CheckFile(TempNode)
                HostNode.Nodes.Add(TempNode)
            Next
        ElseIf NodeTag.FileType = PackageType.SHDC Then
            ActiveReader.BaseStream.Seek(NodeTag.Index + &H1C, SeekOrigin.Begin)
            Dim TempHeaderStart As Integer = ActiveReader.ReadUInt32
            Dim TempHeaderLength As Integer = ActiveReader.ReadUInt32
            Dim PachPartsCount As Integer = (TempHeaderLength / &H10) '1 index
            'Dim TempNodes As List(Of TreeNode) = New List(Of TreeNode)
            For i As Integer = 0 To PachPartsCount - 1
                ActiveReader.BaseStream.Seek(NodeTag.Index + TempHeaderStart + (i * &H10), SeekOrigin.Begin)
                Dim PartName As String = Hex(ActiveReader.ReadUInt32)
                'MessageBox.Show(PartName)
                Dim TempNode As TreeNode = New TreeNode(PartName)
                TempNode.ToolTipText = HostNode.ToolTipText
                Dim TempNodeProps As NodeProperties = New NodeProperties
                TempNodeProps.Index = NodeTag.Index + ActiveReader.ReadUInt32()
                TempNodeProps.length = ActiveReader.ReadUInt64
                TempNodeProps.StoredData = New Byte() {}
                TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index)
                TempNode.Tag = TempNodeProps
                HostNode.Nodes.Add(TempNode)
            Next
            'For i As Integer = 0 To TempNodes.Count - 1
            'CheckFile(TempNodes(i))
            'Next
            'For i As Integer = 0 To TempNodes.Count - 1
            'HostNode.Nodes.Add(TempNodes(i))
            'Next
        ElseIf NodeTag.FileType = PackageType.EPK8 Then
            ActiveReader.BaseStream.Seek(&H4, SeekOrigin.Begin)
            Dim HeaderLength As Integer = ActiveReader.ReadUInt32()
            Dim index As Integer = 0
            Do While index < HeaderLength - 1
                ActiveReader.BaseStream.Seek(&H800 + index, SeekOrigin.Begin)
                Dim DirectoryName As String = ActiveReader.ReadChars(4)
                Dim DirectoryTreeNode As TreeNode = New TreeNode(DirectoryName)
                Dim DirectoryContainsCount As Integer = ActiveReader.ReadUInt16 / 4
                Dim DirectoryNodeProps As NodeProperties = New NodeProperties
                DirectoryNodeProps.StoredData = New Byte() {}
                DirectoryNodeProps.FileType = PackageType.PachDirectory
                index += &HC
                For i As Integer = 0 To DirectoryContainsCount - 1
                    ActiveReader.BaseStream.Seek(&H800 + index, SeekOrigin.Begin)
                    Dim PachName As String = ActiveReader.ReadChars(8)
                    Dim TempNode As TreeNode = New TreeNode(PachName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.Index = ActiveReader.ReadUInt32() * &H800 + &H4000
                    If i = 0 Then
                        DirectoryNodeProps.Index = TempNodeProps.Index
                    End If
                    TempNodeProps.length = ActiveReader.ReadUInt32() * &H100
                    DirectoryNodeProps.length += TempNodeProps.length
                    TempNodeProps.StoredData = New Byte() {}
                    TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index)
                    index += &H10
                    TempNode.Tag = TempNodeProps
                    DirectoryTreeNode.Nodes.Add(TempNode)
                Next
                DirectoryTreeNode.Tag = DirectoryNodeProps
                HostNode.Nodes.Add(DirectoryTreeNode)
            Loop
        ElseIf NodeTag.FileType = PackageType.EPAC Then
            ActiveReader.BaseStream.Seek(&H4, SeekOrigin.Begin)
            Dim HeaderLength As Integer = ActiveReader.ReadUInt32()
            Dim index As Integer = 0
            Do While index < HeaderLength - 1
                ActiveReader.BaseStream.Seek(&H800 + index, SeekOrigin.Begin)
                Dim DirectoryName As String = ActiveReader.ReadChars(4)
                Dim DirectoryTreeNode As TreeNode = New TreeNode(DirectoryName)
                Dim DirectoryContainsCount As Integer = ActiveReader.ReadUInt16 / 3
                Dim DirectoryNodeProps As NodeProperties = New NodeProperties
                DirectoryNodeProps.StoredData = New Byte() {}
                DirectoryNodeProps.FileType = PackageType.PachDirectory
                index += &HC
                For i As Integer = 0 To DirectoryContainsCount - 1
                    ActiveReader.BaseStream.Seek(&H800 + index, SeekOrigin.Begin)
                    Dim PachName As String = ActiveReader.ReadChars(4)
                    'MessageBox.Show(PachName)
                    Dim TempNode As TreeNode = New TreeNode(PachName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.Index = ActiveReader.ReadUInt32() * &H800 + &H4000
                    If i = 0 Then
                        DirectoryNodeProps.Index = TempNodeProps.Index
                    End If
                    TempNodeProps.length = ActiveReader.ReadUInt32() * &H100
                    DirectoryNodeProps.length += TempNodeProps.length
                    TempNodeProps.StoredData = New Byte() {}
                    TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index)
                    index += &HC
                    TempNode.Tag = TempNodeProps
                    DirectoryTreeNode.Nodes.Add(TempNode)
                Next
                DirectoryTreeNode.Tag = DirectoryNodeProps
                HostNode.Nodes.Add(DirectoryTreeNode)
            Loop
        ElseIf NodeTag.FileType = PackageType.PACH Then
            ActiveReader.BaseStream.Seek(NodeTag.Index + &H4, SeekOrigin.Begin)
            Dim Partcount As Integer = ActiveReader.ReadUInt32
            For i As Integer = 0 To Partcount - 1
                ActiveReader.BaseStream.Seek(NodeTag.Index + &H8 + (i * &HC), SeekOrigin.Begin)
                Dim PartName As String = Hex(ActiveReader.ReadUInt32)
                Dim TempNode As TreeNode = New TreeNode(PartName)
                TempNode.ToolTipText = HostNode.ToolTipText
                Dim TempNodeProps As NodeProperties = New NodeProperties
                TempNodeProps.Index = NodeTag.Index + ActiveReader.ReadUInt32() + &H8 + Partcount * &HC
                TempNodeProps.length = ActiveReader.ReadUInt32()
                TempNodeProps.StoredData = New Byte() {}
                TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index)
                TempNode.Tag = TempNodeProps
                HostNode.Nodes.Add(TempNode)
            Next
        ElseIf NodeTag.FileType = PackageType.ZLIB Then
            ActiveReader.BaseStream.Seek(0, SeekOrigin.Begin)
            Dim FileBytes As Byte() = ActiveReader.ReadBytes(ActiveReader.BaseStream.Length)
            Dim ZlibBytes As Byte() = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, ZlibBytes, 0, NodeTag.length)
            Dim align As Byte() = New Byte(3) {}
            Array.Copy(ZlibBytes, 4, align, 0, 4)
            Dim compressed As Byte() = New Byte(3) {}
            Array.Copy(ZlibBytes, 8, compressed, 0, 4)
            Dim uncompressed As Byte() = New Byte(3) {}
            Array.Copy(ZlibBytes, 12, uncompressed, 0, 4)
            Dim input As Byte() = New Byte(BitConverter.ToInt32(compressed, 0)) {}
            Array.Copy(ZlibBytes, 16, input, 0, NodeTag.length - 16)
            Dim output As Byte() = New Byte(BitConverter.ToInt32(uncompressed, 0)) {}
            output = ZlibStream.UncompressBuffer(input)
            Dim TempNode As TreeNode = New TreeNode(HostNode.Text & " UNCOMPRESS")
            TempNode.ToolTipText = HostNode.ToolTipText
            Dim TempNodeProps As NodeProperties = New NodeProperties
            TempNodeProps.Index = 0
            TempNodeProps.length = output.Length
            TempNodeProps.FileType = CheckHeaderType(TempNodeProps.Index, output)
            'MessageBox.Show(output.Length)
            TempNodeProps.StoredData = output
            TempNode.Tag = TempNodeProps
            HostNode.Nodes.Add(TempNode)
        ElseIf NodeTag.FileType = PackageType.YOBJ Then
            'To Be Added
        ElseIf NodeTag.FileType = PackageType.TextureLibrary Then
            'To Be Added
            If NodeTag.StoredData.Length > 0 Then
                Dim TextureCount As Integer = NodeTag.StoredData(0)
                For i As Integer = 0 To TextureCount - 1
                    Dim ImageName As String = Encoding.Default.GetChars(NodeTag.StoredData, i * &H20 + &H10, &H10)
                    Dim TempNode As TreeNode = New TreeNode(ImageName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.FileType = PackageType.DDS
                    TempNodeProps.length = BitConverter.ToUInt32(NodeTag.StoredData, i * &H20 + &H10 + &H14)
                    TempNodeProps.Index = BitConverter.ToUInt64(NodeTag.StoredData, i * &H20 + &H10 + &H18)
                    TempNodeProps.StoredData = NodeTag.StoredData
                    TempNode.Tag = TempNodeProps
                    HostNode.Nodes.Add(TempNode)
                Next
            Else
                ActiveReader.BaseStream.Seek(NodeTag.Index, SeekOrigin.Begin)
                Dim TextureCount As Integer = ActiveReader.ReadByte
                ActiveReader.BaseStream.Seek(NodeTag.Index & &H10, SeekOrigin.Begin)
                For i As Integer = 0 To TextureCount - 1
                    Dim ImageName As String = ActiveReader.ReadChars(&H10)
                    Dim TempNode As TreeNode = New TreeNode(ImageName)
                    TempNode.ToolTipText = HostNode.ToolTipText
                    Dim TempNodeProps As NodeProperties = New NodeProperties
                    TempNodeProps.FileType = PackageType.DDS
                    ActiveReader.BaseStream.Seek(&H4, SeekOrigin.Current)
                    TempNodeProps.length = ActiveReader.ReadUInt32
                    TempNodeProps.Index = ActiveReader.ReadUInt64
                    TempNodeProps.StoredData = New Byte() {}
                    TempNode.Tag = TempNodeProps
                    HostNode.Nodes.Add(TempNode)
                Next
            End If
        ElseIf NodeTag.FileType = PackageType.StringFile Then
            'Do nothing
        ElseIf NodeTag.FileType = PackageType.Unknown Then
        End If
        HostNode.Tag = NodeTag

    End Sub
    Function CheckHeaderType(Optional Index As Long = 0, Optional ByVal ByteArray As Byte() = Nothing)
        'To be split into 2 seperate functions once all processes are added
        Dim FirstFour As String
        'Make sure the file has bytes
        If ActiveReader.BaseStream.Length = 0 Then
            Return PackageType.Unknown
        End If
        If ByteArray Is Nothing Then
            ActiveReader.BaseStream.Seek(Index, SeekOrigin.Begin)
            FirstFour = ActiveReader.ReadChars(4)
        Else
            FirstFour = Encoding.Default.GetChars(ByteArray, 0, 4)
        End If
        If FirstFour = "HSPC" Then
            Return PackageType.HSPC
        ElseIf FirstFour = "SHDC" Then
            Return PackageType.SHDC
        ElseIf FirstFour = "EPK8" Then
            Return PackageType.EPK8
        ElseIf FirstFour = "PACH" Then
            Return PackageType.PACH
        ElseIf FirstFour = "EPAC" Then
            Return PackageType.EPAC
        ElseIf FirstFour = "ZLIB" Then
            Return PackageType.ZLIB
        ElseIf FirstFour = "YOBJ" Then
            Return PackageType.YOBJ
        ElseIf FirstFour = "NIBJ" Then
            Return PackageType.NIBJ
        ElseIf FirstFour.Contains("STG") Then
            Return PackageType.ShowInfo
        Else
            'dds check
            Dim DDSCheck As String = ""
            If ByteArray Is Nothing Then
                ActiveReader.BaseStream.Seek(Index + &H20, SeekOrigin.Begin)
                DDSCheck = ActiveReader.ReadChars(3)

            Else
                DDSCheck = Encoding.Default.GetChars(ByteArray, &H20, 3)
            End If
            If DDSCheck = "dds" Then
                Return PackageType.TextureLibrary
            End If
            Dim MiscCheck As String = ""
            If ByteArray Is Nothing Then
                ActiveReader.BaseStream.Seek(Index + &H10, SeekOrigin.Begin)
                MiscCheck = ActiveReader.ReadChars(4)
            Else
                MiscCheck = Encoding.Default.GetChars(ByteArray, &H10, 4)
            End If
            If MiscCheck = "aren" Then
                Return PackageType.ArenaInfo
            End If
            'String File Test
            Dim StringTest As UInt32
            If ByteArray Is Nothing Then
                ActiveReader.BaseStream.Seek(Index, SeekOrigin.Begin)
                StringTest = ActiveReader.ReadUInt32
            Else
                StringTest = BitConverter.ToUInt32(ByteArray, 0)
            End If
            If StringTest = 0 Then
                Return PackageType.StringFile
            End If
            Return PackageType.Unknown
        End If
    End Function
#End Region
#Region "Menu Strip"
    Dim SelectedFile As String
    Private Sub LoadHomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadHomeToolStripMenuItem.Click
        LoadHome()
    End Sub
    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            SelectedFile = OpenFileDialog1.FileName
            LoadParameters()
        End If
    End Sub
#End Region
#Region "TreeView Population"
    Sub LoadParameters()
        TreeView1.Nodes.Clear()
        ProgressBar1.Value = 0
    End Sub
    Sub LoadHome()
        TreeView1.Nodes.Clear()
        ProgressBar1.Value = 0
        Dim HomeDirectory As String = Path.GetDirectoryName(My.Settings.ExeLocation) & Path.DirectorySeparatorChar
        CurrentViewToolStripMenuItem.Text = "Current View: " & HomeDirectory
        Dim HomeDI As DirectoryInfo = New DirectoryInfo(HomeDirectory)
        ProgressBar1.Maximum = Directory.GetFiles(HomeDirectory, "*.*", SearchOption.AllDirectories).Length +
                                Directory.GetDirectories(HomeDirectory, "**", SearchOption.AllDirectories).Length
        Dim TempNode As TreeNode = TreeView1.Nodes.Add(HomeDI.Name)
        TempNode.ToolTipText = HomeDI.FullName
        TempNode.Tag = New NodeProperties With {.FileType = PackageType.Folder}
        TempNode.StateImageIndex = 0
        LoadFiles(HomeDirectory, TempNode)
        LoadSubDirectories(HomeDirectory, TempNode)
    End Sub
    Sub LoadSubDirectories(DirectoryPath As String, ParentNode As TreeNode)
        Dim ListofSubDirectores() As String = Directory.GetDirectories(DirectoryPath)
        For Each SubDirectory As String In ListofSubDirectores
            Dim NewDI As DirectoryInfo = New DirectoryInfo(SubDirectory)
            Dim TempNode As TreeNode = ParentNode.Nodes.Add(NewDI.Name)
            TempNode.ToolTipText = NewDI.FullName
            TempNode.Tag = New NodeProperties With {.FileType = PackageType.Folder}
            TempNode.StateImageIndex = 0
            LoadFiles(SubDirectory, TempNode)
            LoadSubDirectories(SubDirectory, TempNode)
            UpdateProgress()
        Next
    End Sub
    Sub LoadFiles(DirectoryPath As String, ParentNode As TreeNode)
        Dim ListofFiles() As String = Directory.GetFiles(DirectoryPath)
        For Each FilePath As String In ListofFiles
            Dim NewFI As FileInfo = New FileInfo(FilePath)
            Dim TempNode As TreeNode = ParentNode.Nodes.Add(NewFI.Name)
            TempNode.ToolTipText = NewFI.FullName
            TempNode.Tag = New NodeProperties With {.FileType = PackageType.Unchecked,
                .Index = 0}
            TempNode.StateImageIndex = 1
            UpdateProgress()
        Next
    End Sub
    Sub UpdateProgress()
        If ProgressBar1.Value < ProgressBar1.Maximum Then
            ProgressBar1.Value += 1
            Dim Percent As Integer = CInt((ProgressBar1.Value / ProgressBar1.Maximum) * 100)
            ProgressBar1.CreateGraphics().DrawString(Percent.ToString() + "%",
                                                     New Font("Arial", 8.25, FontStyle.Regular),
                                                     Brushes.Black,
                                                     New PointF(ProgressBar1.Width / 2 - 10,
                                                                ProgressBar1.Height / 2 - 7))
            'Application.DoEvents()
        End If
    End Sub

    Private Sub TreeView1_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterSelect
        If File.Exists(e.Node.ToolTipText.ToString()) Then
            If Not CType(e.Node.Tag, NodeProperties).FileType = PackageType.Unknown Then
                If e.Node.Nodes.Count = 0 Then
                    CheckFile(e.Node)
                    ActiveReader.Close()
                End If
            End If
            HexViewFileName.Text = TreeView1.SelectedNode.Text
            AddHexText(TreeView1.SelectedNode)
            TextViewFileName.Text = TreeView1.SelectedNode.Text
            AddText(TreeView1.SelectedNode)
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.StringFile Then
                'StringView
                If Not TabControl1.TabPages.Contains(StringView) Then
                    TabControl1.TabPages.Add(StringView)
                    FillStringView(TreeView1.SelectedNode)
                End If
            Else
                If TabControl1.TabPages.Contains(StringView) Then
                    TabControl1.TabPages.Remove(StringView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.ArenaInfo Then
                'StringView
                If Not TabControl1.TabPages.Contains(MiscView) Then
                    TabControl1.TabPages.Add(MiscView)
                    FillMiscView(TreeView1.SelectedNode)
                End If
            Else
                If TabControl1.TabPages.Contains(MiscView) Then
                    TabControl1.TabPages.Remove(MiscView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.ShowInfo Then
                'StringView
                If Not TabControl1.TabPages.Contains(ShowView) Then
                    TabControl1.TabPages.Add(ShowView)
                    FillShowView(TreeView1.SelectedNode)
                End If
            Else
                If TabControl1.TabPages.Contains(ShowView) Then
                    TabControl1.TabPages.Remove(ShowView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.NIBJ Then
                'StringView
                If Not TabControl1.TabPages.Contains(NIBJView) Then
                    TabControl1.TabPages.Add(NIBJView)
                    FillNIBJView(TreeView1.SelectedNode)
                End If
            Else
                If TabControl1.TabPages.Contains(NIBJView) Then
                    TabControl1.TabPages.Remove(NIBJView)
                End If
            End If
            If CType(e.Node.Tag, NodeProperties).FileType = PackageType.DDS Then
                'StringView
                If Not TabControl1.TabPages.Contains(PictureView) Then
                    TabControl1.TabPages.Add(PictureView)
                    LoadPicture(TreeView1.SelectedNode)
                End If
            Else
                If TabControl1.TabPages.Contains(PictureView) Then
                    TabControl1.TabPages.Remove(PictureView)
                End If
            End If
        End If
    End Sub

#End Region
#Region "Hex View Controls"
    Sub AddHexText(SelectedFilePath As TreeNode)
        If File.Exists(SelectedFilePath.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If HexViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(HexViewBitWidth.Text)
            Else
                bitwidth = CInt(HexViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As NodeProperties = CType(SelectedFilePath.Tag, NodeProperties)
            Dim Filebytes As Byte()
            If NodeTag.StoredData.Length > 0 Then
                Filebytes = NodeTag.StoredData
            Else
                Filebytes = File.ReadAllBytes(SelectedFilePath.ToolTipText)
            End If
            Dim HexLength As Long = 0
            If NodeTag.length = 0 Then
                HexLength = Filebytes.Length
            Else
                HexLength = NodeTag.length
            End If
            Dim ByteString As String = ""
            If HexLength < &H1000 Then
                ByteString = (BitConverter.ToString(Filebytes, NodeTag.Index, HexLength).Replace("-", " "))
            Else
                ByteString = (BitConverter.ToString(Filebytes, NodeTag.Index, &H1000).Replace("-", " "))
            End If
            'SelectedFilePath = SelectedFilePath.Replace(vbCr, "").Replace(vbLf, "")
            Dim builder As New StringBuilder(ByteString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 3)
            For i As Int32 = startIndex To (bitwidth * 3) Step -(bitwidth * 3)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Hex_Selected.Text = builder.ToString()
        End If
    End Sub
    Private Sub ToolStripComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles HexViewBitWidth.TextChanged
        If HexViewBitWidth.Text.Length > 0 AndAlso
           CInt(HexViewBitWidth.Text) > 0 Then
            If HexViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = HexViewBitWidth.SelectedIndex
                TextViewBitWidth.SelectedIndex = HexViewBitWidth.SelectedIndex
            Else
                TextViewBitWidth.SelectedIndex = -1
                TextViewBitWidth.Text = HexViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddHexText(TreeView1.SelectedNode)
            End If
        End If
    End Sub
#End Region
#Region "Text View Controls"
    Sub AddText(SelectedFilePath As TreeNode)
        If File.Exists(SelectedFilePath.ToolTipText) Then
            Dim bitwidth As Integer = 0
            If TextViewBitWidth.Text.Length > 0 Then
                bitwidth = CInt(TextViewBitWidth.Text)
            Else
                bitwidth = CInt(TextViewBitWidth.SelectedItem)
            End If
            Dim NodeTag As NodeProperties = CType(SelectedFilePath.Tag, NodeProperties)
            Dim Filebytes As Byte()
            If NodeTag.StoredData.Length > 0 Then
                Filebytes = NodeTag.StoredData
            Else
                Filebytes = File.ReadAllBytes(SelectedFilePath.ToolTipText)
            End If
            Dim TextString As String = ""
            Dim HexLength As Long = 0
            If NodeTag.length = 0 Then
                HexLength = Filebytes.Length
            Else
                HexLength = NodeTag.length
            End If
            If HexLength < &H1000 Then
                TextString = New String(".", HexLength)
            Else
                TextString = New String(".", &H1000)
            End If
            Dim FirstBuilder As New StringBuilder(TextString)
            For i As Integer = 0 To TextString.Length - 1
                If Filebytes(i + NodeTag.Index) > 31 AndAlso (Filebytes(i + NodeTag.Index) < 257) Then
                    FirstBuilder(i) = Encoding.Default.GetChars(Filebytes, i + NodeTag.Index, 1)(0)
                End If
            Next
            TextString = FirstBuilder.ToString()
            TextString = TextString.Replace(vbCr, ".").Replace(vbLf, ".")
            Dim builder As New StringBuilder(TextString)
            Dim startIndex = builder.Length - (builder.Length Mod bitwidth * 1)
            For i As Int32 = startIndex To (bitwidth * 1) Step -(bitwidth * 1)
                builder.Insert(i, vbCr & vbLf)
            Next i
            Text_Selected.Text = builder.ToString()
        End If
    End Sub
    Private Sub TextViewBitWidth_TextChanged(sender As Object, e As EventArgs) Handles TextViewBitWidth.TextChanged
        If TextViewBitWidth.Text.Length > 0 AndAlso
           CInt(TextViewBitWidth.Text) > 0 Then
            If TextViewBitWidth.SelectedIndex > -1 Then
                My.Settings.BitWidthIndex = TextViewBitWidth.SelectedIndex
                HexViewBitWidth.SelectedIndex = TextViewBitWidth.SelectedIndex
            Else
                HexViewBitWidth.SelectedIndex = -1
                HexViewBitWidth.Text = TextViewBitWidth.Text
            End If
            If TreeView1.SelectedNode IsNot Nothing Then
                AddText(TreeView1.SelectedNode)
            End If
        End If
    End Sub

#End Region
#Region "String View Controls"
    Dim StringList(&H100000) As String
    Sub FillStringView(SelectedData As TreeNode)
        DataGridStringView.Rows.Clear()
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim StringBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            StringBytes = NodeTag.StoredData
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            StringBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, StringBytes, 0, NodeTag.length)
        End If
        Dim StringCount As Integer = BitConverter.ToInt32(StringBytes, 4)
        StringCountToolStripMenuItem.Text = "String Count: " & StringCount
        ProgressBar1.Maximum = StringCount
        ProgressBar1.Value = 0
        'Get Data On the Pach parts
        Dim StringFileOffset(Int16.MaxValue) As Integer
        Dim StringFileLength(Int16.MaxValue) As Integer
        Dim StringFileReference(Int16.MaxValue) As Integer
        For j As Integer = 0 To StringCount - 1
            StringFileOffset(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 0)
            StringFileLength(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 4)
            StringFileReference(j) = BitConverter.ToInt32(StringBytes, 8 + j * 12 + 8)
            'Trim all 00 chars so the strings don't end abrubtly in future manipulation
            StringList(StringFileReference(j)) = Encoding.Default.GetString(StringBytes, StringFileOffset(j), StringFileLength(j)).TrimEnd(Chr(0))
            DataGridStringView.Rows.Add(Hex(StringFileReference(j)),
                                        StringList(StringFileReference(j)),
                                        StringFileLength(j).ToString)
            ProgressBar1.Value = j
        Next

    End Sub
#End Region
#Region "Misc View Controls"
    Sub FillMiscView(SelectedData As TreeNode)
        DataGridMiscView.Rows.Clear()
        DataGridMiscView.Columns.Clear()
        Dim GameType As Integer = MiscViewType.SelectedIndex
        GetMiscColumns(GameType)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim MiscBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            MiscBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, MiscBytes, 0, NodeTag.length)
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            MiscBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, MiscBytes, 0, NodeTag.length)
        End If
        Dim ArenaCount As Integer = BitConverter.ToInt32(MiscBytes, 0)
        For i As Integer = 0 To ArenaCount - 1
            Dim ArenaNum As String = Encoding.ASCII.GetString(MiscBytes, 25 + i * 32, 2)
            Dim TempIndex As Integer = BitConverter.ToInt32(MiscBytes, 40 + i * 32)
            Dim TempLength As Integer = BitConverter.ToInt32(MiscBytes, 36 + i * 32)
            Dim ArenaJson As String = Encoding.ASCII.GetString(MiscBytes, TempIndex, TempLength)
            'MessageBox.Show(Arena_String)
            Dim Stadium As String = ArenaJson.Substring(ArenaJson.IndexOf("Stadium") + 9,
                                                        ArenaJson.IndexOf(",", ArenaJson.IndexOf("Stadium") + 9) - ArenaJson.IndexOf("Stadium") - 9)
            Dim Advert As String = ArenaJson.Substring(ArenaJson.IndexOf("Advertisement") + 15,
                                                       ArenaJson.IndexOf(",", ArenaJson.IndexOf("Advertisement") + 15) - ArenaJson.IndexOf("Advertisement") - 15)
            Dim CornerPost As String = ArenaJson.Substring(ArenaJson.IndexOf("CornerPost") + 12,
                                                           ArenaJson.IndexOf(",", ArenaJson.IndexOf("CornerPost") + 12) - ArenaJson.IndexOf("CornerPost") - 12)
            Dim TempLEDCorner As Integer = ArenaJson.IndexOf("LED_CornerPost")
            Dim LEDCorner As String = "-1"
            If TempLEDCorner = -1 Then
            Else
                LEDCorner = ArenaJson.Substring(ArenaJson.IndexOf("LED_CornerPost") + 16, ArenaJson.IndexOf(",", ArenaJson.IndexOf("LED_CornerPost") + 16) - ArenaJson.IndexOf("LED_CornerPost") - 16)
            End If
            Dim Rope As String = ArenaJson.Substring(ArenaJson.IndexOf("Rope") + 6,
                                                     ArenaJson.IndexOf(",", ArenaJson.IndexOf("Rope") + 6) - ArenaJson.IndexOf("Rope") - 6)
            Dim Apron As String = ArenaJson.Substring(ArenaJson.IndexOf("Apron") + 7,
                                                      ArenaJson.IndexOf(",", ArenaJson.IndexOf("Apron") + 7) - ArenaJson.IndexOf("Apron") - 7)
            Dim TempLEDApron As Integer = ArenaJson.IndexOf("LED_Apron")
            Dim LEDApron As String = "-1"
            If TempLEDApron = -1 Then
            Else
                LEDApron = ArenaJson.Substring(ArenaJson.IndexOf("LED_Apron") + 11,
                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("LED_Apron") + 11) - ArenaJson.IndexOf("LED_Apron") - 11)
            End If
            Dim Turnbuckle As String = ArenaJson.Substring(ArenaJson.IndexOf("Turnbuckle") + 12,
                                                           ArenaJson.IndexOf(",", ArenaJson.IndexOf("Turnbuckle") + 12) - ArenaJson.IndexOf("Turnbuckle") - 12)
            Dim Barricade As String = ArenaJson.Substring(ArenaJson.IndexOf("Barricade") + 11,
                                                          ArenaJson.IndexOf(",", ArenaJson.IndexOf("Barricade") + 11) - ArenaJson.IndexOf("Barricade") - 11)
            Dim Fence As String = ArenaJson.Substring(ArenaJson.IndexOf("Fence") + 7,
                                                      ArenaJson.IndexOf(",", ArenaJson.IndexOf("Fence") + 7) - ArenaJson.IndexOf("Fence") - 7)
            Dim CeilingLight As String = ArenaJson.Substring(ArenaJson.IndexOf("CeilingLighting") + 17,
                                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("CeilingLighting") + 17) - ArenaJson.IndexOf("CeilingLighting") - 17)
            Dim SpotLight As String = ArenaJson.Substring(ArenaJson.IndexOf("Spotlight") + 11,
                                                          ArenaJson.IndexOf(",", ArenaJson.IndexOf("Spotlight") + 11) - ArenaJson.IndexOf("Spotlight") - 11)
            Dim Stairs As String = ArenaJson.Substring(ArenaJson.IndexOf("Stairs") + 8,
                                                       ArenaJson.IndexOf(",", ArenaJson.IndexOf("Stairs") + 8) - ArenaJson.IndexOf("Stairs") - 8)
            Dim CommentarySeat As String = ArenaJson.Substring(ArenaJson.IndexOf("CommentarySeat") + 16,
                                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("CommentarySeat") + 16) - ArenaJson.IndexOf("CommentarySeat") - 16)
            Dim RingMat As String = ArenaJson.Substring(ArenaJson.IndexOf("RingMat") + 9,
                                                        ArenaJson.IndexOf(",", ArenaJson.IndexOf("RingMat") + 9) - ArenaJson.IndexOf("RingMat") - 9)
            Dim FloorMat As String = ArenaJson.Substring(ArenaJson.IndexOf("FloorMattress") + 15,
                                                         ArenaJson.IndexOf(",", ArenaJson.IndexOf("FloorMattress") + 15) - ArenaJson.IndexOf("FloorMattress") - 15)
            Dim Crowd As String = ArenaJson.Substring(ArenaJson.IndexOf("Crowd") + 7,
                                                      ArenaJson.IndexOf(",", ArenaJson.IndexOf("Crowd") + 7) - ArenaJson.IndexOf("Crowd") - 7)
            Dim IBL As String = ArenaJson.Substring(ArenaJson.IndexOf("IBL") + 5,
                                                    ArenaJson.IndexOf(",", ArenaJson.IndexOf("IBL") + 5) - ArenaJson.IndexOf("IBL") - 5)
            Dim TempTitantron As Integer = ArenaJson.IndexOf("Titantron")
            Dim Titantron As String = "-1"
            If TempTitantron = -1 Then
            Else
                Titantron = ArenaJson.Substring(ArenaJson.IndexOf("Titantron") + 11,
                                                ArenaJson.IndexOf(",", ArenaJson.IndexOf("Titantron") + 11) - ArenaJson.IndexOf("Titantron") - 11)
            End If
            Dim TempMinitron As Integer = ArenaJson.IndexOf("Minitron")
            Dim Minitron As String = "-1"
            If TempMinitron = -1 Then
            Else
                Minitron = ArenaJson.Substring(ArenaJson.IndexOf("Minitron") + 10,
                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("Minitron") + 10) - ArenaJson.IndexOf("Minitron") - 10)
            End If
            Dim TempWall_L As Integer = ArenaJson.IndexOf("Wall_L")
            Dim Wall_L As String = "-1"
            If TempWall_L = -1 Then
            Else
                Wall_L = ArenaJson.Substring(ArenaJson.IndexOf("Wall_L") + 8,
                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("Wall_L") + 8) - ArenaJson.IndexOf("Wall_L") - 8)
            End If
            Dim TempWall_R As Integer = ArenaJson.IndexOf("Wall_R")
            Dim Wall_R As String = "-1"
            If TempWall_R = -1 Then
            Else
                Wall_R = ArenaJson.Substring(ArenaJson.IndexOf("Wall_R") + 8,
                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("Wall_R") + 8) - ArenaJson.IndexOf("Wall_R") - 8)
            End If
            Dim TempHeader As Integer = ArenaJson.IndexOf("Header")
            Dim Header As String = "-1"
            If TempHeader = -1 Then
            Else
                Header = ArenaJson.Substring(ArenaJson.IndexOf("Header") + 8,
                                             ArenaJson.IndexOf(",", ArenaJson.IndexOf("Header") + 8) - ArenaJson.IndexOf("Header") - 8)
            End If
            Dim TempFloor As Integer = ArenaJson.IndexOf("Floor""")
            Dim Floor As String = "-1"
            If TempFloor = -1 Then
            Else
                Floor = ArenaJson.Substring(ArenaJson.IndexOf("Floor""") + 7,
                                            ArenaJson.IndexOf(",", ArenaJson.IndexOf("Floor""") + 7) - ArenaJson.IndexOf("Floor""") - 7)
            End If
            Dim TempMiscObject As Integer = ArenaJson.IndexOf("Floor""")
            Dim MiscObject As String = "-1"
            If TempMiscObject = -1 Then
            Else
                MiscObject = ArenaJson.Substring(ArenaJson.IndexOf("MiscObjects") + 13,
                                                 ArenaJson.IndexOf(",", ArenaJson.IndexOf("MiscObjects") + 13) - ArenaJson.IndexOf("MiscObjects") - 13)
            End If
            Dim TempLightType As Integer = ArenaJson.IndexOf("LightingType")
            Dim LightingType As String = "-1"
            If TempLightType = -1 Then
            Else
                LightingType = ArenaJson.Substring(ArenaJson.IndexOf("LightingType") + 14,
                                                   ArenaJson.IndexOf(",", ArenaJson.IndexOf("LightingType") + 14) - ArenaJson.IndexOf("LightingType") - 14)
            End If
            Dim TempCornerCM As Integer = ArenaJson.IndexOf("CornerPost_CM")
            Dim CornerPost_CM As String = "0"
            If TempCornerCM = -1 Then
            Else
                CornerPost_CM = ArenaJson.Substring(ArenaJson.IndexOf("CornerPost_CM") + 15,
                                                    ArenaJson.IndexOf(",", ArenaJson.IndexOf("CornerPost_CM") + 15) - ArenaJson.IndexOf("CornerPost_CM") - 15)
            End If
            Dim TempRopeCM As Integer = ArenaJson.IndexOf("Rope_CM")
            Dim Rope_CM As String = "0"
            If TempRopeCM = -1 Then
            Else
                Rope_CM = ArenaJson.Substring(ArenaJson.IndexOf("Rope_CM") + 9,
                                              ArenaJson.IndexOf(",", ArenaJson.IndexOf("Rope_CM") + 9) - ArenaJson.IndexOf("Rope_CM") - 9)
            End If
            Dim TempApronCM As Integer = ArenaJson.IndexOf("Apron_CM")
            Dim Apron_CM As String = "0"
            If TempApronCM = -1 Then
            Else
                Apron_CM = ArenaJson.Substring(ArenaJson.IndexOf("Apron_CM") + 10,
                                               ArenaJson.IndexOf(",", ArenaJson.IndexOf("Apron_CM") + 10) - ArenaJson.IndexOf("Apron_CM") - 10)
            End If
            Dim TempTurnCM As Integer = ArenaJson.IndexOf("Turnbuckle_CM")
            Dim Turnbuckle_CM As String = "0"
            If TempTurnCM = -1 Then
            Else
                Turnbuckle_CM = ArenaJson.Substring(ArenaJson.IndexOf("Turnbuckle_CM") + 15,
                                                    ArenaJson.IndexOf(",", ArenaJson.IndexOf("Turnbuckle_CM") + 15) - ArenaJson.IndexOf("Turnbuckle_CM") - 15)
            End If
            Dim TempRinmMatCM As Integer = ArenaJson.IndexOf("Turnbuckle_CM")
            Dim RingMat_CM As String = "0"
            If TempRinmMatCM = -1 Then
            Else
                RingMat_CM = ArenaJson.Substring(ArenaJson.IndexOf("RingMat_CM") + 12,
                                                 ArenaJson.IndexOf(",", ArenaJson.IndexOf("RingMat_CM") + 12) - ArenaJson.IndexOf("RingMat_CM") - 12)
            End If
            Dim version As String = ArenaJson.Substring(ArenaJson.IndexOf("version") + 9,
                                                        ArenaJson.IndexOf("}", ArenaJson.IndexOf("version") + 9) - ArenaJson.IndexOf("version") - 9)
            'MessageBox.Show(temparena)
            If GameType = 0 Then '2K15
                DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, Rope, Apron, Turnbuckle, Barricade, Fence,
                                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, version)
            ElseIf GameType = 1 Then '2K16
                DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
                                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
                                       Wall_R, Header, Floor, MiscObject, version)
            ElseIf GameType = 2 Then '2K17 
                DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
                                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
                                       Wall_R, Header, Floor, MiscObject, version)
            ElseIf GameType = 3 Then '2K18
                DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, LEDCorner, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
                                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
                                       Wall_R, Header, Floor, MiscObject, LightingType, CornerPost_CM, Rope_CM, Apron_CM, Turnbuckle_CM, RingMat_CM, version)
            ElseIf GameType = 4 Then '2K19
                DataGridMiscView.Rows.Add(Stadium, Advert, CornerPost, LEDCorner, Rope, Apron, LEDApron, Turnbuckle, Barricade, Fence,
                                       CeilingLight, SpotLight, Stairs, CommentarySeat, RingMat, FloorMat, Crowd, IBL, Titantron, Minitron, Wall_L,
                                       Wall_R, Header, Floor, MiscObject, LightingType, CornerPost_CM, Rope_CM, Apron_CM, Turnbuckle_CM, RingMat_CM, version)
            End If
            DataGridMiscView.Rows.Item(i).HeaderCell.Value = ArenaNum
        Next
    End Sub
    Sub GetMiscColumns(MenuIndex As Integer)
        Dim ArenaParts As DataGridViewColumnCollection = DataGridMiscView.Columns
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Stadium",
                       .Name = "Stadium"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Advert",
                       .Name = "Advert"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CornerPost",
                       .Name = "CornerPost"})
        If MenuIndex > 2 Then '2K18 and Beyond
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "LEDCorner",
                       .Name = "LEDCorner"})
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Rope",
                       .Name = "Rope"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Apron",
                       .Name = "Apron"})
        If MenuIndex > 0 Then '2K16 and Beyond
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "LEDApron",
                       .Name = "LEDApron"})
        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Turnbuckle",
                       .Name = "Turnbuckle"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Barricade",
                       .Name = "Barricade"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Fence",
                       .Name = "Fence"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CeilingL",
                       .Name = "CeilingL"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "SpotL",
                       .Name = "SpotL"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Stairs",
                       .Name = "Stairs"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "ComSeat",
                       .Name = "ComSeat"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "RingMat",
                       .Name = "RingMat"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "FloorMat",
                       .Name = "FloorMat"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Crowd",
                       .Name = "Crowd"})
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "IBL",
                       .Name = "IBL"})
        If MenuIndex > 0 Then '2K16 and Beyond
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Titantron",
                                   .Name = "Titantron"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Minitron",
                                   .Name = "Minitron"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Wall_R",
                                   .Name = "Wall_R"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Header",
                                   .Name = "Header"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "Floor",
                                   .Name = "Floor"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "MiscO",
                                   .Name = "MiscO"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "MiscO",
                                   .Name = "MiscO"})
        End If
        If MenuIndex > 1 Then '2K17 and Beyond
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                                   .HeaderText = "LightT",
                                   .Name = "LightT"})
        End If
        If MenuIndex > 2 Then '2K18 and Beyond
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "CornerCM",
                       .Name = "CornerCM"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "RopeCM",
                       .Name = "RopeCM"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "TurnbCM",
                       .Name = "TurnbCM"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "ApronCM",
                       .Name = "ApronCM"})
            ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "RingMatCM",
                       .Name = "RingMatCM"})

        End If
        ArenaParts.Add(New DataGridViewTextBoxColumn() With {
                       .HeaderText = "Version",
                       .Name = "Version"})
    End Sub

    Private Sub MiscViewType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MiscViewType.SelectedIndexChanged
        My.Settings.MiscModeIndex = MiscViewType.SelectedIndex
        If TreeView1.SelectedNode IsNot Nothing Then
            FillMiscView(TreeView1.SelectedNode)
        End If
    End Sub
#End Region
#Region "Show View Controls"
    Sub FillShowView(SelectedData As TreeNode)
        DataGridShowView.Rows.Clear()
        Dim GameType As Integer = ShowViewType.SelectedIndex
        'GetMiscColumns(GameType)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim ShowBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            ShowBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, ShowBytes, 0, NodeTag.length)
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            ShowBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, ShowBytes, 0, NodeTag.length)
        End If
        Dim FileLength As Integer = ShowBytes.Length
        Dim index As Integer = 0
        Dim current_poition As Long = &HC
        While current_poition < FileLength
            DataGridShowView.Rows.Add()
            DataGridShowView(0, index).Value = Hex(BitConverter.ToInt32(ShowBytes, current_poition)) ' Dim NameRef As String = 
            DataGridShowView(1, index).Value = Hex(ShowBytes(current_poition + 4)) 'Dim S1 As String =
            DataGridShowView(2, index).Value = Hex(ShowBytes(current_poition + 5)) 'Dim S2 As String =
            DataGridShowView(3, index).Value = Hex(ShowBytes(current_poition + 6)) 'Dim S3 As String = 
            DataGridShowView(4, index).Value = Hex(ShowBytes(current_poition + 7)) ' Dim S4 As String = 
            DataGridShowView(5, index).Value = Hex(ShowBytes(current_poition + 8)) 'Dim A1 As String =
            DataGridShowView(6, index).Value = Hex(ShowBytes(current_poition + 10)) 'Dim A2 As String = 
            DataGridShowView(7, index).Value = Hex(BitConverter.ToInt16(ShowBytes, current_poition + 12)) 'Dim B As String = 
            DataGridShowView(8, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 24) 'Dim C1 As Boolean = 
            DataGridShowView(9, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 25) ' Dim C2 As Boolean = 
            DataGridShowView(10, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 26) 'Dim C3 As Boolean = 
            DataGridShowView(11, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 27) ' Dim C4 As Boolean = 
            DataGridShowView(12, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 30) 'Dim C5 As Boolean = 
            DataGridShowView(13, index).Value = Hex(ShowBytes(current_poition + 31)) 'Dim Stage As String = 
            DataGridShowView(14, index).Value = Hex(ShowBytes(current_poition + 34)) 'Dim D1 As String = 
            DataGridShowView(15, index).Value = Hex(ShowBytes(current_poition + 35)) 'Dim D2 As String = 
            DataGridShowView(16, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 36) 'Dim Crowd As Boolean = 
            DataGridShowView(17, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 37) ' Dim E1 As Boolean = 
            DataGridShowView(18, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 38) 'Dim E2 As Boolean = 
            DataGridShowView(19, index).Value = BitConverter.ToBoolean(ShowBytes, current_poition + 39) ' Dim E3 As Boolean = 
            DataGridShowView(20, index).Value = Hex(ShowBytes(current_poition + 40)) 'Dim Ref As String
            'Dim Filter As String
            DataGridShowView(21, index).Value = Hex(ShowBytes(current_poition + 41)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 42)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 43)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 44)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 45)).PadLeft(2, "0") &
                                             Hex(ShowBytes(current_poition + 46)).PadLeft(2, "0")
            'Dim F1 As String
            DataGridShowView(22, index).Value = Hex(BitConverter.ToInt32(ShowBytes, current_poition + 52))
            'DataGridView1(22, index).Value = Hex(show_array(current_poition + 52)).PadLeft(2, "0") &
            'Hex(show_array(current_poition + 53)).PadLeft(2, "0") &
            'Hex(show_array(current_poition + 54)).PadLeft(2, "0") &
            'Hex(show_array(current_poition + 55)).PadLeft(2, "0")
            DataGridShowView(23, index).Value = Hex(ShowBytes(current_poition + 56)) 'Dim F2 As String
            DataGridShowView(24, index).Value = Hex(ShowBytes(current_poition + 60)) 'Dim G1 As String
            DataGridShowView(25, index).Value = Hex(ShowBytes(current_poition + 62)) 'Dim G2 As String
            DataGridShowView(26, index).Value = Hex(ShowBytes(current_poition + 64)) 'Dim H1 As String
            DataGridShowView(27, index).Value = Hex(ShowBytes(current_poition + 65)) 'Dim H2 As String
            DataGridShowView(28, index).Value = Hex(ShowBytes(current_poition + 66)) 'Dim H3 As String
            DataGridShowView(29, index).Value = Hex(ShowBytes(current_poition + 67)) 'Dim H4 As String
            DataGridShowView(30, index).Value = Hex(ShowBytes(current_poition + 69)) 'Dim Bar As String
            'Dim Unknown As String
            Dim temparray As Byte() = New Byte(33) {}
            Buffer.BlockCopy(ShowBytes, current_poition + 70, temparray, 0, 34)
            DataGridShowView(31, index).Value = (BitConverter.ToString(temparray).Replace("-", ""))
            DataGridShowView(32, index).Value = Hex(ShowBytes(current_poition + 107)) 'Dim I1 As String
            DataGridShowView(33, index).Value = Hex(ShowBytes(current_poition + 108)) 'Dim I2 As String
            DataGridShowView(34, index).Value = Hex(ShowBytes(current_poition + 110))  'Dim I3 As String
            DataGridShowView(35, index).Value = Hex(ShowBytes(current_poition + 113)) 'Dim live As String
            DataGridShowView(36, index).Value = Hex(ShowBytes(current_poition + 116)) 'Dim J As String
            'DataGridView1.Rows.Add(NameRef, S1, S2, S3, S4, A1, A2, B, C1, C2, C3, C4, C5, Stage, D1, D2, Crowd, E1, E2, E3)
            DataGridShowView.Rows(index).HeaderCell.Value = index.ToString
            index += 1
            current_poition += &H78
        End While
    End Sub

#End Region
#Region "NIJB View Controls"
    Sub FillNIBJView(SelectedData As TreeNode)
        DataGridNIBJView.Rows.Clear()
        DataGridNIBJView.Columns.Clear()
        Dim GameType As Integer = NIBJViewType.SelectedIndex
        'GetNIJBColumns(GameType)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim NIJBBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            NIJBBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, NIJBBytes, 0, NodeTag.length)
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            NIJBBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, NIJBBytes, 0, NodeTag.length)
        End If
        Dim LightCount As Integer = BitConverter.ToInt32(NIJBBytes, &H8)
        Dim ShowCount As Integer = BitConverter.ToInt32(NIJBBytes, &HC)
        Dim Folder As String = Encoding.Default.GetChars(NIJBBytes, &H10, &H10)
        Dim Properties As String = Encoding.Default.GetChars(NIJBBytes, &H20, &H10)
        FileAttributesToolStripMenuItem.Text = Folder & " > " & Properties
        For i As Integer = 0 To LightCount - 1
            DataGridNIBJView.Columns.Add("Column" & i, Encoding.ASCII.GetString(NIJBBytes, &H30 + i * &H20, &H10))
        Next
        For i As Integer = 0 To ShowCount - 1
            DataGridNIBJView.Rows.Add()
            For j As Integer = 0 To LightCount - 1
                DataGridNIBJView(j, i).Value = Strings.Right(Hex(BitConverter.ToInt32(NIJBBytes, &H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadRight(8, "0"), 8)
                DataGridNIBJView.Rows(i).Cells(j).Style.BackColor = ColorTranslator.FromHtml("#" & (DataGridNIBJView(j, i).Value.ToString.Substring(2, 6)))
                Dim FontColor As String = Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 2)).PadLeft(2, "0") &
                    Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount + 1)).PadLeft(2, "0") &
                    Hex(&HFF - NIJBBytes(&H30 + &H20 * LightCount + i * 4 + j * 4 * ShowCount)).PadLeft(2, "0")
                DataGridNIBJView.Rows(i).Cells(j).Style.ForeColor = ColorTranslator.FromHtml("#" & FontColor)
            Next
            DataGridNIBJView.Rows(i).HeaderCell.Value = i.ToString
        Next
    End Sub
#End Region
#Region "Picture View Controls"
    Sub LoadPicture(SelectedData As TreeNode)
        Dim NodeTag As NodeProperties = New NodeProperties
        NodeTag.FileType = CType(SelectedData.Tag, NodeProperties).FileType
        NodeTag.Index = CType(SelectedData.Tag, NodeProperties).Index
        NodeTag.length = CType(SelectedData.Tag, NodeProperties).length
        NodeTag.StoredData = CType(SelectedData.Tag, NodeProperties).StoredData
        Dim PictureBytes As Byte()
        If NodeTag.StoredData.Length > 0 Then
            Dim FileBytes As Byte() = NodeTag.StoredData
            PictureBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, PictureBytes, 0, NodeTag.length)
        Else
            Dim FileBytes As Byte() = File.ReadAllBytes(SelectedData.ToolTipText)
            PictureBytes = New Byte(NodeTag.length - 1) {}
            Array.Copy(FileBytes, NodeTag.Index, PictureBytes, 0, NodeTag.length)
        End If
        Dim ImageStream As MemoryStream = New MemoryStream(PictureBytes)
        Dim tempconfig As PfimConfig = New PfimConfig
        Dim TempName As String = Path.GetTempFileName
        TempName += ".dds"
        MessageBox.Show(TempName)
        File.WriteAllBytes(TempName, PictureBytes)
        'Dim testclass As DirectXTexNet.TexHelperImpl = New DirectXTexNet.TexHelperImpl

        'testclass.LoadFromDDSFile(TempName, Nothing)
        Dim DDSFile = Pfim.Pfim.FromFile(TempName)
        Dim TempFormat As PixelFormat
        Select Case (DDSFile.Format)
            Case Pfim.ImageFormat.Rgb24
                TempFormat = PixelFormat.Format24bppRgb
                Exit Select
            Case Pfim.ImageFormat.Rgba32
                TempFormat = PixelFormat.Format32bppArgb
                Exit Select
            Case Pfim.ImageFormat.R5g5b5
                TempFormat = PixelFormat.Format16bppRgb555
                Exit Select
            Case Pfim.ImageFormat.R5g6b5
                TempFormat = PixelFormat.Format16bppRgb565
                Exit Select
            Case Pfim.ImageFormat.R5g5b5a1
                TempFormat = PixelFormat.Format16bppArgb1555
                Exit Select
            Case Pfim.ImageFormat.Rgb8
                TempFormat = PixelFormat.Format8bppIndexed
                Exit Select
            Case Else
                Dim msg As String = $"{DDSFile.Format} is not recognized for Bitmap on Windows Forms. " +
                               "You'd need to write a conversion function to convert the data to known format"
                Dim caption As String = "Unrecognized format"
                MessageBox.Show(msg, caption, MessageBoxButtons.OK)
        End Select

        Dim ms As New MemoryStream(DDSFile.Data)
        PictureBox1.Image = Image.FromStream(ms)
        File.Delete(TempName)
        'Try
        'Dim structSize As Integer = Marshal.SizeOf(TypeOf (DDSFile.Data)
        'Dim bitmappointer As IntPtr = Marshal.AllocHGlobal(DDSFile.Data.Length)
        'For i As Integer = 0 To DDSFile.Data.Length - 1
        'Marshal.StructureToPtr(DDSFile.Data(i), bitmappointer + i, True)
        'Next
        'Dim DrawnBitmat As Bitmap = New Bitmap(DDSFile.Width, DDSFile.Height, DDSFile.Stride, TempFormat, bitmappointer)
        'PictureBox1.Image = DrawnBitmat
        'Marshal.FreeHGlobal(bitmappointer)
        'Catch ex As Exception
        'MessageBox.Show(ex.Message)
        'End Try
    End Sub

#End Region
End Class
