<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintBarProd
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
        Me.tbQty = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btSave = New System.Windows.Forms.Button()
        Me.tbQtyP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'tbQty
        '
        Me.tbQty.Location = New System.Drawing.Point(98, 25)
        Me.tbQty.Name = "tbQty"
        Me.tbQty.Size = New System.Drawing.Size(89, 20)
        Me.tbQty.TabIndex = 604
        Me.tbQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(31, 28)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 13)
        Me.Label12.TabIndex = 603
        Me.Label12.Text = "ระบุจำนวน  :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(190, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 13)
        Me.Label1.TabIndex = 605
        Me.Label1.Text = "คู่"
        '
        'btSave
        '
        Me.btSave.Location = New System.Drawing.Point(189, 82)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(75, 23)
        Me.btSave.TabIndex = 606
        Me.btSave.Text = "Print.."
        Me.btSave.UseVisualStyleBackColor = True
        '
        'tbQtyP
        '
        Me.tbQtyP.Location = New System.Drawing.Point(98, 51)
        Me.tbQtyP.Name = "tbQtyP"
        Me.tbQtyP.ReadOnly = True
        Me.tbQtyP.Size = New System.Drawing.Size(89, 20)
        Me.tbQtyP.TabIndex = 607
        Me.tbQtyP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(190, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 13)
        Me.Label2.TabIndex = 608
        Me.Label2.Text = "ดวง"
        '
        'frmPrintBarProd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(269, 109)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.tbQtyP)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbQty)
        Me.Controls.Add(Me.Label12)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintBarProd"
        Me.Text = "Print Barcode สินค้า"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbQty As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btSave As Button
    Friend WithEvents tbQtyP As TextBox
    Friend WithEvents Label2 As Label
End Class
