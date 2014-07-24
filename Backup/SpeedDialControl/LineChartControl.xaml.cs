using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace SpeedDialControl.LineChart
{
    /// <summary>
    /// Interaction logic for LineChartControl.xaml
    /// </summary>
    public partial class LineChartControl : Canvas
    {

        Polyline myPolyline;
        DispatcherTimer timer = new DispatcherTimer();
        Random gen = new Random(DateTime.Now.Millisecond);
        int x = 0;
        int step = 10;
        ObservableCollection<Point> points = new ObservableCollection<Point>();
        public LineChartControl()
        {
            InitializeComponent();
            myPolyline = new Polyline();
          myPolyline.Stroke = System.Windows.Media.Brushes.Red;
            myPolyline.StrokeThickness = 1;
            myPolyline.FillRule = FillRule.Nonzero;
            this.area.Children.Add(myPolyline);
            for (int i = 0; i < 99; i++)
            {
                Random r = new Random();
                Point p = new Point(i +10 , r.Next(100) * 10);
                points.Add(p);
            }
            //timer.Interval = TimeSpan.FromMilliseconds(500);
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
            this.DataContext = points;
        }
        public void Update(int value)
        {
                points.RemoveAt(points.Count -1);
                ObservableCollection<Point> newpoint = new ObservableCollection<Point>();
                for (int i = 1; i < points.Count - 1; i++)
                {
                    Point p = new Point(points[i].X - 10, points[i].Y);
                    newpoint.Add(p);
                    myPolyline.Points.Add(p);
                }
                newpoint.Add(new Point(points.Count + 10, value * 10));
            points = newpoint;
            this.area.Children.Clear();
            this.area.Children.Add(myPolyline);
            
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Point point = new Point(x, gen.Next(1000));
            x += step;
            points.Add(point);
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Point point = (Point)(sender as Border).DataContext;
            myPolyline.Points.Add(point);
        }
    }
}
