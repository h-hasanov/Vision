using System.Collections.Generic;
using System.IO;
using System.Linq;
using HH.Data.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.FileSystem.IO.CsvReader.Interfaces;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.Presentation.Interfaces;
using HH.Presentation.Services;
using ICsvReader = HH.FileSystem.IO.CsvReader.Interfaces.ICsvReader;


namespace HH.FileSystem.IO.FileReader.Implementations
{
    public sealed class DelimitedTextService : IDelimitedTextService
    {
        private readonly INamingService _namingService;
        private readonly ITypeDetectionService _typeDetectionService;
        private readonly IDataFieldDefinitionFactory _dataFieldDefinitionFactory;
        private const string FieldDefaultName = "Field";
        private readonly ICsvReaderFactory _csvReaderFactory;



        public DelimitedTextService(ICsvReaderFactory csvReader)
            : this(csvReader,
                  new DataFieldDefinitionFactory(),
                  new TypeDetectionService(),
                  new NamingService())
        {

        }

        internal DelimitedTextService(ICsvReaderFactory csvReaderFactory,
            IDataFieldDefinitionFactory dataFieldDefinitionFactory,
            ITypeDetectionService typeDetectionService,
            INamingService namingService)
        {
            _namingService = namingService.ArgumentNullCheck(nameof(namingService));
            _typeDetectionService = typeDetectionService.ArgumentNullCheck(nameof(typeDetectionService));
            _dataFieldDefinitionFactory = dataFieldDefinitionFactory.ArgumentNullCheck(nameof(dataFieldDefinitionFactory));
            _csvReaderFactory = csvReaderFactory.ArgumentNullCheck(nameof(csvReaderFactory));
        }

        /// <summary>
        /// Detects the file definition.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="delimitedTextReaderConfiguration"></param>
        /// <returns></returns>
        public IDataFieldDefinition[] DetectDataFieldDefinitions(TextReader reader, IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration)
        {
            var csvReader = _csvReaderFactory.CreateCsvReader(reader, delimitedTextReaderConfiguration);
            var data = Read(csvReader).Take(50).ToArray();

            if (data.Length == 0)
            {
                csvReader.Dispose();
                return new IDataFieldDefinition[0];
            }

            var fieldTypes = _typeDetectionService.DetectTypes(data);

            var fieldCount = csvReader.FieldCount;
            var headerNames = delimitedTextReaderConfiguration.HasHeaderRecord
                ? _namingService.UniquateNames(csvReader.Headers)
                : Enumerable.Range(1, fieldCount).Select(c => $"{FieldDefaultName} {c}").ToArray();

            var dataFields = Enumerable.Range(0, fieldCount)
                    .Select(i => _dataFieldDefinitionFactory.CreateDataFieldDefinition(fieldTypes[i], headerNames[i], i))
                    .ToArray();

            csvReader.Dispose();
            return dataFields;
        }

        private static IEnumerable<string[]> Read(ICsvReader csvReader)
        {
            while (csvReader.Read())
            {
                yield return csvReader.CurrentRecord;
            }
        }

        /// <summary>
        /// Reads the text with the given configuration
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="delimitedTextReaderConfiguration"></param>
        /// <returns></returns>
        public ICsvReader CreateCsvReader(TextReader reader,
            IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration)
        {
            return _csvReaderFactory.CreateCsvReader(reader, delimitedTextReaderConfiguration);
        }

        /// <summary>
        /// Reads the rows of the file.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="delimitedTextReaderConfiguration"></param>
        /// <param name="dataFieldDefinitions"></param>
        /// <returns></returns>
        public IDataReader CreateDataReader(TextReader reader, IDelimitedTextReaderConfiguration delimitedTextReaderConfiguration, IDataFieldDefinition[] dataFieldDefinitions)
        {
            var csvReader = _csvReaderFactory.CreateCsvReader(reader, delimitedTextReaderConfiguration);

            var includedDataFieldDefinitions = dataFieldDefinitions.Where(c => c.Include).ToArray();
            if (includedDataFieldDefinitions.Length == 0)
            {
                csvReader.Dispose();
                throw new InvalidDataException("No specified data fields to read");
            }
            return new DelimitedTextReader(dataFieldDefinitions, csvReader);
        }
    }
}
