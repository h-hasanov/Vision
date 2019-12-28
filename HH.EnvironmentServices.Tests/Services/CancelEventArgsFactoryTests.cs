using HH.EnvironmentServices.Interfaces;
using HH.EnvironmentServices.Services;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.Services
{
    [TestFixture]
    internal sealed class CancelEventArgsFactoryTests
    {
        private AutoMocker _autoMocker;
        private ICancelEventArgsFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new CancelEventArgsFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Create_Creats_CancelEventArgs_Correctly()
        {
            //Act
            var result = _sut.CreateCancelEventArgs();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
