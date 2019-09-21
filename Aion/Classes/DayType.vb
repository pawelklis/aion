
Imports System.Text

<DebuggerDisplay("data={data} dzien={DayName} ")>
Public Class DayType
    Public Property Id As String
    Public Property Data As Date
    Public Property IsHoliday As Boolean
    Public Property IsSunday As Boolean
    Public Property IsSaturday As Boolean
    Public Property Style As StyleType


    Public Property Planned As EntryType
    Public Property Executed As EntryType



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

        Me.Planned = New EntryType
        Me.Executed = New EntryType

        Me.Style = New StyleType

    End Sub

    Public Enum EntryTypeValue
        Planned = 1
        Executed = 2
    End Enum

    Public Function SetValue(shift As ShiftType, entrytype As EntryTypeValue) As String
        Select Case entrytype
            Case EntryTypeValue.Planned
                Dim a As String = CheckEntry(shift, Me.Planned)
                If a <> "" Then
                    Return a
                End If

                Me.Planned.Entries.Add(shift)

            Case EntryTypeValue.Executed

                Dim a As String = CheckEntry(shift, Me.Executed)
                If a <> "" Then
                    Return a
                End If
                Me.Executed.Entries.Add(shift)
        End Select

        Return Me.GetValue(entrytype)
    End Function
    Public Function GetValue(entrytype As EntryTypeValue) As String
        Dim sb As New StringBuilder
        Select Case entrytype
            Case EntryTypeValue.Planned
                For Each s In Me.Planned.Entries
                    sb.AppendLine(s.Name)
                Next
            Case EntryTypeValue.Executed
                For Each s In Me.Planned.Entries
                    sb.AppendLine(s.Name)
                Next

        End Select
        Return sb.ToString
    End Function
    Public Function RemoveValue(id As String) As String
        For Each v In Me.Planned.Entries
            If v.Id = id Then
                Me.Planned.Entries.Remove(v)
                Return "Usunięto wpis"
            End If
        Next
        For Each v In Me.Executed.Entries
            If v.Id = id Then
                Me.Executed.Entries.Remove(v)
                Return "Usunięto wpis"
            End If
        Next
        Return "Nie znaleziono wpisu"
    End Function
    Private Function CheckEntry(shift As ShiftType, entry As EntryType) As String
        For Each s In entry.Entries
            If s.StartTime < shift.StartTime Then
                If s.EndTime > shift.EndTime Then
                    Return "Wpis koliduje z innymi"
                End If
            End If

            If shift.StartTime > s.StartTime And shift.StartTime < s.EndTime Then
                Return "Wpis koliduje z innymi " & shift.StartTime.TotalHours & ":" & shift.StartTime.TotalMinutes
            End If

            If shift.EndTime < s.EndTime And shift.EndTime > s.StartTime Then
                Return "Wpis koliduje z innymi " & shift.EndTime.TotalHours & ":" & shift.EndTime.TotalMinutes
            End If

        Next
        Return ""
    End Function
    Public Function DayName() As String
        Dim myCulture As System.Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture
        Return myCulture.DateTimeFormat.GetDayName(Me.Data.DayOfWeek)
    End Function
End Class
