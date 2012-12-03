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
Imports System.ComponentModel
Public Class EditProfile
    Dim da As New MyFire_storage.Data
    Dim ChangeAvatar As String = Nothing
    Dim ChangeBackground As String = Nothing
    Dim Tiled As Boolean
    Dim user As TwitterUser
    Dim MainWindow As MainWindow = Application.Current.Windows.OfType(Of MainWindow).First
    Private WithEvents _profileuploader As New BackgroundWorker
    Private WithEvents _backgrounduploader As New BackgroundWorker

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        UserProfileImage.Source = myfireactions.Image_Format(user.ProfileImageLocation)
        UserBackgroundImage.Source = myfireactions.Image_Format(user.ProfileBackgroundImageLocation)
        NameBox.Text = user.Name
        LocationBox.Text = user.Location
        UrlBox.Text = user.Website
        BioBox.Text = user.Description
        cb_tilebackground.IsChecked = user.IsProfileBackgroundTiled
    End Sub

    Public Function Edit_User(ByVal UserObject As TwitterUser) As TwitterUser
        user = UserObject
        Me.ShowDialog()
        Return user
    End Function

    Private Sub SaveCH_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles SaveCH.Click
        Dim options As New UpdateProfileOptions
        options.Name = NameBox.Text
        options.Description = BioBox.Text
        options.Location = LocationBox.Text
        options.Url = UrlBox.Text
        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.UpdateProfile(tokens.Tokens, options)
        user = response.ResponseObject
        Me.Close()
    End Sub

#Region "Change Avatar"
    Private Sub btn_changeavatar_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_changeavatar.Click
        Dim OFD1 As New Microsoft.Win32.OpenFileDialog
        OFD1.Title = "Select Picture"
        OFD1.Filter = "Images|*.jpg;*.png;*.gif"
        OFD1.InitialDirectory = System.Environment.SpecialFolder.MyPictures
        OFD1.ShowDialog()
        If OFD1.FileName = "" Then
            Exit Sub
        End If
        ChangeAvatar = OFD1.FileName
        btn_changeavatar.Visibility = Windows.Visibility.Collapsed
        btn_applyavatar.Visibility = Windows.Visibility.Visible
        btn_cancelavatar.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub btn_applyavatar_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_applyavatar.Click
        btn_changeavatar.Visibility = Windows.Visibility.Visible
        btn_applyavatar.Visibility = Windows.Visibility.Collapsed
        btn_cancelavatar.Visibility = Windows.Visibility.Collapsed
        btn_changeavatar.Content = "Uploading..."
        btn_changeavatar.IsEnabled = False
        _profileuploader.RunWorkerAsync()
    End Sub

    Private Sub _profileuploader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _profileuploader.DoWork
        Dim Path As String = ChangeAvatar
        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.UpdateProfileImage(tokens.Tokens, Path)
        If response.Result <> RequestResult.Success Then
            exceptionhandler.ExceptionMessage(New Exception(response.ErrorMessage))
            _profileuploader.CancelAsync()
        End If
        Me.Dispatcher.Invoke(Sub()
                                 Dim bi As New BitmapImage
                                 bi.BeginInit()
                                 bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
                                 bi.UriSource = New Uri(ChangeAvatar)
                                 bi.EndInit()
                                 UserProfileImage.Source = bi
                                 user = response.ResponseObject
                             End Sub)
    End Sub

    Private Sub _profileuploader_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _profileuploader.RunWorkerCompleted
        exceptionhandler.InfoMessage("Profile image changed.", "myFire")
        btn_changeavatar.Content = "Change User Image"
        btn_changeavatar.IsEnabled = True
    End Sub

    Private Sub btn_cancelavatar_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_cancelavatar.Click
        btn_changeavatar.Visibility = Windows.Visibility.Visible
        btn_applyavatar.Visibility = Windows.Visibility.Collapsed
        btn_cancelavatar.Visibility = Windows.Visibility.Collapsed
    End Sub
#End Region

#Region "Change Background"
    Private Sub btn_changebackground_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_changebackground.Click
        Dim OFD1 As New Microsoft.Win32.OpenFileDialog
        OFD1.Title = "Select Picture"
        OFD1.Filter = "Images|*.jpg;*.png;*.gif"
        OFD1.InitialDirectory = System.Environment.SpecialFolder.MyPictures
        OFD1.ShowDialog()
        If OFD1.FileName = "" Then
            Exit Sub
        End If
        ChangeBackground = OFD1.FileName
        btn_changebackground.Visibility = Windows.Visibility.Collapsed
        btn_applybackground.Visibility = Windows.Visibility.Visible
        btn_cancelbackground.Visibility = Windows.Visibility.Visible
    End Sub

    Private Sub _backgrounduploader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles _backgrounduploader.DoWork
        Dim Path As String = ChangeBackground
        Dim Tiled As Boolean = Tiled
        Dim o As New UpdateProfileBackgroundImageOptions
        o.Tiled = Tiled
        Dim response As TwitterResponse(Of TwitterUser) = Nothing
        Try
            response = TwitterAccount.UpdateProfileBackgroundImage(tokens.Tokens, Path, o)
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
            _backgrounduploader.CancelAsync()
        End Try
        If response.Result <> RequestResult.Success Then
            exceptionhandler.ExceptionMessage(New Exception(response.ErrorMessage))
            _backgrounduploader.CancelAsync()
        End If
        Me.Dispatcher.Invoke(Sub()
                                 Dim bi As New BitmapImage
                                 bi.BeginInit()
                                 bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
                                 bi.UriSource = New Uri(ChangeBackground)
                                 bi.EndInit()
                                 UserBackgroundImage.Source = bi
                                 user = response.ResponseObject
                             End Sub)
    End Sub

    Private Sub _backgrounduploader_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles _backgrounduploader.RunWorkerCompleted
        exceptionhandler.InfoMessage("Background image changed.", "myFire")
        MainWindow.ContentBackground = myfireactions.Format_BackgroundImage(ChangeAvatar)
        btn_changebackground.Content = "Change Background Image"
        btn_changebackground.IsEnabled = True
    End Sub


    Private Sub btn_applybackground_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_applybackground.Click
        btn_changebackground.Visibility = Windows.Visibility.Visible
        btn_applybackground.Visibility = Windows.Visibility.Collapsed
        btn_cancelbackground.Visibility = Windows.Visibility.Collapsed
        btn_changebackground.Content = "Uploading..."
        _backgrounduploader.RunWorkerAsync()
        btn_changebackground.IsEnabled = False
    End Sub

    Private Sub btn_cancelbackground_Click(sender As Object, e As System.Windows.RoutedEventArgs) Handles btn_cancelbackground.Click
        btn_changebackground.Visibility = Windows.Visibility.Visible
        btn_applybackground.Visibility = Windows.Visibility.Collapsed
        btn_cancelbackground.Visibility = Windows.Visibility.Collapsed
    End Sub
#End Region

    Private Sub cb_tilebackground_Checked(sender As Object, e As System.Windows.RoutedEventArgs) Handles cb_tilebackground.Checked
        Tiled = True
    End Sub

    Private Sub cb_tilebackground_Unchecked(sender As Object, e As System.Windows.RoutedEventArgs) Handles cb_tilebackground.Unchecked
        Tiled = False
    End Sub
End Class
