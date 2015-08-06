using System;
using System.Threading;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    sealed class AsyncCancelCommand : ICommand
    {
        AsyncCommandBase asyncCommandBase;


        public AsyncCancelCommand(AsyncCommandBase command)
        {
            asyncCommandBase = command;
            asyncCommandBase.PropertyChanged += (sender, e) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


        public event EventHandler CanExecuteChanged;


        public CancellationToken Token { get { return asyncCommandBase.CancellationToken; } }


        public bool CanExecute(object parameter = null)
        {
            return asyncCommandBase.IsExecuting && !Token.IsCancellationRequested;
        }

        public void Execute(object parameter = null)
        {
            asyncCommandBase?.Cancel();
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
