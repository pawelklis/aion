<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserEdit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Login = New System.Windows.Forms.Label()
        Me.txlogin = New System.Windows.Forms.TextBox()
        Me.txname = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txmail = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtel = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txpwd = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txconfirmpwd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Login
        '
        Me.Login.AutoSize = True
        Me.Login.Location = New System.Drawing.Point(34, 39)
        Me.Login.Name = "Login"
        Me.Login.Size = New System.Drawing.Size(43, 19)
        Me.Login.TabIndex = 0
        Me.Login.Text = "Login"
        '
        'txlogin
        '
        Me.txlogin.Location = New System.Drawing.Point(127, 36)
        Me.txlogin.Name = "txlogin"
        Me.txlogin.Size = New System.Drawing.Size(189, 25)
        Me.txlogin.TabIndex = 1
        '
        'txname
        '
        Me.txname.Location = New System.Drawing.Point(127, 67)
        Me.txname.Name = "txname"
        Me.txname.Size = New System.Drawing.Size(189, 25)
        Me.txname.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 19)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nazwa"
        '
        'txmail
        '
        Me.txmail.Location = New System.Drawing.Point(127, 98)
        Me.txmail.Name = "txmail"
        Me.txmail.Size = New System.Drawing.Size(189, 25)
        Me.txmail.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Email"
        '
        'txtel
        '
        Me.txtel.Location = New System.Drawing.Point(127, 129)
        Me.txtel.Name = "txtel"
        Me.txtel.Size = New System.Drawing.Size(189, 25)
        Me.txtel.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 19)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Telefon"
        '
        'txpwd
        '
        Me.txpwd.Location = New System.Drawing.Point(127, 160)
        Me.txpwd.Name = "txpwd"
        Me.txpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txpwd.Size = New System.Drawing.Size(189, 25)
        Me.txpwd.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(34, 163)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 19)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Hasło"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(241, 224)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 33)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Zapisz"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'txconfirmpwd
        '
        Me.txconfirmpwd.Location = New System.Drawing.Point(127, 193)
        Me.txconfirmpwd.Name = "txconfirmpwd"
        Me.txconfirmpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txconfirmpwd.Size = New System.Drawing.Size(189, 25)
        Me.txconfirmpwd.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(34, 196)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 19)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Powtórz hasło"
        '
        'frmUserEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(933, 588)
        Me.Controls.Add(Me.txconfirmpwd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txpwd)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txmail)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txname)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txlogin)
        Me.Controls.Add(Me.Login)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmUserEdit"
        Me.Text = "frmUserEdit"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Login As Label
    Friend WithEvents txlogin As TextBox
    Friend WithEvents txname As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents txmail As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtel As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txpwd As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents txconfirmpwd As TextBox
    Friend WithEvents Label1 As Label
End Class
