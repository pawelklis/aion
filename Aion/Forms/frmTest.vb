Public Class frmTest
    Private Sub FrmTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.Main()

    End Sub
    Public Sub Bind(g As GrafikType)
        g.DataSrc(Me.dg1, 1)

    End Sub
End Class