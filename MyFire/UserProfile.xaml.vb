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
Public Class UserProfile
    Dim User As New TwitterUser
    Dim da As New MyFire_storage.Data
    Dim Following As Boolean
    Dim SearchedLocation As Boolean = False
    Dim FollowButtonAction As Decimal = 0

#Region "Background Workers"
    Friend WithEvents _timelines As New BackgroundWorker
    Friend WithEvents _mentions As New BackgroundWorker
    Friend WithEvents _favourites As New BackgroundWorker
#End Region

#Region "Search"
    Private Sub Search(ByVal User As TwitterUser)

        '* Check user identity
        If User.Verified = True Then
            user_identity.Text = "Verified"
            user_identity.Foreground = Brushes.LightBlue
        ElseIf User.IsProtected = True Then
            user_identity.Text = "Protected"
        Else
            user_identity.Text = ""
        End If

        '* Displays users screen name, name, and profile image
        user_name.Text = User.Name
        user_screenname.Text = "@" & User.ScreenName
        TweetUserImage.ImageSource = myfireactions.Image_Format(User.ProfileImageLocation)
        User_Followers.Text = myfireactions.Number_Converter(User.NumberOfFollowers)
        User_Friends.Text = myfireactions.Number_Converter(User.NumberOfFriends)
        User_Tweets.Text = myfireactions.Number_Converter(User.NumberOfStatuses)
        User_Favourites.Text = myfireactions.Number_Converter(User.NumberOfFavorites)

        '* Sets background image
        Dim bi As New BitmapImage
        bi.BeginInit()
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.UriSource = New Uri(User.ProfileBackgroundImageLocation)
        bi.EndInit()
        BackgroundImage.ImageSource = bi

        '* Sets title, and tab headers.
        Me.Title = Me.Title.Replace("{screen_name}", User.ScreenName)

        '* Sets Description
        If User.Description = Nothing Then
            me_about_description.Visibility = Windows.Visibility.Collapsed
        Else
            User_Description.Text = User.Description
        End If

        '* Sets Location
        If User.Location = Nothing Then
            me_about_location.Visibility = Windows.Visibility.Collapsed
        Else
            User_location.Text = User.Location
        End If

        '* Sets URL
        If User.Website = Nothing Then
            me_about_url.Visibility = Windows.Visibility.Collapsed
        Else
            User_URL.Text = User.Website
        End If

        '* More Information
        User_More_CreateDate.Text = "Member since: " & User.CreatedDate.ToString
        User_More_ID.Text = "User Number: " & myfireactions.Number_Converter(User.Id, "")
        User_More_Languange.Text = "Language: " & User.Language
        Try
            User_More_LastTweet.Text = "Last tweet: " & myfireactions.DateConverter(User.Status.CreatedDate)
        Catch ex As Exception
            User_More_LastTweet.Visibility = Windows.Visibility.Collapsed
        End Try
        If User.IsFollowing = True Then
            FollowButtonAction = 2
            FollowButton.Text = "Unfollow"
        ElseIf User.ScreenName = tokens.GetDefaultScreenName Then
            FollowButtonAction = 3
            FollowButton.Text = "Edit"
        Else
            FollowButtonAction = 1
            FollowButton.Text = "Follow"
        End If
    End Sub
#End Region

#Region "Get"

#Region "Timeline"
    Private Sub _timelines_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _timelines.DoWork
        Dim t As IList(Of Tweet)
        Dim TweetList As TwitterStatusCollection = Nothing
        Try
            TweetList = myfireactions.Twitter_Timeline_User(User.Id)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In TweetList
                                     t = New List(Of Tweet)() From {New Tweet With {.TwitterStatusObject = Tweet}}
                                     TMList.Items.Add(t)
                                 Next
                             End Sub)
    End Sub

    Private Sub _timelines_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _timelines.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub get_timeline()
        Me.Cursor = Cursors.AppStarting
        _timelines.RunWorkerAsync()
    End Sub
#End Region

#Region "Mentions"
    Private Sub _mentions_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _mentions.DoWork
        Dim t As IList(Of Tweet)
        Dim TweetList As TwitterStatusCollection = myfireactions.Twitter_Timeline_User_Mentions(User.Id)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In TweetList
                                     t = New List(Of Tweet)() From {New Tweet With {.TwitterStatusObject = Tweet}}
                                     MEList.Items.Add(t)
                                 Next
                             End Sub)
    End Sub

    Private Sub _mentions_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _mentions.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub get_mentions()
        Me.Cursor = Cursors.AppStarting
        _mentions.RunWorkerAsync()
    End Sub
#End Region

#Region "Favourites"
    Private Sub _favourites_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _favourites.DoWork
        Dim t As IList(Of Tweet)
        Dim TweetList As TwitterStatusCollection = myfireactions.Twitter_Timeline_User_Favourites(User.Id)
        Me.Dispatcher.Invoke(Sub()
                                 For Each Tweet As TwitterStatus In TweetList
                                     t = New List(Of Tweet)() From {New Tweet With {.TwitterStatusObject = Tweet}}
                                     FAVList.Items.Add(t)
                                 Next
                             End Sub)
    End Sub

    Private Sub _favourites_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _favourites.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub get_favourites()
        Me.Cursor = Cursors.AppStarting
        _favourites.RunWorkerAsync()
    End Sub
#End Region

#End Region

#Region "Tabs"

    Private Sub TabControl1_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles TabControl1.SelectionChanged
        'Timeline
        If TabControl1.SelectedIndex = 1 Then
            If TMList.Items.Count = 0 Then
                get_timeline()
            End If
        End If

        'Mentions
        If TabControl1.SelectedIndex = 2 Then
            If MEList.Items.Count = 0 Then
                get_mentions()
            End If
        End If

        'Favourites
        If TabControl1.SelectedIndex = 3 Then
            If FAVList.Items.Count = 0 And User.NumberOfFavorites > 0 Then
                get_favourites()
            End If
        End If
    End Sub

#End Region

    Public Sub Show_User(ByVal ScreenName As String)
        If My.Settings.api_profileload = False Then
            Process.Start("http://twitter.com/" & ScreenName)
            Exit Sub
        End If
        Dim response As TwitterResponse(Of TwitterUser) = TwitterUser.Show(tokens.Tokens, ScreenName)
        If response.Result <> RequestResult.Success Then
            MessageBox.Show("Error: " & response.ErrorMessage, "myFire", MessageBoxButton.OK, MessageBoxImage.Error)
            Me.Close()
            Exit Sub
        End If
        User = response.ResponseObject
        Search(response.ResponseObject)
        Me.ShowDialog()
    End Sub

    Public Sub Show_User(ByVal TwitterUser As TwitterUser)
        If My.Settings.api_profileload = False Then
            Process.Start("http://twitter.com/" & TwitterUser.ScreenName)
            Exit Sub
        End If
        User = TwitterUser
        Search(TwitterUser)
        Me.ShowDialog()
    End Sub

    Private Sub User_URL_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles User_URL.MouseDown
        Process.Start(User.Website)
    End Sub

    Private Sub FollowButton_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles FollowButton.MouseDown
        If FollowButtonAction = 0 Then
            Exit Sub
        End If
        If FollowButtonAction = 1 Then
            Try
                myfireactions.Twitter_Friendship_Create(User.Id)
                InfoMessage("Followed @" & User.ScreenName, "myFire")
                FollowButtonAction = 2
                FollowButton.Text = "Unfollow"
                Exit Sub
            Catch ex As Exception
                ExceptionMessage(ex)
            End Try
        End If
        If FollowButtonAction = 2 Then
            Try
                myfireactions.Twitter_Friendship_Delete(User.Id)
                InfoMessage("Unfollowed @" & User.ScreenName, "myFire")
                FollowButtonAction = 1
                FollowButton.Text = "Follow"
                Exit Sub
            Catch ex As Exception
                ExceptionMessage(ex)
            End Try
        End If
        If FollowButtonAction = 3 Then
            Dim ed As New EditProfile
            Dim editprofile As TwitterUser = ed.Edit_User(User)
            Search(editprofile)
        End If
    End Sub

    Private Sub Image1_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles Image1.MouseDown
        User_CM.IsOpen = True
    End Sub

    Private Sub User_CM_Mention(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            Dim t As New TweetDialog
            t.Tweet_Format("@" & User.ScreenName)
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub User_CM_Message(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            If User.IsFollowedBy = False Then
                MessageBox.Show("You can only message users that follow you.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
                Exit Sub
            End If
            Dim t As New TweetDialog
            t.Message_Format(User.ScreenName)
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub User_CM_Mute(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            If My.Settings.Filter_User.Contains(User.ScreenName) = True Then
                My.Settings.Filter_User.Remove(User.ScreenName)
                My.Settings.Save()
                InfoMessage("Unmuted!", "myFire")
            Else
                My.Settings.Filter_User.Add(User.ScreenName)
                My.Settings.Save()
                InfoMessage("Muted!", "myFire")
            End If
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub User_CM_Block(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_Block_Create(User.Id)
            InfoMessage("Blocked @" & User.ScreenName, "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub User_CM_Report(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_User_Report(User.Id)
            InfoMessage("Reported @" & User.ScreenName, "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub User_Followers_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles User_Followers.MouseDown
        Dim u As New myFire_UserSearch_List
        u.Followers(User.Id)
    End Sub

    Private Sub User_Friends_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles User_Friends.MouseDown
        Dim u As New myFire_UserSearch_List
        u.Friends(User.Id)
    End Sub

    Private Sub Border_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs)
        Dim p As New myFire_pictureviewer
        User.ProfileImageLocation = User.ProfileImageLocation.Replace("_normal", "")
        p.Show_Image(User.ProfileImageLocation)
    End Sub

    Private Sub me_about_location_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles me_about_location.MouseDown
        'Dim bapi As New MyFire_data.BingAPI.Mapping
        'Dim Result As MyFire_data.SearchResponse = bapi.GeocodeAddress(User.Location)
        'Dim mpa As New myFire_MapDialog
        'mpa.ShowCord(Result.ResourceSet.Resource(0).Point.latitude, Result.ResourceSet.Resource(0).Point.longitude)
    End Sub
End Class
