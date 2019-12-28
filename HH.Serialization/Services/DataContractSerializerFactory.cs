using System;
using System.Diagnostics;
using HH.Serialization.Interfaces;

namespace HH.Serialization.Services
{
    [DebuggerNonUserCode]
    public sealed class DataContractSerializerFactory : IDataContractSerializerFactory
    {
        public IDataContractSerializer CreateDataContractSerializer(Type t)
        {
            return new DataContractSerializer(t);
        }
    }
}
