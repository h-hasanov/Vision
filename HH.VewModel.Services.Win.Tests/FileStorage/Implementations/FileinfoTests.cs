using System.IO;
using HH.TestUtils;
using HH.ViewModel.Services.FileStorage.Interfaces;
using HH.ViewModel.Services.Win.FileStorage.Implementations;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ViewModel.Services.Win.Tests.FileStorage.Implementations
{
    [TestFixture]
    internal sealed class FileinfoTests
    {
        private AutoMocker _autoMocker;
        private FileInfo _fileInfo;
        private IFileInfo _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _fileInfo = _autoMocker.Mock<FileInfo>();
            _sut = new Fileinfo(_fileInfo);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void FullName_Returns_Source_FullName()
        {
            //Arrange
            const string fullName = "c:\\asd\\somewhere\\a.xml";
            _fileInfo.Expect(c => c.FullName).Return(fullName);

            //Act
            var result = _sut.FullName;

            //Assert
            Assert.AreEqual(fullName, result);
        }
    }
}
