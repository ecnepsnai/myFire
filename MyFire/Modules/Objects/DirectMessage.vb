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
Public Class DirectMessage
    Public Property MessageObject() As TwitterDirectMessage
        Get
            Return m_MessageObject
        End Get
        Set(Message As TwitterDirectMessage)
            MessageText = Message.Text
            MessageUsername = Message.Sender.ScreenName
            MessageDate = myfireactions.DateConverter(Message.CreatedDate)
            MessageImage = myfireactions.Image_Format(Message.Sender.ProfileImageLocation)
            MessageUserObject = Message.Sender
            MessageId = Message.Id
            m_MessageObject = Message
        End Set
    End Property
    Private m_MessageObject As TwitterDirectMessage

    Public Property MessageText() As String
        Get
            Return m_MessageText
        End Get
        Set(value As String)
            m_MessageText = value
        End Set
    End Property
    Private m_MessageText As String

    Public Property MessageUsername As String
        Get
            Return m_MessageUsername
        End Get
        Set(value As String)
            m_MessageUsername = value
        End Set
    End Property
    Private m_MessageUsername As String

    Public Property MessageDate As String
        Get
            Return m_MessageDate
        End Get
        Set(value As String)
            m_MessageDate = value
        End Set
    End Property
    Private m_MessageDate As String

    Public Property MessageImage As ImageSource
        Get
            Return m_MessageImage
        End Get
        Set(value As ImageSource)
            m_MessageImage = value
        End Set
    End Property
    Private m_MessageImage As ImageSource

    Public Property MessageUserObject As TwitterUser
        Get
            Return m_MessageUserObject
        End Get
        Set(value As TwitterUser)
            m_MessageUserObject = value
        End Set
    End Property
    Private m_MessageUserObject As TwitterUser

    Public Property MessageId As Decimal
        Get
            Return m_MessageId
        End Get
        Set(value As Decimal)
            m_MessageId = value
        End Set
    End Property
    Private m_MessageId As Decimal
End Class
