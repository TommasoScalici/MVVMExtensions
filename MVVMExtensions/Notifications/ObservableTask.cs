using System;
using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public class ObservableTask : ObservableObject
    {
        public ObservableTask(Task task)
        {
            Task = task;
            TaskObserver = WatchTaskAsync(task);
        }


        public string ErrorMessage { get { return Exception?.InnerException?.Message; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException { get { return Exception?.InnerException; } }
        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public bool IsNotCompleted { get { return !IsCompleted; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsRunning { get; protected set; }
        public bool IsSuccesfullyCompleted { get { return Status == TaskStatus.RanToCompletion; } }
        public TaskStatus Status { get { return Task.Status; } }
        public Task TaskObserver { get; protected set; }
        public Task Task { get; private set; }


        protected async Task WatchTaskAsync(Task task)
        {
            try
            {
                IsRunning = true;
                RaiseAllPropertyChanged();
                await task;
                IsRunning = false;
            }

            catch
            {
            }

            finally
            {
                RaiseAllPropertyChanged();
            }
        }
    }


    public class ObservableTask<TResult> : ObservableTask
    {
        Task<TResult> task;


        public ObservableTask(Task<TResult> task)
            : base(task)
        {
            this.task = task;
        }

        /// <summary>
        /// Gets the result value of the inner <see cref="Task{TResult}"/>.
        /// </summary>
        public TResult Result { get { return Status == TaskStatus.RanToCompletion ? task.Result : default(TResult); } }
    }
}
