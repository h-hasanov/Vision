using System;
using System.Windows.Input;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ViewModel.Services.Tests.ModalDialog.Implementations
{
    [TestFixture]
    internal sealed class OkDialogViewModelTests
    {
        private AutoMocker _autoMocker;
        private IViewModel _content;
        private IDialogSettings _dialogSettings;
        private ICommandFactory _commandFactory;
        private IOkDialogViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _content = _autoMocker.Mock<IViewModel>();
            _dialogSettings = _autoMocker.Mock<IDialogSettings>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();

            _sut = new OkDialogViewModel(_commandFactory, _content, _dialogSettings);
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
            Assert.AreEqual(_content, _sut.Content);
            Assert.AreEqual(_dialogSettings, _sut.DialogSettings);
        }

        [Test]
        public void OkCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.Ok))
                .Repeat.Once()
                .Return(command);

            //Act & Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.OkCommand);
        }

        [Test]
        public void Ok_Closes_Dialog()
        {
            //Arrange
            var onClosed = _autoMocker.Mock<EventHandler<DialogResult>>();
            onClosed.Expect(c => c(_sut, DialogResult.Ok));
            _sut.Closed += onClosed;

            //Act
            _sut.Ok();
        }
    }
}
