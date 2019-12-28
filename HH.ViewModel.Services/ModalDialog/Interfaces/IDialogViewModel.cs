using System;
using System.Threading.Tasks;
using HH.ViewModel.Interfaces;

namespace HH.ViewModel.Services.ModalDialog.Interfaces
{
    public interface IDialogViewModel<TResult> : IViewModel
    {
        IDialogSettings DialogSettings { get; }
        Task<TResult> WaitUntilClosed();
        event EventHandler<TResult> Closed;
    }
}
