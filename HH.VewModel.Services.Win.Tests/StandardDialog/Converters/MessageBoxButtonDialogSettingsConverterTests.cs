using HH.TestUtils;
using HH.ViewModel.Services.StandardDialog.Enums;
using HH.ViewModel.Services.Win.StandardDialog.Converters;
using MahApps.Metro.Controls.Dialogs;
using NUnit.Framework;

namespace HH.ViewModel.Services.Win.Tests.StandardDialog.Converters
{
    [TestFixture]
    internal sealed class MessageBoxButtonDialogSettingsConverterTests
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
        public void CreateDialogSettings_With_Ok_Creates_Expected_Settings()
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.Ok;

            //Act
            var result = button.CreateDialogSettings();

            //Assert
            Assert.AreEqual(MessageDialogStyle.Affirmative, result.Item1);
            Assert.AreEqual(Resources.Ok, result.Item2.AffirmativeButtonText);
        }

        [Test]
        public void CreateDialogSettings_With_OkCancel_Creates_Expected_Settings()
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.OkCancel;

            //Act
            var result = button.CreateDialogSettings();

            //Assert
            Assert.AreEqual(MessageDialogStyle.AffirmativeAndNegative, result.Item1);
            Assert.AreEqual(Resources.Ok, result.Item2.AffirmativeButtonText);
            Assert.AreEqual(Resources.Cancel, result.Item2.NegativeButtonText);
        }

        [Test]
        public void CreateDialogSettings_With_YesNo_Creates_Expected_Settings()
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.YesNo;

            //Act
            var result = button.CreateDialogSettings();

            //Assert
            Assert.AreEqual(MessageDialogStyle.AffirmativeAndNegative, result.Item1);
            Assert.AreEqual(Resources.Yes, result.Item2.AffirmativeButtonText);
            Assert.AreEqual(Resources.No, result.Item2.NegativeButtonText);
        }

        [Test]
        public void CreateDialogSettings_With_YesNoCancel_Creates_Expected_Settings()
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.YesNoCancel;

            //Act
            var result = button.CreateDialogSettings();

            //Assert
            Assert.AreEqual(MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, result.Item1);
            Assert.AreEqual(Resources.Yes, result.Item2.AffirmativeButtonText);
            Assert.AreEqual(Resources.No, result.Item2.NegativeButtonText);
            Assert.AreEqual(Resources.Cancel, result.Item2.FirstAuxiliaryButtonText);
        }

        [Test]
        public void ConvertToMessageBoxResult_With_Ok_Converts_Correctly()
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.Ok;
            const MessageDialogResult dialogResult = MessageDialogResult.Affirmative;

            //Act
            var result = dialogResult.ConvertToMessageBoxResult(button);

            //Assert
            Assert.AreEqual(MessageBoxResult.OK, result);
        }

        [TestCase(MessageDialogResult.Affirmative, MessageBoxResult.OK)]
        [TestCase(MessageDialogResult.Negative, MessageBoxResult.Cancel)]
        public void ConvertToMessageBoxResult_With_OkCancel_Converts_Correctly(MessageDialogResult dialogResult, MessageBoxResult expectedResult)
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.OkCancel;

            //Act
            var result = dialogResult.ConvertToMessageBoxResult(button);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(MessageDialogResult.Affirmative, MessageBoxResult.Yes)]
        [TestCase(MessageDialogResult.Negative, MessageBoxResult.No)]
        public void ConvertToMessageBoxResult_With_YesNo_Converts_Correctly(MessageDialogResult dialogResult, MessageBoxResult expectedResult)
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.YesNo;

            //Act
            var result = dialogResult.ConvertToMessageBoxResult(button);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(MessageDialogResult.Affirmative, MessageBoxResult.Yes)]
        [TestCase(MessageDialogResult.Negative, MessageBoxResult.No)]
        [TestCase(MessageDialogResult.FirstAuxiliary, MessageBoxResult.Cancel)]
        public void ConvertToMessageBoxResult_With_YesNoCancel_Converts_Correctly(MessageDialogResult dialogResult, MessageBoxResult expectedResult)
        {
            //Arrange
            const MessageBoxButton button = MessageBoxButton.YesNoCancel;

            //Act
            var result = dialogResult.ConvertToMessageBoxResult(button);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
