using System;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.Model.Factories.Implementations;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.Model.Tests.Factories.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoFactoryTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ErrorInfoFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateError_Creates_Error_Correctly()
        {
            //Arrange
            const string description = "asdsa";
            var navigateToErrorAction = _autoMocker.Mock<Action>();

            //Act
            var result = _sut.CreateError(description, navigateToErrorAction);

            //Assert
            Assert.AreEqual(ErrorSeverity.Error, result.Severity);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(navigateToErrorAction, result.NavigateToErrorAction);
        }

        [Test]
        public void CreateInformation_Creates_Information_Correctly()
        {
            //Arrange
            const string description = "asdsa";
            var navigateToErrorAction = _autoMocker.Mock<Action>();

            //Act
            var result = _sut.CreateInformation(description, navigateToErrorAction);

            //Assert
            Assert.AreEqual(ErrorSeverity.Information, result.Severity);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(navigateToErrorAction, result.NavigateToErrorAction);
        }

        [Test]
        public void CreateWarning_Creates_Warning_Correctly()
        {
            //Arrange
            const string description = "asdsa";
            var navigateToErrorAction = _autoMocker.Mock<Action>();

            //Act
            var result = _sut.CreateWarning(description, navigateToErrorAction);

            //Assert
            Assert.AreEqual(ErrorSeverity.Warning, result.Severity);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(navigateToErrorAction, result.NavigateToErrorAction);
        }
    }
}
