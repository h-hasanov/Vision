using HH.Extensions.Enums;
using HH.Extensions.Tests.Resources;
using HH.Presentation.Attributes;
using NUnit.Framework;

namespace HH.Extensions.Tests.Enums
{
    [TestFixture]
    internal sealed class EnumExtensionsTests
    {
        [TestCase(LocalizableTestEnum.HasDescription, "I have a descrription Too")]
        [TestCase(LocalizableTestEnum.NoDescription, "NoDescription")]
        [TestCase(LocalizableTestEnum.DescriptionNotFound, "[[]]")]
        public void GetDescription_Returns_CorrectDescription_WhenLocalized(LocalizableTestEnum @enum, string description)
        {
            //Assert
            Assert.AreEqual(description, @enum.GetDescription());
        }

        [Test]
        public void GetValues_Returns_Correct_Values()
        {
            //Arrange
            var expectedEnumValues = new[]
            {
                LocalizableTestEnum.HasDescription, LocalizableTestEnum.NoDescription,
                LocalizableTestEnum.DescriptionNotFound
            };

            //Act
            var result = EnumExtensions.GetValues<LocalizableTestEnum>();

            //Assert
            CollectionAssert.AreEquivalent(expectedEnumValues, result);
        }

        #region Helpers

        public enum LocalizableTestEnum : byte
        {
            [LocalizedDescription("HasDescription", typeof(EnumResources))]
            HasDescription,

            NoDescription,

            [LocalizedDescription("", typeof(EnumResources))]
            DescriptionNotFound
        }

        #endregion
    }
}
