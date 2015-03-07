using System;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public interface IRelayCommand : ICommand
    {
        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        event EventHandler<CommandEventArgs> Executed;
    }

    public interface IRelayCommand<T> : ICommand
    {
        /// <summary>
        /// Raised after the command has executed.
        /// </summary>
        event EventHandler<CommandEventArgs<T>> Executed;
    }
}
