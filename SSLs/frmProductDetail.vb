Imports Microsoft.Office.Interop

Public Class frmProductDetail

    Public dt As New List(Of ProductDetails)
    Public dtUnt As New List(Of ProductUnit)
    Public Stat As Boolean
    Public SId, UntId As Integer

    Private Sub frmProductDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelData()
        btNew_Click(sender, e)
    End Sub

    Private Sub ButtonFalse()
        btNew.Enabled = False
        btDelete.Enabled = False
        Stat = False
    End Sub

    Private Sub ButtonTrue()
        btNew.Enabled = True
        btDelete.Enabled = True
        Stat = True
    End Sub

    Private Sub btNew_Click(sender As Object, e As EventArgs) Handles btNew.Click
        ClearTextBox(Me)
        ButtonFalse()
        tbCode.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbCode.KeyDown, tbName.KeyDown, tbUnit.KeyDown, tbPalletRow.KeyDown, tbPalletLevel.KeyDown, tbPalletTotal.KeyDown, tbWidth.KeyDown, tbLength.KeyDown, tbHeight.KeyDown, tbGrossWeight.KeyDown, tbMinStock.KeyDown, tbMaxStock.KeyDown, tbQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbUnit_Leave(sender As Object, e As EventArgs) Handles tbUnit.Leave
        If tbUnit.Text <> "" Then
            Dim da = (From c In dtUnt Order By c.Code Where c.Code.Contains(tbUnit.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 5
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbUnit.Text.Trim
                frmSearch.ShowDialog()
                UntId = s_ID
                tbUnit.Text = s_Code
                tbUnitDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    UntId = a.Id
                    tbUnit.Text = a.Code
                    tbUnitDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbUnit.SelectAll()
                tbUnit.Focus()
            End If
        Else
            tbUnitDesc.Clear()
        End If
    End Sub

    Private Sub btUnit_Click(sender As Object, e As EventArgs) Handles btUnit.Click
        bt_Num = 5
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbUnit.Text
        frmSearch.ShowDialog()
        UntId = s_ID
        tbUnit.Text = s_Code
        tbUnitDesc.Text = s_Desc
        tbQty.Focus()
    End Sub

    Private Sub tbNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbPalletRow.KeyPress, tbPalletLevel.KeyPress, tbPalletTotal.KeyPress, tbWidth.KeyPress, tbLength.KeyPress, tbHeight.KeyPress, tbGrossWeight.KeyPress, tbMaxStock.KeyPress, tbMinStock.KeyPress, tbQty.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub tbQty_Leave(sender As Object, e As EventArgs) Handles tbQty.Leave
        If IsNumeric(tbQty.Text) Then
            tbQty.Text = Format(CDbl(tbQty.Text), "#,##0.00")
        Else
            tbQty.Text = "0.00"
        End If
    End Sub

    Private Sub tbPalletRow_Leave(sender As Object, e As EventArgs) Handles tbPalletRow.Leave
        If IsNumeric(tbPalletRow.Text) Then
            tbPalletRow.Text = Format(CDbl(tbPalletRow.Text), "#,##0")
        Else
            tbPalletRow.Text = "0"
        End If
    End Sub

    Private Sub tbPalletLevel_Leave(sender As Object, e As EventArgs) Handles tbPalletLevel.Leave
        If IsNumeric(tbPalletLevel.Text) Then
            tbPalletLevel.Text = Format(CDbl(tbPalletLevel.Text), "#,##0")
        Else
            tbPalletLevel.Text = "0"
        End If
    End Sub

    Private Sub tbWidth_Leave(sender As Object, e As EventArgs) Handles tbWidth.Leave
        If IsNumeric(tbWidth.Text) Then
            tbWidth.Text = Format(CDbl(tbWidth.Text), "#,##0.00")
        Else
            tbWidth.Text = "0.00"
        End If
    End Sub

    Private Sub tbLength_Leave(sender As Object, e As EventArgs) Handles tbLength.Leave
        If IsNumeric(tbLength.Text) Then
            tbLength.Text = Format(CDbl(tbLength.Text), "#,##0.00")
        Else
            tbLength.Text = "0.00"
        End If
    End Sub

    Private Sub tbHeight_Leave(sender As Object, e As EventArgs) Handles tbHeight.Leave
        If IsNumeric(tbHeight.Text) Then
            tbHeight.Text = Format(CDbl(tbHeight.Text), "#,##0.00")
        Else
            tbHeight.Text = "0.00"
        End If
    End Sub

    Private Sub tbGrossWeight_Leave(sender As Object, e As EventArgs) Handles tbGrossWeight.Leave
        If IsNumeric(tbGrossWeight.Text) Then
            tbGrossWeight.Text = Format(CDbl(tbGrossWeight.Text), "#,##0.0000")
        Else
            tbGrossWeight.Text = "0.0000"
        End If
    End Sub

    Private Sub tbPalletRow_TextChanged(sender As Object, e As EventArgs) Handles tbPalletRow.TextChanged
        If IsNumeric(tbPalletRow.Text) Then
            tbPalletTotal.Text = Format(CDbl(tbPalletRow.Text) * CDbl(IIf(tbPalletLevel.Text = "", 0, tbPalletLevel.Text)), "#,##0")
        End If
    End Sub

    Private Sub tbPalletLevel_TextChanged(sender As Object, e As EventArgs) Handles tbPalletLevel.TextChanged
        If IsNumeric(tbPalletLevel.Text) Then
            tbPalletTotal.Text = Format(CDbl(IIf(tbPalletRow.Text = "", 0, tbPalletRow.Text)) * CDbl(tbPalletLevel.Text), "#,##0")
        End If
    End Sub

    Sub SelData()
        Try
            Using db = New PTGwmsEntities
                dt = db.ProductDetails.Where(Function(x) x.Enable = True And x.FKProduct = frmProducts.SId).ToList
                dtUnt = db.ProductUnit.Where(Function(x) x.Enable = True).ToList
            End Using
            Dim dtGrid = (From c In dt Order By c.CreateDate Select New With {c.Id, .รหัส = c.Code, .รายละเอียด = c.Description, c.FKProductUnit, .UCode = c.ProductUnit.Code, .หน่วย = c.ProductUnit.Name, .บรรจุ = c.PackSize, .ฐานพาเลต = c.PalletRow, .ชั้นพาเลต = c.PalletLevel, .รวมพาเลต = c.PalletTotal, .กว้าง = c.Width, .ยาว = c.Length, .สูง = c.Height, .น้ำหนัก = c.GrossWeight, c.MinStock, c.MaxStock, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                .Columns("FKProductUnit").Visible = False
                .Columns("UCode").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex <> -1 Then
            With DataGridView1
                SId = .Rows(e.RowIndex).Cells(0).Value
                tbCode.Text = .Rows(e.RowIndex).Cells(1).Value.ToString
                tbName.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
                UntId = .Rows(e.RowIndex).Cells(3).Value.ToString
                tbUnit.Text = .Rows(e.RowIndex).Cells(4).Value.ToString
                tbUnitDesc.Text = .Rows(e.RowIndex).Cells(5).Value.ToString
                tbQty.Text = .Rows(e.RowIndex).Cells(6).Value.ToString
                tbPalletRow.Text = .Rows(e.RowIndex).Cells(7).Value.ToString
                tbPalletLevel.Text = .Rows(e.RowIndex).Cells(8).Value.ToString
                tbPalletTotal.Text = .Rows(e.RowIndex).Cells(9).Value.ToString
                tbWidth.Text = .Rows(e.RowIndex).Cells(10).Value.ToString
                tbLength.Text = .Rows(e.RowIndex).Cells(11).Value.ToString
                tbHeight.Text = .Rows(e.RowIndex).Cells(12).Value.ToString
                tbGrossWeight.Text = .Rows(e.RowIndex).Cells(13).Value.ToString
                tbMinStock.Text = .Rows(e.RowIndex).Cells(14).Value.ToString
                tbMaxStock.Text = .Rows(e.RowIndex).Cells(15).Value.ToString
            End With
            ButtonTrue()
        End If
    End Sub

    Private Sub tbMinStock_Leave(sender As Object, e As EventArgs) Handles tbMinStock.Leave
        If IsNumeric(tbMinStock.Text) Then
            tbMinStock.Text = Format(CDbl(tbMinStock.Text), "#,##0")
        Else
            tbMinStock.Text = "0"
        End If
    End Sub

    Private Sub tbMaxStock_Leave(sender As Object, e As EventArgs) Handles tbMaxStock.Leave
        If IsNumeric(tbMaxStock.Text) Then
            tbMaxStock.Text = Format(CDbl(tbMaxStock.Text), "#,##0")
        Else
            tbMaxStock.Text = "0"
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCode.Focus()
            Exit Sub
        End If
        If tbUnit.Text = "" Then
            MessageBox.Show("กรุณาป้อน หน่วย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbUnit.Focus()
            Exit Sub
        End If
        If tbQty.Text = "" Or tbQty.Text = "0.00" Then
            MessageBox.Show("กรุณาป้อน บรรจุ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbQty.Focus()
            Exit Sub
        End If
        If Stat = False Then
            If Not IsNothing((From c In dt Where c.Code = tbCode.Text).FirstOrDefault) Then
                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbCode.Focus()
                tbCode.SelectAll()
                Exit Sub
            End If
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If Stat = False Then
                        db.ProductDetails.Add(New ProductDetails With {.Code = tbCode.Text, .Description = tbName.Text, .FKProduct = frmProducts.SId, .FKProductUnit = UntId, .PackSize = CDbl(tbQty.Text), .PalletRow = CDbl(tbPalletRow.Text), .PalletLevel = CDbl(tbPalletLevel.Text), .PalletTotal = CDbl(tbPalletTotal.Text), .Width = CDbl(tbWidth.Text), .Length = CDbl(tbLength.Text), .Height = CDbl(tbHeight.Text), .GrossWeight = CDbl(tbGrossWeight.Text), .MinStock = CDbl(tbMinStock.Text), .MaxStock = CDbl(tbMaxStock.Text), .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                    Else
                        Dim ds = (From c In db.ProductDetails Where c.Id = SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Code = tbCode.Text
                            ds.Description = tbName.Text
                            ds.FKProduct = frmProducts.SId
                            ds.FKProductUnit = UntId
                            ds.PackSize = CDbl(tbQty.Text)
                            ds.PalletRow = CDbl(tbPalletRow.Text)
                            ds.PalletLevel = CDbl(tbPalletLevel.Text)
                            ds.PalletTotal = CDbl(tbPalletTotal.Text)
                            ds.Width = CDbl(tbWidth.Text)
                            ds.Length = CDbl(tbLength.Text)
                            ds.Height = CDbl(tbHeight.Text)
                            ds.GrossWeight = CDbl(tbGrossWeight.Text)
                            ds.MinStock = CDbl(tbMinStock.Text)
                            ds.MaxStock = CDbl(tbMaxStock.Text)
                            ds.UpdateDate = DateTimeServer()
                            ds.UpdateBy = Username
                        End If
                    End If
                    db.SaveChanges()
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData()
            btNew_Click(sender, e)
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In db.ProductDetails Where c.Id = SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        db.SaveChanges()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            MessageBox.Show("ลบข้อมูลเรียบร้อย", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelData()
            btNew_Click(sender, e)
        End If
    End Sub

    Private Sub btExcel_Click(sender As System.Object, e As System.EventArgs) Handles btExcel.Click
        If DataGridView1.RowCount = 0 Then Exit Sub
        DataGridView1.Columns.RemoveAt(0)
        DataGridView1.Columns.RemoveAt(2)
        DataGridView1.Columns.RemoveAt(2)

        Dim strDg As String = "R"

        Dim xlApp As New Excel.Application
        Dim xlSheet As Excel.Worksheet
        Dim xlBook As Excel.Workbook

        xlBook = xlApp.Workbooks.Add()
        xlSheet = xlBook.Worksheets(1)
        xlApp.ActiveSheet.Cells(1, 1) = "ข้อมูล รายละเอียดสินค้า"
        xlApp.ActiveSheet.Range("A1:" & strDg & "1").Merge()
        xlApp.ActiveSheet.Range("A1").HorizontalAlignment = Excel.Constants.xlCenter

        'xlApp.ActiveSheet.Cells(2, 1) = "ค้นหาโดย : " & tbSearch.Text
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
    End Sub
End Class