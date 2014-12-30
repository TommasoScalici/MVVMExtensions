using System;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public class RelayCommand : CommandBase
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;
        private readonly Action<object> executeWithParam;
        private readonly Predicate<object> canExecuteWithParam;


        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? new Func<bool>(() => true);
        }

        public RelayCommand(Action execute, Predicate<object> canExecuteWithParam)
        {
            this.execute = execute;
            this.canExecuteWithParam = canExecuteWithParam ?? new Predicate<object>(x => true);
        }

        public RelayCommand(Action<object> executeWithParam, Func<bool> canExecute = null)
        {
            this.executeWithParam = executeWithParam;
            this.canExecute = canExecute ?? new Func<bool>(() => true);
        }

        public RelayCommand(Action<object> executeWithParam, Predicate<object> canExecuteWithParam)
        {
            this.executeWithParam = executeWithParam;
            this.canExecuteWithParam = canExecuteWithParam ?? new Predicate<object>(x => true);
        }

        protected RelayCommand() { }


        public override bool CanExecute(object parameter = null) => canExecuteWithParam?.Invoke(parameter) ?? CanExecute();

        public bool CanExecute() => canExecute?.Invoke() ?? false;

        public override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute() || CanExecute(parameter))
            {
                RaiseCanExecuteChanged();

                execute?.Invoke();
                executeWithParam?.Invoke(parameter);

                OnExecuted();
            }
        }
    }


    public class RelayCommand<T> : CommandBase
    {
        private readonly Action<T> execute;
        private readonly Predicate<T> canExecute;


        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public override bool CanExecute(object parameter = null) => canExecute?.Invoke((T)parameter) ?? false;

        public override void Execute(object parameter, bool ignoreCanExecute = false)
        {
            if (ignoreCanExecute || CanExecute((T)parameter))
            {
                RaiseCanExecuteChanged();

                execute?.Invoke((T)parameter);

                OnExecuted();
            }
        }
    }
}
