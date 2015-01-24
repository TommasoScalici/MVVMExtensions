using System;
using System.Windows.Input;

using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class CommandBase : ObservableObject, ICommand
    {
        public event EventHandler CanExecuteChanged;


        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter, bool ignoreCanExecute = false);
        public void Execute(object parameter = null) => Execute(parameter, false);
        public void RaiseCanExecuteChanged() => OnCanExecuteChanged();
        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        protected virtual void OnExecuted(EventArgs e) => RaiseCanExecuteChanged();
    }

}
