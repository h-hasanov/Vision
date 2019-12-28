using HH.Finance.Interfaces;
using NUnit.Framework;

namespace HH.Finance.Tests.DiffusionProcesses
{
    internal sealed class SquareRootProcessTests
    {
        private ISquareRootProcess _sut;

        private double _mean;
        private double _speed;
        private double _volatility;

        [SetUp]
        public void Setup()
        {
            _mean = 0.025;
            _speed = 0.03;
            _volatility = 0.2;
            _sut = new DiffussionProcesses.SquareRootProcess(_mean, _speed, _volatility);
        }

        [Test]
        public void Ctor_SetsParameters_Correctly()
        {
            //Arrange
            const int mean = 123;
            const int speed = 333;
            const int volatility = 20;

            //Act
            _sut = new DiffussionProcesses.SquareRootProcess(mean, speed, volatility);

            //Assert
            Assert.AreEqual(mean, _sut.Mean);
            Assert.AreEqual(speed, _sut.Speed);
            Assert.AreEqual(volatility, _sut.Volatility);
        }

        [Test]
        public void Drift_Returns_CorrectValue()
        {
            //Arrange
            const double expectedResult = 0.00045;

            //Act
            var result = _sut.Drift(default(double), 0.01);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [Test]
        public void Diffusion_Returns_CorrectValue()
        {
            //Arrange
            const double expectedResult = 0.08;

            //Act
            var result = _sut.Diffusion(default(double), 0.16);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [Test]
        public void Expectation_Returns_CorrectValue()
        {
            //Arrange
            const double dt = 0.04;
            const double expectedResult = 0.029994;

            //Act
            var result = _sut.Expectation(default(double), 0.03, dt);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Variance_Returns_CorrectValue()
        {
            //Arrange
            const double dt = 0.04;
            const double expectedResult = 0.000256;

            //Act
            var result = _sut.Variance(default(double), 0.16, dt);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }
    }
}
