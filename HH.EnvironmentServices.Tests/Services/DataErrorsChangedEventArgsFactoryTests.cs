using HH.EnvironmentServices.Services;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.Services
{
    [TestFixture]
    internal sealed class DataErrorsChangedEventArgsFactoryTests
    {
        private AutoMocker _autoMocker;
        private DataErrorsChangedEventArgsFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new DataErrorsChangedEventArgsFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateDataErrorsChangedEventArgs_Creates_Correctly()
        {
            //Arrange
            const string propertyName = "asdas";

            //Act
            var result = _sut.CreateDataErrorsChangedEventArgs(propertyName);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(propertyName, result.PropertyName);
        }
    }
}
