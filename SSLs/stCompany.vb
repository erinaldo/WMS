Public Class stCompany
    Private Shared _instance As stCompany
    Private dtComp As List(Of Company)

    Public Property Comp() As List(Of Company)
        Get
            Return dtComp
        End Get
        Set
            dtComp = Value
        End Set
    End Property

    Public Shared Function Instance() As stCompany
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stCompany()
                Dim data = db.Company.Where(Function(x) x.Enable = True).ToList
                _instance.Comp = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stCompany
        _instance = Nothing
        Return _instance
    End Function
End Class
