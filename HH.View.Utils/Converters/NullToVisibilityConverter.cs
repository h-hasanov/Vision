using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HH.Extensions.Objects;

namespace HH.View.Utils.Converters
{
    public sealed class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Collapsed;
            if (value.GetType().IsClass) return Visibility.Visible;
            return value.IsDefault(value.GetType()) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
