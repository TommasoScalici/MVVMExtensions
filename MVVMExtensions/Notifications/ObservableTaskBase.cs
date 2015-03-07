using System;
using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public abstract class ObservableTaskBase : ObservableObject
    {
        public abstract bool IsCanceled { get; }
        public abstract bool IsFaulted { get; }
        public abstract bool IsNotCompleted { get; }
        public abstract bool IsCompleted { get; }
        public abstract bool IsRunning { get; protected set; }
        public abstract bool IsSuccesfullyCompleted { get; }
        public abstract string ErrorMessage { get; }
        public abstract AggregateException Exception { get; }
        public abstract Exception InnerException { get; }
        public abstract TaskStatus Status { get; }
        public abstract Task TaskObserver { get; protected set; }
    }
}
