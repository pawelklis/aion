Module Module1

    Sub Main()

        Dim g As New GrafikType(2019)

        Print(g.Months(0).AddHoliday("2019-02-02", "test"))
        Print(g.Months(0).AddHoliday("2019-01-02", "3 króli"))
        Print(g.Months(0).AddHoliday("2019-01-02", "3 króli 2"))



        Print(g.Months(0).AddGroup(""))
        Print(g.Months(0).AddGroup("G1"))
        Print(g.Months(0).RemoveGroup(""))
        Print(g.Months(0).RemoveGroup("test"))
        Print(g.Months(0).RemoveGroup("g1"))

        Dim ww As WorkerType
        For i = 1 To 10
            ww = New WorkerType(g.CustomFieldsDictionary, 1.0, g.Months(0))
            ww.Name = "Pracownik " & i
            Print(g.Months(0).Groups(0).AddWorker(ww))
        Next



        Dim ss As New ShiftType With {
            .StartTime = TimeSpan.Parse("07:00"),
            .EndTime = TimeSpan.Parse("15:00"),
            .IsWork = True,
            .Name = "8D"}
        g.AddShift(ss.Name, ss.StartTime, ss.EndTime, ss.IsWork, ss.HourType)

        'Print(g.SetValue(ww.Id, g.Shifts(0).Id, 1, 1, DayType.EntryTypeValue.Planned))

        Print(g.Months(0).RemoveHoliday("2019-01-02"))
        Print(g.Months(0).AddHoliday("2019-01-06", "3 króli"))


        frmTest.Bind(g)

        Console.ReadLine()
    End Sub
    Sub Print(v As String)

        Console.WriteLine(v)
    End Sub
End Module
