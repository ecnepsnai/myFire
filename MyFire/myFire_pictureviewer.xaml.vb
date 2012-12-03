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
Imports System.Windows.Forms

Public Class myFire_pictureviewer
    Private ImageLink As String
    Public Sub Show_Image(ByVal Image As BitmapImage)
        PictureImage.Width = Image.Width
        PictureImage.Height = Image.Height
        PictureImage.Source = Image
        LinkMenu.Header = Image.UriSource.ToString
        ImageLink = Image.UriSource.ToString
        Me.ShowDialog()
    End Sub
    Public Sub Show_Image(ByVal ImageURL As String)
        Dim bi As New BitmapImage
        bi.BeginInit()
        bi.UriSource = New Uri(ImageURL)
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.EndInit()
        PictureImage.Source = bi
        LinkMenu.Header = ImageURL
        ImageLink = ImageURL
        Me.ShowDialog()
    End Sub

    Private Sub cm1(sender As System.Object, e As System.Windows.RoutedEventArgs)
        My.Computer.Clipboard.Clear()
        My.Computer.Clipboard.SetText(ImageLink)
    End Sub

    Private Sub cm2(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Process.Start(ImageLink)
    End Sub

    Private Sub cm3(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim t As New TweetDialog
        t.Tweet_Format(ImageLink)
    End Sub

    Private Sub cm4(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Dim ofd As New SaveFileDialog
        ofd.Title = "Same Image"
        If ImageLink.ToLower.EndsWith(".jpg") Then
            ofd.Filter = "Image|*.jpg"
        End If
        If ImageLink.ToLower.EndsWith(".png") Then
            ofd.Filter = "Image|*.png"
        End If
        If ImageLink.ToLower.EndsWith(".gif") Then
            ofd.Filter = "Image|*.gif"
        End If
        ofd.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
        Dim response As Boolean = ofd.ShowDialog
        If response = True Then
            Try
                My.Computer.Network.DownloadFile(ImageLink, ofd.FileName)
                InfoMessage("Saved", "myFire")
            Catch ex As Exception
                ExceptionMessage(ex)
            End Try
        End If
    End Sub
End Class
