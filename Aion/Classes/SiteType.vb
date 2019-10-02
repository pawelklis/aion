



Public Class SiteType
    Public Property IdSite As Integer
    Public Property Name As String
    Public Property IdCompany As Integer
    Sub New()

    End Sub
    Public Sub New(name As String, idcompany As Integer)
        Me.Name = name
        Me.IdCompany = idcompany
        Me.Save()

        Dim cd As SiteDetailType
        cd = New SiteDetailType With {.Field = "Miasto", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Ulica", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Nr domu", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Nr lokalu", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Kod pocztowy", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "NIP", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Regon", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Telefon", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "Fax", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
        cd = New SiteDetailType With {.Field = "E-mail", .IdSite = Me.IdSite, .Val = ""}
        cd.Save()
    End Sub

    Public Function Details() As List(Of SiteDetailType)
        Details = mConnection.GetObject(Of SiteDetailType)("select * 
from " & EnumTables.sitedetail.ToString & "
where " & EnumIDTables.idsite.ToString & " = " & Me.IdSite & ";")
    End Function
    Public Sub Save()
        If Me.IdSite = 0 Then
            mConnection.Insert(EnumTables.site.ToString, EnumIDTables.idsite.ToString, Me)
        Else
            mConnection.Update(EnumTables.site.ToString, EnumIDTables.idsite.ToString, Me)
        End If
    End Sub

    Public Shared Function Load(idcompany As Integer) As List(Of SiteType)
        Load = mConnection.GetObject(Of SiteType)("select * from " & EnumTables.site.ToString & " 
where " & EnumIDTables.idcompany.ToString & " =" & idcompany & ";")
    End Function
    Public Shared Function LoadOne(idCompany As Integer) As SiteType
        LoadOne = mConnection.GetSingleObject(Of SiteType)("select * 
from " & EnumTables.site.ToString & "
where " & EnumIDTables.idcompany.ToString & "=" & idCompany & ";")
    End Function
    Public Shared Function LoadId(id As Integer) As SiteType
        LoadId = mConnection.GetSingleObject(Of SiteType)("select * 
from " & EnumTables.site.ToString & "
where " & EnumIDTables.idsite.ToString & "=" & id & ";")
    End Function

End Class
