using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private bool isExecuting;


        public bool IsExecuting { get { return isExecuting; } set { Set(ref isExecuting, value); } }
        public CancellationToken CancellationToken { get { return cancellationTokenSource.Token; } }
        public ICommand CancelCommand { get { return new AsyncCancelCommand(this) { Token = CancellationToken }; } }


        public void Cancel()
        {
            cancellationTokenSource.Cancel();
        }

        protected virtual async Task ExecuteAsync(object parameter = null)
        {
            cancellationTokenSource = new CancellationTokenSource();
            await Task.Yield();
        }
    }
}
