Public Class stProdBrand

    Private Shared _instance As stProdBrand
    Private dtProdBrn As List(Of ProductBrands)

    Public Property ProdBrn() As List(Of ProductBrands)
        Get
            Return dtProdBrn
        End Get
        Set
            dtProdBrn = Value
        End Set
    End Property

    Public Shared Function Instance() As stProdBrand
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stProdBrand()
                Dim data = db.ProductBrands.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.ProdBrn = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stProdBrand
        _instance = Nothing
        Return _instance
    End Function

End Class
