using System;
using System.Collections.Generic;
using GEngine.Utils.Randomization.Generators;

namespace GEngine.Utils.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Checks if list already contains the elements. If it does not, it adds it o the list.
        /// </summary>
        public static bool AddIfDoesNotContain<T>(this List<T> list, T elements)
        {
            bool contains = list.Contains(elements);

            if (contains)
            {
                return false;
            }

            list.Add(elements);
            return true;
        }

        public static void Shuffle<T>(this List<T> list, IRandomGenerator randomGenerator)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = randomGenerator.NewInt(0, n);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        /// <summary>
        /// For each elements on the collection, checks if the predicate succeeeds. If it does, it adds the item to the list.
        /// </summary>
        public static void AddRangePredicate<T>(this List<T> list, IEnumerable<T> collection, Func<T, bool> shouldAddPredicate)
        {
            foreach (T item in collection)
            {
                bool shouldAdd = shouldAddPredicate.Invoke(item);

                if (!shouldAdd)
                {
                    continue;
                }

                list.Add(item);
            }
        }

        /// <summary>
        /// Adds the elements from the specified collection to the end of the list, if the element is not null.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to add the elements to.</param>
        /// <param name="collection">The collection of elements to add.</param>
        public static void AddRangeIfNotNull<T>(this List<T> list, IEnumerable<T> collection)
        {
            list.AddRangePredicate(collection, o => o != null);
        }

        /// <summary>
        /// For each elements on the collection, checks if list already contains the elements. If it does not, it adds it o the list.
        /// </summary>
        public static void AddRangeIfDoesNotContain<T>(this List<T> list, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                list.AddIfDoesNotContain(item);
            }
        }

        /// <summary>
        /// Casts all the elements of a list into another type, and returns a new list with the casted elements.
        /// </summary>
        public static List<TConverted> CastAllAsNew<T, TConverted>(this List<T> list) where TConverted : class
        {
            return list.ConvertAll(x => x as TConverted);
        }

        /// <summary>
        /// Casts all the elements of a IEnumerable into another type, and returns an IEnumerable with the casted elements.
        /// </summary>
        public static IEnumerable<TConverted> CastAll<T, TConverted>(this IEnumerable<T> list) where TConverted : class
        {
            return list.Select(x => (x as TConverted)!);
        }

        /// <summary>
        /// Casts all the elements of a list into another type, and returns a new list with the casted elements.
        /// </summary>
        public static void CastAllNonAlloc<T, TConverted>(
            this List<T> list,
            ref List<TConverted> converted
            ) where TConverted : class
        {
            converted.Clear();

            foreach (T element in list)
            {
                converted.Add(element as TConverted);
            }
        }

        /// <summary>
        /// Inserts an element at the position the func becomes true for the first time or at the end.
        /// </summary>
        /// <param name="list">The list to apply this to.</param>
        /// <param name="element">The element to insert.</param>
        /// <param name="shouldAddAtPositionFunc">First element is the element to insert, second is the list element.</param>
        /// <typeparam name="T">The type of the list.</typeparam>
        public static int InsertByFunc<T>(this List<T> list, T element, Func<T, T, bool> shouldAddAtPositionFunc)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (shouldAddAtPositionFunc.Invoke(element, list[i]))
                {
                    list.Insert(i, element);
                    return i;
                }
            }

            list.Add(element);
            return list.Count - 1;
        }

        /// <summary>
        /// Tries to get the first element on the list.
        /// If it could get it, it removes the element from the list and outputs it as value, and returns true.
        /// If it could not get it, returns false.
        /// </summary>
        public static bool TryPop<T>(this List<T> list, out T value)
        {
            bool couldGet = list.TryGet(0, out value);

            if (!couldGet)
            {
                return false;
            }

            list.RemoveAt(0);
            return true;
        }

        /// <summary>
        /// Removes all instances of an element from the list.
        /// </summary>
        public static void RemoveAll<T>(this List<T> list, T element)
        {
            for (int i = list.Count - 1; i >= 0; --i)
            {
                T checkingValue = list[i];

                if (checkingValue.Equals(element))
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}
