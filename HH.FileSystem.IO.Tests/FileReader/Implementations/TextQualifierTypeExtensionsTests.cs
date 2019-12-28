using HH.FileSystem.IO.Enums;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class TextQualifierTypeExtensionsTests
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

        [TestCase(TextQualifierType.DoubleQuote, '"')]
        [TestCase(TextQualifierType.SingleQuote, '\'')]
        [TestCase(TextQualifierType.None, char.MinValue)]
        public void ToChar_Returns_Expected_Char(TextQualifierType textQualifierType, char expectedChar)
        {
            //Assert
            Assert.AreEqual(expectedChar, textQualifierType.ToChar());
        }
    }
}
