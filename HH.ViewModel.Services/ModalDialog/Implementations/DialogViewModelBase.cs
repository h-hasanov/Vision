using System;
using System.Threading.Tasks;
using HH.EnvironmentServices.Utils;
using HH.ViewModel.Services.ModalDialog.Interfaces;
using HH.ViewModel.ViewModels;

namespace HH.ViewModel.Services.ModalDialog.Implementations
{
    public abstract class DialogViewModelBase<TResult> : ViewModelBase, IDialogViewModel<TResult>
    {
        #region Fields

        private readonly TaskCompletionSource<TResult> _tcs;

        #endregion

        #region Constructors

        protected DialogViewModelBase(IDialogSettings dialogSettings)
        {
            DialogSettings = dialogSettings.ArgumentNullCheck(nameof(dialogSettings));
            _tcs = new TaskCompletionSource<TResult>();
        }

        #endregion

        #region Properties

        public IDialogSettings DialogSettings { get; }
        public Task<TResult> WaitUntilClosed()
        {
            return _tcs.Task;
        }

        #endregion

        #region ClosedEvent

        public event EventHandler<TResult> Closed;

        private void OnClosed(TResult dialogResult)
        {
            Closed?.Invoke(this, dialogResult);
        }

        #endregion

        #region Methods

        protected void Close(TResult dialogResult)
        {
            _tcs.SetResult(dialogResult);
            OnClosed(dialogResult);
        }

        #endregion
    }
}
