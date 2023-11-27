using System;
using GEngine.Utils.Optionals;

namespace GEngine.Utils.Extensions
{
    public static class BoolExtensions
    {
        /// <summary>
        /// Converts a boolean to an integer. When true returns 1 otherwise 0.
        /// </summary>
        public static int ToInt(this bool value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts a boolean to an integer. When true returns 1 otherwise -1.
        /// </summary>
        public static int ToPositiveNegativeOneInt(this bool value)
        {
            return Convert.ToInt32(value) * 2 - 1;
        }

        /// <summary>
        /// Converts a boolean to an integer. When true returns 0 otherwise 1.
        /// </summary>
        public static int ToInverseInt(this bool value)
        {
            return Convert.ToInt32(!value);
        }

        /// <summary>
        /// Converts a boolean to a float. When true returns 1f otherwise 0f.
        /// </summary>
        public static float ToFloat(this bool value)
        {
            return Convert.ToSingle(value);
        }

        /// <summary>
        /// Converts a boolean to a float. When true returns 1f otherwise -1f.
        /// </summary>
        public static float ToPositiveNegativeOneFloat(this bool value)
        {
            return Convert.ToSingle(value) * 2f - 1f;
        }

        /// <summary>
        /// Converts a boolean to a float. When true returns 0f otherwise 1f.
        /// </summary>
        public static float ToInverseFloat(this bool value)
        {
            return Convert.ToSingle(!value);
        }

        /// <summary>
        /// Converts a boolean and associated value of any type to an Optional of the associated type
        /// Very useful when used together with the explicit boolean Try pattern
        /// </summary>
        /// <param name="isValueSome">The </param>
        /// <param name="value">The value to encapsulate in the optional</param>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <returns>An optional with the value if the boolean is true otherwise an Optional{T}.None</returns>
        /// <example>
        /// var optionalInt = something.TryGet(out int result).ToOptional(result);
        /// </example>
        public static Optional<T> ToOptional<T>(this bool isValueSome, T value)
        {
            return Optional<T>.Maybe(isValueSome, value);
        }
    }
}
