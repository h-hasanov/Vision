using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using HH.FileSystem.IO.FileWriter;
using HH.FileSystem.IO.Interfaces;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileWriter
{
    [TestFixture]
    internal sealed class DelimitedTextWriterTests
    {
        private IDelimitedTextWriter _delimitedTextWriter;

        [SetUp]
        public void Setup()
        {
            _delimitedTextWriter = new DelimitedTextWriter();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [Category("CI")]
        public void WriteDelimitedFile_WithSeparator_WritesCorrectly(int type)
        {
            //Arrange
            var separator = default(char);
            switch (type)
            {
                case 0:
                    separator = ',';
                    break;
                case 1:
                    separator = ';';
                    break;
                case 2:
                    separator = '\t';
                    break;
            }
            var headers = GetHeaders();
            var values = GetValues();

            //Act
            _delimitedTextWriter.WriteDelimitedFile(new StreamWriter(new MemoryStream()), values, ConvertObjects, headers, separator);
        }

        private static IEnumerable<object> ConvertObjects(IEnumerable<object> values)
        {
            return values;
        }

        [Test]
        [Category("Performance")]
        public void Writing_Speed_Test()
        {
            //Arrange
            const char separator = ',';
            const int rows = 2000000;
            const int columns = 10;
            var headers = GetHeaders(columns);
            var values = GetValues(rows, columns);

            //Act
            var watch = new Stopwatch();
            watch.Start();
            _delimitedTextWriter.WriteDelimitedFile(new StreamWriter(new MemoryStream()), values, ConvertObjects, headers, separator);
            watch.Stop();

            //Assert
            Assert.IsTrue(watch.Elapsed.TotalSeconds < 10, "Elapsed time more than the allowed");
        }

        #region Helpers

        private static IEnumerable<string> GetHeaders(int columns)
        {
            var headers = new List<string>();
            for (var i = 0; i < columns; i++)
            {
                headers.Add((i + 1).ToString(CultureInfo.InvariantCulture));
            }
            return headers;
        }

        private static IEnumerable<IEnumerable<object>> GetValues(int rows, int columns)
        {
            var random = new Random();

            for (var i = 0; i < rows; i++)
            {
                var newRow = new List<object>();
                for (var j = 0; j < columns; j++)
                {
                    newRow.Add(random.Next());
                }
                yield return newRow;
            }
        }

        private static IEnumerable<string> GetHeaders()
        {
            return new[] { "Name", "Surname", "Age" };
        }

        private IEnumerable<IEnumerable<object>> GetValues()
        {
            return new List<List<object>>
            {
                new List<object>(new[] {"Hasan", "Hasanov", "23"}),
                new List<object>(new[] {"Zeyneb", "Kadirova"}),
                new List<object>(new[] {" ", "Yakubova", "14"}),
                new List<object>(new[]{""}),
                new List<object>(new[] {"Jaysun", "Yakubov", "13"}),
            };
        }

        #endregion
    }
}
