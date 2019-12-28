using HH.FileSystem.IO.Compression.Enums;
using HH.FileSystem.IO.Compression.Implementations;
using HH.FileSystem.IO.Compression.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.FileSystem.IO.Tests.Compression.Implementations
{
    [TestFixture]
    internal sealed class ZipArchiveDirectoryTests
    {
        private AutoMocker _autoMocker;
        private IZipArchive _zipArchive;
        private const string DirectoryFullPath = "Document/Solution/Project/";
        private IZipArchiveDirectory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _zipArchive = _autoMocker.Mock<IZipArchive>();

            _sut = new ZipArchiveDirectory(_zipArchive, DirectoryFullPath);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Entries_Returns_Expected_Entries()
        {
            //Arrange
            var directory1 = _autoMocker.Mock<IZipArchiveEntry>();
            directory1.Expect(c => c.Name).Return(string.Empty);

            var entry1 = _autoMocker.Mock<IZipArchiveEntry>();
            entry1.Expect(c => c.FullName).Return($"{DirectoryFullPath}a.manifest");
            entry1.Expect(c => c.Name).Return("a.manifest");

            var childDirectory = _autoMocker.Mock<IZipArchiveEntry>();
            childDirectory.Expect(c => c.Name).Return(string.Empty);

            var entry2 = _autoMocker.Mock<IZipArchiveEntry>();
            entry2.Expect(c => c.FullName).Return($"{DirectoryFullPath}b.m");
            entry2.Expect(c => c.Name).Return("b.m");

            _zipArchive.Expect(c => c.Entries).Return(new[] { directory1, entry1, childDirectory, entry2 });

            //Act
            var result = _sut.Entries;

            //Assert
            CollectionAssert.AreEqual(new[] { entry1, entry2 }, result);
        }

        [Test]
        public void CreateEntry_With_Name_Creates_Correctly()
        {
            //Arrange
            const string entryName = "b.m";
            var entry = _autoMocker.Mock<IZipArchiveEntry>();
            _zipArchive.Expect(c => c.CreateEntry($"{DirectoryFullPath}{entryName}"))
                .Return(entry);

            //Act
            var result = _sut.CreateEntry(entryName);

            //Assert
            Assert.AreEqual(entry, result);
        }

        [Test]
        public void CreateEntry_With_Name_And_CompressionLevel_Creates_Correctly()
        {
            //Arrange
            const string entryName = "b.m";
            const CompressionLevel compressionLevel = CompressionLevel.Optimal;
            var entry = _autoMocker.Mock<IZipArchiveEntry>();
            _zipArchive.Expect(c => c.CreateEntry($"{DirectoryFullPath}{entryName}", compressionLevel))
                .Return(entry);

            //Act
            var result = _sut.CreateEntry(entryName, compressionLevel);

            //Assert
            Assert.AreEqual(entry, result);
        }

        [Test]
        public void GetDirectory_Returns_Correctly()
        {
            //Arrange
            const string directoryName = "someDirectory";
            var directory = _autoMocker.Mock<IZipArchiveDirectory>();
            _zipArchive.Expect(c => c.GetDirectory($"{DirectoryFullPath}{directoryName}"))
                .Return(directory);

            //Act
            var result = _sut.GetDirectory(directoryName);

            //Assert
            Assert.AreEqual(directory, result);
        }

        [Test]
        public void CreateDirectory_Returns_Correctly()
        {
            //Arrange
            const string directoryName = "someDirectory";
            var directory = _autoMocker.Mock<IZipArchiveDirectory>();
            _zipArchive.Expect(c => c.CreateDirectory($"{DirectoryFullPath}{directoryName}"))
                .Return(directory);

            //Act
            var result = _sut.CreateDirectory(directoryName);

            //Assert
            Assert.AreEqual(directory, result);
        }
    }
}
