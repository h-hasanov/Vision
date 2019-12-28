using System;
using HH.EnvironmentServices.Utils;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.Utils
{
    [TestFixture]
    internal sealed class ArgumentCheckTests
    {
        [Test]
        public void ArgumentNullCheck_Checks_Correctly_If_Not_Null()
        {
            //Assert
            Assert.DoesNotThrow(() => new Person().ArgumentNullCheck("personProperty", "Person cannot be null"));
        }

        [Test]
        public void ArgumentNullCheck_Throws_If_Null_Correct_Message_With_Message()
        {
            //Arrange
            const string parameterName = "personProperty";
            const string expectedMessage = "Person cannot be null";
            string expectedExceptionMessage = $"{expectedMessage} Parameter name: {parameterName}";

            //Assert
            Assert.Throws<ArgumentNullException>(() => default(Person).ArgumentNullCheck(parameterName, expectedMessage), expectedExceptionMessage);
        }

        [Test]
        public void ArgumentNullCheck_Throws_If_Null_Correct_Message_Without_Message()
        {
            //Arrange
            const string parameterName = "personProperty";
            string expectedExceptionMessage = $"Parameter name: {parameterName}";

            //Assert
            Assert.Throws<ArgumentNullException>(() => default(Person).ArgumentNullCheck(parameterName), expectedExceptionMessage);
        }

        [Test]
        public void ArgumentNullOrWhitespaceCheck_Throws_If_Null_Correct_Message_With_Message()
        {
            //Arrange
            const string parameterName = "personProperty";
            const string expectedMessage = "Person cannot be null";
            string expectedExceptionMessage = $"{expectedMessage} Parameter name: {parameterName}";

            //Assert
            Assert.Throws<ArgumentNullException>(() => default(string).ArgumentNullOrWhitespaceCheck(parameterName, expectedMessage), expectedExceptionMessage);
        }

        [Test]
        public void ArgumentNullOrWhitespaceCheck_Throws_If_Null_Correct_Message_Without_Message()
        {
            //Arrange
            const string parameterName = "personProperty";
            string expectedExceptionMessage = $"Parameter name: {parameterName}";

            //Assert
            Assert.Throws<ArgumentNullException>(() => default(string).ArgumentNullOrWhitespaceCheck(parameterName), expectedExceptionMessage);
        }
    }
}
