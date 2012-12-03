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
Public Class myFire_EditListMembers
    Private TwList As TwitterList
    Sub EditListMembers(ByVal List As TwitterList)
        Dim response As TwitterResponse(Of TwitterUserCollection) = TwitterList.GetMembers(tokens.Tokens, tokens.GetDefaultScreenName, List.Slug)
        Dim t As IList(Of User)
        Try
            For Each User As TwitterUser In response.ResponseObject
                t = New List(Of User)() From {New User() With {.UserObject = User}}
                URList.Items.Add(t)
            Next
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        TwList = List
        Me.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Try
            myfireactions.Twitter_List_Members_Create(TwList.Id, UserBox.Text)
            exceptionhandler.InfoMessage("User added!", "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        Try
            Dim SelectedUser As User = URList.SelectedItem(0)
            myfireactions.Twitter_List_Members_Destroy(TwList.Id, SelectedUser.UserScreenName)
            exceptionhandler.InfoMessage("User removed!", "myFire")
            URList.Items.Remove(URList.SelectedItem)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded

    End Sub
End Class
