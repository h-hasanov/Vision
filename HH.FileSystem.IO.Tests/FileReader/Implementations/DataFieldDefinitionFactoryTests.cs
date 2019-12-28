using System;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class DataFieldDefinitionFactoryTests
    {
        private AutoMocker _autoMocker;
        private IDataFieldDefinitionFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();

            _sut = new DataFieldDefinitionFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [TestCase(typeof(bool), typeof(IDataFieldDefinition<bool>))]
        [TestCase(typeof(int), typeof(IDataFieldDefinition<int>))]
        [TestCase(typeof(double), typeof(IDataFieldDefinition<double>))]
        [TestCase(typeof(DateTime), typeof(IDataFieldDefinition<DateTime>))]
        [TestCase(typeof(string), typeof(IDataFieldDefinition<string>))]
        public void CreateDataFieldDefinition_Creates_Expected_DataFieldDefinition(Type dataType, Type dataFieldDefinitionType)
        {
            //Act
            var result = _sut.CreateDataFieldDefinition(dataType, "a", int.MaxValue / 3);

            //Assert
            Assert.AreEqual(dataType, result.DataType);
            Assert.IsInstanceOf(dataFieldDefinitionType, result);
        }

        [Test]
        public void CreateDataFieldDefinition_Throws_If_Unknown_Type()
        {
            //Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CreateDataFieldDefinition(typeof (ITestInterface), "a", 3));
        }
    }
}
