Public Class stIOMaster
    Private Shared _instance As stIOMaster
    Private dtIO As List(Of IOMaster)

    Public Property IO() As List(Of IOMaster)
        Get
            Return dtIO
        End Get
        Set
            dtIO = Value
        End Set
    End Property

    Public Shared Function Instance() As stIOMaster
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stIOMaster()
                Dim data = db.IOMaster.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.IO = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stIOMaster
        _instance = Nothing
        Return _instance
    End Function
End Class
