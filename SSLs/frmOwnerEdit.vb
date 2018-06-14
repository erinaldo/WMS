Imports System.Data.Entity

Public Class frmOwnerEdit

    Dim oldCode As String

    Private Sub frmOwnerEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        oldCode = ""
        If sEdit = True Then
            Dim Sel = (From c In stOwner.Instance.Own Where c.Id = frmOwner.SId).FirstOrDefault
            If Not IsNothing(Sel) Then
                tbCode.Text = Sel.Code
                oldCode = Sel.Code
                tbName.Text = Sel.Name
                tbAddress.Text = Sel.Address
                tbTel.Text = Sel.Tel
                tbFax.Text = Sel.Fax
                tbEmail.Text = Sel.Email
                tbTax.Text = Sel.TaxNo
                tbDesc.Text = Sel.Description
                btDelete.Enabled = True
            End If
        Else
            ClearData()
        End If
    End Sub

    Sub ClearData()
        ClearTextBox(Me)
        btDelete.Enabled = False
        tbCode.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbCode.KeyDown, tbName.KeyDown, tbDesc.KeyDown, tbAddress.KeyDown, tbTel.KeyDown, tbFax.KeyDown, tbEmail.KeyDown, tbTax.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In stOwner.Instance.Own Where c.Id = frmOwner.SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        stOwner.Instance.Own.Remove(ds)
                        db.Entry(ds).State = EntityState.Modified
                        db.SaveChanges()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            MessageBox.Show("ลบข้อมูลเรียบร้อย", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            frmOwner.SelData()
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCode.Focus()
            Exit Sub
        End If
        If tbName.Text = "" Then
            MessageBox.Show("กรุณาป้อน ชื่อเจ้าของ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            Exit Sub
        End If
        If sNew = True Then
            If Not IsNothing((From c In stOwner.Instance.Own Where c.Code = tbCode.Text).FirstOrDefault) Then
                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbCode.Focus()
                tbCode.SelectAll()
                Exit Sub
            End If
        Else
            If tbCode.Text <> oldCode Then
                If Not IsNothing((From c In stOwner.Instance.Own Where c.Code = tbCode.Text).FirstOrDefault) Then
                    MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tbCode.Focus()
                    tbCode.SelectAll()
                    Exit Sub
                End If
            End If
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If sNew = True Then
                        db.Owners.Add(New Owners With {.FKCompany = CompID, .Code = tbCode.Text, .Name = tbName.Text, .Address = tbAddress.Text, .Tel = tbTel.Text, .Fax = tbFax.Text, .Email = tbEmail.Text, .TaxNo = tbTax.Text, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        db.SaveChanges()
                        ClearData()
                        Dim lastadd = (From c In db.Owners Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                        stOwner.Instance.Own.Add(lastadd)
                    Else
                        Dim ds = (From c In stOwner.Instance.Own Where c.Id = frmOwner.SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Code = tbCode.Text
                            ds.Name = tbName.Text
                            ds.Address = tbAddress.Text
                            ds.Tel = tbTel.Text
                            ds.Fax = tbFax.Text
                            ds.Email = tbEmail.Text
                            ds.TaxNo = tbTax.Text
                            ds.Description = tbDesc.Text
                            ds.UpdateDate = DateTimeServer()
                            ds.UpdateBy = Username
                        End If
                        db.Entry(ds).State = EntityState.Modified
                        db.SaveChanges()
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmOwner.SelData()
            If sEdit = True Then Me.Close()
        End If
    End Sub
End Class