using System.Collections.Specialized;
using HH.EnvironmentServices.Interfaces;
using HH.EnvironmentServices.Services;
using HH.TestUtils;
using NUnit.Framework;

namespace HH.EnvironmentServices.Tests.Services
{
    [TestFixture]
    internal sealed class NotifyCollectionChangedEventArgsFactoryTests
    {
        private AutoMocker _autoMocker;
        private INotifyCollectionChangedEventArgsFactory _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _sut = new NotifyCollectionChangedEventArgsFactory();
        }

        [TearDown]
        public void TearDown()
        {
            _autoMocker.VerifyAllExpectations();
        }

        [Test]
        public void CreateNotifyCollectionChangedEventArgs_With_Action_Returns_Correct_EventArgs()
        {
            //Arrange
            const NotifyCollectionChangedAction action = NotifyCollectionChangedAction.Reset;

            //Act
            var result = _sut.CreateNotifyCollectionChangedEventArgs(action);

            //Assert
            Assert.AreEqual(action, result.Action);

            Assert.IsNull(result.NewItems);
            Assert.IsNull(result.OldItems);

            Assert.AreEqual(-1, result.NewStartingIndex);
            Assert.AreEqual(-1, result.OldStartingIndex);
        }

        [TestCase(NotifyCollectionChangedAction.Add)]
        [TestCase(NotifyCollectionChangedAction.Remove)]
        public void CreateNotifyCollectionChangedEventArgs_With_Action_ChangedItem_Index_Returns_Correct_EventArgs(NotifyCollectionChangedAction action)
        {
            //Arrange
            var changedItem = _autoMocker.Mock<INotifyCollectionChangedEventArgsFactory>();
            const int index = int.MaxValue;

            //Act
            var result = _sut.CreateNotifyCollectionChangedEventArgs(action, changedItem, index);

            //Assert
            Assert.AreEqual(action, result.Action);

            if (action == NotifyCollectionChangedAction.Add)
            {
                CollectionAssert.AreEqual(new[] { changedItem }, result.NewItems);
                Assert.AreEqual(index, result.NewStartingIndex);

                Assert.IsNull(result.OldItems);
                Assert.AreEqual(-1, result.OldStartingIndex);
            }

            if (action == NotifyCollectionChangedAction.Remove)
            {
                CollectionAssert.AreEqual(new[] { changedItem }, result.OldItems);
                Assert.AreEqual(index, result.OldStartingIndex);

                Assert.IsNull(result.NewItems);
                Assert.AreEqual(-1, result.NewStartingIndex);
            }
        }

        [Test]
        public void CreateNotifyCollectionChangedEventArgs_Creates_Args_Correctly()
        {
            //Arrange
            object newItem = 4;
            object oldItem = 5;
            const int index = int.MaxValue;

            //Act
            var result = _sut.CreateNotifyCollectionChangedEventArgs(newItem, oldItem, index);

            //Assert
            Assert.AreEqual(newItem, result.NewItems[0]);
            Assert.AreEqual(1, result.NewItems.Count);

            Assert.AreEqual(oldItem, result.OldItems[0]);
            Assert.AreEqual(1, result.OldItems.Count);

            Assert.AreEqual(index, result.NewStartingIndex);
            Assert.AreEqual(index, result.OldStartingIndex);
        }
    }
}
