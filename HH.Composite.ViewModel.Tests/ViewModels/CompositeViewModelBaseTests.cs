using System.Linq;
using System.Windows.Input;
using HH.Composite.Model.Interfaces;
using HH.Composite.ViewModel.Interfaces;
using HH.Composite.ViewModel.ViewModels;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.Data.Filter.Interfaces;
using HH.TestUtils;
using HH.ViewModel.Interfaces;
using NUnit.Framework;
using Rhino.Mocks;

namespace HH.Composite.ViewModel.Tests.ViewModels
{
    [TestFixture]
    internal sealed class CompositeViewModelBaseTests
    {
        private AutoMocker _autoMocker;
        private ICompositeViewModelFactory _compositeViewModelFactory;
        private ICompositeEntityCollectionViewModelFactory _compositeEntityCollectionViewModelFactory;
        private IReadOnlyEntityCollectionViewModel<ITestComposite, ITestCompositeViewModel> _children;
        private IReadOnlyEntityCollection<ITestComposite> _modelChildren;
        private ITestComposite _model;
        private ITestCompositeViewModel _parent;
        private ICommandFactory _commandFactory;
        private ICommand _expandAllChildrenCommand;
        private ICommand _collapseAllChildrenCommand;
        private ITestCompositeViewModel _sut;

        [SetUp]
        public void Setup()
        {
            _autoMocker = new AutoMocker();
            _compositeViewModelFactory = _autoMocker.Mock<ICompositeViewModelFactory>();
            _compositeEntityCollectionViewModelFactory = _autoMocker.Mock<ICompositeEntityCollectionViewModelFactory>();
            _children = _autoMocker.Mock<IReadOnlyEntityCollectionViewModel<ITestComposite, ITestCompositeViewModel>>();
            _model = _autoMocker.Mock<ITestComposite>();
            _modelChildren = _autoMocker.Mock<IReadOnlyEntityCollection<ITestComposite>>();
            _parent = _autoMocker.Mock<ITestCompositeViewModel>();
            _commandFactory = _autoMocker.Mock<ICommandFactory>();
            _expandAllChildrenCommand = _autoMocker.Mock<ICommand>();
            _collapseAllChildrenCommand = _autoMocker.Mock<ICommand>();

            InitializeSut();
            _sut = new TestCompositeViewModel(_compositeEntityCollectionViewModelFactory, _compositeViewModelFactory, _commandFactory, _model);
            _sut.SetParent(_parent);
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
            Assert.IsTrue(_sut.IsVisible);
            Assert.AreEqual(_model, _sut.Model);
            Assert.AreEqual(_children, _sut.Children);
            Assert.AreEqual(_parent, _sut.Parent);
        }

        [Test]
        public void ExpandAllChildrenCommand_Returns_Expected_Command()
        {
            //Arrange
            _commandFactory.Expect(c => c.CreateCommand(_sut.ExpandAll, _sut.CanExpandAll))
                .Repeat.Once()
                .Return(_expandAllChildrenCommand);

            //Act
            var resultOne = _sut.ExpandAllChildrenCommand;
            var resultTwo = _sut.ExpandAllChildrenCommand;

            //Assert
            Assert.AreEqual(_expandAllChildrenCommand, resultOne);
            Assert.AreEqual(_expandAllChildrenCommand, resultTwo);
        }

        [Test]
        public void CollapseAllChildrenCommand_Returns_Expected_Command()
        {
            //Arrange
            _commandFactory.Expect(c => c.CreateCommand(_sut.CollapseAll, _sut.CanCollapseAll))
                .Repeat.Once()
                .Return(_collapseAllChildrenCommand);

            //Act
            var resultOne = _sut.CollapseAllChildrenCommand;
            var resultTwo = _sut.CollapseAllChildrenCommand;

            //Assert
            Assert.AreEqual(_collapseAllChildrenCommand, resultOne);
            Assert.AreEqual(_collapseAllChildrenCommand, resultTwo);
        }

        [Test]
        public void OnChildAdded_New_ViewModel_Created_And_Parent_Set()
        {
            //Arrange
            var model = _autoMocker.Mock<ITestComposite>();
            var viewModel = _autoMocker.Mock<ITestCompositeViewModel>();
            _compositeViewModelFactory.Expect(c => c.CreateViewModel(model)).Return(viewModel);
            viewModel.Expect(c => c.SetParent(_sut));

            //Act
            var result = ((TestCompositeViewModel)_sut).OnChildAddedCaller(model);

            //Assert
            Assert.AreEqual(viewModel, result);
        }

        [Test]
        public void OnChildRemoving_Parent_Set_To_Null()
        {
            //Arrange
            var viewModel = _autoMocker.Mock<ITestCompositeViewModel>();
            viewModel.Expect(c => c.SetParent(default(ITestCompositeViewModel)));

            //Act
            ((TestCompositeViewModel)_sut).OnChildRemovingCaller(viewModel);
        }

        #region Properties

        [TestCase(false)]
        [TestCase(true)]
        public void HasChildren_Returns_Model_HasChildren(bool hasChildren)
        {
            //Arrange
            _model.Expect(c => c.HasChildren).Return(hasChildren);

            //Act
            var result = _sut.HasChildren;

            //Assert
            Assert.AreEqual(hasChildren, result);
        }

        [Test]
        public void Depth_Returns_Model_Depth()
        {
            //Arrange
            const int depth = int.MaxValue / 123;
            _model.Expect(c => c.Depth).Return(depth);

            //Act
            var result = _sut.Depth;

            //Assert
            Assert.AreEqual(depth, result);
        }

        #endregion

        #region IsExpanded

        [Test]
        public void IsExpanded_Sets_Expanded_To_False_Does_Not_Try_To_Expand_Parent()
        {
            //Act
            _sut.IsExpanded = false;

            //Assert
            Assert.IsFalse(_sut.IsExpanded);
        }

        [Test]
        public void IsExpanded_Sets_Expanded_To_True_With_NullParent_Does_Not_Try_To_Expand_Parent()
        {
            //Arrange
            _sut.SetParent(null);

            //Act
            _sut.IsExpanded = true;

            //Assert
            Assert.IsTrue(_sut.IsExpanded);
        }

        [Test]
        public void IsExpanded_Sets_Expanded_To_True_With_Already_Expanded_Parent_Does_Not_Try_To_Expand_Parent()
        {
            //Arrange
            _parent.Expect(c => c.IsExpanded).Return(true);

            //Act
            _sut.IsExpanded = true;

            //Assert
            Assert.IsTrue(_sut.IsExpanded);
        }

        [Test]
        public void IsExpanded_Sets_Expanded_To_True_Expands_Parent()
        {
            //Arrange
            _parent.Expect(c => c.IsExpanded).Return(false);
            _parent.Expect(c => c.IsExpanded = true);

            //Act
            _sut.IsExpanded = true;

            //Assert
            Assert.IsTrue(_sut.IsExpanded);
        }

        #endregion

        #region IsSelected

        [Test]
        public void IsSelected_Sets_Expanded_To_False_Does_Not_Try_To_Expand_Parent()
        {
            //Act
            _sut.IsSelected = false;

            //Assert
            Assert.IsFalse(_sut.IsSelected);
        }

        [Test]
        public void IsSelected_Sets_Expanded_To_True_With_NullParent_Does_Not_Try_To_Expand_Parent()
        {
            //Arrange
            _sut.SetParent(null);

            //Act
            _sut.IsSelected = true;

            //Assert
            Assert.IsTrue(_sut.IsSelected);
        }

        [Test]
        public void IsSelected_Sets_Expanded_To_True_With_Already_Expanded_Parent_Does_Not_Try_To_Expand_Parent()
        {
            //Arrange
            _parent.Expect(c => c.IsExpanded).Return(true);

            //Act
            _sut.IsSelected = true;

            //Assert
            Assert.IsTrue(_sut.IsSelected);
        }

        [Test]
        public void IsSelected_Sets_Expanded_To_True_Expands_Parent()
        {
            //Arrange
            _parent.Expect(c => c.IsExpanded).Return(false);
            _parent.Expect(c => c.IsExpanded = true);

            //Act
            _sut.IsSelected = true;

            //Assert
            Assert.IsTrue(_sut.IsSelected);
        }

        #endregion

        #region IsVisible

        [Test]
        public void IsVisible_If_False_Hides_All_Children_Too()
        {
            //Arrange
            var parent = _autoMocker.Mock<ITestCompositeViewModel>();
            _sut.SetParent(parent);
            var childOne = _autoMocker.Mock<ITestCompositeViewModel>();
            childOne.Expect(c => c.IsVisible = false);

            var childTwo = _autoMocker.Mock<ITestCompositeViewModel>();
            childTwo.Expect(c => c.IsVisible = false);

            _children.StubEnumerator(new[] { childOne, childTwo });

            //Act
            _sut.IsVisible = false;

            //Assert
            _parent.AssertWasNotCalled(c => c.IsVisible);
        }

        [Test]
        public void IsVisible_If_True_With_Null_Parent_Does_Nothing()
        {
            //Arrange
            _sut.SetParent(null);

            //Assert
            Assert.DoesNotThrow(() => _sut.IsVisible = true);
        }

        [Test]
        public void IsVisible_If_True_With_With_Parent_Already_Visible_DoesNot_Make_Visible_Again()
        {
            //Arrange
            var parent = _autoMocker.Mock<ITestCompositeViewModel>();
            parent.Expect(c => c.IsVisible).Return(true);
            _sut.SetParent(parent);

            //Act
            _sut.IsVisible = true;

            //Assert
            parent.AssertWasNotCalled(c => c.IsVisible = true);
        }

        [Test]
        public void IsVisible_If_True_With_With_Parent_Not_Visible_Makes_Parent_Visible()
        {
            //Arrange
            var parent = _autoMocker.Mock<ITestCompositeViewModel>();
            parent.Expect(c => c.IsVisible).Return(false);
            parent.Expect(c => c.IsVisible = true);
            _sut.SetParent(parent);

            //Act
            _sut.IsVisible = true;
        }

        #endregion

        #region Expand/Collapse

        [Test]
        public void ExpandAll_Expands_All_Children()
        {
            //Arrange
            var childOne = _autoMocker.Mock<ITestCompositeViewModel>();
            childOne.Expect(c => c.IsExpanded = true);
            childOne.Expect(c => c.ExpandAll());

            var childTwo = _autoMocker.Mock<ITestCompositeViewModel>();
            childTwo.Expect(c => c.IsExpanded = true);
            childTwo.Expect(c => c.ExpandAll());
            _children.StubEnumerator(new[] { childOne, childTwo });

            //Act
            _sut.ExpandAll();
        }

        [Test]
        public void CollapseAll_Collapses_All_Children()
        {
            //Arrange
            var childOne = _autoMocker.Mock<ITestCompositeViewModel>();
            childOne.Expect(c => c.IsExpanded = false);
            childOne.Expect(c => c.CollapseAll());

            var childTwo = _autoMocker.Mock<ITestCompositeViewModel>();
            childTwo.Expect(c => c.IsExpanded = false);
            childTwo.Expect(c => c.CollapseAll());
            _children.StubEnumerator(new[] { childOne, childTwo });

            //Act
            _sut.CollapseAll();
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void CanExpandAll_Returns_Correct_Value(bool hasChildren, bool canExpand)
        {
            //Arrange
            _model.Expect(c => c.HasChildren).Return(hasChildren);

            //Act
            var result = _sut.CanExpandAll();

            //Assert
            Assert.AreEqual(canExpand, result);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void CanCollapseChildren_Returns_Correct_Value(bool hasChildren, bool canExpand)
        {
            //Arrange
            _model.Expect(c => c.HasChildren).Return(hasChildren);

            //Act
            var result = _sut.CanCollapseAll();

            //Assert
            Assert.AreEqual(canExpand, result);
        }

        #endregion

        #region Methods

        [Test]
        public void SetParent_Sets_Parent_Correctly()
        {
            //Arrange
            var newParent = _autoMocker.Mock<ITestCompositeViewModel>();

            //Act
            _sut.SetParent(newParent);

            //Assert
            Assert.AreEqual(newParent, _sut.Parent);
        }

        #endregion

        #region Searching

        [Test]
        public void Search_Searches_Correctly()
        {
            //Arrange
            var firstChild = _autoMocker.Mock<ITestCompositeViewModel>();
            var childOfFirstChild = _autoMocker.Mock<ITestCompositeViewModel>();
            var secondChild = _autoMocker.Mock<ITestCompositeViewModel>();
            _sut.Children.StubEnumerator(new[] { firstChild, secondChild });

            var criteria = _autoMocker.Mock<ICriteria<ITestCompositeViewModel>>();
            criteria.Expect(c => c.MeetCriteria(_sut.Children)).Return(new[] { secondChild });

            firstChild.Expect(c => c.Search(criteria)).Return(new[] { childOfFirstChild });
            secondChild.Expect(c => c.Search(criteria)).Return(Enumerable.Empty<ITestCompositeViewModel>());

            //Act
            var result = _sut.Search(criteria);

            //Assert
            CollectionAssert.AreEquivalent(new[] { secondChild, childOfFirstChild }, result);
        }

        #endregion

        #region Filtering

        [Test]
        public void Filter_Filters_Correctly()
        {
            //Arrange
            var firstChild = _autoMocker.Mock<ITestCompositeViewModel>();
            var childOfFirstChild = _autoMocker.Mock<ITestCompositeViewModel>();
            var secondChild = _autoMocker.Mock<ITestCompositeViewModel>();
            _sut.Children.StubEnumerator(new[] { firstChild, secondChild });

            var criteria = _autoMocker.Mock<ICriteria<ITestCompositeViewModel>>();
            criteria.Expect(c => c.MeetCriteria(_sut.Children)).Return(new[] { secondChild });

            firstChild.Expect(c => c.Search(criteria)).Return(new[] { childOfFirstChild });
            secondChild.Expect(c => c.Search(criteria)).Return(Enumerable.Empty<ITestCompositeViewModel>());

            secondChild.Expect(c => c.IsVisible = true).Repeat.Once();
            secondChild.Expect(c => c.IsExpanded = true).Repeat.Once();

            childOfFirstChild.Expect(c => c.IsVisible = true).Repeat.Once();
            childOfFirstChild.Expect(c => c.IsExpanded = true).Repeat.Once();

            //Act
            _sut.Filter(criteria);
        }

        #endregion

        #region Visitor

        [Test]
        public void Accept_Allows_Visitor_To_Visit_Node_and_Children()
        {
            //Arrange
            var visitor = _autoMocker.Mock<ITestDataTreNodeViewModelVisitor>();
            var childOne = _autoMocker.Mock<ITestCompositeViewModel>();
            var childTwo = _autoMocker.Mock<ITestCompositeViewModel>();
            _sut.Children.StubEnumerator(new[] { childOne, childTwo });
            visitor.Expect(c => c.Visit(_sut));
            visitor.Expect(c => c.Visit(childOne));
            visitor.Expect(c => c.Visit(childTwo));

            //Act
            _sut.Accept(visitor);
        }

        #endregion

        #region Dispose

        [Test]
        public void Dispose_Disposes_Correctly()
        {
            //Arrange
            _children.Expect(c => c.Dispose());

            //Act
            _sut.Dispose();
        }

        #endregion

        #region Helpers

        private void InitializeSut()
        {
            _model.Expect(c => c.Children).Return(_modelChildren);
            _compositeEntityCollectionViewModelFactory.Expect(
                c =>
                    c.CreateCompositeEntityCollectionViewModel(_modelChildren,
                        _compositeViewModelFactory.CreateViewModel, null)).IgnoreArguments().Return(_children);
        }

        #endregion
    }



    public interface ITestComposite : IComposite<ITestComposite>
    {

    }

    public interface ITestCompositeViewModel : ICompositeViewModel<ITestComposite, ITestCompositeViewModel>
    {

    }

    public interface ICompositeViewModelFactory
    {
        ITestCompositeViewModel CreateViewModel(ITestComposite model);
    }

    public interface ITestDataTreNodeViewModelVisitor
        : ICompositeViewModelVisitor<ITestComposite, ITestCompositeViewModel>
    {

    }

    public class TestCompositeViewModel : CompositeViewModelBase<ITestComposite, ITestCompositeViewModel>, ITestCompositeViewModel
    {
        public TestCompositeViewModel(
            ICompositeEntityCollectionViewModelFactory entityCollectionViewModelFactory,
            ICompositeViewModelFactory compositeViewModelFactory,
            ICommandFactory commandFactory, ITestComposite model)
            : base(entityCollectionViewModelFactory, commandFactory, model, compositeViewModelFactory.CreateViewModel)
        {
        }

        protected override ITestCompositeViewModel This
        {
            get { return this; }
        }

        public ITestCompositeViewModel OnChildAddedCaller(ITestComposite modelNode)
        {
            return OnChildAdded(modelNode);
        }

        public void OnChildRemovingCaller(ITestCompositeViewModel viewModel)
        {
            OnChildRemoving(viewModel);
        }
    }
}
