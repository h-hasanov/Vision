using HH.Composite.Model.Interfaces;

namespace HH.Composite.ViewModel.Interfaces
{
    public interface ICompositeViewModelVisitor<TModel, TViewModel>
        where TModel : IComposite<TModel>
        where TViewModel : ICompositeViewModel<TModel, TViewModel>
    {
        void Visit(ICompositeViewModel<TModel, TViewModel> node);
    }
}