using HH.TestUtils;
using HH.View.Utils.Converters;
using NUnit.Framework;

namespace HH.View.Utils.Tests.Converters
{
    [TestFixture]
    internal sealed class ZeroBaseToOneBaseConverterTests
    {
        private AutoMocker _autoMocker;
        private ZeroBaseToOneBaseConverter _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ZeroBaseToOneBaseConverter();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Convert_Coverts_Correctly()
        {
            //Arrange
            const int value = 10;
            const int expectedValue = value + 1;

            //Act
            var result = _sut.Convert(value, null, null, null);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }
    }
}
