using System;
using GEngine.Utils.Di.Container;

namespace GEngine.Utils.Di.Bindings
{
    public sealed class ReferenceInstanceBinding : DiBinding
    {
        readonly object _obj;

        public ReferenceInstanceBinding(
            Type identifierType,
            Type actualType,
            object obj
            )
            : base(identifierType, actualType)
        {
            _obj = obj;
        }

        protected override object OnBind(IDiResolveContainer container)
        {
            return _obj;
        }
    }
}
