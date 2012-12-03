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
Imports Twitterizer
Public Class myFire_UserManagment

    Dim da As New MyFire_storage.Data

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim t As IList(Of UserStoreObject)
        For Each User As StoredUser In da.Users_ReturnUserList
            t = New List(Of UserStoreObject)() From {New UserStoreObject() With {.StoredUserObject = User}}
            UserList.Items.Add(t)
        Next
    End Sub

    Private Sub UserList_SelectionChanged(sender As Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles UserList.SelectionChanged
        If UserList.Items.Count = 1 Then
            btn_RemoveUser.IsEnabled = False
        Else
            btn_RemoveUser.IsEnabled = True
        End If
    End Sub

    Private Sub btn_RemoveUser_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_RemoveUser.Click
        Dim StoredUser As StoredUser = TryCast(UserList.SelectedItem, StoredUser)
        Try
            da.Users_DeleteSingleUser(StoredUser)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        My.Settings.linked_ScreenNames.Remove(StoredUser.ScreenName)
        UserList.Items.Remove(UserList.SelectedItem)
        My.Settings.Save()
    End Sub

    Private Sub btn_AddUser_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_AddUser.Click

        ' TODO: Add account linking. '

        'Dim AddAccount As New myFire_AddTwitterAccount
        'Dim User As OAuthTokenResponse = AddAccount.AddAccount
        'If User Is Nothing Then
        '    MessageBox.Show("You must login to Twitter to access myFire.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
        '    Exit Sub
        'End If
        'da.Users_WriteSingleUser(New StoredUser(User.ScreenName, myfireactions.CurrentTime("Linked at: "), User.UserId, "https://api.twitter.com/1/users/profile_image?screen_name=" & User.ScreenName & "&size=normal", User.Token, User.TokenSecret))
        'My.Settings.myFire_PrimaryAccount = User.ScreenName
        'My.Settings.linked_ScreenNames.Add(User.ScreenName)
        'My.Settings.Save()
        'MessageBox.Show("Account added.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
        'UserList.Items.Clear()
        'Dim t As IList(Of UserStoreObject)
        'For Each StoredUser As StoredUser In da.Users_ReturnUserList
        '    t = New List(Of UserStoreObject)() From {New UserStoreObject() With {.StoredUserObject = StoredUser}}
        '    UserList.Items.Add(t)
        'Next
    End Sub
End Class
