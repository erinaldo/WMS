Imports System.Globalization
Imports System.Threading

Public Class frmTransfer

    Public dtCur As New List(Of CurrentStocks)
    Public ZoneID, WHId, PdId, StatId, LocId, rowID As Integer

    Private Sub frmTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        'tbStatus.Enabled = False
        'btStatus.Enabled = False
        tbDocNo.Text = "TRN" & Format(DateTimeServer(), "yyMMddHHmm")
        tbDocNo.Focus()
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbWH.KeyDown, tbZone.KeyDown, tbDesc.KeyDown, tbQty.KeyDown, tbLocation.KeyDown, tbStatus.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbStatus_Leave(sender As Object, e As EventArgs) Handles tbStatus.Leave
        If tbStatus.Text <> "" Then
            Dim da = (From c In stItemStatus.Instance.ItemStatus Order By c.Code Where c.Code.Contains(tbStatus.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 51
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbStatus.Text.Trim
                frmSearch.ShowDialog()
                StatId = s_ID
                tbStatus.Text = s_Code
            ElseIf da.Count = 1 Then
                For Each a In da
                    StatId = a.Id
                    tbStatus.Text = a.Code
                Next
            Else
                MessageBox.Show("สถานะ ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbStatus.SelectAll()
                tbStatus.Focus()
            End If
        End If
    End Sub

    Private Sub btStatus_Click(sender As Object, e As EventArgs) Handles btStatus.Click
        bt_Num = 51
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbStatus.Text
        frmSearch.ShowDialog()
        StatId = s_ID
        tbStatus.Text = s_Code
        tbQty.Focus()
    End Sub

    Private Sub tbZone_Leave(sender As Object, e As EventArgs) Handles tbZone.Leave
        If tbZone.Text <> "" Then
            If tbWH.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWH.Focus()
                Exit Sub
            End If
            Dim da = (From c In stZone.Instance.Zone Where c.FKWarehouse = WHId Order By c.Id Where c.Code.Contains(tbZone.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 44
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbZone.Text.Trim
                frmSearch.ShowDialog()
                ZoneID = s_ID
                tbZone.Text = s_Code
                tbZoneDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    ZoneID = a.Id
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
        bt_Num = 44
        If tbWH.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWH.Focus()
            Exit Sub
        End If
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbZone.Text
        frmSearch.ShowDialog()
        ZoneID = s_ID
        tbZone.Text = s_Code
        tbZoneDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

    Private Sub tbLocation_Leave(sender As Object, e As EventArgs) Handles tbLocation.Leave
        If tbLocation.Text <> "" Then
            If tbWH.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWH.Focus()
                Exit Sub
            End If
            If tbZone.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน โซน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbZone.Focus()
                Exit Sub
            End If
            Dim da = (From c In stLocation.Instance.Location Order By c.Name Where c.FKWarehouse = WHId And c.FKZone = ZoneID And c.Name.Contains(tbLocation.Text.Trim) Select c.Id, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 45
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbLocation.Text.Trim
                frmSearch.ShowDialog()
                LocId = s_ID
                tbLocation.Text = s_Code
            ElseIf da.Count = 1 Then
                For Each a In da
                    LocId = a.Id
                    tbLocation.Text = a.Name
                Next
            Else
                MessageBox.Show("Location ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbLocation.SelectAll()
                tbLocation.Focus()
            End If
        End If
    End Sub

    Private Sub btLocation_Click(sender As Object, e As EventArgs) Handles btLocation.Click
        If tbWH.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWH.Focus()
            Exit Sub
        End If
        If tbZone.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน โซน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbZone.Focus()
            Exit Sub
        End If
        bt_Num = 45
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbLocation.Text
        frmSearch.ShowDialog()
        LocId = s_ID
        tbLocation.Text = s_Code
        btConfirm.Focus()
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Cursor = Cursors.WaitCursor
            Dim dtGrid2 = (From c In dtCur Order By c.Id Where (c.Qty - c.BookQty) > 0 And (c.Locations.Name.Contains(tbSearch.Text) Or c.Products.Code.Contains(tbSearch.Text) Or c.Products.BaseBarcode.Contains(tbSearch.Text) Or c.Products.Name.Contains(tbSearch.Text)) Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .Barcode = c.Products.BaseBarcode, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty - c.BookQty, "#,##0.00"), c.LotNo,
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

    Public Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            If tbWH.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWH.Focus()
                Cursor = Cursors.Default
                Exit Sub
            End If
            If tbZone.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน โซน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbZone.Focus()
                Cursor = Cursors.Default
                Exit Sub
            End If
            Using db = New PTGwmsEntities
                dtCur = (From c In db.CurrentStocks.Include("Locations").Include("Products").Include("ItemStatus") Where c.Enable = True And c.FKCompany = CompID And c.FKWarehouse = WHId And c.Products.FKZone = ZoneID And (c.Qty - c.BookQty) > 0).ToList
                If dtCur.Count > 0 Then
                    Dim dtGrid2 = (From c In dtCur Order By c.Id Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .Barcode = c.Products.BaseBarcode, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty - c.BookQty, "#,##0.00"), c.LotNo,
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
                ComboBox1.SelectedIndex = -1
            End Using
            btConfirm.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Public Sub SelData2()
        Try
            Cursor = Cursors.WaitCursor
            Dim dtGrid2 = (From c In dtCur Order By c.Id Where c.Enable = True And (c.Qty - c.BookQty) > 0 And c.Products.Code.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .Barcode = c.Products.BaseBarcode, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty - c.BookQty, "#,##0.00"), c.LotNo,
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
            btConfirm.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub tbNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbQty.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub tbQty_Leave(sender As Object, e As EventArgs) Handles tbQty.Leave
        If IsNumeric(tbQty.Text) Then
            tbQty.Text = Format(CDbl(tbQty.Text), "#,##0.00")
        Else
            tbQty.Text = "0.00"
        End If
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If tbStatus.Text = "" Then
            MessageBox.Show("กรุณาป้อน สถานะ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbStatus.Focus()
            Exit Sub
        End If
        If tbLocation.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Location ปลายทาง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbLocation.Focus()
            Exit Sub
        End If
        If tbQty.Text = "" Then
            MessageBox.Show("กรุณาป้อน จำนวน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbQty.Focus()
            Exit Sub
        Else
            If CDbl(tbQty.Text) <= 0 Then
                MessageBox.Show("กรุณาป้อน จำนวน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbQty.Focus()
                Exit Sub
            End If
        End If
        Dim sel = (From c In dtCur Where c.Id = rowID).FirstOrDefault
        If Not IsNothing(sel) Then
            If sel.Locations.Name = tbLocation.Text Then
                MessageBox.Show("ไม่อนุญาตให้โอนไปที่ Location เดียวกัน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If CDbl(tbQty.Text) > (sel.Qty - sel.BookQty) Then
                MessageBox.Show("ป้อนจำนวนเกิน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbQty.Focus()
                Exit Sub
            End If
            Dim i As Integer = 0
            For i = 0 To DataGrid1.RowCount - 1
                If sel.Locations.Name & sel.Products.Code & sel.ItemStatus.Code & sel.LotNo & sel.ProductDate.ToString & tbLocation.Text = DataGrid1.Rows(i).Cells(0).Value.ToString & DataGrid1.Rows(i).Cells(1).Value.ToString & DataGrid1.Rows(i).Cells(3).Value.ToString & DataGrid1.Rows(i).Cells(5).Value.ToString & DataGrid1.Rows(i).Cells(6).Value.ToString & DataGrid1.Rows(i).Cells(10).Value.ToString Then
                    MessageBox.Show("ป้อนข้อมูลซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    tbQty.SelectAll()
                    tbQty.Focus()
                    Exit Sub
                End If
            Next
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim chk = (From c In db.CurrentStocks Where Enabled = True And c.Id = sel.Id And (c.Qty - c.BookQty) > 0).FirstOrDefault
                    If Not IsNothing(chk) Then
                        If CDbl(tbQty.Text) <= (chk.Qty - chk.BookQty) Then
                            chk.BookQty = chk.BookQty + CDbl(tbQty.Text)
                            chk.UpdateBy = Username
                            chk.UpdateDate = DateTimeServer()
                            If tbStatus.Text <> chk.ItemStatus.Code Then
                                Dim onh = (From c In db.StockOnhand Where Enabled = True And c.FKProduct = chk.FKProduct And c.FKItemStatus = chk.FKItemStatus And c.FKWarehouse = chk.FKWarehouse).FirstOrDefault
                                If Not IsNothing(onh) Then
                                    If CDbl(tbQty.Text) <= (onh.Qty - onh.BookQty) Then
                                        onh.BookQty = onh.BookQty + CDbl(tbQty.Text)
                                        onh.UpdateDate = DateTimeServer()
                                        onh.UpdateBy = Username
                                    Else
                                        MessageBox.Show("ยอด Stock Onhand ไม่พอ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        tbQty.Focus()
                                        Exit Sub
                                    End If
                                Else
                                    MessageBox.Show("ไม่พบสินค้าใน StockOnhand", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    tbQty.Focus()
                                    Exit Sub
                                End If
                            End If
                            DataGrid1.Rows.Add(sel.Locations.Name, sel.Products.Code, sel.Products.Name, tbStatus.Text, tbQty.Text, sel.LotNo, sel.ProductDate, sel.ExpDate, sel.ReceiveDate, sel.PalletCode, tbLocation.Text, sel.FKLocation, sel.FKProduct, StatId, sel.FKProductUnit, sel.Id, sel.ItemStatus.Code)
                            sel.BookQty = sel.BookQty + CDbl(tbQty.Text)
                            db.TransferLog.Add(New TransferLog With {.TransferNo = tbDocNo.Text, .RowIDCurrStock = sel.Id, .FKLocationSrc = sel.FKLocation, .LocationTo = tbLocation.Text, .FKProduct = sel.FKProduct, .QtyFrom = CDbl(tbQty.Text), .FKProductUnit = sel.FKProductUnit, .LotNo = sel.LotNo, .ProductDate = sel.ProductDate, .ExpDate = sel.ExpDate, .ReceiveDate = sel.ReceiveDate, .FKItemStatus = StatId, .OldItemStatus = sel.ItemStatus.Code, .TransferBy = ComboBox1.SelectedValue, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            db.SaveChanges()
                        Else
                            MessageBox.Show("ยอด Stock ไม่พอ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbQty.Focus()
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("ยอด Stock ไม่พอ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbQty.Focus()
                        Exit Sub
                    End If
                End Using
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        SumGrid()
        SelData2()
    End Sub

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        Dim price As Decimal = 0
        For index As Integer = 0 To DataGrid1.RowCount - 1
            qty += Convert.ToDecimal(DataGrid1.Rows(index).Cells(4).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbDocNo.Text = "" Then
            MessageBox.Show("กรุณาป้อน เลขที่ใบโอน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbDocNo.Focus()
            Exit Sub
        End If
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือกผู้โอน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Exit Sub
        End If
        If DataGrid1.Rows.Count = 0 Then
            MessageBox.Show("ยังไม่มีรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbLocation.SelectAll()
            tbLocation.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim rid, PrdID, ItmID As Integer
                    Dim chk = (From c In db.TransferHD Where c.DocNo = tbDocNo.Text.Trim).FirstOrDefault
                    If Not IsNothing(chk) Then
                        MessageBox.Show("ป้อนเลขที่เอกสารซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbDocNo.Focus()
                        Cursor = Cursors.Default
                        Exit Sub
                    End If
                    Dim TransferHD = New TransferHD With {.FKCompany = CompID, .FKWarehouse = WHId, .DocNo = tbDocNo.Text, .DocDate = dtDocdate.Value, .Description = tbDesc.Text,
                                        .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                    For Each dr As DataGridViewRow In DataGrid1.Rows
                        rid = dr.Cells(15).Value
                        PrdID = dr.Cells(12).Value
                        ItmID = dr.Cells(13).Value
                        TransferHD.TransferDTL.Add(New TransferDTL With {.RowIDCurrStock = rid, .FKLocationSrc = dr.Cells(11).Value, .LocationTo = dr.Cells(10).Value, .FKProduct = dr.Cells(12).Value, .QtyFrom = CDbl(dr.Cells(4).Value), .FKProductUnit = dr.Cells(14).Value, .LotNo = dr.Cells(5).Value, .ProductDate = dr.Cells(6).Value, .ExpDate = dr.Cells(7).Value, .ReceiveDate = dr.Cells(8).Value, .FKItemStatus = dr.Cells(13).Value, .OldItemStatus = dr.Cells(16).Value, .TransferBy = ComboBox1.SelectedValue, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        'Dim chkLo = (From c In db.CurrentStocks Where c.Enable = True And c.FKProduct = PrdID And (c.Qty < 0 Or c.BookQty < 0)).ToList
                        'If chkLo.Count > 0 Then
                        '    Cursor = Cursors.Default
                        '    MessageBox.Show("พบรายการสินค้าติดลบใน Stock Location รหัส : " & PrdID, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    Exit Sub
                        'End If
                        'Dim dt = (From c In db.CurrentStocks Where c.Id = rid).FirstOrDefault
                        'If Not IsNothing(dt) Then
                        '    dt.BookQty = dt.BookQty + CDbl(dr.Cells(4).Value)
                        '    dt.UpdateBy = Username
                        '    dt.UpdateDate = DateTimeServer()
                        '    If dr.Cells(16).Value.ToString <> dr.Cells(3).Value.ToString Then
                        '        Dim stk = (From c In db.StockOnhand Order By c.Id Descending Where c.FKCompany = dt.FKCompany And c.FKOwner = dt.FKOwner And c.FKWarehouse = dt.FKWarehouse And c.FKProduct = dt.FKProduct And c.FKItemStatus = dt.FKItemStatus).FirstOrDefault
                        '        If Not IsNothing(stk) Then
                        '            stk.BookQty = stk.BookQty + CDbl(dr.Cells(4).Value)
                        '            stk.UpdateDate = DateTimeServer()
                        '            stk.UpdateBy = Username
                        '        End If
                        '    End If
                        'End If
                    Next
                    db.TransferHD.Add(TransferHD)
                    db.SaveChanges()
                End Using
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btClear_Click(sender, e)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btLoadStock_Click(sender As Object, e As EventArgs) Handles btLoadStock.Click
        SelData()
    End Sub

    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        DataGrid1.Rows.Clear()
        DataGridView2.DataSource = Nothing
        ClearTextBox(Me)
        tbDocNo.Text = "TRN" & Format(DateTimeServer(), "yyMMddHHmm")
        tbDocNo.Focus()
    End Sub

    Private Sub DataGrid1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGrid1.KeyDown
        If e.KeyCode = Keys.Delete Then
            With DataGrid1
                Dim intRow As Integer = .CurrentRow.Cells(15).Value
                If .Rows.Count = 0 Then Exit Sub
                If MessageBox.Show("ต้องการลบรายการนี้ ใช่หรือไม่?", "ยืนยันการลบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                    Dim sel = (From c In dtCur Where c.Id = intRow).FirstOrDefault
                    If Not IsNothing(sel) Then
                        sel.BookQty = sel.BookQty - CDbl(.CurrentRow.Cells(4).Value)
                    End If
                    Try
                        Cursor = Cursors.WaitCursor
                        Using db = New PTGwmsEntities
                            Dim cur = (From c In db.CurrentStocks Where c.Enable = True And c.Id = intRow).FirstOrDefault
                            If Not IsNothing(cur) Then
                                cur.BookQty = cur.BookQty - CDbl(.CurrentRow.Cells(4).Value)
                                cur.UpdateBy = Username
                                cur.UpdateDate = DateTimeServer()
                                If .CurrentRow.Cells(3).Value <> .CurrentRow.Cells(16).Value Then
                                    Dim onh = (From c In db.StockOnhand Where Enabled = True And c.FKProduct = cur.FKProduct And c.FKItemStatus = cur.FKItemStatus And c.FKWarehouse = cur.FKWarehouse).FirstOrDefault
                                    If Not IsNothing(onh) Then
                                        onh.BookQty = onh.BookQty - CDbl(.CurrentRow.Cells(4).Value)
                                        onh.UpdateDate = DateTimeServer()
                                        onh.UpdateBy = Username
                                    Else
                                        MessageBox.Show("ไม่พบสินค้าใน StockOnhand", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        tbQty.Focus()
                                        Exit Sub
                                    End If
                                End If
                                db.SaveChanges()
                            Else
                                MessageBox.Show("ไม่พบสินค้าใน Stock Location", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End Using
                        Cursor = Cursors.Default
                    Catch ex As Exception
                        Cursor = Cursors.Default
                        MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    .Rows.Remove(.CurrentRow)
                    SumGrid()
                    SelData2()
                End If
            End With
        End If
    End Sub

    Private Sub frmTransfer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If DataGrid1.Rows.Count > 0 Then
            MessageBox.Show("ยังมีรายการโอนค้างอยู่ กรุณาลบรายการออกให้หมดก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.Cancel = True
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            rowID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            StatId = DataGridView2.Rows(e.RowIndex).Cells(6).Value
            tbStatus.Text = DataGridView2.Rows(e.RowIndex).Cells(11).Value
            tbQty.Text = DataGridView2.Rows(e.RowIndex).Cells(12).Value
            tbQty.Focus()
            btConfirm.Enabled = True
        End If
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

    Private Sub tbWH_Leave(sender As Object, e As EventArgs) Handles tbWH.Leave
        If tbWH.Text <> "" Then
            Dim da = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbWH.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 43
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
        bt_Num = 43
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWH.Text
        frmSearch.ShowDialog()
        WHId = s_ID
        tbWH.Text = s_Code
        tbWHDesc.Text = s_Desc
        tbZone.Focus()
    End Sub

End Class