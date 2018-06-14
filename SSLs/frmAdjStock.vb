Imports System.Globalization
Imports System.Threading

Public Class frmAdjStock

    Public dtCur As New List(Of CurrentStocks)
    Public OwnID, ZoneID, WHId, PdId, StatId, LocId, rowID, ProdSL As Integer

    Private Sub frmAdjStock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
    End Sub

    Private Sub btClear_Click(sender As Object, e As EventArgs) Handles btClear.Click
        dtDocdate.CustomFormat = "dd/MM/yyyy"
        DataGrid1.Rows.Clear()
        DataGridView2.DataSource = Nothing
        ClearTextBox(GroupBox1)
        ClearTextBox(Me)
        tbDocNo.Focus()
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbDocNo.KeyDown, dtDocdate.KeyDown, tbOwner.KeyDown, tbWH.KeyDown, tbDesc.KeyDown, tbStatus.KeyDown, tbQty.KeyDown, tbLotNo.KeyDown, dtProductDate.KeyDown, dtProdExp.KeyDown, ComboBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbOwner_Leave(sender As Object, e As EventArgs) Handles tbOwner.Leave
        If tbOwner.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbOwner.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 48
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
        bt_Num = 48
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbOwner.Text
        frmSearch.ShowDialog()
        OwnID = s_ID
        tbOwner.Text = s_Code
        tbOwnerDesc.Text = s_Desc
        tbWH.Focus()
    End Sub

    Private Sub tbWH_Leave(sender As Object, e As EventArgs) Handles tbWH.Leave
        If tbWH.Text <> "" Then
            Dim da = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbWH.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 49
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
        bt_Num = 49
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWH.Text
        frmSearch.ShowDialog()
        WHId = s_ID
        tbWH.Text = s_Code
        tbWHDesc.Text = s_Desc
        tbDesc.Focus()
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

    Private Sub tbQty_Leave(sender As Object, e As EventArgs) Handles tbQty.Leave
        If IsNumeric(tbQty.Text) Then
            tbQty.Text = Format(CDbl(tbQty.Text), "#,##0.00")
        Else
            tbQty.Text = "0.00"
        End If
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            rowID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            Dim ds = (From c In dtCur Where c.Id = rowID).FirstOrDefault
            If Not IsNothing(ds) Then
                tbStatus.Text = ds.ItemStatus.Code
                tbQty.Text = ds.Qty
                tbLotNo.Text = ds.LotNo
                dtProductDate.Value = ds.ProductDate
                dtProdExp.Value = ds.ExpDate
                ProdSL = ds.Products.ShelfLife
            End If
            tbStatus.Focus()
            btConfirm.Enabled = True
        End If
    End Sub

    Private Sub dtProductDate_Leave(sender As Object, e As EventArgs) Handles dtProductDate.Leave
        dtProdExp.Value = DateAdd(DateInterval.Day, ProdSL, dtProductDate.Value)
    End Sub

    Private Sub dtProdExp_Leave(sender As Object, e As EventArgs) Handles dtProdExp.Leave
        dtProductDate.Value = DateAdd(DateInterval.Day, ProdSL * -1, dtProdExp.Value)
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGrid1.RowPostPaint
        Using b As New SolidBrush(DataGrid1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If tbStatus.Text = "" Then
            MessageBox.Show("กรุณาป้อน สถานะ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbStatus.Focus()
            Exit Sub
        End If
        If ComboBox1.Text = "" Then
            MessageBox.Show("กรุณาป้อนเหตุผล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Exit Sub
        End If
        Dim sel = (From c In dtCur Where c.Id = rowID).FirstOrDefault
        If Not IsNothing(sel) Then
            DataGrid1.Rows.Add(sel.Locations.Name, sel.Products.Code, sel.Products.Name, tbStatus.Text, tbQty.Text, tbLotNo.Text, dtProductDate.Value, dtProdExp.Value, sel.ReceiveDate, sel.PalletCode, ComboBox1.Text, sel.FKLocation, sel.FKProduct, sel.FKItemStatus, sel.FKProductUnit, sel.Id, sel.FKVendor)
            sel.Enable = False
        End If
        SumGrid()
        SelData2()
        tbStatus.Clear()
        tbQty.Clear()
        tbLotNo.Clear()
        dtProductDate.Value = DateTimeServer()
        dtProdExp.Value = DateTimeServer()
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub SumGrid()
        Dim qty As Decimal = 0
        Dim price As Decimal = 0
        For index As Integer = 0 To DataGrid1.RowCount - 1
            qty += Convert.ToDecimal(DataGrid1.Rows(index).Cells(4).Value)
        Next
        tbTotalQty.Text = Format(qty, "#,##0.00")
    End Sub

    Private Sub dgvUserDetails1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub tbStatus_Leave(sender As Object, e As EventArgs) Handles tbStatus.Leave
        If tbStatus.Text <> "" Then
            Dim da = (From c In stItemStatus.Instance.ItemStatus Order By c.Code Where c.Code.Contains(tbStatus.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 50
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
        bt_Num = 50
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbStatus.Text
        frmSearch.ShowDialog()
        StatId = s_ID
        tbStatus.Text = s_Code
        tbQty.Focus()
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
        SelData()
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid2 = (From c In dtCur Order By c.Id Where c.Locations.Name.Contains(tbSearch.Text) Or c.Products.Code.Contains(tbSearch.Text) Or c.Products.Name.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"), c.LotNo,
                    .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), c.PalletCode,
                    .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("FKOwner").Visible = False
                .Columns("FKVendor").Visible = False
                .Columns("FKLocation").Visible = False
                .Columns("FKProduct").Visible = False
                .Columns("FKProductUnit").Visible = False
                .Columns("FKItemStatus").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                dtCur = (From c In db.CurrentStocks.Include("Locations").Include("Products").Include("ItemStatus") Where c.Enable = True And c.FKCompany = CompID And c.FKWarehouse = WHId And c.FKOwner = OwnID And c.Qty > 0).ToList
                If dtCur.Count > 0 Then
                    Dim dtGrid2 = (From c In dtCur Order By c.Id Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"), c.LotNo,
                    .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), c.PalletCode,
                    .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                    With DataGridView2
                        .DataSource = dtGrid2
                        .Columns("Id").Visible = False
                        .Columns("FKOwner").Visible = False
                        .Columns("FKVendor").Visible = False
                        .Columns("FKLocation").Visible = False
                        .Columns("FKProduct").Visible = False
                        .Columns("FKProductUnit").Visible = False
                        .Columns("FKItemStatus").Visible = False
                        .AutoResizeColumns()
                        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                        .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End With
                End If
                Dim dtAdjRs = db.AdjustReason.Where(Function(x) x.FKCompany = CompID And x.Enable = True).ToList
                ComboBox1.DataSource = dtAdjRs
                ComboBox1.DisplayMember = "Name"
                ComboBox1.ValueMember = "Id"
                btConfirm.Enabled = False
                ComboBox1.SelectedIndex = -1
            End Using
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Public Sub SelData2()
        Try
            Cursor = Cursors.WaitCursor
            Dim dtGrid2 = (From c In dtCur Order By c.Id Where c.Enable = True Select New With {c.Id, c.FKOwner, c.FKVendor, c.FKLocation, c.FKProduct, c.FKProductUnit, c.FKItemStatus,
                    .Location = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Qty, "#,##0.00"), c.LotNo,
                    .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), c.PalletCode,
                    .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("FKOwner").Visible = False
                .Columns("FKVendor").Visible = False
                .Columns("FKLocation").Visible = False
                .Columns("FKProduct").Visible = False
                .Columns("FKProductUnit").Visible = False
                .Columns("FKItemStatus").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            btConfirm.Enabled = False
            ComboBox1.SelectedIndex = -1
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

End Class