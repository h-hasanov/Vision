using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HH.FileSystem.IO.Interfaces;

namespace HH.FileSystem.IO.FileWriter
{
    [DebuggerNonUserCode]
    public class DelimitedTextWriter : IDelimitedTextWriter
    {
        /// <summary>
        /// Writes delimited files.
        /// </summary>
        /// <param name="textWriter"></param>
        /// <param name="values"></param>
        /// <param name="rowFactory"></param>
        /// <param name="headers"></param>
        /// <param name="separator"></param>
        public void WriteDelimitedFile<T>(TextWriter textWriter, IEnumerable<T> values, Func<T, IEnumerable<object>> rowFactory, IEnumerable<string> headers, char separator)
        {
            try
            {
                var builder = new StringBuilder();
                AddRow(headers, builder, separator);

                builder.AppendLine();

                textWriter.Write(builder.ToString());
                builder = new StringBuilder();

                foreach (var row in values)
                {
                    AddRow(rowFactory(row), builder, separator);

                    builder.AppendLine();
                    textWriter.Write(builder);

                    builder.Clear();
                }
                textWriter.Dispose();
            }
            catch (Exception)
            {
                textWriter?.Dispose();
            }
        }

        private static void AddRow<T>(IEnumerable<T> values, StringBuilder builder, char separator)
        {
            var enumerableValues = values as T[] ?? values.ToArray();
            var length = enumerableValues.Length;
            for (var i = 0; i < length - 1; i++)
            {
                builder.Append(enumerableValues[i]).Append(separator);
            }
            builder.Append(enumerableValues[length - 1]);
        }
    }
}
