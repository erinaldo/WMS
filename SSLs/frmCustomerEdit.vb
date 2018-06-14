Imports System.Data.Entity

Public Class frmCustomerEdit

    Dim oldCode As String

    Private Sub frmCustomerEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        oldCode = ""
        Dim own = (From c In stOwner.Instance.Own Select c.Id, Name = c.Code & "-" & c.Name).ToList
        cbOwner.DataSource = own
        cbOwner.DisplayMember = "Name"
        cbOwner.ValueMember = "Id"
        If sEdit = True Then
            Dim Sel = (From c In stCustomer.Instance.Cust Where c.Id = frmCustomer.SId).FirstOrDefault
            If Not IsNothing(Sel) Then
                cbOwner.SelectedValue = Sel.FKOwner
                tbCode.Text = Sel.Code
                oldCode = Sel.Code
                tbName.Text = Sel.Name
                tbAddress.Text = Sel.Address
                tbTel.Text = Sel.Tel
                tbFax.Text = Sel.Fax
                tbEmail.Text = Sel.Email
                tbDesc.Text = Sel.Description
                btDelete.Enabled = True
            End If
        Else
            cbOwner.Enabled = True
            ClearData()
        End If
    End Sub

    Sub ClearData()
        ClearTextBox(Me)
        btDelete.Enabled = False
        tbCode.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbCode.KeyDown, tbName.KeyDown, tbDesc.KeyDown, tbAddress.KeyDown, tbTel.KeyDown, tbFax.KeyDown, tbEmail.KeyDown, cbOwner.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In stCustomer.Instance.Cust Where c.Id = frmCustomer.SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        stCustomer.Instance.Cust.Remove(ds)
                    End If
                    Dim ds1 = (From c In db.Customers Where c.Id = frmCustomer.SId).FirstOrDefault
                    If Not IsNothing(ds1) Then
                        ds1.UpdateDate = DateTimeServer()
                        ds1.UpdateBy = Username
                        ds1.Enable = False
                        db.SaveChanges()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            MessageBox.Show("ลบข้อมูลเรียบร้อย", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            frmCustomer.SelData()
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim FKOwn As Integer = cbOwner.SelectedValue
        If tbCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCode.Focus()
            Exit Sub
        End If
        If tbName.Text = "" Then
            MessageBox.Show("กรุณาป้อน ชื่อลูกค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            Exit Sub
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If sNew = True Then
                        If Not IsNothing((From c In db.Customers Where c.FKOwner = FKOwn And c.Code = tbCode.Text).FirstOrDefault) Then
                            MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbCode.Focus()
                            tbCode.SelectAll()
                            Exit Sub
                        End If
                        db.Customers.Add(New Customers With {.FKCompany = CompID, .FKOwner = cbOwner.SelectedValue, .Code = tbCode.Text, .Name = tbName.Text, .Address = tbAddress.Text, .Tel = tbTel.Text, .Fax = tbFax.Text, .Email = tbEmail.Text, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        db.SaveChanges()
                        ClearData()
                        Dim lastadd = (From c In db.Customers.Include("Owners") Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                        stCustomer.Instance.Cust.Add(lastadd)
                    Else
                        If tbCode.Text <> oldCode Then
                            If Not IsNothing((From c In db.Customers Where c.FKOwner = FKOwn And c.Code = tbCode.Text).FirstOrDefault) Then
                                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbCode.Focus()
                                tbCode.SelectAll()
                                Exit Sub
                            End If
                        End If
                        Dim ds1 = (From c In db.Customers Where c.Id = frmCustomer.SId).FirstOrDefault
                        If Not IsNothing(ds1) Then
                            ds1.FKOwner = cbOwner.SelectedValue
                            ds1.Code = tbCode.Text
                            ds1.Name = tbName.Text
                            ds1.Address = tbAddress.Text
                            ds1.Tel = tbTel.Text
                            ds1.Fax = tbFax.Text
                            ds1.Email = tbEmail.Text
                            ds1.Description = tbDesc.Text
                            ds1.UpdateDate = DateTimeServer()
                            ds1.UpdateBy = Username
                            db.SaveChanges()
                        End If
                        Dim ds = (From c In stCustomer.Instance.Cust Where c.Id = frmCustomer.SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            stCustomer.Instance.Cust.Remove(ds)
                            Dim lastadd = (From c In db.Customers.Include("Owners") Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                            stCustomer.Instance.Cust.Add(lastadd)
                        End If
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmCustomer.SelData()
            If sEdit = True Then Me.Close()
        End If
    End Sub
End Class