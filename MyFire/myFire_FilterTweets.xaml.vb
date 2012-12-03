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
Public Class myFire_FilterTweets

    ' Users 
    Private Sub btn_UserAdd_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_UserAdd.Click
        Dim Response As String = InputBox("Enter the username of the person you wish to add.  Do not include the '@'", "Enter a user.", "")
        If Response = "" Then
            Exit Sub
        End If
        Response = Response.Replace("@", "")
        list_FromUser.Items.Add(Response)
    End Sub

    Private Sub btn_UserRemove_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_UserRemove.Click
        list_FromUser.Items.Remove(list_FromUser.SelectedItem)
    End Sub

    Private Sub list_FromUser_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles list_FromUser.SelectionChanged
        btn_UserRemove.IsEnabled = True
    End Sub

    ' Device
    Private Sub btn_DeviceAdd_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_DeviceAdd.Click
        Dim Response As String = InputBox("Enter the application name you wish to add.  Example: web", "Enter a device.", "")
        If Response = "" Then
            Exit Sub
        End If
        list_FromDevice.Items.Add(Response)
    End Sub

    Private Sub btn_DeviceRemove_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_DeviceRemove.Click
        list_FromDevice.Items.Remove(list_FromDevice.SelectedItem)
    End Sub

    Private Sub list_FromDevice_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles list_FromDevice.SelectionChanged
        btn_DeviceRemove.IsEnabled = True
    End Sub

    ' Word
    Private Sub btn_WordAdd_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_WordAdd.Click
        Dim Response As String = InputBox("Enter the phrase you with to add.  Example: #iPad", "Enter a phrase.", "")
        If Response = "" Then
            Exit Sub
        End If
        list_WithWord.Items.Add(Response)
    End Sub

    Private Sub btn_WordRemove_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles btn_WordRemove.Click
        list_WithWord.Items.Remove(list_WithWord.SelectedItem)
    End Sub

    Private Sub list_WithWord_SelectionChanged(sender As System.Object, e As System.Windows.Controls.SelectionChangedEventArgs) Handles list_WithWord.SelectionChanged
        btn_WordRemove.IsEnabled = True
    End Sub

    Private Sub ApplyBTN_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles ApplyBTN.Click
        My.Settings.Filter_Device.Clear()
        My.Settings.Filter_User.Clear()
        My.Settings.Filter_Word.Clear()
        For Each User As String In list_FromUser.Items
            My.Settings.Filter_User.Add(User)
        Next
        For Each Device As String In list_FromDevice.Items
            My.Settings.Filter_Device.Add(Device)
        Next
        For Each Word As String In list_WithWord.Items
            My.Settings.Filter_Word.Add(Word)
        Next
        My.Settings.Save()
        Me.Close()
    End Sub

    Private Sub Window_Loaded(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        For Each User As String In My.Settings.Filter_User
            list_FromUser.Items.Add(User)
        Next
        For Each Device As String In My.Settings.Filter_Device
            list_FromDevice.Items.Add(Device)
        Next
        For Each Word As String In My.Settings.Filter_Word
            list_WithWord.Items.Add(Word)
        Next
    End Sub
End Class
