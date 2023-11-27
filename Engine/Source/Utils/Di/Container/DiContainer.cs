using GEngine.Utils.Di.Bindings;

namespace GEngine.Utils.Di.Container
{
    public sealed class DiContainer : IDiContainer
    {
        readonly Dictionary<Type, IDiBinding> _bindings;
        readonly Dictionary<object, Dictionary<Type, IDiBinding>> _bindingsById;

        readonly List<Action<IDiResolveContainer>> _whenInit;
        readonly List<Action<IDiResolveContainer>> _whenDispose;

        readonly IReadOnlyList<IDiContainer> _extraContainers;

        readonly List<IDiBinding> _resolvingStack = new();
        readonly Stack<IDiBinding> _toInitStack = new();

        public DiContainer(
            Dictionary<Type, IDiBinding> bindings,
            Dictionary<object, Dictionary<Type, IDiBinding>> bindingsById,
            List<Action<IDiResolveContainer>> whenInit,
            List<Action<IDiResolveContainer>> whenDispose,
            IReadOnlyList<IDiContainer> extraContainers
            )
        {
            _bindings = bindings;
            _bindingsById = bindingsById;
            _whenInit = whenInit;
            _whenDispose = whenDispose;
            _extraContainers = extraContainers;
        }

        public void BindNonLazy()
        {
            foreach (Action<IDiResolveContainer> action in _whenInit)
            {
                action.Invoke(this);
            }

            foreach(KeyValuePair<Type, IDiBinding> binding in _bindings)
            {
                if(binding.Value.Lazy)
                {
                    continue;
                }

                Bind(binding.Value);
            }

            foreach(Dictionary<Type, IDiBinding> bindingsForId in _bindingsById.Values)
            {
                foreach(KeyValuePair<Type, IDiBinding> binding in bindingsForId)
                {
                    if(binding.Value.Lazy)
                    {
                        continue;
                    }

                    Bind(binding.Value);
                }
            }
        }

        void Bind(IDiBinding binding)
        {
            if (!binding.Binded)
            {
                _resolvingStack.Add(binding);

                binding.Bind(this);

                if (binding.Value == null)
                {
                    throw new NullReferenceException(
                        $"Object of type {binding.IdentifierType.Name} Binding returned a null object"
                    );
                }

                _resolvingStack.Remove(binding);
            }

            if (!binding.Inited)
            {
                _toInitStack.Push(binding);
            }

            if (_resolvingStack.Count > 0)
            {
                return;
            }

            while (_toInitStack.Count > 0)
            {
                IDiBinding toInit = _toInitStack.Pop();

                toInit.Init(this);
            }
        }

        public T Resolve<T>()
        {
            bool found = TryResolve(out T value);

            if (found)
            {
                return value;
            }

            throw new Exception($"Object of type {typeof(T).Name} could not be resolved");
        }

        public T Resolve<T>(object id)
        {
            bool found = TryResolve(id, out T value);

            if (found)
            {
                return value;
            }

            throw new Exception($"Object with id {id} of type {typeof(T).Name} could not be resolved");
        }

        public Lazy<T> LazyResolve<T>()
        {
            return new Lazy<T>(Resolve<T>);
        }

        public bool TryResolve<T>(out T value)
        {
            return TryResolveWithBindings(_bindings, out value);
        }

        public bool TryResolve<T>(object id, out T value)
        {
            bool idFound = _bindingsById.TryGetValue(id, out Dictionary<Type, IDiBinding> bindings);

            if (!idFound)
            {
                throw new Exception($"Tried to resolve but id {id} could not be found");
            }

            return TryResolveWithBindings(bindings, out value);
        }

        public void Dispose()
        {
            _resolvingStack.Clear();

            foreach (KeyValuePair<Type, IDiBinding> binding in _bindings)
            {
                binding.Value.Dispose(this);
            }

            foreach(Dictionary<Type, IDiBinding> bindingsForId in _bindingsById.Values)
            {
                foreach(KeyValuePair<Type, IDiBinding> binding in bindingsForId)
                {
                    if(binding.Value.Lazy)
                    {
                        continue;
                    }

                    binding.Value.Dispose(this);
                }
            }

            foreach (Action<IDiResolveContainer> action in _whenDispose)
            {
                action.Invoke(this);
            }
        }

        bool TryResolveWithBindings<T>(Dictionary<Type, IDiBinding> bindings, out T value)
        {
            Type type = typeof(T);

            bool found = bindings.TryGetValue(type, out IDiBinding binding);

            if (found)
            {
                bool isCircularDependency = _resolvingStack.Contains(binding);

                if (isCircularDependency)
                {
                    throw new Exception($"Circular dependency found resolving {type.Name}");
                }

                Bind(binding);
                value = (T)binding.Value;
                return true;
            }

            foreach (IDiContainer extraContainer in _extraContainers)
            {
                found = extraContainer.TryResolve(out value);

                if (found)
                {
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}
