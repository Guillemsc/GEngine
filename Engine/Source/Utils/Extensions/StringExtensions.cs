using System.Collections.Generic;
using System.Linq;

namespace GEngine.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string Pluralize(this string value, IReadOnlyList<object> parameters) => StringPluralizationExtensions.Pluralize(value, parameters);

        /// <summary><para>Indicates whether a specified string is null, empty, or consists only of white-space characters.</para></summary>
        /// <param name="value">The string to test.</param>
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary><para>Indicates whether the specified string is null or an <see cref="F:System.String.Empty" /> string.</para></summary>
        /// <param name="value">The string to test. </param>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        public static string Repeat(this string text, int n) => string.Concat(Enumerable.Repeat(text, n));
    }
}
