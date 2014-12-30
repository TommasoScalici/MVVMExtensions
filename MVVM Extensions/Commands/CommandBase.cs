using System;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public event EventHandler Executed;


        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter, bool ignoreCanExecute = false);

        public void Execute(object parameter = null) => Execute(parameter, false);

        public void RaiseCanExecuteChanged() => OnCanExecuteChanged();

        protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        protected virtual void OnExecuted()
        {
            Executed?.Invoke(this, EventArgs.Empty);
            RaiseCanExecuteChanged();
        }
    }
}
