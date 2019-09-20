


<DebuggerDisplay("Name={Name(False)} Days={DaysInMonth} Norma ={norm} DniPracy={WorkDaysCount}")>
<Serializable> Public Class MonthType

    Public Property Id As String
    Public Property Number As Integer
    Public Property Yer As Integer
    Public Property Holidays As List(Of HolidayType)
    Public Property Groups As List(Of GroupType)
    Public Property Shifts As List(Of shifttype)
    Public Sub New(number As Integer, yer As Integer)
        Me.Id = Guid.NewGuid.ToString
        Me.Number = number
        Me.Yer = yer
        Me.Holidays = New List(Of HolidayType)
        Me.Groups = New List(Of GroupType)
        Me.AddGroup("")
    End Sub

    Public Function Name(ShortName As Boolean) As String
        Return DateAndTime.MonthName(Me.Number, ShortName)
    End Function
    Public Function DaysInMonth() As Integer
        Return DateTime.DaysInMonth(Me.Yer, Me.Number)
    End Function
    Public Function Norm() As Integer
        Return Me.WorkDaysCount * 8
    End Function
    Public Function WorkDaysCount() As Integer
        WorkDaysCount = 0
        For i = 1 To Me.DaysInMonth
            Dim d As Date = Me.Yer & "-" & Me.Number & "-" & i
            If d.DayOfWeek <> DayOfWeek.Saturday And d.DayOfWeek <> DayOfWeek.Sunday Then
                Dim ok As Boolean = True
                For Each h In Me.Holidays
                    If h.Data.ToShortDateString = d.ToShortDateString Then
                        ok = False
                    End If
                Next
                If ok = True Then
                    WorkDaysCount += 1
                End If
            End If
        Next
    End Function
    Public Function AddHoliday(data As Date, Optional name As String = "") As String
        If Month(data) = Me.Number Then
            For Each hh In Me.Holidays
                If hh.Data = data Then
                    Return ("Święto już istnieje")
                End If
            Next
            Dim H As New HolidayType(data, name)
            Me.Holidays.Add(H)

            For Each g In Me.Groups
                For Each w In g.Workers
                    For Each d In w.Days
                        If d.Data.ToShortDateString = data.ToShortDateString Then
                            d.IsHoliday = True
                        End If
                    Next
                Next
            Next

            Return "Dodano święto " & data
        Else
            Return "Data nie zawiera się w wybranym miesiącu"
        End If
    End Function
    Public Function RemoveHoliday(data As Date) As String
        For Each h In Me.Holidays
            If h.Data.ToShortDateString = data.ToShortDateString Then
                Me.Holidays.Remove(h)

                For Each g In Me.Groups
                    For Each w In g.Workers
                        For Each d In w.Days
                            If d.Data.ToShortDateString = data.ToShortDateString Then
                                d.IsHoliday = False
                            End If
                        Next
                    Next
                Next

                Return "Usunięto święto " & data
            End If
        Next
        Return "Nie znaleziono święta do usunięcia " & data
    End Function
    Public Function AddGroup(name As String) As String
        For Each gr In Me.Groups
            If gr.Name.ToLower = name.ToLower Then
                Return "Grupa o nazwie " & name & " już istnieje"
            End If
        Next
        Dim G As New GroupType(name, Me.Yer, Me.Number)
        Me.Groups.Add(G)
        Return "Dodano grupę: " & name & "."
    End Function
    Public Function RemoveGroup(name As String) As String
        If name = "" Then
            Return "Grupy nie można usunąć"
        End If
        For Each g In Me.Groups
            If g.Name.ToLower = name.ToLower Then
                Me.Groups.Remove(g)
                Return ("Usunięto grupę " & name)
            End If
        Next
        Return "Brak grupy " & name & " do usunięcia"
    End Function


    Public Function AddShift(name As String, starttime As TimeSpan, endtime As TimeSpan, iswork As Boolean, hourtype As HoursType) As String
        If String.IsNullOrEmpty(name) Then Return "Nazwa jest wymagana"
        If Len(name) > 5 Then Return "Nazwa jest za długa, maksymalnie 5 znaków"

        For Each ss In Me.Shifts
            If ss.Name = name Then Return "Nazwa jest już zajęta"
        Next

        Dim s As New ShiftType
        s.Name = name
        s.StartTime = starttime
        s.EndTime = endtime
        s.IsWork = iswork
        s.HourType = hourtype

        Me.Shifts.Add(s)
        Return "Dodano zmianę"
    End Function
    Public Function RemoveShift(name As String) As Boolean
        For Each s In Me.Shifts
            If s.Name = name Then
                Me.Shifts.Remove(s)
                Return "Usunięto zmianę"
            End If
        Next
        Return "Nie znalezniono zmiany " & name & ", nie można usunąć"
    End Function


End Class
