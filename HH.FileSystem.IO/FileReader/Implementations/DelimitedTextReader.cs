using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HH.Data.Interfaces;
using HH.EnvironmentServices.BaseModels;
using HH.EnvironmentServices.Utils;
using HH.FileSystem.IO.FileReader.Interfaces;
using ICsvReader = HH.FileSystem.IO.CsvReader.Interfaces.ICsvReader;

namespace HH.FileSystem.IO.FileReader.Implementations
{
    internal sealed class DelimitedTextReader : DisposableBase, IDataReader
    {
        private readonly ICsvReader _csvReader;
        private readonly IDictionary<int, IDataFieldDefinition> _indexMapper;
        private readonly IDictionary<string, IDataFieldDefinition> _nameMapper;

        public DelimitedTextReader(IDataFieldDefinition[] dataFieldDefinitions, ICsvReader csvReader)
        {
            _csvReader = csvReader.ArgumentNullCheck(nameof(csvReader));
            dataFieldDefinitions.ArgumentNullCheck(nameof(dataFieldDefinitions));

            if (dataFieldDefinitions.Select(c => c.Name).Distinct().Count() != dataFieldDefinitions.Length)
                throw new InvalidDataException("Data field names must be unique");

            var includedDataFieldDefinitions = dataFieldDefinitions.Where(c => c.Include).ToArray();
            if (includedDataFieldDefinitions.Length == 0)
                throw new InvalidDataException("At least 1 included data field is required");

            _indexMapper = includedDataFieldDefinitions.ToDictionary(c => c.Index, c => c);
            _nameMapper = includedDataFieldDefinitions.ToDictionary(c => c.Name, c => c);

            FieldTypes = includedDataFieldDefinitions.Select(c => c.DataType).ToArray();
        }

        public string[] Headers { get { return _nameMapper.Keys.ToArray(); } }

        public string[] CurrentRecord { get { return _csvReader.CurrentRecord; } }

        public Type[] FieldTypes { get; }


        public bool Read()
        {
            return _csvReader.Read();
        }

        public T GetField<T>(string columnName)
        {
            return GetValue<T>(_nameMapper[columnName]);
        }

        public T GetField<T>(int index)
        {
            return GetValue<T>(_indexMapper[index]);
        }

        private T GetValue<T>(IDataFieldDefinition dataFieldDefinition)
        {
            var cellEntry = _csvReader.GetField(dataFieldDefinition.Index);
            if (string.IsNullOrWhiteSpace(cellEntry))
            {
                return ((IDataFieldDefinition<T>)dataFieldDefinition).MissingReplacementValue;
            }

            T output;
            if (!_csvReader.TryGetField(dataFieldDefinition.Index, out output))
            {
                return ((IDataFieldDefinition<T>)dataFieldDefinition).ErrorReplacementValue;
            }
            return output;
        }

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            _csvReader?.Dispose();
        }
    }
}
