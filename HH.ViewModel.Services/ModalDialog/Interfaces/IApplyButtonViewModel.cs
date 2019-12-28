using System.Windows.Input;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IApplyButtonViewModel 
    {
        ICommand ApplyCommand { get; }
        void Apply();
    }
}
