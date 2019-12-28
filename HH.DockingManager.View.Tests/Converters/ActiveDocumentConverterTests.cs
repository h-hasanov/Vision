using System.Collections.Generic;
using System.Windows.Data;
using HH.DockingManager.View.Converters;
using HH.DockingManager.ViewModel.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.DockingManager.View.Tests.Converters
{
    [TestFixture]
    internal sealed class ActiveDocumentConverterTests
    {
        private ActiveDocumentConverter _sut;
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ActiveDocumentConverter();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Convert_Converts_Correctly()
        {
            //Arrange
            var firstItem = _autoMocker.Mock<IEditorViewModel>();
            var expectedFirstItem = firstItem;

            var secondItem = new List<object>();
            var expectedSecondItem = Binding.DoNothing;

            //Assert
            Assert.AreEqual(expectedFirstItem, _sut.Convert(firstItem, null, null, null));
            Assert.AreEqual(expectedSecondItem, _sut.Convert(secondItem, null, null, null));
        }

        [Test]
        public void ConvertBack_Converts_Correctly()
        {
            //Arrange
            var firstItem = _autoMocker.Mock<IEditorViewModel>();
            var expectedFirstItem = firstItem;

            var secondItem = new List<object>();
            var expectedSecondItem = Binding.DoNothing;

            //Assert
            Assert.AreEqual(expectedFirstItem, _sut.ConvertBack(firstItem, null, null, null));
            Assert.AreEqual(expectedSecondItem, _sut.ConvertBack(secondItem, null, null, null));
        }
    }
}
