Public Class stOwner

    Private Shared _instance As stOwner
    Private dtOwn As List(Of Owners)

    Public Property Own() As List(Of Owners)
        Get
            Return dtOwn
        End Get
        Set
            dtOwn = Value
        End Set
    End Property

    Public Shared Function Instance() As stOwner
        If _instance Is Nothing Then
            Using db As New PTGwmsEntities()
                _instance = New stOwner()
                If RoleID = 1 Then
                    Dim data = db.Owners.Where(Function(x) x.Enable = True And x.FKCompany = CompID).ToList
                    _instance.Own = data
                Else
                    Dim ow As New List(Of String)
                    Dim Own = (From c In db.OwnerAccess Where c.Enable = True And c.FKUser = Username).ToList
                    If Own.Count > 0 Then
                        For Each a In Own
                            ow.Add(a.FKOwner)
                        Next
                    End If
                    Dim data = db.Owners.Where(Function(x) x.Enable = True And x.FKCompany = CompID And ow.Contains(x.Id.ToString)).ToList
                    _instance.Own = data
                End If
            End Using
        End If
        Return _instance
    End Function

    Public Shared Function SetInstance() As stOwner
        _instance = Nothing
        Return _instance
    End Function

End Class
