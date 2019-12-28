using System.IO;
using HH.FileSystem.IO.CsvReader.Interfaces;
using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.Presentation.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class DelimitedTextServiceTests
    {
        private AutoMocker _autoMocker;
        private ICsvReaderFactory _csvReaderFactory;
        private IDataFieldDefinitionFactory _dataFieldDefinitionFactory;
        private ITypeDetectionService _typeDetectionService;
        private INamingService _namingService;
        private IDelimitedTextService _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _csvReaderFactory = _autoMocker.Mock<ICsvReaderFactory>();
            _dataFieldDefinitionFactory = _autoMocker.Mock<IDataFieldDefinitionFactory>();
            _typeDetectionService = _autoMocker.Mock<ITypeDetectionService>();
            _namingService = _autoMocker.Mock<INamingService>();

            _sut = new DelimitedTextService(_csvReaderFactory, _dataFieldDefinitionFactory,
                 _typeDetectionService, _namingService);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void DetectFileDefinition_Returns_Empty_Definition_If_File_Empty()
        {
            //Arrange
            var textReader = _autoMocker.Mock<TextReader>();
            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = false };

            var csvReader = _autoMocker.Mock<ICsvReader>();
            _csvReaderFactory.Expect(c => c.CreateCsvReader(textReader, fileConfiguration)).Return(csvReader);

            csvReader.Expect(c => c.Read()).Return(false);
            csvReader.Expect(c => c.Dispose());

            //Act
            var result = _sut.DetectDataFieldDefinitions(textReader, fileConfiguration);

            //Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void DetectFileDefinition_Detects_FileDefinition_Correctly_With_Header()
        {
            //Arrange
            const int numberOfRows = 2;
            var headerNames = new[] { "int_column", "string_column" };
            var uniqueHeaderNames = new[] { "int_column", "string_column" };
            var dataRows = new[] { new[] { "1", "b" }, new[] { "3", "d" } };
            var dataColumnTypes = new[] { typeof(int), typeof(string) };
            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = true };

            var textReader = _autoMocker.Mock<TextReader>();

            var csvReader = _autoMocker.Mock<ICsvReader>();
            _csvReaderFactory.Expect(c => c.CreateCsvReader(textReader, fileConfiguration)).Return(csvReader);

            for (var rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
            {
                csvReader.Expect(c => c.Read()).Repeat.Once().Return(true);
                csvReader.Expect(c => c.CurrentRecord).Repeat.Once().Return(dataRows[rowIndex]);
            }
            csvReader.Expect(c => c.Read()).Repeat.Once().Return(false);
            csvReader.Expect(c => c.Headers).Return(headerNames);
            _namingService.Expect(c => c.UniquateNames(headerNames)).Return(uniqueHeaderNames);

            _typeDetectionService.Expect(c => c.DetectTypes(dataRows)).Return(dataColumnTypes);

            csvReader.Expect(c => c.FieldCount).Return(2);

            var dataFieldDefinitions = new IDataFieldDefinition[dataColumnTypes.Length];
            for (var columnIndex = 0; columnIndex < dataColumnTypes.Length; columnIndex++)
            {
                var dataFieldDefinition = _autoMocker.Mock<IDataFieldDefinition>();
                dataFieldDefinitions[columnIndex] = dataFieldDefinition;

                var index = columnIndex;
                _dataFieldDefinitionFactory.Expect(
                    c =>
                        c.CreateDataFieldDefinition(dataColumnTypes[index], uniqueHeaderNames[index], index))
                    .Repeat.Once()
                    .Return(dataFieldDefinition);
            }
            csvReader.Expect(c => c.Dispose());

            //Act
            var result = _sut.DetectDataFieldDefinitions(textReader, fileConfiguration);

            //Assert
            Assert.AreEqual(dataColumnTypes.Length, result.Length);
            for (var index = 0; index < result.Length; index++)
            {
                Assert.AreEqual(dataFieldDefinitions[index], result[index]);
            }
        }

        [Test]
        public void DetectFileDefinition_Detects_FileDefinition_Correctly_WithOut_Header()
        {
            //Arrange
            const int numberOfRows = 2;
            var headerNames = new[] { "Field 1", "Field 2" };
            var dataRows = new[] { new[] { "1", "b" }, new[] { "3", "d" } };
            var dataColumnTypes = new[] { typeof(int), typeof(string) };

            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = false };

            var textReader = _autoMocker.Mock<TextReader>();

            var csvReader = _autoMocker.Mock<ICsvReader>();
            _csvReaderFactory.Expect(c => c.CreateCsvReader(textReader, fileConfiguration)).Return(csvReader);

            for (var rowIndex = 0; rowIndex < numberOfRows; rowIndex++)
            {
                csvReader.Expect(c => c.Read()).Repeat.Once().Return(true);
                csvReader.Expect(c => c.CurrentRecord).Repeat.Once().Return(dataRows[rowIndex]);
            }
            csvReader.Expect(c => c.Read()).Repeat.Once().Return(false);

            _typeDetectionService.Expect(c => c.DetectTypes(dataRows)).Return(dataColumnTypes);

            csvReader.Expect(c => c.FieldCount).Return(2);

            var dataFieldDefinitions = new IDataFieldDefinition[dataColumnTypes.Length];
            for (var columnIndex = 0; columnIndex < dataColumnTypes.Length; columnIndex++)
            {
                var dataFieldDefinition = _autoMocker.Mock<IDataFieldDefinition>();
                dataFieldDefinitions[columnIndex] = dataFieldDefinition;

                var index = columnIndex;
                _dataFieldDefinitionFactory.Expect(
                    c =>
                        c.CreateDataFieldDefinition(dataColumnTypes[index], headerNames[index], index))
                    .Repeat.Once()
                    .Return(dataFieldDefinition);
            }

            csvReader.Expect(c => c.Dispose());

            //Act
            var result = _sut.DetectDataFieldDefinitions(textReader, fileConfiguration);

            //Assert
            Assert.AreEqual(dataColumnTypes.Length, result.Length);
            for (var index = 0; index < result.Length; index++)
            {
                Assert.AreEqual(dataFieldDefinitions[index], result[index]);
            }
        }

        [Test]
        public void CreateDataReader_Throws_If_No_Active_DataFieldDefinitions()
        {
            //Arrange
            var textReader = _autoMocker.Mock<TextReader>();
            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = false };

            var dataFieldDefinition1 = _autoMocker.Mock<IDataFieldDefinition>();
            dataFieldDefinition1.Expect(c => c.Include).Return(false);

            var csvReader = _autoMocker.Mock<ICsvReader>();
            _csvReaderFactory.Expect(c => c.CreateCsvReader(textReader, fileConfiguration)).Return(csvReader);

            csvReader.Expect(c => c.Dispose());

            //Act & Assert
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidDataException>(() => _sut.CreateDataReader(textReader, fileConfiguration, new[] { dataFieldDefinition1 }));
        }

        [Test]
        public void CreateDataReader_With_Configuration_And_DataFieldDefinitions_Creates_DataReader_Correctly()
        {
            //Arrange
            var columnTypes = new[] { typeof(bool), typeof(double) };
            var textReader = _autoMocker.Mock<TextReader>();
            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = true };

            var csvReader = _autoMocker.Mock<ICsvReader>();
            _csvReaderFactory.Expect(c => c.CreateCsvReader(textReader, fileConfiguration)).Return(csvReader);

            var dataFieldDefinitions = new IDataFieldDefinition[columnTypes.Length];
            for (var i = 0; i < dataFieldDefinitions.Length; i++)
            {
                var dataFieldDefinition = _autoMocker.Mock<IDataFieldDefinition>();
                dataFieldDefinition.Expect(c => c.Name).Return($"Field {i}");
                dataFieldDefinition.Expect(c => c.Index).Return(i);
                dataFieldDefinition.Expect(c => c.Include).Return(true);
                dataFieldDefinitions[i] = dataFieldDefinition;
            }

            //Act
            var result = _sut.CreateDataReader(textReader, fileConfiguration, dataFieldDefinitions);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateCsvReader_With_Configuration_Creates_DataReader_Correctly()
        {
            //Arrange
            var textReader = _autoMocker.Mock<TextReader>();
            var fileConfiguration = new DelimitedTextReaderConfiguration { HasHeaderRecord = true };

            var csvReader = _autoMocker.Mock<ICsvReader>();
            _csvReaderFactory.Expect(c => c.CreateCsvReader(textReader, fileConfiguration)).Return(csvReader);

            //Act
            var result = _sut.CreateCsvReader(textReader, fileConfiguration);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
