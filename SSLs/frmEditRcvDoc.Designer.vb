<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditRcvDoc
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
        Me.dtRcvDate = New System.Windows.Forms.DateTimePicker()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.dtDocdate = New System.Windows.Forms.DateTimePicker()
        Me.tbDocNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'dtRcvDate
        '
        Me.dtRcvDate.Enabled = False
        Me.dtRcvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtRcvDate.Location = New System.Drawing.Point(100, 71)
        Me.dtRcvDate.Name = "dtRcvDate"
        Me.dtRcvDate.Size = New System.Drawing.Size(110, 20)
        Me.dtRcvDate.TabIndex = 419
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(35, 74)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(63, 13)
        Me.Label25.TabIndex = 422
        Me.Label25.Text = "วันที่รับเข้า :"
        '
        'dtDocdate
        '
        Me.dtDocdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtDocdate.Location = New System.Drawing.Point(100, 45)
        Me.dtDocdate.Name = "dtDocdate"
        Me.dtDocdate.Size = New System.Drawing.Size(110, 20)
        Me.dtDocdate.TabIndex = 418
        '
        'tbDocNo
        '
        Me.tbDocNo.Location = New System.Drawing.Point(100, 19)
        Me.tbDocNo.MaxLength = 15
        Me.tbDocNo.Name = "tbDocNo"
        Me.tbDocNo.Size = New System.Drawing.Size(181, 20)
        Me.tbDocNo.TabIndex = 417
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(26, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 421
        Me.Label2.Text = "วันที่เอกสาร :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 420
        Me.Label1.Text = "เลขที่เอกสาร :"
        '
        'btSave
        '
        Me.btSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btSave.Location = New System.Drawing.Point(240, 119)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(73, 23)
        Me.btSave.TabIndex = 423
        Me.btSave.Text = "Save"
        Me.btSave.UseVisualStyleBackColor = True
        '
        'frmEditRcvDoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(316, 145)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.dtRcvDate)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.dtDocdate)
        Me.Controls.Add(Me.tbDocNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditRcvDoc"
        Me.Text = "แก้ไขเลขที่เอกสาร"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtRcvDate As DateTimePicker
    Friend WithEvents Label25 As Label
    Friend WithEvents dtDocdate As DateTimePicker
    Friend WithEvents tbDocNo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btSave As Button
End Class
