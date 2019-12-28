using System.IO;
using HH.FileSystem.IO.CsvReader.Interfaces;
using HH.FileSystem.IO.FileReader.Interfaces;

namespace HH.FileSystem.IO.Win.CsvReader.Implementations
{
    public sealed class CsvReaderFactoryLw : ICsvReaderFactory
    {
        public ICsvReader CreateCsvReader(TextReader textReader, IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration)
        {
            return new CsvReaderLw(textReader, delimitedTextReaderConfiguration);
        }
    }
}
