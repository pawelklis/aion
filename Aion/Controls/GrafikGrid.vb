



Imports System.Drawing
Imports System.Windows.Forms

Public Class GrafikGrid
    Inherits MetroFramework.Controls.MetroGrid
    Implements MetroFramework.Interfaces.IMetroControl

    Public Sub New()
        Me.Style = MetroFramework.MetroColorStyle.Silver
    End Sub

    Private Sub _CellPainting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs) Handles Me.CellPainting

        If e.RowIndex > -1 And e.ColumnIndex > -1 Then

            Dim c As DataGridViewCell = Me.Rows(e.RowIndex).Cells(e.ColumnIndex)

            '  If SelectedCells.Contains(c) Then
            If e.RowIndex = Me.CurrentCell.RowIndex Then
                e.Paint(e.CellBounds, DataGridViewPaintParts.All)

                Using cpen As New Pen(Color.Red, 2)

                    e.Graphics.DrawRectangle(cpen, e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 3, e.CellBounds.Height - 3)

                End Using

                e.Handled = True

            End If

        End If

    End Sub

End Class
