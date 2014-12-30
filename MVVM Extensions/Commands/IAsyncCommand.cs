using System.Windows.Input;
using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public interface IAsyncCommand : ICommand
    {
        ObservableTask Execution { get; }
    }

    public interface IAsyncCommand<T> : IAsyncCommand
    {
        new ObservableTask<T> Execution { get; }
    }
}
