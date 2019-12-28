using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using HH.ViewModel.Services.Interfaces;
using HH.ViewModel.Services.Services;

namespace HH.ViewModel.Services.Win.Services
{
    [DebuggerNonUserCode]
    public class ApplicationService : IApplicationService
    {
        private readonly Window _mainWindow;
        private readonly List<Func<Task<ApplicationClosing>>> _closingCallbacks;

        public ApplicationService()
        {
            _closingCallbacks = new List<Func<Task<ApplicationClosing>>>();
            _mainWindow = Application.Current.MainWindow;

            _mainWindow.Closing += HandleWindowClosing;
            _mainWindow.Closed += HandleWindowClosed;
            _mainWindow.Loaded += HandleWindowLoaded;
        }

        public event EventHandler ApplicationLoaded;
        public event EventHandler ApplicationClosed;

        public void RegisterApplicationClosingCallback(Func<Task<ApplicationClosing>> callback)
        {
            _closingCallbacks.Add(callback);
        }

        public void ForceShutDown()
        {
            _mainWindow.Closing -= HandleWindowClosing;
            _mainWindow.Closed -= HandleWindowClosed;
            _mainWindow.Loaded -= HandleWindowLoaded;
            _mainWindow.Close();
        }

        public void CloseApplication()
        {
            _mainWindow.Close();
        }

        private void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            DecideCanClose();
            _mainWindow.IsEnabled = true;
        }

        private async void DecideCanClose()
        {
            //This is added to show the ui for 5ms and then start asking questions, otherwise it is too quick
            //and Window complains that cannot be closed while closing.
            await Task.Delay(5); 
            var canClose = true;
            foreach (var closingCallback in _closingCallbacks)
            {
                var result = await closingCallback();
                if (result == ApplicationClosing.Cancel)
                {
                    canClose = false;
                    break;
                }
            }
            if (canClose)
            {
                _mainWindow.Closing -= HandleWindowClosing;
                _mainWindow.Close();
            }
        }

        private void HandleWindowClosed(object sender, EventArgs e)
        {
            ApplicationClosed?.Invoke(this, e);
        }

        private void HandleWindowLoaded(object sender, EventArgs e)
        {
            ApplicationLoaded?.Invoke(this, e);
        }
    }
}
