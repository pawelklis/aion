

Imports System.Drawing

<Serializable> Public Class StyleType
    Public Property Id As String
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Font As Font
    Public Property ForeColor As color
    Public Property BackColor As Color

    Public Sub New()
        Me.Id = Guid.NewGuid.ToString
        Me.Font = New Font("Arial", 10, FontStyle.Regular)
        Me.ForeColor = Color.Black
        Me.BackColor = Color.Empty

    End Sub

End Class
