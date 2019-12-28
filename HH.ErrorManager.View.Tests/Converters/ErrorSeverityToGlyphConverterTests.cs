using System.Windows.Data;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.View.Converters;
using HH.Icons.Model.Enums;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.ErrorManager.View.Tests.Converters
{
    [TestFixture]
    internal sealed class ErrorSeverityToGlyphConverterTests
    {
        private AutoMocker _autoMocker;
        private IValueConverter _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new ErrorSeverityToGlyphConverter();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(ErrorSeverity.Error, GlyphType.Close)]
        [TestCase(ErrorSeverity.Information, GlyphType.Information)]
        [TestCase(ErrorSeverity.Warning, GlyphType.Warning)]
        public void Convert_Converts_Correctly(ErrorSeverity severity, GlyphType expectedGlyph)
        {
            //Act
            var result = (GlyphType)_sut.Convert(severity, null, null, null);

            //Assert
            Assert.AreEqual(expectedGlyph, result);
        }
    }
}
