using System.IO;
using HH.FileSystem.IO.Compression.Enums;
using HH.FileSystem.IO.Compression.Interfaces;

namespace HH.FileSystem.IO.Compression.Factories.Interfaces
{
    public interface IZipArchiveFactory
    {
        IZipArchive CreateZipArchive(Stream stream);
        IZipArchive CreateZipArchive(Stream stream, ZipArchiveMode zipArchiveMode);
    }
}
