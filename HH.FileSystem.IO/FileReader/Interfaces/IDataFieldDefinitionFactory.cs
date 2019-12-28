using System;

namespace HH.FileSystem.IO.FileReader.Interfaces
{
    public interface IDataFieldDefinitionFactory
    {
        IDataFieldDefinition CreateDataFieldDefinition(Type dataType, string name, int index,
            bool include = true);

        IDataFieldDefinition<T> CreateDataFieldDefinition<T>(string name, int index, bool include = true,
            T missingReplacementValue = default(T),
            T errorReplacementValue = default(T));
    }
}
