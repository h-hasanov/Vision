using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Enums;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.Wizard.Interfaces;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using DialogView = HH.ViewModel.Services.Win.ModalDialog.Views.DialogView;
using System.Collections.Generic;
using HH.EnvironmentServices.Utils;

namespace HH.ViewModel.Services.Win.ModalDialog.Implementations
{
    [DebuggerNonUserCode]
    public sealed class DialogManager : IDialogManager
    {
        private readonly IDialogViewModelFactory _dialogViewModelFactory;
        private static readonly MetroWindow MainWindow = (MetroWindow)Application.Current.MainWindow;

        public DialogManager(IDialogViewModelFactory dialogViewModelFactory)
        {
            _dialogViewModelFactory = dialogViewModelFactory.ArgumentNullCheck(nameof(dialogViewModelFactory));
        }

        #region Async dialogs

        public Task<DialogResult> OpenOkDialogAsync(IViewModel viewModel, IDialogSettings dialogSettings)
        {
            return ShowDialogAsync(_dialogViewModelFactory.CreateOkDialogViewModel(viewModel, dialogSettings));
        }

        public Task<DialogResult> OpenOkCancelDialogAsync(IEditableDialogContent viewModel, IDialogSettings dialogSettings)
        {
            return ShowDialogAsync(_dialogViewModelFactory.CreateOkCancelDialogViewModel(viewModel, dialogSettings));
        }

        public Task<DialogResult> OpenOkApplyCancelDialogAsync(IEditableDialogContent viewModel, IDialogSettings dialogSettings)
        {
            return ShowDialogAsync(_dialogViewModelFactory.CreateOkApplyCancelDialogViewModel(viewModel, dialogSettings));
        }

        public Task<DialogResult> ShowWizardDialogAsync(IEnumerable<IWizardStepViewModel> wizardSteps, IDialogSettings dialogSettings)
        {
            return ShowDialogAsync(_dialogViewModelFactory.CreateWizardViewModel(wizardSteps, dialogSettings));
        }

        public Task<TResult> ShowDialogAsync<TResult>(IDialogViewModel<TResult> dialogViewModel)
        {
            var dialogView = CreateMetroDialog(dialogViewModel);
            return ShowDialogAsync(dialogView, dialogViewModel);
        }

        private static async Task<TResult> ShowDialogAsync<TResult>(BaseMetroDialog dialogView, IDialogViewModel<TResult> dialogViewModel)
        {
            await MainWindow.ShowMetroDialogAsync(dialogView);
            var result = await dialogViewModel.WaitUntilClosed();
            await MainWindow.HideMetroDialogAsync(dialogView);
            return result;
        }

        #endregion Async dialogs

        private static BaseMetroDialog CreateMetroDialog<TResult>(IDialogViewModel<TResult> dialogViewModel)
        {
            return new DialogView { DataContext = dialogViewModel };
        }
    }
}