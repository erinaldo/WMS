Public Class stMenu

    Private Shared _instance As stMenu
    Private dtMenu As List(Of Menu)

    Public Property Menu() As List(Of Menu)
        Get
            Return dtMenu
        End Get
        Set
            dtMenu = Value
        End Set
    End Property

    Public Shared Function Instance() As stMenu
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stMenu()
                Dim data = db.Menu.Where(Function(x) x.Enable = True).ToList
                _instance.Menu = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stMenu
        _instance = Nothing
        Return _instance
    End Function

End Class
