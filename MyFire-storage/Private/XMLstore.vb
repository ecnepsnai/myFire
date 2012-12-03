'* 
'* myFire
'*
'* This file is part of the myFire software
'* Copyright (c) 2012, Ian Spence
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
Imports System.Xml
Friend Class XMLstore
    Dim dir_UserTemp As String = My.Computer.FileSystem.SpecialDirectories.Temp
    Dim dir_iFXSoftware As String = dir_UserTemp & "\iFX_Software"
    Dim dir_myFire As String = dir_iFXSoftware & "\MyFire"
    Dim file_userstore As String = dir_myFire & "\userstore.xml"

    Dim writer As XmlTextWriter

    Public Sub New()
        If My.Computer.FileSystem.DirectoryExists(dir_UserTemp) = False Then
            Throw New System.IO.IOException("Application Data folder could not be found / accessed.  Please check UAC settings.")
        End If
        If My.Computer.FileSystem.DirectoryExists(dir_iFXSoftware) = False Then
            Try
                My.Computer.FileSystem.CreateDirectory(dir_iFXSoftware)
            Catch ex As Exception
                Throw ex
            End Try
        End If
        If My.Computer.FileSystem.DirectoryExists(dir_myFire) = False Then
            Try
                My.Computer.FileSystem.CreateDirectory(dir_myFire)
            Catch ex As Exception
                Throw ex
            End Try
        End If
        If My.Computer.FileSystem.FileExists(file_userstore) = False Then
            Try
                CreateUserStore()
            Catch ex As Exception
                Throw ex
            End Try
        End If
    End Sub

    Friend Sub DeleteUser(ByVal UserStore As StoredUser)
        Dim doc As New XmlDocument
        doc.Load(file_userstore)
        Dim UsersObject As XmlNode = doc.SelectSingleNode("/Users")
        Dim SU As StoredUser
        For Each userNode As XmlNode In UsersObject.ChildNodes
            SU = New StoredUser(userNode.SelectSingleNode("ScreenName").InnerText, userNode.SelectSingleNode("description").InnerText, userNode.SelectSingleNode("userid").InnerText, userNode.SelectSingleNode("profilepicture").InnerText, userNode.SelectSingleNode("token").InnerText, userNode.SelectSingleNode("tokensecret").InnerText, userNode.SelectSingleNode("DefaultAccount").InnerText)
            If SU Is UserStore Then
                UsersObject.RemoveChild(userNode)
            End If
        Next
    End Sub

    Friend Function GetUsers() As List(Of StoredUser)
        Dim returnl As New List(Of StoredUser)
        Dim doc As New XmlDocument
        doc.Load(file_userstore)
        Dim SU As StoredUser
        Dim UsersObject As XmlNode = doc.SelectSingleNode("/Users")
        For Each UserNode As XmlNode In UsersObject.ChildNodes
            SU = New StoredUser(UserNode.SelectSingleNode("ScreenName").InnerText, UserNode.SelectSingleNode("description").InnerText, UserNode.SelectSingleNode("userid").InnerText, UserNode.SelectSingleNode("profilepicture").InnerText, UserNode.SelectSingleNode("token").InnerText, UserNode.SelectSingleNode("tokensecret").InnerText, UserNode.SelectSingleNode("DefaultAccount").InnerText)
            returnl.Add(SU)
        Next
        Return returnl
    End Function

    Friend Sub CreateUserStore()
        Try
            writer = New XmlTextWriter(file_userstore, System.Text.Encoding.UTF8)
            writer.WriteStartDocument(False)
            writer.Formatting = Formatting.Indented
            writer.Indentation = 2
            writer.WriteStartElement("Users")
            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Friend Sub StoreUser(ByVal User As StoredUser)
        Try
            Dim doc As New XmlDocument
            doc.Load(file_userstore)
            Dim root As XmlNode = doc.DocumentElement
            Dim UserElement As XmlElement = doc.CreateElement("User")
            Dim ScreenNameNode As XmlElement = doc.CreateElement("ScreenName")
            ScreenNameNode.InnerText = User.ScreenName
            Dim UserDescriptionNode As XmlElement = doc.CreateElement("description")
            UserDescriptionNode.InnerText = User.UserDescription
            Dim UserIdNode As XmlElement = doc.CreateElement("userid")
            UserIdNode.InnerText = User.UserId
            Dim ProfilePicutreNode As XmlElement = doc.CreateElement("profilepicture")
            ProfilePicutreNode.InnerText = User.ProfilePicture
            Dim TokenNode As XmlElement = doc.CreateElement("token")
            TokenNode.InnerText = User.Token
            Dim SecretTokenNode As XmlElement = doc.CreateElement("tokensecret")
            SecretTokenNode.InnerText = User.TokenSecret
            Dim DefaultAccount As XmlElement = doc.CreateElement("DefaultAccount")
            DefaultAccount.InnerText = User.DefaultAccount
            UserElement.AppendChild(ScreenNameNode)
            UserElement.AppendChild(UserDescriptionNode)
            UserElement.AppendChild(UserIdNode)
            UserElement.AppendChild(ProfilePicutreNode)
            UserElement.AppendChild(TokenNode)
            UserElement.AppendChild(SecretTokenNode)
            UserElement.AppendChild(DefaultAccount)
            root.InsertAfter(UserElement, root.LastChild)
            doc.Save(file_userstore)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Friend Function GetDefaultUser() As StoredUser
        Dim doc As New XmlDocument
        doc.Load(file_userstore)
        Dim SU As StoredUser = Nothing
        Dim UsersObject As XmlNode = doc.SelectSingleNode("/Users")
        For Each UserNode As XmlNode In UsersObject.ChildNodes
            If UserNode.SelectSingleNode("DefaultAccount").InnerText = "True" Then
                SU = New StoredUser(UserNode.SelectSingleNode("ScreenName").InnerText, UserNode.SelectSingleNode("description").InnerText, UserNode.SelectSingleNode("userid").InnerText, UserNode.SelectSingleNode("profilepicture").InnerText, UserNode.SelectSingleNode("token").InnerText, UserNode.SelectSingleNode("tokensecret").InnerText, UserNode.SelectSingleNode("DefaultAccount").InnerText)
            End If
        Next
        Return SU
    End Function

    Friend Sub SetDefaultUser(ByVal screenname As String)
        Dim Updated As Boolean = False
        Dim doc As New XmlDocument
        doc.Load(file_userstore)
        Dim UsersObject As XmlNode = doc.SelectSingleNode("/Users")
        Dim NewNode As XmlNode
        For Each UserNode As XmlNode In UsersObject.ChildNodes
            If UserNode.SelectSingleNode("DefaultAccount").InnerText = "True" Then
                NewNode = UserNode
                NewNode.SelectSingleNode("DefaultAccount").InnerText = "False"
                UsersObject.ReplaceChild(NewNode, UserNode)
                NewNode = Nothing
            End If
        Next
        For Each UserNode As XmlNode In UsersObject.ChildNodes
            If UserNode.SelectSingleNode("ScreenName").InnerText = screenname Then
                NewNode = UserNode
                NewNode.SelectSingleNode("DefaultAccount").InnerText = "False"
                UsersObject.ReplaceChild(NewNode, UserNode)
                Updated = True
            End If
        Next
        If Updated = False Then
            Throw New KeyNotFoundException("User was not found.")
        End If
    End Sub
End Class
