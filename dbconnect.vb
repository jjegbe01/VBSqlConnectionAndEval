Private Function FindCurrentAppCode(ByVal primaryKey As String) As String
        Dim sSQL As String = String.Empty 'Holds SQL query
        Dim TempReader As DbDataReader 'Database reader
        Dim tempStr As String = Nothing 'Used locally to evaluate data
        Dim retVal As String = Nothing 'Holds the string that is returned from the method
        Dim tempParam As SqlClient.SqlParameter
		
		'Instantiate cmd
		Using cmd As IdbCommand = BUDataBase.CreateNewDatabaseConnection.CreateCommand
			'Instantiate DB connection
			Using connection as SqlClient.SqlConnection = BUDataBase.CreateNewDatabaseConnection
				'Fill out tempParam details; [Name, Datatype, and size]
				tempParam = New SqlClient.SqlParameter("@ParameterOne", SqlDbType.VarChar, 10)
				'Set tempParam value equal to the value being passed into the method
				tempParam.Value = primaryKey
				'Set properties for cmd, *don't forget to add tempParam as a usable parameter*
				With cmd
					.CommandType = CommandType.Text
					.CommandTimeout = 0
					.Connection = connection
					.Parameters.Add(tempParam)
				End With
				
				'Generate SQL query
				sSQL = "SELECT "
				sSQL = sSQL & "       column_1, column_2, column_3, " & vbCrLf
				sSQL = sSQL & "       column_4, column_5, column_6, " & vbCrLf
				sSQL = sSQL & "       column_7, column_8, column_9 " & vbCrLf
				sSQL = sSQL & "FROM " & Settings.RefDataLocal & ".tablename " & " with(nolock)" & vbCrLf
				sSQL = sSQL & "WHERE column_1 = @ParameterOne" & vbCrLf
				'This last line is optional
				sSQL = sSQL & "AND column_3 in ('X', 'Y')"
				
				cmd.CommandText = sSQL
				TempReader = cmd.ExecuteReader
				If TempReader.Read() AndAlso TempReader.HasRows Then
					tempStr = TempReader("column_3")
					If (tempStr = "X" OrElse tempStr = "Y") Then
						retVal = tempStr
					End If
				End If
				connection.Close()
				TempReader.Close()
				Return retVal
			End Using
		End Using
	End Function
