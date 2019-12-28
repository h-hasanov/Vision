using System.IO;
using HH.FileSystem.IO.Compression.Enums;
using HH.FileSystem.IO.Compression.Factories.Interfaces;
using HH.FileSystem.IO.Compression.Implementations;
using HH.FileSystem.IO.Compression.Interfaces;

namespace HH.FileSystem.IO.Compression.Factories.Implementations
{
    public sealed class ZipArchiveFactory : IZipArchiveFactory
    {
        public IZipArchive CreateZipArchive(Stream stream)
        {
            return new ZipArchive(stream);
        }

        public IZipArchive CreateZipArchive(Stream stream, ZipArchiveMode zipArchiveMode)
        {
            return new ZipArchive(stream, zipArchiveMode);
        }
    }
}
