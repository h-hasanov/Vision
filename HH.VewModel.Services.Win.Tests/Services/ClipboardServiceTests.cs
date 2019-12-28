using System.Threading;
using System.Windows;
using HH.ViewModel.Services.Interfaces;
using HH.ViewModel.Services.Win.Services;
using NUnit.Framework;

namespace HH.ViewModel.Services.Win.Tests.Services
{
    [TestFixture]
    internal sealed class ClipboardServiceTests
    {
        private IClipboardService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ClipboardService();
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void GetClipBoardTextSize_Returns_Expected_Size()
        {
            //Arrange
            const int expectedRows = 2;
            const int expectedColumns = 3;

            Clipboard.SetText("1,2,\r\n,,\r\n", TextDataFormat.CommaSeparatedValue);

            //Act
            var size = _sut.GetClipBoardTextSize();

            //Assert
            Assert.AreEqual(expectedRows, size.NumberOfRows);
            Assert.AreEqual(expectedColumns, size.NumberOfColumns);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void GetClipBoardText_With_CsvData_Gets_Correct_Values()
        {
            //Arrange
            Clipboard.SetText("1,2,\r\n,,\r\n3,,5\r\n", TextDataFormat.CommaSeparatedValue);
            var expectedData = new[]
            {
                new[] {"1", "2", ""},
                new[] {"", "", ""},
                new[] {"3", "", "5"}
            };

            //Act
            var result = _sut.GetClipBoardText();

            //Assert
            var rowCounter = 0;
            foreach (var row in result)
            {
                CollectionAssert.AreEqual(expectedData[rowCounter], row);
                rowCounter++;
            }
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void GetClipBoardText_With_TabTextData_Gets_Correct_Values()
        {
            //Arrange
            Clipboard.SetText("1\t2\t\r\n\t\t\r\n3\t\t5", TextDataFormat.Text);
            var expectedData = new[]
            {
                new[] {"1", "2", ""},
                new[] {"", "", ""},
                new[] {"3", "", "5"}
            };

            //Act
            var result = _sut.GetClipBoardText();

            //Assert
            var rowCounter = 0;
            foreach (var row in result)
            {
                CollectionAssert.AreEqual(expectedData[rowCounter], row);
                rowCounter++;
            }
        }
    }
}
