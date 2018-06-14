Public Class frmPickingCon

    Dim dt1 As New List(Of IssueSum)
    Dim dtPLoc As New List(Of V_PickLocationSel)
    Dim dtPL As New List(Of PickLocation)
    Dim batchID As String
    Dim plID As Integer

    Private WithEvents songsDataGridView As New System.Windows.Forms.DataGridView
    Dim ckBox As New CheckBox()
    Dim width_columcheckbox As Double = 50

    Private WithEvents songsDataGridView1 As New System.Windows.Forms.DataGridView
    Dim ckBox1 As New CheckBox()
    Dim width_columcheckbox1 As Double = 50

    Private Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        DataGridView2.DataSource = Nothing
        SelData()
    End Sub

    Public Sub SelData()
        Cursor = Cursors.WaitCursor
        Using db = New PTGwmsEntities
            Dim dt As New List(Of IssueSum)
            If RoleID = 1 Then
                dt1 = db.IssueSum.Include("Warehouse").Include("Products").Include("ItemStatus").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing).ToList
                dt = db.IssueSum.Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing).ToList
            Else
                dt1 = db.IssueSum.Include("Warehouse").Include("Products").Include("ItemStatus").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing And frmMain.ow.Contains(x.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
                dt = db.IssueSum.Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate Is Nothing And frmMain.ow.Contains(x.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
            End If
            Dim iss = (From c In dt Group c By c.FKCompany, c.FKOwner, Owner = c.Owners.Code, เลขที่ชุด = c.BatchNo, ConfirmDate = c.CreateDate.ToString("dd/MM/yyyy") Into จำนวน = Sum(c.Qty)).ToList
            With DataGridView2
                .DataSource = iss
                .Columns("FKCompany").Visible = False
                .Columns("FKOwner").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End Using
        DataGridView1.DataSource = Nothing
        btProcess.Enabled = False
        Cursor = Cursors.Default
    End Sub

    Public Sub SelData2()
        Dim iss = (From c In dt1 Where c.BatchNo = batchID Select New With {c.Id, c.FKCompany, c.FKOwner, c.FKWarehouse, c.FKProduct, c.FKItemStatus, .คลังสินค้า = c.Warehouse.Code, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .สถานะ = c.ItemStatus.Code, .จำนวน = c.Qty}).ToList
        If iss.Count > 0 Then
            With DataGridView1
                .DataSource = iss
                .Columns("Id").Visible = False
                .Columns("FKCompany").Visible = False
                .Columns("FKOwner").Visible = False
                .Columns("FKWarehouse").Visible = False
                .Columns("FKProduct").Visible = False
                .Columns("FKItemStatus").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End If
    End Sub

    Private Sub btProcess_Click(sender As Object, e As EventArgs) Handles btProcess.Click
        If MessageBox.Show("ต้องการยืนยันการเบิกสินค้า ใช่หรือไม่?", "ยืนยันการการเบิก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    For Each dr As DataGridViewRow In DataGridView1.Rows
                        Dim RID As Integer = dr.Cells(0).Value
                        Dim Company As Integer = dr.Cells(1).Value
                        Dim Owner As Integer = dr.Cells(2).Value
                        Dim Wh As Integer = dr.Cells(3).Value
                        Dim Prod As Integer = dr.Cells(4).Value
                        Dim Stat As Integer = dr.Cells(5).Value
                        Dim qty As Double = dr.Cells(10).Value
                        Dim loc = (From c In db.CurrentStocks Order By c.ExpDate, c.ReceiveDate, c.Qty - c.BookQty, c.Locations.LocationType.Code Where c.Enable = True And c.Qty > 0 And c.FKCompany = Company And c.FKOwner = Owner And c.FKWarehouse = Wh And c.FKProduct = Prod And c.FKItemStatus = Stat).ToList
                        Do While qty > 0
                            Dim b = (From c In loc Where (c.Qty - c.BookQty) > 0).FirstOrDefault
                            If Not IsNothing(b) Then
                                If qty > (b.Qty - b.BookQty) Then
                                    db.PickLocation.Add(New PickLocation With {.FKIssueSum = RID, .FKCompany = Company, .FKLocation = b.FKLocation, .ProductDate = b.ProductDate, .ExpDate = b.ExpDate, .ReceiveDate = b.ReceiveDate, .FKVendor = b.FKVendor, .Qty = (b.Qty - b.BookQty), .CurStockID = b.Id, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    qty = qty - (b.Qty - b.BookQty)
                                    b.BookQty += (b.Qty - b.BookQty)
                                Else
                                    db.PickLocation.Add(New PickLocation With {.FKIssueSum = RID, .FKCompany = Company, .FKLocation = b.FKLocation, .ProductDate = b.ProductDate, .ExpDate = b.ExpDate, .ReceiveDate = b.ReceiveDate, .FKVendor = b.FKVendor, .Qty = qty, .CurStockID = b.Id, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    b.BookQty += qty
                                    qty = qty - qty
                                End If
                                If b.BookQty < 0 Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("Stock Location ติดลบ Error ID : " & Prod, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                If b.Qty < 0 Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("Stock Location ติดลบ Error ID : " & Prod, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                            Else
                                Cursor = Cursors.Default
                                MessageBox.Show("Stock Location ไม่พอ Error ID : " & Prod, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        Loop
                        Dim d = (From c In db.IssueSum Where Enabled = True And c.Id = RID).FirstOrDefault
                        If Not IsNothing(d) Then
                            If d.ConfirmDate IsNot Nothing Then
                                Cursor = Cursors.Default
                                MessageBox.Show("รายการนี้สั่งเบิกไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            Else
                                d.ConfirmBy = Username
                                d.ConfirmDate = DateTimeServer()
                            End If
                        Else
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                    Next
                    db.SaveChanges()
                End Using
                Cursor = Cursors.Default
                MessageBox.Show("ประมวลผลเรียบร้อย", "Process Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btRefresh_Click(sender, e)
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

    Private Sub dgvUserDetails1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails3_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView3.RowPostPaint
        Using b As New SolidBrush(DataGridView3.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails4_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView4.RowPostPaint
        Using b As New SolidBrush(DataGridView4.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails5_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView5.RowPostPaint
        Using b As New SolidBrush(DataGridView5.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            batchID = DataGridView2.Rows(e.RowIndex).Cells(3).Value
            SelData2()
            btProcess.Enabled = True
        End If
    End Sub

    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        If e.RowIndex <> -1 Then
            'plID = DataGridView3.Rows(e.RowIndex).Cells(0).Value
            btConfirm.Enabled = True
        End If
    End Sub

    Private Sub btRefresh1_Click(sender As Object, e As EventArgs) Handles btRefresh1.Click
        SelDataStat()
    End Sub

    Sub SelDataStat()
        Try
            Cursor = Cursors.WaitCursor
            DataGridView3.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView3.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView3.GetCellDisplayRectangle(0, -1, True)
            ckBox1.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox1.Width / 2)
            rect.Y += 3
            ckBox1.Location = rect.Location
            AddHandler ckBox1.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged1)
            DataGridView3.Controls.Add(ckBox1)
            DataGridView3.Columns(0).Frozen = False
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dtPLoc = db.V_PickLocationSel.Where(Function(x) x.FKCompany = CompID).ToList
                Else
                    dtPLoc = db.V_PickLocationSel.Where(Function(x) x.FKCompany = CompID And frmMain.ow.Contains(x.OwnCode.ToString) And frmMain.wh.Contains(x.WHCode.ToString)).ToList
                End If
                Dim dtGrid3 = (From c In dtPLoc Order By c.Id Select New With {c.Id, .เลขที่ชุด = c.BatchNo, .วันที่ส่งเบิก = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .Owner = c.OwnCode, .คลัง = c.WHCode,
                .รหัสสินค้า = c.ProdCode, .Barcode = c.BaseBarcode, .ชื่อสินค้า = c.ProdName, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับ = Format(c.ReceiveDate, "dd/MM/yyyy"), .สถานะ = c.ItmCode, .Location = c.LocName, .จำนวน = Format(c.Qty, "#,##0.00"), .โซนเก็บ = c.ZCode, .ผู้หยิบสินค้า = c.PickBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView3
                    .DataSource = dtGrid3
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                Label3.Text = "รอหยิบทั้งหมด : " & Format(dtPLoc.Count, "#,##0") & " รายการ"
                Dim dtUser = db.Users.Where(Function(x) x.FKCompany = CompID And x.Enable = True).ToList
                ComboBox2.DataSource = dtUser
                ComboBox2.DisplayMember = "Name"
                ComboBox2.ValueMember = "Id"
            End Using
            btConfirm.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If MessageBox.Show("ต้องการยืนยันการหยิบสินค้า ใช่หรือไม่?", "ยืนยันการหยิบสินค้า", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Try
                    Dim rowID As Integer
                    Dim cnt As Integer = 0
                    For Each row As DataGridViewRow In DataGridView3.Rows
                        rowID = row.Cells(1).Value
                        If row.Cells(0).Value = True Then
                            cnt = cnt + 1
                            If cnt > 1000 Then
                                Cursor = Cursors.Default
                                MessageBox.Show("ทำรายการได้ไม่เกิน 1,000 รายการต่อครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                            Dim a = (From c In db.PickLocation Where c.Enable = True And c.Id = rowID).FirstOrDefault
                            If Not IsNothing(a) Then
                                If a.ConfirmDate IsNot Nothing Then
                                    SelDataStat()
                                    Cursor = Cursors.Default
                                    MessageBox.Show("มีบางรายการยืนแล้ว กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                ElseIf a.PickingUse IsNot Nothing Then
                                    If a.PickingUse <> Username Then
                                        Cursor = Cursors.Default
                                        MessageBox.Show("รายการนี้มีผู้ใช้แล้ว กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                        Exit Sub
                                    End If
                                Else
                                    a.PickingUse = Username
                                End If
                            Else
                                SelDataStat()
                                Cursor = Cursors.Default
                                MessageBox.Show("ไม่พบข้อมูล กรุณาลองใหม่", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                        End If
                    Next
                    If cnt = 0 Then
                        Cursor = Cursors.Default
                        MessageBox.Show("กรุณาเลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    db.SaveChanges()
                    execSQL("exec [wms].[UPDATE_PICKLOC_CURSTOCK] '" & Username & "'")
                    SelDataStat()
                    Cursor = Cursors.Default
                Catch ex As Exception
                    Cursor = Cursors.Default
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    Private Sub btAssignPick_Click(sender As Object, e As EventArgs) Handles btAssignPick.Click
        If MessageBox.Show("ยืนยันการสั่งหยิบสินค้า ใช่หรือไม่?", "ยืนยันการสั่งหยิบสินค้า", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim rowID As Integer
                    Dim cnt As Integer = 0
                    For Each row As DataGridViewRow In DataGridView5.Rows
                        rowID = row.Cells(1).Value
                        If row.Cells(0).Value = True Then
                            cnt = cnt + 1
                            Dim d = (From c In db.PickLocation Where c.Id = rowID).FirstOrDefault
                            If Not IsNothing(d) Then
                                d.AssignDate = DateTimeServer()
                                d.AssingBy = Username
                                d.PickBy = ComboBox1.SelectedValue
                            End If
                        End If
                    Next
                    If cnt = 0 Then
                        MessageBox.Show("กรุณาเลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    db.SaveChanges()
                    Button1_Click(sender, e)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    Private Sub DataGridView4_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellClick
        If e.RowIndex <> -1 Then
            batchID = DataGridView4.Rows(e.RowIndex).Cells(3).Value
            btAssignPick.Enabled = True
            SelDataSel()
        End If
    End Sub

    Private Sub myDgv_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView5.ColumnWidthChanged
        Dim rect As Rectangle = DataGridView5.GetCellDisplayRectangle(0, -1, True)
        ckBox.Size = New Size(14, 14)
        rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
        rect.Y += 3
        ckBox.Location = rect.Location
    End Sub

    Private Sub myDgv_ColumnWidthChanged1(sender As Object, e As DataGridViewColumnEventArgs) Handles DataGridView3.ColumnWidthChanged
        Dim rect As Rectangle = DataGridView3.GetCellDisplayRectangle(0, -1, True)
        ckBox1.Size = New Size(14, 14)
        rect.X = rect.Location.X + (rect.Width / 2) - (ckBox1.Width / 2)
        rect.Y += 3
        ckBox1.Location = rect.Location
    End Sub

    Private Sub ckBox_CheckedChanged()
        Dim i As Integer = 0
        If ckBox.Checked = True Then
            For j As Integer = 0 To Me.DataGridView5.RowCount - 1
                Me.DataGridView5(0, j).Value = True
            Next
        Else
            For j As Integer = 0 To Me.DataGridView5.RowCount - 1
                Me.DataGridView5(0, j).Value = False
            Next
        End If
    End Sub

    Private Sub ckBox_CheckedChanged1()
        Dim i As Integer = 0
        If ckBox1.Checked = True Then
            For j As Integer = 0 To Me.DataGridView3.RowCount - 1
                Me.DataGridView3(0, j).Value = True
            Next
        Else
            For j As Integer = 0 To Me.DataGridView3.RowCount - 1
                Me.DataGridView3(0, j).Value = False
            Next
        End If
    End Sub

    Private Sub myDgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView5.CellContentClick
        If e.ColumnIndex = 0 Then
            If DataGridView5.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                DataGridView5.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            ElseIf DataGridView5.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
                DataGridView5.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
            End If
        End If
    End Sub

    Private Sub myDgv_CellContentClick1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick
        If e.ColumnIndex = 0 Then
            If DataGridView3.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True Then
                DataGridView3.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False
            ElseIf DataGridView3.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
                DataGridView3.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True
            End If
        End If
    End Sub

    Sub SelDataSel()
        Try
            DataGridView5.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView5.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView5.GetCellDisplayRectangle(0, -1, True)
            ckBox.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
            rect.Y += 3
            ckBox.Location = rect.Location
            AddHandler ckBox.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged)
            DataGridView5.Controls.Add(ckBox)
            DataGridView5.Columns(0).Frozen = False
            Dim dtGrid3 = (From c In dtPL Order By c.Id Where c.IssueSum.BatchNo = batchID Select New With {c.Id, .คลัง = c.IssueSum.Warehouse.Code,
                .รหัสสินค้า = c.IssueSum.Products.Code, .Barcode = c.IssueSum.Products.BaseBarcode, .ชื่อสินค้า = c.IssueSum.Products.Name, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับ = Format(c.ReceiveDate, "dd/MM/yyyy"), .สถานะ = c.IssueSum.ItemStatus.Code, .Location = c.Locations.Name, .จำนวน = Format(c.Qty, "#,##0.00"), .โซนเก็บ = c.IssueSum.Products.Zone.Code, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy}).ToList
            With DataGridView5
                .DataSource = dtGrid3
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dtPL = db.PickLocation.Include("IssueSum").Include("IssueSum.Owners").Include("IssueSum.Products").Include("IssueSum.Warehouse").Include("IssueSum.ItemStatus").Include("Locations").Include("IssueSum.Products.Zone").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.IssueSum.ConfirmDate IsNot Nothing And x.ConfirmDate Is Nothing And x.AssignDate Is Nothing).ToList
                Else
                    dtPL = db.PickLocation.Include("IssueSum").Include("IssueSum.Owners").Include("IssueSum.Products").Include("IssueSum.Warehouse").Include("IssueSum.ItemStatus").Include("Locations").Include("IssueSum.Products.Zone").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.IssueSum.ConfirmDate IsNot Nothing And x.ConfirmDate Is Nothing And x.AssignDate Is Nothing And frmMain.ow.Contains(x.IssueSum.Owners.Code.ToString) And frmMain.wh.Contains(x.IssueSum.Warehouse.Code.ToString)).ToList
                End If
                Dim pk = (From c In dtPL Group c By c.FKCompany, c.IssueSum.FKOwner, Owner = c.IssueSum.Owners.Code, เลขที่ชุด = c.IssueSum.BatchNo, ConfirmDate = c.IssueSum.CreateDate.ToString("dd/MM/yyyy") Into จำนวน = Sum(c.Qty)).ToList
                With DataGridView4
                    .DataSource = pk
                    .Columns("FKCompany").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
                Dim dtUser = db.Users.Where(Function(x) x.FKCompany = CompID And x.Enable = True).ToList
                ComboBox1.DataSource = dtUser
                ComboBox1.DisplayMember = "Name"
                ComboBox1.ValueMember = "Id"
                btAssignPick.Enabled = False
                DataGridView5.DataSource = Nothing
                Cursor = Cursors.Default
            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If MessageBox.Show("ยืนยันการสั่งหยิบสินค้า ใช่หรือไม่?", "ยืนยันการสั่งหยิบสินค้า", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim rowID As Integer
                    Dim cnt As Integer = 0
                    For Each row As DataGridViewRow In DataGridView3.Rows
                        rowID = row.Cells(1).Value
                        If row.Cells(0).Value = True Then
                            cnt = cnt + 1
                            Dim d = (From c In db.PickLocation Where c.Id = rowID).FirstOrDefault
                            If Not IsNothing(d) Then
                                d.AssignDate = DateTimeServer()
                                d.AssingBy = Username
                                d.PickBy = ComboBox2.SelectedValue
                            End If
                        End If
                    Next
                    If cnt = 0 Then
                        MessageBox.Show("กรุณาเลือกรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    db.SaveChanges()
                    btRefresh1_Click(sender, e)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
        End If
    End Sub

    Private Sub tbSearch_TextChanged(sender As Object, e As EventArgs) Handles tbSearch.TextChanged
        DataGridView5.Columns.Clear()
        Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
        ColumnCheckBox.Width = width_columcheckbox
        ColumnCheckBox.DataPropertyName = "Select"
        DataGridView5.Columns.Add(ColumnCheckBox)
        Dim rect As Rectangle = DataGridView5.GetCellDisplayRectangle(0, -1, True)
        ckBox.Size = New Size(14, 14)
        rect.X = rect.Location.X + (rect.Width / 2) - (ckBox.Width / 2)
        rect.Y += 3
        ckBox.Location = rect.Location
        AddHandler ckBox.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged)
        DataGridView5.Controls.Add(ckBox)
        DataGridView5.Columns(0).Frozen = False
        Dim dtGrid3 = (From c In dtPL Order By c.Id Where c.IssueSum.BatchNo = batchID And (c.IssueSum.Products.Code.Contains(tbSearch.Text) Or c.IssueSum.Products.BaseBarcode.Contains(tbSearch.Text) Or c.IssueSum.Products.Name.Contains(tbSearch.Text) Or c.Locations.Name.Contains(tbSearch.Text) Or c.IssueSum.Products.Zone.Code.Contains(tbSearch.Text)) Select New With {c.Id, .คลัง = c.IssueSum.Warehouse.Code,
            .รหัสสินค้า = c.IssueSum.Products.Code, .Barcode = c.IssueSum.Products.BaseBarcode, .ชื่อสินค้า = c.IssueSum.Products.Name, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับ = Format(c.ReceiveDate, "dd/MM/yyyy"), .สถานะ = c.IssueSum.ItemStatus.Code, .Location = c.Locations.Name, .จำนวน = Format(c.Qty, "#,##0.00"), .โซนเก็บ = c.IssueSum.Products.Zone.Code, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy}).ToList
        With DataGridView5
            .DataSource = dtGrid3
            .Columns("Id").Visible = False
            .AutoResizeColumns()
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End With
    End Sub

    Private Sub frmPickingCon_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label3.Text = ""
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Try
            Cursor = Cursors.WaitCursor
            DataGridView3.Columns.Clear()
            Dim ColumnCheckBox As New DataGridViewCheckBoxColumn()
            ColumnCheckBox.Width = width_columcheckbox
            ColumnCheckBox.DataPropertyName = "Select"
            DataGridView3.Columns.Add(ColumnCheckBox)
            Dim rect As Rectangle = DataGridView3.GetCellDisplayRectangle(0, -1, True)
            ckBox1.Size = New Size(14, 14)
            rect.X = rect.Location.X + (rect.Width / 2) - (ckBox1.Width / 2)
            rect.Y += 3
            ckBox1.Location = rect.Location
            AddHandler ckBox1.CheckedChanged, New EventHandler(AddressOf ckBox_CheckedChanged1)
            DataGridView3.Controls.Add(ckBox1)
            DataGridView3.Columns(0).Frozen = False
            Dim dtGrid3 = (From c In dtPLoc Order By c.Id Where c.BatchNo.Contains(TextBox1.Text) Or c.OwnCode.Contains(TextBox1.Text) Or c.WHCode.Contains(TextBox1.Text) Or c.ProdCode.Contains(TextBox1.Text) Or c.ProdName.Contains(TextBox1.Text) Or c.BaseBarcode.Contains(TextBox1.Text) Or c.ItmCode.Contains(TextBox1.Text) Or c.LocName.Contains(TextBox1.Text) Or c.ZCode.Contains(TextBox1.Text) Select New With {c.Id, .เลขที่ชุด = c.BatchNo, .วันที่ส่งเบิก = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .Owner = c.OwnCode, .คลัง = c.WHCode,
            .รหัสสินค้า = c.ProdCode, .Barcode = c.BaseBarcode, .ชื่อสินค้า = c.ProdName, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับ = Format(c.ReceiveDate, "dd/MM/yyyy"), .สถานะ = c.ItmCode, .Location = c.LocName, .จำนวน = Format(c.Qty, "#,##0.00"), .โซนเก็บ = c.ZCode, .ผู้หยิบสินค้า = c.PickBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView3
                .DataSource = dtGrid3
                .Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            Label3.Text = "รอหยิบทั้งหมด : " & Format(dtPLoc.Count, "#,##0") & " รายการ"
            btConfirm.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class