Public Class stBoxSize
    Private Shared _instance As stBoxSize
    Private dtBoxSize As List(Of BoxSize)

    Public Property BoxSize() As List(Of BoxSize)
        Get
            Return dtBoxSize
        End Get
        Set
            dtBoxSize = Value
        End Set
    End Property

    Public Shared Function Instance() As stBoxSize
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stBoxSize()
                Dim data = db.BoxSize.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.BoxSize = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stBoxSize
        _instance = Nothing
        Return _instance
    End Function
End Class
