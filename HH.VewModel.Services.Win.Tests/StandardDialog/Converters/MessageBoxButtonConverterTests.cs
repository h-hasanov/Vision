using System.Collections.Generic;
using HH.TestUtils;
using HH.ViewModel.Services.StandardDialog.Enums;
using HH.ViewModel.Services.Win.StandardDialog.Converters;
using NUnit.Framework;

namespace HH.ViewModel.Services.Win.Tests.StandardDialog.Converters
{
    [TestFixture]
    internal sealed class MessageBoxButtonConverterTests
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
        public void Convert_Converts_Correctly()
        {
            //Arrange
            var mapper = new Dictionary<MessageBoxButton, System.Windows.MessageBoxButton>
            {
                {MessageBoxButton.Ok, System.Windows.MessageBoxButton.OK},
                {MessageBoxButton.OkCancel, System.Windows.MessageBoxButton.OKCancel},
                {MessageBoxButton.YesNoCancel, System.Windows.MessageBoxButton.YesNoCancel},
                {MessageBoxButton.YesNo, System.Windows.MessageBoxButton.YesNo}
            };

            //Act & Assert
            foreach (var messageBoxResult in mapper)
            {
                Assert.AreEqual(messageBoxResult.Value, MessageBoxButtonConverter.Convert(messageBoxResult.Key));
            }
        }
    }
}
