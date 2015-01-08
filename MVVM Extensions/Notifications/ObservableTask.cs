using System;
using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public class ObservableTask : ObservableTaskBase, IObservableTask
    {
        public ObservableTask(Task task)
        {
            Task = task;
            TaskObserver = WatchTaskAsync(task);
        }

        public override string ErrorMessage { get { return Exception?.InnerException?.Message; } }
        public override AggregateException Exception { get { return Task.Exception; } }
        public override Exception InnerException { get { return Exception?.InnerException; } }
        public override bool IsCanceled { get { return Task.IsCanceled; } }
        public override bool IsFaulted { get { return Task.IsFaulted; } }
        public override bool IsNotCompleted { get { return !IsCompleted; } }
        public override bool IsCompleted { get { return Task.IsCompleted; } }
        public override bool IsRunning { get; protected set; }
        public override bool IsSuccesfullyCompleted { get { return Status == TaskStatus.RanToCompletion; } }
        public override TaskStatus Status { get { return Task.Status; } }
        public override Task TaskObserver { get; protected set; }
        public Task Task { get; private set; }


        private async Task WatchTaskAsync(Task task)
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


    public class ObservableTask<TResult> : ObservableTaskBase, IObservableTask<TResult>
    {
        public ObservableTask(Task<TResult> task)
        {
            Task = task;
            TaskObserver = WatchTaskAsync(task);
        }


        public override string ErrorMessage { get { return Exception?.InnerException?.Message; } }
        public override AggregateException Exception { get { return Task.Exception; } }
        public override Exception InnerException { get { return Exception?.InnerException; } }
        public override bool IsCanceled { get { return Task.IsCanceled; } }
        public override bool IsFaulted { get { return Task.IsFaulted; } }
        public override bool IsNotCompleted { get { return !IsCompleted; } }
        public override bool IsCompleted { get { return Task.IsCompleted; } }
        public override bool IsRunning { get; protected set; }
        public override bool IsSuccesfullyCompleted { get { return Status == TaskStatus.RanToCompletion; } }
        public override TaskStatus Status { get { return Task.Status; } }
        public override Task TaskObserver { get; protected set; }
        public Task<TResult> Task { get; private set; }
        public TResult Result { get { return Status == TaskStatus.RanToCompletion ? Task.Result : default(TResult); } }

        private async Task WatchTaskAsync(Task task)
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
}
