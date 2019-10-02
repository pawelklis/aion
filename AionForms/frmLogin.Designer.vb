<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.txlogin = New System.Windows.Forms.TextBox()
        Me.txpass = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.AionForms.My.Resources.Resources.logo1
        Me.PictureBox1.Location = New System.Drawing.Point(1, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(111, 57)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'txlogin
        '
        Me.txlogin.Location = New System.Drawing.Point(130, 134)
        Me.txlogin.Name = "txlogin"
        Me.txlogin.Size = New System.Drawing.Size(155, 25)
        Me.txlogin.TabIndex = 1
        '
        'txpass
        '
        Me.txpass.Location = New System.Drawing.Point(130, 165)
        Me.txpass.Name = "txpass"
        Me.txpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txpass.Size = New System.Drawing.Size(155, 25)
        Me.txpass.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(130, 207)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(155, 38)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Zaloguj"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(404, 335)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txpass)
        Me.Controls.Add(Me.txlogin)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmLogin"
        Me.Text = "frmLogin"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txlogin As TextBox
    Friend WithEvents txpass As TextBox
    Friend WithEvents Button1 As Button
End Class
