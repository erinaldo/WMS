Imports System.Globalization
Imports System.Threading
Imports System.IO
Imports System.Data
Imports System.Data.OleDb

Public Class frmInterfaceOrder

    'Public dtHD As New List(Of PickTempOrderHD)
    'Public dtDTL As New List(Of PickTempOrderDTL)
    Public dtV As New List(Of V_InterfaceOrder)
    Dim docNo As String
    Dim hdID As Integer
    Private Excel03ConString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"
    Private Excel07ConString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR={1}'"
    Dim filePath, extension As String

    Private Sub btLoadData_Click(sender As Object, e As EventArgs) Handles btLoadData.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Dim ds = selSQL("select count(*) cnt from " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_ITEM where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null and documentno in (select documentno from " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_HEAD where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null)")
                If ds.Tables(0).Rows(0).Item("cnt") > 0 Then
                    Using db = New PTGwmsEntities
                        Dim hd = selSQL("select * from " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_HEAD where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null")
                        If hd.Tables(0).Rows.Count > 0 Then
                            Dim i As Integer
                            For i = 0 To hd.Tables(0).Rows.Count - 1
                                db.PickTempOrderHD.Add(New PickTempOrderHD With {
                                .TXLL_ID = hd.Tables(0).Rows(i).Item("TXLL_ID"),
                                .SRC_NAME = hd.Tables(0).Rows(i).Item("SRC_NAME"),
                                .TARGET_NAME = hd.Tables(0).Rows(i).Item("TARGET_NAME"),
                                .COMP_CODE = hd.Tables(0).Rows(i).Item("COMP_CODE"),
                                .DOCUMENTNO = hd.Tables(0).Rows(i).Item("DOCUMENTNO"),
                                .DOCUMENTDATE = hd.Tables(0).Rows(i).Item("DOCUMENTDATE"),
                                .DESCRIPTION = "-",
                                .PICKINGTYPECODE = "12",
                                .CUSTOMERCODE = hd.Tables(0).Rows(i).Item("CUSTOMERCODE"),
                                .CreateDate = DateTimeServer(), .CreateBy = Username,
                                .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                Dim dtl = selSQL("select * from " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_ITEM where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null and documentno in (select documentno from " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_HEAD where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null and documentno = '" & hd.Tables(0).Rows(i).Item("DOCUMENTNO") & "')")
                                If dtl.Tables(0).Rows.Count > 0 Then
                                    Dim k As Integer
                                    For k = 0 To dtl.Tables(0).Rows.Count - 1
                                        db.PickTempOrderDTL.Add(New PickTempOrderDTL With {
                                        .TXLL_ID = dtl.Tables(0).Rows(k).Item("TXLL_ID"),
                                        .SRC_NAME = dtl.Tables(0).Rows(k).Item("SRC_NAME"),
                                        .TARGET_NAME = dtl.Tables(0).Rows(k).Item("TARGET_NAME"),
                                        .DOCUMENTNO = dtl.Tables(0).Rows(k).Item("DOCUMENTNO"),
                                        .WAREHOUSECODE = hd.Tables(0).Rows(i).Item("WAREHOUSECODE"),
                                        .ITEM_NO = dtl.Tables(0).Rows(k).Item("ITEM_NO"),
                                        .PRODUCTCODE = dtl.Tables(0).Rows(k).Item("PRODUCTCODE"),
                                        .PRODUCTQTY = dtl.Tables(0).Rows(k).Item("PRODUCTQTY"),
                                        .ITEMSTATUSCODE = dtl.Tables(0).Rows(k).Item("ITEMSTATUSCODE"),
                                        .PRODUCTUNITCODE = dtl.Tables(0).Rows(k).Item("PRODUCTUNITCODE"),
                                        .CreateDate = DateTimeServer(), .CreateBy = Username,
                                        .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    Next
                                End If
                            Next
                        End If
                        db.SaveChanges()
                        execSQL("update " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_ITEM set inf_status = 'P' where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null and documentno in (select documentno from " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_HEAD where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null)")
                        execSQL("update " & DBLink & "..STAG.TXLL_WMS_LL001_DEMAND_HEAD set inf_status = 'P' where target_name = 'WMS'  and inf_status = 'N'  and error_msg is null")
                        Cursor = Cursors.Default
                        MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SelData()
                    End Using

                Else
                    Cursor = Cursors.Default
                    MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub tbSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbSearch.TextChanged
        Try
            Dim dt = (From c In dtV Where c.DOCUMENTNO.Contains(tbSearch.Text) Or c.DOCUMENTDATE.Contains(tbSearch.Text) Or c.COMP_CODE.Contains(tbSearch.Text) Or c.CUSTOMERCODE.Contains(tbSearch.Text) Select c.HDID, c.DOCUMENTNO, c.DOCUMENTDATE, c.DESCRIPTION, c.PICKINGTYPECODE, c.COMP_CODE, c.CUSTOMERCODE, c.InterfaceName, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy).Distinct.ToList
            Dim dtGrid2 = (From c In dt Order By c.HDID Select New With {.OrderID = c.HDID, .เลขที่ใบขอเบิก = c.DOCUMENTNO, .วันที่ใบขอเบิก = c.DOCUMENTDATE, .รายละเอียด = c.DESCRIPTION, .ประเภท = c.PICKINGTYPECODE, .บริษัท = c.COMP_CODE, .ลูกค้า = c.CUSTOMERCODE, c.InterfaceName, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
            With DataGridView2
                .DataSource = dtGrid2
                '.Columns("Id").Visible = False
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                '.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub SelData()
        Try
            Cursor = Cursors.WaitCursor
            Using db = New PTGwmsEntities
                'dtDTL = db.PickTempOrderDTL.Where(Function(x) x.Enable = True And frmMain.wh.Contains(x.WAREHOUSECODE.ToString)).ToList
                'Dim doc As New List(Of String)
                'If dtDTL.Count > 0 Then
                '    For Each a In dtDTL
                '        doc.Add(a.DOCUMENTNO)
                '    Next
                'End If
                'dtHD = db.PickTempOrderHD.Where(Function(x) x.Enable = True And x.ConfirmDate Is Nothing And frmMain.ow.Contains(x.COMP_CODE.ToString) And doc.Contains(x.DOCUMENTNO.ToString)).ToList
                'dtHD = (From c In db.PickTempOrderHD Where c.Enable = True And c.ConfirmDate Is Nothing).ToList
                dtV = db.V_InterfaceOrder.Where(Function(x) frmMain.ow.Contains(x.COMP_CODE.ToString) And frmMain.wh.Contains(x.WAREHOUSECODE.ToString)).ToList
                Dim dt = (From c In dtV Select c.HDID, c.DOCUMENTNO, c.DOCUMENTDATE, c.DESCRIPTION, c.PICKINGTYPECODE, c.COMP_CODE, c.CUSTOMERCODE, c.InterfaceName, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy).Distinct.ToList
                Dim dtGrid2 = (From c In dt Order By c.HDID Select New With {.OrderID = c.HDID, .เลขที่ใบขอเบิก = c.DOCUMENTNO, .วันที่ใบขอเบิก = c.DOCUMENTDATE, .รายละเอียด = c.DESCRIPTION, .ประเภท = c.PICKINGTYPECODE, .บริษัท = c.COMP_CODE, .ลูกค้า = c.CUSTOMERCODE, c.InterfaceName, .CreateDate = Format(c.CreateDate, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy, .UpdateDate = Format(c.UpdateDate, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy}).ToList
                With DataGridView2
                    .DataSource = dtGrid2
                    '.Columns("Id").Visible = False
                    .AutoResizeColumns()
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                    '.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    '.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End With
            End Using
            DataGridView1.DataSource = Nothing
            btConfirm.Enabled = False
            btCancelDemand.Enabled = False
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Sub SelData2()
        Try
            Dim dtGrid = (From c In dtV Order By CInt(c.ITEM_NO) Where c.DOCUMENTNO = docNo Select New With {c.Id, .ItemNo = c.ITEM_NO, .รหัสสินค้า = c.PRODUCTCODE, .ชื่อสินค้า = c.PRODUCTNAME, .จำนวน = c.PRODUCTQTY, .ItemStatus = c.ITEMSTATUSCODE, .หน่วยนับ = c.PRODUCTUNITCODE, .คลัง = c.WAREHOUSECODE, c.InterfaceName1, .CreateDate = Format(c.CreateDate1, "dd/MM/yyyy HH:mm"), .CreateBy = c.CreateBy1, .UpdateDate = Format(c.UpdateDate1, "dd/MM/yyyy HH:mm"), .UpdateBy = c.UpdateBy1}).ToList
            With DataGridView1
                .DataSource = dtGrid
                .Columns("Id").Visible = False
                '.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                '.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .AutoResizeColumns()
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub DataGridView2_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If e.RowIndex <> -1 Then
            hdID = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            docNo = DataGridView2.Rows(e.RowIndex).Cells(1).Value.ToString
            SelData2()
            btConfirm.Enabled = True
            btCancelDemand.Enabled = True
        End If
    End Sub

    Private Sub dgvUserDetails_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView1.RowPostPaint
        Using b As New SolidBrush(DataGridView1.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub dgvUserDetails1_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles DataGridView2.RowPostPaint
        Using b As New SolidBrush(DataGridView2.RowHeadersDefaultCellStyle.ForeColor)
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4)
        End Using
    End Sub

    Private Sub btConfirm_Click(sender As Object, e As EventArgs) Handles btConfirm.Click
        If MessageBox.Show("ต้องการ Convert Data ใช่หรือไม่?", "ยืนยันการ Confirm Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Dim ds = (From c In dtV Where c.HDID = hdID).FirstOrDefault
                If Not IsNothing(ds) Then
                    Cursor = Cursors.WaitCursor
                    Using db = New PTGwmsEntities
                        Dim chkID = (From c In db.PickTempOrderHD Where c.Id = hdID And c.ConfirmDate IsNot Nothing).FirstOrDefault
                        If Not IsNothing(chkID) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("รายการนี้ Convert ไปแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Dim compCode = (From c In db.Owners Where c.Enable = True And c.FKCompany = CompID And c.Code = ds.COMP_CODE Select c.Id, c.Code).FirstOrDefault
                        If IsNothing(compCode) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล Owner : " & ds.COMP_CODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Dim CustCode = (From c In db.Customers Where c.Enable = True And c.FKCompany = CompID And c.Code = ds.CUSTOMERCODE Select c.Id, c.Code).FirstOrDefault
                        If IsNothing(CustCode) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล Customer : " & ds.CUSTOMERCODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        'Dim dtPType = (From c In db.PickingType Where Enabled = True And c.FKCompany = CompID And c.Code = "01").FirstOrDefault 'ประเภทเบิกทั้วไป
                        'If IsNothing(dtPType) Then
                        '    Cursor = Cursors.Default
                        '    MessageBox.Show("ไม่พบข้อมูล ประเภทเบิกทั่วไป (01)", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        '    Exit Sub
                        'End If
                        Dim dtPType = (From c In db.PickingType Where Enabled = True And c.FKCompany = CompID And c.Code = ds.PICKINGTYPECODE).FirstOrDefault
                        If IsNothing(dtPType) Then
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล รหัสประเภทเบิก : " & ds.PICKINGTYPECODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Dim dtProd = (From c In db.Products Where c.Enable = True And c.FKCompany = CompID And c.FKOwner = compCode.Id).ToList
                        Dim dtUnit = (From c In db.ProductUnit Where c.Enable = True And c.FKCompany = CompID).ToList
                        Dim dtStat = (From c In db.ItemStatus Where c.Enable = True And c.FKCompany = CompID).ToList
                        Dim docDate As String = ds.DOCUMENTDATE
                        Dim _date As DateTime = DateTime.ParseExact(docDate, "yyyyMMdd", CultureInfo.InvariantCulture)

                        Dim PickOrderHD = New PickOrderHD With {.SrcName = ds.SRC_NAME, .InterfaceName = ds.InterfaceName, .OrderNo = ds.DOCUMENTNO, .OrderDate = _date, .RefNo = "-", .PickingDate = DateTimeServer(), .FKCompany = CompID, .FKPickingType = dtPType.Id, .FKTransport = 1, .FKCustomer = CustCode.Id, .FKOwner = compCode.Id, .Description = "-", .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True}
                        Dim dsDTL = (From c In dtV Where c.DOCUMENTNO = ds.DOCUMENTNO).ToList
                        If dsDTL.Count > 0 Then
                            For Each L In dsDTL
                                Dim WhCode = (From c In db.Warehouse Where c.FKCompany = CompID And c.Enable = True And c.Code = L.WAREHOUSECODE Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(WhCode) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล Warehouse : " & L.WAREHOUSECODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim PdHD = (From c In dtProd Where c.Enable = True And c.FKCompany = CompID And c.FKOwner = compCode.Id And c.Zone.FKWarehouse = WhCode.Id And c.Code = L.PRODUCTCODE Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(PdHD) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล Products : " & L.PRODUCTCODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim UnID = (From c In dtUnit Where c.Enable = True And c.FKCompany = CompID And c.Code = L.PRODUCTUNITCODE Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(UnID) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล Product Unit : " & L.PRODUCTUNITCODE & " (" & L.PRODUCTCODE & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim itmID = (From c In dtStat Where c.Enable = True And c.FKCompany = CompID And c.Code = L.ITEMSTATUSCODE Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(itmID) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล Item Status : " & L.ITEMSTATUSCODE, "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                Dim PdDTL = (From c In db.ProductDetails Where c.Enable = True And c.FKProduct = PdHD.Id And c.FKProductUnit = UnID.Id Select c.Id, c.Code).FirstOrDefault
                                If IsNothing(PdDTL) Then
                                    Cursor = Cursors.Default
                                    MessageBox.Show("ไม่พบข้อมูล หน่วยสินค้า : " & L.PRODUCTUNITCODE & " (" & L.PRODUCTCODE & ")", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End If
                                PickOrderHD.PickOrderDTL.Add(New PickOrderDTL With {.FKWarehouse = WhCode.Id, .InterfaceName = L.InterfaceName, .FKProductDetail = PdDTL.Id, .FKItemStatus = itmID.Id, .Qty = CDbl(L.PRODUCTQTY), .QtyConfirm = CDbl(L.PRODUCTQTY), .Remark = "", .ItemNo = L.ITEM_NO, .Func = L.Func, .ItemCate = L.ItemCate, .PLANT = L.PLANT, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            Next
                        End If
                        Dim U = (From c In db.PickTempOrderHD Where c.Id = hdID).FirstOrDefault
                        If Not IsNothing(U) Then
                            U.ConfirmDate = DateTimeServer()
                            U.ConfirmBy = Username
                        End If
                        db.PickOrderHD.Add(PickOrderHD)
                        db.SaveChanges()
                    End Using
                    Cursor = Cursors.Default
                    MessageBox.Show("Convert Data เรียบร้อย", "Convert Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    SelData()
                Else
                    Cursor = Cursors.Default
                    MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtImportExcel_Click(sender As Object, e As EventArgs) Handles BtImportExcel.Click
        'OpenFileDialog1.Multiselect = True
        'OpenFileDialog1.Filter = "Excel Files|*.xlsx|*.xlsm|*.xlsb|*.xls|*‌​.xml"
        'OpenFileDialog1.ShowDialog()
        If TextBox1.Text = "" Then
            Exit Sub
        End If
        Try
            Cursor = Cursors.WaitCursor
            Dim header As String = "YES"
            Dim conStr As String, sheetName As String

            conStr = String.Empty
            Select Case extension

                Case ".xls"
                    conStr = String.Format(Excel03ConString, filePath, header)
                    Exit Select

                Case ".xlsx"
                    conStr = String.Format(Excel07ConString, filePath, header)
                    Exit Select
            End Select
            Using con As New OleDbConnection(conStr)
                Using cmd As New OleDbCommand()
                    cmd.Connection = con
                    con.Open()
                    Dim dtExcelSchema As DataTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                    sheetName = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
                    con.Close()
                End Using
            End Using
            Using con As New OleDbConnection(conStr)
                Using cmd As New OleDbCommand()
                    Using oda As New OleDbDataAdapter()
                        Dim dt As New DataTable()
                        cmd.CommandText = (Convert.ToString("SELECT * From [") & sheetName) + "]"
                        cmd.Connection = con
                        con.Open()
                        oda.SelectCommand = cmd
                        oda.Fill(dt)
                        con.Close()
                        If dt.Rows.Count > 0 Then
                            Using db = New PTGwmsEntities
                                db.PickTempOrderHD.Add(New PickTempOrderHD With
                            {.TXLL_ID = 0,
                                .SRC_NAME = dt.Rows(0).Item("SRC_NAME").ToString(),
                                .COMP_CODE = dt.Rows(0).Item("COMP_CODE").ToString(),
                                .DOCUMENTNO = dt.Rows(0).Item("DOCUMENTNO").ToString(),
                                .DOCUMENTDATE = dt.Rows(0).Item("DOCUMENTDATE"),
                                .DESCRIPTION = dt.Rows(0).Item("DESCRIPTION").ToString(),
                                .PICKINGTYPECODE = dt.Rows(0).Item("PICKINGTYPECODE").ToString(),
                                .CUSTOMERCODE = dt.Rows(0).Item("CUSTOMERCODE").ToString(),
                                .CreateDate = DateTimeServer(),
                                .CreateBy = Username,
                                .UpdateDate = DateTimeServer(),
                                .UpdateBy = Username,
                                .Enable = True
                            })

                                For Each dr As DataRow In dt.Rows
                                    db.PickTempOrderDTL.Add(New PickTempOrderDTL With
                                {
                                .TXLL_ID = 0,
                                .SRC_NAME = dr("SRC_NAME").ToString(),
                                .DOCUMENTNO = dr("DOCUMENTNO").ToString(),
                                .WAREHOUSECODE = dr("WAREHOUSECODE").ToString(),
                                .ITEM_NO = dr("ITEM_NO").ToString(),
                                .PRODUCTCODE = dr("PRODUCTCODE").ToString(),
                                .PRODUCTNAME = dr("PRODUCTNAME").ToString(),
                                .PRODUCTQTY = dr("PRODUCTQTY").ToString(),
                                .ITEMSTATUSCODE = dr("ITEMSTATUSCODE").ToString(),
                                .PRODUCTUNITCODE = dr("PRODUCTUNITCODE").ToString(),
                                .CreateDate = DateTimeServer(),
                                .CreateBy = Username,
                                .UpdateDate = DateTimeServer(),
                                .UpdateBy = Username,
                                .Enable = True
                                })
                                Next
                                db.SaveChanges()
                            End Using
                            Cursor = Cursors.Default
                            MessageBox.Show("Inport Excel เรียบร้อย", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            TextBox1.Text = ""
                        Else
                            Cursor = Cursors.Default
                            MessageBox.Show("ไม่พบข้อมูล", "เเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            TextBox1.Text = ""
                            Exit Sub
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TextBox1.Text = ""
            Exit Sub
        End Try
    End Sub

    Private Sub frmInterfaceOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        btConfirm.Enabled = False
        btCancelDemand.Enabled = False
    End Sub

    Private Sub btRefresh_Click(sender As Object, e As EventArgs) Handles btRefresh.Click
        SelData()
    End Sub

    Private Sub btLoadSD_Click(sender As Object, e As EventArgs) Handles btLoadSD.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim ssd = (From c In db.SaveSalesOrderDetails Where c.Enable = True And c.TakeOrderDate Is Nothing And c.Status = 11 And frmMain.ow.Contains(c.CompCode.ToString) And frmMain.wh.Contains(c.Plant.ToString)).ToList
                    Dim sd = (From c In ssd Select c.CompCode, c.SDDoc, c.SoldTo, c.ReqDate, c.FKSaveSalesOrder).Distinct.ToList
                    If sd.Count > 0 Then
                        For Each s In sd
                            'กรณีที่มีการ Load ข้อมูลเข้ามาอีกรอบ--------
                            Dim pHD = (From c In db.PickOrderHD Where c.Enable = True And c.OrderNo = s.SDDoc And c.ConfirmDate IsNot Nothing).FirstOrDefault
                            If Not IsNothing(pHD) Then
                                pHD.Enable = False
                                pHD.UpdateBy = Username
                                pHD.UpdateDate = DateTimeServer()
                                Dim pDTL = (From c In db.PickOrderDTL Where c.Enable = True And c.FKPickOrderHD = pHD.Id).ToList
                                For Each p In pDTL
                                    p.Enable = False
                                    p.UpdateBy = Username
                                    p.UpdateDate = DateTimeServer()
                                Next
                                Dim chk = (From c In db.PickTempOrderHD Where c.Enable = True And c.DOCUMENTNO = s.SDDoc).FirstOrDefault
                                If Not IsNothing(chk) Then
                                    chk.Enable = False
                                    chk.UpdateBy = Username
                                    chk.UpdateDate = DateTimeServer()
                                    Dim dtl = (From c In db.PickTempOrderDTL Where c.Enable = True And c.DOCUMENTNO = s.SDDoc).ToList
                                    For Each d In dtl
                                        d.Enable = False
                                        d.UpdateBy = Username
                                        d.UpdateDate = DateTimeServer()
                                    Next
                                End If
                            Else
                                'ดักกรณีที่ยังไม่ได้กดปุ่ม Convert
                                Dim chk = (From c In db.PickTempOrderHD Where c.Enable = True And c.DOCUMENTNO = s.SDDoc).FirstOrDefault
                                If Not IsNothing(chk) Then
                                    chk.Enable = False
                                    chk.UpdateBy = Username
                                    chk.UpdateDate = DateTimeServer()
                                    Dim dtl = (From c In db.PickTempOrderDTL Where c.Enable = True And c.DOCUMENTNO = s.SDDoc).ToList
                                    For Each d In dtl
                                        d.Enable = False
                                        d.UpdateBy = Username
                                        d.UpdateDate = DateTimeServer()
                                    Next
                                End If
                            End If
                            '--------------------------------------

                            db.PickTempOrderHD.Add(New PickTempOrderHD With {.SRC_NAME = "SAP", .InterfaceName = "SD003", .COMP_CODE = s.CompCode, .DOCUMENTNO = s.SDDoc, .DOCUMENTDATE = s.ReqDate.ToString("yyyyMMdd"), .PICKINGTYPECODE = "01", .CUSTOMERCODE = s.SoldTo, .HeaderID = s.FKSaveSalesOrder, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            Dim sdd = (From c In ssd Where c.FKSaveSalesOrder = s.FKSaveSalesOrder).ToList
                            If sdd.Count > 0 Then
                                For Each t In sdd
                                    db.PickTempOrderDTL.Add(New PickTempOrderDTL With {.SRC_NAME = "SAP", .InterfaceName = "SD003", .DOCUMENTNO = s.SDDoc, .PRODUCTCODE = t.Material, .PRODUCTNAME = t.MaterialText, .PRODUCTQTY = t.BaseQty, .ITEMSTATUSCODE = "01", .WAREHOUSECODE = t.WMsWH, .PRODUCTUNITCODE = t.BaseUnit, .FKHeader = t.FKSaveSalesOrder, .Func = t.Func, .ItemCate = t.ItemCate, .PLANT = t.Plant, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    t.TakeOrderDate = DateTimeServer()
                                    t.TakeOrderBy = Username
                                Next
                            End If
                        Next
                        db.SaveChanges()
                        Cursor = Cursors.Default
                        MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SelData()
                    Else
                        Cursor = Cursors.Default
                        MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btSTO_Click(sender As Object, e As EventArgs) Handles btSTO.Click
        If MessageBox.Show("ต้องการ Load Data ใช่หรือไม่?", "ยืนยันการ Load Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim ssd = (From c In db.SaveDeliveryOrderDetails Where c.TakeOrderDate Is Nothing And c.Status = 11 And frmMain.ow.Contains(c.CompCode.ToString) And frmMain.wh.Contains(c.Plant.ToString) And c.RejectDate Is Nothing).ToList
                    Dim sd = (From c In ssd Select c.CompCode, c.DODoc, c.ShipTo, c.DODate, c.FKSaveDeliveryOrder).Distinct.ToList
                    If sd.Count > 0 Then
                        For Each s In sd
                            db.PickTempOrderHD.Add(New PickTempOrderHD With {.SRC_NAME = "SAP", .InterfaceName = "SD031", .COMP_CODE = s.CompCode, .DOCUMENTNO = s.DODoc, .DOCUMENTDATE = s.DODate.ToString("yyyyMMdd"), .PICKINGTYPECODE = "01", .CUSTOMERCODE = s.ShipTo, .HeaderID = s.FKSaveDeliveryOrder, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                            Dim sdd = (From c In ssd Where c.FKSaveDeliveryOrder = s.FKSaveDeliveryOrder).ToList
                            If sdd.Count > 0 Then
                                For Each t In sdd
                                    db.PickTempOrderDTL.Add(New PickTempOrderDTL With {.SRC_NAME = "SAP", .InterfaceName = "SD031", .DOCUMENTNO = s.DODoc, .PRODUCTCODE = t.Material, .PRODUCTNAME = t.MaterialText, .PRODUCTQTY = t.BaseQty, .ITEMSTATUSCODE = "01", .WAREHOUSECODE = t.WMsWH, .PRODUCTUNITCODE = t.BaseUnit, .FKHeader = t.FKSaveDeliveryOrder, .Func = t.Func, .ItemCate = t.ItemCate, .PLANT = t.Plant, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True})
                                    t.TakeOrderDate = DateTimeServer()
                                    t.TakeOrderBy = Username
                                Next
                            End If
                        Next
                        db.SaveChanges()
                        Cursor = Cursors.Default
                        MessageBox.Show("Load Data เรียบร้อย", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        SelData()
                    Else
                        Cursor = Cursors.Default
                        MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        filePath = OpenFileDialog1.FileName
        extension = Path.GetExtension(filePath)
        TextBox1.Text = filePath
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Multiselect = True
        OpenFileDialog1.Filter = "Excel Files|*.xlsx|*.xlsm|*.xlsb|*.xls|*‌​.xml"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub btCancelDemand_Click(sender As Object, e As EventArgs) Handles btCancelDemand.Click
        If MessageBox.Show("ต้องการยืนยัน ยกเลิก Demand ใช่หรือไม่?", "ยืนยันการยกเลิก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim dt = (From c In db.PickTempOrderHD Where c.Id = hdID).ToList
                    For Each hd In dt
                        hd.Enable = False
                        hd.UpdateDate = DateTimeServer()
                        hd.UpdateBy = Username
                        Dim pkLoc = (From c In db.PickTempOrderDTL Where c.DOCUMENTNO = hd.DOCUMENTNO).ToList
                        For Each p In pkLoc
                            p.Enable = False
                            p.UpdateDate = DateTimeServer()
                            p.UpdateBy = Username
                        Next
                    Next
                    db.SaveChanges()
                End Using
                Cursor = Cursors.Default
                MessageBox.Show("ยกเลิก Demand เรียบร้อย", "Cancel Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btRefresh_Click(sender, e)
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
End Class