Public Class frmConnectionSettings
    Private Sub FrmConnectionSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txSerwer.Text = My.Settings.serwer
        txDB.Text = My.Settings.dbname
        txPWD.Text = My.Settings.pwd
    End Sub

    Function Test()
        Dim m As New MySqlCore2(txSerwer.Text, txDB.Text, txPWD.Text)
        If m.ConnectionTest = True Then
            MsgBox("Połączenie prawidłowe", MsgBoxStyle.Information)
            Return True
        Else
            MsgBox("Brak połączenia", MsgBoxStyle.Critical)
            Return False
        End If
    End Function

    Sub Save()
        My.Settings.serwer = txSerwer.Text
        My.Settings.dbname = txDB.Text
        My.Settings.pwd = txPWD.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Test()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Test() Then
            Save()
            Me.Close()
        Else

        End If
    End Sub
End Class