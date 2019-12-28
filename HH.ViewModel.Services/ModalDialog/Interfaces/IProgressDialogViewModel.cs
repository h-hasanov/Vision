using System.Threading.Tasks;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IProgressDialogViewModel
    {
        /// <summary>
        /// Sets the ProgressBar's IsIndeterminate to true. To set it to false, call SetProgress.
        /// </summary>
        void SetIndeterminate();

        /// <summary>
        /// Sets the dialog's progress bar value and sets IsIndeterminate to false.
        /// </summary>
        /// <param name="value">The percentage to set as the value.</param>
        void SetProgress(double value);

        /// <summary>
        /// Sets the dialog's message content.
        /// </summary>
        /// <param name="message"></param>
        void SetProgressMessage(string message);

        /// <summary>
        /// Begins an operation to close the ProgressDialog.
        /// </summary>
        /// 
        /// <returns>
        /// A task representing the operation.
        /// </returns>
        Task CloseAsync();
    }
}
