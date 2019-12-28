using HH.ViewModel.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IDialogSettings : IViewModel
    {
        string Title { get; set; }
    }
}
