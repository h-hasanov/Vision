using HH.ErrorManager.Model.Collections.Interfaces;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Implementations;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ErrorManager.Model.Tests.Models.Implementations
{
    [TestFixture]
    internal sealed class ErrorInfoContainerTests
    {
        private AutoMocker _autoMocker;
        private IErrorInfoCollectionFactory _errorInfoCollectionFactory;
        private IErrorInfoCollection _errorInfoCollection;
        private const string Description = "asdasdsa";
        private IErrorInfoContainer _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();

            _errorInfoCollectionFactory = _autoMocker.Mock<IErrorInfoCollectionFactory>();
            _errorInfoCollection = _autoMocker.Mock<IErrorInfoCollection>();

            _errorInfoCollectionFactory.Expect(c => c.CreatErrorInfoCollection())
                .IgnoreArguments()
                .Return(_errorInfoCollection);


            _sut = new ErrorInfoContainer(Description, _errorInfoCollectionFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void Ctor_With_Description_Creates_Correctly()
        {
            //Arrange
            const string description = "asddsa";

            //Act
            var result = new ErrorInfoContainer(description);

            //Assert
            Assert.AreEqual(description, result.Description);
        }

        [Test]
        public void Ctor_Sets_Properties()
        {
            //Assert
            Assert.AreEqual(_errorInfoCollection, _sut.ErrorInfoCollection);
            Assert.AreEqual(Description, _sut.Description);
        }
    }
}
