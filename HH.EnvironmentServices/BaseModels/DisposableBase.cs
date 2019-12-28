using System;

namespace HH.EnvironmentServices.BaseModels
{
    public abstract class DisposableBase : IDisposable
    {
        protected bool Disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (Disposed) return;
            if (disposing)
            {
                DisposeManagedResources();
            }

            DisposeUnmanagedResources();
            Disposed = true;
        }

        protected void ThrowExceptionIfDisposed()
        {
            if (Disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        protected virtual void DisposeManagedResources()
        {
            
        }

        protected virtual void DisposeUnmanagedResources()
        {
        }
    }
}