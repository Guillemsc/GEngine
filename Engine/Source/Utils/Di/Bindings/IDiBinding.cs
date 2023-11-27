using System;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Bindings
{
    public interface IDiBinding
    {
        Type IdentifierType { get; }
        Type ActualType { get; }

        object Value { get; }

        bool Lazy { get; }

        public bool Binded { get; }
        public bool Inited { get; }
        public bool Disposed { get; }

        void Bind(IDiResolveContainer container);
        void Init(IDiResolveContainer container);
        void Dispose(IDiResolveContainer container);
    }
}
