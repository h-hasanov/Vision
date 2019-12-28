using System;
using HH.Extensions.Generics;
using NUnit.Framework;

namespace HH.Extensions.Tests.Generics
{
    [TestFixture]
    internal sealed class GenericExtensionsTests
    {
        [TestCase("3.2", 3.2)]
        [TestCase("3", 3)]
        [TestCase("-2.5", -2.5)]
        public void Parse_ParsesToDouble_Correctly(string value, double expected)
        {
            //Assert
            Assert.AreEqual(expected, value.Parse<string, double>());
        }

        [TestCase("3", "3")]
        [TestCase("asd", "asd")]
        public void Parse_ParsesToString_Correctly(string value, string expected)
        {
            //Assert
            Assert.AreEqual(expected, value.Parse<string, string>());
        }

        [TestCase("3", 3)]
        [TestCase("-4", -4)]
        public void Parse_ParsesToInt_Correctly(string value, int expected)
        {
            //Assert
            Assert.AreEqual(expected, value.Parse<string, int>());
        }

        [Test]
        public void Parse_Throws_If_TargetType_Not_Recognized()
        {
            //Assert
            Assert.Throws<NotImplementedException>(() => "asd".Parse<string, short>());
        }

        [Test]
        public void AreEqual_Returns_True_If_Same_Instance()
        {
            //Arrange
            var reference = new object();
            var value = reference;

            //Act
            var result = GenericExtensions.AreEqual(reference, value);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void AreEqual_Returns_False_If_Not_Same_Instance()
        {
            //Arrange
            var reference = new object();
            var value = new object();

            //Act
            var result = GenericExtensions.AreEqual(reference, value);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
