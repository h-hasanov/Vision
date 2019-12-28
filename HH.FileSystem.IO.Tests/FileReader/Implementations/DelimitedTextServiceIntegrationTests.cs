using System;
using System.Globalization;
using System.IO;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.FileSystem.IO.Win;
using HH.FileSystem.IO.Win.CsvReader.Implementations;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class DelimitedTextServiceIntegrationTests
    {
        private AutoMocker _autoMocker;
        private readonly IDelimitedTextService _actualService = new DelimitedTextService(new CsvReaderFactoryLw());


        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void DetectFileDefinition_With_Headers_Detects_FileDefinition_Correctly()
        {
            //Arrange
            var expectedTypes = new[] { typeof(double), typeof(DateTime), typeof(int), typeof(string), typeof(bool) };
            var expectedHeaderNames = new[]
            {"column_double", "column_dateTime", "column_integer", "column_string", "column_bool"};

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("column_double,column_dateTime,column_integer,column_string,column_bool");
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("0.219666314,02 / 07 / 2016,2,b,FALSE");
            writer.WriteLine("0.174173197,03 / 07 / 2016,3,c,TRUE");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            //Act
            var result = _actualService.DetectDataFieldDefinitions(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = true });

            //Assert
            Assert.AreEqual(expectedTypes.Length, result.Length);
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], result[i].DataType);
                Assert.AreEqual(expectedHeaderNames[i], result[i].Name);
                Assert.AreEqual(i, result[i].Index);
                Assert.IsTrue(result[i].Include);
            }
        }

        [Test]
        public void DetectFileDefinition_With_Duplicate_Headers_Detects_FileDefinition_Correctly()
        {
            //Arrange
            var expectedTypes = new[] { typeof(double), typeof(DateTime), typeof(int), typeof(string), typeof(bool) };
            var expectedHeaderNames = new[]
            {"column_double", "column_dateTime", "column_double1", "column_string", "column_bool"};

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("column_double,column_dateTime,column_double,column_string,column_bool");
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("0.219666314,02 / 07 / 2016,2,b,FALSE");
            writer.WriteLine("0.174173197,03 / 07 / 2016,3,c,TRUE");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            //Act
            var result = _actualService.DetectDataFieldDefinitions(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = true });

            //Assert
            Assert.AreEqual(expectedTypes.Length, result.Length);
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], result[i].DataType);
                Assert.AreEqual(expectedHeaderNames[i], result[i].Name);
                Assert.AreEqual(i, result[i].Index);
                Assert.IsTrue(result[i].Include);
            }
        }

        [Test]
        public void DetectFileDefinition_Without_Headers_Detects_FileDefinition_Correctly()
        {
            //Arrange
            var expectedTypes = new[] { typeof(double), typeof(DateTime), typeof(int), typeof(string), typeof(bool) };
            var expectedHeaderNames = new[] { "Field 1", "Field 2", "Field 3", "Field 4", "Field 5" };
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("0.219666314,02 / 07 / 2016,2,b,FALSE");
            writer.WriteLine("0.174173197,03 / 07 / 2016,3,c,TRUE");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            //Act
            var result = _actualService.DetectDataFieldDefinitions(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false });

            //Assert
            Assert.AreEqual(expectedTypes.Length, result.Length);
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], result[i].DataType);
                Assert.AreEqual(expectedHeaderNames[i], result[i].Name);
                Assert.AreEqual(i, result[i].Index);
                Assert.IsTrue(result[i].Include);
            }
        }

        [Test]
        public void DetectFileDefinition_With_IncompleteFile_Detects_FileDefinition_Correctly()
        {
            //Arrange
            var expectedTypes = new[]
            {typeof (string), typeof (string), typeof (string), typeof (string), typeof (string)};
            var expectedHeaderNames = new[]
            {"column_double", "column_dateTime", "column_integer", "column_string", "column_bool"};

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("column_double,column_dateTime,column_integer,column_string,column_bool");
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("a, b, ,, k");
            writer.WriteLine(" , ,c,,");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            //Act
            var result = _actualService.DetectDataFieldDefinitions(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = true });

            //Assert
            Assert.AreEqual(expectedTypes.Length, result.Length);
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], result[i].DataType);
                Assert.AreEqual(expectedHeaderNames[i], result[i].Name);
                Assert.AreEqual(i, result[i].Index);
                Assert.IsTrue(result[i].Include);
            }
        }

        [Test]
        public void DetectFileDefinition_With_Headers_And_German_Local_Detects_FileDefinition_Correctly()
        {
            //Arrange
            var expectedTypes = new[] { typeof(int), typeof(string), typeof(string), typeof(double) };
            var expectedHeaderNames = new[]
            {"Year", "Make", "Model", "Length"};

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("Year;Make;Model;Length");
            writer.WriteLine("1997;Ford;E350;2,34");
            writer.WriteLine("2000;Mercury;Cougar;2,38");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            //Act
            var result = _actualService.DetectDataFieldDefinitions(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = true, Delimiter = ";", CultureInfo = new CultureInfo("de-DE") });

            //Assert
            Assert.AreEqual(expectedTypes.Length, result.Length);
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], result[i].DataType);
                Assert.AreEqual(expectedHeaderNames[i], result[i].Name);
                Assert.AreEqual(i, result[i].Index);
                Assert.IsTrue(result[i].Include);
            }
        }

        [Test]
        public void CreateDataReader_Reads_Rows_Correctly()
        {
            //Arrange
            var dataFieldDefinitionFactory = new DataFieldDefinitionFactory();
            var expectedTypes = new[] { typeof(double), typeof(DateTime), typeof(int), typeof(string), typeof(bool) };
            var expectedHeaderNames = new[]
            {"column_double", "column_dateTime", "column_integer", "column_string", "column_bool"};
            var dataFieldDefinitions = new IDataFieldDefinition[expectedTypes.Length];
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                dataFieldDefinitions[i] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedTypes[i],
                    expectedHeaderNames[i], i);
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("column_double,column_dateTime,column_integer,column_string,column_bool");
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("0.219666314,02 / 07 / 2016,2,b,FALSE");
            writer.WriteLine("0.174173197,03 / 07 / 2016,3,c,TRUE");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            var expectedValues = new[]
            {
                new object[] {0.126988379, 0.219666314, 0.174173197, 0.211730396, 0.5758083},
                new object[]
                {
                    DateTime.Parse("01/07/2016"), DateTime.Parse("02/07/2016"), DateTime.Parse("03/07/2016"),
                    DateTime.Parse("04/07/2016"), DateTime.Parse("05/07/2016")
                },
                new object[] {1, 2, 3, 750000, 5},
                new object[] {"a", "b", "c", "d", "e"},
                new object[] {true, false, true, false, true}
            };

            //Act
            var result = _actualService.CreateDataReader(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = true }, dataFieldDefinitions);

            //Assert
            var rowIndex = 0;
            while (result.Read())
            {
                Assert.AreEqual(expectedValues[0][rowIndex], result.GetField<double>(0));
                Assert.AreEqual(expectedValues[1][rowIndex], result.GetField<DateTime>(1));
                Assert.AreEqual(expectedValues[2][rowIndex], result.GetField<int>(2));
                Assert.AreEqual(expectedValues[3][rowIndex], result.GetField<string>(3));
                Assert.AreEqual(expectedValues[4][rowIndex], result.GetField<bool>(4));
                rowIndex++;
            }
        }

        [Test]
        public void CreateDataReader_Without_Headers_Reads_Rows_Correctly()
        {
            //Arrange
            var dataFieldDefinitionFactory = new DataFieldDefinitionFactory();
            var expectedTypes = new[] { typeof(double), typeof(DateTime), typeof(int), typeof(string), typeof(bool) };
            var expectedHeaderNames = new[] { "Field 1", "Field 2", "Field 3", "Field 4", "Field 5" };
            var dataFieldDefinitions = new IDataFieldDefinition[expectedTypes.Length];
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                dataFieldDefinitions[i] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedTypes[i],
                    expectedHeaderNames[i], i);
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("0.219666314,02 / 07 / 2016,2,b,FALSE");
            writer.WriteLine("0.174173197,03 / 07 / 2016,3,c,TRUE");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);

            var expectedValues = new[]
            {
                new object[] {0.126988379, 0.219666314, 0.174173197, 0.211730396, 0.5758083},
                new object[]
                {
                    DateTime.Parse("01/07/2016"), DateTime.Parse("02/07/2016"), DateTime.Parse("03/07/2016"),
                    DateTime.Parse("04/07/2016"), DateTime.Parse("05/07/2016")
                },
                new object[] {1, 2, 3, 750000, 5},
                new object[] {"a", "b", "c", "d", "e"},
                new object[] {true, false, true, false, true}
            };

            //Act
            var result = _actualService.CreateDataReader(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }, dataFieldDefinitions);

            //Assert
            var rowIndex = 0;
            while (result.Read())
            {
                Assert.AreEqual(expectedValues[0][rowIndex], result.GetField<double>(0));
                Assert.AreEqual(expectedValues[1][rowIndex], result.GetField<DateTime>(1));
                Assert.AreEqual(expectedValues[2][rowIndex], result.GetField<int>(2));
                Assert.AreEqual(expectedValues[3][rowIndex], result.GetField<string>(3));
                Assert.AreEqual(expectedValues[4][rowIndex], result.GetField<bool>(4));
                rowIndex++;
            }
        }

        [Test]
        public void CreateDataReader_With_Missing_And_Invalid_Data_Reads_Rows_Correctly()
        {
            //Arrange
            var dataFieldDefinitionFactory = new DataFieldDefinitionFactory();
            var expectedTypes = new[] { typeof(double), typeof(DateTime), typeof(int), typeof(string), typeof(bool) };
            var expectedHeaderNames = new[]
            {"column_double", "column_dateTime", "column_integer", "column_string", "column_bool"};
            var dataFieldDefinitions = new IDataFieldDefinition[expectedTypes.Length];

            dataFieldDefinitions[0] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedHeaderNames[0], 0,
                true, -3.1, 4.1);

            dataFieldDefinitions[1] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedHeaderNames[1], 1,
                true, new DateTime(2016, 10, 3), new DateTime(2016, 11, 4));

            dataFieldDefinitions[2] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedHeaderNames[2], 2,
                true, -1, 10);

            dataFieldDefinitions[3] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedHeaderNames[3], 3,
                true, "missing", "error");

            dataFieldDefinitions[4] = dataFieldDefinitionFactory.CreateDataFieldDefinition(expectedHeaderNames[4], 4,
                true, false);

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("column_double,column_dateTime,column_integer,column_string,column_bool");
            writer.WriteLine("0.126988379,01 / 07 / 2016,1,a,TRUE");
            writer.WriteLine("a, b, ,, k");
            writer.WriteLine(" , ,c,,");
            writer.WriteLine("0.211730396,04 / 07 / 2016,750000,d,FALSE");
            writer.WriteLine("0.5758083,05 / 07 / 2016,5,e,TRUE");
            writer.Flush();
            stream.Position = 0;

            var reader = new StreamReader(stream);


            var expectedValues = new[]
            {
                new object[] {0.126988379, 4.1, -3.1, 0.211730396, 0.5758083},
                new object[]
                {
                    DateTime.Parse("01/07/2016"), new DateTime(2016, 11, 4), new DateTime(2016, 10, 3),
                    DateTime.Parse("04/07/2016"), DateTime.Parse("05/07/2016")
                },
                new object[] {1, -1, 10, 750000, 5},
                new object[] {"a", "missing", "missing", "d", "e"},
                new object[] {true, false, false, false, true}
            };

            //Act
            var result = _actualService.CreateDataReader(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = true }, dataFieldDefinitions);

            //Assert
            var rowIndex = 0;
            while (result.Read())
            {
                Assert.AreEqual(expectedValues[0][rowIndex], result.GetField<double>(0));
                Assert.AreEqual(expectedValues[1][rowIndex], result.GetField<DateTime>(1));
                Assert.AreEqual(expectedValues[2][rowIndex], result.GetField<int>(2));
                Assert.AreEqual(expectedValues[3][rowIndex], result.GetField<string>(3));
                Assert.AreEqual(expectedValues[4][rowIndex], result.GetField<bool>(4));
                rowIndex++;
            }
        }
    }
}
