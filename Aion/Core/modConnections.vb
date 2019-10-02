

Public Module modConnections

    Public Function mConnection() As MySqlCore2
        Dim m As New MySqlCore2(My.Settings.serwer, My.Settings.dbname, My.Settings.pwd)
        Return m
    End Function



    Public Enum EnumTables
        company
        companydetail
        site
        sitedetail
        user
        vacation
        worker
        contract
        workerdetail
        group
        holiday
        grafik
        grafikhw
        grafikentry
        customcolumn
        customvalue
        shift
    End Enum

    Public Enum EnumIDTables
        idcompany
        idcompanydetail
        idsite
        idsitedetail
        iduser
        idvacation
        idworker
        idcontract
        idworkerdetail
        idgroup
        idholiday
        idgrafik
        idgrafikhw
        idgrafikentry
        idcustomcolumn
        idcustomvalue
        idshift
    End Enum


End Module
