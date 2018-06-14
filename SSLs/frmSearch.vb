Public Class frmSearch
    Private Sub frmSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim headerText() As String = {}
        Select Case bt_Num
            Case 5
                headerText = {"Id", "รหัส", "หน่วยนับ"}
            Case 8
                headerText = {"Id", "รหัสสินค้า", "บาร์โค้ด", "ชื่อสินค้า", "หน่วยนับ", "บรรจุ"}
            Case 14
                headerText = {"Id", "รหัส", "หน่วยนับ"}
            Case 31
                headerText = {"Id", "รหัสสินค้า", "บาร์โค้ด", "ชื่อสินค้า", "หน่วยนับ", "บรรจุ"}
            Case Else
                headerText = {"Id", "รหัส", "รายการ"}
        End Select

        With DataGrid1
            For i = 0 To headerText.Count - 1 Step +1
                .Columns(i).HeaderText() = headerText(i)
            Next
            Select Case bt_Num
                Case 8
                    .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Case 31
                    .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Case Else
                    .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End Select
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
        End With
        s_ID = 0
        s_Code = ""
        s_Desc = ""
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGrid1.RowPostPaint
        Using b As New SolidBrush(DataGrid1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub DataGrid1_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGrid1.CellDoubleClick
        s_ID = DataGrid1.CurrentRow.Cells(0).Value
        s_Code = DataGrid1.CurrentRow.Cells(1).Value
        s_Desc = DataGrid1.CurrentRow.Cells(2).Value
        Me.Hide()
    End Sub

    Private Sub DataGrid1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGrid1.KeyDown
        If e.KeyCode = Keys.Enter Then
            If DataGrid1.Rows.Count = 0 Then Exit Sub
            s_ID = DataGrid1.CurrentRow.Cells(0).Value
            s_Code = DataGrid1.CurrentRow.Cells(1).Value
            s_Desc = DataGrid1.CurrentRow.Cells(2).Value
            Me.Close()
        End If
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Cursor = Cursors.WaitCursor
        Select Case bt_Num
            Case 1
                Dim dv = (From c In stProdGroup.Instance.ProdGrp Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 2
                Dim dv = (From c In stProdBrand.Instance.ProdBrn Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 3
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            'Case 4
            '    Dim dv = (From c In frmProductDetail.dtPrd Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
            '    DataGrid1.DataSource = dv
            Case 5
                Dim dv = (From c In frmProductDetail.dtUnt Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 6
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 7
                Dim dv = (From c In stRcvType.Instance.RcvType Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 8
                Dim dv = (From c In frmReceiveKey.dtPd Order By c.Id Where c.Products.Code.Contains(tbSearch.Text) Or c.Code.Contains(tbSearch.Text) Or c.Products.Name.Contains(tbSearch.Text) Or c.ProductUnit.Code.Contains(tbSearch.Text) Or c.PackSize.ToString.Contains(tbSearch.Text) Select New With {c.Id, .J = c.Products.Code, .K = c.Code, .L = c.Products.Name, .M = c.ProductUnit.Code, .N = c.PackSize}).ToList
                DataGrid1.DataSource = dv
            Case 9
                Dim dv = (From c In frmReceiveKey.dtLoc Order By c.Id Where c.FKZone = frmReceiveKey.ZoneID And (c.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text)) Select New With {c.Id, .Code = c.Name, .Name = c.Description}).ToList
                DataGrid1.DataSource = dv
            Case 10
                Dim dv = (From c In stZone.Instance.Zone Order By c.Id Where c.FKWarehouse = frmLocationEdit.WhID And (c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text)) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 11
                Dim dv = (From c In stLocType.Instance.LocType Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 12
                Dim dv = (From c In stZone.Instance.Zone Order By c.Id Where c.Warehouse.Id = frmProductEdit.cbWH.SelectedValue And (c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text)) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 13
                Dim dv = (From c In stProductType.Instance.ProdType Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 14
                Dim dv = (From c In stUnit.Instance.Unit Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            'Case 15
            '    Dim dv = (From c In frmLocation.dtOwn Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
            '    DataGrid1.DataSource = dv
            Case 16 'Owner
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 17
                Dim dv = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 18
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 19
                Dim dv = (From c In stVendor.Instance.VD Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            'Case 20
            '    Dim dv = (From c In frmZone.dtOw Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
            '    DataGrid1.DataSource = dv
            'Case 21
            '    Dim dv = (From c In frmOutLocation.dtVd Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
            '    DataGrid1.DataSource = dv
            Case 22
                Dim dv = (From c In frmOutLocation.dtT Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 23
                Dim dv = (From c In frmOutLocation.dtZ Order By c.Id Where c.FKWarehouse = frmOutLocation.cbWh.SelectedValue And c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 24
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            'Case 25
            '    Dim dv = (From c In frmCustomer.dtOw Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
            '    DataGrid1.DataSource = dv
            Case 26
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 27
                Dim dv = (From c In stCustomer.Instance.Cust Order By c.Id Where c.FKOwner = frmIssueKey.OwnID And (c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text)) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 28
                Dim dv = (From c In stTransport.Instance.Transport Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 29
                Dim dv = (From c In stPickType.Instance.PickType Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 30
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 31
                Dim dv = (From c In frmIssueKey.dtPd Order By c.Id Where c.Products.Code.Contains(tbSearch.Text) Or c.Code.Contains(tbSearch.Text) Or c.Products.Name.Contains(tbSearch.Text) Or c.ProductUnit.Name.Contains(tbSearch.Text) Or c.PackSize.ToString.Contains(tbSearch.Text) Select New With {c.Id, .J = c.Products.Code, .K = c.Code, .L = c.Products.Name, .M = c.ProductUnit.Name, .N = c.PackSize}).ToList
                DataGrid1.DataSource = dv
            Case 32
                Dim dv = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 33
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 34
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 35
                Dim dv = (From c In stCostCenter.Instance.CostCnt Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 36
                Dim dv = (From c In stIOMaster.Instance.IO Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 37
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 38
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 39
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 40
                Dim dv = (From c In stMenu.Instance.Menu Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 41
                Dim dv = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 42
                Dim dv = (From c In frmEditRcvItem.dtLoc Order By c.Id Where (c.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text)) Select New With {c.Id, .Code = c.Name, .Name = c.Description}).ToList
                DataGrid1.DataSource = dv
            Case 43
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 44
                Dim dv = (From c In stZone.Instance.Zone Order By c.Id Where c.FKWarehouse = frmTransfer.WHId And (c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text)) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 45
                Dim dv = (From c In stLocation.Instance.Location Order By c.Id Where c.FKWarehouse = frmTransfer.WHId And c.FKZone = frmTransfer.ZoneID And (c.Name.Contains(tbSearch.Text) Or c.Description.Contains(tbSearch.Text)) Select New With {c.Id, .Code = c.Name, .Name = c.Description}).ToList
                DataGrid1.DataSource = dv
            Case 46
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 47
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 48
                Dim dv = (From c In stOwner.Instance.Own Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 49
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 50
                Dim dv = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 51
                Dim dv = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 52
                Dim dv = (From c In stZone.Instance.Zone Order By c.Id Where c.FKWarehouse = frmLocationOutEdit.WhID And (c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text)) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 53
                Dim dv = (From c In stWarehouse.Instance.WH Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 54
                Dim dv = (From c In stLocType.Instance.LocType Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
            Case 55
                Dim dv = (From c In stItemStatus.Instance.ItemStatus Order By c.Id Where c.Code.Contains(tbSearch.Text) Or c.Name.Contains(tbSearch.Text) Select c.Id, c.Code, c.Name).ToList
                DataGrid1.DataSource = dv
        End Select
        Cursor = Cursors.Default
        DataGrid1.Columns("Id").Visible = False
    End Sub
End Class