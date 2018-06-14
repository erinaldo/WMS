Public Class frmPrintRcv

    Public dt As New List(Of RcvHeader)

    Private Sub frmPrintRcv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Dim dtGrid2 = (From c In dt Order By c.Id Where c.DocumentNo.Contains(tbSearch.Text) Or c.DocumentDate.ToString.Contains(tbSearch.Text) Or c.RefNo.Contains(tbSearch.Text) Or c.RefDate.ToString.Contains(tbSearch.Text) Or c.Owners.Code.Contains(tbSearch.Text) Or c.Owners.Name.Contains(tbSearch.Text) Or c.Vendor.Code.Contains(tbSearch.Text) Or c.Vendor.Name.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, .เลขที่เอกสาร = c.DocumentNo, .วันที่เอกสาร = Format(c.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RefNo, .วันที่อ้างอิง = Format(c.RefDate, "dd/MM/yyyy HH:mm"), .Owner = c.Owners.Code & "-" & c.Owners.Name, .Vender = c.Vendor.Code & "-" & c.Vendor.Name, .ประเภทรับ = c.RcvType.Code & "-" & c.RcvType.Name, .จำนวน = Format(c.TotalQty, "#,##0.00"), .ราคา = Format(c.TotalAmt, "#,##0.0000"), .หมายเหตุ = c.Description,
                .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
            btPrint.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dt = db.RcvHeader.Include("Owners").Include("Vendor").Include("RcvType").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.ConfirmDate IsNot Nothing).ToList
                Else
                    Dim Rcv = db.RcvDetails.Where(Function(x) x.RcvHeader.FKCompany = CompID And x.Enable = True And x.RcvHeader.ConfirmDate IsNot Nothing And frmMain.ow.Contains(x.RcvHeader.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString)).ToList
                    Dim doc As New List(Of String)
                    If Rcv.Count > 0 Then
                        For Each a In Rcv
                            doc.Add(a.RcvHeader.DocumentNo)
                        Next
                    End If
                    dt = db.RcvHeader.Where(Function(x) x.FKCompany = CompID And x.Enable = True And frmMain.ow.Contains(x.Owners.Code) And doc.Contains(x.DocumentNo.ToString) And x.ConfirmDate IsNot Nothing).ToList
                End If
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, c.FKOwner, .เลขที่เอกสาร = c.DocumentNo, .วันที่เอกสาร = Format(c.DocumentDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.RefNo, .วันที่อ้างอิง = Format(c.RefDate, "dd/MM/yyyy HH:mm"), .Owner = c.Owners.Code & "-" & c.Owners.Name, .Vender = c.Vendor.Code & "-" & c.Vendor.Name, .ประเภทรับ = c.RcvType.Code & "-" & c.RcvType.Name, .จำนวน = Format(c.TotalQty, "#,##0.00"), .ราคา = Format(c.TotalAmt, "#,##0.0000"), .หมายเหตุ = c.Description,
                .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
            btPrint.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            DocID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            btPrint.Enabled = True
        End If
    End Sub

    Private Sub btPrint_Click(sender As Object, e As EventArgs) Handles btPrint.Click
        Cursor = Cursors.WaitCursor
        frmPrintRptRcv.Show()
        Cursor = Cursors.Default
    End Sub
End Class