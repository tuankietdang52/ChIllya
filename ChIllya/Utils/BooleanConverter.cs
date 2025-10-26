using System.Globalization;

namespace ChIllya.Utils
{
    public class BooleanConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isValue)
            {
                return !isValue;
            }

            return value;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isValue)
            {
                return !isValue;
            }

            return value;
        }
    }
}
