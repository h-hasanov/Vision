using HH.Finance.Enums;
using HH.Finance.Instruments;
using HH.Finance.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Finance.Tests.Instruments
{
    [TestFixture]
    internal sealed class OptionGreeksCalculatorTests
    {
        private AutoMocker _autoMocker;
        private IOptionInput _optionInput;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _optionInput = _autoMocker.Mock<IOptionInput>();
        }

        [TestCase(OptionType.Call, 0.506511541638925)]
        [TestCase(OptionType.Put, -0.478640882848326)]
        public void Delta_Calculates_Correctly(OptionType optionType, double expectedResult)
        {
            //Arrange
            SetOptionInputExpectations(optionType);

            //Act
            var result = OptionGreeksCalculator.GetDelta(_optionInput);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
            _optionInput.VerifyAllExpectations();
        }

        [TestCase(OptionType.Call, 27.771785174693658d)]
        [TestCase(OptionType.Put, 27.771785174693658d)]
        public void Vega_Calculates_Correctly(OptionType optionType, double expectedResult)
        {
            //Arrange
            SetOptionInputExpectations(optionType);

            //Act
            var result = OptionGreeksCalculator.GetVega(_optionInput);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [TestCase(OptionType.Call, 0.027811289442)]
        [TestCase(OptionType.Put, 0.027811289442)]
        public void Gamma_Calculates_Correctly(OptionType optionType, double expectedResult)
        {
            //Arrange
            SetOptionInputExpectations(optionType);

            //Act
            var result = OptionGreeksCalculator.GetGamma(_optionInput);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [TestCase(OptionType.Call, 22.666278824431323d)]
        [TestCase(OptionType.Put, -26.833754177559818d)]
        public void Rho_Calculates_Correctly(OptionType optionType, double expectedResult)
        {
            //Arrange
            SetOptionInputExpectations(optionType);

            //Act
            var result = OptionGreeksCalculator.GetRho(_optionInput);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }

        [TestCase(OptionType.Call, -5.8479020491072848d)]
        [TestCase(OptionType.Put, -4.843235227757182d)]
        public void Theta_Calculates_Correctly(OptionType optionType, double expectedResult)
        {
            //Arrange
            SetOptionInputExpectations(optionType);

            //Act
            var result = OptionGreeksCalculator.GetTheta(_optionInput);

            //Assert
            Assert.AreEqual(expectedResult, result, Constants.ComparisonDelta);
        }



        private void SetOptionInputExpectations(OptionType optionType)
        {
            _optionInput.Expect(c => c.OptionType).Return(optionType);

            _optionInput.Expect(c => c.Price).Return(100);
            _optionInput.Expect(c => c.Strike).Return(101);
            _optionInput.Expect(c => c.Dividend).Return(0.03);
            _optionInput.Expect(c => c.Rate).Return(0.04);
            _optionInput.Expect(c => c.Time).Return(0.5);
            _optionInput.Expect(c => c.Volatility).Return(0.2);
        }
    }
}
