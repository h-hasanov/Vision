using System.Linq;
using System.Windows.Input;
using HH.Data.Collections.Generics.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.ViewModel.Interfaces;
using HH.ViewModel.ViewModels;

namespace HH.ErrorManager.ViewModel.Implementations
{
    public sealed class ErrorManagerViewModel : ViewModelBase, IErrorManagerViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private ICommand _expandAllCommand;
        private ICommand _collapseAllCommand;

        public ErrorManagerViewModel(IErrorManager errorManager,
            IEntityCollectionViewModelFactory entityCollectionViewModelFactory,
            IErrorInfoContainerViewModelFactory errorInfoContainerViewModelFactory,
            ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
            errorManager.ArgumentNullCheck(nameof(errorManager));
            entityCollectionViewModelFactory.ArgumentNullCheck(nameof(entityCollectionViewModelFactory));
            errorInfoContainerViewModelFactory.ArgumentNullCheck(nameof(errorInfoContainerViewModelFactory));

            ErrorInfoContainerViewModelCollection =
                entityCollectionViewModelFactory.CreateReadOnlyEntityCollectionViewModel(errorManager.ErrorInfoContainerCollection,
                    errorInfoContainerViewModelFactory.CreateErrorInfoContainerViewModel);
        }

        #region Commands

        public ICommand ExpandAllCommand
        {
            get
            {
                return _expandAllCommand ?? (_expandAllCommand = _commandFactory.CreateCommand(ExpandAll, CanExpandAll));
            }
        }


        public ICommand CollapseAllCommand
        {
            get
            {
                return _collapseAllCommand ?? (_collapseAllCommand = _commandFactory.CreateCommand(CollapseAll, CanCollapseAll));
            }
        }

        #endregion Commands

        public IReadOnlyObservableDataCollection<IErrorInfoContainerViewModel> ErrorInfoContainerViewModelCollection
        {
            get;
        }

        #region Expand/Collapse

        public void ExpandAll()
        {
            foreach (var errorInfoContainerViewModel in ErrorInfoContainerViewModelCollection)
            {
                errorInfoContainerViewModel.IsExpanded = true;
            }
        }

        public bool CanExpandAll()
        {
            return CanExpandOrCollapse();
        }

        public void CollapseAll()
        {
            foreach (var errorInfoContainerViewModel in ErrorInfoContainerViewModelCollection)
            {
                errorInfoContainerViewModel.IsExpanded = false;
            }
        }

        public bool CanCollapseAll()
        {
            return CanExpandOrCollapse();
        }

        private bool CanExpandOrCollapse()
        {
            return ErrorInfoContainerViewModelCollection.Any();
        }

        #endregion Expand/Collapse
    }
}
