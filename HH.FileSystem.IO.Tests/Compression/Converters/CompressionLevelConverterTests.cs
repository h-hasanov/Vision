using HH.FileSystem.IO.Compression.Converters;
using HH.FileSystem.IO.Compression.Enums;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.Compression.Converters
{
    [TestFixture]
    internal sealed class CompressionLevelConverterTests
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

        [TestCase(CompressionLevel.Fastest, System.IO.Compression.CompressionLevel.Fastest)]
        [TestCase(CompressionLevel.NoCompression, System.IO.Compression.CompressionLevel.NoCompression)]
        [TestCase(CompressionLevel.Optimal, System.IO.Compression.CompressionLevel.Optimal)]
        public void Convert_Converts_Correctly(CompressionLevel compressionLevel,
            System.IO.Compression.CompressionLevel expectedResult)
        {
            //Assert
            Assert.AreEqual(expectedResult, CompressionLevelConverter.Convert(compressionLevel));
        }
    }
}
