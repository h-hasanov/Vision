using System.Collections.Generic;
using HH.TestUtils;
using HH.ViewModel.Services.StandardDialog.Enums;
using HH.ViewModel.Services.Win.StandardDialog.Converters;
using NUnit.Framework;

namespace HH.ViewModel.Services.Win.Tests.StandardDialog.Converters
{
    [TestFixture]
    internal sealed class MessageBoxImageConverterTests
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
            var mapper = new Dictionary<MessageBoxImage, System.Windows.MessageBoxImage>
            {
                {MessageBoxImage.None, System.Windows.MessageBoxImage.None},
                {MessageBoxImage.Hand, System.Windows.MessageBoxImage.Hand},
                {MessageBoxImage.Stop, System.Windows.MessageBoxImage.Stop},
                {MessageBoxImage.Error, System.Windows.MessageBoxImage.Error},
                {MessageBoxImage.Question, System.Windows.MessageBoxImage.Question},
                {MessageBoxImage.Exclamation, System.Windows.MessageBoxImage.Exclamation},
                {MessageBoxImage.Warning, System.Windows.MessageBoxImage.Warning},
                {MessageBoxImage.Asterisk, System.Windows.MessageBoxImage.Asterisk},
                {MessageBoxImage.Information, System.Windows.MessageBoxImage.Information}
            };

            //Act & Assert
            foreach (var messageBoxResult in mapper)
            {
                Assert.AreEqual(messageBoxResult.Value, MessageBoxImageConverter.Convert(messageBoxResult.Key));
            }
        }
    }
}
