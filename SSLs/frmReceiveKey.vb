Imports System.Globalization
Imports System.Threading

Public Class frmReceiveKey

    Public dtPd As New List(Of ProductDetails) 'ProductDetails
    Public dtLoc As New List(Of Locations)
    Public dtUn As New List(Of ProductUnit)
    Public RTId, WHId, PdId, LocId, StatId, VdId, plID, VvID, ZoneID As Integer

    Private Sub frmReceiveKey_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        DataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        btClear_Click(sender, e)
    End Sub

    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        dtDocdate.CustomFormat = "dd/MM/yyyy"
        dtRefDate.CustomFormat = "dd/MM/yyyy"
        dtProductDate.CustomFormat = "dd/MM/yyyy"
        dtProdExp.CustomFormat = "dd/MM/yyyy"
        dtProdExp.Value = DateAdd(DateInterval.Day, 365, DateTimeServer)
        dtRcvDate.CustomFormat = "dd/MM/yyyy"
        DataGrid1.Rows.Clear()
        ClearTextBox(GroupBox1)
        ClearTextBox(Me)
        tbDocNo.Text = "RCV" & Format(DateTimeServer(), "yyMMddHHmm")
        btTransferIn.Enabled = False
        tbRefNo.Enabled = True
        tbDocNo.Focus()
    End Sub

    Sub SelDataProd()
        If tbVendor.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbVendor.Focus()
            Exit Sub
        End If
        If tbWarehouse.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWarehouse.Focus()
            Exit Sub
        End If
        Try
            Using db = New PTGwmsEntities
                dtPd = db.ProductDetails.Include("Products").Include("ProductUnit").Include("Products.Zone").Where(Function(x) x.Enable = True And x.Products.Enable = True And x.Products.FKOwner = VdId And x.Products.Zone.FKWarehouse = WHId).ToList
                'dtPd = db.V_RcvProduct.Where(Function(x) x.FKCompany = CompID And x.FKOwner = VdId).ToList
                dtLoc = db.Locations.Where(Function(x) x.Enable = True And x.FKCompany = CompID And x.FKWarehouse = WHId).ToList
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbRcvType.KeyDown, tbProduct.KeyDown, dtProductDate.KeyDown, tbQty.KeyDown, tbLocation.KeyDown, tbRefNo.KeyDown, tbVendor.KeyDown, tbStatus.KeyDown, tbVen.KeyDown, tbUnitPrice.KeyDown, tbPallet.KeyDown, tbWarehouse.KeyDown, dtRefDate.KeyDown, dtRcvDate.KeyDown, tbLotNo.KeyDown, dtProdExp.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbDesc_KeyDown(sender As Object, e As KeyEventArgs) Handles tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            tbWarehouse.Focus()
        End If
    End Sub

    Private Sub tbVendor_Leave(sender As Object, e As EventArgs) Handles tbVendor.Leave
        If tbVendor.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbVendor.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 18
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbVendor.Text.Trim
                frmSearch.ShowDialog()
                VdId = s_ID
                tbVendor.Text = s_Code
                tbVendorDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    VdId = a.Id
                    tbVendor.Text = a.Code
                    tbVendorDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbVendor.SelectAll()
                tbVendor.Focus()
            End If
        Else
            tbVendorDesc.Clear()
        End If
    End Sub

    Private Sub btVendor_Click(sender As Object, e As EventArgs) Handles btVendor.Click
        bt_Num = 18
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbVendor.Text
        frmSearch.ShowDialog()
        VdId = s_ID
        tbVendor.Text = s_Code
        tbVendorDesc.Text = s_Desc
        tbVen.Focus()
    End Sub

    Private Sub tbVen_Leave(sender As Object, e As EventArgs) Handles tbVen.Leave
        If tbVen.Text <> "" Then
            Dim da = (From c In stVendor.Instance.VD Order By c.Id Where c.Code.Contains(tbVen.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 19
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbVen.Text.Trim
                frmSearch.ShowDialog()
                VvID = s_ID
                tbVen.Text = s_Code
                tbVenDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    VvID = a.Id
                    tbVen.Text = a.Code
                    tbVenDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbVen.SelectAll()
                tbVen.Focus()
            End If
        Else
            tbVenDesc.Clear()
        End If
    End Sub

    Private Sub btVen_Click(sender As Object, e As EventArgs) Handles btVen.Click
        bt_Num = 19
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbVen.Text
        frmSearch.ShowDialog()
        VvID = s_ID
        tbVen.Text = s_Code
        tbVenDesc.Text = s_Desc
        tbRcvType.Focus()
    End Sub

    Private Sub btWarehouse_Click(sender As Object, e As EventArgs) Handles btWarehouse.Click
        bt_Num = 6
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWarehouse.Text
        frmSearch.ShowDialog()
        WHId = s_ID
        tbWarehouse.Text = s_Code
        tbProduct.Focus()
        SelDataProd()
    End Sub

    Private Sub tbWarehouse_Leave(sender As Object, e As EventArgs) Handles tbWarehouse.Leave
        If tbWarehouse.Text <> "" Then
            Dim da = (From c In stWarehouse.Instance.WH Order By c.Code Where c.Code.Contains(tbWarehouse.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 6
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbWarehouse.Text.Trim
                frmSearch.ShowDialog()
                WHId = s_ID
                tbWarehouse.Text = s_Code
                SelDataProd()
            ElseIf da.Count = 1 Then
                For Each a In da
                    WHId = a.Id
                    tbWarehouse.Text = a.Code
                Next
                SelDataProd()
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWarehouse.SelectAll()
                tbWarehouse.Focus()
            End If
        End If
    End Sub

    Private Sub btProduct_Click(sender As Object, e As EventArgs) Handles btProduct.Click
        If tbVendor.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbVendor.Focus()
            Exit Sub
        End If
        If tbWarehouse.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWarehouse.Focus()
            Exit Sub
        End If
        bt_Num = 8
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbProduct.Text
        frmSearch.ShowDialog()
        PdId = s_ID
        Dim daa = (From c In dtPd Where c.Id = s_ID Select New With {c.Id, c.Code, c.Products.Name, .Unit = c.ProductUnit.Code, c.PackSize, c.Products.FKZone, c.Products.ShelfLife, .Zone = c.Products.Zone.Code, c.UnitCost}).FirstOrDefault
        If Not IsNothing(daa) Then
            tbProduct.Text = daa.Code
            tbProductDesc.Text = daa.Name
            tbUnit.Text = daa.Unit
            tbPacksize.Text = daa.PackSize
            tbShelfLife.Text = daa.ShelfLife
            dtProdExp.Value = DateAdd(DateInterval.Day, CDbl(daa.ShelfLife), dtProductDate.Value)
            tbZone.Text = daa.Zone
            ZoneID = daa.FKZone
            tbUnitPrice.Text = daa.UnitCost
            tbQty.Focus()
        End If
    End Sub

    Private Sub tbProduct_Leave(sender As Object, e As EventArgs) Handles tbProduct.Leave
        If tbProduct.Text <> "" Then
            If tbVendor.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbVendor.Focus()
                Exit Sub
            End If
            If tbWarehouse.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWarehouse.Focus()
                Exit Sub
            End If
            Dim da = (From c In dtPd Order By c.Code Where c.Products.Code.Contains(tbProduct.Text.Trim) Or c.Code.Contains(tbProduct.Text.Trim) Select New With {c.Id, c.Code, c.Products.Name, .Unit = c.ProductUnit.Code, c.PackSize, c.Products.FKZone, c.Products.ShelfLife, .Zone = c.Products.Zone.Code, c.UnitCost}).ToList
            If da.Count > 1 Then
                bt_Num = 8
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbProduct.Text.Trim
                frmSearch.ShowDialog()
                PdId = s_ID
                Dim daa = (From c In dtPd Where c.Id = s_ID Select New With {c.Id, c.Code, c.Products.Name, .Unit = c.ProductUnit.Code, c.PackSize, c.Products.FKZone, c.Products.ShelfLife, .Zone = c.Products.Zone.Code, c.UnitCost}).FirstOrDefault
                If Not IsNothing(daa) Then
                    tbProduct.Text = daa.Code
                    tbProductDesc.Text = daa.Name
                    tbUnit.Text = daa.Unit
                    tbPacksize.Text = daa.PackSize
                    tbShelfLife.Text = daa.ShelfLife
                    dtProdExp.Value = DateAdd(DateInterval.Day, CDbl(daa.ShelfLife), dtProductDate.Value)
                    tbZone.Text = daa.Zone
                    ZoneID = daa.FKZone
                    tbUnitPrice.Text = daa.UnitCost
                End If
            ElseIf da.Count = 1 Then
                For Each a In da
                    PdId = a.Id
                    tbProduct.Text = a.Code
                    tbProductDesc.Text = a.Name
                    tbUnit.Text = a.Unit
                    tbPacksize.Text = a.PackSize
                    tbShelfLife.Text = a.ShelfLife
                    dtProdExp.Value = DateAdd(DateInterval.Day, CDbl(a.ShelfLife), dtProductDate.Value)
                    tbZone.Text = a.Zone
                    ZoneID = a.FKZone
                    tbUnitPrice.Text = a.UnitCost
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbProduct.SelectAll()
                tbProduct.Focus()
            End If
        Else
            tbProductDesc.Clear()
            tbShelfLife.Clear()
            tbZone.Clear()
            tbUnit.Clear()
            tbPacksize.Clear()
            tbQty.Clear()
            tbUnitPrice.Clear()
            tbNetPrice.Clear()
        End If
    End Sub

    Private Sub tbNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbQty.KeyPress, tbUnitPrice.KeyPress, tbPallet.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        Dim price As Decimal = 0
        For index As Integer = 0 To DataGrid1.RowCount - 1
            qty += Convert.ToDecimal(DataGrid1.Rows(index).Cells(5).Value)
            price += Convert.ToDecimal(DataGrid1.Rows(index).Cells(7).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
        tbTotalAmt.Text = Format(price, "#,##0.0000")
    End Sub

    Private Sub tbQty_Leave(sender As Object, e As EventArgs) Handles tbQty.Leave
        If IsNumeric(tbQty.Text) Then
            tbQty.Text = Format(CDbl(tbQty.Text), "#,##0.00")
        Else
            tbQty.Text = "0.00"
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGrid1.RowPostPaint
        Using b As New SolidBrush(DataGrid1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGrid1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGrid1.KeyDown
        If e.KeyCode = Keys.Delete Then
            With DataGrid1
                If .Rows.Count = 0 Then Exit Sub
                If MessageBox.Show("ต้องการลบรายการนี้ ใช่หรือไม่?", "ยืนยันการลบ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                    .Rows.Remove(.CurrentRow)
                    tbProductDesc.Clear()
                    SumGrid()
                    tbProduct.SelectAll()
                    tbProduct.Focus()
                End If
            End With
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        Dim IntPallet As Int16
        If tbDocNo.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน เลขที่เอกสาร", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbDocNo.Focus()
            Exit Sub
        End If
        If tbVendor.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน เจ้าของสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbVendor.Focus()
            Exit Sub
        End If
        If tbVen.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Vendor", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbVen.Focus()
            Exit Sub
        End If
        If tbRcvType.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน ประเภท", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbRcvType.Focus()
            Exit Sub
        End If
        If DataGrid1.Rows.Count = 0 Then
            MessageBox.Show("ยังไม่มีรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbProduct.SelectAll()
            tbProduct.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    Dim chkDoc = (From c In db.RcvHeader Where c.DocumentNo = tbDocNo.Text).FirstOrDefault
                    If Not IsNothing(chkDoc) Then
                        MessageBox.Show("ป้อนเลขที่เอกสารซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbDocNo.Focus()
                        Exit Sub
                    End If
                    Dim RcvHeader = New RcvHeader With {.FKCompany = CompID, .DocumentNo = tbDocNo.Text, .DocumentDate = dtDocdate.Value, .ReceiveDate = dtRcvDate.Value, .FKVendor = VvID, .FKOwner = VdId, .RefNo = tbRefNo.Text, .RefDate = dtRefDate.Value, .FKRcvType = RTId, .TotalQty = CDbl(tbTotalQty.Text), .TotalAmt = CDbl(tbTotalAmt.Text), .Description = tbDesc.Text,
                                        .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                    For Each dr As DataGridViewRow In DataGrid1.Rows
                        If dr.Cells(13).Value.ToString = "" Then
                            IntPallet = Nothing
                        Else
                            IntPallet = dr.Cells(13).Value
                        End If
                        RcvHeader.RcvDetails.Add(New RcvDetails With {.FKWarehouse = dr.Cells(20).Value, .FKProductDetail = dr.Cells(15).Value, .FKItemStatus = dr.Cells(18).Value, .Quantity = CDbl(dr.Cells(5).Value), .PriceUnit = CDbl(dr.Cells(6).Value), .NetPrice = CDbl(dr.Cells(7).Value), .LotNo = dr.Cells(8).Value, .ProductDate = dr.Cells(17).Value, .ExpDate = dr.Cells(24).Value, .FKLocation = IIf(dr.Cells(16).Value = 0, Nothing, dr.Cells(16).Value), .Location = IIf(dr.Cells(16).Value = 0, Nothing, dr.Cells(22).Value), .PalletNo = IntPallet,
                                                   .Remark = dr.Cells(14).Value, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Next
                    If tbRcvType.Text = "02" Then
                        Dim hd = (From c In db.PickOrderHD Where c.Id = DocID).FirstOrDefault
                        If Not IsNothing(hd) Then
                            hd.TransferInDate = DateTimeServer()
                            hd.TransferInBy = Username
                        End If
                    End If
                        db.RcvHeader.Add(RcvHeader)
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

    Private Sub btTransferIn_Click(sender As Object, e As EventArgs) Handles btTransferIn.Click
        If tbVendor.Text = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbVendor.SelectAll()
            tbVendor.Focus()
        Else
            frmSearchTtransferIn.ShowDialog()
            If DocID <> 0 Then
                Using db = New PTGwmsEntities
                    Dim hd = (From c In db.PickOrderHD Where c.Id = DocID).FirstOrDefault
                    If Not IsNothing(hd) Then
                        tbRefNo.Text = hd.OrderNo
                        dtRefDate.Value = hd.OrderDate
                        Dim wh = (From c In db.Warehouse Where c.FKCompany = CompID And c.Code = hd.Customers.Code).FirstOrDefault
                        If IsNothing(wh) Then
                            MessageBox.Show("ไม่พบรหัสคลัง " & hd.Customers.Code, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbRefNo.Clear()
                            Exit Sub
                        End If
                        Dim d = (From c In db.PickOrderDTL Where c.FKPickOrderHD = hd.Id And c.QtyConfirm > 0).ToList
                        Dim prdID, OwID As Integer
                        Dim prdCode, prdUnt As String
                        With DataGrid1
                            .Rows.Clear()
                            For i = 0 To d.Count - 1
                                prdCode = d(i).ProductDetails.Products.Code
                                prdUnt = d(i).ProductDetails.ProductUnit.Code
                                OwID = d(i).ProductDetails.Products.FKOwner
                                Dim prd = (From c In db.ProductDetails Where c.Products.FKCompany = CompID And c.Products.FKOwner = OwID And c.Products.Zone.FKWarehouse = wh.Id And c.Products.Code = prdCode And c.ProductUnit.Code = prdUnt).FirstOrDefault
                                If IsNothing(prd) Then
                                    MessageBox.Show("ไม่พบหน่วยนับ ของสินค้า " & prdUnt & "(" & prdCode & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    tbRefNo.Clear()
                                    Exit Sub
                                End If
                                .Rows.Add()
                                prdID = d(i).ProductDetails.FKProduct
                                .Rows(i).Cells(0).Value = wh.Code
                                .Rows(i).Cells(1).Value = prd.Code
                                .Rows(i).Cells(2).Value = prd.ProductUnit.Code
                                .Rows(i).Cells(3).Value = Format(prd.PackSize, "#,##0.00")
                                .Rows(i).Cells(4).Value = prd.Products.Zone.Code
                                .Rows(i).Cells(5).Value = Format(d(i).QtyConfirm, "#,##0.00")
                                .Rows(i).Cells(6).Value = Format(d(i).UnitPrice, "#,##0.0000")
                                .Rows(i).Cells(7).Value = Format(d(i).Amt, "#,##0.0000")
                                .Rows(i).Cells(8).Value = "-"
                                .Rows(i).Cells(9).Value = Format(DateTimeServer, "dd/MM/yyyy")
                                .Rows(i).Cells(10).Value = Format(DateAdd(DateInterval.Day, prd.Products.ShelfLife, DateTimeServer), "dd/MM/yyyy")
                                .Rows(i).Cells(11).Value = d(i).ItemStatus.Code
                                .Rows(i).Cells(12).Value = ""
                                .Rows(i).Cells(13).Value = 0
                                .Rows(i).Cells(14).Value = ""
                                .Rows(i).Cells(15).Value = prd.Id
                                .Rows(i).Cells(16).Value = 0
                                .Rows(i).Cells(17).Value = DateTimeServer()
                                .Rows(i).Cells(18).Value = d(i).FKItemStatus
                                .Rows(i).Cells(19).Value = prd.Products.Name
                                .Rows(i).Cells(20).Value = wh.Id
                                .Rows(i).Cells(21).Value = prd.Products.ShelfLife
                                .Rows(i).Cells(22).Value = ""
                                .Rows(i).Cells(23).Value = prd.Products.FKZone
                                .Rows(i).Cells(24).Value = DateAdd(DateInterval.Day, prd.Products.ShelfLife, DateTimeServer)
                            Next
                        End With
                    End If
                    tbRefNo.Enabled = False
                    SumGrid()
                End Using
            End If
        End If
    End Sub

    Private Sub tbPallet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbPallet.KeyPress
        If tbLocation.Text <> "" Then
            e.Handled = True
        Else
            Select Case Asc(e.KeyChar)
                Case 48 To 57
                    e.Handled = False
                Case 8, 13, 46
                    e.Handled = False
                Case Else
                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub tbUnitPrice_Leave(sender As Object, e As EventArgs) Handles tbUnitPrice.Leave
        If IsNumeric(tbUnitPrice.Text) Then
            tbUnitPrice.Text = Format(CDbl(tbUnitPrice.Text), "#,##0.0000")
        Else
            tbUnitPrice.Text = "0.0000"
        End If
    End Sub

    Private Sub tbQty_TextChanged(sender As Object, e As EventArgs) Handles tbQty.TextChanged
        tbNetPrice.Clear()
        If IsNumeric(tbQty.Text) And IsNumeric(tbUnitPrice.Text) Then
            If CDbl(tbQty.Text) >= 0 And CDbl(tbUnitPrice.Text) >= 0 Then
                tbNetPrice.Text = Format((tbQty.Text) * CDbl(tbUnitPrice.Text), "#,##0.0000")
            End If
        End If
    End Sub

    Private Sub tbUnitPrice_TextChanged(sender As Object, e As EventArgs) Handles tbUnitPrice.TextChanged
        tbNetPrice.Clear()
        If IsNumeric(tbQty.Text) And IsNumeric(tbUnitPrice.Text) Then
            If CDbl(tbQty.Text) >= 0 And CDbl(tbUnitPrice.Text) >= 0 Then
                tbNetPrice.Text = Format((tbQty.Text) * CDbl(tbUnitPrice.Text), "#,##0.0000")
            End If
        End If
    End Sub

    Private Sub tbRemark_KeyDown(sender As Object, e As KeyEventArgs) Handles tbRemark.KeyDown
        If e.KeyCode = Keys.Enter Then
            If tbWarehouse.Text = "" Then
                MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWarehouse.Focus()
                Exit Sub
            End If
            If tbProduct.Text = "" Then
                MessageBox.Show("กรุณาป้อน รหัสสินค้า/Barcode", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbProduct.Focus()
                Exit Sub
            End If
            If tbStatus.Text = "" Then
                MessageBox.Show("กรุณาป้อน สถานะ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbStatus.Focus()
                Exit Sub
            End If
            If tbQty.Text = "" Or IsNumeric(tbQty.Text) = False Or CDbl(tbQty.Text) <= 0 Then
                MessageBox.Show("กรุณาป้อน จำนวน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbQty.Focus()
                Exit Sub
            End If
            If tbPallet.Text <> "" Then
                Dim i As Integer = 0
                For i = 0 To DataGrid1.RowCount - 1
                    If (tbPallet.Text = DataGrid1.Rows(i).Cells(13).Value.ToString) And (tbZone.Text <> DataGrid1.Rows(i).Cells(4).Value.ToString) Then
                        MessageBox.Show("ป้อน รหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbPallet.SelectAll()
                        tbPallet.Focus()
                        Exit Sub
                    End If
                Next
            End If
            DataGrid1.Rows.Add(tbWarehouse.Text, tbProduct.Text, tbUnit.Text, tbPacksize.Text, tbZone.Text, tbQty.Text, tbUnitPrice.Text, tbNetPrice.Text, tbLotNo.Text, dtProductDate.Text, dtProdExp.Text, tbStatus.Text, tbLocation.Text, tbPallet.Text, tbRemark.Text, PdId, LocId, dtProductDate.Value, StatId, tbProductDesc.Text, WHId, tbShelfLife.Text, tbLocation.Text, ZoneID, dtProdExp.Value)
            SumGrid()
            tbProduct.Clear()
            tbProductDesc.Clear()
            tbUnit.Clear()
            tbPacksize.Clear()
            tbQty.Clear()
            tbUnitPrice.Clear()
            tbNetPrice.Clear()
            tbLotNo.Clear()
            dtProductDate.Value = DateTimeServer()
            dtProdExp.Value = DateAdd(DateInterval.Day, 365, DateTimeServer)
            tbShelfLife.Clear()
            tbStatus.Clear()
            tbLocation.Clear()
            tbPallet.Clear()
            tbRemark.Clear()
            LocId = 0
            tbProduct.Focus()
        End If
    End Sub

    Private Sub dtProductDate_Leave(sender As Object, e As EventArgs) Handles dtProductDate.Leave
        If tbProduct.Text <> "" Then
            dtProdExp.Value = DateAdd(DateInterval.Day, CDbl(tbShelfLife.Text), dtProductDate.Value)
        End If
    End Sub

    Private Sub dtProdExp_Leave(sender As Object, e As EventArgs) Handles dtProdExp.Leave
        If tbProduct.Text <> "" Then
            dtProductDate.Value = DateAdd(DateInterval.Day, CDbl(tbShelfLife.Text) * -1, dtProdExp.Value)
        End If
    End Sub

    Private Sub DataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGrid1.CellClick
        If e.RowIndex <> -1 Then
            With DataGrid1
                tbProductDesc.Text = .Rows(e.RowIndex).Cells(19).Value.ToString
            End With
        End If
    End Sub

    Private Sub tbStatus_Leave(sender As Object, e As EventArgs) Handles tbStatus.Leave
        If tbStatus.Text <> "" Then
            Dim da = (From c In stItemStatus.Instance.ItemStatus Order By c.Code Where c.Code.Contains(tbStatus.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 17
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
        bt_Num = 17
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbStatus.Text
        frmSearch.ShowDialog()
        StatId = s_ID
        tbStatus.Text = s_Code
        tbQty.Focus()
    End Sub

    Private Sub tbPallet_Leave(sender As Object, e As EventArgs) Handles tbPallet.Leave
        If tbPallet.Text = "0" Then
            MessageBox.Show("กรุณาป้อนตัวเลขมากกว่า 0 เท่านั้น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbPallet.SelectAll()
            tbPallet.Focus()
        End If
    End Sub

    Private Sub tbLocation_Leave(sender As Object, e As EventArgs) Handles tbLocation.Leave
        If tbLocation.Text <> "" Then
            Dim da = (From c In dtLoc Order By c.Name Where c.FKZone = ZoneID And c.Name.Contains(tbLocation.Text.Trim) Select c.Id, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 9
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
        bt_Num = 9
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbLocation.Text
        frmSearch.ShowDialog()
        LocId = s_ID
        tbLocation.Text = s_Code
        tbPallet.Focus()
    End Sub

    Private Sub btRcvType_Click(sender As Object, e As EventArgs) Handles btRcvType.Click
        bt_Num = 7
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbRcvType.Text
        frmSearch.ShowDialog()
        RTId = s_ID
        tbRcvType.Text = s_Code
        tbRcvTypeDesc.Text = s_Desc
        If tbRcvType.Text = "02" Then
            btTransferIn.Enabled = True
        Else
            btTransferIn.Enabled = False
        End If
        tbRefNo.Focus()
    End Sub

    Private Sub tbRcvType_Leave(sender As Object, e As EventArgs) Handles tbRcvType.Leave
        If tbRcvType.Text <> "" Then
            Dim da = (From c In stRcvType.Instance.RcvType Order By c.Code Where c.Code.Contains(tbRcvType.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 7
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbRcvType.Text.Trim
                frmSearch.ShowDialog()
                RTId = s_ID
                tbRcvType.Text = s_Code
                tbRcvTypeDesc.Text = s_Desc
                If tbRcvType.Text = "02" Then
                    btTransferIn.Enabled = True
                Else
                    btTransferIn.Enabled = False
                End If
            ElseIf da.Count = 1 Then
                For Each a In da
                    RTId = a.Id
                    tbRcvType.Text = a.Code
                    tbRcvTypeDesc.Text = a.Name
                Next
                If tbRcvType.Text = "02" Then
                    btTransferIn.Enabled = True
                Else
                    btTransferIn.Enabled = False
                End If
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbRcvType.SelectAll()
                tbRcvType.Focus()
            End If
        Else
            tbRcvTypeDesc.Clear()
        End If
    End Sub

End Class