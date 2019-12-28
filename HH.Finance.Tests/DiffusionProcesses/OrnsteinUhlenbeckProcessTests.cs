using HH.Finance.DiffussionProcesses;
using HH.Finance.Interfaces;
using NUnit.Framework;

namespace HH.Finance.Tests.DiffusionProcesses
{
    [TestFixture]
    internal sealed class OrnsteinUhlenbeckProcessTests
    {

        private IOrnsteinUhlenbeckProcess _sut;
        private double _speed;
        private double _volatility;

        [SetUp]
        public void Setup()
        {
            _speed = 0.03;
            _volatility = 0.2;
            _sut = new OrnsteinUhlenbeckProcess(_speed, _volatility);
        }

        [Test]
        public void Ctor_SetsParameters_Correctly()
        {
            //Arrange
            const int speed = 333;
            const int volatility = 20;

            //Act
            _sut = new OrnsteinUhlenbeckProcess(speed, volatility);

            //Assert
            Assert.AreEqual(speed, _sut.Speed);
            Assert.AreEqual(volatility, _sut.Volatility);
        }

        [Test]
        public void Drift_Returns_CorrectValue()
        {
            //Arrange
            const double expectedResult = -0.129;

            //Act
            var result = _sut.Drift(default(double), 4.3);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [Test]
        public void Diffusion_Returns_CorrectValue()
        {
            //Arrange
            const double expectedResult = 0.2;

            //Act
            var result = _sut.Diffusion(default(double), default(double));

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Expectation_Returns_CorrectValue()
        {
            //Arrange
            const double dt = 0.04;
            const double expectedResult = 4.294843095;

            //Act
            var result = _sut.Expectation(default(double), 4.3, dt);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [Test]
        public void Variance_Returns_CorrectValue()
        {
            //Arrange
            const double dt = 0.04;
            const double expectedResult = 0.001598081534;

            //Act
            var result = _sut.Variance(default(double), default(double), dt);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }
    }
}