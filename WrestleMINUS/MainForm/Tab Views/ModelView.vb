Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    Dim CompleteYobjBytes As Byte()
    Dim SelectedObjHeader As ObjectHeaderInformation

    Sub FillObjectViews(SelectedData As TreeNode)
        CompleteYobjBytes = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        FillObjectHeaderView()
        FillObjectBoneView()
        FillObjectTextureView()
        FillObjectShaderView()
        FillObjectEmoteComboBox()
        FillSubObjectViews(DataGridObjectView.Rows(0).Tag)
    End Sub

    'CompleteYobjBytes Header
    '0x0 JBOY / YOBJ
    '0x4 Offset to Pof0 (Yobj Size)
    '0x8 &H10
    '0xC Length of Yobj Size
    '0x10 Note 07 or 03?
    '0x14 Filler?
    '0x18 SubItemCount
    '0x1C HeaderStartOffset
    '0x20 BoneCount
    '0x24 TextureCount
    '0x28 BonesStartOffset
    '0x2C TextureStartOffset
    '0x30 ShaderStartOffset
    '0x34 ShaderCount
    '0x38 Filler? 00
    '0x3C Filler? 00
    '0x40 Filler? 00
    '0x44 Filler? 00
    '0x48 EmoteNameCount
    '0x4C EmoteNameStartOffset
    '0x50 BoneStructureCount
    '0x54 BoneStructureStartOffset

    'https://en.wikipedia.org/wiki/Wavefront_.obj_file

    Public Class ObjectHeaderInformation
        Public IndexCount As UInt32 = 0
        Public Rendered As Boolean = False
        Public FillerBytes As Byte() = New Byte() {}
        Public FillerString As String = ""
        Public UnknownA As UInt32 = 0

        Public VertexCount As UInt32 = 0
        Public VertexOffset As UInt32 = 0

        Public VertexHeadCount As UInt32 = 0
        Public VertexHeadList As List(Of List(Of ObjectVertex))

        Public WeightCount As UInt32 = 0
        Public WeightOffset As UInt32 = 0
        Public WeightCollection As List(Of List(Of ObjectVertexWeight))

        Public TextureCordOffset As UInt32 = 0
        Public TextureCordCount As UInt32 = 0
        Public TextureCordList As List(Of ObjectTextureCoords)

        Public NormalsOffset As UInt32 = 0
        Public NormalList As List(Of ObjectVertexNormals)

        Public UnknownB As UInt32 = 0
        Public ShaderBytes As Byte() = New Byte() {}
        Public ShaderString As String = ""
        Public UnknownC As UInt32 = 0
        Public MaterialIndex As UInt32 = 0

        Public ParameterCount As UInt32 = 0
        Public ParameterOffset As UInt32 = 0
        Public ParameterList As List(Of ObjectVertexParameter)

        Public FacesOffset As UInt32 = 0
        Public TriStripList As List(Of List(Of UInt16))
        Public FaceList As List(Of ObjectFaceReference)

        Public UnknownD As UInt32 = 0
        Public UnknownE As UInt32 = 0
        Public UnknownF As UInt32 = 0
        Public UnknownG As UInt32 = 0
        Public UnknownH As UInt32 = 0
    End Class

    Public Class ObjectVertex
        Public IndexCount As UInt32 = 0
        Public X As Single = 0
        Public Y As Single = 0
        Public Z As Single = 0
        Public RX As Single = 0
        Public RY As Single = 0
        Public RZ As Single = 0
        Public Weight As Single = 0
    End Class

    Public Class ObjectVertexWeight
        Public Weight As Single = 0
        Public Num As UInt32 = 0
    End Class

    Public Class ObjectTextureCoords
        Public VertNumber As UInt32 = 0
        Public U As Single = 0
        Public V As Single = 0
        Public Weight As Single = 0
    End Class

    Public Class ObjectVertexNormals
        Public VertNumber As UInt32 = 0
        Public X As Single = 0
        Public Y As Single = 0
        Public Z As Single = 0
    End Class

    Public Class ObjectVertexParameter
        Public U As Single = 0
        Public V As Single = 0
        Public Weight As Single = 0
    End Class

    Public Class ObjectFaceReference
        Public Verticies As UInt16() = {1, 2, 3}
    End Class

#Region "Header Read"

    Dim DefaultFill As String = "00 00 00 00 FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF FF"

    Function ParseBytesToObjectHeaderInformation(TestedByteArray As Byte()) As ObjectHeaderInformation
        Dim TempFillerBytes As Byte() = New Byte(&H54 - 1) {}
        Array.Copy(TestedByteArray, 8, TempFillerBytes, 0, &H54)
        Dim TempShaderBytes As Byte() = New Byte(&H10 - 1) {}
        Array.Copy(TestedByteArray, &H7C, TempShaderBytes, 0, &H10)
        Dim ReturnedObjectHeaderInfo As ObjectHeaderInformation = New ObjectHeaderInformation With {
           .VertexCount = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, 0, 4), 0),
           .Rendered = BitConverter.ToBoolean(TestedByteArray, 4 + 3),
           .FillerBytes = TempFillerBytes,
           .FillerString = BitConverter.ToString(TempFillerBytes, 0).Replace("-", " "),
           .WeightCount = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H5C, 4), 0),
           .UnknownA = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H60, 4), 0),
           .VertexHeadCount = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H64, 4), 0),
           .VertexOffset = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H68, 4), 0),
           .WeightOffset = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H6C, 4), 0),
           .TextureCordOffset = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H70, 4), 0),
           .NormalsOffset = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H74, 4), 0),
           .UnknownB = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H78, 4), 0),
           .ShaderBytes = TempShaderBytes,
           .ShaderString = Encoding.Default.GetString(TempShaderBytes).TrimEnd(Chr(0)),
           .UnknownC = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H8C, 4), 0),
           .MaterialIndex = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H90, 4), 0),
           .ParameterCount = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H94, 4), 0),
           .ParameterOffset = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H98, 4), 0),
           .FacesOffset = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H9C, 4), 0),
           .TextureCordCount = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &HA0, 4), 0),
           .UnknownD = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &HA4, 4), 0),
           .UnknownE = BitConverter.ToUInt32(TestedByteArray, &HA8),
           .UnknownF = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &HAC, 4), 0),
           .UnknownG = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &HB0, 4), 0),
           .UnknownH = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &HB4, 4), 0)
           }

        ReturnedObjectHeaderInfo.VertexHeadList = New List(Of List(Of ObjectVertex))
        For I As Integer = 0 To ReturnedObjectHeaderInfo.VertexHeadCount - 1
            Dim FirstObjectVertexIndex As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, ReturnedObjectHeaderInfo.VertexOffset + 8 + I * 4, 4), 0) + 8
            ReturnedObjectHeaderInfo.VertexHeadList.Add(ParseBytesToVertexInformation(CompleteYobjBytes, FirstObjectVertexIndex, ReturnedObjectHeaderInfo.VertexCount))
        Next
        ReturnedObjectHeaderInfo.WeightCollection = ParseBytesToWeightsList(CompleteYobjBytes, ReturnedObjectHeaderInfo.WeightOffset + 8, ReturnedObjectHeaderInfo.VertexCount, ReturnedObjectHeaderInfo.WeightCount)

        ReturnedObjectHeaderInfo.NormalList = ParseBytesToNormalsList(CompleteYobjBytes, ReturnedObjectHeaderInfo.NormalsOffset + 8, ReturnedObjectHeaderInfo.VertexCount)

        If Not ReturnedObjectHeaderInfo.VertexCount = ReturnedObjectHeaderInfo.TextureCordCount Then
            MessageBox.Show("Object Vertex and Texture Count Mismatch")
        End If
        ReturnedObjectHeaderInfo.TextureCordList = ParseBytesToTextureCordList(CompleteYobjBytes, ReturnedObjectHeaderInfo.TextureCordOffset + 8, ReturnedObjectHeaderInfo.TextureCordCount)

        ReturnedObjectHeaderInfo.ParameterList = New List(Of ObjectVertexParameter)
        ReturnedObjectHeaderInfo.TriStripList = ParseBytesToTriStripList(CompleteYobjBytes, ReturnedObjectHeaderInfo.FacesOffset + 8)
        ReturnedObjectHeaderInfo.FaceList = ParseTriStripListToFaceList(ReturnedObjectHeaderInfo.TriStripList)
        Return ReturnedObjectHeaderInfo
    End Function

    Function ParseBytesToVertexInformation(ByVal ByteContainer As Byte(), ByVal VertexStart As UInt32, ByVal VertexCount As UInt32) As List(Of ObjectVertex)
        Dim ReturnedVertList As List(Of ObjectVertex) = New List(Of ObjectVertex)
        For i As UInt32 = 0 To VertexCount - 1
            Dim ReturnedVertexInfo As ObjectVertex = New ObjectVertex With {
                .IndexCount = i + 1,
                .X = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + 0, 4), 0),
                .Z = -BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + 4, 4), 0),
                .Y = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + 8, 4), 0),
                .RX = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + &HC, 4), 0),
                .RZ = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + &H10, 4), 0),
                .RY = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + &H14, 4), 0),
                .Weight = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, VertexStart + i * &H1C + &H18, 4), 0)}
            ReturnedVertList.Add(ReturnedVertexInfo)
        Next
        Return ReturnedVertList
    End Function

    Function ParseBytesToWeightsList(ByVal ByteContainer As Byte(), ByVal WeightsOffset As UInt32, ByVal VertexCount As UInt32, WeightsCount As UInt32) As List(Of List(Of ObjectVertexWeight))
        Dim ReturnedCompleteList As List(Of List(Of ObjectVertexWeight)) = New List(Of List(Of ObjectVertexWeight))
        For i As Integer = 0 To WeightsCount - 1
            Dim ReturnedVertList As List(Of ObjectVertexWeight) = New List(Of ObjectVertexWeight)
            ReturnedCompleteList.Add(ReturnedVertList)
        Next
        For iCurrentVert As UInt32 = 0 To VertexCount - 1
            For jCurrentWeight As Integer = 0 To WeightsCount - 1
                Dim ReturnedVertexWeights As ObjectVertexWeight = New ObjectVertexWeight With {
                    .Num = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ByteContainer, WeightsOffset + (iCurrentVert * 8 * WeightsCount) + (jCurrentWeight * 8), 4), 0),
                .Weight = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, WeightsOffset + (iCurrentVert * 8 * WeightsCount) + (jCurrentWeight * 8) + 4, 4), 0)}
                ReturnedCompleteList(jCurrentWeight).Add(ReturnedVertexWeights)
            Next
        Next
        Return ReturnedCompleteList
    End Function

    Function ParseBytesToNormalsList(ByVal ByteContainer As Byte(), ByVal NormalsOffset As UInt32, ByVal VertexCount As UInt32) As List(Of ObjectVertexNormals)
        Dim ReturnedNormalList As List(Of ObjectVertexNormals) = New List(Of ObjectVertexNormals)
        For i As UInt32 = 0 To VertexCount - 1
            Dim TempNormalInfo As ObjectVertexNormals = New ObjectVertexNormals With {
                .VertNumber = i + 1,
                .X = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, NormalsOffset + i * &HC + 0, 4), 0),
                .Z = -BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, NormalsOffset + i * &HC + 4, 4), 0),
                .Y = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, NormalsOffset + i * &HC + 8, 4), 0)}
            ReturnedNormalList.Add(TempNormalInfo)
        Next
        Return ReturnedNormalList
    End Function

    Function ParseBytesToTextureCordList(ByVal ByteContainer As Byte(), ByVal TextureCordOffset As UInt32, ByVal TextureCordCount As UInt32) As List(Of ObjectTextureCoords)
        Dim ReturnedTextureCordList As List(Of ObjectTextureCoords) = New List(Of ObjectTextureCoords)
        For i As UInt32 = 0 To TextureCordCount - 1
            Dim TempTextureCordInfo As ObjectTextureCoords = New ObjectTextureCoords With {
                .VertNumber = i + 1,
                .U = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, TextureCordOffset + i * 8 + 0, 4), 0),
                .V = -BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(ByteContainer, TextureCordOffset + i * 8 + 4, 4), 0),
                .Weight = 0}
            ReturnedTextureCordList.Add(TempTextureCordInfo)
        Next
        Return ReturnedTextureCordList
    End Function

    Function ParseBytesToTriStripList(ByVal ByteContainer As Byte(), ByVal FaceListOffset As UInt32) As List(Of List(Of UInt16))
        Dim FaceCount As Int32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 4, 4), 0)
        Dim ReturnedFaceList As List(Of List(Of UInt16)) = New List(Of List(Of UInt16))
        Dim FaceIndex As UInt16 = 0
        Do While FaceIndex < FaceCount - 3
            Dim TempObjectFaceRef As List(Of UInt16) = New List(Of UInt16)
            Dim BufferFace As Boolean = False
            Do While FaceIndex < FaceCount - 4
                Dim TestedVertex As UInt16 = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 12 + FaceIndex * 2 + 0, 2), 0) + 1
                Select Case TempObjectFaceRef.Count
                    Case 0
                        If BufferFace Then
                            TempObjectFaceRef.Add(0)
                            BufferFace = False
                        End If
                        TempObjectFaceRef.Add(TestedVertex)
                        FaceIndex += 1
                    Case 1
                        If TempObjectFaceRef.Contains(TestedVertex) Then
                            'ditch this Strip
                            Exit Do
                        Else
                            TempObjectFaceRef.Add(TestedVertex)
                            FaceIndex += 1
                        End If
                    Case 2
                        If TempObjectFaceRef.Contains(TestedVertex) Then
                            'ditch this Strip
                            Exit Do
                        Else
                            TempObjectFaceRef.Add(TestedVertex)
                            FaceIndex += 1
                        End If
                    Case Else
                        If TempObjectFaceRef(TempObjectFaceRef.Count - 1) = TestedVertex Then
                            'Save this strip as it has at least 3
                            'Here We have 2 Nums in a row
                            ReturnedFaceList.Add(TempObjectFaceRef)
                            Exit Do
                        ElseIf TempObjectFaceRef(TempObjectFaceRef.Count - 2) = TestedVertex Then
                            'Here we have ha gap so we need to start the next strip one back
                            FaceIndex -= 1
                            BufferFace = True
                            'Save this strip as it has at least 3
                            ReturnedFaceList.Add(TempObjectFaceRef)
                            Exit Do
                        Else
                            TempObjectFaceRef.Add(TestedVertex)
                            FaceIndex += 1
                        End If
                End Select
            Loop
            If FaceIndex = FaceCount - 4 Then
                Dim TestedVertex As UInt16 = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 12 + FaceIndex * 2 + 0, 2), 0) + 1
                TempObjectFaceRef.Add(TestedVertex)
                ReturnedFaceList.Add(TempObjectFaceRef)
                FaceIndex += 1
            End If
        Loop
        Return ReturnedFaceList
    End Function

    Function ParseBytesToFaceList(ByVal ByteContainer As Byte(), ByVal FaceListOffset As UInt32) As List(Of ObjectFaceReference)
        Dim FaceCount As Int32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 4, 4), 0)
        Dim ReturnedFaceList As List(Of ObjectFaceReference) = New List(Of ObjectFaceReference)
        For i As UInt32 = 0 To FaceCount - 3
            'note that we add 0 area faces.  I want to test if objs accept them as well
            Dim TempObjectFaceRef As ObjectFaceReference = New ObjectFaceReference With {
                .Verticies = New UInt16(2) {
                 BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 12 + i * 2 + 0, 2), 0) + 1,
                BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 12 + i * 2 + 2, 2), 0) + 1,
                BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(ByteContainer, FaceListOffset + 12 + i * 2 + 4, 2), 0) + 1}}
            If TempObjectFaceRef.Verticies(0) = TempObjectFaceRef.Verticies(1) OrElse
               TempObjectFaceRef.Verticies(0) = TempObjectFaceRef.Verticies(2) OrElse
               TempObjectFaceRef.Verticies(1) = TempObjectFaceRef.Verticies(2) Then
                Continue For
            Else
                ReturnedFaceList.Add(TempObjectFaceRef)
            End If
        Next
        Return ReturnedFaceList
    End Function

    Function ParseTriStripListToFaceList(ByVal TriStripList As List(Of List(Of UInt16))) As List(Of ObjectFaceReference)
        Dim ReturnedList As List(Of ObjectFaceReference) = New List(Of ObjectFaceReference)
        For JTempTriStrip As Integer = 0 To TriStripList.Count - 1
            Dim ClockWise As Boolean = True
            If TriStripList(JTempTriStrip)(0) = 0 Then
                TriStripList(JTempTriStrip).RemoveAt(0)
                ClockWise = True
            Else
                If TriStripList(JTempTriStrip).Count Mod 2 = 1 Then
                    ClockWise = False
                End If
            End If
            For i As Integer = 0 To TriStripList(JTempTriStrip).Count - 3
                If Not TriStripList(JTempTriStrip)(i) = 0 Then
                    If Not ClockWise Then
                        If i Mod 2 = 1 Then
                            ReturnedList.Add(New ObjectFaceReference With {.Verticies = New UInt16(2) {TriStripList(JTempTriStrip)(i), TriStripList(JTempTriStrip)(i + 1), TriStripList(JTempTriStrip)(i + 2)}})
                        Else
                            ReturnedList.Add(New ObjectFaceReference With {.Verticies = New UInt16(2) {TriStripList(JTempTriStrip)(i), TriStripList(JTempTriStrip)(i + 2), TriStripList(JTempTriStrip)(i + 1)}})
                        End If
                    Else
                        If i Mod 2 = 1 Then
                            ReturnedList.Add(New ObjectFaceReference With {.Verticies = New UInt16(2) {TriStripList(JTempTriStrip)(i), TriStripList(JTempTriStrip)(i + 2), TriStripList(JTempTriStrip)(i + 1)}})
                        Else
                            ReturnedList.Add(New ObjectFaceReference With {.Verticies = New UInt16(2) {TriStripList(JTempTriStrip)(i), TriStripList(JTempTriStrip)(i + 1), TriStripList(JTempTriStrip)(i + 2)}})
                        End If
                    End If
                End If
            Next
        Next
        Return ReturnedList
    End Function

#End Region

#Region "Export to Wave Obj"

    Sub WriteYobjtoWafeformat(SelectedObject As List(Of ObjectHeaderInformation), SaveFileToPath As String)
        If File_FolderHandlers.CheckFileWriteable(SaveFileToPath, False) Then
            Dim ReturnedFileText As List(Of String) = New List(Of String)
            For i As Integer = 0 To SelectedObject.Count - 1
                'Write Geo Verts
                For J As Integer = 0 To SelectedObject(i).VertexHeadList(0).Count - 1
                    'If Single.IsNaN(SelectedObject(i).VertexHeadList(0)(J).Weight) Then
                    ReturnedFileText.Add("v " & SelectedObject(i).VertexHeadList(0)(J).X & " " & SelectedObject(i).VertexHeadList(0)(J).Y & " " & SelectedObject(i).VertexHeadList(0)(J).Z)
                    'Else
                    'ReturnedFileText.Add("v " & SelectedObject(i).VertexHeadList(0)(J).X & " " & SelectedObject(i).VertexHeadList(0)(J).Y & " " & SelectedObject(i).VertexHeadList(0)(J).Z & " " & SelectedObject(i).VertexHeadList(0)(J).Weight)
                    'End If
                Next
                'Write Texture Cords
                For J As Integer = 0 To SelectedObject(i).TextureCordList.Count - 1
                    'If Single.IsNaN(SelectedObject(i).TextureCordList(J).Weight) Then
                    ReturnedFileText.Add("vt " & SelectedObject(i).TextureCordList(J).U & " " & SelectedObject(i).TextureCordList(J).V & " 0")
                    'Else
                    '    ReturnedFileText.Add("vt " & SelectedObject(i).TextureCordList(J).U & " " & SelectedObject(i).TextureCordList(J).V & " " & SelectedObject(i).TextureCordList(J).Weight)
                    'End If
                Next
                'Write Vertex Normals
                'For J As Integer = 0 To SelectedObject(i).NormalList.Count - 1
                '    ReturnedFileText.Add("vp " & SelectedObject(i).NormalList(J).X & " " & SelectedObject(i).NormalList(J).Y & " " & SelectedObject(i).NormalList(J).Z)
                'Next
                ReturnedFileText.Add("g Object" & SelectedObject(i).IndexCount)
                ReturnedFileText.Add("s 1")
                'Write Faces
                For J As Integer = 0 To SelectedObject(i).FaceList.Count - 1
                    ReturnedFileText.Add("f " & SelectedObject(i).FaceList(J).Verticies(0) & "/" & SelectedObject(i).FaceList(J).Verticies(0) & " " &
                                         SelectedObject(i).FaceList(J).Verticies(1) & "/" & SelectedObject(i).FaceList(J).Verticies(1) & " " &
                                         SelectedObject(i).FaceList(J).Verticies(2) & "/" & SelectedObject(i).FaceList(J).Verticies(2))
                Next
                'For J As Integer = 0 To SelectedObject(i).FaceList.Count - 1
                '    ReturnedFileText.Add("f " & SelectedObject(i).FaceList(J).Verticies(0) & "/" & SelectedObject(i).FaceList(J).Verticies(0) & "/" & SelectedObject(i).FaceList(J).Verticies(0) & " " &
                '                         SelectedObject(i).FaceList(J).Verticies(1) & "/" & SelectedObject(i).FaceList(J).Verticies(1) & "/" & SelectedObject(i).FaceList(J).Verticies(1) & " " &
                '                         SelectedObject(i).FaceList(J).Verticies(2) & "/" & SelectedObject(i).FaceList(J).Verticies(2) & "/" & SelectedObject(i).FaceList(J).Verticies(2))
                'Next
            Next
            File.WriteAllLines(SaveFileToPath, ReturnedFileText)
            MessageBox.Show("File Written")
            'Return ReturnedFileText
        Else
            'Return Nothing
        End If
    End Sub

#End Region

#Region "Header View"

    Sub FillObjectHeaderView()
        'We need to show what type of position file it is
        Dim SubItemCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H18, 4), 0)
        Dim HeaderStartOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H1C, 4), 0) + 8
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridObjectView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SubItemCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SubItemCount - 1
            Dim TempObjectBytes As Byte() = New Byte(&HB8 - 1) {}
            Array.Copy(CompleteYobjBytes, HeaderStartOffset + i * &HB8, TempObjectBytes, 0, &HB8)
            Dim TempObjectHeaderInformation As ObjectHeaderInformation = ParseBytesToObjectHeaderInformation(TempObjectBytes)
            TempObjectHeaderInformation.IndexCount = i
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectVertexCount)).Value = TempObjectHeaderInformation.VertexCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectRendered)).Value = TempObjectHeaderInformation.Rendered
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectHeaderFiller)).Value = TempObjectHeaderInformation.FillerString
            If TempObjectHeaderInformation.FillerString = DefaultFill Then
                TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectHeaderFiller)).Style = ReadOnlyCellStyle
            End If
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectWeightNumber)).Value = TempObjectHeaderInformation.WeightCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownIntA)).Value = TempObjectHeaderInformation.UnknownA
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectVerHeaderCount)).Value = TempObjectHeaderInformation.VertexHeadCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectVerticeOffset)).Value = Hex(TempObjectHeaderInformation.VertexOffset)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectWeightsOffset)).Value = Hex(TempObjectHeaderInformation.WeightOffset)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUVOffset)).Value = Hex(TempObjectHeaderInformation.TextureCordOffset)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectNormalsOffset)).Value = Hex(TempObjectHeaderInformation.NormalsOffset)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectInternalNum)).Value = TempObjectHeaderInformation.UnknownB
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectHeaderShader)).Value = TempObjectHeaderInformation.ShaderString
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjecHeaderUnknownC)).Value = TempObjectHeaderInformation.UnknownC
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectMaterialIndex)).Value = TempObjectHeaderInformation.MaterialIndex
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectParameterCount)).Value = TempObjectHeaderInformation.ParameterCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectParameterOffset)).Value = Hex(TempObjectHeaderInformation.ParameterOffset)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectFaceOffset)).Value = Hex(TempObjectHeaderInformation.FacesOffset)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUVCount)).Value = TempObjectHeaderInformation.TextureCordCount
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownD)).Value = TempObjectHeaderInformation.UnknownD
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownE)).Value = TempObjectHeaderInformation.UnknownE
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownF)).Value = Hex(TempObjectHeaderInformation.UnknownF)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownG)).Value = Hex(TempObjectHeaderInformation.UnknownG)
            TempGridRow.Cells(DataGridObjectView.Columns.IndexOf(ObjectUnknownH)).Value = Hex(TempObjectHeaderInformation.UnknownH)
            TempGridRow.Tag = TempObjectHeaderInformation
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Private Sub DataGridObjectView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridObjectView.CellContentClick
        Dim senderGrid = DirectCast(sender, DataGridView)
        If TypeOf senderGrid.Columns(e.ColumnIndex) Is DataGridViewButtonColumn Then
            Select Case e.ColumnIndex
                Case DataGridObjectView.Columns.IndexOf(ObjectHeaderLoad)
                    'load object row
                    FillSubObjectViews(senderGrid.Rows(e.RowIndex).Tag)

                Case DataGridObjectView.Columns.IndexOf(ObjectExportToObj)
                    Dim SaveObjectFile As SaveFileDialog = New SaveFileDialog With {
                        .FileName = "Object" & senderGrid.Rows(e.RowIndex).Tag.IndexCount,
                        .Filter = "Wavefront Object|*.obj|All files (*.*)|*.*"}
                    If SaveObjectFile.ShowDialog = DialogResult.OK Then
                        WriteYobjtoWafeformat(New List(Of ObjectHeaderInformation) From {senderGrid.Rows(e.RowIndex).Tag}, SaveObjectFile.FileName)
                    End If
            End Select
        End If
    End Sub

#Region "Bone Information"

    Public Class ObjectBoneInformation
        Public IndexCount As UInt32 = 0
        Public StructureOrder As UInt32 = 0
        Public NameBytes As Byte() = New Byte() {}
        Public Name As String = ""
        Public UnknownA As UInt32 = 0
        Public UnknownB As UInt32 = 0
        Public UnknownC As UInt32 = 0
        Public UnknownD As UInt32 = 0
        Public UnknownE As UInt32 = 0
        Public UnknownF As UInt32 = 0
        Public UnknownG As UInt32 = 0
        Public UnknownH As UInt32 = 0
        Public UnknownI As UInt32 = 0
        Public UnknownJ As UInt32 = 0
        Public UnknownK As UInt32 = 0
        Public UnknownL As UInt32 = 0
        Public UnknownM As UInt32 = 0
        Public UnknownN As Single = 0
        Public UnknownO As Single = 0
        Public UnknownP As Single = 0
    End Class

    Sub FillObjectBoneView()
        Dim BoneCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H20, 4), 0)
        Dim BonesStartOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H28, 4), 0) + 8
        'Bones List
        Dim ClonedBoneRow As DataGridViewRow = ClearandGetClone(DataGridObjectBoneView)
        Dim WorkingBoneCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = BoneCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To BoneCount - 1
            Dim TempObjectBoneBytes As Byte() = New Byte(&H50 - 1) {}
            Array.Copy(CompleteYobjBytes, BonesStartOffset + i * &H50, TempObjectBoneBytes, 0, &H50)
            Dim TempObjectBoneInformation As ObjectBoneInformation = ParseBytestoObjectBoneInformation(TempObjectBoneBytes)
            TempObjectBoneInformation.IndexCount = i
            Dim TempBoneGridRow As DataGridViewRow = ClonedBoneRow.Clone()
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneCountCol)).Value = i
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneCountCol)).Style = ReadOnlyCellStyle
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneName)).Value = TempObjectBoneInformation.Name
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownA)).Value = TempObjectBoneInformation.UnknownA
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownB)).Value = TempObjectBoneInformation.UnknownB
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownC)).Value = TempObjectBoneInformation.UnknownC
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownD)).Value = TempObjectBoneInformation.UnknownD
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownE)).Value = TempObjectBoneInformation.UnknownE
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownF)).Value = TempObjectBoneInformation.UnknownF
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownG)).Value = TempObjectBoneInformation.UnknownG
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownH)).Value = TempObjectBoneInformation.UnknownH
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownI)).Value = TempObjectBoneInformation.UnknownI
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownJ)).Value = TempObjectBoneInformation.UnknownJ
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownK)).Value = TempObjectBoneInformation.UnknownK
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownL)).Value = TempObjectBoneInformation.UnknownL
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownM)).Value = TempObjectBoneInformation.UnknownM
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownN)).Value = TempObjectBoneInformation.UnknownN
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownO)).Value = TempObjectBoneInformation.UnknownO
            TempBoneGridRow.Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneUnknownP)).Value = TempObjectBoneInformation.UnknownP
            TempBoneGridRow.Tag = TempObjectBoneInformation
            WorkingBoneCollection.Add(TempBoneGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectBoneView.Rows.AddRange(WorkingBoneCollection.ToArray())
        Dim BoneStructureCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H50, 4), 0)
        Dim BoneStructureStartOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H54, 4), 0) + 8
        'Bone int List
        Dim RevisedBoneStructureCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, BoneStructureStartOffset, 4), 0)
        ProgressBar1.Maximum = RevisedBoneStructureCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To RevisedBoneStructureCount - 1
            Dim TemporaryBoneNumber As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, BoneStructureStartOffset + 8 + i * 4, 4), 0)
            DataGridObjectBoneView.Rows(TemporaryBoneNumber).Cells(DataGridObjectBoneView.Columns.IndexOf(ObjectBoneOrder)).Value = i + 1
            DataGridObjectBoneView.Rows(TemporaryBoneNumber).Tag.StructureOrder = i + 1
        Next
    End Sub

    Function ParseBytestoObjectBoneInformation(TestedByteArray As Byte()) As ObjectBoneInformation
        Dim TempBoneName As Byte() = New Byte(&H10 - 1) {}
        Array.Copy(TestedByteArray, 0, TempBoneName, 0, &H10)
        Dim ReturnedBoneInfo As ObjectBoneInformation = New ObjectBoneInformation With {
            .NameBytes = TempBoneName,
            .Name = Encoding.Default.GetString(TempBoneName).TrimEnd(Chr(0)),
            .UnknownA = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H10, 4), 0),
            .UnknownB = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H14, 4), 0),
            .UnknownC = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H18, 4), 0),
            .UnknownD = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H1C, 4), 0),
            .UnknownE = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H20, 4), 0),
            .UnknownF = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H24, 4), 0),
            .UnknownG = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H28, 4), 0),
            .UnknownH = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H2C, 4), 0),
            .UnknownI = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H30, 4), 0),
            .UnknownJ = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H34, 4), 0),
            .UnknownK = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H38, 4), 0),
            .UnknownL = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H3C, 4), 0),
            .UnknownM = BitConverter.ToUInt32(TestedByteArray, &H40),
            .UnknownN = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H44, 4), 0),
            .UnknownO = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H48, 4), 0),
            .UnknownP = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H4C, 4), 0)}
        Return ReturnedBoneInfo
    End Function

#End Region

#Region "Texture & Shader Info"

    Sub FillObjectTextureView()
        Dim TextureCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H24, 4), 0)
        Dim TextureStartOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H2C, 4), 0) + 8
        'Texture List
        Dim ClonedTextureRow As DataGridViewRow = ClearandGetClone(DataGridObjectTextureView)
        Dim WorkingTextureCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = TextureCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To TextureCount - 1
            Dim TempObjectTextureBytes As Byte() = New Byte(&H10 - 1) {}
            Array.Copy(CompleteYobjBytes, TextureStartOffset + i * &H10, TempObjectTextureBytes, 0, &H10)
            Dim TempTextureGridRow As DataGridViewRow = ClonedTextureRow.Clone()
            TempTextureGridRow.Cells(DataGridObjectTextureView.Columns.IndexOf(ObjectTextureCount)).Value = i
            TempTextureGridRow.Cells(DataGridObjectTextureView.Columns.IndexOf(ObjectTextureCount)).Style = ReadOnlyCellStyle
            TempTextureGridRow.Cells(DataGridObjectTextureView.Columns.IndexOf(ObjectTextureCol)).Value = Encoding.Default.GetString(TempObjectTextureBytes).TrimEnd(Chr(0))
            WorkingTextureCollection.Add(TempTextureGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectTextureView.Rows.AddRange(WorkingTextureCollection.ToArray())
    End Sub

    Sub FillObjectShaderView()
        Dim ShaderCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H34, 4), 0)
        Dim ShaderStartOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H30, 4), 0) + 8
        'Shader List
        Dim ClonedShaderRow As DataGridViewRow = ClearandGetClone(DataGridObjectShaderView)
        Dim WorkingShaderCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = ShaderCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To ShaderCount - 1
            Dim TempObjectShaderBytes As Byte() = New Byte(&H20 - 1) {}
            Array.Copy(CompleteYobjBytes, ShaderStartOffset + i * &H20, TempObjectShaderBytes, 0, &H20)
            Dim TempShaderGridRow As DataGridViewRow = ClonedShaderRow.Clone()
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderCount)).Value = i
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderCount)).Style = ReadOnlyCellStyle
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderCol)).Value = Encoding.Default.GetString(TempObjectShaderBytes, 0, &H10).TrimEnd(Chr(0))
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderType)).Value = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TempObjectShaderBytes, &H10, 4), 0)
            TempShaderGridRow.Cells(DataGridObjectShaderView.Columns.IndexOf(ObjectShaderB)).Value = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TempObjectShaderBytes, &H18, 4), 0)
            WorkingShaderCollection.Add(TempShaderGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectShaderView.Rows.AddRange(WorkingShaderCollection.ToArray())
    End Sub

#End Region

#Region "Emote Selector"

    Sub FillObjectEmoteComboBox()
        Dim EmoteNameCount As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H48, 4), 0)
        Dim EmoteNameStartOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, &H4C, 4), 0) + 8
        ObjectEmoteListComboBox.Items.Clear()
        If EmoteNameCount > 0 Then
            Dim EmoteList As List(Of String) = New List(Of String) From {
                "Neutral"
            }
            ProgressBar1.Maximum = EmoteNameCount - 1
            ProgressBar1.Value = 0
            For i As Integer = 0 To EmoteNameCount - 1
                EmoteList.Add(Encoding.Default.GetString(CompleteYobjBytes, EmoteNameStartOffset + i * &H10, &H10).TrimEnd(Chr(0)))
            Next
            ObjectEmoteListComboBox.Items.AddRange(EmoteList.ToArray())
            ObjectEmoteListComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub ObjectEmoteListComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ObjectEmoteListComboBox.SelectedIndexChanged
        If Not IsNothing(SelectedObjHeader) Then
            If Not SelectedObjHeader.VertexHeadCount = 1 Then
                FillVertexView()
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Individual Object"

    Sub FillSubObjectViews(SelectedObject As ObjectHeaderInformation)
        SelectedObjHeader = SelectedObject
        LoadedObjectToolStripMenuItem.Text = "Loaded Object: " & SelectedObjHeader.IndexCount
        FillVertexView()
        GetVertexViewDisplayedColumns()
        'FillUVsView()
        FillTriStripsView()
        FillFacesView()
        FillParamsView()
    End Sub

    Sub FillVertexView()
        'Clearing Previously generated as  weights
        If DataGridObjectVertexView.Columns.Count > DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 Then
            For i As Integer = DataGridObjectVertexView.Columns.Count - 1 To DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 Step -1
                DataGridObjectVertexView.Columns.RemoveAt(i)
            Next
        End If
        'For the weights I want to try adding columns at the end of the vertex data grid
        For i As Integer = 0 To SelectedObjHeader.WeightCount - 1
            DataGridObjectVertexView.Columns.Add("VertWeightNum" & i, "Weight Num " & i)
            DataGridObjectVertexView.Columns.Add("VertWeightSingle" & i, "Weight Single " & i)
        Next

        Dim ClonedVertexRow As DataGridViewRow = ClearandGetClone(DataGridObjectVertexView)
        Dim WorkingVertexCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SelectedObjHeader.VertexCount - 1
        ProgressBar1.Value = 0
        Dim SelectedEmote As UInt32 = 0
        If ObjectEmoteListComboBox.SelectedIndex < 0 Then
            SelectedEmote = 0
        Else
            SelectedEmote = ObjectEmoteListComboBox.SelectedIndex
        End If

        For i As Integer = 0 To SelectedObjHeader.VertexCount - 1
            SelectedObjHeader.VertexHeadList(SelectedEmote)(i).IndexCount = i + 1
            Dim TempVertexGridRow As DataGridViewRow = ClonedVertexRow.Clone()
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertCountCol)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).IndexCount
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertCountCol)).Style = ReadOnlyCellStyle
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertX)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).X
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertY)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).Y
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertZ)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).Z
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertRX)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).RX
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertRY)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).RY
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertRZ)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).RZ
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertWeight)).Value = SelectedObjHeader.VertexHeadList(SelectedEmote)(i).Weight
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertWeight)).Style = ReadOnlyCellStyle
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertU)).Value = SelectedObjHeader.TextureCordList(i).U
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertV)).Value = SelectedObjHeader.TextureCordList(i).V
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal1)).Value = SelectedObjHeader.NormalList(i).X
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal2)).Value = SelectedObjHeader.NormalList(i).Y
            TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3)).Value = SelectedObjHeader.NormalList(i).Z
            For JWeightNum As Integer = 0 To SelectedObjHeader.WeightCount - 1
                TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 + JWeightNum * 2).Value = SelectedObjHeader.WeightCollection(JWeightNum)(i).Num
                TempVertexGridRow.Cells(DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 2 + JWeightNum * 2).Value = SelectedObjHeader.WeightCollection(JWeightNum)(i).Weight
            Next
            TempVertexGridRow.Tag = SelectedObjHeader.VertexHeadList(SelectedEmote)(i)
            WorkingVertexCollection.Add(TempVertexGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectVertexView.Rows.AddRange(WorkingVertexCollection.ToArray())
    End Sub

    Private Sub ShowWeightsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowWeightsToolStripMenuItem.Click
        If ShowWeightsToolStripMenuItem.Text.Contains("☑") Then
            ShowWeightsToolStripMenuItem.Text = "☐ Show Weights"
        ElseIf ShowWeightsToolStripMenuItem.Text.Contains("☐") Then
            '☐
            ShowWeightsToolStripMenuItem.Text = "☑ Show Weights"
        End If
        GetVertexViewDisplayedColumns()
    End Sub

    Private Sub ShowNormalsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowNormalsToolStripMenuItem.Click
        If ShowNormalsToolStripMenuItem.Text.Contains("☑") Then
            ShowNormalsToolStripMenuItem.Text = "☐ Show Normals"
        ElseIf ShowNormalsToolStripMenuItem.Text.Contains("☐") Then
            '☐
            ShowNormalsToolStripMenuItem.Text = "☑ Show Normals"
        End If
        GetVertexViewDisplayedColumns()
    End Sub

    Sub GetVertexViewDisplayedColumns()
        For i As Integer = 0 To DataGridObjectVertexView.Columns.GetColumnCount(DataGridViewElementStates.None) - 1
            DataGridObjectVertexView.Columns(i).Visible = True
        Next
        If ShowWeightsToolStripMenuItem.Text.Contains("☑") Then
        Else
            For i As Integer = DataGridObjectVertexView.Columns.IndexOf(ObjectVertNormal3) + 1 To DataGridObjectVertexView.Columns.Count - 1
                DataGridObjectVertexView.Columns(i).Visible = False
            Next
        End If
        If ShowNormalsToolStripMenuItem.Text.Contains("☑") Then
        Else
            ObjectVertNormal1.Visible = False
            ObjectVertNormal2.Visible = False
            ObjectVertNormal3.Visible = False
        End If
    End Sub

    Sub FillTriStripsView()
        Dim ClonedRow As DataGridViewRow = ClearandGetClone(DataGridObjectTriStripsView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ' MessageBox.Show(Hex(PossibleFaceCount))
        ProgressBar1.Maximum = SelectedObjHeader.TriStripList.Count - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SelectedObjHeader.TriStripList.Count - 1
            Dim TempGridRow As DataGridViewRow = ClonedRow.Clone()
            TempGridRow.Cells(DataGridObjectTriStripsView.Columns.IndexOf(ObjectTriStripNum)).Value = i
            TempGridRow.Cells(DataGridObjectTriStripsView.Columns.IndexOf(ObjectTriStripNum)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectTriStripsView.Columns.IndexOf(ObjectTriStripVerts)).Value = String.Join(",", SelectedObjHeader.TriStripList(i))
            TempGridRow.Cells(DataGridObjectTriStripsView.Columns.IndexOf(ObjectTriStripVertCount)).Value = SelectedObjHeader.TriStripList(i).Count
            'ObjectTriStripVertCount
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectTriStripsView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Sub FillFacesView()
        'DataGridObjectFacesView
        Dim ClonedRow As DataGridViewRow = ClearandGetClone(DataGridObjectFacesView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ' MessageBox.Show(Hex(PossibleFaceCount))
        ProgressBar1.Maximum = SelectedObjHeader.FaceList.Count - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SelectedObjHeader.FaceList.Count - 1
            Dim TempGridRow As DataGridViewRow = ClonedRow.Clone()
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceCurrentCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceCurrentCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceVertex1)).Value = SelectedObjHeader.FaceList(i).Verticies(0)
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceVertex2)).Value = SelectedObjHeader.FaceList(i).Verticies(1)
            TempGridRow.Cells(DataGridObjectFacesView.Columns.IndexOf(ObjectFaceVertex3)).Value = SelectedObjHeader.FaceList(i).Verticies(2)
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectFacesView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

    Sub FillParamsView()
        'DataGridObjectFacesView
        Dim ClonedRow As DataGridViewRow = ClearandGetClone(DataGridObjectParamView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = SelectedObjHeader.ParameterCount - 1
        ProgressBar1.Value = 0
        For i As Integer = 0 To SelectedObjHeader.ParameterCount - 1
            Dim CurrentParamOffset As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, SelectedObjHeader.ParameterOffset + 8 + i * 4, 4), 0)
            Dim TempGridRow As DataGridViewRow = ClonedRow.Clone()
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamCountCol)).Value = i
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamCountCol)).Style = ReadOnlyCellStyle
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamName)).Value = Encoding.Default.GetString(CompleteYobjBytes, CurrentParamOffset + 8, &H10)
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamInt1)).Value = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, CurrentParamOffset + 8 + &H10, 2), 0)
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamInt2)).Value = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, CurrentParamOffset + 8 + &H12, 2), 0)
            TempGridRow.Cells(DataGridObjectParamView.Columns.IndexOf(ObjectParamSingle)).Value = BitConverter.ToSingle(HexaDecimalHandlers.EndianReverse(CompleteYobjBytes, CurrentParamOffset + 8 + &H14, 4), 0)
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        DataGridObjectParamView.Rows.AddRange(WorkingCollection.ToArray())
    End Sub

#End Region
End Class
