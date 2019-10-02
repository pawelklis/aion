


Public Class UserType
    Public Property IdUser As Integer
    Public Property Login As String
    Public Property Pwd As String
    Public Property Name As String
    Public Property Mail As String
    Public Property Phone As String
    Public Property IdSite As Integer



    Public Sub Save()
        If Me.IdUser = 0 Then
            mConnection.Insert(EnumTables.user.ToString, EnumIDTables.iduser.ToString, Me)
        Else
            mConnection.Update(EnumTables.user.ToString, EnumIDTables.iduser.ToString, Me)
        End If
    End Sub

    Public Shared Function LoginUser(logn As String, pwd As String) As UserType
        LoginUser = mConnection.GetSingleObject(Of UserType)("select * from " & EnumTables.user.ToString & " 
where login='" & logn & "' and pwd ='" & pwd & "';")
    End Function

End Class
