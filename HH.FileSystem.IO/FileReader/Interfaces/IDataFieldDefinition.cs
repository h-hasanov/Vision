using System;

namespace HH.FileSystem.IO.FileReader.Interfaces
{
    public interface IDataFieldDefinition
    {
        string Name { get; }
        Type DataType { get; }
        int Index { get; }
        bool Include { get; }
        string DisplayMissingReplacementValue { get; }
        string DisplayErrorReplacementValue { get; }
    }

    public interface IDataFieldDefinition<T> : IDataFieldDefinition
    {
        T MissingReplacementValue { get; set; }
        T ErrorReplacementValue { get; set; }
    }
}
