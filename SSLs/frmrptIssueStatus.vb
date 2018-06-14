Imports System.Globalization
Imports System.Threading
Imports Microsoft.Office.Interop

Public Class frmrptIssueStatus

    Public dt As New List(Of V_PickStatus)

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
            Dim dtGrid2 = (From c In dt Order By c.Id Where c.BaseBarcode.Contains(tbSearch.Text) Or c.OwnCode.Contains(tbSearch.Text) Or c.WHCode.Contains(tbSearch.Text) Or c.BatchNo.Contains(tbSearch.Text) Or c.ProdCode.Contains(tbSearch.Text) Or c.ProdName.Contains(tbSearch.Text) Or c.LocName.Contains(tbSearch.Text) Select New With {.Owner = c.OwnCode, .คลัง = c.WHCode, .เลขที่ชุดเบิก = c.BatchNo, .วันที่เบิก = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .รหัสสินค้า = c.ProdCode, .Barcode = c.BaseBarcode, .ชื่อสินค้า = c.ProdName, .สถานะ = c.ItmCode, .Location = c.LocName, .จำนวน = Format(c.Qty, "#,##0.00"), .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับ = Format(c.ReceiveDate, "dd/MM/yyyy"), .วันที่สั่งหยิบ = Format(c.AssignDate, "dd/MM/yyyy HH:mm"), .ผู้สั่งหยิบ = c.AssingBy, .วันที่หยิบ = Format(c.ConfirmDate, "dd/MM/yyyy HH:mm"), .ผู้หยิบ = c.PickBy}).ToList
            With DataGridView1
                .DataSource = dtGrid2
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With DataGridView1
                For i As Integer = 0 To .Rows.Count - 1
                    'For ColNo As Integer = 15 To 16
                    If .Rows(i).Cells(15).Value <> Nothing And .Rows(i).Cells(16).Value <> Nothing Then
                        .Rows(i).Cells(15).Style.BackColor = Color.GreenYellow
                        .Rows(i).Cells(16).Style.BackColor = Color.GreenYellow
                    End If
                    'Next
                Next
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
                If RoleID = 1 Then
                    dt = db.V_PickStatus.Where(Function(x) x.FKCompany = CompID And x.UDate >= strDate And x.UDate <= strDateTo).ToList
                Else
                    dt = db.V_PickStatus.Where(Function(x) x.FKCompany = CompID And frmMain.ow.Contains(x.OwnCode.ToString) And frmMain.wh.Contains(x.WHCode.ToString) And x.UDate >= strDate And x.UDate <= strDateTo).ToList
                End If

                If dt.Count > 0 Then
                    Dim dtGrid2 = (From c In dt Order By c.Id Select New With {.Owner = c.OwnCode, .คลัง = c.WHCode, .เลขที่ชุดเบิก = c.BatchNo, .วันที่เบิก = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .รหัสสินค้า = c.ProdCode, .Barcode = c.BaseBarcode, .ชื่อสินค้า = c.ProdName, .สถานะ = c.ItmCode, .Location = c.LocName, .จำนวน = Format(c.Qty, "#,##0.00"), .วันที่ผลิต = Format(c.ProductDate, "dd/MM/yyyy"), .วันหมดอายุ = Format(c.ExpDate, "dd/MM/yyyy"), .วันที่รับ = Format(c.ReceiveDate, "dd/MM/yyyy"), .วันที่สั่งหยิบ = Format(c.AssignDate, "dd/MM/yyyy HH:mm"), .ผู้สั่งหยิบ = c.AssingBy, .วันที่หยิบ = Format(c.ConfirmDate, "dd/MM/yyyy HH:mm"), .ผู้หยิบ = c.PickBy}).ToList
                    With DataGridView1
                        .DataSource = dtGrid2
                        .AutoResizeColumns()
                        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                        .Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    End With
                End If
            End Using
            With DataGridView1
                For i As Integer = 0 To .Rows.Count - 1
                    For ColNo As Integer = 15 To 16
                        If .Rows(i).Cells(ColNo).Value <> Nothing Then
                            .Rows(i).Cells(ColNo).Style.BackColor = Color.GreenYellow
                        End If
                    Next
                Next
            End With
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

        Dim strDg As String = "R"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "รายงานสถานะการหยิบสินค้า"
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

    Private Sub frmrptIssueStatus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
    End Sub
End Class