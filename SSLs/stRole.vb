Public Class stRole
    Private Shared _instance As stRole
    Private dtRole As List(Of Role)

    Public Property Role() As List(Of Role)
        Get
            Return dtRole
        End Get
        Set
            dtRole = Value
        End Set
    End Property

    Public Shared Function Instance() As stRole
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stRole()
                Dim data = db.Role.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Role = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stRole
        _instance = Nothing
        Return _instance
    End Function
End Class
