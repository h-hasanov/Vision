using System.Windows.Input;
using HH.Data.Entity.ViewModel.Interfaces;
using HH.ErrorManager.Model.Enums;
using HH.ErrorManager.Model.Models.Interfaces;

namespace HH.ErrorManager.ViewModel.Interfaces
{
    public interface IErrorInfoViewModel : IEntityViewModel, IDescriptive
    {
        ErrorSeverity Severity { get; }
        ICommand NavigateToErrorCommand { get; }
        void NavigateToError();
    }
}
