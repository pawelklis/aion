Imports Aion

Public Class frmCompanyEdit

    Private Company As CompanyType
    Public Sub New(cpm As companytype)
        Me.Company = cpm





        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub FrmCompanyEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txName.Text = Company.Name

        dg1.DataSource = Company.Details

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Company.Name = txName.Text

        For Each det In Company.Details
            For i = 0 To dg1.Rows.Count - 1
                Dim f As String = dg1.Rows(i).Cells("field").Value.ToString
                Dim v As String = dg1.Rows(i).Cells("val").Value.ToString
                If f = det.Field Then
                    det.Val = v
                    det.Save()
                End If


            Next
        Next
        Company.Save()
        Me.Close()


    End Sub
End Class