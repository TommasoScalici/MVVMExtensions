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
        private Func<object, bool> canExecute;
        private AsyncCancelCommand cancelCommand;
        private ObservableTask execution;


        public event EventHandler<AsyncCommandEventArgs> Executed;


        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<Task> execute, Func<object, bool> canExecute)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute;
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            cancelCommand = new AsyncCancelCommand();
        }


        public override ICommand CancelCommand { get { return cancelCommand; } }
        public ObservableTask Execution
        {
            get { return execution; }
            protected set { execution = value; RaisePropertyChanged(); }
        }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
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


    public class AsyncCommand<TResult> : AsyncCommandBase, IAsyncCommand<TResult>
    {
        private Func<CancellationToken, Task<TResult>> execute;
        private Func<object, bool> canExecute;
        private AsyncCancelCommand cancelCommand;
        private ObservableTask<TResult> execution;


        public event EventHandler<AsyncCommandEventArgs<TResult>> Executed;


        public AsyncCommand(Func<Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = token => execute?.Invoke();
            this.canExecute = canExecute;
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            cancelCommand = new AsyncCancelCommand();
        }


        public override ICommand CancelCommand { get { return cancelCommand; } }
        public ObservableTask<TResult> Execution
        {
            get { return execution; }
            protected set { execution = value; RaisePropertyChanged(); }
        }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs<TResult>(Execution, parameter));
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            cancelCommand.NotifyCommandStarted();
            Execution = new ObservableTask<TResult>(execute?.Invoke(cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskObserver;
            cancelCommand.NotifyCommandFinished();
            IsExecuting = false;
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs<TResult>);
            base.OnExecuted(e);
        }
    }


    public class AsyncCommand<TParameter, TResult> : AsyncCommandBase, IAsyncCommand<TParameter, TResult>
    {
        private Func<CancellationToken, TParameter, Task<TResult>> execute;
        private Func<object, bool> canExecute;
        private AsyncCancelCommand cancelCommand;
        private ObservableTask<TResult> execution;


        public event EventHandler<AsyncCommandEventArgs<TParameter, TResult>> Executed;


        public AsyncCommand(Func<TParameter, Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = (token, parameter) => execute?.Invoke(parameter);
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<TParameter, Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = (token, parameter) => execute?.Invoke(parameter);
            this.canExecute = canExecute;
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, TParameter, Task<TResult>> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncCommand(Func<CancellationToken, TParameter, Task<TResult>> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            cancelCommand = new AsyncCancelCommand();
        }


        public override ICommand CancelCommand { get { return cancelCommand; } }
        public ObservableTask<TResult> Execution
        {
            get { return execution; }
            protected set { execution = value; RaisePropertyChanged(); }
        }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted(new AsyncCommandEventArgs<TParameter, TResult>(Execution, (TParameter)parameter));
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            IsExecuting = true;
            cancelCommand.NotifyCommandStarted();
            Execution = new ObservableTask<TResult>(execute?.Invoke(cancelCommand.Token, (TParameter)parameter));
            RaiseCanExecuteChanged();
            await Execution.TaskObserver;
            cancelCommand.NotifyCommandFinished();
            IsExecuting = false;
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as AsyncCommandEventArgs<TParameter, TResult>);
            base.OnExecuted(e);
        }
    }
}
