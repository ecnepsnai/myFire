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
Imports System.ComponentModel

Public Class UserSearch
    Dim da As New MyFire_storage.Data
    Friend WithEvents listgetter As New BackgroundWorker

    Private Sub SelectButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles SelectButton.Click
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub

    Private Sub UserSearch_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Closed
        Me.Close()
    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If My.Settings.api_autocomplete = True Then
            If My.Settings.cache_Followers.Count < 1 Then
                listgetter.RunWorkerAsync()
            Else
                For Each User As String In My.Settings.cache_Followers
                    UserBox.Items.Add(User)
                Next
            End If
        Else
            SelectedUser.Text = InputBox("Enter the users screen name, without the @", "myFire")
            Me.Close()
        End If
    End Sub

    Public Function SearchUser() As String
        Me.ShowDialog()
        Return SelectedUser.Text
    End Function

    Private Sub listgetter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles listgetter.DoWork
        Dim Response As TwitterResponse(Of TwitterUserCollection) = TwitterFriendship.Followers(tokens.Tokens)
        If Response.Result <> RequestResult.Success Then
            e.Cancel = True
        End If
        For Each User As TwitterUser In Response.ResponseObject
            If My.Settings.cache_Followers.Contains(User.ScreenName) = False Then
                My.Settings.cache_Followers.Add(User.ScreenName)
            Else
                Me.Dispatcher.Invoke(Sub()
                                         UserBox.Items.Add(User.ScreenName)
                                     End Sub)
            End If
        Next
    End Sub

    Private Sub listgetter_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles listgetter.RunWorkerCompleted
        Me.Cursor = Cursors.Arrow
    End Sub

    Private Sub TextBlock1_KeyUp(sender As Object, e As System.Windows.Input.KeyEventArgs) Handles TextBlock1.KeyUp
        If e.Key = Key.Enter Then
            Me.Close()
        End If
    End Sub

    Private Sub UserBox_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles UserBox.SelectionChanged
        SelectedUser.Text = UserBox.SelectedItem
    End Sub
End Class
