using System.IO;
using HH.FileSystem.IO.Compression.Enums;
using HH.FileSystem.IO.Compression.Factories.Implementations;
using HH.FileSystem.IO.Compression.Factories.Interfaces;
using HH.FileSystem.IO.Compression.Implementations;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.Compression.Factories.Implementations
{
    [TestFixture]
    internal sealed class ZipArchiveFactoryTests
    {
        private AutoMocker _autoMocker;
        private IZipArchiveFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ZipArchiveFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateZipArchive_With_Empty_Stream_Throws()
        {
            //Arrange
            var stream = new MemoryStream();

            //Act
            Assert.Throws<InvalidDataException>(() => _sut.CreateZipArchive(stream));
        }

        [Test]
        public void CreateZipArchive_With_Empty_Stream_And_Create_Mode_DoesNot_Throw()
        {
            //Arrange
            var stream = new MemoryStream();

            //Act
            var result = _sut.CreateZipArchive(stream, ZipArchiveMode.Create);

            //Act
            Assert.IsNotNull(result);
        }
    }
}
