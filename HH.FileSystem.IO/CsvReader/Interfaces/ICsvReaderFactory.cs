using System.IO;
using HH.FileSystem.IO.FileReader.Interfaces;

namespace HH.FileSystem.IO.CsvReader.Interfaces
{
    public interface ICsvReaderFactory
    {
        ICsvReader CreateCsvReader(TextReader textReader, IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration);
    }
}
