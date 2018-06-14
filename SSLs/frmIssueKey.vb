Imports System.Globalization
Imports System.Threading

Public Class frmIssueKey

    Public dtPd As New List(Of ProductDetails)
    Public OwnID, WHId, PdId, CustID, StatId, TrnID, IssID, ZoneID, CostID, IOID As Integer
    Private Sub frmIssueKey_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        DataGrid1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        btClear_Click(sender, e)
    End Sub

    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        dtDocdate.CustomFormat = "dd/MM/yyyy"
        dtRefDate.CustomFormat = "dd/MM/yyyy"
        dtIssueDate.CustomFormat = "dd/MM/yyyy"
        DataGrid1.Rows.Clear()
        ClearTextBox(GroupBox1)
        ClearTextBox(Me)
        tbDocNo.Text = "ISS" & Format(DateTimeServer(), "yyMMddHHmm")
        tbDocNo.Focus()
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbRefNo.KeyDown, dtRefDate.KeyDown, tbOwner.KeyDown, tbCust.KeyDown, tbTrans.KeyDown, tbIssueType.KeyDown, tbBatch.KeyDown, dtIssueDate.KeyDown, tbWarehouse.KeyDown, tbProduct.KeyDown, tbQty.KeyDown, tbStatus.KeyDown, tbCostCnt.KeyDown, tbIO.KeyDown
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

    Private Sub tbOwner_Leave(sender As Object, e As EventArgs) Handles tbOwner.Leave
        If tbOwner.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbOwner.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 26
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

    Private Sub btOwner_Click(sender As Object, e As EventArgs) Handles btOwner.Click
        bt_Num = 26
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbOwner.Text
        frmSearch.ShowDialog()
        OwnID = s_ID
        tbOwner.Text = s_Code
        tbOwnerDesc.Text = s_Desc
        tbCust.Focus()
    End Sub

    Private Sub tbCust_Leave(sender As Object, e As EventArgs) Handles tbCust.Leave
        If tbCust.Text <> "" Then
            Dim da = (From c In stCustomer.Instance.Cust Order By c.Id Where c.FKOwner = OwnID And c.Code.Contains(tbCust.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 27
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbCust.Text.Trim
                frmSearch.ShowDialog()
                CustID = s_ID
                tbCust.Text = s_Code
                tbCustDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    CustID = a.Id
                    tbCust.Text = a.Code
                    tbCustDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbCust.SelectAll()
                tbCust.Focus()
            End If
        Else
            tbCustDesc.Clear()
        End If
    End Sub

    Private Sub btCust_Click(sender As Object, e As EventArgs) Handles btCust.Click
        bt_Num = 27
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbCust.Text
        frmSearch.ShowDialog()
        CustID = s_ID
        tbCust.Text = s_Code
        tbCustDesc.Text = s_Desc
        tbTrans.Focus()
    End Sub

    Private Sub tbTrans_Leave(sender As Object, e As EventArgs) Handles tbTrans.Leave
        If tbTrans.Text <> "" Then
            Dim da = (From c In stTransport.Instance.Transport Order By c.Id Where c.Code.Contains(tbTrans.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 28
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbTrans.Text.Trim
                frmSearch.ShowDialog()
                TrnID = s_ID
                tbTrans.Text = s_Code
                tbTransDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    TrnID = a.Id
                    tbTrans.Text = a.Code
                    tbTransDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbTrans.SelectAll()
                tbTrans.Focus()
            End If
        Else
            tbTransDesc.Clear()
        End If
    End Sub

    Private Sub btTrans_Click(sender As Object, e As EventArgs) Handles btTrans.Click
        bt_Num = 28
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbTrans.Text
        frmSearch.ShowDialog()
        TrnID = s_ID
        tbTrans.Text = s_Code
        tbTransDesc.Text = s_Desc
        tbIssueType.Focus()
    End Sub

    Private Sub tbIssueType_Leave(sender As Object, e As EventArgs) Handles tbIssueType.Leave
        If tbIssueType.Text <> "" Then
            Dim da = (From c In stPickType.Instance.PickType Order By c.Id Where c.Code.Contains(tbIssueType.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 29
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbIssueType.Text.Trim
                frmSearch.ShowDialog()
                IssID = s_ID
                tbIssueType.Text = s_Code
                tbIssueTypeDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    IssID = a.Id
                    tbIssueType.Text = a.Code
                    tbIssueTypeDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbIssueType.SelectAll()
                tbIssueType.Focus()
            End If
        Else
            tbIssueTypeDesc.Clear()
        End If
    End Sub

    Private Sub btIssueType_Click(sender As Object, e As EventArgs) Handles btIssueType.Click
        bt_Num = 29
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbIssueType.Text
        frmSearch.ShowDialog()
        IssID = s_ID
        tbIssueType.Text = s_Code
        tbIssueTypeDesc.Text = s_Desc
        tbBatch.Focus()
    End Sub

    Private Sub btCostCnt_Click(sender As Object, e As EventArgs) Handles btCostCnt.Click
        bt_Num = 35
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbCostCnt.Text
        frmSearch.ShowDialog()
        CostID = s_ID
        tbCostCnt.Text = s_Code
        tbCostCntDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

    Private Sub tbCostCnt_Leave(sender As Object, e As EventArgs) Handles tbCostCnt.Leave
        If tbCostCnt.Text <> "" Then
            Dim da = (From c In stCostCenter.Instance.CostCnt Order By c.Id Where c.Code.Contains(tbCostCnt.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 35
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbCostCnt.Text.Trim
                frmSearch.ShowDialog()
                CostID = s_ID
                tbCostCnt.Text = s_Code
                tbCostCntDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    CostID = a.Id
                    tbCostCnt.Text = a.Code
                    tbCostCntDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbCostCnt.SelectAll()
                tbCostCnt.Focus()
            End If
        Else
            tbCostCntDesc.Clear()
        End If
    End Sub

    Sub SelDataProd()
        If tbOwner.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbOwner.Focus()
            Exit Sub
        End If
        If tbWarehouse.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWarehouse.Focus()
            Exit Sub
        End If
        Try
            Using db = New PTGwmsEntities
                dtPd = db.ProductDetails.Include("Products").Include("ProductUnit").Include("Products.Zone").Where(Function(x) x.Enable = True And x.Products.Enable = True And x.Products.FKOwner = OwnID And x.Products.Zone.FKWarehouse = WHId).ToList
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btWarehouse_Click(sender As Object, e As EventArgs) Handles btWarehouse.Click
        bt_Num = 30
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
                bt_Num = 30
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
        If tbOwner.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbOwner.Focus()
            Exit Sub
        End If
        bt_Num = 31
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbProduct.Text
        frmSearch.ShowDialog()
        PdId = s_ID
        Dim daa = (From c In dtPd Where c.Id = s_ID Select New With {c.Id, c.Code, .Name = c.Products.Name, .Unit = c.ProductUnit.Name, c.PackSize, c.Products.FKZone, .Zone = c.Products.Zone.Code}).FirstOrDefault
        If Not IsNothing(daa) Then
            tbProduct.Text = daa.Code
            tbProductDesc.Text = daa.Name
            tbUnit.Text = daa.Unit
            tbPacksize.Text = daa.PackSize
            tbZone.Text = daa.Zone
            ZoneID = daa.FKZone
            tbQty.Focus()
        End If
    End Sub

    Private Sub tbProduct_Leave(sender As Object, e As EventArgs) Handles tbProduct.Leave
        If tbProduct.Text <> "" Then
            If tbOwner.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbOwner.Focus()
                Exit Sub
            End If
            If tbWarehouse.Text.Trim = "" Then
                MessageBox.Show("กรุณาป้อน คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWarehouse.Focus()
                Exit Sub
            End If
            Dim da = (From c In dtPd Order By c.Code Where c.Products.Code.Contains(tbProduct.Text.Trim) Or c.Code.Contains(tbProduct.Text.Trim) Select New With {c.Id, c.Code, .Name = c.Products.Name, .Unit = c.ProductUnit.Name, c.PackSize, c.Products.FKZone, .Zone = c.Products.Zone.Code}).ToList
            If da.Count > 1 Then
                bt_Num = 31
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbProduct.Text.Trim
                frmSearch.ShowDialog()
                PdId = s_ID
                Dim daa = (From c In dtPd Where c.Id = s_ID Select New With {c.Id, c.Code, .Name = c.Products.Name, .Unit = c.ProductUnit.Name, c.PackSize, c.Products.FKZone, .Zone = c.Products.Zone.Code}).FirstOrDefault
                If Not IsNothing(daa) Then
                    tbProduct.Text = daa.Code
                    tbProductDesc.Text = daa.Name
                    tbUnit.Text = daa.Unit
                    tbPacksize.Text = daa.PackSize
                    tbZone.Text = daa.Zone
                    ZoneID = daa.FKZone
                End If
            ElseIf da.Count = 1 Then
                For Each a In da
                    PdId = a.Id
                    tbProduct.Text = a.Code
                    tbProductDesc.Text = a.Name
                    tbUnit.Text = a.Unit
                    tbPacksize.Text = a.PackSize
                    tbZone.Text = a.Zone
                    ZoneID = a.FKZone
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbProduct.SelectAll()
                tbProduct.Focus()
            End If
        Else
            tbProductDesc.Clear()
            tbZone.Clear()
            tbUnit.Clear()
            tbPacksize.Clear()
            tbQty.Clear()
        End If
    End Sub

    Private Sub tbStatus_Leave(sender As Object, e As EventArgs) Handles tbStatus.Leave
        If tbStatus.Text <> "" Then
            Dim da = (From c In stItemStatus.Instance.ItemStatus Order By c.Code Where c.Code.Contains(tbStatus.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 32
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
        bt_Num = 32
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbStatus.Text
        frmSearch.ShowDialog()
        StatId = s_ID
        tbStatus.Text = s_Code
        tbIO.Focus()
    End Sub

    Private Sub btIO_Click(sender As Object, e As EventArgs) Handles btIO.Click
        bt_Num = 36
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbIO.Text
        frmSearch.ShowDialog()
        IOID = s_ID
        tbIO.Text = s_Code
        tbRemark.Focus()
    End Sub

    Private Sub tbIO_Leave(sender As Object, e As EventArgs) Handles tbIO.Leave
        If tbIO.Text <> "" Then
            Dim da = (From c In stIOMaster.Instance.IO Order By c.Code Where c.Code.Contains(tbIO.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 36
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbIO.Text.Trim
                frmSearch.ShowDialog()
                IOID = s_ID
                tbIO.Text = s_Code
            ElseIf da.Count = 1 Then
                For Each a In da
                    IOID = a.Id
                    tbIO.Text = a.Code
                Next
            Else
                MessageBox.Show("I/O ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbIO.SelectAll()
                tbIO.Focus()
            End If
        End If
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
        If tbCust.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน ลูกค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCust.Focus()
            Exit Sub
        End If
        If tbTrans.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน บริษัทขนส่ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbTrans.Focus()
            Exit Sub
        End If
        If tbBatch.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน เลขที่ชุด", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbBatch.Focus()
            Exit Sub
        End If
        If tbIssueType.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน ประเภท", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbIssueType.Focus()
            Exit Sub
        End If
        If tbCostCnt.Text.Trim = "" Then
            CostID = 1905
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
                    Dim chk = (From c In db.PickOrderHD Where c.OrderNo = tbDocNo.Text.Trim).FirstOrDefault
                    If Not IsNothing(chk) Then
                        MessageBox.Show("ป้อนเลขที่เอกสารซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbDocNo.Focus()
                        Exit Sub
                    End If
                    Dim PickOrderHD = New PickOrderHD With {.FKCompany = CompID, .OrderNo = tbDocNo.Text, .OrderDate = dtDocdate.Value, .FKCustomer = CustID, .FKCostCenter = CostID, .FKOwner = OwnID, .RefNo = tbRefNo.Text, .RefDate = dtRefDate.Value, .FKPickingType = IssID, .TotalQty = CDbl(tbTotalQty.Text), .Description = tbDesc.Text, .FKTransport = TrnID, .BatchNo = tbBatch.Text, .PickingDate = dtIssueDate.Value,
                                        .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                    For Each dr As DataGridViewRow In DataGrid1.Rows
                        PickOrderHD.PickOrderDTL.Add(New PickOrderDTL With {.FKIOMaster = dr.Cells(14).Value, .FKWarehouse = dr.Cells(12).Value, .FKProductDetail = dr.Cells(9).Value, .FKItemStatus = dr.Cells(10).Value, .Qty = CDbl(dr.Cells(5).Value), .QtyConfirm = CDbl(dr.Cells(5).Value), .Remark = dr.Cells(8).Value, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Next
                    db.PickOrderHD.Add(PickOrderHD)
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
            If tbIO.Text = "" Then
                IOID = 595
            End If
            DataGrid1.Rows.Add(tbWarehouse.Text, tbProduct.Text, tbUnit.Text, tbPacksize.Text, tbZone.Text, tbQty.Text, tbStatus.Text, tbIO.Text, tbRemark.Text, PdId, StatId, tbProductDesc.Text, WHId, ZoneID, IOID)
            SumGrid()
            tbProduct.Clear()
            tbProductDesc.Clear()
            tbUnit.Clear()
            tbPacksize.Clear()
            tbQty.Clear()
            tbStatus.Clear()
            tbIO.Clear()
            tbRemark.Clear()
            tbProduct.Focus()
        End If
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

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        For index As Integer = 0 To DataGrid1.RowCount - 1
            qty += Convert.ToDecimal(DataGrid1.Rows(index).Cells(5).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
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

    Private Sub DataGrid1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGrid1.CellClick
        If e.RowIndex <> -1 Then
            With DataGrid1
                tbProductDesc.Text = .Rows(e.RowIndex).Cells(11).Value.ToString
            End With
        End If
    End Sub

    Private Sub tbQty_Leave(sender As Object, e As EventArgs) Handles tbQty.Leave
        If IsNumeric(tbQty.Text) Then
            tbQty.Text = Format(CDbl(tbQty.Text), "#,##0.00")
        Else
            tbQty.Text = "0.00"
        End If
    End Sub

End Class