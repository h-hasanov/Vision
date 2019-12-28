using System;

namespace HH.FileSystem.IO.Enums
{
    public static class TextQualifierTypeExtensions
    {
        public static char ToChar(this TextQualifierType textQualifierType)
        {
            switch (textQualifierType)
            {
                case TextQualifierType.None:
                    return char.MinValue;
                case TextQualifierType.DoubleQuote:
                    return '"';
                case TextQualifierType.SingleQuote:
                    return '\'';
                default:
                    throw new ArgumentOutOfRangeException(nameof(textQualifierType), textQualifierType, null);
            }
        }
    }
}