
Public Class frmLogin

    Private Sub TextBox1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox2.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                Cursor = Cursors.WaitCursor
                Using db = New PTGwmsEntities
                    Dim ds = (From c In db.Users Where c.Enable = True And c.Id = TextBox1.Text And c.Password = TextBox2.Text).FirstOrDefault
                    If IsNothing(ds) Then
                        Cursor = Cursors.Default
                        MessageBox.Show("Username หรือ Password ไม่ถูกต้อง", "แจ้งเตือน !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ClearTextBox(Me)
                        TextBox1.Focus()
                    Else
                        If ds.StatusUse = True Then
                            Cursor = Cursors.Default
                            MessageBox.Show("User นี้มีผู้ใช้งานแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        Username = TextBox1.Text
                        UName = ds.Name
                        CompID = ds.FKCompany
                        Comp = ds.Company.Name
                        RoleID = ds.FKRole
                        frmMain.Show()
                        Me.Hide()
                        ds.StatusUse = True
                        db.SaveChanges()
                        Cursor = Cursors.Default
                    End If
                End Using
            Catch ex As Exception
                Cursor = Cursors.Default
                MessageBox.Show(ex.Message, "Exception Error !!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If
    End Sub

End Class
