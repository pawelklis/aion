
<DebuggerDisplay("Name={Name}")>
<Serializable> Public Class GroupType

    Public Property Id As String
    Public Property Name As String
    Public Property Yer As Integer
    Public Property Month As Integer
    Public Property Workers As List(Of WorkerType)
    Public Property Shifts As List(Of ShiftType)
    Public Sub New(name As String, yer As Integer, month As Integer)
        Me.Id = Guid.NewGuid.ToString
        Me.Name = name
        Me.Yer = yer
        Me.Month = month
        Me.Workers = New List(Of WorkerType)
    End Sub

    Public Function AddWorker(w As WorkerType) As String
        Me.Workers.Add(w)
        Return "Dodano pracownika " & w.Name
    End Function
    Public Function RemoveWorker(id As String) As String
        For Each w In Me.Workers
            If w.Id = id Then
                Me.Workers.Remove(w)
                Return "Usunięto pracownika " & w.Name
            End If
        Next
        Return "Nie znaleziono w grupie pracownika , nie usunięto danych"
    End Function

    Public Function AddShift(name As String, starttime As TimeSpan, endtime As TimeSpan, iswork As Boolean, hourtype As HoursType) As String
        If String.IsNullOrEmpty(name) Then Return "Nazwa jest wymagana"
        If Len(name) > 5 Then Return "Nazwa jest za długa, maksymalnie 5 znaków"

        For Each ss In Me.Shifts
            If ss.Name = name Then Return "Nazwa jest już zajęta"
        Next

        Dim s As New ShiftType With {
            .Name = name,
            .StartTime = starttime,
            .EndTime = endtime,
            .IsWork = iswork,
            .HourType = hourtype
        }

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
