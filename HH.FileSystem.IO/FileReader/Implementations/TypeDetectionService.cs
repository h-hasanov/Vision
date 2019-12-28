using System;
using System.Collections.Generic;
using System.Linq;
using HH.Extensions.Types;
using HH.FileSystem.IO.FileReader.Interfaces;
using static HH.Extensions.Types.TypeExtensions;

namespace HH.FileSystem.IO.FileReader.Implementations
{
    public sealed class TypeDetectionService : ITypeDetectionService
    {
        private readonly IList<Type> _supportedTypes = new[] { BoolType, Int32Type, DoubleType, DateTimeType, StringType };

        public Type[] DetectTypes(IEnumerable<string[]> data)
        {
            var columns = -1;
            var fieldTypes = new Type[0];

            foreach (var dataRow in data)
            {
                if (columns == -1)
                {
                    columns = dataRow.Length;
                    fieldTypes = Enumerable.Repeat(_supportedTypes.First(), columns).ToArray();
                }

                for (var j = 0; j < columns; j++)
                {
                    var cellEntry = dataRow[j];
                    while (!fieldTypes[j].CanConvertFromString(cellEntry))
                    {
                        var currentIndex = _supportedTypes.IndexOf(fieldTypes[j]);
                        fieldTypes[j] = _supportedTypes[currentIndex + 1];
                    }
                }
            }

            return fieldTypes;
        }
    }
}
