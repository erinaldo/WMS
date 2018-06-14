Public Class stRcvType

    Private Shared _instance As stRcvType
    Private dtRcvType As List(Of RcvType)

    Public Property RcvType() As List(Of RcvType)
        Get
            Return dtRcvType
        End Get
        Set
            dtRcvType = Value
        End Set
    End Property

    Public Shared Function Instance() As stRcvType
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stRcvType()
                Dim data = db.RcvType.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.RcvType = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stRcvType
        _instance = Nothing
        Return _instance
    End Function

End Class
