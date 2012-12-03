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
Public Class myFire_direct_messages

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        GetMessages()
    End Sub

#Region "Direct Messages"
    Private Sub GetMessages()
        Me.Cursor = Cursors.AppStarting
        Try
            Dim response As TwitterResponse(Of TwitterDirectMessageCollection) = TwitterDirectMessage.DirectMessages(tokens.Tokens)
            If response.Result <> RequestResult.Success Then
                exceptionhandler.ExceptionMessage(New Exception(response.ErrorMessage))
                Exit Sub
            End If
            Dim t As IList(Of DirectMessage)
            For Each Message As TwitterDirectMessage In response.ResponseObject
                t = New List(Of DirectMessage)() From { _
                  New DirectMessage() With { _
                      .MessageObject = Message
                  }
                }
                DMlist.Items.Add(t)
            Next
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        Me.Cursor = System.Windows.Input.Cursors.Arrow
    End Sub
#End Region

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Dim US As New UserSearch
        Dim TD As New TweetDialog
        TD.Message_Format(US.SearchUser)
    End Sub
End Class
