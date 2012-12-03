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

Public Class Data

#Region "Encryption"
    Private Function Encoding(ByVal Value As String)
        Encryption.Create("DV5JMRtqW1SU,@.'$yO_L5EvnjcC'^b4'*o<9qbWDmq^@d)K.\Y_7IwPx5#=\YJ")
        Dim cipherText As String = Encryption.EncryptData(Value)
        Return cipherText
    End Function

    Private Function Decode(ByVal Value As String, Optional ByVal Password As String = "DV5JMRtqW1SU,@.'$yO_L5EvnjcC'^b4'*o<9qbWDmq^@d)K.\Y_7IwPx5#=\YJ")
        Dim ReturnValue As String
        Dim cipherText As String = Value
        Encryption.Create(Password)

        ' DecryptData throws if the wrong password is used.
        Try
            Dim plainText As String = Encryption.DecryptData(cipherText)
            ReturnValue = plainText
        Catch ex As System.Security.Cryptography.CryptographicException
            ReturnValue = String.Empty
        End Try
        Return ReturnValue
    End Function
#End Region

#Region "Reset Function"
    Dim dir_UserTemp As String = My.Computer.FileSystem.SpecialDirectories.Temp
    Dim dir_iFXSoftware As String = dir_UserTemp & "\iFX_Software"
    Dim dir_myFire As String = dir_iFXSoftware & "\MyFire"
    Dim file_userstore As String = dir_myFire & "\userstore.xml"


    ''' <summary>
    ''' Resets the settings to their default values.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Reset(ByVal ScreenNameList As Specialized.StringCollection)
        Try
            My.Computer.FileSystem.DeleteFile(file_userstore)
            My.Computer.FileSystem.DeleteDirectory(dir_myFire, FileIO.DeleteDirectoryOption.DeleteAllContents)
            My.Computer.FileSystem.DeleteDirectory(dir_iFXSoftware, FileIO.DeleteDirectoryOption.DeleteAllContents)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


    ''' <summary>
    ''' Returns a list of currently authorized users.
    ''' </summary>
    ''' <returns>A list of <see>StoredUser</see> objects.</returns>
    ''' <remarks></remarks>
    Public Function Users_ReturnUserList() As List(Of StoredUser)
        Try
            Dim x As New XMLstore
            Return x.GetUsers
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Writes a user to the XMLUserList.
    ''' </summary>
    ''' <param name="User">The <see>StoredUser</see> object.</param>
    ''' <remarks></remarks>
    Public Sub Users_WriteSingleUser(ByVal User As StoredUser)
        Try
            Dim x As New XMLstore
            x.StoreUser(User)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Deleted the user from the user store.
    ''' </summary>
    ''' <param name="DeleteUser">The <see>StoredUser</see> object.</param>
    ''' <remarks></remarks>
    Public Sub Users_DeleteSingleUser(ByVal DeleteUser As StoredUser)
        Try
            Dim x As New XMLstore
            x.DeleteUser(DeleteUser)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Returns a list of Screen Names for use around the application.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Users_ListOfScreenNames() As List(Of String)
        Try
            Dim x As New XMLstore
            Dim U As List(Of StoredUser) = x.GetUsers
            Dim ReturnList As New List(Of String)
            For Each User As StoredUser In U
                ReturnList.Add(User.ScreenName)
            Next
            Return ReturnList
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Sets the default user from the list of users.
    ''' </summary>
    ''' <param name="screenname">The screen name of the user to be made default.</param>
    ''' <remarks>Will throw a <see>KeyNotFoundException</see> if no user was found.</remarks>
    Public Sub SetDefaultUser(ByVal screenname As String)
        Try
            Dim x As New XMLstore
            x.SetDefaultUser(screenname)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Gets the default user from the list of users.
    ''' </summary>
    ''' <returns>A SturedUser object for the default user.</returns>
    ''' <remarks>Will throw a KeyNotFoundException if no default user was found.</remarks>
    Public Function GetDefaultUser() As StoredUser
        Dim x As New XMLstore
        Dim u As List(Of StoredUser) = x.GetUsers
        Dim r As StoredUser = Nothing
        For Each User As StoredUser In u
            If User.DefaultAccount Then
                r = User
            End If
        Next
        If r Is Nothing Then
            Throw New KeyNotFoundException("No default user was found.")
            Exit Function
        End If
        Return r
    End Function
End Class