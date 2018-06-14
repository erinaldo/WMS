Public Class stUnit

    Private Shared _instance As stUnit
    Private dtUnit As List(Of ProductUnit)

    Public Property Unit() As List(Of ProductUnit)
        Get
            Return dtUnit
        End Get
        Set
            dtUnit = Value
        End Set
    End Property

    Public Shared Function Instance() As stUnit
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stUnit()
                Dim data = db.ProductUnit.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Unit = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stUnit
        _instance = Nothing
        Return _instance
    End Function
End Class
