Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms


Public Class AProgram
    Sub New()
        Me.Sesja = New SessionClass

        If My.Settings.serwer = "localhost" Then
            Dim ff As New frmConnectionSettings
            ff.ShowDialog()

        End If
        Me.CheckConnection()


    End Sub

    Public Property Sesja As SessionClass



    Private Function CheckConnection()
        Dim m As New MySqlCore2(My.Settings.serwer, My.Settings.dbname, My.Settings.pwd)
        If m.ConnectionTest = False Then
            Dim f As New frmConnectionSettings
            f.ShowDialog()
        End If
        Return True
    End Function

    Public Function CheckCompany() As Integer
        CheckCompany = mConnection.GetCount("select count(*) from " & EnumTables.company.ToString)
    End Function
    Public Function CheckSite() As Integer
        CheckSite = mConnection.GetCount("select count(*) from " & EnumTables.site.ToString)
    End Function
    Public Function CheckUser(idsite As Integer) As Integer
        CheckUser = mConnection.GetCount("select count(*) from " & EnumTables.user.ToString & " where idsite=" & idsite)
    End Function

    Shared Sub clear()
        mConnection.ExecuteNonQuery("delete from " & EnumTables.companydetail.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.sitedetail.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.site.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
        mConnection.ExecuteNonQuery("delete from " & EnumTables.company.ToString)
    End Sub




    Public Class SessionClass
        Public Property Company As CompanyType
        Public Property Site As SiteType
        Public Property User As UserType

    End Class

End Class

