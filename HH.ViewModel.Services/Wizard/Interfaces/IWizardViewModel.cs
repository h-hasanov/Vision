using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Interfaces;

namespace HH.ViewModel.Services.Wizard.Interfaces
{
    public interface IWizardViewModel : IDialogViewModel<DialogResult>, 
        ICancelButtonViewModel, 
        IBackButtonViewModel, 
        INextButtonViewModel, 
        IFinishButtonViewModel
    {
        IWizardStepViewModel[] Steps { get; }
        IWizardStepViewModel CurrentStep { get; }
    }
}
