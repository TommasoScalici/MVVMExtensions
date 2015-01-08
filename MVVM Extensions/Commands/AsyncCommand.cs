using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class AsyncCommand : AsyncCommandBase, IAsyncCommand
    {
        private Func<CancellationToken, Task> execute;
        private AsyncCancelCommand cancelCommand;
        private ObservableTask execution;


        public event EventHandler<AsyncCommandEventArgs> Executed;


        public AsyncCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null)
            : base(canExecute)
        {
            this.execute = execute;
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task> execute, Predicate<object> canExecuteWithParam)
            : base(canExecuteWithParam)
        {
            this.execute = execute;
            cancelCommand = new AsyncCancelCommand();
        }


        public override ICommand CancelCommand { get { return cancelCommand; } }
        public ObservableTask Execution
        {
            get { return execution; }
            protected set { execution = value; RaisePropertyChanged(); }
        }


        public async override void Execute(object parameter = null, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs(Execution, parameter));
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            cancelCommand.NotifyCommandStarted();
            Execution = new ObservableTask(execute?.Invoke(cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskObserver;
            cancelCommand.NotifyCommandFinished();
            IsExecuting = false;
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs);
            base.OnExecuted(e);
        }

    }


    public class AsyncCommand<T> : AsyncCommandBase, IAsyncCommand<object, T>
    {
        private Func<CancellationToken, Task<T>> execute;
        private AsyncCancelCommand cancelCommand;
        private ObservableTask<T> execution;


        public event EventHandler<AsyncCommandEventArgs<object, T>> Executed;


        public AsyncCommand(Func<CancellationToken, Task<T>> execute, Func<bool> canExecute = null)
            : base(canExecute)
        {
            this.execute = execute;
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task<T>> execute, Predicate<object> canExecuteWithParam)
            : base(canExecuteWithParam)
        {
            this.execute = execute;
            cancelCommand = new AsyncCancelCommand();
        }


        public override ICommand CancelCommand { get { return cancelCommand; } }
        public ObservableTask<T> Execution
        {
            get { return execution; }
            protected set { execution = value; RaisePropertyChanged(); }
        }


        public async override void Execute(object parameter = null, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs<object, T>(Execution, (T)parameter));
            }
        }

        protected async override Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            cancelCommand.NotifyCommandStarted();
            Execution = new ObservableTask<T>(execute?.Invoke(cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskObserver;
            cancelCommand.NotifyCommandFinished();
            IsExecuting = false;
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs<object, T>);
            base.OnExecuted(e);
        }
    }
}
