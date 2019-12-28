using System;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.Model.Models.Implementations;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.Model.Tests.Models.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoTests
    {
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(ErrorSeverity.Error)]
        [TestCase(ErrorSeverity.Warning)]
        [TestCase(ErrorSeverity.Information)]
        public void Ctor_Sets_Properties(ErrorSeverity errorSeverity)
        {
            //Arrange
            const string description = "asdsad";
            var navigateToErrorAction = _autoMocker.Mock<Action>();

            //Act
            var result = new ErrorInfo(errorSeverity, description, navigateToErrorAction);

            //Assert
            Assert.AreEqual(errorSeverity, result.Severity);
            Assert.AreEqual(description, result.Description);
            Assert.AreEqual(navigateToErrorAction, result.NavigateToErrorAction);
        }
    }
}
