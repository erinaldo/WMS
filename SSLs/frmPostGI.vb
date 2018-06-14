Imports System.Globalization
Imports System.Threading
Imports ClassLibrarySD004
Imports ClassLibrarySD004.Model_Design

Public Class frmPostGI

    Private Sub btRefresh1_Click(sender As Object, e As EventArgs) Handles btRefresh1.Click
        Cursor = Cursors.WaitCursor
        DataGridView3.DataSource = Nothing
        Using db = New PTGwmsEntities
            Dim dt As New List(Of PickOrderDTL)
            If RoleID = 1 Then
                dt = db.PickOrderDTL.Include("PickOrderHD").Where(Function(x) x.PickOrderHD.FKCompany = CompID And x.Enable = True And x.GIDate Is Nothing And x.PickOrderHD.ConfirmDate IsNot Nothing And x.PickOrderHD.PickedDate IsNot Nothing).ToList
            Else
                dt = db.PickOrderDTL.Include("PickOrderHD").Where(Function(x) x.PickOrderHD.FKCompany = CompID And x.Enable = True And x.GIDate Is Nothing And x.PickOrderHD.ConfirmDate IsNot Nothing And x.PickOrderHD.PickedDate IsNot Nothing And frmMain.ow.Contains(x.PickOrderHD.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
            End If
            Dim iss = (From c In dt Group c By c.PickOrderHD.FKCompany, c.PickOrderHD.FKOwner, Owner = c.PickOrderHD.Owners.Code, เลขที่ชุด = c.PickOrderHD.BatchNo Into จำนวน = Sum(c.QtyConfirm) Order By Owner, เลขที่ชุด).ToList
            If iss.Count > 0 Then
                With DataGridView3
                    .DataSource = iss
                    .Columns("FKCompany").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End If
        End Using
        DataGridView1.DataSource = Nothing
        Cursor = Cursors.Default
        btConfirm.Enabled = False
        btPrintTote.Enabled = False
        btPrintSlip.Enabled = False
        btPrintDO.Enabled = False
        btCancelBatch.Enabled = False
    End Sub

    Private Sub DataGridView3_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex <> -1 Then
            batchID = DataGridView3.Rows(e.RowIndex).Cells(3).Value.ToString
            btCancelBatch.Enabled = True
            SelData2()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            DocID = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
            btConfirm.Enabled = True
            btPrintTote.Enabled = True
            btPrintSlip.Enabled = True
            btPrintDO.Enabled = True
        End If
    End Sub

    Public Sub SelData2()
        Using db = New PTGwmsEntities
            Dim iss = (From c In db.PickOrderHD Where c.BatchNo = batchID And c.PostGIDate Is Nothing Select New With {c.Id, .เลขที่เอกสาร = c.OrderNo, .วันที่เอกสาร = c.OrderDate, .วันที่เบิก = c.PickingDate, .สาขา = c.Customers.Name, c.RefNo, c.RefDate}).ToList
            'Dim iss = (From c In db.PickOrderHD Where c.BatchNo = batchID Select New With {c.Id, .เลขที่เอกสาร = c.OrderNo, .วันที่เอกสาร = c.OrderDate, .วันที่เบิก = c.PickingDate, .สาขา = c.Customers.Name, c.RefNo, c.RefDate}).ToList
            If iss.Count > 0 Then
                With DataGridView1
                    .DataSource = iss
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End If
        End Using
        btConfirm.Enabled = False
        btPrintTote.Enabled = False
        btPrintSlip.Enabled = False
        btPrintDO.Enabled = False
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If MessageBox.Show("ต้องการยืนยัน Post GI ใช่หรือไม่?", "ยืนยันการ Post GI", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
                Using db = New PTGwmsEntities
                    Dim sel = (From c In db.PickOrderHD Where c.Id = DocID).FirstOrDefault
                    If Not IsNothing(sel) Then
                        If sel.PostGIDate Is Nothing Then

                            'Start Call Webservice sap SD ------------------------------------
                            If sel.SrcName = "SAP" Then
                                Dim sdt As New List(Of RequestSD004)
                                Dim sd = (From c In db.PickOrderDTL Where c.FKPickOrderHD = sel.Id And c.QtyCheck > 0).ToList
                                If sd.Count > 0 Then
                                    For Each d In sd
                                        If d.InterfaceName = "SD003" Then
                                            sdt.Add(New RequestSD004 With {.DocRef = d.PickOrderHD.OrderNo, .Material = d.ProductDetails.Products.Code, .BaseQty = d.QtyCheck, .BaseUnit = d.ProductDetails.ProductUnit.Code, .Func = d.Func, .ItemCate = d.ItemCate})
                                        ElseIf d.InterfaceName = "SD031" Then
                                            sdt.Add(New RequestSD004 With {.DocRef = d.PickOrderHD.OrderNo, .Material = d.ProductDetails.Products.Code, .BaseQty = d.QtyCheck, .BaseUnit = d.ProductDetails.ProductUnit.Code, .Func = d.Func, .ItemCate = d.ItemCate})
                                        End If
                                    Next
                                End If
                                ClassLibrarySD004.SDI004WMsToSap.DoInDllSD004(sdt)

                                Dim chk1 = (From c In db.SD004Dtl Where c.SapStatus = "S" And c.DocRef = sel.OrderNo).FirstOrDefault
                                If IsNothing(chk1) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("Post GI ไม่สำเร็จ กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                            'End Call Webservice sap SD ---------------------------------------------------------------------------------------------------

                            'Start Insert Stock Card...........
                            execSQL("exec [wms].[POST_GI_UPDATE_STOCK_ONH] " & sel.Id & ",'" & Username & "'")

                            Dim chk = (From c In db.PickOrderHD Where c.Id = DocID And c.PostGIDate Is Nothing).FirstOrDefault
                            If Not IsNothing(chk) Then
                                Cursor = Cursors.Default
                                MessageBox.Show("Post GI ไม่สำเร็จ กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            'Start ยิงข้อมูลใบโอนจ่าย mart punthai------------------------------
                            If sel.SrcName = "PUN" Or sel.SrcName = "MMS" Then
                                Dim pm = (From c In db.PickOrderDTL Where c.FKPickOrderHD = sel.Id And c.QtyCheck > 0 Select c.PickOrderHD.SrcName, c.PickOrderHD.OrderNo, c.PickOrderHD.OrderDate, c.PickOrderHD.RefNo, c.PickOrderHD.PickingDate, PickTypeName = c.PickOrderHD.PickingType.Name, PickTypeCode = c.PickOrderHD.PickingType.Code, PlantCode = c.Warehouse.Code, CompCode = c.PickOrderHD.Owners.Code, CustCode = c.PickOrderHD.Customers.Code, c.PickOrderHD.BatchNo).Distinct.FirstOrDefault
                                If Not IsNothing(pm) Then
                                    execSQL("Declare @NextSeq As Int
                                            Select @NextSeq = NextVal 
                                            From OpenQuery(" & DBLink & ", 'SELECT STAG.TXLL_WMS_LL002_TF_OUT_HEAD_SEQ.NEXTVAL FROM DUAL')
                                            INSERT INTO " & DBLink & "..STAG.TXLL_WMS_LL002_TF_OUT_HEADER (TXLL_ID,SRC_NAME,TARGET_NAME,SHOP_ID,DOCUMENT_NO,DOCUMENT_TRANSACTION_TYPE,PO_STATUS,DELIVERY_DATE,DOCUMENT_DATE,
                                            TOTAL_DISCOUNT,VAT_TYPE,VAT_AMOUNT,NET_AMOUNT,TOTAL_AMOUNT,REF_DOC,REMARK,PROCESS_STATUS,RECORD_STATUS,TRANSFER_FROM_SHOP_ID,
                                            TRANSFER_TO_SHOP_ID,NET_AMOUNT_EXCLUDE_VAT,NET_AMOUNT_INCLUDE_VAT,TOTAL_AMOUNT_EXCLUDE_VAT,TOTAL_AMOUNT_INCLUDE_VAT,
                                            APPROVED_BY,APPROVED_DT,IS_APPROVED,IS_CANCEL_APPROVED,IS_SUBMIT,IS_CANCEL_SUBMIT,IS_PRINT,IS_CANCEL_DOCUMENT,IS_TYPE_REQUEST,
                                            IS_ADJUST_RECEIVED,IS_DELETED,CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE)
                                            VALUES (@NextSeq,'WMS','" & pm.SrcName & "','" & pm.PlantCode & "','" & pm.BatchNo & "','" & IIf(pm.SrcName = "PUN", "04", "8") & "','" & IIf(pm.SrcName = "PUN", "02", "2") & "',
                                            convert(datetime,'" & Format(pm.PickingDate, "yyyyMMdd") & "'),convert(datetime,'" & Format(pm.OrderDate, "yyyyMMdd") & "'),0,'00',0,0,0,'" & pm.OrderNo & "','" & "โอนจ่ายสินค้า " & pm.SrcName & "','02','Y','" & pm.PlantCode & "',
                                            '" & pm.CustCode & "',0,0,0,0,'WMS',getdate(),'N','N','N','N','Y','N','01','Y','N',getdate(),'WMS',getdate(),'N',convert(datetime,'99991231'))")
                                    Dim sd = (From c In db.PickOrderDTL Where c.FKPickOrderHD = sel.Id And c.QtyCheck > 0).ToList
                                    If sd.Count > 0 Then
                                        For Each d In sd
                                            Dim onh = (From c In db.StockOnhand Where c.FKCompany = d.PickOrderHD.FKCompany And c.FKWarehouse = d.FKWarehouse And c.FKOwner = d.PickOrderHD.FKOwner And c.FKItemStatus = d.FKItemStatus And c.FKProduct = d.ProductDetails.FKProduct).FirstOrDefault
                                            If Not IsNothing(onh) Then
                                                execSQL("Declare @NextSeq As Int
                                                        Select @NextSeq = NextVal 
                                                        From OpenQuery(" & DBLink & ", 'SELECT STAG.TXLL_WMS_LL002_TF_OUT_DETL_SEQ.NEXTVAL FROM DUAL')
                                                        INSERT INTO " & DBLink & "..STAG.TXLL_WMS_LL002_TF_OUT_DETAIL (TXLL_ID,SRC_NAME,TARGET_NAME,NO,DOCUMENT_NO,MATERIAL_ID,MATERIAL_UNIT_ID,QTY,PRICE,DISCOUNT,VAT_TYPE,VAT_RATE,TOTA_AMOUNT, 
                                                        PROCESS_STATUS,RECORD_STATUS,TRANSFER_QTY,TRANSFER_APPROVED_QTY,PRICE_EXCLUDE_VAT,PRICE_INCLUDE_VAT,TOTAL_AMOUNT_EXCLUDE_VAT,
                                                        TOTAL_AMOUNT_INCLUDE_VAT,MATERIAL_STOCK,CURRENT_COST,ACTUAL_APPROVED_QTY,IS_DELETED,CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE)
                                                        VALUES (@NextSeq,'WMS','" & pm.SrcName & "','" & d.ItemNo & "','" & pm.BatchNo & "','" & d.ProductDetails.Products.Code & "','" & d.ProductDetails.ProductUnit.Code & "',
                                                        " & d.QtyCheck & "," & onh.QtyCost * d.ProductDetails.PackSize & ",0,'02',0," & (d.QtyCheck * d.ProductDetails.PackSize) * onh.QtyCost & ",
                                                        '02','Y'," & d.QtyCheck & "," & d.QtyCheck & "," & (d.QtyCheck * d.ProductDetails.PackSize) * onh.QtyCost & ",
                                                        " & (d.QtyCheck * d.ProductDetails.PackSize) * onh.QtyCost & "," & (d.QtyCheck * d.ProductDetails.PackSize) * onh.QtyCost & "," & (d.QtyCheck * d.ProductDetails.PackSize) * onh.QtyCost & ",
                                                        " & onh.Qty / d.ProductDetails.PackSize & "," & onh.QtyCost * d.ProductDetails.PackSize & "," & d.QtyCheck & ",'N',getdate(),'WMS',getdate(),'N',convert(datetime,'99991231'))")
                                            End If
                                        Next
                                    End If
                                End If
                            End If
                            'End ยิงข้อมูลใบโอนจ่าย mart punthai---------------------------------------------------------------------------------------------------

                            execSQL("exec [wms].[POST_GI] " & DocID & ",'" & Username & "'")
                        Else
                            Cursor = Cursors.Default
                            MessageBox.Show("รายการนี้ Post GI ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If

                    End If

                End Using
                Cursor = Cursors.Default
                MessageBox.Show("Post GI เรียบร้อย", "Post GI Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btRefresh1_Click(sender, e)
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails3_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView3.RowPostPaint
        Using b As New SolidBrush(DataGridView3.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btPrintTote_Click(sender As Object, e As EventArgs) Handles btPrintTote.Click
        Cursor = Cursors.WaitCursor
        frmPrintTote.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub btPrintSlip_Click(sender As Object, e As EventArgs) Handles btPrintSlip.Click
        Cursor = Cursors.WaitCursor
        frmPrintToteSlip.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub frmPostGI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btConfirm.Enabled = False
        btPrintTote.Enabled = False
        btPrintSlip.Enabled = False
        btPrintDO.Enabled = False
        'btCancelBatch.Enabled = False
    End Sub

    Private Sub btPrintDO_Click(sender As Object, e As EventArgs) Handles btPrintDO.Click
        Cursor = Cursors.WaitCursor
        frmrptPrintDO.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Cursor = Cursors.WaitCursor
        frmReprintDO.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub btCancelBatch_Click(sender As Object, e As EventArgs) Handles btCancelBatch.Click
        If MessageBox.Show("ต้องการยืนยัน ยกเลิกชุดเบิก ใช่หรือไม่?", "ยืนยันการยกเลิก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim pst = (From c In db.PickOrderHD Where c.BatchNo = batchID).ToList
                    For Each h In pst
                        If h.PostGIDate IsNot Nothing Then
                            Cursor = Cursors.Default
                            MessageBox.Show("มีรายการที่ Post GI ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Next
                    Dim dt = (From c In db.PickOrderHD Where c.BatchNo = batchID).ToList
                    For Each hd In dt
                        hd.BatchNo = Nothing
                        hd.ConfirmDate = Nothing
                        hd.ConfirmBy = Nothing
                        hd.PickedDate = Nothing
                        hd.PickedBy = Nothing
                        hd.PDAPickedDate = Nothing
                        hd.PDAPickedBy = Nothing
                        hd.UpdateDate = DateTimeServer()
                        hd.UpdateBy = Username
                        Dim pkLoc = (From c In db.PickOrderDTL Where c.FKPickOrderHD = hd.Id).ToList
                        For Each p In pkLoc
                            p.QtyCheck = 0
                            p.UpdateDate = DateTimeServer()
                            p.UpdateBy = Username
                            Dim pickT = (From c In db.PickTote Where c.FKPickOrderDTL = p.Id).ToList
                            For Each t In pickT
                                t.Enable = False
                                t.UpdateDate = DateTimeServer()
                                t.UpdateBy = Username
                            Next
                        Next
                    Next
                    Dim iss = (From c In db.IssueSum Where c.BatchNo = batchID).ToList 'batchID
                    If Not IsNothing(iss) Then
                        For Each s In iss
                            s.Enable = False
                            s.UpdateDate = DateTimeServer()
                            s.UpdateBy = Username
                            Dim onh = (From c In db.StockOnhand Where c.FKCompany = s.FKCompany And c.FKWarehouse = s.FKWarehouse And c.FKOwner = s.FKOwner And c.FKProduct = s.FKProduct And c.FKItemStatus = s.FKItemStatus).FirstOrDefault
                            If Not IsNothing(onh) Then
                                onh.BookQty = onh.BookQty - s.Qty
                                onh.UpdateDate = DateTimeServer()
                                onh.UpdateBy = Username
                            End If
                            Dim pLoc = (From c In db.PickLocation Where c.FKIssueSum = s.Id).ToList
                            For Each pl In pLoc
                                pl.Enable = False
                                pl.UpdateDate = DateTimeServer()
                                pl.UpdateBy = Username
                                Dim LocS = (From c In db.CurrentStocks Where c.Id = pl.CurStockID).FirstOrDefault
                                If Not IsNothing(LocS) Then
                                    If pl.ConfirmDate Is Nothing Then
                                        'กรณี ยังไม่หยิบสินค้า
                                        LocS.BookQty = LocS.BookQty - pl.Qty
                                        LocS.Enable = True
                                        LocS.UpdateDate = DateTimeServer()
                                        LocS.UpdateBy = Username
                                    Else
                                        'กรณี ที่หยิบแล้ว
                                        If LocS.Enable = True Then
                                            LocS.Qty = LocS.Qty + pl.Qty
                                            LocS.UpdateDate = DateTimeServer()
                                            LocS.UpdateBy = Username
                                        Else
                                            db.CurrentStocks.Add(New CurrentStocks With {.LotNo = LocS.LotNo, .FKCompany = LocS.FKCompany, .FKWarehouse = LocS.FKWarehouse, .FKOwner = LocS.FKOwner, .FKVendor = LocS.FKVendor, .FKLocation = LocS.FKLocation, .FKProduct = LocS.FKProduct, .Qty = pl.Qty, .BookQty = 0, .PriceUnit = LocS.PriceUnit, .NetPrice = LocS.NetPrice, .FKProductUnit = LocS.FKProductUnit, .ProductDate = LocS.ProductDate, .ExpDate = LocS.ExpDate, .ReceiveDate = LocS.ReceiveDate, .FKItemStatus = LocS.FKItemStatus, .PalletCode = LocS.PalletCode, .SourceConfirm = "Restore", .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                    db.SaveChanges()
                End Using
                Cursor = Cursors.Default
                MessageBox.Show("ยกเลิกชุดเบิก เรียบร้อย", "Cancel Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btRefresh1_Click(sender, e)
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
End Class