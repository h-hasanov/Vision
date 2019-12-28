using System;
using HH.ErrorManager.Model.Collections.Implementations;
using HH.ErrorManager.Model.Collections.Interfaces;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ErrorManager.Model.Tests.Collections.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoCollectionTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoFactory _errorInfoFactory;
        private IErrorInfoCollection _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _errorInfoFactory = _autoMocker.Mock<IErrorInfoFactory>();

            _sut = new ErrorInfoCollection(_errorInfoFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void AddError_Adds_Error_Correctly()
        {
            //Arrange
            const string description = "asdas";
            var errorInfo = _autoMocker.Mock<IErrorInfo>();
            var navigateToErrorAction = _autoMocker.Mock<Action>();
            _errorInfoFactory.Expect(c => c.CreateError(description, navigateToErrorAction)).Return(errorInfo);

            //Act
            _sut.AddError(description, navigateToErrorAction);

            //Assert
            Assert.IsTrue(_sut.Contains(errorInfo));
        }

        [Test]
        public void AddInformation_Adds_Information_Correctly()
        {
            //Arrange
            const string description = "asdas";
            var errorInfo = _autoMocker.Mock<IErrorInfo>();
            var navigateToErrorAction = _autoMocker.Mock<Action>();
            _errorInfoFactory.Expect(c => c.CreateInformation(description, navigateToErrorAction)).Return(errorInfo);

            //Act
            _sut.AddInformation(description, navigateToErrorAction);

            //Assert
            Assert.IsTrue(_sut.Contains(errorInfo));
        }

        [Test]
        public void AddWarning_Adds_Warning_Correctly()
        {
            //Arrange
            const string description = "asdas";
            var errorInfo = _autoMocker.Mock<IErrorInfo>();
            var navigateToErrorAction = _autoMocker.Mock<Action>();
            _errorInfoFactory.Expect(c => c.CreateWarning(description, navigateToErrorAction)).Return(errorInfo);

            //Act
            _sut.AddWarning(description, navigateToErrorAction);

            //Assert
            Assert.IsTrue(_sut.Contains(errorInfo));
        }
    }
}
