<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRTV
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column22 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGrid1 = New System.Windows.Forms.DataGridView()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column21 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dtIssueDate = New System.Windows.Forms.DateTimePicker()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.tbTransDesc = New System.Windows.Forms.TextBox()
        Me.tbTrans = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.dtRefDate = New System.Windows.Forms.DateTimePicker()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.tbCustDesc = New System.Windows.Forms.TextBox()
        Me.tbCust = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.tbOwnerDesc = New System.Windows.Forms.TextBox()
        Me.tbOwner = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btClear = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tbRefNo = New System.Windows.Forms.TextBox()
        Me.tbTotalQty = New System.Windows.Forms.TextBox()
        Me.btSave = New System.Windows.Forms.Button()
        Me.tbProductDesc = New System.Windows.Forms.TextBox()
        Me.tbDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btTrans = New System.Windows.Forms.Button()
        Me.btCust = New System.Windows.Forms.Button()
        Me.btOwner = New System.Windows.Forms.Button()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Column5
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle1
        Me.Column5.HeaderText = "จำนวน"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 80
        '
        'Column22
        '
        Me.Column22.HeaderText = "โซน"
        Me.Column22.Name = "Column22"
        Me.Column22.ReadOnly = True
        Me.Column22.Width = 50
        '
        'Column17
        '
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column17.DefaultCellStyle = DataGridViewCellStyle2
        Me.Column17.HeaderText = "บรรจุ"
        Me.Column17.Name = "Column17"
        Me.Column17.ReadOnly = True
        Me.Column17.Width = 60
        '
        'Column3
        '
        Me.Column3.HeaderText = "หน่วย"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 70
        '
        'Column1
        '
        Me.Column1.HeaderText = "รหัสสินค้า/Barcode"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 120
        '
        'DataGrid1
        '
        Me.DataGrid1.AllowUserToAddRows = False
        Me.DataGrid1.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.DataGrid1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column16, Me.Column1, Me.Column3, Me.Column17, Me.Column22, Me.Column5, Me.Column12, Me.Column6, Me.Column4, Me.Column9, Me.Column13, Me.Column2, Me.Column18, Me.Column21, Me.Column7})
        Me.DataGrid1.Location = New System.Drawing.Point(6, 14)
        Me.DataGrid1.Name = "DataGrid1"
        Me.DataGrid1.ReadOnly = True
        Me.DataGrid1.Size = New System.Drawing.Size(998, 313)
        Me.DataGrid1.TabIndex = 104
        '
        'Column16
        '
        Me.Column16.HeaderText = "คลัง"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        Me.Column16.Width = 60
        '
        'Column12
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.Column12.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column12.HeaderText = "สถานะ"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 50
        '
        'Column6
        '
        Me.Column6.HeaderText = "I/O"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column4.HeaderText = "Remark"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "ProdId"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        Me.Column9.Visible = False
        '
        'Column13
        '
        Me.Column13.HeaderText = "StatId"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Visible = False
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column2.HeaderText = "ProductName"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Visible = False
        Me.Column2.Width = 282
        '
        'Column18
        '
        Me.Column18.HeaderText = "WhID"
        Me.Column18.Name = "Column18"
        Me.Column18.ReadOnly = True
        Me.Column18.Visible = False
        '
        'Column21
        '
        Me.Column21.HeaderText = "Zone"
        Me.Column21.Name = "Column21"
        Me.Column21.ReadOnly = True
        Me.Column21.Visible = False
        '
        'Column7
        '
        Me.Column7.HeaderText = "IOID"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Visible = False
        '
        'dtIssueDate
        '
        Me.dtIssueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtIssueDate.Location = New System.Drawing.Point(540, 7)
        Me.dtIssueDate.Name = "dtIssueDate"
        Me.dtIssueDate.Size = New System.Drawing.Size(110, 20)
        Me.dtIssueDate.TabIndex = 467
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(482, 10)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(53, 13)
        Me.Label26.TabIndex = 496
        Me.Label26.Text = "วันที่เบิก :"
        '
        'tbTransDesc
        '
        Me.tbTransDesc.Location = New System.Drawing.Point(165, 59)
        Me.tbTransDesc.Name = "tbTransDesc"
        Me.tbTransDesc.ReadOnly = True
        Me.tbTransDesc.Size = New System.Drawing.Size(299, 20)
        Me.tbTransDesc.TabIndex = 494
        '
        'tbTrans
        '
        Me.tbTrans.Location = New System.Drawing.Point(82, 59)
        Me.tbTrans.Name = "tbTrans"
        Me.tbTrans.Size = New System.Drawing.Size(60, 20)
        Me.tbTrans.TabIndex = 464
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label25.Location = New System.Drawing.Point(40, 62)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(38, 13)
        Me.Label25.TabIndex = 492
        Me.Label25.Text = "ขนส่ง :"
        '
        'dtRefDate
        '
        Me.dtRefDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRefDate.Location = New System.Drawing.Point(353, 7)
        Me.dtRefDate.Name = "dtRefDate"
        Me.dtRefDate.Size = New System.Drawing.Size(111, 20)
        Me.dtRefDate.TabIndex = 461
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(295, 10)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(52, 13)
        Me.Label24.TabIndex = 491
        Me.Label24.Text = "วันที่ PO :"
        '
        'tbCustDesc
        '
        Me.tbCustDesc.Location = New System.Drawing.Point(647, 33)
        Me.tbCustDesc.Name = "tbCustDesc"
        Me.tbCustDesc.ReadOnly = True
        Me.tbCustDesc.Size = New System.Drawing.Size(313, 20)
        Me.tbCustDesc.TabIndex = 490
        '
        'tbCust
        '
        Me.tbCust.Location = New System.Drawing.Point(540, 33)
        Me.tbCust.Name = "tbCust"
        Me.tbCust.Size = New System.Drawing.Size(84, 20)
        Me.tbCust.TabIndex = 463
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label17.Location = New System.Drawing.Point(488, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(47, 13)
        Me.Label17.TabIndex = 488
        Me.Label17.Text = "Vendor :"
        '
        'tbOwnerDesc
        '
        Me.tbOwnerDesc.Location = New System.Drawing.Point(165, 33)
        Me.tbOwnerDesc.Name = "tbOwnerDesc"
        Me.tbOwnerDesc.ReadOnly = True
        Me.tbOwnerDesc.Size = New System.Drawing.Size(299, 20)
        Me.tbOwnerDesc.TabIndex = 487
        '
        'tbOwner
        '
        Me.tbOwner.Location = New System.Drawing.Point(82, 33)
        Me.tbOwner.Name = "tbOwner"
        Me.tbOwner.Size = New System.Drawing.Size(60, 20)
        Me.tbOwner.TabIndex = 462
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label15.Location = New System.Drawing.Point(34, 37)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(44, 13)
        Me.Label15.TabIndex = 485
        Me.Label15.Text = "Owner :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(23, 10)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(55, 13)
        Me.Label14.TabIndex = 484
        Me.Label14.Text = "เลขที่ PO :"
        '
        'btClear
        '
        Me.btClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btClear.Location = New System.Drawing.Point(99, 443)
        Me.btClear.Name = "btClear"
        Me.btClear.Size = New System.Drawing.Size(73, 23)
        Me.btClear.TabIndex = 483
        Me.btClear.Text = "Clear"
        Me.btClear.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(829, 449)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 13)
        Me.Label10.TabIndex = 482
        Me.Label10.Text = "จำนวนรวม :"
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label9.Location = New System.Drawing.Point(187, 449)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 13)
        Me.Label9.TabIndex = 474
        Me.Label9.Text = "ชื่อสินค้า"
        '
        'tbRefNo
        '
        Me.tbRefNo.Location = New System.Drawing.Point(82, 7)
        Me.tbRefNo.MaxLength = 60
        Me.tbRefNo.Name = "tbRefNo"
        Me.tbRefNo.Size = New System.Drawing.Size(178, 20)
        Me.tbRefNo.TabIndex = 460
        '
        'tbTotalQty
        '
        Me.tbTotalQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTotalQty.Location = New System.Drawing.Point(898, 446)
        Me.tbTotalQty.Name = "tbTotalQty"
        Me.tbTotalQty.ReadOnly = True
        Me.tbTotalQty.Size = New System.Drawing.Size(111, 20)
        Me.tbTotalQty.TabIndex = 481
        Me.tbTotalQty.TabStop = False
        Me.tbTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(11, 443)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(73, 23)
        Me.btSave.TabIndex = 480
        Me.btSave.Text = "Save"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'tbProductDesc
        '
        Me.tbProductDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbProductDesc.Location = New System.Drawing.Point(239, 446)
        Me.tbProductDesc.Name = "tbProductDesc"
        Me.tbProductDesc.ReadOnly = True
        Me.tbProductDesc.Size = New System.Drawing.Size(343, 20)
        Me.tbProductDesc.TabIndex = 473
        Me.tbProductDesc.TabStop = False
        '
        'tbDesc
        '
        Me.tbDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDesc.Location = New System.Drawing.Point(82, 85)
        Me.tbDesc.MaxLength = 200
        Me.tbDesc.Name = "tbDesc"
        Me.tbDesc.Size = New System.Drawing.Size(932, 20)
        Me.tbDesc.TabIndex = 470
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 478
        Me.Label4.Text = "หมายเหตุ :"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.DataGrid1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 104)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1009, 335)
        Me.GroupBox1.TabIndex = 479
        Me.GroupBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.Button1.Location = New System.Drawing.Point(260, 7)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(23, 20)
        Me.Button1.TabIndex = 497
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btTrans
        '
        Me.btTrans.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btTrans.Location = New System.Drawing.Point(142, 59)
        Me.btTrans.Name = "btTrans"
        Me.btTrans.Size = New System.Drawing.Size(23, 20)
        Me.btTrans.TabIndex = 493
        Me.btTrans.UseVisualStyleBackColor = True
        '
        'btCust
        '
        Me.btCust.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btCust.Location = New System.Drawing.Point(624, 33)
        Me.btCust.Name = "btCust"
        Me.btCust.Size = New System.Drawing.Size(23, 20)
        Me.btCust.TabIndex = 489
        Me.btCust.UseVisualStyleBackColor = True
        '
        'btOwner
        '
        Me.btOwner.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btOwner.Location = New System.Drawing.Point(142, 33)
        Me.btOwner.Name = "btOwner"
        Me.btOwner.Size = New System.Drawing.Size(23, 20)
        Me.btOwner.TabIndex = 486
        Me.btOwner.UseVisualStyleBackColor = True
        '
        'frmRTV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 473)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dtIssueDate)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.tbTransDesc)
        Me.Controls.Add(Me.btTrans)
        Me.Controls.Add(Me.tbTrans)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.dtRefDate)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.btCust)
        Me.Controls.Add(Me.tbCustDesc)
        Me.Controls.Add(Me.tbCust)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.tbOwnerDesc)
        Me.Controls.Add(Me.btOwner)
        Me.Controls.Add(Me.tbOwner)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.btClear)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.tbRefNo)
        Me.Controls.Add(Me.tbTotalQty)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.tbProductDesc)
        Me.Controls.Add(Me.tbDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmRTV"
        Me.Text = "Return To vendor"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column22 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGrid1 As DataGridView
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column18 As DataGridViewTextBoxColumn
    Friend WithEvents Column21 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents dtIssueDate As DateTimePicker
    Friend WithEvents Label26 As Label
    Friend WithEvents tbTransDesc As TextBox
    Friend WithEvents btTrans As Button
    Friend WithEvents tbTrans As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents dtRefDate As DateTimePicker
    Friend WithEvents Label24 As Label
    Friend WithEvents btCust As Button
    Friend WithEvents tbCustDesc As TextBox
    Friend WithEvents tbCust As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents tbOwnerDesc As TextBox
    Friend WithEvents btOwner As Button
    Friend WithEvents tbOwner As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents btClear As Button
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents tbRefNo As TextBox
    Friend WithEvents tbTotalQty As TextBox
    Friend WithEvents btSave As Button
    Friend WithEvents tbProductDesc As TextBox
    Friend WithEvents tbDesc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Button1 As Button
End Class
