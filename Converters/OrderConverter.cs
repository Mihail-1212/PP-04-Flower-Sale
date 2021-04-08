using System;
using System.Globalization;
using System.Windows.Data;

namespace FlowersSale.Converters
{
    class OrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return value;
            decimal totalPrice = (decimal)value;
            return $"Оформить ({String.Format("{0:0.00}", totalPrice).Replace(',', '.')})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
