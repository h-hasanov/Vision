using HH.Data.Collections.Generics.Interfaces;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.ViewModel.Interfaces;
using IDescriptive = HH.ErrorManager.Model.Models.Interfaces.IDescriptive;

namespace HH.ErrorManager.ViewModel.Interfaces
{
    public interface IErrorInfoContainerViewModel : IEntityViewModel, IDescriptive, IExpandable
    {
        IReadOnlyObservableDataCollection<IErrorInfoViewModel> ErrorInfoViewModelCollection { get; }
    }
}
