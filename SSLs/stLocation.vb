Public Class stLocation

    Private Shared _instance As stLocation
    Private dtLocation As List(Of Locations)

    Public Property Location() As List(Of Locations)
        Get
            Return dtLocation
        End Get
        Set
            dtLocation = Value
        End Set
    End Property

    Public Shared Function Instance() As stLocation
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stLocation()

                Dim data = db.Locations.Include("Warehouse").Include("Zone").Include("LocationType").Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Location = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stLocation
        _instance = Nothing
        Return _instance
    End Function

End Class
