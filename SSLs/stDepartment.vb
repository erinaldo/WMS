Public Class stDepartment
    Private Shared _instance As stDepartment
    Private dtDep As List(Of Department)

    Public Property Dep() As List(Of Department)
        Get
            Return dtDep
        End Get
        Set
            dtDep = Value
        End Set
    End Property

    Public Shared Function Instance() As stDepartment
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stDepartment()
                Dim data = db.Department.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                _instance.dtDep = data
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stDepartment
        _instance = Nothing
        Return _instance
    End Function
End Class
