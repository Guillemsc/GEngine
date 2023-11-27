namespace GEngine.Utils.Comparers
{
    /// <summary>
    /// Comparer for comparing two keys, handling equality as beeing greater.
    /// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
    /// Note: this will break Remove(key) or IndexOfKey(key) since the comparer never returns 0 to signal key equality.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class DuplicateKeyComparer<TKey> :
            IComparer<TKey> where TKey : IComparable
    {
        public int Compare(TKey? x, TKey? y)
        {
            int result = x!.CompareTo(y);

            return result == 0 ? 1 : result;
        }
    }
}