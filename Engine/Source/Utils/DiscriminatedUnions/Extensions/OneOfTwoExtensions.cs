using System;
using GEngine.Utils.Optionals;

namespace GEngine.Utils.DiscriminatedUnions
{
    public static class OnOfTwoExtensions
    {
        /// <summary>
        /// Depending on the union value, executes one of the actions.
        /// </summary>
        public static IOneOf<TFirst, TSecond> Switch<TFirst, TSecond>(
            this OneOf<TFirst, TSecond> oneOf,
            Action<TFirst> firstAction,
            Action<TSecond> secondAction
        )
        {
            if (oneOf.TryGetFirst(out TFirst first))
            {
                firstAction.Invoke(first);
                return oneOf;
            }

            TSecond second = oneOf.UnsafeGetSecond();
            secondAction.Invoke(second);
            return oneOf;
        }

        /// <summary>
        /// Depending on the union value, executes one of the actions.
        /// This actions then all return the same type.
        /// </summary>
        public static TReturn Match<TFirst, TSecond, TReturn>(
            this IOneOf<TFirst, TSecond> oneOf,
            Func<TFirst, TReturn> firstAction,
            Func<TSecond, TReturn> secondAction
        )
        {
            if (oneOf.TryGetFirst(out TFirst first))
            {
                return firstAction.Invoke(first);
            }

            TSecond second = oneOf.UnsafeGetSecond();
            return secondAction.Invoke(second);
        }

        /// <summary>
        /// Retrieves an optional value of the first type from an instance of <see cref="IOneOf{TFirst, TSecond}"/>.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first value.</typeparam>
        /// <typeparam name="TSecond">The type of the second value.</typeparam>
        /// <param name="oneOf">The instance of <see cref="IOneOf{TFirst, TSecond}"/> to retrieve the value from.</param>
        /// <returns>An <see cref="Optional{T}"/> containing the first value if it exists; otherwise, returns <see cref="Optional{T}.None"/>.</returns>
        public static Optional<TFirst> GetFirstOptional<TFirst, TSecond>(IOneOf<TFirst, TSecond> oneOf)
        {
            if(!oneOf.TryGetFirst(out TFirst first))
            {
                return Optional<TFirst>.None;
            }

            return Optional<TFirst>.Some(first);
        }

        /// <summary>
        /// Retrieves an optional value of the second type from an instance of <see cref="IOneOf{TFirst, TSecond}"/>.
        /// </summary>
        /// <typeparam name="TFirst">The type of the first value.</typeparam>
        /// <typeparam name="TSecond">The type of the second value.</typeparam>
        /// <param name="oneOf">The instance of <see cref="IOneOf{TFirst, TSecond}"/> to retrieve the value from.</param>
        /// <returns>An <see cref="Optional{T}"/> containing the second value if it exists; otherwise, returns <see cref="Optional{T}.None"/>.</returns>
        public static Optional<TSecond> GetSecondOptional<TFirst, TSecond>(IOneOf<TFirst, TSecond> oneOf)
        {
            if(!oneOf.TryGetSecond(out TSecond second))
            {
                return Optional<TSecond>.None;
            }

            return Optional<TSecond>.Some(second);
        }
    }
}
