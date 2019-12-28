using HH.Finance.Instruments;
using HH.Finance.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Finance.Tests.Instruments
{
    [TestFixture]
    internal sealed class InstrumentBaseTests
    {
        private IInstrument _sut;
        private IISIN _isin;
        private string _description;
        private AutoMocker _autoMocker;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _isin = _autoMocker.Mock<IISIN>();
            _description = "some Description";
            _sut = new InstrumentBase(_isin, _description);
        }

        [Test]
        public void Ctor_Sets_Correctly()
        {
            //Act

            //Assert
            Assert.AreEqual(_isin,_sut.ISIN);
            Assert.AreEqual(_description,_sut.Description);
        }
    }
}
