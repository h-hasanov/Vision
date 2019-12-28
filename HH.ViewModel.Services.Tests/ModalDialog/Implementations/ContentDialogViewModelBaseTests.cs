using HH.TestUtils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using NUnit.Framework;

namespace HH.ViewModel.Services.Tests.ModalDialog.Implementations
{
    [TestFixture]
    internal sealed class ContentDialogViewModelBaseTests
    {
        private AutoMocker _autoMocker;
        private IViewModel _content;
        private IDialogSettings _dialogSettings;
        private IContentDialogViewModel<IViewModel, DialogResult> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _content = _autoMocker.Mock<IViewModel>();
            _dialogSettings = _autoMocker.Mock<IDialogSettings>();

            _sut = new TestContentDialogViewModel(_content, _dialogSettings);
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
        }
    }

    internal sealed class TestContentDialogViewModel : ContentDialogViewModelBase<IViewModel, DialogResult>
    {
        public TestContentDialogViewModel(IViewModel content, IDialogSettings dialogSettings)
            : base(content, dialogSettings)
        {
        }
    }
}
