using System.IO;

namespace HH.FileSystem.IO.StreamServices.Factories.Interfaces
{
    public interface IStreamWriterFactory
    {
        StreamWriter CreateStreamWriter(Stream stream);
    }
}
