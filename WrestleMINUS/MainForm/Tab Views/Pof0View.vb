Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm

    Sub FillPof0View(SelectedData As TreeNode)
        Dim ParentFileBytes As Byte() = FilePartHandlers.GetFilePartBytes(SelectedData.Tag)
        'After the Parent File we want the Pof0 array
        Dim Pof0Index As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ParentFileBytes, 4, 4), 0) + 8
        Dim Pof0Length As UInt32 = BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ParentFileBytes, Pof0Index + 4, 4), 0)
        Dim Pof0Bytes As Byte() = New Byte(Pof0Length - 1) {}
        Array.Copy(ParentFileBytes, Pof0Index + 8, Pof0Bytes, 0, Pof0Length)
        'getting a generic row so we can create one for the collection
        Dim CloneRow As DataGridViewRow = ClearandGetClone(DataGridPof0View)
        Dim WorkingCollection As List(Of DataGridViewRow) = New List(Of DataGridViewRow)
        ProgressBar1.Maximum = Pof0Length
        ProgressBar1.Value = 0
        Dim Pof0CurrentRead As UInt32 = 0
        Dim ParentFileCurrentOffset As UInt32 = 8
        Do While Pof0CurrentRead < Pof0Length
            Dim TempGridRow As DataGridViewRow = CloneRow.Clone()
            'Parse if the current Pof0 Byte is a 1 byte or 2 byte reference.
            TempGridRow.Cells(0).Value = Hex(Pof0CurrentRead)
            TempGridRow.Cells(0).Style = ReadOnlyCellStyle
            If Pof0Bytes(Pof0CurrentRead) >= &HC0 Then
                '4 Byte ref
                TempGridRow.Cells(1).Value = Hex(BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(Pof0Bytes, Pof0CurrentRead, 4), 0))
                Dim TranslatedLength As UInt32 = 0
                TranslatedLength = (BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(Pof0Bytes, Pof0CurrentRead, 4), 0) - BitConverter.ToUInt32({0, 0, 0, &HC0}, 0)) * 4
                TempGridRow.Cells(2).Value = TranslatedLength
                TempGridRow.Cells(3).Value = Hex(TranslatedLength)
                ParentFileCurrentOffset += TranslatedLength
                TempGridRow.Cells(4).Value = ParentFileCurrentOffset
                TempGridRow.Cells(5).Value = Hex(ParentFileCurrentOffset)
                TempGridRow.Cells(6).Value = Hex(BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ParentFileBytes, ParentFileCurrentOffset, 4), 0))
                TempGridRow.Cells(6).Style = ReadOnlyCellStyle
                Pof0CurrentRead += 3
            ElseIf Pof0Bytes(Pof0CurrentRead) >= &H80 Then
                '2 Byte ref
                TempGridRow.Cells(1).Value = Hex(BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(Pof0Bytes, Pof0CurrentRead, 2), 0))
                Dim TranslatedLength As UInt32 = 0
                TranslatedLength = (BitConverter.ToUInt16(HexaDecimalHandlers.EndianReverse(Pof0Bytes, Pof0CurrentRead, 2), 0) - &H8000) * 4
                TempGridRow.Cells(2).Value = TranslatedLength
                TempGridRow.Cells(3).Value = Hex(TranslatedLength)
                ParentFileCurrentOffset += TranslatedLength
                TempGridRow.Cells(4).Value = ParentFileCurrentOffset
                TempGridRow.Cells(5).Value = Hex(ParentFileCurrentOffset)
                TempGridRow.Cells(6).Value = Hex(BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ParentFileBytes, ParentFileCurrentOffset, 4), 0))
                TempGridRow.Cells(6).Style = ReadOnlyCellStyle
                Pof0CurrentRead += 1
            ElseIf Pof0Bytes(Pof0CurrentRead) >= &H40 Then
                '1 byte ref
                TempGridRow.Cells(1).Value = Hex(Pof0Bytes(Pof0CurrentRead))
                Dim TranslatedLength As UInt16 = (Pof0Bytes(Pof0CurrentRead) - &H40) * 4
                TempGridRow.Cells(2).Value = TranslatedLength
                TempGridRow.Cells(3).Value = Hex(TranslatedLength)
                ParentFileCurrentOffset += TranslatedLength
                TempGridRow.Cells(4).Value = ParentFileCurrentOffset
                TempGridRow.Cells(5).Value = Hex(ParentFileCurrentOffset)
                TempGridRow.Cells(6).Value = Hex(BitConverter.ToUInt32(HexaDecimalHandlers.EndianReverse(ParentFileBytes, ParentFileCurrentOffset, 4), 0))
                TempGridRow.Cells(6).Style = ReadOnlyCellStyle
            ElseIf Pof0Bytes(Pof0CurrentRead) = 0 Then
                'there is no added offset so we shouldn't add a line
                Pof0CurrentRead += 1
                Continue Do
            Else
                'Shouldn't happen whut
                TempGridRow.Cells(1).Value = Hex(Pof0Bytes(Pof0CurrentRead))
                TempGridRow.Cells(2).Value = "ERROR"
            End If
            WorkingCollection.Add(TempGridRow)
            ProgressBar1.Value = Pof0CurrentRead
            Pof0CurrentRead += 1
        Loop
        ProgressBar1.Value = Pof0Length
        DataGridPof0View.Rows.AddRange(WorkingCollection.ToArray())
        For i As Integer = 0 To DataGridPof0View.Rows.Count - 1
            DataGridPof0View.Rows(i).HeaderCell.Value = i + 1
        Next
    End Sub

End Class
