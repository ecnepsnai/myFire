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
Public Class myFire_TweetList
    Public Sub User_Timeline(ByVal UserID As Decimal)
        Dim t As IList(Of Tweet)
        Try
            For Each Tweet As TwitterStatus In myfireactions.Twitter_Timeline_User(UserID)
                t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                TMList.Items.Add(t)
            Next
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        Me.ShowDialog()
    End Sub

    Public Sub User_Favourites(ByVal UserID As Decimal)
        Dim t As IList(Of Tweet)
        Try
            For Each Tweet As TwitterStatus In myfireactions.Twitter_Favourites(UserID)
                t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                TMList.Items.Add(t)
            Next
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        Me.ShowDialog()
    End Sub

    Public Sub List_Tweets(ByVal ListID As Decimal)
        Dim t As IList(Of Tweet)
        Try
            For Each Tweet As TwitterStatus In myfireactions.Twitter_List_Tweets(ListID, tokens.GetDefaultScreenName)
                t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
                TMList.Items.Add(t)
            Next
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        Me.ShowDialog()
    End Sub
End Class
