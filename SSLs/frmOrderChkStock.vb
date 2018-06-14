Imports System.Globalization
Imports System.Threading

Public Class frmOrderChkStock

    Dim stkChk As Boolean = True
    Dim prodID, DtlID As Integer
    Dim OldQty As Double

    Private Sub frmOrderChkStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
    End Sub

    Public Sub SelData()
        Cursor = Cursors.WaitCursor
        stkChk = True
        Using db = New PTGwmsEntities
            Dim chk = (From c In frmOrderManage.dtDTL Where c.ConfirmBy = "Y" Group c By c.PickOrderHD.FKPickingType, c.PickOrderHD.FKCompany, c.PickOrderHD.FKOwner, c.FKWarehouse, c.ProductDetails.Products.Id, c.FKItemStatus, คลังสินค้า = c.Warehouse.Code, รหัสสินค้า = c.ProductDetails.Products.Code, Barcode = c.ProductDetails.Products.BaseBarcode, ชื่อสินค้า = c.ProductDetails.Products.Name, สถานะ = c.ItemStatus.Code Into จำนวนขอเบิก = Sum(c.Qty * c.ProductDetails.PackSize), ยืนยันเบิก = Sum(c.QtyConfirm * c.ProductDetails.PackSize)).ToList
            Dim chkJoin = (From c In chk Join d In db.StockOnhand On c.FKCompany Equals d.FKCompany And c.FKOwner Equals d.FKOwner And c.FKWarehouse Equals d.FKWarehouse And c.Id Equals d.FKProduct And c.FKItemStatus Equals d.FKItemStatus
                           Order By c.Barcode Select New With {c.FKCompany, c.FKOwner, c.FKWarehouse, c.Id, c.FKItemStatus, c.คลังสินค้า, c.รหัสสินค้า, c.Barcode, c.ชื่อสินค้า, c.สถานะ, .จำนวนขอเบิก = Format(c.จำนวนขอเบิก, "#,##0.00"), .ยืนยันเบิก = Format(c.ยืนยันเบิก, "#,##0.00"), .Stock = d.Qty - d.BookQty, .diff = (d.Qty - d.BookQty) - c.ยืนยันเบิก, c.FKPickingType}).ToList
            If chkJoin.Count > 0 Then
                With DataGridView1
                    .DataSource = chkJoin
                    .Columns("FKCompany").Visible = False
                    .Columns("FKOwner").Visible = False
                    .Columns("FKWarehouse").Visible = False
                    .Columns("Id").Visible = False
                    .Columns("FKItemStatus").Visible = False
                    .Columns("FKPickingType").Visible = False
                    .Columns("diff").Visible = False
                    .AutoResizeColumns()
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                    For i As Integer = 0 To .Rows.Count - 1
                        For ColNo As Integer = 13 To 13
                            If .Rows(i).Cells(ColNo).Value < 0 Then
                                'If .Rows(i).Cells(12).Value > 0 Then
                                .Rows(i).Cells(ColNo - 1).Style.BackColor = Color.OrangeRed
                                stkChk = False
                                'End If
                            End If
                        Next
                    Next

                End With
            End If
            btEditQty.Enabled = False
            tbQty.ReadOnly = True
        End Using
        Cursor = Cursors.Default
    End Sub

    Sub SelData2()
        Try
            Dim dtGrid = (From c In frmOrderManage.dtDTL Where c.ConfirmBy = "Y" And c.ProductDetails.Products.Id = prodID Select New With {c.Id, .เลขที่เอกสาร = c.PickOrderHD.OrderNo, .วันที่เอกสาร = c.PickOrderHD.OrderDate.ToString("dd/MM/yyyy"), .Owner = c.PickOrderHD.Owners.Code, .Customer = c.PickOrderHD.Customers.Code & "-" & c.PickOrderHD.Customers.Name, .จำนวนขอเบิก = c.Qty, .ยืนยันเบิก = Format(c.QtyConfirm, "#,##0.00"), .หน่วย = c.ProductDetails.ProductUnit.Code}).ToList
            With DataGridView2
                .DataSource = dtGrid
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            btEditQty.Enabled = False
            tbQty.ReadOnly = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            prodID = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            SelData2()
            btProcess.Enabled = True
            btEditQty.Enabled = False
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btProcess_Click(sender As Object, e As EventArgs) Handles btProcess.Click
        Cursor = Cursors.WaitCursor
        SelData()
        If stkChk = False Then
            Cursor = Cursors.Default
            MessageBox.Show("Stock ไม่พอ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        Else
            If MessageBox.Show("ต้องการยืนยันการเบิกสินค้า ใช่หรือไม่?", "ยืนยันการการเบิก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
                Try
                    Cursor = Cursors.WaitCursor
                    Using db = New PTGwmsEntities
                        db.BatchRunning.Add(New BatchRunning With {.FKCompany = CompID, .CreateDate = DateTimeServer(), .CreateBy = Username})
                        db.SaveChanges()
                        Dim batch = (From c In db.BatchRunning Order By c.Id Descending Where c.FKCompany = CompID And c.CreateBy = Username).FirstOrDefault
                        batchID = batch.FKCompany & Format(batch.Id, "00000000")
                        For Each dr As DataGridViewRow In DataGridView1.Rows
                            If dr.Cells(12).Value > 0 Then
                                db.IssueSum.Add(New IssueSum With {.BatchNo = batchID, .FKCompany = dr.Cells(0).Value, .FKOwner = dr.Cells(1).Value, .FKWarehouse = dr.Cells(2).Value, .FKProduct = dr.Cells(3).Value, .FKItemStatus = dr.Cells(4).Value, .Qty = CDbl(dr.Cells(11).Value), .FKPickingType = dr.Cells(14).Value, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                Dim Company As Integer = dr.Cells(0).Value
                                Dim Owner As Integer = dr.Cells(1).Value
                                Dim Wh As Integer = dr.Cells(2).Value
                                Dim Prod As Integer = dr.Cells(3).Value
                                Dim Stat As Integer = dr.Cells(4).Value
                                Dim stk = (From c In db.StockOnhand Order By c.Id Descending Where c.FKCompany = Company And c.FKOwner = Owner And c.FKWarehouse = Wh And c.FKProduct = Prod And c.FKItemStatus = Stat).FirstOrDefault
                                If Not IsNothing(stk) Then
                                    If dr.Cells(12).Value = stk.Qty - stk.BookQty Then
                                        stk.BookQty = stk.BookQty + CDbl(dr.Cells(11).Value)
                                        stk.UpdateBy = Username
                                        stk.UpdateDate = DateTimeServer()
                                    Else
                                        SelData()
                                        Cursor = Cursors.Default
                                        MessageBox.Show("กรุณาลองใหม่อีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                End If
                            End If
                        Next
                        For Each row As DataGridViewRow In frmOrderManage.DataGridView2.Rows
                            If row.Cells(0).Value = True Then
                                Dim a As Integer = row.Cells(1).Value
                                Dim sel = (From c In db.PickOrderHD Where c.Enable = True And c.Id = a).FirstOrDefault
                                If Not IsNothing(sel) Then
                                    If sel.ConfirmDate IsNot Nothing Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("รายการนี้ Confirm ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    Else
                                        sel.BatchNo = batchID
                                        sel.ConfirmBy = Username
                                        sel.ConfirmDate = DateTimeServer()
                                        sel.PDAPickedBy = Username
                                        sel.PDAPickedDate = DateTimeServer()
                                    End If
                                Else
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        Next
                        db.SaveChanges()
                    End Using
                    Cursor = Cursors.Default
                    MessageBox.Show("ยืนยันข้อมูลเรียบร้อย", "Confirm Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                    frmOrderManage.SelData()
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            DtlID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            OldQty = DataGridView2.Rows(e.RowIndex).Cells(5).Value
            tbQty.Text = DataGridView2.Rows(e.RowIndex).Cells(6).Value
            tbQty.ReadOnly = False
            btEditQty.Enabled = True
            tbQty.Focus()
        End If
    End Sub

    Private Sub btEditQty_Click(sender As Object, e As EventArgs) Handles btEditQty.Click
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
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim ds = (From c In db.PickOrderDTL Where c.Id = DtlID).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.QtyConfirm = CDbl(tbQty.Text)
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                    End If
                    Dim ds1 = (From c In frmOrderManage.dtDTL Where c.Id = DtlID).FirstOrDefault
                    If Not IsNothing(ds1) Then
                        ds1.QtyConfirm = CDbl(tbQty.Text)
                    End If
                    db.SaveChanges()
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Cursor = Cursors.Default
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData2()
            tbQty.Clear()
        End If
    End Sub

    Private Sub tbQty_KeyDown(sender As Object, e As KeyEventArgs) Handles tbQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            btEditQty.Focus()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SelData()
    End Sub

    Private Sub btClearStock_Click(sender As Object, e As EventArgs) Handles btClearStock.Click
        If MessageBox.Show("ต้องการปรับยอดเป็น 0 ใช่หรือไม่?", "ยืนยันการปรับยอด", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                SelData()
                Using db = New PTGwmsEntities
                    Cursor = Cursors.WaitCursor
                    For Each dr As DataGridViewRow In DataGridView1.Rows
                        If dr.Cells(12).Value = 0 Then
                            Dim Prod As Integer = dr.Cells(3).Value
                            For Each row As DataGridViewRow In frmOrderManage.DataGridView2.Rows
                                If row.Cells(0).Value = True Then
                                    Dim a As Integer = row.Cells(1).Value
                                    Dim sel = (From c In db.PickOrderDTL Where c.Enable = True And c.FKPickOrderHD = a And c.QtyConfirm > 0 And c.ProductDetails.FKProduct = Prod).ToList
                                    For Each pd In sel
                                        pd.QtyConfirm = 0
                                        pd.UpdateBy = Username
                                        pd.UpdateDate = DateTimeServer()
                                    Next
                                    Dim sel2 = (From c In frmOrderManage.dtDTL Where c.Enable = True And c.FKPickOrderHD = a And c.QtyConfirm > 0 And c.ProductDetails.FKProduct = Prod).ToList
                                    For Each pd2 In sel2
                                        pd2.QtyConfirm = 0
                                    Next
                                End If
                            Next
                        End If
                    Next
                    db.SaveChanges()
                End Using
                SelData()
                Cursor = Cursors.Default
                MessageBox.Show("ปรับยอดเรียบร้อย", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
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

End Class