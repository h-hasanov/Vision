using System.Collections.Generic;
using HH.TestUtils;
using HH.ViewModel.Services.StandardDialog.Enums;
using HH.ViewModel.Services.Win.StandardDialog.Converters;
using NUnit.Framework;

namespace HH.ViewModel.Services.Win.Tests.StandardDialog.Converters
{
    [TestFixture]
    internal sealed class MessageBoxResultConverterTests
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
            var mapper = new Dictionary<MessageBoxResult, System.Windows.MessageBoxResult>
            {
                {MessageBoxResult.None, System.Windows.MessageBoxResult.None},
                {MessageBoxResult.OK, System.Windows.MessageBoxResult.OK},
                {MessageBoxResult.Cancel, System.Windows.MessageBoxResult.Cancel},
                {MessageBoxResult.Yes, System.Windows.MessageBoxResult.Yes},
                {MessageBoxResult.No, System.Windows.MessageBoxResult.No},
            };

            //Act & Assert
            foreach (var messageBoxResult in mapper)
            {
                Assert.AreEqual(messageBoxResult.Value, MessageBoxResultConverter.Convert(messageBoxResult.Key));
            }
        }

        [Test]
        public void Convert_Converts_Correctly_Backwards()
        {
            //Arrange
            var mapper = new Dictionary<System.Windows.MessageBoxResult, MessageBoxResult>
            {
                {System.Windows.MessageBoxResult.None, MessageBoxResult.None},
                {System.Windows.MessageBoxResult.OK, MessageBoxResult.OK},
                {System.Windows.MessageBoxResult.Cancel, MessageBoxResult.Cancel},
                {System.Windows.MessageBoxResult.Yes, MessageBoxResult.Yes},
                {System.Windows.MessageBoxResult.No, MessageBoxResult.No},
            };

            //Act & Assert
            foreach (var messageBoxResult in mapper)
            {
                Assert.AreEqual(messageBoxResult.Value, MessageBoxResultConverter.Convert(messageBoxResult.Key));
            }
        }
    }
}
