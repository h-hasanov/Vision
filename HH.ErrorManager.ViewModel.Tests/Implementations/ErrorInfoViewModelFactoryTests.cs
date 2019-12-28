using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Implementations;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using NUnit.Framework;

namespace HH.ErrorManager.ViewModel.Tests.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoViewModelFactoryTests
    {
        private AutoMocker _autoMocker;
        private ICommandFactory _commandFactory;
        private IErrorInfoViewModelFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();
            _sut = new ErrorInfoViewModelFactory(_commandFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateErrorInfoViewModel_Creates_ErrorInfoViewModel_Correctly()
        {
            //Arrange
            var errorInfo = _autoMocker.Mock<IErrorInfo>();

            //Act
            var result = _sut.CreateErrorInfoViewModel(errorInfo);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
