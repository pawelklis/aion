Imports System.Windows.Forms

Public Class frmTest

    Dim Gr As GrafikType
    Private Sub FrmTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.Main()

    End Sub
    Public Sub Bind(g As GrafikType)
        Gr = g

        'g.SetValue(Me.dg1.Rows(1).Cells("workerguid").Value, Gr.Shifts(0).Id, 1, 1, DayType.EntryTypeValue.Planned)

        g.DataSrc(Me.dg1, 0)

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Bind(Gr)
    End Sub

    Private Sub Dg1_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs) Handles dg1.CellContentClick

    End Sub

    Private Sub dg1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dg1.CellEndEdit

    End Sub
End Class