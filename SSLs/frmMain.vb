Public Class frmMain

    Public wh As New List(Of String)
    Public ow As New List(Of String)

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If MessageBox.Show("ต้องการออกจากโปรแแกรมใช่หรือไม่", "ออกจากโปรแกรม ?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim ds = (From c In db.Users Where c.Enable = True And c.Id = Username).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.StatusUse = False
                        db.SaveChanges()
                        Cursor = Cursors.Default
                    End If
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Timer1.Stop()
            e.Cancel = False
            End
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel2.Text = " |   วันที่ : " & Date.Now
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = Me.Text & " " & My.Application.Deployment.CurrentVersion.ToString
        Catch ex As Exception
            Me.Text = Me.Text
        End Try

        ToolStripStatusLabel1.Text = "ผู้ใช้งาน : " & UName
        ToolStripStatusLabel3.Text = " |   บริษัท : " & Comp
        ToolStripStatusLabel4.Text = " |   Developer : Seino Team"
        Timer1.Start()
        Using db = New PTGwmsEntities
            If RoleID = 1 Then
                Dim wha = (From c In db.Warehouse Where c.FKCompany = CompID And c.Enable = True).ToList
                For Each a In wha
                    wh.Add(a.Code)
                Next
                Dim Own = (From c In db.Owners Where c.FKCompany = CompID And c.Enable = True).ToList
                For Each a In Own
                    ow.Add(a.Code)
                Next
            Else
                Dim wha = (From c In db.WarehouseAccess Where c.FKCompany = CompID And c.Enable = True And c.FKUser = Username).ToList
                For Each a In wha
                    wh.Add(a.Warehouse.Code)
                Next
                Dim Own = (From c In db.OwnerAccess Where c.FKCompany = CompID And c.Enable = True And c.FKUser = Username).ToList
                For Each a In Own
                    ow.Add(a.Owners.Code)
                Next
            End If

            If RoleID <> 1 Then
                For Each item As ToolStripMenuItem In Me.MenuStrip1.Items
                    For Each menu As ToolStripItem In DirectCast(item, ToolStripMenuItem).DropDownItems
                        menu.Enabled = False
                    Next
                Next
                Dim mnu = (From c In db.MenuAccess Where c.Enable = True And c.FKRole = RoleID).ToList
                If mnu.Count > 0 Then
                    For Each a In mnu
                        For Each item As ToolStripMenuItem In Me.MenuStrip1.Items
                            For Each menu As ToolStripItem In DirectCast(item, ToolStripMenuItem).DropDownItems
                                If a.Menu.DBField = menu.Name Then
                                    menu.Enabled = True
                                End If
                            Next
                        Next
                    Next
                End If
            End If
        End Using
    End Sub

    Private Sub RoleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RoleToolStripMenuItem.Click
        frmRole.MdiParent = Me
        frmRole.Show()
        frmRole.Focus()
    End Sub

    Private Sub UserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserToolStripMenuItem.Click
        frmUser.MdiParent = Me
        frmUser.Show()
        frmUser.Focus()
    End Sub

    Private Sub ZoneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoneToolStripMenuItem.Click
        frmZone.MdiParent = Me
        frmZone.Show()
        frmZone.Focus()
    End Sub

    Private Sub LocationTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocationTypeToolStripMenuItem.Click
        frmLocationType.MdiParent = Me
        frmLocationType.Show()
        frmLocationType.Focus()
    End Sub

    Private Sub WarehouseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WarehouseToolStripMenuItem.Click
        frmWarehouse.MdiParent = Me
        frmWarehouse.Show()
        frmWarehouse.Focus()
    End Sub

    Private Sub LocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocationToolStripMenuItem.Click
        frmLocation.MdiParent = Me
        frmLocation.Show()
        frmLocation.Focus()
    End Sub

    Private Sub RcvTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RcvTypeToolStripMenuItem.Click
        frmRcvType.MdiParent = Me
        frmRcvType.Show()
        frmRcvType.Focus()
    End Sub

    Private Sub ItemStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ItemStatusToolStripMenuItem.Click
        frmItemStatus.MdiParent = Me
        frmItemStatus.Show()
        frmItemStatus.Focus()
    End Sub

    Private Sub ProductGroupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductGroupToolStripMenuItem.Click
        frmProductGroup.MdiParent = Me
        frmProductGroup.Show()
        frmProductGroup.Focus()
    End Sub

    Private Sub ProductBrandToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductBrandToolStripMenuItem.Click
        frmProductBrand.MdiParent = Me
        frmProductBrand.Show()
        frmProductBrand.Focus()
    End Sub

    Private Sub ProductUnitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductUnitToolStripMenuItem.Click
        frmProductUnit.MdiParent = Me
        frmProductUnit.Show()
        frmProductUnit.Focus()
    End Sub

    Private Sub PickingTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PickingTypeToolStripMenuItem.Click
        frmPickingType.MdiParent = Me
        frmPickingType.Show()
        frmPickingType.Focus()
    End Sub

    Private Sub ProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem.Click
        frmProducts.MdiParent = Me
        frmProducts.Show()
        frmProducts.Focus()
    End Sub

    Private Sub KeyReceiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KeyReceiveToolStripMenuItem.Click
        frmReceiveKey.MdiParent = Me
        frmReceiveKey.Show()
        frmReceiveKey.Focus()
    End Sub

    Private Sub ReceiveToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ReceiveToolStripMenuItem1.Click
        frmReceive.MdiParent = Me
        frmReceive.Show()
        frmReceive.Focus()
    End Sub

    Private Sub ProductTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductTypeToolStripMenuItem.Click
        frmProductType.MdiParent = Me
        frmProductType.Show()
        frmProductType.Focus()
    End Sub

    Private Sub VendorsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VendorsToolStripMenuItem.Click
        frmVendor.MdiParent = Me
        frmVendor.Show()
        frmVendor.Focus()
    End Sub

    Private Sub CompanyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyToolStripMenuItem.Click
        frmCompany.MdiParent = Me
        frmCompany.Show()
        frmCompany.Focus()
    End Sub

    Private Sub OwnersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OwnersToolStripMenuItem.Click
        frmOwner.MdiParent = Me
        frmOwner.Show()
        frmOwner.Focus()
    End Sub

    Private Sub OutLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutLocationToolStripMenuItem.Click
        frmLocationOut.MdiParent = Me
        frmLocationOut.Show()
        frmLocationOut.Focus()
    End Sub

    Private Sub TransportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransportToolStripMenuItem.Click
        frmTransport.MdiParent = Me
        frmTransport.Show()
        frmTransport.Focus()
    End Sub

    Private Sub CustomerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerToolStripMenuItem.Click
        frmCustomer.MdiParent = Me
        frmCustomer.Show()
        frmCustomer.Focus()
    End Sub

    Private Sub IssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IssueToolStripMenuItem.Click
        frmIssueKey.MdiParent = Me
        frmIssueKey.Show()
        frmIssueKey.Focus()
    End Sub

    Private Sub InterfacePOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterfacePOToolStripMenuItem.Click
        frmInterfaceRcv.MdiParent = Me
        frmInterfaceRcv.Show()
        frmInterfaceRcv.Focus()
    End Sub

    Private Sub ManageOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ManageOrderToolStripMenuItem.Click
        frmOrderManage.MdiParent = Me
        frmOrderManage.Show()
        frmOrderManage.Focus()
    End Sub

    Private Sub PickingControlsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PickingControlsToolStripMenuItem.Click
        frmPickingCon.MdiParent = Me
        frmPickingCon.Show()
        frmPickingCon.Focus()
    End Sub

    Private Sub LoadDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadDataToolStripMenuItem.Click
        frmInterfaceOrder.MdiParent = Me
        frmInterfaceOrder.Show()
        frmInterfaceOrder.Focus()
    End Sub

    Private Sub EmployeeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeToolStripMenuItem.Click
        frmEmployee.MdiParent = Me
        frmEmployee.Show()
        frmEmployee.Focus()
    End Sub

    Private Sub DepartmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentToolStripMenuItem.Click
        frmDepartment.MdiParent = Me
        frmDepartment.Show()
        frmDepartment.Focus()
    End Sub

    Private Sub CostCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CostCenterToolStripMenuItem.Click
        frmCostCenter.MdiParent = Me
        frmCostCenter.Show()
        frmCostCenter.Focus()
    End Sub

    Private Sub IOMasterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IOMasterToolStripMenuItem.Click
        frmIOMaster.MdiParent = Me
        frmIOMaster.Show()
        frmIOMaster.Focus()
    End Sub

    Private Sub PostGIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PostGIToolStripMenuItem.Click
        frmPostGI.MdiParent = Me
        frmPostGI.Show()
        frmPostGI.Focus()
    End Sub

    Private Sub StockBalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockBalToolStripMenuItem.Click
        frmRptStockBal.MdiParent = Me
        frmRptStockBal.Show()
        frmRptStockBal.Focus()
    End Sub

    Private Sub StockCardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockCardToolStripMenuItem.Click
        frmRptStockCard.MdiParent = Me
        frmRptStockCard.Show()
        frmRptStockCard.Focus()
    End Sub

    Private Sub PhyCountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PhyCountToolStripMenuItem.Click
        frmPhyCount.MdiParent = Me
        frmPhyCount.Show()
        frmPhyCount.Focus()
    End Sub

    Private Sub RptStockLocationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptStockLocationToolStripMenuItem.Click
        frmStockLocation.MdiParent = Me
        frmStockLocation.Show()
        frmStockLocation.Focus()
    End Sub

    Private Sub ReturnToVendorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReturnToVendorToolStripMenuItem.Click
        frmRTV.MdiParent = Me
        frmRTV.Show()
        frmRTV.Focus()
    End Sub

    Private Sub MenuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuToolStripMenuItem.Click
        frmMenu.MdiParent = Me
        frmMenu.Show()
        frmMenu.Focus()
    End Sub

    Private Sub MenuManToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuManToolStripMenuItem.Click
        frmMenuMan.MdiParent = Me
        frmMenuMan.Show()
        frmMenuMan.Focus()
    End Sub

    Private Sub RptRcvStatusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptRcvStatusToolStripMenuItem.Click
        frmrtpRcvStatus.MdiParent = Me
        frmrtpRcvStatus.Show()
        frmrtpRcvStatus.Focus()
    End Sub

    Private Sub RptStatusIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptStatusIssueToolStripMenuItem.Click
        frmrptIssueStatus.MdiParent = Me
        frmrptIssueStatus.Show()
        frmrptIssueStatus.Focus()
    End Sub

    Private Sub TransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferToolStripMenuItem.Click
        frmTransfer.MdiParent = Me
        frmTransfer.Show()
        frmTransfer.Focus()
    End Sub

    Private Sub TransferConfirmToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransferConfirmToolStripMenuItem.Click
        frmTransferConfirm.MdiParent = Me
        frmTransferConfirm.Show()
        frmTransferConfirm.Focus()
    End Sub

    Private Sub RptTransferToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptTransferToolStripMenuItem.Click
        frmrptTransfer.MdiParent = Me
        frmrptTransfer.Show()
        frmrptTransfer.Focus()
    End Sub

    Private Sub CycleCountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CycleCountToolStripMenuItem.Click
        frmCycleCount.MdiParent = Me
        frmCycleCount.Show()
        frmCycleCount.Focus()
    End Sub

    Private Sub RptCycCoutnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptCycCoutnToolStripMenuItem.Click
        frmrptCycleCount.MdiParent = Me
        frmrptCycleCount.Show()
        frmrptCycleCount.Focus()
    End Sub

    Private Sub AdjStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjStockToolStripMenuItem.Click
        frmAdjStock.MdiParent = Me
        frmAdjStock.Show()
        frmAdjStock.Focus()
    End Sub

    Private Sub AdjReasonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjReasonToolStripMenuItem.Click
        frmAdjustReason.MdiParent = Me
        frmAdjustReason.Show()
        frmAdjustReason.Focus()
    End Sub

    Private Sub GL100APToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GL100APToolStripMenuItem.Click
        frmGL100AP.MdiParent = Me
        frmGL100AP.Show()
        frmGL100AP.Focus()
    End Sub

    Private Sub GL100ARToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GL100ARToolStripMenuItem.Click
        frmGL100AR.MdiParent = Me
        frmGL100AR.Show()
        frmGL100AR.Focus()
    End Sub

    Private Sub BoxSizeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoxSizeToolStripMenuItem.Click
        frmBoxSize.MdiParent = Me
        frmBoxSize.Show()
        frmBoxSize.Focus()
    End Sub

    Private Sub VehicleTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VehicleTypeToolStripMenuItem.Click
        frmVehicleType.MdiParent = Me
        frmVehicleType.Show()
        frmVehicleType.Focus()
    End Sub

    Private Sub OtherfeesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OtherfeesToolStripMenuItem.Click
        frmOtherfees.MdiParent = Me
        frmOtherfees.Show()
        frmOtherfees.Focus()
    End Sub

    Private Sub RptIntermalBarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptIntermalBarToolStripMenuItem.Click
        frmRptPrintInternalBar.MdiParent = Me
        frmRptPrintInternalBar.Show()
        frmRptPrintInternalBar.Focus()
    End Sub

    Private Sub PrintBarLocToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintBarLocToolStripMenuItem.Click
        frmPrintBarLoc.MdiParent = Me
        frmPrintBarLoc.Show()
        frmPrintBarLoc.Focus()
    End Sub

    Private Sub RptIssueToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptIssueToolStripMenuItem.Click
        frmrptIssueConfirm.MdiParent = Me
        frmrptIssueConfirm.Show()
        frmrptIssueConfirm.Focus()
    End Sub

    Private Sub RptReceiveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptReceiveToolStripMenuItem.Click
        frmRcvReport.MdiParent = Me
        frmRcvReport.Show()
        frmRcvReport.Focus()
    End Sub

    Private Sub RptPhyCountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptPhyCountToolStripMenuItem.Click
        frmReportStockCount.MdiParent = Me
        frmReportStockCount.Show()
        frmReportStockCount.Focus()
    End Sub

    Private Sub RptStockBalHisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RptStockBalHisToolStripMenuItem.Click
        frmRptStockOnhandHis.MdiParent = Me
        frmRptStockOnhandHis.Show()
        frmRptStockOnhandHis.Focus()
    End Sub

    Private Sub AdjustProdDateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustProdDateToolStripMenuItem.Click
        frmAdjustProdDate.MdiParent = Me
        frmAdjustProdDate.Show()
        frmAdjustProdDate.Focus()
    End Sub
End Class