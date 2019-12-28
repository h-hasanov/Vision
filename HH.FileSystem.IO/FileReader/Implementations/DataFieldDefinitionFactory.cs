using System;
using HH.FileSystem.IO.FileReader.Interfaces;
using static HH.Extensions.Types.TypeExtensions;

namespace HH.FileSystem.IO.FileReader.Implementations
{
    public sealed class DataFieldDefinitionFactory : IDataFieldDefinitionFactory
    {
        public IDataFieldDefinition CreateDataFieldDefinition(Type dataType, string name, int index, bool include = true)
        {
            if (dataType == BoolType)
            {
                return CreateDataFieldDefinition<bool>(name, index, include);
            }

            if (dataType == Int32Type)
            {
                return CreateDataFieldDefinition<int>(name, index, include);
            }

            if (dataType == DoubleType)
            {
                return CreateDataFieldDefinition<double>(name, index, include);
            }

            if (dataType == DateTimeType)
            {
                return CreateDataFieldDefinition<DateTime>(name, index, include);
            }

            if (dataType == StringType)
            {
                return CreateDataFieldDefinition<string>(name, index, include);
            }

            throw new NotImplementedException();
        }

        public IDataFieldDefinition<T> CreateDataFieldDefinition<T>(string name, int index, bool include = true,
            T missingReplacementValue = default(T),
            T errorReplacementValue = default(T))
        {
            return new DataFieldDefinition<T>(name, index, include, missingReplacementValue, errorReplacementValue);
        }
    }
}
