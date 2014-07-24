Imports System.Windows.Threading
Imports DNBSoft.WPF.RollingMonitor
Class MainWindow
    Dim pref As New PerformanceCounter
    Dim WithEvents time1 As New DispatcherTimer
    Dim CPU As Integer
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        pref.CategoryName = "Processor"
        pref.CounterName = "% Processor Time"
        pref.InstanceName = "_Total"
        pref.InstanceLifetime = PerformanceCounterInstanceLifetime.Global
        time1.Interval = TimeSpan.FromMilliseconds(1000)
        time1.Start()
        Dim ro As RollingSeries = New RollingSeries(chart1, New RollingSeries.NextValueDelegate(AddressOf mupp))
    End Sub

    Private Function mupp() As Double
        Return CPU
    End Function
    Private Sub Time1_Tick() Handles time1.Tick
        ' CPU = CType(pref.NextValue, Integer)
        Dim r As Random = New Random()
        Dim _r = r.Next(100)
        SpeedDialControl1.Value = _r
        HalfCircleControl1.setProgress(_r)
        linechart1.Update(_r)
        Dim av As Long = My.Computer.Info.AvailablePhysicalMemory
        Dim tot As Long = My.Computer.Info.TotalPhysicalMemory
        SpeedDialControl2.Value = ((((tot) - av) / tot) * 100)
    End Sub
End Class
