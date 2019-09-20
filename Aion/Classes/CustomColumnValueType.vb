

<Serializable> Public Class CustomColumnValueType
    Public Property Id As String
    Public Property Name As String
    Public Property Val As String

    Public Sub New(name As String)
        Me.Id = Guid.NewGuid.ToString
        Me.Name = name
    End Sub


End Class
