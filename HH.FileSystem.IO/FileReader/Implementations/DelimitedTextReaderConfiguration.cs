using System.Globalization;
using HH.FileSystem.IO.Enums;
using HH.FileSystem.IO.FileReader.Interfaces;

namespace HH.FileSystem.IO.FileReader.Implementations
{
    public sealed class DelimitedTextReaderConfiguration : IDelimitedTextReaderConfiguration
    {
        public DelimitedTextReaderConfiguration()
        {
            CultureInfo = CultureInfo.CurrentCulture;
            Delimiter = ",";
            HasHeaderRecord = true;
            TextQualifierType=TextQualifierType.DoubleQuote;
            SkipEmptyRecords = false;
            IgnoreBlankLines = false;
            AllowComments = false;
            Comment = '#';
        }

        public bool SkipEmptyRecords { get; set; }
        public bool HasHeaderRecord { get; set; }
        public bool IgnoreBlankLines { get; set; }
        public string Delimiter { get; set; }
        public CultureInfo CultureInfo { get; set; }
        public TextQualifierType TextQualifierType { get; set; }
        public bool AllowComments { get; set; }
        public char Comment { get; set; }
    }
}
