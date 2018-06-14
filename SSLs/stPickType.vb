Public Class stPickType
    Private Shared _instance As stPickType
    Private dtPickType As List(Of PickingType)

    Public Property PickType() As List(Of PickingType)
        Get
            Return dtPickType
        End Get
        Set
            dtPickType = Value
        End Set
    End Property

    Public Shared Function Instance() As stPickType
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stPickType()
                Dim data = db.PickingType.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.PickType = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stPickType
        _instance = Nothing
        Return _instance
    End Function
End Class
