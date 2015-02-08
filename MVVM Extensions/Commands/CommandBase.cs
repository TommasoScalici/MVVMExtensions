using System;
using System.Windows.Input;

using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class CommandBase : ObservableObject, ICommand
    {
        /// <summary>
        /// Raised when the binding with the command needs to be refreshed to reflect changes on the UI.
        /// </summary>
        public event EventHandler CanExecuteChanged;


        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter, bool ignoreCanExecute = false);
        public void Execute(object parameter = null) => Execute(parameter, false);
        public void RaiseCanExecuteChanged() => OnCanExecuteChanged();
        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        protected virtual void OnExecuted(EventArgs e) => RaiseCanExecuteChanged();
    }

}
