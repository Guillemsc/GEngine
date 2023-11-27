using System;
using System.Collections.Generic;
using System.Text;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Predicates.Extensions
{
    public static class PredicateExtensions
    {
        /// <summary>
        /// Checks if all predicates in the collection are satisfied.
        /// </summary>
        /// <param name="predicatesCollection">The collection of predicates to check.</param>
        /// <returns><c>true</c> if all predicates are satisfied, <c>false</c> otherwise.</returns>
        public static bool AreAllPredicatesSatisfied(this IReadOnlyCollection<IPredicate> predicatesCollection)
        {
            foreach (var predicate in predicatesCollection)
            {
                bool isSatisfied = predicate.IsSatisfied();

                if (!isSatisfied)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets a string representation of the state of all predicates in the collection.
        /// Used for debug purposes.
        /// </summary>
        /// <param name="predicatesCollection">The collection of predicates.</param>
        /// <returns>A string representation of the state of all predicates.</returns>
        public static string GetPredicatesSateString(this IReadOnlyCollection<IPredicate> predicatesCollection)
        {
            StringBuilder stringBuilder = new();

            bool satisfied = true;

            foreach (var predicate in predicatesCollection)
            {
                bool isSatisfied = predicate.IsSatisfied();

                satisfied &= isSatisfied;

                string extraInfoString = string.Empty;

                Func<string> method = predicate.ToString;
                bool isToStringOverriden = method.Method.IsOverriden();

                if (isToStringOverriden)
                {
                    extraInfoString = $" [{predicate}]";
                }

                stringBuilder.AppendLine($"-{predicate.GetType().Name}: {isSatisfied}{extraInfoString}");
            }

            stringBuilder.AppendLine($"Result: {satisfied}");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets a string representation of the state of the specified predicate.
        /// Used for debug purposes.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string representation of the state of the predicate.</returns>
        public static string GetPredicateSateString(this IPredicate predicate)
        {
            bool isSatisfied = predicate.IsSatisfied();

            string extraInfoString = string.Empty;

            Func<string> method = predicate.ToString;
            bool isToStringOverriden = method.Method.IsOverriden();

            if (isToStringOverriden)
            {
                extraInfoString = $" [{predicate}]";
            }

            return $"{predicate.GetType().Name}: {isSatisfied}{extraInfoString}";
        }
    }
}
