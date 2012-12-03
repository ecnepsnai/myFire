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

Imports System.Xml
Imports System.IO

Public Class ImageOfTheDay
    Private _imageurl As Uri
    Private _copyright As String

    Public Sub New()
        If My.Computer.Network.IsAvailable = False Then
            Throw New System.Net.WebException("Could not establish a connection to bing.com")
        End If
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString("http://www.bing.com/hpimagearchive.aspx?format=xml&idx=0&n=1&mbl=1&mkt=en-ww")
        Dim doc As New XmlDocument
        doc.LoadXml(result)
        Dim URLnode As XmlNode = doc.SelectSingleNode("/images/image/url")
        Dim Copyrightnode As XmlNode = doc.SelectSingleNode("/images/image/copyright")
        _imageurl = New Uri("http://www.bing.com/" & URLnode.InnerText)
        _copyright = Copyrightnode.InnerText
    End Sub

    Public Property ImageURL As Uri
        Get
            Return _imageurl
        End Get
        Set(value As Uri)
            _imageurl = value
        End Set
    End Property

    Public Property Copyright As String
        Get
            Return _copyright
        End Get
        Set(value As String)
            _copyright = value
        End Set
    End Property
End Class