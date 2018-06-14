Public Class frmLocationEdit

    Public SId, ZId, LtId, WhID As Integer
    Dim codeOld As String

    Private Sub frmLocationEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        codeOld = ""
        If sEdit = True Then
            Dim Sel = (From c In stLocation.Instance.Location Where c.Id = frmLocation.SId).FirstOrDefault
            If Not IsNothing(Sel) Then
                WhID = Sel.FKWarehouse
                tbWarehouse.Text = Sel.Warehouse.Code
                tbWarehouseDesc.Text = Sel.Warehouse.Name
                tbLocation.Text = Sel.Name
                codeOld = Sel.Name
                tbZone.Text = Sel.Zone.Code
                tbZoneDesc.Text = Sel.Zone.Name
                tbType.Text = Sel.LocationType.Code
                tbTypeDesc.Text = Sel.LocationType.Name
                tbDesc.Text = Sel.Description
                CheckBox1.Checked = Sel.AutoReceive
                btDelete.Enabled = True
            End If
        Else
            ClearData()
        End If
    End Sub

    Sub ClearData()
        ClearTextBox(Me)
        btDelete.Enabled = False
        tbLocation.Focus()
    End Sub

    Private Sub TextBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbWarehouse.KeyDown, tbLocation.KeyDown, tbZone.KeyDown, tbType.KeyDown, tbDesc.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub btDelete_Click(sender As System.Object, e As System.EventArgs) Handles btDelete.Click
        If MessageBox.Show("ต้องการลบข้อมูล ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Using db = New PTGwmsEntities
                Try
                    Dim ds = (From c In stLocation.Instance.Location Where c.Id = frmLocation.SId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.UpdateDate = DateTimeServer()
                        ds.UpdateBy = Username
                        ds.Enable = False
                        stLocation.Instance.Location.Remove(ds)
                    End If
                    Dim ds1 = (From c In db.Locations Where c.Id = frmLocation.SId).FirstOrDefault
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
            Me.Close()
            frmLocation.SelData()
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbWarehouse.Text = "" Then
            MessageBox.Show("กรุณาป้อน คลัง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbWarehouse.Focus()
            Exit Sub
        End If
        If tbLocation.Text = "" Then
            MessageBox.Show("กรุณาป้อน Location", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbLocation.Focus()
            Exit Sub
        End If
        If tbZone.Text = "" Then
            MessageBox.Show("กรุณาป้อน โซนจัดเก็บ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbZone.Focus()
            Exit Sub
        End If
        If tbType.Text = "" Then
            MessageBox.Show("กรุณาป้อน ประเภท", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbType.Focus()
            Exit Sub
        End If

        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    If sNew = True Then
                        If Not IsNothing((From c In db.Locations Where c.FKCompany = CompID And c.FKWarehouse = WhID And c.Name = tbLocation.Text).FirstOrDefault) Then
                            MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            tbLocation.Focus()
                            tbLocation.SelectAll()
                            Exit Sub
                        End If
                        db.Locations.Add(New Locations With {.FKCompany = CompID, .FKWarehouse = WhID, .Name = tbLocation.Text, .FKZone = ZId, .FKLocationType = LtId, .Description = tbDesc.Text, .CreateDate = DateTimeServer(), .CreateBy = Username, .UpdateDate = DateTimeServer(), .UpdateBy = Username, .Enable = True, .InStorage = True, .AutoReceive = IIf(CheckBox1.Checked = True, True, False)})
                        db.SaveChanges()
                        ClearData()
                        Dim lastadd = (From c In db.Locations.Include("Warehouse").Include("Zone").Include("LocationType") Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                        stLocation.Instance.Location.Add(lastadd)
                    Else
                        If tbLocation.Text <> codeOld Then
                            If Not IsNothing((From c In db.Locations Where c.FKCompany = CompID And c.FKWarehouse = WhID And c.Name = tbLocation.Text).FirstOrDefault) Then
                                MessageBox.Show("ป้อนรหัสซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                tbLocation.Focus()
                                tbLocation.SelectAll()
                                Exit Sub
                            End If
                        End If
                        Dim ds1 = (From c In db.Locations Where c.Id = frmLocation.SId).FirstOrDefault
                        If Not IsNothing(ds1) Then
                            ds1.FKWarehouse = WhID
                            ds1.Name = tbLocation.Text
                            ds1.FKZone = ZId
                            ds1.FKLocationType = LtId
                            ds1.Description = tbDesc.Text
                            ds1.UpdateDate = DateTimeServer()
                            ds1.UpdateBy = Username
                            ds1.AutoReceive = IIf(CheckBox1.Checked = True, True, False)
                            db.SaveChanges()
                        End If
                        Dim ds = (From c In stLocation.Instance.Location Where c.Id = frmLocation.SId).FirstOrDefault
                        If Not IsNothing(ds) Then
                            stLocation.Instance.Location.Remove(ds)
                            Dim lastadd = (From c In db.Locations.Include("Warehouse").Include("Zone").Include("LocationType") Order By c.UpdateDate Descending Where c.FKCompany = CompID And c.UpdateBy = Username).FirstOrDefault
                            stLocation.Instance.Location.Add(lastadd)
                        End If
                    End If
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmLocation.SelData()
            If sEdit = True Then Me.Close()
        End If
    End Sub

    Private Sub tbZone_Leave(sender As Object, e As EventArgs) Handles tbZone.Leave
        If tbZone.Text <> "" Then
            If tbWarehouse.Text = "" Then
                MessageBox.Show("กรุณาป้อน คลัง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWarehouse.Focus()
                Exit Sub
            End If
            Dim da = (From c In stZone.Instance.Zone Order By c.Code Where c.FKWarehouse = WhID And c.Code.Contains(tbZone.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 10
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbZone.Text.Trim
                frmSearch.ShowDialog()
                ZId = s_ID
                tbZone.Text = s_Code
                tbZoneDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    ZId = a.Id
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
        bt_Num = 10
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbZone.Text
        frmSearch.ShowDialog()
        ZId = s_ID
        tbZone.Text = s_Code
        tbZoneDesc.Text = s_Desc
        tbType.Focus()
    End Sub

    Private Sub tbWh_Leave(sender As Object, e As EventArgs) Handles tbWarehouse.Leave
        If tbWarehouse.Text <> "" Then
            Dim da = (From c In stWarehouse.Instance.WH Order By c.Code Where c.Code.Contains(tbWarehouse.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 33
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbWarehouse.Text.Trim
                frmSearch.ShowDialog()
                WhID = s_ID
                tbWarehouse.Text = s_Code
                tbWarehouseDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    WhID = a.Id
                    tbWarehouse.Text = a.Code
                    tbWarehouseDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("รหัส ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbWarehouse.SelectAll()
                tbWarehouse.Focus()
            End If
        Else
            tbWarehouseDesc.Clear()
        End If
    End Sub

    Private Sub btWh_Click(sender As Object, e As EventArgs) Handles btWarehouse.Click
        bt_Num = 33
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbWarehouse.Text
        frmSearch.ShowDialog()
        WhID = s_ID
        tbWarehouse.Text = s_Code
        tbWarehouseDesc.Text = s_Desc
        tbLocation.Focus()
    End Sub

    Private Sub tbType_Leave(sender As Object, e As EventArgs) Handles tbType.Leave
        If tbType.Text <> "" Then
            Dim da = (From c In stLocType.Instance.LocType Order By c.Code Where c.Code.Contains(tbType.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 11
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbType.Text.Trim
                frmSearch.ShowDialog()
                LtId = s_ID
                tbType.Text = s_Code
                tbTypeDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    LtId = a.Id
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

    Private Sub btType_Click(sender As Object, e As EventArgs) Handles btType.Click
        bt_Num = 11
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbType.Text
        frmSearch.ShowDialog()
        LtId = s_ID
        tbType.Text = s_Code
        tbTypeDesc.Text = s_Desc
        tbDesc.Focus()
    End Sub


End Class