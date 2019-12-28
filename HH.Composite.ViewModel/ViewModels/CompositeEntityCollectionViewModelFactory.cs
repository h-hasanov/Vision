using System;
using HH.Composite.Model.Interfaces;
using HH.Composite.ViewModel.Interfaces;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;

namespace HH.Composite.ViewModel.ViewModels
{
    public sealed class CompositeEntityCollectionViewModelFactory : ICompositeEntityCollectionViewModelFactory
    {
        public IReadOnlyEntityCollectionViewModel<TModel, TViewModel> CreateCompositeEntityCollectionViewModel<TModel, TViewModel>(IReadOnlyEntityCollection<TModel> source,
            Func<TModel, TViewModel> viewModelFactory, Action<TViewModel> onViewModelRemoving) where TModel : class, IComposite<TModel> where TViewModel : class, ICompositeViewModel<TModel, TViewModel>
        {
            return new CompositeEntityCollectionViewModel<TModel, TViewModel>(source,viewModelFactory, onViewModelRemoving);
        }
    }
}
