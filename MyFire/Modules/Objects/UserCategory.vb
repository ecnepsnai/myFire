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
Public Class UserCategory
    Public Property UserCategoryObject As TwitterUserCategory
        Get
            Return _usercategoryobject
        End Get
        Set(value As TwitterUserCategory)
            CategoryTitle = value.Name
            CategorySize = "Number of users: " & value.NumberOfUsers
            _usercategoryobject = value
        End Set
    End Property
    Private _usercategoryobject As TwitterUserCategory

    Public Property CategoryTitle As String
        Get
            Return _categorytitle
        End Get
        Set(value As String)
            _categorytitle = value
        End Set
    End Property
    Private _categorytitle As String

    Public Property CategorySize As String
        Get
            Return _categorysize
        End Get
        Set(value As String)
            _categorysize = value
        End Set
    End Property
    Private _categorysize As String
End Class
