using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Di.Delegates;
using GEngine.Utils.Di.Extensions;
using GEngine.Utils.Di.Installers;
using GEngine.Utils.Disposing.Disposables;
using GEngine.Utils.Loadables;

namespace GEngine.Utils.Di.Contexts
{
    public sealed class AsyncDiContext<TResult> : IAsyncDiContext<TResult>
    {
        readonly List<IInstaller> _installers = new();
        readonly List<IAsyncLoadable<IInstaller>> _asyncLoadables = new();
        readonly List<ILoadable<IInstaller>> _loadables = new();

        bool _hasValidContainer;
        IDiContainer? _container;

        public IAsyncDiContext<TResult> AddInstallerAsyncLoadable(IAsyncLoadable<IInstaller> asyncLoadable)
        {
            _asyncLoadables.Add(asyncLoadable);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstallerLoadable(ILoadable<IInstaller> asyncLoadable)
        {
            _loadables.Add(asyncLoadable);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstaller(IInstaller installer)
        {
            _installers.Add(installer);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstallers(IReadOnlyList<IInstaller> installers)
        {
            _installers.AddRange(installers);
            return this;
        }

        public IAsyncDiContext<TResult> AddInstaller(InstallDelegate installer)
        {
            _installers.Add(new CallbackInstaller(installer.Invoke));
            return this;
        }

        public async Task<ITaskDisposable<TResult>> Install()
        {
            List<IInstaller> allInstallers = new(_installers);
            List<IAsyncDisposable> asyncDisposables = new();
            List<IDisposable> syncDisposables = new();

            foreach (var asyncLoadable in _asyncLoadables)
            {
                IAsyncDisposable<IInstaller> asyncDisposable = await asyncLoadable.Load(CancellationToken.None);
                asyncDisposables.Add(asyncDisposable);
                allInstallers.Add(asyncDisposable.Value);
            }

            foreach (var loadable in _loadables)
            {
                IDisposable<IInstaller> installer = loadable.Load();
                syncDisposables.Add(installer);
                allInstallers.Add(installer.Value);
            }

            _container = DiContainerBuilderExtensions.BuildFromInstallers(allInstallers);

            async Task Dispose(TResult result)
            {
                _hasValidContainer = false;

                _container.Dispose();

                foreach (var asyncDisposable in asyncDisposables)
                {
                    await asyncDisposable.DisposeAsync();
                }

                foreach (var disposable in syncDisposables)
                {
                    disposable.Dispose();
                }
            }

            TResult result = _container.Resolve<TResult>();

            _hasValidContainer = true;

            return new CallbackTaskDisposable<TResult>(
                result,
                Dispose
            );
        }

        public IDiContainer? GetContainerUnsafe()
        {
            if (!_hasValidContainer)
            {
                throw new AccessViolationException("Tried to get container but it was not created or already disposed");
            }

            return _container;
        }
    }
}
