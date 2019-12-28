using System;
using HH.FileSystem.IO.Compression.Enums;

namespace HH.FileSystem.IO.Compression.Converters
{
    internal static class ZipArchiveModeConverter
    {
        public static System.IO.Compression.ZipArchiveMode Convert(ZipArchiveMode zipArchiveMode)
        {
            switch (zipArchiveMode)
            {
                case ZipArchiveMode.Read:
                    return System.IO.Compression.ZipArchiveMode.Read;
                case ZipArchiveMode.Create:
                    return System.IO.Compression.ZipArchiveMode.Create;
                case ZipArchiveMode.Update:
                    return System.IO.Compression.ZipArchiveMode.Update;
                default:
                    throw new ArgumentOutOfRangeException(nameof(zipArchiveMode), zipArchiveMode, null);
            }
        }
    }
}
