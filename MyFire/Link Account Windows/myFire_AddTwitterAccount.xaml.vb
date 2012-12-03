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
Imports System.Reflection

Public Class myFire_AddTwitterAccount
    Dim ReturnToken As OAuthTokenResponse

    Public Function AddAccount() As Twitterizer.OAuthTokenResponse
        Me.ShowDialog()
        Return ReturnToken
    End Function

    Private Sub myFire_AddTwitterAccount_Loaded(sender As Object, e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim authorizationTokens As New OAuthTokenResponse
        Try
            authorizationTokens = OAuthUtility.GetRequestToken("FECrJM3BblYTEFl2ndn9Q", "WbDjmwWndiHEABdsI8GQdhXAK2p5Dp9s4JZIwrnHxVs", "http://support.ifxsoftware.net/myfire/twitter.html")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        LinkingBrowser.Navigate("http://twitter.com/oauth/authorize?force_login=True&oauth_token=" & authorizationTokens.Token)
    End Sub

    Private Sub LinkingBrowser_LoadCompleted(sender As Object, e As System.Windows.Navigation.NavigationEventArgs) Handles LinkingBrowser.LoadCompleted
        If LinkingBrowser.Source.ToString.Contains("http://support.ifxsoftware.net/myfire/twitter.html?oauth_token=") Then
            Dim testString As String = LinkingBrowser.Source.ToString
            testString = testString.Replace("http://support.ifxsoftware.net/myfire/twitter.html?oauth_token=", "")
            Dim elements() As String = testString.Split("&"c)
            Dim temp_oAuth As String
            Dim temp_oAuthVerifier As String
            temp_oAuth = elements(0)
            temp_oAuthVerifier = elements(1)
            temp_oAuthVerifier = temp_oAuthVerifier.Replace("oauth_verifier=", "")
            ReturnToken = OAuthUtility.GetAccessToken("FECrJM3BblYTEFl2ndn9Q", "WbDjmwWndiHEABdsI8GQdhXAK2p5Dp9s4JZIwrnHxVs", temp_oAuth, temp_oAuthVerifier)
            Me.Close()
        End If
    End Sub

    Private Sub LinkingBrowser_Navigated(sender As System.Object, e As System.Windows.Navigation.NavigationEventArgs) Handles LinkingBrowser.Navigated
        HideScriptErrors(LinkingBrowser, True)
        URLbox.Text = LinkingBrowser.Source.ToString
    End Sub

    Private Sub Window_Closing(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If My.Computer.Keyboard.AltKeyDown = True Then
            e.Cancel = True
            Dim m As New myFire_manuallink
            ReturnToken = m.Link(LinkingBrowser.Source.ToString)
            Me.Close()
        End If
    End Sub

    Public Sub HideScriptErrors(wb As WebBrowser, Hide As Boolean)
        Dim fiComWebBrowser As FieldInfo = GetType(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance Or BindingFlags.NonPublic)
        If fiComWebBrowser Is Nothing Then
            Return
        End If
        Dim objComWebBrowser As Object = fiComWebBrowser.GetValue(wb)
        If objComWebBrowser Is Nothing Then
            Return
        End If
        objComWebBrowser.[GetType]().InvokeMember("Silent", BindingFlags.SetProperty, Nothing, objComWebBrowser, New Object() {Hide})
    End Sub
End Class
