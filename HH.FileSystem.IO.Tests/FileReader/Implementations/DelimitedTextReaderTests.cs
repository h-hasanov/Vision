using System;
using System.IO;
using HH.Data.Interfaces;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;
using ICsvReader = HH.FileSystem.IO.CsvReader.Interfaces.ICsvReader;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class DelimitedTextReaderTests
    {
        private AutoMocker _autoMocker;
        private IDataFieldDefinition<ITestInterface> _dataFieldDefinition0;
        private const string DataFieldDefinition0Name = "Field 0";
        private Type _dataFieldDefinition0Type;
        private IDataFieldDefinition<ITestInterface> _dataFieldDefinition1;
        private const string DataFieldDefinition1Name = "Field 1";
        private Type _dataFieldDefinition1Type;
        private ICsvReader _csvReader;
        private IDataReader _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _dataFieldDefinition0 = _autoMocker.Mock<IDataFieldDefinition<ITestInterface>>();
            _dataFieldDefinition0.Expect(c => c.Name).Return(DataFieldDefinition0Name);
            _dataFieldDefinition0.Expect(c => c.Index).Return(0);
            _dataFieldDefinition0.Expect(c => c.Include).Return(true);
            _dataFieldDefinition0.Expect(c => c.DataType).Return(_dataFieldDefinition0Type);


            _dataFieldDefinition1 = _autoMocker.Mock<IDataFieldDefinition<ITestInterface>>();
            _dataFieldDefinition1.Expect(c => c.Name).Return(DataFieldDefinition1Name);
            _dataFieldDefinition1.Expect(c => c.Index).Return(1);
            _dataFieldDefinition1.Expect(c => c.Include).Return(true);
            _dataFieldDefinition1.Expect(c => c.DataType).Return(_dataFieldDefinition1Type);


            _csvReader = _autoMocker.Mock<ICsvReader>();

            _dataFieldDefinition0Type = typeof(double);
            _dataFieldDefinition1Type = typeof(DateTime);

            _sut = new DelimitedTextReader(new IDataFieldDefinition[] { _dataFieldDefinition0, _dataFieldDefinition1 }, _csvReader);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_With_Duplicated_FieldNames_Throws()
        {
            //Arrange
            const string duplicatedName = "a";
            var dataFieldDefinition1 = _autoMocker.Mock<IDataFieldDefinition>();
            dataFieldDefinition1.Expect(c => c.Name).Return(duplicatedName);

            var dataFieldDefinition2 = _autoMocker.Mock<IDataFieldDefinition>();
            dataFieldDefinition2.Expect(c => c.Name).Return(duplicatedName);

            //Act
            Assert.Throws<InvalidDataException>(
                () => _sut = new DelimitedTextReader(new[] { dataFieldDefinition1, dataFieldDefinition2 }, _csvReader));
        }

        [Test]
        public void Ctor_With_No_Included_Fields_Throws()
        {
            //Arrange
            var dataFieldDefinition1 = _autoMocker.Mock<IDataFieldDefinition>();
            dataFieldDefinition1.Expect(c => c.Name).Return("a");
            dataFieldDefinition1.Expect(c => c.Include).Return(false);

            var dataFieldDefinition2 = _autoMocker.Mock<IDataFieldDefinition>();
            dataFieldDefinition2.Expect(c => c.Name).Return("b");
            dataFieldDefinition2.Expect(c => c.Include).Return(false);

            //Act
            Assert.Throws<InvalidDataException>(
                () => _sut = new DelimitedTextReader(new[] { dataFieldDefinition1, dataFieldDefinition2 }, _csvReader));
        }

        [Test]
        public void FieldTypes_Returns_Expected_Values()
        {
            //Assert
            CollectionAssert.AreEqual(new[] { _dataFieldDefinition0Type, _dataFieldDefinition1Type }, _sut.FieldTypes);
        }

        [Test]
        public void Headers_Returns_Expected_Values()
        {
            //Assert
            CollectionAssert.AreEqual(new[] { DataFieldDefinition0Name, DataFieldDefinition1Name }, _sut.Headers);
        }

        [Test]
        public void CurrentRecord_Returns_Expected_Values()
        {
            //Arrange
            var currentRecord = new[] { "a", "b" };
            _csvReader.Expect(c => c.CurrentRecord).Return(currentRecord);

            //Act
            var result = _sut.CurrentRecord;

            //Assert
            Assert.AreEqual(currentRecord, result);
        }

        [Test]
        public void GetField_By_Index_Gets_MissingReplacementValue_If_Missing()
        {
            //Arrange
            const int index = 1;
            _csvReader.Expect(c => c.GetField(index)).Return("");

            var missingReplacement = _autoMocker.Mock<ITestInterface>();
            _dataFieldDefinition1.Expect(c => c.MissingReplacementValue).Return(missingReplacement);

            //Act
            var result = _sut.GetField<ITestInterface>(index);

            //Assert
            Assert.AreEqual(missingReplacement, result);
        }

        [Test]
        public void GetField_By_Index_Gets_ErrorReplacementValue_If_CannotParse()
        {
            //Arrange
            const int index = 1;
            _csvReader.Expect(c => c.GetField(index)).Return("some test interface value");

            var errorReplacement = _autoMocker.Mock<ITestInterface>();
            _dataFieldDefinition1.Expect(c => c.ErrorReplacementValue).Return(errorReplacement);

            ITestInterface parsedValue = null;
            _csvReader.Stub(c =>
            {
                c.TryGetField(index, out parsedValue);
            }).OutRef(_autoMocker.Mock<ITestInterface>()).Return(false);

            //Act
            var result = _sut.GetField<ITestInterface>(index);

            //Assert
            Assert.AreEqual(errorReplacement, result);
            Assert.AreNotEqual(parsedValue, result);
            Assert.IsNull(parsedValue);
        }

        [Test]
        public void GetField_By_Index_Gets_Correctly_If_Valid()
        {
            //Arrange
            const int index = 1;
            _csvReader.Expect(c => c.GetField(index)).Return("some test interface value");

            var outParsedValue = _autoMocker.Mock<ITestInterface>();
            ITestInterface parsedValue;
            _csvReader.Stub(c =>
            {
                c.TryGetField(index, out parsedValue);
            }).OutRef(outParsedValue).Return(true);

            //Act
            var result = _sut.GetField<ITestInterface>(index);

            //Assert
            Assert.AreEqual(outParsedValue, result);
        }

        [Test]
        public void GetField_By_Name_Gets_MissingReplacementValue_If_Missing()
        {
            //Arrange
            const int index = 1;
            _csvReader.Expect(c => c.GetField(index)).Return("");

            var missingReplacement = _autoMocker.Mock<ITestInterface>();
            _dataFieldDefinition1.Expect(c => c.MissingReplacementValue).Return(missingReplacement);

            //Act
            var result = _sut.GetField<ITestInterface>(DataFieldDefinition1Name);

            //Assert
            Assert.AreEqual(missingReplacement, result);
        }

        [Test]
        public void GetField_By_Name_Gets_ErrorReplacementValue_If_CannotParse()
        {
            //Arrange
            const int index = 1;
            _csvReader.Expect(c => c.GetField(index)).Return("some test interface value");


            var errorReplacement = _autoMocker.Mock<ITestInterface>();
            _dataFieldDefinition1.Expect(c => c.ErrorReplacementValue).Return(errorReplacement);

            ITestInterface parsedValue = null;
            _csvReader.Stub(c =>
            {
                c.TryGetField(index, out parsedValue);
            }).OutRef(_autoMocker.Mock<ITestInterface>()).Return(false);

            //Act
            var result = _sut.GetField<ITestInterface>(DataFieldDefinition1Name);

            //Assert
            Assert.AreEqual(errorReplacement, result);
            Assert.AreNotEqual(parsedValue, result);
            Assert.IsNull(parsedValue);
        }

        [Test]
        public void GetField_By_Name_Gets_Correctly_If_Valid()
        {
            //Arrange
            const int index = 1;
            _csvReader.Expect(c => c.GetField(index)).Return("some test interface value");

            var outParsedValue = _autoMocker.Mock<ITestInterface>();
            ITestInterface parsedValue;
            _csvReader.Stub(c =>
            {
                c.TryGetField(index, out parsedValue);
            }).OutRef(outParsedValue).Return(true);

            //Act
            var result = _sut.GetField<ITestInterface>(DataFieldDefinition1Name);

            //Assert
            Assert.AreEqual(outParsedValue, result);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Read_Reads_Correctly(bool canRead)
        {
            //Arrange
            _csvReader.Expect(c => c.Read()).Return(canRead);

            //Act
            var result = _sut.Read();

            //Assert
            Assert.AreEqual(canRead, result);
        }

        [Test]
        public void Dispose_Disposes_Correctly()
        {
            //Arrange
            _csvReader.Expect(c => c.Dispose());

            //Act
            _sut.Dispose();
        }
    }
}
