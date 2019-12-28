using HH.EnvironmentServices.Utils;
using HH.ViewModel.Interfaces;
using HH.ViewModel.Services.ModalDialog.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public abstract class ContentDialogViewModelBase<TContent, TResult> : DialogViewModelBase<TResult>,
        IContentDialogViewModel<TContent, TResult> where TContent : class, IViewModel
    {
        protected ContentDialogViewModelBase(TContent content, IDialogSettings dialogSettings) : base(dialogSettings)
        {
            Content = content.ArgumentNullCheck(nameof(content));
        }

        public TContent Content { get; }
    }
}