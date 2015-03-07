using System;
using System.ComponentModel;
using System.Windows.Input;

using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public interface IAsyncCommand : ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        event EventHandler<AsyncCommandEventArgs> Executed;

        ObservableTask Execution { get; }
        bool IsExecuting { get; }
    }


    public interface IAsyncCommand<T> : ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        event EventHandler<AsyncCommandEventArgs<T>> Executed;


        ObservableTask<T> Execution { get; }
        bool IsExecuting { get; }
    }


    public interface IAsyncCommand<TParameter, TResult> : ICommand, INotifyPropertyChanged
    {
        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        event EventHandler<AsyncCommandEventArgs<TParameter, TResult>> Executed;


        ObservableTask<TResult> Execution { get; }
        bool IsExecuting { get; }
    }
}
