Public Class MasterForm

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Private Sub ToolStripButton9_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton9.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton10_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton10.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Label1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles frmLabel.MouseDown
        drag = True 'Sets the variable drag to true.
        mousex = Windows.Forms.Cursor.Position.X - Me.Left 'Sets variable mousex
        mousey = Windows.Forms.Cursor.Position.Y - Me.Top 'Sets variable mousey
    End Sub

    Private Sub Label1_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles frmLabel.MouseMove
        If drag Then
            Me.Top = Windows.Forms.Cursor.Position.Y - mousey
            Me.Left = Windows.Forms.Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Label1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles frmLabel.MouseUp
        drag = False 'Sets drag to false, so the form does not move according to the code in MouseMove

    End Sub

    Private Sub ToolStripButton11_Click(sender As System.Object, e As System.EventArgs) Handles ToolStripButton11.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        Else
            Me.WindowState = FormWindowState.Maximized
        End If

    End Sub
End Class
