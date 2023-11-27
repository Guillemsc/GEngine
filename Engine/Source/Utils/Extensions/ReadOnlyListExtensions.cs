namespace GEngine.Utils.Extensions
{
    public static class ReadOnlyListExtensions
    {
        /// <summary>
        /// Tries to get the element corresponding to the provided index.
        /// If index is out of bounds, it returns false.
        /// If array is empty it returns false.
        /// </summary>
        public static bool TryGet<T>(this IReadOnlyList<T> list, int index, out T item)
        {
            bool outsideBounds = index < 0 || list.Count <= index;

            if (outsideBounds)
            {
                item = default;
                return false;
            }

            item = list[index];
            return true;
        }

        /// <summary>
        /// Given an index, it clamps it between 0 and the array lenght - 1.
        /// </summary>
        public static int ClampIndex<T>(this IReadOnlyList<T> list, int index)
        {
            return Math.Clamp(index, 0, list.Count - 1);
        }

        /// <summary>
        /// Given an index, it clamps it between 0 and the array lenght - 1.
        /// Then, with the clamped index, it tries to get the element corresponding to that index.
        /// If array is empty it returns false.
        /// </summary>
        public static bool TryGetClamped<T>(this IReadOnlyList<T> list, int index, out T item)
        {
            index = list.ClampIndex(index);

            return list.TryGet(index, out item);
        }

        /// <summary>
        /// If item can be found inside the list, it returns its index.
        /// </summary>
        public static bool TryGetItemIndex<T>(this IReadOnlyList<T> list, T item, out int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T checkingItem = list[i];

                if (!checkingItem.Equals(item))
                {
                    continue;
                }

                index = i;
                return true;
            }

            index = default;
            return false;
        }

        /// <summary>
        /// If item can be found inside the list, it returns its index.
        /// </summary>
        public static bool TryGetItemIndex<T>(this IReadOnlyList<T> list, Predicate<T> predicate, out int index)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T checkingItem = list[i];

                if (!predicate.Invoke(checkingItem))
                {
                    continue;
                }

                index = i;
                return true;
            }

            index = default;
            return false;
        }

        /// <summary>
        /// Checks if provided index is the last index of the list.
        /// </summary>
        /// <returns>True if it's the last index, false if it's not. If list is empty, returns false.</returns>
        public static bool IsLastIndex<T>(this List<T> list, int index)
        {
            if (index < 0)
            {
                return false;
            }

            return list.Count - 1 == index;
        }
    }
}
