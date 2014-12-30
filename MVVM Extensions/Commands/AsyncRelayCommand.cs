using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<CancellationToken, Task> execute;
        private readonly Func<bool> canExecute;
        private readonly Predicate<object> canExecuteWithParam;
        private AsyncCancelCommand cancelCommand;


        public override ICommand CancelCommand { get { return cancelCommand; } }


        public AsyncRelayCommand(Func<CancellationToken, Task> executeTask, Func<bool> canExecute = null)
        {
            this.execute = executeTask;
            this.canExecute = canExecute ?? new Func<bool>(() => true);
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncRelayCommand(Func<CancellationToken, Task> executeTask, Predicate<object> canExecuteWithParam)
        {
            this.execute = executeTask;
            this.canExecuteWithParam = canExecuteWithParam;
            cancelCommand = new AsyncCancelCommand();
        }

        protected AsyncRelayCommand() { }

        public override bool CanExecute(object parameter = null) => canExecuteWithParam?.Invoke(parameter) ?? CanExecute();

        public bool CanExecute() => canExecute?.Invoke() ?? false;

        public async override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                await ExecuteAsync(parameter);
                OnExecuted();
            }
        }

        protected override async Task ExecuteAsync(object parameter = null)
        {
            cancelCommand.NotifyCommandStarted();
            Execution = new ObservableTask(execute?.Invoke(cancelCommand.Token));
            RaiseCanExecuteChanged();
            await Execution.TaskObserver;
            cancelCommand.NotifyCommandFinished();
        }
    }


    public class AsyncRelayCommand<T> : AsyncRelayCommand
    {
        private readonly Func<CancellationToken, Task> execute;
        private readonly Func<bool> canExecute;
        private readonly Predicate<object> canExecuteWithParam;
        private AsyncCancelCommand cancelCommand;

        public AsyncRelayCommand(Func<CancellationToken, Task<T>> executeTask, Func<bool> canExecute = null)
        {
            this.execute = executeTask;
            this.canExecute = canExecute ?? new Func<bool>(() => true);
            cancelCommand = new AsyncCancelCommand();
        }

        public AsyncRelayCommand(Func<CancellationToken, Task<T>> executeTask, Predicate<object> canExecuteWithParam)
        {
            this.execute = executeTask;
            this.canExecuteWithParam = canExecuteWithParam;
            cancelCommand = new AsyncCancelCommand();
        }
    }
}
