Imports Microsoft.Office.Interop

Public Class frmLocation

    Public SId As Integer

    Private Sub frmLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        sNew = True
        sEdit = False
        frmLocationEdit.ShowDialog()
    End Sub

    Sub SelData()
        Try
            If RoleID = 1 Then
                Dim dtGrid = (From c In stLocation.Instance.Location Order By c.Warehouse.Code, c.Name Where c.InStorage = True Select New With {c.Id, .WHCode = c.Warehouse.Code, .คลังสินค้า = c.Warehouse.Name, .Location = c.Name, .ZoneCode = c.Zone.Code, .โซน = c.Zone.Name, .TypeCode = c.LocationType.Code, .ประเภท = c.LocationType.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            Else
                Dim dtGrid = (From c In stLocation.Instance.Location Order By c.Warehouse.Code, c.Name Where c.InStorage = True And frmMain.wh.Contains(c.Warehouse.Code.ToString) Select New With {c.Id, .WHCode = c.Warehouse.Code, .คลังสินค้า = c.Warehouse.Name, .Location = c.Name, .ZoneCode = c.Zone.Code, .โซน = c.Zone.Name, .TypeCode = c.LocationType.Code, .ประเภท = c.LocationType.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView1
                    .DataSource = dtGrid
                    .Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                End With
            End If
            btEdit.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            If RoleID = 1 Then
                Dim dtGrid = (From c In stLocation.Instance.Location Order By c.Warehouse.Code, c.Name Where c.InStorage = True And (c.Warehouse.Code.Contains(tbSearch.Text) Or c.Warehouse.Name.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Or c.Zone.Code.Contains(tbSearch.Text) Or c.Zone.Name.Contains(tbSearch.Text) Or c.LocationType.Code.Contains(tbSearch.Text) Or c.LocationType.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text)) Select New With {c.Id, .WHCode = c.Warehouse.Code, .คลังสินค้า = c.Warehouse.Name, .Location = c.Name, .ZoneCode = c.Zone.Code, .โซน = c.Zone.Name, .TypeCode = c.LocationType.Code, .ประเภท = c.LocationType.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                DataGridView1.DataSource = dtGrid
                DataGridView1.Columns("Id").Visible = False
            Else
                Dim dtGrid = (From c In stLocation.Instance.Location Order By c.Warehouse.Code, c.Name Where c.InStorage = True And frmMain.wh.Contains(c.Warehouse.Code.ToString) And (c.Warehouse.Code.Contains(tbSearch.Text) Or c.Warehouse.Name.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Or c.Zone.Code.Contains(tbSearch.Text) Or c.Zone.Name.Contains(tbSearch.Text) Or c.LocationType.Code.Contains(tbSearch.Text) Or c.LocationType.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text)) Select New With {c.Id, .WHCode = c.Warehouse.Code, .คลังสินค้า = c.Warehouse.Name, .Location = c.Name, .ZoneCode = c.Zone.Code, .โซน = c.Zone.Name, .TypeCode = c.LocationType.Code, .ประเภท = c.LocationType.Name, .หมายเหตุ = c.Description, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                DataGridView1.DataSource = dtGrid
                DataGridView1.Columns("Id").Visible = False
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                SId = .Rows(e.RowIndex).Cells(0).Value
            End With
            btEdit.Enabled = True
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btEdit_Click(sender As Object, e As EventArgs) Handles btEdit.Click
        sEdit = True
        sNew = False
        frmLocationEdit.ShowDialog()
    End Sub

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        DataGridView1.Columns.RemoveAt(0)

        Dim strDg As String = "M"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล Location (" & Comp & ")"
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
    End Sub
End Class