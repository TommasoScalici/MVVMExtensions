using System;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public interface IRelayCommand : ICommand
    {
        event EventHandler<CommandEventArgs> Executed;
    }

    public interface IRelayCommand<T> : ICommand
    {
        event EventHandler<CommandEventArgs<T>> Executed;
    }
}
