using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Chronometer.Converter
{
    public class BooleanToColorConverter : IValueConverter
    {
        public Brush TrueColor { get; set; }
        public Brush FalseColor { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            if (boolValue)
                return TrueColor;
            return FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
