using System;
using System.Linq;
using HH.EnvironmentServices.Interfaces;
using HH.EnvironmentServices.Services;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.Services
{
    [TestFixture]
    internal sealed class GuidFactoryTests
    {
        private AutoMocker _autoMocker;
        private IGuidFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new GuidFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Creates_Unique_Guids()
        {
            //Arrange
            const int numberOfGuids = 10;
            var guids = new Guid[numberOfGuids];

            //Act
            for (var i = 0; i < numberOfGuids; i++)
            {
                guids[i] = _sut.CreateGuid();
            }

            //Assert
            Assert.AreEqual(guids.Distinct().Count(), numberOfGuids);
            foreach (var guid in guids)
            {
                Assert.AreNotEqual(Guid.Empty, guid);
            }
        }
    }
}
