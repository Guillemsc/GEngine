using System;
using GEngine.Utils.Caching.Resettables;

namespace GEngine.Utils.Caching.Cache
{
    /// <summary>
    /// Utility for caching the result of some operation.
    /// When the operation is executed, the result is cached and used instead for the next times.
    /// </summary>
    public sealed class CachedResult<TResult> : IClearableCache
    {
        readonly Func<TResult> _getResult;

        bool _executed;
        TResult _result;

        public CachedResult(Func<TResult> getResult)
        {
            _getResult = getResult;
        }

        /// <summary>
        /// Gets the result of the function execution.
        /// If result is not cached, function is executed and result is cached.
        /// If result is cached, stored value is returned.
        /// </summary>
        public TResult Get()
        {
            if (_executed)
            {
                return _result;
            }

            _executed = true;

            _result = _getResult.Invoke();

            return _result;
        }

        public void ClearCache()
        {
            _executed = false;
        }
    }
}
