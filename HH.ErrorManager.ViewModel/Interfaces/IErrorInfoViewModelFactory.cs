using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.ViewModel.Interfaces
{
    public interface IErrorInfoViewModelFactory
    {
        IErrorInfoViewModel CreateErrorInfoViewModel(IErrorInfo errorInfo);
    }
}
