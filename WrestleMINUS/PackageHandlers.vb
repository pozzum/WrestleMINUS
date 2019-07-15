Imports System.Text 'Text Encoding

Public Class PackageHandlers

    Shared ReadOnly LibraryContainsList As String() = New String() {"dds",
    "ymx",
    "tex",
    "map",
    "hkx",
    "pac",
    "txt",
    "cvx",
    "bin",
    "tpl"} ' list adds files from ufc undesputed.

    Shared Function CheckHeaderType(Index As Long, ByVal ByteArray As Byte(), Optional FileNamePath As String = "") As PackageType
        'To be split into 2 seperate functions once all processes are added
        Dim FirstFour As String
        'Make sure the file has bytes
        If ByteArray.Length = 0 Then
            Return PackageType.bin
        End If
        If Index > ByteArray.Length Then
            Return PackageType.bin
        End If
        FirstFour = Encoding.Default.GetChars(ByteArray, Index, 4)
        Select Case FirstFour
            Case "HSPC"
                Return PackageType.HSPC
            Case "SHDC"
                Return PackageType.SHDC
            Case "EPK8"
                Return PackageType.EPK8
            Case "PACH"
                Return PackageType.PACH
            Case "EPAC"
                Return PackageType.EPAC
            Case "ZLIB"
                Return PackageType.ZLIB
            Case "YOBJ"
                Return PackageType.YOBJ
            Case "JBOY"
                Return PackageType.YOBJ
            Case "NIBJ"
                Return PackageType.NIBJ
            Case "0FOP"
                Return PackageType.OFOP
            Case "YANM"
                Return PackageType.YANM
            Case "OODL"
                Return PackageType.OODL
            Case "VMUM"
                Return PackageType.VMUM
            Case "DUMY"
                Return PackageType.DUMY
            Case Else
                'if we don't have a perfect 4 match we go to check the 3 character matches
                Select Case True
                    Case FirstFour.Contains("STG")
                        Return PackageType.ShowInfo
                    Case FirstFour.Contains("DDS")
                        Return PackageType.DDS
                    Case FirstFour.Contains("KB2")
                        Return PackageType.bk2
                    Case FirstFour.Contains("COS")
                        Return PackageType.CostumeFile
                    Case FirstFour.Contains("BPE")
                        Return PackageType.BPE
                    Case FirstFour.Contains("ê¡Y")
                        Return PackageType.YOBJArray
                    Case FirstFour.Contains(" ¯0")
                        Return PackageType.TPL
                    Case Else
                        'if we do not have a header text to guide us we have some additional text checks that are consistent.
                        'some of these checks don't 100% require this many bytes, but none should functionally have that little byte length
                        If ByteArray.Length > &H30 Then
                            Select Case True
                                Case Encoding.Default.GetChars(ByteArray, Index + &H10, 4) = "aren"
                                    Return PackageType.ArenaInfo
                                Case Encoding.Default.GetChars(ByteArray, Index + &H14, 6) = "M_Head"
                                    Return PackageType.MaskFile
                                Case Encoding.Default.GetChars(ByteArray, Index + &H14, 6) = "M_Body"
                                    Return PackageType.MaskFile
                                Case BitConverter.ToUInt32(ByteArray, Index + &H14) = 2004 'this is an unsafe check.  It just works for all known games so far.
                                    Return PackageType.TitleFile
                                Case Encoding.Default.GetChars(ByteArray, Index + &H18, 3) = "yM_"
                                    Return PackageType.MuscleFile
                                Case LibraryContainsList.Contains(Encoding.Default.GetChars(ByteArray, Index + &H20, 3)) 'ufc games can have additional file types  create a short list of 3 char codes to test
                                    Return PackageType.TextureLibrary
                                Case Encoding.Default.GetChars(ByteArray, Index + &H24, 4) = "root"
                                    Return PackageType.YANMPack
                                Case FileNamePath.ToLower.Contains("string")
                                    Dim NumberCheck As UInt32 = BitConverter.ToUInt32(ByteArray, Index + 0)
                                    If NumberCheck = 0 Then
                                        Return PackageType.StringFile
                                    Else
                                        Return PackageType.bin
                                    End If
                                Case Else
                                    Return PackageType.bin
                            End Select
                        Else
                            Return PackageType.bin
                        End If
                End Select
        End Select
    End Function

    Shared Function Expandable(TestType As PackageType) As Boolean
        If TestType = PackageType.Unchecked OrElse
           TestType = PackageType.Folder OrElse
           TestType = PackageType.HSPC OrElse
           TestType = PackageType.EPK8 OrElse
           TestType = PackageType.EPAC OrElse
           TestType = PackageType.SHDC OrElse
           TestType = PackageType.PACH OrElse
            TestType = PackageType.BPE OrElse
           TestType = PackageType.ZLIB OrElse
           TestType = PackageType.OODL OrElse
           TestType = PackageType.PachDirectory_4 OrElse
           TestType = PackageType.PachDirectory_8 OrElse
           TestType = PackageType.TextureLibrary OrElse
           TestType = PackageType.YANMPack OrElse
           TestType = PackageType.YOBJ Then
            Return True
        End If
        Return False
    End Function

    Shared Function GetImageIndex(SentType As PackageType) As UInt32
        Select Case SentType
            Case PackageType.Folder
                Return 1
            Case PackageType.HSPC
                Return 2
            Case PackageType.EPK8
                Return 3
            Case PackageType.EPAC
                Return 4
            Case PackageType.SHDC
                Return 5
            Case PackageType.PACH
                Return 6
            Case PackageType.PachDirectory_4
                Return 6
            Case PackageType.PachDirectory_8
                Return 6
            Case PackageType.BPE
                Return 7
            Case PackageType.ZLIB
                Return 8
            Case PackageType.OODL
                Return 9
            Case PackageType.TextureLibrary
                Return 10
            Case PackageType.TPL
                Return 10
            Case PackageType.YANMPack
                Return 11
            Case PackageType.YANM
                Return 11
            Case PackageType.OFOP
                Return 11
            Case PackageType.YOBJ
                Return 12
            Case PackageType.YOBJArray
                Return 12
            Case PackageType.StringFile
                Return 13
            Case PackageType.DDS
                Return 14
            Case PackageType.DUMY
                Return 14
            Case PackageType.ArenaInfo
                Return 15
            Case PackageType.ShowInfo
                Return 16
            Case PackageType.NIBJ
                Return 17
            Case PackageType.bk2
                Return 18
            Case PackageType.CostumeFile
                Return 19
            Case PackageType.MuscleFile
                Return 20
            Case PackageType.UFC_PAC
                Return 20
            Case PackageType.UFC_BIN
                Return 20
            Case PackageType.UFC_CVX
                Return 20
            Case PackageType.UFC_HKX
                Return 20
            Case PackageType.UFC_MAP
                Return 20
            Case PackageType.UFC_TXT
                Return 20
            Case PackageType.MaskFile
                Return 21
            Case PackageType.VMUM
                Return 22
            Case PackageType.TitleFile
                Return 23
            Case Else
                Return 0
        End Select
    End Function

End Class