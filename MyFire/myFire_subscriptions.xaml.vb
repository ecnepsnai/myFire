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
Imports MyFire_data

Public Class myFire_subscriptions
    Dim da As New MyFire_data.Data
    Dim FollowUsername As String
    Dim SubscribedUsername As String

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        For Each User In My.Settings.subscriptions
            Subscription_List.Items.Add(User)
        Next
        Dim o As New UsersIdsOptions
        o.ScreenName = My.Settings.myFire_PrimaryAccount
        Dim UserIDList As TwitterResponse(Of UserIdCollection) = TwitterFriendship.FriendsIds(tokens.Tokens, o)
        Dim p As New LookupUsersOptions
        p.UserIds = UserIDList.ResponseObject
        Dim UserScreenNameList As TwitterResponse(Of TwitterUserCollection) = TwitterUser.Lookup(tokens.Tokens, p)
        For Each User As TwitterUser In UserScreenNameList.ResponseObject
            Following_List.Items.Add(User.ScreenName)
        Next
    End Sub

    Private Sub Following_List_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles Following_List.SelectionChanged
        If Following_List.SelectedIndex = -1 Then
            Add_Button.IsEnabled = False
        Else
            Add_Button.IsEnabled = True
        End If
        FollowUsername = Following_List.SelectedItem.ToString
    End Sub

    Private Sub Subscription_List_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles Subscription_List.SelectionChanged
        If Subscription_List.SelectedIndex = -1 Then
            Remove_Button.IsEnabled = False
        Else
            Remove_Button.IsEnabled = True
        End If
        SubscribedUsername = Subscription_List.SelectedItem.ToString
    End Sub

    Private Sub Add_Button_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Add_Button.Click
        Subscription_List.Items.Add(FollowUsername)
        My.Settings.subscriptions.Add(FollowUsername)
        My.Settings.Save()
        MessageBox.Show("Subscribed!", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub

    Private Sub Remove_Button_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Remove_Button.Click
        My.Settings.subscriptions.Remove(SubscribedUsername)
        Subscription_List.Items.Remove(SubscribedUsername)
        My.Settings.Save()
        MessageBox.Show("Removed!", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub
End Class
