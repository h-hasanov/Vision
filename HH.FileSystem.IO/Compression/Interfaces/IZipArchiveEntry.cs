using System.IO;

namespace HH.FileSystem.IO.Compression.Interfaces
{
    public interface IZipArchiveEntry
    {
        Stream Open();
        string Name { get; }
        string FullName { get; }
    }
}
