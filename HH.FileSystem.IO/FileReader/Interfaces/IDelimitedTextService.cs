using System.IO;
using HH.Data.Interfaces;
using HH.FileSystem.IO.CsvReader.Interfaces;

namespace HH.FileSystem.IO.FileReader.Interfaces
{
    public interface IDelimitedTextService
    {
        /// <summary>
        /// Detects the file definition.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="delimitedTextReaderConfiguration"></param>
        /// <returns></returns>
        IDataFieldDefinition[] DetectDataFieldDefinitions(TextReader reader, 
            IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration);

        /// <summary>
        /// Reads the text with the given configuration
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="delimitedTextReaderConfiguration"></param>
        /// <returns></returns>
        ICsvReader CreateCsvReader(TextReader reader,
            IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration);

        /// <summary>
        /// Reads the text with the given configuration and data field definitions
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="delimitedTextReaderConfiguration"></param>
        /// <param name="dataFieldDefinitions"></param>
        /// <returns></returns>
        IDataReader CreateDataReader(TextReader reader, IDelimitedTextReaderConfiguration 
            delimitedTextReaderConfiguration, 
            IDataFieldDefinition[] dataFieldDefinitions);
    }
}
