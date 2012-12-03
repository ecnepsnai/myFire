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
Imports MyFire_storage
Imports Twitterizer
Public Module tokens

    Dim da As New MyFire_storage.Data
    Private su As StoredUser

    ''' <summary>
    ''' Gets the tokens for the default user.
    ''' </summary>
    ''' <returns><see>OAuthTokens</see></returns>
    ''' <remarks></remarks>
    Public Function Tokens() As OAuthTokens
        Try
            su = da.GetDefaultUser
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
            Return Nothing
        End Try
        Dim t As New OAuthTokens
        Try
            t.AccessToken = su.Token
            t.AccessTokenSecret = su.TokenSecret
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
            Return Nothing
        End Try

        ' ############################### '
        ' Enter your Twitter tokens here. '
        ' ############################### '
        t.ConsumerKey = ""
        t.ConsumerSecret = ""
        Return t
    End Function

    ''' <summary>
    ''' Gets the token set for the specific user.
    ''' </summary>
    ''' <param name="ScreenName">The user.</param>
    ''' <returns><see>OAuthTokens</see></returns>
    ''' <remarks></remarks>
    Public Function TokensFromUser(ByVal ScreenName As String) As OAuthTokens
        Dim t As New OAuthTokens
        Dim StoredUserList As List(Of StoredUser) = da.Users_ReturnUserList
        For Each User As StoredUser In StoredUserList
            If User.ScreenName = ScreenName Then
                t.AccessToken = User.Token
                t.AccessTokenSecret = User.TokenSecret

                ' ############################### '
                ' Enter your Twitter tokens here. '
                ' ############################### '
                t.ConsumerKey = ""
                t.ConsumerSecret = ""
            End If
        Next
        Return t
    End Function

    ''' <summary>
    ''' Gets myFire's Bing App ID.
    ''' </summary>
    Public Function BingAppId() As String

        ' ############################# '
        ' TODO: Encrypt the Bing App ID '
        ' ############################# '

        Return My.Resources.BingAppID
    End Function

    ''' <summary>
    ''' Gets the default user from the XML store.
    ''' </summary>
    Public Function GetDefaultUser() As StoredUser
        Try
            Return da.GetDefaultUser
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Gets the screen name of the default user.
    ''' </summary>
    Public Function GetDefaultScreenName() As String
        Try
            Return da.GetDefaultUser.ScreenName
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
            Return Nothing
        End Try
    End Function
End Module
