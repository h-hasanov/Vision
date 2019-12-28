using HH.Data.Entity.ViewModel.Interfaces;
using HH.EnvironmentServices.Utils;
using HH.ErrorManager.Model.Models.Interfaces;
using HH.ErrorManager.ViewModel.Interfaces;

namespace HH.ErrorManager.ViewModel.Implementations
{
    public sealed class ErrorInfoContainerViewModelFactory : IErrorInfoContainerViewModelFactory
    {
        private readonly IErrorInfoViewModelFactory _errorInfoViewModelFactory;
        private readonly IEntityCollectionViewModelFactory _entityCollectionViewModelFactory;

        public ErrorInfoContainerViewModelFactory(IEntityCollectionViewModelFactory entityCollectionViewModelFactory,
            IErrorInfoViewModelFactory errorInfoViewModelFactory)
        {
            _errorInfoViewModelFactory = errorInfoViewModelFactory.ArgumentNullCheck(nameof(_errorInfoViewModelFactory));
            _entityCollectionViewModelFactory =
                entityCollectionViewModelFactory.ArgumentNullCheck(nameof(entityCollectionViewModelFactory));
        }

        public IErrorInfoContainerViewModel CreateErrorInfoContainerViewModel(IErrorInfoContainer errorInfoContainer)
        {
            return new ErrorInfoContainerViewModel(errorInfoContainer, _entityCollectionViewModelFactory, _errorInfoViewModelFactory);
        }
    }
}
