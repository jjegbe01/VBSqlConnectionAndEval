Private Function FindCurrentAppCode(ByVal primaryKey As String) As String
        Dim sSQL As String = String.Empty 'Holds SQL query
        Dim TempReader As DbDataReader 'Database reader
        Dim cmd As IDbCommand = BUDataBase.CreateNewDatabaseConnection().CreateCommand 'Executes SQL statements on datasource
        Dim tempStr As String = Nothing 'Used locally to evaluate data
        Dim retVal As String = Nothing 'Holds the string that is returned from the method
        
        'Initialize cmd
        With cmd
            .CommandType = CommandType.Text
            .CommandTimeout = 0
        End With
        
        'Generate SQL query
        sSQL = "SELECT "
        sSQL = sSQL & "       column_1, column_2, column_3, " & vbCrLf
        sSQL = sSQL & "       column_4, column_5, column_6, " & vbCrLf
        sSQL = sSQL & "       column_7, column_8, column_9 " & vbCrLf
        sSQL = sSQL & "FROM " & Settings.RefDataLocal & ".tablename " & vbCrLf
        sSQL = sSQL & "WHERE column_1 = '" & primaryKey & "' " & vbCrLf
        
        'Set cmd's command text equal to our SQL query
        cmd.CommandText = sSQL
        'Execute our query
        TempReader = cmd.ExecuteReader
        
        '**Use TempReader to evaluate returned data**
        '***All rows past here are optional***
        
        'Make sure that data exists
        If TempReader.HasRows AndAlso TempReader.Read() Then
            'Use a Do While to iterate through all rows returned from our query
            Do While TempReader.Read()
                'Set tempStr equal to a paticular value in each row
                tempStr = TempReader("column_1")
                'Evaluate tempStr and perform needed actions
                If tempStr = "value1" Or tempStr = "value2" Then
                    retVal = tempStr
                    Exit Do    
                End If
            Loop
        Else
            retVal = ""
        End If
        'Return string
        Return retVal
End Function