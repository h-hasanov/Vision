using System;
using System.Collections.Generic;
using System.IO;

namespace HH.FileSystem.IO.Interfaces
{
    public interface IDelimitedTextWriter
    {
        void WriteDelimitedFile<T>(TextWriter textWriter, IEnumerable<T> values, Func<T, IEnumerable<object>> rowFactory, IEnumerable<string> headers, char separator);
    }
}
