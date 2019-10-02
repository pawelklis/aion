


Public Class ShiftType

    Public Property IdShift As Integer
    Public Property Name As String '07:30-19:30   len=11  max=15
    Public Property StartTime As String 'max=45
    Public Property EndTime As String    'max=45
    Public Property IsWork As Integer
    Public Property IdSite As Integer
    Public Property IdWorker As Integer
    Public Property IdGroup As Integer


    Public Sub Save()
        If Me.IdShift = 0 Then
            mConnection.Insert(EnumTables.shift.ToString, EnumIDTables.idshift.ToString, Me)
        Else
            mConnection.Update(EnumTables.shift.ToString, EnumIDTables.idshift.ToString, Me)
        End If
    End Sub

End Class
