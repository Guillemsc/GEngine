namespace GEngine.Utils.Caching.Cache
{
    public delegate bool TryGetResultDelegate<T>(out T result);

    /// <summary>
    /// Utility for caching the result of some operation.
    /// When the operation is executed, the result is cached and used instead for the next times.
    /// </summary>
    public sealed class TryGetCachedResult<TResult>
    {
        readonly TryGetResultDelegate<TResult> _tryGetResult;

        bool _executed;

        bool _hasResult;
        TResult _result;

        public TryGetCachedResult(TryGetResultDelegate<TResult> tryGetResult)
        {
            _tryGetResult = tryGetResult;
        }

        /// <summary>
        /// Gets the result of the function execution.
        /// If result is not cached, function is executed and result is cached.
        /// If result is cached, stored value is returned.
        /// </summary>
        public bool TryGet(out TResult result)
        {
            if (_executed)
            {
                result = _result;
                return _hasResult;
            }

            _executed = true;

            _hasResult = _tryGetResult.Invoke(out _result);

            result = _result;
            return _hasResult;
        }
    }
}
