using System.Windows.Input;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ViewModel.Services.Tests.ModalDialog.Implementations
{
    [TestFixture]
    internal sealed class OkApplyCancelDialogViewModelTests
    {
        private AutoMocker _autoMocker;
        private IEditableDialogContent _content;
        private IDialogSettings _dialogSettings;
        private ICommandFactory _commandFactory;
        private IOkApplyCancelDialogViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _content = _autoMocker.Mock<IEditableDialogContent>();
            _dialogSettings = _autoMocker.Mock<IDialogSettings>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();

            _sut = new OkApplyCancelDialogViewModel(_commandFactory, _content, _dialogSettings);
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
        public void Apply_Returns_ExpectedCommand()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.Apply, _content.CanAcceptChanges))
                    .Repeat.Once()
                    .Return(command);

            //Act & Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.ApplyCommand);
        }

        [Test]
        public void Apply_AcceptsChanges()
        {
            //Arrange
            _content.Expect(c => c.AcceptChanges());

            //Act
            _sut.Ok();
        }
    }
}
