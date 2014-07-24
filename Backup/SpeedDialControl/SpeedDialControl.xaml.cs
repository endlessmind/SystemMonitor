using System;
using System.Collections.Generic;
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

namespace SpeedDialApp
{
    /// <summary>
    /// Interaction logic for SpeedDialControl.xaml
    /// </summary>
    /// 
    public partial class SpeedDialControl : UserControl
    {
        public SpeedDialControl()
        {
            InitializeComponent();
        }

        public int Value
        {
            get {
                m_value = Convert.ToInt32(SpeedDial1.Value);
                return m_value; }
            set {
                m_value = value;
                SpeedDial1.Value = value;
            }
        }
        private int m_value;

    }
}
