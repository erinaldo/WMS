Public Class stProductType

    Private Shared _instance As stProductType
    Private dtProdType As List(Of ProductType)

    Public Property ProdType() As List(Of ProductType)
        Get
            Return dtProdType
        End Get
        Set
            dtProdType = Value
        End Set
    End Property

    Public Shared Function Instance() As stProductType
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stProductType()
                Dim data = db.ProductType.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.ProdType = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stProductType
        _instance = Nothing
        Return _instance
    End Function

End Class
