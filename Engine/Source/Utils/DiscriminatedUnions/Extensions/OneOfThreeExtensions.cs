using System;
using GEngine.Utils.Optionals;

namespace GEngine.Utils.DiscriminatedUnions
{
    public static class OneOfThreeExtensions
    {
        /// <summary>
        /// Depending on the union value, executes one of the actions.
        /// </summary>
        public static IOneOf<TFirst, TSecond, TThird> Switch<TFirst, TSecond, TThird>(
            this IOneOf<TFirst, TSecond, TThird> oneOf,
            Action<TFirst> firstAction,
            Action<TSecond> secondAction,
            Action<TThird> thirdAction
        )
        {
            if (oneOf.TryGetFirst(out TFirst first))
            {
                firstAction.Invoke(first);
                return oneOf;
            }

            if (oneOf.TryGetSecond(out TSecond second))
            {
                secondAction.Invoke(second);
                return oneOf;
            }

            TThird third = oneOf.UnsafeGetThird();
            thirdAction.Invoke(third);
            return oneOf;
        }

        /// <summary>
        /// Depending on the union value, executes one of the actions.
        /// This actions then all return the same type.
        /// </summary>
        public static TReturn Match<TFirst, TSecond, TThird, TReturn>(
            this IOneOf<TFirst, TSecond, TThird> oneOf,
            Func<TFirst, TReturn> firstAction,
            Func<TSecond, TReturn> secondAction,
            Func<TThird, TReturn> thirdAction
        )
        {
            if (oneOf.TryGetFirst(out TFirst first))
            {
                return firstAction.Invoke(first);
            }

            if (oneOf.TryGetSecond(out TSecond second))
            {
                return secondAction.Invoke(second);
            }

            TThird third = oneOf.UnsafeGetThird();
            return thirdAction.Invoke(third);
        }

        /// <summary>
        /// Retrieves an optional value of the first type from an instance of <see cref="IOneOf{TFirst, TSecond, TThird}"/>.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first value.</typeparam>
        /// <typeparam name="TSecond">The type of the second value.</typeparam>
        /// <typeparam name="TThird">The type of the third value.</typeparam>
        /// <param name="oneOf">The instance of <see cref="IOneOf{TFirst, TSecond, TThird}"/> to retrieve the value from.</param>
        /// <returns>An <see cref="Optional{T}"/> containing the first value if it exists; otherwise, returns <see cref="Optional{T}.None"/>.</returns>
        public static Optional<TFirst> GetFirstOptional<TFirst, TSecond, TThird>(IOneOf<TFirst, TSecond, TThird> oneOf)
        {
            if(!oneOf.TryGetFirst(out TFirst first))
            {
                return Optional<TFirst>.None;
            }

            return Optional<TFirst>.Some(first);
        }

        /// <summary>
        /// Retrieves an optional value of the second type from an instance of <see cref="IOneOf{TFirst, TSecond, TThird}"/>.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first value.</typeparam>
        /// <typeparam name="TSecond">The type of the second value.</typeparam>
        /// <typeparam name="TThird">The type of the third value.</typeparam>
        /// <param name="oneOf">The instance of <see cref="IOneOf{TFirst, TSecond, TThird}"/> to retrieve the value from.</param>
        /// <returns>An <see cref="Optional{T}"/> containing the second value if it exists; otherwise, returns <see cref="Optional{T}.None"/>.</returns>
        public static Optional<TSecond> GetSecondOptional<TFirst, TSecond, TThird>(IOneOf<TFirst, TSecond, TThird> oneOf)
        {
            if(!oneOf.TryGetSecond(out TSecond second))
            {
                return Optional<TSecond>.None;
            }

            return Optional<TSecond>.Some(second);
        }

        /// <summary>
        /// Retrieves an optional value of the third type from an instance of <see cref="IOneOf{TFirst, TSecond, TThird}"/>.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first value.</typeparam>
        /// <typeparam name="TSecond">The type of the second value.</typeparam>
        /// <typeparam name="TThird">The type of the third value.</typeparam>
        /// <param name="oneOf">The instance of <see cref="IOneOf{TFirst, TSecond, TThird}"/> to retrieve the value from.</param>
        /// <returns>An <see cref="Optional{T}"/> containing the second value if it exists; otherwise, returns <see cref="Optional{T}.None"/>.</returns>
        public static Optional<TThird> GetThirdOptional<TFirst, TSecond, TThird>(IOneOf<TFirst, TSecond, TThird> oneOf)
        {
            if(!oneOf.TryGetThird(out TThird third))
            {
                return Optional<TThird>.None;
            }

            return Optional<TThird>.Some(third);
        }
    }
}
