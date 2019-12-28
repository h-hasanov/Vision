using System;
using HH.TestUtils;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ViewModel.Services.Tests.ModalDialog.Implementations
{
    [TestFixture]
    internal sealed class DialogViewModelBaseTests
    {
        private AutoMocker _autoMocker;
        private IDialogSettings _dialogSettings;
        private IDialogViewModel<DialogResult> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _dialogSettings = _autoMocker.Mock<IDialogSettings>();

            _sut = new TestDialogViewModel(_dialogSettings);
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
            Assert.AreEqual(_dialogSettings, _sut.DialogSettings);
        }

        [TestCase(DialogResult.Ok)]
        [TestCase(DialogResult.Cancel)]
        public void Close_Closes_Correctly_Dialog(DialogResult dialogResult)
        {
            //Arrange
            var onClosed = _autoMocker.Mock<EventHandler<DialogResult>>();
            onClosed.Expect(c => c(_sut, dialogResult));
            _sut.Closed += onClosed;

            _sut.WaitUntilClosed().ContinueWith(c =>
            {
                Assert.AreEqual(dialogResult, c.Result);
            });

            //Act
            ((TestDialogViewModel)_sut).CloseCall(dialogResult);
        }
    }

    internal sealed class TestDialogViewModel : DialogViewModelBase<DialogResult>
    {
        public TestDialogViewModel(IDialogSettings dialogSettings)
            : base(dialogSettings)
        {
        }

        public void CloseCall(DialogResult dialogResult)
        {
            Close(dialogResult);
        }
    }
}
