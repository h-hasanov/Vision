using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace HH.View.Utils.Converters
{
    [DebuggerNonUserCode]
    public class NegativeBooleanToVisiblityConverter : IValueConverter
    {
        #region Constructors

        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var bValue = (bool)value;
            if (!bValue)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            return false;
        }
        #endregion
    }
}