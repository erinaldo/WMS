<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintBarLoc
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
        Me.btSave = New System.Windows.Forms.Button()
        Me.tbQty = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btSave
        '
        Me.btSave.Location = New System.Drawing.Point(166, 90)
        Me.btSave.Name = "btSave"
        Me.btSave.Size = New System.Drawing.Size(75, 23)
        Me.btSave.TabIndex = 612
        Me.btSave.Text = "Print.."
        Me.btSave.UseVisualStyleBackColor = True
        '
        'tbQty
        '
        Me.tbQty.Location = New System.Drawing.Point(120, 45)
        Me.tbQty.Name = "tbQty"
        Me.tbQty.Size = New System.Drawing.Size(121, 20)
        Me.tbQty.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(57, 48)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 13)
        Me.Label12.TabIndex = 609
        Me.Label12.Text = "Location  :"
        '
        'frmPrintBarLoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 141)
        Me.Controls.Add(Me.btSave)
        Me.Controls.Add(Me.tbQty)
        Me.Controls.Add(Me.Label12)
        Me.Name = "frmPrintBarLoc"
        Me.Text = "พิมพ์ Label (Internal Barcode)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btSave As Button
    Friend WithEvents tbQty As TextBox
    Friend WithEvents Label12 As Label
End Class
