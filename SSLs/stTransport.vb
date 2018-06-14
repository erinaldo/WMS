Public Class stTransport
    Private Shared _instance As stTransport
    Private dtTransport As List(Of Transports)

    Public Property Transport() As List(Of Transports)
        Get
            Return dtTransport
        End Get
        Set
            dtTransport = Value
        End Set
    End Property

    Public Shared Function Instance() As stTransport
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stTransport()
                Dim data = db.Transports.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Transport = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stTransport
        _instance = Nothing
        Return _instance
    End Function
End Class
