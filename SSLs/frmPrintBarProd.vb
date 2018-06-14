Public Class frmPrintBarProd
    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If MessageBox.Show("ต้องการ Print Label ใช่หรือไม่?", "ยืนยันการ Print Label", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In db.LabelLog Where c.PrintFlag = False).ToList
                    If ds.Count > 0 Then
                        For Each a In ds
                            a.PrintFlag = True
                        Next
                    End If
                    For i = 1 To CDbl(tbQty.Text)
                        db.LabelLog.Add(New LabelLog With {.FKCompany = CompID, .FKOwner = frmProductEdit.OwnID, .FKProductDetail = frmProductEdit.DtlID, .CreateDate = DateTimeServer(), .CreateBy = Username, .Enable = True})
                    Next
                    db.SaveChanges()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            frmrptPrintLabelProd.Show()
            Me.Close()
        End If
    End Sub

    Private Sub tbNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbQty.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub tbQty_KeyDown(sender As Object, e As KeyEventArgs) Handles tbQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            btSave.Focus()
        End If
    End Sub

    Private Sub tbQty_TextChanged(sender As Object, e As EventArgs) Handles tbQty.TextChanged
        If IsNumeric(tbQty.Text) Then
            tbQtyP.Text = Format(CDbl(tbQty.Text) * 2, "#,##0")
        End If
    End Sub
End Class