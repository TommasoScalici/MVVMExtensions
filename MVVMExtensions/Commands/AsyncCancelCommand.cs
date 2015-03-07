using System;
using System.Threading;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    internal sealed class AsyncCancelCommand : ICommand
    {
        private AsyncCommandBase asyncCommandBase;

        public AsyncCancelCommand(AsyncCommandBase command)
        {
            asyncCommandBase = command;
            asyncCommandBase.PropertyChanged += (sender, e) => CanExecuteChanged(this, EventArgs.Empty);
        }


        public CancellationToken Token { get; set; }


        public event EventHandler CanExecuteChanged;


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
