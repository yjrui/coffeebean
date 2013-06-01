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
using snake.Model;

namespace snake
{
    /// <summary>
    /// Interaction logic for ContactDetails.xaml
    /// </summary>
    public partial class ContactDetails : UserControl
    {
        private Contact _contact;

        public ContactDetails(Contact contact_)
        {
            InitializeComponent();
            _contact = contact_;
            DataContext = _contact;
        }
    }
}
