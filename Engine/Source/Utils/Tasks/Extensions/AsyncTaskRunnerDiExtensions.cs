using System;
using GEngine.Utils.Di.Builder;
using GEngine.Utils.Tasks.Runners;

namespace GEngine.Utils.Tasks.Extensions
{
    public static class AsyncTaskRunnerDiExtensions
    {
        [Obsolete("Use InstallAsyncTaskRunner")]
        public static IDiBindingActionBuilder<AsyncTaskRunner> BindAsyncTaskRunner(
            this IDiContainerBuilder builder
        ) => InstallAsyncTaskRunner(builder);

        public static IDiBindingActionBuilder<AsyncTaskRunner> InstallAsyncTaskRunner(
            this IDiContainerBuilder builder
        )
        {
            void Dispose(AsyncTaskRunner asyncTaskRunner)
            {
                asyncTaskRunner.Cancel();
                asyncTaskRunner.Dispose();
            }

            return builder.Bind<IAsyncTaskRunner, AsyncTaskRunner>()
                .FromInstance(new AsyncTaskRunner())
                .WhenDispose(Dispose)
                .NonLazy();
        }
    }
}
