Imports System.Data.Entity
Imports Microsoft.Office.Interop
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Threading
Imports System.Globalization

Public Class frmReceive

    Public dt As New List(Of RcvHeader)
    Public dtRcv As New List(Of RcvDetails)
    Public dtRcvLoc As New List(Of RcvLocation)
    Dim LocEmpty As New List(Of V_LocationEmpty)
    Public RcvHdId, RcvId, OwnerID, RcvLocID As Integer

    Private Sub frmReceive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btProcess.Enabled = False
        btConfirm.Enabled = False
        btEditDoc.Enabled = False
        btEditItem.Enabled = False
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dt = db.RcvHeader.Include("Owners").Include("Vendor").Include("RcvType").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing).ToList
                Else
                    Dim Rcv = db.RcvDetails.Where(Function(x) x.RcvHeader.FKCompany = CompID And x.RcvHeader.ConfirmDate Is Nothing And x.Enable = True And frmMain.ow.Contains(x.RcvHeader.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
                    Dim doc As New List(Of String)
                    If Rcv.Count > 0 Then
                        For Each a In Rcv
                            doc.Add(a.RcvHeader.DocumentNo)
                        Next
                    End If
                    dt = db.RcvHeader.Where(Function(x) x.FKCompany = CompID And x.Enable = True And frmMain.ow.Contains(x.Owners.Code) And doc.Contains(x.DocumentNo.ToString) And x.ConfirmDate Is Nothing).ToList
                End If
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, c.FKOwner, .เลขที่เอกสาร = c.DocumentNo, .วันที่เอกสาร = Format(c.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RefNo, .วันที่อ้างอิง = Format(c.RefDate, "dd/MM/yyyy HH:mm"), .Owner = c.Owners.Code & "-" & c.Owners.Name, .Vender = c.Vendor.Code & "-" & c.Vendor.Name, .ประเภทรับ = c.RcvType.Code & "-" & c.RcvType.Name, .จำนวน = Format(c.TotalQty, "#,##0.00"), .ราคา = Format(c.TotalAmt, "#,##0.0000"), .หมายเหตุ = c.Description,
                .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView2
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
            DataGridView1.DataSource = Nothing
            btProcess.Enabled = False
            btEditDoc.Enabled = False
            btEditItem.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Sub SelDataStat()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dtRcvLoc = db.RcvLocation.Include("RcvHeader").Include("RcvHeader.Owners").Include("RcvHeader.Vendor").Include("Locations").Include("ProductDetails.Products.Zone").Include("Warehouse").Include("ProductDetails.ProductUnit").Where(Function(x) x.RcvHeader.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing).ToList
                Else
                    dtRcvLoc = db.RcvLocation.Include("RcvHeader").Include("RcvHeader.Owners").Include("RcvHeader.Vendor").Include("Locations").Include("ProductDetails.Products.Zone").Include("Warehouse").Include("ProductDetails.ProductUnit").Where(Function(x) x.RcvHeader.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing And frmMain.ow.Contains(x.RcvHeader.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
                End If
                Dim dtGrid3 = (From c In dtRcvLoc Order By c.Id Select New With {c.Id, .เลขที่เอกสาร = c.RcvHeader.DocumentNo, .วันที่เอกสาร = Format(c.RcvHeader.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RcvHeader.RefNo, .วันที่อ้างอิง = Format(c.RcvHeader.RefDate, "dd/MM/yyyy HH:mm"), .Owner = c.RcvHeader.Owners.Code & "-" & c.RcvHeader.Owners.Name, .คลัง = c.Warehouse.Code & "-" & c.Warehouse.Name, .Vender = c.RcvHeader.Vendor.Code & "-" & c.RcvHeader.Vendor.Name, .ประเภทรับ = c.RcvHeader.RcvType.Code & "-" & c.RcvHeader.RcvType.Name,
                .รหัสสินค้า = c.ProductDetails.Products.Code, .Barcode = c.ProductDetails.Code, .ชื่อสินค้า = c.ProductDetails.Products.Name, .หน่วย = c.ProductDetails.ProductUnit.Name, .บรรจุ = c.ProductDetails.PackSize, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .สถานะ = c.ItemStatus.Code & "-" & c.ItemStatus.Name, .Location = c.Locations.Name, .จำนวน = Format(c.Quantity, "#,##0.00"), .UnitPrice = Format(c.PriceUnit, "#,##0.0000"), .ราคารวม = Format(c.NetPrice, "#,##0.0000"), .โซนเก็บ = c.ProductDetails.Products.Zone.Code & "-" & c.ProductDetails.Products.Zone.Name, .หมายเหตุ = c.RcvHeader.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView3
                    .DataSource = dtGrid3
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
            btConfirm.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Sub SelData2()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                dtRcv = db.RcvDetails.Include("ProductDetails.Products").Include("ProductDetails.ProductUnit").Include("ItemStatus").Include("Warehouse").Include("ProductDetails.Products.Zone").Include("RcvHeader").Where(Function(x) x.Enable = True And x.FKRcvHeader = RcvHdId).ToList
            End Using
            Dim dtGrid = (From c In dtRcv Order By c.Id Select New With {c.Id, .คลัง = c.Warehouse.Code & "-" & c.Warehouse.Name, .โซนเก็บ = c.ProductDetails.Products.Zone.Code & "-" & c.ProductDetails.Products.Zone.Name, .รหัสสินค้า = c.ProductDetails.Products.Code, .Barcode = c.ProductDetails.Code, .ชื่อสินค้า = c.ProductDetails.Products.Name, .หน่วย = c.ProductDetails.ProductUnit.Name, .บรรจุ = Format(c.ProductDetails.PackSize, "#,##0.00"), c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .สถานะ = c.ItemStatus.Code & "-" & c.ItemStatus.Name, .จำนวน = Format(c.Quantity, "#,##0.00"), .UnitPrice = Format(c.PriceUnit, "#,##0.0000"), .ราคารวม = Format(c.NetPrice, "#,##0.0000"),
             c.Location, .พาเลต = IIf(c.PalletNo = 0, "", c.PalletNo), c.FKLocation, c.ProductDetails.Products.FKZone}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns("FKLocation").Visible = False
                .Columns("FKZone").Visible = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
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

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails3_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView3.RowPostPaint
        Using b As New SolidBrush(DataGridView3.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            RcvId = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            btEditItem.Enabled = True
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            RcvHdId = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            OwnerID = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            SelData2()
            btProcess.Enabled = True
            btEditDoc.Enabled = True
            btEditItem.Enabled = False
        End If
    End Sub

    Private Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        SelData()
    End Sub

    Sub SelLoc()
        Using db = New PTGwmsEntities
            LocEmpty = db.V_LocationEmpty.Where(Function(x) x.FKCompany = CompID).ToList
        End Using
    End Sub

    Private Sub btRefresh1_Click(sender As Object, e As EventArgs) Handles btRefresh1.Click
        SelDataStat()
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If MessageBox.Show("ต้องการยืนยันการจัดเก็บ ใช่หรือไม่?", "ยืนยันการจัดเก็บ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim chkID = (From c In db.RcvLocation Where c.Id = RcvLocID And c.ConfirmDate IsNot Nothing).FirstOrDefault
                    If Not IsNothing(chkID) Then
                        Cursor = Cursors.Default
                        MessageBox.Show("รายการนี้ Confirm ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Dim dtPut = (From c In dtRcvLoc Where c.Id = RcvLocID).FirstOrDefault
                    If Not IsNothing(dtPut) Then
                        db.CurrentStocks.Add(New CurrentStocks With {.LotNo = dtPut.LotNo, .FKCompany = dtPut.RcvHeader.FKCompany, .FKWarehouse = dtPut.FKWarehouse, .FKOwner = dtPut.RcvHeader.FKOwner, .FKVendor = dtPut.RcvHeader.FKVendor, .FKLocation = dtPut.FKLocation, .FKProduct = dtPut.ProductDetails.FKProduct, .Qty = dtPut.BaseQty, .BookQty = 0, .PriceUnit = dtPut.PriceUnit, .NetPrice = dtPut.NetPrice, .FKProductUnit = dtPut.ProductDetails.FKProductUnit, .ProductDate = dtPut.ProductDate, .ExpDate = dtPut.ExpDate, .ReceiveDate = dtPut.CreateDate, .FKItemStatus = dtPut.FKItemStatus, .PalletCode = dtPut.PalletCode, .SourceConfirm = "Manual", .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        Dim dts = (From c In db.RcvLocation Where c.Id = RcvLocID).FirstOrDefault
                        If Not IsNothing(dts) Then
                            dts.CheckDate = DateTimeServer()
                            dts.CheckBy = Username
                            dts.ConfirmDate = DateTimeServer()
                            dts.ConfirmBy = Username
                        End If
                    End If
                    db.SaveChanges()
                    SelDataStat()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    Private Sub btReprintLabel_Click(sender As Object, e As EventArgs) Handles btReprintLabel.Click
        Cursor = Cursors.WaitCursor
        frmReprintLabel.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub tbSearch_TextChanged(sender As Object, e As EventArgs) Handles tbSearch.TextChanged
        Try
            Cursor = Cursors.WaitCursor
            Dim dtGrid3 = (From c In dtRcvLoc Order By c.Id Where c.RcvHeader.DocumentNo.Contains(tbSearch.Text) Or c.RcvHeader.DocumentDate.ToString.Contains(tbSearch.Text) Or c.RcvHeader.RefNo.Contains(tbSearch.Text) Or c.RcvHeader.Owners.Code.Contains(tbSearch.Text) Or c.RcvHeader.Owners.Name.Contains(tbSearch.Text) Or c.Warehouse.Code.Contains(tbSearch.Text) Or c.Warehouse.Name.Contains(tbSearch.Text) Or c.ProductDetails.Products.Code.Contains(tbSearch.Text) Or c.ProductDetails.Products.Name.Contains(tbSearch.Text) Or c.ProductDetails.Code.Contains(tbSearch.Text) Select New With {c.Id, .เลขที่เอกสาร = c.RcvHeader.DocumentNo, .วันที่เอกสาร = Format(c.RcvHeader.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RcvHeader.RefNo, .วันที่อ้างอิง = Format(c.RcvHeader.RefDate, "dd/MM/yyyy HH:mm"), .Owner = c.RcvHeader.Owners.Code & "-" & c.RcvHeader.Owners.Name, .คลัง = c.Warehouse.Code & "-" & c.Warehouse.Name, .Vender = c.RcvHeader.Vendor.Code & "-" & c.RcvHeader.Vendor.Name, .ประเภทรับ = c.RcvHeader.RcvType.Code & "-" & c.RcvHeader.RcvType.Name,
            .รหัสสินค้า = c.ProductDetails.Products.Code, .Barcode = c.ProductDetails.Code, .ชื่อสินค้า = c.ProductDetails.Products.Name, .หน่วย = c.ProductDetails.ProductUnit.Name, .บรรจุ = c.ProductDetails.PackSize, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .สถานะ = c.ItemStatus.Code & "-" & c.ItemStatus.Name, .Location = c.Locations.Name, .จำนวน = Format(c.Quantity, "#,##0.00"), .UnitPrice = Format(c.PriceUnit, "#,##0.0000"), .ราคารวม = Format(c.NetPrice, "#,##0.0000"), .โซนเก็บ = c.ProductDetails.Products.Zone.Code & "-" & c.ProductDetails.Products.Zone.Name, .หมายเหตุ = c.RcvHeader.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView3
                .DataSource = dtGrid3
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            btConfirm.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub btEditDoc_Click(sender As Object, e As EventArgs) Handles btEditDoc.Click
        frmEditRcvDoc.ShowDialog()
    End Sub

    Private Sub btEditItem_Click(sender As Object, e As EventArgs) Handles btEditItem.Click
        frmEditRcvItem.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor = Cursors.WaitCursor
        frmPrintRcv.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex <> -1 Then
            RcvLocID = DataGridView3.Rows(e.RowIndex).Cells(0).Value
            btConfirm.Enabled = True
        End If
    End Sub

    Private Sub btProcess_Click(sender As Object, e As EventArgs) Handles btProcess.Click
        If MessageBox.Show("ต้องการยืนยันการรับสินค้า ใช่หรือไม่?", "ยืนยันการรับสินค้า", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Try
                    Dim chkID = (From c In db.RcvHeader Where c.Id = RcvHdId And c.ConfirmDate IsNot Nothing).FirstOrDefault
                    If Not IsNothing(chkID) Then
                        Cursor = Cursors.Default
                        MessageBox.Show("รายการนี้ Process ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
                    db.RcvRunning.Add(New RcvRunning With {.FKCompany = CompID, .CreateDate = DateTimeServer(), .CreateBy = Username})
                    db.SaveChanges()
                    Dim PLNo = (From c In db.RcvRunning Order By c.Id Descending Where c.FKCompany = CompID And c.CreateBy = Username).FirstOrDefault
                    Dim PLID As String = Format(DateTimeServer, "yyMM") & Format(PLNo.Id, "000000")
                    Dim palletNum98 As Integer = 1
                    Dim barNo As Integer = 1
                    Dim Head = (From c In db.V_SumRcvDetail Where c.FKRcvHeader = RcvHdId).ToList
                    SelLoc()
                    Dim Floc = (From c In LocEmpty).ToList
                    Dim Onhand As New List(Of StockOnhand)
                    Dim OnhandAdd As New List(Of StockOnhand)
                    Onhand = db.StockOnhand.Where(Function(x) x.Enable = True And x.FKCompany = CompID And x.FKOwner = OwnerID).ToList
                    DocID = RcvHdId

                    'กรณีระบุ Location
                    Dim dsLoc = (From c In Head Where c.FKLocation IsNot Nothing).ToList
                    If dsLoc.Count > 0 Then
                        For Each L In dsLoc
                            db.RcvLocation.Add(New RcvLocation With {.LotNo = L.LotNo, .BaseQty = L.qty * L.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = 0, .FKRcvHeader = L.FKRcvHeader, .FKWarehouse = L.FKWarehouse, .FKProductDetail = L.FKProductDetail, .FKItemStatus = L.FKItemStatus, .ProductDate = L.ProductDate, .ExpDate = L.ExpDate, .FKLocation = L.FKLocation, .Quantity = L.qty, .PriceUnit = L.PriceUnit, .NetPrice = L.Netprice, .Remark = L.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            Dim LBL = (From c In Floc Where c.Id = L.FKLocation).FirstOrDefault
                            If Not IsNothing(LBL) Then
                                LBL.Book = "Y"
                            End If
                            barNo = barNo + 1
                        Next
                    End If
                    'กรณีไม่ระบุ Location แต่ ระบุ Pallet Number
                    Dim dsPL = (From c In Head Where c.PalletNo > 0).ToList
                    Dim dsDist = (From c In dsPL Select c.FKCompany, c.FKOwner, c.FKRcvHeader, c.FKWarehouse, c.FKItemStatus, c.FKZone, c.PalletNo).Distinct.ToList
                    If dsDist.Count > 0 Then
                        For Each dd In dsDist
                            Dim FlocS = (From c In Floc Order By c.Name Where c.FKCompany = dd.FKCompany And c.FKWarehouse = dd.FKWarehouse And c.FKZone = dd.FKZone And c.InStorage = True And c.Book = "N").FirstOrDefault
                            If Not IsNothing(FlocS) Then
                                'มี Location ว่าง
                                Dim Loc = (From c In dsPL Where c.FKCompany = dd.FKCompany And c.FKOwner = dd.FKOwner And c.FKWarehouse = dd.FKWarehouse And c.FKZone = dd.FKZone And c.FKItemStatus = dd.FKItemStatus And c.PalletNo = dd.PalletNo).ToList
                                If Loc.Count > 0 Then
                                    For Each d In Loc
                                        db.RcvLocation.Add(New RcvLocation With {.LotNo = d.LotNo, .BaseQty = d.qty * d.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = 0, .FKRcvHeader = d.FKRcvHeader, .FKWarehouse = d.FKWarehouse, .FKProductDetail = d.FKProductDetail, .FKItemStatus = d.FKItemStatus, .ProductDate = d.ProductDate, .ExpDate = d.ExpDate, .FKLocation = FlocS.Id, .Quantity = d.qty, .PriceUnit = d.PriceUnit, .NetPrice = d.Netprice, .Remark = d.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    Next
                                End If
                                FlocS.Book = "Y"
                                barNo = barNo + 1
                            Else
                                'กรณีไม่มี Location ว่าง
                                Dim FlocN = (From c In Floc Order By c.Name Where c.FKCompany = dd.FKCompany And c.FKWarehouse = dd.FKWarehouse And c.FKZone = dd.FKZone And c.InStorage = False).FirstOrDefault
                                If Not IsNothing(FlocN) Then
                                    Dim Loc = (From c In dsPL Where c.FKCompany = dd.FKCompany And c.FKOwner = dd.FKOwner And c.FKWarehouse = dd.FKWarehouse And c.FKZone = dd.FKZone And c.FKItemStatus = dd.FKItemStatus And c.PalletNo = dd.PalletNo).ToList
                                    If Loc.Count > 0 Then
                                        For Each d In Loc
                                            db.RcvLocation.Add(New RcvLocation With {.LotNo = d.LotNo, .BaseQty = d.qty * d.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = palletNum98, .FKRcvHeader = d.FKRcvHeader, .FKWarehouse = d.FKWarehouse, .FKProductDetail = d.FKProductDetail, .FKItemStatus = d.FKItemStatus, .ProductDate = d.ProductDate, .ExpDate = d.ExpDate, .FKLocation = FlocN.Id, .Quantity = d.qty, .PriceUnit = d.PriceUnit, .NetPrice = d.Netprice, .Remark = d.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                        Next
                                    End If
                                    palletNum98 = palletNum98 + 1
                                    barNo = barNo + 1
                                Else
                                    Cursor = Cursors.Default
                                    MessageBox.Show("กรุณาสร้าง Location นอกพื้นที่เก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                    'กรณีไม่ระบุ Location และไม่ระบุ Pallet Number
                    Dim dsNLP = (From c In Head Where c.FKLocation Is Nothing And c.PalletNo = 0).ToList
                    If dsNLP.Count > 0 Then
                        For Each n In dsNLP
                            If n.PalletTotal >= n.qty Then
                                'Dim dd1 = (From c In db.ManageZone Where c.FKProduct = n.FKProduct And c.Zone.Warehouse.Id = n.FKWarehouse).FirstOrDefault
                                Dim LP1 = (From c In Floc Order By c.Name Where c.FKCompany = n.FKCompany And c.FKWarehouse = n.FKWarehouse And c.FKZone = n.FKZone And c.InStorage = True And c.Book = "N").FirstOrDefault
                                If Not IsNothing(LP1) Then
                                    'กรณีมี Location ว่าง
                                    db.RcvLocation.Add(New RcvLocation With {.LotNo = n.LotNo, .BaseQty = n.qty * n.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = 0, .FKRcvHeader = n.FKRcvHeader, .FKWarehouse = n.FKWarehouse, .FKProductDetail = n.FKProductDetail, .FKItemStatus = n.FKItemStatus, .ProductDate = n.ProductDate, .ExpDate = n.ExpDate, .FKLocation = LP1.Id, .Quantity = n.qty, .PriceUnit = n.PriceUnit, .NetPrice = n.Netprice, .Remark = n.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    LP1.Book = "Y"
                                    barNo = barNo + 1
                                Else
                                    'กรณีไม่มี Location ว่าง
                                    'Dim dd = (From c In db.ManageZone Where c.FKProduct = n.FKProduct And c.Zone.Warehouse.Id = n.FKWarehouse).FirstOrDefault
                                    Dim LP11 = (From c In Floc Order By c.Name Where c.FKCompany = n.FKCompany And c.FKWarehouse = n.FKWarehouse And c.FKZone = n.FKZone And c.InStorage = False).FirstOrDefault
                                    If Not IsNothing(LP11) Then
                                        db.RcvLocation.Add(New RcvLocation With {.LotNo = n.LotNo, .BaseQty = n.qty * n.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = palletNum98, .FKRcvHeader = n.FKRcvHeader, .FKWarehouse = n.FKWarehouse, .FKProductDetail = n.FKProductDetail, .FKItemStatus = n.FKItemStatus, .ProductDate = n.ProductDate, .ExpDate = n.ExpDate, .FKLocation = LP11.Id, .Quantity = n.qty, .PriceUnit = n.PriceUnit, .NetPrice = n.Netprice, .Remark = n.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                        palletNum98 = palletNum98 + 1
                                        barNo = barNo + 1
                                    Else
                                        Cursor = Cursors.Default
                                        MessageBox.Show("กรุณาสร้าง Location นอกพื้นที่เก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            Else
                                Dim intNum, intQty As Integer
                                intQty = n.qty
                                If (n.qty Mod n.PalletTotal) > 0 Then
                                    intNum = (n.qty \ n.PalletTotal) + 1
                                Else
                                    intNum = (n.qty \ n.PalletTotal)
                                End If
                                For i = 1 To intNum
                                    If n.PalletTotal >= intQty Then
                                        'Dim dd = (From c In db.ManageZone Where c.FKProduct = n.FKProduct And c.Zone.Warehouse.Id = n.FKWarehouse).FirstOrDefault
                                        Dim LP2 = (From c In Floc Order By c.Name Where c.FKCompany = n.FKCompany And c.FKWarehouse = n.FKWarehouse And c.FKZone = n.FKZone And c.InStorage = True And c.Book = "N").FirstOrDefault
                                        If Not IsNothing(LP2) Then
                                            'กรณีมี Location ว่าง
                                            db.RcvLocation.Add(New RcvLocation With {.LotNo = n.LotNo, .BaseQty = intQty * n.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = 0, .FKRcvHeader = n.FKRcvHeader, .FKWarehouse = n.FKWarehouse, .FKProductDetail = n.FKProductDetail, .FKItemStatus = n.FKItemStatus, .ProductDate = n.ProductDate, .ExpDate = n.ExpDate, .FKLocation = LP2.Id, .Quantity = CDbl(intQty), .PriceUnit = n.PriceUnit, .NetPrice = CDbl(intQty * n.PriceUnit), .Remark = n.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                            LP2.Book = "Y"
                                            barNo = barNo + 1
                                        Else
                                            'กรณีไม่มี Location ว่าง
                                            'Dim dd1 = (From c In db.ManageZone Where c.FKProduct = n.FKProduct And c.Zone.Warehouse.Id = n.FKWarehouse).FirstOrDefault
                                            Dim LP21 = (From c In Floc Order By c.Name Where c.FKCompany = n.FKCompany And c.FKWarehouse = n.FKWarehouse And c.FKZone = n.FKZone And c.InStorage = False).FirstOrDefault
                                            If Not IsNothing(LP21) Then
                                                db.RcvLocation.Add(New RcvLocation With {.LotNo = n.LotNo, .BaseQty = intQty * n.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = palletNum98, .FKRcvHeader = n.FKRcvHeader, .FKWarehouse = n.FKWarehouse, .FKProductDetail = n.FKProductDetail, .FKItemStatus = n.FKItemStatus, .ProductDate = n.ProductDate, .ExpDate = n.ExpDate, .FKLocation = LP21.Id, .Quantity = CDbl(intQty), .PriceUnit = n.PriceUnit, .NetPrice = CDbl(intQty * n.PriceUnit), .Remark = n.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                                palletNum98 = palletNum98 + 1
                                                barNo = barNo + 1
                                            Else
                                                Cursor = Cursors.Default
                                                MessageBox.Show("กรุณาสร้าง Location นอกพื้นที่เก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If
                                        End If
                                    Else
                                        'Dim dd = (From c In db.ManageZone Where c.FKProduct = n.FKProduct And c.Zone.Warehouse.Id = n.FKWarehouse).FirstOrDefault
                                        Dim LP3 = (From c In Floc Order By c.Name Where c.FKCompany = n.FKCompany And c.FKWarehouse = n.FKWarehouse And c.FKZone = n.FKZone And c.InStorage = True And c.Book = "N").FirstOrDefault
                                        If Not IsNothing(LP3) Then
                                            'กรณีมี Location ว่าง
                                            db.RcvLocation.Add(New RcvLocation With {.LotNo = n.LotNo, .BaseQty = n.PalletTotal * n.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = 0, .FKRcvHeader = n.FKRcvHeader, .FKWarehouse = n.FKWarehouse, .FKProductDetail = n.FKProductDetail, .FKItemStatus = n.FKItemStatus, .ProductDate = n.ProductDate, .ExpDate = n.ExpDate, .FKLocation = LP3.Id, .Quantity = n.PalletTotal, .PriceUnit = n.PriceUnit, .NetPrice = CDbl(n.PalletTotal * n.PriceUnit), .Remark = n.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                            LP3.Book = "Y"
                                            barNo = barNo + 1
                                        Else
                                            'กรณีไม่มี Location ว่าง
                                            'Dim dd1 = (From c In db.ManageZone Where c.FKProduct = n.FKProduct And c.Zone.Warehouse.Id = n.FKWarehouse).FirstOrDefault
                                            Dim LP31 = (From c In Floc Order By c.Name Where c.FKCompany = n.FKCompany And c.FKWarehouse = n.FKWarehouse And c.FKZone = n.FKZone And c.InStorage = False).FirstOrDefault
                                            If Not IsNothing(LP31) Then
                                                db.RcvLocation.Add(New RcvLocation With {.LotNo = n.LotNo, .BaseQty = n.PalletTotal * n.PackSize, .PalletCode = PLID & Format(barNo, "000"), .PalletNo = palletNum98, .FKRcvHeader = n.FKRcvHeader, .FKWarehouse = n.FKWarehouse, .FKProductDetail = n.FKProductDetail, .FKItemStatus = n.FKItemStatus, .ProductDate = n.ProductDate, .ExpDate = n.ExpDate, .FKLocation = LP31.Id, .Quantity = n.PalletTotal, .PriceUnit = n.PriceUnit, .NetPrice = CDbl(n.PalletTotal * n.PriceUnit), .Remark = n.Remark, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                                palletNum98 = palletNum98 + 1
                                                barNo = barNo + 1
                                            Else
                                                Cursor = Cursors.Default
                                                MessageBox.Show("กรุณาสร้าง Location นอกพื้นที่เก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                Exit Sub
                                            End If
                                        End If
                                        intQty = intQty - n.PalletTotal
                                    End If
                                Next
                            End If
                        Next
                    End If

                    Dim stc = (From c In db.RcvDetails Where c.FKRcvHeader = RcvHdId Group c By
                                   c.RcvHeader.FKCompany,
                                   c.RcvHeader.DocumentNo,
                                   c.RcvHeader.DocumentDate,
                                   c.RcvHeader.RefNo,
                                   c.RcvHeader.ReceiveDate,
                                   c.RcvHeader.RcvType.Name,
                                   c.FKWarehouse,
                                   c.RcvHeader.FKOwner,
                                   c.ProductDetails.FKProduct,
                                   c.FKItemStatus Into qty = Sum(c.Quantity * c.ProductDetails.PackSize), netprice = Sum(c.NetPrice)).ToList
                    If stc.Count > 0 Then
                        For Each s In stc
                            Dim onh = (From c In db.StockOnhand Where c.FKCompany = s.FKCompany And c.FKWarehouse = s.FKWarehouse And c.FKOwner = s.FKOwner And c.FKItemStatus = s.FKItemStatus And c.FKProduct = s.FKProduct).FirstOrDefault
                            If Not IsNothing(onh) Then
                                db.StockCard.Add(New StockCard With {
                                 .TransactionDate = s.ReceiveDate,
                                 .DocNo = s.DocumentNo,
                                 .DocDate = s.DocumentDate,
                                 .Reference = s.RefNo,
                                 .Description = s.Name,
                                 .FKCompany = s.FKCompany,
                                 .FKWarehouse = s.FKWarehouse,
                                 .FKOwner = s.FKOwner,
                                 .FKProduct = s.FKProduct,
                                 .FKItemStatus = s.FKItemStatus,
                                 .InQty = s.qty,
                                 .InCost = s.netprice / s.qty,
                                 .InValue = s.netprice,
                                 .BalQty = s.qty + onh.Qty,
                                 .BalCost = (s.netprice + onh.NetPrice) / (s.qty + onh.Qty),
                                 .BalValue = s.netprice + onh.NetPrice,
                                 .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                onh.QtyCost = (s.netprice + onh.NetPrice) / (s.qty + onh.Qty)
                                onh.NetPrice = s.netprice + onh.NetPrice
                            Else
                                db.StockCard.Add(New StockCard With {
                                 .TransactionDate = s.ReceiveDate,
                                 .DocNo = s.DocumentNo,
                                 .DocDate = s.DocumentDate,
                                 .Reference = s.RefNo,
                                 .Description = s.Name,
                                 .FKCompany = s.FKCompany,
                                 .FKWarehouse = s.FKWarehouse,
                                 .FKOwner = s.FKOwner,
                                 .FKProduct = s.FKProduct,
                                 .FKItemStatus = s.FKItemStatus,
                                 .InQty = s.qty,
                                 .InCost = s.netprice / s.qty,
                                 .InValue = s.netprice,
                                 .BalQty = s.qty,
                                 .BalCost = s.netprice / s.qty,
                                 .BalValue = s.netprice,
                                 .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            End If
                        Next
                    End If
                    Dim dtSum = (From c In Head Group c By c.FKCompany, c.FKWarehouse, c.FKOwner, c.FKProduct, c.FKItemStatus Into qty = Sum(c.qty * c.PackSize), netprice = Sum(c.Netprice)).ToList
                    If dtSum.Count > 0 Then
                        For Each a In dtSum
                            Dim Oh = (From c In Onhand Where c.FKCompany = a.FKCompany And c.FKWarehouse = a.FKWarehouse And c.FKOwner = a.FKOwner And c.FKProduct = a.FKProduct And c.FKItemStatus = a.FKItemStatus).FirstOrDefault
                            If Not IsNothing(Oh) Then
                                Oh.Qty = Oh.Qty + a.qty
                                Oh.UpdateBy = Username
                                Oh.UpdateDate = DateTimeServer()
                                db.Entry(Oh).State = EntityState.Modified
                            Else
                                OnhandAdd.Add(New StockOnhand With {
                                              .FKCompany = a.FKCompany,
                                              .FKWarehouse = a.FKWarehouse,
                                              .FKOwner = a.FKOwner,
                                              .FKProduct = a.FKProduct,
                                              .FKItemStatus = a.FKItemStatus,
                                              .Qty = a.qty,
                                              .QtyCost = a.netprice / a.qty,
                                              .NetPrice = a.netprice,
                                              .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            End If
                        Next
                    End If
                    Dim rcvU = (From c In db.RcvHeader Where c.Id = RcvHdId).FirstOrDefault
                    If Not IsNothing(rcvU) Then
                        rcvU.ConfirmDate = DateTimeServer()
                        rcvU.ConfirmBy = Username
                    End If
                    db.StockOnhand.AddRange(OnhandAdd)
                    db.SaveChanges()
                    Dim UdRcv = (From c In db.RcvHeader Where c.Id = RcvHdId).FirstOrDefault 'Interface To CDB
                    If Not IsNothing(UdRcv) Then
                        Dim rcvDTL = (From c In dtRcv Where c.FKRcvHeader = RcvHdId).ToList
                        If UdRcv.SrcName = "SAP" And UdRcv.TargetType = Nothing Then 'การรับน้ำมันเครื่อง Mvt.101 I-MM036 Ref. PO
                            For Each a In rcvDTL
                                execSQL("Declare @NextSeq As Int
                                     Select @NextSeq = NextVal 
                                     From OpenQuery(" & DBLink & ", 'SELECT STAG.IF_MM011_GOODSMVT_CREATE_SEQ.NEXTVAL FROM DUAL')
                                     INSERT INTO " & DBLink & "..STAG.IF_MM011_GOODSMVT_CREATE (IF_ID,SRC_NAME,TARGET_NAME,PSTNG_DATE,DOC_DATE,REF_DOC_NO,HEADER_TXT,MATERIAL,PLANT,MOVE_TYPE,ENTRY_QNT,ENTRY_UOM,PO_NUMBER,PO_ITEM,ITEM_TEXT,IS_DELETED,CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE,MVT_IND,GM_CODE,DOCUMENT_NO,BILL_OF_LADING,ZITEM,VER_GR_GI_SLIP,VER_GR_GI_SLIPX,STGE_LOC,GR_RCPT) 
                                     VALUES (@NextSeq,'WMS','SAP','" & Format(a.RcvHeader.ReceiveDate, "dd.MM.yyyy") & "','" & Format(a.RcvHeader.DocumentDate, "dd.MM.yyyy") & "','" & Microsoft.VisualBasic.Left(a.RcvHeader.RefNo, 16) & "','" & Microsoft.VisualBasic.Left(a.RcvHeader.Description, 25) & "','" & Microsoft.VisualBasic.Left(a.ProductDetails.Products.Code, 18) & "','" & Microsoft.VisualBasic.Left(a.Warehouse.Code, 4) & "','101'," & CDbl(a.Quantity) & ",'" & Microsoft.VisualBasic.Left(a.ProductDetails.ProductUnit.Code, 3) & "','" & Microsoft.VisualBasic.Left(a.RcvHeader.RefNo, 10) & "','" & Microsoft.VisualBasic.Left(a.POITEM, 5) & "','" & Microsoft.VisualBasic.Left(a.Remark, 50) & "','N',getdate(),'" & Username & "',getdate(),'N',convert(datetime,'99991231'),'B','01','" & Microsoft.VisualBasic.Left(a.RcvHeader.DocumentNo, 50) & "','" & Microsoft.VisualBasic.Left(a.RcvHeader.DocumentNo, 16) & "','" & Microsoft.VisualBasic.Left(a.POITEM, 5) & "','3','X','4001','" & Username & "');")
                            Next
                        ElseIf UdRcv.SrcName = "SAP" And UdRcv.TargetType = "SD006" Then
                            For Each a In rcvDTL
                                execSQL("Declare @NextSeq As Int
                                     Select @NextSeq = NextVal 
                                     From OpenQuery(" & DBLink & ", 'SELECT STAG.IF_SD006_DELIVERY_UPDATE_P_SEQ.NEXTVAL FROM DUAL')
                                     INSERT INTO " & DBLink & "..STAG.IF_SD006_DELIVERY_UPDATE_PICK (IF_ID,SRC_NAME,TARGET_NAME,DO_DOC,DO_DATE,MATERIAL,BASE_QTY,BASE_UNIT,IS_DELETED,CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE,ITEM_CATE) 
                                     VALUES (@NextSeq,'WMS','SAP','" & a.RcvHeader.RefNo & "','" & Format(a.RcvHeader.RefDate, "yyyyMMdd") & "','" & a.ProductDetails.Products.Code & "'," & CDbl(a.Quantity) & ",'" & a.ProductDetails.ProductUnit.Code & "','N',getdate(),'" & Username & "',getdate(),'N',convert(datetime,'99991231'),'" & a.ITEMCATE & "');")
                            Next
                        ElseIf UdRcv.SrcName = "VRM" Or UdRcv.SrcName = "PUN" Or UdRcv.SrcName = "MMS" Then 'Interface ทำรับธรรมดา VRM >> Mvt.101 I-MM011
                            If UdRcv.RcvType.Code = "15" Then 'กรณีรับสินค้าคืน
                                Dim i As Integer = 1
                                execSQL("Declare @NextSeq As Int
                                    Select @NextSeq = NextVal 
                                    From OpenQuery(" & DBLink & ", 'SELECT STAG.TXLL_WMS_LL008_GR_DOC_HD_SEQ.NEXTVAL FROM DUAL')
                                    INSERT INTO " & DBLink & "..STAG.TXLL_WMS_LL008_GR_DOC_OUT_HEAD (TXLL_ID,SRC_NAME,TARGET_NAME,SHOP_ID,DOCUMENT_NO,DOCUMENT_TRANSACTION_TYPE,
                                    PO_STATUS,RECEIVE_DATE,DOCUMENT_DATE,VAT_TYPE,REF_DOC,PROCESS_STATUS,RECORD_STATUS,TRANSFER_FROM_SHOP_ID,TRANSFER_TO_SHOP_ID,IS_DELETED,
                                    CREATED_BY,CREATED_DT,UPDATED_BY,UPDATED_DT,APPROVED_BY,APPROVED_DT,IS_APPROVED,IS_CANCEL_APPROVED,IS_SUBMIT,
                                    IS_CANCEL_SUBMIT,IS_PRINT,IS_CANCEL_DOCUMENT,IS_TYPE_REQUEST,IS_ADJUST_RECEIVED,INF_STATUS) 
                                    VALUES (@NextSeq,'WMS','PUN','DP02','" & UdRcv.DocumentNo & "','05','02',convert(datetime,'" & Format(UdRcv.ReceiveDate, "yyyyMMdd") & "'),convert(datetime,'" & Format(UdRcv.DocumentDate, "yyyyMMdd") & "'),'00','" & UdRcv.RefNo & "','02','Y','" & UdRcv.TargetType & "','DP02',
                                    'N','WMS',getdate(),'WMS',getdate(),'WMS',getdate(),'N','N','N','N','Y','N','00','Y','N');")
                                For Each a In rcvDTL
                                    execSQL("Declare @NextSeq As Int
                                        Select @NextSeq = NextVal 
                                        From OpenQuery(" & DBLink & ", 'SELECT STAG.TXLL_WMS_LL008_GR_DOC_DT_SEQ.NEXTVAL FROM DUAL')
                                        INSERT INTO " & DBLink & "..STAG.TXLL_WMS_LL008_GR_DOC_OUT_DT (TXLL_ID,NO,DOCUMENT_NO,MATERIAL_ID,MATERIAL_UNIT_ID,QTY,PRICE,VAT_TYPE,
                                        TOTAL_AMOUNT,PROCESS_STATUS,RECORD_STATUS,TRANSFER_QTY,TRANSFER_APPROVED_QTY,IS_DELETE,CREATED_BY,CREATED_DT,UPDATED_BY,UPDATED_DT,
                                        MATERIAL_STOCK,RECEIPT_DAMAGED_QTY,CURRENT_COST,ACTUAL_APPROVED_QTY) 
                                        VALUES (@NextSeq,'" & i & "','" & a.RcvHeader.DocumentNo & "','" & a.ProductDetails.Products.Code & "','" & a.ProductDetails.ProductUnit.Code & "'," & a.Quantity & "," & a.PriceUnit & ",'00'," & a.NetPrice & ",'02','Y'," & a.Quantity & "," & a.Quantity & ",'N','WMS',getdate(),'WMS',getdate(),0,0,0," & a.Quantity & ");")
                                    i = i + 1
                                Next
                            Else
                                For Each a In rcvDTL
                                    execSQL("Declare @NextSeq As Int
                                     Select @NextSeq = NextVal 
                                     From OpenQuery(" & DBLink & ", 'SELECT STAG.TXLL_VRM_MM011_PO_RECEIPT_SEQ.NEXTVAL FROM DUAL')
                                     INSERT INTO " & DBLink & "..STAG.TXLL_VRM_MM011_PO_RECEIPT (TXLL_ID,SRC_NAME,TARGET_NAME,PSTNG_DATE,DOC_DATE,REF_DOC_NO,DOCUMENT_NO,BILL_OF_LADING,HEADER_TXT,VER_GR_GI_SLIP,VER_GR_GI_SLIPX,GM_CODE,MVT_IND,ITEM,MATERIAL,PLANT,MOVE_TYPE,ENTRY_QNT,ENTRY_UOM,PO_NUMBER,PO_ITEM,ITEM_TEXT,IS_DELETED,CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE,GR_RCPT) 
                                     VALUES (@NextSeq,'WMS','SAP','" & Format(a.RcvHeader.ReceiveDate, "dd.MM.yyyy") & "','" & Format(a.RcvHeader.DocumentDate, "dd.MM.yyyy") & "','" & a.RcvHeader.RefNo & "','" & a.RcvHeader.DocumentNo & "','" & Microsoft.VisualBasic.Right(a.RcvHeader.DocumentNo, 16) & "','" & Microsoft.VisualBasic.Left(a.RcvHeader.Description, 25) & "','3','X','01','B','" & a.POITEM & "','" & a.ProductDetails.Products.Code & "','" & Microsoft.VisualBasic.Left(a.Warehouse.Code, 4) & "','101'," & CDbl(a.Quantity) & ",'" & a.ProductDetails.ProductUnit.Code & "','" & a.RcvHeader.PONumber & "','" & a.POITEM & "','" & Microsoft.VisualBasic.Left(a.ProductDetails.Products.Name, 50) & "','N',getdate(),'" & Username & "',getdate(),'N',convert(datetime,'99991231'),'" & Username & "');")
                                Next
                            End If
                        End If
                    End If
                    Cursor = Cursors.Default
                    MessageBox.Show("บันทึกการรับสินค้าเรียบร้อย", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    btRefresh_Click(sender, e)
                    frmPrintRcvLabel.Show()
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            'With DataGridView1
            '    If .Rows.Count = 0 Then Exit Sub
            '    If MessageBox.Show("ต้องการยกเลิกรายการนี้ ใช่หรือไม่?", "ยืนยันการยกเลิก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            '        Using db = New PTGwmsEntities
            '            Try
            '                Dim ds = (From c In db.RcvDetails Where c.Id = RcvId).FirstOrDefault
            '                If Not IsNothing(ds) Then
            '                    ds.UpdateDate = DateTimeServer
            '                    ds.UpdateBy = Username
            '                    ds.Enable = False
            '                End If
            '                Dim ds1 = (From c In dtRcv Where c.Id = RcvId).FirstOrDefault
            '                If Not IsNothing(ds1) Then
            '                    ds1.Enable = False
            '                End If
            '                db.SaveChanges()
            '            Catch ex As Exception
            '                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '                Exit Sub
            '            End Try
            '        End Using
            '        SelData2()
            '    End If
            'End With
        End If
    End Sub

End Class