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

Public Class Support_MoreInfo
    Private HasAttachment As Boolean = False
    Private Attachment As String
    Private WithEvents _mailworker As New BackgroundWorker

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button2.Click
        If tb_email.Text = "" Then
            MessageBox.Show("Please enter information in all fields.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
            Exit Sub
        End If
        If tb_message.Text = "" Then
            MessageBox.Show("Please enter information in all fields.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
            Exit Sub
        End If
        If tb_name.Text = "" Then
            MessageBox.Show("Please enter information in all fields.", "myFire", MessageBoxButton.OK, MessageBoxImage.Information)
            Exit Sub
        End If
        Me.Cursor = Cursors.AppStarting
        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New Net.NetworkCredential("noreply@ifxsoftware.net", "FreeFall3")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.ifxsoftware.net"
            mail = New MailMessage()
            mail.From = New MailAddress("noreply@ifxsoftware.net")
            mail.To.Add("support@ifxsoftware.net")
            mail.Subject = "MyFire Crash Report!"
            mail.Body = BodyCalc()
            If HasAttachment = True Then
                mail.Attachments.Add(New Attachment(Attachment))
            End If
            SmtpServer.Send(mail)
            exceptionhandler.InfoMessage("Thank you very much!", "Error report Sent!")
        Catch ex As Exception
            exceptionhandler.WarningMessage(ex.Message, "Error sending report!")
        End Try
        Me.Cursor = Cursors.Arrow
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Dim ofd As New Microsoft.Win32.OpenFileDialog
        ofd.Title = "Add Attachment"
        ofd.Multiselect = False
        Dim FileChosen As Boolean = ofd.ShowDialog
        If FileChosen = True Then
            HasAttachment = True
            Attachment = ofd.FileName
            tb_file.Text = ofd.SafeFileName
        End If
    End Sub

    Private Function BodyCalc() As String
        Dim rb As String
        rb = "Support Number: " & CalculateSupportNumber() & vbNewLine & "Name: " & tb_name.Text & vbNewLine & "Email: " & tb_email.Text & vbNewLine & "Comment: " & tb_message.Text & vbNewLine & ".  Error Log: "
        Dim log As String = ""
        For Each Item As String In My.Settings.support_errorlog
            log = log & Item & vbNewLine
        Next
        rb = rb & log
        Return rb
    End Function

    Private Sub Button3_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button3.Click
        Me.Cursor = Cursors.AppStarting
        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New  _
  Net.NetworkCredential("noreply@ifxsoftware.net", "FreeFall3")
            SmtpServer.Port = 587
            SmtpServer.Host = "smtp.ifxsoftware.net"
            mail = New MailMessage()
            mail.From = New MailAddress("hello@ifxsoftware.net")
            mail.To.Add("support@ifxsoftware.net")
            mail.Subject = "MyFire Crash Report!"
            mail.Body = "Support Number: " & CalculateSupportNumber()
            SmtpServer.Send(mail)
            exceptionhandler.InfoMessage("Thank you very much!", "Error report Sent!")
        Catch ex As Exception
            exceptionhandler.WarningMessage(ex.Message, "Error sending report!")
        End Try
        Me.Cursor = Cursors.Arrow
        Me.Close()
    End Sub

    Function CalculateSupportNumber()
        Dim SU As String
        SU = System.Environment.Version.ToString & "-" & System.Environment.OSVersion.ToString
        Return SU
    End Function

    Private Sub _mailworker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _mailworker.DoWork
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New  _
Net.NetworkCredential("noreply@ifxsoftware.net", "FreeFall3")
        SmtpServer.Port = 587
        SmtpServer.Host = "smtp.ifxsoftware.net"
        mail = New MailMessage()
        mail.From = New MailAddress("hello@ifxsoftware.net")
        mail.To.Add("support@ifxsoftware.net")
        mail.Subject = "MyFire Crash Report!"
        mail.Body = "Support Number: " & CalculateSupportNumber()
        SmtpServer.Send(mail)
    End Sub
End Class
