using System.IO;

namespace HH.FileSystem.IO.StreamServices.Factories.Interfaces
{
    public interface IStreamReaderFactory
    {
        StreamReader CreateStreamReader(Stream stream);
    }
}
