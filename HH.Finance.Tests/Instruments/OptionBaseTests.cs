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
    internal sealed class OptionBaseTests
    {
        private IOptionInput _optionInput;
        private IOption _sut;
        private IPricingEngine _pricingEngine;
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker=new AutoMocker();
            _pricingEngine = _autoMocker.Mock<IPricingEngine>();
            _optionInput = _autoMocker.Mock<IOptionInput>();

            _sut = new OptionBase(_optionInput, _pricingEngine);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
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
            var sut = new OptionBase(price, strike, volatility, rate, dividend, time, exerciseType, optionType,
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
            var sut = new OptionBase(optionInput, pricingEngine);

            //Assert
            Assert.AreEqual(optionInput, sut.OptionInput);
            Assert.AreEqual(pricingEngine, sut.PricingEngine);
        }

        [TestCase(GreekType.Delta, 0.506511541638925)]
        [TestCase(GreekType.Vega, 27.771785174693658d)]
        [TestCase(GreekType.Theta, -5.8479020491072848d)]
        [TestCase(GreekType.Rho, 22.666278824431323d)]
        [TestCase(GreekType.Gamma, 0.027811289442)]
        public void GetGreek_CalculatesGreeks_Correctly(GreekType greekType, double expectedValue)
        {
            //Arrange
            SetOptionInputExpectations();
            var t = _optionInput.OptionType;

            //Act
            var result = _sut.GetGreek(greekType);

            //Assert
            Assert.AreEqual(greekType, result.Type);
            Assert.AreEqual(expectedValue, result.Value, Constants.ComparisonDelta);
        }

        [TestCase(-1, true)]
        [TestCase(1, false)]
        public void IsExpired_Returns_CorrectValue(double time, bool expectedIsExpired)
        {
            //Arrange
            _optionInput.Expect(c => c.Time).Return(time);
            _sut=new OptionBase(_optionInput,_pricingEngine);

            //Act
            var isExpired = _sut.IsExpired();

            //Assert
            Assert.AreEqual(expectedIsExpired,isExpired);
        }

        [Test]
        public void SetPricingEngine_Sets_Engine()
        {
            //Arrange
            var pricingEngine = _autoMocker.Mock<IPricingEngine>();
            Assert.AreNotEqual(pricingEngine,_sut.PricingEngine);

            //Act
            _sut.SetPricingEngine(pricingEngine);

            //Assert
            Assert.AreEqual(pricingEngine,_sut.PricingEngine);
        }

        private void SetOptionInputExpectations()
        {
            _optionInput.Expect(c => c.OptionType).Return(OptionType.Call);
            _optionInput.Expect(c => c.Price).Return(100);
            _optionInput.Expect(c => c.Strike).Return(101);
            _optionInput.Expect(c => c.Dividend).Return(0.03);
            _optionInput.Expect(c => c.Rate).Return(0.04);
            _optionInput.Expect(c => c.Time).Return(0.5);
            _optionInput.Expect(c => c.Volatility).Return(0.2);
        }
    }
}
