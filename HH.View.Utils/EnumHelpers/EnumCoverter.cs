using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using HH.Extensions.Enums;

namespace HH.View.Utils.EnumHelpers
{
    [DebuggerNonUserCode]
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value == null ? DependencyProperty.UnsetValue : ((Enum) value).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return Enum.ToObject(targetType, value);
        }
    }
}