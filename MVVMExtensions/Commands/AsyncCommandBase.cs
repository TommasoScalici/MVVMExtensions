using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ICommand cancelCommand;


        protected AsyncCommandBase() => cancelCommand = new AsyncCancelCommand(this);


        /// <summary>
        /// Return true if the asynchronous <see cref="Task"/> associated with the command is currently executing.
        /// </summary>
        public bool IsExecuting { get => Read<bool>(); protected set => Write(value); }
        /// <summary>
        /// The <see cref="System.Threading.CancellationToken"/> bound to the inner <see cref="Task"/> associated with the command.
        /// </summary>
        public CancellationToken CancellationToken => cancellationTokenSource.Token;         /// <summary>
                                                                                             /// You have to execute this command to send a <see cref="Cancel"/> request to the <see cref="Task"/> associated
                                                                                             /// with the <see cref="AsyncCommand"/>.
                                                                                             /// </summary>
        public ICommand CancelCommand => cancelCommand;

        /// <summary>
        /// Call the <see cref="Cancel"/> for the <see cref="CancellationTokenSource"/> bound to
        /// the inner <see cref="Task"/> associated with the command.
        /// </summary>
        public void Cancel() => cancellationTokenSource.Cancel();

        protected virtual async Task ExecuteAsync(object parameter = null)
        {
            cancellationTokenSource = new CancellationTokenSource();
            await Task.Yield();
        }
    }
}
