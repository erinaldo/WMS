Imports Microsoft.Office.Interop

Public Class frmOutLocation

    Public dt As New List(Of Locations)
    Public dtZ As New List(Of Zone)
    Public dtT As New List(Of LocationType)
    Public dtWh As New List(Of Warehouse)
    Public Stat As Boolean
    Public SId, ZId, LtId As Integer

    Private Sub frmOutLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
        btNew_Click(sender, e)
        cbWh.SelectedIndex = -1
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
        tbName.Clear()
        tbZone.Clear()
        tbZoneDesc.Clear()
        tbType.Clear()
        tbTypeDesc.Clear()
        tbDesc.Clear()
        ButtonFalse()
        tbName.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbName.KeyDown, tbDesc.KeyDown, tbZone.KeyDown, tbType.KeyDown, cbWh.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Sub SelData()
        Try
            Using db = New PTGwmsEntities
                dt = db.Locations.Include("Warehouse").Include("Zone").Include("LocationType").Where(Function(x) x.Enable = True And x.InStorage = False And x.FKCompany = CompID).ToList
                dtZ = db.Zone.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                dtT = db.LocationType.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                dtWh = db.Warehouse.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
            End Using
            Dim dtw = (From c In dtWh Order By c.Id Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
            cbWh.DataSource = dtw
            cbWh.DisplayMember = "Name"
            cbWh.ValueMember = "Id"
            Dim dtGrid = (From c In dt Order By c.CreateDate Select New With {c.Id, c.FKWarehouse, .WHCode = c.Warehouse.Code, .คลังสินค้า = c.Warehouse.Name, .Location = c.Name, c.FKZone, .ZoneCode = c.Zone.Code, .โซน = c.Zone.Name, c.FKLocationType, .TypeCode = c.LocationType.Code, .ประเภท = c.LocationType.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns("FKZone").Visible = False
                .Columns("FKLocationType").Visible = False
                .Columns("FKWarehouse").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid = (From c In dt Order By c.Id Where c.Warehouse.Code.Contains(tbSearch.Text) Or c.Warehouse.Name.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Or c.Zone.Code.Contains(tbSearch.Text) Or c.Zone.Name.Contains(tbSearch.Text) Or c.LocationType.Code.Contains(tbSearch.Text) Or c.LocationType.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text) Select New With {c.Id, c.FKWarehouse, .WHCode = c.Warehouse.Code, .คลังสินค้า = c.Warehouse.Name, .Location = c.Name, c.FKZone, .ZoneCode = c.Zone.Code, .โซน = c.Zone.Name, c.FKLocationType, .TypeCode = c.LocationType.Code, .ประเภท = c.LocationType.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            DataGridView1.DataSource = dtGrid
            DataGridView1.Columns("Id").Visible = False
            DataGridView1.Columns("FKZone").Visible = False
            DataGridView1.Columns("FKLocationType").Visible = False
            DataGridView1.Columns("FKWarehouse").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                SId = .Rows(e.RowIndex).Cells(0).Value
                cbWh.SelectedValue = .Rows(e.RowIndex).Cells(1).Value
                tbName.Text = .Rows(e.RowIndex).Cells(4).Value.ToString
                ZId = .Rows(e.RowIndex).Cells(5).Value
                tbZone.Text = .Rows(e.RowIndex).Cells(6).Value.ToString
                tbZoneDesc.Text = .Rows(e.RowIndex).Cells(7).Value.ToString
                LtId = .Rows(e.RowIndex).Cells(8).Value
                tbType.Text = .Rows(e.RowIndex).Cells(9).Value.ToString
                tbTypeDesc.Text = .Rows(e.RowIndex).Cells(10).Value.ToString
                tbDesc.Text = .Rows(e.RowIndex).Cells(11).Value.ToString
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
        If cbWh.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือก คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            cbWh.Focus()
            Exit Sub
        End If
        If tbName.Text = "" Then
            MessageBox.Show("กรุณาป้อน Location", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            Exit Sub
        End If
        If tbZone.Text = "" Then
            MessageBox.Show("กรุณาป้อน โซนจัดเก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbZone.Focus()
            Exit Sub
        End If
        If tbType.Text = "" Then
            MessageBox.Show("กรุณาป้อน ประเภท", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbType.Focus()
            Exit Sub
        End If

        If Not IsNothing((From c In dt Where c.FKWarehouse = cbWh.SelectedValue And c.FKZone = ZId).FirstOrDefault) Then
            MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName.Focus()
            tbName.SelectAll()
            Exit Sub
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If Stat = False Then
                        db.Locations.Add(New Locations With {.InStorage = False, .Name = tbName.Text, .Description = tbDesc.Text, .FKCompany = CompID, .FKZone = ZId, .FKLocationType = LtId, .FKWarehouse = cbWh.SelectedValue, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Else
                        Dim ds = (From c In db.Locations Where c.Id = SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Name = tbName.Text
                            ds.Description = tbDesc.Text
                            ds.FKCompany = CompID
                            ds.FKZone = ZId
                            ds.FKLocationType = LtId
                            ds.FKWarehouse = cbWh.SelectedValue
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

    Private Sub tbZone_Leave(sender As Object, e As EventArgs) Handles tbZone.Leave
        If tbZone.Text <> "" Then
            Dim da = (From c In dtZ Order By c.Code Where c.FKWarehouse = cbWh.SelectedValue And c.Code.Contains(tbZone.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 23
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbZone.Text.Trim
                frmSearch.ShowDialog()
                ZId = s_ID
                tbZone.Text = s_Code
                tbZoneDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    ZId = a.Id
                    tbZone.Text = a.Code
                    tbZoneDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbZone.SelectAll()
                tbZone.Focus()
            End If
        Else
            tbZoneDesc.Clear()
        End If
    End Sub

    Private Sub btZone_Click(sender As Object, e As EventArgs) Handles btZone.Click
        bt_Num = 23
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbZone.Text
        frmSearch.ShowDialog()
        ZId = s_ID
        tbZone.Text = s_Code
        tbZoneDesc.Text = s_Desc
    End Sub

    Private Sub tbType_Leave(sender As Object, e As EventArgs) Handles tbType.Leave
        If tbType.Text <> "" Then
            Dim da = (From c In dtT Order By c.Code Where c.Code.Contains(tbType.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 22
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbType.Text.Trim
                frmSearch.ShowDialog()
                LtId = s_ID
                tbType.Text = s_Code
                tbTypeDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    LtId = a.Id
                    tbType.Text = a.Code
                    tbTypeDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbType.SelectAll()
                tbType.Focus()
            End If
        Else
            tbTypeDesc.Clear()
        End If
    End Sub

    Private Sub btType_Click(sender As Object, e As EventArgs) Handles btType.Click
        bt_Num = 22
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbType.Text
        frmSearch.ShowDialog()
        LtId = s_ID
        tbType.Text = s_Code
        tbTypeDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In db.Locations Where c.Id = SId).FirstOrDefault
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

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(3)
        DataGridView1.Columns.RemoveAt(5)

        Dim strDg As String = "M"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล Location นอกพื้นที่เก็บ (" & Comp & ")"
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