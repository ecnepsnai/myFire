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
Public Class MessageWindow
    Public Sub ShowMessage(ByVal MessageTitle As String, ByVal MessageBody As String, ByVal TopMost As Boolean)
        tb_title.Text = MessageTitle
        l_errormessage.Text = MessageBody
        Me.Topmost = TopMost
        Me.ShowDialog()
    End Sub

    Private Sub OK_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles OK.Click
        Me.DialogResult = True
        Me.Close()
    End Sub

    Private Sub FatalError_KeyUp(sender As System.Object, e As System.Windows.Input.KeyEventArgs) Handles MyBase.KeyUp
        If e.Key = Key.Enter Then
            Me.Close()
        End If
    End Sub
End Class
