Public Class frmPrintBarLoc
    Private Sub tbQty_KeyDown(sender As Object, e As KeyEventArgs) Handles tbQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            btSave.Focus()
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        LocName = tbQty.Text
        frmPrintBarLocation.Show()
    End Sub
End Class