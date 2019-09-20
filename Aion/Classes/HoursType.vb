

Public Class HoursType
    Public Property Id As String
    Public Property Name As String

    Public Sub New()
        Me.Id = Guid.NewGuid.ToString

    End Sub

End Class
