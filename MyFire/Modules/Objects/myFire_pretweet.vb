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
Public Class myFire_pretweet
    Private _text As String
    Private _inreplyid As Decimal
    Private _media As String
    Private _geo As Point

    Public Sub New(ByVal TweetText As String, ByVal TweetReplyId As Decimal, ByVal TweetMedia As String, ByVal TweetGeo As Point)
        Text = TweetText
        InReplyToId = TweetReplyId
        Media = TweetMedia
        Geo = TweetGeo
    End Sub

    Public Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    Public Property InReplyToId As Decimal
        Get
            Return _inreplyid
        End Get
        Set(value As Decimal)
            _inreplyid = value
        End Set
    End Property

    Public Property Media As String
        Get
            Return _media
        End Get
        Set(value As String)
            _media = value
        End Set
    End Property

    Public Property Geo As Point
        Get
            Return _geo
        End Get
        Set(value As Point)
            _geo = value
        End Set
    End Property
End Class
