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
Public Class Support
    Private Sub SupportLabel_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles SupportLabel.MouseDown
        Try
            My.Computer.Clipboard.Clear()
            My.Computer.Clipboard.SetText(myfireactions.CalculateSupportNumber)
            MsgBox("Copied to clipboard!", MsgBoxStyle.Information, "myFire")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Process.Start("http://www.ifxsoftware.net/livezilla/chat.php")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        Process.Start("http://support.ifxsoftware.net/myfire_contact.html")
    End Sub

    Private Sub Support_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If myfireactions.myFire_CheckWindowsVersion = True Then
            Button1.IsEnabled = False
            Button2.IsEnabled = False
            MessageBox.Show("Sorry, but you are using an unsupported version of Windows.  Please try again on a computer running Windows Vista or 7", "Sorry.", MessageBoxButton.OK, MessageBoxImage.Information)
            Label1.Content = ""
        End If
        BuildLabel.Content = BuildLabel.Content.ToString.Replace("{version}", "OFR" & My.Application.Info.Version.ToString)
        SupportLabel.Content = "Support Number: " & myfireactions.CalculateSupportNumber
    End Sub
End Class
