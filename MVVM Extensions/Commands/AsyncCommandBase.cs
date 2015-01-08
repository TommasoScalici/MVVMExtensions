using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class AsyncCommandBase : CommandBase, INotifyPropertyChanged
    {
        private readonly Func<bool> canExecute;
        private readonly Predicate<object> canExecuteWithParam;
        private bool isExecuting;


        public AsyncCommandBase(Func<bool> canExecute = null)
        {
            this.canExecute = canExecute ?? new Func<bool>(() => true);
        }

        public AsyncCommandBase(Predicate<object> canExecuteWithParam)
        {
            this.canExecuteWithParam = canExecuteWithParam;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public abstract ICommand CancelCommand { get; }
        public virtual bool IsExecuting
        {
            get { return isExecuting; }
            protected set { isExecuting = value; RaisePropertyChanged(); }
        }


        protected abstract Task ExecuteAsync(object parameter = null);
        public override bool CanExecute(object parameter = null) => canExecuteWithParam?.Invoke(parameter) ?? CanExecute();
        public bool CanExecute() => canExecute?.Invoke() ?? false;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
