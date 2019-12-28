using HH.Data.Validation.Enums;
using HH.Data.Validation.Implementations;
using HH.Data.Validation.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Data.Validation.Tests.Implementations
{
    [TestFixture]
    internal sealed class InformationValidationMessageTests
    {
        private AutoMocker _autoMocker;
        private IInformationValidationMessage _sut;

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

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Arrange
            const string displayMessage = "some sort of message";

            //Act
            _sut = new InformationValidationMessage(displayMessage);

            //Assert
            Assert.AreEqual(displayMessage, _sut.Text);
            Assert.AreEqual(MessageType.Information, _sut.MessageType);
        }
    }
}
