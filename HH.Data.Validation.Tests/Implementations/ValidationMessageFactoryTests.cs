using System;
using HH.Data.Validation.Enums;
using HH.Data.Validation.Implementations;
using HH.Data.Validation.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Data.Validation.Tests.Implementations
{
    [TestFixture]
    internal sealed class ValidationMessageFactoryTests
    {
        private AutoMocker _autoMocker;
        private IValidationMessageFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ValidationMessageFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(MessageType.Information,typeof(IInformationValidationMessage))]
        [TestCase(MessageType.Error,typeof(IErrorValidationMessage))]
        [TestCase(MessageType.Warning,typeof(IWarningValidationMessage))]
        public void CreateValidationMessage_Creates_Correctly(MessageType messageType, Type expectedType)
        {
            //Arrange

            //Act
            var result = _sut.CreateValidationMessage("asd", messageType);

            //Assert
            Assert.IsTrue(expectedType.IsInstanceOfType(result));
        }
    }
}
