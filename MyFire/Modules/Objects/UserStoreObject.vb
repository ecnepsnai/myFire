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
Public Class UserStoreObject
    Public Property StoredUserObject() As StoredUser
        Get
            Return _storeduser
        End Get
        Set(value As StoredUser)
            StoredUserImage = myfireactions.Image_Format(value.ProfilePicture)
            StoredUserTitle = value.ScreenName
            StoredUserDescription = value.UserDescription
            _storeduser = value
        End Set
    End Property
    Private _storeduser As StoredUser

    Public Property StoredUserImage() As ImageSource
        Get
            Return _storeduserimage
        End Get
        Set(value As ImageSource)
            _storeduserimage = value
        End Set
    End Property
    Private _storeduserimage As ImageSource

    Public Property StoredUserTitle As String
        Get
            Return _storedusertitle
        End Get
        Set(value As String)
            _storedusertitle = value
        End Set
    End Property
    Private _storedusertitle As String

    Public Property StoredUserDescription As String
        Get
            Return _storeduserdescription
        End Get
        Set(value As String)
            _storeduserdescription = value
        End Set
    End Property
    Private _storeduserdescription As String
End Class
