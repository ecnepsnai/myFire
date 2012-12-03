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
Imports System.IO
Imports System.Net
Imports System.Windows.Forms
Imports MyFire_storage
Imports Twitterizer
Imports System.ComponentModel
Imports System.Globalization
Imports System.Threading
Imports System.Xml

Module myfireactions
    Private DraftTweet As myFire_pretweet
    Private WithEvents _streamingicon As New NotifyIcon
    Dim da As New MyFire_storage.Data
    Friend WithEvents TweetBackgroundWorker As New BackgroundWorker
    Private bgwkrtkns As OAuthTokens = Nothing

#Region "Custom Exceptions"
    Private TweetTooLongException As New Exception("Tweet text must be no greater than 140 characters.")
    Private MediaTweetTooLongException As New Exception("Tweet text must be no greater than 119 characters.")
    Private MediaNotFoundException As New Exception("The attached media was not found.")
    Private MediaNotRecognized As New Exception("The attached media is not a accepted file type.")
    Private FileTooLargeException As New Exception("The file is too large.")
    Private AlreadySubscribedExcpetion As New Exception("You are already subscribed to this user.")
    Private NotSubscribedException As New Exception("You are not subscribed to this user.")
#End Region

#Region "myFire Methods"
    Function FormatTweet(ByVal Tweet As TwitterStatus, Optional ByVal ExpandTCoLinks As Boolean = True) As String
        Tweet.Text = WebUtility.HtmlDecode(Tweet.Text)
        myFire_exp_autoreport(Tweet)
        If Tweet.Entities IsNot Nothing Then
            If Tweet.Entities.Count > 0 Then
                Dim MediaEntity As Entities.TwitterMediaEntity = Nothing
                Dim LinkEntity As Entities.TwitterUrlEntity = Nothing
                Dim MentionEntity As Entities.TwitterMentionEntity = Nothing
                For Each Entity As Entities.TwitterEntity In Tweet.Entities
                    Dim EntityType As String = Entity.ToString
                    If EntityType = "Twitterizer.Entities.TwitterMediaEntity" Then
                        MediaEntity = Entity
                        If ExpandTCoLinks = True Then
                            Tweet.Text = Tweet.Text.Replace(MediaEntity.Url, MediaEntity.DisplayUrl)
                        End If
                    End If
                    If EntityType = "Twitterizer.Entities.TwitterUrlEntity" Then
                        LinkEntity = Entity
                        If ExpandTCoLinks = True Then
                            Tweet.Text = Tweet.Text.Replace(LinkEntity.Url, LinkEntity.DisplayUrl)
                        End If
                    End If
                Next
            End If
        End If
        Return Tweet.Text
    End Function

    Function HtmlDecode(ByVal Text As String) As String
        Text = WebUtility.HtmlDecode(Text)
        Return Text
    End Function

    Function Format_BackgroundImage(ByVal Source As String) As ImageBrush
        Dim ib As New ImageBrush
        ib.ImageSource = Image_Format(Source)
        Return ib
    End Function

    Function Image_User(ByVal URL As String) As ImageSource
        Dim bi As New BitmapImage
        bi.BeginInit()
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.UriSource = New Uri(URL)
        bi.EndInit()
        Return bi
    End Function

    Function Image_Format(ByVal URL As String, Optional ByVal IncraseSize As Boolean = True) As ImageSource
        Dim bi As New BitmapImage
        If IncraseSize = True Then
            URL = URL.Replace("_normal", "_bigger")
        End If
        bi.BeginInit()
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.UriSource = New Uri(URL)
        bi.EndInit()
        Return bi
    End Function

    Function DateConverter(ByVal ImportDate As Date) As String
        Dim Now As Date = Date.Now
        Dim Timespan As TimeSpan = Now.Subtract(ImportDate)
        Dim returndate As String = ""
        If My.Settings.display_daterelitave = False Then
            Return ImportDate.ToString
            Exit Function
        End If

        If Timespan.Days > 0 Then
            If Timespan.Days = 1 Then
                returndate = Timespan.Days & " day"
            Else
                returndate = Timespan.Days & " days"
            End If
            Return returndate
            Exit Function
        End If

        If Timespan.Hours > 0 Then
            If Timespan.Hours = 1 Then
                returndate = Timespan.Hours & " hour"
            Else
                returndate = Timespan.Hours & " hours"
            End If
            Return returndate
            Exit Function
        End If

        If Timespan.Minutes > 0 Then
            If Timespan.Minutes = 1 Then
                returndate = Timespan.Minutes & " minute"
            Else
                returndate = Timespan.Minutes & " minutes"
            End If
            Return returndate
            Exit Function
        End If

        If Timespan.Seconds > 0 Then
            If Timespan.Seconds = 1 Then
                returndate = Timespan.Seconds & " second"
            Else
                returndate = Timespan.Seconds & " seconds"
            End If
            Return returndate
            Exit Function
        End If
        Return "Just now"
    End Function

    Function Number_Converter(ByVal Number As Long, Optional ByVal NumberType As String = Nothing) As String
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-us")
        Dim MyDouble As Double = Number
        Dim ReturnString As String = MyDouble.ToString("N")
        ReturnString = ReturnString.Replace(".00", "")
        If NumberType IsNot Nothing Then
            ReturnString = ReturnString & " " & NumberType
        Else
            ReturnString = ReturnString
        End If
        Return ReturnString
    End Function

    Function CalculateSupportNumber()
        Dim SU As String
        SU = System.Environment.Version.ToString & "-" & System.Environment.OSVersion.ToString
        Return SU
    End Function

    ''' <summary>
    ''' Checks is the host OS is supported.  If false, do not provide support.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function myFire_CheckWindowsVersion() As Boolean
        Dim OSString As String = Environment.OSVersion.ToString
        If OSString.Contains("Microsoft Windows NT 5.1.2600") Then 'Windows XP SP3
            myFire_CheckWindowsVersion = True
        ElseIf OSString.Contains("Microsoft Windows NT 6.0.6000") Then 'Windows Vista SP2
            myFire_CheckWindowsVersion = False
        ElseIf OSString.Contains("Microsoft Windows NT 6.1.7600") Then 'Windows Vista SP2
            myFire_CheckWindowsVersion = False
        Else
            myFire_CheckWindowsVersion = True
        End If
    End Function

    ''' <summary>
    ''' //EXPIREMENTAL FEATURE//  If the host user does not follow the creator of this tweet.  He will be automatically reported.
    ''' </summary>
    ''' <param name="Tweet">The TwitterStatus containing the mention.</param>
    ''' <remarks>//EXPIREMENTAL FEATURE//</remarks>
    Sub myFire_exp_autoreport(ByVal Tweet As TwitterStatus)
        'If My.Settings.Expiremental_AutoReport = True Then
        '    If Tweet.User.IsFollowing = False Then
        '        If Tweet.Text.Contains("http://") = True Then
        '            Try
        '                myfireactions.Twitter_User_Report(Tweet.User.Id)
        '            Catch ex As Exception
        '                ExceptionMessage(ex)
        '            End Try
        '        End If
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Gets the TopTweets from Twitter.
    ''' </summary>
    ''' <param name="NumberOfStatuses">The number of statuses that can be returned.  Must be larger than 1 and smaller than 200.</param>
    Function Twitter_Explore_TopTweets(ByVal NumberOfStatuses As Integer) As TwitterStatusCollection
        If NumberOfStatuses < 1 Then
            Throw New Exception("NumberOfStatuses cannot be lower than 1.")
        End If
        If NumberOfStatuses > 200 Then
            Throw New Exception("NumberOfStatuses cannot be higher than 200.")
        End If
        Dim o As New UserTimelineOptions
        o.ScreenName = "toptweets"
        o.Count = NumberOfStatuses
        o.IncludeRetweets = True
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.UserTimeline(tokens.Tokens, o)
        Try
            TwitterResultPhaser(response.Result, response.ErrorMessage)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return response.ResponseObject
    End Function

    Sub TwitterResultPhaser(ByVal Result As RequestResult, ByVal ErrorMessage As String)
        If Result = RequestResult.BadRequest Then
            Throw New WebException(ErrorMessage)
        ElseIf Result = RequestResult.ConnectionFailure Then
            Throw New WebException(ErrorMessage)
        ElseIf Result = RequestResult.FileNotFound Then
            Throw New FileNotFoundException(ErrorMessage)
        ElseIf Result = RequestResult.NotAcceptable Then
            Throw New Exception(ErrorMessage)
        ElseIf Result = RequestResult.RateLimited Then
            Throw New WebException(ErrorMessage)
        End If
    End Sub

    Function CurrentTime(Optional ByVal OpeningString As String = "") As String
        Return OpeningString & Date.Now.ToString
    End Function

#Region "Greetings"
    Private SupString(20)
    Function Tweet_Title_Format(ByVal ScreenName As String) As String
        SupString(0) = "Yo " & ScreenName & ", What's happening?"
        SupString(1) = "What’s up?"
        SupString(2) = "How are you?"
        SupString(3) = "What’s going on?"
        SupString(4) = "What’s up, buttercup?"
        SupString(5) = "What’s the word, humming bird?"
        SupString(6) = "What’s the gist, Physicist?"
        SupString(7) = "What’s on your mind?"
        SupString(8) = "Sup?"
        SupString(9) = "Tell me about it!"
        SupString(10) = "Tell me a story?"
        SupString(11) = "Remember that time..."
        SupString(12) = "Remember that awkward moment when..."
        SupString(13) = "Would you like to talk about it?"
        SupString(14) = "How does that make you feel?"
        SupString(15) = "What's up, " & ScreenName & "?"
        SupString(16) = "Is sand really overpowered?"
        SupString(17) = "How you doin?"
        SupString(18) = "What's your favourite TV show?"
        SupString(19) = "How are you doing, " & ScreenName & "?"
        SupString(20) = "What did you do today, " & ScreenName & "?"
        Return SupString(RandomNumber(20, 0))
    End Function

    Function Window_Title_Format(ByVal ScreenName As String) As String
        If Date.Now.Hour < 12 Then
            Return "Good morning, " & ScreenName
        ElseIf Date.Now.Hour < 17 Then
            Return "Good afternoon, " & ScreenName
        Else
            Return "Good evening, " & ScreenName
        End If
    End Function

    Private Function RandomNumber(ByVal rUpper As Integer, Optional ByVal rLower As Integer = 0) As Integer
        Randomize()
        RandomNumber = Int((rUpper - rLower + 1) * Rnd() + rLower)
    End Function
#End Region

    Sub Beta_DataMine()
        Dim exeVersion As String = My.Application.Info.Version.ToString
        Dim osVersion As String = My.Computer.Info.OSVersion
        Dim osArcType As String = My.Computer.Info.OSPlatform
        Dim osNetVersion As String = System.Environment.Version.ToString

        Dim URL As String = "http://ecnepsnai.com/myFire/datamine.php?exeVersion=" & exeVersion & "&osVersion=" & osVersion & "&osArcType=" & osArcType & "&osNetVersion=" & osNetVersion
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString(URL)
    End Sub

    Sub Beta_ErrorMine(ByVal ex As Exception)
        'Dim exeVersion As String = My.Application.Info.Version.ToString
        'Dim osVersion As String = My.Computer.Info.OSVersion
        'Dim errMessage As String = ex.Message
        'Dim errStackTrace As String = ex.ToString

        'Dim URL As String = "http://ecnepsnai.com/myFire/error.php?exeVersion=" & exeVersion & "&osVersion=" & osVersion & "&errMessage=" & errMessage & "&errStackTrace=" & errStackTrace
        'Dim webClient As New System.Net.WebClient
        'Dim result As String = webClient.DownloadString(URL)
    End Sub

    ''' <summary>
    ''' Loads the URL if the site is safe.  Not for use with t.co links.
    ''' </summary>
    Sub LoadSiteIfSafe(ByVal url As String)
        If IsSiteSafe(url) = False Then
            Dim w As New myFire_unsafelink
            w.ShowWarning(url)
        Else
            Process.Start(url)
        End If
    End Sub

#End Region

#Region "Image Methods"

    Function TwitterMediaEntityPhaser(ByVal MediaEntity As Entities.TwitterMediaEntity) As myFire_ImageEntity
        Dim ImageEntity As New myFire_ImageEntity(MediaEntity.MediaUrl, MediaEntity.MediaUrl, False)
        Return ImageEntity
    End Function

    Function TwitterURLEntityPhaser(ByVal Url As String) As myFire_ImageEntity
        'TwitPicImage
        If Url.ToLower.Contains("twitpic.com") = True Then
            Dim thumb As String = Url
            Dim image As String = Url
            thumb = thumb.Replace("http://twitpic.com/", "http://twitpic.com/show/mini/")
            image = image.Replace("http://twitpic.com/", "")
            image = "http://twitpic.com/show/full/" & image & ".jpg"
            Dim ImageEntity As New myFire_ImageEntity(image, thumb, True)
            Return ImageEntity
            Exit Function
        End If

        'yFrogImage
        If Url.ToLower.Contains("yfrog.com") = True Then
            Dim thumb As String = Url & ":small"
            Dim ImageEntity As New myFire_ImageEntity(thumb, "", True)
            Return ImageEntity
            Exit Function
        End If

        'YouTubeImage
        If Url.ToLower.Contains("youtube.com") = True Then
            Dim ImageURL As String = Url
            If ImageURL.StartsWith("http://www.youtube.com/watch?v=") = False Then
                ImageURL = "http://img.youtube.com/vi/ID/1.jpg"
            Else
                ImageURL = ImageURL.Replace("http://www.youtube.com/watch?v=", "")
                ImageURL = "http://img.youtube.com/vi/ID/" & ImageURL & ".jpg"
            End If
            Dim ImageEntity As New myFire_ImageEntity(ImageURL, "", True)
            Return ImageEntity
            Exit Function
        End If

        '.jpgLink
        If Url.ToLower.EndsWith(".jpg") = True Then
            Dim ImageURL As String = Url
            Dim ImageEntity As New myFire_ImageEntity(ImageURL, "", False)
            Return ImageEntity
            Exit Function
        End If

        '.pngLink
        If Url.ToLower.EndsWith(".png") = True Then
            Dim ImageURL As String = Url
            Dim ImageEntity As New myFire_ImageEntity(ImageURL, "", False)
            Return ImageEntity
            Exit Function
        End If

        Return Nothing
    End Function
#End Region

#Region "Command Methods"
    ''' <summary>
    ''' Tweetbox commands
    ''' </summary>
    ''' <param name="CommandChunk">The command and parameters</param>
    ''' <remarks></remarks>
    Sub myFire_Commands(ByVal CommandChunk As String)
        If CommandChunk.StartsWith("//reset") Then

        ElseIf CommandChunk.StartsWith("//asynctweet") Then
            Dim var1 As String = CommandChunk.Replace("//asynctweet ", "")
            If var1.Contains("true") Then
                My.Settings.myFire_TweetInBackground = True
                My.Settings.Save()
            ElseIf var1.Contains("false") Then
                My.Settings.myFire_TweetInBackground = False
                My.Settings.Save()
            Else
                Throw New Exception("Syntax Error")
            End If
        End If
    End Sub
#End Region

#Region "Network Methods"

    ''' <summary>
    ''' Gets a list of advertisement sources from the web server.
    ''' </summary>
    Function GetAdvertisementSources() As List(Of String)
        Dim r As New List(Of String)
        If My.Computer.Network.IsAvailable = False Then
            Throw New System.Net.WebException("Could not establish a connection to ecnepsnai.com")
        End If
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString("http://ecnepsnai.com/myFire/ads.xml")
        If String.IsNullOrEmpty(result) Then
            Throw New System.Net.WebException("No data returned from the source.")
            Exit Function
        End If

        Dim doc As New XmlDocument
        doc.LoadXml(result)
        Dim AdvertisingSources As XmlNode = doc.SelectSingleNode("myFire/AdvertisingSources")
        Dim SN As XmlNodeList = AdvertisingSources.ChildNodes
        For Each Source As XmlNode In SN
            r.Add(Source.InnerText)
        Next
        Return r
    End Function

    ''' <summary>
    ''' Gets the update result for the specified or current version.
    ''' </summary>
    Function GetUpdateResult(ByVal version As String) As String
        If My.Computer.Network.IsAvailable = False Then
            Throw New System.Net.WebException("Could not establish a connection to ecnepsnai.com")
        End If
        Dim webClient As New System.Net.WebClient
        Dim result As String = "Error"
        Try
            result = webClient.DownloadString("http://ecnepsnai.com/myFire/update.php?token=myFire" & version)
        Catch ex As Exception
            Throw ex
        End Try
        If result.Contains("Error") Then
            Throw New Exception(result)
            Exit Function
        End If
        Return result
    End Function

    ''' <summary>
    ''' Gets the GoogleSafeSearch result for the URL.  True meaning site is safe.
    ''' </summary>
    ''' <param name="url">The URL in string form of the site.</param>
    ''' <returns>A <see>Boolean</see> defining the result.</returns>
    ''' <remarks></remarks>
    Function IsSiteSafe(ByVal url As String) As Boolean
        Dim webClient As New System.Net.WebClient
        Dim result As String
        Try
            result = webClient.DownloadString("https://sb-ssl.google.com/safebrowsing/api/lookup?client=api&apikey=ABQIAAAASypbx66JfApT5XqbxVuRFxQME5tB7b9WxORTXdpDhu-Wk49FGg&appver=1.0&pver=3.0&url=" & url)
        Catch ex As Exception
            Return True
        End Try
        If result = "phishing" Then
            Return False
        ElseIf result = "malware" Then
            Return False
        ElseIf result = "phishing,malware" Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

    ''

#Region "Twitter Methods"
    '
    ' All Twitter methods must be called through myFireActions to allow for easy changes.
    ' Only exceptions are Account Linking, and editing.
    '

#Region "Twitter_Status"
    ''' <summary>
    ''' Posts a tweet to the authorized users timeline.
    ''' </summary>
    ''' <param name="Text">The tweet text.</param>
    ''' <param name="InReplyToId">The ID of the status this tweet is replying to.</param>
    ''' <param name="Path">An absolute path to an Image file.</param>
    ''' <param name="glat">The latitude of the user.</param>
    ''' <param name="glon">The longitude of the user.</param>
    ''' <remarks>Tweet text must not be greater than 140 characters.</remarks>
    Sub Twitter_Status_Update(ByVal Text As String, Optional ByVal InReplyToId As Decimal = -1, Optional ByVal Path As String = "", Optional ByVal glat As Decimal = -1, Optional ByVal glon As Decimal = -1)
        If My.Settings.myFire_TweetInBackground = True Then
            DraftTweet = New myFire_pretweet(Text, InReplyToId, Path, New System.Windows.Point(glat, glon))
            bgwkrtkns = tokens.Tokens
            TweetBackgroundWorker.Dispose()
            TweetBackgroundWorker.RunWorkerAsync()
        Else
            'Initial check to see if tweet is too long
            If Path Is Nothing Then
                If Text.Length > 140 Then
                    Throw TweetTooLongException
                    Exit Sub
                End If
            Else
                If Path IsNot Nothing And Text.Length > 119 Then
                    Throw MediaTweetTooLongException
                    Exit Sub
                End If
            End If

            Dim o As New StatusUpdateOptions

            'Adds the InReplyToId if it is not default.
            If Not InReplyToId = -1 Then
                o.InReplyToStatusId = InReplyToId
            End If

            'Adds location information to the tweet.
            If Not glat = -1 Then
                If Not glon = -1 Then
                    o.Latitude = glat
                    o.Longitude = glon
                End If
            End If

            'Posts a media tweet.
            If Path IsNot Nothing Then
                'Check to see if the file exists
                If System.IO.File.Exists(Path) = False Then
                    Throw MediaNotFoundException
                    Exit Sub
                End If

                'Check to see if the filetype is supported.
                If Path.ToUpper.EndsWith(".JPG") Then
                    'File Supported!
                ElseIf Path.ToUpper.EndsWith(".PNG") Then
                    'File Supported!
                ElseIf Path.ToUpper.EndsWith(".GIF") Then
                    'File Supported!
                Else
                    Throw MediaNotRecognized
                    Exit Sub
                End If

                Dim MediaResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.UpdateWithMedia(tokens.Tokens, Text, Path, o)
                Try
                    TwitterResultPhaser(MediaResponse.Result, MediaResponse.ErrorMessage)
                    Exit Sub
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "myFire", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            'Sends normal tweet.
            Dim TextResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.Update(tokens.Tokens, Text, o)
            Try
                TwitterResultPhaser(TextResponse.Result, TextResponse.ErrorMessage)
                Exit Sub
            Catch ex As Exception
                MessageBox.Show(ex.Message, "myFire", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

#Region "Background Worker"
    Private Sub TweetBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles TweetBackgroundWorker.DoWork
        Dim Tweet_Text As String = DraftTweet.Text
        Dim Tweet_InReplyId As Decimal = DraftTweet.InReplyToId
        Dim Tweet_Path As String = DraftTweet.Media
        Dim Tweet_geo_lat As Decimal = DraftTweet.Geo.X
        Dim Tweet_geo_lon As Decimal = DraftTweet.Geo.Y
        Dim tokens As OAuthTokens = bgwkrtkns

        'Initial check to see if tweet is too long
        If Tweet_Path Is Nothing Then
            If Tweet_Text.Contains("http://") = False Then
                If Tweet_Text.Length > 140 Then
                    Throw TweetTooLongException
                    Exit Sub
                End If
            End If
        Else
            If Tweet_Path IsNot Nothing And Tweet_Text.Length > 119 Then
                Throw MediaTweetTooLongException
                Exit Sub
            End If
        End If

        Dim o As New StatusUpdateOptions

        'Adds the InReplyToId if it is not default.
        If Not Tweet_InReplyId = -1 Then
            o.InReplyToStatusId = Tweet_InReplyId
        End If

        'Adds location information to the tweet.
        If Not Tweet_geo_lat = -1 Then
            If Not Tweet_geo_lon = -1 Then
                o.Latitude = Tweet_geo_lat
                o.Longitude = Tweet_geo_lon
            End If
        End If

        'Posts a media tweet.
        If Tweet_Path IsNot Nothing Then
            'Check to see if the file exists
            If System.IO.File.Exists(Tweet_Path) = False Then
                Throw MediaNotFoundException
                Exit Sub
            End If

            'Check to see if the filetype is supported.
            If Tweet_Path.ToUpper.EndsWith(".JPG") Then
                'File Supported!
            ElseIf Tweet_Path.ToUpper.EndsWith(".PNG") Then
                'File Supported!
            ElseIf Tweet_Path.ToUpper.EndsWith(".GIF") Then
                'File Supported!
            Else
                Throw MediaNotRecognized
                Exit Sub
            End If

            Dim MediaResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.UpdateWithMedia(tokens, Tweet_Text, Tweet_Path, o)
            If MediaResponse.Result <> RequestResult.Success Then
                Throw New Exception(MediaResponse.ErrorMessage)
            Else
                Exit Sub
            End If
        End If

        'Sends normal tweet.
        Try
            Dim TextResponse As TwitterResponse(Of TwitterStatus) = TwitterStatus.Update(tokens, Tweet_Text, o)
            If TextResponse.Result <> RequestResult.Success Then
                Throw New Exception(TextResponse.ErrorMessage)
            Else
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
#End Region

    ''' <summary>
    ''' Deletes the specified tweet.
    ''' </summary>
    ''' <param name="TweetID">The id of the Tweet you want to delete.</param>
    ''' <remarks>Must be a tweet from the authenticated user.</remarks>
    Sub Twitter_Status_Delete(ByVal TweetID As Decimal)
        Dim Response As TwitterResponse(Of TwitterStatus) = TwitterStatus.Delete(tokens.Tokens, TweetID)
        Try
            TwitterResultPhaser(Response.Result, Response.ErrorMessage)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "myFire", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Adds the specified Tweet to the authenticated users favourite list.
    ''' </summary>
    ''' <param name="TweetID">The ID of the Tweet you wish to favourite.</param>
    ''' <remarks></remarks>
    Sub Twitter_Status_Favourite(ByVal TweetID As Decimal)
        Dim response As TwitterResponse(Of TwitterStatus) = TwitterFavorite.Create(tokens.Tokens, TweetID)
        Try
            TwitterResultPhaser(response.Result, response.ErrorMessage)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "myFire", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Retweets the specified Tweet.
    ''' </summary>
    ''' <param name="TweetID">The ID of the Tweet you wish to retweet.</param>
    ''' <remarks>The user of the Tweet must not be protected.</remarks>
    Sub Twitter_Status_Retweet(ByVal TweetID As Decimal)
        Dim response As TwitterResponse(Of TwitterStatus) = TwitterStatus.Retweet(tokens.Tokens, TweetID)
        Try
            TwitterResultPhaser(response.Result, response.ErrorMessage)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "myFire", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the Tweet associated with the ID.
    ''' </summary>
    ''' <param name="TweetID">The ID you wish to lookup.</param>
    ''' <returns>A TwitterStatus object of the Tweet ID</returns>
    ''' <remarks></remarks>
    Function Twitter_Status_Show(ByVal TweetID As Decimal) As TwitterStatus
        Dim response As TwitterResponse(Of TwitterStatus) = TwitterStatus.Show(tokens.Tokens, TweetID)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Translates the tweet into English.
    ''' </summary>
    ''' <param name="Text">The tweet.</param>
    ''' <returns>A translated tweet in English.</returns>
    ''' <remarks></remarks>
    Function Twitter_Status_Translate(ByVal Text As String) As String

        ' TODO: Update translation methods '

        Return Text

        'Try
        '    Dim BAPI As New BingAPI.Translate
        '    Return BAPI.TranslateText(Text, "", "en")
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Function

    ''' <summary>
    ''' Returns true if the tweet passes the filter check.  If false do not show the tweet.
    ''' </summary>
    ''' <param name="Tweet">The status.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Twitter_Status_FilterTweet(ByVal Tweet As TwitterStatus) As Boolean
        Dim ReturnValue As Boolean = True
        If My.Settings.Filter_User.Contains(Tweet.User.ScreenName) = True Then
            ReturnValue = False
        End If
        If My.Settings.Filter_Word.Contains(Tweet.Text) = True Then
            ReturnValue = False
        End If
        For Each Source As String In My.Settings.Filter_Device
            If Tweet.Source.Contains(Source) = True Then
                ReturnValue = False
            End If
        Next
        If My.Settings.Filter_Spam = True Then
            For Each Source As String In My.Settings.Spam_KnownAds
                If Tweet.Source.Contains(Source) = True Then
                    ReturnValue = False
                End If
            Next
        End If
        Return ReturnValue
    End Function
#End Region

#Region "Twitter_Search"
    ''' <summary>
    ''' Searches TwitterStatuses containing the query.
    ''' </summary>
    ''' <param name="query">The search query.</param>
    ''' <returns>A collection of twitter search results.</returns>
    ''' <remarks></remarks>
    Function Twitter_Search_Tweets(ByVal query As String) As TwitterSearchResultCollection
        Dim o As New SearchOptions
        o.NumberPerPage = 50
        Dim response As TwitterResponse(Of TwitterSearchResultCollection) = TwitterSearch.Search(query, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            For Each Result As TwitterSearchResult In response.ResponseObject
                Result.Text = HtmlDecode(Result.Text)
            Next
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Searches the twitter user collection for the specified query.
    ''' </summary>
    ''' <param name="query">The search query.</param>
    ''' <returns>A collection on TwitterUsers.</returns>
    ''' <remarks></remarks>
    Function Twitter_Search_Users(ByVal query As String) As TwitterUserCollection
        Dim o As New UserSearchOptions
        o.NumberPerPage = 50
        Dim response As TwitterResponse(Of TwitterUserCollection) = TwitterUser.Search(tokens.Tokens, query, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function
#End Region

#Region "Twitter_User"
    '
    ' All Twitter_User methods require a UserId, screen name is not supported.
    '

    ''' <summary>
    ''' Gets the TwitterUser associated with the ID.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <returns>A <see>TwitterUser</see> object.</returns>
    ''' <remarks></remarks>
    Function Twitter_User_Lookup(ByVal UserId As Decimal) As TwitterUser
        Dim response As TwitterResponse(Of TwitterUser) = TwitterUser.Show(tokens.Tokens, UserId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Reports the specified user to the TwitterSpam team.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_User_Report(ByVal UserId As Decimal)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterSpam.ReportUser(tokens.Tokens, UserId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Returns a list of Users the the specified user follows.
    ''' </summary>
    ''' <param name="UserId">The User Id.</param>
    ''' <returns>A list of TwitterUser.</returns>
    ''' <remarks></remarks>
    Function Twitter_User_Friends(ByVal UserId As Decimal) As TwitterUserCollection
        Dim o As New UsersIdsOptions
        o.UserId = UserId
        Dim UserIDList As TwitterResponse(Of UserIdCollection) = TwitterFriendship.FriendsIds(tokens.Tokens, o)
        If UserIDList.ResponseObject.Count = 0 Then
            Return Nothing
            Exit Function
        End If
        Dim p As New LookupUsersOptions
        p.UserIds = UserIDList.ResponseObject
        Dim UserScreenNameList As TwitterResponse(Of TwitterUserCollection) = TwitterUser.Lookup(tokens.Tokens, p)
        Return UserScreenNameList.ResponseObject
    End Function

    ''' <summary>
    ''' Returns a list of Users that follow the specified user.
    ''' </summary>
    ''' <param name="UserId">The User Id.</param>
    ''' <returns>A list of TwitterUser.</returns>
    ''' <remarks></remarks>
    Function Twitter_User_Followers(ByVal UserId As Decimal) As TwitterUserCollection
        Dim o As New FollowersOptions
        o.UserId = UserId
        Dim response As TwitterResponse(Of TwitterUserCollection) = TwitterFriendship.Followers(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function
#End Region

#Region "Twitter_SuggestedUser"
    Function Twitter_SuggestedUsers() As TwitterUserCategoryCollection
        Dim response As TwitterResponse(Of TwitterUserCategoryCollection) = TwitterUserCategory.SuggestedUserCategories(tokens.Tokens)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function

    Function Twitter_SuggestedUsersList(ByVal Slug As String) As TwitterUserCollection
        Dim response As TwitterResponse(Of TwitterUserCategory) = TwitterUserCategory.SuggestedUsers(tokens.Tokens, Slug)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject.Users
    End Function
#End Region

#Region "Twitter_Friendship"
    ''' <summary>
    ''' Follows the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_Friendship_Create(ByVal UserId As Decimal)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterFriendship.Create(tokens.Tokens, UserId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Unfollows the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_Friendship_Delete(ByVal UserId As Decimal)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterFriendship.Delete(tokens.Tokens, UserId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Follows the specified user.
    ''' </summary>
    ''' <param name="ScreenName">The ScreenName of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_Friendship_Create(ByVal ScreenName As String)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterFriendship.Create(tokens.Tokens, ScreenName)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Unfollows the specified user.
    ''' </summary>
    ''' <param name="ScreenName">The ScreenName of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_Friendship_Delete(ByVal ScreenName As String)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterFriendship.Delete(tokens.Tokens, ScreenName)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Does this user follow me?
    ''' </summary>
    ''' <param name="ScreenName">The Users ScreenName</param>
    ''' <returns>A boolean.</returns>
    ''' <remarks></remarks>
    Function Twitter_Friendship_FollowsMe(ByVal ScreenName As String) As Boolean
        Dim response As TwitterResponse(Of TwitterRelationship) = TwitterFriendship.Show(tokens.Tokens, tokens.GetDefaultScreenName, ScreenName)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
            Exit Function
        End If
        Return response.ResponseObject.Target.FollowedBy
    End Function

    ''' <summary>
    ''' Do I follow this user>
    ''' </summary>
    ''' <param name="ScreenName">The Users ScreenName</param>
    ''' <returns>A boolean.</returns>
    ''' <remarks></remarks>
    Function Twitter_Friendship_Following(ByVal ScreenName As String) As Boolean
        Dim response As TwitterResponse(Of TwitterRelationship) = TwitterFriendship.Show(tokens.GetDefaultScreenName, ScreenName)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
            Exit Function
        End If
        Return response.ResponseObject.Source.Following
    End Function
#End Region

#Region "Twitter_Favourite"
    ''' <summary>
    ''' Returns the 20 most recent favorite statuses for the authenticating user or user specified by the ID parameter in the requested format.
    ''' </summary>
    ''' <param name="UserId">The ID or screen name of the user for whom to request a list of favorite statuses.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Twitter_Favourites(ByVal UserId As Decimal) As TwitterStatusCollection
        Dim o As New ListFavoritesOptions
        o.UserNameOrId = UserId
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterFavorite.List(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function
#End Region

#Region "Twitter_List"
    ''' <summary>
    ''' Returns the lists that the authenticated user follows, including private lists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Twitter_List_Get() As TwitterListCollection
        Dim response As TwitterResponse(Of TwitterListCollection) = TwitterList.GetLists(tokens.Tokens)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function

    ''' <summary>
    ''' Returns all lists the authenticating or specified user subscribes to, including their own.
    ''' </summary>
    Function Twitter_List_Get_All() As TwitterListCollection
        Dim response As TwitterResponse(Of TwitterListCollection) = TwitterList.GetAllLists(tokens.Tokens)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function

    ''' <summary>
    ''' Creates a new list for the authenticated user. Note that you can't create more than 20 lists per account.
    ''' </summary>
    ''' <param name="ListName">The name for the list.</param>
    ''' <param name="IsPublic">Whether your list is public or private. Values can be public or private. If no mode is specified the list will be public.</param>
    ''' <param name="Description">The description to give the list.</param>
    Function Twitter_List_Create(ByVal ListName As String, ByVal IsPublic As Boolean, Optional ByVal Description As String = "") As TwitterList
        Dim response As TwitterResponse(Of TwitterList) = TwitterList.[New](tokens.Tokens, ListName, IsPublic, Description, Nothing)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function

    ''' <summary>
    ''' Deletes the specified list. The authenticated user must own the list to be able to destroy it.
    ''' </summary>
    ''' <param name="ListId">The numerical id of the list.</param>
    Sub Twitter_List_Destroy(ByVal ListId As Decimal)
        Dim response As TwitterResponse(Of TwitterList) = TwitterList.Delete(tokens.Tokens, tokens.GetDefaultScreenName, ListId, Nothing)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
    End Sub

    ''' <summary>
    ''' Updates the specified list. The authenticated user must own the list to be able to update it.
    ''' </summary>
    ''' <param name="ListId">The numerical id of the list.</param>
    Function Twitter_List_Update(ByVal ListId As Decimal, ByVal ListOptions As UpdateListOptions) As TwitterList
        Dim response As TwitterResponse(Of TwitterList) = TwitterList.Update(tokens.Tokens, ListId, ListOptions)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function

    ''' <summary>
    ''' Returns the members of the specified list. Private list members will only be shown if the authenticated user owns the specified list.
    ''' </summary>
    ''' <param name="ListId">The numerical id of the list.</param>
    Function Twitter_List_Members(ByVal ListId As Decimal)
        Dim response As TwitterResponse(Of TwitterUserCollection) = TwitterList.GetMembers(tokens.Tokens, tokens.GetDefaultScreenName, ListId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function

    ''' <summary>
    ''' Add a member to a list. The authenticated user must own the list to be able to add members to it. Note that lists can't have more than 500 members.
    ''' </summary>
    ''' <param name="ListId">The numerical id of the list.</param>
    ''' <param name="ScreenName">The Screen Name of the user.</param>
    Sub Twitter_List_Members_Create(ByVal ListId As Decimal, ByVal ScreenName As String)
        Dim response As TwitterResponse(Of TwitterList) = TwitterList.AddMember(tokens.Tokens, ListId, ScreenName)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
    End Sub

    ''' <summary>
    ''' Removes the specified member from the list. The authenticated user must be the list's owner to remove members from the list.
    ''' </summary>
    ''' <param name="ListId">The numerical id of the list.</param>
    ''' <param name="ScreenName">The Screen Name of the user.</param>
    Sub Twitter_List_Members_Destroy(ByVal ListId As Decimal, ByVal ScreenName As String)
        Dim response As TwitterResponse(Of TwitterList) = TwitterList.RemoveMember(tokens.Tokens, ListId, ScreenName)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
    End Sub

    ''' <summary>
    ''' Gets the statuses from the twitter list.
    ''' </summary>
    ''' <param name="ListId">The ListID</param>
    ''' <param name="ScreenName">The screen name of the user.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Twitter_List_Tweets(ByVal ListId As Decimal, ByVal ScreenName As String) As TwitterStatusCollection
        Dim o As New Twitterizer.ListStatusesOptions
        o.IncludeEntites = True
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterList.GetStatuses(tokens.Tokens, ScreenName, ListId, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function
#End Region

#Region "Twitter_Block"
    ''' <summary>
    ''' Blocks the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_Block_Create(ByVal UserId As Decimal)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterBlock.Create(tokens.Tokens, UserId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Unblocks the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <remarks></remarks>
    Sub Twitter_Block_Destroy(ByVal UserId As Decimal)
        Dim response As TwitterResponse(Of TwitterUser) = TwitterBlock.Destroy(tokens.Tokens, UserId)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub
#End Region

#Region "Twitter_Timeline"
    ''' <summary>
    ''' Gets the home timeline for the authenticated user.
    ''' </summary>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_Home() As TwitterStatusCollection
        Dim o As New TimelineOptions
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.HomeTimeline(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Gets the mentions timeline for the authenticated user.
    ''' </summary>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_Mentions() As TwitterStatusCollection
        Dim o As New TimelineOptions
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.Mentions(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Gets the timeline of the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_User(ByVal UserId As String) As TwitterStatusCollection
        Dim o As New UserTimelineOptions
        o.UserId = UserId
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.UserTimeline(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Try
                Return response.ResponseObject
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Gets the mentions of the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_User_Mentions(ByVal UserId As String) As TwitterStatusCollection
        Dim o As New UserTimelineOptions
        o.UserId = UserId
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.Mentions(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Gets the favourites of the specified user.
    ''' </summary>
    ''' <param name="UserId">The UserId of the user.</param>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_User_Favourites(ByVal UserId As Decimal) As TwitterStatusCollection
        Dim o As New ListFavoritesOptions
        o.UserNameOrId = UserId
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterFavorite.List(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Gets the public timeline.
    ''' </summary>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_Public() As TwitterStatusCollection
        Dim o As New TimelineOptions
        o.Count = My.Settings.api_statuscount
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.PublicTimeline(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

#Region "Twitter_Timeline_Retweets"
    ''' <summary>
    ''' Gets a list of retweets from users following list.
    ''' </summary>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_Retweets_ByOthers() As TwitterStatusCollection
        Dim o As New TimelineOptions
        If My.Settings.api_statuscount > 100 Then
            o.Count = 100
        Else
            o.Count = My.Settings.api_statuscount
        End If
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.RetweetedToMe(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Gets a list of retweets from users Timeline.
    ''' </summary>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_Retweets_ByMe() As TwitterStatusCollection
        Dim o As New TimelineOptions
        If My.Settings.api_statuscount > 100 Then
            o.Count = 100
        Else
            o.Count = My.Settings.api_statuscount
        End If
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.RetweetedByMe(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Gets a list of your tweets that have been retweeted.
    ''' </summary>
    ''' <returns>A list of Tweets.</returns>
    ''' <remarks></remarks>
    Function Twitter_Timeline_Retweets_MyTweets() As TwitterStatusCollection
        Dim o As New RetweetsOfMeOptions
        If My.Settings.api_statuscount > 100 Then
            o.Count = 100
        Else
            o.Count = My.Settings.api_statuscount
        End If
        Dim response As TwitterResponse(Of TwitterStatusCollection) = TwitterTimeline.RetweetsOfMe(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function
#End Region
#End Region

#Region "Twitter_Trending"

    ''' <summary>
    ''' Gets the current trending topics.
    ''' </summary>
    ''' <param name="WOEID">The Yahoo where on earth ID.  1 for worldwide</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Twitter_Trends(ByVal WOEID As Integer) As TwitterTrendCollection
        Dim response As TwitterResponse(Of TwitterTrendCollection) = TwitterTrend.Trends(WOEID)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        End If
        Return response.ResponseObject
    End Function
#End Region

#Region "Twitter_Account"
    ''' <summary>
    ''' Updates the public settings of the authenticated user.
    ''' </summary>
    ''' <param name="Name">Name</param>
    ''' <param name="Description">Description</param>
    ''' <param name="Location">Location</param>
    ''' <param name="URL">URL</param>
    ''' <remarks></remarks>
    Sub Twitter_Account_Update(ByVal Name As String, ByVal Description As String, ByVal Location As String, ByVal URL As String)
        Dim o As New UpdateProfileOptions
        o.Name = Name
        o.Description = Description
        o.Location = Location
        o.Url = URL
        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.UpdateProfile(tokens.Tokens, o)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Updates the profile image of the user.
    ''' </summary>
    ''' <param name="Path">An absolute path to a file.</param>
    ''' <remarks></remarks>
    Sub Twitter_Account_UpdateProfileImage(ByVal Path As String)
        'Check to see if the filetype is supported.
        If Path.ToUpper.EndsWith(".JPG") Then
            'File Supported!
        ElseIf Path.ToUpper.EndsWith(".PNG") Then
            'File Supported!
        ElseIf Path.ToUpper.EndsWith(".GIF") Then
            'File Supported!
        Else
            Throw MediaNotRecognized
            Exit Sub
        End If
        Dim FileInfo As New FileInfo(Path)
        Dim FileSize As Long = FileInfo.Length
        If FileSize > 716800 Then
            Throw FileTooLargeException
            Exit Sub
        End If

        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.UpdateProfileImage(tokens.Tokens, Path)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Updates the profile background image of the user.
    ''' </summary>
    ''' <param name="Path">An absolute path to a file.</param>
    ''' <remarks></remarks>
    Sub Twitter_Account_UpdateBackgroundImage(ByVal Path As String)
        'Check to see if the filetype is supported.
        If Path.ToUpper.EndsWith(".JPG") Then
            'File Supported!
        ElseIf Path.ToUpper.EndsWith(".PNG") Then
            'File Supported!
        ElseIf Path.ToUpper.EndsWith(".GIF") Then
            'File Supported!
        Else
            Throw MediaNotRecognized
            Exit Sub
        End If
        Dim FileInfo As New FileInfo(Path)
        Dim FileSize As Long = FileInfo.Length
        If FileSize > 819200 Then
            Throw FileTooLargeException
            Exit Sub
        End If

        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.UpdateProfileBackgroundImage(tokens.Tokens, Path)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Verifies the current token set.
    ''' </summary>
    ''' <remarks>True meaning valid token set.</remarks>
    Function Twitter_Account_Verify() As Boolean
        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.VerifyCredentials(tokens.Tokens)
        If response.Result <> RequestResult.Success Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Verifies the specifies token set.
    ''' </summary>
    ''' <param name="ConsumerToken">The consumer key.</param>
    ''' <param name="ConsumerSecret">The secret consumer key.</param>
    ''' <param name="AccessToken">The users access token.</param>
    ''' <param name="AccessSecret">The users secret access token.</param>
    ''' <remarks>True meaning valid token set.</remarks>
    Function Twitter_Account_Verify(ByVal ConsumerToken As Decimal, ByVal ConsumerSecret As Decimal, ByVal AccessToken As Decimal, ByVal AccessSecret As Decimal) As Boolean
        Dim t As New OAuthTokens
        t.AccessToken = AccessToken
        t.AccessTokenSecret = AccessSecret
        t.ConsumerKey = ConsumerToken
        t.ConsumerSecret = ConsumerSecret
        Dim response As TwitterResponse(Of TwitterUser) = TwitterAccount.VerifyCredentials(t)
        If response.Result <> RequestResult.Success Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Twitter_DirectMessage"
    ''' <summary>
    ''' Gets a list of the users direct messages.
    ''' </summary>
    ''' <returns>A list of <see>TwitterDirectMessage</see>s.</returns>
    ''' <remarks></remarks>
    Function Twitter_DirectMessage_Get() As TwitterDirectMessageCollection
        Dim response As TwitterResponse(Of TwitterDirectMessageCollection) = TwitterDirectMessage.DirectMessages(tokens.Tokens)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function

    ''' <summary>
    ''' Sends a direct message to the specified user.
    ''' </summary>
    ''' <param name="Text">The text of the message.</param>
    ''' <param name="UserId">The user.</param>
    ''' <remarks></remarks>
    Sub Twitter_DirectMessage_Post(ByVal Text As String, ByVal UserId As String)
        Dim response As TwitterResponse(Of TwitterDirectMessage) = TwitterDirectMessage.Send(tokens.Tokens, UserId, Text)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Deletes a direct message.
    ''' </summary>
    ''' <param name="MessageId">The MessageId</param>
    ''' <remarks></remarks>
    Sub Twitter_DirectMessage_Delete(ByVal MessageId As Decimal)
        Dim response As TwitterResponse(Of TwitterDirectMessage) = TwitterDirectMessage.Delete(tokens.Tokens, MessageId, New Twitterizer.OptionalProperties)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Return a single DirectMessage of the specified Id.
    ''' </summary>
    ''' <param name="MessageId">The ID of the message.</param>
    ''' <returns>A <see>TwitterDirectMessage</see>.</returns>
    ''' <remarks></remarks>
    Function Twitter_DirectMessage_Show(ByVal MessageId As Decimal) As TwitterDirectMessage
        Dim response As TwitterResponse(Of TwitterDirectMessage) = TwitterDirectMessage.Show(tokens.Tokens, MessageId, New Twitterizer.OptionalProperties)
        If response.Result <> RequestResult.Success Then
            Throw New Exception(response.ErrorMessage)
        Else
            Return response.ResponseObject
        End If
    End Function
#End Region

#End Region

#Region "3rd Party Methods"
    ''' <summary>
    ''' Gets the image thumbnail for the specified URL
    ''' </summary>
    ''' <param name="URL">The TwitPic url.  Must be in an expanded state (contains http://)</param>
    ''' <returns>The image source to the image.</returns>
    ''' <remarks></remarks>
    Function TwitPic_ImageThumbnail(ByVal URL As String) As ImageSource
        Dim ImageURL As String = URL
        ImageURL = ImageURL.Replace("http://twitpic.com/", "http://twitpic.com/show/mini/")
        Dim bi As New BitmapImage
        bi.BeginInit()
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.UriSource = New Uri(ImageURL)
        bi.EndInit()
        Return bi
    End Function

    ''' <summary>
    ''' Gets the RAW image file for the specified ID.
    ''' </summary>
    Function TwitPic_Image(ByVal URL As String) As ImageSource
        Dim ImageURL As String = URL
        ImageURL = ImageURL.Replace("http://twitpic.com/", "")
        ImageURL = "http://twitpic.com/show/full/" & ImageURL & ".jpg"
        Dim bi As New BitmapImage
        bi.BeginInit()
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.UriSource = New Uri(ImageURL)
        bi.EndInit()
        Return bi
    End Function

    ''' <summary>
    ''' Gets the image thumbnail for the specified URL.
    ''' </summary>
    ''' <param name="URL">The yfrog url.  Must be in an expanded state (containing http://)</param>
    ''' <returns>The image source to the image.</returns>
    ''' <remarks></remarks>
    Function yfrog_ImageThumbnail(ByVal URL As String) As ImageSource
        Dim ImageURL As String = URL
        ImageURL = ImageURL & ":small"
        Dim bi As New BitmapImage
        bi.BeginInit()
        bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi.UriSource = New Uri(ImageURL)
        bi.EndInit()
        Return bi
    End Function

    ''' <summary>
    ''' Gets the video thumbnail for the specified video.
    ''' </summary>
    ''' <param name="URL">The YouTube url.  Must be in an expanded state (containing https://)</param>
    ''' <returns>The image source to the image.</returns>
    ''' <remarks></remarks>
    Function YouTube_VideoThumbnail(ByVal URL As String) As ImageSource
        Dim ImageURL As String = URL
        Dim bi As New BitmapImage
        If ImageURL.StartsWith("http://www.youtube.com/watch?v=") = False Then
            ImageURL = "http://img.youtube.com/vi/ID/1.jpg"
            bi.BeginInit()
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
            bi.UriSource = New Uri(ImageURL)
            bi.EndInit()
            Return bi
        Else
            ImageURL = ImageURL.Replace("http://www.youtube.com/watch?v=", "")
            bi.BeginInit()
            bi.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
            bi.UriSource = New Uri(ImageURL)
            bi.EndInit()
            Return bi
        End If
    End Function
#End Region

#Region "Notification Methods"
    ''' <summary>
    ''' Creates the NotificationIcon object.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Notification_CreateIcon()
        _streamingicon.Text = "myFire"
        _streamingicon.Icon = My.Resources.Retro_Fire_Ball
        _streamingicon.Visible = True
    End Sub

    ''' <summary>
    ''' Destroys the NotificationIcon object.
    ''' </summary>
    ''' <remarks></remarks>
    Sub Notification_DestroyIcon()
        _streamingicon.Visible = False
        _streamingicon.Dispose()
    End Sub

    ''' <summary>
    ''' Display a notification.
    ''' </summary>
    Sub Notification_Show_Mention(ByVal Tweet As TwitterStatus)
        Dim nw As New myFire_notificationwindow_general
        nw.ShowActivated = False
        nw.Topmost = True
        nw.ShowInTaskbar = False
        nw.ShowMention(Tweet)
    End Sub

    ''' <summary>
    ''' Display a notification.
    ''' </summary>
    Sub Notification_Show_Status(ByVal Tweet As TwitterStatus)
        If Tweet.User.ScreenName = tokens.GetDefaultScreenName Then
            Exit Sub
        End If
        Dim nw As New myFire_notificationwindow_general
        nw.ShowActivated = False
        nw.Topmost = True
        nw.ShowInTaskbar = False
        nw.ShowTweet(Tweet)
    End Sub

    ''' <summary>
    ''' Display a notification.
    ''' </summary>
    Sub Notification_Show_Message(ByVal Message As TwitterDirectMessage)
        If Message.Sender.ScreenName = tokens.GetDefaultScreenName Then
            Exit Sub
        End If
        Dim nw As New myFire_notificationwindow_general
        nw.ShowActivated = False
        nw.Topmost = True
        nw.ShowInTaskbar = False
        nw.ShowDirectMessage(Message)
    End Sub

    ''' <summary>
    ''' Display a notification.
    ''' </summary>
    Sub Notification_Show_Test(ByVal Title As String, ByVal Message As String, ByVal Icon As ToolTipIcon, Optional ByVal Timeout As Integer = 200)
        Dim nw As New myFire_notificationwindow_general
        nw.ShowActivated = False
        nw.Topmost = True
        nw.ShowInTaskbar = False
        nw.ShowGeneral(Title, Message)
    End Sub
#End Region
End Module
