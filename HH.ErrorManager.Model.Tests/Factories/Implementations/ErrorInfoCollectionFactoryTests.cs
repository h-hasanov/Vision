using HH.ErrorManager.Model.Factories.Implementations;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.Model.Tests.Factories.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoCollectionFactoryTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoFactory _errorInfoFactory;
        private IErrorInfoCollectionFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _errorInfoFactory = _autoMocker.Mock<IErrorInfoFactory>();

            _sut = new ErrorInfoCollectionFactory(_errorInfoFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreatErrorInfoCollection_Creates_Correctly()
        {
            //Act
            var result = _sut.CreatErrorInfoCollection();

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
