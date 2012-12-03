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
Imports System.ComponentModel
Imports System.Net.NetworkInformation
Imports System.Windows.Threading
Imports Microsoft.Win32
Imports MyFire_storage
Imports System.Diagnostics


Class MainWindow

#Region "Declarations"
    Dim da As New MyFire_storage.Data
    Dim unread_dm As Boolean = False
    Private tooltip_Streaming As Boolean
    Dim CurrentUser As TwitterUser
    Private ConversationList As New List(Of String)
    Private StreamingError As Streaming.StopReasons
    Dim UnreadTweetsCount As Integer = 0
    Private SelectedList As mTwitterList

#Region "Background Workers"
    Friend WithEvents _timelineworker As New BackgroundWorker
    Friend WithEvents _mentionsworker As New BackgroundWorker
    Friend WithEvents _messagesworker As New BackgroundWorker
    Friend WithEvents _favouriteworker As New BackgroundWorker
    Friend WithEvents _retweetsworker As New BackgroundWorker
    Friend WithEvents _styleworker As New BackgroundWorker
    Friend WithEvents _toptweetsworker As New BackgroundWorker
    Friend WithEvents _updateworker As New BackgroundWorker
    Friend WithEvents _listsworker As New BackgroundWorker
    Friend WithEvents _listtweetworker As New BackgroundWorker
#End Region

#End Region

#Region "Get"

#Region "Timeline"
    Private Sub _timelineworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _timelineworker.DoWork
        Dim t As IList(Of Tweet)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In myfireactions.Twitter_Timeline_Home
                                     Try
                                         If myfireactions.Twitter_Status_FilterTweet(Tweet) = True Then
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             TMList.Items.Add(t)
                                         Else
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             FTList.Items.Add(t)
                                         End If
                                     Catch ex As Exception
                                         exceptionhandler.ExceptionMessage(ex)
                                     End Try
                                 Next

                                 If TMList.Items.Count = 0 Then
                                     Nothing_Tweets.Visibility = Windows.Visibility.Visible
                                 Else
                                     Nothing_Tweets.Visibility = Windows.Visibility.Collapsed
                                 End If
                             End Sub)
        'Dim t As Tweet
        'Dim g_tweets As New List(Of Tweet)
        'Dim StatusList As TwitterStatusCollection
        'Try
        '    StatusList = myfireactions.Twitter_Timeline_Home
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString)
        '    Exit Sub
        'End Try
        'For Each Tweet As TwitterStatus In StatusList
        '    If myfireactions.Twitter_Status_FilterTweet(Tweet) = True Then
        '        t = New Tweet() With {.TwitterStatusObject = Tweet}
        '        g_tweets.Add(t)
        '    End If
        'Next

        'Me.Dispatcher.Invoke(Sub()
        '                         For Each gt As Tweet In g_tweets
        '                             TMList.Items.Add(gt)
        '                         Next
        '                     End Sub)
    End Sub

    Private Sub _timelineworker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _timelineworker.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Get_Timeline()
        Me.Cursor = Cursors.AppStarting
        _timelineworker.RunWorkerAsync()
    End Sub
#End Region

#Region "Mentions"
    Private Sub _mentionsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _mentionsworker.DoWork
        Dim t As IList(Of Tweet)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In myfireactions.Twitter_Timeline_Mentions
                                     Try
                                         If myfireactions.Twitter_Status_FilterTweet(Tweet) = True Then
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             MEList.Items.Add(t)
                                         Else
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             FTList.Items.Add(t)
                                         End If
                                     Catch ex As Exception
                                         exceptionhandler.ExceptionMessage(ex)
                                     End Try
                                 Next

                                 If MEList.Items.Count = 0 Then
                                     Nothing_Mentions.Visibility = Windows.Visibility.Visible
                                 Else
                                     Nothing_Mentions.Visibility = Windows.Visibility.Collapsed
                                 End If
                             End Sub)
    End Sub

    Private Sub _mentionsworker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _mentionsworker.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Get_Mentions()
        Me.Cursor = Cursors.AppStarting
        _mentionsworker.RunWorkerAsync()
    End Sub
#End Region

#Region "Lists"
    Private Sub _listsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _listsworker.DoWork
        Dim t As New List(Of mTwitterList)
        Me.Dispatcher.Invoke(Sub()
                                 For Each List As TwitterList In myfireactions.Twitter_List_Get_All
                                     Try
                                         t = New List(Of mTwitterList)() From {New mTwitterList() With {.TwitterListObject = List}}
                                         List_ListsList.Items.Add(t)
                                     Catch ex As Exception
                                         exceptionhandler.ExceptionMessage(ex)
                                     End Try
                                 Next
                             End Sub)
    End Sub

    Private Sub _listtweetworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _listtweetworker.DoWork
        Dim t As New List(Of Tweet)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In myfireactions.Twitter_List_Tweets(SelectedList.TwitterListObject.Id, tokens.GetDefaultScreenName)
                                     Try
                                         t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                         List_TweetsList.Items.Add(t)
                                     Catch ex As Exception
                                         exceptionhandler.ExceptionMessage(ex)
                                     End Try
                                 Next
                             End Sub)
    End Sub

    Private Sub Get_Lists()
        _listsworker.RunWorkerAsync()
    End Sub

    Private Sub Get_List_Tweets()
        _listtweetworker.RunWorkerAsync()
    End Sub
#End Region

#Region "Messages"
    Private Sub _messagesworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _messagesworker.DoWork
        Dim t As IList(Of DirectMessage)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Message As TwitterDirectMessage In myfireactions.Twitter_DirectMessage_Get
                                     Try
                                         t = New List(Of DirectMessage)() From {New DirectMessage() With {.MessageObject = Message}}
                                         DMList.Items.Add(t)
                                     Catch ex As Exception
                                         exceptionhandler.ExceptionMessage(ex)
                                     End Try
                                 Next

                                 If DMList.Items.Count = 0 Then
                                     Nothing_Messages.Visibility = Windows.Visibility.Visible
                                 Else
                                     Nothing_Messages.Visibility = Windows.Visibility.Collapsed
                                 End If
                             End Sub)
    End Sub

    Private Sub _messagesworker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _messagesworker.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Get_Messages()
        Me.Cursor = Cursors.AppStarting
        _messagesworker.RunWorkerAsync()
    End Sub
#End Region

#Region "Favourites"
    Private Sub _favouriteworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _favouriteworker.DoWork
        Dim t As IList(Of Tweet)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In myfireactions.Twitter_Favourites(CurrentUser.Id)
                                     Try
                                         If myfireactions.FormatTweet(Tweet) = True Then
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             FAVList.Items.Add(t)
                                         Else
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             FTList.Items.Add(t)
                                         End If
                                     Catch ex As Exception
                                         exceptionhandler.ExceptionMessage(ex)
                                     End Try
                                 Next

                                 If FAVList.Items.Count = 0 Then
                                     Nothing_Favourites.Visibility = Windows.Visibility.Visible
                                 Else
                                     Nothing_Favourites.Visibility = Windows.Visibility.Collapsed
                                 End If
                             End Sub)
    End Sub

    Private Sub _favouriteworker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _favouriteworker.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Get_Favourites()
        Me.Cursor = Cursors.AppStarting
        _favouriteworker.RunWorkerAsync()
    End Sub
#End Region

#Region "Retweets"
    Private Sub _retweetsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _retweetsworker.DoWork
        Dim TweetList As TwitterStatusCollection = myfireactions.Twitter_Timeline_Retweets_ByOthers
        Dim t As IList(Of Tweet)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In TweetList
                                     Try
                                         If myfireactions.FormatTweet(Tweet.RetweetedStatus) Then
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             RTList.Items.Add(t)
                                         Else
                                             t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                             FTList.Items.Add(t)
                                         End If
                                     Catch ex As Exception
                                         ExceptionMessage(ex)
                                     End Try
                                 Next

                                 If RTList.Items.Count = 0 Then
                                     Nothing_Retweets.Visibility = Windows.Visibility.Visible
                                 Else
                                     Nothing_Retweets.Visibility = Windows.Visibility.Collapsed
                                 End If
                             End Sub)
    End Sub

    Private Sub _retweetsworker_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _retweetsworker.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub Get_Retweets()
        Me.Cursor = Cursors.AppStarting
        _retweetsworker.RunWorkerAsync()
    End Sub
#End Region

#Region "Trending Topics"
    Private Sub _toptweetsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _toptweetsworker.DoWork
        'Dim t As IList(Of Tweet)
        'Me.Dispatcher.Invoke(Sub()
        '                         For Each Tweet As TwitterStatus In myfireactions.Twitter_Explore_TopTweets(15)
        '                             Try
        '                                 If myfireactions.Twitter_Status_FilterTweet(Tweet) = True Then
        '                                     t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
        '                                     TopList.Items.Add(t)
        '                                 End If
        '                             Catch ex As Exception
        '                                 exceptionhandler.ExceptionMessage(ex)
        '                             End Try
        '                         Next
        '                     End Sub)


        'Me.Dispatcher.Invoke(Sub()
        '                         For Each Trend As TwitterTrend In myfireactions.Twitter_Trends(1)
        '                             TrendsList.Items.Add(Trend.Name)
        '                         Next
        '                     End Sub)
    End Sub
#End Region

#End Region

#Region "Context Menu Methods"
    Private Sub cm0(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Process.Start("http://myfire.uservoice.com/")
    End Sub

    Sub cm1()
        Dim us As New UserSearch
        Dim UserName As String
        UserName = us.SearchUser
        If UserName = "" Then
            Exit Sub
        End If
        Try
            myfireactions.Twitter_Friendship_Create(UserName)
            exceptionhandler.InfoMessage("Followed: @" & UserName, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Sub cm2()
        Dim s As New Settings
        s.ShowDialog()
    End Sub

    Sub cm3()
        Dim Userprofile As New UserProfile
        Dim UserSearch As New UserSearch
        Dim search As String = UserSearch.SearchUser
        If search = "" Then
            Exit Sub
        End If
        Userprofile.Show_User(search)
    End Sub

    Sub cm8()
        Dim Userresposne As TwitterResponse(Of TwitterUser) = TwitterUser.Show(tokens.Tokens, tokens.GetDefaultScreenName)
        Dim Tweetresponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.Delete(tokens.Tokens, Userresposne.ResponseObject.Status.Id)
        If Tweetresponse.Result <> RequestResult.Success Then
            exceptionhandler.ExceptionMessage(New Exception(Tweetresponse.ErrorMessage))
        Else
            exceptionhandler.InfoMessage("Tweet Deleted!", "myFire")
        End If
    End Sub

    Sub cmS()
        Dim s As New Support
        s.ShowDialog()
    End Sub

    Sub cm10()
        Dim us As New myFire_UserManagment
        us.ShowDialog()
        ac_user_list.Items.Clear()
        For Each ScreenName As String In My.Settings.linked_ScreenNames
            ac_user_list.Items.Add(ScreenName)
        Next
    End Sub

    Sub tb1()
        Dim f As New myFire_FilterTweets
        f.ShowDialog()
    End Sub

    Sub tb2()
        TerminateStream()
    End Sub
#End Region

#Region "Tabs"

    Private Sub TabControl1_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles TabControl1.SelectionChanged
        'Mentions
        If TabControl1.SelectedIndex = 1 Then
            If MEList.Items.Count = 0 Then
                Get_Mentions()
            End If
        End If

        'Lists
        If TabControl1.SelectedIndex = 2 Then
            If List_ListsList.Items.Count = 0 Then
                Get_Lists()
            End If
        End If

        'Direct Messages
        If TabControl1.SelectedIndex = 3 Then
            If DMList.Items.Count = 0 Then
                Get_Messages()
            End If
        End If

        'Favourites
        If TabControl1.SelectedIndex = 4 Then
            If FAVList.Items.Count = 0 Then
                Get_Favourites()
            End If
        End If

        'Retweets
        If TabControl1.SelectedIndex = 6 Then
            If RTList.Items.Count = 0 Then
                Get_Retweets()
            End If
        End If

        'Filters
        If TabControl1.SelectedIndex = 7 Then
            If FTList.Items.Count = 0 Then
                If Nothing_Filter.Visibility = Windows.Visibility.Collapsed Then
                    Nothing_Filter.Visibility = Windows.Visibility.Visible
                End If
            Else
                If Nothing_Filter.Visibility = Windows.Visibility.Visible Then
                    Nothing_Filter.Visibility = Windows.Visibility.Collapsed
                End If
            End If
        End If
    End Sub

    Sub HideTabsSettings()
        If My.Settings.tab_timeline = False Then
            TimelineTab.Visibility = Windows.Visibility.Collapsed
        End If
        If My.Settings.tab_mentions = False Then
            MentionsTab.Visibility = Windows.Visibility.Collapsed
        End If
        If My.Settings.tab_messages = False Then
            MessagesTab.Visibility = Windows.Visibility.Collapsed
        End If
        If My.Settings.tab_favourites = False Then
            FavouritesTab.Visibility = Windows.Visibility.Collapsed
        End If
        If My.Settings.tab_search = False Then
            SearchTab.Visibility = Windows.Visibility.Collapsed
        End If
        If My.Settings.tab_ban = False Then
            BanTab.Visibility = Windows.Visibility.Collapsed
        End If
    End Sub

#Region "Timeline"
    Private Sub tabimg_home_MouseEnter(sender As System.Object, e As System.Windows.Input.MouseEventArgs) Handles tabimg_home.MouseEnter
        Dim tooltip As ToolTip = sender.ToolTip
        Dim sp As StackPanel = TryCast(tooltip.Content, StackPanel)
        Dim TB2 As TextBlock = sp.Children(2)
        If tooltip_Streaming = True Then
            TB2.Text = "Streaming active."
        Else
            TB2.Text = "Streaming interrupted."
        End If
    End Sub
#End Region

#Region "Mentions"
    Private Sub tabimg_connect_MouseEnter(sender As System.Object, e As System.Windows.Input.MouseEventArgs) Handles tabimg_connect.MouseEnter
        Dim tooltip As ToolTip = sender.ToolTip
        Dim sp As StackPanel = TryCast(tooltip.Content, StackPanel)
        Dim TB2 As TextBlock = sp.Children(2)
        If tooltip_Streaming = True Then
            TB2.Text = "Streaming active."
        Else
            TB2.Text = "Streaming interrupted."
        End If
    End Sub
#End Region

#Region "Direct Messages"
    Private Sub tabimg_message_MouseEnter(sender As System.Object, e As System.Windows.Input.MouseEventArgs) Handles tabimg_messages.MouseEnter
        Dim tooltip As ToolTip = sender.ToolTip
        Dim sp As StackPanel = TryCast(tooltip.Content, StackPanel)
        Dim TB2 As TextBlock = sp.Children(2)
        If tooltip_Streaming = True Then
            TB2.Text = "Streaming active."
        Else
            TB2.Text = "Streaming interrupted."
        End If
    End Sub
#End Region

#Region "Search"
    'Private Sub search_suggestedusers_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles search_suggestedusers.MouseDown
    '    Dim sg As New myFire_suggestedusers
    '    sg.ShowDialog()
    'End Sub

    'Private Sub TrendsList_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
    '    Dim st As New myFire_TweetSearchList
    '    st.Search(TrendsList.SelectedItem)
    'End Sub

    'Private Sub search_Tweets_KeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles search_Tweets.KeyDown
    '    If e.Key = Key.Return Or e.Key = Key.Enter Then
    '        Dim st As New myFire_TweetSearchList
    '        st.Search(search_Tweets.Text)
    '    End If
    'End Sub

    'Private Sub search_Users_KeyDown(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles search_Users.KeyDown
    '    If e.Key = Key.Return Or e.Key = Key.Enter Then
    '        Dim ut As New myFire_UserSearch_List
    '        ut.Search(search_Users.Text)
    '    End If
    'End Sub

    Private Sub SearchTwitter_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles SearchTwitter.TextChanged
        If SearchTwitter.Text <> "Search Twitter." And SearchTwitter.Text.Length <> 0 Then
            search_input.Visibility = Windows.Visibility.Visible
            search_tb_users.Text = "Users containing " & SearchTwitter.Text
            search_tb_directuser.Text = "Go to @" & SearchTwitter.Text
            search_tb_tweets.Text = "Tweets containing " & SearchTwitter.Text

            If SearchTwitter.Text.Contains(" ") = True Then
                DirectUserGrid.Visibility = Windows.Visibility.Collapsed
            Else
                DirectUserGrid.Visibility = Windows.Visibility.Visible
            End If
        Else
            Try
                search_input.Visibility = Windows.Visibility.Collapsed
            Catch
            End Try
        End If
    End Sub

    Private Sub SearchTwitter_GotFocus(sender As Object, e As System.Windows.RoutedEventArgs) Handles SearchTwitter.GotFocus
        If SearchTwitter.Text = "Search Twitter." Then
            SearchTwitter.Text = ""
        End If
    End Sub

    Private Sub SearchTwitter_LostFocus(sender As Object, e As System.Windows.RoutedEventArgs) Handles SearchTwitter.LostFocus
        If SearchTwitter.Text = "" Then
            SearchTwitter.Text = "Search Twitter."
        End If
    End Sub

    Private Sub search_tb_users_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles search_tb_users.MouseDown
        Dim u As New myFire_UserSearch_List
        u.Search(SearchTwitter.Text)
    End Sub

    Private Sub search_tb_directuser_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles search_tb_directuser.MouseDown
        Dim u As New UserProfile
        u.Show_User(SearchTwitter.Text)
    End Sub

    Private Sub search_tb_tweets_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles search_tb_tweets.MouseDown
        Dim u As New myFire_TweetSearchList
        u.Search(SearchTwitter.Text)
    End Sub
#End Region

#Region "Account"

    Private Sub me_about_me_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles me_about_me.MouseDown
        Dim up As New UserProfile
        up.Show_User(CurrentUser)
    End Sub

    Private Sub me_about_directmessages_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles me_about_directmessages.MouseDown
        If unread_dm = True Then
            unread_dm = False
            me_dm_unread_ind.Visibility = Windows.Visibility.Hidden
        End If
        Dim dm As New myFire_direct_messages
        dm.ShowDialog()
    End Sub

    Private Sub me_switch_grid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles me_switch_grid.MouseDown
        If ac_user_list.Items.Count = 0 Then
            For Each ScreenName As String In My.Settings.linked_ScreenNames
                ac_user_list.Items.Add(ScreenName)
            Next
        End If
        Fade_Rectangle.Visibility = Windows.Visibility.Visible
        UsersGrid.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub me_about_grid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles me_about_grid.MouseDown
        Dim about As New myFire_About
        about.ShowDialog()
    End Sub

    Private Sub me_about_lists_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles me_about_lists.MouseDown
        Dim l As New myFire_Lists
        l.ShowDialog()
    End Sub

    Private Sub me_settings_grid_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles me_settings_grid.MouseDown
        Dim s As New Settings
        s.ShowDialog()
    End Sub
#End Region

#End Region

#Region "Window Actions"

    Private Sub MainWindow_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        UnreadTweetsCount = 0
        Me.Title = "myFire | " & "@" & tokens.GetDefaultScreenName
    End Sub

    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim d As New MyFire_storage.Data
        If d.Users_ListOfScreenNames.Count = 0 Then
            Dim ws As New myFire_WelcomeScreen
            ws.LinkAccount()
        End If
        _styleworker.RunWorkerAsync()
        myfireactions.Notification_CreateIcon()
        If My.Settings.api_timeline = True Then
            Get_Timeline()
        End If
        TimelineTab.Focus()
        LoadAnalytics()
        streamingmanager.InitiateStream(tokens.GetDefaultScreenName)
        _updateworker.RunWorkerAsync()
        If My.Settings.myFire_LoadCount = 5 Then
            Dim s As New myFire_supportmyfire
            s.ShowDialog()
        End If
        My.Settings.myFire_LoadCount += 1
        My.Settings.Save()
    End Sub

    Private Sub MainWindow_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        My.Settings.last_load = Today
        My.Settings.Save()
        If My.Settings.myFire_CloseToTray = True Then
            e.Cancel = True
            Me.WindowState = Windows.WindowState.Minimized
        Else
            myfireactions.Notification_DestroyIcon()
            End
        End If
    End Sub

    Private Sub MainWindow_KeyUp(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles Me.KeyUp
        If TabControl1.SelectedIndex = 4 Then
            Exit Sub
        End If
        If e.Key = Key.T Then
            Dim t As New TweetDialog
            t.ShowDialog()
        End If
        If e.Key = Key.D Then
            Dim t As New TweetDialog
            t.Message_Format()
        End If
    End Sub

    Private Sub LoadAnalytics()
        AnalyticsBrowser.Navigate("http://analytics.ifxsoftware.net/" & My.Application.Info.Version.ToString & ".html")
    End Sub

#Region "Windows 7 Taskbar Methods"
    Private Sub ThumbButtonInfo0_Click(sender As System.Object, e As System.EventArgs) Handles ThumbButtonInfo0.Click
        Dim t As New TweetDialog
        t.Message_Format()
    End Sub

    Sub tbb1() Handles ThumbButtonInfo1.Click
        Dim td As New TweetDialog
        td.ShowDialog()
    End Sub

    Sub tbb2() Handles ThumbButtonInfo2.Click
        Dim Userprofile As New UserProfile
        Dim UserSearch As New UserSearch
        Dim search As String = UserSearch.SearchUser
        If search = "" Then
            Exit Sub
        End If
        Userprofile.Show_User(search)
    End Sub

    Sub tbb3() Handles ThumbButtonInfo3.Click
        Dim us As New UserSearch
        Dim UserName As String
        UserName = us.SearchUser
        If UserName = "" Then
            Exit Sub
        End If
        Try
            myfireactions.Twitter_Friendship_Create(UserName)
            exceptionhandler.InfoMessage("Followed: @" & UserName, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub
#End Region

#Region "Style Worker"
    Private Sub _styleworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _styleworker.DoWork
        Dim MyInfo As TwitterResponse(Of TwitterUser) = TwitterUser.Show(tokens.Tokens, tokens.GetDefaultScreenName)
        Me.Dispatcher.Invoke(Sub()
                                 ' Styles
                                 CurrentUser = MyInfo.ResponseObject
                                 Try
                                     Dim Username As String = MyInfo.ResponseObject.ScreenName
                                     If Username.ToLower.EndsWith("s") Then
                                         Username = Username & "'"
                                     Else
                                         Username = Username & "'s"
                                     End If
                                     Welcome_Label.Text = Window_Title_Format(tokens.GetDefaultScreenName)
                                     Me.Title = "myFire | " & "@" & tokens.GetDefaultScreenName
                                     If My.Settings.design_twitteruserbackground = True Then
                                         ContentBackground.ImageSource = myfireactions.Image_Format(MyInfo.ResponseObject.ProfileBackgroundImageLocation)
                                         CopyrightInfo.Visibility = Windows.Visibility.Collapsed
                                     Else
                                         Dim d As New ImageOfTheDay
                                         ContentBackground.ImageSource = myfireactions.Image_Format(d.ImageURL.ToString)
                                         CopyrightInfo.Visibility = Windows.Visibility.Visible
                                         CopyrightInfo.ToolTip = d.Copyright
                                     End If
                                     HideTabsSettings()

                                     'Me Tab
                                     me_label_screenname.Text = MyInfo.ResponseObject.ScreenName
                                     me_ProfileImagea.Background = New ImageBrush(myfireactions.Image_Format(MyInfo.ResponseObject.ProfileImageLocation))
                                 Catch ex As Exception
                                     exceptionhandler.ExceptionMessage(ex)
                                 End Try
                             End Sub)
    End Sub
#End Region
#End Region

#Region "Streaming Methods"
    Private Sub UpdateTitle()
        If Me.IsActive = False Then
            UnreadTweetsCount += 1
            Me.Title = "(" & UnreadTweetsCount & ") myFire | " & "@" & tokens.GetDefaultScreenName
        End If
    End Sub

    Public Sub Conversation_Add(ByVal ScreenName As String)
        Try
            ConversationList.Add(ScreenName)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Conversation_Remove(ByVal ScreenName As String)
        Try
            ConversationList.Remove(ScreenName)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Conversation_SendToWindow(ByVal Tweet As TwitterStatus)
        Dim t As myFire_ConversationView
        For Each ConversationWindow As Window In Application.Current.Windows.OfType(Of myFire_ConversationView)()
            t = ConversationWindow
            If t.Reciever.ScreenName = Tweet.User.ScreenName Then
                t.AddStatus(Tweet)
            End If
        Next
    End Sub
    Public Sub streaming_AddTimelineItem(ByVal Tweet As TwitterStatus)
        Me.Dispatcher.Invoke(Sub()
                                 Dim t As IList(Of Tweet)
                                 t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                 TMList.Items.Insert(0, t)
                                 UpdateTitle()
                             End Sub)
    End Sub

    Public Sub streaming_AddMentionsItem(ByVal Tweet As TwitterStatus)
        Me.Dispatcher.Invoke(Sub()
                                 Dim t As IList(Of Tweet)
                                 t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                                 MEList.Items.Insert(0, t)

                                 If ConversationList.Contains(Tweet.User.ScreenName) Then
                                     Conversation_SendToWindow(Tweet)
                                 End If
                             End Sub)
    End Sub

    Public Sub streaming_AddMessageItem(ByVal Message As TwitterDirectMessage)
        Me.Dispatcher.Invoke(Sub()
                                 Dim t As IList(Of DirectMessage)
                                 t = New List(Of DirectMessage)() From {New DirectMessage() With {.MessageObject = Message}}
                                 DMList.Items.Insert(0, t)
                                 unread_dm = True
                                 me_dm_unread_ind.Visibility = Windows.Visibility.Visible
                             End Sub)
    End Sub

    Public Sub streaming_RemoteTweet(ByVal Id As Decimal)

    End Sub

    Public Sub streaming_RemoveMessage(ByVal Id As Decimal)

    End Sub

    Public Sub streaming_notification_status(ByVal Tweet As TwitterStatus)
        Me.Dispatcher.Invoke(Sub()
                                 myfireactions.Notification_Show_Status(Tweet)
                             End Sub)
    End Sub

    Public Sub streaming_notification_message(ByVal Message As TwitterDirectMessage)
        Me.Dispatcher.Invoke(Sub()
                                 myfireactions.Notification_Show_Message(Message)
                             End Sub)
    End Sub

    Public Sub streaming_Started()
        tooltip_Streaming = True
        Me.Dispatcher.Invoke(Sub()
                                 topmenu_restart.Visibility = Windows.Visibility.Hidden
                             End Sub)
    End Sub

    Public Sub streaming_Stopped(ByVal Reason As Streaming.StopReasons)
        tooltip_Streaming = False
        Me.Dispatcher.Invoke(Sub()
                                 myfireactions.Notification_Show_Test("Streaming offline.", Reason.ToString, Forms.ToolTipIcon.Warning)
                                 StreamingError = Reason
                                 topmenu_restart.Visibility = Windows.Visibility.Visible
                             End Sub)
    End Sub
#End Region

#Region "Check For Updates"
    Private Sub _updateworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _updateworker.DoWork
        Dim Version As String = My.Application.Info.Version.Major & My.Application.Info.Version.Minor & My.Application.Info.Version.Build & My.Application.Info.Version.Revision
        Dim result As String = ""
        Try
            result = myfireactions.GetUpdateResult(Version)
        Catch ex As Exception
            Me.Dispatcher.Invoke(Sub()
                                     exceptionhandler.ExceptionMessage(ex)
                                 End Sub)
        End Try
        If result = "nothing" Then
            'do noting
        ElseIf result = "update" Then
            Me.Dispatcher.Invoke(Sub()
                                     If MessageBox.Show("An update is available for myFire.  Do you want to download it now?", "myFire", MessageBoxButton.YesNo, MessageBoxImage.Question) = MessageBoxResult.Yes Then
                                         Process.Start("http://code.google.com/p/project-myfire/wiki/DownloadsPage?tm=2")
                                     End If
                                 End Sub)
        ElseIf result = "dead" Then
            Me.Dispatcher.Invoke(Sub()
                                     MessageBox.Show("myFire cannot start due to an unknown error. #0x000a1", "myFire", MessageBoxButton.OK, MessageBoxImage.Error)
                                     End
                                 End Sub)
        ElseIf result = "private beta" Then
            Me.Dispatcher.Invoke(Sub()
                                     DevButton.Visibility = Windows.Visibility.Visible
                                     BetaTag.Visibility = Windows.Visibility.Visible
                                 End Sub)
        ElseIf result = "public beta" Then
            Me.Dispatcher.Invoke(Sub()
                                     BetaTag.Visibility = Windows.Visibility.Visible
                                 End Sub)
        End If

        For Each SpamSource As String In myfireactions.GetAdvertisementSources
            If My.Settings.Spam_KnownAds.Contains(SpamSource) = False Then
                My.Settings.Spam_KnownAds.Add(SpamSource)
            End If
        Next
        My.Settings.Save()
    End Sub
#End Region

#Region "Switch Accounts"
    Private Sub topmenu_account_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles topmenu_account.Click
        If ac_user_list.Items.Count = 0 Then
            Dim t As IList(Of UserStoreObject)
            For Each User As StoredUser In da.Users_ReturnUserList
                t = New List(Of UserStoreObject)() From {New UserStoreObject() With {.StoredUserObject = User}}
                ac_user_list.Items.Add(t)
            Next
        End If
        Fade_Rectangle.Visibility = Windows.Visibility.Visible
        UsersGrid.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub Fade_Rectangle_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles Fade_Rectangle.MouseDown
        Fade_Rectangle.Visibility = Windows.Visibility.Hidden
        UsersGrid.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub ac_user_list_MouseDoubleClick(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles ac_user_list.MouseDoubleClick
        Dim selecteduser As UserStoreObject = TryCast(sender.SelectedItem(0), UserStoreObject)
        SwitchAccounts(selecteduser.StoredUserObject)
        Fade_Rectangle.Visibility = Windows.Visibility.Hidden
        UsersGrid.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        MenuCM.IsOpen = True
    End Sub

    ''' <summary>
    ''' Changes the UI to match the new User
    ''' </summary>
    Public Sub SwitchAccounts(ByVal DesiredUser As MyFire_storage.StoredUser)

        ' TODO: Update Account Switching. '

        Throw New NotImplementedException("Feature needs to be updated.")
        'My.Settings.myFire_PrimaryAccount = DesiredUser.ScreenName
        'My.Settings.Save()
        'streamingmanager.TerminateStream()
        'streamingmanager.InitiateStream(DesiredUser.ScreenName)
        'TMList.Items.Clear()
        'MEList.Items.Clear()
        'DMList.Items.Clear()
        'FAVList.Items.Clear()
        'RTList.Items.Clear()
        'FTList.Items.Clear()
        '_styleworker.RunWorkerAsync()
        'Get_Timeline()
        'TimelineTab.Focus()
    End Sub
#End Region

    Private Sub MainWindow_Unloaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Unloaded
        myfireactions.Notification_DestroyIcon()
    End Sub

    Private Sub DevButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles DevButton.Click
        'Dim t As New TweetDisplayDialog
        't.Show_Tweet(250116825554251776)

        If myfireactions.IsSiteSafe(InputBox("enter url")) = False Then
            MessageBox.Show("Site is not safe.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
        Else
            MessageBox.Show("Site is safe.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
        End If
    End Sub

    Private Sub topmenu_compose_MouseUp(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles topmenu_compose.MouseUp
        Dim td As New TweetDialog
        td.ShowDialog()
    End Sub

    Private Sub topmenu_compose_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles topmenu_compose.Click
        Dim td As New TweetDialog
        td.ShowDialog()
    End Sub

    Private Sub topmenu_restart_Click(sender As Object, e As RoutedEventArgs) Handles topmenu_restart.Click
        If MessageBox.Show("myFire cannot connect to Twitter Streaming.  The error was: " & StreamingError.ToString & ".  Would you like to try to connect again?", "Streaming Error.", MessageBoxButton.YesNo, MessageBoxImage.Error) = MessageBoxResult.Yes Then
            Try
                InitiateStream(tokens.GetDefaultScreenName)
            Catch ex As Exception
                exceptionhandler.ExceptionMessage(ex)
            End Try
        End If
    End Sub

    Private Sub List_ListsList_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles List_ListsList.SelectionChanged
        SelectedList = TryCast(sender.SelectedItem(0), mTwitterList)
        List_TweetsList.Items.Clear()
        Get_List_Tweets()
        List_SelectionNotice.Visibility = Windows.Visibility.Collapsed
    End Sub
End Class