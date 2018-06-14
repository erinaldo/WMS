Imports CrystalDecisions.Shared

Public Class frmPrintRptRcv
    Private Sub CrystalReportViewer1_Load(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Load
        Cursor = Cursors.WaitCursor
        Dim pFields As New ParameterFields()
        Dim pJobNo As New ParameterField()
        Dim dJobNo As New ParameterDiscreteValue()

        Me.Text = "พิมพ์ใบรับสินค้า"
        pJobNo.ParameterFieldName = "DocID"
        dJobNo.Value = DocID
        pJobNo.CurrentValues.Add(dJobNo)
        pFields.Add(pJobNo)

        CrystalReportViewer1.ParameterFieldInfo = pFields

        Dim conInfo As New ConnectionInfo
        With conInfo
            .ServerName = ServName
            .DatabaseName = DBName
            .UserID = UserDB
            .Password = PassDB
        End With
        For Each cnInfo As TableLogOnInfo In Me.CrystalReportViewer1.LogOnInfo
            cnInfo.ConnectionInfo = conInfo
        Next
        Cursor = Cursors.Default
    End Sub

End Class