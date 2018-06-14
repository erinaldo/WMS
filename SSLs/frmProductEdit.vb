Public Class frmProductEdit

    Public dtDtl As New List(Of ProductDetails)
    Public OwnID, UnitID, TypeID, GrpID, BrnID, ZoneID, UID, DtlID As Integer
    Dim oldCode As String
    Public unitStat As Boolean = False
    Dim bnt As String = "-"
    Dim Issnt As String = "-"
    Dim bup, Issup As Double
    Dim BBar As String = "-"
    Dim UBar As String = "-"

    Private Sub frmProductEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        oldCode = ""
        Dim Wh = (From c In stWarehouse.Instance.WH Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        cbWH.DataSource = Wh
        cbWH.DisplayMember = "Name"
        cbWH.ValueMember = "Id"
        cbWH.SelectedIndex = -1
        btEditZone.Enabled = False
        If sEdit = True Then
            SelProdDtl()
        Else
            ClearData()
        End If
    End Sub

    Sub SelProdDtl()
        Dim Sel = (From c In stProduct.Instance.Prod Where c.Id = frmProducts.SId).FirstOrDefault
        If Not IsNothing(Sel) Then
            Using db = New PTGwmsEntities
                dtDtl = (From c In db.ProductDetails.Include("ProductUnit") Where c.Enable = True And c.FKProduct = Sel.Id).ToList
            End Using
            OwnID = Sel.FKOwner
            tbOwner.Text = Sel.Owners.Code
            tbOwnerDesc.Text = Sel.Owners.Name
            tbCode.Text = Sel.Code
            oldCode = Sel.Code
            tbName1.Text = Sel.Name
            tbName2.Text = Sel.NameEng
            TypeID = Sel.FKProductType
            tbType.Text = Sel.ProductType.Code
            tbTypeDesc.Text = Sel.ProductType.Name
            GrpID = Sel.FKProductGroup
            tbGroup.Text = Sel.ProductGroups.Code
            tbGroupDesc.Text = Sel.ProductGroups.Name
            BrnID = Sel.FKProductBrand
            tbBrand.Text = Sel.ProductBrands.Code
            tbBrandDesc.Text = Sel.ProductBrands.Name
            cbWH.SelectedValue = Sel.Zone.FKWarehouse
            ZoneID = Sel.FKZone
            tbZone.Text = Sel.Zone.Code
            tbZoneDesc.Text = Sel.Zone.Name
            tbShelfLife.Text = Sel.ShelfLife
            tbRcvLimit.Text = Sel.ReceiveLimit
            tbIssueLimit.Text = Sel.IssueLimit
            tbDesc.Text = Sel.Description
            Dim d = (From c In dtDtl).ToList
            With DataGrid1
                .Rows.Clear()
                For i = 0 To d.Count - 1
                    .Rows.Add()
                    .Rows(i).Cells(0).Value = d(i).ProductUnit.Code
                    .Rows(i).Cells(1).Value = Format(d(i).PackSize, "#,##0.00")
                    .Rows(i).Cells(2).Value = Format(d(i).UnitCost, "#,##0.0000")
                    .Rows(i).Cells(3).Value = d(i).Code
                    .Rows(i).Cells(4).Value = d(i).Description
                    .Rows(i).Cells(5).Value = d(i).IsBaseUnit
                    .Rows(i).Cells(6).Value = d(i).IssueUnit
                    .Rows(i).Cells(7).Value = d(i).FKProductUnit
                    .Rows(i).Cells(8).Value = d(i).Id
                    .Rows(i).Cells(9).Value = d(i).MinStock
                    .Rows(i).Cells(10).Value = d(i).MaxStock
                    .Rows(i).Cells(11).Value = d(i).PalletRow
                    .Rows(i).Cells(12).Value = d(i).PalletLevel
                    .Rows(i).Cells(13).Value = d(i).PalletTotal
                    .Rows(i).Cells(14).Value = d(i).Width
                    .Rows(i).Cells(15).Value = d(i).Length
                    .Rows(i).Cells(16).Value = d(i).Height
                    .Rows(i).Cells(17).Value = d(i).GrossWeight
                    .Rows(i).Cells(18).Value = d(i).ProductUnit.Name
                Next
            End With
            btDelete.Enabled = True
            btEditUnit.Enabled = False
            btDelUnit.Enabled = False
            btPrintBarPrord.Enabled = False
        End If
    End Sub

    Sub ClearData()
        tbOwner.Clear()
        tbOwnerDesc.Clear()
        tbCode.Clear()
        tbName1.Clear()
        tbName2.Clear()
        tbType.Clear()
        tbTypeDesc.Clear()
        tbGroup.Clear()
        tbGroupDesc.Clear()
        tbBrand.Clear()
        tbBrandDesc.Clear()
        tbDesc.Clear()
        tbZone.Clear()
        tbZoneDesc.Clear()
        tbShelfLife.Clear()
        tbRcvLimit.Clear()
        tbIssueLimit.Clear()
        DataGrid1.Rows.Clear()
        DataGridView1.Rows.Clear()
        btDelete.Enabled = False
        btEditUnit.Enabled = False
        btDelUnit.Enabled = False
        btPrintBarPrord.Enabled = False
        unitStat = False
        tbOwner.Focus()
    End Sub

    Private Sub DataGrid1_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGrid1.CellClick
        If e.RowIndex <> -1 Then
            With DataGrid1
                UID = .Rows(e.RowIndex).Cells(7).Value
                DtlID = .Rows(e.RowIndex).Cells(8).Value
            End With
            btEditUnit.Enabled = True
            'btDelUnit.Enabled = True
            btPrintBarPrord.Enabled = True
        End If
    End Sub

    Private Sub btOwner_Click(sender As Object, e As EventArgs) Handles btOwner.Click
        bt_Num = 16
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbOwner.Text
        frmSearch.ShowDialog()
        OwnID = s_ID
        tbOwner.Text = s_Code
        tbOwnerDesc.Text = s_Desc
        tbCode.Focus()
    End Sub

    Private Sub tbOwner_Leave(sender As Object, e As EventArgs) Handles tbOwner.Leave
        If tbOwner.Text <> "" Then
            Dim da = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbOwner.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 16
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbOwner.Text.Trim
                frmSearch.ShowDialog()
                OwnID = s_ID
                tbOwner.Text = s_Code
                tbOwnerDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    OwnID = a.Id
                    tbOwner.Text = a.Code
                    tbOwnerDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbOwner.SelectAll()
                tbOwner.Focus()
            End If
        Else
            tbOwnerDesc.Clear()
        End If
    End Sub

    Private Sub btGroup_Click(sender As Object, e As EventArgs) Handles btGroup.Click
        bt_Num = 1
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbGroup.Text
        frmSearch.ShowDialog()
        GrpID = s_ID
        tbGroup.Text = s_Code
        tbGroupDesc.Text = s_Desc
        tbBrand.Focus()
    End Sub

    Private Sub tbGroup_Leave(sender As Object, e As EventArgs) Handles tbGroup.Leave
        If tbGroup.Text <> "" Then
            Dim da = (From c In stProdGroup.Instance.ProdGrp Order By c.Code Where c.Code.Contains(tbGroup.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 1
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbGroup.Text.Trim
                frmSearch.ShowDialog()
                GrpID = s_ID
                tbGroup.Text = s_Code
                tbGroupDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    GrpID = a.Id
                    tbGroup.Text = a.Code
                    tbGroupDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbGroup.SelectAll()
                tbGroup.Focus()
            End If
        Else
            tbGroupDesc.Clear()
        End If
    End Sub

    Private Sub tbBrand_Leave(sender As Object, e As EventArgs) Handles tbBrand.Leave
        If tbBrand.Text <> "" Then
            Dim da = (From c In stProdBrand.Instance.ProdBrn Order By c.Code Where c.Code.Contains(tbBrand.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 2
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbBrand.Text.Trim
                frmSearch.ShowDialog()
                BrnID = s_ID
                tbBrand.Text = s_Code
                tbBrandDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    BrnID = a.Id
                    tbBrand.Text = a.Code
                    tbBrandDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbBrand.SelectAll()
                tbBrand.Focus()
            End If
        Else
            tbBrandDesc.Clear()
        End If
    End Sub

    Private Sub btBrand_Click(sender As Object, e As EventArgs) Handles btBrand.Click
        bt_Num = 2
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbBrand.Text
        frmSearch.ShowDialog()
        BrnID = s_ID
        tbBrand.Text = s_Code
        tbBrandDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub

    Private Sub tbZone_Leave(sender As Object, e As EventArgs) Handles tbZone.Leave
        If tbZone.Text <> "" Then
            If cbWH.SelectedIndex = -1 Then
                MessageBox.Show("กรุณาเลือก คลังสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                cbWH.Focus()
                Exit Sub
            End If
            Dim da = (From c In stZone.Instance.Zone Order By c.Code Where c.Warehouse.Id = cbWH.SelectedValue And c.Code.Contains(tbZone.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 12
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbZone.Text.Trim
                frmSearch.ShowDialog()
                ZoneID = s_ID
                tbZone.Text = s_Code
                tbZoneDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    ZoneID = a.Id
                    tbZone.Text = a.Code
                    tbZoneDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbZone.SelectAll()
                tbZone.Focus()
            End If
        Else
            tbZoneDesc.Clear()
        End If
    End Sub

    Private Sub btZone_Click(sender As Object, e As EventArgs) Handles btZone.Click
        bt_Num = 12
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbZone.Text
        frmSearch.ShowDialog()
        ZoneID = s_ID
        tbZone.Text = s_Code
        tbZoneDesc.Text = s_Desc
        tbShelfLife.Focus()
    End Sub

    Private Sub btType_Click(sender As Object, e As EventArgs) Handles btType.Click
        bt_Num = 13
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbType.Text
        frmSearch.ShowDialog()
        TypeID = s_ID
        tbType.Text = s_Code
        tbTypeDesc.Text = s_Desc
        tbZone.Focus()
    End Sub

    Private Sub tbType_Leave(sender As Object, e As EventArgs) Handles tbType.Leave
        If tbType.Text <> "" Then
            Dim da = (From c In stProductType.Instance.ProdType Order By c.Code Where c.Code.Contains(tbType.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 13
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbType.Text.Trim
                frmSearch.ShowDialog()
                TypeID = s_ID
                tbType.Text = s_Code
                tbTypeDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    TypeID = a.Id
                    tbType.Text = a.Code
                    tbTypeDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbType.SelectAll()
                tbType.Focus()
            End If
        Else
            tbTypeDesc.Clear()
        End If
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbOwner.KeyDown, tbCode.KeyDown, tbName1.KeyDown, tbName2.KeyDown, tbType.KeyDown, tbGroup.KeyDown, tbBrand.KeyDown, tbZone.KeyDown, tbShelfLife.KeyDown, tbRcvLimit.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btDelete_Click(sender As Object, e As EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim stk = (From c In db.StockOnhand Where c.FKCompany = CompID And c.FKProduct = frmProducts.SId And c.Qty > 0).FirstOrDefault
                    If Not IsNothing(stk) Then
                        MessageBox.Show("ยังมีสินค้าใน Stock Onhand ไม่สามารถลบได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Dim cur = (From c In db.CurrentStocks Where c.Enable = True And c.FKProduct = frmProducts.SId And c.Qty > 0).FirstOrDefault
                    If Not IsNothing(cur) Then
                        MessageBox.Show("ยังมีสินค้าใน Stock Location ไม่สามารถลบได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    Dim ds = (From c In stProduct.Instance.Prod Where c.Id = frmProducts.SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        stProduct.Instance.Prod.Remove(ds)
                    End If
                    Dim ds1 = (From c In db.Products Where Enabled = True And c.Id = frmProducts.SId).FirstOrDefault
                    If Not IsNothing(ds1) Then
                        ds1.UpdateDate = DateTimeServer()
                        ds1.UpdateBy = Username
                        ds1.Enable = False
                        Dim ds2 = (From c In db.ProductDetails Where Enabled = True And c.FKProduct = ds1.Id).ToList
                        For Each a In ds2
                            a.UpdateDate = DateTimeServer()
                            a.UpdateBy = Username
                            a.Enable = False
                        Next
                        db.SaveChanges()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            MessageBox.Show("ลบข้อมูลเรียบร้อย", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            frmProducts.Button1_Click(sender, e)
        End If
    End Sub

    Private Sub btAddUnit_Click(sender As Object, e As EventArgs) Handles btAddUnit.Click
        unitStat = False
        frmAddUnit.ShowDialog()
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGrid1.RowPostPaint
        Using b As New SolidBrush(DataGrid1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub tbIssueLimit_KeyDown(sender As Object, e As KeyEventArgs) Handles tbIssueLimit.KeyDown
        If e.KeyCode = Keys.Enter Then
            btSave.Focus()
        End If
    End Sub

    Private Sub btEditUnit_Click(sender As Object, e As EventArgs) Handles btEditUnit.Click
        unitStat = True
        frmAddUnit.ShowDialog()
    End Sub

    Private Sub cbWH_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWH.SelectedIndexChanged
        'If IsNumeric(cbWH.SelectedValue) = False Then Exit Sub
        'If cbWH.SelectedIndex > -1 Then
        '    cbZone.Text = ""
        '    Dim Zn = (From c In stZone.Instance.Zone Where c.FKWarehouse = cbWH.SelectedValue Select New With {c.Id, .Name = c.Code & "-" & c.Name}).ToList
        '    cbZone.DataSource = Zn
        '    cbZone.DisplayMember = "Name"
        '    cbZone.ValueMember = "Id"
        '    cbZone.SelectedIndex = -1
        'End If
    End Sub

    Private Sub btDelUnit_Click(sender As Object, e As EventArgs) Handles btDelUnit.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    'Dim stk = (From c In db.StockOnhand Where c.FKProduct = frmProducts.SId Group c By c.FKProduct Into Qty = Sum(c.Qty)).FirstOrDefault
                    'If stk.Qty > 0 Then
                    '    MessageBox.Show("ไม่สามารถลบข้อมูลได้ เนื่องจากยังมียอด Stock คงเหลือ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    '    tbOwner.Focus()
                    '    Exit Sub
                    'End If
                    Dim ds1 = (From c In db.ProductDetails Where c.Id = DtlID).FirstOrDefault
                    If Not IsNothing(ds1) Then
                        ds1.UpdateDate = DateTimeServer()
                        ds1.UpdateBy = Username
                        ds1.Enable = False
                        db.SaveChanges()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End Using
            MessageBox.Show("ลบข้อมูลเรียบร้อย", "Delete Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            SelProdDtl()
        End If
    End Sub

    Private Sub btPrintBarPrord_Click(sender As Object, e As EventArgs) Handles btPrintBarPrord.Click
        frmPrintBarProd.Show()
    End Sub

    Private Sub tbDesc_KeyDown(sender As Object, e As KeyEventArgs) Handles tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            btSave.Focus()
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbOwner.Text = "" Then
            MessageBox.Show("กรุณาป้อน เจ้าของสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbOwner.Focus()
            Exit Sub
        End If
        If tbCode.Text = "" Then
            MessageBox.Show("กรุณาป้อน รหัส", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbCode.Focus()
            Exit Sub
        End If
        If tbName1.Text = "" Then
            MessageBox.Show("กรุณาป้อน ชื่อสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbName1.Focus()
            Exit Sub
        End If
        If tbType.Text = "" Then
            MessageBox.Show("กรุณาป้อน ประเภท", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbType.Focus()
            Exit Sub
        End If
        If tbGroup.Text = "" Then
            MessageBox.Show("กรุณาป้อน กลุ่มสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbGroup.Focus()
            Exit Sub
        End If
        If tbBrand.Text = "" Then
            MessageBox.Show("กรุณาป้อน ยี่ห้อ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbBrand.Focus()
            Exit Sub
        End If
        If DataGrid1.RowCount = 0 Then
            MessageBox.Show("กรุณาป้อน เพิ่มหน่วยนับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btAddUnit.Focus()
            Exit Sub
        End If
        If tbZone.Text = "" Then
            MessageBox.Show("กรุณาป้อน โซนจัดเก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbZone.Focus()
            Exit Sub
        End If
        If tbShelfLife.Text = "" Or tbShelfLife.Text = "0" Then
            MessageBox.Show("กรุณาป้อน อายุสินค้า", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbShelfLife.Focus()
            Exit Sub
        End If
        If tbRcvLimit.Text = "" Or tbRcvLimit.Text = "0" Then
            MessageBox.Show("กรุณาป้อน อายุการรับ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbRcvLimit.Focus()
            Exit Sub
        End If
        If tbIssueLimit.Text = "" Or tbIssueLimit.Text = "0" Then
            MessageBox.Show("กรุณาป้อน อายุการเบิก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbIssueLimit.Focus()
            Exit Sub
        End If
        Dim chk1 As Boolean = False
        For a = 0 To DataGrid1.RowCount - 1
            If DataGrid1.Rows(a).Cells(5).Value = 1 Or DataGrid1.Rows(a).Cells(5).Value = True Then
                chk1 = True
            End If
        Next
        If chk1 = False Then
            MessageBox.Show("กรุณากำหนด หน่วยหลัก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim chk As Boolean = False
        For a = 0 To DataGrid1.RowCount - 1
            If DataGrid1.Rows(a).Cells(6).Value = 1 Or DataGrid1.Rows(a).Cells(6).Value = True Then
                chk = True
            End If
        Next
        If chk = False Then
            MessageBox.Show("กรุณากำหนด หน่วยจ่าย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    For Each dr1 As DataGridViewRow In DataGrid1.Rows
                        If dr1.Cells(5).Value = True Or dr1.Cells(5).Value = 1 Then
                            bnt = dr1.Cells(0).Value
                            bup = dr1.Cells(1).Value
                            BBar = dr1.Cells(3).Value
                        End If
                        If dr1.Cells(6).Value = True Or dr1.Cells(6).Value = 1 Then
                            Issnt = dr1.Cells(0).Value
                            Issup = dr1.Cells(1).Value
                            UBar = dr1.Cells(3).Value
                        End If
                    Next
                    If sNew = True Then
                        If Not IsNothing((From c In db.Products Where c.FKCompany = CompID And c.FKOwner = OwnID And c.FKZone = ZoneID And c.Code = tbCode.Text).FirstOrDefault) Then
                            MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbCode.Focus()
                            tbCode.SelectAll()
                            Exit Sub
                        End If
                        Dim itm = (From c In db.ItemStatus Where c.FKCompany = CompID And c.Code = "01").FirstOrDefault
                        If IsNothing(itm) Then
                            MessageBox.Show("ไม่พบสถานะสินค้า (01)", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Dim Products = New Products With {.Code = tbCode.Text, .Name = tbName1.Text, .NameEng = tbName2.Text, .FKCompany = CompID, .FKProductGroup = GrpID, .FKProductBrand = BrnID, .FKProductType = TypeID, .FKOwner = OwnID, .FKZone = ZoneID, .ShelfLife = CDbl(tbShelfLife.Text), .ReceiveLimit = CDbl(tbRcvLimit.Text), .IssueLimit = CDbl(tbIssueLimit.Text), .Description = tbDesc.Text, .BaseBarcode = BBar, .BaseUnitCode = bnt, .BaseUnitPackSize = bup, .IssueBarcode = UBar, .IssueUnitCode = Issnt, .IssueUnitPackSize = Issup, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                        For Each dr As DataGridViewRow In DataGrid1.Rows
                            Products.ProductDetails.Add(New ProductDetails With {.Code = dr.Cells(3).Value, .Description = dr.Cells(4).Value, .FKProductUnit = dr.Cells(7).Value, .PackSize = dr.Cells(1).Value, .UnitCost = dr.Cells(2).Value, .IsBaseUnit = dr.Cells(5).Value, .IssueUnit = dr.Cells(6).Value, .MinStock = dr.Cells(9).Value, .MaxStock = dr.Cells(10).Value, .PalletLevel = dr.Cells(12).Value, .PalletRow = dr.Cells(11).Value, .PalletTotal = dr.Cells(13).Value, .Width = dr.Cells(14).Value, .Length = dr.Cells(15).Value, .Height = dr.Cells(16).Value, .GrossWeight = dr.Cells(17).Value, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        Next
                        Products.StockOnhand.Add(New StockOnhand With {.FKCompany = CompID, .FKWarehouse = cbWH.SelectedValue, .FKOwner = OwnID, .FKItemStatus = itm.Id,
                                              .Qty = 0, .QtyCost = 0, .NetPrice = 0, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                        db.Products.Add(Products)
                        db.SaveChanges()
                        ClearData()
                        Dim lastadd = (From c In db.Products.Include("Owners").Include("ProductType").Include("ProductGroups").Include("ProductBrands").Include("Zone") Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                        stProduct.Instance.Prod.Add(lastadd)
                    Else
                        If tbCode.Text <> oldCode Then
                            If Not IsNothing((From c In db.Products Where c.FKCompany = CompID And c.FKOwner = OwnID And c.FKZone = ZoneID And c.Code = tbCode.Text).FirstOrDefault) Then
                                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbCode.Focus()
                                tbCode.SelectAll()
                                Exit Sub
                            End If
                        End If
                        Dim ds = (From c In db.Products Where c.Id = frmProducts.SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            ds.Code = tbCode.Text
                            ds.Name = tbName1.Text
                            ds.NameEng = tbName2.Text
                            ds.FKCompany = CompID
                            ds.FKProductGroup = GrpID
                            ds.FKProductBrand = BrnID
                            ds.FKProductType = TypeID
                            ds.FKOwner = OwnID
                            ds.FKZone = ZoneID
                            ds.BaseBarcode = BBar
                            ds.BaseUnitCode = bnt
                            ds.BaseUnitPackSize = bup
                            ds.IssueBarcode = UBar
                            ds.IssueUnitCode = Issnt
                            ds.IssueUnitPackSize = Issup
                            ds.ShelfLife = CDbl(tbShelfLife.Text)
                            ds.ReceiveLimit = CDbl(tbRcvLimit.Text)
                            ds.IssueLimit = CDbl(tbIssueLimit.Text)
                            ds.Description = tbDesc.Text
                            ds.UpdateDate = DateTimeServer()
                            ds.UpdateBy = Username
                            Dim rID As Integer
                            For Each dr As DataGridViewRow In DataGrid1.Rows
                                If dr.Cells(8).Value = 0 Then
                                    db.ProductDetails.Add(New ProductDetails With {.FKProduct = frmProducts.SId, .Code = dr.Cells(3).Value, .Description = dr.Cells(4).Value, .FKProductUnit = dr.Cells(7).Value, .PackSize = dr.Cells(1).Value, .UnitCost = dr.Cells(2).Value, .IsBaseUnit = dr.Cells(5).Value, .IssueUnit = dr.Cells(6).Value, .MinStock = dr.Cells(9).Value, .MaxStock = dr.Cells(10).Value, .PalletLevel = dr.Cells(12).Value, .PalletRow = dr.Cells(11).Value, .PalletTotal = dr.Cells(13).Value, .Width = dr.Cells(14).Value, .Length = dr.Cells(15).Value, .Height = dr.Cells(16).Value, .GrossWeight = dr.Cells(17).Value, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                End If
                                If dr.Cells(19).Value = 1 And dr.Cells(8).Value > 0 Then
                                    rID = dr.Cells(8).Value
                                    Dim dt = (From c In db.ProductDetails Where c.Id = rID).FirstOrDefault
                                    If Not IsNothing(dt) Then
                                        dt.Code = dr.Cells(3).Value
                                        dt.Description = dr.Cells(4).Value
                                        dt.FKProductUnit = dr.Cells(7).Value
                                        dt.PackSize = dr.Cells(1).Value
                                        dt.UnitCost = dr.Cells(2).Value
                                        dt.IsBaseUnit = dr.Cells(5).Value
                                        dt.IssueUnit = dr.Cells(6).Value
                                        dt.MinStock = dr.Cells(9).Value
                                        dt.MaxStock = dr.Cells(10).Value
                                        dt.PalletLevel = dr.Cells(12).Value
                                        dt.PalletRow = dr.Cells(11).Value
                                        dt.PalletTotal = dr.Cells(13).Value
                                        dt.Width = dr.Cells(14).Value
                                        dt.Length = dr.Cells(15).Value
                                        dt.Height = dr.Cells(16).Value
                                        dt.GrossWeight = dr.Cells(17).Value
                                        dt.UpdateDate = DateTimeServer()
                                        dt.UpdateBy = Username
                                    End If
                                End If
                            Next
                            db.SaveChanges()
                        End If
                        Dim ds1 = (From c In stProduct.Instance.Prod Where c.Id = frmProducts.SId).FirstOrDefault
                        If Not IsNothing(ds1) Then
                            stProduct.Instance.Prod.Remove(ds1)
                            Dim lastadd = (From c In db.Products.Include("Owners").Include("ProductType").Include("ProductGroups").Include("ProductBrands").Include("Zone") Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                            stProduct.Instance.Prod.Add(lastadd)
                        End If
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmProducts.Button1_Click(sender, e)
            If sEdit = True Then Me.Close()
        End If
    End Sub

    Private Sub tbShelfLife_Leave(sender As Object, e As EventArgs) Handles tbShelfLife.Leave
        If IsNumeric(tbShelfLife.Text) Then
            tbShelfLife.Text = Format(CDbl(tbShelfLife.Text), "#,##0")
        Else
            tbShelfLife.Text = "999"
        End If
    End Sub

    Private Sub tbRcvLimit_Leave(sender As Object, e As EventArgs) Handles tbRcvLimit.Leave
        If IsNumeric(tbRcvLimit.Text) Then
            tbRcvLimit.Text = Format(CDbl(tbRcvLimit.Text), "#,##0")
        Else
            tbRcvLimit.Text = "999"
        End If
    End Sub

    Private Sub tbIssueLimit_Leave(sender As Object, e As EventArgs) Handles tbIssueLimit.Leave
        If IsNumeric(tbIssueLimit.Text) Then
            tbIssueLimit.Text = Format(CDbl(tbIssueLimit.Text), "#,##0")
        Else
            tbIssueLimit.Text = "999"
        End If
    End Sub

    Private Sub tbNum_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles tbIssueLimit.KeyPress, tbRcvLimit.KeyPress, tbShelfLife.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False
            Case Else
                e.Handled = True
        End Select
    End Sub

End Class