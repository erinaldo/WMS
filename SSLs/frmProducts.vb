Imports Microsoft.Office.Interop

Public Class frmProducts

    Public SId As Integer
    Public dtPrd As New List(Of Products)

    Private Sub frmProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Own = (From c In stOwner.Instance.Own Order By c.Code, c.Name Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        ComboBox1.DataSource = Own
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.SelectedIndex = -1
        Dim wh = (From c In stWarehouse.Instance.WH Order By c.Code, c.Name Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        ComboBox2.DataSource = wh
        ComboBox2.DisplayMember = "Name"
        ComboBox2.ValueMember = "Id"
        ComboBox2.SelectedIndex = -1
        'SelData()
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        sNew = True
        sEdit = False
        'If ComboBox2.SelectedIndex = -1 Then
        '    MessageBox.Show("กรุณาเลือก คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    ComboBox2.Focus()
        '    Exit Sub
        'End If
        frmProductEdit.ShowDialog()
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid = (From c In dtPrd Where c.Owners.Code.Contains(tbSearch.Text) Or c.Owners.Name.Contains(tbSearch.Text) Or c.Code.Contains(tbSearch.Text) Or c.BaseBarcode.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Or c.NameEng.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text) Select New With {c.Id, c.FKOwner, .OwnerCode = c.Owners.Code, .Owner = c.Owners.Name, .รหัสสินค้า = c.Code, .Barcode = c.BaseBarcode, .ชื่อสินค้า1 = c.Name, .ชื่อสินค้า2 = c.NameEng, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            DataGridView1.DataSource = dtGrid
            DataGridView1.Columns("Id").Visible = False
            DataGridView1.Columns("FKOwner").Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Sub SelData()
    '    Try
    '        Cursor = Cursors.WaitCursor
    '        dtPrd = (From c In stProduct.Instance.Prod Order By c.Id Where c.FKOwner = ComboBox1.SelectedValue And c.Zone.FKWarehouse = ComboBox2.SelectedValue).ToList
    '        Dim dtGrid = (From c In dtPrd Select New With {c.Id, c.FKOwner, .OwnerCode = c.Owners.Code, .Owner = c.Owners.Name, .รหัสสินค้า = c.Code, .Barcode = c.BaseBarcode, .ชื่อสินค้า1 = c.Name, .ชื่อสินค้า2 = c.NameEng, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
    '        With DataGridView1
    '            .DataSource = dtGrid
    '            .Columns("Id").Visible = False
    '            .Columns("FKOwner").Visible = False
    '            .AutoResizeColumns()
    '            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
    '        End With
    '        btEdit.Enabled = False
    '        Cursor = Cursors.Default
    '    Catch ex As Exception
    '        Cursor = Cursors.Default
    '        MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                SId = .Rows(e.RowIndex).Cells(0).Value
            End With
            btEdit.Enabled = True
        End If
    End Sub

    Private Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        Cursor = Cursors.WaitCursor
        sEdit = True
        sNew = False
        frmProductEdit.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        Cursor = Cursors.WaitCursor
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(0)

        Dim strDg As String = "L"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล รายการสินค้า (" & Comp & ")"
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

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือก Owner", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Exit Sub
        Else
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("กรุณาเลือก คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ComboBox2.Focus()
                Exit Sub
            End If
            Try
                Cursor = Cursors.WaitCursor
                dtPrd = (From c In stProduct.Instance.Prod Where c.FKOwner = ComboBox1.SelectedValue And c.Zone.FKWarehouse = ComboBox2.SelectedValue).ToList
                Dim dtGrid = (From c In dtPrd Order By c.Id Select New With {c.Id, c.FKOwner, .OwnerCode = c.Owners.Code, .Owner = c.Owners.Name, .รหัสสินค้า = c.Code, .Barcode = c.BaseBarcode, .ชื่อสินค้า1 = c.Name, .ชื่อสินค้า2 = c.NameEng, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid
                    .Columns("Id").Visible = False
                    .Columns("FKOwner").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
                btEdit.Enabled = False
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
End Class