Imports MySql.Data.MySqlClient

Public Class client

    Public Overloads Function hasOpenDatabase(Host As String, Database As String, User As String, Password As String) As MySqlConnection
        Dim tsql As String = "Server=" & Host & ";Database=" & Database & ";Uid=" & User & ";Pwd=" & Password & ";"
        Dim cnactiveconnection As New MySqlConnection(tsql)

        Try
            cnactiveconnection.Open()
            If cnactiveconnection.State = ConnectionState.Open Then Return cnactiveconnection Else Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Overloads Function hasOpenDatabase(Connection As MySqlConnection, Host As String, Database As String, User As String, Password As String) As MySqlConnection
        Dim tsql As String = "Server=" & Host & ";Database=" & Database & ";Uid=" & User & ";Pwd=" & Password & ";"
        Dim cnactiveconnection As New MySqlConnection(tsql)

        Try
            If hasCloseDatabase(Connection) = True Then
                cnactiveconnection.Open()
                If cnactiveconnection.State = ConnectionState.Open Then Return cnactiveconnection Else Return Nothing
            Else : Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function hasPingDatabase(Host As String, Database As String, User As String, Password As String) As Boolean
        Dim Conn As MySqlConnection = hasOpenDatabase(Host, Database, User, Password)
        Try
            If Conn.State = ConnectionState.Open Then
                hasCloseDatabase(Conn)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function hasCloseDatabase(Connection As MySqlConnection) As Boolean
        Try
            Connection.Close()
        Catch ex As Exception
        End Try
        Return True
    End Function

    Public Function hasQuery2Dataset(Connection As MySqlConnection, tsql As String) As DataSet

        Dim da As MySqlDataAdapter = New MySqlDataAdapter()
        Dim ds As DataSet = New DataSet()

        da.SelectCommand = New MySqlCommand(tsql)
        da.SelectCommand.Connection = Connection
        da.Fill(ds)
        hasCloseDatabase(Connection)
        Return ds

    End Function

    Public Function hasQuery2Datatable(Connection As MySqlConnection, tsql As String) As DataTable

        Dim da As MySqlDataAdapter = New MySqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Dim dt As New DataTable

        da.SelectCommand = New MySqlCommand(tsql)
        da.SelectCommand.Connection = Connection
        da.Fill(ds)
        hasCloseDatabase(Connection)
        dt = ds.Tables(0)
        Return dt

    End Function

    Public Function hasQuery2Arraylist(Connection As MySqlConnection, tsql As String) As ArrayList

        Dim cmd As New MySqlCommand(tsql, Connection)
        Dim rs As MySqlDataReader = cmd.ExecuteReader
        Dim x As New ArrayList

        Try
            If rs.HasRows Then
                Do While rs.Read
                    If rs.IsDBNull(0) = False Then x.Add(rs(0)) Else x.Add("")
                Loop
            End If
            If rs.IsClosed = False Then rs.Close()
        Catch ex As Exception
        End Try
        hasCloseDatabase(Connection)
        Return x

    End Function

    Public Function hasQuery2Count(Connection As MySqlConnection, tsql As String) As Integer

        Dim cmd As New MySqlCommand(tsql, Connection)
        Dim rs As MySqlDataReader = cmd.ExecuteReader
        Dim x As Integer = 0

        Try
            If rs.HasRows Then
                If rs.Read Then If rs.IsDBNull(0) = False Then x = rs(0)
            End If
            If rs.IsClosed = False Then rs.Close()
        Catch ex As Exception
        End Try
        hasCloseDatabase(Connection)
        Return x

    End Function

End Class
