using System;
using System.Diagnostics;
using System.IO;
using HH.Serialization.Interfaces;

namespace HH.Serialization.Services
{
    [DebuggerNonUserCode]
    public class XmlSerializer : IXmlSerializer
    {
        private readonly System.Xml.Serialization.XmlSerializer _xmlSerializer;

        public XmlSerializer(Type t)
        {
            _xmlSerializer = new System.Xml.Serialization.XmlSerializer(t);
        }

        public void Serialize(Stream stream, object graph)
        {
            _xmlSerializer.Serialize(stream, graph);
        }

        public object ReadObject(Stream stream)
        {
            return _xmlSerializer.Deserialize(stream);
        }
    }
}
