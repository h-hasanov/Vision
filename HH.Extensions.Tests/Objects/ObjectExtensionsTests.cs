using System;
using System.Collections.Generic;
using HH.Extensions.Objects;
using NUnit.Framework;

namespace HH.Extensions.Tests.Objects
{
    [TestFixture]
    internal sealed class ObjectExtensionsTests
    {
        [Test]
        public void IsDefault_ChecksWhetherDefault_ReturnCorrectly()
        {
            //Arrange
            var dictionary = new Dictionary<object, bool>
            {
                {DateTime.Now,false},
                {default(DateTime), true},
                {default(TimeSpan), true},
                {TimeSpan.FromDays(1), false},
            };

            //Assert
            foreach (var o in dictionary)
            {
                var result = o.Key.IsDefault(o.Key.GetType());
                Assert.AreEqual(o.Value, result);
            }
        }
    }
}
