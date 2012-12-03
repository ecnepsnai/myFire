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
Imports Twitterizer.Streaming
Imports System.Net.NetworkInformation

Module streamingmanager
    Private jsonView As Boolean = False
    Private StreamOff As Boolean = False
    Private StreamOptions As New Streaming.StreamOptions
    Private ts As Streaming.TwitterStream
    Dim MainWindow As MainWindow = Application.Current.Windows.OfType(Of MainWindow).First

    ''' <summary>
    ''' Initiates the TwitterStream for the session.
    ''' </summary>
    ''' <param name="ScreenName">The screen name of the first user to track.</param>
    Sub InitiateStream(ByVal ScreenName As String)
        ts = New Streaming.TwitterStream(tokens.Tokens, "myFire", StreamOptions)
            StreamOptions.Track.Add(ScreenName)
            ts.StartUserStream(AddressOf StreamInit, AddressOf StreamStopped, AddressOf NewTweet, AddressOf DeletedTweet, AddressOf NewDirectMessage, AddressOf DeletedDirectMessage, AddressOf OtherEvent, AddressOf RawJson)
    End Sub

    ''' <summary>
    ''' Adds the specified user to the tracking list.
    ''' </summary>
    ''' <param name="ScreenName">The user to add.</param>
    Sub AddUserStream(ByVal ScreenName As String)
        StreamOptions.Track.Add(ScreenName)
    End Sub

    ''' <summary>
    ''' Removes the specified user from the tracking list.
    ''' </summary>
    ''' <param name="ScreenName">The user to remove.</param>
    ''' <remarks></remarks>
    Sub StopUserStream(ByVal ScreenName As String)
        StreamOptions.Track.Remove(ScreenName)
    End Sub

    ''' <summary>
    ''' Stops the stream and disposes all resources.
    ''' </summary>
    Sub TerminateStream()
        ts.EndStream()
        ts.Dispose()
        StreamOptions.Track.Clear()
    End Sub

    Private Sub NewTweet(ByVal Tweet As TwitterStatus)
        If Not jsonView Then
            If myfireactions.Twitter_Status_FilterTweet(Tweet) = False Then
                Exit Sub
            End If
            If Tweet.Text.Contains(tokens.GetDefaultScreenName) = True Then
                If Not Tweet.Text.Contains("@") Then
                    Exit Sub
                End If
            End If
            If Tweet.RetweetedStatus IsNot Nothing Then
                MainWindow.streaming_notification_status(Tweet.RetweetedStatus)
            Else
                MainWindow.streaming_notification_status(Tweet)
            End If
            If Tweet.Text.Contains("@" & tokens.GetDefaultScreenName) = True Then
                MainWindow.streaming_AddMentionsItem(Tweet)
                MainWindow.streaming_AddTimelineItem(Tweet)
            Else
                MainWindow.streaming_AddTimelineItem(Tweet)
            End If
        End If
    End Sub

    Private Sub NewDirectMessage(ByVal message As TwitterDirectMessage)
        If Not jsonView Then
            MainWindow.streaming_notification_message(message)
            MainWindow.streaming_AddMessageItem(message)
        End If
    End Sub

    Private Sub DeletedTweet(ByVal e As TwitterStreamDeletedEvent)
        If Not jsonView Then
            MainWindow.streaming_RemoteTweet(e.Id)
        End If
    End Sub

    Private Sub DeletedDirectMessage(ByVal e As TwitterStreamDeletedEvent)
        If Not jsonView Then
            MainWindow.streaming_RemoveMessage(e.Id)
        End If
    End Sub

    Private Sub StreamInit(ByVal friends As TwitterIdCollection)
        If Not jsonView Then
            MainWindow.streaming_Started()
        End If
    End Sub

    Private Sub StreamStopped(ByVal reason As StopReasons)
        If Not jsonView Then
            MainWindow.streaming_Stopped(reason)
        End If
    End Sub

    Private Sub OtherEvent(ByVal e As TwitterStreamEvent)
        If Not jsonView Then
            '
        End If
    End Sub

    Private Sub RawJson(ByVal json As String)
        '
    End Sub
End Module
