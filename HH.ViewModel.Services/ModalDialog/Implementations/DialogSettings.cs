using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.ViewModels;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public class DialogSettings : ViewModelBase, IDialogSettings
    {
        private string _title;

        public DialogSettings()
        {
            Title = "No Title";
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
