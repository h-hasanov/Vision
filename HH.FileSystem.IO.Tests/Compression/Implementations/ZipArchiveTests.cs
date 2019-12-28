using System.IO;
using System.Linq;
using HH.FileSystem.IO.Compression.Enums;
using HH.FileSystem.IO.Compression.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using ZipArchive = HH.FileSystem.IO.Compression.Implementations.ZipArchive;

namespace HH.FileSystem.IO.Tests.Compression.Implementations
{
    [TestFixture]
    internal sealed class ZipArchiveTests
    {
        private AutoMocker _autoMocker;
        private MemoryStream _memoryStream;
        private IZipArchive _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _memoryStream = new MemoryStream();
            _sut = new ZipArchive(_memoryStream, ZipArchiveMode.Create);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_With_Empty_Stream_Throws()
        {
            //Arrange
            var stream = new MemoryStream();

            //Act
            Assert.Throws<InvalidDataException>(() => _sut = new ZipArchive(stream));
        }

        [Test]
        public void Ctor_With_Empty_Stream_And_Create_Mode_DoesNot_Throw()
        {
            //Arrange
            var stream = new MemoryStream();

            //Act
            _sut = new ZipArchive(stream, ZipArchiveMode.Create);

            //Act
            Assert.IsNotNull(_sut);
        }

        [Test]
        public void CreateEntry_With_Name_Creates_Entry()
        {
            //Arrange
            const string name = "asd";

            //Act
            var result = _sut.CreateEntry(name);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public void CreateEntry_With_Name_And_CompressionLevels_Creates_Entry()
        {
            //Arrange
            const string name = "asd";
            const CompressionLevel compressionLevel = CompressionLevel.Fastest;

            //Act
            var result = _sut.CreateEntry(name, compressionLevel);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(name, result.Name);
        }

        [Test]
        public void Entries_Return_Expected_Entries()
        {
            _sut = new ZipArchive(_memoryStream, ZipArchiveMode.Update);
            Assert.AreEqual(0, _sut.Entries.Count);
            _sut.CreateEntry("a", CompressionLevel.Fastest);
            _sut.CreateEntry("b");
            Assert.AreEqual(2, _sut.Entries.Count);
        }

        [Test]
        public void Entries_Return_Expected_Entries_When_Created_With_ExistingEntries()
        {
            const string fileName = "ZipArchiveTests_Entries_Return_Expected_Entries_When_Created_With_ExistingEntries.test";
            var writeStream = new FileStream(fileName, FileMode.Create);

            const string existingFileName = "adsada";
            using (var existingArchive = new System.IO.Compression.ZipArchive(writeStream, System.IO.Compression.ZipArchiveMode.Create))
            {
                existingArchive.CreateEntry(existingFileName);
            }

            using (var readStream = new FileStream(fileName, FileMode.Open))
            {
                _sut = new ZipArchive(readStream, ZipArchiveMode.Update);
                Assert.AreEqual(1, _sut.Entries.Count);
                _sut.CreateEntry("a", CompressionLevel.Fastest);
                _sut.CreateEntry("b");
                Assert.AreEqual(3, _sut.Entries.Count);

                var expectedFileNames = new[] { existingFileName, "a", "b" };
                var i = 0;
                foreach (var zipArchiveEntry in _sut.Entries)
                {
                    Assert.AreEqual(expectedFileNames[i], zipArchiveEntry.Name);
                    i++;
                }
            }
        }

        [TestCase("Hello")]
        [TestCase("Hello/world")]
        public void CreateDirectory_Creates_Directory(string directoryName)
        {
            //Act
            var directory = _sut.CreateDirectory(directoryName);

            //Assert
            Assert.IsNotNull(directory);
            Assert.AreEqual(1, _sut.Entries.Count(c => c.FullName == $"{directoryName}/"));
        }

        [TestCase("Hello")]
        [TestCase("Hello/world")]
        public void GeDirectory_Returns_Directory_If_Exists(string directoryName)
        {
            //Arrange
            _sut.CreateDirectory(directoryName);

            //Act
            var searchresult = _sut.GetDirectory(directoryName);

            //Assert
            Assert.IsNotNull(searchresult);
            Assert.AreEqual(1, _sut.Entries.Count(c => c.FullName == $"{directoryName}/"));
        }

        [Test]
        public void GetDirectory_Throws_If_Directory_Not_Found()
        {
            //Arrange
            _sut.CreateDirectory("a");

            //Act & Assert
            Assert.Throws<FileNotFoundException>(() => _sut.GetDirectory("b"));
        }

        [Test]
        public void Dispose_Disposes_Correctly()
        {
            //Assert
            Assert.DoesNotThrow(() => _sut.Dispose());
        }
    }
}
