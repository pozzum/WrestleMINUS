Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Public Enum YANMBone As Integer
        Unknown = 0
        vector = &HC5879E5
        J_Hips = &HF91A7A26
        Ch_rotation = &HDAECC2E
        J_Spine1 = &H3D185308
        J_Head = &HF9D973BE
        J_Neck = &HF91972B1
        J_Clavicle_L = &HD153E363
        J_Shoulder_L = &HA46A2F43
        J_Elbow_L = &H6742F8AA
        J_Wrist_L = &HD4C2705A
        J_MiddleF0_L = &HB10CEB4C
        J_MiddleF1_L = &HB10CAB4C
        J_MiddleF2_L = &HB10C6B4C
        J_MiddleF3_L = &HB10C2B4C
        J_Spine2
        J_Chest
        J_Jaw
        J_Tongue1
        J_Tongue2
        J_Tongue3
        J_Tongue4
        J_Eye_L
        J_Eye_R
        H_Neck_tw
        J_IndexF0_L
        J_IndexF1_L
        J_IndexF2_L
        J_IndexF3_L
        J_PinkyF0_L
        J_PinkyF1_L
        J_PinkyF2_L
        J_PinkyF3_L
        J_RingF0_L
        J_RingF1_L
        J_RingF2_L
        J_RingF3_L
        J_ThumbF1_L
        J_ThumbF2_L
        J_ThumbF3_L
        H_Elbow_L_tw01
        H_Elbow_L_tw02
        H_Delt_L_OS01
        H_Ebw_In_L
        H_Ebw_Out_L
        H_TrapBase_L
        J_Clavicle_R
        J_Shoulder_R
        J_Elbow_R
        J_Wrist_R
        J_MiddleF0_R
        J_MiddleF1_R
        J_MiddleF2_R
        J_MiddleF3_R
        J_IndexF0_R
        J_IndexF1_R
        J_IndexF2_R
        J_IndexF3_R
        J_PinkyF0_R
        J_PinkyF1_R
        J_PinkyF2_R
        J_PinkyF3_R
        J_RingF0_R
        J_RingF1_R
        J_RingF2_R
        J_RingF3_R
        J_ThumbF1_R
        J_ThumbF2_R
        J_ThumbF3_R
        H_Elbow_R_tw01
        H_Elbow_R_tw02
        H_Ebw_In_R
        H_Ebw_Out_R
        H_Delt_R_OS01
        H_TrapBase_R
        J_Leg_L
        J_Knee_L
        J_Foot_L
        J_Toe_L
        H_Hip_L_OS1
        H_Kn_L_OS02
        H_Kn_L_OS01
        J_Leg_R
        J_Knee_R
        J_Foot_R
        J_Toe_R
        H_Kn_R_OS02
        H_Kn_R_OS01
        H_Hip_R_OS1
        H_Leg_Vol_C_R
    End Enum

    Public Class AnimationHeaderInformation
        Public StartingID As UInt16 = 0
        Public HeaderLength As UInt16 = 0
        Public BoneType As Int32 = 0
        Public BoneParsed As YANMBone = YANMBone.Unknown
        Public AnimationPartAIndex As UInt32 = 0
        Public AnimationPartALength As UInt32 = 0
        Public AnimationPartBIndex As UInt32 = 0
        Public AnimationPartBLength As UInt32 = 0
        Public RemainingBytes As Byte() = New Byte() {}
        Public RemByteString As String = ""
        Public AnimationPartABytes As Byte() = New Byte() {}
        Public AnimationPartAString As String = ""
        Public AnimationPartBBytes As Byte() = New Byte() {}
        Public AnimationPartBString As String = ""
        Public XTranslation As Boolean = False
        Public YTranslation As Boolean = False
        Public ZTranslation As Boolean = False
        Public XRotation As Boolean = False
        Public YRotation As Boolean = False
        Public ZRotation As Boolean = False
        Public FrameCount As UInt32 = 0
        Public FinalAnimationString As String = ""
        Public FinalAnimationParsed As String = ""
    End Class

    Sub FillAnimationView(SelectedData As TreeNode)
        Dim AnimationBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        Dim AnimationBoneCount As UInt16 = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(AnimationBytes, &HA, 2), 0)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridAnimationView)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = AnimationBoneCount - 2
        ProgressBar1.Value = 0
        Dim RollingIndex As UInt32 = &H10
        For i As Integer = 0 To AnimationBoneCount - 2
            Dim TempPartLength As UInt16 = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(AnimationBytes, RollingIndex + 2, 2), 0)
            Dim TempAnimationBytes As Byte() = New Byte(TempPartLength - 1) {}
            Array.Copy(AnimationBytes, RollingIndex, TempAnimationBytes, 0, TempPartLength)
            RollingIndex += TempPartLength
            If Not TempPartLength = &H30 Then
                If Not TempPartLength = &H18 Then
                    MessageBox.Show(Hex(TempPartLength))
                End If
            End If
            Dim TempAnimationInformation As AnimationHeaderInformation = ParseBytesToAnimationHeaderInformation(TempAnimationBytes)
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            TempGridRow.Cells(0).Value = i
            TempGridRow.Cells(1).Value = TempAnimationInformation.StartingID
            TempGridRow.Cells(2).Value = Hex(TempAnimationInformation.StartingID)
            TempGridRow.Cells(3).Value = TempAnimationInformation.HeaderLength
            TempGridRow.Cells(4).Value = Hex(TempAnimationInformation.HeaderLength)
            'Try
            TempGridRow.Cells(5).Value = CType(TempAnimationInformation.BoneType, YANMBone).ToString
            'Catch ex As Exception
            'TempGridRow.Cells(5).Value = TempAnimationInformation.BoneType
            'End Try
            TempGridRow.Cells(6).Value = Hex(TempAnimationInformation.BoneType).PadLeft(8, "0")
            TempGridRow.Cells(7).Value = TempAnimationInformation.AnimationPartAIndex
            TempGridRow.Cells(8).Value = Hex(TempAnimationInformation.AnimationPartAIndex)
            TempGridRow.Cells(9).Value = TempAnimationInformation.AnimationPartALength * 8
            TempGridRow.Cells(10).Value = Hex(TempAnimationInformation.AnimationPartALength * 8)
            TempGridRow.Cells(11).Value = TempAnimationInformation.AnimationPartBIndex
            TempGridRow.Cells(12).Value = Hex(TempAnimationInformation.AnimationPartBIndex)
            TempGridRow.Cells(13).Value = TempAnimationInformation.AnimationPartBLength * 8
            TempGridRow.Cells(14).Value = Hex(TempAnimationInformation.AnimationPartBLength * 8)
            TempGridRow.Cells(15).Value = TempAnimationInformation.RemByteString
            'Getting Animation Information from the main file
            TempAnimationInformation.AnimationPartABytes = New Byte(TempAnimationInformation.AnimationPartALength * 8 - 1) {}
            Array.Copy(AnimationBytes, TempAnimationInformation.AnimationPartAIndex + 8, TempAnimationInformation.AnimationPartABytes, 0, TempAnimationInformation.AnimationPartALength * 8)
            TempAnimationInformation.AnimationPartBBytes = New Byte(TempAnimationInformation.AnimationPartBLength * 8 - 1) {}
            Array.Copy(AnimationBytes, TempAnimationInformation.AnimationPartBIndex + 8, TempAnimationInformation.AnimationPartBBytes, 0, TempAnimationInformation.AnimationPartBLength * 8)
            FinishAnimationParse(TempAnimationInformation)
            'Applying the additional information to the additional columns
            TempGridRow.Cells(16).Value = TempAnimationInformation.AnimationPartAString
            TempGridRow.Cells(17).Value = TempAnimationInformation.AnimationPartBString
            TempGridRow.Cells(18).Value = TempAnimationInformation.XTranslation
            TempGridRow.Cells(19).Value = TempAnimationInformation.YTranslation
            TempGridRow.Cells(20).Value = TempAnimationInformation.ZTranslation
            TempGridRow.Cells(21).Value = TempAnimationInformation.XRotation
            TempGridRow.Cells(22).Value = TempAnimationInformation.YRotation
            TempGridRow.Cells(23).Value = TempAnimationInformation.ZRotation
            TempGridRow.Cells(24).Value = TempAnimationInformation.FrameCount
            TempGridRow.Cells(25).Value = Hex(TempAnimationInformation.FrameCount)
            TempGridRow.Cells(26).Value = TempAnimationInformation.FinalAnimationString
            TempGridRow.Cells(27).Value = TempAnimationInformation.FinalAnimationParsed
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = i
        Next
        GetAnimationViewDisplayedColumns()
        DataGridAnimationView.Rows.AddRange(WorkingCollection.ToArray())
        For i As Integer = 0 To DataGridAnimationView.Rows.Count - 1
            DataGridAnimationView.Rows(i).HeaderCell.Value = i + 1
        Next

    End Sub

    Function ParseBytesToAnimationHeaderInformation(TestedByteArray As Byte()) As AnimationHeaderInformation
        Dim ReturnedAnimationInfo As AnimationHeaderInformation = New AnimationHeaderInformation With {
           .StartingID = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(TestedByteArray, 0, 2), 0),
           .HeaderLength = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(TestedByteArray, 2, 2), 0),
           .BoneType = BitConverter.ToInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, 4), 0),
           .AnimationPartAIndex = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, 8), 0),
           .AnimationPartALength = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &HC), 0),
           .AnimationPartBIndex = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H10), 0),
           .AnimationPartBLength = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(TestedByteArray, &H14), 0)}
        If TestedByteArray.Length > &H18 Then
            ReturnedAnimationInfo.RemainingBytes = New Byte(&H18 - 1) {}
            Array.Copy(TestedByteArray, &H18, ReturnedAnimationInfo.RemainingBytes, 0, &H18)
            ReturnedAnimationInfo.RemByteString = (BitConverter.ToString(ReturnedAnimationInfo.RemainingBytes).Replace("-", " "))
        Else
            ReturnedAnimationInfo.RemainingBytes = New Byte() {}
            ReturnedAnimationInfo.RemByteString = ""

        End If
        Return ReturnedAnimationInfo
    End Function

    Sub FinishAnimationParse(ByRef PartialAnimationInformation As AnimationHeaderInformation)
        PartialAnimationInformation.AnimationPartAString = BitConverter.ToString(PartialAnimationInformation.AnimationPartABytes).Replace("-", " ")
        PartialAnimationInformation.AnimationPartAString = GeneralTools.TruncateString(PartialAnimationInformation.AnimationPartAString, 32000)
        PartialAnimationInformation.AnimationPartBString = BitConverter.ToString(PartialAnimationInformation.AnimationPartBBytes).Replace("-", " ")
        PartialAnimationInformation.AnimationPartBString = GeneralTools.TruncateString(PartialAnimationInformation.AnimationPartBString, 32000)
        If PartialAnimationInformation.StartingID < 1000 Then
            'A is the header to parse
            'here is a check to parse the other byte header if frames = 0
            'Check boxes X - Y - Z Transitional
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 0) = 1 Then
                PartialAnimationInformation.XTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 0) = &HFF00 Then
                PartialAnimationInformation.XTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 0)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 2) = 1 Then
                PartialAnimationInformation.YTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 2) = &HFF00 Then
                PartialAnimationInformation.YTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 2)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 4) = 1 Then
                PartialAnimationInformation.ZTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 4) = &HFF00 Then
                PartialAnimationInformation.ZTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 4)))
            End If

            'Check boxes X - Y - Z Rotational
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 8) = 1 Then
                PartialAnimationInformation.XRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 8) = &HFF00 Then
                PartialAnimationInformation.XRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 8)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 10) = 1 Then
                PartialAnimationInformation.YRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 10) = &HFF00 Then
                PartialAnimationInformation.YRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 10)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 12) = 1 Then
                PartialAnimationInformation.ZRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 12) = &HFF00 Then
                PartialAnimationInformation.ZRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartABytes, 12)))
            End If
            PartialAnimationInformation.FrameCount = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(PartialAnimationInformation.AnimationPartABytes, 14, 2), 0)
            PartialAnimationInformation.FinalAnimationString = PartialAnimationInformation.AnimationPartBString
            If PartialAnimationInformation.StartingID = 772 Then
                'Dim TestSubFrames As Integer = 0
                For i As Integer = 0 To PartialAnimationInformation.AnimationPartBLength Step 8
                    'PartialAnimationInformation.FinalAnimationParsed += BitConverter.ToDouble(HexaDecimalHandlers.EndianReverse(PartialAnimationInformation.AnimationPartBBytes, i, 8), 0).ToString() & ", "
                    'TestSubFrames += PartialAnimationInformation.AnimationPartBBytes(i + 7)
                    For j As Integer = 0 To 5
                        If PartialAnimationInformation.AnimationPartBBytes(i + j) > 128 Then
                            PartialAnimationInformation.FinalAnimationParsed += (-(&HFF - PartialAnimationInformation.AnimationPartBBytes(i + j))).ToString & ", "
                        Else
                            PartialAnimationInformation.FinalAnimationParsed += (PartialAnimationInformation.AnimationPartBBytes(i + j)).ToString & ", "
                        End If
                    Next
                    PartialAnimationInformation.FinalAnimationParsed += vbNewLine
                Next
                'PartialAnimationInformation.FinalAnimationParsed = TestSubFrames
            ElseIf PartialAnimationInformation.StartingID = 516 Then
                Dim TestedFrames As Integer = 0
                For i As Integer = 16 To PartialAnimationInformation.AnimationPartBLength Step 20
                    TestedFrames += BitConverter.ToInt16(HexaDecimalHandlers.EndianReverse(PartialAnimationInformation.AnimationPartBBytes, i + 4, 2), 0)
                Next
                PartialAnimationInformation.FinalAnimationParsed = TestedFrames
            End If
        Else
            'B is our header to parse
            'Check boxes X - Y - Z Transitional
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 0) = 1 Then
                PartialAnimationInformation.XTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 0) = &HFF00 Then
                PartialAnimationInformation.XTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 0)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 2) = 1 Then
                PartialAnimationInformation.YTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 2) = &HFF00 Then
                PartialAnimationInformation.YTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 2)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 4) = 1 Then
                PartialAnimationInformation.ZTranslation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 4) = &HFF00 Then
                PartialAnimationInformation.ZTranslation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 4)))
            End If

            'Check boxes X - Y - Z Rotational
            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 8) = 1 Then
                PartialAnimationInformation.XRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 8) = &HFF00 Then
                PartialAnimationInformation.XRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 8)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 10) = 1 Then
                PartialAnimationInformation.YRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 10) = &HFF00 Then
                PartialAnimationInformation.YRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 10)))
            End If

            If BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 12) = 1 Then
                PartialAnimationInformation.ZRotation = True
            ElseIf BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 12) = &HFF00 Then
                PartialAnimationInformation.ZRotation = False
            Else
                MessageBox.Show(Hex(BitConverter.ToUInt16(PartialAnimationInformation.AnimationPartBBytes, 12)))
            End If
            PartialAnimationInformation.FrameCount = BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(PartialAnimationInformation.AnimationPartBBytes, 14, 2), 0)
            PartialAnimationInformation.FinalAnimationString = PartialAnimationInformation.AnimationPartAString
            If PartialAnimationInformation.StartingID = 772 Then
                'Dim TestSubFrames As Integer = 0
                For i As Integer = 0 To PartialAnimationInformation.AnimationPartALength Step 8
                    'PartialAnimationInformation.FinalAnimationParsed += BitConverter.ToDouble(HexaDecimalHandlers.EndianReverse(PartialAnimationInformation.AnimationPartABytes, i, 8), 0).ToString() & ", "
                    'TestSubFrames += PartialAnimationInformation.AnimationPartABytes(i + 7)
                    For j As Integer = 0 To 5
                        If PartialAnimationInformation.AnimationPartABytes(i + j) > 128 Then
                            PartialAnimationInformation.FinalAnimationParsed += (-(&HFF - PartialAnimationInformation.AnimationPartABytes(i + j))).ToString & ", "
                        Else
                            PartialAnimationInformation.FinalAnimationParsed += (PartialAnimationInformation.AnimationPartABytes(i + j)).ToString & ", "
                        End If
                    Next
                    PartialAnimationInformation.FinalAnimationParsed += vbNewLine
                Next
                'PartialAnimationInformation.FinalAnimationParsed = TestSubFrames
            ElseIf PartialAnimationInformation.StartingID = 516 Then
                Dim TestedFrames As Integer = 0
                For i As Integer = 16 To PartialAnimationInformation.AnimationPartALength Step 20
                    TestedFrames += BitConverter.ToInt16(HexaDecimalHandlers.EndianReverse(PartialAnimationInformation.AnimationPartABytes, i + 4, 2), 0)
                Next
                PartialAnimationInformation.FinalAnimationParsed = TestedFrames
            End If
        End If

        '772 = 8 byte chunks
    End Sub

    Sub GetAnimationViewDisplayedColumns()
        For i As Integer = 0 To DataGridAnimationView.Columns.GetColumnCount(DataGridViewElementStates.None) - 1
            DataGridAnimationView.Columns(i).Visible = True
        Next
        If AnimationShowDebugToolStripMenuItem.Text.Contains("☑") Then
            If AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
                'Show Hex Columns and Hide Decimal Columns
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
                'Show Hex Columns and Also Show Decimal Columns
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☒") Then
                'Hide Hex Columns and Show Decimal Columns
            End If
        Else '☐
            DataGridAnimationView.Columns(3).Visible = False
            DataGridAnimationView.Columns(4).Visible = False
            DataGridAnimationView.Columns(7).Visible = False
            DataGridAnimationView.Columns(8).Visible = False
            DataGridAnimationView.Columns(9).Visible = False
            DataGridAnimationView.Columns(10).Visible = False
            DataGridAnimationView.Columns(11).Visible = False
            DataGridAnimationView.Columns(12).Visible = False
            DataGridAnimationView.Columns(13).Visible = False
            DataGridAnimationView.Columns(14).Visible = False
            DataGridAnimationView.Columns(16).Visible = False
            DataGridAnimationView.Columns(17).Visible = False
            If AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
                'Show Hex Columns and Hide Decimal Columns
                DataGridAnimationView.Columns(1).Visible = False
                DataGridAnimationView.Columns(24).Visible = False
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☐") Then
                'Show Hex Columns and Also Show Decimal Columns
            ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☒") Then
                'Hide Hex Columns and Show Decimal Columns
                DataGridAnimationView.Columns(2).Visible = False
                DataGridAnimationView.Columns(25).Visible = False
            End If
        End If
    End Sub

    Private Sub AnimationShowHexToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnimationShowHexToolStripMenuItem.Click
        If AnimationShowHexToolStripMenuItem.Text.Contains("☑") Then
            AnimationShowHexToolStripMenuItem.Text = "☐ Show Hex"
        ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☐") Then
            '☐
            AnimationShowHexToolStripMenuItem.Text = "☒ Show Hex"
            '☒
        ElseIf AnimationShowHexToolStripMenuItem.Text.Contains("☒") Then
            AnimationShowHexToolStripMenuItem.Text = "☑ Show Hex"
        End If
        GetAnimationViewDisplayedColumns()
    End Sub

    Private Sub AnimationShowDebugToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnimationShowDebugToolStripMenuItem.Click
        If AnimationShowDebugToolStripMenuItem.Text.Contains("☑") Then
            AnimationShowDebugToolStripMenuItem.Text = "☐ Show Debug"
        ElseIf AnimationShowDebugToolStripMenuItem.Text.Contains("☐") Then
            '☐
            AnimationShowDebugToolStripMenuItem.Text = "☑ Show Debug"
        End If
        GetAnimationViewDisplayedColumns()
    End Sub

End Class
