using System.Collections.Generic;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.Wizard.Implementations;
using HH.ViewModel.Services.Wizard.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public sealed class DialogViewModelFactory : IDialogViewModelFactory
    {
        private readonly ICommandFactory _commandFactory;

        public DialogViewModelFactory(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory.ArgumentNullCheck(nameof(commandFactory));
        }

        public IOkDialogViewModel CreateOkDialogViewModel(IViewModel viewModel, IDialogSettings dialogSettings)
        {
            return new OkDialogViewModel(_commandFactory, viewModel, dialogSettings);
        }

        public IOkCancelDialogViewModel CreateOkCancelDialogViewModel(IEditableDialogContent viewModel, IDialogSettings dialogSettings)
        {
            return new OkCancelDialogViewModel(_commandFactory, viewModel, dialogSettings);
        }

        public IOkApplyCancelDialogViewModel CreateOkApplyCancelDialogViewModel(IEditableDialogContent viewModel,
            IDialogSettings dialogSettings)
        {
            return new OkApplyCancelDialogViewModel(_commandFactory, viewModel, dialogSettings);
        }

        public IWizardViewModel CreateWizardViewModel(IEnumerable<IWizardStepViewModel> wizardSteps, IDialogSettings dialogSettings)
        {
            return new WizardViewModel(dialogSettings, wizardSteps, _commandFactory);
        }
    }
}
