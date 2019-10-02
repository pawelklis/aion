Imports Aion


Public Class frmLogin
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim us As UserType = UserType.LoginUser(txlogin.Text, txpass.Text)
        If IsNothing(us) Then
            MsgBox("Błędny login lub hasło")
            Exit Sub
        End If
        If us.IdSite <> aProg.Sesja.Site.IdSite Then
            MsgBox("Użytkownik nie ma dostępu do tej lokalizacji, skontaktuj się z administratorem")
            Exit Sub
        End If

        aProg.Sesja.User = us
        Me.Close()


    End Sub
End Class