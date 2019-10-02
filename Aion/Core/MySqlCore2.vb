

Imports MySql.Data.MySqlClient
Imports System.IO
    Imports System.Reflection
    Imports System.Text


''' <summary>
'''   ConnectionTest
'''   ExecuteNonQuery
'''   ExecuteScalar
'''   FillDatatable
'''   GetTablesList
'''   GetColumnsList
'''   Insert
'''   InsertList
'''   InsertOrUpdate
'''   Update
'''   UpdateList
'''   GetCount
'''   GetObject
'''   GetObject(list)
'''   Delete
'''   Truncate
'''   DropTable
'''   CreateTable
'''   AddColumn
'''   DeleteColumn
''' </summary>
Public Class MySqlCore2

    Public ReadOnly Property ConnectionStr As String
    Public Property ErrorHistory As Dictionary(Of DateTime, String)
    Public Property EventHistory As Dictionary(Of DateTime, String)
    Public Property LastError As String

    Public ReadOnly ConParameters As Parameters
    Class Parameters
        Public Property Database As String
        Public Property Password As String
        Public Property Server As String
        Public Property SecondServer As String
        Public Property User As String
        Public Property Port As Integer
        Public Property CharacterSet As String
        Public Property ConnectionTimeout As Integer
        Public Property ConvertZeroDate As Boolean
        Public Property UseCompression As Boolean

    End Class
    Public Function ConString() As String
        Return Me.ConnectionStr
    End Function


    Public Property con As MySqlConnection
    Public Property com As MySqlCommand
    Public Property adpt As MySqlDataAdapter

    Public Sub EventHistoryAdd()
        If IsNothing(Me.EventHistory) Then Me.EventHistory = New Dictionary(Of Date, String)
        Dim text As String = ""
        If Not IsNothing(com) Then
            If Not IsNothing(com.CommandText) Then
                text = text & " " & vbNewLine & com.CommandText
            End If
        End If
        Try
            Me.EventHistory.Add(Now, text)
            ' SaveLog(Now & "   _   " & text & vbNewLine & "_______________________________________________________________________________")

        Catch ex As Exception

        End Try

    End Sub
    Public Sub ErrorHistoryAdd(ex As Exception)
        If IsNothing(Me.ErrorHistory) Then Me.ErrorHistory = New Dictionary(Of Date, String)
        Me.LastError = ex.ToString

        Dim text As String = ""
        If Not IsNothing(com) Then
            If Not IsNothing(com.CommandText) Then
                text = text & " " & vbNewLine & com.CommandText
            End If
        End If
        Me.ErrorHistory.Add(Now, "EventType:" & vbNewLine & text & vbNewLine & vbNewLine & "Exception:" & vbNewLine & ex.ToString)
        SaveLog(Now & "   _   " & "EventType:" & vbNewLine & text & vbNewLine & vbNewLine & "Exception:" & vbNewLine & ex.ToString & vbNewLine & "_______________________________________________________________________________")
    End Sub
    Public Sub SaveLog(extext As String)
        'WYSTEPUJE JESZCZE RAZ W PUBLIC METHODS
        Try
            Dim pat As String ' = SciezkaLog() & "\" & Year(Now) & "_" & Month(Now) & "_" & Now.Day & "_" & Polacz.ConParameters.Database
            If Directory.Exists(pat) = False Then
                Directory.CreateDirectory(pat)
            End If
            pat = pat & "\log_" & Now.ToShortDateString.Replace("-", "_").Replace(":", "__") & ".txt"

            Dim filePath As String = pat

            'Using writer As New StreamWriter(filePath, True)
            '    If File.Exists(filePath) Then
            '        writer.WriteLine(extext)
            '    Else
            '        writer.WriteLine(extext)
            '    End If
            'End Using

            File.AppendAllText(pat, extext & vbNewLine & vbNewLine & vbNewLine)
        Catch ex As Exception

        End Try

    End Sub

    Public Sub New(server As String, database As String, password As String, Optional SecondServer As String = Nothing,
                       Optional UseCompression As Boolean = True, Optional CharacterSet As String = "utf8",
                       Optional user As String = "root", Optional port As Integer = 3306,
                       Optional ConnectionTimeout As Integer = 9999999, Optional ConvertZeroDate As Boolean = True)
        Me.ConParameters = New Parameters
        With Me.ConParameters
            .Database = database
            .Password = password
            .Server = server
            .User = user
            .Port = port
            .CharacterSet = CharacterSet
            .ConnectionTimeout = ConnectionTimeout
            .ConvertZeroDate = ConvertZeroDate
            .UseCompression = UseCompression
            .SecondServer = SecondServer
        End With



        Dim ConnectionString As String = "SERVER=" & server & ";UID=" & user & ";DATABASE=" & database & ";PORT=" & port & ";password=" & password & ";  Character Set=" & CharacterSet & "; Connect Timeout=" & ConnectionTimeout & ";Pooling=false;Convert Zero Datetime=" & ConvertZeroDate & ";"

        Me.ConnectionStr = ConnectionString
        If Not IsNothing(SecondServer) Then
            If Me.ConnectionTest = False Then
                ConnectionString = "SERVER=" & SecondServer & ";UID=" & user & ";DATABASE=" & database & ";PORT=" & port & ";password=" & password & ";  Character Set=" & CharacterSet & "; Connect Timeout=" & ConnectionTimeout & ";Pooling=false;Convert Zero Datetime=" & ConvertZeroDate & ";"
                Me.ConnectionStr = ConnectionString
            End If
        End If

    End Sub
    ''' <summary>
    ''' Test połączenia z bazą danych
    ''' </summary>
    ''' <returns></returns>
    Public Function ConnectionTest() As Boolean

        Dim testcon As New MySqlConnection(Me.ConnectionStr)
        Try
            testcon.Open()
            Return True
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return False
        Finally
            testcon.Close()
        End Try
        EventHistoryAdd()
    End Function
    Public Function Connection() As MySqlConnection
        If Not IsNothing(con) Then
            Try
                con.Close()
            Catch ex As Exception

            End Try


        End If

        con = New MySqlConnection(ConnectionStr)
        con.Open()
        Return con
    End Function
    Public Function toDict(ob As Object) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)
        Dim info() As PropertyInfo = ob.GetType().GetProperties()
        For Each p In info
            dict.Add(p.Name.ToLower, p.GetValue(ob, Nothing))
        Next
        Return dict
    End Function
#Region "Commands"



    Public Function ExecuteNonQuery(sqlCommand As String) As Boolean
        Try
            com = New MySqlCommand(sqlCommand, Connection)
            com.ExecuteNonQuery()
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return False
        End Try
        EventHistoryAdd()
        Return True
    End Function
    Public Function ExecuteScalar(sqlCommand As String) As String
        com = New MySqlCommand(sqlCommand, Connection)
        Try

            ExecuteScalar = com.ExecuteScalar()
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return False
        Finally
            Connection.Close()

        End Try
        EventHistoryAdd()
        Return ExecuteScalar
    End Function
    Public Function FillDatatable(sqlCommand As String) As DataTable
        Dim dt As New DataTable

        Try
            com = New MySqlCommand(sqlCommand, Connection)
            adpt = New MySqlDataAdapter(sqlCommand, con)
            adpt.Fill(dt)
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return Nothing
        Finally
            Connection.Close()
        End Try
        EventHistoryAdd()
        Return dt
    End Function
    Public Function GetTablesList() As List(Of String)
        GetTablesList = New List(Of String)
        Try
            com = New MySqlCommand("Show tables", Connection)
            Dim r As MySqlDataReader = com.ExecuteReader
            While r.Read()
                GetTablesList.Add(r.Item(0).ToString)
            End While

        Catch ex As Exception
            ErrorHistoryAdd(ex)
        Finally
            Connection.Close()

        End Try
        EventHistoryAdd()
    End Function
    Public Function GetColumnsList(TableName As String) As List(Of Tuple(Of String, String, String, String, String, String))
        GetColumnsList = New List(Of Tuple(Of String, String, String, String, String, String))
        GetColumnsList.Add(New Tuple(Of String, String, String, String, String, String)("Field", "Type", "Null", "Key", "Default", "Extra"))
        Try
            com = New MySqlCommand("Show columns from " & TableName, Connection)
            Dim r As MySqlDataReader = com.ExecuteReader
            While r.Read()
                Dim item As New Tuple(Of String, String, String, String, String, String)(r.Item(0).ToString, r.Item(1).ToString, r.Item(2).ToString, r.Item(3).ToString, r.Item(4).ToString, r.Item(5).ToString)
                GetColumnsList.Add(item)
            End While

        Catch ex As Exception
            ErrorHistoryAdd(ex)
        Finally
            Connection.Close()
        End Try
        EventHistoryAdd()
    End Function


#End Region
#Region "INSERT"
    Public Function GetInsertString(TableName As String, IdColumn As String, PropertiesDict As Dictionary(Of String, Object)) As String
        Dim command As String = ""
        Dim strNazwypol As String = ""
        Dim strParametry As String = ""
        For Each p In PropertiesDict
            '         If p.Key <> IdColumn Then
            strNazwypol = strNazwypol & p.Key.ToLower & ","
            strParametry = strParametry & "@" & p.Key.ToLower & ","
            '         End If

        Next
        strNazwypol = strNazwypol.Substring(0, Len(strNazwypol) - 1)
        strParametry = strParametry.Substring(0, Len(strParametry) - 1)
        command = "INSERT INTO " & TableName & " (" & strNazwypol & ") VALUES (" & strParametry & ");select LAST_INSERT_ID();"
        Return command
    End Function
    Public Function GetInsertStringWithoutID(TableName As String, IdColumn As String, PropertiesDict As Dictionary(Of String, Object)) As String
        Dim command As String = ""
        Dim strNazwypol As String = ""
        Dim strParametry As String = ""
        For Each p In PropertiesDict
            If p.Key <> IdColumn Then
                strNazwypol = strNazwypol & p.Key & ","
                strParametry = strParametry & "@" & p.Key & ","
            End If

        Next
        strNazwypol = strNazwypol.Substring(0, Len(strNazwypol) - 1)
        strParametry = strParametry.Substring(0, Len(strParametry) - 1)
        command = "INSERT INTO " & TableName & " (" & strNazwypol & ") VALUES (" & strParametry & ");select LAST_INSERT_ID();"
        Return command
    End Function
    Public Function Insert(TableName As String, IdColumn As String, ob As Object) As Boolean

        Dim PropertiesDict As New Dictionary(Of String, Object)
        PropertiesDict = toDict(ob)
        Dim command As String = GetInsertString(TableName, IdColumn, PropertiesDict)

        Try



            com = New MySqlCommand(command, Connection)
            com.CommandType = CommandType.Text
            For Each p In PropertiesDict
                com.Parameters.AddWithValue("@" & p.Key, p.Value)
            Next


            Dim info() As PropertyInfo = ob.GetType().GetProperties()
            For Each p In info
                If p.Name.ToLower = IdColumn.ToLower Then
                    Dim id As Integer = com.ExecuteScalar()
                    p.SetValue(ob, id, Nothing)
                End If
            Next

        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return False

        Finally
            con.Close()
            con.Dispose()
            con = Nothing
        End Try
        EventHistoryAdd()
        Return True
    End Function
    ''' <summary>
    ''' Insert Listy Obiektów za pomocą transakcji
    ''' </summary>
    ''' <param name="TableName"></param>
    ''' <param name="ListOb"></param>
    ''' <returns></returns>
    Public Function InsertList(TableName As String, idColumn As String, ListOb As List(Of Object), Optional BezID As Boolean = True) As Boolean
        If ListOb.Count = 0 Then
            LastError = "Lista nie posiada obiektów"
            Return False
        End If

        Dim PropertiesDict As New Dictionary(Of String, Object)
        PropertiesDict = toDict(ListOb(0))
        Dim command As String = GetInsertString(TableName, idColumn, PropertiesDict)
        If BezID = False Then
            command = GetInsertStringWithoutID(TableName, idColumn, PropertiesDict)
        End If
        Dim tr As MySqlTransaction
        Try

            com = New MySqlCommand(command, Connection)
            tr = con.BeginTransaction()
            com.Transaction = tr

            For Each ob In ListOb
                PropertiesDict = New Dictionary(Of String, Object)
                PropertiesDict = toDict(ob)

                com.CommandType = CommandType.Text
                com.Parameters.Clear()

                For Each p In PropertiesDict
                    com.Parameters.AddWithValue("@" & p.Key, p.Value)
                Next

                Dim info() As PropertyInfo = ob.GetType().GetProperties()
                Dim ec As Boolean = False
                For Each p In info
                    If p.Name = idColumn Then
                        p.SetValue(ob, com.ExecuteNonQuery(), Nothing)
                        ec = True
                        Exit For
                    End If
                Next
                If ec = False Then
                    Try
                        com.ExecuteNonQuery()
                    Catch ex As Exception

                    End Try

                End If
            Next
            'com.ExecuteNonQuery()
            tr.Commit()


        Catch ex As Exception
            tr.Rollback()
            ErrorHistoryAdd(ex)
            Return False

        Finally
            con.Close()
            con.Dispose()
            con = Nothing
        End Try

        EventHistoryAdd()
        Return True

    End Function
#End Region
#Region "UPDATE"
    Public Function GetUpdatetString(TableName As String, idColumnName As String, PropertiesDict As Dictionary(Of String, Object)) As String
        Dim command As String = ""
        Dim strNazwypol As String = ""
        Dim strParametry As String = ""
        Dim strPOLE As String = ""
        For Each p In PropertiesDict
            If p.Key <> idColumnName Then
                strPOLE = strPOLE & p.Key & "=" & "@" & p.Key & ","
            End If
        Next

        strPOLE = strPOLE.Substring(0, Len(strPOLE) - 1)

        command = "UPDATE " & TableName & " SET " & strPOLE & " WHERE " & idColumnName & "=@" & idColumnName & " ;"

        Return command
    End Function
    Public Function InsertOrUpdate(TableName As String, idColumnName As String, ob As Object) As Boolean

        Dim PropertiesDict As New Dictionary(Of String, Object)
        PropertiesDict = toDict(ob)
        Dim wId
        For Each p In PropertiesDict
            If p.Key = idColumnName Then
                wId = p.Value
                Exit For
            End If
        Next
        Dim cont As Integer = GetCount("select count(*) from " & TableName & " WHERE " & idColumnName & "='" & wId & "';")

        If cont = 0 Then
            Return Insert(TableName, idColumnName, ob)
        Else
            Return Update(TableName, idColumnName, ob)
        End If
        EventHistoryAdd()
    End Function
    Public Function Update(TableName As String, idColumnName As String, ob As Object) As Boolean

        Dim PropertiesDict As New Dictionary(Of String, Object)
        PropertiesDict = toDict(ob)
        Dim command As String = GetUpdatetString(TableName, idColumnName, PropertiesDict)

        Try
            com = New MySqlCommand(command, Connection)
            com.CommandType = CommandType.Text
            For Each p In PropertiesDict
                com.Parameters.AddWithValue("@" & p.Key, p.Value)
            Next
            com.ExecuteNonQuery()

        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return False

        Finally
            con.Close()
            con.Dispose()
            con = Nothing
        End Try
        EventHistoryAdd()
        Return True
    End Function
    ''' <summary>
    ''' Update Listy Obiektów za pomocą transakcji
    ''' </summary>
    ''' <param name="TableName"></param>
    ''' <param name="ListOb"></param>
    ''' <returns></returns>
    Public Function UpdateList(TableName As String, idColumnName As String, ListOb As List(Of Object)) As Boolean
        If ListOb.Count = 0 Then
            LastError = "Lista nie posiada obiektów"
            Return False
        End If

        Dim PropertiesDict As New Dictionary(Of String, Object)
        PropertiesDict = toDict(ListOb(0))
        Dim command As String = GetUpdatetString(TableName, idColumnName, PropertiesDict)

        Dim tr As MySqlTransaction
        Try

            com = New MySqlCommand(command, Connection)
            tr = con.BeginTransaction()
            com.Transaction = tr

            For Each ob In ListOb
                PropertiesDict = New Dictionary(Of String, Object)
                PropertiesDict = toDict(ob)

                com.CommandType = CommandType.Text
                com.Parameters.Clear()

                For Each p In PropertiesDict
                    com.Parameters.AddWithValue("@" & p.Key, p.Value)
                Next
                com.ExecuteNonQuery()

            Next

            tr.Commit()


        Catch ex As Exception
            tr.Rollback()
            ErrorHistoryAdd(ex)
            Return False

        Finally
            con.Close()
            con.Dispose()
            con = Nothing
        End Try
        EventHistoryAdd()
        Return True

    End Function
#End Region
#Region "GET"
    ''' <summary>
    ''' Zwraca String
    ''' </summary>
    ''' <param name="sqlCommand"></param>
    ''' <returns></returns>
    Public Function GetString(sqlCommand As String, Optional ReturnValueIffNull As String = "") As String
        GetString = ReturnValueIffNull
        Try
            com = New MySqlCommand(sqlCommand, Connection)
            Dim rd As MySqlDataReader = com.ExecuteReader
            While rd.Read
                GetString = rd(0).ToString
            End While


        Catch ex As Exception
            ErrorHistoryAdd(ex)
            GetString = ReturnValueIffNull

        Finally
            Connection.Close()

        End Try

        If IsNumeric(ReturnValueIffNull) Then
            If Not IsNumeric(GetString) Then
                GetString = ReturnValueIffNull
            End If
        End If

        EventHistoryAdd()
    End Function
    ''' <summary>
    ''' Zwraca Byte
    ''' </summary>
    ''' <param name="sqlCommand"></param>
    ''' <returns></returns>
    Public Function GetByte(sqlCommand As String) As Byte()
        Try
            com = New MySqlCommand(sqlCommand, Connection)
            Dim rd As MySqlDataReader = com.ExecuteReader
            While rd.Read
                GetByte = rd(0)
            End While


        Catch ex As Exception
            ErrorHistoryAdd(ex)
            GetByte = Nothing

        Finally
            Connection.Close()

        End Try
        EventHistoryAdd()
    End Function
    ''' <summary>
    ''' Zwraca liczbę rekordów Integer
    ''' </summary>
    ''' <param name="sqlCommand"></param>
    ''' <returns></returns>
    Public Function GetCount(sqlCommand As String) As Integer
        Try
            com = New MySqlCommand(sqlCommand, Connection)
            GetCount = com.ExecuteScalar
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            GetCount = 0
        Finally
            If GetCount < 0 Then GetCount = 0
            Connection.Close()
        End Try
        EventHistoryAdd()
    End Function
    ''' <summary>
    ''' Tworzy dowolny obiekt na podstawie dowolnej klasy z pojedynczego rekordu z tabeli
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="TableName"></param>
    ''' <param name="idColumnName"></param>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetObject(Of T As New)(TableName As String, idColumnName As String, id As String)
        Dim ob As New T
        Dim Props = GetType(T).GetProperties()
        Dim dt As New DataTable

        Dim command As String = ""
        command = "SELECT * FROM " & TableName & " WHERE " & idColumnName & "='" & id & "';"
        Try
            com = New MySqlCommand(command, Connection)
            adpt = New MySqlDataAdapter(command, con)
            adpt.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                For xx = 0 To dt.Columns.Count - 1
                    For Each p In Props
                        If p.Name = dt.Columns(xx).ColumnName Then
                            Dim c = dt.Rows(i)(xx)
                            Try
                                If dt.Columns(xx).DataType = c.GetType Then
                                    p.SetValue(ob, dt.Rows(i)(xx), Nothing)
                                End If
                            Catch ex As Exception
                                ErrorHistoryAdd(ex)
                            End Try
                        End If
                    Next
                Next
            Next
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return Nothing

        Finally
            Connection.Close()
            con.Close()
            con.Dispose()
            con = Nothing
        End Try

        EventHistoryAdd()
        Return ob

    End Function
    ''' <summary>
    ''' Tworzy listę obiektów na podstawie dowolnej klasy wg komendy mysQL
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="SQLcommand"></param>
    ''' <returns></returns>
    Public Function GetObject(Of T As New)(SQLcommand As String) As List(Of T)
        Dim l As New List(Of T)

        Dim Props = GetType(T).GetProperties()
        Dim dt As New DataTable

        Dim command As String = ""
        command = SQLcommand
        Try
            com = New MySqlCommand(command, Connection)
            adpt = New MySqlDataAdapter(command, con)
            adpt.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                Dim ob As New T
                For xx = 0 To dt.Columns.Count - 1
                    For Each p In Props
                        If p.Name.ToLower = dt.Columns(xx).ColumnName.ToLower Then
                            Dim c = dt.Rows(i)(xx)
                            Try
                                If dt.Columns(xx).DataType = c.GetType Then
                                    p.SetValue(ob, dt.Rows(i)(xx), Nothing)
                                    Exit For
                                End If
                            Catch ex As Exception
                                Try
                                    Dim fs As New FSCore

                                    p.SetValue(ob, fs.DeserializeListFromDB(Of Object)(dt.Rows(i)(xx))(0), Nothing)
                                Catch exx As Exception
                                    Try
                                        Debug.Print(ex.Message.ToString)
                                        If ex.Message.ToString = "Obiektu typu 'System.Int32' nie można przekonwertować na typ 'System.String'." Then
                                            Dim it As String = dt.Rows(i)(xx)
                                            p.SetValue(ob, it, Nothing)
                                        End If

                                    Catch exxxx As Exception
                                        ErrorHistoryAdd(ex)
                                    End Try
                                End Try

                            End Try
                        End If
                    Next
                Next
                l.Add(ob)
            Next
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return Nothing

        Finally
            Connection.Close()
            con.Close()
            con.Dispose()
            con = Nothing
        End Try

        EventHistoryAdd()
        Return l

    End Function


    Public Function GetSingleObject(Of T As New)(SQLcommand As String) As T
        Dim l As New List(Of T)

        Dim Props = GetType(T).GetProperties()
        Dim dt As New DataTable

        Dim command As String = ""
        command = SQLcommand
        Try
            com = New MySqlCommand(command, Connection)
            adpt = New MySqlDataAdapter(command, con)
            adpt.Fill(dt)

            For i = 0 To dt.Rows.Count - 1
                Dim ob As New T
                For xx = 0 To dt.Columns.Count - 1
                    For Each p In Props
                        If p.Name.ToLower = dt.Columns(xx).ColumnName.ToLower Then
                            Dim c = dt.Rows(i)(xx)
                            Try
                                If dt.Columns(xx).DataType = c.GetType Then
                                    p.SetValue(ob, dt.Rows(i)(xx), Nothing)
                                    Exit For
                                End If
                            Catch ex As Exception
                                Try
                                    Dim fs As New FSCore

                                    p.SetValue(ob, fs.DeserializeListFromDB(Of Object)(dt.Rows(i)(xx))(0), Nothing)
                                Catch exx As Exception
                                    Try
                                        Debug.Print(ex.Message.ToString)
                                        If ex.Message.ToString = "Obiektu typu 'System.Int32' nie można przekonwertować na typ 'System.String'." Then
                                            Dim it As String = dt.Rows(i)(xx)
                                            p.SetValue(ob, it, Nothing)
                                        End If

                                    Catch exxxx As Exception
                                        ErrorHistoryAdd(ex)
                                    End Try
                                End Try

                            End Try
                        End If
                    Next
                Next
                l.Add(ob)
            Next
        Catch ex As Exception
            ErrorHistoryAdd(ex)
            Return Nothing

        Finally
            Connection.Close()
            con.Close()
            con.Dispose()
            con = Nothing
        End Try

        EventHistoryAdd()
        If IsNothing(l) Then Return Nothing
        If l.Count = 0 Then Return Nothing


        Return l(0)

    End Function

#End Region
#Region "DELETE"
    Public Function Delete(TableName As String, idColumnName As String, id As String) As Boolean

        Return Me.ExecuteNonQuery("DELETE from " & TableName & " WHERE " & idColumnName & "='" & id & "';")

    End Function

    Public Function Truncate(TableName As String) As Boolean
        Return Me.ExecuteNonQuery("TRUNCATE " & TableName & ";")
    End Function

    Public Function DropTable(TableName As String) As Boolean
        Return Me.ExecuteNonQuery("Drop TABLE " & TableName & ";")
    End Function



#End Region
#Region "Create"

    Public Function CreateTable(TF As TableDefinition) As Boolean
        Dim sb As New StringBuilder
        sb.Append("Create TABLE `" & TF.TableName & "` (")

        For Each s In TF.ColumnsList
            sb.Append("`" & s.ColumnName & "` ")
            If s.DataType = TableDefinition.DataTypes.VARCHAR Then
                sb.Append("" & s.DataType.ToString.Replace("_", "") & "(" & s.Lenght & ") ")
            Else
                sb.Append("" & s.DataType.ToString.Replace("_", "") & " ")
            End If

            If s.NotNull = True Then sb.Append("NOT NULL ")
            If s.AI = True Then sb.Append("AUTO_INCREMENT ")
            sb.Append(",")
        Next

        For Each s In TF.ColumnsList
            If s.PrimmaryKey = True Then
                sb.Append("PRIMARY KEY(`" & s.ColumnName & "`)")
            End If
        Next
        sb.Append(");")


        TF.CreateString = sb.ToString


        TF.Created = Me.ExecuteNonQuery(TF.CreateString)
        Return TF.Created
    End Function
    Public Function AddColumn(TableName As String, k As TableDefinition.ColumnsDefinition) As Boolean
        '        ALTER TABLE `sio_pp`.`test` 
        'ADD COLUMN `nowa` VARCHAR(45) NULL AFTER `intt`;
        Dim sb As New StringBuilder
        sb.Append("ALTER TABLE `" & TableName & "`")

        sb.Append("ADD COLUMN ")
        sb.Append("`" & k.ColumnName & "` ")
        If k.DataType = TableDefinition.DataTypes.VARCHAR Then
            sb.Append("" & k.DataType.ToString.Replace("_", "") & "(" & k.Lenght & ") ")
        Else
            sb.Append("" & k.DataType.ToString.Replace("_", "") & " ")
        End If

        If k.NotNull = True Then sb.Append("NOT NULL ")
        If k.AI = True Then sb.Append("AUTO_INCREMENT ")
        sb.Append("")

        Dim lk As List(Of Tuple(Of String, String, String, String, String, String))
        lk = GetColumnsList(TableName)
        Dim ostatnia As String = lk(lk.Count - 1).Item1

        sb.Append(" AFTER `" & ostatnia & "`;")

        Dim ss As String = sb.ToString
        Return Me.ExecuteNonQuery(ss)

    End Function
    Public Function DeleteColumn(TableName As String, ColumnName As String)
        '        ALTER TABLE `sio_pp`.`test` 
        'DROP COLUMN `nowa`;
        Dim sb As New StringBuilder
        sb.Append("ALTER TABLE `" & TableName & "`")
        sb.Append(" DROP COLUMN `" & ColumnName & "`;")

        Dim ss As String = sb.ToString
        Return Me.ExecuteNonQuery(ss)

    End Function
    Public Class TableDefinition
        Enum DataTypes
            BIT
            TINYINT
            SMALLINT
            MEDIUMINT
            INT
            BIGINT
            REAL
            Double_
            FLOAT
            Decimal_
            NUMERIC
            Date_
            TIME
            TIMESTAMP
            DATETIME
            YEAR
            Char_
            VARCHAR
            BINARY
            VARBINARY
            TINYBLOB
            BLOB
            MEDIUMBLOB
            LONGBLOB
            TINYTEXT
            TEXT
            MEDIUMTEXT
            LONGTEXT
            JSON
        End Enum

        ' CREATE TABLE `sio_pp`.`abc` (
        '`idabc` INT Not NULL AUTO_INCREMENT,
        '`abccol` VARCHAR(45) NULL,
        'PRIMARY KEY(`idabc`));


        Public Property TableName As String
        Public Property CreateString As String
        Public Property ColumnsList As List(Of ColumnsDefinition)
        Public Property Created As Boolean = False
        Sub New(TableName As String)
            Me.TableName = TableName
        End Sub
        Sub AddColumn(K As ColumnsDefinition)
            If IsNothing(Me.ColumnsList) Then Me.ColumnsList = New List(Of ColumnsDefinition)
            Me.ColumnsList.Add(K)

        End Sub

        Public Class ColumnsDefinition
            Public Property ColumnName As String
            Public Property DataType As DataTypes
            Public Property DefaultValue As String = "NULL"
            Public Property AI As Boolean = False
            Public Property NotNull As Boolean = False
            Public Property PrimmaryKey As Boolean = False
            Public Property Lenght As Integer = 255


        End Class
    End Class
#End Region

End Class










