using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HH.ViewModel.Services.Win.Commands
{
    /// <summary>
    /// This class is a simple implmentation of <see cref="ICommand"/> that hooks into the <see cref="System.Windows.Input.CommandManager"/> to perform re-query operations.
    ///  </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerNonUserCode]
    internal class DelegateCommand<T> : ICommand
    {
        private readonly Func<Exception, bool> _errorHandler;
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="errorHandler">Optional error handler.</param>
        public DelegateCommand(Action execute, Func<Exception, bool> errorHandler = null)
            : this(notUsed => execute())
        {
            Func<Exception, bool> defaultHandler = exception => false;
            _errorHandler = errorHandler ?? defaultHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand{T}" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action<T> execute,
            Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T) parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            try
            {
                _execute((T) parameter);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception caught executing command" + ex);
                if (!_errorHandler(ex))
                    MessageBox.Show(ex.ToString(), "Unhandled Error");
            }
        }
    }

    /// <summary>
    /// This class is a simple implmentation of <see cref="ICommand"/> that hooks into the <see cref="CommandManager"/> to perform re-query operations.
    /// </summary>
    [DebuggerNonUserCode]
    internal class DelegateCommand : ICommand
    {
        private readonly Func<Exception, bool> _errorHandler;
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="errorHandler">Optional Error handler.</param>
        public DelegateCommand(Action execute, Func<Exception, bool> errorHandler = null)
            : this(notUsed => execute())
        {
            Func<Exception, bool> defaultHandler = exception => false;
            _errorHandler = errorHandler ?? defaultHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute"></param>
        /// <param name="errorHandler">Optional error handler.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute, Func<Exception, bool> errorHandler = null)
            : this(notUsed => execute(), notUsed => canExecute())
        {
            Func<Exception, bool> defaultHandler = exception => false;
            _errorHandler = errorHandler ?? defaultHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand" /> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <param name="canExecute">The can execute.</param>
        public DelegateCommand(Action<object> execute,
            Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            try
            {
                _execute(parameter);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception caught executing command" + ex);
                if (!_errorHandler(ex))
                    MessageBox.Show(ex.ToString(), "Unhandled Error");
            }
        }
    }
}