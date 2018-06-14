Public Class frmEditRcvDoc
    Private Sub frmEditRcvDoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ds = (From c In frmReceive.dt Where c.Id = frmReceive.RcvHdId).FirstOrDefault
        If Not IsNothing(ds) Then
            tbDocNo.Text = ds.DocumentNo
            dtDocdate.Value = ds.DocumentDate
            dtRcvDate.Value = ds.ReceiveDate
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbDocNo.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน เลขที่เอกสาร", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbDocNo.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    Dim ds = (From c In db.RcvHeader Where c.Id = frmReceive.RcvHdId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.DocumentNo = tbDocNo.Text
                        ds.DocumentDate = dtDocdate.Value
                        ds.ReceiveDate = dtRcvDate.Value
                        ds.UpdateBy = Username
                        ds.UpdateDate = DateTimeServer()
                    End If
                    db.SaveChanges()
                End Using
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmReceive.SelData()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, dtRcvDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

End Class