using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class AsyncCommandEventArgs : CommandEventArgs
    {
        public AsyncCommandEventArgs(ObservableTask task, object parameter)
            : base(parameter) => Task = task;


        public ObservableTask Task { get; private set; }
    }


    public class AsyncCommandEventArgs<TResult> : CommandEventArgs
    {
        public AsyncCommandEventArgs(ObservableTask<TResult> task, object parameter)
            : base(parameter)
        {
            Result = task.Result;
            Task = task;
        }


        public ObservableTask<TResult> Task { get; private set; }
        public TResult Result { get; private set; }
    }


    public class AsyncCommandEventArgs<TParameter, TResult> : CommandEventArgs<TParameter>
    {
        public AsyncCommandEventArgs(ObservableTask<TResult> task, TParameter parameter)
            : base(parameter)
        {
            Result = task.Result;
            Task = task;
        }

        public ObservableTask<TResult> Task { get; private set; }
        public TResult Result { get; private set; }
    }
}
