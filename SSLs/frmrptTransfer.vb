Imports Microsoft.Office.Interop

Public Class frmrptTransfer

    Public dt As New List(Of TransferDTL)

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
            Dim dtGrid2 = (From c In dt Order By c.Id Where c.TransferHD.DocNo.Contains(tbSearch.Text) Or c.TransferHD.Warehouse.Code.Contains(tbSearch.Text) Or c.Locations.Name.Contains(tbSearch.Text) Or c.Products.Code.Contains(tbSearch.Text) Or c.Products.Name.Contains(tbSearch.Text) Or c.Products.Zone.Code.Contains(tbSearch.Text) Or c.ItemStatus.Code.Contains(tbSearch.Text) Or c.LocationTo.Contains(tbSearch.Text) Select New With {c.Id, c.RowIDCurrStock, .เลขที่โอน = c.TransferHD.DocNo, .วันที่โอน = Format(c.TransferHD.DocDate, "dd/MM/yyyy"), .คลัง = c.TransferHD.Warehouse.Code, .โอนจาก = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .โซนเก็บ = c.Products.Zone.Code, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.QtyFrom, "#,##0.00"), c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), .โอนไปที = c.LocationTo, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid2
                .Columns("Id").Visible = False
                .Columns("RowIDCurrStock").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                If RoleID = 1 Then
                    dt = db.TransferDTL.Where(Function(x) x.Enable = True And x.TransferHD.FKCompany = CompID).ToList
                Else
                    dt = db.TransferDTL.Where(Function(x) x.Enable = True And x.TransferHD.FKCompany = CompID And frmMain.wh.Contains(x.TransferHD.Warehouse.Code.ToString)).ToList
                End If

                If dt.Count > 0 Then
                    Dim dtGrid2 = (From c In dt Order By c.Id Select New With {c.Id, c.RowIDCurrStock, .เลขที่โอน = c.TransferHD.DocNo, .วันที่โอน = Format(c.TransferHD.DocDate, "dd/MM/yyyy"), .คลัง = c.TransferHD.Warehouse.Code, .โอนจาก = c.Locations.Name, .รหัสสินค้า = c.Products.Code, .ชื่อสินค้า = c.Products.Name, .โซนเก็บ = c.Products.Zone.Code, .สถานะ = c.ItemStatus.Code, .จำนวน = Format(c.QtyFrom, "#,##0.00"), c.LotNo, .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับเข้า = Format(c.ReceiveDate, "dd/MM/yyyy"), .โอนไปที = c.LocationTo, .วันที่ยืนยันโอน = Format(c.TransferDate, "dd/MM/yyyy HH:mm"), .ผู้ยืนยันโอน = c.TransferBy}).ToList
                    With DataGridView1
                        .DataSource = dtGrid2
                        .Columns("Id").Visible = False
                        .Columns("RowIDCurrStock").Visible = False
                        .AutoResizeColumns()
                        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                        .Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End With
                End If
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
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(0)

        Dim strDg As String = "Q"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "รายงานการโอนสินค้าภายในคลัง"
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
End Class