
Imports Aion

Public Class frmUserEdit
    Private user As UserType
    Public Sub New(usr As UserType)
        Me.user = usr
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub FrmUserEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txlogin.Text = user.Login
        txname.Text = user.Name
        txmail.Text = user.Mail
        txtel.Text = user.Phone
        txpwd.Text = user.Pwd
        txconfirmpwd.Text = user.Pwd
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        user.Login = txlogin.Text
        user.Name = txname.Text
        user.Mail = txmail.Text
        user.Phone = txtel.Text
        user.Pwd = txpwd.Text
        user.save
        Me.Close()

    End Sub
End Class