using System;
using GEngine.Utils.Di.Bindings;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Builder
{
    public sealed class DiBindingBuilder<T> : IDiBindingBuilder<T>
    {
        readonly Action<IDiBinding> _addBindingAction;
        readonly Type _identifierType;

        public DiBindingBuilder(
            Action<IDiBinding> addBindingAction,
            Type identifierType
            )
        {
            _addBindingAction = addBindingAction;
            _identifierType = identifierType;
        }

        public IDiBindingActionBuilder<T> FromNew()
        {
            Type type = typeof(T);

            bool canBeCreated = type.GetConstructor(Type.EmptyTypes) != null && !type.IsAbstract;

            if(!canBeCreated)
            {
                throw new Exception($"Object of type {type.Name} cannot be instantiated on runtime " +
                                    $"because either: it's abstract, or does not have an empty constructor");
            }

            DiBinding binding = new NewInstanceBinding(_identifierType, type);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromInstance(T instance)
        {
            Type type = typeof(T);

            if (instance == null)
            {
                throw new Exception($"From instance for type {type.FullName} can't be called with null");
            }

            DiBinding binding = new ReferenceInstanceBinding(_identifierType, type, instance);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromFunction(Func<IDiResolveContainer, T> func)
        {
            Type type = typeof(T);

            object CastedFunc(IDiResolveContainer c) => func.Invoke(c);

            DiBinding binding = new FunctionDiBinding(_identifierType, type, CastedFunc);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromFunction(Func<T> func)
        {
            Type type = typeof(T);

            object CastedFunc() => func.Invoke();

            DiBinding binding = new FunctionWithoutContainerDiBinding(_identifierType, type, CastedFunc);

            return AddBinding(binding);
        }

        public IDiBindingActionBuilder<T> FromContainer(IDiResolveContainer container)
        {
            return FromFunction(_ => container.Resolve<T>());
        }

        public IDiBindingActionBuilder<T> FromContainer()
        {
            return FromFunction(c => c.Resolve<T>());
        }

        DiBindingActionBuilder<T> AddBinding(DiBinding binding)
        {
            _addBindingAction.Invoke(binding);

            return new DiBindingActionBuilder<T>(binding);
        }
    }
}
