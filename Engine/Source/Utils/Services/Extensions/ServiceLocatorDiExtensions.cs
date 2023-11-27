using System;
using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Services.Locators;

namespace GEngine.Utils.Services.Extensions
{
    public static class ServiceLocatorDiExtensions
    {
        /// <summary>
        /// Registers to <see cref="ServiceLocator"/> when init, and unregisters on dispose.
        /// </summary>
        public static IDiBindingActionBuilder<T> LinkToServiceLocator<T>(this IDiBindingActionBuilder<T> builder)
        {
            builder.WhenInit(o => ServiceLocator.Register(builder.IdentifierType, o));
            builder.WhenDispose(() => ServiceLocator.Unregister(builder.IdentifierType));
            builder.NonLazy();

            return builder;
        }

        /// <summary>
        /// If it was not already registered, registers to <see cref="ServiceLocator"/> when init, and unregisters on dispose.
        /// </summary>
        public static IDiBindingActionBuilder<T> TryLinkToServiceLocator<T>(this IDiBindingActionBuilder<T> builder)
        {
            bool alreadyRegistered = false;

            builder.WhenInit(o =>
            {
                alreadyRegistered = ServiceLocator.Has(builder.IdentifierType);

                if (alreadyRegistered)
                {
                    return;
                }

                ServiceLocator.Register(builder.IdentifierType, o);
            });

            builder.WhenDispose(() =>
            {
                if (alreadyRegistered)
                {
                    return;
                }

                ServiceLocator.Unregister(builder.IdentifierType);
            });

            builder.NonLazy();

            return builder;
        }

        /// <summary>
        /// Gets the instance from the <see cref="ServiceLocator"/>.
        /// </summary>
        public static IDiBindingActionBuilder<T> FromServiceLocator<T>(this IDiBindingBuilder<T> builder)
        {
            return builder.FromInstance(ServiceLocator.Get<T>());
        }

        /// <summary>
        /// Tries to get the instance from the <see cref="ServiceLocator"/>. If it could not be found,
        /// it calls the function to get the instance.
        /// </summary>
        public static IDiBindingActionBuilder<T> FromServiceLocatorOrFunction<T>(
            this IDiBindingBuilder<T> builder,
            Func<T> func
            )
        {
            T GetInstance()
            {
                bool exists = ServiceLocator.Has<T>();

                if (exists)
                {
                    return ServiceLocator.Get<T>();
                }

                return func.Invoke();
            }

            return builder.FromFunction(GetInstance);
        }

        public static IDiBindingActionBuilder<TBinding> FromServiceLocator<TService, TBinding>(
            this IDiBindingBuilder<TBinding> builder,
            Func<TService, TBinding> serviceFunc
            )
        {
            TBinding Function(IDiResolveContainer resolver)
            {
                return serviceFunc.Invoke(ServiceLocator.Get<TService>());
            }

            return builder.FromFunction(Function);
        }

        public static IDiBindingActionBuilder<TBinding> FromServiceLocator<TService, TBinding>(
            this IDiBindingBuilder<TBinding> builder,
            Func<IDiResolveContainer, TService, TBinding> serviceFunc
            )
        {
            TBinding Function(IDiResolveContainer resolver)
            {
                return serviceFunc.Invoke(resolver, ServiceLocator.Get<TService>());
            }

            return builder.FromFunction(Function);
        }
    }
}
