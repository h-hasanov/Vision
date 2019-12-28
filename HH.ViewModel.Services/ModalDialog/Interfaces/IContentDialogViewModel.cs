using HH.ViewModel.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IContentDialogViewModel<out TContent, TResult> : IDialogViewModel<TResult> where TContent : IViewModel
    {
        TContent Content { get; }
    }
}