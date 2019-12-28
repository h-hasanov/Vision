using System;
using System.Windows;
using HH.ErrorManager.View.Selectors;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.View.Tests.Selectors
{
    [TestFixture]
    internal sealed class ErrorStyleSelectorTests
    {
        private AutoMocker _autoMocker;
        private Style _errorInfoContainerViewModelStyle;
        private Style _errorInfoViewModelStyle;
        private ErrorStyleSelector _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _errorInfoContainerViewModelStyle = _autoMocker.Mock<Style>();
            _errorInfoViewModelStyle = _autoMocker.Mock<Style>();

            _sut = new ErrorStyleSelector
            {
                ErrorInfoContainerViewModelStyle = _errorInfoContainerViewModelStyle,
                ErrorInfoViewModelStyle = _errorInfoViewModelStyle
            };
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void SelectStyle_Selects_Correct_Style()
        {
            //Arrange
            var errorInfoViewModel = _autoMocker.Mock<IErrorInfoViewModel>();
            var errorInfoContainerViewModel = _autoMocker.Mock<IErrorInfoContainerViewModel>();

            //Act & Assert
            Assert.AreEqual(_errorInfoViewModelStyle, _sut.SelectStyle(errorInfoViewModel, null));
            Assert.AreEqual(_errorInfoContainerViewModelStyle, _sut.SelectStyle(errorInfoContainerViewModel, null));
            Assert.Throws<NotImplementedException>(() => _sut.SelectStyle(_autoMocker.Mock<ITestInterface>(), null));
        }
    }
}
