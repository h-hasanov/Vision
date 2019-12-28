using HH.ErrorManager.Model.Factories.Implementations;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.Model.Tests.Factories.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoContainerFactoryTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoContainerFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ErrorInfoContainerFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateErrorInfoContainer_Creates_ErrorInfoContainer_Correctly()
        {
            //Arrange
            const string description = "adsa";

            //Act
            var result = _sut.CreateErrorInfoContainer(description);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(description, result.Description);
        }
    }
}
