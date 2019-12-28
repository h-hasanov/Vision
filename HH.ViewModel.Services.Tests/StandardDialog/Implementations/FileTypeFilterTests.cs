using HH.TestUtils;
using HH.ViewModel.Services.StandardDialog.Implementations;
using NUnit.Framework;

namespace HH.ViewModel.Services.Tests.StandardDialog.Implementations
{
    [TestFixture]
    internal sealed class FileTypeFilterTests
    {
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Arrange
            const string extension = ".b";
            const string description = "a";

            //Act
            var result = new FileTypeFilter(extension, description);

            //Assert
            Assert.AreEqual(extension, result.Extension);
            Assert.AreEqual(description, result.Description);
        }
    }
}
