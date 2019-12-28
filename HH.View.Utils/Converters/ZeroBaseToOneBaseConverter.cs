using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace HH.View.Utils.Converters
{
    [DebuggerNonUserCode]
    public sealed class ZeroBaseToOneBaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intValue = (int) value;
            intValue += 1;
            return intValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
