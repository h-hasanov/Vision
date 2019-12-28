using System;
using System.Collections.Generic;
using System.ComponentModel;
using HH.Extensions.Types;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.Extensions.Tests.Types
{
    [TestFixture]
    internal sealed class TypeExtensionsTests
    {
        [Test]
        public void StaticTypes_Return_ExpectedTypes()
        {
            //Assert
            Assert.AreEqual(typeof(bool), TypeExtensions.BoolType);
            Assert.AreEqual(typeof(int), TypeExtensions.Int32Type);
            Assert.AreEqual(typeof(double), TypeExtensions.DoubleType);
            Assert.AreEqual(typeof(DateTime), TypeExtensions.DateTimeType);
            Assert.AreEqual(typeof(string), TypeExtensions.StringType);
        }

        [Test]
        public void GetDefault_Returns_Default()
        {
            //Arrange
            var types = new Dictionary<Type, object>
            {
                {typeof (DateTime), default(DateTime)},
                {typeof (TimeSpan), default(TimeSpan)}
            };

            //Assert
            foreach (var type in types)
            {
                Assert.AreEqual(type.Value, type.Key.GetDefault());
            }
        }

        [Test]
        public void IsInstanceOfType_Returns_Correctly_If_Instance()
        {
            //Arrange
            var obj = new Person();

            //Assert
            Assert.IsTrue(TypeExtensions.IsInstanceOfType(typeof(IPerson), obj));
        }

        [Test]
        public void IsInstanceOfType_Returns_Correctly_IfNot_Instance()
        {
            //Arrange
            var obj = new Person();

            //Assert
            Assert.IsFalse(TypeExtensions.IsInstanceOfType(typeof(IChangeTracking), obj));
        }

        [TestCase("false", true)]
        [TestCase("True", true)]
        [TestCase("Tasdrue", false)]
        public void CanConvertFromString_With_BooleanType_Returns_ExpectedValue(string input, bool canConvert)
        {
            //Assert
            Assert.AreEqual(canConvert, typeof(bool).CanConvertFromString(input));
        }

        [TestCase("3", true)]
        [TestCase("-10", true)]
        [TestCase("Tasdrue", false)]
        public void CanConvertFromString_With_Int32Type_Returns_ExpectedValue(string input, bool canConvert)
        {
            //Assert
            Assert.AreEqual(canConvert, typeof(int).CanConvertFromString(input));
        }

        [TestCase("3.3", true)]
        [TestCase("-10.1", true)]
        [TestCase("Tasdrue", false)]
        public void CanConvertFromString_With_DoubleType_Returns_ExpectedValue(string input, bool canConvert)
        {
            //Assert
            Assert.AreEqual(canConvert, typeof(double).CanConvertFromString(input));
        }

        [TestCase("01/10/2016", true)]
        [TestCase("01/10/2016 asd", false)]
        public void CanConvertFromString_With_DateTimeType_Returns_ExpectedValue(string input, bool canConvert)
        {
            //Assert
            Assert.AreEqual(canConvert, typeof(DateTime).CanConvertFromString(input));
        }

        [TestCase("asdasdas", true)]
        [TestCase(" ", true)]
        public void CanConvertFromString_With_StringsType_Returns_ExpectedValue(string input, bool canConvert)
        {
            //Assert
            Assert.AreEqual(canConvert, typeof(string).CanConvertFromString(input));
        }
    }
}
