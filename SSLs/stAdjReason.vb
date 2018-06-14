Public Class stAdjReason
    Private Shared _instance As stAdjReason
    Private dtAdjReason As List(Of AdjustReason)

    Public Property AdjReason() As List(Of AdjustReason)
        Get
            Return dtAdjReason
        End Get
        Set
            dtAdjReason = Value
        End Set
    End Property

    Public Shared Function Instance() As stAdjReason
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stAdjReason()
                Dim data = db.AdjustReason.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.AdjReason = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stAdjReason
        _instance = Nothing
        Return _instance
    End Function
End Class
