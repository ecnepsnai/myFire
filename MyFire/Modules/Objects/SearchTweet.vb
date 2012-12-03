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
Public Class SearchTweet
    Public Property TweetSearchObject() As TwitterSearchResult
        Get
            Return m_TwitterSearchResult
        End Get
        Set(Result As TwitterSearchResult)
            SearchText = Result.Text
            SearchUsername = Result.FromUserScreenName
            SearchImageSource = myfireactions.Image_Format(Result.ProfileImageLocation)
            SearchDate = myfireactions.DateConverter(Result.CreatedDate)
            m_TwitterSearchResult = Result
        End Set
    End Property
    Private m_TwitterSearchResult As TwitterSearchResult

    Public Property SearchText As String
        Get
            Return m_SearchText
        End Get
        Set(value As String)
            m_SearchText = value
        End Set
    End Property
    Private m_SearchText As String

    Public Property SearchUsername As String
        Get
            Return m_SearchUsername
        End Get
        Set(value As String)
            m_SearchUsername = value
        End Set
    End Property
    Private m_SearchUsername As String

    Public Property SearchImageSource As ImageSource
        Get
            Return m_SearchImageSource
        End Get
        Set(value As ImageSource)
            m_SearchImageSource = value
        End Set
    End Property
    Private m_SearchImageSource As ImageSource

    Public Property SearchDate As String
        Get
            Return m_SearchDate
        End Get
        Set(value As String)
            m_SearchDate = value
        End Set
    End Property
    Private m_SearchDate As String
End Class
