using System;
using System.Globalization;
using System.Windows.Data;
using HH.ErrorManager.Model.Enums;
using HH.Icons.Model.Enums;

namespace HH.ErrorManager.View.Converters
{
    internal sealed class ErrorSeverityToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ToGlyph((ErrorSeverity)value);
        }

        private static GlyphType ToGlyph(ErrorSeverity errorSeverity)
        {
            switch (errorSeverity)
            {
                case ErrorSeverity.Error:
                    return GlyphType.Close;
                case ErrorSeverity.Information:
                    return GlyphType.Information;
                case ErrorSeverity.Warning:
                    return GlyphType.Warning;
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
