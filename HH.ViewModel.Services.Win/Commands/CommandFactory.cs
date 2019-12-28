using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using HH.ViewModel.Interfaces;

namespace HH.ViewModel.Services.Win.Commands
{
    [DebuggerNonUserCode]
    public sealed class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(Action execute)
        {
            return new DelegateCommand(execute);
        }

        public ICommand CreateAsyncCommand(Func<Task> execute)
        {
            return new AsyncDelegateCommand(execute);
        }

        public ICommand CreateAsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            return new AsyncDelegateCommand(execute, canExecute);
        }

        public ICommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new DelegateCommand(execute, canExecute);
        }

        public ICommand CreateCommand<T>(Action<T> execute)
        {
            return new DelegateCommand<T>(execute);
        }

        public ICommand CreateCommand<T>(Action<T> execute, Predicate<T> canExecute)
        {
            return new DelegateCommand<T>(execute, canExecute);
        }
    }
}
