using System.Collections.Generic;
using System.IO;
using System.Linq;
using HH.EnvironmentServices.Utils;
using HH.FileSystem.IO.Compression.Converters;
using HH.FileSystem.IO.Compression.Interfaces;
using CompressionLevel = HH.FileSystem.IO.Compression.Enums.CompressionLevel;
using ZipArchiveMode = HH.FileSystem.IO.Compression.Enums.ZipArchiveMode;

namespace HH.FileSystem.IO.Compression.Implementations
{
    internal sealed class ZipArchive : IZipArchive
    {
        private readonly System.IO.Compression.ZipArchive _zipArchive;
        private readonly IDictionary<IZipArchiveEntry, System.IO.Compression.ZipArchiveEntry> _entries;

        public ZipArchive(Stream stream)
            : this(new System.IO.Compression.ZipArchive(stream))
        {

        }

        public ZipArchive(Stream stream, ZipArchiveMode zipArchiveMode)
            : this(new System.IO.Compression.ZipArchive(stream, ZipArchiveModeConverter.Convert(zipArchiveMode)))
        {

        }

        private ZipArchive(System.IO.Compression.ZipArchive zipArchive)
        {
            _zipArchive = zipArchive.ArgumentNullCheck(nameof(zipArchive));
            _entries = new Dictionary<IZipArchiveEntry, System.IO.Compression.ZipArchiveEntry>();

            SynchronizeEntries();
        }


        public IReadOnlyCollection<IZipArchiveEntry> Entries
        {
            get { return _entries.Keys.ToArray(); }
        }

        private void SynchronizeEntries()
        {
            if (_zipArchive.Mode == System.IO.Compression.ZipArchiveMode.Create) return;
            foreach (var modelEntry in _zipArchive.Entries)
            {
                CreateZipArchiveEntry(modelEntry);
            }
        }

        public IZipArchiveEntry CreateEntry(string entryName)
        {
            var modelEntry = _zipArchive.CreateEntry(entryName);
            return CreateZipArchiveEntry(modelEntry);
        }

        public IZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
        {
            var systemCompressionLevel = CompressionLevelConverter.Convert(compressionLevel);
            var modelEntry = _zipArchive.CreateEntry(entryName, systemCompressionLevel);

            return CreateZipArchiveEntry(modelEntry);
        }

        public IZipArchiveDirectory CreateDirectory(string name)
        {
            var fullPath = $"{name}/";
            CreateEntry(fullPath);
            return new ZipArchiveDirectory(this, fullPath);
        }

        public IZipArchiveDirectory GetDirectory(string name)
        {
            var expectedFullPath = $"{name}/";
            foreach (var zipArchiveEntry in _entries)
            {
                if (zipArchiveEntry.Value.FullName == expectedFullPath)
                    return new ZipArchiveDirectory(this, expectedFullPath);
            }
            throw new FileNotFoundException("Directory not found");
        }

        private IZipArchiveEntry CreateZipArchiveEntry(System.IO.Compression.ZipArchiveEntry modelEntry)
        {
            var zipArchiveEntry = new ZipArchiveEntry(modelEntry);
            _entries.Add(zipArchiveEntry, modelEntry);
            return zipArchiveEntry;
        }

        public void Dispose()
        {
            _zipArchive.Dispose();
        }
    }
}
