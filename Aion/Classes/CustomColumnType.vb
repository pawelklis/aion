

<Serializable> Public Class CustomColumnType
    Public Property Id As String
    Public Property Name As String
    Public Property IsCustom As Boolean
    Public Property IsVisible As Boolean
    Public Property isDayColumn As Boolean
    Public Property Style As StyleType


    Public Sub New()
        Me.Id = Guid.NewGuid.ToString
        Me.Style = New StyleType
    End Sub

End Class
