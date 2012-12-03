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
Imports System.ComponentModel
Public Class myFire_WelcomeScreen
    Dim DailyImage As ImageOfTheDay
    Private WithEvents _advertisementsworker As New BackgroundWorker

    Dim AuthURL As String
    Dim da As New MyFire_storage.Data
    Dim PrimaryUser As TwitterUser
    Dim Done As Boolean = False

    Public Function LinkAccount() As TwitterUser
        Me.ShowDialog()
        Dim ht As New MyFire_HowTo
        ht.ShowDialog()
        Return PrimaryUser
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If Done = False Then
            Dim AddAccount As New myFire_AddTwitterAccount
            Dim User As OAuthTokenResponse = AddAccount.AddAccount
            If User Is Nothing Then
                MessageBox.Show("You must login to Twitter to access myFire.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
                Exit Sub
            End If
            AddFirstAccount(User)
        ElseIf Done = True Then
            Dim AddAccount As New myFire_AddTwitterAccount
            Dim User As OAuthTokenResponse = AddAccount.AddAccount
            If User Is Nothing Then
                Exit Sub
            End If

        End If
    End Sub

    Private Sub myFire_WelcomeScreen_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Closing
        If Done = False Then
            End
        Else
            _advertisementsworker.RunWorkerAsync()
        End If
    End Sub

    Private Sub AddFirstAccount(ByVal User As OAuthTokenResponse)
        Dim su As New StoredUser(User.ScreenName, myfireactions.CurrentTime("Linked at: "), User.UserId, "https://api.twitter.com/1/users/profile_image?screen_name=" & User.ScreenName & "&size=normal", User.Token, User.TokenSecret, True)
        Try
            da.Users_WriteSingleUser(su)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        My.Settings.FirstRun = False
        My.Settings.Save()

        TitleBlock.Text = "Welcome to myFire, @" & User.ScreenName
        HeaderBlock.Text = "You can now close this window to start using myFire, or add another account."
        PormotionOptions.Visibility = Windows.Visibility.Visible
        Button1.Content = "Add another account"
        Done = True
        PormotionOptions.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub AddAnotherAccount(ByVal User As OAuthTokenResponse)
        Dim su As New StoredUser(User.ScreenName, myfireactions.CurrentTime("Linked at: "), User.UserId, "https://api.twitter.com/1/users/profile_image?screen_name=" & User.ScreenName & "&size=normal", User.Token, User.TokenSecret, False)
        Try
            da.Users_WriteSingleUser(su)
            exceptionhandler.InfoMessage("Added @" & User.ScreenName, "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub _advertisementsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _advertisementsworker.DoWork
        Dim ShouldPostTweet As Boolean
        Dim ShouldFollowUser As Boolean
        Me.Dispatcher.Invoke(Sub()
                                 ShouldPostTweet = PostTweet.IsChecked
                                 ShouldFollowUser = FollowMyFire.IsChecked
                             End Sub)
        If ShouldPostTweet = True Then
            Try
                myfireactions.Twitter_Status_Update("Just started using @MyFireApp!  You should totally try it out.  http://myFire.ifxsoftware.net/")
            Catch ex As Exception

            End Try
        End If
        If ShouldFollowUser = True Then
            Try
                myfireactions.Twitter_Friendship_Create(369908971)
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub myFire_WelcomeScreen_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        DailyImage = New ImageOfTheDay
        WelcomeImage.ImageSource = myfireactions.Image_Format(DailyImage.ImageURL.ToString, False)
        CopyrightBlock.ToolTip = DailyImage.Copyright
        If My.Settings.beta_mine = False Then
            myfireactions.Beta_DataMine()
            My.Settings.beta_mine = True
            My.Settings.Save()
        End If
    End Sub
End Class
