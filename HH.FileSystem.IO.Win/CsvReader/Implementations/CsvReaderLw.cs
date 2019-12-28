using System.IO;
using HH.Extensions.Types;
using HH.FileSystem.IO.CsvReader.Interfaces;
using HH.FileSystem.IO.Enums;
using HH.FileSystem.IO.FileReader.Interfaces;
using LumenWorks.Framework.IO.Csv;

namespace HH.FileSystem.IO.Win.CsvReader.Implementations
{
    public sealed class CsvReaderLw : ICsvReader
    {
        private readonly IDelimitedTextReaderConfiguration _delimitedTextReaderConfiguration;
        private readonly LumenWorks.Framework.IO.Csv.CsvReader _csvReader;

        public CsvReaderLw(TextReader textReader, IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration)
        {
            _delimitedTextReaderConfiguration = delimitedTextReaderConfiguration;
            var ignoreBlankLines = delimitedTextReaderConfiguration.IgnoreBlankLines;
            var delimiter = delimitedTextReaderConfiguration.Delimiter.ToCharArray()[0];

            var comment = char.MinValue;
            if (delimitedTextReaderConfiguration.AllowComments)
                comment = delimitedTextReaderConfiguration.Comment;

            var quote = delimitedTextReaderConfiguration.TextQualifierType.ToChar();

            _csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(
                reader: textReader,
                hasHeaders: false, //Set this to false otherwise duplicate headers cause errors. When you read the file set the headers manually in the Read method
                delimiter: delimiter,
                quote: quote,
                escape: quote,
                comment: comment,
                trimmingOptions: ValueTrimmingOptions.None)
            {
                MissingFieldAction = MissingFieldAction.ReplaceByNull,
                SkipEmptyLines = ignoreBlankLines
            };
        }

        public string[] Headers { get; private set; }

        public string[] CurrentRecord
        {
            get
            {
                var fieldCount = _csvReader.FieldCount;
                if (fieldCount == 0)
                    return null;

                var currentRecord = new string[fieldCount];
                for (var i = 0; i < fieldCount; i++)
                {
                    currentRecord[i] = _csvReader[i];
                }
                return currentRecord;
            }
        }

        public int FieldCount
        {
            get { return _csvReader.FieldCount; }
        }

        public string GetField(int index)
        {
            return _csvReader[index];
        }

        public T GetField<T>(int index)
        {
            return TypeExtensions.ConvertFromString<T>(_csvReader[index], _delimitedTextReaderConfiguration.CultureInfo);
        }

        public bool TryGetField<T>(int index, out T output)
        {
            try
            {
                output = GetField<T>(index);
                return true;
            }
            catch
            {
                output = default(T);
                return false;
            }
        }

        public bool Read()
        {
            var nextRecord = _csvReader.ReadNextRecord();
            if (_csvReader.CurrentRecordIndex == 0 && _delimitedTextReaderConfiguration.HasHeaderRecord)
            {
                Headers = CurrentRecord;
                Read();
            }
            return nextRecord;
        }

        public void Dispose()
        {
            _csvReader?.Dispose();
        }
    }
}
