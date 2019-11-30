Imports System.IO   'Files
Imports System.Text 'Binary Formatter

Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class MainForm
    'moving functions from on tree view to on tab select to reduce load times during tree movement on keyboard
    Private Sub TabControl1_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles MainMenuTabControl.Selecting
        If e.TabPage.Name = StringView.Name Then
            'separating this out fixes a Load string issue.
            FillStringView(ReadNode)
        Else
            If InformationLoaded = False Then
                InformationLoaded = True
                FillTabDataGrid(e.TabPage)
            End If
        End If
    End Sub

    Private Sub FillTabDataGrid(SelectedTab As TabPage)
        Select Case SelectedTab.Name
            Case MiscView.Name
                FillMiscView(ReadNode)
            Case ShowView.Name
                FillShowView(ReadNode)
            Case NIBJView.Name
                FillNIBJView(ReadNode)
            Case PictureView.Name
                FillPictureView(ReadNode)
            Case ObjectView.Name
                FillObjectViews(ReadNode)
            Case AttireView.Name
                FillAttireView(ReadNode)
            Case MuscleView.Name
                FillMuscleView(ReadNode)
            Case MaskView.Name
                FillMaskView(ReadNode)
            Case ObjArrayView.Name
                FillObjectArrayView(ReadNode)
            Case AssetView.Name
                FillAssetFileView(ReadNode)
            Case TitleView.Name
                FillTitleFileView(ReadNode)
            Case SoundView.Name
                FillSoundRefFileView(ReadNode)
            Case MenuItemView.Name
                FillMenuItemView(ReadNode)
            Case AnimationView.Name
                FillAnimationView(ReadNode)
                FillPof0View(ReadNode)
            Case Pof0View.Name
                FillPof0View(ReadNode)
                InformationLoaded = False
            Case WeaponPositionView.Name
                FillWeaponPositionView(ReadNode)
            Case CakFileView.Name
                FillCAkFileView(ReadNode)
        End Select
    End Sub

    Function GetTabTypes(SelectedType As PackageType) As List(Of TabPage)
        Dim ReturnedList As List(Of TabPage) = New List(Of TabPage)
        Select Case SelectedType
            Case PackageType.StringFile
                ReturnedList.Add(StringView)
            Case PackageType.sdb
                ReturnedList.Add(StringView)
            Case PackageType.ArenaInfo
                ReturnedList.Add(MiscView)
            Case PackageType.ShowInfo
                ReturnedList.Add(ShowView)
            Case PackageType.NIBJ
                ReturnedList.Add(NIBJView)
            Case PackageType.DDS
                ReturnedList.Add(PictureView)
            Case PackageType.YOBJ
                ReturnedList.Add(ObjectView)
                ReturnedList.Add(Pof0View)
            Case PackageType.CostumeFile
                ReturnedList.Add(AttireView)
            Case PackageType.MaskFile
                ReturnedList.Add(MaskView)
            Case PackageType.MuscleFile
                ReturnedList.Add(MuscleView)
            Case PackageType.YOBJArray
                ReturnedList.Add(ObjArrayView)
            Case PackageType.VMUM
                ReturnedList.Add(AssetView)
            Case PackageType.TitleFile
                ReturnedList.Add(TitleView)
            Case PackageType.SoundReference
                ReturnedList.Add(SoundView)
            Case PackageType.LSD
                ReturnedList.Add(MenuItemView)
            Case PackageType.YANM
                ReturnedList.Add(AnimationView)
                ReturnedList.Add(Pof0View)
            Case PackageType.WeaponPosition
                ReturnedList.Add(WeaponPositionView)
            Case PackageType.Cak
                ReturnedList.Add(CakFileView)
            Case Else
                'Nothing
        End Select
        Return ReturnedList
    End Function

    Sub LoadTabs(NewTabList As List(Of TabPage))
        For Each TempNewTab As TabPage In NewTabList
            If Not MainMenuTabControl.TabPages.Contains(TempNewTab) Then
                MainMenuTabControl.TabPages.Add(TempNewTab)
                InformationLoaded = False
                If TempNewTab.Name = MuscleView.Name Then
                    FillMuscleView(ReadNode)
                End If
            ElseIf TempNewTab.Name = MuscleView.Name Then
                FillMuscleView(ReadNode)
            End If
        Next
    End Sub

End Class
