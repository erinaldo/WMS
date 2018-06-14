Public Class frmEditRcvItem

    Public StatId, LocID, ZoneID, whID, SFL As Integer
    Public dtLoc As New List(Of Locations)

    Private Sub frmEditRcvItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ds = (From c In frmReceive.dtRcv Where c.Id = frmReceive.RcvId).FirstOrDefault
        If Not IsNothing(ds) Then
            tbLotNo.Text = ds.LotNo
            dtProductDate.Value = ds.ProductDate
            dtProdExp.Value = ds.ExpDate
            whID = ds.FKWarehouse
            StatId = ds.FKItemStatus
            tbStatus.Text = ds.ItemStatus.Code
            tbStatusDesc.Text = ds.ItemStatus.Name
            ZoneID = ds.ProductDetails.Products.FKZone
            SFL = ds.ProductDetails.Products.ShelfLife
            If ds.Location <> Nothing Then
                LocID = ds.FKLocation
                tbLocation.Text = ds.Location
            End If
            If ds.PalletNo = 0 Then
                tbPallet.Text = ""
            Else
                tbPallet.Text = ds.PalletNo
            End If
            tbRemark.Text = ds.Remark
            Try
                Using db = New PTGwmsEntities
                    dtLoc = db.Locations.Where(Function(x) x.Enable = True And x.FKCompany = CompID And x.FKWarehouse = whID And x.FKZone = ZoneID).ToList
                End Using
            Catch ex As Exception
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub dtProductDate_Leave(sender As Object, e As EventArgs) Handles dtProductDate.Leave
        dtProdExp.Value = DateAdd(DateInterval.Day, SFL, dtProductDate.Value)
    End Sub

    Private Sub dtProdExp_Leave(sender As Object, e As EventArgs) Handles dtProdExp.Leave
        dtProductDate.Value = DateAdd(DateInterval.Day, SFL * -1, dtProdExp.Value)
    End Sub

    Private Sub tb_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles tbRemark.KeyDown, dtProductDate.KeyDown, tbLocation.KeyDown, tbStatus.KeyDown, tbPallet.KeyDown, tbLotNo.KeyDown, dtProdExp.KeyDown
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{TAB}")
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub tbPallet_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbPallet.KeyPress
        If tbLocation.Text <> "" Then
            e.Handled = True
        Else
            Select Case Asc(e.KeyChar)
                Case 48 To 57
                    e.Handled = False
                Case 8, 13, 46
                    e.Handled = False
                Case Else
                    e.Handled = True
            End Select
        End If
    End Sub

    Private Sub btSave_Click(sender As Object, e As EventArgs) Handles btSave.Click
        If tbStatus.Text.Trim = "" Then
            MessageBox.Show("กรุณาป้อน สถานะ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbStatus.Focus()
            Exit Sub
        End If
        If MessageBox.Show("ต้องการบันทึกข้อมูล ใช่หรือไม่?", "ยืนยันการบันทึก", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.OK Then
            Try
                Using db = New PTGwmsEntities
                    Dim ds = (From c In db.RcvDetails Where c.Id = frmReceive.RcvId).FirstOrDefault
                    If Not IsNothing(ds) Then
                        ds.LotNo = tbLotNo.Text
                        ds.ProductDate = dtProductDate.Value
                        ds.ExpDate = dtProdExp.Value
                        ds.FKItemStatus = StatId
                        If tbLocation.Text = "" Then
                            ds.Location = Nothing
                            ds.FKLocation = Nothing
                        Else
                            ds.Location = tbLocation.Text
                            ds.FKLocation = LocID
                        End If
                        If tbPallet.Text = "" Then
                            ds.PalletNo = 0
                        Else
                            ds.PalletNo = tbPallet.Text
                        End If
                        ds.Remark = tbRemark.Text
                        ds.UpdateBy = Username
                        ds.UpdateDate = DateTimeServer()
                    End If
                    db.SaveChanges()
                End Using
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                frmReceive.SelData2()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub tbStatus_Leave(sender As Object, e As EventArgs) Handles tbStatus.Leave
        If tbStatus.Text <> "" Then
            Dim da = (From c In stItemStatus.Instance.ItemStatus Order By c.Code Where c.Code.Contains(tbStatus.Text.Trim) Select c.Id, c.Code, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 41
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbStatus.Text.Trim
                frmSearch.ShowDialog()
                StatId = s_ID
                tbStatus.Text = s_Code
                tbStatusDesc.Text = s_Desc
            ElseIf da.Count = 1 Then
                For Each a In da
                    StatId = a.Id
                    tbStatus.Text = a.Code
                    tbStatusDesc.Text = a.Name
                Next
            Else
                MessageBox.Show("สถานะ ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbStatus.SelectAll()
                tbStatus.Focus()
            End If
        End If
    End Sub

    Private Sub btStatus_Click(sender As Object, e As EventArgs) Handles btStatus.Click
        bt_Num = 41
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbStatus.Text
        frmSearch.ShowDialog()
        StatId = s_ID
        tbStatus.Text = s_Code
        tbStatusDesc.Text = s_Desc
        tbLocation.Focus()
    End Sub

    Private Sub tbPallet_Leave(sender As Object, e As EventArgs) Handles tbPallet.Leave
        If tbPallet.Text = "0" Then
            MessageBox.Show("กรุณาป้อนตัวเลขมากกว่า 0 เท่านั้น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            tbPallet.SelectAll()
            tbPallet.Focus()
        End If
    End Sub

    Private Sub tbLocation_Leave(sender As Object, e As EventArgs) Handles tbLocation.Leave
        If tbLocation.Text <> "" Then
            Dim da = (From c In dtLoc Order By c.Name Where c.Name.Contains(tbLocation.Text.Trim) Select c.Id, c.Name).ToList
            If da.Count > 1 Then
                bt_Num = 42
                frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
                frmSearch.tbSearch.Text = tbLocation.Text.Trim
                frmSearch.ShowDialog()
                LocId = s_ID
                tbLocation.Text = s_Code
            ElseIf da.Count = 1 Then
                For Each a In da
                    LocId = a.Id
                    tbLocation.Text = a.Name
                Next
            Else
                MessageBox.Show("Location ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                tbLocation.SelectAll()
                tbLocation.Focus()
            End If
        End If
    End Sub

    Private Sub btLocation_Click(sender As Object, e As EventArgs) Handles btLocation.Click
        bt_Num = 42
        frmSearch.tbSearch.Text = "1" : frmSearch.tbSearch.Text = ""
        frmSearch.tbSearch.Text = tbLocation.Text
        frmSearch.ShowDialog()
        LocId = s_ID
        tbLocation.Text = s_Code
        tbPallet.Focus()
    End Sub
End Class