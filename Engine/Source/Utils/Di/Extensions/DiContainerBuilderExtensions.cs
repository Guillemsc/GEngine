using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Di.Installers;
using GEngine.Utils.Disposing.Disposables;
using GEngine.Utils.Optionals;

namespace GEngine.Utils.Di.Extensions
{
    public static class DiContainerBuilderExtensions
    {
        public static IDisposable<TResult> BuildAsContext<TResult>(Action<IDiContainerBuilder> action)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Install(action);

            IDiContainer? container = builder.Build();

            void Dispose(TResult result)
            {
                container.Dispose();
            }

            TResult result = container.Resolve<TResult>();

            return new CallbackDisposable<TResult>(
                result,
                Dispose
            );
        }

        public static IDiContainer? BuildFromInstallers(params IInstaller[] installers)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Install(installers);

            return builder.Build();
        }

        public static IDiContainer? BuildFromInstallers(IReadOnlyList<IInstaller> installers)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Install(installers);

            return builder.Build();
        }

        public static IDiContainer? BuildFromInstance<TInterface>(TInterface value)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface>().FromInstance(value);

            return builder.Build();
        }

        public static IDiContainer? BuildFromInstance<TInterface1, TInterface2>(TInterface1 value1, TInterface2 value2)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);

            return builder.Build();
        }

        public static IDiContainer? BuildFromInstance<TInterface1, TInterface2, TInterface3>(TInterface1 value1, TInterface2 value2, TInterface3 value3)
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);

            return builder.Build();
        }

        public static IDiContainer? BuildFromInstance<
            TInterface1,
            TInterface2,
            TInterface3,
            TInterface4
            >(
            TInterface1 value1,
            TInterface2 value2,
            TInterface3 value3,
            TInterface4 value4
            )
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);
            builder.Bind<TInterface4>().FromInstance(value4);

            return builder.Build();
        }

        public static IDiContainer? BuildFromInstance<
            TInterface1,
            TInterface2,
            TInterface3,
            TInterface4,
            TInterface5
            >(
            TInterface1 value1,
            TInterface2 value2,
            TInterface3 value3,
            TInterface4 value4,
            TInterface5 value5
            )
        {
            IDiContainerBuilder builder = new DiContainerBuilder();

            builder.Bind<TInterface1>().FromInstance(value1);
            builder.Bind<TInterface2>().FromInstance(value2);
            builder.Bind<TInterface3>().FromInstance(value3);
            builder.Bind<TInterface4>().FromInstance(value4);
            builder.Bind<TInterface5>().FromInstance(value5);

            return builder.Build();
        }

        public static void InstallOptional<T>(this IDiContainerBuilder builder, Optional<T> optionalInstaller)
            where T : IInstaller
        {
            if (!optionalInstaller.TryGet(out var installer))
            {
                return;
            }

            builder.Install(installer);
        }

        public static void TryAddSettings<T>(this IDiContainerBuilder builder, T value)
        {
            if (builder.TryGetSettings<T>(out _))
            {
                return;
            }

            builder.AddSettings<T>(value);
        }

        public static void TryAddSettings<T>(this IDiContainerBuilder builder, Optional<T> optionalValue)
        {
            if (!optionalValue.TryGet(out var value))
            {
                return;
            }

            if (builder.TryGetSettings<T>(out _))
            {
                return;
            }

            builder.AddSettings<T>(value);
        }
    }
}
