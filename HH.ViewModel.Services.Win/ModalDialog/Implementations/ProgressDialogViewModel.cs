using System.Diagnostics;
using System.Threading.Tasks;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Services.ModalDialog.Interfaces;

namespace HH.ViewModel.Services.Win.ModalDialog.Implementations
{
    [DebuggerNonUserCode]
    internal sealed class ProgressDialogViewModel : IProgressDialogViewModel
    {
        private readonly MahApps.Metro.Controls.Dialogs.ProgressDialogController _progressDialogController;

        public ProgressDialogViewModel(MahApps.Metro.Controls.Dialogs.ProgressDialogController progressDialogController)
        {
            _progressDialogController = progressDialogController.ArgumentNullCheck(nameof(progressDialogController));
        }

        public void SetIndeterminate()
        {
            _progressDialogController.SetIndeterminate();
        }

        public void SetProgress(double value)
        {
            _progressDialogController.SetProgress(value);
        }

        public void SetProgressMessage(string message)
        {
            _progressDialogController.SetMessage(message);
        }

        public Task CloseAsync()
        {
            return _progressDialogController.CloseAsync();
        }
    }
}
