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
Public Class User
    Property UserObject() As TwitterUser
        Get
            Return m_TwitterUser
        End Get
        Set(User As TwitterUser)
            UserScreenName = User.ScreenName
            UserBio = User.Description
            UserImage = myfireactions.Image_Format(User.ProfileImageLocation)
            m_TwitterUser = User
        End Set
    End Property
    Private m_TwitterUser As TwitterUser

    Property UserScreenName() As String
        Get
            Return m_Text
        End Get
        Set(ByVal value As String)
            m_Text = value
        End Set
    End Property
    Private m_Text As String

    Property UserBio() As String
        Get
            Return m_Username
        End Get
        Set(ByVal value As String)
            m_Username = value
        End Set
    End Property
    Private m_Username As String

    Property UserImage() As ImageSource
        Get
            Return m_Image
        End Get
        Set(ByVal value As ImageSource)
            m_Image = value
        End Set
    End Property
    Private m_Image As ImageSource

    Property UserFollowVisibility() As Visibility
        Get
            Return m_FollowVisibility
        End Get
        Set(value As Visibility)
            m_FollowVisibility = value
        End Set
    End Property
    Private m_FollowVisibility As Visibility = Visibility.Hidden
End Class
