
Partial Class OptionsMenu

#Region "Form Functions"

    Private Sub OptionsMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = Me.Text & " Ver: " & My.Application.Info.Version.ToString
        LoadSettings()
    End Sub

    Sub LoadSettings()

        LoadFileSelectTab()
        LoadOptionsTab()
        LoadAdvancedTab()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        My.Settings.OptionMenuSelectedTab = TabControl1.SelectedIndex
    End Sub

#End Region

End Class