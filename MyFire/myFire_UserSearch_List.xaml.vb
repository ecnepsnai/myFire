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

Public Class myFire_UserSearch_List

#Region "Followers"
    Private Followers_ID As Decimal
    Private WithEvents _followersworker As New BackgroundWorker

    Public Sub Followers(ByVal UserId As Decimal)
        Followers_ID = UserId
        _followersworker.RunWorkerAsync()
        Me.ShowDialog()
    End Sub

    Private Sub _followersworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _followersworker.DoWork
        Dim t As IList(Of User)
        Dim UserList As TwitterUserCollection = myfireactions.Twitter_User_Followers(Followers_ID)
        Me.Dispatcher.Invoke(Sub()
                                 Try
                                     For Each User As TwitterUser In UserList
                                         t = New List(Of User)() From {New User() With {.UserObject = User, .UserFollowVisibility = Windows.Visibility.Visible}}
                                         URList.Items.Add(t)
                                     Next
                                 Catch ex As Exception
                                     exceptionhandler.ExceptionMessage(ex)
                                 End Try
                             End Sub)
    End Sub
#End Region

#Region "Friends"
    Private Friends_ID As Decimal
    Private WithEvents _friendsworker As New BackgroundWorker

    Public Sub Friends(ByVal UserId As Decimal)
        Friends_ID = UserId
        _friendsworker.RunWorkerAsync()
        Me.ShowDialog()
    End Sub

    Private Sub _friendsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _friendsworker.DoWork
        Dim t As IList(Of User)
        Dim UserList As TwitterUserCollection = myfireactions.Twitter_User_Friends(Friends_ID)
        Me.Dispatcher.Invoke(Sub()
                                 Try
                                     For Each User As TwitterUser In UserList
                                         t = New List(Of User)() From {New User() With {.UserObject = User}}
                                         URList.Items.Add(t)
                                     Next
                                 Catch ex As Exception
                                     ExceptionMessage(ex)
                                 End Try
                             End Sub)
    End Sub
#End Region

#Region "Search"
    Private Search_Query As String
    Private WithEvents _searchworker As New BackgroundWorker

    Public Sub Search(ByVal query As String)
        Search_Query = query
        _searchworker.RunWorkerAsync()
        Me.ShowDialog()
    End Sub

    Private Sub _searchworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _searchworker.DoWork
        Dim t As IList(Of User)
        Dim UserList As TwitterUserCollection = myfireactions.Twitter_Search_Users(Search_Query)
        Me.Dispatcher.Invoke(Sub()
                                 Try
                                     For Each User As TwitterUser In UserList
                                         t = New List(Of User)() From {New User() With {.UserObject = User, .UserFollowVisibility = Windows.Visibility.Visible}}
                                         URList.Items.Add(t)
                                     Next
                                 Catch ex As Exception
                                     ExceptionMessage(ex)
                                 End Try
                             End Sub)
    End Sub
#End Region

#Region "Suggested Users"
    Private Suggested_Slug As String
    Private WithEvents _suggestionsworker As New BackgroundWorker

    Public Sub SuggestedUsers(ByVal Slug As String)
        Suggested_Slug = Slug
        _suggestionsworker.RunWorkerAsync()
        Me.ShowDialog()
    End Sub

    Private Sub _suggestionsworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _suggestionsworker.DoWork
        Dim t As IList(Of User)
        Dim UserList As TwitterUserCollection = myfireactions.Twitter_SuggestedUsersList(Suggested_Slug)
        Me.Dispatcher.Invoke(Sub()
                                 Try
                                     For Each User As TwitterUser In UserList
                                         t = New List(Of User)() From {New User() With {.UserObject = User, .UserFollowVisibility = Windows.Visibility.Visible}}
                                         URList.Items.Add(t)
                                     Next
                                 Catch ex As Exception
                                     ExceptionMessage(ex)
                                 End Try
                             End Sub)
    End Sub
#End Region

End Class
