using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.ViewModel.Interfaces
{
    public interface IErrorInfoContainerViewModelFactory
    {
        IErrorInfoContainerViewModel CreateErrorInfoContainerViewModel(IErrorInfoContainer errorInfoContainer);
    }
}
