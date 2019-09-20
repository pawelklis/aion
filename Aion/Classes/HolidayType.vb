

<DebuggerDisplay("Name={Name} Data={data}")>
<Serializable> Public Class HolidayType
    Public Property Id As String
    Public Property Data As Date
    Public Property Name As String

    Public Sub New(data As Date, Optional name As String = "")
        Me.Id = Guid.NewGuid.ToString
        Me.Data = data
        Me.Name = name
    End Sub
End Class
