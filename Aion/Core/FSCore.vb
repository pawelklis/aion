Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization.Json
Imports System.Text

Public Class FSCore
    Public Property LastError As String


    'Public Function SerializeListToJson(Of t)(ByVal list As List(Of t)) As String
    '    Dim ms As New MemoryStream
    '    Dim ser As DataContractJsonSerializer = New DataContractJsonSerializer(list.GetType)
    '    ser.WriteObject(ms, list)
    '    ms.Position = 0
    '    Dim dr As New StreamReader(ms)
    '    Return dr.ReadToEnd
    'End Function
    'Public Function DeserializeListFromJson(Of T)(ByVal data As String)
    '    Dim ms As New MemoryStream(Encoding.UTF8.GetBytes(data))
    '    ms.Position = 0
    '    Dim ser As DataContractJsonSerializer = New DataContractJsonSerializer(GetType(T))
    '    Dim list
    '    list = DirectCast(ser.ReadObject(ms), List(Of T))
    '    'list = ser.ReadObject(ms)
    '    Return list

    'End Function

    Public Function SerializeListForDB(Of T)(ByVal list As List(Of T)) As Byte()
        Dim ms As MemoryStream = New MemoryStream()
        Dim bf As BinaryFormatter = New BinaryFormatter()
        bf.Serialize(ms, list)
        ms.Position = 0
        Dim serializedList As Byte() = New Byte(ms.Length - 1) {}
        ms.Read(serializedList, 0, CInt(ms.Length))
        ms.Close()
        Return serializedList


    End Function

    Public Function DeserializeListFromDB(Of T)(ByVal data As Byte()) As List(Of T)
        Try
            Dim ms As MemoryStream = New MemoryStream()
            If Not IsNothing(data) Then
                ms.Write(data, 0, data.Length)
                ms.Position = 0

            End If
            Dim bf As BinaryFormatter = New BinaryFormatter()
            Dim list As List(Of T)

            list = TryCast(bf.Deserialize(ms), List(Of T))

            Return list
        Catch ex As SerializationException
            's_adlog.Add("", 9, ex.ToString, 0)
            Debug.WriteLine(ex.ToString())
            Return Nothing
        End Try
    End Function





    Public Function Serialize(ob As Object, path As String, file As String) As Boolean
        Try

            If Directory.Exists(path) = False Then
                Directory.CreateDirectory(path)
            End If

            Dim formatter As IFormatter = New BinaryFormatter
            Dim stream As IO.Stream = New FileStream(path & "\" & ob.file, FileMode.Create, FileAccess.Write, FileShare.None)
            formatter.Serialize(stream, ob)
            stream.Close()
            Return True
        Catch ex As Exception
            LastError = ex.ToString
            Return False

        End Try

    End Function
    Public Function Serialize(ob As Object, pathFileName As String) As Boolean
        Try

            If Directory.Exists(Path.GetDirectoryName(pathFileName)) = False Then
                Directory.CreateDirectory(Path.GetDirectoryName(pathFileName))
            End If

            Dim formatter As IFormatter = New BinaryFormatter
            Dim stream As IO.Stream = New FileStream(pathFileName, FileMode.Create, FileAccess.Write, FileShare.None)
            formatter.Serialize(stream, ob)
            stream.Close()
            Return True
        Catch ex As Exception
            LastError = ex.ToString
            Return False

        End Try

    End Function
    Public Function deSerialize(Of T)(path As String, FileName As String)
        deSerialize = Nothing
        path = path.Replace("\\", "\")

        If path.Substring(Len(path) - 1, 1) <> "\" Then path = path & "\"
        Try

            Dim g As T

            If File.Exists(path & FileName) = True Then


                If IsNothing(path) Then Exit Function
                If IsNothing(FileName) Then Exit Function
                Dim formatter As IFormatter = New BinaryFormatter
                Dim stream As IO.Stream = New FileStream(path & "\" & FileName, FileMode.Open, FileAccess.Read, FileShare.Read)

                g = DirectCast(formatter.Deserialize(stream), T)

                stream.Close()
            Else
                Return Nothing
            End If
            Return g

        Catch ex As Exception
            LastError = ex.ToString
            Return Nothing
        End Try

    End Function
    Public Function deSerialize(Of T)(pathFileName As String)
        deSerialize = Nothing

        Try

            Dim g As T

            If File.Exists(pathFileName) = True Then



                Dim formatter As IFormatter = New BinaryFormatter
                Dim stream As IO.Stream = New FileStream(pathFileName, FileMode.Open, FileAccess.Read, FileShare.Read)

                g = DirectCast(formatter.Deserialize(stream), T)

                stream.Close()
            Else
                Return Nothing
            End If
            Return g

        Catch ex As Exception
            LastError = ex.ToString
            Return Nothing
        End Try

    End Function

    Public Function ListaPlikow(path As String) As List(Of String)
        Dim l As New List(Of String)

        Dim folder As New DirectoryInfo(path)
        For Each fil In folder.GetFiles
            Try
                l.Add(System.IO.Path.GetFileName(fil.Name))
            Catch ex As Exception
                LastError = ex.ToString
            End Try

        Next

        Return l
    End Function
    Public Function ListaFolderow(path As String) As List(Of String)
        Dim l As New List(Of String)

        Dim folder As New DirectoryInfo(path)
        For Each fil In folder.GetDirectories
            Try
                l.Add(System.IO.Path.GetDirectoryName(fil.Name))
            Catch ex As Exception
                LastError = ex.ToString
            End Try

        Next

        Return l
    End Function
End Class
