using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace TommasoScalici.MVVMExtensions
{
    public static class DispatcherExtensions
    {
        public static async Task<T> RunTaskAsync<T>(this CoreDispatcher dispatcher, Func<Task<T>> func,
                                                    CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
        {
            var taskCompletionSource = new TaskCompletionSource<T>();
            await dispatcher.RunAsync(priority, async () =>
            {
                try
                {
                    taskCompletionSource.SetResult(await func());
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            });
            return await taskCompletionSource.Task;
        }

        public static async Task RunTaskAsync(this CoreDispatcher dispatcher, Func<Task> func,
                                              CoreDispatcherPriority priority = CoreDispatcherPriority.Normal)
            => await RunTaskAsync(dispatcher, async () => { await func(); return false; }, priority);
    }
}
