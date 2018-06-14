Imports System.Globalization
Imports System.Threading
Imports Microsoft.Office.Interop

Public Class frmGL100AR
    Private Sub frmGL100AR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        dtDateFrom.CustomFormat = "dd/MM/yyyy"
        dtDateTo.CustomFormat = "dd/MM/yyyy"
    End Sub

    Private Sub btSearch_Click(sender As Object, e As EventArgs) Handles btSearch.Click
        SelData()
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Dim DateFrom As String = Format(dtDateFrom.Value, "yyyyMMdd").ToString
            Dim DateTo As String = Format(dtDateTo.Value, "yyyyMMdd").ToString
            Using db = New PTGwmsEntities
                Dim dt = db.V_GL100AR.Where(Function(x) x.TransDate >= DateFrom And x.TransDate <= DateTo).ToList
                If dt.Count > 0 Then
                    Dim dtGrid = (From c In dt Order By c.Id Select New With {c.COCD, c.DOCTYPE, .DOCDATE = Format(c.DOCDATE, "dd.MM.yyyy"), .POSTINGDATE = Format(c.POSTINGDATE, "dd.MM.yyyy"), c.POSTINGPERIOD, c.CURRENCY, c.EXCHANGERATE, c.TRANSLATIONDATE, c.LEDGERGROUP, c.REFERENCE,
                    c.DOCHEADERTEXT, c.BRANCH, c.REFKEYHEAD1, c.REFKEYHEAD2, c.PstKy, c.ACCOUNT, c.SPECIALGL, c.ALTERNATIVEGLACCOUNT, c.AMOUNTDOCUMENTCURRENCY, c.AMOUNTLOCALCURRENCY, c.AMOUNTGROUPCURRENCY, c.BUSINESSAREA, c.TRADINGPARTNER, c.VALUEDATE, c.PAYMENTTERM, c.BASELINEDATE,
                    c.PAYMENTMETHOD, c.PAYMENTBLOCK, c.INVOICEREFERENCE, c.FISCALYEAROFINVOICEREFERNCE, c.Qty, c.UOM, c.ASSIGNMENT, c.ITEMTEXT, c.TAXCODE, c.BUSINESSPLACE, c.TAXBASEAMOUNTDOCCURRENCY, c.TAXBASEAMOUNTLOCALCURRENCY, c.TAXBASEAMOUNTGROUPCURRENCY, c.TAXAMOUNTDOCCURRENCY,
                    c.TAXAMOUNTLOCALCURRENCY, c.TAXAMOUNTGROUPCURRENCY2, c.COSTCENTER, c.PROFITCENTER, c.INTERNALORDER, c.PASEGMENT, c.ControllingArea, c.Company, c.Plant, c.BA, c.ProfitCenter1, c.Costcenter1, .องค์กรการขาย = c.CTH1, .ช่องทางการจัดจำหน่าย = c.CTH2, .กลุ่มผลิตภัณฑ์ = c.CTH3,
                    .ภูมิภาคการขาย = c.CTH4, .หัวหน้าทีมขาย = c.CTH5, c.SalesSup, c.SalesRep, .กลุ่มลูกหนี้ = c.CTH6, .การจัดกลุ่มลูกค้า = c.CTH7, .การจัดกลุ่มลูกค้า2 = c.CTH8, .การจัดกลุ่มลูกค้า3 = c.CTH9, .การจัดเกรดลูกค้า = c.CTH10, .ตำบล = c.CTH11, .อำเภอ = c.CTH12, c.SalesDistrict, .รหัสจังหวัด = c.CTH13, c.CustomerNo,
                    c.MatGrp1, c.MatGrp2, c.MatGrp3, c.MatGrp4, c.MatGrp5, c.MatNo, c.PDH1, c.PDH2, c.PDH3, c.PDH4, c.PDH5, c.Materialgroup, c.ValualtionType, c.WTTYPE1, c.WTCODE1, c.WTBASE1DOCCURRENCY, c.WTBASE1LOCALCURRENCY, c.WTAMT1DOCCURRENCY, c.WTAMT1LOCALCURRENCY,
                    c.WTTYPE2, c.WTCODE2, c.WTBASE2DOCCURRENCY, c.WTBASE2LOCALCURRENCY, c.WTAMT2DOCCURRENCY, c.WTAMT2LOCALCURRENCY, c.ONETIME, c.NAME1, c.NAME2, c.NAME3, c.NAME4, c.STREET, c.CITY, c.POSTALCODE, c.LANGUAGEKEY, c.COUNTRYKEY, c.TAXNUMBER3, c.TYPEOFBUSINESS}).ToList
                    With DataGridView1
                        .DataSource = dtGrid
                        .AutoResizeColumns()
                        .SelectionMode = DataGridViewSelectionMode.FullRowSelect
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

        Dim strDg As String = "DC"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "REPORT GL100AR"
        xlApp.ActiveSheet.Range("A1:" & strDg & "1").Merge()
        xlApp.ActiveSheet.Range("A1").HorizontalAlignment = Excel.Constants.xlLeft

        xlApp.ActiveSheet.Cells(2, 1) = "ค้นหาโดย >> วันที่ : " & dtDateFrom.Text & " ถึง " & dtDateTo.Text
        xlApp.ActiveSheet.Range("A2:" & strDg & "2").Merge()
        xlApp.ActiveSheet.Range("A2").HorizontalAlignment = Excel.Constants.xlLeft

        xlApp.ActiveSheet.Cells(3, 1) = "วันที่ออกรายงาน : " & Now.ToString("dd/MM/yyyy HH:mm") & ""
        xlSheet.Range("A3:" & strDg & "3").Merge()
        xlApp.ActiveSheet.Range("A3").HorizontalAlignment = Excel.Constants.xlLeft

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
        Cursor = Cursors.Default
    End Sub
End Class