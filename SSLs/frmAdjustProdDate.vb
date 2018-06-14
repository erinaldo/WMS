Public Class frmAdjustProdDate

    Public dt As New List(Of V_StockLocation)
    Dim SID, ShelfLife As Integer
    Dim PrdDate, ExpDate As String

    Private Sub frmAdjustProdDate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Wh = (From c In stWarehouse.Instance.WH Order By c.Code, c.Name Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        ComboBox1.DataSource = Wh
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.SelectedIndex = -1
        btSave.Enabled = False
    End Sub

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        Dim bookQty As Decimal = 0
        For index As Integer = 0 To DataGridView1.RowCount - 1
            qty += Convert.ToDecimal(DataGridView1.Rows(index).Cells(14).Value)
            bookQty += Convert.ToDecimal(DataGridView1.Rows(index).Cells(15).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
        tbTotalBookQty.Text = Format(bookQty, "#,##0.00")
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Dim intWh As Integer = ComboBox1.SelectedValue
                If RoleID = 1 Then
                    dt = db.V_StockLocation.Where(Function(x) x.FKCompany = CompID And x.FKWarehouse = intWh And x.Material_Code.Contains(tbProdCode.Text) And x.BaseBarcode.Contains(tbBarcode.Text)).ToList
                Else
                    dt = db.V_StockLocation.Where(Function(x) x.FKCompany = CompID And x.FKWarehouse = intWh And frmMain.ow.Contains(x.OwnCode.ToString) And frmMain.wh.Contains(x.WHCode.ToString) And x.Material_Code.Contains(tbProdCode.Text) And x.BaseBarcode.Contains(tbBarcode.Text)).ToList
                End If
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, .คลัง = c.WHCode, .Owner = c.OwnCode, .รหัสสินค้า = c.Material_Code, .ชื่อสินค้า = c.Material_Description, .Barcode = c.BaseBarcode, .โซนเก็บ = c.ZCode, .สถานะ = c.ItemStatCode, .Location = c.Location, c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.Expire_Date, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), .Aging = DateDiff(DateInterval.Day, Now, c.ReceiveDate) * -1, .จำนวน = Format(c.Qty, "#,##0.00"), .ยอดจอง = Format(c.BookQty, "#,##0.00"), .หน่วย = c.BaseUnitCode, .Vendor = c.VenCode & "-" & c.Vendor, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                SumGrid()
                btSave.Enabled = False
            End Using
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid2 = (From c In dt Order By c.Id Where c.WHCode.Contains(tbSearch.Text) Or c.OwnCode.Contains(tbSearch.Text) Or c.Material_Code.Contains(tbSearch.Text) Or c.Material_Description.Contains(tbSearch.Text) Or c.ZCode.Contains(tbSearch.Text) Or c.Location.Contains(tbSearch.Text) Or c.BaseBarcode.Contains(tbSearch.Text) Or c.UpdateBy.Contains(tbSearch.Text) Select New With {c.Id, .คลัง = c.WHCode, .Owner = c.OwnCode, .รหัสสินค้า = c.Material_Code, .ชื่อสินค้า = c.Material_Description, .Barcode = c.BaseBarcode, .โซนเก็บ = c.ZCode, .สถานะ = c.ItemStatCode, .Location = c.Location, c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.Expire_Date, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), .Aging = DateDiff(DateInterval.Day, Now, c.ReceiveDate) * -1, .จำนวน = Format(c.Qty, "#,##0.00"), .ยอดจอง = Format(c.BookQty, "#,##0.00"), .หน่วย = c.BaseUnitCode, .Vendor = c.VenCode & "-" & c.Vendor, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            SumGrid()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btRefresh1_Click(sender As Object, e As EventArgs) Handles btRefresh1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือก คลัง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Exit Sub
        Else
            SelData()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            Cursor = Cursors.WaitCursor
            With DataGridView1
                SID = .Rows(e.RowIndex).Cells(0).Value
                Dim dtDate = (From c In dt Where c.Id = SID).FirstOrDefault
                If Not IsNothing(dtDate) Then
                    Dim prd = (From c In stProduct.Instance.Prod Where c.Code = dtDate.Material_Code And c.Zone.FKWarehouse = dtDate.FKWarehouse).FirstOrDefault
                    If Not IsNothing(prd) Then
                        ShelfLife = prd.ShelfLife
                        dtProdDate.Value = dtDate.ProductDate
                        dtExpDate.Value = dtDate.Expire_Date
                        PrdDate = dtProdDate.Text
                        ExpDate = dtExpDate.Text
                    Else
                        Cursor = Cursors.Default
                        MessageBox.Show("ไม่พบข้อมูล ShelfLife", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        btSave.Enabled = False
                        Exit Sub
                    End If
                End If
            End With
            Cursor = Cursors.Default
            btSave.Enabled = True
        End If
    End Sub

    Private Sub dtExpDate_Leave(sender As Object, e As EventArgs) Handles dtExpDate.Leave
        dtProdDate.Value = DateAdd(DateInterval.Day, ShelfLife * -1, dtExpDate.Value)
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If dtProdDate.Text = PrdDate And dtExpDate.Text = ExpDate Then
            MessageBox.Show("ยังไม่มีการแก้ไข", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dtProdDate.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    Dim Loc = (From c In db.CurrentStocks Where c.Id = SID).FirstOrDefault
                    If Not IsNothing(Loc) Then
                        db.CurrentStocksLog.Add(New CurrentStocksLog With {.Id = Loc.Id, .FKCompany = Loc.FKCompany, .FKOwner = Loc.FKWarehouse, .FKWarehouse = Loc.FKWarehouse, .FKVendor = Loc.FKVendor, .FKLocation = Loc.FKLocation, .FKProduct = Loc.FKProduct, .Qty = Loc.Qty, .BookQty = Loc.BookQty, .PriceUnit = Loc.PriceUnit, .NetPrice = Loc.NetPrice, .FKProductUnit = Loc.FKProductUnit, .LotNo = Loc.LotNo, .ProductDate = Loc.ProductDate, .ExpDate = Loc.ExpDate, .ReceiveDate = Loc.ReceiveDate, .FKItemStatus = Loc.FKItemStatus, .PalletCode = Loc.PalletCode, .ACCTASSCAT = Loc.ACCTASSCAT, .SourceConfirm = Loc.SourceConfirm, .CreateDate = Date.Now, .CreateBy = Username, .UpdateDate = Loc.UpdateDate, .UpdateBy = Loc.UpdateBy, .Enable = True})
                        Loc.ProductDate = dtProdDate.Value
                        Loc.ExpDate = dtExpDate.Value
                        Loc.UpdateDate = DateTimeServer()
                        Loc.UpdateBy = Username
                    End If
                    db.SaveChanges()
                End Using
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btRefresh1_Click(sender, e)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub dtProdDate_Leave(sender As Object, e As EventArgs) Handles dtProdDate.Leave
        dtExpDate.Value = DateAdd(DateInterval.Day, ShelfLife, dtProdDate.Value)
    End Sub

End Class