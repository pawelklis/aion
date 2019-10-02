

Imports Aion


Public Class frmSiteEdit

    Private site As SiteType
    Public Sub New(st As SiteType)
        Me.site = st

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FrmSiteEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txName.Text = Me.site.Name
        dg1.DataSource = Me.site.Details

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.site.Name = txName.Text

        For Each det In Me.site.Details
            For i = 0 To dg1.Rows.Count - 1
                Dim f As String = dg1.Rows(i).Cells("field").Value.ToString
                Dim v As String = dg1.Rows(i).Cells("val").Value.ToString
                If f = det.Field Then
                    det.Val = v
                    det.Save()
                End If


            Next
        Next
        Me.site.Save()
        Me.Close()

    End Sub
End Class