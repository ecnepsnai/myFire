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
Imports MyFire_storage
Imports System.Object


Public Class TweetDialog
    Dim IsMessage As Boolean = False
    Dim AttachedMedia As String
    Dim lat As Double
    Dim lon As Double
    Dim InReplyToId As String
    Dim InReplyToUser As String
    Dim ToUser As String
    Dim Command As Boolean = False
    Public SearchedUser As String
    Dim AllowedCharacters As Integer = 140
    Dim d As New MyFire_storage.Data

#Region "Comp buttons"
    Private Sub comp_trend_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles comp_trend.MouseDown
        Dim TrendSearch As New TrendSearch
        TweetBox.AppendText(TrendSearch.SearchUser)
    End Sub

    Private Sub comp_message_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles comp_message.MouseDown
        Dim UserSearch As New UserSearch
        ToUser = UserSearch.SearchUser()
        If ToUser = "" Then
            Exit Sub
        End If
        If myfireactions.Twitter_Friendship_FollowsMe(ToUser) = False Then
            exceptionhandler.WarningMessage("You cannot message a user who does not follow you.", "myFire")
            Exit Sub
        End If
        If ToUser IsNot "" Then
            MessagePanel.Visibility = Windows.Visibility.Visible
            UserLBL.Text = "Message: " & ToUser
            IsMessage = True
        End If
    End Sub
#End Region

#Region "Updating Methods"
    Sub Twitter_Post(ByVal Status As String, ByVal Tokens As OAuthTokens)
        Try
            Dim tweetResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.Update(Tokens, Status)
            If tweetResponse.Result <> RequestResult.Success Then
                Throw New Exception(tweetResponse.ErrorMessage)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Twitter_Media_Post(ByVal Status As String, ByVal Media As String, ByVal Tokens As OAuthTokens)
        Try
            Dim tweetResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.UpdateWithMedia(Tokens, Status, Media)
            If tweetResponse.Result <> RequestResult.Success Then
                Throw New Exception(tweetResponse.ErrorMessage)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Twitter_Reply(ByVal ReplyID As Decimal, ByVal Status As String, ByVal Tokens As OAuthTokens)
        Try
            Dim o As New StatusUpdateOptions
            o.InReplyToStatusId = ReplyID
            Dim tweetResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.Update(Tokens, Status, o)
            If tweetResponse.Result <> RequestResult.Success Then
                Throw New Exception(tweetResponse.ErrorMessage)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Twitter_Reply_Media(ByVal ReplyID As Decimal, ByVal Status As String, ByVal Media As String, ByVal Tokens As OAuthTokens)
        Try
            Dim o As New StatusUpdateOptions
            o.InReplyToStatusId = ReplyID
            Dim tweetResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.UpdateWithMedia(Tokens, Status, Media, o)
            If tweetResponse.Result <> RequestResult.Success Then
                Throw New Exception(tweetResponse.ErrorMessage)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Twitter_Message(ByVal Message As String, ByVal ScreenName As String, ByVal Tokens As OAuthTokens)
        Try
            Dim messageResponse As TwitterResponse(Of TwitterDirectMessage) = TwitterDirectMessage.Send(Tokens, ScreenName, Message)
            If messageResponse.Result <> RequestResult.Success Then
                Throw New Exception(messageResponse.ErrorMessage)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub PostTweet()
        If Command = True Then
            myfireactions.myFire_Commands(TweetBox.Text)
            Exit Sub
        End If
        If IsMessage = True Then
            Try
                myfireactions.Twitter_DirectMessage_Post(TweetBox.Text, ToUser)
                exceptionhandler.InfoMessage("Message Sent!", "myFire!")
                Me.Close()
                Exit Sub
            Catch ex As Exception
                exceptionhandler.ExceptionMessage(ex)
                Exit Sub
            End Try
        Else
            If TweetBox.Text.Length > AllowedCharacters Then
                Dim response As MessageBoxResult = MessageBox.Show("Your tweet is over 140 characters, however it contains a URL.  Twitter shortens URLs when tweeting, Would you like to try and send it anyway?", "myFire", MessageBoxButton.YesNo, MessageBoxImage.Question)
                If response = MessageBoxResult.No Then
                    Exit Sub
                End If
            End If

            ' TODO: Add multiple account posting '

            Try
                myfireactions.Twitter_Status_Update(TweetBox.Text, InReplyToId, AttachedMedia, lat, lon)
            Catch ex As Exception
                exceptionhandler.ExceptionMessage(ex)
                Exit Sub
            End Try
        End If
        AttachedPanel.Visibility = Windows.Visibility.Hidden
        MessagePanel.Visibility = Windows.Visibility.Hidden
        If My.Settings.myFire_TweetInBackground = False Then
            exceptionhandler.InfoMessage("Tweet posted!", "myFire")
        End If
        Me.Close()
    End Sub

    Private Sub TweetBox_KeyUp(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles TweetBox.KeyUp
        If e.Key = System.Windows.Input.Key.Enter Then
            PostTweet()
        End If
    End Sub

    Private Sub TweetBox_TextChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles TweetBox.TextChanged
        ' Math for Remaining Characters, Replace 140 with 500 for facebook posts.
        CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)

        If TweetBox.Text.Length > AllowedCharacters Then
            CharLBL.Foreground = Brushes.Red
            CharLBL_Effect.Color = Colors.Red
        Else
            CharLBL.Foreground = Brushes.White
            CharLBL_Effect.Color = Colors.Black
        End If

        ' For commands
        If TweetBox.Text.StartsWith("//") Then
            Command = True
        Else
            Command = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        PostTweet()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        AttachedMedia = ""
        AttachedPanel.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button3.Click
        MessagePanel.Visibility = Windows.Visibility.Hidden
        IsMessage = False
    End Sub

    Private Sub TweetDialog_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If AttachedPanel.Visibility = Windows.Visibility.Visible Then
            AttachedPanel.Visibility = Windows.Visibility.Collapsed
        End If
        If MessagePanel.Visibility = Windows.Visibility.Visible Then
            MessagePanel.Visibility = Windows.Visibility.Collapsed
        End If
    End Sub

    Private Sub TweetDialog_KeyUp(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.KeyUp
        If e.Key = Key.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub TweetDialog_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        TweetBox.Focus()
        Dim mu As MenuItem
        For Each User As String In My.Settings.linked_ScreenNames
            mu = New MenuItem
            mu.Header = User
            mu.IsCheckable = True
            If User = tokens.GetDefaultScreenName Then
                mu.IsChecked = True
            Else
            End If
            UsersContextMenu.Items.Add(mu)
        Next
        Me.Title = myfireactions.Tweet_Title_Format(tokens.GetDefaultScreenName)
        TweetBox.SelectionStart = TweetBox.Text.ToCharArray().Length
        TweetBox.SelectionLength = 0
    End Sub

    Public Sub Tweet_Format(ByVal Tweet As String)
        TweetBox.Text = Tweet
        Me.ShowDialog()
    End Sub

    Public Sub Reply_Format(ByVal User As String, ByVal ID As String, Optional ByVal Tweet As String = "")
        If Tweet = "" Then
            TweetBox.Text = "@" & User & " "
        Else
            TweetBox.Text = Tweet
        End If
        InReplyToId = ID
        InReplyToUser = User
        Me.Title = "Reply to: @" & User
        Me.ShowDialog()
    End Sub

    Public Sub Message_Format(Optional ByVal ScreenName As String = Nothing, Optional ByVal Text As String = Nothing)
        If ScreenName = Nothing Then
            Dim UserSearch As New UserSearch
            ToUser = UserSearch.SearchUser()
            If ToUser = "" Then
                Exit Sub
            End If
            If ToUser IsNot "" Then
                MessagePanel.Visibility = Windows.Visibility.Visible
                UserLBL.Text = "Message: " & ToUser
                IsMessage = True
            End If
            Me.ShowDialog()
        Else
            ToUser = ScreenName
            If ToUser = "" Then
                Exit Sub
            End If
            If ToUser IsNot "" Then
                MessagePanel.Visibility = Windows.Visibility.Visible
                UserLBL.Text = "Message: " & ToUser
                IsMessage = True
                If Text IsNot Nothing Then
                    TweetBox.Text = Text
                End If
            End If
            Me.ShowDialog()
        End If
    End Sub

    Public Sub Media_Format(ByVal Tweet As String, ByVal Media As String)
        AttachedMedia = Media
        AttachedPanel.Visibility = Windows.Visibility.Visible
        AttachmentLabel.Text = Media
        AllowedCharacters = 120
        CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)
        Me.ShowDialog()
    End Sub

    Public Sub Reply_MediaFormat(ByVal Tweet As String, ByVal Media As String, ByVal InReplyToId As Decimal, ByVal InReplyToUser As String)
        TweetBox.Text = Tweet
        AttachedMedia = Media
        AttachedPanel.Visibility = Windows.Visibility.Visible
        AllowedCharacters = 120
        CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)
        If Tweet = "" Then
            TweetBox.Text = "@" & InReplyToUser & " "
        Else
            TweetBox.Text = Tweet
        End If
        InReplyToId = InReplyToId
        InReplyToUser = InReplyToUser
        Me.Title = "Reply to: @" & InReplyToUser
        Me.ShowDialog()
    End Sub

    Private Sub DropGrid_Drop(sender As Object, e As System.Windows.DragEventArgs) Handles DropGrid.Drop
        Dim FileList As New List(Of String)
        For Each File As String In e.Data.GetData(DataFormats.FileDrop)
            FileList.Add(File)
        Next
        If FileList.Count > 1 Then
            MessageBox.Show("Only one file can be attached at a time.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            DropGrid.Visibility = Windows.Visibility.Hidden
        Else
            If FileList(0).ToUpper.Contains(".JPG") Then
                AttachedMedia = FileList(0)
                AttachedPanel.Visibility = Windows.Visibility.Visible
                AttachmentLabel.Text = FileList(0)
                AllowedCharacters = 120
                CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)
                DropGrid.Visibility = Windows.Visibility.Hidden
                Exit Sub
            End If
            If FileList(0).ToUpper.Contains(".PNG") Then
                AttachedMedia = FileList(0)
                AttachedPanel.Visibility = Windows.Visibility.Visible
                AttachmentLabel.Text = FileList(0)
                AllowedCharacters = 120
                CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)
                DropGrid.Visibility = Windows.Visibility.Hidden
                Exit Sub
            End If
            If FileList(0).ToUpper.Contains(".PNG") Then
                AttachedMedia = FileList(0)
                AttachedPanel.Visibility = Windows.Visibility.Visible
                AttachmentLabel.Text = FileList(0)
                AllowedCharacters = 120
                CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)
                DropGrid.Visibility = Windows.Visibility.Hidden
                Exit Sub
            End If
            MessageBox.Show("Only .JPG, .PNG, and .GIF files can be attached to tweets.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
            DropGrid.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub Grid1_DragEnter(sender As System.Object, e As System.Windows.DragEventArgs) Handles Grid1.DragEnter
        DropGrid.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub Grid1_DragLeave(sender As Object, e As System.Windows.DragEventArgs) Handles Grid1.DragLeave
        DropGrid.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub TweetBox_DragEnter(sender As System.Object, e As System.Windows.DragEventArgs) Handles TweetBox.DragEnter
        DropGrid.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub TweetBox_DragLeave(sender As System.Object, e As System.Windows.DragEventArgs) Handles TweetBox.DragLeave
        DropGrid.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub comp_cog_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles comp_cog.MouseDown
        UsersContextMenu.IsOpen = True
    End Sub

    Private Sub comp_image_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles comp_image.MouseDown
        Dim OFD As New Microsoft.Win32.OpenFileDialog
        OFD.Filter = "Supported Media|*.jpg;*.png;*.gif|All Files|*.*"
        OFD.Title = "Attach Media"
        OFD.FileName = ""
        OFD.Multiselect = False
        Dim Result? As Boolean = OFD.ShowDialog
        If Result = True Then
            AttachedMedia = OFD.FileName
            AttachedPanel.Visibility = Windows.Visibility.Visible
            AttachmentLabel.Text = OFD.SafeFileName
            AllowedCharacters = 120
            CharLBL.Text = String.Format("{0}/{1}", TweetBox.Text.Length, AllowedCharacters)
        End If
    End Sub

    Private Sub comp_mention_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles comp_mention.MouseDown
        Dim UserSearch As New UserSearch
        ToUser = UserSearch.SearchUser()
        If ToUser = "" Then
            Exit Sub
        End If
        TweetBox.AppendText("@" & ToUser)
    End Sub

    Private Sub DragGrid_DragEnter(sender As System.Object, e As System.Windows.DragEventArgs) Handles DragGrid.DragEnter
        DropGrid.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub DragGrid_DragLeave(sender As Object, e As System.Windows.DragEventArgs) Handles DragGrid.DragLeave
        DropGrid.Visibility = Windows.Visibility.Hidden
    End Sub
End Class
