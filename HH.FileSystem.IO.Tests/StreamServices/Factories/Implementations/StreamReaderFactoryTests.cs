using System.IO;
using HH.FileSystem.IO.StreamServices.Factories.Implementations;
using HH.FileSystem.IO.StreamServices.Factories.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.StreamServices.Factories.Implementations
{
    [TestFixture]
    internal sealed class StreamReaderFactoryTests
    {
        private AutoMocker _autoMocker;
        private IStreamReaderFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new StreamReaderFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateStreamReader_Creates_StreamReader_Correctly()
        {
            //Arrange
            var stream = new MemoryStream();

            //Act
            var streamReader = _sut.CreateStreamReader(stream);

            //Assert
            Assert.IsNotNull(streamReader);
        }
    }
}
