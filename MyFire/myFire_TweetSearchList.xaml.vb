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

Public Class myFire_TweetSearchList
    Private WithEvents _searchworker As New BackgroundWorker
    Private Search_query As String

    Public Sub Search(ByVal query As String)
        Search_query = query
        _searchworker.RunWorkerAsync()
        Me.ShowDialog()
    End Sub

    Private Sub _searchworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _searchworker.DoWork
        Dim t As IList(Of SearchTweet)
        Dim TweetList As TwitterSearchResultCollection = myfireactions.Twitter_Search_Tweets(Search_query)
        Me.Dispatcher.Invoke(Sub()
                                 Try
                                     For Each Result As TwitterSearchResult In TweetList
                                         t = New List(Of SearchTweet)() From {New SearchTweet() With {.TweetSearchObject = Result}}
                                         TMList.Items.Add(t)
                                     Next
                                 Catch ex As Exception
                                     ExceptionMessage(ex)
                                 End Try
                             End Sub)
    End Sub
End Class
