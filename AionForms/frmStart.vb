Imports Aion

Public Class frmStart
    Private Sub FrmStart_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitProgram()

    End Sub

    Sub InitProgram()

        If MsgBox("Wyczyścić", vbYesNo) = MsgBoxResult.Yes Then
            AProgram.clear()
        End If

        If IsNothing(aProg) Then aProg = New AProgram


companyCheck:
        Dim CompanyCount As Integer = 0
        CompanyCount = aProg.CheckCompany()

        If CompanyCount = 0 Then
            'show adding Company form
            Dim f As New frmCompanyEdit(New CompanyType(""))
            f.ShowDialog()
            CompanyCount = aProg.CheckCompany()
        End If
        If CompanyCount = 1 Then
            'select sesja company
            Dim cs As CompanyType = CompanyType.LoadOne
            aProg.Sesja.Company = cs
            GoTo sitecheck
        End If
        If CompanyCount > 1 Then
            'show company select form
            frmSelectCompany.ShowDialog()

        End If
        If CompanyCount = 0 Then
            If MsgBox("Aby kontynuować wymagane jest dodanie danych firmy, czy chcesz kontynuować?", vbYesNo) = MsgBoxResult.No Then
                Me.Close()
                Exit Sub

            End If
            GoTo companyCheck
        End If

sitecheck:
        If IsNothing(aProg.Sesja.Company) Then
            MsgBox("Błąd, brak firmy lub brak połączenia z bazą danych.")
            Me.Close()
            Exit Sub
        End If

        Dim SiteCount As Integer = 0
        SiteCount = aProg.CheckSite()
        If SiteCount = 0 Then
            'show adding site form
            Dim f As New frmSiteEdit(New SiteType("", aProg.Sesja.Company.IdCompany))
            f.ShowDialog()
            SiteCount = aProg.CheckSite()
        End If
        If SiteCount = 1 Then
            'select sesja site
            Dim ss As SiteType = SiteType.LoadOne(aProg.Sesja.Company.IdCompany)
            aProg.Sesja.Site = ss
            GoTo usercheck
        End If
        If SiteCount > 1 Then
            'show site select form
            frmSelectSite.ShowDialog()

        End If
        If IsNothing(aProg.Sesja.Site) Then
            GoTo sitecheck
        End If
usercheck:
        If IsNothing(aProg.Sesja.Site) Then
            Dim f As New frmSiteEdit(New SiteType("", aProg.Sesja.Company.IdCompany))
            f.ShowDialog()

        End If
        Dim UserCount As Integer = 0
        UserCount = aProg.CheckUser(aProg.Sesja.Site.IdSite)
        If UserCount = 0 Then
            'show user add form
            Dim f As New frmUserEdit(New UserType With {.IdSite = aProg.Sesja.Site.IdSite})
            f.ShowDialog()
            UserCount = aProg.CheckUser(aProg.Sesja.Site.IdSite)
            GoTo usercheck

        End If
        If UserCount > 0 Then
            'show login form
            frmLogin.ShowDialog()

        End If
    End Sub

End Class
