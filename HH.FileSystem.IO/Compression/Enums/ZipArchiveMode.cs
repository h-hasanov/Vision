namespace HH.FileSystem.IO.Compression.Enums
{
    public enum ZipArchiveMode : byte
    {
        //
        // Summary:
        //     Only reading archive entries is permitted.
        Read = 0,
        //
        // Summary:
        //     Only creating new archive entries is permitted.
        Create = 1,
        //
        // Summary:
        //     Both read and write operations are permitted for archive entries.
        Update = 2
    }
}
