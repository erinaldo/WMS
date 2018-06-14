<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditRcvItem
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.tbLotNo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbRemark = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.tbStatus = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.tbPallet = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tbLocation = New System.Windows.Forms.TextBox()
        Me.btStatus = New System.Windows.Forms.Button()
        Me.btLocation = New System.Windows.Forms.Button()
        Me.btSave = New System.Windows.Forms.Button()
        Me.tbStatusDesc = New System.Windows.Forms.TextBox()
        Me.dtProdExp = New System.Windows.Forms.DateTimePicker()
        Me.dtProductDate = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label26.Location = New System.Drawing.Point(27, 9)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(45, 13)
        Me.Label26.TabIndex = 447
        Me.Label26.Text = "Lot No :"
        '
        'tbLotNo
        '
        Me.tbLotNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbLotNo.Location = New System.Drawing.Point(78, 6)
        Me.tbLotNo.MaxLength = 20
        Me.tbLotNo.Name = "tbLotNo"
        Me.tbLotNo.Size = New System.Drawing.Size(102, 20)
        Me.tbLotNo.TabIndex = 432
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label8.Location = New System.Drawing.Point(22, 165)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 445
        Me.Label8.Text = "Remark :"
        '
        'tbRemark
        '
        Me.tbRemark.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbRemark.Location = New System.Drawing.Point(78, 162)
        Me.tbRemark.MaxLength = 200
        Me.tbRemark.Name = "tbRemark"
        Me.tbRemark.Size = New System.Drawing.Size(331, 20)
        Me.tbRemark.TabIndex = 438
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label16.Location = New System.Drawing.Point(27, 87)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 13)
        Me.Label16.TabIndex = 444
        Me.Label16.Text = "สถานะ :"
        '
        'tbStatus
        '
        Me.tbStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbStatus.Location = New System.Drawing.Point(78, 84)
        Me.tbStatus.MaxLength = 4
        Me.tbStatus.Name = "tbStatus"
        Me.tbStatus.Size = New System.Drawing.Size(38, 20)
        Me.tbStatus.TabIndex = 435
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label13.Location = New System.Drawing.Point(28, 139)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(44, 13)
        Me.Label13.TabIndex = 442
        Me.Label13.Text = "พาเลต :"
        '
        'tbPallet
        '
        Me.tbPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbPallet.Location = New System.Drawing.Point(78, 136)
        Me.tbPallet.MaxLength = 4
        Me.tbPallet.Name = "tbPallet"
        Me.tbPallet.Size = New System.Drawing.Size(44, 20)
        Me.tbPallet.TabIndex = 437
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label12.Location = New System.Drawing.Point(18, 114)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 440
        Me.Label12.Text = "Location :"
        '
        'tbLocation
        '
        Me.tbLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbLocation.Location = New System.Drawing.Point(78, 110)
        Me.tbLocation.MaxLength = 10
        Me.tbLocation.Name = "tbLocation"
        Me.tbLocation.Size = New System.Drawing.Size(75, 20)
        Me.tbLocation.TabIndex = 436
        '
        'btStatus
        '
        Me.btStatus.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btStatus.Location = New System.Drawing.Point(116, 84)
        Me.btStatus.Name = "btStatus"
        Me.btStatus.Size = New System.Drawing.Size(23, 20)
        Me.btStatus.TabIndex = 450
        Me.btStatus.UseVisualStyleBackColor = True
        '
        'btLocation
        '
        Me.btLocation.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btLocation.Location = New System.Drawing.Point(153, 110)
        Me.btLocation.Name = "btLocation"
        Me.btLocation.Size = New System.Drawing.Size(23, 20)
        Me.btLocation.TabIndex = 449
        Me.btLocation.UseVisualStyleBackColor = True
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(336, 188)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(73, 23)
        Me.btSave.TabIndex = 448
        Me.btSave.Text = "Save"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'tbStatusDesc
        '
        Me.tbStatusDesc.Location = New System.Drawing.Point(139, 85)
        Me.tbStatusDesc.Name = "tbStatusDesc"
        Me.tbStatusDesc.ReadOnly = True
        Me.tbStatusDesc.Size = New System.Drawing.Size(270, 20)
        Me.tbStatusDesc.TabIndex = 451
        '
        'dtProdExp
        '
        Me.dtProdExp.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtProdExp.Location = New System.Drawing.Point(78, 58)
        Me.dtProdExp.Name = "dtProdExp"
        Me.dtProdExp.Size = New System.Drawing.Size(102, 20)
        Me.dtProdExp.TabIndex = 434
        '
        'dtProductDate
        '
        Me.dtProductDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtProductDate.Location = New System.Drawing.Point(78, 32)
        Me.dtProductDate.Name = "dtProductDate"
        Me.dtProductDate.Size = New System.Drawing.Size(102, 20)
        Me.dtProductDate.TabIndex = 433
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label11.Location = New System.Drawing.Point(18, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(54, 13)
        Me.Label11.TabIndex = 439
        Me.Label11.Text = "วันที่ผลิต :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label20.Location = New System.Drawing.Point(6, 62)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 446
        Me.Label20.Text = "วันหมดอายุ :"
        '
        'frmEditRcvItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 213)
        Me.Controls.Add(Me.tbStatusDesc)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.tbLotNo)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.dtProdExp)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbRemark)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.btStatus)
        Me.Controls.Add(Me.tbStatus)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.tbPallet)
        Me.Controls.Add(Me.btLocation)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.tbLocation)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.dtProductDate)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditRcvItem"
        Me.Text = "แก้ไขรายการสินค้า"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label26 As Label
    Friend WithEvents tbLotNo As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents tbRemark As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents btStatus As Button
    Friend WithEvents tbStatus As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents tbPallet As TextBox
    Friend WithEvents btLocation As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents tbLocation As TextBox
    Friend WithEvents btSave As Button
    Friend WithEvents tbStatusDesc As TextBox
    Friend WithEvents dtProdExp As DateTimePicker
    Friend WithEvents dtProductDate As DateTimePicker
    Friend WithEvents Label11 As Label
    Friend WithEvents Label20 As Label
End Class
