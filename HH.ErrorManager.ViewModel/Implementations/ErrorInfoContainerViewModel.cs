using HH.Data.Collections.Generics.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Interfaces;
using HH.ViewModel.ViewModels;

namespace HH.ErrorManager.ViewModel.Implementations
{
    public sealed class ErrorInfoContainerViewModel : ViewModelBase, IErrorInfoContainerViewModel
    {
        private readonly IErrorInfoContainer _model;
        private bool _isExpanded;

        public ErrorInfoContainerViewModel(IErrorInfoContainer model,
            IEntityCollectionViewModelFactory entityCollectionViewModelFactory,
            IErrorInfoViewModelFactory errorInfoViewModelFactory)
        {
            _model = model.ArgumentNullCheck(nameof(model));
            ErrorInfoViewModelCollection =
                entityCollectionViewModelFactory.CreateReadOnlyEntityCollectionViewModel(model.ErrorInfoCollection,
                    errorInfoViewModelFactory.CreateErrorInfoViewModel);
        }

        public string Description { get { return _model.Description; } }
        public IReadOnlyObservableDataCollection<IErrorInfoViewModel> ErrorInfoViewModelCollection { get; }

        #region Expandable

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value); }
        }

        #endregion Expandable
    }
}
