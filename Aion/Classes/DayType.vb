
<DebuggerDisplay("data={data} dzien={DayName} ")>
Public Class DayType
    Public Property Id As String
    Public Property Data As Date
    Public Property IsHoliday As Boolean
    Public Property IsSunday As Boolean
    Public Property IsSaturday As Boolean



    Public Sub New(data As Date, holidays As List(Of HolidayType))
        Me.Id = Guid.NewGuid.ToString
        Me.Data = data
        If Me.Data.DayOfWeek = DayOfWeek.Sunday Then
            Me.IsSunday = True
        End If
        If Me.Data.DayOfWeek = DayOfWeek.Saturday Then
            Me.IsSaturday = True
        End If

        For Each h In holidays
            If h.Data.ToShortDateString = Me.Data.ToShortDateString Then
                Me.IsHoliday = True
            End If
        Next

    End Sub

    Public Function DayName() As String
        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
        Return myCulture.DateTimeFormat.GetDayName(Me.Data.DayOfWeek)
    End Function
End Class
