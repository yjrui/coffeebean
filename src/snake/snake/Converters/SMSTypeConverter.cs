using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using datasource;

namespace snake.Converters
{
    public class SMSTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.ToString() == dSMS.SMSType.DELIVER.ToString())
                return "接收";
            else if (value.ToString() == dSMS.SMSType.SUBMIT.ToString())
                return "发送";
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
