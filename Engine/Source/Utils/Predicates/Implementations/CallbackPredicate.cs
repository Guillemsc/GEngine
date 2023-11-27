using System;

namespace GEngine.Utils.Predicates
{
    /// <summary>
    /// A <see cref="IPredicate"/> that is checks if it's satisfied through a method.
    /// </summary>
    /// <inheritdoc />
    public sealed class CallbackPredicate : IPredicate
    {
        readonly Func<bool> _callback;

        public CallbackPredicate(Func<bool> callback)
        {
            _callback = callback;
        }

        public bool IsSatisfied()
        {
            return _callback.Invoke();
        }
    }

    /// <summary>
    /// A <see cref="IPredicate"/> that is checks if it's satisfied through a method.
    /// </summary>
    /// <inheritdoc />
    public sealed class CallbackPredicate<T> : IPredicate<T>
    {
        readonly Func<T, bool> _callback;

        public CallbackPredicate(Func<T, bool> callback)
        {
            _callback = callback;
        }

        public bool IsSatisfied(T arg)
        {
            return _callback.Invoke(arg);
        }
    }
}
