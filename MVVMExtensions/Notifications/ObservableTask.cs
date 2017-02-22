using System;
using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public class ObservableTask : ObservableObject, IObservableTask
    {
        public ObservableTask(Task task)
        {
            Task = task;
            TaskObserver = task.ContinueWith(t => RaiseAllPropertyChanged(), TaskScheduler.FromCurrentSynchronizationContext());
        }

        public string ErrorMessage => Exception?.InnerException?.Message; public AggregateException Exception => Task.Exception; public Exception InnerException => Exception?.InnerException; public bool IsCanceled => Task.IsCanceled; public bool IsFaulted => Task.IsFaulted; public bool IsNotCompleted => !IsCompleted; public bool IsCompleted => Task.IsCompleted; public bool IsSuccesfullyCompleted => Status == TaskStatus.RanToCompletion; public TaskStatus Status => Task.Status; public Task TaskObserver { get; protected set; }
        public Task Task { get; private set; }
    }


    public class ObservableTask<TResult> : ObservableObject, IObservableTask<TResult>
    {
        public ObservableTask(Task<TResult> task)
        {
            Task = task;
            TaskObserver = task.ContinueWith(t => RaiseAllPropertyChanged(), TaskScheduler.FromCurrentSynchronizationContext());
        }


        public string ErrorMessage => Exception?.InnerException?.Message; public AggregateException Exception => Task.Exception; public Exception InnerException => Exception?.InnerException; public bool IsCanceled => Task.IsCanceled; public bool IsFaulted => Task.IsFaulted; public bool IsNotCompleted => !IsCompleted; public bool IsCompleted => Task.IsCompleted; public bool IsSuccesfullyCompleted => Status == TaskStatus.RanToCompletion; public TaskStatus Status => Task.Status; public Task TaskObserver { get; protected set; }
        Task IObservableTask.Task => Task; public Task<TResult> Task { get; private set; }
        public TResult Result => Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult);
    }
}
