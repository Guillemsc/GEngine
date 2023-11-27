using System.Text;

namespace GEngine.Utils.Extensions
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns if the value is divisible by two.
        /// </summary>
        /// <example>2, 4, 6, etc...</example>
        public static bool IsEven(this int value) => value % 2 == 0;

        /// <summary>
        /// Returns if the value is not divisible by two.
        /// </summary>
        /// <example>1, 3, 5, etc....</example>
        public static bool IsOdd(this int value) => value % 2 != 0;

        /// <summary>
        /// Checks if the value is in-between the provided range.
        /// </summary>
        public static bool IsBetween(this int value, int minIncluding, int maxIncluding)
            => value >= minIncluding && value <= maxIncluding;

        /// <summary>
        /// Returns false if value is 0, returns true otherwise.
        /// </summary>
        public static bool ToBool(this int value) => Convert.ToBoolean(value);

        /// <summary>
        /// Equivalent of <see cref="TimeSpan.FromMilliseconds"/>.
        /// </summary>
        public static TimeSpan ToMilliseconds(this int value) => TimeSpan.FromMilliseconds(value);
        
        /// <summary>
        /// Equivalent of <see cref="TimeSpan.FromSeconds"/>.
        /// </summary>
        public static TimeSpan ToSeconds(this int value) => TimeSpan.FromSeconds(value);

        /// <summary>
        /// Equivalent of <see cref="TimeSpan.FromHours"/>.
        /// </summary>
        public static TimeSpan ToHours(this int value) => TimeSpan.FromHours(value);
        
        /// <summary>
        /// Returns the absolute value of a specified number.
        /// </summary>
        public static int Abs(this int value) => Math.Abs(value);

        /// <summary>
        /// Converts an integer to its correspoinding alphabetical value.
        /// When this value goes over the max letter inside the alphabet, it concatenates the result, creating a sequence.
        /// Only works with 0 and positive integers.
        /// </summary>
        /// <example>0 == A, 26 == Z, 27 == AA, 28 == AB, 53 == BA, 703 == AAA</example>
        public static string ToAlphabeticalId(this int value)
        {
            value += 1;

            if (value < 0)
            {
                return string.Empty;
            }

            const int alphabetCharactersCount = 26;

            // Small optimization if value has only one character
            if (value <= alphabetCharactersCount)
            {
                return Convert.ToChar(value + 64).ToString();
            }

            StringBuilder stringBuilder = new();

            List<int> toCheck = new()
            {
                value
            };

            while (toCheck.Count > 0)
            {
                int checking = toCheck[0];
                toCheck.RemoveAt(0);

                int div = checking / alphabetCharactersCount;
                int mod = checking % alphabetCharactersCount;
                if (mod == 0) { mod = alphabetCharactersCount; div--; }

                if (checking > alphabetCharactersCount)
                {
                    toCheck.Insert(0, mod);
                    toCheck.Insert(0, div);
                    continue;
                }

                char character = Convert.ToChar(checking + 64);

                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// This method calculates a value that moves back and forth between 0 and
        /// a given 'length' as the input number 't' increases. This can be useful for situations
        /// where you need a value to oscillate within a specific range.
        /// </summary>
        public static int PingPong(this int t, int length)
        {
            if (length == 0)
            {
                return 0;
            }

            var q = t / length;
            var r = t % length;

            return q % 2 == 0 ? r : length - r;
        }

        /// <summary>
        /// Increments the given value and then cycles it within a specified range [start, end).
        /// </summary>
        /// <param name="value">The value to be incremented and cycled.</param>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range. This is exclusive.</param>
        /// <returns>The incremented and cycled value.</returns>
        public static int IncrementAndCycle(this int value, int start, int end)
        {
            int next = (value - start + 1) % (end - start) + start;
            return next;
        }

        /// <summary>
        /// Increments the given value and then cycles it within a specified range [0, end).
        /// </summary>
        /// <param name="value">The value to be incremented and cycled.</param>
        /// <param name="end">The end of the range. This is exclusive.</param>
        /// <returns>The incremented and cycled value.</returns>
        public static int IncrementAndCycle(this int value, int end)
        {
            return IncrementAndCycle(value, 0, end);
        }

        /// <summary>
        /// Decrements the given value and then cycles it within a specified range [start, end).
        /// </summary>
        /// <param name="value">The value to be decremented and cycled.</param>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range. This is exclusive.</param>
        /// <returns>The decremented and cycled value.</returns>
        public static int DecrementAndCycle(this int value, int start, int end)
        {
            int next = (value - 1 - start + (end - start)) % (end - start) + start;
            return next;
        }

        /// <summary>
        /// Decrements the given value and then cycles it within a specified range [0, end).
        /// </summary>
        /// <param name="value">The value to be decremented and cycled.</param>
        /// <param name="end">The end of the range. This is exclusive.</param>
        /// <returns>The decremented and cycled value.</returns>
        public static int DecrementAndCycle(this int value, int end)
        {
            return DecrementAndCycle(value, 0, end);
        }

        /// <summary>
        /// Checks if the provided integer is a power of two.
        /// </summary>
        /// <param name="x">The integer to check.</param>
        /// <returns>True if the integer is a power of two, false otherwise.</returns>
        public static bool IsPowerOfTwo(this int x)
        {
            return (x & (x - 1)) == 0;
        }

        /// <summary>
        /// Returns the next power of two that is greater than the provided integer.
        /// </summary>
        /// <param name="x">The integer for which to find the next power of two.</param>
        /// <returns>The next power of two that is equal to or greater than the provided integer.</returns>
        public static int NextPowerOfTwo(this int x)
        {
            x += 1;
            var result = 2;
            while (result < x)
            {
                result <<= 1;
            }
            return result;
        }

        /// <summary>
        /// Returns the largest power of two that is less than the provided integer.
        /// </summary>
        /// <param name="x">The integer for which to find the previous power of two.</param>
        /// <returns>The largest power of two that is less than the provided integer.</returns>
        public static int PreviousPowerOfTwo(this int x)
        {
            if (x <= 1)
            {
                throw new ArgumentException("Minimum power of 2 is 1");
            }

            x -= 1;

            x |= (x >> 1);
            x |= (x >> 2);
            x |= (x >> 4);
            x |= (x >> 8);
            x |= (x >> 16);
            return x - (x >> 1);
        }

        /// <summary>
        /// Returns if there is any power of 2 lower than the provided number
        /// </summary>
        /// <param name="x">The integer for which to check for a previous power of two.</param>
        public static bool HasPreviousPowerOfTwo(this int x)
        {
            return x > 1;
        }
    }
}
