using HH.Math.Functions;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Math.Tests.Functions
{
    [TestFixture]
    internal sealed class ErrorFunctionTests
    {
        [TestCase(0, 0)]
        [TestCase(0.01, 0.011283416)]
        [TestCase(0.08, 0.090078126)]
        [TestCase(0.36, 0.389329701)]
        [TestCase(2.97, 0.999973334)]
        [TestCase(2.02, 0.995719451)]
        [TestCase(2.64, 0.999811181)]
        [TestCase(3.5, 0.999999257)]
        [TestCase(double.PositiveInfinity, 1)]
        [TestCase(double.NegativeInfinity, -1)]
        public void Erf_Returns_ExpectedValue(double x, double expectedResult)
        {
            //Arrange

            //Act
            var result = x.Erf();

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.HighToleranceDelta);
        }

        [TestCase(0, 1)]
        [TestCase(0.01, 1 - 0.011283416)]
        [TestCase(0.08, 1 - 0.090078126)]
        [TestCase(0.36, 1 - 0.389329701)]
        [TestCase(2.97, 1 - 0.999973334)]
        [TestCase(2.02, 1 - 0.995719451)]
        [TestCase(2.64, 1 - 0.999811181)]
        [TestCase(3.5, 1 - 0.999999257)]
        [TestCase(7.3545708245448225, 1.227576329926042E-25d)]
        [TestCase(double.PositiveInfinity, 0)]
        [TestCase(double.NegativeInfinity, 2)]
        public void Erfc_Returns_ExpectedValue(double x, double expectedResult)
        {
            //Arrange

            //Act
            var result = x.Erfc();

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.HighToleranceDelta);
        }

        [TestCase(0, 1)]
        [TestCase(0.01, 1 - 0.011283416)]
        [TestCase(0.08, 1 - 0.090078126)]
        [TestCase(0.36, 1 - 0.389329701)]
        [TestCase(2.970001493, 1 - 0.999973334)]
        [TestCase(2.02, 1 - 0.995719451)]
        [TestCase(2.64, 1 - 0.999811181)]
        [TestCase(3.50001822, 1 - 0.999999257)]
        [TestCase(7.401125592, 1.227576329926042E-25d)]
        [TestCase(double.PositiveInfinity, 0)]
        [TestCase(double.NegativeInfinity, 2)]
        public void ErfcInv_Returns_ExpectedValue(double expectedResult, double x)
        {
            //Arrange

            //Act
            var result = x.ErfcInv();

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.HighToleranceDelta);
        }
    }
}
