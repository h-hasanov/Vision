using System;
using HH.ViewModel.Services.Interfaces;
using HH.ViewModel.Services.Services;
using NUnit.Framework;

namespace HH.ViewModel.Services.Tests.Services
{
    [TestFixture]
    internal sealed class ClipboardSizeTests
    {
        [Test]
        public void Ctor_Sets_Properties()
        {
            //Arrange
            const int numberOfRows = 3;
            const int numberOfColumns = 124;

            //Act
            var sut = new ClipboardSize(numberOfRows, numberOfColumns);

            //Assert
            Assert.AreEqual(numberOfRows, sut.NumberOfRows);
            Assert.AreEqual(numberOfColumns, sut.NumberOfColumns);
        }

        [TestCase(-1, 10)]
        [TestCase(10, -1)]
        [TestCase(-2, -1)]
        public void Ctor_Throws_If_Size_Invalid(int numberOfRows, int numberOfColumns)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new ClipboardSize(numberOfRows, numberOfColumns));
        }
    }
}
