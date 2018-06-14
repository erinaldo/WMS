<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterfaceRcv
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
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.btConfirm = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btRefresh = New System.Windows.Forms.Button()
        Me.btLoadData = New System.Windows.Forms.Button()
        Me.dtDocdate = New System.Windows.Forms.DateTimePicker()
        Me.tbDocNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btLoadDatLube = New System.Windows.Forms.Button()
        Me.btLoadReturnOrder = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbQty = New System.Windows.Forms.TextBox()
        Me.btEditRcv = New System.Windows.Forms.Button()
        Me.tbSearch = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btConvert = New System.Windows.Forms.Button()
        Me.btLoadRTV = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView2.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(4, 30)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(975, 167)
        Me.DataGridView2.TabIndex = 298
        '
        'btConfirm
        '
        Me.btConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btConfirm.Location = New System.Drawing.Point(898, 423)
        Me.btConfirm.Name = "btConfirm"
        Me.btConfirm.Size = New System.Drawing.Size(81, 23)
        Me.btConfirm.TabIndex = 3
        Me.btConfirm.Text = "Confirm"
        Me.btConfirm.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(4, 203)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(975, 162)
        Me.DataGridView1.TabIndex = 296
        '
        'btRefresh
        '
        Me.btRefresh.Location = New System.Drawing.Point(4, 3)
        Me.btRefresh.Name = "btRefresh"
        Me.btRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btRefresh.TabIndex = 299
        Me.btRefresh.Text = "Refresh"
        Me.btRefresh.UseVisualStyleBackColor = True
        '
        'btLoadData
        '
        Me.btLoadData.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btLoadData.Enabled = False
        Me.btLoadData.Location = New System.Drawing.Point(4, 424)
        Me.btLoadData.Name = "btLoadData"
        Me.btLoadData.Size = New System.Drawing.Size(75, 23)
        Me.btLoadData.TabIndex = 300
        Me.btLoadData.Text = "Load Data.."
        Me.btLoadData.UseVisualStyleBackColor = True
        '
        'dtDocdate
        '
        Me.dtDocdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtDocdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDocdate.Location = New System.Drawing.Point(869, 372)
        Me.dtDocdate.Name = "dtDocdate"
        Me.dtDocdate.Size = New System.Drawing.Size(110, 20)
        Me.dtDocdate.TabIndex = 1
        '
        'tbDocNo
        '
        Me.tbDocNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbDocNo.Location = New System.Drawing.Point(525, 372)
        Me.tbDocNo.MaxLength = 15
        Me.tbDocNo.Name = "tbDocNo"
        Me.tbDocNo.Size = New System.Drawing.Size(169, 20)
        Me.tbDocNo.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(788, 375)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 304
        Me.Label2.Text = "วันที่เอกสาร :"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(449, 375)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 303
        Me.Label1.Text = "เลขที่เอกสาร :"
        '
        'tbDesc
        '
        Me.tbDesc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbDesc.Location = New System.Drawing.Point(525, 398)
        Me.tbDesc.MaxLength = 200
        Me.tbDesc.Name = "tbDesc"
        Me.tbDesc.Size = New System.Drawing.Size(454, 20)
        Me.tbDesc.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(463, 401)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 393
        Me.Label4.Text = "หมายเหตุ :"
        '
        'btLoadDatLube
        '
        Me.btLoadDatLube.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btLoadDatLube.Location = New System.Drawing.Point(85, 424)
        Me.btLoadDatLube.Name = "btLoadDatLube"
        Me.btLoadDatLube.Size = New System.Drawing.Size(118, 23)
        Me.btLoadDatLube.TabIndex = 394
        Me.btLoadDatLube.Text = "Load Data (LUBE).."
        Me.btLoadDatLube.UseVisualStyleBackColor = True
        '
        'btLoadReturnOrder
        '
        Me.btLoadReturnOrder.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btLoadReturnOrder.Location = New System.Drawing.Point(209, 424)
        Me.btLoadReturnOrder.Name = "btLoadReturnOrder"
        Me.btLoadReturnOrder.Size = New System.Drawing.Size(118, 23)
        Me.btLoadReturnOrder.TabIndex = 396
        Me.btLoadReturnOrder.Text = "Load Return Order.."
        Me.btLoadReturnOrder.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(127, 377)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 13)
        Me.Label3.TabIndex = 398
        Me.Label3.Text = "ยืนยันจำนวน :"
        '
        'tbQty
        '
        Me.tbQty.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbQty.Location = New System.Drawing.Point(203, 374)
        Me.tbQty.Name = "tbQty"
        Me.tbQty.Size = New System.Drawing.Size(111, 20)
        Me.tbQty.TabIndex = 397
        Me.tbQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btEditRcv
        '
        Me.btEditRcv.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btEditRcv.Location = New System.Drawing.Point(4, 373)
        Me.btEditRcv.Name = "btEditRcv"
        Me.btEditRcv.Size = New System.Drawing.Size(117, 23)
        Me.btEditRcv.TabIndex = 399
        Me.btEditRcv.Text = "ยืนยันแก้ไขจำนวน"
        Me.btEditRcv.UseVisualStyleBackColor = True
        '
        'tbSearch
        '
        Me.tbSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSearch.Location = New System.Drawing.Point(762, 5)
        Me.tbSearch.Name = "tbSearch"
        Me.tbSearch.Size = New System.Drawing.Size(217, 20)
        Me.tbSearch.TabIndex = 400
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(718, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 401
        Me.Label5.Text = "ค้นหา :"
        '
        'btConvert
        '
        Me.btConvert.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btConvert.Location = New System.Drawing.Point(762, 424)
        Me.btConvert.Name = "btConvert"
        Me.btConvert.Size = New System.Drawing.Size(91, 23)
        Me.btConvert.TabIndex = 402
        Me.btConvert.Text = "Check Master"
        Me.btConvert.UseVisualStyleBackColor = True
        '
        'btLoadRTV
        '
        Me.btLoadRTV.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btLoadRTV.Location = New System.Drawing.Point(333, 424)
        Me.btLoadRTV.Name = "btLoadRTV"
        Me.btLoadRTV.Size = New System.Drawing.Size(98, 23)
        Me.btLoadRTV.TabIndex = 403
        Me.btLoadRTV.Text = "Load RTV.."
        Me.btLoadRTV.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(482, 423)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 404
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'frmInterfaceRcv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 451)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btLoadRTV)
        Me.Controls.Add(Me.btConvert)
        Me.Controls.Add(Me.tbSearch)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btEditRcv)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbQty)
        Me.Controls.Add(Me.btLoadReturnOrder)
        Me.Controls.Add(Me.btLoadDatLube)
        Me.Controls.Add(Me.tbDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtDocdate)
        Me.Controls.Add(Me.tbDocNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btLoadData)
        Me.Controls.Add(Me.btRefresh)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.btConfirm)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmInterfaceRcv"
        Me.Text = "รายการใบสั่งซื้อ(PO)"
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents btConfirm As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btRefresh As Button
    Friend WithEvents btLoadData As Button
    Friend WithEvents dtDocdate As DateTimePicker
    Friend WithEvents tbDocNo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents tbDesc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btLoadDatLube As Button
    Friend WithEvents btLoadReturnOrder As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents tbQty As TextBox
    Friend WithEvents btEditRcv As Button
    Friend WithEvents tbSearch As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btConvert As Button
    Friend WithEvents btLoadRTV As Button
    Friend WithEvents Button1 As Button
End Class
