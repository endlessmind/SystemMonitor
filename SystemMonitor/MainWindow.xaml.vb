Imports System.Windows.Threading
Imports DNBSoft.WPF.RollingMonitor
Imports System.Diagnostics
Imports System.Collections.ObjectModel

Class MainWindow
    Dim pref As New PerformanceCounter
    Dim WithEvents time1 As New DispatcherTimer
    Dim cpuCount As Integer = Environment.ProcessorCount
    Dim perfCounters As List(Of PerformanceCounter) = New List(Of PerformanceCounter)
    Dim CPUList As ObservableCollection(Of CPU) = New ObservableCollection(Of CPU)
    Dim CPU As Integer
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        pref = New PerformanceCounter()
        With pref
            .CategoryName = "Processor"
            .CounterName = "% Processor Time"
            .InstanceName = "_Total"
        End With

        Console.WriteLine(cpuCount & " CPU's")

        For i As Integer = 0 To cpuCount - 1
            Dim c As PerformanceCounter = New PerformanceCounter()
            c.CategoryName = "Processor"
            c.CounterName = "% Processor Time"
            c.InstanceName = i
            perfCounters.Add(c)

            Dim cpu As CPU = New CPU()
            cpu.Name = "Core " & i
            cpu.Value = c.NextValue()
            cpu.Unit = "%"

            CPUList.Add(cpu)

            Dim cpuMhz As String = My.Computer.Registry.GetValue( _
"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\" & i, "~mhz", "n/a")

            Dim cpuNameString = My.Computer.Registry.GetValue( _
                "HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\" & i, "ProcessorNameString", _
                    "n/a")

            Dim cpuVendorID As String = My.Computer.Registry.GetValue( _
                "HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\" & i, "VendorIdentifier", _
                    "n/a")

            Dim cpuIdentifier As String = My.Computer.Registry.GetValue( _
                "HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\CentralProcessor\" & i, "Identifier", "n/a") '"ProcessorNameString", "n/a")
            Console.WriteLine(cpuMhz)
            Console.WriteLine(cpuNameString)
            lbCPU.Content = cpuNameString & "@" & cpuMhz & "Mhz"
            Console.WriteLine(cpuVendorID)
            Console.WriteLine(cpuIdentifier)
            Console.WriteLine("      ")
            Console.WriteLine("      ")
        Next
        lbCores.ItemsSource = CPUList





        Console.WriteLine(pref.NextValue())
        time1.Interval = TimeSpan.FromMilliseconds(1000)
        time1.Start()
        HalfCircleControl2.setProgress(slider1.Value)
        HalfCircleControl1.setProgress(slider1.Value)
        For i As Integer = 0 To cpuCount - 1
            CPUList(i).Value = 0
        Next

    End Sub

    Private Function mupp() As Double
        Return CPU
    End Function
    Private Sub Time1_Tick() Handles time1.Tick
        CPU = CType(pref.NextValue, Integer)
        'Console.WriteLine(pref.NextValue())


        Dim av As Long = My.Computer.Info.AvailablePhysicalMemory
        Dim tot As Long = My.Computer.Info.TotalPhysicalMemory
        ' SpeedDialControl2.Value = ((((tot) - av) / tot) * 100) '' Minnet
        'Dim r As Random = New Random()
        ' HalfCircleControl2.setProgress(r.Next(100))
        If check1.IsChecked Then
            For i As Integer = 0 To cpuCount - 1
                CPUList(i).Value = perfCounters(i).NextValue()
            Next

            HalfCircleControl1.setProgress(CPU)
            HalfCircleControl2.setProgress(((((tot) - av) / tot) * 100))
        End If
    End Sub

    Private Sub Slider_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
        If Not check1.IsChecked Then
            HalfCircleControl2.setProgress(slider1.Value)
            HalfCircleControl1.setProgress(slider1.Value)
            For i As Integer = 0 To cpuCount - 1
                CPUList(i).Value = slider1.Value
            Next
        End If
    End Sub

    Private Sub check1_Checked(sender As Object, e As RoutedEventArgs) Handles check1.Checked
        HalfCircleControl2.Unit = "RAM"
        HalfCircleControl1.Unit = "CPU"
    End Sub

    Private Sub check1_Unchecked(sender As Object, e As RoutedEventArgs) Handles check1.Unchecked
        HalfCircleControl2.Unit = "Km/h"
        HalfCircleControl1.Unit = "RPM"
    End Sub
End Class
