using System;
using System.Globalization;
using System.Windows.Data;

namespace Daily.Common
{
    [ValueConversion(typeof(int), typeof(int))]
    public class AbsoluteValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value < 0)
            {
                return Math.Abs((int)value);
            }

            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
