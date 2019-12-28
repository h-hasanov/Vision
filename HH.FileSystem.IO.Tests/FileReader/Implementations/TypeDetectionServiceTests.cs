using System;
using System.Globalization;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class TypeDetectionServiceTests
    {
        private AutoMocker _autoMocker;
        private ITypeDetectionService _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new TypeDetectionService();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void DetectTypes_With_PerfectData_Detects_Correctly()
        {
            //Arrange
            var data = new[]
            {
                new[] {1.ToString(), (-3).ToString()},
                new[]
                {
                    new DateTime(1992, 11, 1, 11, 15, 13).ToString(CultureInfo.InvariantCulture),
                    new DateTime(1991, 08, 11, 06, 30, 11).ToString(CultureInfo.InvariantCulture)
                },
                new[] {2.3.ToString(CultureInfo.InvariantCulture), (-11.2).ToString(CultureInfo.InvariantCulture)},
                new[] {"a", "b"},
                new[] {true.ToString(), false.ToString()}
            };

            var transposedData = Transpose(data);

            var expectedTypes = new[] { typeof(int), typeof(DateTime), typeof(double), typeof(string), typeof(bool) };

            //Act
            var resultTypes = _sut.DetectTypes(transposedData);

            //Assert
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], resultTypes[i]);
            }
        }

        [Test]
        public void DetectTypes_With_InvalidData_Detects_Correctly()
        {
            //Arrange
            var data = new[]
            {
                new[] {1.ToString(), "a", (-3).ToString()},
                new[]
                {
                    new DateTime(1992, 11, 1, 11, 15, 13).ToString(CultureInfo.InvariantCulture), "b",
                    new DateTime(1991, 08, 11, 06, 30, 11).ToString(CultureInfo.InvariantCulture)
                },
                new[] {2.3.ToString(CultureInfo.InvariantCulture), "c", (-11.2).ToString(CultureInfo.InvariantCulture)},
                new[] {"a", "d", "b"},
                new[] {true.ToString(), "e", false.ToString()}
            };

            var transposedData = Transpose(data);

            var expectedTypes = new[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };

            //Act
            var resultTypes = _sut.DetectTypes(transposedData);

            //Assert
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], resultTypes[i]);
            }
        }

        [Test]
        public void DetectTypes_With_MissingData_Detects_Correctly()
        {
            //Arrange
            var data = new[]
            {
                new[] {1.ToString(), string.Empty, (-3).ToString()},
                new[]
                {
                    new DateTime(1992, 11, 1, 11, 15, 13).ToString(CultureInfo.InvariantCulture),
                    string.Empty,
                    new DateTime(1991, 08, 11, 06, 30, 11).ToString(CultureInfo.InvariantCulture)
                },
                new[]
                {
                    2.3.ToString(CultureInfo.InvariantCulture),
                    string.Empty,
                    (-11.2).ToString(CultureInfo.InvariantCulture)
                },
                new[] {"a", string.Empty, "b"},
                new[] {true.ToString(), string.Empty, false.ToString()}
            };

            var transposedData = Transpose(data);

            var expectedTypes = new[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };

            //Act
            var resultTypes = _sut.DetectTypes(transposedData);

            //Assert
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], resultTypes[i]);
            }
        }

        [Test]
        public void DetectTypes_With_MissingData_And_InvalidData_Detects_Correctly()
        {
            //Arrange
            var data = new[]
            {
                new[] {1.ToString(), string.Empty, "a", (-3).ToString()},
                new[]
                {
                    new DateTime(1992, 11, 1, 11, 15, 13).ToString(CultureInfo.InvariantCulture),
                    string.Empty,
                    "b",
                    new DateTime(1991, 08, 11, 06, 30, 11).ToString(CultureInfo.InvariantCulture)
                },
                new[]
                {
                    2.3.ToString(CultureInfo.InvariantCulture),
                    string.Empty,
                    "c",
                    (-11.2).ToString(CultureInfo.InvariantCulture)
                },
                new[] {"a", string.Empty,"d", "b"},
                new[] {true.ToString(), string.Empty,"e", false.ToString()}
            };

            var transposedData = Transpose(data);

            var expectedTypes = new[] { typeof(string), typeof(string), typeof(string), typeof(string), typeof(string) };

            //Act
            var resultTypes = _sut.DetectTypes(transposedData);

            //Assert
            for (var i = 0; i < expectedTypes.Length; i++)
            {
                Assert.AreEqual(expectedTypes[i], resultTypes[i]);
            }
        }

        public static T[][] Transpose<T>(T[][] arr)
        {
            var rowCount = arr.Length;
            var columnCount = arr[0].Length;
            var transposed = new T[columnCount][];
            for (var column = 0; column < columnCount; column++)
            {
                transposed[column] = new T[rowCount];
                for (var row = 0; row < rowCount; row++)
                {
                    transposed[column][row] = arr[row][column];
                }
            }
            return transposed;
        }
    }
}
