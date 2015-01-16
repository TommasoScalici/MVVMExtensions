using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class AsyncCommandBase : CommandBase, INotifyPropertyChanged
    {
        private bool isExecuting;


        public event PropertyChangedEventHandler PropertyChanged;


        public abstract ICommand CancelCommand { get; }
        public virtual bool IsExecuting
        {
            get { return isExecuting; }
            protected set { isExecuting = value; RaisePropertyChanged(); }
        }


        protected abstract Task ExecuteAsync(object parameter = null);

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
