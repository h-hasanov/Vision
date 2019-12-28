using System;
using System.Windows.Input;
using HH.ErrorManager.Model.Enums;
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
    internal sealed class ErrorInfoViewModelTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfo _errorInfo;
        private ICommandFactory _commandFactory;
        private IErrorInfoViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _errorInfo = _autoMocker.Mock<IErrorInfo>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();

            _sut = new ErrorInfoViewModel(_errorInfo, _commandFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        #region Commands

        [Test]
        public void NavigateToErrorCommand_Returns_Expected_Command()
        {
            //Arrange
            var command = _autoMocker.Mock<ICommand>();
            _commandFactory.Expect(c => c.CreateCommand(_sut.NavigateToError))
             .Repeat.Once()
             .Return(command);

            //Act and Assert
            TestHelpers.AssertCommandDoesNotChange(command, () => _sut.NavigateToErrorCommand);
        }

        #endregion Commands

        #region Properties

        [Test]
        public void Severity_Returns_Expected_Severity()
        {
            //Arrange
            const ErrorSeverity severity = ErrorSeverity.Information;
            _errorInfo.Expect(c => c.Severity).Return(severity);

            //Act
            var result = _sut.Severity;

            //Assert
            Assert.AreEqual(severity, result);
        }

        [Test]
        public void Description_Returns_Expected_Description()
        {
            //Arrange
            const string description = "asdsa";
            _errorInfo.Expect(c => c.Description).Return(description);

            //Act
            var result = _sut.Description;

            //Assert
            Assert.AreEqual(description, result);
        }

        #endregion Properties

        #region Methods

        [Test]
        public void NavigateToError_Navigates_Correctly()
        {
            //Arrange
            var navigationAction = _autoMocker.Mock<Action>();
            navigationAction.Expect(c => c());
            _errorInfo.Expect(c => c.NavigateToErrorAction).Return(navigationAction);

            //Act
            _sut.NavigateToError();
        }

        #endregion Methods
    }
}
