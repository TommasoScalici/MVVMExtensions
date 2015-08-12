using System;
using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public interface IObservableTask
    {
        string ErrorMessage { get; }
        AggregateException Exception { get; }
        Exception InnerException { get; }
        bool IsCanceled { get; }
        bool IsFaulted { get; }
        bool IsNotCompleted { get; }
        bool IsCompleted { get; }
        bool IsSuccesfullyCompleted { get; }
        TaskStatus Status { get; }
        Task TaskObserver { get; }
        Task Task { get; }
    }


    public interface IObservableTask<TResult> : IObservableTask
    {
        new Task<TResult> Task { get; }
        TResult Result { get; }
    }
}
