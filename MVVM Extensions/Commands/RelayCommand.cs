using System;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class RelayCommand : CommandBase, IRelayCommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;


        public event EventHandler<CommandEventArgs> Executed;


        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = x => execute?.Invoke();
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public RelayCommand(Action<object> execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute == null ? new Func<object, bool>(x => true) : x => canExecute();
        }

        public RelayCommand(Action execute, Func<object, bool> canExecute)
        {
            this.execute = x => execute?.Invoke();
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke(parameter) ?? false;

        public override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                RaiseCanExecuteChanged();
                execute?.Invoke(parameter);
                OnExecuted(new CommandEventArgs(parameter));
            }
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as CommandEventArgs);
            base.OnExecuted(e);
        }
    }


    public class RelayCommand<T> : CommandBase, IRelayCommand<T>
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;


        public event EventHandler<CommandEventArgs<T>> Executed;


        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? new Func<T, bool>(x => true);
        }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke((T)parameter) ?? false;

        public override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute((T)parameter))
            {
                RaiseCanExecuteChanged();
                execute?.Invoke((T)parameter);
                OnExecuted(new CommandEventArgs<T>((T)parameter));
            }
        }

        protected override void OnExecuted(EventArgs e)
        {
            Executed?.Invoke(this, e as CommandEventArgs<T>);
            RaiseCanExecuteChanged();
        }
    }
}
