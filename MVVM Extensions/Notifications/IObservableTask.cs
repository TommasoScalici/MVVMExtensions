using System.Threading.Tasks;

namespace TommasoScalici.MVVMExtensions.Notifications
{
    public interface IObservableTask
    {
        Task Task { get; }
    }


    public interface IObservableTask<TResult>
    {
        Task<TResult> Task { get; }
    }
}
