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
Imports MyFire_storage

Public Class TweetDisplayDialog
    Dim Tweet As TwitterStatus
    Dim SourceURL As String
    Private WithEvents h As Hyperlink
    Dim MediaEntity As New List(Of Entities.TwitterMediaEntity)
    Dim LinkEntity As New List(Of Entities.TwitterUrlEntity)
    Dim HashEntity As New List(Of Entities.TwitterHashTagEntity)
    Dim MentionEntity As New List(Of Entities.TwitterMentionEntity)

    Dim myFireEntity As myFire_ImageEntity
    Private Sub Search(ByVal Status As TwitterStatus)
        Tweet = Status
        If Tweet.Entities IsNot Nothing Then
            If Tweet.Entities.Count > 0 Then
                FormatEntities(Tweet.Entities)
            End If
        End If
        TweetText.Document.ContentEnd.InsertTextInRun(Status.Text)
        Dim FlowDocument As FlowDocument = TweetText.Document
        Try
            'Media Entity
            If MediaEntity.Count > 0 Then
                For Each Entity As Entities.TwitterMediaEntity In MediaEntity
                    Dim Start As TextPointer = FlowDocument.ContentStart
                    Dim StartPosition As TextPointer
                    Dim EndPosition As TextPointer
                    If Entity.StartIndex = 0 Then
                        StartPosition = Start.GetPositionAtOffset(Entity.StartIndex)
                    Else
                        StartPosition = Start.GetPositionAtOffset(Entity.StartIndex + 3)
                    End If
                    EndPosition = Start.GetPositionAtOffset(Entity.StartIndex + 3 + Entity.DisplayUrl.Length, LogicalDirection.Backward)
                    Dim h As New Hyperlink(StartPosition, EndPosition)
                    AddHandler h.MouseLeftButtonDown, AddressOf Hyperclick_Link
                    h.NavigateUri = New Uri(Entity.Url)
                    h.Cursor = Cursors.Hand
                    h.ToolTip = "View Image"
                Next
            End If

            'Link Entity
            If LinkEntity.Count > 0 Then
                For Each Entity As Entities.TwitterUrlEntity In LinkEntity
                    Dim Start As TextPointer = FlowDocument.ContentStart
                    Dim StartPosition As TextPointer
                    Dim EndPosition As TextPointer
                    If Entity.StartIndex = 0 Then
                        StartPosition = Start.GetPositionAtOffset(Entity.StartIndex)
                    Else
                        StartPosition = Start.GetPositionAtOffset(Entity.StartIndex + 2)
                    End If
                    EndPosition = Start.GetPositionAtOffset(Entity.StartIndex + 3 + Entity.DisplayUrl.Length, LogicalDirection.Backward)
                    Dim h As New Hyperlink(StartPosition, EndPosition)
                    AddHandler h.MouseLeftButtonDown, AddressOf Hyperclick_Link
                    If My.Settings.SkipTCO = True Then
                        h.NavigateUri = New Uri(Entity.ExpandedUrl)
                    Else
                        h.NavigateUri = New Uri(Entity.Url)
                    End If
                    h.Cursor = Cursors.Hand
                    h.ToolTip = "Go to: " & Entity.DisplayUrl
                Next
            End If

            'HashTag Entity
            If HashEntity.Count > 0 Then
                For Each Entity As Entities.TwitterHashTagEntity In HashEntity
                    Dim Start As TextPointer = FlowDocument.ContentStart
                    Dim StartPosition As TextPointer
                    Dim EndPosition As TextPointer
                    If Entity.StartIndex = 0 Then
                        StartPosition = Start.GetPositionAtOffset(Entity.StartIndex)
                    Else
                        StartPosition = Start.GetPositionAtOffset(Entity.StartIndex + 3)
                    End If
                    EndPosition = Start.GetPositionAtOffset(Entity.StartIndex + 3 + Entity.Text.Length, LogicalDirection.Backward)
                    Dim h As New Hyperlink(StartPosition, EndPosition)
                    AddHandler h.MouseLeftButtonDown, AddressOf Hyperclick_Link
                    h.NavigateUri = New Uri("TREN:" & Entity.Text)
                    h.Cursor = Cursors.Hand
                    h.ToolTip = "View tweets with: #" & Entity.Text
                Next
            End If

            'Mention Entity
            If MentionEntity.Count > 0 Then
                For Each Entity As Entities.TwitterMentionEntity In MentionEntity
                    Dim Start As TextPointer = FlowDocument.ContentStart
                    Dim StartPosition As TextPointer
                    Dim EndPosition As TextPointer
                    Dim Maths1 As Integer = 0

                    StartPosition = Start.GetPositionAtOffset(Entity.StartIndex)
                    EndPosition = Start.GetPositionAtOffset(Entity.StartIndex + 3 + Entity.ScreenName.Length, LogicalDirection.Backward)
                    Dim h As New Hyperlink(StartPosition, EndPosition)
                    AddHandler h.MouseLeftButtonDown, AddressOf Hyperclick_Link
                    h.NavigateUri = New Uri("USER:" & Entity.ScreenName)
                    h.Cursor = Cursors.Hand
                    h.ToolTip = "Go to: @" & Entity.ScreenName
                Next
            End If
        Catch ex As Exception
            'Do nothing!  >:) HAHAHAHHAHAH
        End Try

        TweetText.Document = FlowDocument
        TweetDate.Text = myfireactions.DateConverter(Tweet.CreatedDate) & " ago."
        TweetID.Text = "Tweet number: " & myfireactions.Number_Converter(Tweet.Id, "")
        user_name.Text = "@" & Tweet.User.ScreenName
        TweetUserImage.Source = myfireactions.Image_Format(Tweet.User.ProfileImageLocation)
        UserBackground.ImageSource = myfireactions.Image_Format(Tweet.User.ProfileBackgroundImageLocation)
        Me.Title = Me.Title.Replace("{id}", Tweet.User.ScreenName)
        If Tweet.InReplyToStatusId Is Nothing Then
            InReplyToBtn.Visibility = Windows.Visibility.Collapsed
        Else
            InReplyToBtn.Visibility = Windows.Visibility.Visible
        End If
        If Tweet.Geo Is Nothing Then
            LocationBtn.Visibility = Windows.Visibility.Collapsed
        Else
            LocationBtn.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Public Sub Show_Tweet(ByVal Tweet As TwitterStatus)
        Search(Tweet)
        Me.ShowDialog()
    End Sub

    Public Sub Show_Tweet(ByVal StatusId As Decimal)
        Dim response As TwitterResponse(Of TwitterStatus) = TwitterStatus.Show(tokens.Tokens, StatusId)
        If response.Result <> RequestResult.Success Then
            exceptionhandler.ExceptionMessage(New Exception(response.ErrorMessage))
        End If
        Search(response.ResponseObject)
        Me.ShowDialog()
    End Sub

    Private Sub ProfileImage_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles TweetUserImage.MouseDown
        Dim UserDialog As New UserProfile
        UserDialog.Show_User(Tweet.User.ScreenName)
    End Sub

    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim td As New TweetDialog
        td.Reply_Format(Tweet.User.ScreenName, Tweet.Id)
    End Sub

    Private Sub MenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_Status_Retweet(Tweet.Id)
            exceptionhandler.InfoMessage("Tweet retweeted!", "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub MenuItem_Click_2(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_Status_Favourite(Tweet.Id)
            exceptionhandler.InfoMessage("Tweet favourited!", "myFire")
        Catch ex As Exception
            exceptionhandler.ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub MenuItem_Click_3(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        exceptionhandler.TranslateMessage(Tweet.Text)
    End Sub

    Private Sub InReplyToBtn_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles InReplyToBtn.Click
        Dim td As New TweetDisplayDialog
        td.Show_Tweet(Tweet.InReplyToStatusId)
    End Sub

    Private Sub ViewLocationBTN_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles LocationBtn.Click
        'Dim m As New myFire_MapDialog
        'm.ShowCord(Tweet.Geo.Coordinates.Item(0).Latitude, Tweet.Geo.Coordinates.Item(0).Longitude)
    End Sub

    Private Sub MediaImage_MouseDown(sender As System.Object, e As System.Windows.Input.MouseButtonEventArgs) Handles MediaImage.MouseDown
        Dim p As New myFire_pictureviewer
        If myFireEntity.OpenWebsite = True Then
            Process.Start(myFireEntity.ImageUrl.ToString)
        Else
            p.Show_Image(myFireEntity.Image)
        End If
    End Sub

    Private Sub UserGrid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles UserGrid.MouseDown
        Dim u As New UserProfile
        u.Show_User(Tweet.User)
    End Sub

    Private Sub FormatEntities(ByVal EntityList As Entities.TwitterEntityCollection)
        For Each Entity As Entities.TwitterEntity In EntityList
            Dim EntityType As String = Entity.ToString

            'MediaEntity
            If EntityType = "Twitterizer.Entities.TwitterMediaEntity" Then
                MediaEntity.Add(Entity)
                Dim tme As Entities.TwitterMediaEntity = Entity
                MediaBorder.Visibility = Windows.Visibility.Visible
                myFireEntity = myfireactions.TwitterMediaEntityPhaser(tme)
                MediaImage.Source = myFireEntity.ImageThumbnail
                Exit Sub
            End If

            'URLEntity
            If EntityType = "Twitterizer.Entities.TwitterUrlEntity" Then
                LinkEntity.Add(Entity)
                Dim tme As Entities.TwitterUrlEntity = Entity
                myFireEntity = myfireactions.TwitterURLEntityPhaser(tme.ExpandedUrl)
                If myFireEntity IsNot Nothing Then
                    MediaBorder.Visibility = Windows.Visibility.Visible
                    MediaImage.Source = myFireEntity.ImageThumbnail
                    Exit Sub
                End If
            End If

            'HashEntity
            If EntityType = "Twitterizer.Entities.TwitterHashTagEntity" Then
                HashEntity.Add(Entity)
            End If

            'MentionEntity
            If EntityType = "Twitterizer.Entities.TwitterMentionEntity" Then
                MentionEntity.Add(Entity)
            End If
        Next
    End Sub

    Private Sub tweet_favourite(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_Status_Favourite(Tweet.Id)
            InfoMessage("Tweet favourited!", "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub tweet_retweet(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_Status_Retweet(Tweet.Id)
            InfoMessage("Tweet favourited!", "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub tweet_quote(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            Dim t As New TweetDialog
            t.Tweet_Format("RT @" & Tweet.User.ScreenName & ": " & Tweet.Text)
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub tweet_translate(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            exceptionhandler.TranslateMessage(Tweet.Text)
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub user_report(sender As System.Object, e As System.Windows.RoutedEventArgs)
        Try
            myfireactions.Twitter_User_Report(Tweet.User.Id)
            InfoMessage("User reported.", "myFire")
        Catch ex As Exception
            ExceptionMessage(ex)
        End Try
    End Sub

    Private Sub MenuBtn_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles MenuBtn.Click
        MenuCM.IsOpen = True
    End Sub

    ''' <summary>
    ''' Can block grass?
    ''' </summary>
    Private Sub Hyperclick_Link(sender As Hyperlink, e As RoutedEventArgs)
        Dim NavigationURL As String = sender.NavigateUri.ToString.ToUpper
        If NavigationURL.StartsWith("TREN:") Then
            Dim t As New myFire_TweetSearchList
            t.Search(NavigationURL.Replace("TREN:", ""))
            Exit Sub
        ElseIf NavigationURL.StartsWith("USER:") Then
            Dim u As New UserProfile
            u.Show_User(NavigationURL.Replace("USER:", ""))
            Exit Sub
        Else
            If My.Settings.SkipTCO = True Then
                myfireactions.LoadSiteIfSafe(sender.NavigateUri.ToString)
            Else
                Process.Start(sender.NavigateUri.ToString)
            End If
        End If
    End Sub
End Class
