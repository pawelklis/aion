

<DebuggerDisplay("Name={Name}")>
<Serializable> Public Class WorkerType

    Public Property Id As String
    Public Property Name As String
    Public Property Etat As Double

    Public Property CustomFields As Dictionary(Of String, String)
    Public Property CustomColumnValues As List(Of CustomColumnValueType)
    Public Property VacationShedule As List(Of VacationType)

    Public Property Days As List(Of DayType)

    Public Sub New(CustomWorkerFields As List(Of String), etat As Double, month As MonthType)
        Me.Id = Guid.NewGuid.ToString
        Me.CustomFields = New Dictionary(Of String, String)
        For Each cf In CustomWorkerFields
            Me.CustomFields.Add(cf, "")
        Next
        Me.CustomColumnValues = New List(Of CustomColumnValueType)
        Me.Etat = etat
        Me.VacationShedule = New List(Of VacationType)
        Me.Days = New List(Of DayType)

        For i = 1 To month.DaysInMonth
            Dim d As New DayType(month.Yer & "-" & month.Number & "-" & i, month.Holidays)
            Me.Days.Add(d)
        Next

    End Sub

    Public Function Suma(entryType As DayType.EntryTypeValue) As TimeSpan

        Dim mns As Integer = 0

        Select Case entryType
            Case DayType.EntryTypeValue.Executed
                For Each d In Me.Days
                    For Each entry In d.Executed.Entries
                        If entry.IsWork = True Then

                            mns += entry.TimeDifference.TotalMinutes
                        End If
                    Next
                Next

            Case DayType.EntryTypeValue.Planned
                For Each d In Me.Days
                    For Each entry In d.Planned.Entries
                        If entry.IsWork = True Then

                            mns += entry.TimeDifference.TotalMinutes
                        End If
                    Next
                Next
        End Select

        Suma = TimeSpan.FromMinutes(mns)

    End Function

    Public Function AddCustomField(name As String) As String

        If Me.CustomFields.ContainsKey(name) Then
            Return "Pole " & name & " już istnieje, nie można dodać."
        Else
            Me.CustomFields.Add(name, "")
            Return "Dodano pole"
        End If

    End Function
    Public Function RemoveCustomField(name As String) As String
        For Each cf In Me.CustomFields
            If cf.Key = name Then
                Me.CustomFields.Remove(cf.Key)
                Return "Usunięto pole " & name
            End If
        Next
        Return "Nie znaleziono pola " & name & " do usunięcia"
    End Function

    Public Function GetCustomValue(name As String) As String
        For Each cc In Me.CustomColumnValues
            If cc.Name = name Then
                Return cc.Val
            End If
        Next
        Return ""
    End Function
    Public Function AddCustomColumnValue(cv As CustomColumnValueType) As String
        For Each c In Me.CustomColumnValues
            If c.Name = cv.Name Then
                Return "Istnieje już pole " & cv.Name & ", nie można dodać"
            End If
        Next
        Me.CustomColumnValues.Add(cv)
        Return "Dodano pole " & cv.Name
    End Function
    Public Function RemoveCustomColumnValue(name As String) As String
        For Each c In Me.CustomColumnValues
            If c.Name = name Then
                Me.CustomColumnValues.Remove(c)
                Return "Usunięto pole " & name
            End If
        Next
        Return "Nie znaleziono pola " & name & " , nie można usunąć"
    End Function

    Public Function AddVacationShedule(startdate As DateTime, enddate As DateTime, description As String, hourType As HoursType) As String
        Dim vs As New VacationType With {
            .StartDate = startdate,
            .EndDate = enddate,
            .Description = description,
            .HourType = hourType
        }

        Me.VacationShedule.Add(vs)
        Return "Dodano urlop"
    End Function

    Public Function RemoveVacationShedule(id As String)
        For Each v In Me.VacationShedule
            If v.Id = id Then
                Me.VacationShedule.Remove(v)
                Return "Usunięto urlop"
            End If
        Next

        Return ("Nie znaleziono urlopu do usunięcia")
    End Function


End Class
