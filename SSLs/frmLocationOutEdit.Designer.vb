<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLocationOutEdit
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
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.btDelete = New System.Windows.Forms.Button()
        Me.btSave = New System.Windows.Forms.Button()
        Me.tbDesc = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbTypeDesc = New System.Windows.Forms.TextBox()
        Me.tbType = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbZoneDesc = New System.Windows.Forms.TextBox()
        Me.tbZone = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tbLocation = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbWarehouseDesc = New System.Windows.Forms.TextBox()
        Me.tbWarehouse = New System.Windows.Forms.TextBox()
        Me.btType = New System.Windows.Forms.Button()
        Me.btZone = New System.Windows.Forms.Button()
        Me.btWarehouse = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(480, 86)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(91, 17)
        Me.CheckBox1.TabIndex = 447
        Me.CheckBox1.Text = "Auto Receive"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'btDelete
        '
        Me.btDelete.Location = New System.Drawing.Point(492, 159)
        Me.btDelete.Name = "btDelete"
        Me.btDelete.Size = New System.Drawing.Size(75, 23)
        Me.btDelete.TabIndex = 446
        Me.btDelete.Text = "Delete"
        Me.btDelete.UseVisualStyleBackColor = True
        '
        'btSave
        '
        Me.btSave.Location = New System.Drawing.Point(610, 159)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(75, 23)
        Me.btSave.TabIndex = 434
        Me.btSave.Text = "Save"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'tbDesc
        '
        Me.tbDesc.Location = New System.Drawing.Point(73, 110)
        Me.tbDesc.MaxLength = 200
        Me.tbDesc.Multiline = True
        Me.tbDesc.Name = "tbDesc"
        Me.tbDesc.Size = New System.Drawing.Size(612, 40)
        Me.tbDesc.TabIndex = 433
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 445
        Me.Label4.Text = "หมายเหตุ :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 87)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 444
        Me.Label3.Text = "ประเภท :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbTypeDesc
        '
        Me.tbTypeDesc.Location = New System.Drawing.Point(165, 84)
        Me.tbTypeDesc.Name = "tbTypeDesc"
        Me.tbTypeDesc.ReadOnly = True
        Me.tbTypeDesc.Size = New System.Drawing.Size(298, 20)
        Me.tbTypeDesc.TabIndex = 443
        '
        'tbType
        '
        Me.tbType.Location = New System.Drawing.Point(73, 84)
        Me.tbType.Name = "tbType"
        Me.tbType.Size = New System.Drawing.Size(69, 20)
        Me.tbType.TabIndex = 432
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 441
        Me.Label2.Text = "โซนจัดเก็บ :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbZoneDesc
        '
        Me.tbZoneDesc.Location = New System.Drawing.Point(165, 58)
        Me.tbZoneDesc.Name = "tbZoneDesc"
        Me.tbZoneDesc.ReadOnly = True
        Me.tbZoneDesc.Size = New System.Drawing.Size(298, 20)
        Me.tbZoneDesc.TabIndex = 440
        '
        'tbZone
        '
        Me.tbZone.Location = New System.Drawing.Point(73, 58)
        Me.tbZone.Name = "tbZone"
        Me.tbZone.Size = New System.Drawing.Size(69, 20)
        Me.tbZone.TabIndex = 431
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 438
        Me.Label6.Text = "Location :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbLocation
        '
        Me.tbLocation.Location = New System.Drawing.Point(73, 32)
        Me.tbLocation.MaxLength = 20
        Me.tbLocation.Name = "tbLocation"
        Me.tbLocation.Size = New System.Drawing.Size(133, 20)
        Me.tbLocation.TabIndex = 430
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 13)
        Me.Label1.TabIndex = 437
        Me.Label1.Text = "คลัง :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tbWarehouseDesc
        '
        Me.tbWarehouseDesc.Location = New System.Drawing.Point(165, 6)
        Me.tbWarehouseDesc.Name = "tbWarehouseDesc"
        Me.tbWarehouseDesc.ReadOnly = True
        Me.tbWarehouseDesc.Size = New System.Drawing.Size(520, 20)
        Me.tbWarehouseDesc.TabIndex = 436
        '
        'tbWarehouse
        '
        Me.tbWarehouse.Location = New System.Drawing.Point(73, 6)
        Me.tbWarehouse.Name = "tbWarehouse"
        Me.tbWarehouse.Size = New System.Drawing.Size(69, 20)
        Me.tbWarehouse.TabIndex = 429
        '
        'btType
        '
        Me.btType.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btType.Location = New System.Drawing.Point(142, 84)
        Me.btType.Name = "btType"
        Me.btType.Size = New System.Drawing.Size(23, 20)
        Me.btType.TabIndex = 442
        Me.btType.UseVisualStyleBackColor = True
        '
        'btZone
        '
        Me.btZone.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btZone.Location = New System.Drawing.Point(142, 58)
        Me.btZone.Name = "btZone"
        Me.btZone.Size = New System.Drawing.Size(23, 20)
        Me.btZone.TabIndex = 439
        Me.btZone.UseVisualStyleBackColor = True
        '
        'btWarehouse
        '
        Me.btWarehouse.Image = Global.WMs.My.Resources.Resources.rsz_find
        Me.btWarehouse.Location = New System.Drawing.Point(142, 6)
        Me.btWarehouse.Name = "btWarehouse"
        Me.btWarehouse.Size = New System.Drawing.Size(23, 20)
        Me.btWarehouse.TabIndex = 435
        Me.btWarehouse.UseVisualStyleBackColor = True
        '
        'frmLocationOutEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 188)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.btDelete)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.tbDesc)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbTypeDesc)
        Me.Controls.Add(Me.btType)
        Me.Controls.Add(Me.tbType)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbZoneDesc)
        Me.Controls.Add(Me.btZone)
        Me.Controls.Add(Me.tbZone)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.tbLocation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbWarehouseDesc)
        Me.Controls.Add(Me.btWarehouse)
        Me.Controls.Add(Me.tbWarehouse)
        Me.Name = "frmLocationOutEdit"
        Me.Text = "เพิ่ม/แก้ไข Location นอกพื้นที่จัดเก็บ"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents btDelete As Button
    Friend WithEvents btSave As Button
    Friend WithEvents tbDesc As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents tbTypeDesc As TextBox
    Friend WithEvents btType As Button
    Friend WithEvents tbType As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbZoneDesc As TextBox
    Friend WithEvents btZone As Button
    Friend WithEvents tbZone As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents tbLocation As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tbWarehouseDesc As TextBox
    Friend WithEvents btWarehouse As Button
    Friend WithEvents tbWarehouse As TextBox
End Class
