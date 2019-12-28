using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using HH.ViewModel.Services.FileStorage.Interfaces;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.Services.StandardDialog.Enums;
using HH.ViewModel.Services.StandardDialog.Implementations;
using HH.ViewModel.Services.StandardDialog.Interfaces;
using HH.ViewModel.Services.Win.FileStorage.Implementations;
using HH.ViewModel.Services.Win.ModalDialog.Implementations;
using HH.ViewModel.Services.Win.StandardDialog.Converters;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace HH.ViewModel.Services.Win.StandardDialog.Implementations
{
    [DebuggerNonUserCode]
    public class StandardDialogService : IStandardDialogService
    {
        private static readonly MetroWindow MainWindow = (MetroWindow)Application.Current.MainWindow;

        public IFileInfo OpenFileDialog(FileTypeFilter filter)
        {
            return OpenFileDialog(new[] { filter });
        }

        public IFileInfo OpenFileDialog(FileTypeFilter[] filters)
        {
            var dlg = new OpenFileDialog {Filter = filters.ConvertToStringFilter()};
            return ShowDialog(dlg);
        }

        public IFileInfo SaveFileDialog(string fileName, FileTypeFilter filter)
        {
            return SaveFileDialog(fileName, new[] { filter });
        }

        public IFileInfo SaveFileDialog(string filename, FileTypeFilter[] filters)
        {
            var dlg = new SaveFileDialog {FileName = filename, Filter = filters.ConvertToStringFilter()};
            return ShowDialog(dlg);
        }

        private static IFileInfo ShowDialog(Microsoft.Win32.FileDialog dlg)
        {
            FileInfo fileInfo = null;
            var result = dlg.ShowDialog();
            
            if (result == true)
            {
                fileInfo = new FileInfo(dlg.FileName);
            }

            return ConvertToFileInfo(fileInfo);
        }

        private static IFileInfo ConvertToFileInfo(FileInfo fileInfo)
        {
            return fileInfo == null ? null : new Fileinfo(fileInfo);
        }

        public IFileInfo ShowOpenFolderExplorer()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            return new Fileinfo(new FileInfo(folderBrowserDialog.SelectedPath));
        }

        public async Task<MessageBoxResult> ShowMessageBoxAsync(string content, string title, MessageBoxButton messageBoxButton = MessageBoxButton.Ok)
        {
            var dialogSettings = messageBoxButton.CreateDialogSettings();
            var messageDialogResult = await MainWindow.ShowMessageAsync(title, content, dialogSettings.Item1, dialogSettings.Item2);
            return messageDialogResult.ConvertToMessageBoxResult(messageBoxButton);
        }

        public Task<MessageBoxResult> ShowYesNoCancelMessageBoxAsync(string content, string title)
        {
            return ShowMessageBoxAsync(content, title, MessageBoxButton.YesNoCancel);
        }

        public Task<MessageBoxResult> ShowErrorAsync(string content, string title)
        {
            return ShowMessageBoxAsync(content, title);
        }

        public Task<string> ShowOkCancelInputAsync(string title, string message, string defaultInput)
        {
            var dialogSettings = MessageBoxButton.OkCancel.CreateDialogSettings();
            dialogSettings.Item2.DefaultText = defaultInput;
            var input = MainWindow.ShowInputAsync(title, message, dialogSettings.Item2);
            return input;
        }

        public async Task<IProgressDialogViewModel> ShowProgressAsync(string title, string message)
        {
            var progressDialog = await MainWindow.ShowProgressAsync(title, message);
            return new ProgressDialogViewModel(progressDialog);
        }
    }
}
