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
Public Class myFire_manuallink
    Private ReturnToken As OAuthTokenResponse
    Public Function Link(ByVal URL As String) As OAuthTokenResponse
        tb_URL.Text = URL
        Me.ShowDialog()
        Return ReturnToken
    End Function

    Private Sub OK_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles OK.Click
        If tb_oauthverifier.Text = "" Then
            MessageBox.Show("Please enter the oAuth Verifier.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
        End If
        If tb_oauthtoken.Text = "" Then
            MessageBox.Show("Please enter the oAuth Token.", "myFire", MessageBoxButton.OK, MessageBoxImage.Exclamation)
        End If
        Dim respone As OAuthTokenResponse = Nothing
        Try
            respone = OAuthUtility.GetAccessToken("FECrJM3BblYTEFl2ndn9Q", "WbDjmwWndiHEABdsI8GQdhXAK2p5Dp9s4JZIwrnHxVs", tb_oauthtoken.Text, tb_oauthverifier.Text)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
        ReturnToken = respone
    End Sub
End Class
