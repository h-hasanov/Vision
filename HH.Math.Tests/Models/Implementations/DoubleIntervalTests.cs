using HH.Math.Enums;
using HH.Math.Models.Implementations;
using HH.Math.Models.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Math.Tests.Models.Implementations
{
    [TestFixture]
    internal sealed class DoubleIntervalTests
    {
        private AutoMocker _autoMocker;
        private const double LowerBound = -3;
        private const double UpperBound = 5;
        private const ClosureType ClosureType = Enums.ClosureType.ClosedOpen;
        private IInterval<double> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new Interval<double>(LowerBound, UpperBound, ClosureType);
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
            Assert.AreEqual(LowerBound, _sut.LowerBound);
            Assert.AreEqual(UpperBound, _sut.UpperBound);
            Assert.AreEqual(ClosureType, _sut.ClosureType);
        }

        [TestCase(LowerBound, false)]
        [TestCase(UpperBound, false)]
        [TestCase(LowerBound + 0.1, true)]
        [TestCase(UpperBound - 0.1, true)]
        [TestCase(LowerBound - 0.1, false)]
        [TestCase(UpperBound + 0.1, false)]
        public void IsInside_With_OpenOpen_Returns_EpectedValue(double value, bool isInside)
        {
            //Arrange
            var sut = new Interval<double>(LowerBound, UpperBound, ClosureType.OpenOpen);

            //Act
            var result = sut.IsInside(value);

            //Assert
            Assert.AreEqual(isInside, result);
        }

        [TestCase(LowerBound, false)]
        [TestCase(UpperBound, true)]
        [TestCase(LowerBound + 0.1, true)]
        [TestCase(UpperBound - 0.1, true)]
        [TestCase(LowerBound - 0.1, false)]
        [TestCase(UpperBound + 0.1, false)]
        public void IsInside_With_OpenClosed_Returns_EpectedValue(double value, bool isInside)
        {
            //Arrange
            var sut = new Interval<double>(LowerBound, UpperBound, ClosureType.OpenClosed);

            //Act
            var result = sut.IsInside(value);

            //Assert
            Assert.AreEqual(isInside, result);
        }

        [TestCase(LowerBound, true)]
        [TestCase(UpperBound, false)]
        [TestCase(LowerBound + 0.1, true)]
        [TestCase(UpperBound - 0.1, true)]
        [TestCase(LowerBound - 0.1, false)]
        [TestCase(UpperBound + 0.1, false)]
        public void IsInside_With_ClosedOpen_Returns_EpectedValue(double value, bool isInside)
        {
            //Arrange
            var sut = new Interval<double>(LowerBound, UpperBound, ClosureType.ClosedOpen);

            //Act
            var result = sut.IsInside(value);

            //Assert
            Assert.AreEqual(isInside, result);
        }

        [TestCase(LowerBound, true)]
        [TestCase(UpperBound, true)]
        [TestCase(LowerBound + 0.1, true)]
        [TestCase(UpperBound - 0.1, true)]
        [TestCase(LowerBound - 0.1, false)]
        [TestCase(UpperBound + 0.1, false)]
        public void IsInside_With_ClosedClosed_Returns_EpectedValue(double value, bool isInside)
        {
            //Arrange
            var sut = new Interval<double>(LowerBound, UpperBound, ClosureType.ClosedClosed);

            //Act
            var result = sut.IsInside(value);

            //Assert
            Assert.AreEqual(isInside, result);
        }
    }
}
