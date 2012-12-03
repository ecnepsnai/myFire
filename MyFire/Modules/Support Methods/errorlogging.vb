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

'* 
'* Warning:
'* The information collected by this logging device is protected under the
'* myFire privacy policy.  All and any information may be collected but can not
'* be used to identify you (the user).  Further more, you (the user) agree to
'* this data collection when you installed the program and agreed to the 
'* End User License Agreement (Paragraph 10, subsection f.)
'* 
Imports System.IO
Module errorlogging
    ''' <summary>
    ''' Adds a new entry to the error log.
    ''' </summary>
    ''' <param name="ex">The exception</param>
    ''' <remarks></remarks>
    Sub WriteException(ByVal ex As Exception)
        My.Settings.support_errorlog.Add(ex.ToString)
        My.Settings.Save()
    End Sub

#Region "Special"
    ''' <summary>
    ''' Adds a special message to the error log.
    ''' </summary>
    ''' <param name="Message">The message</param>
    ''' <remarks></remarks>
    Sub WriteSpeical(ByVal Message As String, Optional ByVal InnerException As Exception = Nothing)
        Dim Ex As New Exception(Message, InnerException)
        My.Settings.support_errorlog.Add(Ex.ToString)
        My.Settings.Save()
    End Sub
#End Region
End Module
