using System.Globalization;
using HH.FileSystem.IO.Enums;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class DelimitedTextReaderConfigurationTests
    {
        private AutoMocker _autoMocker;
        private IDelimitedTextReaderConfiguration _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new DelimitedTextReaderConfiguration();
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
            Assert.AreEqual(CultureInfo.CurrentCulture, _sut.CultureInfo);
            Assert.AreEqual(",", _sut.Delimiter);
            Assert.AreEqual(true, _sut.HasHeaderRecord);
            Assert.AreEqual(TextQualifierType.DoubleQuote, _sut.TextQualifierType);
            Assert.AreEqual(false, _sut.SkipEmptyRecords);
            Assert.AreEqual(false, _sut.IgnoreBlankLines);
        }
    }
}
