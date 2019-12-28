using System.Threading.Tasks;
using HH.ViewModel.Services.FileStorage.Interfaces;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.StandardDialog.Enums;
using HH.ViewModel.Services.StandardDialog.Implementations;

namespace HH.ViewModel.Services.StandardDialog.Interfaces
{
    public interface IStandardDialogService
    {
        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IFileInfo OpenFileDialog(FileTypeFilter filter);

        /// <summary>
        /// Shows the open file dialog.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IFileInfo OpenFileDialog(FileTypeFilter[] filters);

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filter">The file type filter.</param>
        /// <returns></returns>
        IFileInfo SaveFileDialog(string fileName, FileTypeFilter filter);

        /// <summary>
        /// Shows the save file dialog.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="filters">The file type filters.</param>
        /// <returns></returns>
        IFileInfo SaveFileDialog(string fileName, FileTypeFilter[] filters);

        /// <summary>
        /// Shows the open folder explorer.
        /// </summary>
        /// <returns></returns>
        IFileInfo ShowOpenFolderExplorer();

        /// <summary>
        /// Shows the content box.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="title">The title.</param>
        /// <param name="messageBoxButton">The content box button.</param>
        /// <returns></returns>
        Task<MessageBoxResult> ShowMessageBoxAsync(
            string content,
            string title,
            MessageBoxButton messageBoxButton = MessageBoxButton.Ok);

        /// <summary>
        /// Shows a message box with Yes/No/Cancel options and the given content and title.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<MessageBoxResult> ShowYesNoCancelMessageBoxAsync(string content, string title);

        /// <summary>
        /// Shows the error content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<MessageBoxResult> ShowErrorAsync(string content, string title);

        /// <summary>
        /// Opens an input dialog with the given title and message
        /// There are 2 buttons -> Ok and Cancel
        /// If Ok pressed return the input text otherwise returns null
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="defaultInput"></param>
        /// <returns></returns>
        Task<string> ShowOkCancelInputAsync(string title, string message, string defaultInput);

        /// <summary>
        /// Creates a ProgressDialog inside of the current window.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<IProgressDialogViewModel> ShowProgressAsync(string title, string message);
    }
}
