

Public Class SiteDetailType
    Public Property IdSiteDetail As Integer
    Public Property Field As String
    Public Property Val As String
    Public Property IdSite As Integer

    Public Sub Save()
        If Me.IdSiteDetail = 0 Then
            mConnection.Insert(EnumTables.sitedetail.ToString, EnumIDTables.idsitedetail.ToString, Me)
        Else
            mConnection.Update(EnumTables.sitedetail.ToString, EnumIDTables.idsitedetail.ToString, Me)
        End If
    End Sub

End Class
