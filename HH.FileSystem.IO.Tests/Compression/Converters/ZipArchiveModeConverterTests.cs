using HH.FileSystem.IO.Compression.Converters;
using HH.FileSystem.IO.Compression.Enums;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.Compression.Converters
{
    [TestFixture]
    internal sealed class ZipArchiveModeConverterTests
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

        [TestCase(ZipArchiveMode.Create, System.IO.Compression.ZipArchiveMode.Create)]
        [TestCase(ZipArchiveMode.Update, System.IO.Compression.ZipArchiveMode.Update)]
        [TestCase(ZipArchiveMode.Read, System.IO.Compression.ZipArchiveMode.Read)]
        public void Convert_Converts_Correctly(ZipArchiveMode zipArchiveMode,
            System.IO.Compression.ZipArchiveMode expectedResult)
        {
            //Assert
            Assert.AreEqual(expectedResult, ZipArchiveModeConverter.Convert(zipArchiveMode));
        }
    }
}
