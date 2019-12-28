using System.Collections.Generic;
using HH.FileSystem.IO.Compression.Enums;

namespace HH.FileSystem.IO.Compression.Interfaces
{
    public interface IZipArchiveDirectory
    {
        IZipArchiveEntry CreateEntry(string entryName);
        IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel);

        IZipArchiveDirectory CreateDirectory(string directoryName);

        IReadOnlyCollection<IZipArchiveEntry> Entries { get; }
        IZipArchiveDirectory GetDirectory(string name);
    }
}
