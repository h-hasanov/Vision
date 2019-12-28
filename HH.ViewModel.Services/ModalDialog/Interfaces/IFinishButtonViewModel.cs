using System.Windows.Input;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IFinishButtonViewModel
    {
        ICommand FinishCommand { get; }
        void Finish();
        bool CanFinish();
    }
}
