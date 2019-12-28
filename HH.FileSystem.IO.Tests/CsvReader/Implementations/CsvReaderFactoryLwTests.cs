using System.IO;
using HH.FileSystem.IO.CsvReader.Interfaces;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.Win.CsvReader.Implementations;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.CsvReader.Implementations
{
    [TestFixture]
    internal sealed class CsvReaderFactoryLwTests
    {
        private AutoMocker _autoMocker;
        private ICsvReaderFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new CsvReaderFactoryLw();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateCsvReader_Creates_CsvReader()
        {
            //Arrange
            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = true };
            var textReader = _autoMocker.Mock<TextReader>();

            //Act
            var result = _sut.CreateCsvReader(textReader, fileConfiguration);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
