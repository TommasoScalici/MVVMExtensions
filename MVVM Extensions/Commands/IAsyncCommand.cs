using System;
using System.ComponentModel;
using System.Windows.Input;

using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public interface IAsyncCommand : ICommand, INotifyPropertyChanged
    {
        event EventHandler<AsyncCommandEventArgs> Executed;

        ICommand CancelCommand { get; }
        ObservableTask Execution { get; }
        bool IsExecuting { get; }
    }


    public interface IAsyncCommand<TParameter, TResult> : ICommand, INotifyPropertyChanged
    {
        event EventHandler<AsyncCommandEventArgs<TParameter, TResult>> Executed;

        ICommand CancelCommand { get; }
        ObservableTask<TResult> Execution { get; }
        bool IsExecuting { get; }
    }
}
