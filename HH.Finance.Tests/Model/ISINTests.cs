using HH.Finance.Interfaces;
using HH.Finance.Model;
using NUnit.Framework;

namespace HH.Finance.Tests.Model
{
    [TestFixture]
    internal sealed class ISINTests
    {
        private IISIN _sut;

        [Test]
        public void Ctor_SetsParameters_Correctly()
        {
            //Arrange
            const string isinCode = "some code";

            //Act
            _sut = new ISIN(isinCode);

            //Assert
            Assert.AreEqual(isinCode, _sut.Code);
        }
    }
}
