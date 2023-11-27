using System;

namespace GEngine.Utils.Extensions
{
    public static class DoubleExtensions
    {
        public static double GetDecimals(this double value)
        {
            var result = value % 1d;
            return result;
        }
        
        /// <summary>
        /// Equivalent of <see cref="TimeSpan.FromMilliseconds"/>.
        /// </summary>
        public static TimeSpan ToMilliseconds(this double value) => TimeSpan.FromMilliseconds(value);
    }
}
