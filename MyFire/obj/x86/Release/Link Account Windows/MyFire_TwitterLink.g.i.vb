﻿#ExternalChecksum("..\..\..\..\Link Account Windows\MyFire_TwitterLink.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","E9D3F4D410F9EB4B09267AEFDB7D71B9")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.239
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell


'''<summary>
'''MyFire_TwitterLink
'''</summary>
<Microsoft.VisualBasic.CompilerServices.DesignerGenerated(),  _
 System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
Partial Public Class MyFire_TwitterLink
    Inherits System.Windows.Window
    Implements System.Windows.Markup.IComponentConnector
    
    
    #ExternalSource("..\..\..\..\Link Account Windows\MyFire_TwitterLink.xaml",3)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents Window As MyFire_TwitterLink
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Link Account Windows\MyFire_TwitterLink.xaml",7)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Link Account Windows\MyFire_TwitterLink.xaml",8)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents LinkGrid As System.Windows.Controls.Grid
    
    #End ExternalSource
    
    
    #ExternalSource("..\..\..\..\Link Account Windows\MyFire_TwitterLink.xaml",9)
    <System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")>  _
    Friend WithEvents LinkingWebBrowser As System.Windows.Controls.WebBrowser
    
    #End ExternalSource
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub InitializeComponent() Implements System.Windows.Markup.IComponentConnector.InitializeComponent
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        Dim resourceLocater As System.Uri = New System.Uri("/MyFire;component/link%20account%20windows/myfire_twitterlink.xaml", System.UriKind.Relative)
        
        #ExternalSource("..\..\..\..\Link Account Windows\MyFire_TwitterLink.xaml",1)
        System.Windows.Application.LoadComponent(Me, resourceLocater)
        
        #End ExternalSource
    End Sub
    
    <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity"),  _
     System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")>  _
    Sub System_Windows_Markup_IComponentConnector_Connect(ByVal connectionId As Integer, ByVal target As Object) Implements System.Windows.Markup.IComponentConnector.Connect
        If (connectionId = 1) Then
            Me.Window = CType(target,MyFire_TwitterLink)
            Return
        End If
        If (connectionId = 2) Then
            Me.LayoutRoot = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 3) Then
            Me.LinkGrid = CType(target,System.Windows.Controls.Grid)
            Return
        End If
        If (connectionId = 4) Then
            Me.LinkingWebBrowser = CType(target,System.Windows.Controls.WebBrowser)
            Return
        End If
        Me._contentLoaded = true
    End Sub
End Class

