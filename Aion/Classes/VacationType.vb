
<DebuggerDisplay("Name={Name}")>
Public Class VacationType

    Public Property Id As String
    Public Property Name As String
    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
    Public Property Description As String
    Public Property HourType As HoursType
    Public Sub New()
        Me.Id = Guid.NewGuid.ToString
    End Sub

End Class
