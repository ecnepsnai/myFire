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
Module exceptionhandler
    Sub ErrorMessage(ByVal Message As String, ByVal MainTitle As String)
        MessageBox.Show(Message, MainTitle, MessageBoxButton.OK, MessageBoxImage.Error)
    End Sub
    Sub InfoMessage(ByVal Message As String, ByVal MainTitle As String)
        Dim NotificationWindow As New NotificationWindow
        NotificationWindow.ShowNotification(MainTitle, Message, "blue")
    End Sub
    Sub WarningMessage(ByVal Message As String, ByVal MainTitle As String)
        Dim MessageWindow As New MessageWindow
        MessageWindow.ShowMessage(MainTitle, Message, False)
    End Sub
    Sub ExceptionMessage(ByVal ex As Exception, Optional ByVal DisableSendTo As Boolean = False)
        Dim fe As New FatalError
        fe.ShowError(ex, DisableSendTo)
    End Sub

#Region "Special Handlers"
    Sub TranslateMessage(ByVal TweetText As String)
        MessageBox.Show(myfireactions.Twitter_Status_Translate(TweetText), "Translated Tweet: (Powered by Microsoft® Translator)", MessageBoxButton.OK, MessageBoxImage.Information)
    End Sub
#End Region
End Module
