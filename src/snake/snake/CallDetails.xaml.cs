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
using datasource;

namespace snake
{
    /// <summary>
    /// Interaction logic for CallDetails.xaml
    /// </summary>
    public partial class CallDetails : UserControl
    {
        private Call _call;
        public CallDetails(Call call_)
        {
            InitializeComponent();
            _call = call_;
            DataContext = call_;
        }
    }
}
