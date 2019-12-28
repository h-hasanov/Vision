using System.IO;
using System.IO.Compression;
using HH.FileSystem.IO.Compression.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using ZipArchiveEntry = HH.FileSystem.IO.Compression.Implementations.ZipArchiveEntry;

namespace HH.FileSystem.IO.Tests.Compression.Implementations
{
    [TestFixture]
    internal sealed class ZipArchiveEntryTests
    {
        private AutoMocker _autoMocker;
        private System.IO.Compression.ZipArchiveEntry _zipArchiveEntry;
        private const string ZipArchiveDirectoryName = "Some/where/nice/";
        private const string ZipArchiveEntryName = "Archive XYZ";
        private IZipArchiveEntry _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            var memoryStream = new MemoryStream();
            var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create);
            _zipArchiveEntry = zipArchive.CreateEntry($"{ZipArchiveDirectoryName}{ZipArchiveEntryName}");
            _sut = new ZipArchiveEntry(_zipArchiveEntry);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Assert
            Assert.AreEqual(ZipArchiveEntryName, _sut.Name);
            Assert.AreEqual($"{ZipArchiveDirectoryName}{ZipArchiveEntryName}", _sut.FullName);
            Assert.IsNotNull(_sut.Open());
        }
    }
}
