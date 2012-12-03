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
Imports System.Net.Mail
Imports System.ComponentModel
Public Class FatalError

    Private senderex As Exception

    Sub ShowError(ByVal ex As Exception, ByVal DisableSendTo As Boolean)
        l_errormessage.Text = ex.Message
        StackTraceBox.Text = ex.ToString
        If DisableSendTo = True Then
            Button1.IsEnabled = False
        Else
            Button1.IsEnabled = True
        End If
        myfireactions.Beta_ErrorMine(ex)
        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles OK.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        MessageBox.Show("As per the beta software agreement, error information is automatically collected and sent to iFX Software.  This information is completely anonymous.", "myFire Beta.", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub
End Class
