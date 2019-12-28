using System.IO;

namespace HH.Serialization.Interfaces
{
    public interface IDataContractSerializer
    {
        void WriteObject(Stream stream, object graph);
        object ReadObject(Stream stream);
    }
}
