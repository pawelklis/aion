

Public Class CompanyType

    Public Property IdCompany As Integer
    Public Property Name As String

    Public Sub New()

    End Sub
    Public Sub New(name As String)
        Me.Name = name
        Me.Save()

        Dim cd As CompanyDetailType
        cd = New CompanyDetailType With {.Field = "Miasto", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Ulica", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Nr domu", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Nr lokalu", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Kod pocztowy", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "NIP", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Regon", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Telefon", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "Fax", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
        cd = New CompanyDetailType With {.Field = "E-mail", .IdCompany = Me.IdCompany, .Val = ""}
        cd.Save()
    End Sub

    Public Function Details() As List(Of CompanyDetailType)
        Details = mConnection.GetObject(Of CompanyDetailType)("select * 
from " & EnumTables.companydetail.ToString & "
where " & EnumIDTables.idcompany.ToString & " = " & Me.IdCompany & ";")
    End Function

    Public Sub Save()
        Dim m As MySqlCore2 = mConnection()
        If Me.IdCompany = 0 Then
            m.Insert(EnumTables.company.ToString, EnumIDTables.idcompany.ToString, Me)
        Else
            m.Update(EnumTables.company.ToString, EnumIDTables.idcompany.ToString, Me)
        End If
    End Sub

    Public Shared Function Load() As List(Of CompanyType)
        Load = mConnection.GetObject(Of CompanyType)("select * from " & EnumTables.company.ToString & " ;")
    End Function
    Public Shared Function LoadOne() As CompanyType
        LoadOne = mConnection.GetSingleObject(Of CompanyType)("select * from " & EnumTables.company.ToString & " ;")
    End Function
    Public Shared Function Load(id As Integer) As CompanyType
        Load = mConnection.GetSingleObject(Of CompanyType)("select * 
from " & EnumTables.company.ToString & "
where " & EnumIDTables.idcompany.ToString & "=" & id & ";")
    End Function
End Class
