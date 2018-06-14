Public Class stCustomer

    Private Shared _instance As stCustomer
    Private dtCust As List(Of Customers)

    Public Property Cust() As List(Of Customers)
        Get
            Return dtCust
        End Get
        Set
            dtCust = Value
        End Set
    End Property

    Public Shared Function Instance() As stCustomer
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stCustomer()
                Dim data = db.Customers.Include("Owners").Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Cust = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stCustomer
        _instance = Nothing
        Return _instance
    End Function

End Class
