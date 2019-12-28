using System;
using System.Windows.Input;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.Wizard.Implementations;
using HH.ViewModel.Services.Wizard.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ViewModel.Services.Tests.Wizard.Implementations
{
    [TestFixture]
    internal sealed class WizardViewModelTests
    {
        private AutoMocker _autoMocker;
        private IWizardStepViewModel _stepOne;
        private IWizardStepViewModel _stepTwo;
        private IWizardStepViewModel _stepThree;
        private IWizardStepViewModel[] _allSteps;
        private IDialogSettings _dialogSettings;
        private ICommandFactory _commandFactory;
        private const string OriginalTitle = "Test Wizard";
        private IWizardViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _stepOne = _autoMocker.Mock<IWizardStepViewModel>();
            _stepTwo = _autoMocker.Mock<IWizardStepViewModel>();
            _stepThree = _autoMocker.Mock<IWizardStepViewModel>();
            _dialogSettings = _autoMocker.Mock<IDialogSettings>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();
            _dialogSettings = new DialogSettings { Title = OriginalTitle };

            _allSteps = new[] { _stepOne, _stepTwo, _stepThree };

            _sut = new WizardViewModel(_dialogSettings, _allSteps, _commandFactory);
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
            Assert.AreEqual(_stepOne, _sut.CurrentStep);
            CollectionAssert.AreEqual(_allSteps, _sut.Steps);
            Assert.AreEqual($"{OriginalTitle} - Step 1 of 3", _sut.DialogSettings.Title);
        }

        #region Commands

        [Test]
        public void CancelCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.Cancel))
                .Repeat.Once()
                .Return(command);

            //Act & Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.CancelCommand);
        }

        [Test]
        public void MoveBackCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.MoveBack, _sut.CanMoveBack))
                .Repeat.Once()
                .Return(command);

            //Act & Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.MoveBackCommand);
        }

        [Test]
        public void MoveNextCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.MoveNext, _sut.CanMoveNext))
                .Repeat.Once()
                .Return(command);

            //Act & Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.MoveNextCommand);
        }

        [Test]
        public void FinishCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.Finish, _sut.CanFinish))
                .Repeat.Once()
                .Return(command);

            //Act & Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.FinishCommand);
        }

        #endregion Commands

        #region Cancel

        [Test]
        public void Cancel_Cancels_Correctly()
        {
            //Arrange
            var onClose = _autoMocker.Mock<EventHandler<DialogResult>>();
            onClose.Expect(c => c(_sut, DialogResult.Cancel));
            _sut.Closed += onClose;

            //Act
            _sut.Cancel();
        }

        #endregion Cancel

        #region MoveBack

        [Test]
        public void CanMoveBack_Returns_False_If_CurrentStep_Is_FirstStep()
        {
            //Arrange
            Assert.AreEqual(_stepOne, _sut.CurrentStep);

            //Act
            var result = _sut.CanMoveBack();

            //Assert
            Assert.IsFalse(result);
            _stepOne.AssertWasNotCalled(c => c.CanAcceptChanges());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanMoveBack_Returns_ExpectedValue_If_CurrentStep_Not_FirstStep(bool canMoveBack)
        {
            //Arrange
            _sut.MoveNext();
            _stepTwo.Expect(c => c.CanAcceptChanges()).Return(canMoveBack);

            //Act
            var result = _sut.CanMoveBack();

            //Assert
            Assert.AreEqual(canMoveBack, result);
        }

        [Test]
        public void MoveBack_MovesBack_Correctly()
        {
            //Arrange
            _stepOne.Expect(c => c.AcceptChanges());
            Assert.AreEqual(_stepOne, _sut.CurrentStep);

            _sut.MoveNext();
            Assert.AreEqual(_stepTwo, _sut.CurrentStep);
            Assert.AreEqual($"{OriginalTitle} - Step 2 of 3", _sut.DialogSettings.Title);
            _stepTwo.Expect(c => c.AcceptChanges());

            //Act
            _sut.MoveBack();

            //Assert
            Assert.AreEqual(_stepOne, _sut.CurrentStep);
            Assert.AreEqual($"{OriginalTitle} - Step 1 of 3", _sut.DialogSettings.Title);
        }

        #endregion MoveBack

        #region MoveNext

        [Test]
        public void CanMoveNext_Returns_False_If_CurrentStep_Is_FinalStep()
        {
            //Arrange
            Assert.AreEqual(_stepOne, _sut.CurrentStep);
            _stepOne.Expect(c => c.AcceptChanges());
            _sut.MoveNext();
            _stepTwo.Expect(c => c.AcceptChanges());
            _sut.MoveNext();
            Assert.AreEqual(_stepThree, _sut.CurrentStep);

            //Act
            var result = _sut.CanMoveNext();

            //Assert
            Assert.IsFalse(result);
            _stepThree.AssertWasNotCalled(c => c.CanAcceptChanges());
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanMoveNext_Returns_Expected_Value_If_Not_FinalStep(bool canMoveNext)
        {
            //Arrange
            _stepOne.Expect(c => c.CanAcceptChanges()).Return(canMoveNext);

            //Act
            var result = _sut.CanMoveNext();

            //Assert
            Assert.AreEqual(canMoveNext, result);
        }

        [Test]
        public void MoveNext_MovesNext_Correctly()
        {
            //Arrange
            Assert.AreEqual(_stepOne, _sut.CurrentStep);
            _stepOne.Expect(c => c.AcceptChanges());

            //Act
            _sut.MoveNext();

            //Assert
            Assert.AreEqual(_stepTwo, _sut.CurrentStep);
            Assert.AreEqual($"{OriginalTitle} - Step 2 of 3", _sut.DialogSettings.Title);
        }

        #endregion MoveNext

        #region Finish

        [Test]
        public void CanFinish_Returns_False_If_Not_FinalStep()
        {
            //Arrange
            Assert.AreEqual(_stepOne, _sut.CurrentStep);

            //Act
            var result = _sut.CanFinish();

            //Assert
            Assert.IsFalse(result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CanFinish_Returns_Expected_Value_If_Final_Step(bool canFinish)
        {
            //Arrange
            Assert.AreEqual(_stepOne, _sut.CurrentStep);
            _stepOne.Expect(c => c.AcceptChanges());
            _sut.MoveNext();
            _stepTwo.Expect(c => c.AcceptChanges());
            _sut.MoveNext();
            Assert.AreEqual(_stepThree, _sut.CurrentStep);
            _stepThree.Expect(c => c.CanAcceptChanges()).Return(canFinish);

            //Act
            var result = _sut.CanFinish();

            //Assert
            Assert.AreEqual(canFinish, result);
        }

        [Test]
        public void Finish_Finishes_Correctly()
        {
            //Arrange
            Assert.AreEqual(_stepOne, _sut.CurrentStep);
            _stepOne.Expect(c => c.AcceptChanges());
            _sut.MoveNext();
            _stepTwo.Expect(c => c.AcceptChanges());
            _sut.MoveNext();
            Assert.AreEqual(_stepThree, _sut.CurrentStep);
            Assert.AreEqual($"{OriginalTitle} - Step 3 of 3", _sut.DialogSettings.Title);

            var onClose = _autoMocker.Mock<EventHandler<DialogResult>>();
            onClose.Expect(c => c(_sut, DialogResult.Ok));
            _sut.Closed += onClose;
            _stepThree.Expect(c => c.AcceptChanges());

            //Act
            _sut.Finish();
        }

        #endregion Finish
    }
}
