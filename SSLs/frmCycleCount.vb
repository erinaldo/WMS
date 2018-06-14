Imports System.Globalization
Imports System.Threading

Public Class frmCycleCount

    Public dtCur As New List(Of CurrentStocks)
    Public OwnID, ZoneID, WHId, PdId, StatId, LocId As Integer
    Private WithEvents songsDataGridView As New System.Windows.Forms.DataGridView
    Dim ckBox As New CheckBox()
    Dim width_columcheckbox As Double = 50

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือกผู้ตรวจนับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Exit Sub
        End If
        Dim i As Integer = 0
        For Each row As DataGridViewRow In Me.DataGridView2.Rows
            Dim sel = (From c In dtCur Where c.Id = row.Cells(1).Value).FirstOrDefault
            If Not IsNothing(sel) Then
                If row.Cells(0).Value = True Then
                    DataGrid1.Rows.Add(sel.Locations.Name, sel.Products.Code, sel.Products.Name, sel.ItemStatus.Code, sel.Qty - sel.BookQty, sel.LotNo, sel.ProductDate, sel.ExpDate, sel.ReceiveDate, sel.PalletCode, ComboBox1.SelectedValue, sel.FKLocation, sel.FKProduct, sel.FKItemStatus, sel.FKProductUnit, sel.Id, sel.FKVendor)
                    i = i + 1
                    sel.Enable = False
                End If
            End If
        Next
        If i = 0 Then
            MessageBox.Show("กรุณาเลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            i = 0
            Exit Sub
        End If
        i = 0
        SumGrid()
        SelData2()
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub myDgv_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView2.ColumnWidthChanged
        Dim rect As Rectangle = DataGridView2.GetCellDisplayRectangle(0, -1, True)
        ckBox.Size = New Size(14, 14)
        rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
        rect.Y += 3
        ckBox.Location = rect.Location
    End Sub
    Private Sub ckBox_CheckedChanged()
        Dim i As Integer = 0
        If ckBox.Checked = True Then
            For j As Integer = 0 To Me.DataGridView2.RowCount - 1
                Me.DataGridView2(0, j).Value = True
            Next
        Else
            For j As Integer = 0 To Me.DataGridView2.RowCount - 1
                Me.DataGridView2(0, j).Value = False
            Next
        End If
    End Sub

    Private Sub myDgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellContentClick
        If e.ColumnIndex = 0 Then
            If DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            ElseIf DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
                DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
            End If
        End If
    End Sub

    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        dtDocdate.CustomFormat = "dd/MM/yyyy"
        DataGrid1.Rows.Clear()
        DataGridView2.DataSource = Nothing
        ClearTextBox(GroupBox1)
        ClearTextBox(Me)
        tbDocNo.Text = "CNT" & Format(DateTimeServer(), "yyMMddHHmm")
        tbDocNo.Focus()
    End Sub

    Private Sub frmCycleCount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        ComboBox2.SelectedIndex = 0
        tbDocNo.Text = "CNT" & Format(DateTimeServer(), "yyMMddHHmm")
        tbDocNo.Focus()
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbOwner.KeyDown, tbWH.KeyDown, tbDesc.KeyDown, ComboBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbOwner_Leave(sender As Object, e As EventArgs) Handles tbOwner.Leave
        If tbOwner.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbOwner.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 46
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbOwner.Text.Trim
                frmSearch.ShowDialog()
                OwnID = s_ID
                tbOwner.Text = s_Code
                tbOwnerDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    OwnID = a.Id
                    tbOwner.Text = a.Code
                    tbOwnerDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbOwner.SelectAll()
                tbOwner.Focus()
            End If
        Else
            tbOwnerDesc.Clear()
        End If
    End Sub

    Private Sub btLoadStock_Click(sender As Object, e As EventArgs) Handles btLoadStock.Click
        If tbOwner.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbOwner.Focus()
            Exit Sub
        End If
        If tbWH.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWH.Focus()
            Exit Sub
        End If
        SelData()
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            DataGridView2.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView2.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView2.GetCellDisplayRectangle(0, -1, True)
            ckBox.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
            rect.Y += 3
            ckBox.Location = rect.Location
            AddHandler ckBox.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged)
            DataGridView2.Controls.Add(ckBox)
            DataGridView2.Columns(0).Frozen = False
            Dim dtGrid2 = (From c In dtCur Order By c.Id Where c.Locations.Name.Contains(tbSearch.Text) Or c.Products.Code.Contains(tbSearch.Text) Or c.Products.Name.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"), c.LotNo,
                    .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), c.PalletCode,
                    .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("FKOwner").Visible = False
                .Columns("FKVendor").Visible = False
                .Columns("FKLocation").Visible = False
                .Columns("FKProduct").Visible = False
                .Columns("FKProductUnit").Visible = False
                .Columns("FKItemStatus").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            DataGridView2.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView2.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView2.GetCellDisplayRectangle(0, -1, True)
            ckBox.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
            rect.Y += 3
            ckBox.Location = rect.Location
            AddHandler ckBox.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged)
            DataGridView2.Controls.Add(ckBox)
            DataGridView2.Columns(0).Frozen = False
            Using db = New PTGwmsEntities
                dtCur = (From c In db.CurrentStocks.Include("Locations").Include("Products").Include("ItemStatus") Where c.Enable = True And c.FKCompany = CompID And c.FKWarehouse = WHId And c.FKOwner = OwnID And c.Qty > 0).ToList
                If dtCur.Count > 0 Then
                    Dim dtGrid2 = (From c In dtCur Order By c.Id Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"), c.LotNo,
                    .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), c.PalletCode,
                    .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                    With DataGridView2
                        .DataSource = dtGrid2
                        .Columns("Id").Visible = False
                        .Columns("FKOwner").Visible = False
                        .Columns("FKVendor").Visible = False
                        .Columns("FKLocation").Visible = False
                        .Columns("FKProduct").Visible = False
                        .Columns("FKProductUnit").Visible = False
                        .Columns("FKItemStatus").Visible = False
                        .AutoResizeColumns()
                        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                        .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End With
                End If
                Dim dtUser = db.Users.Where(Function(x) x.FKCompany = CompID And x.Enable = True).ToList
                ComboBox1.DataSource = dtUser
                ComboBox1.DisplayMember = "Name"
                ComboBox1.ValueMember = "Id"
            End Using
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGrid1.RowPostPaint
        Using b As New SolidBrush(DataGrid1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbDocNo.Text = "" Then
            MessageBox.Show("กรุณาป้อน เลขที่ตรวจนับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbDocNo.Focus()
            Exit Sub
        End If
        If ComboBox2.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือกประเภทการตรวจนับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox2.Focus()
            Exit Sub
        End If
        If DataGrid1.Rows.Count = 0 Then
            MessageBox.Show("ยังไม่มีรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.SelectAll()
            ComboBox1.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    Dim chk = (From c In db.CycleCountHD Where c.DocNo = tbDocNo.Text.Trim).FirstOrDefault
                    If Not IsNothing(chk) Then
                        MessageBox.Show("ป้อนเลขที่เอกสารซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbDocNo.Focus()
                        Exit Sub
                    End If
                    Dim CycleCountHD = New CycleCountHD With {.FKCompany = CompID, .FKWarehouse = WHId, .FKOwner = OwnID, .DocNo = tbDocNo.Text, .DocDate = dtDocdate.Value, .Description = tbDesc.Text,
                    .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                    For Each dr As DataGridViewRow In DataGrid1.Rows
                        CycleCountHD.CycleCountDTL.Add(New CycleCountDTL With {.FKVendor = dr.Cells(16).Value, .FKLocation = dr.Cells(11).Value, .FKProduct = dr.Cells(12).Value, .Qty = CDbl(dr.Cells(4).Value), .QtyCount = 0, .FKProductUnit = dr.Cells(14).Value, .LotNo = dr.Cells(5).Value, .ProductDate = dr.Cells(6).Value, .ExpDate = dr.Cells(7).Value, .ReceiveDate = dr.Cells(8).Value, .FKItemStatus = dr.Cells(13).Value, .PalletCode = dr.Cells(9).Value, .CheckBy = dr.Cells(10).Value, .CheckType = ComboBox2.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Next
                    db.CycleCountHD.Add(CycleCountHD)
                    db.SaveChanges()
                End Using
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btClear_Click(sender, e)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 1 Then
            tbSearch.Enabled = False
            tbDocNo.Text = "PHY" & Format(DateTimeServer(), "yyMMddHHmm")
        Else
            tbSearch.Enabled = True
            tbDocNo.Text = "CNT" & Format(DateTimeServer(), "yyMMddHHmm")
        End If
    End Sub

    Public Sub SelData2()
        Try
            Cursor = Cursors.WaitCursor
            DataGridView2.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView2.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView2.GetCellDisplayRectangle(0, -1, True)
            ckBox.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
            rect.Y += 3
            ckBox.Location = rect.Location
            AddHandler ckBox.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged)
            DataGridView2.Controls.Add(ckBox)
            DataGridView2.Columns(0).Frozen = False
            Dim dtGrid2 = (From c In dtCur Order By c.Id Where c.Enable = True Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"), c.LotNo,
                    .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), c.PalletCode,
                    .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("FKOwner").Visible = False
                .Columns("FKVendor").Visible = False
                .Columns("FKLocation").Visible = False
                .Columns("FKProduct").Visible = False
                .Columns("FKProductUnit").Visible = False
                .Columns("FKItemStatus").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        Dim price As Decimal = 0
        For index As Integer = 0 To DataGrid1.RowCount - 1
            qty += Convert.ToDecimal(DataGrid1.Rows(index).Cells(4).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
    End Sub

    Private Sub btOwner_Click(sender As Object, e As EventArgs) Handles btOwner.Click
        bt_Num = 46
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbOwner.Text
        frmSearch.ShowDialog()
        OwnID = s_ID
        tbOwner.Text = s_Code
        tbOwnerDesc.Text = s_Desc
        tbWH.Focus()
    End Sub

    Private Sub tbWH_Leave(sender As Object, e As EventArgs) Handles tbWH.Leave
        If tbWH.Text <> "" Then
            Dim da = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbWH.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 47
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbWH.Text.Trim
                frmSearch.ShowDialog()
                WHId = s_ID
                tbWH.Text = s_Code
                tbWHDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    WHId = a.Id
                    tbWH.Text = a.Code
                    tbWHDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWH.SelectAll()
                tbWH.Focus()
            End If
        Else
            tbWHDesc.Clear()
        End If
    End Sub

    Private Sub btWH_Click(sender As Object, e As EventArgs) Handles btWH.Click
        bt_Num = 47
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWH.Text
        frmSearch.ShowDialog()
        WHId = s_ID
        tbWH.Text = s_Code
        tbWHDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

End Class