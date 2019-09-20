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

        Dim ww As New WorkerType(g.CustomFieldsDictionary, 1.0, g.Months(0))

        Print(g.Months(0).Groups(0).AddWorker(ww))

        Print(g.Months(0).RemoveHoliday("2019-01-02"))
        Print(g.Months(0).AddHoliday("2019-01-06", "3 króli"))

    End Sub
    Sub Print(v As String)

        Console.WriteLine(v)
    End Sub
End Module
