using GEngine.Utils.Collections;
using GEngine.Utils.Singletons;

namespace GEngine.Utils.Services.Locators
{
    /// <summary>
    /// Register and retrieve services everywhere on your code.
    /// Should only be used for generic services that could be used anywhere on the game.
    /// </summary>
    public sealed class ServiceLocator : Singleton<ServiceLocator>
    {
        readonly TypeDictionary _services = new();

        /// <summary>
        /// Registers a services. Only one service of the same type can be registered.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type had already been added.</exception>
        public static void Register<T>(T service)
        {
            Type type = typeof(T);

            Register(type, service);
        }

        /// <summary>
        /// Registers a services, with the provided type detached from the object itself.
        /// Only one service of the same type can be registered.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type had already been added.</exception>
        public static void Register(Type type, object service)
        {
            bool alreadyAdded = Instance._services.ContainsKey(type);

            if (alreadyAdded)
            {
                throw new InvalidOperationException($"Type {type} already added at {nameof(ServiceLocator)}");
            }

            Instance._services.Add(type, service);
        }

        /// <summary>
        /// Unregisters a services.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type was not present.</exception>
        public static void Unregister<T>(T _)
        {
            Type type = typeof(T);

            Unregister(type);
        }

        /// <summary>
        /// Unregisters a services.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type was not present.</exception>
        public static void Unregister<T>()
        {
            Type type = typeof(T);

            Unregister(type);
        }

        /// <summary>
        /// Unregisters a services.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type was not present.</exception>
        public static void Unregister(Type type)
        {
            bool alreadyAdded = Instance._services.ContainsKey(type);

            if (!alreadyAdded)
            {
                throw new InvalidOperationException($"Type {type} not found at {nameof(ServiceLocator)}");
            }

            Instance._services.Remove(type);
        }

        /// <summary>
        /// Gets a registered service.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type had already been added.</exception>
        public static object Get(Type type)
        {
            bool found = Instance._services.TryGetValue(type, out object value);

            if(!found)
            {
                throw new InvalidOperationException($"Type {type} could not be found at {nameof(ServiceLocator)}");
            }

            return value;
        }

        /// <summary>
        /// Gets a registered service.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when type is not found.</exception>
        public static T Get<T>()
        {
            Type type = typeof(T);

            object value = Get(type);

            return (T)value;
        }

        /// <summary>
        /// Tries to get a registered services.
        /// </summary>
        public static bool TryGet<T>(out T item)
        {
            return Instance._services.TryGetValue<T>(out item);
        }

        /// <summary>
        /// Tries to get a registered services.
        /// </summary>
        public static void TryGet<T>(Action<T> itemAction)
        {
            bool found = TryGet<T>(out T value);

            if (!found)
            {
                return;
            }

            itemAction?.Invoke(value);
        }

        /// <summary>
        /// Checks if the service is registered.
        /// </summary>
        public static bool Has(Type type)
        {
            return Instance._services.TryGetValue(type, out _);
        }

        /// <summary>
        /// Checks if the service is registered.
        /// </summary>
        public static bool Has<T>()
        {
            Type type = typeof(T);

            return Has(type);
        }
    }
}
