using System.Collections.Generic;
using System.Windows.Input;
using HH.Composite.Model.Interfaces;
using HH.Data.Collections.Generics.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.Data.Filter.Interfaces;
using HH.ViewModel.Interfaces;

namespace HH.Composite.ViewModel.Interfaces
{
    public interface ICompositeViewModel<TModel, TViewModel> :
        IEntityViewModel,
        IExpandableHierarchy,
        ISelectable,
        IVisible
        where TModel : IComposite<TModel>
        where TViewModel : ICompositeViewModel<TModel, TViewModel>
    {

        #region Properties

        TModel Model { get; }
        TViewModel Parent { get; }
        bool HasChildren { get; }
        int Depth { get; }
        IReadOnlyObservableDataCollection<TViewModel> Children { get; }

        #endregion

        #region Commands

        ICommand ExpandAllChildrenCommand { get; }

        ICommand CollapseAllChildrenCommand { get; }

        #endregion

        #region Methods

        void SetParent(TViewModel parent);

        #endregion

        #region Searching

        IEnumerable<TViewModel> Search(ICriteria<TViewModel> searchCriteria);

        #endregion

        #region Filter

        void Filter(ICriteria<TViewModel> filterCriteria);

        #endregion

        #region Visitor

        void Accept(ICompositeViewModelVisitor<TModel, TViewModel> visitor);

        #endregion

        #region Expand/Collapse

        bool CanExpandAll();
        bool CanCollapseAll();

        #endregion
    }
}
