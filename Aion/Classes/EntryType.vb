



<DebuggerDisplay("Name={Name}")>
<Serializable> Public Class EntryType

    Public Property Id As String
    Public Property Name As String

    Public Property Entries As List(Of ShiftType)

    Public Sub New()
        Me.Id = Guid.NewGuid.ToString
        Me.Entries = New List(Of ShiftType)

    End Sub

End Class
