using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace snake.Converters
{
    public class CallStatusConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int enumValue;
            if (int.TryParse(value.ToString(), out enumValue))
            {
                switch (enumValue)
                {
                    case 0: return "Received";
                    case 1: return "Called";
                    case 2: return "Missed";
                }
            }
            throw new ArgumentException("not supported value: ", value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
