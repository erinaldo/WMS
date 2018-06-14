Public Class frmAddUnit

    Dim UntID As Integer

    Private Sub frmAddUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If frmProductEdit.unitStat = True Then
            Using db = New PTGwmsEntities
                Dim stk = (From c In db.StockOnhand Where c.FKCompany = CompID And c.FKProduct = frmProducts.SId And c.Qty > 0).FirstOrDefault
                If Not IsNothing(stk) Then
                    tbUnit.Enabled = False
                    btUnit.Enabled = False
                    tbPackSize.Enabled = False
                Else
                    tbUnit.Enabled = True
                    btUnit.Enabled = True
                    tbPackSize.Enabled = True
                End If
            End Using
            With frmProductEdit.DataGrid1
                UntID = frmProductEdit.UID
                tbUnit.Text = .Rows(.CurrentRow.Index).Cells(0).Value.ToString
                tbUnitDesc.Text = .Rows(.CurrentRow.Index).Cells(18).Value.ToString
                tbPackSize.Text = .Rows(.CurrentRow.Index).Cells(1).Value
                tbCost.Text = .Rows(.CurrentRow.Index).Cells(2).Value
                tbBarCode.Text = .Rows(.CurrentRow.Index).Cells(3).Value.ToString
                tbName.Text = .Rows(.CurrentRow.Index).Cells(4).Value.ToString
                tbMinStock.Text = .Rows(.CurrentRow.Index).Cells(9).Value
                tbMaxStock.Text = .Rows(.CurrentRow.Index).Cells(10).Value
                tbPalletRow.Text = .Rows(.CurrentRow.Index).Cells(11).Value
                tbPalletLevel.Text = .Rows(.CurrentRow.Index).Cells(12).Value
                tbPalletTotal.Text = .Rows(.CurrentRow.Index).Cells(13).Value
                tbWidth.Text = .Rows(.CurrentRow.Index).Cells(14).Value
                tbLength.Text = .Rows(.CurrentRow.Index).Cells(15).Value
                tbHeight.Text = .Rows(.CurrentRow.Index).Cells(16).Value
                tbGrossWeight.Text = .Rows(.CurrentRow.Index).Cells(17).Value
                CheckBox1.Checked = .Rows(.CurrentRow.Index).Cells(5).Value
                CheckBox2.Checked = .Rows(.CurrentRow.Index).Cells(6).Value
            End With
        Else
            ClearTextBox(Me)
            tbPackSize.Text = "0.00"
            tbCost.Text = "0.0000"
            tbMinStock.Text = "0"
            tbMaxStock.Text = "0"
            tbWidth.Text = "0.00"
            tbLength.Text = "0.00"
            tbHeight.Text = "0.00"
            tbGrossWeight.Text = "0.0000"
            CheckBox1.Checked = False
            tbUnit.Focus()
            btSave.Text = "OK"
            tbUnit.Enabled = True
            btUnit.Enabled = True
            tbPackSize.Enabled = True
        End If

    End Sub

    Private Sub tbUnit_Leave(sender As Object, e As EventArgs) Handles tbUnit.Leave
        If tbUnit.Text <> "" Then
            Dim da = (From c In stUnit.Instance.Unit Order By c.Code Where c.Code.Contains(tbUnit.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 14
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbUnit.Text.Trim
                frmSearch.ShowDialog()
                UntID = s_ID
                tbUnit.Text = s_Code
                tbUnitDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    UntID = a.Id
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
        bt_Num = 14
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbUnit.Text
        frmSearch.ShowDialog()
        UntID = s_ID
        tbUnit.Text = s_Code
        tbUnitDesc.Text = s_Desc
        tbPackSize.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbUnit.KeyDown, tbPackSize.KeyDown, tbCost.KeyDown, tbBarCode.KeyDown, tbName.KeyDown, tbMinStock.KeyDown, tbMaxStock.KeyDown, tbPalletRow.KeyDown, tbPalletLevel.KeyDown, tbWidth.KeyDown, tbLength.KeyDown, tbHeight.KeyDown, tbGrossWeight.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbUnit.Text = "" Then
            MessageBox.Show("wกรุณาป้อน หน่วยนับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbUnit.Focus()
            Exit Sub
        End If
        If tbPackSize.Text = "" Or tbPackSize.Text = "0.00" Then
            MessageBox.Show("กรุณาป้อน บรรจุ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbPackSize.Focus()
            Exit Sub
        End If
        If tbBarCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส Barcode", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbBarCode.Focus()
            Exit Sub
        End If
        If tbPalletTotal.Text = "" Or tbPalletTotal.Text = "0" Then
            MessageBox.Show("กรุณาป้อน จำนวนใน Pallet", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbPalletRow.Focus()
            Exit Sub
        End If
        If sNew = True Then
            If frmProductEdit.unitStat = False Then
                For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                    If frmProductEdit.DataGrid1.Rows(a).Cells(0).Value.ToString = tbUnit.Text Then
                        MessageBox.Show("ป้อนหน่วยซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbUnit.SelectAll()
                        tbUnit.Focus()
                        Exit Sub
                    End If
                Next
                For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                    If frmProductEdit.DataGrid1.Rows(a).Cells(3).Value.ToString = tbBarCode.Text Then
                        MessageBox.Show("ป้อน Barcode ซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbBarCode.SelectAll()
                        tbBarCode.Focus()
                        Exit Sub
                    End If
                Next
                If CheckBox1.Checked = True Then
                    For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                        If frmProductEdit.DataGrid1.Rows(a).Cells(5).Value = 1 Or frmProductEdit.DataGrid1.Rows(a).Cells(5).Value = True Then
                            MessageBox.Show("ไม่สามารถสร้าง หน่วยหลักซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbUnit.SelectAll()
                            tbUnit.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                If CheckBox2.Checked = True Then
                    For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                        If frmProductEdit.DataGrid1.Rows(a).Cells(6).Value = 1 Or frmProductEdit.DataGrid1.Rows(a).Cells(6).Value = True Then
                            MessageBox.Show("ไม่สามารถสร้าง หน่วยจ่ายซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbUnit.SelectAll()
                            tbUnit.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                frmProductEdit.DataGrid1.Rows.Add(tbUnit.Text, tbPackSize.Text, tbCost.Text, tbBarCode.Text, tbName.Text, IIf(CheckBox1.Checked = False, 0, 1), IIf(CheckBox2.Checked = False, 0, 1), UntID, 0, tbMinStock.Text, tbMaxStock.Text, tbPalletRow.Text, tbPalletLevel.Text, tbPalletTotal.Text, tbWidth.Text, tbLength.Text, tbHeight.Text, tbGrossWeight.Text, tbUnitDesc.Text, 0)
                Me.Close()
            Else
                'frmProductEdit.DataGrid1.Rows.Remove(frmProductEdit.DataGrid1.Rows(frmProductEdit.DataGrid1.SelectedCells.Item(0).RowIndex))
                With frmProductEdit.DataGrid1
                    If .Rows(.SelectedCells.Item(0).RowIndex).Cells(0).Value.ToString <> tbUnit.Text Then
                        For i = 0 To .RowCount - 1
                            If tbUnit.Text = .Rows(i).Cells(0).Value.ToString Then
                                MessageBox.Show("ป้อน หน่วยนับซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbUnit.SelectAll()
                                tbUnit.Focus()
                                Exit Sub
                            End If
                        Next
                    End If
                    If .Rows(.SelectedCells.Item(0).RowIndex).Cells(3).Value.ToString <> tbBarCode.Text Then
                        For i = 0 To .RowCount - 1
                            If tbUnit.Text = .Rows(i).Cells(3).Value.ToString Then
                                MessageBox.Show("ป้อน Barcode ซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbBarCode.SelectAll()
                                tbBarCode.Focus()
                                Exit Sub
                            End If
                        Next
                    End If
                    If CheckBox1.Checked = True Then
                        For a = 0 To .RowCount - 1
                            If .Rows(a).Cells(5).Value = 1 Or .Rows(a).Cells(5).Value = True Then
                                .Rows(a).Cells(5).Value = 0

                            End If
                        Next
                    End If
                    If CheckBox2.Checked = True Then
                        For a = 0 To .RowCount - 1
                            If .Rows(a).Cells(6).Value = 1 Or .Rows(a).Cells(6).Value = True Then
                                .Rows(a).Cells(6).Value = 0
                            End If
                        Next
                    End If
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(0).Value = tbUnit.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(1).Value = tbPackSize.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(2).Value = tbCost.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(3).Value = tbBarCode.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(4).Value = tbName.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(5).Value = IIf(CheckBox1.Checked = False, 0, 1)
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(6).Value = IIf(CheckBox2.Checked = False, 0, 1)
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(7).Value = UntID
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(8).Value = 0
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(9).Value = tbMinStock.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(10).Value = tbMaxStock.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(11).Value = tbPalletRow.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(12).Value = tbPalletLevel.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(13).Value = tbPalletTotal.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(14).Value = tbWidth.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(15).Value = tbLength.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(16).Value = tbHeight.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(17).Value = tbGrossWeight.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(18).Value = tbUnitDesc.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(19).Value = 0
                    Me.Close()
                End With
            End If
        Else
            If frmProductEdit.unitStat = False Then
                For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                    If frmProductEdit.DataGrid1.Rows(a).Cells(0).Value.ToString = tbUnit.Text Then
                        MessageBox.Show("ป้อนหน่วยซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbUnit.SelectAll()
                        tbUnit.Focus()
                        Exit Sub
                    End If
                Next
                For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                    If frmProductEdit.DataGrid1.Rows(a).Cells(3).Value.ToString = tbBarCode.Text Then
                        MessageBox.Show("ป้อน Barcode ซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        tbBarCode.SelectAll()
                        tbBarCode.Focus()
                        Exit Sub
                    End If
                Next
                If CheckBox1.Checked = True Then
                    For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                        If frmProductEdit.DataGrid1.Rows(a).Cells(5).Value = 1 Or frmProductEdit.DataGrid1.Rows(a).Cells(5).Value = True Then
                            MessageBox.Show("ไม่สามารถสร้าง หน่วยหลักซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbUnit.SelectAll()
                            tbUnit.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                If CheckBox2.Checked = True Then
                    For a = 0 To frmProductEdit.DataGrid1.RowCount - 1
                        If frmProductEdit.DataGrid1.Rows(a).Cells(6).Value = 1 Or frmProductEdit.DataGrid1.Rows(a).Cells(6).Value = True Then
                            MessageBox.Show("ไม่สามารถสร้าง หน่วยจ่ายซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbUnit.SelectAll()
                            tbUnit.Focus()
                            Exit Sub
                        End If
                    Next
                End If
                frmProductEdit.DataGrid1.Rows.Add(tbUnit.Text, tbPackSize.Text, tbCost.Text, tbBarCode.Text, tbName.Text, IIf(CheckBox1.Checked = False, 0, 1), IIf(CheckBox2.Checked = False, 0, 1), UntID, 0, tbMinStock.Text, tbMaxStock.Text, tbPalletRow.Text, tbPalletLevel.Text, tbPalletTotal.Text, tbWidth.Text, tbLength.Text, tbHeight.Text, tbGrossWeight.Text, tbUnitDesc.Text, 0)
                Me.Close()
            Else
                With frmProductEdit.DataGrid1
                    If .Rows(.SelectedCells.Item(0).RowIndex).Cells(0).Value.ToString <> tbUnit.Text Then
                        For i = 0 To .RowCount - 1
                            If tbUnit.Text = .Rows(i).Cells(0).Value.ToString Then
                                MessageBox.Show("ป้อน หน่วยนับซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbUnit.SelectAll()
                                tbUnit.Focus()
                                Exit Sub
                            End If
                        Next
                    End If
                    If .Rows(.SelectedCells.Item(0).RowIndex).Cells(3).Value.ToString <> tbBarCode.Text Then
                        For i = 0 To .RowCount - 1
                            If tbUnit.Text = .Rows(i).Cells(3).Value.ToString Then
                                MessageBox.Show("ป้อน Barcode ซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbBarCode.SelectAll()
                                tbBarCode.Focus()
                                Exit Sub
                            End If
                        Next
                    End If
                    If CheckBox1.Checked = True Then
                        For a = 0 To .RowCount - 1
                            If .Rows(a).Cells(5).Value = 1 Or .Rows(a).Cells(5).Value = True Then
                                .Rows(a).Cells(5).Value = 0
                                .Rows(a).Cells(19).Value = 1
                            End If
                        Next
                    End If
                    If CheckBox2.Checked = True Then
                        For a = 0 To .RowCount - 1
                            If .Rows(a).Cells(6).Value = 1 Or .Rows(a).Cells(6).Value = True Then
                                .Rows(a).Cells(6).Value = 0
                                .Rows(a).Cells(19).Value = 1
                            End If
                        Next
                    End If
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(0).Value = tbUnit.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(1).Value = tbPackSize.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(2).Value = tbCost.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(3).Value = tbBarCode.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(4).Value = tbName.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(5).Value = IIf(CheckBox1.Checked = False, 0, 1)
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(6).Value = IIf(CheckBox2.Checked = False, 0, 1)
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(7).Value = UntID
                    '.Rows(.SelectedCells.Item(0).RowIndex).Cells(8).Value = 0
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(9).Value = tbMinStock.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(10).Value = tbMaxStock.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(11).Value = tbPalletRow.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(12).Value = tbPalletLevel.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(13).Value = tbPalletTotal.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(14).Value = tbWidth.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(15).Value = tbLength.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(16).Value = tbHeight.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(17).Value = tbGrossWeight.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(18).Value = tbUnitDesc.Text
                    .Rows(.SelectedCells.Item(0).RowIndex).Cells(19).Value = 1
                    Me.Close()
                End With
            End If
        End If
    End Sub

    Private Sub tbNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbPackSize.KeyPress, tbCost.KeyPress, tbMaxStock.KeyPress, tbMinStock.KeyPress, tbPalletLevel.KeyPress, tbPalletRow.KeyPress, tbWidth.KeyPress, tbLength.KeyPress, tbHeight.KeyPress, tbGrossWeight.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub tbPackSize_Leave(sender As Object, e As EventArgs) Handles tbPackSize.Leave
        If IsNumeric(tbPackSize.Text) Then
            tbPackSize.Text = Format(CDbl(tbPackSize.Text), "#,##0.00")
        Else
            tbPackSize.Text = "0.00"
        End If
    End Sub

    Private Sub tbCost_Leave(sender As Object, e As EventArgs) Handles tbCost.Leave
        If IsNumeric(tbCost.Text) Then
            tbCost.Text = Format(CDbl(tbCost.Text), "#,##0.0000")
        Else
            tbCost.Text = "0.0000"
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

    Private Sub tbPalletRow_Leave(sender As Object, e As EventArgs) Handles tbPalletRow.Leave
        If IsNumeric(tbPalletRow.Text) Then
            tbPalletRow.Text = Format(CDbl(tbPalletRow.Text), "#,##0")
        Else
            tbPalletRow.Text = "99"
        End If
    End Sub

    Private Sub tbPalletLevel_Leave(sender As Object, e As EventArgs) Handles tbPalletLevel.Leave
        If IsNumeric(tbPalletLevel.Text) Then
            tbPalletLevel.Text = Format(CDbl(tbPalletLevel.Text), "#,##0")
        Else
            tbPalletLevel.Text = "99"
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

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        Me.Close()
    End Sub
End Class