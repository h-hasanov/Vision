using HH.Finance.Interfaces;
using HH.Finance.Result;
using NUnit.Framework;

namespace HH.Finance.Tests.Result
{
    [TestFixture]
    internal sealed class OptionValueTests
    {
        private IOptionValue _sut;

        [TestCase(1)]
        [TestCase(2)]
        public void Ctor_Sets_Correctly(double value)
        {
            //Arrange
            
            //Act
            _sut=new OptionValue(value);

            //Assert
            Assert.AreEqual(value,_sut.Value);
        }
    }
}
