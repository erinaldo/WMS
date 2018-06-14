Imports System.Data.Entity

Public Class frmVendorEdit

    Dim oldCode As String

    Private Sub frmVendorEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        oldCode = ""
        If sEdit = True Then
            Dim Sel = (From c In stVendor.Instance.VD Where c.Id = frmVendor.SId).FirstOrDefault
            If Not IsNothing(Sel) Then
                tbCode.Text = Sel.Code
                oldCode = Sel.Code
                tbName.Text = Sel.Name
                tbAddress.Text = Sel.Address
                tbTel.Text = Sel.Tel
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

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbCode.KeyDown, tbName.KeyDown, tbAddress.KeyDown, tbTel.KeyDown, tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In stVendor.Instance.VD Where c.Id = frmVendor.SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        stVendor.Instance.VD.Remove(ds)
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
            frmVendor.SelData()
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส Vendor", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCode.Focus()
            Exit Sub
        End If
        If tbName.Text = "" Then
            MessageBox.Show("กรุณาป้อน ชื่อ Vendor", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            Exit Sub
        End If
        If sNew = True Then
            If Not IsNothing((From c In stVendor.Instance.VD Where c.Code = tbCode.Text).FirstOrDefault) Then
                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbCode.Focus()
                tbCode.SelectAll()
                Exit Sub
            End If
        Else
            If tbCode.Text <> oldCode Then
                If Not IsNothing((From c In stVendor.Instance.VD Where c.Code = tbCode.Text).FirstOrDefault) Then
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
                        db.Vendor.Add(New Vendor With {.FKCompany = CompID, .Code = tbCode.Text, .Name = tbName.Text, .Address = tbAddress.Text, .Tel = tbTel.Text, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        db.SaveChanges()
                        ClearData()
                        Dim lastadd = (From c In db.Vendor Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                        stVendor.Instance.VD.Add(lastadd)
                    Else
                        Dim ds = (From c In stVendor.Instance.VD Where c.Id = frmVendor.SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Code = tbCode.Text
                            ds.Name = tbName.Text
                            ds.Address = tbAddress.Text
                            ds.Tel = tbTel.Text
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
            frmVendor.SelData()
            If sEdit = True Then Me.Close()
        End If
    End Sub
End Class