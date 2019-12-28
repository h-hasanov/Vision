using System;

namespace HH.Serialization.Interfaces
{
    public interface IDataContractSerializerFactory
    {
        IDataContractSerializer CreateDataContractSerializer(Type t);
    }
}
