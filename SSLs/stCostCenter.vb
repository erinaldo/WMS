Public Class stCostCenter
    Private Shared _instance As stCostCenter
    Private dtCostCnt As List(Of CostCenter)

    Public Property CostCnt() As List(Of CostCenter)
        Get
            Return dtCostCnt
        End Get
        Set
            dtCostCnt = Value
        End Set
    End Property

    Public Shared Function Instance() As stCostCenter
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stCostCenter()
                Dim data = db.CostCenter.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.CostCnt = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stCostCenter
        _instance = Nothing
        Return _instance
    End Function
End Class
