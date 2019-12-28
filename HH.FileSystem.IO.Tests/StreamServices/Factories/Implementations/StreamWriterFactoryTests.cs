using System.IO;
using HH.FileSystem.IO.StreamServices.Factories.Implementations;
using HH.FileSystem.IO.StreamServices.Factories.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.StreamServices.Factories.Implementations
{
    [TestFixture]
    internal sealed class StreamWriterFactoryTests
    {
        private AutoMocker _autoMocker;
        private IStreamWriterFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new StreamWriterFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateStreamWriter_Creates_StreamWriter()
        {
            //Arrange
            var stream = new MemoryStream();

            //Act
            var result = _sut.CreateStreamWriter(stream);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
