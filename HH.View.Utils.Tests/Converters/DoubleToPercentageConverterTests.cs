using System;
using System.Windows.Data;
using HH.TestUtils;
using HH.View.Utils.Converters;
using NUnit.Framework;

namespace HH.View.Utils.Tests.Converters
{
    [TestFixture]
    internal sealed class DoubleToPercentageConverterTests
    {
        private AutoMocker _autoMocker;
        private IValueConverter _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new DoubleToPercentageConverter();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(0, 0)]
        [TestCase(0.3, 30)]
        [TestCase(0.7, 70)]
        [TestCase(1, 100)]
        public void Convert_Coverts_Correctly(double value, double expectedValue)
        {
            //Act
            var result = _sut.Convert(value, null, null, null);

            //Assert
            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void ConvertBack_Throws_Not_Implemented_Exception()
        {
            //Arrange
            Assert.Throws<NotImplementedException>(() => _sut.ConvertBack(0.3, null, null, null));
        }
    }
}
