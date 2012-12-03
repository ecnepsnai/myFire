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
Public Class mTwitterList
    Public Property TwitterListObject As TwitterList
        Get
            Return _listobject
        End Get
        Set(value As TwitterList)
            TwitterListImage = myfireactions.Image_Format(value.User.ProfileImageLocation)
            TwitterListTitle = value.FullName
            TwitterListDescription = value.Description
            _listobject = value
        End Set
    End Property
    Private _listobject As TwitterList

    Public Property TwitterListImage As ImageSource
        Get
            Return m_TwitterListImage
        End Get
        Set(value As ImageSource)
            m_TwitterListImage = value
        End Set
    End Property
    Private m_TwitterListImage As ImageSource

    Public Property TwitterListTitle As String
        Get
            Return m_TwitterListTitle
        End Get
        Set(value As String)
            m_TwitterListTitle = value
        End Set
    End Property
    Private m_TwitterListTitle As String

    Public Property TwitterListDescription As String
        Get
            Return m_TwitterListDescription
        End Get
        Set(value As String)
            m_TwitterListDescription = value
        End Set
    End Property
    Private m_TwitterListDescription As String
End Class
