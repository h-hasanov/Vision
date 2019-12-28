using System.IO;
using HH.EnvironmentServices.Utils;
using HH.FileSystem.IO.Compression.Interfaces;

namespace HH.FileSystem.IO.Compression.Implementations
{
    internal sealed class ZipArchiveEntry : IZipArchiveEntry
    {
        private readonly System.IO.Compression.ZipArchiveEntry _zipArchiveEntry;

        public ZipArchiveEntry(System.IO.Compression.ZipArchiveEntry zipArchiveEntry)
        {
            _zipArchiveEntry = zipArchiveEntry.ArgumentNullCheck(nameof(zipArchiveEntry));
        }

        public string Name { get { return _zipArchiveEntry.Name; } }
        public string FullName { get { return _zipArchiveEntry.FullName; } }

        public Stream Open()
        {
            return _zipArchiveEntry.Open();
        }
    }
}
