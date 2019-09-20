

<Serializable> Public Class CustomColumnType
    Public Property Id As String
    Public Property Name As String
    Public Property IsCustom As Boolean
    Public Property IsVisible As Boolean

    Public Sub New()
        Me.Id = Guid.NewGuid.ToString
    End Sub

End Class
