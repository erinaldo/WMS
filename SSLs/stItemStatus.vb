Public Class stItemStatus

    Private Shared _instance As stItemStatus
    Private dtItemStatus As List(Of ItemStatus)

    Public Property ItemStatus() As List(Of ItemStatus)
        Get
            Return dtItemStatus
        End Get
        Set
            dtItemStatus = Value
        End Set
    End Property

    Public Shared Function Instance() As stItemStatus
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stItemStatus()
                Dim data = db.ItemStatus.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.ItemStatus = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stItemStatus
        _instance = Nothing
        Return _instance
    End Function

End Class
