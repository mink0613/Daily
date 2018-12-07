using Daily.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Daily.Common
{
    [ValueConversion(typeof(TotalAmountType), typeof(SolidColorBrush))]
    public class TotalAmountTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((TotalAmountType)value == TotalAmountType.Minus)
            {
                return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Blue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
