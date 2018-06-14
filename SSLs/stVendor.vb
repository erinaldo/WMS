Public Class stVendor

    Private Shared _instance As stVendor
    Private dtVD As List(Of Vendor)

    Public Property VD() As List(Of Vendor)
        Get
            Return dtVD
        End Get
        Set
            dtVD = Value
        End Set
    End Property

    Public Shared Function Instance() As stVendor
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stVendor()
                Dim data = db.Vendor.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.VD = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stVendor
        _instance = Nothing
        Return _instance
    End Function

End Class
