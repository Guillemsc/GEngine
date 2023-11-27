using System;
using System.Collections.Generic;
using System.Linq;
using GEngine.Utils.Optionals;
using GEngine.Utils.Randomization.Generators;

namespace GEngine.Utils.Extensions
{
    public static class EnumerableExtensions
    {
        public delegate bool TrySelectDelegate<in TValue, TResult>(TValue value, out TResult result);

        /// <summary>
        /// Tries to map all the elements from from one type to the other and
        /// returns all the elements that were able to be mapped.
        /// </summary>
        public static IEnumerable<TResult> SelectValid<TValue, TResult>(
            this IEnumerable<TValue> enumerable,
            TrySelectDelegate<TValue, TResult> trySelectDelegate
        )
        {
            foreach (var element in enumerable)
            {
                if (trySelectDelegate.Invoke(element, out var result))
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Tries to map all the elements from the source type to the destiny type and only returns
        /// true if it was able to map all the elements.
        /// </summary>
        public static bool TrySelectAll<TValue, TResult>(
            this IEnumerable<TValue> enumerable,
            TrySelectDelegate<TValue, TResult> trySelectDelegate,
            out List<TResult> results
        )
        {
            results = new List<TResult>();

            foreach (TValue value in enumerable)
            {
                if (!trySelectDelegate(value, out TResult result))
                {
                    results = default;
                    return false;
                }

                results.Add(result);
            }

            return true;
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{TEnumerated}"/> and combines the resulting sequences
        /// into a single sequence while maintaining a reference to the source element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the input sequence.</typeparam>
        /// <typeparam name="TEnumerated">The type of the elements in the inner sequences.</typeparam>
        /// <param name="enumerable">The input sequence to process.</param>
        /// <param name="func">A projection function to apply to each element in the input sequence.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains pairs of the source element and the projected elements in the inner sequences.
        /// Each pair is represented as a tuple with two elements: 'source' of type <typeparamref name="TSource"/> and 'enumerated' of type <typeparamref name="TEnumerated"/>.
        /// </returns>
        public static IEnumerable<(TSource source, TEnumerated enumerated)> SelectManyWithSource<TSource, TEnumerated>(
            this IEnumerable<TSource> enumerable,
            Func<TSource, IEnumerable<TEnumerated>> func
        )
        {
            foreach (var value in enumerable)
            {
                var innerEnumerable = func(value);
                if (innerEnumerable != null)
                {
                    foreach (var enumerated in innerEnumerable)
                    {
                        yield return (value, enumerated);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether the specified enumerable contains any elements that satisfy the given predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="enumerable">The input sequence to process.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>true if the enumerable contains any elements that satisfy the given predicate; otherwise, false.</returns>
        public static bool Contains<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            foreach (var element in enumerable)
            {
                if (predicate(element))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Projects each element of a sequence into a new form that includes the element's index.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="enumerable">The input sequence to process.</param>
        /// <returns>An IEnumerable of tuples, where each tuple contains an element and its index.</returns>
        /// <example>["hello", "world", ":)"] -> [("hello", 0), ("world", 1), (":)", 2)]</example>
        public static IEnumerable<(T value, int index)> ZipIndex<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Select(DelegateExtensions.ParamsToTuple);
        }

        [Obsolete("This method is deprecated, please use Zip instead.")]
        public static IEnumerable<(TFirst, TSecond)> ZipPairs<TFirst, TSecond>(
            this IEnumerable<TFirst> firstEnumerable,
            IEnumerable<TSecond> secondEnumerable
        )
        {
            return firstEnumerable.Zip(secondEnumerable);
        }

        /// <summary>
        /// Given two enumerables [1,2,3] [a,b,c] returns all pairs (1,a), (1,b), (1,c), (2,a), (2,b), (3,c), etc.
        /// </summary>
        public static IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second
        )
        {
            return first.Zip(second, (a, b) => (a, b));
        }

        /// <summary>
        /// Given an enumerable [1,2,3] returns all pairs (1,1), (1,2), (1,3), (2,1), (2,2), (3,3), etc.
        /// </summary>
        public static IEnumerable<(T First, T Second)> Zip<T>(
            this IEnumerable<T> first
        )
        {
            IReadOnlyList<T> collection = first.ToReadOnlyList();

            return collection.Zip(collection);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form that includes a boolean flag indicating if it's the last element.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="enumerable">The input sequence to process.</param>
        /// <returns>An IEnumerable of tuples, where each tuple contains an element and a boolean flag indicating if it's the last element.</returns>
        /// <example>["hello", "world", ":)"] -> [("hello", false), ("world", false), (":)", true)]</example>
        public static IEnumerable<(T value, bool isLast)> ZipIsLast<T>(this IEnumerable<T> enumerable)
        {
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            T previousValue = enumerator.Current;

            while (enumerator.MoveNext())
            {
                yield return (previousValue, false);
                previousValue = enumerator.Current;
            }

            yield return (previousValue, true);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form that includes a boolean flag indicating if it's the first element.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the input sequence.</typeparam>
        /// <param name="enumerable">The input sequence to process.</param>
        /// <returns>An IEnumerable of tuples, where each tuple contains an element and a boolean flag indicating if it's the first element.</returns>
        /// <example>["hello", "world", ":)"] -> [("hello", true), ("world", false), (":)", false)]</example>
        public static IEnumerable<(T value, bool isFirst)> ZipIsFirst<T>(this IEnumerable<T> enumerable)
        {
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }

            yield return (enumerator.Current, true);

            while (enumerator.MoveNext())
            {
                yield return (enumerator.Current, false);
            }
        }

        /// <summary>
        /// Using the provided comparer, gets the smallest value from the collection.
        /// If the enumerator does not have any element, it returns the default for the type.
        /// </summary>
        public static T MinObjectOrDefault<T>(this IEnumerable<T> enumerable, Comparer<T> comparer)
        {
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return default;
            }

            T best = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;

                if (comparer.Compare(best, element) < 0)
                {
                    continue;
                }

                best = element;
            }

            return best;
        }

        /// <summary>
        /// The provided function gets a Y value from for each T element.
        /// Using the provided comparer, gets the smallest Y value from the collection.
        /// If the enumerator does not have any element, it returns the default for the type.
        /// </summary>
        public static T MinObjectOrDefault<T, Y>(this IEnumerable<T> enumerable, Func<T, Y> toCompareFunc, Comparer<Y> comparer)
        {
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return default;
            }

            T best = enumerator.Current;
            Y bestValue = toCompareFunc.Invoke(best);

            while (enumerator.MoveNext())
            {
                var element = enumerator.Current;
                var elementValue = toCompareFunc.Invoke(element);

                if (comparer.Compare(bestValue, elementValue) < 0)
                {
                    continue;
                }

                best = element;
                bestValue = elementValue;
            }

            return best;
        }

        /// <summary>
        /// If collections is a <see cref="IReadOnlyCollection{T}"/>, it returns itself.
        /// If the collection is not a <see cref="IReadOnlyCollection{T}"/>, it performs ToArray and returns the result.
        /// </summary>
        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyCollection<T> readOnlyCollection)
            {
                return readOnlyCollection;
            }

            return enumerable.ToArray();
        }

        /// <summary>
        /// If collections is a <see cref="IReadOnlyList{T}"/>, it returns itself.
        /// If the collection is not a <see cref="IReadOnlyList{T}"/>, it performs ToArray and returns the result.
        /// </summary>
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyList<T> readOnlyCollection)
            {
                return readOnlyCollection;
            }

            return enumerable.ToArray();
        }

        /// <summary>
        /// Equivalent to enumerable.Where(o => o != null).
        /// </summary>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Where(o => o != null);
        }

        /// <summary>
        /// Flattens an enumerable of enumerables into one single sequence.
        /// </summary>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> enumerable)
        {
            return enumerable.SelectMany(DelegateExtensions.Self);
        }

        /// <summary>
        /// Determines whether the number of elements in the <see cref="IEnumerable{T}"/> is equal to or greater than the specified count.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="count">The minimum number of elements expected.</param>
        /// <returns><c>true</c> if the number of elements is equal to or greater than the specified count; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public static bool HasMoreThanOrEqual<T>(this IEnumerable<T> source, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "The count must be non-negative.");
            }

            return source.Take(count).Count() == count;
        }

        /// <summary>
        /// Determines whether the number of elements in the <see cref="IEnumerable{T}"/> that satisfy the specified predicate is equal to or greater than the specified count.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="count">The minimum number of elements expected.</param>
        /// <param name="predicate">The predicate used to filter the elements.</param>
        /// <returns><c>true</c> if the number of elements that satisfy the predicate is equal to or greater than the specified count; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public static bool HasMoreThanOrEqual<T>(this IEnumerable<T> source, int count, Func<T, bool> predicate)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "The count must be non-negative.");
            }

            return source.Where(predicate).Take(count).Count() == count;
        }

        /// <summary>
        /// Determines whether the number of elements in the <see cref="IEnumerable{T}"/> is equal to or less than the specified count.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="count">The maximum number of elements expected.</param>
        /// <returns><c>true</c> if the number of elements is equal to or less than the specified count; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is negative.</exception>
        public static bool HasLessThanOrEqual<T>(this IEnumerable<T> source, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "The count must be non-negative.");
            }

            return source.Take(count + 1).Count() <= count;
        }

        /// <summary>
        /// Determines whether the number of elements in the <see cref="IEnumerable{T}"/> that satisfy the specified predicate is equal to or less than the specified count.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="count">The maximum number of elements expected.</param>
        /// <param name="predicate">The predicate used to filter the elements.</param>
        /// <returns><c>true</c> if the number of elements that satisfy the predicate is equal to or less than the specified count; otherwise, <c>false</c>.</
        /// <summary>
        /// Takes the sequence of all the elements inside the collection, and duplicates if an N amount of times.
        /// </summary>
        public static bool HasLessThanOrEqual<T>(this IEnumerable<T> source, int count, Func<T, bool> predicate)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "The count must be non-negative.");
            }

            return source.Where(predicate).Take(count + 1).Count() <= count;
        }

        /// <summary>
        /// Repeats each element in the <see cref="IEnumerable{T}"/> a specified number of times and returns them sorted by appearance
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="enumerable">The source collection.</param>
        /// <param name="count">The number of times each element should be repeated.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> containing the repeated elements.</returns>
        /// <remarks>
        /// The method takes each element in the <paramref name="enumerable"/> and repeats it <paramref name="count"/> times.
        /// The resulting collection is flattened into a single <see cref="IEnumerable{T}"/> containing the repeated elements.
        /// </remarks>
        /// <example>Given an enumerable {1,2,3} repeated 3 times will return {1,1,1,2,2,2,3,3,3} </example>
        public static IEnumerable<T> RepeatElements<T>(this IEnumerable<T> enumerable, int count)
            => enumerable.Select(x => Enumerable.Repeat(x, count)).Flatten();

        /// <summary>
        /// If the collection is not empty, returns the minimum value.
        /// If it's empty, it returns the default value for the type.
        /// </summary>
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable)
        {
            TryGetMin(enumerable, out var value);
            return value;
        }

        /// <summary>
        /// If the collection is not empty, returns the minimum value.
        /// </summary>
        public static bool TryGetMin<T>(this IEnumerable<T> enumerable, out T value)
        {
            var readOnlyCollection = enumerable.ToReadOnlyCollection();
            if (readOnlyCollection.Count == 0)
            {
                value = default;
                return false;
            }

            value = readOnlyCollection.Min();
            return false;
        }

        /// <summary>
        /// If the collection is not empty, returns the maximum value.
        /// If it's empty, it returns the default value for the type.
        /// </summary>
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable)
        {
            TryGetMax(enumerable, out var value);
            return value;
        }

        /// <summary>
        /// If the collection is not empty, returns the maximum value.
        /// </summary>
        public static bool TryGetMax<T>(this IEnumerable<T> enumerable, out T value)
        {
            var readOnlyCollection = enumerable.ToReadOnlyCollection();
            if (readOnlyCollection.Count == 0)
            {
                value = default;
                return false;
            }

            value = readOnlyCollection.Max();
            return false;
        }

        /// <summary>
        /// Tries to get the first element of the collection that matches the provided predicate.
        /// </summary>
        public static bool TryGetFirst<T>(
            this IEnumerable<T> enumerable,
            Predicate<T> predicate,
            out T element)
        {
            foreach (var i in enumerable)
            {
                if (predicate.Invoke(i))
                {
                    element = i;
                    return true;
                }
            }

            element = default;
            return false;
        }

        /// <summary>
        /// Tries to get the first element of the collection.
        /// </summary>
        public static bool TryGetFirst<T>(
            this IEnumerable<T> enumerable,
            out T element)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                element = default;
                return false;
            }

            element = enumerator.Current;
            return true;
        }

        /// <summary>
        /// Returns an optional value that represents the first element in the <see cref="IEnumerable{T}"/> that satisfies the specified predicate,
        /// or an empty optional if no such element is found.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="enumerable">The source collection.</param>
        /// <param name="predicate">A predicate function used to test each element.</param>
        /// <returns>An optional value that contains the first element satisfying the predicate, or an empty optional if no such element is found.</returns>
        public static Optional<T> FirstOptional<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            if(!enumerable.TryGetFirst(predicate, out T result))
            {
                return Optional<T>.None;
            }

            return Optional<T>.Some(result);
        }

        /// <summary>
        /// Generates a collection of ints with the requested range.
        /// </summary>
        public static IEnumerable<int> RangeStartEnd(int fromIncluding, int toExcluding)
        {
            for (int i = fromIncluding; i < toExcluding; ++i)
            {
                yield return i;
            }
        }

        [Obsolete("Use TakeRandomNonRepeated")]
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> enumerable, IRandomGenerator random, int count)
        {
            return TakeRandomNonRepeating(enumerable, random, count);
        }

        /// <summary>
        /// Randomly takes an N amount of elements from the collection.
        /// It uses <see cref="TakeRandomNonRepeating{T}(System.Collections.Generic.IEnumerable{T},IRandomGenerator,int)"/>
        /// or <see cref="EnumerableExtensions.TakeRandomRepeating{T}"/>
        /// depending on the provided isRepeating bool.
        /// </summary>
        public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> enumerable, IRandomGenerator random, int count, bool isRepeating)
        {
            if (isRepeating)
            {
                return TakeRandomRepeating(enumerable, random, count);
            }

            return TakeRandomNonRepeating(enumerable, random, count);
        }

        /// <summary>
        /// Randomly takes an N amount of elements from the collection.
        /// It will never take the same element twice.
        /// If it has run out of elements to take, it will return the whole collection.
        /// </summary>
        public static IEnumerable<T> TakeRandomNonRepeating<T>(this IEnumerable<T> enumerable, IRandomGenerator random, int count)
        {
            IEnumerable<T> suffledElements = enumerable.OrderBy(x => random.NewInt());
            return suffledElements.Take(count);
        }

        /// <summary>
        /// Randomly takes an N amount of elements from the collection.
        /// It can take the same element twice, so it will always return the requested amount of elements.
        /// </summary>
        public static IEnumerable<T> TakeRandomRepeating<T>(this IEnumerable<T> enumerable, IRandomGenerator random, int count)
        {
            List<T> shuffledElements = enumerable
                .OrderBy(x => random.NewInt())
                .ToList();

            T[] elements = new T[count];
            for (int i = 0; i < count; i++)
            {
                elements[i] = shuffledElements.TakeRandom(random);
            }

            return elements;
        }

        /// <summary>
        /// Randomly takes an elements from the collection.
        /// </summary>
        public static T TakeRandom<T>(this IEnumerable<T> enumerable, IRandomGenerator random)
        {
            IReadOnlyList<T> readonlyList = enumerable.ToReadOnlyList();
            int index = random.NewInt(0, readonlyList.Count);

            return readonlyList[index];
        }

        public static T TakeRandomWeighted<T>(this IEnumerable<T> enumerable, IRandomGenerator random, Func<T, float> getWeight)
        {
            var readOnlyList = enumerable.ToReadOnlyList();

            //Get the totalWeight of the elements in the enumerable
            float totalWeight = readOnlyList.Sum(getWeight);

            // Generate a random number between 0 and the total weight
            float randomWeight = random.NewFloat() * totalWeight;

            // Find the element that has the selected random weight
            foreach (T item in readOnlyList)
            {
                randomWeight -= getWeight(item);

                if (randomWeight <= 0f)
                {
                    return item;
                }
            }

            throw new InvalidOperationException("This should never happen. No element was selected.");
        }

        /// <summary>
        /// Randomly takes a valid index from the collection.
        /// </summary>
        public static int TakeRandomIndex<T>(this IEnumerable<T> enumerable, IRandomGenerator random)
        {
            IReadOnlyList<T> readonlyList = enumerable.ToReadOnlyList();
            int index = random.NewInt(0, readonlyList.Count);

            return index;
        }

        /// <summary>
        /// Randomly takes an N amount of elements from the collection.
        /// It will never take the same element twice.
        /// If it has run out of elements to take, it will return the whole collection.
        /// </summary>
        public static IEnumerable<T> TakeRandomNonRepeating<T>(this IEnumerable<T> enumerable, Random random, int count)
        {
            IEnumerable<T> suffledElements = enumerable.OrderBy(x => random.Next());
            return suffledElements.Take(count);
        }

        /// <summary>
        /// Randomly takes an elements from the collection.
        /// </summary>
        public static T TakeRandom<T>(this IEnumerable<T> enumerable, Random random)
        {
            IReadOnlyList<T> readonlyList = enumerable.ToReadOnlyList();
            int index = random.Next(0, readonlyList.Count);

            return readonlyList[index];
        }

        /// <summary>
        /// Generates the difference between the items of two collections.
        /// </summary>
        public static void GetDifference<T>(
            this IEnumerable<T> oldEnumerable,
            IEnumerable<T> newEnumerable,
            out List<T> addedElements,
            out List<T> removedElements)
        {
            addedElements = new List<T>();
            removedElements = new List<T>();
            GetDifferenceRef(
                oldEnumerable,
                newEnumerable,
                ref addedElements,
                ref removedElements
            );
        }

        /// <summary>
        /// Generates the difference between the items of two collections.
        /// </summary>
        public static void GetDifferenceRef<T>(
            this IEnumerable<T> oldEnumerable,
            IEnumerable<T> newEnumerable,
            ref List<T> addedElements,
            ref List<T> removedElements)
        {
            IReadOnlyCollection<T> oldEnumerableReadonly = oldEnumerable.ToReadOnlyCollection();
            IReadOnlyCollection<T> newEnumerableReadonly = newEnumerable.ToReadOnlyCollection();

            removedElements.AddRange(oldEnumerableReadonly);

            foreach (T element in newEnumerableReadonly)
            {
                bool isOld = removedElements.Remove(element);

                if (isOld)
                {
                    continue;
                }

                addedElements.Add(element);
            }
        }
    }
}
