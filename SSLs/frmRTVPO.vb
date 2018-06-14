Public Class frmRTVPO

    Public dt As New List(Of RcvHeader)
    Public dtRcv As New List(Of RcvDetails)
    Dim RcvHdId, RcvId, OwnerID, RcvLocID As Integer
    Private WithEvents songsDataGridView As New System.Windows.Forms.DataGridView
    Dim ckBox As New CheckBox()
    Dim width_columcheckbox As Double = 50

    Private Sub frmRTVPO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                dt = db.RcvHeader.Include("Owners").Include("Vendor").Include("RcvType").Where(Function(x) x.Enable = True And x.PONumber IsNot Nothing And x.PONumber <> "-" And x.ConfirmDate IsNot Nothing).ToList
                dtRcv = db.RcvDetails.Include("ProductDetails.Products").Include("ProductDetails.ProductUnit").Include("ItemStatus").Include("Warehouse").Include("ProductDetails.Products.Zone").Where(Function(x) x.Enable = True).ToList
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, c.FKOwner, .เลขที่เอกสาร = c.DocumentNo, .วันที่เอกสาร = Format(c.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RefNo, .วันที่อ้างอิง = Format(c.RefDate, "dd/MM/yyyy HH:mm"), .เลขที่ใบสั่งซื้อ = c.PONumber, .Owner = c.Owners.Code, .Vender = c.Vendor.Code & "-" & c.Vendor.Name, .ประเภทรับ = c.RcvType.Code, .หมายเหตุ = c.Description,
                .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView2
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End Using
            DataGridView1.DataSource = Nothing
            btProcess.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

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

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid2 = (From c In dt Order By c.Id Where c.DocumentNo.Contains(tbSearch.Text) Or c.RefNo.Contains(tbSearch.Text) Or c.PONumber.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, .เลขที่เอกสาร = c.DocumentNo, .วันที่เอกสาร = Format(c.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RefNo, .วันที่อ้างอิง = Format(c.RefDate, "dd/MM/yyyy HH:mm"), .เลขที่ใบสั่งซื้อ = c.PONumber, .Owner = c.Owners.Code, .Vender = c.Vendor.Code & "-" & c.Vendor.Name, .ประเภทรับ = c.RcvType.Code, .หมายเหตุ = c.Description,
                .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("FKOwner").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData2()
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

            Dim dtGrid = (From c In dtRcv Order By c.Id Where c.Enable = True And c.FKRcvHeader = RcvHdId Select New With {c.Id, .คลัง = c.Warehouse.Code, .โซนเก็บ = c.ProductDetails.Products.Zone.Code, c.POITEM, .รหัสสินค้า = c.ProductDetails.Products.Code, .Barcode = c.ProductDetails.Code, .ชื่อสินค้า = c.ProductDetails.Products.Name, .หน่วย = c.ProductDetails.ProductUnit.Name, .บรรจุ = Format(c.ProductDetails.PackSize, "#,##0.00"), c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.Quantity, "#,##0.00"), c.ProductDetails.Products.FKZone}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns("FKZone").Visible = False
                .Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            RcvHdId = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            OwnerID = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            SelData2()
            btProcess.Enabled = True
        End If
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

End Class