using HH.EnvironmentServices.Interfaces;
using HH.EnvironmentServices.Services;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.Services
{
    [TestFixture]
    internal sealed class NotifyPropertyChangedEventArgsFactoryTests
    {
        private AutoMocker _autoMocker;
        private INotifyPropertyChangedEventArgsFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new NotifyPropertyChangedEventArgsFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreatePropertyChangedEventArgs_With_PropertyName_Returns_Correct_EventArgs()
        {
            //Arrange
            const string propertyName = "some sort of property name";

            //Act
            var result = _sut.CreatePropertyChangedEventArgs(propertyName);

            //Assert
            Assert.AreEqual(propertyName, result.PropertyName);
        }
    }
}
