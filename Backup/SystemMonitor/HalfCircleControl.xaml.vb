Imports System.Threading
Imports System.Windows.Threading

Public Class HalfCircleControl

    Private progress As Integer = 0
    Private NewProgress As Integer = 0
    Dim WithEvents time1 As New DispatcherTimer
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        time1.Interval = TimeSpan.FromMilliseconds(1)

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub Time1_Tick() Handles time1.Tick

        Dim centerW As Integer = canvas2.ActualWidth / 2
        Dim centerH As Integer = 3
        Dim rt As New Rectangle
        canvas2.Children.Clear()

        Dim l1 As Line = New Line()
        l1.StrokeThickness = 2
        l1.Stroke = Brushes.Black

        l1.X1 = centerW
        l1.Y1 = centerH
        Dim Endx1 As Integer
        Dim Endy1 As Integer

        If NewProgress > progress Then
            progress += 1
        ElseIf NewProgress < progress Then
            progress -= 1
        End If
        Console.WriteLine(progress)
        Endx1 = centerW + (centerW - 35) * Math.Cos(1.8 * (100 + progress) * Math.PI / 180)
        Endy1 = centerH - (centerW - 35) * Math.Sin(1.8 * (100 + progress) * Math.PI / 180)
        l1.X2 = Endx1
        l1.Y2 = Endy1

        canvas2.Children.Add(l1)

        Dim e As New Ellipse
        e.Fill = Brushes.DarkGreen
        e.Height = 20
        e.Width = 20
        e.Stretch = Stretch.Fill
        e.Stroke = Brushes.Black
        e.StrokeThickness = 2
        canvas.SetTop(e, 3 - 10)
        canvas.SetLeft(e, centerW - 10)
        canvas2.Children.Add(e)

        If NewProgress = progress Then
            time1.Stop()
        End If

    End Sub

    Public Sub setProgress(ByVal value As Integer)
        '  progress = value
        NewProgress = value
        lblText.Content = "%" & value
        ' Validate()

        time1.Start()




    End Sub

    Public Sub Validate()
        canvas.Children.Clear()
        Dim centerW As Integer = canvas.ActualWidth / 2
        Dim centerH As Integer = 3
        Console.WriteLine(centerW & " - " & centerH)
        Dim e As New Ellipse
        e.Fill = Brushes.Red
        e.Height = (canvas.ActualHeight - 20) * 2
        e.Width = canvas.ActualWidth - 3
        Dim recG As New RectangleGeometry
        recG.Rect = New Rect(0, (e.Height / 2) + 1, e.Width, e.Height)
        e.Clip = recG
        e.Stretch = Stretch.Fill
        e.Stroke = Brushes.Black
        e.StrokeThickness = 2
        canvas.SetTop(e, -(canvas.ActualHeight - 20))
        canvas.SetLeft(e, centerW - (e.Width / 2))
        canvas.Children.Add(e)

        For i As Integer = 0 To 19
            Dim Startx As Integer
            Dim Starty As Integer

            Dim Endx As Integer
            Dim Endy As Integer
            If i = 0 Or i = 10 Or i = 15 Then
                Startx = centerW + (centerW - 3) * Math.Cos(18 * i * Math.PI / 180)
                Starty = centerH - (centerW - 3) * Math.Sin(18 * i * Math.PI / 180)

                Endx = centerW + (centerW - 20) * Math.Cos(18 * i * Math.PI / 180)
                Endy = centerH - (centerW - 20) * Math.Sin(18 * i * Math.PI / 180)
            Else
                Startx = centerW + (centerW - 3) * Math.Cos(18 * i * Math.PI / 180)
                Starty = centerH - (centerW - 3) * Math.Sin(18 * i * Math.PI / 180)

                Endx = centerW + (centerW - 13) * Math.Cos(18 * i * Math.PI / 180)
                Endy = centerH - (centerW - 13) * Math.Sin(18 * i * Math.PI / 180)
            End If
            Console.WriteLine(Math.Sin(18 * i * Math.PI / 180))
            Console.WriteLine(i)
            Console.WriteLine("")
            Dim l As Line = New Line()
            If i = 0 Or i = 10 Or i = 15 Then
                l.StrokeThickness = 3
            Else
                l.StrokeThickness = 1
            End If

            l.Stroke = Brushes.Black

            l.X1 = Startx
            l.Y1 = Starty

            l.X2 = Endx
            l.Y2 = Endy
            If i = 0 Or i > 9 Then
                canvas.Children.Add(l)
            End If

        Next

    End Sub

    Private Sub HalfCircleControl_SizeChanged(sender As Object, e As System.Windows.SizeChangedEventArgs) Handles Me.SizeChanged
        Validate()

        Dim centerW As Integer = canvas2.ActualWidth / 2
        Dim centerH As Integer = 3
        Dim l1 As Line = New Line()
        l1.StrokeThickness = 2
        l1.Stroke = Brushes.Black

        l1.X1 = centerW
        l1.Y1 = centerH
        Dim Endx1 As Integer
        Dim Endy1 As Integer
        Endx1 = centerW + (centerW - 35) * Math.Cos(1.8 * (100 + progress) * Math.PI / 180)
        Endy1 = centerH - (centerW - 35) * Math.Sin(1.8 * (100 + progress) * Math.PI / 180)
        l1.X2 = Endx1
        l1.Y2 = Endy1

        canvas2.Children.Add(l1)

        Dim e1 As New Ellipse
        e1.Fill = Brushes.DarkGreen
        e1.Height = 20
        e1.Width = 20
        e1.Stretch = Stretch.Fill
        e1.Stroke = Brushes.Black
        e1.StrokeThickness = 2
        canvas.SetTop(e1, 3 - 10)
        canvas.SetLeft(e1, centerW - 10)
        canvas2.Children.Add(e1)
    End Sub
End Class
