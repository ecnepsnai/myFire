Public Class myFire_supportmyfire

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Process.Start("http://rdir.ifxsoftware.net/e009.html")
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        Dim t As New TweetDialog
        t.Tweet_Format("Check out @MyFireApp, it's an awesome new @Twitter app built for your Windows PC.  http://ifxsoftware.net/MyFire.html")
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button3.Click
        Process.Start("http://rdir.ifxsoftware.net/e010.html")
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button4.Click
        Try
            myfireactions.Twitter_Friendship_Create("MyFireApp")
            exceptionhandler.InfoMessage("Followed @MyFireApp", "")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button5.Click
        Process.Start("http://rdir.ifxsoftware.net/e011.html")
    End Sub
End Class
