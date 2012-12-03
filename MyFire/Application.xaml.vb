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
Imports MyFire_storage
Imports System.Windows
Imports Twitterizer
Imports System.ComponentModel
Imports System.IO

Class Application
    Dim da As New MyFire_storage.Data
    Private WithEvents _reportadvertisement As New BackgroundWorker
    Private AdvertisementTweet As Tweet
    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Private Sub Application_DispatcherUnhandledException(ByVal sender As Object, ByVal e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs) Handles Me.DispatcherUnhandledException
        Dim fe As New FatalError
        fe.ShowError(e.Exception, False)
        End
    End Sub

    Private Sub Application_Startup(ByVal sender As Object, ByVal e As System.Windows.StartupEventArgs) Handles Me.Startup
        Dim Argslist As New List(Of String)
        For Each arg In e.Args
            Argslist.Add(arg)
        Next
        For Each Item As String In Argslist
            '*
            '* ARGUMENTS LIST GOES HERE.
            '*
            '* Example: 
            '*  If Item = "{argument}" Then
            '*      Some-action
            '*  End If
            '*

            If Item = "/c" Then
                'Reset All Settings
                Dim Result As MessageBoxResult = MessageBox.Show("Are you sure you want to reset all the settings?", "myFire", MessageBoxButton.YesNo, MessageBoxImage.Question)
                If Result = MessageBoxResult.Yes Then
                    Try
                        da.Reset(My.Settings.linked_ScreenNames)
                        My.Settings.Reset()
                        My.Settings.Save()
                        WarningMessage("All settings reset.", "Notice:")
                        End
                    Catch ex As Exception
                        exceptionhandler.ExceptionMessage(ex)
                        End
                    End Try
                End If
            ElseIf Item.StartsWith("-x") Then
                Dim CurrentBGS As Boolean = My.Settings.myFire_TweetInBackground
                If CurrentBGS = True Then
                    My.Settings.myFire_TweetInBackground = False
                    My.Settings.Save()
                End If
                Dim filename As String
                filename = Item
                filename = filename.Replace("-x ", "")
                Dim StreamReader As StreamReader = New StreamReader(filename)
                Dim textfile As String = StreamReader.ReadToEnd
                Dim t As New TweetDialog
                t.Tweet_Format(textfile)
                If Not CurrentBGS = My.Settings.myFire_TweetInBackground Then
                    My.Settings.myFire_TweetInBackground = CurrentBGS
                    My.Settings.Save()
                End If
            ElseIf Item.StartsWith("-p") Then
                Dim CurrentBGS As Boolean = My.Settings.myFire_TweetInBackground
                If CurrentBGS = True Then
                    My.Settings.myFire_TweetInBackground = False
                    My.Settings.Save()
                End If
                Dim filename As String
                filename = Item
                filename = filename.Replace("-p ", "")
                Dim t As New TweetDialog
                t.Media_Format("", filename)
                If Not CurrentBGS = My.Settings.myFire_TweetInBackground Then
                    My.Settings.myFire_TweetInBackground = CurrentBGS
                    My.Settings.Save()
                End If
            ElseIf Item = "/q" Then
                Dim t As New TweetDialog
                t.ShowDialog()
                End
            End If
        Next
    End Sub

#Region "Tweet List"

#Region "Context Menu"
#Region "Tweet"
    Private Sub cm_tweet_conversation(sender As Object, e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Dim t As New myFire_ConversationView
        t.Show_Conversation(Tweet.TwitterStatusObject, tokens.GetDefaultScreenName)
    End Sub
    Private Sub cm_tweet_favourite(ByVal sender As MenuItem, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Status_Favourite(Tweet.TwitterStatusObject.Id)
            exceptionhandler.InfoMessage("Tweet Favourited!", "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_tweet_retweet(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Status_Retweet(Tweet.TwitterStatusObject.Id)
            exceptionhandler.InfoMessage("Tweet Retweeted!", "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_tweet_retweet_quote(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            Dim td As New TweetDialog
            td.Tweet_Format("RT @" & Tweet.TwitterStatusObject.User.ScreenName & ": " & Tweet.TweetText)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_tweet_reply(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            Dim td As New TweetDialog
            Dim TweetId As Decimal = Tweet.TwitterStatusObject.Id
            Dim TweetUser As String = Tweet.TweetUsername
            td.Reply_Format(TweetUser, TweetId)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_tweet_translate(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Status_Translate(Tweet.TweetText)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_tweet_delete(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Status_Delete(Tweet.TwitterStatusObject.Id)
            InfoMessage("Tweet deleted", "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub _reportadvertisement_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _reportadvertisement.DoWork
        Dim tweettoreport As Tweet = Nothing
        'Me.Dispatcher.Invoke(Sub()
        '                         tweettoreport = AdvertisementTweet
        '                     End Sub)
        'Dim SmtpServer As New SmtpClient()
        'Dim mail As New MailMessage()
        'SmtpServer.Credentials = New Net.NetworkCredential("noreply@ifxsoftware.net", "FreeFall3")
        'SmtpServer.Port = 587
        'SmtpServer.Host = "smtp.ifxsoftware.net"
        'mail = New MailMessage()
        'mail.From = New MailAddress("noreply@ifxsoftware.net")
        'mail.To.Add("support@ifxsoftware.net")
        'mail.Subject = "Advertisement tweet report"
        'Dim MessageBody As String
        'MessageBody = "myFire advertisement tweet report." & vbNewLine
        'MessageBody = MessageBody & "======================================" & vbNewLine
        'MessageBody = MessageBody & "Tweet Text: " & tweettoreport.TweetText & vbNewLine
        'MessageBody = MessageBody & "Tweet Source: " & tweettoreport.TwitterStatusObject.Source & vbNewLine
        'mail.Body = MessageBody
        'Try
        '    SmtpServer.Send(mail)
        'Catch ex As Exception
        'End Try
    End Sub

    Private Sub cm_tweet_advertisement(sender As System.Object, e As System.Windows.RoutedEventArgs)
        AdvertisementTweet = TryCast(sender.DataContext(0), Tweet)
        _reportadvertisement.RunWorkerAsync()
        InfoMessage("Thanks!", "myFire")
    End Sub
#End Region
#Region "User"
    Private Sub cm_user_follow(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Friendship_Create(Tweet.TwitterStatusObject.User.Id)
            exceptionhandler.InfoMessage("Followed @" & Tweet.TweetUsername, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_user_unfollow(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Friendship_Delete(Tweet.TwitterStatusObject.User.Id)
            exceptionhandler.InfoMessage("Unfollowed @" & Tweet.TweetUsername, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_user_view(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Dim up As New UserProfile
        Try
            up.Show_User(Tweet.TwitterStatusObject.User)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_user_search(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Dim u As New myFire_UserSearch_List
        u.Search(Tweet.TwitterStatusObject.User.ScreenName)
    End Sub
    Private Sub cm_user_block(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_Block_Create(Tweet.TwitterStatusObject.User.Id)
            exceptionhandler.InfoMessage("Blocked @" & Tweet.TweetUsername, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
    Private Sub cm_user_block_report(ByVal sender As MenuItem, ByVal e As RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_User_Report(Tweet.TwitterStatusObject.User.Id)
            exceptionhandler.InfoMessage("Reported @" & Tweet.TweetUsername, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
#End Region
#End Region

#Region "ListItem Actions"
    Private Sub li_leftclick(ByVal sender As StackPanel, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
            Dim TweetId As Decimal = Tweet.TwitterStatusObject.Id
            Dim TweetUser As String = Tweet.TweetUsername
            TweetUser = TweetUser.Replace("@", "")

            If My.Computer.Keyboard.AltKeyDown Then
                Try
                    myfireactions.Twitter_Status_Favourite(TweetId)
                    exceptionhandler.InfoMessage("Tweet Favourited!", "myFire")
                Catch ex As Exception
                    exceptionhandler.ExceptionMessage(ex)
                End Try
            ElseIf My.Computer.Keyboard.CtrlKeyDown Then
                Try
                    myfireactions.Twitter_Status_Favourite(TweetId)
                    exceptionhandler.InfoMessage("Tweet Retweeted!", "myFire")
                Catch ex As Exception
                    exceptionhandler.ExceptionMessage(ex)
                End Try
            ElseIf My.Computer.Keyboard.ShiftKeyDown Then
                Try
                    exceptionhandler.TranslateMessage(Tweet.TwitterStatusObject.Text)
                Catch ex As Exception
                    exceptionhandler.ExceptionMessage(ex)
                End Try
            Else
                Dim td As New TweetDialog
                Try
                    td.Reply_Format(TweetUser, TweetId)
                Catch ex As Exception

                End Try
            End If
        Else
            Exit Sub
        End If
    End Sub
    Private Sub li_rightclick(ByVal sender As StackPanel, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
            Try
                Dim tv As New TweetDisplayDialog
                tv.Show_Tweet(tweet.TwitterStatusObject)
            Catch ex As Exception
                exceptionhandler.ExceptionMessage(ex)
            End Try
        End If
    End Sub
    Private Sub Image1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
            Dim up As New UserProfile
            up.Show_User(tweet.TwitterStatusObject.User)
        End If
    End Sub
#End Region

    Private Sub Grid1_MouseEnter(ByVal sender As StackPanel, ByVal e As MouseEventArgs)
        Dim tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Dim t As ToolTip = TryCast(sender.ToolTip, ToolTip)
        Dim sp As StackPanel = t.Content
        Dim TB1 As TextBlock = sp.Children(2)
        Dim TB2 As TextBlock = sp.Children(0)

        If My.Computer.Keyboard.AltKeyDown = True Then
            TB1.Text = "Double click to favourite this tweet!"
        ElseIf My.Computer.Keyboard.CtrlKeyDown = True Then
            TB1.Text = "Double click to retweet this tweet!"
        ElseIf My.Computer.Keyboard.ShiftKeyDown = True Then
            TB1.Text = "Double click to translate this tweet!"
        Else
            TB1.Text = "Double click to reply to this tweet!"
        End If
        TB2.Text = "Tweet ID: " & myfireactions.Number_Converter(tweet.TwitterStatusObject.Id) & ".  Sent: " & myfireactions.DateConverter(tweet.TweetDate) & " ago"
    End Sub

    Private Sub Image1_MouseEnter(ByVal sender As Object, ByVal e As RoutedEventArgs)
        Dim tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Dim user As TwitterUser = tweet.TwitterStatusObject.User
        Dim t As ToolTip = TryCast(sender.ToolTip, ToolTip)
        Dim sp As StackPanel = t.Content
        Dim TB1 As TextBlock = sp.Children(0)
        Dim TB2 As TextBlock = sp.Children(2)
        Dim Text As String
        Text = "@" & user.ScreenName & vbNewLine
        Text = Text & user.Name & vbNewLine
        Text = Text & myfireactions.Number_Converter(user.NumberOfStatuses, "tweets.") & vbNewLine
        Text = Text & myfireactions.Number_Converter(user.NumberOfFollowers, "followers.") & vbNewLine
        Text = Text & myfireactions.Number_Converter(user.NumberOfFriends, "following.") & vbNewLine
        Text = Text & user.Description
        TB1.Text = Text
        TB2.Text = "Double click to view more about this user."
    End Sub

    Private Sub Grid_MouseEnter(sender As Grid, e As System.Windows.Input.MouseEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Dim StackPanel As StackPanel = TryCast(sender.Children(1), StackPanel)
        Dim TextBlock As TextBlock = TryCast(StackPanel.Children(2), TextBlock)
        TextBlock.Text = myfireactions.DateConverter(Tweet.TweetDate)

        If Tweet.TweetText.Contains("@" & tokens.GetDefaultScreenName) = True Then
            If Tweet.TweetText.Contains("http://") = True Then
                Dim ReportButton As Button = sender.Children(2)
                ReportButton.Visibility = Visibility.Visible
            End If
        End If
    End Sub

    Private Sub Report_User(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim Tweet As Tweet = TryCast(sender.DataContext(0), Tweet)
        Try
            myfireactions.Twitter_User_Report(Tweet.TwitterStatusObject.User.Id)
            InfoMessage("Reported @" & Tweet.TwitterStatusObject.User.ScreenName, "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub Grid_MouseLeave(sender As System.Object, e As System.Windows.Input.MouseEventArgs)
        Dim ReportButton As Button = sender.Children(2)
        ReportButton.Visibility = Visibility.Collapsed
    End Sub
#End Region

#Region "Direct Message List"

#Region "Context Menu"
    Private Sub cm_message_reply(sender As MenuItem, e As System.Windows.RoutedEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Dim td As New TweetDialog
        td.Message_Format(Message.MessageUsername)
    End Sub

    Private Sub cm_message_foward(sender As MenuItem, e As System.Windows.RoutedEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Dim td As New TweetDialog
        td.Message_Format(Nothing, Message.MessageText)
    End Sub

    Private Sub cm_message_translate(sender As MenuItem, e As System.Windows.RoutedEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        myfireactions.Twitter_Status_Translate(Message.MessageText)
    End Sub

    Private Sub cm_message_report(sender As MenuItem, e As System.Windows.RoutedEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Try
            myfireactions.Twitter_User_Report(Message.MessageUserObject.Id)
            InfoMessage("User reported.", "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub cm_message_delete(sender As MenuItem, e As System.Windows.RoutedEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Try
            myfireactions.Twitter_DirectMessage_Delete(Message.MessageId)
            InfoMessage("Message Deleted", "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub
#End Region

    Private Sub Grid_Loaded_1(sender As Grid, e As System.Windows.RoutedEventArgs)
        Dim StackPanel As StackPanel = TryCast(sender.Children(1), StackPanel)
        Dim TextBlock As TextBlock = TryCast(StackPanel.Children(1), TextBlock)
        TextBlock.FontSize = My.Settings.FontSize
    End Sub

    Private Sub Grid_MouseEnter_1(sender As Grid, e As System.Windows.Input.MouseEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Dim StackPanel As StackPanel = TryCast(sender.Children(1), StackPanel)
        Dim TextBlock As TextBlock = TryCast(StackPanel.Children(2), TextBlock)
        TextBlock.Text = myfireactions.DateConverter(Message.MessageObject.CreatedDate)
    End Sub

    Private Sub MessageImage_MouseEnter(ByVal sender As Border, ByVal e As MouseEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Dim ToolTip As ToolTip = sender.ToolTip
        Dim SP As StackPanel = ToolTip.Content
        Dim TB1 As TextBlock = SP.Children(0)
        Dim TB2 As TextBlock = SP.Children(2)
        Dim Text As String
        Text = "@" & Message.MessageObject.Sender.ScreenName & vbNewLine
        Text = Text & Message.MessageUserObject.Name & vbNewLine
        Text = Text & myfireactions.Number_Converter(Message.MessageUserObject.NumberOfStatuses, "tweets.") & vbNewLine
        Text = Text & myfireactions.Number_Converter(Message.MessageUserObject.NumberOfFollowers, "followers.") & vbNewLine
        Text = Text & myfireactions.Number_Converter(Message.MessageUserObject.NumberOfFriends, "following.") & vbNewLine
        Text = Text & Message.MessageUserObject.Description
        TB1.Text = Text
        TB2.Text = "Double click to view more about this user."
    End Sub

    Private Sub MessageImage_MouseDown(ByVal sender As Image, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim MessageUser As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
            Dim up As New UserProfile
            up.Show_User(MessageUser.MessageUserObject)
        End If
    End Sub

    Private Sub MessageStackpanel_MouseEnter(ByVal sender As StackPanel, ByVal e As MouseEventArgs)
        Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
        Dim ToolTip As ToolTip = sender.ToolTip
        Dim SP As StackPanel = ToolTip.Content
        Dim TB1 As TextBlock = SP.Children(2)
        Dim TB2 As TextBlock = SP.Children(0)
        TB1.Text = "Double click to reply to this message!"
        TB2.Text = "Message ID: " & Message.MessageId & ".  Sent at: " & myfireactions.DateConverter(Message.MessageObject.CreatedDate)
    End Sub

    Private Sub MessageStackpanel_leftclick(ByVal sender As StackPanel, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim Message As DirectMessage = TryCast(sender.DataContext(0), DirectMessage)
            Dim td As New TweetDialog
            td.Message_Format(Message.MessageUsername)
        End If
    End Sub

    Private Sub MessageStackpanel_rightclick(ByVal sender As StackPanel, ByVal e As MouseButtonEventArgs)
        '
    End Sub
#End Region

#Region "User List"
    Private Sub UserCategoryGird_MouseDown(sender As Grid, e As System.Windows.Input.MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim Category As UserCategory = TryCast(sender.DataContext(0), UserCategory)
            Dim usl As New myFire_UserSearch_List
            usl.SuggestedUsers(Category.UserCategoryObject.Slug)
        End If
    End Sub

    Private Sub Grid2_MouseDown(ByVal sender As Grid, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim User As MyFire.User = TryCast(sender.DataContext(0), MyFire.User)
            Dim up As New UserProfile
            up.Show_User(User.UserObject)
        End If
    End Sub

    Private Sub Grid2_MouseEnter(ByVal sender As Grid, ByVal e As MouseEventArgs)
        Dim Obj As Button = sender.Children.Item(2)
        Dim MfUser As MyFire.User = TryCast(sender.DataContext(0), MyFire.User)
        Dim User As TwitterUser = MfUser.UserObject
        If MfUser.UserFollowVisibility = Visibility.Visible Then
            Obj.Visibility = Visibility.Visible
        End If
        Dim tt As ToolTip = TryCast(sender.ToolTip, ToolTip)
        Dim SP As StackPanel = TryCast(tt.Content, StackPanel)
        Dim TB1 As TextBlock = SP.Children(0)
        Dim TB2 As TextBlock = SP.Children(2)
        Dim Text As String
        Text = "@" & User.ScreenName & vbNewLine
        Text = Text & User.Name & vbNewLine
        Text = Text & myfireactions.Number_Converter(User.NumberOfStatuses, "tweets.") & vbNewLine
        Text = Text & myfireactions.Number_Converter(User.NumberOfFollowers, "followers.") & vbNewLine
        Text = Text & myfireactions.Number_Converter(User.NumberOfFriends, "following.") & vbNewLine
        Text = Text & User.Description
        TB1.Text = Text
        TB2.Text = "Double click to view more about this user."
    End Sub

    Private Sub Grid2_MouseLeave(sender As Grid, e As System.Windows.Input.MouseEventArgs)
        Dim Obj As Button = sender.Children.Item(2)
        Obj.Visibility = Visibility.Hidden
    End Sub

    Private Sub fOLLOW_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim MfUser As MyFire.User = TryCast(sender.DataContext(0), MyFire.User)
        Try
            myfireactions.Twitter_Friendship_Create(MfUser.UserObject.Id)
            InfoMessage("Followed @" & MfUser.UserObject.ScreenName, "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub
#End Region

#Region "Search List"
    Private Sub SearchImage_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim tweet As SearchTweet = TryCast(sender.DataContext(0), SearchTweet)
            Dim up As New UserProfile
            up.Show_User(tweet.SearchUsername)
        End If
    End Sub

    Private Sub SearchSP_LeftClick(ByVal sender As StackPanel, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim Tweet As SearchTweet = TryCast(sender.DataContext(0), SearchTweet)
            Dim TweetId As Decimal = Tweet.TweetSearchObject.Id
            If My.Computer.Keyboard.AltKeyDown Then
                Try
                    myfireactions.Twitter_Status_Favourite(TweetId)
                    exceptionhandler.InfoMessage("Tweet Favourited!", "myFire")
                Catch ex As Exception
                    exceptionhandler.ExceptionMessage(ex)
                End Try
            ElseIf My.Computer.Keyboard.CtrlKeyDown Then
                Try
                    myfireactions.Twitter_Status_Retweet(TweetId)
                    exceptionhandler.InfoMessage("Tweet Retweeted!", "myFire")
                Catch ex As Exception
                    exceptionhandler.ExceptionMessage(ex)
                End Try
            ElseIf My.Computer.Keyboard.ShiftKeyDown Then
                Try
                    exceptionhandler.TranslateMessage(Tweet.TweetSearchObject.Text)
                Catch ex As Exception
                    exceptionhandler.ExceptionMessage(ex)
                End Try
            Else
                Dim td As New TweetDialog
                td.Reply_Format(Tweet.SearchUsername, Tweet.TweetSearchObject.Id)
            End If
        End If
    End Sub

    Private Sub SearchSP_RightClick(ByVal sender As StackPanel, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim Tweet As SearchTweet = TryCast(sender.DataContext(0), SearchTweet)
            Dim td As New TweetDisplayDialog
            td.Show_Tweet(Tweet.TweetSearchObject.Id)
        End If
    End Sub

    Private Sub SearchSP_MouseEnter(ByVal sender As StackPanel, ByVal e As MouseEventArgs)
        Dim tweet As SearchTweet = TryCast(sender.DataContext(0), SearchTweet)
        Dim t As ToolTip = TryCast(sender.ToolTip, ToolTip)
        Dim sp As StackPanel = t.Content
        Dim TB1 As TextBlock = sp.Children(2)
        Dim TB2 As TextBlock = sp.Children(0)

        If My.Computer.Keyboard.AltKeyDown = True Then
            TB1.Text = "Double click to favourite this tweet!"
        ElseIf My.Computer.Keyboard.CtrlKeyDown = True Then
            TB1.Text = "Double click to retweet this tweet!"
        ElseIf My.Computer.Keyboard.ShiftKeyDown = True Then
            TB1.Text = "Double click to translate this tweet!"
        Else
            TB1.Text = "Double click to reply to this tweet!"
        End If
        TB2.Text = "Tweet ID: " & tweet.TweetSearchObject.Id & ".  Sent at: " & myfireactions.DateConverter(tweet.TweetSearchObject.CreatedDate)
    End Sub
#End Region

#Region "TwitterList List"
#Region "Context Menu"
    Private Sub Grid3_cm_1(ByVal sender As MenuItem, ByVal e As RoutedEventArgs)
        Dim mList As mTwitterList = TryCast(sender.DataContext(0), mTwitterList)
        Dim List As TwitterList = mList.TwitterListObject
        Dim ed As New myFire_EditList
        ed.EditList(List)
    End Sub
    Private Sub Grid3_cm_2(ByVal sender As MenuItem, ByVal e As RoutedEventArgs)
        Dim mList As mTwitterList = TryCast(sender.DataContext(0), mTwitterList)
        Dim List As TwitterList = mList.TwitterListObject
        Dim ed As New myFire_EditListMembers
        ed.EditListMembers(List)
    End Sub
    Private Sub Grid3_cm_3(ByVal sender As MenuItem, ByVal e As RoutedEventArgs)
        Dim mList As mTwitterList = TryCast(sender.DataContext(0), mTwitterList)
        Dim List As TwitterList = mList.TwitterListObject
        Try
            myfireactions.Twitter_List_Destroy(List.Id)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        exceptionhandler.InfoMessage("List Deleted.", "myFire")
    End Sub
#End Region
    Private Sub Grid3_MouseDown(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.ClickCount = 2 Then
            Dim ListContext As mTwitterList = TryCast(sender.DataContext(0), mTwitterList)
            Dim List As TwitterList = ListContext.TwitterListObject
            Dim TL As New myFire_TweetList
            TL.List_Tweets(List.Id)
        End If
    End Sub

    Private Sub Grid3_MouseEnter(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim ListContext As mTwitterList = TryCast(sender.DataContext(0), mTwitterList)
        Dim List As TwitterList = ListContext.TwitterListObject
        Dim ToolTip As ToolTip = TryCast(sender.ToolTip, ToolTip)
        Dim SP As StackPanel = TryCast(ToolTip.Content, StackPanel)
        Dim TextBlock1 As TextBlock = SP.Children(0)
        TextBlock1.Text = "List ID: " & List.Id
    End Sub
#End Region
End Class
