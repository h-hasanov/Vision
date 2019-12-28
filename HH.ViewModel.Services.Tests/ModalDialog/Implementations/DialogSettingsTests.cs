using HH.TestUtils;
using HH.ViewModel.Services.ModalDialog.Implementations;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using NUnit.Framework;

namespace HH.ViewModel.Services.Tests.ModalDialog.Implementations
{
    [TestFixture]
    internal sealed class DialogSettingsTests
    {
        private AutoMocker _autoMocker;
        private IDialogSettings _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new DialogSettings();
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
            Assert.AreEqual("No Title", _sut.Title);
        }

        [Test]
        public void Title_Sets_Correctly()
        {
            //Act & Assert
            TestHelpers.AssertPropertyChanged(_sut, c => _sut.Title = c, "No Title", "asd", nameof(_sut.Title));
        }
    }
}
