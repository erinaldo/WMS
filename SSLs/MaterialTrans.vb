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

Partial Public Class MaterialTrans
    Public Property Id As Integer
    Public Property FKOwner As Integer
    Public Property FKWarehouse As Integer
    Public Property FKProduct As Integer
    Public Property FKProductUnit As Integer
    Public Property RefDocument As String
    Public Property RefDocDate As Nullable(Of Date)
    Public Property QtyIn As Decimal
    Public Property QtyOut As Decimal
    Public Property TotalCost As Decimal
    Public Property Description As String
    Public Property CreateDate As Date
    Public Property CreateBy As String
    Public Property Enable As Boolean

    Public Overridable Property Owners As Owners
    Public Overridable Property Products As Products
    Public Overridable Property ProductUnit As ProductUnit
    Public Overridable Property Warehouse As Warehouse

End Class
