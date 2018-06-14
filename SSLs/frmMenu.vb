Imports Microsoft.Office.Interop

Public Class frmMenu

    Public Stat As Boolean
    Public SId As Integer
    Dim codeOld As String

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
        btNew_Click(sender, e)
    End Sub

    Private Sub ButtonFalse()
        btNew.Enabled = False
        btDelete.Enabled = False
        Stat = False
    End Sub

    Private Sub ButtonTrue()
        btNew.Enabled = True
        btDelete.Enabled = True
        Stat = True
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearTextBox(Me)
        ButtonFalse()
        tbCode.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbCode.KeyDown, tbName.KeyDown, tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Dim dtGrid = (From c In stMenu.Instance.Menu Order By c.Code Select New With {c.Id, .รหัส = c.Code, .ชื่อเมนู = c.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                SId = .Rows(e.RowIndex).Cells(0).Value
                Dim ds = (From c In stMenu.Instance.Menu Where c.Id = SId).FirstOrDefault
                If Not IsNothing(ds) Then
                    tbCode.Text = ds.Code
                    codeOld = ds.Code
                    tbName.Text = ds.Name
                    tbDesc.Text = ds.Description
                End If
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
        If tbCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCode.Focus()
            Exit Sub
        End If
        If tbName.Text = "" Then
            MessageBox.Show("กรุณาป้อน ชื่อเมนู", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            Exit Sub
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If Stat = False Then
                        If Not IsNothing((From c In stMenu.Instance.Menu Where c.Code = tbCode.Text).FirstOrDefault) Then
                            MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbCode.Focus()
                            tbCode.SelectAll()
                            Exit Sub
                        End If
                        db.Menu.Add(New Menu With {.Code = tbCode.Text, .Name = tbName.Text, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        db.SaveChanges()
                        btNew_Click(sender, e)
                        Dim lastadd = (From c In db.Menu Order By c.UpdateDate Descending Where c.UpdateBy = Username).FirstOrDefault
                        stMenu.Instance.Menu.Add(lastadd)
                    Else
                        Dim ds = (From c In db.Menu Where c.Id = SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Code = tbCode.Text
                            ds.Name = tbName.Text
                            ds.Description = tbDesc.Text
                            ds.UpdateDate = DateTimeServer()
                            ds.UpdateBy = Username
                            db.SaveChanges()
                        End If
                        Dim ds1 = (From c In stMenu.Instance.Menu Where c.Id = SId).FirstOrDefault
                        If Not IsNothing(ds1) Then
                            stMenu.Instance.Menu.Remove(ds1)
                            Dim lastadd = (From c In db.Menu Order By c.UpdateDate Descending Where c.UpdateBy = Username).FirstOrDefault
                            stMenu.Instance.Menu.Add(lastadd)
                        End If
                    End If
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
                    Dim ds = (From c In db.Menu Where c.Id = SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        db.SaveChanges()
                    End If
                    Dim ds1 = (From c In stMenu.Instance.Menu Where c.Id = SId).FirstOrDefault
                    If Not IsNothing(ds1) Then
                        ds1.UpdateDate = DateTimeServer()
                        ds1.UpdateBy = Username
                        ds1.Enable = False
                        stMenu.Instance.Menu.Remove(ds1)
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
            Dim dtGrid = (From c In stMenu.Instance.Menu Order By c.Code Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text) Select New With {c.Id, .รหัส = c.Code, .ชื่อเมนู = c.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            DataGridView1.DataSource = dtGrid
            DataGridView1.Columns("Id").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        Cursor = Cursors.WaitCursor
        DataGridView1.Columns.RemoveAt(0)

        Dim strDg As String = "H"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล เมนูการใช้งาน"
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
        Cursor = Cursors.Default
    End Sub
End Class