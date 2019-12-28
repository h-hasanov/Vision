using System.Collections.Generic;
using HH.Data.Entity.Model.Interfaces;
using HH.ErrorManager.Model.Factories.Interfaces;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.ErrorManager.Model.Tests.Models.Implementations
{
    [TestFixture]
    internal sealed class ErrorManagerTests
    {
        private AutoMocker _autoMocker;
        private IEntityCollectionFactory _entityCollectionFactory;
        private IEntityCollection<IErrorInfoContainer> _errorInfoContainerCollection;
        private IDictionary<IEntity, IErrorInfoContainer> _entityToErrorInfoContainerMapper;
        private IErrorInfoContainerFactory _errorInfoContainerFactory;
        private IErrorManager _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _entityCollectionFactory = _autoMocker.Mock<IEntityCollectionFactory>();
            _errorInfoContainerCollection = _autoMocker.Mock<IEntityCollection<IErrorInfoContainer>>();
            _entityToErrorInfoContainerMapper = _autoMocker.Mock<IDictionary<IEntity, IErrorInfoContainer>>();
            _errorInfoContainerFactory = _autoMocker.Mock<IErrorInfoContainerFactory>();

            _entityCollectionFactory.Expect(c => c.CreateEntityCollection<IErrorInfoContainer>(null))
                .IgnoreArguments()
                .Return(_errorInfoContainerCollection);


            _sut = new Model.Models.Implementations.ErrorManager(_entityCollectionFactory, _entityToErrorInfoContainerMapper, _errorInfoContainerFactory);
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
            Assert.AreEqual(_errorInfoContainerCollection, _sut.ErrorInfoContainerCollection);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ContainsOwner_Returns_Expected_Value(bool contains)
        {
            //Arrange
            var owner = _autoMocker.Mock<IEntity>();
            _entityToErrorInfoContainerMapper.Expect(c => c.ContainsKey(owner)).Return(contains);

            //Act
            var result = _sut.ContainsOwner(owner);

            //Assert
            Assert.AreEqual(contains, result);
        }

        [Test]
        public void CreateErrorInfoContainer_Returns_ErrorInfoContainer_If_Contained()
        {
            //Arrange
            var owner = _autoMocker.Mock<IEntity>();
            _entityToErrorInfoContainerMapper.Expect(c => c.ContainsKey(owner)).Return(true);
            var errorInfoContainer = _autoMocker.Mock<IErrorInfoContainer>();
            _entityToErrorInfoContainerMapper.Expect(c => c[owner]).Return(errorInfoContainer);

            //Act
            var result = _sut.CreateErrorInfoContainer("asd", owner);

            //Assert
            Assert.AreEqual(errorInfoContainer, result);
        }

        [Test]
        public void CreateErrorInfoContainer_Creates_New_ErrorInfoContainer_If_Not_Contained()
        {
            //Arrange
            const string description = "asd";
            var owner = _autoMocker.Mock<IEntity>();
            _entityToErrorInfoContainerMapper.Expect(c => c.ContainsKey(owner)).Return(false);
            var errorInfoContainer = _autoMocker.Mock<IErrorInfoContainer>();
            _errorInfoContainerFactory.Expect(c => c.CreateErrorInfoContainer(description)).Return(errorInfoContainer);
            _entityToErrorInfoContainerMapper.Expect(c => c.Add(owner, errorInfoContainer));
            _errorInfoContainerCollection.Expect(c => c.Add(errorInfoContainer));

            //Act
            var result = _sut.CreateErrorInfoContainer(description, owner);

            //Assert
            Assert.AreEqual(errorInfoContainer, result);
        }

        [Test]
        public void RemoveErrorInfoContainer_Removes_ErrorInfoContainer_If_Contained()
        {
            //Arrange
            var owner = _autoMocker.Mock<IEntity>();
            var errorInfoContainerToRemove = _autoMocker.Mock<IErrorInfoContainer>();
            _entityToErrorInfoContainerMapper.Expect(c => c.ContainsKey(owner)).Return(true);
            _entityToErrorInfoContainerMapper.Expect(c => c[owner]).Return(errorInfoContainerToRemove);
            _errorInfoContainerCollection.Expect(c => c.Remove(errorInfoContainerToRemove)).Return(true);
            _entityToErrorInfoContainerMapper.Expect(c => c.Remove(owner)).Return(true);

            //Act
            var result = _sut.RemoveErrorInfoContainer(owner);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void RemoveErrorInfoContainer_DoesNot_Remove_ErrorInfoContainer_If_Not_Contained()
        {
            //Arrange
            var owner = _autoMocker.Mock<IEntity>();
            _entityToErrorInfoContainerMapper.Expect(c => c.ContainsKey(owner)).Return(false);

            //Act
            var result = _sut.RemoveErrorInfoContainer(owner);

            //Assert
            Assert.IsFalse(result);
            _entityToErrorInfoContainerMapper.AssertWasNotCalled(c => c.Remove(owner));
        }

        [Test]
        public void Clear_Clears_Correctly()
        {
            //Arrange
            _entityToErrorInfoContainerMapper.Expect(c => c.Clear());
            _errorInfoContainerCollection.Expect(c => c.Clear());

            //Act
            _sut.Clear();
        }
    }
}
