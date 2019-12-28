using System;
using HH.FileSystem.IO.Compression.Enums;

namespace HH.FileSystem.IO.Compression.Converters
{
    internal static class CompressionLevelConverter
    {
        public static System.IO.Compression.CompressionLevel Convert(CompressionLevel compressionLevel)
        {
            switch (compressionLevel)
            {
                case CompressionLevel.Optimal:
                    return System.IO.Compression.CompressionLevel.Optimal;

                case CompressionLevel.Fastest:
                    return System.IO.Compression.CompressionLevel.Fastest;

                case CompressionLevel.NoCompression:
                    return System.IO.Compression.CompressionLevel.NoCompression;

                default:
                    throw new ArgumentOutOfRangeException(nameof(compressionLevel), compressionLevel, null);
            }
        }
    }
}
