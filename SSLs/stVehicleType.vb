Public Class stVehicleType
    Private Shared _instance As stVehicleType
    Private dtVehicleType As List(Of VehicleType)

    Public Property VehicleType() As List(Of VehicleType)
        Get
            Return dtVehicleType
        End Get
        Set
            dtVehicleType = Value
        End Set
    End Property

    Public Shared Function Instance() As stVehicleType
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stVehicleType()
                Dim data = db.VehicleType.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.VehicleType = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stVehicleType
        _instance = Nothing
        Return _instance
    End Function
End Class
