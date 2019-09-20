
<DebuggerDisplay("Rok={yer}")>
<Serializable> Public Class GrafikType
    Public Property Id As String
    Public Property Name As String
    Public Property Yer As Integer
    Public Property Months As List(Of MonthType)
    Public Property CustomFieldsDictionary As List(Of String)
    Public Property CustomColumns As List(Of CustomColumnType)
    Public Property Shifts As List(Of ShiftType)
    Public Property HourTypes As List(Of HoursType)
    Public Sub New(yer As Integer)
        Me.Id = Guid.NewGuid.ToString
        Me.Yer = yer
        Me.CustomFieldsDictionary = New List(Of String)
        Me.months = New List(Of MonthType)
        For i = 1 To 12
            Me.AddMonth(i)
        Next
        Me.CustomColumns = New List(Of CustomColumnType)

    End Sub

    Private Sub AddMonth(number As Integer)
        Dim M As New MonthType(number, Me.Yer)
        Me.Months.Add(M)
    End Sub

    Function AddCustomWorkerField(name As String) As String

        If Me.CustomFieldsDictionary.Contains(name) Then
            Return "Pole już istnieje, nie można dodać dwóch pól o tej samej nazwie"
        End If

        For Each m In Me.Months
            For Each group In m.Groups
                For Each worker In group.Workers
                    worker.AddCustomField(name)
                Next
            Next
        Next
        Return "Dodano pole " & name
    End Function

    Function RemoveCustomWorkerField(name As String) As String
        For Each cf In Me.CustomFieldsDictionary
            If cf = name Then
                Me.CustomFieldsDictionary.Remove(cf)
                For Each m In Me.Months
                    For Each group In m.Groups
                        For Each worker In group.Workers
                            worker.RemoveCustomField(name)
                        Next
                    Next
                Next
                Return "Usunięto pole " & name
            End If
        Next
        Return "Nie znaleziono pola " & name & " do usunięcia"
    End Function

    Public Function AddCustomColumn(CustomCol As CustomColumnType) As String
        For Each cc In Me.CustomColumns
            If cc.Name = Name Then
                Return "Kolumna " & CustomCol.Name & " już istnieje, nie można dodać"
            End If
        Next

        For Each m In Me.Months
            For Each group In m.Groups
                For Each worker In group.Workers
                    worker.AddCustomColumnValue(New CustomColumnValueType(CustomCol.Name))
                Next
            Next
        Next

        Me.CustomColumns.Add(CustomCol)
        Return "Dodano kolumnę " & CustomCol.Name
    End Function

    Public Function RemoveCustomColumn(name As String) As String
        For Each c In Me.CustomColumns
            If c.Name = name Then
                Me.CustomColumns.Remove(c)

                For Each m In Me.Months
                    For Each group In m.Groups
                        For Each w In group.Workers
                            w.RemoveCustomColumnValue(name)
                        Next
                    Next
                Next
                Return "Usunięto kolumnę " & name
            End If
        Next
        Return "Nie znaleziono kolumny " & name & " , nie można usunąć."
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


    Public Function AddHourType(name As String, starttime As TimeSpan, endtime As TimeSpan, iswork As Boolean) As String
        If String.IsNullOrEmpty(name) Then Return "Nazwa jest wymagana"


        For Each ss In Me.HourTypes
            If ss.Name = name Then Return "Nazwa jest już zajęta"
        Next

        Dim s As New HoursType
        s.Name = name


        Me.HourTypes.Add(s)
        Return "Dodano rodzja godzin"
    End Function
    Public Function RemoveHourType(name As String) As Boolean
        For Each s In Me.HourTypes
            If s.Name = name Then
                Me.HourTypes.Remove(s)
                Return "Usunięto rodzaj godzin"
            End If
        Next
        Return "Nie znalezniono rodzaju godzin " & name & ", nie można usunąć"
    End Function

End Class
