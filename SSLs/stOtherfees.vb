Public Class stOtherfees
    Private Shared _instance As stOtherfees
    Private dtOtherfees As List(Of Otherfees)

    Public Property Otherfees() As List(Of Otherfees)
        Get
            Return dtOtherfees
        End Get
        Set
            dtOtherfees = Value
        End Set
    End Property

    Public Shared Function Instance() As stOtherfees
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stOtherfees()
                Dim data = db.Otherfees.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Otherfees = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stOtherfees
        _instance = Nothing
        Return _instance
    End Function
End Class
