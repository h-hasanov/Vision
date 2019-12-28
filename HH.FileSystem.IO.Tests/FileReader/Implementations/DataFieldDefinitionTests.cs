using HH.FileSystem.IO.FileReader.Implementations;
using HH.FileSystem.IO.FileReader.Interfaces;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.FileSystem.IO.Tests.FileReader.Implementations
{
    [TestFixture]
    internal sealed class DataFieldDefinitionTests
    {
        private AutoMocker _autoMocker;
        private const string Name = "Some field";
        private const int Index = int.MaxValue;
        private const bool Include = true;
        private ITestInterface _missingReplacementValue;
        private ITestInterface _errorReplacementValue;
        private IDataFieldDefinition<ITestInterface> _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _missingReplacementValue = _autoMocker.Mock<ITestInterface>();
            _errorReplacementValue = _autoMocker.Mock<ITestInterface>();

            _sut = new DataFieldDefinition<ITestInterface>(Name, Index, Include, _missingReplacementValue, _errorReplacementValue);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Assert
            Assert.AreEqual(Name, _sut.Name);
            Assert.AreEqual(Index, _sut.Index);
            Assert.AreEqual(Include, _sut.Include);
            Assert.AreEqual(_missingReplacementValue, _sut.MissingReplacementValue);
            Assert.AreEqual(_errorReplacementValue, _sut.ErrorReplacementValue);
            Assert.AreEqual(_missingReplacementValue.ToString(), _sut.DisplayMissingReplacementValue);
            Assert.AreEqual(_errorReplacementValue.ToString(), _sut.DisplayErrorReplacementValue);
        }

        [Test]
        public void DisplayMissingReplacementValue_DoesNot_Throw_If_MissingReplacementValue_Null()
        {
            //Arrange
            _sut.MissingReplacementValue = null;

            //Assert
            Assert.DoesNotThrow(() => { var c = _sut.DisplayMissingReplacementValue; });
        }

        [Test]
        public void DisplayErrorReplacementValue_DoesNot_Throw_If_ErrorReplacementValue_Null()
        {
            //Arrange
            _sut.ErrorReplacementValue = null;

            //Assert
            Assert.DoesNotThrow(() => { var c = _sut.DisplayErrorReplacementValue; });
        }
    }
}
