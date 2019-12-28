using System.Globalization;
using HH.FileSystem.IO.Enums;

namespace HH.FileSystem.IO.FileReader.Interfaces
{
    public interface IDelimitedTextReaderConfiguration
    {
        bool SkipEmptyRecords { get; set; }
        bool HasHeaderRecord { get; set; }
        bool IgnoreBlankLines { get; set; }
        string Delimiter { get; set; }
        CultureInfo CultureInfo { get; set; }
        TextQualifierType TextQualifierType { get; set; }
        bool AllowComments { get; set; }
        char Comment { get; set; }
    }
}
