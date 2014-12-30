using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TommasoScalici.MVVMExtensions.Notifications;

namespace TommasoScalici.MVVMExtensions.Commands
{
    public abstract class AsyncCommandBase : CommandBase, IAsyncCommand, INotifyPropertyChanged
    {
        private ObservableTask execution;


        public event PropertyChangedEventHandler PropertyChanged;


        public abstract ICommand CancelCommand { get; }

        public ObservableTask Execution
        {
            get { return execution; }
            protected set
            {
                execution = value;
                RaisePropertyChanged();
            }
        }

        protected abstract Task ExecuteAsync(object parameter = null);

        protected override void OnCanExecuteChanged()
        {
            RaisePropertyChanged(nameof(Execution));
            base.OnCanExecuteChanged();
        }

        protected override void OnExecuted()
        {
            RaisePropertyChanged(nameof(Execution));
            base.OnExecuted();
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
