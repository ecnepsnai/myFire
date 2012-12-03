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
Public Class Settings
    Dim da As New MyFire_storage.Data

    Private Sub Settings_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        My.Settings.Save()
    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        cb_backgrounding.IsChecked = My.Settings.myFire_TweetInBackground
        cb_ads.IsChecked = My.Settings.Filter_Spam
        cb_tco.IsChecked = My.Settings.SkipTCO
        cb_tab_timeline.IsChecked = My.Settings.tab_timeline
        cb_tab_mentions.IsChecked = My.Settings.tab_mentions
        cb_tab_messages.IsChecked = My.Settings.tab_messages
        cb_tab_favourites.IsChecked = My.Settings.tab_favourites
        cb_tab_search.IsChecked = My.Settings.tab_search
        cb_tab_ban.IsChecked = My.Settings.tab_ban
        cb_api_NativeFollowersFriends.IsChecked = My.Settings.api_userlist
        cb_api_TimelineLaunch.IsChecked = My.Settings.api_timeline
        cb_api_PopulateAutobox.IsChecked = My.Settings.api_autocomplete
        cb_api_InteractiveMaps.IsChecked = My.Settings.api_mapscontrol
        TweetNumber.Text = My.Settings.api_statuscount
        cb_api_ProfileLoad.IsChecked = My.Settings.api_profileload
        cb_closemy.IsChecked = My.Settings.myFire_CloseToTray
        SetFontCombo()
        SetBackgroundRB()
        SetDisplayRB()
        SetDateRB()
        NotificationSettings()
    End Sub

    Private Sub NotificationSettings()
        cb_notification_window_tweet.IsChecked = My.Settings.Notifications_Display_Tweets
        cb_notification_window_mentions.IsChecked = My.Settings.Notifications_Display_Mentions
        cb_notification_window_message.IsChecked = My.Settings.Notifications_Display_Messages
        cb_notification_sound_tweet.IsChecked = My.Settings.Notifications_Sounds_Tweets
        cb_notification_sound_mention.IsChecked = My.Settings.Notifications_Sounds_Mentions
        cb_notification_sound_message.IsChecked = My.Settings.Notifications_Sounds_Messages
        If My.Settings.Notifications_Window_Placement = 0 Then
            rb_notification_position_left.IsChecked = True
        ElseIf My.Settings.Notifications_Window_Placement = 1 Then
            rb_notification_position_right.IsChecked = True
        End If
    End Sub

    Private Sub btn_Reset_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_Reset.Click
        Dim result As MessageBoxResult
        result = MessageBox.Show("Are you sure you want to reset all settings?", "myFire", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If result = MessageBoxResult.No Then
            Exit Sub
        End If
        da.Reset(My.Settings.linked_ScreenNames)
        My.Settings.Reset()
        My.Settings.beta_mine = True
        My.Settings.Save()
        MessageBox.Show("All settings reset.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
        End
    End Sub

    Private Sub btn_DataCollection_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles btn_DataCollection.Click
        Dim MessageBoxResult As MessageBoxResult = MessageBox.Show("Would you like myFire to send crash reports and usage statistics to iFX Software Inc?  These reports help us improve our application and are completely anonymous.", "myFire", MessageBoxButton.YesNo, MessageBoxImage.Question)
        If MessageBoxResult = Windows.MessageBoxResult.Yes Then
            My.Settings.SendUsageLogs = True
            MessageBox.Show("Thank you very much!", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
        Else
            My.Settings.SendUsageLogs = False
        End If
        My.Settings.Save()
    End Sub

    Sub SetFontCombo()
        Dim size As Double = My.Settings.FontSize
        If size = 8 Then
            rb_font_tiny.IsChecked = True
        ElseIf size = 12 Then
            rb_font_small.IsChecked = True
        ElseIf size = 14 Then
            rb_font_medium.IsChecked = True
        ElseIf size = 16 Then
            rb_font_large.IsChecked = True
        ElseIf size = 18 Then
            rb_font_huge.IsChecked = True
        Else
            '
        End If
    End Sub

    Sub SetBackgroundRB()
        If My.Settings.design_twitteruserbackground = True Then
            rb_design_twitter.IsChecked = True
        Else
            rb_design_bing.IsChecked = True
        End If
    End Sub

    Sub SetDisplayRB()
        If My.Settings.display_displayscreenanme = True Then
            rb_name_username.IsChecked = True
        Else
            rb_name_name.IsChecked = True
        End If
    End Sub

    Sub SetDateRB()
        If My.Settings.display_daterelitave = True Then
            rb_date_relitave.IsChecked = True
        Else
            rb_date_absolute.IsChecked = True
        End If
    End Sub

#Region "Save Settings"

#Region "General Tab"
    Private Sub cb_closemy_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_closemy.Click
        My.Settings.myFire_CloseToTray = cb_closemy.IsChecked
    End Sub

    Private Sub cb_backgrounding_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_backgrounding.Click
        My.Settings.myFire_TweetInBackground = cb_backgrounding.IsChecked
    End Sub

    Private Sub cb_ads_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_ads.Click
        If cb_ads.IsChecked = False Then
            MessageBox.Show("myFire will not block advertisements.  But just remember that these advertisements can contain malicious links that if clicked may harm your computer.  Please remember not to click on the links in advertisements tweets.", "Because we care...", MessageBoxButton.OK, MessageBoxImage.Warning)
        End If
        My.Settings.Filter_Spam = cb_ads.IsChecked
    End Sub
#End Region

#Region "Tab Visibility Tab"
    Private Sub cb_tab_timeline_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_tab_timeline.Click
        My.Settings.tab_timeline = cb_tab_timeline.IsChecked
    End Sub

    Private Sub cb_tab_mentions_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_tab_mentions.Click
        My.Settings.tab_mentions = cb_tab_mentions.IsChecked
    End Sub

    Private Sub cb_tab_messages_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_tab_messages.Click
        My.Settings.tab_messages = cb_tab_messages.IsChecked
    End Sub

    Private Sub cb_tab_favourites_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_tab_favourites.Click
        My.Settings.tab_favourites = cb_tab_favourites.IsChecked
    End Sub

    Private Sub cb_tab_search_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_tab_search.Click
        My.Settings.tab_search = cb_tab_search.IsChecked
    End Sub

    Private Sub cb_tab_ban_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_tab_ban.Click
        My.Settings.tab_ban = cb_tab_ban.IsChecked
    End Sub
#End Region

#Region "Display Tab"
    Private Sub rb_font_tiny_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_font_tiny.Checked
        My.Settings.FontSize = 8
    End Sub

    Private Sub rb_font_small_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_font_small.Checked
        My.Settings.FontSize = 12
    End Sub

    Private Sub rb_font_medium_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_font_medium.Checked
        My.Settings.FontSize = 14
    End Sub

    Private Sub rb_font_large_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_font_large.Checked
        My.Settings.FontSize = 16
    End Sub

    Private Sub rb_font_huge_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_font_huge.Checked
        My.Settings.FontSize = 18
    End Sub

    Private Sub rb_name_name_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_name_name.Checked
        My.Settings.display_displayscreenanme = False
    End Sub

    Private Sub rb_name_username_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_name_username.Checked
        My.Settings.display_displayscreenanme = True
    End Sub

    Private Sub rb_date_relitave_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_date_relitave.Checked
        My.Settings.display_daterelitave = True
    End Sub

    Private Sub rb_date_absolute_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_date_absolute.Checked
        My.Settings.display_daterelitave = False
    End Sub
#End Region

#Region "API Tab"
    Private Sub cb_api_NativeFollowersFriends_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_api_NativeFollowersFriends.Checked
        My.Settings.api_userlist = cb_api_NativeFollowersFriends.IsChecked
    End Sub

    Private Sub cb_api_ProfileLoad_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_api_ProfileLoad.Click
        My.Settings.api_profileload = cb_api_ProfileLoad.IsChecked
    End Sub

    Private Sub cb_api_TimelineLaunch_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_api_TimelineLaunch.Click
        My.Settings.api_timeline = cb_api_TimelineLaunch.IsChecked
    End Sub

    Private Sub cb_api_InteractiveMaps_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_api_InteractiveMaps.Click
        My.Settings.api_mapscontrol = cb_api_InteractiveMaps.IsChecked
    End Sub

    Private Sub cb_api_PopulateAutobox_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_api_PopulateAutobox.Click
        My.Settings.api_autocomplete = cb_api_PopulateAutobox.IsChecked
    End Sub

    Private Sub TweetNumber_TextChanged(sender As System.Object, e As System.Windows.Controls.TextChangedEventArgs) Handles TweetNumber.TextChanged
        If AreAllValidNumericChars(TweetNumber.Text) = False Then
            TweetNumber.Background = New SolidColorBrush(Colors.Crimson)
            TweetNumber.Foreground = New SolidColorBrush(Colors.White)
        Else
            My.Settings.api_statuscount = TweetNumber.Text
            TweetNumber.Background = New SolidColorBrush(Colors.White)
            TweetNumber.Foreground = New SolidColorBrush(Colors.Black)
        End If
    End Sub
#End Region

#Region "Notifications Tab"


    Private Sub btn_TestNotification_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_TestNotification.Click
        myfireactions.Notification_Show_Test("Test notification.", "Notifications will look like this.", Forms.ToolTipIcon.Warning)
    End Sub
#End Region

#End Region

    Private Function AreAllValidNumericChars(str As String) As Boolean
        Dim ret As Boolean = True
        If str = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator Or str = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator Or str = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol Or str = System.Globalization.NumberFormatInfo.CurrentInfo.NegativeSign Or str = System.Globalization.NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol Or str = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator Or str = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator Or str = System.Globalization.NumberFormatInfo.CurrentInfo.PercentDecimalSeparator Or str = System.Globalization.NumberFormatInfo.CurrentInfo.PercentGroupSeparator Or str = System.Globalization.NumberFormatInfo.CurrentInfo.PercentSymbol Or str = System.Globalization.NumberFormatInfo.CurrentInfo.PerMilleSymbol Or str = System.Globalization.NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol Or str = System.Globalization.NumberFormatInfo.CurrentInfo.PositiveSign Then
            Return ret
        End If

        Dim l As Integer = str.Length
        For i As Integer = 0 To l - 1
            Dim ch As Char = str(i)
            ret = ret And [Char].IsDigit(ch)
        Next

        Return ret
    End Function

    Private Sub about_ads_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles about_ads.MouseDown
        MessageBox.Show("Users can sign up with marketing agencies that periodically send an advertisement tweet from their account.  Almost all of the time these tweets contain unsafe links and are always unsightly.  myFire can automatically block these tweets from appearing.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub

    Private Sub rb_design_twitter_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_design_twitter.Checked
        My.Settings.design_twitteruserbackground = True
    End Sub

    Private Sub rb_design_bing_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_design_bing.Checked
        My.Settings.design_twitteruserbackground = False
    End Sub

    Private Sub cb_notification_window_tweet_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_notification_window_tweet.Click
        My.Settings.Notifications_Display_Tweets = cb_notification_window_tweet.IsChecked
    End Sub

    Private Sub cb_notification_sound_tweet_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_notification_sound_tweet.Click
        My.Settings.Notifications_Sounds_Tweets = cb_notification_sound_tweet.IsChecked
    End Sub

    Private Sub cb_notification_window_mentions_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_notification_window_mentions.Click
        My.Settings.Notifications_Display_Mentions = cb_notification_window_mentions.IsChecked
    End Sub

    Private Sub cb_notification_sound_mention_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_notification_sound_mention.Click
        My.Settings.Notifications_Sounds_Mentions = cb_notification_sound_mention.IsChecked
    End Sub

    Private Sub cb_notification_window_message_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_notification_window_message.Click
        My.Settings.Notifications_Display_Messages = cb_notification_window_message.IsChecked
    End Sub

    Private Sub cb_notification_sound_message_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles cb_notification_sound_message.Click
        My.Settings.Notifications_Sounds_Messages = cb_notification_sound_message.IsChecked
    End Sub

    Private Sub rb_notification_position_left_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_notification_position_left.Checked
        My.Settings.Notifications_Window_Placement = 0
    End Sub

    Private Sub rb_notification_position_right_Checked(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles rb_notification_position_right.Checked
        My.Settings.Notifications_Window_Placement = 1
    End Sub

    Private Sub btn_filters_Click(sender As Object, e As RoutedEventArgs) Handles btn_filters.Click
        Dim filter As New myFire_FilterTweets
        filter.ShowDialog()
    End Sub

    Private Sub cb_tco_Click(sender As Object, e As RoutedEventArgs) Handles cb_tco.Click
        My.Settings.SkipTCO = cb_tco.IsChecked
    End Sub
End Class
