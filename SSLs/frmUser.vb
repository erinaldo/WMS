Imports Microsoft.Office.Interop

Public Class frmUser

    Public dt As New List(Of Users)
    Public dtR As New List(Of Role)
    Public dtB As New List(Of Company)
    Public Stat As Boolean

    Private Sub frmUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
        btNew_Click(sender, e)
    End Sub

    Private Sub ButtonFalse()
        btNew.Enabled = False
        btDelete.Enabled = False
        tbUserId.ReadOnly = False
    End Sub

    Private Sub ButtonTrue()
        btNew.Enabled = True
        btDelete.Enabled = True
        tbUserId.ReadOnly = True
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearTextBox(Me)
        ButtonFalse()
        cbRole.SelectedValue = 0
        cbBranch.SelectedValue = 0
        tbUserId.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbUserId.KeyDown, tbPassword.KeyDown, tbConfirm.KeyDown, tbName.KeyDown, cbBranch.KeyDown, cbRole.KeyDown, tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub SelData()
        Try
            Using db = New PTGwmsEntities
                dt = db.Users.Include("Role").Include("Company").Where(Function(x) x.FKCompany = CompID And x.Enable = True).ToList
                dtR = db.Role.Where(Function(x) x.FKCompany = CompID And x.Enable = True).ToList
                dtB = db.Company.Where(Function(x) x.Enable = True).ToList
            End Using
            cbRole.DataSource = dtR
            cbRole.DisplayMember = "Name"
            cbRole.ValueMember = "Id"
            cbBranch.DataSource = dtB
            cbBranch.DisplayMember = "Name"
            cbBranch.ValueMember = "Id"
            Dim dtGrid = (From c In dt Order By c.CreateDate Select New With {.รหัสผู้ใช้ = c.Id, c.Password, .ชื่อผู้ใช้ = c.Name, .Rid = c.FKRole, .สิทธิ์ = c.Role.Name, .Bid = c.FKCompany, .บริษัท = c.Company.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Rid").Visible = False
                .Columns("Bid").Visible = False
                .Columns("Password").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                tbUserId.Text = .Rows(e.RowIndex).Cells(0).Value.ToString
                tbPassword.Text = .Rows(e.RowIndex).Cells(1).Value.ToString
                tbConfirm.Text = .Rows(e.RowIndex).Cells(1).Value.ToString
                tbName.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
                cbRole.SelectedValue = .Rows(e.RowIndex).Cells(3).Value
                cbBranch.SelectedValue = .Rows(e.RowIndex).Cells(5).Value
                tbDesc.Text = .Rows(e.RowIndex).Cells(7).Value.ToString
            End With
            ButtonTrue()
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbUserId.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัสผู้ใช้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbUserId.Focus()
            Exit Sub
        End If
        If tbPassword.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัสผ่าน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbPassword.Focus()
            Exit Sub
        End If
        If tbPassword.Text.Trim <> tbConfirm.Text.Trim Then
            MessageBox.Show("กรุณายืนยัน รหัสผ่าน อีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbPassword.Focus()
            Exit Sub
        End If
        If tbName.Text = "" Then
            MessageBox.Show("กรุณาป้อน ชื่อผู้ใช้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            Exit Sub
        End If
        If cbRole.SelectedValue = 0 Then
            MessageBox.Show("กรุณาเลือก สิทธิ์", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbRole.Focus()
            Exit Sub
        End If
        If cbBranch.SelectedValue = 0 Then
            MessageBox.Show("กรุณาเลือก บริษัท", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbBranch.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If tbUserId.ReadOnly = False Then
                        db.Users.Add(New Users With {.Id = tbUserId.Text, .Password = tbPassword.Text, .Name = tbName.Text, .FKRole = cbRole.SelectedValue, .FKCompany = cbBranch.SelectedValue, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Else
                        Dim ds = (From c In db.Users Where c.Id = tbUserId.Text.Trim).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Password = tbPassword.Text
                            ds.Name = tbName.Text
                            ds.FKRole = cbRole.SelectedValue
                            ds.FKCompany = cbBranch.SelectedValue
                            ds.Description = tbDesc.Text
                            ds.UpdateDate = DateTimeServer()
                            ds.UpdateBy = Username
                        End If
                    End If
                    db.SaveChanges()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData()
            btNew_Click(sender, e)
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In db.Users Where c.Id = tbUserId.Text.Trim).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        db.SaveChanges()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            MessageBox.Show("ลบข้อมูลเรียบร้อย", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData()
            btNew_Click(sender, e)
        End If
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid = (From c In dt Order By c.CreateDate Where c.Id.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text) Or c.Role.Name.Contains(tbSearch.Text) Or c.Company.Name.Contains(tbSearch.Text) Select New With {.รหัสผู้ใช้ = c.Id, c.Password, .ชื่อผู้ใช้ = c.Name, .Rid = c.FKRole, .สิทธิ์ = c.Role.Name, .Bid = c.FKCompany, .สาขา = c.Company.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            DataGridView1.DataSource = dtGrid
            DataGridView1.Columns("Rid").Visible = False
            DataGridView1.Columns("Bid").Visible = False
            DataGridView1.Columns("Password").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        DataGridView1.Columns.RemoveAt(1)
        DataGridView1.Columns.RemoveAt(2)
        DataGridView1.Columns.RemoveAt(3)

        Dim strDg As String = "J"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล ผู้ใช้งาน"
        xlApp.ActiveSheet.Range("A1:" & strDg & "1").Merge()
        xlApp.ActiveSheet.Range("A1").HorizontalAlignment = Excel.Constants.xlCenter

        xlApp.ActiveSheet.Cells(2, 1) = "ค้นหาโดย : " & tbSearch.Text
        xlApp.ActiveSheet.Range("A2:" & strDg & "2").Merge()
        xlApp.ActiveSheet.Range("A2").HorizontalAlignment = Excel.Constants.xlCenter

        xlApp.ActiveSheet.Cells(3, 1) = "วันที่ออกรายงาน : " & Now.ToString("dd/MM/yyyy HH:mm") & ""
        xlSheet.Range("A3:" & strDg & "3").Merge()
        xlApp.ActiveSheet.Range("A3").HorizontalAlignment = Excel.Constants.xlCenter

        xlApp.ActiveSheet.Range("A1:" & strDg & "3").Interior.ColorIndex = 15
        xlApp.ActiveSheet.Range("A4:" & strDg & "4").Interior.ColorIndex = 6

        xlApp.ActiveSheet.Cells(4, 1).Value = "No."
        For k = 0 To DataGridView1.Columns.Count - 1 Step 1
            xlApp.ActiveSheet.Cells(4, k + 2).Value = DataGridView1.Columns(k).HeaderText
        Next

        For i = 0 To DataGridView1.Rows.Count - 1 Step 1
            xlApp.ActiveSheet.Cells(i + 5, 1).Value = "'" & i + 1
            For j = 0 To DataGridView1.Columns.Count - 1 Step 1
                xlApp.ActiveSheet.Cells(i + 5, j + 2).Value = "'" & DataGridView1.Rows(i).Cells(j).Value & ""
            Next
        Next
        xlApp.ActiveSheet.Range("A1:" & strDg & "1").FONT.Bold = True
        xlApp.ActiveSheet.Range("A1:" & strDg & "" & DataGridView1.Rows.Count + 4).BORDERS.Weight = 2
        xlApp.ActiveSheet.Columns("A:" & strDg & "").AutoFit()
        MessageBox.Show("ออกรายงานเรียบร้อย", "Report Finish", MessageBoxButtons.OK, MessageBoxIcon.Information)
        xlApp.Application.Visible = True
        xlSheet = Nothing
        xlBook = Nothing
        xlApp = Nothing
        tbSearch_TextChanged(sender, e)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmWarehouseAccess.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmOwnerAccess.ShowDialog()
    End Sub
End Class