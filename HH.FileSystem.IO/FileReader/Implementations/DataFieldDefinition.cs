using System;
using HH.FileSystem.IO.FileReader.Interfaces;

namespace HH.FileSystem.IO.FileReader.Implementations
{
    public sealed class DataFieldDefinition<T> : IDataFieldDefinition<T>
    {
        public DataFieldDefinition(string name,
            int index,
            bool include = true,
            T missingReplacementValue = default(T),
            T errorReplacementValue = default(T))
        {
            Name = name;
            MissingReplacementValue = missingReplacementValue;
            ErrorReplacementValue = errorReplacementValue;
            Include = include;
            Index = index;
            DataType = typeof(T);
        }

        public string Name { get; }
        public Type DataType { get; }
        public int Index { get; }
        public bool Include { get; }

        public string DisplayMissingReplacementValue { get { return MissingReplacementValue?.ToString(); } }
        public string DisplayErrorReplacementValue { get { return ErrorReplacementValue?.ToString(); } }

        public T MissingReplacementValue { get; set; }
        public T ErrorReplacementValue { get; set; }
    }
}
