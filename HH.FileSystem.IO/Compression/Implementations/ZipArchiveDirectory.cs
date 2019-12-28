using System.Collections.Generic;
using HH.EnvironmentServices.Utils;
using HH.FileSystem.IO.Compression.Enums;
using HH.FileSystem.IO.Compression.Interfaces;

namespace HH.FileSystem.IO.Compression.Implementations
{
    internal sealed class ZipArchiveDirectory : IZipArchiveDirectory
    {
        private readonly string _fullPath;
        private readonly IZipArchive _zipArchive;

        public ZipArchiveDirectory(IZipArchive zipArchive, string fullPath)
        {
            _fullPath = fullPath;
            _zipArchive = zipArchive.ArgumentNullCheck(nameof(zipArchive));
        }

        public IReadOnlyCollection<IZipArchiveEntry> Entries { get { return GetEntries(); } }

        private IReadOnlyCollection<IZipArchiveEntry> GetEntries()
        {
            var entries = new List<IZipArchiveEntry>();
            foreach (var zipArchiveEntry in _zipArchive.Entries)
            {
                if (zipArchiveEntry.Name != string.Empty &&
                    zipArchiveEntry.FullName == $"{_fullPath}{zipArchiveEntry.Name}")
                    entries.Add(zipArchiveEntry);
            }
            return entries;
        }

        public IZipArchiveEntry CreateEntry(string entryName)
        {
            return _zipArchive.CreateEntry($"{_fullPath}{entryName}");
        }

        public IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
        {
            return _zipArchive.CreateEntry($"{_fullPath}{entryName}", compressionLevel);
        }

        public IZipArchiveDirectory GetDirectory(string name)
        {
            return _zipArchive.GetDirectory($"{_fullPath}{name}");
        }

        public IZipArchiveDirectory CreateDirectory(string directoryName)
        {
            return _zipArchive.CreateDirectory($"{_fullPath}{directoryName}");
        }
    }
}
