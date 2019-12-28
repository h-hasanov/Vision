using System.IO;
using HH.FileSystem.IO.StreamServices.Factories.Interfaces;

namespace HH.FileSystem.IO.StreamServices.Factories.Implementations
{
    public sealed class StreamReaderFactory : IStreamReaderFactory
    {
        public StreamReader CreateStreamReader(Stream stream)
        {
            return new StreamReader(stream);
        }
    }
}
