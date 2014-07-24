Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class CPU
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
      Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub NotifyPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Private mName As String
    Private mValue As Integer
    Private mUnit As String

    Public Property Name As String
        Get
            Return mName
        End Get
        Set(value As String)
            mName = value
        End Set
    End Property

    Public Property Value As Integer
        Get
            Return mValue
        End Get
        Set(value As Integer)
                mValue = value
                NotifyPropertyChanged("Value")

        End Set
    End Property

    Public Property Unit As String
        Get
            Return mUnit
        End Get
        Set(value As String)
            mUnit = value
            NotifyPropertyChanged("Unit")
        End Set
    End Property


End Class
