'* 
'* myFire
'*
'* This file is part of the myFire software
'* Copyright (c) 2011, Ian Spence
'* All rights reserved.
'*
'* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
'* ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
'* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
'* IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
'* INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
'* NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
'* PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
'* WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
'* ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
'* POSSIBILITY OF SUCH DAMAGE.
'*
Imports Twitterizer
Public Class myFire_notificationwindow_general
    Friend WithEvents _timer As New System.Windows.Threading.DispatcherTimer
    Public Sub ShowGeneral(ByVal Title As String, ByVal Message As String, Optional ByVal Image As BitmapImage = Nothing)
        TitleBlock.Text = Title
        MessageBlock.Text = Message
        If Image IsNot Nothing Then
            TweetImage.ImageSource = Image
        End If
        Me.Top = 10
        If My.Settings.Notifications_Window_Placement = 0 Then
            Me.Left = 10
        Else
            Me.Left = Forms.Screen.PrimaryScreen.Bounds.Width - 310
        End If
        soundmanager.NotificationSound()
        Me.Show()
    End Sub

    Public Sub ShowMention(ByVal Status As TwitterStatus)
        TitleBlock.Text = "@" & Status.User.ScreenName & " mentioned you."
        MessageBlock.Text = myfireactions.FormatTweet(Status)
        TweetImage.ImageSource = myfireactions.Image_Format(Status.User.ProfileImageLocation, False)
        Me.Top = 10
        If My.Settings.Notifications_Window_Placement = 0 Then
            Me.Left = 10
        Else
            Me.Left = Forms.Screen.PrimaryScreen.Bounds.Width - 310
        End If
        If My.Settings.sounds_mentions = True Then
            soundmanager.NotificationSound()
        End If
        Me.Show()
    End Sub

    Public Sub ShowTweet(ByVal Status As TwitterStatus)
        TitleBlock.Text = "@" & Status.User.ScreenName
        MessageBlock.Text = myfireactions.FormatTweet(Status)
        TweetImage.ImageSource = myfireactions.Image_Format(Status.User.ProfileImageLocation, False)
        Me.Top = 10
        If My.Settings.Notifications_Window_Placement = 0 Then
            Me.Left = 10
        Else
            Me.Left = Forms.Screen.PrimaryScreen.Bounds.Width - 310
        End If
        If My.Settings.sounds_tweets = True Then
            soundmanager.NotificationSound()
        End If
        Me.Show()
    End Sub

    Public Sub ShowDirectMessage(ByVal Message As TwitterDirectMessage)
        TitleBlock.Text = "@" & Message.Sender.ScreenName & " messaged you."
        MessageBlock.Text = myfireactions.HtmlDecode(Message.Text)
        TweetImage.ImageSource = myfireactions.Image_Format(Message.Sender.ProfileImageLocation, False)
        Message = Message
        Me.Top = 10
        If My.Settings.Notifications_Window_Placement = 0 Then
            Me.Left = 10
        Else
            Me.Left = Forms.Screen.PrimaryScreen.Bounds.Width - 310
        End If
        If My.Settings.sounds_messages = True Then
            soundmanager.NotificationSound()
        End If
        Me.Show()
    End Sub

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        _timer.Interval = New TimeSpan(0, 0, 4)
        _timer.Start()
    End Sub

    Private Sub _timer_Tick(sender As Object, e As System.EventArgs) Handles _timer.Tick
        Me.Close()
    End Sub

    Private Sub myFire_notificationwindow_mention_MouseEnter(sender As Object, e As System.Windows.Input.MouseEventArgs) Handles Me.MouseEnter
        _timer.Stop()
    End Sub

    Private Sub myFire_notificationwindow_mention_MouseLeave(sender As Object, e As System.Windows.Input.MouseEventArgs) Handles Me.MouseLeave
        _timer.Start()
    End Sub
End Class
