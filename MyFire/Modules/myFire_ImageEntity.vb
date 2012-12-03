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
Public Class myFire_ImageEntity
    Private _imagethumbnail As BitmapImage
    Private _image As BitmapImage
    Private _imagethumbnailurl As Uri
    Private _imageurl As Uri
    Private _hasthumbnail As Boolean
    Private _openwebsite As Boolean

    Public Sub New(ByVal Image As String, Optional ByVal Thumb As String = "", Optional ByVal OpenWebsite As Boolean = True)
        Dim bi_image As New BitmapImage
        bi_image.BeginInit()
        bi_image.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
        bi_image.UriSource = New Uri(Image)
        bi_image.EndInit()
        _image = bi_image
        _imageurl = New Uri(Image)
        If Thumb = "" Then
            _hasthumbnail = False
            _imagethumbnail = bi_image
            _imagethumbnailurl = New Uri(Image)
        Else
            Dim bi_thumb As New BitmapImage
            bi_thumb.BeginInit()
            bi_thumb.CreateOptions = BitmapCreateOptions.IgnoreColorProfile
            bi_thumb.UriSource = New Uri(Thumb)
            bi_thumb.EndInit()
            _imagethumbnail = bi_thumb
            _imagethumbnailurl = New Uri(Thumb)
        End If
        _openwebsite = OpenWebsite
    End Sub

    Public ReadOnly Property ImageThumbnail As BitmapImage
        Get
            Return _imagethumbnail
        End Get
    End Property

    Public ReadOnly Property Image As BitmapImage
        Get
            Return _image
        End Get
    End Property

    Public ReadOnly Property ImageThumbnailUrl As Uri
        Get
            Return _imagethumbnailurl
        End Get
    End Property

    Public ReadOnly Property ImageUrl As Uri
        Get
            Return _imageurl
        End Get
    End Property

    Public ReadOnly Property HasImageThumbnail As Boolean
        Get
            Return _hasthumbnail
        End Get
    End Property

    Public ReadOnly Property OpenWebsite As Boolean
        Get
            Return _openwebsite
        End Get
    End Property
End Class
