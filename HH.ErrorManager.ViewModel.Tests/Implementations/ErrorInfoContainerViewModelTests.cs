using System;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Implementations;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ErrorManager.ViewModel.Tests.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoContainerViewModelTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoContainer _model;
        private IEntityCollectionViewModelFactory _entityCollectionViewModelFactory;
        private IErrorInfoViewModelFactory _errorInfoViewModelFactory;
        private IReadOnlyEntityCollectionViewModel<IErrorInfo, IErrorInfoViewModel> _errorInfoViewModelCollection;
        private IErrorInfoContainerViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _model = _autoMocker.Mock<IErrorInfoContainer>();
            _entityCollectionViewModelFactory = _autoMocker.Mock<IEntityCollectionViewModelFactory>();
            _errorInfoViewModelFactory = _autoMocker.Mock<IErrorInfoViewModelFactory>();
            _errorInfoViewModelCollection =
                _autoMocker.Mock<IReadOnlyEntityCollectionViewModel<IErrorInfo, IErrorInfoViewModel>>();

            _entityCollectionViewModelFactory.Expect(
                c =>
                    c.CreateReadOnlyEntityCollectionViewModel(default(IReadOnlyEntityCollection<IErrorInfo>),
                        default(Func<IErrorInfo, IErrorInfoViewModel>)))
                .IgnoreArguments()
                .Return(_errorInfoViewModelCollection);

            _sut = new ErrorInfoContainerViewModel(_model, _entityCollectionViewModelFactory, _errorInfoViewModelFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Assert
            Assert.AreEqual(_errorInfoViewModelCollection, _sut.ErrorInfoViewModelCollection);
            Assert.IsNotNull(_sut.ErrorInfoViewModelCollection);
        }

        [Test]
        public void Description_Returns_ExpectedValue()
        {
            //Arrange
            const string description = "sdsda";
            _model.Expect(c => c.Description).Return(description);

            //Act
            var result = _sut.Description;

            //Assert
            Assert.AreEqual(description, result);
        }

        #region IExpandable

        [Test]
        public void IsExpanded_Sets_Correctly()
        {
            TestHelpers.AssertPropertyChanged(_sut, c => _sut.IsExpanded = c, false, true, nameof(_sut.IsExpanded));
        }

        #endregion IExpandable
    }
}
