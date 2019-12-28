using System;
using HH.Math.Random.Enums;
using HH.Math.Random.Factories.Implementations;
using HH.Math.Random.Factories.Interfaces;
using HH.Math.Random.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Math.Tests.Random.Factories.Implementations
{
    [TestFixture]
    internal sealed class RandomSourceFactoryTests
    {
        private AutoMocker _autoMocker;
        private IRandomSourceFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new RandomSourceFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(RandomSourceType.System, typeof(ISystemRandomSource))]
        public void CreateRandomSource_With_RandomSourceType_And_Seed_Creates_Correctly(RandomSourceType randomSourceType, Type expectedType)
        {
            //Arrange
            const int seed = 43;

            //Act
            var result = _sut.CreateRandomSource(randomSourceType, seed);

            //Assert
            Assert.AreEqual(randomSourceType, result.RandomSourceType);
            Assert.IsTrue(expectedType.IsInstanceOfType(result));
        }

        [TestCase(RandomSourceType.System, typeof(ISystemRandomSource))]
        public void CreateRandomSource_With_RandomSourceType_Creates_Correctly(RandomSourceType randomSourceType, Type expectedType)
        {
            //Arrange

            //Act
            var result = _sut.CreateRandomSource(randomSourceType);

            //Assert
            Assert.AreEqual(randomSourceType, result.RandomSourceType);
            Assert.IsTrue(expectedType.IsInstanceOfType(result));
        }
    }
}
