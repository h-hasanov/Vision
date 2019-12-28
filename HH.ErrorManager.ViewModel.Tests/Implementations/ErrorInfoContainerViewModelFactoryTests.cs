using HH.Data.Entity.ViewModel.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Implementations;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.ViewModel.Tests.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoContainerViewModelFactoryTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoViewModelFactory _errorInfoViewModelFactory;
        private IEntityCollectionViewModelFactory _entityCollectionViewModelFactory;
        private IErrorInfoContainerViewModelFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _errorInfoViewModelFactory = _autoMocker.Mock<IErrorInfoViewModelFactory>();
            _entityCollectionViewModelFactory = _autoMocker.Mock<IEntityCollectionViewModelFactory>();

            _sut = new ErrorInfoContainerViewModelFactory(_entityCollectionViewModelFactory, _errorInfoViewModelFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateErrorInfoContainerViewModel_Creates_ErrorInfoContainerViewModel_Correctly()
        {
            //Arrange
            var errorInfoContainer = _autoMocker.Mock<IErrorInfoContainer>();

            //Act
            var result = _sut.CreateErrorInfoContainerViewModel(errorInfoContainer);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
