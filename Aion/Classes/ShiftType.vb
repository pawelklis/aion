
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

    Public Function TimeDifference() As TimeSpan
        Dim st As New DateTime(2013, 9, 19, Me.StartTime.Hours, Me.StartTime.Minutes, Me.StartTime.Seconds)     ' 10:30 AM today
        Dim et As New DateTime(2013, 9, 19, Me.EndTime.Hours, Me.EndTime.Minutes, Me.EndTime.Seconds)     ' 2:00 AM tomorrow

        Dim duration As TimeSpan = et - st        'Subtract start time from end time
        Return duration

    End Function

End Class
