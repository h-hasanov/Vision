using HH.Math.Functions;
using NUnit.Framework;

namespace HH.Math.Tests.Functions
{
    [TestFixture]
    internal sealed class FactorialFunctionTests
    {
        [TestCase(0.0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(11, 39916800.000000171d)]
        public void Factorial_Returns_AsExpected(double x, double expectedResult)
        {
            //Arrange

            //Act
            var result = x.Factorial();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(0, 0)]
        [TestCase(3, 1.791759469228055d)]
        [TestCase(8.3, 11.252022385979149d)]
        [TestCase(12.67, 21.697265601773584d)]
        [TestCase(100, 363.73937555556347d)]
        public void LogFactorial_Returns_AsExpected(double x, double expectedResult)
        {
            //Act
            var result = x.LogFactorial();

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
