Module appModule

    Friend Host = "localhost"
    Friend Database = "hrmis"
    Friend User = "root"
    Friend Password = "123"

    Friend Sub ShowForm(ChildForm As System.Windows.Forms.Form)
        ChildForm.MdiParent = frmParrent
        ChildForm.Show()
    End Sub
End Module
