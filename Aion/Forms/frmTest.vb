Imports System.Windows.Forms

Public Class frmTest

    Dim Gr As GrafikType
    Private Sub FrmTest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Module1.Main()
        '    Me.dg1.SelectionMode = SelectionMode.One
    End Sub
    Public Sub Bind(g As GrafikType)
        Gr = g

        'g.SetValue(Me.dg1.Rows(1).Cells("workerguid").Value, Gr.Shifts(0).Id, 1, 1, DayType.EntryTypeValue.Planned)

        g.DataSrc(Me.dg1, 0)


        'Dim dt As New DataTable
        'dt.Columns.Add("a")
        'dt.Columns.Add("b")
        'For i = 1 To 10
        '    dt.Rows.Add(i, "sdafasdfasd")
        'Next
        'dg1.DataSource = dt

        'dg1.SelectionMode = DataGridViewSelectionMode.CellSelect
        '   MsgBox(dg1.SelectionMode.ToString)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Bind(Gr)
        '   Me.dg1.SelectionMode = SelectionMode.One
    End Sub

    Private Sub Dg1_CellContentClick(sender As Object, e As Windows.Forms.DataGridViewCellEventArgs)

    End Sub

    Private Sub dg1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub dg1_KeyUp(sender As Object, e As KeyEventArgs)

    End Sub
End Class