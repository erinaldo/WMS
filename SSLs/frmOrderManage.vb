Imports System.Data.Entity
Imports Microsoft.Office.Interop

Public Class frmOrderManage

    Public dtHD As New List(Of PickOrderHD)
    Public dtDTL As New List(Of PickOrderDTL)
    Public OrderID, OwnerID As Integer
    Dim chkNum As Integer = 0
    Private WithEvents songsDataGridView As New System.Windows.Forms.DataGridView
    Dim ckBox As New CheckBox()
    Dim width_columcheckbox As Double = 50

    Private Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        SelData()
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
                If RoleID = 1 Then
                    dtHD = db.PickOrderHD.Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing).ToList
                    dtDTL = db.PickOrderDTL.Include("ItemStatus").Include("Warehouse").Include("ProductDetails.Products").Include("ProductDetails.Products.Zone").Include("ProductDetails.ProductUnit").Include("ProductDetails").Where(Function(x) x.PickOrderHD.FKCompany = CompID And x.Enable = True And x.PickOrderHD.ConfirmDate Is Nothing).ToList
                Else
                    Dim iss = db.PickOrderDTL.Where(Function(x) x.Enable = True And x.PickOrderHD.FKCompany = CompID And x.PickOrderHD.ConfirmDate Is Nothing And frmMain.ow.Contains(x.PickOrderHD.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
                    Dim doc As New List(Of String)
                    If iss.Count > 0 Then
                        For Each a In iss
                            doc.Add(a.PickOrderHD.OrderNo)
                        Next
                    End If
                    dtHD = db.PickOrderHD.Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing And frmMain.ow.Contains(x.Owners.Code.ToString) And doc.Contains(x.OrderNo.ToString)).ToList
                    dtDTL = db.PickOrderDTL.Include("ItemStatus").Include("Warehouse").Include("ProductDetails.Products").Include("ProductDetails.Products.Zone").Include("ProductDetails.ProductUnit").Include("ProductDetails").Where(Function(x) x.PickOrderHD.FKCompany = CompID And x.Enable = True And x.PickOrderHD.ConfirmDate Is Nothing And frmMain.ow.Contains(x.PickOrderHD.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString) And doc.Contains(x.PickOrderHD.OrderNo.ToString)).ToList
                End If
                Dim dtGrid2 = (From c In dtHD Order By c.Id Select New With {c.Id, c.FKOwner, .เลขที่เอกสาร = c.OrderNo, .วันที่เอกสาร = Format(c.OrderDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RefNo, .วันที่อ้างอิง = Format(c.RefDate, "dd/MM/yyyy HH:mm"), .Owner = c.Owners.Code, .Customer = c.Customers.Code & "-" & c.Customers.Name, .ประเภท = c.PickingType.Code, .จำนวน = Format(c.TotalQty, "#,##0.00"), .ราคา = Format(c.TotalAmt, "#,##0.0000"), .หมายเหตุ = c.Description,
                .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView2
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
            DataGridView1.DataSource = Nothing
            btProcess.Enabled = False
            btCancelOrder.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData2()
        Try
            Dim dtGrid = (From c In dtDTL Order By c.Id Where c.Enable = True And c.FKPickOrderHD = OrderID Select New With {c.Id, .คลัง = c.Warehouse.Code, .โซน = c.ProductDetails.Products.Zone.Code, .รหัสสินค้า = c.ProductDetails.Products.Code, .Barcode = c.ProductDetails.Code, .ชื่อสินค้า = c.ProductDetails.Products.Name, .หน่วย = c.ProductDetails.ProductUnit.Name, .บรรจุ = Format(c.ProductDetails.PackSize, "#,##0.00"), .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"),
            c.ProductDetails.Products.FKZone}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns("FKZone").Visible = False
                .Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub frmOrderManage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btProcess.Enabled = False
        btCancelOrder.Enabled = False
    End Sub

    Private Sub dgvUserDetails1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btProcess_Click(sender As Object, e As EventArgs) Handles btProcess.Click
        Dim i As Integer = 0
        Cursor = Cursors.WaitCursor
        For Each row As DataGridViewRow In Me.DataGridView2.Rows
            Dim sel = (From c In dtHD Where c.Id = row.Cells(1).Value).FirstOrDefault
            Dim pid As Integer = row.Cells(1).Value
            If Not IsNothing(sel) Then
                If row.Cells(0).Value = True Then
                    Dim b = (From c In dtDTL Where c.FKPickOrderHD = row.Cells(1).Value).ToList
                    If b.Count > 0 Then
                        For Each a In b
                            a.ConfirmBy = "Y"
                        Next
                    End If
                    sel.ConfirmBy = "Y"
                    i = i + 1
                Else
                    Dim b = (From c In dtDTL Where c.FKPickOrderHD = row.Cells(1).Value).ToList
                    If b.Count > 0 Then
                        For Each a In b
                            a.ConfirmBy = Nothing
                        Next
                    End If
                    sel.ConfirmBy = "N"
                End If
            End If
        Next
        Dim chkPt = (From c In dtDTL Where c.ConfirmBy = "Y" Select c.PickOrderHD.FKPickingType).Distinct.ToList
        If chkPt.Count > 1 Then
            Cursor = Cursors.Default
            MessageBox.Show("ไม่สามารถเลือกหลายประเภทเบิกได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim chkOw = (From c In dtDTL Where c.ConfirmBy = "Y" Select c.PickOrderHD.FKOwner).Distinct.ToList
        If chkOw.Count > 1 Then
            Cursor = Cursors.Default
            MessageBox.Show("ไม่สามารถเลือกหลาย Owner ได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If i > 0 Then
            frmOrderChkStock.ShowDialog()
            frmOrderChkStock.Close()
        Else
            Cursor = Cursors.Default
            MessageBox.Show("กรุณา เลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            i = 0
            Exit Sub
        End If
        i = 0
        Cursor = Cursors.Default
    End Sub

    Private Sub btCancelOrder_Click(sender As Object, e As EventArgs) Handles btCancelOrder.Click
        If MessageBox.Show("ต้องการยืนยัน ยกเลิก Order ใช่หรือไม่?", "ยืนยันการยกเลิก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Dim i As Integer = 0
                Using db = New PTGwmsEntities
                    For Each row As DataGridViewRow In Me.DataGridView2.Rows
                        If row.Cells(0).Value = True Then
                            i = row.Cells(1).Value
                            Dim dt = (From c In db.PickOrderHD Where c.Id = i).ToList
                            For Each hd In dt
                                hd.Enable = False
                                hd.UpdateDate = DateTimeServer()
                                hd.UpdateBy = Username
                                Dim pkLoc = (From c In db.PickOrderDTL Where c.FKPickOrderHD = hd.Id).ToList
                                For Each p In pkLoc
                                    p.Enable = False
                                    p.UpdateDate = DateTimeServer()
                                    p.UpdateBy = Username
                                Next
                            Next
                        End If
                    Next
                    If i = 0 Then
                        Cursor = Cursors.Default
                        MessageBox.Show("กรุณา เลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    db.SaveChanges()
                End Using
                Cursor = Cursors.Default
                MessageBox.Show("ยกเลิก Order เรียบร้อย", "Cancel Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btRefresh_Click(sender, e)
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            OrderID = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            OwnerID = DataGridView2.Rows(e.RowIndex).Cells(2).Value
            SelData2()
            btProcess.Enabled = True
            btCancelOrder.Enabled = True
        End If
    End Sub

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        If e.ColumnIndex = 0 Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    If DataGridView2.Rows(e.RowIndex).Cells(0).Value = True Then
                        Dim ck = (From c In db.PickOrderHD Where c.Id = OrderID).FirstOrDefault
                        If Not IsNothing(ck) Then
                            If ck.SelectUse Is Nothing Then
                                ck.SelectUse = Username
                                db.SaveChanges()
                            Else
                                If ck.SelectUse <> Username Then
                                    DataGridView2.Rows(e.RowIndex).Cells(0).Value = False
                                    Cursor = Cursors.Default
                                    MessageBox.Show("เลขที่ " & ck.OrderNo & " มีผู้ใช้งานแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            End If
                        End If
                    Else
                        Dim ck = (From c In db.PickOrderHD Where c.Id = OrderID).FirstOrDefault
                        If Not IsNothing(ck) Then
                            If ck.SelectUse <> Username Then
                                Exit Sub
                            Else
                                ck.SelectUse = Nothing
                                db.SaveChanges()
                            End If
                        End If
                    End If
                End Using
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
End Class