Partial Class GeneralTools
    'This is included to disable Visual Studio thinking that this file should be a form.
End Class

Partial Class OptionsMenu

#Region "DLL Tab"

    Sub LoadDLLTab()
        If ApplicationHandlers.CheckFontAwesome() Then
            LabelFontAwesome.Text = "FontAwesome Loaded: True"
            LabelFontAwesome.ForeColor = Color.Black
            ButtonSelectFontAwesome.Visible = False
        Else
            LabelFontAwesome.Text = "FontAwe. Loaded: False"
            LabelFontAwesome.ForeColor = Color.Red
            ButtonSelectFontAwesome.Visible = True
        End If
        If Not ApplicationHandlers.CheckIconicZlib() Then
            LabelZlib.Text = "Zlib DLL Loaded: False"
            LabelZlib.ForeColor = Color.Red
            ButtonSelectZlib.Visible = True
        Else
            LabelZlib.Text = "Zlib DLL Loaded: True"
            LabelZlib.ForeColor = Color.Black
            ButtonSelectZlib.Visible = False
        End If
        If Not ApplicationHandlers.CheckOodle6() Then
            LabelOodle_6.Text = "Oodle DLL Loaded: False"
            LabelOodle_6.ForeColor = Color.Red
            ButtonOodle6Select.Visible = True
            ComboBoxOodleCompressionLevel.Enabled = False
        Else
            LabelOodle_6.ForeColor = Color.Black
        End If
        If Not ApplicationHandlers.CheckOodle7() Then
            LabelOodle_7.Text = "Oodle DLL Loaded: False"
            LabelOodle_7.ForeColor = Color.Red
            ButtonOodle7Select.Visible = True
            ComboBoxOodleCompressionLevel.Enabled = False
        Else
            LabelOodle_7.ForeColor = Color.Black
        End If
    End Sub

    Private Sub ButtonSelectFontAwesome_Click(sender As Object, e As EventArgs) Handles ButtonSelectFontAwesome.Click
        ApplicationHandlers.CheckFontAwesome(True)
        LoadSettings()
    End Sub

    Private Sub ButtonSelectZlib_Click(sender As Object, e As EventArgs) Handles ButtonSelectZlib.Click
        ApplicationHandlers.CheckIconicZlib(True)
        LoadSettings()
    End Sub

    Private Sub ButtonOodle6Select_Click(sender As Object, e As EventArgs) Handles ButtonOodle6Select.Click
        ApplicationHandlers.CheckOodle6(True)
        LoadSettings()
    End Sub

    Private Sub ButtonOodle7Select_Click(sender As Object, e As EventArgs) Handles ButtonOodle7Select.Click
        ApplicationHandlers.CheckOodle7(True)
        LoadSettings()
    End Sub

    Private Sub ButtonNewtonJsonSelect_Click(sender As Object, e As EventArgs) Handles ButtonNewtonJsonSelect.Click
        ApplicationHandlers.CheckNewtonsoftJson(True)
        LoadSettings()
    End Sub

#End Region

End Class