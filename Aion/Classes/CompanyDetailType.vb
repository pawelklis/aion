


Public Class CompanyDetailType

    Public Property IdCompanyDetail As Integer
    Public Property Field As String
    Public Property Val As String
    Public Property IdCompany As Integer


    Public Sub Save()
        If Me.IdCompanyDetail = 0 Then
            mConnection.Insert(EnumTables.companydetail.ToString, EnumIDTables.idcompanydetail.ToString, Me)
        Else
            mConnection.Update(EnumTables.companydetail.ToString, EnumIDTables.idcompanydetail.ToString, Me)
        End If
    End Sub

End Class
