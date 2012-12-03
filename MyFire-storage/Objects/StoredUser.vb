'* 
'* myFire
'*
'* This file is part of the myFire software
'* Copyright (c) 2012, Ian Spence
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

Public Class StoredUser

    Private _screenname As String
    Private _description As String
    Private _userid As Decimal
    Private _profilepicture As String
    Private _tokensecret As String
    Private _token As String
    Private _defaultaccount As Boolean

#Region "Encryption"
    Private Function Encoding(ByVal Value As String)
        Encryption.Create("DV5JMRtqW1SU,@.'$yO_L5EvnjcC'^b4'*o<9qbWDmq^@d)K.\Y_7IwPx5#=\YJ")
        Dim cipherText As String = Encryption.EncryptData(Value)
        Return cipherText
    End Function

    Private Function Decode(ByVal Value As String, Optional ByVal Password As String = "DV5JMRtqW1SU,@.'$yO_L5EvnjcC'^b4'*o<9qbWDmq^@d)K.\Y_7IwPx5#=\YJ")
        Dim ReturnValue As String
        Dim cipherText As String = Value
        Encryption.Create(Password)

        ' DecryptData throws if the wrong password is used.
        Try
            Dim plainText As String = Encryption.DecryptData(cipherText)
            ReturnValue = plainText
        Catch ex As System.Security.Cryptography.CryptographicException
            ReturnValue = String.Empty
        End Try
        Return ReturnValue
    End Function
#End Region

    Public Sub New(ByVal ScreenName As String, ByVal Description As String, ByVal UserId As Decimal, ByVal ProfilePicture As String, ByVal Token As String, ByVal TokenSecret As String, ByVal DefaultAccount As String)
        Me.ScreenName = ScreenName
        Me.UserDescription = Description
        Me.UserId = UserId
        Me.ProfilePicture = ProfilePicture
        Me.Token = Token
        Me.TokenSecret = TokenSecret
        Me.DefaultAccount = DefaultAccount
    End Sub

    Public Property ScreenName As String
        Get
            Return _screenname
        End Get
        Set(value As String)
            _screenname = value
        End Set
    End Property

    Public Property UserDescription As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
        End Set
    End Property

    Public Property UserId As Decimal
        Get
            Return _userid
        End Get
        Set(value As Decimal)
            _userid = value
        End Set
    End Property

    Public Property ProfilePicture As String
        Get
            Return _profilepicture
        End Get
        Set(value As String)
            _profilepicture = value
        End Set
    End Property

    Public Property Token As String
        Get
            Return Decode(_token)
        End Get
        Set(value As String)
            _token = Encoding(value)
        End Set
    End Property

    Public Property TokenSecret As String
        Get
            Return Decode(_tokensecret)
        End Get
        Set(value As String)
            _tokensecret = Encoding(value)
        End Set
    End Property

    Public Property DefaultAccount As Boolean
        Get
            Return _defaultaccount
        End Get
        Set(value As Boolean)
            _defaultaccount = value
        End Set
    End Property
End Class
