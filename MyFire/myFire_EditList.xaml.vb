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
Public Class myFire_EditList
    Private listid As String
    Private returnlist As TwitterList = Nothing
    Private IsEdit As Boolean = False
    Public Function CreateList() As TwitterList
        Me.Title = "myFire | Create List"
        Me.ShowDialog()
        Return returnlist
    End Function

    Public Function EditList(ByVal List As TwitterList) As TwitterList
        ListName.Text = List.Name
        ListDescription.Text = List.Description
        PublicList.IsChecked = List.IsPublic
        listid = List.Id
        Me.Title = "myFire | Edit List"
        IsEdit = True
        Me.ShowDialog()
        Return returnlist
    End Function

    Private Sub SaveButton_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles SaveButton.Click
        If IsEdit = True Then
            Dim o As New UpdateListOptions
            o.Name = ListName.Text
            o.Description = ListDescription.Text
            o.IsPublic = PublicList.IsChecked
            Try
                returnlist = myfireactions.Twitter_List_Update(listid, o)
            Catch ex As Exception
                exceptionhandler.ExceptionMessage(ex)
            End Try
            exceptionhandler.InfoMessage("List updated.", "myFire")
            Me.Close()
        Else
            Try
                returnlist = myfireactions.Twitter_List_Create(ListName.Text, PublicList.IsChecked, ListDescription.Text)
            Catch ex As Exception
                exceptionhandler.ExceptionMessage(ex)
            End Try
            exceptionhandler.InfoMessage("List created.", "myFire")
            Me.Close()
        End If
    End Sub
End Class
