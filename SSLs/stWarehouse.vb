Public Class stWarehouse

    Private Shared _instance As stWarehouse
    Private dtWh As List(Of Warehouse)

    Public Property WH() As List(Of Warehouse)
        Get
            Return dtWh
        End Get
        Set
            dtWh = Value
        End Set
    End Property

    Public Shared Function Instance() As stWarehouse
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stWarehouse()
                If RoleID = 1 Then
                    Dim data = db.Warehouse.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                    _instance.WH = data
                Else
                    Dim wh As New List(Of String)
                    Dim wha = (From c In db.WarehouseAccess Where c.Enable = True And c.FKUser = Username).ToList
                    If wha.Count > 0 Then
                        For Each a In wha
                            wh.Add(a.FKWarehouse)
                        Next
                    End If
                    Dim data = db.Warehouse.Where(Function(x) x.Enable = True And x.FKCompany = CompID And wh.Contains(x.Id.ToString)).ToList
                    _instance.WH = data
                End If
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stWarehouse
        _instance = Nothing
        Return _instance
    End Function

End Class




