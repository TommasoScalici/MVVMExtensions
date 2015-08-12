using System;
using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public class ObservableTask : ObservableObject, IObservableTask
    {
        public ObservableTask(Task task)
        {
            Task = task;
            TaskObserver = task.ContinueWith(t => RaiseAllPropertyChanged());
        }

        public string ErrorMessage { get { return Exception?.InnerException?.Message; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException { get { return Exception?.InnerException; } }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public bool IsNotCompleted { get { return !IsCompleted; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsSuccesfullyCompleted { get { return Status == TaskStatus.RanToCompletion; } }
        public TaskStatus Status { get { return Task.Status; } }
        public Task TaskObserver { get; protected set; }
        public Task Task { get; private set; }
    }


    public class ObservableTask<TResult> : ObservableObject, IObservableTask<TResult>
    {
        public ObservableTask(Task<TResult> task)
        {
            Task = task;
            TaskObserver = task.ContinueWith(t => RaiseAllPropertyChanged());
        }


        public string ErrorMessage { get { return Exception?.InnerException?.Message; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException { get { return Exception?.InnerException; } }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public bool IsNotCompleted { get { return !IsCompleted; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsSuccesfullyCompleted { get { return Status == TaskStatus.RanToCompletion; } }
        public TaskStatus Status { get { return Task.Status; } }
        public Task TaskObserver { get; protected set; }
        Task IObservableTask.Task { get { return Task; } }
        public Task<TResult> Task { get; private set; }
        public TResult Result { get { return Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult); } }
    }
}
