using System.IO;
using HH.FileSystem.IO.StreamServices.Factories.Interfaces;

namespace HH.FileSystem.IO.StreamServices.Factories.Implementations
{
    public sealed class StreamWriterFactory : IStreamWriterFactory
    {
        public StreamWriter CreateStreamWriter(Stream stream)
        {
            return new StreamWriter(stream);
        }
    }
}
