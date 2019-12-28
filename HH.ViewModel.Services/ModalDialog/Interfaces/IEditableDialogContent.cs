using HH.ViewModel.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IEditableDialogContent : IValidatableViewModel
    {
        void AcceptChanges();
        bool CanAcceptChanges();
    }
}
