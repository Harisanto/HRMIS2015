Imports dbmysqllib5.client

Public Class frmMasterData


    Private Sub frmMasterData_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        assemblyTrees()
    End Sub

    Private Sub assemblyTrees()
        'cari data tingkat nol
        Dim TSQL As String = "SELECT DISTINCT tbl_01_key_value.KeyName FROM tbl_01_key_value WHERE tbl_01_key_value.GroupID = 0  ORDER BY tbl_01_key_value.ID ASC"
        Dim db As New dbmysqllib5.client
        Dim xTable1 As New DataTable
        xTable1 = db.hasQuery2Datatable(db.hasOpenDatabase(Host, Database, User, Password), TSQL)
        'assembly trees
        TreeView1.Nodes.Clear()
        'Creating the root node
        Dim root = New TreeNode("Master Key of Data")
        TreeView1.Nodes.Add(root)

        For i As Integer = 0 To xTable1.Rows.Count - 1
            TreeView1.Nodes(0).Nodes.Add(New TreeNode(xTable1.Rows(i)(0)))
            '1st child for abal-abal :))
            'TreeView1.Nodes(0).Nodes(i).Nodes.Add(New TreeNode("Querying"))
        Next i
    End Sub

    Private Overloads Sub assemblyTreesChild(tProc As TreeViewEventArgs)
        Dim db As New dbmysqllib5.client
        'lihat apakah sudah breakdown
        With tProc.Node
            If .Level = 1 Then
                If TreeView1.Nodes(0).Nodes(.Index).Nodes.Count > 0 Then Exit Sub
            ElseIf .Level = 2 Then
                If TreeView1.Nodes(0).Nodes(.Parent.Index).Nodes(.Index).Nodes.Count > 0 Then Exit Sub
            Else
                Exit Sub
            End If

            'cari data tingkat nol
            Dim TSQL As String = ""
            If .Level >= 2 Then
                TSQL = "SELECT DISTINCT tbl_01_key_value.ID FROM tbl_01_key_value WHERE tbl_01_key_value.KeyValue = '" & tProc.Node.Text & "' "
                TSQL = "SELECT DISTINCT tbl_01_key_value.ID,tbl_01_key_value.GroupID,tbl_01_key_value.KeyName,tbl_01_key_value.KeyValue FROM tbl_01_key_value WHERE tbl_01_key_value.GroupID = '" & db.hasQuery2Count(db.hasOpenDatabase(Host, Database, User, Password), TSQL) & "'  ORDER BY tbl_01_key_value.ID ASC"
                'hasQuery2Count
            Else
                TSQL = "SELECT DISTINCT tbl_01_key_value.ID,tbl_01_key_value.GroupID,tbl_01_key_value.KeyName,tbl_01_key_value.KeyValue FROM tbl_01_key_value WHERE tbl_01_key_value.KeyName = '" & tProc.Node.Text & "'  ORDER BY tbl_01_key_value.ID ASC"
            End If

            Dim xTable1 As New DataTable
            xTable1 = db.hasQuery2Datatable(db.hasOpenDatabase(Host, Database, User, Password), TSQL)
            'assembly trees

            For i As Integer = 0 To xTable1.Rows.Count - 1
                If .Level >= 2 Then
                    TreeView1.Nodes(0).Nodes(0).Nodes(.Index).Nodes.Add(New TreeNode(xTable1.Rows(i)(3)))
                Else
                    TreeView1.Nodes(0).Nodes(.Index).Nodes.Add(New TreeNode(xTable1.Rows(i)(3)))
                End If
            Next i
        End With
    End Sub


    Private Sub TreeView1_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        'assemblyTreesChild(e.Node.Text, e.Node.Index, e.Node.Level)
        assemblyTreesChild(e)
    End Sub

End Class
