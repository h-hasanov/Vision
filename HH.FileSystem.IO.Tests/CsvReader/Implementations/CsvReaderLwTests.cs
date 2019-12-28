using System.IO;
using HH.FileSystem.IO.Enums;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.Win.CsvReader.Implementations;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.CsvReader.Implementations
{
    [TestFixture]
    internal sealed class CsvReaderLwTests
    {
        private AutoMocker _autoMocker;

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

        //[Test]
        //public void Ctor_Sets_Properties()
        //{
        //    //Arrange
        //    var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = true };
        //    var textReader = _autoMocker.Mock<TextReader>();

        //    //Act
        //    var result = new CsvReaderLw(textReader, fileConfiguration);

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(fileConfiguration.Delimiter, result.CurrentRecord.Delimiter);
        //    Assert.AreEqual(fileConfiguration.HasHeaderRecord, result.Configuration.HasHeaderRecord);
        //    Assert.AreEqual(fileConfiguration.CultureInfo, result.Configuration.CultureInfo);
        //    Assert.AreEqual(fileConfiguration.SkipEmptyRecords, result.Configuration.SkipEmptyRecords);
        //    Assert.AreEqual(fileConfiguration.IgnoreBlankLines, result.Configuration.IgnoreBlankLines);
        //    Assert.AreEqual(fileConfiguration.TextQualifierType.ToChar(), result.Configuration.Quote);
        //}

        [Test]
        public void CsvReader_With_DoubleQuotes_Reads_Correctly()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("one,\"two\",three");
            writer.WriteLine("four,\"\"\"five,and a half\"\"\",six");
            writer.Flush();
            stream.Position = 0;
            var reader = new StreamReader(stream);

            var csvReader = new CsvReaderLw(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false });

            var record = csvReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual("one", csvReader.CurrentRecord[0]);
            Assert.AreEqual("two", csvReader.CurrentRecord[1]);
            Assert.AreEqual("three", csvReader.CurrentRecord[2]);

            record = csvReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual("four", csvReader.CurrentRecord[0]);
            Assert.AreEqual("\"five,and a half\"", csvReader.CurrentRecord[1]);
            Assert.AreEqual("six", csvReader.CurrentRecord[2]);

            record = csvReader.Read();
            Assert.IsFalse(record);
        }

        [Test]
        public void CsvReader_With_SingleQuotes_Reads_Correctly()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("one,\"two\",three");
            writer.WriteLine("four,\"'five, and a half\'\",six");
            writer.Flush();
            stream.Position = 0;
            var reader = new StreamReader(stream);

            var csvReader = new CsvReaderLw(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false });

            var record = csvReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual("one", csvReader.CurrentRecord[0]);
            Assert.AreEqual("two", csvReader.CurrentRecord[1]);
            Assert.AreEqual("three", csvReader.CurrentRecord[2]);

            record = csvReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual("four", csvReader.CurrentRecord[0]);
            Assert.AreEqual("'five, and a half'", csvReader.CurrentRecord[1]);
            Assert.AreEqual("six", csvReader.CurrentRecord[2]);

            record = csvReader.Read();
            Assert.IsFalse(record);
        }

        [Test]
        public void CsvReader_With_EmptySpaces_Reads_Correctly()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine(" one , \"two three\" , four ");
            writer.WriteLine(" \" five \"\" six \"\" seven \" ");
            writer.Flush();
            stream.Position = 0;
            var reader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false });

            var record = dataReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual(" one ", dataReader.CurrentRecord[0]);
            Assert.AreEqual(" \"two three\" ", dataReader.CurrentRecord[1]);
            Assert.AreEqual(" four ", dataReader.CurrentRecord[2]);

            record = dataReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual(" \" five \"\" six \"\" seven \" ", dataReader.CurrentRecord[0]);

            record = dataReader.Read();
            Assert.IsFalse(record);
        }


        [Test]
        public void CsvReader_ParseEmpty_Test()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                var record = dataReader.Read();
                Assert.IsFalse(record);
            }
        }

        [Test]
        public void CsvReader_ParseCrOnly_Test()
        {
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var dataReader = new CsvReaderLw(reader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                writer.Write("\r");
                writer.Flush();
                stream.Position = 0;

                var record = dataReader.Read();
                Assert.IsTrue(record);
            }
        }

        [Test]
        public void CsvReader_Parse_First_Field_Is_Empty_Quoted_Test()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                streamWriter.WriteLine("\"\",\"two\",\"three\"");
                streamWriter.Flush();
                memoryStream.Position = 0;

                var record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(3, dataReader.CurrentRecord.Length);
                Assert.AreEqual("", dataReader.CurrentRecord[0]);
                Assert.AreEqual("two", dataReader.CurrentRecord[1]);
                Assert.AreEqual("three", dataReader.CurrentRecord[2]);
            }
        }

        [Test]
        public void CsvReader_Parse_Last_Field_IsEmpty_Quoted_Test()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                streamWriter.WriteLine("\"one\",\"two\",\"\"");
                streamWriter.Flush();
                memoryStream.Position = 0;

                var record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(3, dataReader.CurrentRecord.Length);
                Assert.AreEqual("one", dataReader.CurrentRecord[0]);
                Assert.AreEqual("two", dataReader.CurrentRecord[1]);
                Assert.AreEqual("", dataReader.CurrentRecord[2]);
            }
        }

        [Test]
        public void CsvReader_Parse_Quote_Only_Quoted_Field_Test()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                streamWriter.WriteLine("\"\"\"\",\"two\",\"three\"");
                streamWriter.Flush();
                memoryStream.Position = 0;

                var record = dataReader.Read();
                Assert.IsNotNull(record);
                Assert.AreEqual(3, dataReader.CurrentRecord.Length);
                Assert.AreEqual("\"", dataReader.CurrentRecord[0]);
                Assert.AreEqual("two", dataReader.CurrentRecord[1]);
                Assert.AreEqual("three", dataReader.CurrentRecord[2]);
            }
        }

        [Test]
        public void CsvReader_Parse_Records_With_Only_One_Field()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                streamWriter.WriteLine("row one");
                streamWriter.WriteLine("row two");
                streamWriter.WriteLine("row three");
                streamWriter.Flush();
                memoryStream.Position = 0;

                var record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(1, dataReader.CurrentRecord.Length);
                Assert.AreEqual("row one", dataReader.CurrentRecord[0]);

                record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(1, dataReader.CurrentRecord.Length);
                Assert.AreEqual("row two", dataReader.CurrentRecord[0]);

                record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(1, dataReader.CurrentRecord.Length);
                Assert.AreEqual("row three", dataReader.CurrentRecord[0]);
            }
        }

        [Test]
        public void CsvReader_Parse_Record_Where_Only_Carriage_ReturnLine_Ending_Is_Used()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                streamWriter.Write("one,two\r");
                streamWriter.Write("three,four\r");
                streamWriter.Write("five,six\r");
                streamWriter.Flush();
                memoryStream.Position = 0;

                var record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(2, dataReader.CurrentRecord.Length);
                Assert.AreEqual("one", dataReader.CurrentRecord[0]);
                Assert.AreEqual("two", dataReader.CurrentRecord[1]);

                record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(2, dataReader.CurrentRecord.Length);
                Assert.AreEqual("three", dataReader.CurrentRecord[0]);
                Assert.AreEqual("four", dataReader.CurrentRecord[1]);

                record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(2, dataReader.CurrentRecord.Length);
                Assert.AreEqual("five", dataReader.CurrentRecord[0]);
                Assert.AreEqual("six", dataReader.CurrentRecord[1]);
            }
        }

        [Test]
        public void CsvReader_Parse_Record_Where_Only_LineFeed_LineEnding_Is_Used()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamReader = new StreamReader(memoryStream))
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false }))
            {
                streamWriter.Write("one,two\n");
                streamWriter.Write("three,four\n");
                streamWriter.Write("five,six\n");
                streamWriter.Flush();
                memoryStream.Position = 0;

                var record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(2, dataReader.CurrentRecord.Length);
                Assert.AreEqual("one", dataReader.CurrentRecord[0]);
                Assert.AreEqual("two", dataReader.CurrentRecord[1]);

                record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(2, dataReader.CurrentRecord.Length);
                Assert.AreEqual("three", dataReader.CurrentRecord[0]);
                Assert.AreEqual("four", dataReader.CurrentRecord[1]);

                record = dataReader.Read();
                Assert.IsTrue(record);
                Assert.AreEqual(2, dataReader.CurrentRecord.Length);
                Assert.AreEqual("five", dataReader.CurrentRecord[0]);
                Assert.AreEqual("six", dataReader.CurrentRecord[1]);
            }
        }

        [Test]
        public void CsvReader_Parse_Commented_Out_LineWith_Comments_On()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("one,two,three");
            writer.WriteLine("#four,five,six");
            writer.WriteLine("seven,eight,nine");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                    AllowComments = true,
                    IgnoreBlankLines = true
                });

            dataReader.Read();
            Assert.AreEqual("one", dataReader.CurrentRecord[0]);
            Assert.AreEqual("two", dataReader.CurrentRecord[1]);
            Assert.AreEqual("three", dataReader.CurrentRecord[2]);

            dataReader.Read();
            Assert.AreEqual("seven", dataReader.CurrentRecord[0]);
            Assert.AreEqual("eight", dataReader.CurrentRecord[1]);
            Assert.AreEqual("nine", dataReader.CurrentRecord[2]);
        }


        [Test]
        public void CsvReader_Parse_Commented_Out_LineWith_Comments_Off()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("one,two,three");
            writer.WriteLine("#four,five,six");
            writer.WriteLine("seven,eight,nine");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            //AllowComments=False
            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false, AllowComments = false });

            dataReader.Read();
            Assert.AreEqual("one", dataReader.CurrentRecord[0]);
            Assert.AreEqual("two", dataReader.CurrentRecord[1]);
            Assert.AreEqual("three", dataReader.CurrentRecord[2]);

            dataReader.Read();
            Assert.AreEqual("#four", dataReader.CurrentRecord[0]);
            Assert.AreEqual("five", dataReader.CurrentRecord[1]);
            Assert.AreEqual("six", dataReader.CurrentRecord[2]);

            dataReader.Read();
            Assert.AreEqual("seven", dataReader.CurrentRecord[0]);
            Assert.AreEqual("eight", dataReader.CurrentRecord[1]);
            Assert.AreEqual("nine", dataReader.CurrentRecord[2]);
        }

        [Test]
        public void CsvReader_Parse_CommentedOut_Line_With_Different_Comment_Comments_On()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("one,two,three");
            writer.WriteLine("*four,five,six");
            writer.WriteLine("seven,eight,nine");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                    AllowComments = true,
                    Comment = '*',
                    IgnoreBlankLines = true
                });

            dataReader.Read();
            dataReader.Read();
            Assert.AreEqual("seven", dataReader.CurrentRecord[0]);
        }

        [Test]
        public void CsvReader_Parse_Using_Different_Delimiter()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("one\ttwo\tthree");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration { HasHeaderRecord = false, Delimiter = "\t" });

            var record = dataReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual("one", dataReader.CurrentRecord[0]);
            Assert.AreEqual("two", dataReader.CurrentRecord[1]);
            Assert.AreEqual("three", dataReader.CurrentRecord[2]);
        }

        [Test]
        public void CsvReader_Parse_Using_Different_Quote()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("'one','two','three'");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                    TextQualifierType = TextQualifierType.SingleQuote
                });

            var record = dataReader.Read();
            Assert.IsTrue(record);
            Assert.AreEqual("one", dataReader.CurrentRecord[0]);
            Assert.AreEqual("two", dataReader.CurrentRecord[1]);
            Assert.AreEqual("three", dataReader.CurrentRecord[2]);
        }

        [Test]
        public void CsvReader_Parse_Final_Record_With_NoEnd_Of_Line()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("1,2,");
            writer.Write("4,5,");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                });

            var record = dataReader.Read();

            Assert.IsTrue(record);
            Assert.AreEqual("", dataReader.CurrentRecord[2]);

            record = dataReader.Read();

            Assert.IsTrue(record);
            Assert.AreEqual("", dataReader.CurrentRecord[2]);

            record = dataReader.Read();

            Assert.IsFalse(record);
        }

        [Test]
        public void CsvReader_Parse_Last_Line_Has_No_CrLf()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write("a");
            writer.Flush();
            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                });

            var record = dataReader.Read();

            Assert.IsTrue(record);
            Assert.AreEqual("a", dataReader.CurrentRecord[0]);

            record = dataReader.Read();

            Assert.IsFalse(record);
        }

        [Test]
        public void CsvReader_Null_Char_Test()
        {
            using (var stream = new MemoryStream())
            using (var streamReader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                }))
            {
                writer.WriteLine("1,\0,3");
                writer.Flush();
                stream.Position = 0;

                var row = dataReader.Read();
                Assert.IsTrue(row);
                Assert.AreEqual("1", dataReader.CurrentRecord[0]);
                Assert.AreEqual("\0", dataReader.CurrentRecord[1]);
                Assert.AreEqual("3", dataReader.CurrentRecord[2]);
            }
        }

        [Test]
        public void CsvReader_Parse_No_Quotes_Test()
        {
            using (var stream = new MemoryStream())
            using (var streamReader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                    TextQualifierType = TextQualifierType.None
                }))
            {
                writer.WriteLine("one,\"two\",three \" four, \"five\" ");
                writer.Flush();
                stream.Position = 0;

                var record = dataReader.Read();

                Assert.IsNotNull(record);
                Assert.AreEqual("one", dataReader.CurrentRecord[0]);
                Assert.AreEqual("\"two\"", dataReader.CurrentRecord[1]);
                Assert.AreEqual("three \" four", dataReader.CurrentRecord[2]);
                Assert.AreEqual(" \"five\" ", dataReader.CurrentRecord[3]);
            }
        }

        [Test]
        public void CsvReader_Last_Line_Has_Comment_Test()
        {
            using (var stream = new MemoryStream())
            using (var streamReader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var dataReader = new CsvReaderLw(streamReader,
                new DelimitedTextReaderConfiguration
                {
                    HasHeaderRecord = false,
                    AllowComments = true
                }))
            {
                writer.WriteLine("#comment");
                writer.Flush();
                stream.Position = 0;


                dataReader.Read();

                Assert.IsNull(dataReader.CurrentRecord);
            }
        }

        [Test]
        public void CsvReader_Last_Line_Has_Comment_No_Eol_Test()
        {
            using (var stream = new MemoryStream())
            using (var streamReader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var dataReader = new CsvReaderLw(streamReader,
               new DelimitedTextReaderConfiguration
               {
                   HasHeaderRecord = false,
                   AllowComments = true,
                   SkipEmptyRecords = false,
                   IgnoreBlankLines = true
               }))
            {
                writer.Write("#c");
                writer.Flush();
                stream.Position = 0;

                dataReader.Read();

                Assert.IsNull(dataReader.CurrentRecord);
            }
        }

        [Test]
        public void CsvReader_Do_Not_Ignore_Blank_Lines_Test()
        {
            using (var stream = new MemoryStream())
            using (var streamReader = new StreamReader(stream))
            using (var writer = new StreamWriter(stream))
            using (var dataReader = new CsvReaderLw(streamReader,
               new DelimitedTextReaderConfiguration
               {
                   HasHeaderRecord = false,
                   SkipEmptyRecords = false,
                   IgnoreBlankLines = false
               }))
            {
                writer.WriteLine("1,2,3");
                writer.WriteLine(",,");
                writer.WriteLine("");
                writer.WriteLine("4,5,6");
                writer.Flush();
                stream.Position = 0;

                var row = dataReader.Read();
                Assert.IsTrue(row);
                Assert.AreEqual("1", dataReader.CurrentRecord[0]);
                Assert.AreEqual("2", dataReader.CurrentRecord[1]);
                Assert.AreEqual("3", dataReader.CurrentRecord[2]);

                row = dataReader.Read();
                Assert.IsTrue(row);
                Assert.AreEqual("", dataReader.CurrentRecord[0]);
                Assert.AreEqual("", dataReader.CurrentRecord[1]);
                Assert.AreEqual("", dataReader.CurrentRecord[2]);

                row = dataReader.Read();
                Assert.IsTrue(row);
                Assert.AreEqual(3, dataReader.CurrentRecord.Length);
                Assert.IsNull(dataReader.CurrentRecord[0]);
                Assert.IsNull(dataReader.CurrentRecord[1]);
                Assert.IsNull(dataReader.CurrentRecord[2]);

                row = dataReader.Read();
                Assert.IsTrue(row);
                Assert.AreEqual("4", dataReader.CurrentRecord[0]);
                Assert.AreEqual("5", dataReader.CurrentRecord[1]);
                Assert.AreEqual("6", dataReader.CurrentRecord[2]);
            }
        }
    }
}