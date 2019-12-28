using HH.TestUtils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.Wizard.Interfaces;
using NUnit.Framework;

namespace HH.ViewModel.Services.Tests.ModalDialog.Implementations
{
    [TestFixture]
    internal sealed class DialogViewModelFactoryTests
    {
        private AutoMocker _autoMocker;
        private ICommandFactory _commandFactory;
        private IDialogViewModelFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();

            _sut = new DialogViewModelFactory(_commandFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateOkDialogViewModel_Creats_OkDialogViewModel()
        {
            //Arrange
            var viewModel = _autoMocker.Mock<IViewModel>();
            var dialogSettings = _autoMocker.Mock<IDialogSettings>();

            //Act
            var result = _sut.CreateOkDialogViewModel(viewModel, dialogSettings);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateOkCancelDialogViewModel_Creats_OkCancelDialogViewModel()
        {
            //Arrange
            var viewModel = _autoMocker.Mock<IEditableDialogContent>();
            var dialogSettings = _autoMocker.Mock<IDialogSettings>();

            //Act
            var result = _sut.CreateOkCancelDialogViewModel(viewModel, dialogSettings);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateOkApplyCancelDialogViewModel_Creats_OkApplyCancelDialogViewModel()
        {
            //Arrange
            var viewModel = _autoMocker.Mock<IEditableDialogContent>();
            var dialogSettings = _autoMocker.Mock<IDialogSettings>();

            //Act
            var result = _sut.CreateOkApplyCancelDialogViewModel(viewModel, dialogSettings);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateWizardViewModel_Creates_WizardViewModel()
        {
            //Arrange
            var steps = new[] {_autoMocker.Mock<IWizardStepViewModel>()};
            var dialogSettings = _autoMocker.Mock<IDialogSettings>();

            //Act
            var result = _sut.CreateWizardViewModel(steps, dialogSettings);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
