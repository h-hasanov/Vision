using System;
using System.Collections.Generic;

namespace HH.FileSystem.IO.FileReader.Interfaces
{
    public interface ITypeDetectionService
    {
        Type[] DetectTypes(IEnumerable<string[]> data);
    }
}
