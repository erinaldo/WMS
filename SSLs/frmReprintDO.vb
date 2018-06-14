Public Class frmReprintDO

    Public dt As New List(Of V_PickOrderPosted)

    Private Sub frmReprintDO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Wh = (From c In stWarehouse.Instance.WH Order By c.Code, c.Name Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        ComboBox1.DataSource = Wh
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.SelectedIndex = -1
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Dim dtGrid2 = (From c In dt Order By c.Id Where c.OrderNo.Contains(tbSearch.Text) Or c.BatchNo.Contains(tbSearch.Text) Or c.OwnCode.Contains(tbSearch.Text) Or c.CustCode.Contains(tbSearch.Text) Or c.CustName.Contains(tbSearch.Text) Or c.PickTypeName.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, c.OrderNo, .OrderDate = Format(c.OrderDate, "dd/MM/yyyy HH:mm"), .BatchNo = c.BatchNo, .Owner = c.OwnCode, .สาขา = c.CustCode & "-" & c.CustName, .ประเภท = c.PickTypeName, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).Take(1000).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
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
                Dim intWh As Integer = ComboBox1.SelectedValue
                dt = db.V_PickOrderPosted.Where(Function(x) x.FKCompany = CompID And x.FKWarehouse = intWh).ToList
                Dim dtGrid2 = (From c In dt Order By c.Id Where c.OrderNo.Contains(tbSearch.Text) Or c.BatchNo.Contains(tbSearch.Text) Or c.OwnCode.Contains(tbSearch.Text) Or c.CustCode.Contains(tbSearch.Text) Or c.CustName.Contains(tbSearch.Text) Or c.PickTypeName.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, c.OrderNo, .OrderDate = Format(c.OrderDate, "dd/MM/yyyy HH:mm"), .BatchNo = c.BatchNo, .Owner = c.OwnCode, .สาขา = c.CustCode & "-" & c.CustName, .ประเภท = c.PickTypeName, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).Take(1000).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
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
        frmrptPrintDO.Show()
        Cursor = Cursors.Default
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
End Class