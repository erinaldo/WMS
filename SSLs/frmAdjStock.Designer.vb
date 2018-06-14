<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdjStock
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
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btConfirm = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.tbSearch = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.tbTotalQty = New System.Windows.Forms.TextBox()
        Me.btSave = New System.Windows.Forms.Button()
        Me.tbDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtDocdate = New System.Windows.Forms.DateTimePicker()
        Me.tbDocNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btLoadStock = New System.Windows.Forms.Button()
        Me.tbWHDesc = New System.Windows.Forms.TextBox()
        Me.tbWH = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.tbOwnerDesc = New System.Windows.Forms.TextBox()
        Me.tbOwner = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btClear = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtProdExp = New System.Windows.Forms.DateTimePicker()
        Me.dtProductDate = New System.Windows.Forms.DateTimePicker()
        Me.tbLotNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.tbQty = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btStatus = New System.Windows.Forms.Button()
        Me.tbStatus = New System.Windows.Forms.TextBox()
        Me.DataGrid1 = New System.Windows.Forms.DataGridView()
        Me.btWH = New System.Windows.Forms.Button()
        Me.btOwner = New System.Windows.Forms.Button()
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
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btConfirm
        '
        Me.btConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btConfirm.Location = New System.Drawing.Point(957, 181)
        Me.btConfirm.Name = "btConfirm"
        Me.btConfirm.Size = New System.Drawing.Size(73, 23)
        Me.btConfirm.TabIndex = 606
        Me.btConfirm.Text = "Confirm"
        Me.btConfirm.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(6, 13)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(1024, 163)
        Me.DataGridView2.TabIndex = 538
        '
        'tbSearch
        '
        Me.tbSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearch.Location = New System.Drawing.Point(823, 86)
        Me.tbSearch.Name = "tbSearch"
        Me.tbSearch.Size = New System.Drawing.Size(217, 20)
        Me.tbSearch.TabIndex = 568
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(779, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 569
        Me.Label5.Text = "ค้นหา :"
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(823, 449)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(65, 13)
        Me.Label10.TabIndex = 560
        Me.Label10.Text = "จำนวนรวม :"
        '
        'tbTotalQty
        '
        Me.tbTotalQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbTotalQty.Location = New System.Drawing.Point(894, 446)
        Me.tbTotalQty.Name = "tbTotalQty"
        Me.tbTotalQty.ReadOnly = True
        Me.tbTotalQty.Size = New System.Drawing.Size(141, 20)
        Me.tbTotalQty.TabIndex = 559
        Me.tbTotalQty.TabStop = False
        Me.tbTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(11, 443)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(73, 23)
        Me.btSave.TabIndex = 558
        Me.btSave.Text = "Save"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'tbDesc
        '
        Me.tbDesc.Location = New System.Drawing.Point(81, 86)
        Me.tbDesc.MaxLength = 200
        Me.tbDesc.Name = "tbDesc"
        Me.tbDesc.Size = New System.Drawing.Size(450, 20)
        Me.tbDesc.TabIndex = 552
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 556
        Me.Label4.Text = "หมายเหตุ :"
        '
        'dtDocdate
        '
        Me.dtDocdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDocdate.Location = New System.Drawing.Point(363, 7)
        Me.dtDocdate.Name = "dtDocdate"
        Me.dtDocdate.Size = New System.Drawing.Size(110, 20)
        Me.dtDocdate.TabIndex = 549
        '
        'tbDocNo
        '
        Me.tbDocNo.Location = New System.Drawing.Point(81, 7)
        Me.tbDocNo.MaxLength = 60
        Me.tbDocNo.Name = "tbDocNo"
        Me.tbDocNo.Size = New System.Drawing.Size(181, 20)
        Me.tbDocNo.TabIndex = 548
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(288, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 555
        Me.Label2.Text = "Adjust Date :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 554
        Me.Label1.Text = "Adjust No. :"
        '
        'btLoadStock
        '
        Me.btLoadStock.Location = New System.Drawing.Point(540, 85)
        Me.btLoadStock.Name = "btLoadStock"
        Me.btLoadStock.Size = New System.Drawing.Size(79, 23)
        Me.btLoadStock.TabIndex = 553
        Me.btLoadStock.Text = "โหลดสต็อก"
        Me.btLoadStock.UseVisualStyleBackColor = True
        '
        'tbWHDesc
        '
        Me.tbWHDesc.Location = New System.Drawing.Point(164, 60)
        Me.tbWHDesc.Name = "tbWHDesc"
        Me.tbWHDesc.ReadOnly = True
        Me.tbWHDesc.Size = New System.Drawing.Size(367, 20)
        Me.tbWHDesc.TabIndex = 567
        '
        'tbWH
        '
        Me.tbWH.Location = New System.Drawing.Point(81, 60)
        Me.tbWH.Name = "tbWH"
        Me.tbWH.Size = New System.Drawing.Size(60, 20)
        Me.tbWH.TabIndex = 551
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label25.Location = New System.Drawing.Point(47, 64)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(31, 13)
        Me.Label25.TabIndex = 565
        Me.Label25.Text = "คลัง :"
        '
        'tbOwnerDesc
        '
        Me.tbOwnerDesc.Location = New System.Drawing.Point(164, 34)
        Me.tbOwnerDesc.Name = "tbOwnerDesc"
        Me.tbOwnerDesc.ReadOnly = True
        Me.tbOwnerDesc.Size = New System.Drawing.Size(367, 20)
        Me.tbOwnerDesc.TabIndex = 564
        '
        'tbOwner
        '
        Me.tbOwner.Location = New System.Drawing.Point(81, 34)
        Me.tbOwner.Name = "tbOwner"
        Me.tbOwner.Size = New System.Drawing.Size(60, 20)
        Me.tbOwner.TabIndex = 550
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label15.Location = New System.Drawing.Point(34, 38)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(44, 13)
        Me.Label15.TabIndex = 562
        Me.Label15.Text = "Owner :"
        '
        'btClear
        '
        Me.btClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btClear.Location = New System.Drawing.Point(99, 443)
        Me.btClear.Name = "btClear"
        Me.btClear.Size = New System.Drawing.Size(73, 23)
        Me.btClear.TabIndex = 561
        Me.btClear.Text = "Clear"
        Me.btClear.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.dtProdExp)
        Me.GroupBox1.Controls.Add(Me.dtProductDate)
        Me.GroupBox1.Controls.Add(Me.tbLotNo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.tbQty)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.btStatus)
        Me.GroupBox1.Controls.Add(Me.tbStatus)
        Me.GroupBox1.Controls.Add(Me.DataGrid1)
        Me.GroupBox1.Controls.Add(Me.btConfirm)
        Me.GroupBox1.Controls.Add(Me.DataGridView2)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 105)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1035, 334)
        Me.GroupBox1.TabIndex = 557
        Me.GroupBox1.TabStop = False
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(784, 183)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(160, 21)
        Me.ComboBox1.TabIndex = 605
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(735, 186)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(45, 13)
        Me.Label9.TabIndex = 606
        Me.Label9.Text = "เหตุผล :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(559, 186)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(66, 13)
        Me.Label8.TabIndex = 575
        Me.Label8.Text = "วันหมดอายุ :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(392, 186)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 574
        Me.Label6.Text = "วันที่ผลิต :"
        '
        'dtProdExp
        '
        Me.dtProdExp.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtProdExp.Location = New System.Drawing.Point(630, 183)
        Me.dtProdExp.Name = "dtProdExp"
        Me.dtProdExp.Size = New System.Drawing.Size(102, 20)
        Me.dtProdExp.TabIndex = 604
        '
        'dtProductDate
        '
        Me.dtProductDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtProductDate.Location = New System.Drawing.Point(451, 182)
        Me.dtProductDate.Name = "dtProductDate"
        Me.dtProductDate.Size = New System.Drawing.Size(102, 20)
        Me.dtProductDate.TabIndex = 603
        '
        'tbLotNo
        '
        Me.tbLotNo.Location = New System.Drawing.Point(293, 182)
        Me.tbLotNo.MaxLength = 60
        Me.tbLotNo.Name = "tbLotNo"
        Me.tbLotNo.Size = New System.Drawing.Size(95, 20)
        Me.tbLotNo.TabIndex = 602
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(245, 186)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 571
        Me.Label3.Text = "LotNo :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label7.Location = New System.Drawing.Point(121, 186)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 550
        Me.Label7.Text = "จำนวน :"
        '
        'tbQty
        '
        Me.tbQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbQty.Location = New System.Drawing.Point(170, 183)
        Me.tbQty.Name = "tbQty"
        Me.tbQty.Size = New System.Drawing.Size(68, 20)
        Me.tbQty.TabIndex = 601
        Me.tbQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label16.Location = New System.Drawing.Point(7, 185)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(44, 13)
        Me.Label16.TabIndex = 548
        Me.Label16.Text = "สถานะ :"
        '
        'btStatus
        '
        Me.btStatus.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btStatus.Location = New System.Drawing.Point(93, 182)
        Me.btStatus.Name = "btStatus"
        Me.btStatus.Size = New System.Drawing.Size(23, 20)
        Me.btStatus.TabIndex = 547
        Me.btStatus.UseVisualStyleBackColor = True
        '
        'tbStatus
        '
        Me.tbStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.tbStatus.Location = New System.Drawing.Point(53, 182)
        Me.tbStatus.MaxLength = 4
        Me.tbStatus.Name = "tbStatus"
        Me.tbStatus.Size = New System.Drawing.Size(40, 20)
        Me.tbStatus.TabIndex = 600
        '
        'DataGrid1
        '
        Me.DataGrid1.AllowUserToAddRows = False
        Me.DataGrid1.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.InactiveBorder
        Me.DataGrid1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGrid1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGrid1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrid1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column12, Me.Column4, Me.Column5, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11, Me.Column3, Me.Column6, Me.Column13, Me.Column14, Me.Column15, Me.Column16, Me.Column17})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGrid1.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGrid1.Location = New System.Drawing.Point(7, 209)
        Me.DataGrid1.Name = "DataGrid1"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGrid1.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGrid1.Size = New System.Drawing.Size(1023, 124)
        Me.DataGrid1.TabIndex = 545
        '
        'btWH
        '
        Me.btWH.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btWH.Location = New System.Drawing.Point(141, 60)
        Me.btWH.Name = "btWH"
        Me.btWH.Size = New System.Drawing.Size(23, 20)
        Me.btWH.TabIndex = 566
        Me.btWH.UseVisualStyleBackColor = True
        '
        'btOwner
        '
        Me.btOwner.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btOwner.Location = New System.Drawing.Point(141, 34)
        Me.btOwner.Name = "btOwner"
        Me.btOwner.Size = New System.Drawing.Size(23, 20)
        Me.btOwner.TabIndex = 563
        Me.btOwner.UseVisualStyleBackColor = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Location"
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
        Me.Column4.Width = 80
        '
        'Column5
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Column5.DefaultCellStyle = DataGridViewCellStyle4
        Me.Column5.HeaderText = "จำนวนชิ้น"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "LotNo"
        Me.Column7.Name = "Column7"
        '
        'Column8
        '
        Me.Column8.HeaderText = "วันที่ผลิต"
        Me.Column8.Name = "Column8"
        '
        'Column9
        '
        Me.Column9.HeaderText = "วันหมดอายุ"
        Me.Column9.Name = "Column9"
        '
        'Column10
        '
        Me.Column10.HeaderText = "วันที่รับเข้า"
        Me.Column10.Name = "Column10"
        '
        'Column11
        '
        Me.Column11.HeaderText = "PalletCode"
        Me.Column11.Name = "Column11"
        '
        'Column3
        '
        Me.Column3.HeaderText = "เหตุผล"
        Me.Column3.Name = "Column3"
        Me.Column3.Width = 160
        '
        'Column6
        '
        Me.Column6.HeaderText = "FKLocation"
        Me.Column6.Name = "Column6"
        Me.Column6.Visible = False
        '
        'Column13
        '
        Me.Column13.HeaderText = "FKProduct"
        Me.Column13.Name = "Column13"
        Me.Column13.Visible = False
        '
        'Column14
        '
        Me.Column14.HeaderText = "FKItemStat"
        Me.Column14.Name = "Column14"
        Me.Column14.Visible = False
        '
        'Column15
        '
        Me.Column15.HeaderText = "FKProdUnit"
        Me.Column15.Name = "Column15"
        Me.Column15.Visible = False
        '
        'Column16
        '
        Me.Column16.HeaderText = "RID"
        Me.Column16.Name = "Column16"
        Me.Column16.Visible = False
        '
        'Column17
        '
        Me.Column17.HeaderText = "FKVendor"
        Me.Column17.Name = "Column17"
        Me.Column17.Visible = False
        '
        'frmAdjStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1045, 472)
        Me.Controls.Add(Me.tbSearch)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.tbTotalQty)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.tbDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtDocdate)
        Me.Controls.Add(Me.tbDocNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btLoadStock)
        Me.Controls.Add(Me.tbWHDesc)
        Me.Controls.Add(Me.btWH)
        Me.Controls.Add(Me.tbWH)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.tbOwnerDesc)
        Me.Controls.Add(Me.btOwner)
        Me.Controls.Add(Me.tbOwner)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btClear)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmAdjStock"
        Me.Text = "Adjust Stock"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btConfirm As Button
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents tbSearch As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents tbTotalQty As TextBox
    Friend WithEvents btSave As Button
    Friend WithEvents tbDesc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dtDocdate As DateTimePicker
    Friend WithEvents tbDocNo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btLoadStock As Button
    Friend WithEvents tbWHDesc As TextBox
    Friend WithEvents btWH As Button
    Friend WithEvents tbWH As TextBox
    Friend WithEvents Label25 As Label
    Friend WithEvents tbOwnerDesc As TextBox
    Friend WithEvents btOwner As Button
    Friend WithEvents tbOwner As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents btClear As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DataGrid1 As DataGridView
    Friend WithEvents Label16 As Label
    Friend WithEvents btStatus As Button
    Friend WithEvents tbStatus As TextBox
    Friend WithEvents tbLotNo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents tbQty As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents dtProdExp As DateTimePicker
    Friend WithEvents dtProductDate As DateTimePicker
    Friend WithEvents Label9 As Label
    Friend WithEvents ComboBox1 As ComboBox
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
