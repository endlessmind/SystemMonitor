Imports System.Threading
Imports System.Windows.Threading

Public Class HalfCircleControl

    Private progress As Integer = 0
    Private NewProgress As Integer = 0
    Private Maximum As Integer = 100
    Dim WithEvents time1 As New DispatcherTimer
    Private mUnit As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        time1.Interval = TimeSpan.FromMilliseconds(10)

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Shared UnitProperty As DependencyProperty = _
DependencyProperty.Register("Unit", GetType(String), GetType(HalfCircleControl), New PropertyMetadata("", New PropertyChangedCallback(AddressOf OnUnitChange), New CoerceValueCallback(AddressOf OnCoreUnit)))


    Private Shared Sub OnUnitChange(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

        CType(d, HalfCircleControl).Unit = (CType(e.NewValue, String))
        'Dim sh As CPU = CType(d, CPU)
    End Sub

    Private Shared Function OnCoreUnit(ByVal sender As DependencyObject, ByVal data As Object) As Object
        Return data
    End Function

    Public Property Unit As String
        Get
            Return mUnit
        End Get
        Set(value As String)
            mUnit = value
            lblUnit.Content = value
        End Set
    End Property

    Private Sub Time1_Tick() Handles time1.Tick

        Dim centerW As Integer = canvas2.ActualWidth / 2
        Dim centerH As Integer = canvas2.ActualHeight / 2
        Dim rt As New Rectangle
        canvas2.Children.Clear()
        Dim l1 As Line = New Line()
        l1.StrokeThickness = 2
        l1.Stroke = Brushes.Red



        l1.X1 = centerW
        l1.Y1 = centerH
        Dim Endx1 As Integer
        Dim Endy1 As Integer
        Dim dif As Integer = 0
        If NewProgress > progress Then
            dif = NewProgress - progress
            If (dif < 10) Then
                progress += 1
            ElseIf (dif > 10) Then
                progress += 8
            End If

        ElseIf NewProgress < progress Then
            dif = NewProgress - progress
            If (dif > -10) Then
                progress -= 1
            ElseIf (dif < -10) Then
                progress -= 8
            End If
            'progress -= 1
        End If
        Dim le As Integer = 0
        If centerW * 2 < 150 Then
            le = 20
        Else
            le = 35
        End If
        Dim angle As Double = progress * (270 / Maximum) * (Math.PI / 180)

        Endx1 = centerW + Math.Cos(angle) * (centerW - 15)
        Endy1 = centerH + Math.Sin(angle) * (centerW - 15)
        l1.X2 = Endx1
        l1.Y2 = Endy1

        canvas2.Children.Add(l1)

        Dim e As New Ellipse
        e.Fill = Brushes.Red
        e.Height = 8
        e.Width = 8
        e.Stretch = Stretch.Fill
        e.Stroke = Brushes.Black
        e.StrokeThickness = 0
        canvas.SetTop(e, centerH - 4)
        canvas.SetLeft(e, centerW - 4)
        canvas2.Children.Add(e)

        If NewProgress = progress Then
            time1.Stop()
        End If

    End Sub

    Public Sub setProgress(ByVal value As Integer)
        NewProgress = value
        lblText.Content = "%" & Math.Round((value / Maximum) * 100)

        time1.Start()




    End Sub

    Public Shared ValueProperty As DependencyProperty = _
DependencyProperty.Register("Value", GetType(Integer), GetType(HalfCircleControl), New PropertyMetadata(0, New PropertyChangedCallback(AddressOf OnValueChange), New CoerceValueCallback(AddressOf OnCoreValue)))

    Private Shared Sub OnValueChange(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)

        CType(d, HalfCircleControl).setProgress(CType(e.NewValue, Integer))
        'Dim sh As CPU = CType(d, CPU)
    End Sub

    Private Shared Function OnCoreValue(ByVal sender As DependencyObject, ByVal data As Object) As Object
        Return data
    End Function

    Public Property Value
        Get
            Return progress
        End Get
        Set(value)
            setProgress(value)
        End Set
    End Property

    Public Sub Validate()
        canvas.Children.Clear()
        Dim centerW As Integer = canvas.ActualWidth / 2
        Dim centerH As Integer = canvas.ActualHeight / 2
        Console.WriteLine(canvas.ActualWidth & " - " & canvas.ActualHeight)
        Dim e As New Ellipse
        e.Fill = Brushes.Black
        e.Height = canvas.ActualHeight
        e.Width = canvas.ActualWidth
        Dim recG As New RectangleGeometry
        recG.Rect = New Rect(0, 0, e.Width, e.Height)
        e.Clip = recG
        e.Stretch = Stretch.Fill
        e.Stroke = Brushes.Black
        e.StrokeThickness = 2
        canvas.SetTop(e, 0)
        canvas.SetLeft(e, centerW - (e.Width / 2))
        canvas.Children.Add(e)

        For i As Integer = 0 To 100 Step 5
            Dim Startx As Integer
            Dim Starty As Integer

            Dim Endx As Integer
            Dim Endy As Integer
            Dim angle As Double = i * (270 / 100) * (Math.PI / 180)
            Dim iString As String = "" & i
            If iString.Contains("5") And Not iString.Contains("0") Then
                Startx = centerW + Math.Cos(angle) * (centerW - 15)
                Starty = centerH + Math.Sin(angle) * (centerW - 15)

                Endx = centerW + Math.Cos(angle) * (centerW - 5)
                Endy = centerH + Math.Sin(angle) * (centerW - 5)
            Else
                Startx = centerW + Math.Cos(angle) * (centerW - 15)
                Starty = centerH + Math.Sin(angle) * (centerW - 15)

                Endx = centerW + Math.Cos(angle) * (centerW - 5)
                Endy = centerH + Math.Sin(angle) * (centerW - 5)
            End If

            Dim l As Line = New Line()
            If iString.Contains("5") And Not iString.Contains("0") Then
                l.StrokeThickness = 1
                l.Stroke = Brushes.DarkGray
            Else
                l.StrokeThickness = 2
                l.Stroke = Brushes.White

            End If
            'RenderOptions.SetEdgeMode(Me, EdgeMode.Unspecified)



            l.X1 = Startx
            l.Y1 = Starty

            l.X2 = Endx
            l.Y2 = Endy

            canvas.Children.Add(l)

        Next
        'Dim lenght As Integer = 28
        ' Dim lengtCount As Integer = 0
        For i As Integer = 75 To 100

            Dim Startx As Double
            Dim Starty As Double

            Dim Endx As Double
            Dim Endy As Double



            Dim angle As Double = i * (270 / 100) * (Math.PI / 180)
            Startx = centerW + Math.Cos(angle) * (centerW - 35)
            Starty = centerH + Math.Sin(angle) * (centerW - 35)


            Endx = centerW + Math.Cos(angle) * (centerW - 25)
            Endy = centerH + Math.Sin(angle) * (centerW - 25)


            Dim l As Line = New Line()


            Dim b As Brush = Brushes.Red
            If i > 0 And i < 50 Then
                '' b = Brushes.Green
            ElseIf i > 49 And i < 85 Then
                l.StrokeThickness = ((i / 100) * 2.5)
                'ElseIf i > 77 And i < 80 Then
                '    l.StrokeThickness = 2
                'ElseIf i > 79 And i < 85 Then
                '    l.StrokeThickness = 3
            ElseIf i > 84 And i < 90 Then
                l.StrokeThickness = 1 + ((i / 100) * 2.7)
            ElseIf i > 89 Then
                l.StrokeThickness = 8
                ''  b = Brushes.Red
            End If

            l.Stroke = b
            'l.SnapsToDevicePixels = True

            l.X1 = Startx
            l.Y1 = Starty

            l.X2 = Endx
            l.Y2 = Endy
            canvas.Children.Add(l)

        Next


    End Sub

    Private Sub HalfCircleControl_SizeChanged(sender As Object, e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
        Validate()

        'Dim centerW As Integer = canvas2.ActualWidth / 2
        'Dim centerH As Integer = 3
        'Dim l1 As Line = New Line()
        'l1.StrokeThickness = 2
        'l1.Stroke = Brushes.Black

        'l1.X1 = centerW
        'l1.Y1 = centerH
        'Dim Endx1 As Integer
        'Dim Endy1 As Integer
        ''Dim le As Integer = 0
        ''If centerW * 2 < 150 Then
        ''    le = 20
        ''Else
        ''    le = 35
        ''End If
        'Dim angle As Double = progress * (270 / Maximum) * (Math.PI / 180)
        'Endx1 = centerW + Math.Cos(angle) * (centerW - 15)
        'Endy1 = centerH + Math.Sin(angle) * (centerW - 15)
        'l1.X2 = Endx1
        'l1.Y2 = Endy1

        'canvas2.Children.Add(l1)

        'Dim e1 As New Ellipse
        'e1.Fill = Brushes.DarkGreen
        'e1.Height = 20
        'e1.Width = 20
        'e1.Stretch = Stretch.Fill
        'e1.Stroke = Brushes.Black
        'e1.StrokeThickness = 2
        'canvas.SetTop(e1, 3 - 10)
        'canvas.SetLeft(e1, centerW - 10)
        'canvas2.Children.Add(e1)
        Time1_Tick()
    End Sub
End Class
