Public Class frmSearchTtransferIn

    Public dt As New List(Of PickOrderHD)

    Private Sub frmSearchTtransferIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DocID = 0
        SelData()
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Dim dtGrid2 = (From c In dt Order By c.Id Where c.OrderNo.Contains(tbSearch.Text) Or c.BatchNo.Contains(tbSearch.Text) Or c.Owners.Code.Contains(tbSearch.Text) Or c.Customers.Code.Contains(tbSearch.Text) Or c.Customers.Name.Contains(tbSearch.Text) Or c.PickingType.Name.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, c.OrderNo, .OrderDate = Format(c.OrderDate, "dd/MM/yyyy HH:mm"), .BatchNo = c.BatchNo, .Owner = c.Owners.Code, .สาขา = c.Customers.Code & "-" & c.Customers.Name, .ประเภท = c.PickingType.Name, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End Using
            'btConfirm.Enabled = False
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
                    dt = db.PickOrderHD.Include("Owners").Include("Customers").Include("PickingType").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.PostGIDate IsNot Nothing And x.TransferInDate Is Nothing And x.PickingType.Code = "12" And x.Owners.Code = frmReceiveKey.tbVendor.Text).ToList
                Else
                    'Dim iss = db.PickOrderDTL.Where(Function(x) x.PickOrderHD.FKCompany = CompID And x.Enable = True And x.PickOrderHD.TransferInDate Is Nothing And frmMain.ow.Contains(x.PickOrderHD.Owners.Code.ToString) And frmMain.wh.Contains(x.Warehouse.Code.ToString) And x.PickOrderHD.PostGIDate IsNot Nothing And x.PickOrderHD.PickingType.Code = "12" And x.PickOrderHD.Owners.Code = frmReceiveKey.tbVendor.Text).ToList
                    Dim iss = db.PickOrderDTL.Where(Function(x) x.PickOrderHD.FKCompany = CompID And x.Enable = True And x.PickOrderHD.TransferInDate Is Nothing And frmMain.ow.Contains(x.PickOrderHD.Owners.Code.ToString) And frmMain.wh.Contains(x.PickOrderHD.Customers.Code.ToString) And x.PickOrderHD.PostGIDate IsNot Nothing And x.PickOrderHD.PickingType.Code = "12" And x.PickOrderHD.Owners.Code = frmReceiveKey.tbVendor.Text).ToList
                    Dim doc As New List(Of String)
                    If iss.Count > 0 Then
                        For Each a In iss
                            doc.Add(a.PickOrderHD.OrderNo)
                        Next
                    End If
                    dt = db.PickOrderHD.Include("Owners").Include("Customers").Include("PickingType").Where(Function(x) x.FKCompany = CompID And x.Enable = True And x.TransferInDate Is Nothing And x.PostGIDate IsNot Nothing And x.PickingType.Code = "12" And x.Owners.Code = frmReceiveKey.tbVendor.Text And doc.Contains(x.OrderNo.ToString)).ToList
                End If
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, c.FKOwner, c.OrderNo, .OrderDate = Format(c.OrderDate, "dd/MM/yyyy HH:mm"), .BatchNo = c.BatchNo, .Owner = c.Owners.Code, .สาขา = c.Customers.Code & "-" & c.Customers.Name, .ประเภท = c.PickingType.Name, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End Using
            'btConfirm.Enabled = False
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

    'Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
    '    If e.RowIndex <> -1 Then
    '        DocID = DataGridView1.Rows(e.RowIndex).Cells(0).Value
    '    End If
    'End Sub

    Private Sub DataGrid1_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        DocID = DataGridView1.CurrentRow.Cells(0).Value
        Me.Hide()
    End Sub

    Private Sub DataGrid1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DataGridView1.Rows.Count = 0 Then Exit Sub
            DocID = DataGridView1.CurrentRow.Cells(0).Value
            Me.Close()
        End If
    End Sub

End Class