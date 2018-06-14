Imports System.Globalization
Imports System.Threading
Imports Microsoft.Office.Interop

Public Class frmRptStockCard

    Public dt As New List(Of V_StockCard)

    Private Sub btRefresh1_Click(sender As Object, e As EventArgs) Handles btRefresh1.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือก คลัง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ComboBox1.Focus()
            Exit Sub
        Else
            SelData()
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dtGrid2 = (From c In dt Order By c.Id Where c.WHCode.Contains(tbSearch.Text) Or c.OwnCode.Contains(tbSearch.Text) Or c.DocNo.Contains(tbSearch.Text) Or c.Reference.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text) Or c.ProdCode.Contains(tbSearch.Text) Or c.ProdName.Contains(tbSearch.Text) Or c.ZCode.Contains(tbSearch.Text) Or c.BaseBarcode.Contains(tbSearch.Text) Or c.UpdateBy.Contains(tbSearch.Text) Select New With {.Owner = c.OwnCode, .คลัง = c.WHCode, .PostingDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .เลขที่เอกสาร = c.DocNo, .วันที่เอกสาร = Format(c.DocDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.Reference, .รายละเอียด = c.Description, .รหัสสินค้า = c.ProdCode, .ชื่อสินค้า = c.ProdName, .Barcode = c.BaseBarcode, .โซนเก็บ = c.ZCode, .สถานะ = c.ItmCode, .จำนวนเข้า = Format(c.InQty, "#,##0.00"), .ทุนเข้าต่อหน่วย = Format(c.InCost, "#,##0.0000"), .ทุนเข้ารวม = Format(c.InValue, "#,##0.0000"), .จำนวนออก = Format(c.OutQty, "#,##0.00"), .ทุนออกต่อหน่วย = Format(c.OutCost, "#,##0.0000"), .ทุนออกรวม = Format(c.OutValue, "#,##0.0000"), .จำนวนคงเหลือ = Format(c.BalQty, "#,##0.00"), .ทุนคงเหลือต่อหน่วย = Format(c.BalCost, "#,##0.0000"), .ทุนคงเหลือรวม = Format(c.BalValue, "#,##0.0000"), .หน่วย = c.UntCode, .วันที่ทำรายการ = Format(c.TransactionDate, "dd/MM/yyyy HH:mm"), .CreateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid2
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                Dim strDate As String = Format(dtDate.Value, "yyyyMMdd")
                Dim strDateTo As String = Format(dtDateTo.Value, "yyyyMMdd")
                Dim intWh As Integer = ComboBox1.SelectedValue
                'If RoleID = 1 Then
                dt = db.V_StockCard.Where(Function(x) x.FKCompany = CompID And x.FKWarehouse = intWh And x.TDate >= strDate And x.TDate <= strDateTo And x.ProdCode.Contains(tbProdCode.Text) And x.BaseBarcode.Contains(tbBarcode.Text)).ToList
                'Else
                '    dt = db.V_StockCard.Where(Function(x) x.FKCompany = CompID And x.FKWarehouse = intWh And x.TDate >= strDate And x.TDate <= strDateTo And frmMain.ow.Contains(x.OwnCode.ToString) And frmMain.wh.Contains(x.WHCode.ToString) And x.ProdCode.Contains(tbProdCode.Text) And x.BaseBarcode.Contains(tbBarcode.Text)).ToList
                'End If
                'Dim dtGrid2 = (From c In dt Order By c.Id Select New With {.Owner = c.OwnCode, .คลัง = c.WHCode, .วันที่ทำรายการ = Format(c.TransactionDate, "dd/MM/yyyy HH:mm"), .เลขที่เอกสาร = c.DocNo, .วันที่เอกสาร = Format(c.DocDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.Reference, .รายละเอียด = c.Description, .รหัสสินค้า = c.ProdCode, .ชื่อสินค้า = c.ProdName, .Barcode = c.BaseBarcode, .โซนเก็บ = c.ZCode, .สถานะ = c.ItmCode, .จำนวนเข้า = Format(c.InQty, "#,##0.00"), .ทุนเข้าต่อหน่วย = Format(c.InCost, "#,##0.0000"), .ทุนเข้ารวม = Format(c.InValue, "#,##0.0000"), .จำนวนออก = Format(c.OutQty, "#,##0.00"), .ทุนออกต่อหน่วย = Format(c.OutCost, "#,##0.0000"), .ทุนออกรวม = Format(c.OutValue, "#,##0.0000"), .จำนวนคงเหลือ = Format(c.BalQty, "#,##0.00"), .ทุนคงเหลือต่อหน่วย = Format(c.BalCost, "#,##0.0000"), .ทุนคงเหลือรวม = Format(c.BalValue, "#,##0.0000"), .หน่วย = c.UntCode, .CreateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.UpdateBy}).ToList
                Dim dtGrid2 = (From c In dt Order By c.Id Select New With {.Owner = c.OwnCode, .คลัง = c.WHCode, .PostingDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .เลขที่เอกสาร = c.DocNo, .วันที่เอกสาร = Format(c.DocDate, "dd/MM/yyyy HH:mm"), .เลขที่อ้างอิง = c.Reference, .รายละเอียด = c.Description, .รหัสสินค้า = c.ProdCode, .ชื่อสินค้า = c.ProdName, .Barcode = c.BaseBarcode, .โซนเก็บ = c.ZCode, .สถานะ = c.ItmCode, .จำนวนเข้า = Format(c.InQty, "#,##0.00"), .ทุนเข้าต่อหน่วย = Format(c.InCost, "#,##0.0000"), .ทุนเข้ารวม = Format(c.InValue, "#,##0.0000"), .จำนวนออก = Format(c.OutQty, "#,##0.00"), .ทุนออกต่อหน่วย = Format(c.OutCost, "#,##0.0000"), .ทุนออกรวม = Format(c.OutValue, "#,##0.0000"), .จำนวนคงเหลือ = Format(c.BalQty, "#,##0.00"), .ทุนคงเหลือต่อหน่วย = Format(c.BalCost, "#,##0.0000"), .ทุนคงเหลือรวม = Format(c.BalValue, "#,##0.0000"), .หน่วย = c.UntCode, .วันที่ทำรายการ = Format(c.TransactionDate, "dd/MM/yyyy HH:mm"), .CreateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid2
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    .Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(16).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(17).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(18).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(19).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Columns(20).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
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

        Dim strDg As String = "Z"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "รายงานสินค้าเคลื่อนไหว"
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

    Private Sub frmRptStockCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Dim Wh = (From c In stWarehouse.Instance.WH Order By c.Code, c.Name Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        ComboBox1.DataSource = Wh
        ComboBox1.DisplayMember = "Name"
        ComboBox1.ValueMember = "Id"
        ComboBox1.SelectedIndex = -1
    End Sub
End Class