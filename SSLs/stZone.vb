Public Class stZone
    Private Shared _instance As stZone
    Private dtZone As List(Of Zone)

    Public Property Zone() As List(Of Zone)
        Get
            Return dtZone
        End Get
        Set
            dtZone = Value
        End Set
    End Property

    Public Shared Function Instance() As stZone
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stZone()
                Dim data = db.Zone.Include("Warehouse").Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Zone = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stZone
        _instance = Nothing
        Return _instance
    End Function
End Class
