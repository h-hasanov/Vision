using HH.Finance.Enums;
using HH.Finance.Instruments;
using HH.Finance.Interfaces;
using HH.Finance.Model;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Finance.Tests.Instruments
{
    [TestFixture]
    internal sealed class BlackScholesOptionTests
    {
        private IBlackScholesOption _sut;
        private IOptionInput _optionInput;
        private IPricingEngine _pricingEngine;
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _optionInput = _autoMocker.Mock<IOptionInput>();
            _pricingEngine = _autoMocker.Mock<IPricingEngine>();
            _sut = new BlackScholesOption(_optionInput, _pricingEngine);
        }

        [Test]
        public void Ctor_Sets_Values_Correctly()
        {
            //Arrange
            const int price = 1;
            const int strike = 2;
            const int volatility = 3;
            const int rate = 4;
            const int dividend = 5;
            const int time = 6;
            const ExerciseType exerciseType = ExerciseType.European;
            const OptionType optionType = OptionType.Put;
            var isin = new ISIN("asd");
            const string description = "some description";
            var pricingEngine = _autoMocker.Mock<IPricingEngine>();

            //Act
            var sut = new BlackScholesOption(price, strike, volatility, rate, dividend, time, exerciseType, optionType,
                isin, description, pricingEngine);

            //Assert
            Assert.AreEqual(price, sut.OptionInput.Price);
            Assert.AreEqual(strike, sut.OptionInput.Strike);
            Assert.AreEqual(volatility, sut.OptionInput.Volatility);
            Assert.AreEqual(rate, sut.OptionInput.Rate);
            Assert.AreEqual(dividend, sut.OptionInput.Dividend);
            Assert.AreEqual(time, sut.OptionInput.Time);
            Assert.AreEqual(exerciseType, sut.OptionInput.ExerciseType);
            Assert.AreEqual(optionType, sut.OptionInput.OptionType);
            Assert.AreEqual(isin, sut.OptionInput.ISIN);
            Assert.AreEqual(description, sut.OptionInput.Description);
            Assert.AreEqual(pricingEngine, sut.PricingEngine);
        }

        [Test]
        public void Ctor_Sets_Interfaces_Correctly()
        {
            //Arrange
            var optionInput = _autoMocker.Mock<IOptionInput>();
            var pricingEngine = _autoMocker.Mock<IPricingEngine>();

            //Act
            var sut = new BlackScholesOption(optionInput, pricingEngine);

            //Assert
            Assert.AreEqual(optionInput, sut.OptionInput);
            Assert.AreEqual(pricingEngine, sut.PricingEngine);
        }

        [TestCase(OptionType.Call, 5.3260187440024538)]
        [TestCase(OptionType.Put, 5.8148907876784719)]
        public void CalculatePrice_Returns_CorrectPrice(OptionType optionType, double expectedPrice)
        {
            //Arrange
            SetOptionInputExpectations(optionType);

            //Act
            var price = _sut.GetOptionValue();

            //Assert
            Assert.AreEqual(expectedPrice, price.Value);
            _optionInput.VerifyAllExpectations();
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
