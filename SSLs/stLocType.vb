Public Class stLocType

    Private Shared _instance As stLocType
    Private dtLocType As List(Of LocationType)

    Public Property LocType() As List(Of LocationType)
        Get
            Return dtLocType
        End Get
        Set
            dtLocType = Value
        End Set
    End Property

    Public Shared Function Instance() As stLocType
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stLocType()
                Dim data = db.LocationType.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.LocType = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stLocType
        _instance = Nothing
        Return _instance
    End Function

End Class
