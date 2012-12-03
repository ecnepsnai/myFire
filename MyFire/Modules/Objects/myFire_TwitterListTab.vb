Public Class myFire_TwitterListTab
    Private _listname As String
    Private _listid As Decimal
    Private _image As Image

    Public Sub New(ByVal ListName As String, ByVal ListID As Decimal, ByVal Image As Image)
        Me.ListName = ListName
        Me.ListID = ListID
        Me.Image = Image
    End Sub

    Public Property ListName As String
        Get
            Return _listname
        End Get
        Set(value As String)
            _listname = value
        End Set
    End Property

    Public Property ListID As Decimal
        Get
            Return _listid
        End Get
        Set(value As Decimal)
            _listid = value
        End Set
    End Property

    Public Property Image As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            _image = value
        End Set
    End Property
End Class
