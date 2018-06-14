Public Class stProduct
    Private Shared _instance As stProduct
    Private dtProd As List(Of Products)

    Public Property Prod() As List(Of Products)
        Get
            Return dtProd
        End Get
        Set
            dtProd = Value
        End Set
    End Property

    Public Shared Function Instance() As stProduct
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stProduct()
                Dim data = db.Products.Include("Owners").Include("ProductBrands").Include("ProductGroups").Include("ProductType").Include("Zone").Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.Prod = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stProduct
        _instance = Nothing
        Return _instance
    End Function
End Class
