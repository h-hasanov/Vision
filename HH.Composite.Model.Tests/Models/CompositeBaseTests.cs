using System;
using System.Diagnostics;
using System.Linq;
using HH.Composite.Model.Interfaces;
using HH.Composite.Model.Models;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Filter.Interfaces;
using HH.TestUtils;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Composite.Model.Tests.Models
{
    [TestFixture]
    internal sealed class CompositeBaseTests
    {
        private AutoMocker _autoMocker;
        private IEntityCollectionFactory _entityCollectionFactory;
        private IEntityCollection<ITestComposite> _children;
        private ITestComposite _parent;
        private ITestComposite _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _entityCollectionFactory = _autoMocker.Mock<IEntityCollectionFactory>();
            _children = _autoMocker.Mock<IEntityCollection<ITestComposite>>();
            _entityCollectionFactory.Expect(c => c.CreateEntityCollection<ITestComposite>(null)).IgnoreArguments().Return(_children);
            _parent = _autoMocker.Mock<ITestComposite>();

            _sut = new TestCompositeBase(_entityCollectionFactory);
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
            Assert.AreEqual(_children, _sut.Children);
            Assert.IsFalse(_sut.IsChanged);
        }

        [Test]
        public void IsChanged_Set_Correctly_ForChildEntities()
        {
            TestHelpers.AssertIsChangedSetCorrectlyForChildEntities(_sut, new IEntity[] { _children });
        }

        [Test]
        public void IsReadOnly_Set_Correctly()
        {
            TestHelpers.AssertPropertyChangedAndIsChanged(_sut, value => _sut.IsReadOnly = value, false, true, nameof(_sut.IsReadOnly));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void HasChildren_Returns_Correct_Value(bool hasChildren)
        {
            //Arrange
            var childCount = hasChildren ? 1 : 0;
            _children.Expect(c => c.Count).Return(childCount);

            //Act
            var result = _sut.HasChildren;

            //Assert
            Assert.AreEqual(hasChildren, result);
        }

        [Test]
        public void Depth_Returns_Zero_If_No_Parent()
        {
            //Act
            var result = _sut.Depth;

            //Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Depth_Returns_Correct_Depth_If_Has_Parent()
        {
            //Arrange
            const int parentDepth = 10;
            _sut.SetParent(_parent);
            _parent.Expect(c => c.Depth).Return(parentDepth);

            //Act
            var result = _sut.Depth;

            //Assert
            Assert.AreEqual(parentDepth + 1, result);
        }

        #region AddChild

        [Test]
        public void AddChild_Adds_Child_Correctly()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var newItem = _autoMocker.Mock<ITestComposite>();
            newItem.Expect(c => c.Parent).Return(default(ITestComposite));
            newItem.Expect(c => c.SetParent(_sut));
            _children.Expect(c => c.Add(newItem));

            //Act
            _sut.AddChild(newItem);
        }

        [Test]
        public void AddChild_Does_Not_Add_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;
            var newItem = _autoMocker.Mock<ITestComposite>();

            //Act
            _sut.AddChild(newItem);

            //Assert
            newItem.AssertWasNotCalled(c => c.SetParent(_sut));
            _children.AssertWasNotCalled(c => c.Add(newItem));
        }

        [Test]
        public void AddChild_Does_Not_Add_If_NewChild_Null()
        {
            //Arrange
            _sut.IsReadOnly = false;

            //Act
            _sut.AddChild(null);
        }

        [Test]
        public void AddChild_Does_Not_Add_If_NewChild_Has_Parent()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var newChild = _autoMocker.Mock<ITestComposite>();
            var newChildParent = _autoMocker.Mock<ITestComposite>();
            newChild.Expect(c => c.Parent).Return(newChildParent);

            //Act
            _sut.AddChild(newChild);
        }

        [Test]
        public void CanAddChild_Returns_False_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;
            var newItem = _autoMocker.Mock<ITestComposite>();

            //Act
            var result = _sut.CanAddChild(newItem);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanAddChild_Returns_False_If_NewItem_Null()
        {
            //Arrange
            _sut.IsReadOnly = false;

            //Act
            var result = _sut.CanAddChild(null);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanAddChild_Returns_False_If_NewItem_Has_Parent()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var newChild = _autoMocker.Mock<ITestComposite>();
            var newChildParent = _autoMocker.Mock<ITestComposite>();
            newChild.Expect(c => c.Parent).Return(newChildParent);

            //Act
            var result = _sut.CanAddChild(newChild);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanAddChild_Returns_True_If_NotReadOnly_NotNull_HasNoParent()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var newChild = _autoMocker.Mock<ITestComposite>();
            newChild.Expect(c => c.Parent).Return(null);

            //Act
            var result = _sut.CanAddChild(newChild);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region Remove

        [Test]
        public void Remove_Removes_From_Parent_If_Not_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var parent = _autoMocker.Mock<ITestComposite>();
            parent.Expect(c => c.RemoveChild(_sut));
            _sut.SetParent(parent);

            //Act
            _sut.Remove();
        }

        [Test]
        public void Remove_Does_Not_Remove_From_Parent_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;
            var parent = _autoMocker.Mock<ITestComposite>();
            _sut.SetParent(parent);

            //Act
            _sut.Remove();

            //Assert
            parent.AssertWasNotCalled(c => c.RemoveChild(parent));
        }

        [Test]
        public void Remove_Does_Not_Throw_If_Parent_Null()
        {
            //Arrange
            _sut.IsReadOnly = false;
            _sut.SetParent(null);

            //Assert
            Assert.DoesNotThrow(() => _sut.Remove());
        }

        [Test]
        public void CanRemove_Returns_Faslse_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;

            //Act
            var result = _sut.CanRemove();

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanRemove_Returns_True_If_NotReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = false;

            //Act
            var result = _sut.CanRemove();

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region RemoveChildAt

        [Test]
        public void RemoveChildAt_Removes_Correctly()
        {
            //Arrange
            _sut.IsReadOnly = false;
            const int totalCount = 123;
            _children.Expect(c => c.Count).Return(totalCount);
            const int indexToRemove = 12;
            var child = _autoMocker.Mock<ITestComposite>();
            _children.Expect(c => c.Contains(child)).Return(true);
            _children.Expect(c => c[indexToRemove]).Return(child);
            child.Expect(c => c.RemoveChildren());
            _children.Expect(c => c.Remove(child)).Return(true);
            child.Expect(c => c.SetParent(default(ITestComposite)));
            child.Expect(c => c.Dispose());

            //Act
            _sut.RemoveChildAt(indexToRemove);
        }

        [Test]
        public void RemoveChildAt_DoesNot_Remove_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;
            const int indexToRemove = 12;
            var child = _autoMocker.Mock<ITestComposite>();

            //Act
            _sut.RemoveChildAt(indexToRemove);

            //Assert
            _children.AssertWasNotCalled(c => c.Count);
            _children.AssertWasNotCalled(c => c[indexToRemove]);
            child.AssertWasNotCalled(c => c.RemoveChildren());
            child.AssertWasNotCalled(c => c.SetParent(default(ITestComposite)));
            child.AssertWasNotCalled(c => c.Dispose());
            _children.AssertWasNotCalled(c => c.Remove(child));
        }

        [TestCase(-1)]
        [TestCase(12)]
        [TestCase(13)]
        public void RemoveChildAt_DoesNot_Remove_If_Outside_Boundaries(int invalidIndex)
        {
            //Arrange
            _sut.IsReadOnly = false;
            _children.Expect(c => c.Count).Return(12);
            var child = _autoMocker.Mock<ITestComposite>();
           
            //Act
            _sut.RemoveChildAt(invalidIndex);

            //Assert
            _children.AssertWasNotCalled(c => c[invalidIndex]);
            child.AssertWasNotCalled(c => c.RemoveChildren());
            _children.AssertWasNotCalled(c => c.Remove(child));
            child.AssertWasNotCalled(c => c.SetParent(default(ITestComposite)));
            child.AssertWasNotCalled(c => c.Dispose());
        }

        [Test]
        public void CanRemoveChildAt_Returns_False_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;

            //Act
            var result = _sut.CanRemoveChildAt(10);

            //Assert
            Assert.IsFalse(result);
        }

        [TestCase(-1)]
        [TestCase(12)]
        [TestCase(13)]
        public void CanRemoveChildAt_Returns_False_If_Outside_Boundaries(int invalidIndex)
        {
            //Arrange
            _sut.IsReadOnly = false;
            _children.Expect(c => c.Count).Return(12);

            //Act
            var result = _sut.CanRemoveChildAt(invalidIndex);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanRemoveChildAt_Returns_True_If_NotReadOnly_And_InsideBoundaries()
        {
            //Arrange
            _sut.IsReadOnly = false;
            _children.Expect(c => c.Count).Return(12);

            //Act
            var result = _sut.CanRemoveChildAt(10);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region RemoveChild

        [Test]
        public void RemoveChild_Removes_Correctly()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var child = _autoMocker.Mock<ITestComposite>();
            child.Expect(c => c.RemoveChildren());
            _children.Expect(c => c.Contains(child)).Return(true);
            _children.Expect(c => c.Remove(child)).Return(true);
            child.Expect(c => c.SetParent(default(ITestComposite)));
            child.Expect(c => c.Dispose());

            //Act
            _sut.RemoveChild(child);
        }

        [Test]
        public void RemoveChild_DoesNot_Remove_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;
            var child = _autoMocker.Mock<ITestComposite>();

            //Act
            _sut.RemoveChild(child);

            //Assert
            child.AssertWasNotCalled(c => c.RemoveChildren());
            _children.AssertWasNotCalled(c => c.Contains(child));
            _children.AssertWasNotCalled(c => c.Remove(child));
            child.AssertWasNotCalled(c => c.SetParent(default(ITestComposite)));
            child.AssertWasNotCalled(c => c.Dispose());
        }

        [Test]
        public void RemoveChild_DoesNot_Remove_If_Child_Null()
        {
            //Arrange
            _sut.IsReadOnly = false;

            //Act
            _sut.RemoveChild(null);
        }

        [Test]
        public void CanRemoveChild_Returns_False_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;
            var child = _autoMocker.Mock<ITestComposite>();

            //Act
            var result = _sut.CanRemoveChild(child);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanRemoveChild_Returns_False_If_Child_Null()
        {
            //Arrange
            _sut.IsReadOnly = false;

            //Act
            var result = _sut.CanRemoveChild(null);

            //Assert
            Assert.IsFalse(result);
        }


        [Test]
        public void CanRemoveChild_Returns_False_If_Child_NotContained()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var child = _autoMocker.Mock<ITestComposite>();
            _children.Expect(c => c.Contains(child)).Return(false);

            //Act
            var result = _sut.CanRemoveChild(child);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanRemoveChild_Returns_True_If_NotReadOnly_ChildNotNull_ChildContained()
        {
            //Arrange
            _sut.IsReadOnly = false;
            var child = _autoMocker.Mock<ITestComposite>();
            _children.Expect(c => c.Contains(child)).Return(true);

            //Act
            var result = _sut.CanRemoveChild(child);

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        #region RemoveChildren

        [Test]
        public void RemoveChildren_DoesNot_Clear_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;

            //Act
            _sut.RemoveChildren();

            //Assert
            _children.AssertWasNotCalled(c => c.Count);
        }

        [Test]
        public void RemoveChildren_Clears_Correctly_If_Not_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = false;
            const int totalCount = 2;
            _children.Expect(c => c.Count).Return(totalCount);
            var firstChild = _autoMocker.Mock<ITestComposite>();
            var secondChild = _autoMocker.Mock<ITestComposite>();
            _children.Expect(c => c.Contains(secondChild)).Repeat.Once().Return(true);
            _children.Expect(c => c.Contains(firstChild)).Repeat.Once().Return(true);
            _children.Expect(c => c[0]).Repeat.Once().Return(secondChild);
            _children.Expect(c => c[0]).Repeat.Once().Return(firstChild);
            secondChild.Expect(c => c.RemoveChildren()).Repeat.Once();
            firstChild.Expect(c => c.RemoveChildren()).Repeat.Once();
            _children.Expect(c => c.Remove(secondChild)).Repeat.Once().Return(true);
            _children.Expect(c => c.Remove(firstChild)).Repeat.Once().Return(true);
            secondChild.Expect(c => c.SetParent(default(ITestComposite))).Repeat.Once();
            firstChild.Expect(c => c.SetParent(default(ITestComposite))).Repeat.Once();
            secondChild.Expect(c => c.Dispose()).Repeat.Once();
            firstChild.Expect(c => c.Dispose()).Repeat.Once();

            //Act
            _sut.RemoveChildren();
        }

        [Test]
        public void CanRemoveChildren_Returns_False_If_ReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = true;

            //Act
            var result = _sut.CanRemoveChildren();

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CanRemoveChildren_Returns_True_If_NotReadOnly()
        {
            //Arrange
            _sut.IsReadOnly = false;

            //Act
            var result = _sut.CanRemoveChildren();

            //Assert
            Assert.IsTrue(result);
        }

        #endregion

        [Test]
        public void SetParent_Sets_Parent_Correctly()
        {
            //Arrange
            var parent = _autoMocker.Mock<ITestComposite>();

            //Act
            _sut.SetParent(parent);

            //Assert
            Assert.AreEqual(parent, _sut.Parent);
        }

        [Test]
        public void Accept_Allows_Visitor_To_Visit_Node_and_Children()
        {
            //Arrange
            var visitor = _autoMocker.Mock<ITestCompositeVisitor>();
            var childOne = _autoMocker.Mock<ITestComposite>();
            var childTwo = _autoMocker.Mock<ITestComposite>();
            _sut.Children.StubEnumerator(new[] { childOne, childTwo });
            visitor.Expect(c => c.Visit(_sut));
            visitor.Expect(c => c.Visit(childOne));
            visitor.Expect(c => c.Visit(childTwo));

            //Act
            _sut.Accept(visitor);
        }

        #region Searching

        [Test]
        public void Search_Searches_Correctly()
        {
            //Arrange
            var firstChild = _autoMocker.Mock<ITestComposite>();
            var childOfFirstChild = _autoMocker.Mock<ITestComposite>();
            var secondChild = _autoMocker.Mock<ITestComposite>();
            _sut.Children.StubEnumerator(new[] { firstChild, secondChild });

            var criteria = _autoMocker.Mock<ICriteria<ITestComposite>>();
            criteria.Expect(c => c.MeetCriteria(_sut.Children)).Return(new[] { secondChild });

            firstChild.Expect(c => c.Search(criteria)).Return(new[] { childOfFirstChild });
            secondChild.Expect(c => c.Search(criteria)).Return(Enumerable.Empty<ITestComposite>());

            //Act
            var result = _sut.Search(criteria);

            //Assert
            CollectionAssert.AreEquivalent(new[] { secondChild, childOfFirstChild }, result);
        }

        #endregion

        #region PerformanceTest

        [Test]
        [Category("Performance")]
        [Ignore("Performance")]
        public void PerformanceTest()
        {
            //Arrange
            GC.Collect();
            const int depth = 10;
            const int numberOfChildNodes = 2;
            var root = new TestCompositeBase();
            var watch = new Stopwatch();
            watch.Start();

            //Act
            CreateNode(root, depth, 0, numberOfChildNodes);

            //Assert
            watch.Stop();
            Console.WriteLine(watch.Elapsed.TotalSeconds);
            Assert.IsTrue(watch.Elapsed.TotalSeconds < 0.2);
            Utils.AssertMemory(20);
        }

        private void CreateNode(ITestComposite currentNode, int depth, int currentDepth, int numberOfChildNodes)
        {
            if (currentDepth < depth)
            {
                for (var i = 0; i < numberOfChildNodes; i++)
                {
                    var newNode = new TestCompositeBase();
                    CreateNode(newNode, depth, currentDepth + 1, numberOfChildNodes);
                    currentNode.AddChild(newNode);
                }
            }
        }

        #endregion

        [Test]
        public void Dispose_Disposes_Correctly()
        {
            //Arrange
            _children.Expect(c => c.Dispose());

            //Act
            _sut.Dispose();
        }
    }

    public interface ITestCompositeVisitor : ICompositeVisitor<ITestComposite>
    {

    }

    public interface ITestComposite : IComposite<ITestComposite>
    {

    }

    public class TestCompositeBase : CompositeBase<ITestComposite>, ITestComposite
    {
        public TestCompositeBase(IEntityCollectionFactory entityCollectionFactory)
            : base(entityCollectionFactory)
        {

        }

        public TestCompositeBase()
        {

        }

        protected override ITestComposite This { get { return this; } }
    }
}
