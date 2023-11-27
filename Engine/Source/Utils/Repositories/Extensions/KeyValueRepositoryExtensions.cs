namespace GEngine.Utils.Repositories.Extensions
{
    public static class KeyValueRepositoryExtensions
    {
        public static bool Replace<TKey, TValue>(this IKeyValueRepository<TKey, TValue> keyValueRepository, TKey key, TValue value)
        {
            var replaced = keyValueRepository.Remove(key);
            keyValueRepository.Add(key, value);
            return replaced;
        }

        public static bool TryPop<TKey, TValue>(this IKeyValueRepository<TKey, TValue> keyValueRepository, TKey key, out TValue value)
        {
            if (!keyValueRepository.TryGet(key, out value))
            {
                return false;
            }
            keyValueRepository.Remove(key);
            return true;
        }
    }
}
