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
using System.Windows.Shapes;

namespace snake
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ImportOption : Window
    {
        public ImportOption()
        {
            InitializeComponent();
        }

        public bool IsOKClicked { get; set; }

        private void lbCancel_Click(object sender, RoutedEventArgs e)
        {
            IsOKClicked = false;
            Close();
        }

        private void lbOK_Click(object sender, RoutedEventArgs e)
        {
            IsOKClicked = true;
            Close();
        }
    }
}
