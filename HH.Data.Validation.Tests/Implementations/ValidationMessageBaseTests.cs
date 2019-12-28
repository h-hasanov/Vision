using HH.Data.Validation.Enums;
using HH.Data.Validation.Implementations;
using HH.Data.Validation.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Data.Validation.Tests.Implementations
{
    [TestFixture]
    internal sealed class ValidationMessageBaseTests
    {
        private AutoMocker _autoMocker;
        private IValidationMessage _sut;

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
            _sut = new TestValidationMessage(displayMessage);

            //Assert
            Assert.AreEqual(displayMessage, _sut.Text);
        }
    }

    internal sealed class TestValidationMessage : ValidationMessageBase
    {
        public TestValidationMessage(string text) : base(text)
        {
        }


        public override MessageType MessageType
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
