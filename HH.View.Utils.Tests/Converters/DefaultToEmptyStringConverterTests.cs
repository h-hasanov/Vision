using System;
using System.Collections.Generic;
using HH.View.Utils.Converters;
using NUnit.Framework;

namespace HH.View.Utils.Tests.Converters
{
    [TestFixture]
    internal sealed class DefaultToEmptyStringConverterTests
    {
        private DefaultToEmptyStringConverter _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new DefaultToEmptyStringConverter();
        }

        [Test]
        public void Convert_ChecksWhetherDefault_ReturnCorrectly()
        {
            var nonDefaultTime = DateTime.Now;
            var nonDefaultTimeSpan = TimeSpan.FromDays(1);
            var dictionary = new Dictionary<object, object>
            {
                {nonDefaultTime,nonDefaultTime},
                {default(DateTime), string.Empty},
                {default(TimeSpan), string.Empty},
                {nonDefaultTimeSpan, nonDefaultTimeSpan},
            };

            //Assert
            foreach (var o in dictionary)
            {
                var result = _sut.Convert(o.Key, null, null, null);
                Assert.AreEqual(o.Value, result);
            }
        }
    }
}
