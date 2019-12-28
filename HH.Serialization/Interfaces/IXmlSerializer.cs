using System.IO;

namespace HH.Serialization.Interfaces
{
    public interface IXmlSerializer
    {
        void Serialize(Stream stream, object graph);
        object ReadObject(Stream stream);
    }
}
