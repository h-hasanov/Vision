using System;
using System.Windows.Data;
using HH.DockingManager.ViewModel.Interfaces;

namespace HH.DockingManager.View.Converters
{
    public class ActiveDocumentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IEditorViewModel)
                return value;

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is IEditorViewModel)
                return value;

            return Binding.DoNothing;
        }
    }
}