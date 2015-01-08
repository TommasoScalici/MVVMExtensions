using System;
using System.Threading;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    internal sealed class AsyncCancelCommand : ICommand
    {
        private bool commandExecuting;
        private CancellationTokenSource cts = new CancellationTokenSource();


        public event EventHandler CanExecuteChanged;


        public CancellationToken Token { get { return cts.Token; } }


        public bool CanExecute(object parameter = null)
        {
            return commandExecuting && !cts.IsCancellationRequested;
        }

        public void Execute(object parameter = null)
        {
            cts.Cancel();
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void NotifyCommandStarted()
        {
            commandExecuting = true;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            if (cts.IsCancellationRequested)
            {
                cts = new CancellationTokenSource();
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void NotifyCommandFinished()
        {
            commandExecuting = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
