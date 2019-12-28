using HH.TestUtils;
using HH.ViewModel.Services.StandardDialog.Implementations;
using HH.ViewModel.Services.Win.StandardDialog.Converters;
using NUnit.Framework;

namespace HH.ViewModel.Services.Win.Tests.StandardDialog.Converters
{
    [TestFixture]
    internal sealed class FileTypeFilterToStringConverterTests
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
        public void ConvertToStringFilter_With_SingleFilter_Converts_Correctly()
        {
            //Arrange
            const string expectedStringFilter = "scsv files (*.scsv)|*.scsv";
            var fileTypeFilter = new FileTypeFilter(".scsv", "scsv");

            //Act
            var result1 = fileTypeFilter.ConvertToStringFilter();
            var result2 = new[] { fileTypeFilter }.ConvertToStringFilter();

            //Assert
            Assert.AreEqual(expectedStringFilter, result1);
            Assert.AreEqual(expectedStringFilter, result2);
        }

        [Test]
        public void ConvertToStringFilter_With_MultipleFilters_Converts_Correctly()
        {
            //Arrange
            const string expectedStringFilter = "scsv files (*.scsv)|*.scsv|csv files (*.csv)|*.csv|All files (*.*)|*.*";
            var fileTypeFilter1 = new FileTypeFilter(".scsv", "scsv");
            var fileTypeFilter2 = new FileTypeFilter(".csv", "csv");
            var fileTypeFilter3 = new FileTypeFilter(".*", "All");

            //Act
            var result = new[] { fileTypeFilter1, fileTypeFilter2, fileTypeFilter3 }.ConvertToStringFilter();

            //Assert
            Assert.AreEqual(expectedStringFilter, result);
        }
    }
}
