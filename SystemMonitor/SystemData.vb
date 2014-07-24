Imports System.Management
Imports System.Text.RegularExpressions

Public Class SystemData

#Region "Constructor"
    Public Sub New()
        Dim cat As New PerformanceCounterCategory("Network Interface")
        _instanceNames = cat.GetInstanceNames()

        _netRecvCounters = New PerformanceCounter(_instanceNames.Length - 1) {}
        For i As Integer = 0 To _instanceNames.Length - 1
            _netRecvCounters(i) = New PerformanceCounter()
        Next

        _netSentCounters = New PerformanceCounter(_instanceNames.Length - 1) {}
        For i As Integer = 0 To _instanceNames.Length - 1
            _netSentCounters(i) = New PerformanceCounter()
        Next

        _compactFormat = False
    End Sub
#End Region

#Region "Members"
    Dim _compactFormat As Boolean

    Dim _memoryCounter As New PerformanceCounter()
    Dim _cpuCounter As New PerformanceCounter()
    Dim _diskReadCounter As New PerformanceCounter()
    Dim _diskWriteCounter As New PerformanceCounter()

    Dim _instanceNames As String()
    Dim _netRecvCounters As PerformanceCounter()
    Dim _netSentCounters As PerformanceCounter()

#End Region
    Public Enum DiskData
        ReadAndWrite
        Read
        Write
    End Enum

    Public Function GetDiskData(ByVal dd As DiskData) As Double
        Return If(dd = DiskData.Read, GetCounterValue(_diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total"), If(dd = DiskData.Write, GetCounterValue(_diskWriteCounter, "PhysicalDisk", "Disk Write Bytes/sec", "_Total"), If(dd = DiskData.ReadAndWrite, GetCounterValue(_diskReadCounter, "PhysicalDisk", "Disk Read Bytes/sec", "_Total") + GetCounterValue(_diskWriteCounter, "PhysicalDisk", "Disk Write Bytes/sec", "_Total"), 0)))
    End Function

    Public Enum NetData
        ReceivedAndSent
        Received
        Sent
    End Enum

    Public Function GetNetData(ByVal nd As NetData) As Double
        If _instanceNames.Length < 1 Then
            Return 0
        End If

        Dim d As Double = 0
        For i As Integer = 0 To _instanceNames.Length - 1
            d += If(nd = NetData.Received, GetCounterValue(_netRecvCounters(i), "Network Interface", "Bytes Received/sec", _instanceNames(i)), If(nd = NetData.Sent, GetCounterValue(_netSentCounters(i), "Network Interface", "Bytes Sent/sec", _instanceNames(i)), If(nd = NetData.ReceivedAndSent, GetCounterValue(_netRecvCounters(i), "Network Interface", "Bytes Received/sec", _instanceNames(i)) + GetCounterValue(_netSentCounters(i), "Network Interface", "Bytes Sent/sec", _instanceNames(i)), 0)))
        Next

        Return d
    End Function
    Private Enum Unit
        B
        KB
        MB
        GB
        ER
    End Enum
    Public Function FormatBytes(ByVal bytes As Double) As String
        Dim unit As Integer = 0
        While bytes > 1024
            bytes /= 1024
            unit += 1
        End While

        Dim s As String = If(_compactFormat, CInt(Math.Truncate(bytes)).ToString(), bytes.ToString("F") & " ")
        Return s & CType(unit, Unit).ToString()
    End Function

    Public Function QueryComputerSystem(ByVal type As String) As String
        Dim str As String = Nothing
        Dim objCS As New ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem")
        For Each objMgmt As ManagementObject In objCS.[Get]()
            str = objMgmt(type).ToString()
        Next
        Return str
    End Function

    Public Function QueryEnvironment(ByVal type As String) As String
        Return Environment.ExpandEnvironmentVariables(type)
    End Function
    Public Function getOSInfo() As String
        Dim objMOS As New ManagementObjectSearcher("SELECT * FROM  Win32_OperatingSystem")

        'Variables to hold our return value
        Dim os As String = ""
        Dim OSArch As Integer = 0

        Try
            For Each objManagement As ManagementObject In objMOS.[Get]()
                ' Get OS version from WMI - This also gives us the edition
                Dim osCaption As Object = objManagement.GetPropertyValue("Caption")
                If osCaption IsNot Nothing Then
                    ' Remove all non-alphanumeric characters so that only letters, numbers, and spaces are left.
                    Dim osC As String = Regex.Replace(osCaption.ToString(), "[^A-Za-z0-9 ]", "")
                    'string osC = osCaption.ToString();
                    ' If the OS starts with "Microsoft," remove it.  We know that already
                    If osC.StartsWith("Microsoft") Then
                        osC = osC.Substring(9)
                    End If
                    ' If the OS now starts with "Windows," again... useless.  Remove it.
                    If osC.Trim().StartsWith("Windows") Then
                        osC = osC.Trim().Substring(7)
                    End If
                    ' Remove any remaining beginning or ending spaces.
                    os = osC.Trim()
                    ' Only proceed if we actually have an OS version - service pack is useless without the OS version.
                    If Not [String].IsNullOrEmpty(os) Then
                        Dim osSP As Object = Nothing
                        Try
                            ' Get OS service pack from WMI
                            osSP = objManagement.GetPropertyValue("ServicePackMajorVersion")
                            If osSP IsNot Nothing AndAlso osSP.ToString() <> "0" Then
                                os += " Service Pack " & osSP.ToString()
                            Else
                                ' Service Pack not found.  Try built-in Environment class.
                                os += getOSServicePackLegacy()
                            End If
                        Catch generatedExceptionName As Exception
                            ' There was a problem getting the service pack from WMI.  Try built-in Environment class.
                            os += getOSServicePackLegacy()
                        End Try
                    End If
                    Dim osA As Object = Nothing
                    Try
                        ' Get OS architecture from WMI
                        osA = objManagement.GetPropertyValue("OSArchitecture")
                        If osA IsNot Nothing Then
                            Dim osAString As String = osA.ToString()
                            ' If "64" is anywhere in there, it's a 64-bit architectore.
                            OSArch = (If(osAString.Contains("64"), 64, 32))
                        End If
                    Catch generatedExceptionName As Exception
                    End Try
                End If
            Next
        Catch generatedExceptionName As Exception
        End Try
        ' If WMI couldn't tell us the OS, use our legacy method.
        ' We won't get the exact OS edition, but something is better than nothing.
        If os = "" Then
            os = getOSLegacy()
        Else
            os = "Windows " & os
        End If
        ' If WMI couldn't tell us the architecture, use our legacy method.
        If OSArch = 0 Then
            OSArch = getOSArchitectureLegacy()
        End If
        Return os & " " & OSArch.ToString() & "-bit"
    End Function
    Private Function getOSServicePackLegacy() As String
        ' Get service pack from Environment Class
        Dim sp As String = Environment.OSVersion.ServicePack
        If sp IsNot Nothing AndAlso sp.ToString() <> "" AndAlso sp.ToString() <> " " Then
            ' If there's a service pack, return it with a space in front (for formatting)
            Return " " & sp.ToString()
        End If
        ' No service pack.  Return an empty string
        Return ""
    End Function
    Private Function getOSArchitectureLegacy() As Integer
        Dim pa As String = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")
        Return (If(([String].IsNullOrEmpty(pa) OrElse [String].Compare(pa, 0, "x86", 0, 3, True) = 0), 32, 64))
    End Function
    Private Function getOSLegacy() As String
        'Get Operating system information.
        Dim os As OperatingSystem = Environment.OSVersion
        'Get version information about the os.
        Dim vs As Version = os.Version

        'Variable to hold our return value
        Dim operatingSystem As String = ""

        If os.Platform = PlatformID.Win32Windows Then
            'This is a pre-NT version of Windows
            Select Case vs.Minor
                Case 0
                    operatingSystem = "95"
                    Exit Select
                Case 10
                    If vs.Revision.ToString() = "2222A" Then
                        operatingSystem = "98SE"
                    Else
                        operatingSystem = "98"
                    End If
                    Exit Select
                Case 90
                    operatingSystem = "Me"
                    Exit Select
                Case Else
                    Exit Select
            End Select
        ElseIf os.Platform = PlatformID.Win32NT Then
            Select Case vs.Major
                Case 3
                    operatingSystem = "NT 3.51"
                    Exit Select
                Case 4
                    operatingSystem = "NT 4.0"
                    Exit Select
                Case 5
                    If vs.Minor = 0 Then
                        operatingSystem = "2000"
                    Else
                        operatingSystem = "XP"
                    End If
                    Exit Select
                Case 6
                    If vs.Minor = 0 Then
                        operatingSystem = "Vista"
                    Else
                        operatingSystem = "7"
                    End If
                    Exit Select
                Case Else
                    Exit Select
            End Select
        End If
        'Make sure we actually got something in our OS check
        'We don't want to just return " Service Pack 2"
        'That information is useless without the OS version.
        If operatingSystem <> "" Then
            'Got something.  Let's see if there's a service pack installed.
            operatingSystem += getOSServicePackLegacy()
        End If
        'Return the information we've gathered.
        Return operatingSystem
    End Function

    Public Function GetGraphicsCardName() As String
        Dim GraphicsCardName = String.Empty
        Try
            Dim WmiSelect As New ManagementObjectSearcher _
            ("root\CIMV2", "SELECT * FROM Win32_VideoController")
            For Each WmiResults As ManagementObject In WmiSelect.Get()
                GraphicsCardName = WmiResults.GetPropertyValue("Name").ToString
                If (Not String.IsNullOrEmpty(GraphicsCardName)) Then
                    Exit For
                End If
            Next
        Catch err As ManagementException
            MessageBox.Show(err.Message)
        End Try
        Return GraphicsCardName
    End Function

#Region "Private Helpers"
    Private Function GetCounterValue(ByVal pc As PerformanceCounter, ByVal categoryName As String, ByVal counterName As String, ByVal instanceName As String) As Double
        pc.CategoryName = categoryName
        pc.CounterName = counterName
        pc.InstanceName = instanceName
        Return pc.NextValue()
    End Function

#End Region


End Class
