using System;

namespace HH.FileSystem.IO.CsvReader.Interfaces
{
    public interface ICsvReader : IDisposable
    {
        string[] Headers { get; }
        string[] CurrentRecord { get; }
        int FieldCount { get; }

        string GetField(int index);
        T GetField<T>(int index);
        bool TryGetField<T>(int index, out T output);

        bool Read();
    }
}
