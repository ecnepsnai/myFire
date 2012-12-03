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

Public Class myFire_ConversationView

    Dim ReplyToID As Decimal
    Public Reciever As TwitterUser
    Dim SenderScreenname As String
    Dim MainWindow As MainWindow = Application.Current.Windows.OfType(Of MainWindow).First

    ''' <summary>
    ''' Creates a new dialog with the initial conversation loaded and ready for reply.
    ''' </summary>
    ''' <param name="InitialStatus">The initial status to load the conversation from.</param>
    ''' <remarks></remarks>
    Public Sub Show_Conversation(ByVal InitialStatus As TwitterStatus, ByVal SenderScreenName As String)
        Dim t As IList(Of Tweet)
        t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = InitialStatus}}
        CList.Items.Add(t)
        ReplyToID = InitialStatus.Id
        Reciever = InitialStatus.User
        Me.SenderScreenname = SenderScreenName
        Me.Title = "Conversation with: @" & Reciever.ScreenName
        messagebox.Text = "@" & Reciever.ScreenName
        MainWindow.Conversation_Add(InitialStatus.User.ScreenName)
        Me.Show()
    End Sub

    Private Sub messagebox_TextChanged(sender As Object, e As TextChangedEventArgs) Handles messagebox.TextChanged
        If messagebox.Text.Length > 140 Then
            messagebox.Background = New SolidColorBrush(Colors.Red)
            btn_Send.IsEnabled = False
        Else
            messagebox.Background = New SolidColorBrush(Colors.White)
            btn_Send.IsEnabled = True
        End If
    End Sub

    Private Sub btn_Send_Click(sender As Object, e As RoutedEventArgs) Handles btn_Send.Click
        PostStatus()
    End Sub

    Private Sub PostStatus()
        If messagebox.Text.Length > 140 Then
            Exit Sub
        End If
        Dim o As New Twitterizer.StatusUpdateOptions
        o.InReplyToStatusId = ReplyToID
        Dim TwitterResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.Update(tokens.Tokens, messagebox.Text, o)
        If TwitterResponse.Result <> RequestResult.Success Then
            exceptionhandler.ExceptionMessage(New Exception(TwitterResponse.ErrorMessage))
            Exit Sub
        End If
        Dim t As IList(Of Tweet)
        t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = TwitterResponse.ResponseObject}}
        CList.Items.Add(t)
        messagebox.Text = "@" & Reciever.ScreenName & " "
    End Sub

    Public Sub AddStatus(ByVal Tweet As TwitterStatus)
        If Tweet.User.Id = Reciever.Id And Tweet.User.ScreenName = Reciever.ScreenName Then
            Dim t As IList(Of Tweet)
            t = New List(Of Tweet)() From {New Tweet() With {.TwitterStatusObject = Tweet}}
            CList.Items.Add(t)
            ReplyToID = Tweet.Id
            If Me.WindowState = Forms.FormWindowState.Minimized Then
                WindowFlash.WindowExtensions.FlashWindow(Me, 10)
            End If
        End If
    End Sub

    Private Sub myFire_ConversationView_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        Try
            MainWindow.Conversation_Remove(Reciever.ScreenName)
        Catch
            '
        End Try
    End Sub

    Private Sub messagebox_KeyUp(sender As Object, e As KeyEventArgs) Handles messagebox.KeyUp
        If e.Key = Key.Enter Then
            PostStatus()
        End If
    End Sub
End Class