using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace HH.View.Utils.Behaviors
{
    [DebuggerNonUserCode]
    public sealed class WaitCursor : IDisposable
    {
        public WaitCursor()
        {
            IncrementOperationCount();
        }

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                DecrementLoadingCount();
                GC.SuppressFinalize(this);
            }
        }

        private static void SetOverrideCursor(Cursor cursor)
        {
            Application.Current.Dispatcher.Invoke(() => Mouse.OverrideCursor = cursor, DispatcherPriority.Send);
        }

        private static int _operationCount;

        private void IncrementOperationCount()
        {
            if (++_operationCount == 1)
            {
                SetOverrideCursor(Cursors.Wait);
            }
        }

        private void DecrementLoadingCount()
        {
            if (_operationCount > 0 && --_operationCount == 0)
            {
                SetOverrideCursor(null);
            }
        }

        ~WaitCursor()
        {
            Dispose();
        }
    }
}