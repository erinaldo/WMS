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

Partial Public Class RcvHeader
    Public Property Id As Integer
    Public Property ReceiveDate As Nullable(Of Date)
    Public Property DocumentNo As String
    Public Property DocumentDate As Date
    Public Property FKCompany As Integer
    Public Property FKRcvType As Integer
    Public Property FKVendor As Nullable(Of Integer)
    Public Property FKOwner As Integer
    Public Property Description As String
    Public Property PONumber As String
    Public Property RefNo As String
    Public Property RefDate As Nullable(Of Date)
    Public Property TotalQty As Nullable(Of Decimal)
    Public Property TotalAmt As Nullable(Of Decimal)
    Public Property ConfirmDate As Nullable(Of Date)
    Public Property ConfirmBy As String
    Public Property SrcName As String
    Public Property TargetName As String
    Public Property TargetType As String
    Public Property CreateDate As Date
    Public Property CreateBy As String
    Public Property UpdateDate As Date
    Public Property UpdateBy As String
    Public Property Enable As Boolean

    Public Overridable Property Company As Company
    Public Overridable Property Owners As Owners
    Public Overridable Property RcvDetails As ICollection(Of RcvDetails) = New HashSet(Of RcvDetails)
    Public Overridable Property RcvType As RcvType
    Public Overridable Property Vendor As Vendor
    Public Overridable Property RcvLocation As ICollection(Of RcvLocation) = New HashSet(Of RcvLocation)

End Class
