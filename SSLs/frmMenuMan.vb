Public Class frmMenuMan

    Dim SID, MnuID, RID As Integer

    Private Sub frmMenuMan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Dim dtGrid = (From c In stRole.Instance.Role Order By c.Code Select New With {c.Id, .รหัส = c.Code, .สิทธิ์การใช้ = c.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            btDelete.Enabled = False
            btSave.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails2_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                SID = .Rows(e.RowIndex).Cells(0).Value
                btSave.Enabled = True
                SelData2()
            End With
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView2
                RID = .Rows(e.RowIndex).Cells(0).Value
                btDelete.Enabled = True
            End With
        End If
    End Sub

    Sub SelData2()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Dim dt = (From c In db.MenuAccess Where c.Enable = True And c.FKRole = SID).ToList
                Dim dtGrid = (From c In dt Order By c.Id Select New With {c.Id, .เมนูใช้งาน = c.Menu.Code & " - " & c.Menu.Name, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView2
                    .DataSource = dtGrid
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End Using
            btDelete.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbWh.Text = "" Then
            MessageBox.Show("กรุณาป้อน เมนู", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWh.Focus()
            Exit Sub
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    db.MenuAccess.Add(New MenuAccess With {.FKRole = SID, .FKMenu = MnuID, .Description = tbWhDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    db.SaveChanges()
                End Using
                tbWh.Clear()
                tbWhDesc.Clear()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData2()
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In db.MenuAccess Where c.Id = RID).FirstOrDefault
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
            SelData2()
        End If
    End Sub

    Private Sub tbWh_Leave(sender As Object, e As EventArgs) Handles tbWh.Leave
        If tbWh.Text <> "" Then
            Dim da = (From c In stMenu.Instance.Menu Order By c.Id Where c.Code.Contains(tbWh.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 40
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbWh.Text.Trim
                frmSearch.ShowDialog()
                MnuID = s_ID
                tbWh.Text = s_Code
                tbWhDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    MnuID = a.Id
                    tbWh.Text = a.Code
                    tbWhDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWh.SelectAll()
                tbWh.Focus()
            End If
        Else
            tbWhDesc.Clear()
        End If
    End Sub

    Private Sub btWh_Click(sender As Object, e As EventArgs) Handles btWh.Click
        bt_Num = 40
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWh.Text
        frmSearch.ShowDialog()
        MnuID = s_ID
        tbWh.Text = s_Code
        tbWhDesc.Text = s_Desc
    End Sub

End Class