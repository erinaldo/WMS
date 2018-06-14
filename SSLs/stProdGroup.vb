Public Class stProdGroup
    Private Shared _instance As stProdGroup
    Private dtProdGrp As List(Of ProductGroups)

    Public Property ProdGrp() As List(Of ProductGroups)
        Get
            Return dtProdGrp
        End Get
        Set
            dtProdGrp = Value
        End Set
    End Property

    Public Shared Function Instance() As stProdGroup
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stProdGroup()
                Dim data = db.ProductGroups.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.ProdGrp = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stProdGroup
        _instance = Nothing
        Return _instance
    End Function
End Class
