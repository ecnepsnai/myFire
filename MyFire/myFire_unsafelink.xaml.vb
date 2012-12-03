Public Class myFire_unsafelink
    Dim site As String
    Public Sub ShowWarning(ByVal URL As String)
        site = URL
        Me.ShowDialog()
    End Sub

    Private Sub myFire_unsafelink_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        My.Computer.Audio.PlaySystemSound(System.Media.SystemSounds.Exclamation)
    End Sub

    Private Sub cb_proc_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_proc.Checked
        btn_go.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub cb_proc_Unchecked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_proc.Unchecked
        btn_go.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Sub btn_esc_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_esc.Click
        Me.Close()
    End Sub

    Private Sub btn_go_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_go.Click
        Process.Start(site)
    End Sub
End Class
