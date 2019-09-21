
Imports System.Windows.Forms

<DebuggerDisplay("Rok={yer}")>
<Serializable> Public Class GrafikType
    Public Property Id As String
    Public Property Name As String
    Public Property Yer As Integer
    Public Property Months As List(Of MonthType)
    Public Property CustomFieldsDictionary As List(Of String)
    Public Property CustomColumns As List(Of CustomColumnType)
    Public Property Shifts As List(Of ShiftType)
    Public Property HourTypes As List(Of HoursType)


    Public Enum ConstColumnNames
        workerguid
        Pracownik
        Etat
        Grupa
        Norma
        Suma
        Różnica
    End Enum
    Public Sub New(yer As Integer)
        Me.Id = Guid.NewGuid.ToString
        Me.Yer = yer
        Me.CustomFieldsDictionary = New List(Of String)
        Me.Months = New List(Of MonthType)
        For i = 1 To 12
            Me.AddMonth(i)
        Next
        Me.CustomColumns = New List(Of CustomColumnType)

        Dim cc As CustomColumnType
        cc = New CustomColumnType With {
            .Name = ConstColumnNames.workerguid.ToString,
            .IsCustom = False,
            .IsVisible = False}
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
            .Name = ConstColumnNames.Pracownik.ToString,
            .IsCustom = False,
            .IsVisible = True}
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Etat.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Grupa.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Norma.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Suma.ToString,
    .IsCustom = False,
    .IsVisible = True
     }
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Różnica.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        Me.AddCustomColumn(cc)


    End Sub

    Private Sub AddMonth(number As Integer)
        Dim M As New MonthType(number, Me.Yer)
        Me.Months.Add(M)
    End Sub


    Public Sub DataSrc(dg As DataGridView, monthNumber As Integer)
        Dim dt As New DataTable
        Dim c As DataGridViewColumn
        dg.Columns.Clear()

        For Each cc In Me.CustomColumns
            Dim dtC As New DataColumn
            dtC.ColumnName = cc.Id
            dtC.Namespace = cc.Name
            dtC.ExtendedProperties.Add("IsCustom", cc.IsCustom)
            dtC.ExtendedProperties.Add("isDayColumn", cc.isDayColumn)

            For i = 1 To

            dt.Columns.Add(dtC)

            c = New DataGridViewTextBoxColumn
            c.Name = cc.Id
            c.HeaderText = cc.Name
            c.DataPropertyName = cc.Id

            dg.Columns.Add(c)

            c.Width = cc.Style.Width
            c.HeaderCell.Style.Font = cc.Style.Font
            c.HeaderCell.Style.ForeColor = cc.Style.ForeColor
            c.HeaderCell.Style.BackColor = cc.Style.BackColor
            c.Visible = True

        Next


        For Each g In Me.Months(monthNumber).Groups
            For Each w In g.Workers
                For Each d In w.Days
                    dt.Rows.Add()
                    Dim i As Integer = dt.Rows.Count - 1

                    For Each dtc As DataColumn In dt.Columns
                        If dtc.ExtendedProperties("isDayColumn") = False Then
                            dt.Rows(i)(dtc.ColumnName) = d.GetValue(DayType.EntryTypeValue.Planned)
                        Else
                            If dtc.ExtendedProperties("isCustom") = False Then
                                If dtc.Namespace = ConstColumnNames.workerguid.ToString Then dt.Rows(i)(dtc.ColumnName) = w.Id
                                If dtc.Namespace = ConstColumnNames.Etat.ToString Then dt.Rows(i)(dtc.ColumnName) = w.Etat
                                If dtc.Namespace = ConstColumnNames.Grupa.ToString Then dt.Rows(i)(dtc.ColumnName) = g.Name
                                If dtc.Namespace = ConstColumnNames.Norma.ToString Then dt.Rows(i)(dtc.ColumnName) = Me.Months(monthNumber).Norm
                                If dtc.Namespace = ConstColumnNames.Pracownik.ToString Then dt.Rows(i)(dtc.ColumnName) = w.Name
                                If dtc.Namespace = ConstColumnNames.Suma.ToString Then dt.Rows(i)(dtc.ColumnName) = w.Id
                                If dtc.Namespace = ConstColumnNames.Różnica.ToString Then dt.Rows(i)(dtc.ColumnName) = w.Id
                                If dtc.ExtendedProperties("isDayColumn") = True Then
                                    dt.Rows(i)(dtc.ColumnName) = d.GetValue(DayType.EntryTypeValue.Executed)
                                End If
                            Else
                                dt.Rows(i)(dtc.ColumnName) = w.GetCustomValue(dtc.Namespace)
                            End If
                        End If

                    Next


                Next
            Next
        Next



        Dim bs As New BindingSource
        bs.DataSource = dt
        dg.DataSource = bs



    End Sub

    Public Function SetValue(workerGuid As String, shiftGuid As String, dayNumber As Integer, monthNumber As Integer, entrytype As DayType.EntryTypeValue)
        Dim mM As MonthType = Nothing
        For Each m In Me.Months
            If m.Number = monthNumber Then mM = m
        Next
        Dim pP As WorkerType = Nothing
        For Each g In mM.Groups
            For Each w In g.Workers
                If w.Id = workerGuid Then pP = w
            Next
        Next
        Dim dD As DayType = Nothing
        For Each d In pP.Days
            If d.Data.Day = dayNumber Then
                dD = d
            End If
        Next
        Dim sS As ShiftType = Nothing
        For Each s In Me.Shifts
            If s.Id = shiftGuid Then sS = s
        Next
        If Not IsNothing(dD) And Not IsNothing(sS) Then
            Return dD.SetValue(sS, entrytype)
        Else
            Return "Błąd wpisu, nie rozpoznano pracownika lub zmiany pracy"
        End If

    End Function

    Function AddCustomWorkerField(name As String) As String

        If Me.CustomFieldsDictionary.Contains(name) Then
            Return "Pole już istnieje, nie można dodać dwóch pól o tej samej nazwie"
        End If

        For Each m In Me.Months
            For Each group In m.Groups
                For Each worker In group.Workers
                    worker.AddCustomField(name)
                Next
            Next
        Next
        Return "Dodano pole " & name
    End Function

    Function RemoveCustomWorkerField(name As String) As String
        For Each cf In Me.CustomFieldsDictionary
            If cf = name Then
                Me.CustomFieldsDictionary.Remove(cf)
                For Each m In Me.Months
                    For Each group In m.Groups
                        For Each worker In group.Workers
                            worker.RemoveCustomField(name)
                        Next
                    Next
                Next
                Return "Usunięto pole " & name
            End If
        Next
        Return "Nie znaleziono pola " & name & " do usunięcia"
    End Function

    Public Function AddCustomColumn(CustomCol As CustomColumnType) As String
        For Each cc In Me.CustomColumns
            If cc.Name = Name Then
                Return "Kolumna " & CustomCol.Name & " już istnieje, nie można dodać"
            End If
        Next

        For Each m In Me.Months
            For Each group In m.Groups
                For Each worker In group.Workers
                    worker.AddCustomColumnValue(New CustomColumnValueType(CustomCol.Name))
                Next
            Next
        Next

        Me.CustomColumns.Add(CustomCol)
        Return "Dodano kolumnę " & CustomCol.Name
    End Function

    Public Function RemoveCustomColumn(name As String) As String
        For Each c In Me.CustomColumns
            If c.Name = name Then
                If c.IsCustom = False Then
                    Return "Nie można usunąć tej kolumny"
                End If
                Me.CustomColumns.Remove(c)

                For Each m In Me.Months
                    For Each group In m.Groups
                        For Each w In group.Workers
                            w.RemoveCustomColumnValue(name)
                        Next
                    Next
                Next
                Return "Usunięto kolumnę " & name
            End If
        Next
        Return "Nie znaleziono kolumny " & name & " , nie można usunąć."
    End Function

    Public Function AddShift(name As String, starttime As TimeSpan, endtime As TimeSpan, iswork As Boolean, hourtype As HoursType) As String
        If String.IsNullOrEmpty(name) Then Return "Nazwa jest wymagana"
        If Len(name) > 5 Then Return "Nazwa jest za długa, maksymalnie 5 znaków"
        If IsNothing(Me.Shifts) Then Me.Shifts = New List(Of ShiftType)
        For Each ss In Me.Shifts
            If ss.Name = name Then Return "Nazwa jest już zajęta"
        Next

        Dim s As New ShiftType With {
            .Name = name,
            .StartTime = starttime,
            .EndTime = endtime,
            .IsWork = iswork,
            .HourType = hourtype
        }

        Me.Shifts.Add(s)
        Return "Dodano zmianę"
    End Function
    Public Function RemoveShift(name As String) As Boolean
        For Each s In Me.Shifts
            If s.Name = name Then
                Me.Shifts.Remove(s)
                Return "Usunięto zmianę"
            End If
        Next
        Return "Nie znalezniono zmiany " & name & ", nie można usunąć"
    End Function

    Public Function AddHourType(name As String) As String
        If String.IsNullOrEmpty(name) Then Return "Nazwa jest wymagana"


        For Each ss In Me.HourTypes
            If ss.Name = name Then Return "Nazwa jest już zajęta"
        Next

        Dim s As New HoursType With {
            .Name = name
        }


        Me.HourTypes.Add(s)
        Return "Dodano rodzaj godzin"
    End Function
    Public Function RemoveHourType(name As String) As Boolean
        For Each s In Me.HourTypes
            If s.Name = name Then
                Me.HourTypes.Remove(s)
                Return "Usunięto rodzaj godzin"
            End If
        Next
        Return "Nie znalezniono rodzaju godzin " & name & ", nie można usunąć"
    End Function

End Class
