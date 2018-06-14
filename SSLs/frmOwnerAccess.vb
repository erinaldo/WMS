Imports Microsoft.Office.Interop

Public Class frmOwnerAccess

    Public dt As New List(Of OwnerAccess)
    Public Stat As Boolean
    Public SId, OwnID As Integer

    Private Sub frmOwnerAccess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "สิทธิ์การเข้าถึง Owner ของผู้ใช้ : " & frmUser.tbName.Text
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
        tbWh.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbWh.KeyDown, tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub SelData()
        Try
            Using db = New PTGwmsEntities
                dt = db.OwnerAccess.Include("Owners").Where(Function(x) x.Enable = True And x.FKCompany = CompID And x.FKUser = frmUser.tbUserId.Text).ToList
            End Using
            Dim dtGrid = (From c In dt Order By c.Id Select New With {c.Id, c.FKOwner, .OwnCode = c.Owners.Code, .OwnerName = c.Owners.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns("FKOwner").Visible = False
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
                SId = .Rows(e.RowIndex).Cells(0).Value
                OwnID = .Rows(e.RowIndex).Cells(1).Value
                tbWh.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
                tbWhDesc.Text = .Rows(e.RowIndex).Cells(3).Value.ToString
                tbDesc.Text = .Rows(e.RowIndex).Cells(4).Value.ToString
            End With
            ButtonTrue()
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub tbWh_Leave(sender As Object, e As EventArgs) Handles tbWh.Leave
        If tbWh.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbWh.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 39
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbWh.Text.Trim
                frmSearch.ShowDialog()
                OwnID = s_ID
                tbWh.Text = s_Code
                tbWhDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    OwnID = a.Id
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
        bt_Num = 39
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWh.Text
        frmSearch.ShowDialog()
        OwnID = s_ID
        tbWh.Text = s_Code
        tbWhDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbWh.Text = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWh.Focus()
            Exit Sub
        End If
        If Stat = False Then
            If Not IsNothing((From c In dt Where c.Owners.Code = tbWh.Text).FirstOrDefault) Then
                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWh.Focus()
                tbWh.SelectAll()
                Exit Sub
            End If
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If Stat = False Then
                        db.OwnerAccess.Add(New OwnerAccess With {.FKCompany = CompID, .FKUser = frmUser.tbUserId.Text, .FKOwner = OwnID, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Else
                        Dim ds = (From c In db.OwnerAccess Where c.Id = SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.FKOwner = OwnID
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
                    Dim ds = (From c In db.OwnerAccess Where c.Id = SId).FirstOrDefault
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
            Dim dtGrid = (From c In dt Order By c.Id Where c.Owners.Code.Contains(tbSearch.Text) Or c.Owners.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, .OwnCode = c.Owners.Code, .OwnerName = c.Owners.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            DataGridView1.DataSource = dtGrid
            DataGridView1.Columns("Id").Visible = False
            DataGridView1.Columns("FKOwner").Visible = False
            DataGridView1.AutoResizeColumns()
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(0)

        Dim strDg As String = "H"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล สิทธิ์การเข้าถึง Owner ของผู้ใช้ : " & frmUser.tbName.Text
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
End Class