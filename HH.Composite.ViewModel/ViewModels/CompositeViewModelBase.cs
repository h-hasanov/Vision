using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HH.Composite.Model.Interfaces;
using HH.Composite.ViewModel.Interfaces;
using HH.Data.Collections.Generics.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.Data.Filter.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.ViewModels;

namespace HH.Composite.ViewModel.ViewModels
{
    public abstract class CompositeViewModelBase<TModel, TViewModel> :
        ViewModelBase,
        ICompositeViewModel<TModel, TViewModel>
        where TModel : class, IComposite<TModel>
        where TViewModel : class, ICompositeViewModel<TModel, TViewModel>
    {
        private readonly ICommandFactory _commandFactory;

        #region Fields

        private readonly Func<TModel, TViewModel> _viewModelFactory;
        private readonly TModel _model;
        private bool _isExpanded;
        private bool _isSelected;
        private bool _isVisible;
        private readonly IReadOnlyEntityCollectionViewModel<TModel, TViewModel> _children;
        private ICommand _expandAllChildrenCommand;
        private ICommand _collapseAllChildrenCommand;

        #endregion

        #region Constructors

        protected CompositeViewModelBase(ICompositeEntityCollectionViewModelFactory compositeEntityCollectionViewModelFactory, ICommandFactory commandFactory,
            TModel model, Func<TModel, TViewModel> viewModelFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
            _viewModelFactory = viewModelFactory.ArgumentNullCheck(nameof(viewModelFactory));
            _model = model.ArgumentNullCheck(nameof(model));
            compositeEntityCollectionViewModelFactory.ArgumentNullCheck(nameof(compositeEntityCollectionViewModelFactory));
           
            IsVisible = true;
            _children = compositeEntityCollectionViewModelFactory.CreateCompositeEntityCollectionViewModel(_model.Children, OnChildAdded, OnChildRemoving);
        }

        protected TViewModel OnChildAdded(TModel model)
        {
            var newViewModel = _viewModelFactory(model);
            newViewModel.SetParent(This);
            return newViewModel;
        }

        protected void OnChildRemoving(TViewModel node)
        {
            node.SetParent(default(TViewModel));
        }

        #endregion

        #region Properties

        public TModel Model { get { return _model; } }

        public TViewModel Parent { get; private set; }

        public bool HasChildren { get { return _model.HasChildren; } }

        public int Depth { get { return _model.Depth; } }

        public IReadOnlyObservableDataCollection<TViewModel> Children
        {
            get { return _children; }
        }

        protected abstract TViewModel This { get; }

        #endregion

        #region Commands

        public ICommand ExpandAllChildrenCommand
        {
            get {
                return _expandAllChildrenCommand ??
                       (_expandAllChildrenCommand =
                           _commandFactory.CreateCommand(ExpandAll, CanExpandAll));
            }
        }

        public ICommand CollapseAllChildrenCommand
        {
            get {
                return _collapseAllChildrenCommand ??
                       (_collapseAllChildrenCommand =
                           _commandFactory.CreateCommand(CollapseAll, CanCollapseAll));
            }
        }

        #endregion

        #region IExpandable

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                SetProperty(ref _isExpanded, value);

                if (!_isExpanded) return; //if collapsed, do not do anything
                if (Parent == null) return; // do not do anything if parent null
                if (Parent.IsExpanded) return; //do not do anything if parent already expanded

                Parent.IsExpanded = _isExpanded; //expand parent
            }
        }

        #endregion

        #region ISelectable

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);

                if (!_isSelected) return; // if not selected do not do anything
                if (Parent == null) return; // do not do anything if parent null
                if (Parent.IsExpanded) return; //do not do anything if parent already expanded

                Parent.IsExpanded = true; //expand parent
            }
        }

        #endregion

        #region IVisible

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                SetProperty(ref _isVisible, value);
                if (!_isVisible)
                {
                    //If the node is invisible, all of its children should be invisible too.
                    ForeachChild(child => child.IsVisible = false);
                }
                else
                {
                    //if the node is visible, its parents must be visible too.
                    if (Parent == null) return; //Do not do anything if no parent
                    if (Parent.IsVisible) return; //if parent visible do not do anything.

                    Parent.IsVisible = true; //set parent to visible.
                }
            }
        }

        #endregion

        #region Methods

        public void SetParent(TViewModel parent)
        {
            Parent = parent;
        }

        #endregion

        #region Search

        public IEnumerable<TViewModel> Search(ICriteria<TViewModel> searchCriteria)
        {
            foreach (var meetCriterion in searchCriteria.MeetCriteria(Children))
            {
                yield return meetCriterion;
            }

            foreach (var viewModel in Children)
            {
                foreach (var model in viewModel.Search(searchCriteria))
                {
                    yield return model;
                }
            }
        }

        #endregion

        #region Filter

        public void Filter(ICriteria<TViewModel> filterCriteria)
        {
            var result = Search(filterCriteria).ToList();
            IsVisible = false;
            foreach (var perfectFitSolutionItemViewModel in result)
            {
                perfectFitSolutionItemViewModel.IsVisible = true;
                perfectFitSolutionItemViewModel.IsExpanded = true;
            }
        }

        #endregion

        #region Visitor

        /// <summary>
        /// This is the visitor pattern.
        /// Allows external users to pass a visitor that can get specific information.
        /// http://www.tutorialspoint.com/design_pattern/design_pattern_quick_guide.htm
        /// </summary>
        /// <param name="visitor"></param>
        public void Accept(ICompositeViewModelVisitor<TModel, TViewModel> visitor)
        {
            visitor.Visit(This);
            ForeachChild(visitor.Visit);
        }

        #endregion

        #region Expand/Collapse

        public bool CanExpandAll()
        {
            return HasChildren;
        }

        public void ExpandAll()
        {
            ExpandOrCollapse(true, c => c.ExpandAll());
        }

        public bool CanCollapseAll()
        {
            return HasChildren;
        }

        public void CollapseAll()
        {
            ExpandOrCollapse(false, c => c.CollapseAll());
        }

        private void ExpandOrCollapse(bool expand, Action<TViewModel> actionOnChildren)
        {
            ForeachChild(child =>
            {
                child.IsExpanded = expand;
                actionOnChildren(child);
            });
        }

        #endregion

        #region Helpers

        protected void ForeachChild(Action<TViewModel> action)
        {
            foreach (var child in Children)
            {
                action(child);
            }
        }

        #endregion

        #region Dispose

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
            if (_children != null)
                _children.Dispose();
        }

        #endregion
    }
}
