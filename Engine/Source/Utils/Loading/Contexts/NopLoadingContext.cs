using System;
using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Loading.Contexts
{
    public sealed class NopLoadingContext : ILoadingContext
    {
        public static readonly NopLoadingContext Instance = new();

        NopLoadingContext()
        {

        }

        public ILoadingContext Enqueue(Func<CancellationToken, Task> function) => this;
        public ILoadingContext Enqueue(Action action) => this;
        public ILoadingContext EnqueueAfterLoad(params Action[] action) => this;
        public ILoadingContext ShowInstantly() => this;
        public ILoadingContext DoNotHide() => this;
        public ILoadingContext RunBeforeLoadActionsInstantly() => this;
        public ILoadingContext DontRunAfterLoadActions() => this;

        public Task Execute(CancellationToken cancellationToken) => Task.CompletedTask;
        public void Execute() { }
        public void ExecuteAsync() { }
    }
}
