using System;
using HH.Composite.Model.Interfaces;
using HH.Composite.ViewModel.Interfaces;
using HH.Data.Entity.Model.Interfaces;
using HH.Data.Entity.ViewModel.Collections;
using HH.Data.Entity.ViewModel.Interfaces;

namespace HH.Composite.ViewModel.ViewModels
{
    internal sealed class CompositeEntityCollectionViewModel<TModel, TViewModel> :
        SimpleReadOnlyEntityCollectionViewModel<TModel, TViewModel>, IReadOnlyEntityCollectionViewModel<TModel, TViewModel>
        where TModel : class, IComposite<TModel>
        where TViewModel : class, ICompositeViewModel<TModel, TViewModel>
    {
        private readonly Action<TViewModel> _onViewModelRemoving;

        public CompositeEntityCollectionViewModel(IReadOnlyEntityCollection<TModel> source,
            Func<TModel, TViewModel> viewModelFactory, Action<TViewModel> onViewModelRemoving) : base(source, viewModelFactory)
        {
            _onViewModelRemoving = onViewModelRemoving;
        }

        protected override void OnItemRemoved(TViewModel viewModel)
        {
            _onViewModelRemoving(viewModel);
        }
    }
}
