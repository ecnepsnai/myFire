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
Public Class Tweet
    Public Property TwitterStatusObject As TwitterStatus
        Get
            Return m_TwitterStatus
        End Get
        Set(Tweet As TwitterStatus)
            If Tweet.RetweetedStatus IsNot Nothing Then
                TweetText = myfireactions.FormatTweet(Tweet.RetweetedStatus)
                If My.Settings.display_displayscreenanme = True Then
                    TweetUsername = Tweet.RetweetedStatus.User.ScreenName & ".  Retweeted by: " & Tweet.User.ScreenName
                Else
                    TweetUsername = Tweet.RetweetedStatus.User.Name & ".  Retweeted by: " & Tweet.User.Name
                End If
                TweetImage = myfireactions.Image_User(Tweet.RetweetedStatus.User.ProfileImageLocation)
                TweetAge = myfireactions.DateConverter(Tweet.RetweetedStatus.CreatedDate)
                TweetDate = Tweet.RetweetedStatus.CreatedDate
                m_TwitterStatus = Tweet.RetweetedStatus
                ReadOnlyPhaser(Tweet.RetweetedStatus.Text)
            Else
                TweetText = myfireactions.FormatTweet(Tweet)
                If My.Settings.display_displayscreenanme = True Then
                    TweetUsername = Tweet.User.ScreenName
                Else
                    TweetUsername = Tweet.User.Name
                End If
                TweetImage = myfireactions.Image_User(Tweet.User.ProfileImageLocation)
                TweetAge = myfireactions.DateConverter(Tweet.CreatedDate)
                TweetDate = Tweet.CreatedDate
                m_TwitterStatus = Tweet
                ReadOnlyPhaser(Tweet.Text)
            End If
        End Set
    End Property
    Private m_TwitterStatus As TwitterStatus

    Private Sub ReadOnlyPhaser(ByVal Text As String)
        If Text.Contains("@" & tokens.GetDefaultScreenName) = True Then
            m_TweetIsMention = True
        Else
            m_TweetIsMention = False
        End If
        If Text.Contains("http://") = True Then
            m_TweetContainsLink = True
        Else
            m_TweetContainsLink = False
        End If
    End Sub

#Region "Objects"

    Public Property TweetText As String
        Get
            Return m_TweetText
        End Get
        Set(value As String)
            m_TweetText = value
        End Set
    End Property
    Private m_TweetText As String

    Public Property TweetUsername() As String
        Get
            Return m_Username
        End Get
        Set(ByVal value As String)
            m_Username = value
        End Set
    End Property
    Private m_Username As String

    Public Property TweetImage() As ImageSource
        Get
            Return m_Image
        End Get
        Set(ByVal value As ImageSource)
            m_Image = value
        End Set
    End Property
    Private m_Image As ImageSource

    Public Property TweetAge() As String
        Get
            Return m_Age
        End Get
        Set(ByVal value As String)
            m_Age = value
        End Set
    End Property
    Private m_Age As String

    Public Property TweetDate() As Date
        Get
            Return m_Date
        End Get
        Set(ByVal value As Date)
            m_Date = value
        End Set
    End Property
    Private m_Date As Date

    Private Property TweetMediaURL As ImageSource
        Get
            Return m_TweetMediaURL
        End Get
        Set(ByVal value As ImageSource)
            m_TweetMediaURL = value
        End Set
    End Property
    Private m_TweetMediaURL As ImageSource

    Public ReadOnly Property TweetIsMention As Boolean
        Get
            Return m_TweetIsMention
        End Get
    End Property
    Private m_TweetIsMention As Boolean

    Public ReadOnly Property TweetContainsLink As Boolean
        Get
            Return m_TweetContainsLink
        End Get
    End Property
    Private m_TweetContainsLink As Boolean
#End Region
End Class