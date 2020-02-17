using Daily.Model;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Daily.Common
{
    [ValueConversion(typeof(ItemType), typeof(SolidColorBrush))]
    public class ItemTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ItemType)value == ItemType.Income)
            {
                return new SolidColorBrush(Colors.Blue);
            }

            return new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
