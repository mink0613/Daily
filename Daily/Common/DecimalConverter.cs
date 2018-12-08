using System;
using System.Globalization;
using System.Windows.Data;

namespace Daily.Common
{
    [ValueConversion(typeof(int), typeof(string))]
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string && (string)value == "")
            {
                return "";
            }

            int intValue = 0;
            if (value is int)
            {
                intValue = (int)value;
            }
            else if (value is string)
            {
                string strValue = (string)value;
                strValue = strValue.Replace(",", "");
                intValue = int.Parse(strValue);
            }
            
            return string.Format("{0:###,###,###,###,###,###,###}", intValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
