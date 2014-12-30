using System;
using System.Diagnostics;
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

        protected ObservableTask() { }

        public Task Task { get; private set; }

        public Task TaskObserver { get; private set; }

        public TaskStatus Status { get { return Task.Status; } }

        public bool IsCanceled { get { return Task.IsCanceled; } }

        public bool IsFaulted { get { return Task.IsFaulted; } }

        public bool IsNotCompleted { get { return !IsCompleted; } }

        public bool IsCompleted { get { return Task.IsCompleted; } }

        public bool IsSuccesfullyCompleted { get { return Status == TaskStatus.RanToCompletion; } }

        public AggregateException Exception { get { return Task.Exception; } }

        public Exception InnerException { get { return Exception?.InnerException; } }

        public string ErrorMessage { get { return Exception?.InnerException?.Message; } }


        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }

            catch
            {
            }

            finally
            {
                NotifyStatusProperties();
            }
        }

        protected void NotifyStatusProperties()
        {
            RaisePropertyChanged(nameof(IsCompleted));
            RaisePropertyChanged(nameof(Status));

            if (Task.IsCanceled)
                RaisePropertyChanged(nameof(IsCanceled));

            else if (Task.IsFaulted)
            {
                RaisePropertyChanged(nameof(ErrorMessage));
                RaisePropertyChanged(nameof(Exception));
                RaisePropertyChanged(nameof(InnerException));
                RaisePropertyChanged(nameof(IsFaulted));
            }

            else
                RaisePropertyChanged(nameof(IsSuccesfullyCompleted));
        }
    }

    public class ObservableTask<TResult> : ObservableTask
    {
        public ObservableTask(Task<TResult> task)
        {
            Task = task;

            if (!task.IsCompleted)
                TaskObserver = WatchTaskAsync(task);
        }

        public new Task<TResult> Task { get; private set; }

        public new Task TaskObserver { get; private set; }

        public TResult Result { get { return Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult); } }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;

                if (!IsCanceled && !IsFaulted)
                    RaisePropertyChanged(nameof(Result));
            }

            catch
            {
            }

            finally
            {
                NotifyStatusProperties();
            }
        }
    }
}
