using System;
using System.Windows.Input;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Implementations;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ErrorManager.ViewModel.Tests.Implementations
{
    [TestFixture]
    internal sealed class ErrorManagerViewModelTests
    {
        private AutoMocker _autoMocker;
        private IEntityCollectionViewModelFactory _entityCollectionViewModelFactory;
        private IErrorManager _errorManager;
        private IReadOnlyEntityCollectionViewModel<IErrorInfoContainer, IErrorInfoContainerViewModel> _errorInfoContainerViewModelCollection;
        private IErrorInfoContainerViewModelFactory _errorInfoContainerViewModelFactory;
        private ICommandFactory _commandFactory;
        private IErrorManagerViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _entityCollectionViewModelFactory = _autoMocker.Mock<IEntityCollectionViewModelFactory>();
            _errorManager = _autoMocker.Mock<IErrorManager>();
            _errorInfoContainerViewModelCollection = _autoMocker.Mock<IReadOnlyEntityCollectionViewModel<IErrorInfoContainer, IErrorInfoContainerViewModel>>();
            _errorInfoContainerViewModelFactory = _autoMocker.Mock<IErrorInfoContainerViewModelFactory>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();

            _entityCollectionViewModelFactory.Expect(
             c => c.CreateReadOnlyEntityCollectionViewModel(default(IReadOnlyEntityCollection<IErrorInfoContainer>), default(Func<IErrorInfoContainer, IErrorInfoContainerViewModel>)))
             .IgnoreArguments()
             .Return(_errorInfoContainerViewModelCollection);

            _sut = new ErrorManagerViewModel(_errorManager, _entityCollectionViewModelFactory,
                _errorInfoContainerViewModelFactory, _commandFactory);
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
            Assert.AreEqual(_errorInfoContainerViewModelCollection, _sut.ErrorInfoContainerViewModelCollection);
            Assert.IsNotNull(_sut.ErrorInfoContainerViewModelCollection);
        }

        #region Commands

        [Test]
        public void ExpandAllCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.ExpandAll, _sut.CanExpandAll))
                    .Repeat.Once()
                    .Return(command);

            //Act
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.ExpandAllCommand);
        }

        [Test]
        public void CollapseAllCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.CollapseAll, _sut.CanCollapseAll))
                    .Repeat.Once()
                    .Return(command);

            //Act
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.CollapseAllCommand);
        }

        #endregion Commands

        #region Expand/Collapse

        [Test]
        public void ExpandAll_Expands_Correctly()
        {
            //Arrange
            var itemOne = _autoMocker.Mock<IErrorInfoContainerViewModel>();
            itemOne.Expect(c => c.IsExpanded = true);
            var itemTwo = _autoMocker.Mock<IErrorInfoContainerViewModel>();
            itemTwo.Expect(c => c.IsExpanded = true);
            _errorInfoContainerViewModelCollection.StubEnumerator(new[] { itemOne, itemTwo });

            //Act
            _sut.ExpandAll();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanExpandAll_Returns_Expected_Value(bool canExpand)
        {
            //Arrange
            if (canExpand)
            {
                var itemOne = _autoMocker.Mock<IErrorInfoContainerViewModel>();
                _errorInfoContainerViewModelCollection.StubEnumerator(new[] { itemOne });
            }
            else
            {
                _errorInfoContainerViewModelCollection.StubEnumerator();
            }

            //Act
            var result = _sut.CanExpandAll();

            //Assert
            Assert.AreEqual(canExpand, result);
        }

        [Test]
        public void CollapseAll_Collapses_Correctly()
        {
            //Arrange
            var itemOne = _autoMocker.Mock<IErrorInfoContainerViewModel>();
            itemOne.Expect(c => c.IsExpanded = false);
            var itemTwo = _autoMocker.Mock<IErrorInfoContainerViewModel>();
            itemTwo.Expect(c => c.IsExpanded = false);
            _errorInfoContainerViewModelCollection.StubEnumerator(new[] { itemOne, itemTwo });

            //Act
            _sut.CollapseAll();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanCollapseAll_Returns_Expected_Value(bool canCollapse)
        {
            //Arrange
            if (canCollapse)
            {
                var itemOne = _autoMocker.Mock<IErrorInfoContainerViewModel>();
                _errorInfoContainerViewModelCollection.StubEnumerator(new[] { itemOne });
            }
            else
            {
                _errorInfoContainerViewModelCollection.StubEnumerator();
            }

            //Act
            var result = _sut.CanCollapseAll();

            //Assert
            Assert.AreEqual(canCollapse, result);
        }

        #endregion Expand/Collapse
    }
}
