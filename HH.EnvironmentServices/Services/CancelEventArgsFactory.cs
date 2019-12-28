using System.ComponentModel;
using HH.EnvironmentServices.Interfaces;

namespace HH.EnvironmentServices.Services
{
    public sealed class CancelEventArgsFactory : ICancelEventArgsFactory
    {
        public CancelEventArgs CreateCancelEventArgs()
        {
            return new CancelEventArgs();
        }
    }
}
