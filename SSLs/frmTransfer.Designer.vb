<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransfer
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tbWHDesc = New System.Windows.Forms.TextBox()
        Me.btWH = New System.Windows.Forms.Button()
        Me.tbWH = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.tbDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtDocdate = New System.Windows.Forms.DateTimePicker()
        Me.tbDocNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbZoneDesc = New System.Windows.Forms.TextBox()
        Me.btZone = New System.Windows.Forms.Button()
        Me.tbZone = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGrid1 = New System.Windows.Forms.DataGridView()
        Me.btClear = New System.Windows.Forms.Button()
        Me.tbTotalQty = New System.Windows.Forms.TextBox()
        Me.btSave = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.btLoadStock = New System.Windows.Forms.Button()
        Me.btConfirm = New System.Windows.Forms.Button()
        Me.btLocation = New System.Windows.Forms.Button()
        Me.tbLocation = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbSearch = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tbQty = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.btStatus = New System.Windows.Forms.Button()
        Me.tbStatus = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column14 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column15 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbWHDesc
        '
        Me.tbWHDesc.Location = New System.Drawing.Point(165, 33)
        Me.tbWHDesc.Name = "tbWHDesc"
        Me.tbWHDesc.ReadOnly = True
        Me.tbWHDesc.Size = New System.Drawing.Size(288, 20)
        Me.tbWHDesc.TabIndex = 528
        '
        'btWH
        '
        Me.btWH.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btWH.Location = New System.Drawing.Point(142, 33)
        Me.btWH.Name = "btWH"
        Me.btWH.Size = New System.Drawing.Size(23, 20)
        Me.btWH.TabIndex = 599
        Me.btWH.UseVisualStyleBackColor = True
        '
        'tbWH
        '
        Me.tbWH.Location = New System.Drawing.Point(82, 33)
        Me.tbWH.Name = "tbWH"
        Me.tbWH.Size = New System.Drawing.Size(60, 20)
        Me.tbWH.TabIndex = 2
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label25.Location = New System.Drawing.Point(48, 37)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(31, 13)
        Me.Label25.TabIndex = 526
        Me.Label25.Text = "คลัง :"
        '
        'tbDesc
        '
        Me.tbDesc.Location = New System.Drawing.Point(82, 59)
        Me.tbDesc.MaxLength = 200
        Me.tbDesc.Name = "tbDesc"
        Me.tbDesc.Size = New System.Drawing.Size(500, 20)
        Me.tbDesc.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 522
        Me.Label4.Text = "หมายเหตุ :"
        '
        'dtDocdate
        '
        Me.dtDocdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDocdate.Location = New System.Drawing.Point(343, 7)
        Me.dtDocdate.Name = "dtDocdate"
        Me.dtDocdate.Size = New System.Drawing.Size(110, 20)
        Me.dtDocdate.TabIndex = 1
        '
        'tbDocNo
        '
        Me.tbDocNo.Location = New System.Drawing.Point(82, 7)
        Me.tbDocNo.MaxLength = 60
        Me.tbDocNo.Name = "tbDocNo"
        Me.tbDocNo.Size = New System.Drawing.Size(152, 20)
        Me.tbDocNo.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(287, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 521
        Me.Label2.Text = "วันที่โอน :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 520
        Me.Label1.Text = "เลขที่ใบโอน :"
        '
        'tbZoneDesc
        '
        Me.tbZoneDesc.Location = New System.Drawing.Point(582, 33)
        Me.tbZoneDesc.Name = "tbZoneDesc"
        Me.tbZoneDesc.ReadOnly = True
        Me.tbZoneDesc.Size = New System.Drawing.Size(288, 20)
        Me.tbZoneDesc.TabIndex = 532
        '
        'btZone
        '
        Me.btZone.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btZone.Location = New System.Drawing.Point(559, 33)
        Me.btZone.Name = "btZone"
        Me.btZone.Size = New System.Drawing.Size(23, 20)
        Me.btZone.TabIndex = 599
        Me.btZone.UseVisualStyleBackColor = True
        '
        'tbZone
        '
        Me.tbZone.Location = New System.Drawing.Point(499, 33)
        Me.tbZone.Name = "tbZone"
        Me.tbZone.Size = New System.Drawing.Size(60, 20)
        Me.tbZone.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(463, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 530
        Me.Label3.Text = "โซน :"
        '
        'DataGrid1
        '
        Me.DataGrid1.AllowUserToAddRows = False
        Me.DataGrid1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.DataGrid1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGrid1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column12, Me.Column4, Me.Column5, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11, Me.Column3, Me.Column6, Me.Column13, Me.Column14, Me.Column15, Me.Column16, Me.Column17})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGrid1.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGrid1.Location = New System.Drawing.Point(4, 296)
        Me.DataGrid1.Name = "DataGrid1"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGrid1.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGrid1.Size = New System.Drawing.Size(951, 122)
        Me.DataGrid1.TabIndex = 533
        '
        'btClear
        '
        Me.btClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btClear.Location = New System.Drawing.Point(380, 423)
        Me.btClear.Name = "btClear"
        Me.btClear.Size = New System.Drawing.Size(73, 23)
        Me.btClear.TabIndex = 536
        Me.btClear.Text = "Clear"
        Me.btClear.UseVisualStyleBackColor = True
        '
        'tbTotalQty
        '
        Me.tbTotalQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTotalQty.Location = New System.Drawing.Point(815, 425)
        Me.tbTotalQty.Name = "tbTotalQty"
        Me.tbTotalQty.ReadOnly = True
        Me.tbTotalQty.Size = New System.Drawing.Size(140, 20)
        Me.tbTotalQty.TabIndex = 535
        Me.tbTotalQty.TabStop = False
        Me.tbTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(292, 423)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(73, 23)
        Me.btSave.TabIndex = 534
        Me.btSave.Text = "Save"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(4, 84)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(951, 183)
        Me.DataGridView2.TabIndex = 537
        '
        'btLoadStock
        '
        Me.btLoadStock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btLoadStock.Location = New System.Drawing.Point(617, 57)
        Me.btLoadStock.Name = "btLoadStock"
        Me.btLoadStock.Size = New System.Drawing.Size(71, 23)
        Me.btLoadStock.TabIndex = 5
        Me.btLoadStock.Text = "โหลดข้อมูล"
        Me.btLoadStock.UseVisualStyleBackColor = True
        '
        'btConfirm
        '
        Me.btConfirm.Location = New System.Drawing.Point(506, 270)
        Me.btConfirm.Name = "btConfirm"
        Me.btConfirm.Size = New System.Drawing.Size(77, 23)
        Me.btConfirm.TabIndex = 12
        Me.btConfirm.Text = "Confirm"
        Me.btConfirm.UseVisualStyleBackColor = True
        '
        'btLocation
        '
        Me.btLocation.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btLocation.Location = New System.Drawing.Point(477, 271)
        Me.btLocation.Name = "btLocation"
        Me.btLocation.Size = New System.Drawing.Size(23, 20)
        Me.btLocation.TabIndex = 599
        Me.btLocation.UseVisualStyleBackColor = True
        '
        'tbLocation
        '
        Me.tbLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbLocation.Location = New System.Drawing.Point(396, 271)
        Me.tbLocation.MaxLength = 10
        Me.tbLocation.Name = "tbLocation"
        Me.tbLocation.Size = New System.Drawing.Size(80, 20)
        Me.tbLocation.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(291, 274)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 13)
        Me.Label5.TabIndex = 542
        Me.Label5.Text = "Location ปลายทาง :"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(744, 428)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 13)
        Me.Label6.TabIndex = 543
        Me.Label6.Text = "จำนวนรวม :"
        '
        'tbSearch
        '
        Me.tbSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearch.Location = New System.Drawing.Point(738, 59)
        Me.tbSearch.Name = "tbSearch"
        Me.tbSearch.Size = New System.Drawing.Size(217, 20)
        Me.tbSearch.TabIndex = 600
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(694, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(40, 13)
        Me.Label7.TabIndex = 601
        Me.Label7.Text = "ค้นหา :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label8.Location = New System.Drawing.Point(125, 274)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 602
        Me.Label8.Text = "จำนวน :"
        '
        'tbQty
        '
        Me.tbQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbQty.Location = New System.Drawing.Point(174, 271)
        Me.tbQty.Name = "tbQty"
        Me.tbQty.Size = New System.Drawing.Size(91, 20)
        Me.tbQty.TabIndex = 10
        Me.tbQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 427)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 604
        Me.Label9.Text = "ผู้โอน :"
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(49, 424)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(224, 21)
        Me.ComboBox1.TabIndex = 603
        '
        'btStatus
        '
        Me.btStatus.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btStatus.Location = New System.Drawing.Point(90, 271)
        Me.btStatus.Name = "btStatus"
        Me.btStatus.Size = New System.Drawing.Size(23, 20)
        Me.btStatus.TabIndex = 606
        Me.btStatus.UseVisualStyleBackColor = True
        '
        'tbStatus
        '
        Me.tbStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbStatus.Location = New System.Drawing.Point(50, 271)
        Me.tbStatus.MaxLength = 4
        Me.tbStatus.Name = "tbStatus"
        Me.tbStatus.Size = New System.Drawing.Size(40, 20)
        Me.tbStatus.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label10.Location = New System.Drawing.Point(4, 274)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(44, 13)
        Me.Label10.TabIndex = 607
        Me.Label10.Text = "สถานะ :"
        '
        'Column1
        '
        Me.Column1.HeaderText = "Location ต้นทาง"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 120
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column2.HeaderText = "รหัสสินค้า"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 120
        '
        'Column12
        '
        Me.Column12.HeaderText = "ชื่อสินค้า"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        Me.Column12.Width = 200
        '
        'Column4
        '
        Me.Column4.HeaderText = "สถานะ"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 80
        '
        'Column5
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle3
        Me.Column5.HeaderText = "จำนวนชิ้น"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "LotNo"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "วันที่ผลิต"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "วันหมดอายุ"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "วันที่รับเข้า"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "PalletCode"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Location ปลายทาง"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 120
        '
        'Column6
        '
        Me.Column6.HeaderText = "FKLocation"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Visible = False
        '
        'Column13
        '
        Me.Column13.HeaderText = "FKProduct"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Visible = False
        '
        'Column14
        '
        Me.Column14.HeaderText = "FKItemStat"
        Me.Column14.Name = "Column14"
        Me.Column14.ReadOnly = True
        Me.Column14.Visible = False
        '
        'Column15
        '
        Me.Column15.HeaderText = "FKProdUnit"
        Me.Column15.Name = "Column15"
        Me.Column15.ReadOnly = True
        Me.Column15.Visible = False
        '
        'Column16
        '
        Me.Column16.HeaderText = "RID"
        Me.Column16.Name = "Column16"
        Me.Column16.ReadOnly = True
        Me.Column16.Visible = False
        '
        'Column17
        '
        Me.Column17.HeaderText = "OldItemStatus"
        Me.Column17.Name = "Column17"
        Me.Column17.Visible = False
        '
        'frmTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(960, 452)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.btStatus)
        Me.Controls.Add(Me.tbStatus)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.tbQty)
        Me.Controls.Add(Me.tbSearch)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btLocation)
        Me.Controls.Add(Me.tbLocation)
        Me.Controls.Add(Me.btConfirm)
        Me.Controls.Add(Me.btLoadStock)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGrid1)
        Me.Controls.Add(Me.btClear)
        Me.Controls.Add(Me.tbTotalQty)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.tbZoneDesc)
        Me.Controls.Add(Me.btZone)
        Me.Controls.Add(Me.tbZone)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbWHDesc)
        Me.Controls.Add(Me.btWH)
        Me.Controls.Add(Me.tbWH)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.tbDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtDocdate)
        Me.Controls.Add(Me.tbDocNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmTransfer"
        Me.Text = "โอนสินค้าภายในคลัง"
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbWHDesc As TextBox
    Friend WithEvents btWH As Button
    Friend WithEvents tbWH As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents tbDesc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dtDocdate As DateTimePicker
    Friend WithEvents tbDocNo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tbZoneDesc As TextBox
    Friend WithEvents btZone As Button
    Friend WithEvents tbZone As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents DataGrid1 As DataGridView
    Friend WithEvents btClear As Button
    Friend WithEvents tbTotalQty As TextBox
    Friend WithEvents btSave As Button
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents btLoadStock As Button
    Friend WithEvents btConfirm As Button
    Friend WithEvents btLocation As Button
    Friend WithEvents tbLocation As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents tbSearch As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents tbQty As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents btStatus As Button
    Friend WithEvents tbStatus As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column7 As DataGridViewTextBoxColumn
    Friend WithEvents Column8 As DataGridViewTextBoxColumn
    Friend WithEvents Column9 As DataGridViewTextBoxColumn
    Friend WithEvents Column10 As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column6 As DataGridViewTextBoxColumn
    Friend WithEvents Column13 As DataGridViewTextBoxColumn
    Friend WithEvents Column14 As DataGridViewTextBoxColumn
    Friend WithEvents Column15 As DataGridViewTextBoxColumn
    Friend WithEvents Column16 As DataGridViewTextBoxColumn
    Friend WithEvents Column17 As DataGridViewTextBoxColumn
End Class
