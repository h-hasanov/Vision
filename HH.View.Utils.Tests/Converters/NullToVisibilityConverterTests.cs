using System;
using System.Windows;
using System.Windows.Data;
using HH.TestUtils;
using HH.View.Utils.Converters;
using NUnit.Framework;

namespace HH.View.Utils.Tests.Converters
{
    [TestFixture]
    internal sealed class NullToVisibilityConverterTests
    {
        private AutoMocker _autoMocker;
        private IValueConverter _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new NullToVisibilityConverter();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Convert_Returns_ExpectValue()
        {
            //Act & Assert
            Assert.AreEqual(Visibility.Collapsed, _sut.Convert(null, null, null, null));
            Assert.AreEqual(Visibility.Visible, _sut.Convert(new ClassWithParameters("a", 3), null, null, null));
        }

        [Test]
        public void Convert_With_DateTime_Returns_ExpectValue()
        {
            //Act & Assert
            Assert.AreEqual(Visibility.Collapsed, _sut.Convert(default(DateTime), null, null, null));
            Assert.AreEqual(Visibility.Collapsed, _sut.Convert(new DateTime(), null, null, null));
            Assert.AreEqual(Visibility.Visible, _sut.Convert(DateTime.Now, null, null, null));
        }
    }

    internal sealed class ClassWithParameters
    {
        private readonly string _a;
        private readonly int _b;

        public ClassWithParameters(string a, int b)
        {
            _a = a;
            _b = b;
        }
    }
}
