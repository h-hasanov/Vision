using HH.Math.Random.Enums;
using HH.Math.Random.Implementations;
using HH.Math.Random.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Math.Tests.Random.Implementations
{
    [TestFixture]
    internal sealed class SystemRandomSourceTests
    {
        private AutoMocker _autoMocker;
        private System.Random _randomSource;
        private ISystemRandomSource _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _randomSource = _autoMocker.Mock<System.Random>();
            _sut = new SystemRandomSource(_randomSource);
        }

        [Test]
        public void Ctor_With_NoParameters_DoesNotThrow()
        {
            //Act & Assert
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow(() => new SystemRandomSource());
        }


        [Test]
        public void Ctor_With_Seed_DoesNotThrow()
        {
            //Act & Assert
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow(() => new SystemRandomSource(43));
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
            Assert.AreEqual(RandomSourceType.System, _sut.RandomSourceType);
        }

        [Test]
        public void Next_Returns_Correct_Value()
        {
            //Arrange
            const int randomNumber = int.MaxValue / 2;
            _randomSource.Expect(c => c.Next(0, int.MaxValue)).Return(randomNumber);

            //Act
            var result = _sut.Next();

            //Assert
            Assert.AreEqual(randomNumber, result);
        }

        [Test]
        public void Next_With_MaxValue_Returns_Correct_Value()
        {
            //Arrange
            const int maxValue = 123;
            const int randomNumber = maxValue / 2;
            _randomSource.Expect(c => c.Next(0, maxValue)).Return(randomNumber);

            //Act
            var result = _sut.Next(maxValue);

            //Assert
            Assert.AreEqual(randomNumber, result);
        }

        [Test]
        public void Next_With_MinValue_And_MaxValue_Returns_Correct_Value()
        {
            //Arrange
            const int minValue = -2;
            const int maxValue = 123;
            const int randomNumber = maxValue / 2;
            _randomSource.Expect(c => c.Next(minValue, maxValue)).Return(randomNumber);

            //Act
            var result = _sut.Next(minValue, maxValue);

            //Assert
            Assert.AreEqual(randomNumber, result);
        }

        [Test]
        public void NextDouble_Returns_Correct_Value()
        {
            //Arrange
            const double randomNumber = int.MaxValue / 2d;
            _randomSource.Expect(c => c.NextDouble()).Return(randomNumber);

            //Act
            var result = _sut.NextDouble();

            //Assert
            Assert.AreEqual(randomNumber, result);
        }
    }
}
