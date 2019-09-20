
<DebuggerDisplay("Name={Name}")>
Public Class ShiftType
    Public Property Id As String
    Public Property Name As String

    Public Property StartTime As TimeSpan
    Public Property EndTime As TimeSpan
    Public Property IsWork As Boolean
    Public Property HourType As HoursType

    Public Sub New()
        Me.Id = Guid.NewGuid.ToString

    End Sub



End Class
