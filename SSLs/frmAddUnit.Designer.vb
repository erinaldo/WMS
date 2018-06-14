<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAddUnit
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.tbUnitDesc = New System.Windows.Forms.TextBox()
        Me.tbUnit = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btUnit = New System.Windows.Forms.Button()
        Me.tbPackSize = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbCost = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbBarCode = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tbName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btSave = New System.Windows.Forms.Button()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbPalletTotal = New System.Windows.Forms.TextBox()
        Me.tbPalletLevel = New System.Windows.Forms.TextBox()
        Me.tbPalletRow = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tbMaxStock = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tbMinStock = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.tbGrossWeight = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbHeight = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.tbLength = New System.Windows.Forms.TextBox()
        Me.tbWidth = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'tbUnitDesc
        '
        Me.tbUnitDesc.Location = New System.Drawing.Point(189, 12)
        Me.tbUnitDesc.Name = "tbUnitDesc"
        Me.tbUnitDesc.ReadOnly = True
        Me.tbUnitDesc.Size = New System.Drawing.Size(375, 20)
        Me.tbUnitDesc.TabIndex = 508
        '
        'tbUnit
        '
        Me.tbUnit.Location = New System.Drawing.Point(85, 12)
        Me.tbUnit.Name = "tbUnit"
        Me.tbUnit.Size = New System.Drawing.Size(81, 20)
        Me.tbUnit.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label10.Location = New System.Drawing.Point(24, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 509
        Me.Label10.Text = "หน่วยนับ :"
        '
        'btUnit
        '
        Me.btUnit.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btUnit.Location = New System.Drawing.Point(166, 12)
        Me.btUnit.Name = "btUnit"
        Me.btUnit.Size = New System.Drawing.Size(23, 20)
        Me.btUnit.TabIndex = 507
        Me.btUnit.UseVisualStyleBackColor = True
        '
        'tbPackSize
        '
        Me.tbPackSize.Location = New System.Drawing.Point(85, 38)
        Me.tbPackSize.Name = "tbPackSize"
        Me.tbPackSize.Size = New System.Drawing.Size(104, 20)
        Me.tbPackSize.TabIndex = 1
        Me.tbPackSize.Text = "0.00"
        Me.tbPackSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 511
        Me.Label1.Text = "บรรจุ :"
        '
        'tbCost
        '
        Me.tbCost.Location = New System.Drawing.Point(85, 64)
        Me.tbCost.Name = "tbCost"
        Me.tbCost.Size = New System.Drawing.Size(104, 20)
        Me.tbCost.TabIndex = 2
        Me.tbCost.Text = "0.0000"
        Me.tbCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 513
        Me.Label2.Text = "ราคาทุน :"
        '
        'tbBarCode
        '
        Me.tbBarCode.Location = New System.Drawing.Point(85, 90)
        Me.tbBarCode.MaxLength = 40
        Me.tbBarCode.Name = "tbBarCode"
        Me.tbBarCode.Size = New System.Drawing.Size(266, 20)
        Me.tbBarCode.TabIndex = 3
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(27, 93)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 13)
        Me.Label17.TabIndex = 515
        Me.Label17.Text = "Barcode :"
        '
        'tbName
        '
        Me.tbName.Location = New System.Drawing.Point(85, 116)
        Me.tbName.Name = "tbName"
        Me.tbName.Size = New System.Drawing.Size(479, 20)
        Me.tbName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 13)
        Me.Label3.TabIndex = 517
        Me.Label3.Text = "รายละเอียด :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btSave
        '
        Me.btSave.Location = New System.Drawing.Point(390, 254)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(75, 23)
        Me.btSave.TabIndex = 14
        Me.btSave.Text = "OK"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'btDelete
        '
        Me.btDelete.Location = New System.Drawing.Point(489, 254)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 23)
        Me.btDelete.TabIndex = 15
        Me.btDelete.Text = "Cancel"
        Me.btDelete.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(79, 260)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox1.TabIndex = 99
        Me.CheckBox1.Text = "หน่วยหลัก"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(346, 172)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(13, 13)
        Me.Label11.TabIndex = 527
        Me.Label11.Text = "="
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(185, 172)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 526
        Me.Label8.Text = "ชั้น(พาเลต)  :"
        '
        'tbPalletTotal
        '
        Me.tbPalletTotal.Location = New System.Drawing.Point(365, 169)
        Me.tbPalletTotal.Name = "tbPalletTotal"
        Me.tbPalletTotal.ReadOnly = True
        Me.tbPalletTotal.Size = New System.Drawing.Size(91, 20)
        Me.tbPalletTotal.TabIndex = 525
        Me.tbPalletTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPalletLevel
        '
        Me.tbPalletLevel.Location = New System.Drawing.Point(254, 169)
        Me.tbPalletLevel.Name = "tbPalletLevel"
        Me.tbPalletLevel.Size = New System.Drawing.Size(89, 20)
        Me.tbPalletLevel.TabIndex = 8
        Me.tbPalletLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbPalletRow
        '
        Me.tbPalletRow.Location = New System.Drawing.Point(85, 169)
        Me.tbPalletRow.Name = "tbPalletRow"
        Me.tbPalletRow.Size = New System.Drawing.Size(89, 20)
        Me.tbPalletRow.TabIndex = 7
        Me.tbPalletRow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(11, 172)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(72, 13)
        Me.Label13.TabIndex = 524
        Me.Label13.Text = "ฐาน(พาเลต)  :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(182, 145)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 13)
        Me.Label4.TabIndex = 521
        Me.Label4.Text = "จำนวนสูงสุด :"
        '
        'tbMaxStock
        '
        Me.tbMaxStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbMaxStock.Location = New System.Drawing.Point(254, 142)
        Me.tbMaxStock.Name = "tbMaxStock"
        Me.tbMaxStock.Size = New System.Drawing.Size(89, 20)
        Me.tbMaxStock.TabIndex = 6
        Me.tbMaxStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 519
        Me.Label5.Text = "จำนวนต่ำสุด :"
        '
        'tbMinStock
        '
        Me.tbMinStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbMinStock.Location = New System.Drawing.Point(85, 142)
        Me.tbMinStock.Name = "tbMinStock"
        Me.tbMinStock.Size = New System.Drawing.Size(89, 20)
        Me.tbMinStock.TabIndex = 5
        Me.tbMinStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(180, 224)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(23, 13)
        Me.Label15.TabIndex = 539
        Me.Label15.Text = "Kg."
        '
        'tbGrossWeight
        '
        Me.tbGrossWeight.Location = New System.Drawing.Point(85, 221)
        Me.tbGrossWeight.Name = "tbGrossWeight"
        Me.tbGrossWeight.Size = New System.Drawing.Size(89, 20)
        Me.tbGrossWeight.TabIndex = 12
        Me.tbGrossWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(5, 224)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(77, 13)
        Me.Label16.TabIndex = 538
        Me.Label16.Text = "GrossWeight  :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(515, 198)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(24, 13)
        Me.Label14.TabIndex = 537
        Me.Label14.Text = "ซม."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(349, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(24, 13)
        Me.Label6.TabIndex = 536
        Me.Label6.Text = "ซม."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(180, 198)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(24, 13)
        Me.Label7.TabIndex = 535
        Me.Label7.Text = "ซม."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(387, 198)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 534
        Me.Label9.Text = "สูง  :"
        '
        'tbHeight
        '
        Me.tbHeight.Location = New System.Drawing.Point(420, 195)
        Me.tbHeight.Name = "tbHeight"
        Me.tbHeight.Size = New System.Drawing.Size(89, 20)
        Me.tbHeight.TabIndex = 11
        Me.tbHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(219, 198)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 533
        Me.Label12.Text = "ยาว  :"
        '
        'tbLength
        '
        Me.tbLength.Location = New System.Drawing.Point(254, 195)
        Me.tbLength.Name = "tbLength"
        Me.tbLength.Size = New System.Drawing.Size(89, 20)
        Me.tbLength.TabIndex = 10
        Me.tbLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tbWidth
        '
        Me.tbWidth.Location = New System.Drawing.Point(85, 195)
        Me.tbWidth.Name = "tbWidth"
        Me.tbWidth.Size = New System.Drawing.Size(89, 20)
        Me.tbWidth.TabIndex = 9
        Me.tbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(43, 198)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(39, 13)
        Me.Label18.TabIndex = 532
        Me.Label18.Text = "กว้าง  :"
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(178, 260)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(72, 17)
        Me.CheckBox2.TabIndex = 540
        Me.CheckBox2.Text = "หน่วยจ่าย"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'frmAddUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(573, 287)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.tbGrossWeight)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tbHeight)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.tbLength)
        Me.Controls.Add(Me.tbWidth)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbPalletTotal)
        Me.Controls.Add(Me.tbPalletLevel)
        Me.Controls.Add(Me.tbPalletRow)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.tbMaxStock)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tbMinStock)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.btDelete)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.tbName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbBarCode)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.tbCost)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbPackSize)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbUnitDesc)
        Me.Controls.Add(Me.tbUnit)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btUnit)
        Me.Name = "frmAddUnit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "เพิ่มหน่วยนับ"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbUnitDesc As TextBox
    Friend WithEvents tbUnit As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents btUnit As Button
    Friend WithEvents tbPackSize As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbCost As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbBarCode As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents tbName As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btSave As Button
    Friend WithEvents btDelete As Button
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents tbPalletTotal As TextBox
    Friend WithEvents tbPalletLevel As TextBox
    Friend WithEvents tbPalletRow As TextBox
    Friend WithEvents Label13 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents tbMaxStock As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents tbMinStock As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents tbGrossWeight As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents tbHeight As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents tbLength As TextBox
    Friend WithEvents tbWidth As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents CheckBox2 As CheckBox
End Class
