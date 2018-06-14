Imports System.Data.Entity
Imports System.Globalization
Imports System.Threading
Imports Microsoft.Office.Interop

Public Class frmInterfaceRcv

    Public dt As New List(Of RcvTempHeader)
    Public dtRcv As New List(Of RcvTempDetail)
    Dim docNo As String
    Dim hdID, DtlID As Integer
    Dim OldQty As Double

    Private Sub frmInterfaceRcv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        tbDocNo.Text = "RCV" & Format(DateTimeServer(), "yyMMddHHmm")
        btConfirm.Enabled = False
        btEditRcv.Enabled = False
        btLoadRTV.Enabled = False
    End Sub

    Private Sub btLoadData_Click(sender As Object, e As EventArgs) Handles btLoadData.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Dim ds = selSQL("select txll_id from [" & DBLink & "]..[STAG].TXLL_VRM_MM010_PO_ITEM where txll_id > (select max(txll_id) from wms.RcvTempDetail where SRC_NAME in ('MMS','PUN','VRM')) and inf_status = 'S' and wms_flag = 'Y' and material is not null")
                If ds.Tables(0).Rows.Count > 0 Then
                    execSQL("exec [wms].[LOAD_PO_MM010]")
                    Cursor = Cursors.Default
                    MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    SelData()
                Else
                    Cursor = Cursors.Default
                    MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid = (From c In dt Order By c.Id Where c.COMP_CODE.Contains(tbSearch.Text) Or c.VENDOR.Contains(tbSearch.Text) Or c.DOCUMENT_NO.Contains(tbSearch.Text) Or c.DOC_DATE.Contains(tbSearch.Text) Or c.PO_NUMBER.Contains(tbSearch.Text) Select New With {c.Id, .บริษัท = c.COMP_CODE, .Vender = c.VENDOR, .เลขที่อ้างอิงเอกสาร = c.DOCUMENT_NO, .วันที่ใบสั่งซื้อ = c.DOC_DATE, .เลขที่ใบสั่งซื้อ = c.PO_NUMBER, .สถานะ = c.DELETE_IND, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                dtRcv = db.RcvTempDetail.Where(Function(x) x.Enable = True And frmMain.wh.Contains(x.PLANT.ToString)).ToList
                Dim doc As New List(Of String)
                If dtRcv.Count > 0 Then
                    For Each a In dtRcv
                        doc.Add(a.DOCUMENT_NO)
                    Next
                End If
                dt = db.RcvTempHeader.Where(Function(x) x.Enable = True And frmMain.ow.Contains(x.COMP_CODE) And doc.Contains(x.DOCUMENT_NO.ToString) And x.ConfirmDate Is Nothing).ToList
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, .บริษัท = c.COMP_CODE, .Vender = c.VENDOR, .เลขที่อ้างอิงเอกสาร = c.DOCUMENT_NO, .วันที่ใบสั่งซื้อ = c.DOC_DATE, .เลขที่ใบสั่งซื้อ = c.PO_NUMBER, .สถานะ = c.DELETE_IND, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView2
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End Using
            DataGridView1.DataSource = Nothing
            btConfirm.Enabled = False
            btEditRcv.Enabled = False
            tbQty.ReadOnly = True
            tbDocNo.Text = "RCV" & Format(DateTimeServer(), "yyMMddHHmm")
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Sub SelData2()
        Try
            Dim dtGrid = (From c In dtRcv Order By c.Id Where c.DOCUMENT_NO = docNo Select New With {c.Id, .POITEM = c.PO_ITEM, .รหัสสินค้า = c.MATERIAL, .ชื่อสินค้า = c.MATERIAL_TEXT, .คลัง = c.PLANT, .จำนวน = c.QUANTITY, .ยืนยันจำนวน = c.QTY_CONFIRM, .ยืนยันแล้ว = c.QTY_CONFIRMED, .คงเหลือ = c.QUANTITY - c.QTY_CONFIRMED, .หน่วย = c.PO_UNIT, .ราคารวม = c.NET_PRICE, .ราคาต่อหน่วย = c.PRICE_UNIT, .วันที่ส่งของ = c.DELIVERY_DATE3, .สถานะ = c.DELETE_IND, c.NO_MORE_GR}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            tbQty.ReadOnly = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            hdID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            docNo = DataGridView2.Rows(e.RowIndex).Cells(3).Value
            SelData2()
            btConfirm.Enabled = True
            btEditRcv.Enabled = False
            tbDocNo.Focus()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            DtlID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            OldQty = DataGridView1.Rows(e.RowIndex).Cells(5).Value - DataGridView1.Rows(e.RowIndex).Cells(7).Value
            tbQty.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
            tbQty.ReadOnly = False
            btEditRcv.Enabled = True
            tbQty.Focus()
        End If
    End Sub

    Private Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        SelData()
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

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If MessageBox.Show("ต้องการ Confirm Data ใช่หรือไม่?", "ยืนยันการ Confirm Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            If tbDocNo.Text = "" Then
                MessageBox.Show("กรุณาป้อน เลขที่เอกสาร", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbDocNo.Focus()
                Exit Sub
            End If
            Try
                Dim ds = (From c In dt Where c.Id = hdID).FirstOrDefault
                If Not IsNothing(ds) Then
                    Cursor = Cursors.WaitCursor
                    If ds.DELETE_IND = "L" Then
                        Cursor = Cursors.Default
                        MessageBox.Show("รายการนี้ยกเลิกไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Using db = New PTGwmsEntities
                        Dim chkDoc = (From c In db.RcvHeader Where c.DocumentNo = tbDocNo.Text).FirstOrDefault
                        If Not IsNothing(chkDoc) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("ป้อนเลขที่เอกสารซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbDocNo.Focus()
                            Exit Sub
                        End If
                        Dim chkID = (From c In db.RcvTempHeader Where c.Id = hdID And c.ConfirmDate IsNot Nothing).FirstOrDefault
                        If Not IsNothing(chkID) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("รายการนี้ Confirm ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Dim compCode = (From c In db.Owners Where c.FKCompany = CompID And c.Code = ds.COMP_CODE Select c.Id, c.Code).FirstOrDefault
                        If IsNothing(compCode) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล Owner : " & ds.COMP_CODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Dim VenCode = (From c In db.Vendor Where c.FKCompany = CompID And c.Code = ds.VENDOR Select c.Id, c.Code).FirstOrDefault
                        If IsNothing(VenCode) Then
                            If ds.DOC_TYPE <> "RPUN" Then
                                Cursor = Cursors.Default
                                MessageBox.Show("ไม่พบข้อมูล Vendor : " & ds.VENDOR, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End If
                        Dim rType, rVen As Integer
                        Dim tgType As String = ds.TargetType
                        Dim dtWh = (From c In db.Warehouse Where c.FKCompany = CompID).ToList
                        Dim dtProd = (From c In db.Products Where c.FKCompany = CompID And c.FKOwner = compCode.Id).ToList
                        Dim dtUnit = (From c In db.ProductUnit Where c.FKCompany = CompID).ToList
                        Dim docDate As String = ds.DOC_DATE
                        Dim _date As DateTime = DateTime.ParseExact(docDate, "yyyyMMdd", CultureInfo.InvariantCulture)
                        If ds.DOC_TYPE = "RPUN" Then
                            Dim rt = (From c In db.RcvType Where c.FKCompany = CompID And c.Code = "15").FirstOrDefault 'รับสินค้าคืน
                            If Not IsNothing(rt) Then
                                rType = rt.Id
                            End If
                            Dim vt = (From c In db.Vendor Where c.FKCompany = CompID And c.Code = "ZZZZZZZZ").FirstOrDefault
                            If Not IsNothing(vt) Then
                                rVen = vt.Id
                            End If
                            tgType = ds.VENDOR
                        Else
                            Dim rt = (From c In db.RcvType Where c.FKCompany = CompID And c.Code = "01").FirstOrDefault 'รับสินค้าทั่วไป
                            If Not IsNothing(rt) Then
                                rType = rt.Id
                            End If
                            rVen = VenCode.Id
                        End If
                        Dim RcvHeader = New RcvHeader With {.ReceiveDate = DateTimeServer(), .DocumentNo = tbDocNo.Text, .DocumentDate = dtDocdate.Value, .FKCompany = CompID, .FKRcvType = rType, .FKVendor = rVen, .FKOwner = compCode.Id, .PONumber = ds.PO_NUMBER, .RefNo = ds.DOCUMENT_NO, .RefDate = _date, .Description = tbDesc.Text, .SrcName = ds.SRC_NAME, .TargetName = ds.TARGET_NAME, .TargetType = tgType, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                        Dim dsDTL = (From c In dtRcv Where c.DOCUMENT_NO = ds.DOCUMENT_NO).ToList
                        If dsDTL.Count > 0 Then
                            For Each L In dsDTL
                                If L.QTY_CONFIRM > 0 Then
                                    Dim WhCode = (From c In dtWh Where c.Code = L.PLANT Select c.Id, c.Code).FirstOrDefault
                                    If IsNothing(WhCode) Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("ไม่พบข้อมูล Warehouse : " & L.PLANT, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                    Dim itm = (From c In db.ItemStatus Where c.FKCompany = CompID And c.Code = "01" Select c.Id, c.Code).FirstOrDefault
                                    If IsNothing(itm) Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("ไม่พบข้อมูลสถานะสินค้า (01)", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                    Dim PdHD = (From c In dtProd Where c.Zone.Warehouse.Code = L.PLANT And c.Code = L.MATERIAL Select c.Id, c.Code, c.ShelfLife).FirstOrDefault
                                    If IsNothing(PdHD) Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("ไม่พบข้อมูล Products :  " & L.MATERIAL, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                    Dim UnID = (From c In dtUnit Where c.Code = L.PO_UNIT Select c.Id, c.Code).FirstOrDefault
                                    If IsNothing(UnID) Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("ไม่พบข้อมูล PO Unit : " & L.PO_UNIT & " (" & L.MATERIAL & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                    Dim PdDTL = (From c In db.ProductDetails Where c.FKProduct = PdHD.Id And c.FKProductUnit = UnID.Id Select c.Id, c.Code).FirstOrDefault
                                    If IsNothing(PdDTL) Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("ไม่พบข้อมูล หน่วยสินค้า : " & L.PO_UNIT & " (" & L.MATERIAL & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                    Dim prc As Double
                                    prc = (CDbl(L.NET_PRICE) / CDbl(L.QUANTITY - L.QTY_CONFIRMED)) * L.QTY_CONFIRM
                                    RcvHeader.RcvDetails.Add(New RcvDetails With {.LotNo = "-", .FKWarehouse = WhCode.Id, .FKProductDetail = PdDTL.Id, .POITEM = L.PO_ITEM, .FKItemStatus = itm.Id, .Quantity = CDbl(L.QTY_CONFIRM), .PriceUnit = CDbl(L.PRICE_UNIT), .NetPrice = prc, .ProductDate = DateTimeServer(), .ExpDate = (DateAdd(DateInterval.Day, PdHD.ShelfLife, DateTimeServer())), .PalletNo = 0, .Remark = "", .ITEMCATE = L.ITEMCATE, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    Dim q = (From c In db.RcvTempDetail Where c.Id = L.Id).FirstOrDefault
                                    If Not IsNothing(q) Then
                                        q.QTY_CONFIRMED = q.QTY_CONFIRMED + L.QTY_CONFIRM
                                        q.QTY_CONFIRM = q.QUANTITY - (L.QTY_CONFIRM + L.QTY_CONFIRMED)
                                        q.NET_PRICE = q.NET_PRICE - prc
                                        'q.NET_CONFIRM = q.NET_CONFIRM + prc
                                        q.UpdateDate = DateTimeServer()
                                        q.UpdateBy = Username
                                    End If
                                End If
                            Next
                        End If
                        'Clear Header----------------------
                        'Dim ck As Boolean = False
                        'For Each dr As DataGridViewRow In DataGridView1.Rows
                        '    If dr.Cells(5).Value <> (dr.Cells(6).Value + dr.Cells(7).Value) Then
                        '        ck = True
                        '    End If
                        'Next
                        'If ck = False Then
                        '    Dim U = (From c In db.RcvTempHeader Where c.Id = hdID).FirstOrDefault
                        '    If Not IsNothing(U) Then
                        '        U.ConfirmDate = DateTimeServer()
                        '        U.ConfirmBy = Username
                        '        U.CompleteDate = DateTimeServer()
                        '        U.CompleteBy = Username
                        '    End If
                        'End If
                        'End Clear Header----------------------
                        db.RcvHeader.Add(RcvHeader)
                        db.SaveChanges()
                        'Clear Header----------------------
                        Dim chk = (From c In db.RcvTempDetail Where c.DOCUMENT_NO = ds.DOCUMENT_NO And c.QUANTITY <> c.QTY_CONFIRMED).ToList
                        If chk.Count = 0 Then
                            Dim U = (From c In db.RcvTempHeader Where c.Id = hdID).FirstOrDefault
                            If Not IsNothing(U) Then
                                U.ConfirmDate = DateTimeServer()
                                U.ConfirmBy = Username
                                U.CompleteDate = DateTimeServer()
                                U.CompleteBy = Username
                                db.SaveChanges()
                            End If
                        End If
                        'End Clear Header----------------------
                    End Using
                    tbDocNo.Clear()
                    dtDocdate.Value = DateTimeServer()
                    tbDesc.Clear()
                    Cursor = Cursors.Default
                    MessageBox.Show("Confirm Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    SelData()
                Else
                    Cursor = Cursors.Default
                    MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btLoadDatLube_Click(sender As Object, e As EventArgs) Handles btLoadDatLube.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Dim ds = selSQL("select count(*) cnt from " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM  where target_name = 'WMS'  and inf_status = 'N' and PO_NUMBER in (select PO_NUMBER from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and inf_status = 'N' )")
                If ds.Tables(0).Rows(0).Item("cnt") > 0 Then
                    Using db = New PTGwmsEntities
                        'Step 1 Insert New Data Header
                        Dim hd = selSQL("select * from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind is null and inf_status = 'N'")
                        If hd.Tables(0).Rows.Count > 0 Then
                            Dim i As Integer
                            For i = 0 To hd.Tables(0).Rows.Count - 1
                                db.RcvTempHeader.Add(New RcvTempHeader With {
                                .TXLL_ID = hd.Tables(0).Rows(i).Item("IF_ID"),
                                .SRC_NAME = hd.Tables(0).Rows(i).Item("SRC_NAME"),
                                .TARGET_NAME = hd.Tables(0).Rows(i).Item("TARGET_NAME"),
                                .PO_NUMBER = hd.Tables(0).Rows(i).Item("PO_NUMBER"),
                                .COMP_CODE = hd.Tables(0).Rows(i).Item("COMP_CODE"),
                                .DOC_TYPE = hd.Tables(0).Rows(i).Item("DOC_TYPE"),
                                .VENDOR = hd.Tables(0).Rows(i).Item("VENDOR"),
                                .DOCUMENT_NO = hd.Tables(0).Rows(i).Item("PO_NUMBER"),
                                .DOC_DATE = hd.Tables(0).Rows(i).Item("DOC_DATE"),
                                .CreateDate = DateTimeServer(), .CreateBy = "INF",
                                .UpdateDate = DateTimeServer(), .UpdateBy = "INF", .Enable = True})
                            Next
                        End If
                        'Step 1 Insert New Data Detail
                        Dim dtl = selSQL("select *  from " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM where target_name = 'WMS' and delete_ind is null and inf_status = 'N' and PO_NUMBER in (select PO_NUMBER from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind is null and inf_status = 'N' )")
                        If dtl.Tables(0).Rows.Count > 0 Then
                            Dim i As Integer
                            For i = 0 To dtl.Tables(0).Rows.Count - 1
                                db.RcvTempDetail.Add(New RcvTempDetail With {
                                .TXLL_ID = dtl.Tables(0).Rows(i).Item("IF_ID"),
                                .SRC_NAME = dtl.Tables(0).Rows(i).Item("SRC_NAME"),
                                .TARGET_NAME = dtl.Tables(0).Rows(i).Item("TARGET_NAME"),
                                .DOCUMENT_NO = dtl.Tables(0).Rows(i).Item("PO_NUMBER"),
                                .PO_REF = dtl.Tables(0).Rows(i).Item("PO_NUMBER"),
                                .PO_ITEM = dtl.Tables(0).Rows(i).Item("PO_ITEM"),
                                .MATERIAL = dtl.Tables(0).Rows(i).Item("MATERIAL"),
                                .PLANT = dtl.Tables(0).Rows(i).Item("PLANT"),
                                .MATL_GROUP = dtl.Tables(0).Rows(i).Item("MATL_GROUP"),
                                .QUANTITY = dtl.Tables(0).Rows(i).Item("QUANTITY"),
                                .QTY_CONFIRM = dtl.Tables(0).Rows(i).Item("QUANTITY"),
                                .PO_UNIT = dtl.Tables(0).Rows(i).Item("PO_UNIT"),
                                .NET_PRICE = dtl.Tables(0).Rows(i).Item("NET_PRICE") * dtl.Tables(0).Rows(i).Item("QUANTITY"),
                                .PRICE_UNIT = dtl.Tables(0).Rows(i).Item("PRICE_UNIT"),
                                .DELIVERY_DATE3 = dtl.Tables(0).Rows(i).Item("DELIVERY_DATE"),
                                .MATERIAL_TEXT = dtl.Tables(0).Rows(i).Item("SHORT_TEXT"),
                                .CreateDate = DateTimeServer(), .CreateBy = "INF",
                                .UpdateDate = DateTimeServer(), .UpdateBy = "INF", .Enable = True})
                            Next
                        End If
                        db.SaveChanges()
                        execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM set inf_status = 'I' where target_name = 'WMS' and delete_ind is null and inf_status = 'N'
                        and PO_NUMBER in (select PO_NUMBER from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind is null and inf_status = 'N' )")
                        execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER set inf_status = 'I' where target_name = 'WMS' and delete_ind is null and inf_status = 'N' ")

                        'Step 2 Update Cancel PO
                        Dim ds2 = selSQL("select distinct po_number from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind = 'L' and inf_status = 'N'")
                        If ds2.Tables(0).Rows.Count > 0 Then
                            Dim i As Integer
                            For i = 0 To ds2.Tables(0).Rows.Count - 1
                                Dim docno As String = ds2.Tables(0).Rows(i).Item("po_number")
                                Dim ef2 = (From c In db.RcvTempHeader Where c.DOCUMENT_NO = docno).FirstOrDefault
                                If Not IsNothing(ef2) Then
                                    ef2.DELETE_IND = "L"
                                    ef2.UpdateBy = "INF"
                                    ef2.UpdateDate = DateTimeServer()
                                End If
                                Dim ef22 = (From c In db.RcvTempDetail Where c.DOCUMENT_NO = docno).ToList
                                If ef22.Count > 0 Then
                                    For Each a In ef22
                                        a.DELETE_IND = "L"
                                        a.UpdateBy = "INF"
                                        a.UpdateDate = DateTimeServer()
                                    Next
                                End If
                            Next
                            execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM set inf_status = 'I' where target_name = 'WMS' and delete_ind = 'L' and inf_status = 'N'
                            and PO_NUMBER in (select PO_NUMBER from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind = 'L' and inf_status = 'N' )")
                            execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER set inf_status = 'I' where target_name = 'WMS' and delete_ind = 'L' and inf_status = 'N' ")
                        End If
                        'Step 3 Update Cancel PO Item
                        Dim ds3 = selSQL("select distinct po_number from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind = 'U' and inf_status = 'N'")
                        If ds3.Tables(0).Rows.Count > 0 Then
                            Dim k As Integer
                            For k = 0 To ds3.Tables(0).Rows.Count - 1
                                Dim docno As String = ds3.Tables(0).Rows(k).Item("po_number")
                                Dim ef2 = (From c In db.RcvTempHeader Where c.DELETE_IND Is Nothing And c.DOCUMENT_NO = docno).FirstOrDefault
                                If Not IsNothing(ef2) Then
                                    ef2.DELETE_IND = "U"
                                    ef2.UpdateBy = "INF"
                                    ef2.UpdateDate = DateTimeServer()
                                    Dim ds33 = selSQL("select distinct po_number,PO_ITEM,MATERIAL from " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM where target_name = 'WMS' and delete_ind = 'L' and inf_status = 'N' and po_number = '" & docno & "'")
                                    If ds33.Tables(0).Rows.Count > 0 Then
                                        Dim b As Integer
                                        For b = 0 To ds33.Tables(0).Rows.Count - 1
                                            Dim itm As String = ds33.Tables(0).Rows(b).Item("PO_ITEM")
                                            Dim mat As String = ds33.Tables(0).Rows(b).Item("MATERIAL")
                                            Dim ef22 = (From c In db.RcvTempDetail Where c.DELETE_IND Is Nothing And c.DOCUMENT_NO = docno And c.PO_ITEM = itm And c.MATERIAL = mat).FirstOrDefault
                                            If Not IsNothing(ef22) Then
                                                ef22.DELETE_IND = "L"
                                                ef22.UpdateBy = "INF"
                                                ef22.UpdateDate = DateTimeServer()
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                            execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM set inf_status = 'I' where target_name = 'WMS' and delete_ind = 'L' and inf_status = 'N'
                            and PO_NUMBER in (select PO_NUMBER from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind = 'U' and inf_status = 'N' )")
                            execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER set inf_status = 'I' where target_name = 'WMS' and delete_ind = 'U' and inf_status = 'N' ")
                        End If
                        'Step 4 Update Close PO Item (กรณีที่รับไม่ครบ แล้วยืนยันรับตามจริง)
                        Dim ds4 = selSQL("select distinct po_number from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind = 'C' and inf_status = 'N'")
                        If ds4.Tables(0).Rows.Count > 0 Then
                            Dim k As Integer
                            For k = 0 To ds4.Tables(0).Rows.Count - 1
                                Dim docno As String = ds4.Tables(0).Rows(k).Item("po_number")
                                Dim ef2 = (From c In db.RcvTempHeader Where c.DELETE_IND <> "L" And c.DOCUMENT_NO = docno).FirstOrDefault
                                If Not IsNothing(ef2) Then
                                    ef2.DELETE_IND = "C"
                                    ef2.UpdateBy = "INF"
                                    ef2.UpdateDate = DateTimeServer()
                                    Dim ds33 = selSQL("select distinct po_number,PO_ITEM,MATERIAL from " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM where target_name = 'WMS' and no_more_gr = 'X' and inf_status = 'N' and po_number = '" & docno & "'")
                                    If ds33.Tables(0).Rows.Count > 0 Then
                                        Dim b As Integer
                                        For b = 0 To ds33.Tables(0).Rows.Count - 1
                                            Dim itm As String = ds33.Tables(0).Rows(b).Item("PO_ITEM")
                                            Dim mat As String = ds33.Tables(0).Rows(b).Item("MATERIAL")
                                            Dim ef22 = (From c In db.RcvTempDetail Where c.DOCUMENT_NO = docno And c.PO_ITEM = itm And c.MATERIAL = mat).FirstOrDefault
                                            If Not IsNothing(ef22) Then
                                                ef22.DELETE_IND = "L"
                                                ef22.NO_MORE_GR = "X"
                                                ef22.UpdateBy = "INF"
                                                ef22.UpdateDate = DateTimeServer()
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                            execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOITEM set inf_status = 'I' where target_name = 'WMS' and no_more_gr = 'X' and inf_status = 'N'
                            and PO_NUMBER in (select PO_NUMBER from " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER where target_name = 'WMS' and delete_ind = 'C' and inf_status = 'N' )")
                            execSQL("update " & DBLink & "..STAG.IF_MM035_BAPIMEPOHEADER set inf_status = 'I' where target_name = 'WMS' and delete_ind = 'C' and inf_status = 'N' ")
                        End If
                        db.SaveChanges()
                    End Using
                    Cursor = Cursors.Default
                    MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    SelData()
                Else
                    Cursor = Cursors.Default
                    MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub tbQty_KeyDown(sender As Object, e As KeyEventArgs) Handles tbQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            btEditRcv.Focus()
        End If
    End Sub

    Private Sub btEditRcv_Click(sender As Object, e As EventArgs) Handles btEditRcv.Click
        If tbQty.Text = "" Then
            MessageBox.Show("กรุณาป้อน จำนวนที่ต้องการเบิก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbQty.Focus()
            Exit Sub
        End If
        If CDbl(tbQty.Text) > OldQty Then
            MessageBox.Show("ป้อนจำนวนเกิน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbQty.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    Dim ds = (From c In db.RcvTempDetail Where c.Id = DtlID).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.QTY_CONFIRM = CDbl(tbQty.Text)

                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                    End If
                    Dim ds1 = (From c In dtRcv Where c.Id = DtlID).FirstOrDefault
                    If Not IsNothing(ds1) Then
                        ds1.QTY_CONFIRM = CDbl(tbQty.Text)
                    End If
                    db.SaveChanges()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData2()
            tbQty.Clear()
        End If
    End Sub

    Private Sub btConvert_Click(sender As Object, e As EventArgs) Handles btConvert.Click
        Try
            Dim ds = (From c In dt Where c.Id = hdID).FirstOrDefault
            If Not IsNothing(ds) Then
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim compCode = (From c In db.Owners Where c.FKCompany = CompID And c.Code = ds.COMP_CODE Select c.Id, c.Code).FirstOrDefault
                    If IsNothing(compCode) Then
                        Cursor = Cursors.Default
                        MessageBox.Show("ไม่พบข้อมูล Owner : " & ds.COMP_CODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Dim VenCode = (From c In db.Vendor Where c.FKCompany = CompID And c.Code = ds.VENDOR Select c.Id, c.Code).FirstOrDefault
                    If IsNothing(VenCode) Then
                        If ds.DOC_TYPE <> "RPUN" Then
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล Vendor : " & ds.VENDOR, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    End If
                    Dim dtWh = (From c In db.Warehouse Where c.FKCompany = CompID).ToList
                    Dim dtProd = (From c In db.Products Where c.FKCompany = CompID And c.FKOwner = compCode.Id).ToList
                    Dim dtUnit = (From c In db.ProductUnit Where c.FKCompany = CompID).ToList
                    Dim docDate As String = ds.DOC_DATE
                    Dim dsDTL = (From c In dtRcv Where c.DOCUMENT_NO = ds.DOCUMENT_NO).ToList
                    If dsDTL.Count > 0 Then
                        For Each L In dsDTL
                            If L.QTY_CONFIRM > 0 Then
                                Dim WhCode = (From c In dtWh Where c.Code = L.PLANT Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(WhCode) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล Warehouse : " & L.PLANT, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim PdHD = (From c In dtProd Where c.Zone.Warehouse.Code = L.PLANT And c.Code = L.MATERIAL Select c.Id, c.Code, c.ShelfLife).FirstOrDefault
                                If IsNothing(PdHD) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล Products : " & L.MATERIAL, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim UnID = (From c In dtUnit Where c.Code = L.PO_UNIT Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(UnID) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล PO Unit : " & L.PO_UNIT & " (" & L.MATERIAL & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim PdDTL = (From c In db.ProductDetails Where c.FKProduct = PdHD.Id And c.FKProductUnit = UnID.Id Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(PdDTL) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล หน่วยสินค้า : " & L.PO_UNIT & " (" & L.MATERIAL & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next
                    End If
                    Dim U = (From c In db.RcvTempHeader Where c.Id = hdID).FirstOrDefault
                    If Not IsNothing(U) Then
                        U.CompleteDate = DateTimeServer()
                        U.CompleteBy = Username
                    End If
                    db.SaveChanges()
                End Using
                Cursor = Cursors.Default
                MessageBox.Show("Convert Data เรียบร้อย", "Convert Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                SelData()
            Else
                Cursor = Cursors.Default
                MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub btLoadRTV_Click(sender As Object, e As EventArgs) Handles btLoadRTV.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    'Check Null >> Vendor Error,Material Error ,Net Price Error----------------------------------------------------------
                    Dim ckVen = selSQL("select distinct document_no from [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_HEAD where inf_status = 'N' and vendor_code is null and document_no in (select distinct document_no from [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL where inf_status = 'N' and PLANT collate database_default in (select code from wms.Warehouse where Enable = 1))")
                    If ckVen.Tables(0).Rows.Count > 0 Then
                        Dim ck As Integer
                        For ck = 0 To ckVen.Tables(0).Rows.Count - 1
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_HEAD set inf_status = 'E' where inf_status = 'N' and document_no = '" & ckVen.Tables(0).Rows(ck).Item("document_no") & "'")
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL set inf_status = 'E' where inf_status = 'N' and document_no = '" & ckVen.Tables(0).Rows(ck).Item("document_no") & "'")
                        Next
                    End If
                    Dim ckMat = selSQL("select distinct document_no from [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL where inf_status = 'N' and material_id is null and PLANT collate database_default in (select code from wms.Warehouse where Enable = 1)")
                    If ckMat.Tables(0).Rows.Count > 0 Then
                        Dim ck As Integer
                        For ck = 0 To ckMat.Tables(0).Rows.Count - 1
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_HEAD set inf_status = 'E' where inf_status = 'N' and document_no = '" & ckMat.Tables(0).Rows(ck).Item("document_no") & "'")
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL set inf_status = 'E' where inf_status = 'N' and document_no = '" & ckMat.Tables(0).Rows(ck).Item("document_no") & "'")
                        Next
                    End If
                    Dim ckNet = selSQL("select distinct document_no from [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL where inf_status = 'N' and material_id is null and PLANT collate database_default in (select code from wms.Warehouse where Enable = 1)")
                    If ckNet.Tables(0).Rows.Count > 0 Then
                        Dim ck As Integer
                        For ck = 0 To ckMat.Tables(0).Rows.Count - 1
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_HEAD set inf_status = 'E' where inf_status = 'N' and document_no = '" & ckNet.Tables(0).Rows(ck).Item("document_no") & "')")
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL set inf_status = 'E' where inf_status = 'N' and document_no = '" & ckNet.Tables(0).Rows(ck).Item("document_no") & "')")
                        Next
                    End If
                    '------------------------------------------------------------------------------
                    Dim ds = selSQL("select * from " & DBLink & "..STAG.TXLL_WMS_LL003_TF_IN_HEAD where inf_status = 'N' and document_no in (select distinct document_no from [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL where inf_status = 'N' and PLANT collate database_default in (select code from wms.Warehouse where Enable = 1))")
                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim i As Integer
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            Dim dtl = selSQL("select * from [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL where inf_status = 'N' and document_no = '" & ds.Tables(0).Rows(i).Item("document_no") & "' and PLANT collate database_default in (select code from wms.Warehouse where Enable = 1)")
                            If dtl.Tables(0).Rows.Count > 0 Then
                                Dim k As Integer
                                For k = 0 To dtl.Tables(0).Rows.Count - 1
                                    db.RcvTempDetail.Add(New RcvTempDetail With {
                                    .TXLL_ID = dtl.Tables(0).Rows(k).Item("TXLL_ID"),
                                    .SRC_NAME = dtl.Tables(0).Rows(k).Item("SRC_NAME"),
                                    .TARGET_NAME = dtl.Tables(0).Rows(k).Item("TARGET_NAME"),
                                    .DOCUMENT_NO = dtl.Tables(0).Rows(k).Item("document_no"),
                                    .PO_REF = "-",
                                    .PO_ITEM = dtl.Tables(0).Rows(k).Item("ITEM_NO"),
                                    .MATERIAL = dtl.Tables(0).Rows(k).Item("material_id"),
                                    .PLANT = dtl.Tables(0).Rows(k).Item("plant"),
                                    .STGE_LOC = "-",
                                    .QUANTITY = dtl.Tables(0).Rows(k).Item("quantity"),
                                    .QTY_CONFIRM = dtl.Tables(0).Rows(k).Item("quantity"),
                                    .PO_UNIT = dtl.Tables(0).Rows(k).Item("po_unit"),
                                    .NET_PRICE = dtl.Tables(0).Rows(k).Item("quantity") * dtl.Tables(0).Rows(k).Item("net_price"),
                                    .PRICE_UNIT = dtl.Tables(0).Rows(k).Item("price_unit"),
                                    .DELIVERY_DATE3 = "-",
                                    .CreateDate = DateTimeServer(), .CreateBy = Username,
                                    .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                Next
                            End If
                            db.RcvTempHeader.Add(New RcvTempHeader With {
                            .TXLL_ID = ds.Tables(0).Rows(i).Item("TXLL_ID"),
                            .SRC_NAME = ds.Tables(0).Rows(i).Item("SRC_NAME"),
                            .TARGET_NAME = ds.Tables(0).Rows(i).Item("TARGET_NAME"),
                            .PO_NUMBER = "-",
                            .COMP_CODE = ds.Tables(0).Rows(i).Item("COMP_CODE"),
                            .DOC_TYPE = ds.Tables(0).Rows(i).Item("doc_type"),
                            .VENDOR = ds.Tables(0).Rows(i).Item("vendor_code"),
                            .DOCUMENT_NO = ds.Tables(0).Rows(i).Item("document_no"),
                            .DOC_DATE = ds.Tables(0).Rows(i).Item("doc_date"),
                            .REF_1 = "-",
                            .OUR_REF = "-",
                            .CreateDate = DateTimeServer(), .CreateBy = Username,
                            .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})

                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_HEAD set inf_status = 'P' where inf_status = 'N' and document_no = '" & ds.Tables(0).Rows(i).Item("document_no") & "'")
                            execSQL("update [" & DBLink & "]..[STAG].TXLL_WMS_LL003_TF_IN_DETAIL set inf_status = 'P' where inf_status = 'N' and document_no = '" & ds.Tables(0).Rows(i).Item("document_no") & "'")
                        Next
                        db.SaveChanges()
                        Cursor = Cursors.Default
                        MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SelData()
                    Else
                        Cursor = Cursors.Default
                        MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor = Cursors.WaitCursor
        'Using db = New PTGwmsEntities
        '    Dim dst = (From c In db.PTC1209_USE).ToList
        '    Dim RcvHeader = New RcvHeader With {.ReceiveDate = DateTimeServer(), .DocumentNo = "ImpPTC1209", .DocumentDate = DateTimeServer(), .FKCompany = 1, .FKRcvType = 30, .FKVendor = 777, .FKOwner = 14, .PONumber = "ImpPTC1209", .RefNo = "ImpPTC1209", .RefDate = DateTimeServer(), .Description = "-", .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
        '    For Each a In dst
        '        RcvHeader.RcvDetails.Add(New RcvDetails With {.LotNo = "-", .FKWarehouse = 250, .FKProductDetail = a.fkproductdetail, .FKItemStatus = 1, .Quantity = CDbl(a.qty), .PriceUnit = CDbl(a.cost / a.qty), .NetPrice = a.cost, .ProductDate = DateTimeServer(), .ExpDate = (DateAdd(DateInterval.Day, 365, DateTimeServer())), .PalletNo = 0, .Remark = "", .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
        '        db.RcvHeader.Add(RcvHeader)
        '    Next
        '    db.SaveChanges()
        'End Using
        Cursor = Cursors.Default
    End Sub

    Private Sub btLoadReturnOrder_Click(sender As Object, e As EventArgs) Handles btLoadReturnOrder.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim ssd = (From c In db.ReturnDeliveryOrderDetails Where c.TakeOrderDate Is Nothing And c.RejectDate Is Nothing).ToList
                    Dim sd = (From c In ssd Select c.CompCode, c.DODoc, c.SoldTo, c.ReturnDate, c.FKReturnDeliveryOrder).Distinct.ToList
                    If sd.Count > 0 Then
                        For Each s In sd
                            db.RcvTempHeader.Add(New RcvTempHeader With {.SRC_NAME = "SAP", .PO_NUMBER = "-", .COMP_CODE = s.CompCode, .DOCUMENT_NO = s.DODoc, .DOC_DATE = s.ReturnDate.ToString("yyyyMMdd"), .VENDOR = s.SoldTo, .HeaderID = s.FKReturnDeliveryOrder, .TargetType = "SD006", .CreateDate = DateTimeServer(), .CreateBy = "INF", .UpdateDate = DateTimeServer(), .UpdateBy = "INF", .Enable = True})
                            Dim sdd = (From c In ssd Where c.FKReturnDeliveryOrder = s.FKReturnDeliveryOrder).ToList
                            If sdd.Count > 0 Then
                                For Each t In sdd
                                    db.RcvTempDetail.Add(New RcvTempDetail With {.SRC_NAME = "SAP", .DOCUMENT_NO = s.DODoc, .MATERIAL = t.Material, .MATERIAL_TEXT = t.MaterialText, .QUANTITY = t.BaseQty, .QTY_CONFIRM = t.BaseQty, .PO_UNIT = t.BaseUnit, .PLANT = t.WMsWH, .FKHeaderID = t.FKReturnDeliveryOrder, .ITEMCATE = t.ItemCate, .CreateDate = DateTimeServer(), .CreateBy = "INF", .UpdateDate = DateTimeServer(), .UpdateBy = "INF", .Enable = True})
                                    t.TakeOrderDate = DateTimeServer()
                                    t.TakeOrderBy = Username
                                Next
                            End If
                        Next
                        db.SaveChanges()
                        Cursor = Cursors.Default
                        MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SelData()
                    Else
                        Cursor = Cursors.Default
                        MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

End Class