using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.Win;
using HH.FileSystem.IO.Win.CsvReader.Implementations;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class CsvReaderLwPerformanceTests
    {
        [Test]
        public void Read_LargeFile_Reads_Correctly()
        {
            //Arrange
            const int bigFileRows = 2000000;
            const int bigFileColumns = 4;

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            WriteDelimitedFile(writer, GetRandomData(bigFileRows, bigFileColumns), c => c,
                GetRandomDataHeaders(bigFileColumns), ',');
            writer.Flush();
            stream.Position = 0;
            var reader = new StreamReader(stream);

            //Act
            var watch = new Stopwatch();
            watch.Start();

            var dataReader = new CsvReaderLw(reader, new DelimitedTextReaderConfiguration
            {
                HasHeaderRecord = true,
                SkipEmptyRecords = false,
                IgnoreBlankLines = false,
                Delimiter = ","
            });

            var rowCounter = 0;
            var entry = 0d;
            while (dataReader.Read())
            {
                for (var j = 0; j < bigFileColumns; j++)
                {
                    //Here we simulate data parsing
                    entry = dataReader.GetField<double>(j);
                }

                rowCounter++;
            }
            watch.Stop();

            //Assert
            Console.WriteLine($"Elapsed time {watch.Elapsed.Seconds}s");
            Assert.IsTrue(6 > watch.Elapsed.TotalSeconds, $"It took longer than it should've. Expected 6s, Actual: {watch.Elapsed.TotalSeconds}");
            Assert.AreEqual(2000000, rowCounter);
            Console.WriteLine(entry);
        }

        #region Helpers

        private static IEnumerable<string> GetRandomDataHeaders(int columns)
        {
            var row = new string[columns];
            for (var i = 0; i < columns; i++)
            {
                row[i] = $"Field {i}";
            }
            return row;
        }

        private static IEnumerable<object[]> GetRandomData(int rows, int columns)
        {
            var row = new object[columns];
            var random = new Random(42);

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    row[j] = random.NextDouble();
                }
                yield return row;
            }
        }

        public void WriteDelimitedFile<T>(TextWriter textWriter, IEnumerable<T> values, Func<T, IEnumerable<object>> rowFactory, IEnumerable<string> headers, char separator)
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

        #endregion 


    }

  
}
