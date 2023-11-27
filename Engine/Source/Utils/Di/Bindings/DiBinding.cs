using System;
using System.Collections.Generic;
using GEngine.Utils.Di.BindingActions;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Bindings
{
    public abstract class DiBinding : IDiBinding
    {
        readonly List<IDiBindingAction> _initActions = new();
        readonly List<IDiBindingAction> _disposeActions = new();

        public Type IdentifierType { get; }
        public Type ActualType { get; }

        public object Value { get; private set; }

        public bool Lazy { get; private set; } = true;

        public bool Binded { get; private set; }
        public bool Inited { get; private set; }
        public bool Disposed { get; private set; }

        protected DiBinding(Type identifierType, Type actualType)
        {
            IdentifierType = identifierType;
            ActualType = actualType;
        }

        public void NonLazy()
        {
            Lazy = false;
        }

        public void AddInitAction(IDiBindingAction initAction)
        {
            if (Binded)
            {
                return;
            }

            _initActions.Add(initAction);
        }

        public void AddDisposeAction(IDiBindingAction disposeAction)
        {
            if (Binded)
            {
                return;
            }

            _disposeActions.Add(disposeAction);
        }

        public void Bind(IDiResolveContainer container)
        {
            if(Binded)
            {
                return;
            }

            Binded = true;

            Value = OnBind(container);
        }

        public void Init(IDiResolveContainer container)
        {
            if (!Binded)
            {
                return;
            }

            if (Inited)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            Inited = true;

            foreach (IDiBindingAction initAction in _initActions)
            {
                initAction.Execute(container, Value);
            }
        }

        public void Dispose(IDiResolveContainer container)
        {
            if (!Binded)
            {
                return;
            }

            if (Disposed)
            {
                return;
            }

            if (Value == null)
            {
                return;
            }

            Disposed = true;

            foreach (IDiBindingAction disposeAction in _disposeActions)
            {
                disposeAction.Execute(container, Value);
            }
        }

        protected abstract object OnBind(IDiResolveContainer container);
    }
}
