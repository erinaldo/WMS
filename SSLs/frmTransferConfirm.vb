Imports Microsoft.Office.Interop

Public Class frmTransferConfirm

    Public dt As New List(Of V_TransferConfirm)

    Private WithEvents songsDataGridView As New System.Windows.Forms.DataGridView
    Dim ckBox As New CheckBox()
    Dim width_columcheckbox As Double = 50

    Private Sub myDgv_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged
        Dim rect As Rectangle = DataGridView1.GetCellDisplayRectangle(0, -1, True)
        ckBox.Size = New Size(14, 14)
        rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
        rect.Y += 3
        ckBox.Location = rect.Location
    End Sub

    Private Sub ckBox_CheckedChanged()
        Dim i As Integer = 0
        If ckBox.Checked = True Then
            For j As Integer = 0 To Me.DataGridView1.RowCount - 1
                Me.DataGridView1(0, j).Value = True
            Next
        Else
            For j As Integer = 0 To Me.DataGridView1.RowCount - 1
                Me.DataGridView1(0, j).Value = False
            Next
        End If
    End Sub

    Private Sub myDgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = 0 Then
            If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            ElseIf DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
                DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
            End If
        End If
    End Sub

    Private Sub btRefresh1_Click(sender As Object, e As EventArgs) Handles btRefresh1.Click
        SelData()
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid2 = (From c In dt Order By c.Id Where c.BaseBarcode.Contains(tbSearch.Text) Or c.DocNo.Contains(tbSearch.Text) Or c.WHCode.Contains(tbSearch.Text) Or c.LocName.Contains(tbSearch.Text) Or c.ProdCode.Contains(tbSearch.Text) Or c.ProdName.Contains(tbSearch.Text) Or c.ZCode.Contains(tbSearch.Text) Or c.ItmCode.Contains(tbSearch.Text) Or c.LocationTo.Contains(tbSearch.Text) Select New With {c.Id, c.RowIDCurrStock, .เลขที่โอน = c.DocNo, .วันที่โอน = Format(c.DocDate, "dd/MM/yyyy"), .คลัง = c.WHCode, .รหัสสินค้า = c.ProdCode, .Barcode = c.BaseBarcode, .ชื่อสินค้า = c.ProdName, .โซนเก็บ = c.ZCode, .สถานะ = c.OldItemStatus, .จำนวน = Format(c.QtyFrom, "#,##0.00"), .โอนจาก = c.LocName, .โอนไปที = c.LocationTo, .โอนสถานะไปที่ = c.ItmCode, c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("RowIDCurrStock").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            DataGridView1.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView1.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView1.GetCellDisplayRectangle(0, -1, True)
            ckBox.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
            rect.Y += 3
            ckBox.Location = rect.Location
            AddHandler ckBox.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged)
            DataGridView1.Controls.Add(ckBox)
            DataGridView1.Columns(0).Frozen = False
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dt = db.V_TransferConfirm.Where(Function(x) x.FKCompany = CompID).ToList
                Else
                    dt = db.V_TransferConfirm.Where(Function(x) x.FKCompany = CompID And frmMain.wh.Contains(x.WHCode.ToString)).ToList
                End If
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, c.RowIDCurrStock, .เลขที่โอน = c.DocNo, .วันที่โอน = Format(c.DocDate, "dd/MM/yyyy"), .คลัง = c.WHCode, .รหัสสินค้า = c.ProdCode, .Barcode = c.BaseBarcode, .ชื่อสินค้า = c.ProdName, .โซนเก็บ = c.ZCode, .สถานะ = c.OldItemStatus, .จำนวน = Format(c.QtyFrom, "#,##0.00"), .โอนจาก = c.LocName, .โอนไปที = c.LocationTo, .โอนสถานะไปที่ = c.ItmCode, c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("RowIDCurrStock").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
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

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        Cursor = Cursors.WaitCursor
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(0)

        Dim strDg As String = "S"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "รายการโอนสินค้าภายในคลังที่ยังไม่ยืนยัน"
        xlApp.ActiveSheet.Range("A1:" & strDg & "1").Merge()
        xlApp.ActiveSheet.Range("A1").HorizontalAlignment = Excel.Constants.xlCenter

        xlApp.ActiveSheet.Cells(2, 1) = "ค้นหาโดย : " & tbSearch.Text
        xlApp.ActiveSheet.Range("A2:" & strDg & "2").Merge()
        xlApp.ActiveSheet.Range("A2").HorizontalAlignment = Excel.Constants.xlCenter

        xlApp.ActiveSheet.Cells(3, 1) = "วันที่ออกรายงาน : " & Now.ToString("dd/MM/yyyy HH:mm") & ""
        xlSheet.Range("A3:" & strDg & "3").Merge()
        xlApp.ActiveSheet.Range("A3").HorizontalAlignment = Excel.Constants.xlCenter

        xlApp.ActiveSheet.Range("A1:" & strDg & "3").Interior.ColorIndex = 15
        xlApp.ActiveSheet.Range("A4:" & strDg & "4").Interior.ColorIndex = 6

        xlApp.ActiveSheet.Cells(4, 1).Value = "No."
        For k = 0 To DataGridView1.Columns.Count - 1 Step 1
            xlApp.ActiveSheet.Cells(4, k + 2).Value = DataGridView1.Columns(k).HeaderText
        Next

        For i = 0 To DataGridView1.Rows.Count - 1 Step 1
            xlApp.ActiveSheet.Cells(i + 5, 1).Value = "'" & i + 1
            For j = 0 To DataGridView1.Columns.Count - 1 Step 1
                xlApp.ActiveSheet.Cells(i + 5, j + 2).Value = "'" & DataGridView1.Rows(i).Cells(j).Value & ""
            Next
        Next
        xlApp.ActiveSheet.Range("A1:" & strDg & "1").FONT.Bold = True
        xlApp.ActiveSheet.Range("A1:" & strDg & "" & DataGridView1.Rows.Count + 4).BORDERS.Weight = 2
        xlApp.ActiveSheet.Columns("A:" & strDg & "").AutoFit()
        MessageBox.Show("ออกรายงานเรียบร้อย", "Report Finish", MessageBoxButtons.OK, MessageBoxIcon.Information)
        xlApp.Application.Visible = True
        xlSheet = Nothing
        xlBook = Nothing
        xlApp = Nothing
        tbSearch_TextChanged(sender, e)
        Cursor = Cursors.Default
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If MessageBox.Show("ต้องการยืนยันการโอน ใช่หรือไม่?", "ยืนยันการจัดเก็บ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Cursor = Cursors.WaitCursor
                    Dim rowID, rowCur As Integer
                    Dim cnt As Integer = 0
                    For Each row As DataGridViewRow In DataGridView1.Rows
                        rowID = row.Cells(1).Value
                        rowCur = row.Cells(2).Value
                        If row.Cells(0).Value = True Then
                            cnt = cnt + 1
                            Dim a = (From c In db.TransferDTL Where c.Id = rowID).FirstOrDefault
                            If Not IsNothing(a) Then
                                Dim chkLo = (From c In db.CurrentStocks Where c.Enable = True And c.FKProduct = a.FKProduct And (c.Qty < 0 Or c.BookQty < 0)).ToList
                                If chkLo.Count > 0 Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("พบรายการสินค้าติดลบใน Stock Location รหัส : " & a.Products.Code, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim lo = (From c In db.CurrentStocks Where c.Id = rowCur).FirstOrDefault 'ตัด Stock Location ต้นทาง
                                If Not IsNothing(lo) Then
                                    Dim vd As Integer = lo.FKVendor
                                    If lo.Qty - a.QtyFrom = 0 Then
                                        lo.Enable = False
                                        lo.UpdateBy = Username
                                        lo.UpdateDate = DateTimeServer()
                                    Else
                                        lo.Qty = lo.Qty - a.QtyFrom
                                        lo.BookQty = lo.BookQty - a.QtyFrom
                                        lo.UpdateBy = Username
                                        lo.UpdateDate = DateTimeServer()
                                    End If
                                    'บวก Stock Location ปลายทาง
                                    Dim tr = (From c In db.CurrentStocks Where c.Enable = True And c.FKVendor = vd And c.Locations.Name = a.LocationTo And c.FKCompany = CompID And c.FKWarehouse = a.TransferHD.FKWarehouse And c.FKProduct = a.FKProduct And c.FKItemStatus = a.FKItemStatus And c.ProductDate = a.ProductDate And c.ExpDate = a.ExpDate And c.ReceiveDate = a.ReceiveDate).FirstOrDefault
                                    If Not IsNothing(tr) Then
                                        tr.Qty = tr.Qty + a.QtyFrom
                                    Else
                                        Dim loc = (From c In db.Locations Where c.FKCompany = a.TransferHD.FKCompany And c.Enable = True And c.FKWarehouse = a.TransferHD.FKWarehouse And c.Name = a.LocationTo).FirstOrDefault
                                        db.CurrentStocks.Add(New CurrentStocks With {.LotNo = a.LotNo, .FKCompany = a.TransferHD.FKCompany, .FKWarehouse = a.TransferHD.FKWarehouse, .FKOwner = a.Products.FKOwner, .FKVendor = lo.FKVendor, .FKLocation = loc.Id, .FKProduct = a.FKProduct, .Qty = a.QtyFrom, .BookQty = 0, .PriceUnit = 0, .NetPrice = 0, .FKProductUnit = a.FKProductUnit, .ProductDate = a.ProductDate, .ExpDate = a.ExpDate, .ReceiveDate = a.ReceiveDate, .FKItemStatus = a.FKItemStatus, .PalletCode = "-", .SourceConfirm = "Manual", .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                        db.SaveChanges()
                                    End If
                                    If a.ItemStatus.Code <> a.OldItemStatus Then
                                        Dim balQty, balVal, balCost, GLCost As Double
                                        Dim onh = (From c In db.StockOnhand Where c.Enable = True And c.FKCompany = a.TransferHD.FKCompany And c.FKWarehouse = a.TransferHD.FKWarehouse And c.FKOwner = a.Products.FKOwner And c.ItemStatus.Code = a.OldItemStatus And c.FKProduct = a.FKProduct).FirstOrDefault
                                        If Not IsNothing(onh) Then
                                            balQty = onh.Qty
                                            balCost = onh.QtyCost
                                            balVal = onh.NetPrice
                                            GLCost = a.QtyFrom * balCost
                                            onh.Qty = onh.Qty - a.QtyFrom
                                            onh.BookQty = onh.BookQty - a.QtyFrom
                                            onh.NetPrice = onh.NetPrice - (a.QtyFrom * onh.QtyCost)
                                            db.StockCard.Add(New StockCard With {.TransactionDate = a.CreateDate, .DocNo = a.TransferHD.DocNo, .DocDate = a.TransferHD.DocDate,
                                             .Reference = a.TransferHD.DocNo, .Description = "โอนเปลี่ยนสถานะ", .FKCompany = onh.FKCompany, .FKWarehouse = onh.FKWarehouse,
                                             .FKOwner = onh.FKOwner, .FKProduct = a.FKProduct, .FKItemStatus = onh.FKItemStatus, .CostCenter = "-",
                                             .IOCode = "-", .CustCode = "-", .TransType = "-", .OutQty = a.QtyFrom, .OutCost = onh.QtyCost,
                                             .OutValue = a.QtyFrom * onh.QtyCost, .BalQty = balQty - a.QtyFrom, .BalCost = balCost, .BalValue = balVal - (a.QtyFrom * balCost),
                                             .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                        End If
                                        Dim onh1 = (From c In db.StockOnhand Where c.Enable = True And c.FKCompany = a.TransferHD.FKCompany And c.FKWarehouse = a.TransferHD.FKWarehouse And c.FKOwner = a.Products.FKOwner And c.FKItemStatus = a.FKItemStatus And c.FKProduct = a.FKProduct).FirstOrDefault
                                        If Not IsNothing(onh1) Then
                                            db.StockCard.Add(New StockCard With {
                                             .TransactionDate = a.CreateDate, .DocNo = a.TransferHD.DocNo, .DocDate = a.TransferHD.DocDate,
                                             .Reference = a.TransferHD.DocNo, .Description = "โอนเปลี่ยนสถานะ", .FKCompany = onh1.FKCompany, .FKWarehouse = onh1.FKWarehouse,
                                             .FKOwner = onh1.FKOwner, .FKProduct = a.FKProduct, .FKItemStatus = onh1.FKItemStatus,
                                             .InQty = a.QtyFrom, .InCost = onh1.QtyCost, .InValue = a.QtyFrom * onh1.QtyCost, .BalQty = a.QtyFrom + onh1.Qty,
                                             .BalCost = ((a.QtyFrom * onh1.QtyCost) + onh1.NetPrice) / (a.QtyFrom + onh1.Qty), .BalValue = (a.QtyFrom * onh1.QtyCost) + onh1.NetPrice,
                                             .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                            onh1.Qty = onh1.Qty + a.QtyFrom
                                            onh1.NetPrice = onh1.NetPrice + (a.QtyFrom * onh1.QtyCost)
                                        Else
                                            db.StockCard.Add(New StockCard With {
                                             .TransactionDate = a.CreateDate, .DocNo = a.TransferHD.DocNo, .DocDate = a.TransferHD.DocDate,
                                             .Reference = a.TransferHD.DocNo, .Description = "โอนเปลี่ยนสถานะ", .FKCompany = a.TransferHD.FKCompany, .FKWarehouse = a.TransferHD.FKWarehouse,
                                             .FKOwner = a.Products.FKOwner, .FKProduct = a.FKProduct, .FKItemStatus = a.FKItemStatus,
                                             .InQty = a.QtyFrom, .InCost = onh.QtyCost, .InValue = a.QtyFrom * onh.QtyCost, .BalQty = a.QtyFrom,
                                             .BalCost = onh.QtyCost, .BalValue = a.QtyFrom * onh.QtyCost,
                                             .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                            db.StockOnhand.Add(New StockOnhand With {.FKCompany = a.TransferHD.FKCompany, .FKWarehouse = a.TransferHD.FKWarehouse,
                                            .FKOwner = a.Products.FKOwner, .FKProduct = a.FKProduct, .FKItemStatus = a.FKItemStatus,
                                            .Qty = a.QtyFrom, .QtyCost = onh.QtyCost, .NetPrice = a.QtyFrom * onh.QtyCost,
                                            .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                            db.SaveChanges()
                                        End If
                                    End If
                                End If
                                Dim ploc = (From c In db.TransferDTL Where c.Id = rowID).FirstOrDefault
                                If Not IsNothing(ploc) Then
                                    ploc.TransferBy = Username
                                    ploc.TransferDate = DateTimeServer()
                                End If
                            End If
                        End If
                    Next
                    If cnt = 0 Then
                        Cursor = Cursors.Default
                        MessageBox.Show("กรุณาเลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    db.SaveChanges()
                    SelData()
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            'rowID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            'rowCur = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            btConfirm.Enabled = True
        End If
    End Sub
End Class