using GEngine.Utils.Di.Bindings;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Di.Delegates;
using GEngine.Utils.Di.Installers;

namespace GEngine.Utils.Di.Builder
{
    public sealed class DiContainerBuilder : IDiContainerBuilder
    {
        readonly Dictionary<Type, IDiBinding> _bindings = new();
        readonly Dictionary<object, Dictionary<Type, IDiBinding>> _bindingsById = new();

        readonly Dictionary<Type, object> _settings = new();

        readonly List<Action<IDiContainer?>> _whenBuild = new();
        readonly List<Action<IDiResolveContainer>> _whenInit = new();
        readonly List<Action<IDiResolveContainer>> _whenDispose = new();

        public void AddBinding(IDiBinding binding)
        {
            bool alreadyAdded = _bindings.ContainsKey(binding.IdentifierType);

            if (alreadyAdded)
            {
                throw new Exception($"Container builder already has a binding " +
                                    $"of type {binding.IdentifierType.Name} registered");
            }

            _bindings.Add(binding.IdentifierType, binding);
        }

        public void AddBindingWithId(object id, IDiBinding binding)
        {
            if (id == null)
            {
                throw new Exception($"Tried to add binding with null id");
            }

            bool idAlreadyExits = _bindingsById.TryGetValue(
                id,
                out Dictionary<Type, IDiBinding> bindings
            );

            if (!idAlreadyExits)
            {
                bindings = new Dictionary<Type, IDiBinding>();
                _bindingsById.Add(id, bindings);
            }
            else
            {
                bool alreadyAdded = _bindingsById.ContainsKey(binding.IdentifierType);

                if (alreadyAdded)
                {
                    throw new Exception($"Container builder already has a binding with id {id}, and " +
                                        $"of type {binding.IdentifierType.Name} registered");
                }
            }

            bindings.Add(binding.IdentifierType, binding);
        }

        public void AddSettings<T>(T settings)
        {
            Type type = typeof(T);

            if (_settings.ContainsKey(type))
            {
                throw new InvalidOperationException($"Settings of type {type.FullName} are already added and can't be added again");
            }

            _settings[type] = settings;

            Bind<T>().FromInstance(settings);
        }

        public T GetSettings<T>()
        {
            if (!TryGetSettings(out T settings))
            {
                throw new InvalidOperationException($"There are no settings of type {typeof(T).FullName} available");
            }

            return settings;
        }

        public bool TryGetSettings<T>(out T settings)
        {
            var type = typeof(T);
            if (!_settings.TryGetValue(type, out object objectSettings))
            {
                settings = default;
                return false;
            }

            settings = (T)objectSettings;
            return true;
        }

        public IDiBindingBuilder<T> Bind<T>()
        {
            return new DiBindingBuilder<T>(AddBinding, typeof(T));
        }

        public IDiBindingBuilder<T> Bind<T>(object id)
        {
            return new DiBindingBuilder<T>(b => AddBindingWithId(id, b), typeof(T));
        }

        public IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>() where TConcrete : TInterface
        {
            Type interfaceType = typeof(TInterface);
            Type concreteType = typeof(TConcrete);

            bool isAssignable = interfaceType.IsAssignableFrom(concreteType);

            if (!isAssignable)
            {
                throw new InvalidOperationException(
                    $"Binding with interface type {interfaceType.Name} is not assignable to concrete type {concreteType.Name}"
                );
            }

            return new DiBindingBuilder<TConcrete>(AddBinding, typeof(TInterface));
        }

        public IDiBindingBuilder<TConcrete> Bind<TInterface, TConcrete>(object id) where TConcrete : TInterface
        {
            Type interfaceType = typeof(TInterface);
            Type concreteType = typeof(TConcrete);

            bool isAssignable = interfaceType.IsAssignableFrom(concreteType);

            if (!isAssignable)
            {
                throw new InvalidOperationException(
                    $"Binding with interface type {interfaceType.Name} is not assignable to concrete type {concreteType.Name}"
                );
            }

            return new DiBindingBuilder<TConcrete>(b => AddBindingWithId(id, b), typeof(TInterface));
        }

        public IDiContainerBuilder Bind<T>(BindingBuilderDelegate<T> bindingBuilderDelegate)
        {
            bindingBuilderDelegate.Invoke(Bind<T>());

            return this;
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(params IInstaller[] installers)
        {
            return Install(installers);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(IReadOnlyList<IInstaller> installers)
        {
            return Install(installers);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(IEnumerable<IInstaller> installers)
        {
            return Install(installers);
        }

        [Obsolete("Use Install instead")]
        public IDiContainerBuilder Bind(Action<IDiContainerBuilder> action)
        {
            return Install(action);
        }

        public IDiContainerBuilder Install(IEnumerable<IInstaller> installers)
        {
            foreach (IInstaller installer in installers)
            {
                if (installer == null)
                {
                    throw new Exception("There was a null Installer while trying to Bind");
                }

                installer.Install(this);
            }

            return this;
        }

        public IDiContainerBuilder Install(params IInstaller[] installers)
        {
            foreach (IInstaller installer in installers)
            {
                if (installer == null)
                {
                    throw new Exception("There was a null Installer while trying to Bind");
                }

                installer.Install(this);
            }

            return this;
        }

        public IDiContainerBuilder Install(InstallDelegate installDelegate)
        {
            Install(new CallbackInstaller(installDelegate.Invoke));

            return this;
        }

        public IDiContainerBuilder Install(IReadOnlyList<IInstaller> installers)
        {
            foreach (IInstaller installer in installers)
            {
                if (installer == null)
                {
                    throw new Exception("There was a null Installer while trying to Bind");
                }

                installer.Install(this);
            }

            return this;
        }

        public IDiContainerBuilder Install(Action<IDiContainerBuilder> action)
        {
            action?.Invoke(this);

            return this;
        }

        public bool TryGetBinding<T>(out IDiBindingActionBuilder<T> diBindingActionBuilder)
        {
            if (_bindings.TryGetValue(typeof(T), out var diBinding))
            {
                var binding = (DiBinding)diBinding;
                diBindingActionBuilder = new DiBindingActionBuilder<T>(binding);
                return true;
            }
            else
            {
                diBindingActionBuilder = null;
                return false;
            }
        }

        public IDiContainerBuilder WhenInit(Action action)
        {
            _whenInit.Add(_ => action.Invoke());

            return this;
        }

        public IDiContainerBuilder WhenInit(Action<IDiResolveContainer> action)
        {
            _whenInit.Add(action);

            return this;
        }

        public IDiContainerBuilder WhenDispose(Action action)
        {
            _whenDispose.Add(_ => action.Invoke());

            return this;
        }

        public IDiContainerBuilder WhenDispose(Action<IDiResolveContainer> action)
        {
            _whenDispose.Add(action);

            return this;
        }

        public IDiContainerBuilder WhenBuild(Action<IDiContainer?> action)
        {
            _whenBuild.Add(action);

            return this;
        }
        
        public IDiContainer? Build()
        {
            return Build(Array.Empty<IDiContainer>());
        }

        public IDiContainer? Build(IDiContainer parentContainer)
        {
            return Build(new[] { parentContainer });
        }

        public IDiContainer? Build(IReadOnlyList<IDiContainer> extraContainers)
        {
            DiContainer? container = new DiContainer(
                _bindings,
                _bindingsById,
                _whenInit,
                _whenDispose,
                extraContainers
            );

            foreach (Action<IDiContainer?> action in _whenBuild)
            {
                action.Invoke(container);
            }

            container.BindNonLazy();

            return container;
        }
    }
}
