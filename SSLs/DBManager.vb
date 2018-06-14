Imports System.Data.SqlClient

Module DBManager

    'Public conn As SqlConnection = New SqlConnection("Data Source=172.16.100.149;Initial Catalog=PTGwms;Persist Security Info=True;User ID=sa;Password=P@ssw0rd")
    Public conn As SqlConnection = New SqlConnection("Data Source=PITAK-NB;Initial Catalog=PTGwms;Persist Security Info=True;User ID=sa;Password=*Adm@krs")
    Public SqlSearch, SqlCol1, SqlCol2, Username, UName, Comp, s_Code, s_Desc, batchID, LocName As String
    Public bt_Num, s_ID, CompID, DocID, RoleID As Integer
    Public sNew As Boolean = False
    Public sEdit As Boolean = False
    Dim da As New SqlDataAdapter

    Public DBLink As String = "WMS2STAG"
    Public ServName As String = "172.16.100.149"
    Public DBName As String = "PTGwms"
    Public UserDB As String = "sa"
    Public PassDB As String = "P@ssw0rd"

    Public Sub ClearTextBox(ByVal root As Control)
        For Each ctrl As Control In root.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            End If
        Next ctrl
    End Sub

    Public Function DateTimeServer() As DateTime
        Using db As New PTGwmsEntities()
            Dim Query = db.Users.[Select](Function(c) DateTime.Now).FirstOrDefault()
            Return Query
        End Using
    End Function

    Public Function selSQL(ByVal query As String) As DataSet
        Dim ds As New DataSet
        Try
            da = New SqlDataAdapter(query, conn)
            da.Fill(ds)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Sql Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return ds
    End Function

    Public Function execSQL(ByVal query As String) As Boolean
        Dim ds As New DataSet
        Try
            da = New SqlDataAdapter(query, conn)
            da.Fill(ds)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Sql Exception", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
        Return True
    End Function

    Public Function Rep(strinput As String) As String
        If strinput = "" Then
            strinput = "NULL"
        Else
            strinput = "'" & Replace(Trim(strinput), "'", "''") & "'"
        End If
        Rep = strinput
    End Function

End Module
