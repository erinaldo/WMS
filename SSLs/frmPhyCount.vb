Imports System.Globalization
Imports System.Threading

Public Class frmPhyCount

    Public OwnID, WHId, PdId, StatId, ItmID As Integer

    Private Sub frmPhyCount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        btClear_Click(sender, e)
    End Sub

    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        dtDocdate.CustomFormat = "dd/MM/yyyy"
        DataGrid1.Rows.Clear()
        ClearTextBox(GroupBox1)
        ClearTextBox(Me)
        tbDocNo.Text = "PC" & Format(DateTimeServer(), "yyMMddHHmm")
        tbDocNo.Focus()
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbOwner.KeyDown, tbWH.KeyDown, tbDesc.KeyDown, tbItm.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btOwner_Click(sender As Object, e As EventArgs) Handles btOwner.Click
        bt_Num = 37
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbOwner.Text
        frmSearch.ShowDialog()
        OwnID = s_ID
        tbOwner.Text = s_Code
        tbOwnerDesc.Text = s_Desc
        tbWH.Focus()
    End Sub

    Private Sub tbOwner_Leave(sender As Object, e As EventArgs) Handles tbOwner.Leave
        If tbOwner.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbOwner.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 37
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

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGrid1.RowPostPaint
        Using b As New SolidBrush(DataGrid1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGrid1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGrid1.CellFormatting
        'this formats all cells in column 0 excluding the newrow
        If e.ColumnIndex = 8 AndAlso e.RowIndex <> DataGrid1.NewRowIndex Then
            e.Value = CDec(e.Value).ToString("N2")
            e.FormattingApplied = True
        End If
    End Sub

    Private Sub dataGrid1_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DataGrid1.CellValidating

        If e.ColumnIndex = 8 Then
            If Not IsNumeric(e.FormattedValue) Then
                MessageBox.Show("กรุณาป้อนเฉพาะตัวเลขเท่านั้น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                DataGrid1.CurrentCell.Value = Nothing
                e.Cancel = True
            End If
        End If

    End Sub

    Private Sub DataGrid1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGrid1.CellEndEdit
        If e.ColumnIndex = 8 Then
            Dim sum As Double
            With DataGrid1
                sum = .Rows(e.RowIndex).Cells(8).Value - .Rows(e.RowIndex).Cells(4).Value
                .Rows(e.RowIndex).Cells(9).Value = sum
                .Rows(e.RowIndex).Cells(10).Value = sum * .Rows(e.RowIndex).Cells(5).Value
            End With
            SumGrid()
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
        If tbItm.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน สถานะ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbItm.Focus()
            Exit Sub
        End If
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                'Dim d = db.StockOnhand.Include("Products").Include("ItemStatus").Where(Function(x) x.Enable = True And x.FKOwner = OwnID And x.FKWarehouse = WHId).ToList
                Dim d = (From c In db.StockOnhand Order By c.Products.Code, c.ItemStatus.Code Where c.Enable = True And c.FKOwner = OwnID And c.FKWarehouse = WHId And c.FKItemStatus = ItmID).ToList
                Dim cnt = (From c In db.CycleCountDTL Where c.CheckType = "Physical Count" And c.CycleCountHD.FKOwner = OwnID And c.CycleCountHD.FKWarehouse = WHId).ToList
                Dim prodID, ItemID As Integer
                Dim cntChk As Double
                With DataGrid1
                    .Rows.Clear()
                    For i = 0 To d.Count - 1
                        .Rows.Add()
                        prodID = d(i).FKProduct
                        ItmID = d(i).FKItemStatus
                        .Rows(i).Cells(0).Value = d(i).Products.Code
                        .Rows(i).Cells(1).Value = d(i).Products.BaseBarcode
                        .Rows(i).Cells(2).Value = d(i).Products.Name
                        .Rows(i).Cells(3).Value = d(i).ItemStatus.Code
                        .Rows(i).Cells(4).Value = d(i).Qty
                        .Rows(i).Cells(5).Value = d(i).QtyCost
                        .Rows(i).Cells(6).Value = d(i).NetPrice
                        .Rows(i).Cells(7).Value = d(i).Products.BaseUnitCode
                        Dim a = (From c In cnt Where c.FKProduct = prodID And c.FKItemStatus = ItemID Group c By c.FKItemStatus, c.FKProduct Into Qty = Sum(c.QtyCount)).FirstOrDefault
                        If Not IsNothing(a) Then
                            cntChk = a.Qty
                        Else
                            cntChk = 0
                        End If
                        .Rows(i).Cells(8).Value = cntChk
                        If cntChk = 0 Then
                            .Rows(i).Cells(9).Value = 0
                            .Rows(i).Cells(10).Value = 0
                        Else
                            .Rows(i).Cells(9).Value = cntChk - d(i).Qty
                            .Rows(i).Cells(10).Value = (cntChk - d(i).Qty) * d(i).QtyCost
                        End If
                        .Rows(i).Cells(11).Value = ""
                        .Rows(i).Cells(12).Value = d(i).FKProduct
                        .Rows(i).Cells(13).Value = d(i).FKItemStatus
                    Next
                    SumGrid()
                End With
            End Using
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        Dim cnt As Decimal = 0
        Dim diff As Decimal = 0
        For index As Integer = 0 To DataGrid1.RowCount - 1
            qty += Convert.ToDecimal(DataGrid1.Rows(index).Cells(4).Value)
            cnt += Convert.ToDecimal(DataGrid1.Rows(index).Cells(8).Value)
            diff += Convert.ToDecimal(DataGrid1.Rows(index).Cells(9).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
        tbTotalCnt.Text = Format(cnt, "#,##0.00")
        tbTotalDiff.Text = Format(diff, "#,##0.00")
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbDocNo.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน เลขที่เอกสาร", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbDocNo.Focus()
            Exit Sub
        End If
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
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim i As Integer = 1
                    'Dim ck = (From c In db.CycleCountDTL Where c.CheckType = "Physical Count" And c.CycleCountHD.FKOwner = OwnID And c.CycleCountHD.FKWarehouse = WHId And Enabled = True And c.CheckDate Is Nothing).ToList
                    'If ck.Count > 0 Then
                    '    Cursor = Cursors.Default
                    '    MessageBox.Show("กรุณาตรวจนับให้ครบทุกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    Exit Sub
                    'End If

                    Dim ckOnh = (From c In db.StockOnhand Where c.Enable = True And c.FKCompany = CompID And c.FKWarehouse = WHId And c.FKOwner = OwnID And c.FKItemStatus = ItmID And c.BookQty > 0).FirstOrDefault
                    If Not IsNothing(ckOnh) Then
                        Cursor = Cursors.Default
                        MessageBox.Show("กรุณาตรวจสอบยอดจอง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If

                    Dim PhyCountHD = New PhyCountHD With {.FKCompany = CompID, .DocNo = tbDocNo.Text, .DocDate = dtDocdate.Value, .FKOwner = OwnID, .FKWarehouse = WHId, .Description = tbDesc.Text,
                                        .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                    For Each dr As DataGridViewRow In DataGrid1.Rows
                        PhyCountHD.PhyCountDTL.Add(New PhyCountDTL With {.FKProduct = dr.Cells(12).Value, .FKItemStatus = dr.Cells(13).Value, .Qty = CDbl(dr.Cells(4).Value), .QtyCost = CDbl(dr.Cells(5).Value), .NetPrice = CDbl(dr.Cells(6).Value), .UnitCode = dr.Cells(7).Value, .QtyCount = CDbl(dr.Cells(8).Value), .QtyDiff = CDbl(dr.Cells(9).Value), .CostDiff = CDbl(dr.Cells(10).Value), .Remark = dr.Cells(11).Value, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        Dim diff As Double = CDbl(dr.Cells(9).Value)
                        Dim Itm As Integer = dr.Cells(13).Value
                        Dim Prd As Integer = dr.Cells(12).Value
                        'update stockonhand
                        If diff <> 0 Then
                            Dim onh = (From c In db.StockOnhand Where c.FKCompany = CompID And c.FKWarehouse = WHId And c.FKOwner = OwnID And c.FKItemStatus = Itm And c.FKProduct = Prd).FirstOrDefault
                            If Not IsNothing(onh) Then

                                'Insert StockCard
                                If diff > 0 Then
                                    db.StockCard.Add(New StockCard With {
                                     .TransactionDate = DateTimeServer(),
                                     .DocNo = tbDocNo.Text,
                                     .DocDate = dtDocdate.Value,
                                     .Reference = tbDocNo.Text,
                                     .Description = "ปรับปรุงยอดตรวจนับ (บวก Stock)",
                                     .FKCompany = CompID,
                                     .FKWarehouse = WHId,
                                     .FKOwner = OwnID,
                                     .FKProduct = onh.FKProduct,
                                     .FKItemStatus = onh.FKItemStatus,
                                     .TransType = "ADJ",
                                     .InQty = diff,
                                     .InCost = onh.QtyCost,
                                     .InValue = diff * onh.QtyCost,
                                     .BalQty = onh.Qty + diff,
                                     .BalCost = onh.QtyCost,
                                     .BalValue = onh.NetPrice + (diff * onh.QtyCost),
                                     .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                Else
                                    db.StockCard.Add(New StockCard With {
                                     .TransactionDate = DateTimeServer(),
                                     .DocNo = tbDocNo.Text,
                                     .DocDate = dtDocdate.Value,
                                     .Reference = tbDocNo.Text,
                                     .Description = "ปรับปรุงยอดตรวจนับ (ตัด Stock)",
                                     .FKCompany = CompID,
                                     .FKWarehouse = WHId,
                                     .FKOwner = OwnID,
                                     .FKProduct = onh.FKProduct,
                                     .FKItemStatus = onh.FKItemStatus,
                                     .TransType = "ADJ",
                                     .OutQty = (diff * -1),
                                     .OutCost = onh.QtyCost,
                                     .OutValue = (diff * -1) * onh.QtyCost,
                                     .BalQty = onh.Qty - (diff * -1),
                                     .BalCost = onh.QtyCost,
                                     .BalValue = onh.NetPrice - ((diff * -1) * onh.QtyCost),
                                     .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                End If

                                onh.Qty = CDbl(dr.Cells(8).Value)
                                onh.BookQty = 0
                                onh.NetPrice = CDbl(dr.Cells(8).Value) * onh.QtyCost
                                onh.UpdateBy = Username
                                onh.UpdateDate = DateTimeServer()

                            End If
                        End If
                    Next
                    db.PhyCountHD.Add(PhyCountHD)
                    Dim chk = (From c In db.CycleCountDTL Where c.CheckType = "Physical Count" And c.CycleCountHD.FKOwner = OwnID And c.CycleCountHD.FKWarehouse = WHId And Enabled = True And c.ConfirmDate Is Nothing).ToList
                    If chk.Count > 0 Then
                        For Each a In chk
                            a.ConfirmDate = DateTimeServer()
                            a.ConfirmBy = Username
                        Next
                    End If
                    db.SaveChanges()
                    Dim Getlast = (From c In db.PhyCountHD Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                    Dim gl = (From c In db.PhyCountDTL.Include("Products.ProductType").Include("PhyCountHD.Owners").Include("PhyCountHD.Warehouse") Where c.FKHD = Getlast.Id And c.QtyDiff <> 0).ToList
                    If gl.Count > 0 Then
                        For Each g In gl
                            If g.Products.ProductType.Code = "ZPTL" Then
                                execSQL("Declare @NextSeq As Int
                                Select @NextSeq = NextVal 
                                From OpenQuery(" & DBLink & ", 'SELECT STAG.IF_MM011_GOODSMVT_CREATE_SEQ.NEXTVAL FROM DUAL')
                                INSERT INTO " & DBLink & "..STAG.IF_MM011_GOODSMVT_CREATE (IF_ID,SRC_NAME,TARGET_NAME,PSTNG_DATE,DOC_DATE,REF_DOC_NO,DOCUMENT_NO,
                                BILL_OF_LADING,HEADER_TXT,VER_GR_GI_SLIP,VER_GR_GI_SLIPX,GM_CODE,PO_ITEM,ZITEM,MATERIAL,PLANT,MOVE_TYPE,ENTRY_QNT,ENTRY_UOM,
                                IS_DELETED,CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE) 
                                VALUES (@NextSeq,'WMS','SAP','" & Format(g.PhyCountHD.DocDate, "dd.MM.yyyy") & "',
                                '" & Format(g.PhyCountHD.DocDate, "dd.MM.yyyy") & "','" & g.PhyCountHD.DocNo & "','" & g.PhyCountHD.DocNo & "',
                                '" & g.PhyCountHD.DocNo & "','PHYCOUNT','3','X','05','" & Format(i, "0000") & "0" & "','" & Format(i, "0000") & "0" & "','" & g.Products.Code & "','" & g.PhyCountHD.Owners.Code & "','" & IIf(g.QtyDiff > 0, "Z13", "Z14") & "'," & CDbl(g.QtyDiff) & ",'" & g.UnitCode & "','N',getdate(),'" & Username & "',getdate(),'N',convert(datetime,'99991231'));")
                            Else
                                execSQL("Declare @NextSeq As Int
                                Select @NextSeq = NextVal 
                                From OpenQuery(" & DBLink & ", 'SELECT STAG.WMS_GL004_BAPI_ACC_SEQ.NEXTVAL FROM DUAL')
                                INSERT INTO " & DBLink & "..STAG.WMS_GL004_BAPI_ACC (IF_ID,SRC_NAME,TARGET_NAME,INTERFACE_GROUP_ID,NO,COMP_CODE,DOC_TYPE,DOC_DATE,POSTING_DATE,
                                CURRENCY,HEADER_TXT,BRANCH,XREF1_HD,AMT_DOCCUR,AMT_DOCCUR_1,AMT_DOCCUR_2,QUANTITY,BASE_UOM,ITEM_TEXT,COMP_CODE_1,WERKS,ARTNR,COSTCENTER,BSCHL,GL_ACCOUNT,BUS_AREA,REF_KEY_1,NAME1,AUFNR,IS_DELETED,
                                CREATED_DATE,CREATED_BY,START_DATE,INF_STATUS,END_DATE)
                                VALUES (@NextSeq,'WMS','SAP','PHYCOUNT','" & i & "','" & g.PhyCountHD.Owners.Code & "','IV','" & Format(g.PhyCountHD.DocDate, "dd.MM.yyyy") & "',
                                '" & Format(g.PhyCountHD.DocDate, "dd.MM.yyyy") & "','THB','Physical Count','" & g.PhyCountHD.Warehouse.Code & "',
                                '" & g.PhyCountHD.DocNo & "'," & IIf(g.QtyDiff > 0, g.QtyDiff * g.QtyCost, (g.QtyDiff * g.QtyCost) * -1) & "," & IIf(g.QtyDiff > 0, g.QtyDiff * g.QtyCost, (g.QtyDiff * g.QtyCost) * -1) & "," & IIf(g.QtyDiff > 0, g.QtyDiff * g.QtyCost, (g.QtyDiff * g.QtyCost) * -1) & ",
                                " & IIf(g.QtyDiff > 0, g.QtyDiff, g.QtyDiff * -1) & ",'" & g.UnitCode & "','" & Microsoft.VisualBasic.Left(g.Products.Name, 75) & "',
                                '" & g.PhyCountHD.Owners.Code & "','" & g.PhyCountHD.Warehouse.Code & "','" & g.Products.Code & "','','S','','3001','12022300','" & IIf(g.QtyDiff > 0, "Z13", "Z14") & "','','N',getdate(),'" & Username & "',getdate(),'N',convert(datetime,'99991231'))")
                            End If
                            i = i + 1
                        Next
                    End If
                End Using
                Cursor = Cursors.Default
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btClear_Click(sender, e)
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btItm_Click(sender As Object, e As EventArgs) Handles btItm.Click
        bt_Num = 55
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbItm.Text
        frmSearch.ShowDialog()
        ItmID = s_ID
        tbItm.Text = s_Code
        tbItmDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

    Private Sub tbItm_Leave(sender As Object, e As EventArgs) Handles tbItm.Leave
        If tbItm.Text <> "" Then
            Dim da = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbItm.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 55
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbItm.Text.Trim
                frmSearch.ShowDialog()
                ItmID = s_ID
                tbItm.Text = s_Code
                tbItmDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    ItmID = a.Id
                    tbItm.Text = a.Code
                    tbItmDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbItm.SelectAll()
                tbItm.Focus()
            End If
        Else
            tbItmDesc.Clear()
        End If
    End Sub

    Private Sub tbWH_Leave(sender As Object, e As EventArgs) Handles tbWH.Leave
        If tbWH.Text <> "" Then
            Dim da = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbWH.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 38
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
        bt_Num = 38
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWH.Text
        frmSearch.ShowDialog()
        WHId = s_ID
        tbWH.Text = s_Code
        tbWHDesc.Text = s_Desc
        tbItm.Focus()
    End Sub

End Class