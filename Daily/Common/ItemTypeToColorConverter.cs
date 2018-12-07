using Daily.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Daily.Common
{
    [ValueConversion(typeof(ItemType), typeof(Visibility))]
    public class ItemTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ItemType)value == ItemType.Outcome)
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
