'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class V_RcvStatus
    Public Property Id As Integer
    Public Property FKCompany As Integer
    Public Property FKOwner As Integer
    Public Property OwnCode As String
    Public Property OwnName As String
    Public Property FKVendor As Nullable(Of Integer)
    Public Property VenCode As String
    Public Property VenName As String
    Public Property ReceiveDate As Nullable(Of Date)
    Public Property DocumentNo As String
    Public Property DocumentDate As Date
    Public Property Description As String
    Public Property PONumber As String
    Public Property RefNo As String
    Public Property RefDate As Nullable(Of Date)
    Public Property FKWarehouse As Integer
    Public Property WHCode As String
    Public Property WHName As String
    Public Property ProdCode As String
    Public Property ProdName As String
    Public Property UnCode As String
    Public Property PackSize As Decimal
    Public Property FKItemStatus As Integer
    Public Property ItmCode As String
    Public Property ItmName As String
    Public Property FKLocation As Integer
    Public Property LocName As String
    Public Property PalletCode As String
    Public Property Quantity As Decimal
    Public Property BaseQty As Decimal
    Public Property PriceUnit As Decimal
    Public Property NetPrice As Decimal
    Public Property LotNo As String
    Public Property ProductDate As Date
    Public Property ExpDate As Date
    Public Property ConfirmDate As Nullable(Of Date)
    Public Property ConfirmBy As String
    Public Property CheckDate As Nullable(Of Date)
    Public Property CheckBy As String
    Public Property RDate As String

End Class