using System;
using System.Globalization;
using System.Windows.Data;

namespace Daily.Common
{
    [ValueConversion(typeof(int), typeof(string))]
    public class AbsoluteDecimalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = (int)value;
            if ((int)value < 0)
            {
                intValue = Math.Abs((int)value);
            }
            
            return string.Format("{0:###,###,###,###,###,###,###}", intValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
