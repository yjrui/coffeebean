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
    /// Interaction logic for SmsDetails.xaml
    /// </summary>
    public partial class SmsDetails : UserControl
    {
        private dSMS _sms;

        public SmsDetails(dSMS sms_)
        {
            InitializeComponent();
            _sms = sms_;
            DataContext = _sms;
        }
    }
}
