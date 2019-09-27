
Imports System.Drawing
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
    Public Property CurrentMonthNumber As Integer = 1
    Public Property CurrentEntryTypeView As DayType.EntryTypeValue = DayType.EntryTypeValue.Planned


    Public Property EmptyValue As String = "x"

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
            .IsVisible = False
           }
        cc.Style.Width = 100
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
            .Name = ConstColumnNames.Pracownik.ToString,
            .IsCustom = False,
            .IsVisible = True}
        cc.Style.Width = 100
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Etat.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        cc.Style.Width = 50
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Grupa.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        cc.Style.Width = 50
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Norma.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        cc.Style.Width = 50
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Suma.ToString,
    .IsCustom = False,
    .IsVisible = True
     }
        cc.Style.Width = 50
        Me.AddCustomColumn(cc)
        cc = New CustomColumnType With {
    .Name = ConstColumnNames.Różnica.ToString,
    .IsCustom = False,
    .IsVisible = True
        }
        cc.Style.Width = 50
        Me.AddCustomColumn(cc)


    End Sub

    Private Sub AddMonth(number As Integer)
        Dim M As New MonthType(number, Me.Yer)
        Me.Months.Add(M)
    End Sub

    Public DG As DataGridView
    Public Sub DataSrc(dgg As DataGridView, monthNumber As Integer)
        dgg.ClearSelection()
        dgg.Columns.Clear()
        dgg.AllowUserToAddRows = False
        DG = dgg
        Dim CC As DataGridViewTextBoxColumn

        For Each c In Me.CustomColumns
            CC = New DataGridViewTextBoxColumn
            CC.Name = c.Name
            CC.HeaderText = c.Name
            CC.Width = c.Style.Width
            CC.Visible = c.IsVisible

            DG.Columns.Add(CC)
        Next

        For Each d In Me.Months(monthNumber).DaysList
            CC = New DataGridViewTextBoxColumn
            CC.Name = d.Data.Day
            CC.HeaderText = d.Data.Day & vbCrLf & d.Data.ToString("ddd")
            CC.Width = 60

            If d.IsHoliday Then CC.DefaultCellStyle.BackColor = Color.Orange
            If d.IsSaturday Then CC.DefaultCellStyle.BackColor = Color.Silver
            If d.IsSunday Then CC.DefaultCellStyle.BackColor = Color.DimGray

            DG.Columns.Add(CC)
        Next

        Dim row As DataGridViewRow
        For Each g In Me.Months(monthNumber).Groups
            For Each w In g.Workers
                DG.Rows.Add()
                row = DG.Rows(DG.Rows.Count - 1)
                row.Height = 60

                row.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                row.DefaultCellStyle.WrapMode = DataGridViewTriState.True

                row.Cells(ConstColumnNames.Pracownik.ToString).Value = w.Name
                row.Cells(ConstColumnNames.Etat.ToString).Value = w.Etat
                row.Cells(ConstColumnNames.Grupa.ToString).Value = g.Name
                row.Cells(ConstColumnNames.Norma.ToString).Value = ""
                row.Cells(ConstColumnNames.Różnica.ToString).Value = ""

                Dim WW As TimeSpan = w.Suma(CurrentEntryTypeView)
                row.Cells(ConstColumnNames.Suma.ToString).Value = Math.Round(WW.TotalHours, 0) & ":" & WW.Minutes.ToString("00")

                row.Cells(ConstColumnNames.workerguid.ToString).Value = w.Id

                For Each d In w.Days
                    row.Cells(d.Data.Day.ToString).Value = d.GetValue(CurrentEntryTypeView)
                    If String.IsNullOrEmpty(row.Cells(d.Data.Day.ToString).Value) Then row.Cells(d.Data.Day.ToString).Value = Me.EmptyValue
                Next

                'For Each c As DataGridViewCell In row.Cells
                '    c.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                '    c.Style.WrapMode = DataGridViewTriState.True
                'Next
            Next
        Next

        AddHandler DG.CellEndEdit, AddressOf dg_CellEndEdit
        AddHandler DG.KeyUp, AddressOf dg_KeyUp

    End Sub
#Region "GridEvents"
    Private Sub UpdateCountingFields(row As Integer)
        Dim idWorker As String = DG.Rows(row).Cells(ConstColumnNames.workerguid.ToString).Value
        Dim sumaCel As DataGridViewCell = DG.Rows(row).Cells(ConstColumnNames.Suma.ToString)

        For Each m In Me.Months
            If m.Number = Me.CurrentMonthNumber Then
                For Each gr In m.Groups
                    For Each w In gr.Workers
                        If w.Id = idWorker Then
                            Dim WW As TimeSpan = w.Suma(CurrentEntryTypeView)
                            sumaCel.Value = Math.Round(WW.TotalHours, 0) & ":" & WW.Minutes.ToString("00")
                        End If
                    Next
                Next
            End If
        Next

    End Sub
    Private Sub dg_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)

        Dim okFlag As Boolean = False

        If IsNumeric(DG.Columns(e.ColumnIndex).Name) Then
            If e.RowIndex > -1 Then
                If e.ColumnIndex > -1 Then
                    Dim v As String = DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString
                    'For Each s In Me.Shifts
                    '    If s.Name = v Then
                    DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Me.SetValue(DG.Rows(e.RowIndex).Cells(ConstColumnNames.workerguid.ToString).Value, v, DG.Columns(e.ColumnIndex).Name, Me.CurrentMonthNumber, CurrentEntryTypeView, okFlag)
                    UpdateCountingFields(e.RowIndex)
                            If String.IsNullOrEmpty(DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Me.EmptyValue

                    'okFlag = True
                    '    End If
                    'Next
                End If
            End If
            If okFlag = False Then
                DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Me.GetDayValue(Me.CurrentMonthNumber, DG.Rows(e.RowIndex).Cells(ConstColumnNames.workerguid.ToString).Value, DG.Columns(e.ColumnIndex).Name, CurrentEntryTypeView)
                If String.IsNullOrEmpty(DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then DG.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = Me.EmptyValue

            End If
        End If


    End Sub

    Private Sub dg_KeyUp(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            For Each cel As DataGridViewCell In DG.SelectedCells
                If IsNumeric(DG.Columns(cel.ColumnIndex).Name) Then
                    Dim idWorker As String = DG.Rows(cel.RowIndex).Cells(ConstColumnNames.workerguid.ToString).Value
                    cel.Value = Me.ClearDayValues(Me.CurrentMonthNumber, idWorker, DG.Columns(cel.ColumnIndex).Name, CurrentEntryTypeView)
                    If String.IsNullOrEmpty(cel.Value) Then cel.Value = Me.EmptyValue

                    UpdateCountingFields(cel.RowIndex)
                End If
            Next
        End If

    End Sub


#End Region
    Public Function SetValue(workerGuid As String, shiftName As String, dayNumber As Integer, monthNumber As Integer, entrytype As DayType.EntryTypeValue, okFlag As Boolean)
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
            If s.Name = shiftName Then sS = s
        Next
        If Not IsNothing(dD) And Not IsNothing(sS) Then
            okFlag = True
            Return dD.SetValue(sS, entrytype)

        Else
            If Not IsNothing(dD) Then
                Dim vString As String() = shiftName.Split("-"c, " "c, " - ", " -", "- ")
                Dim vS As New List(Of String)
                For Each vss In vString
                    If Not String.IsNullOrEmpty(vss) Then
                        If Not String.IsNullOrWhiteSpace(vss) Then
                            vS.Add(vss)
                        End If
                    End If
                Next

                If vS.Count = 2 Then
                    Try
                        Dim st As TimeSpan = TimeSpan.Parse(vS(0) & ":00")
                        Dim et As TimeSpan = TimeSpan.Parse(vS(1) & ":00")
cek:
                        'czy jest shift z takimi godzinami
                        For Each s In Me.Shifts
                            If s.StartTime = st Then
                                If s.EndTime = et Then
                                    'jest
                                    okFlag = True
                                    Return dD.SetValue(s, entrytype)
                                End If
                            End If
                        Next

                        Me.AddShift(st.Hours & "-" & et.Hours, st, et, True, Nothing)
                        GoTo cek



                    Catch ex As Exception

                    End Try

                End If



            End If

            okFlag = False
            Return "Błąd wpisu, nie rozpoznano pracownika lub zmiany pracy"
        End If

    End Function
    Public Function ClearDayValues(monthNumber As Integer, workerGuid As String, dayNumber As Integer, entryType As DayType.EntryTypeValue)
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

        If entryType = DayType.EntryTypeValue.Executed Then
            dD.Executed.Entries.Clear()
        End If
        If entryType = CurrentEntryTypeView Then
            dD.Planned.Entries.Clear()
        End If
        Return ""
    End Function
    Public Function GetDayValue(monthNumber As Integer, workerGuid As String, dayNumber As Integer, entryType As DayType.EntryTypeValue)
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

        Return dD.GetValue(entryType)

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
