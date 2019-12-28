using System;
using System.Diagnostics;
using System.IO;
using HH.Serialization.Interfaces;

namespace HH.Serialization.Services
{
    [DebuggerNonUserCode]
    internal sealed class DataContractSerializer : IDataContractSerializer
    {
        private readonly System.Runtime.Serialization.DataContractSerializer _dataContractSerializer;

        public DataContractSerializer(Type t)
        {
            _dataContractSerializer = new System.Runtime.Serialization.DataContractSerializer(t);
        }

        public void WriteObject(Stream stream, object graph)
        {
            _dataContractSerializer.WriteObject(stream, graph);
        }

        public object ReadObject(Stream stream)
        {
            return _dataContractSerializer.ReadObject(stream);
        }
    }
}
