using System;

namespace GEngine.Utils.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// "00m 00s"
        /// </summary>
        public static readonly string ZeroMSString = "00m 00s";

        /// <summary>
        /// Returns the smaller of two TimeSpan values.
        /// </summary>
        public static TimeSpan Min(TimeSpan t1, TimeSpan t2)
        {
            return t1 < t2 ? t1 : t2;
        }

        /// <summary>
        /// Returns the bigger of two TimeSpan values.
        /// </summary>
        public static TimeSpan Max(TimeSpan t1, TimeSpan t2)
        {
            return t1 < t2 ? t2 : t1;
        }

        /// <summary>
        /// Returns a string matching the "hh\:mm\:ss" format. For example 1.14:30.
        /// </summary>
        public static string ToStringHMS(this TimeSpan timeSpan)
        {
            return timeSpan.ToString(@"hh\:mm\:ss");
        }

        /// <summary>
        /// Returns a string representation of a time span that displays the most relevant two units of time in the format of
        /// "Xd Xh", "Xh Xm", or "Xm Xs", where X is a number.
        /// </summary>
        /// <param name="timeSpan">The TimeSpan to convert to a string representation.</param>
        /// <returns>A string representation of the most relevant two units of time in the TimeSpan.</returns>
        /// <remarks>
        /// Relevancy from more to less: days, hours, minutes.
        /// </remarks>
        public static string ToStringWithMostRelevantPairDHMS(this TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
            {
                return ZeroMSString;
            }

            if (timeSpan.Days > 0)
            {
                return $"{timeSpan:%d}d {timeSpan:hh}h";
            }

            if (timeSpan.Hours > 0)
            {
                return $"{timeSpan:hh}h {timeSpan:mm}m";
            }

            return $"{timeSpan:mm}m {timeSpan:ss}s";
        }

        /// <summary>
        /// Converts a TimeSpan to a string representation in the format "Xd Xh Xm Xs" ("3d 2h 15m 30s"),
        /// but only using the units that are greater than zero.
        /// </summary>
        /// <param name="timeSpan">The TimeSpan to convert.</param>
        /// <returns>A string representation of the TimeSpan.</returns>
        public static string ToStringRelevantDHMS(this TimeSpan timeSpan)
        {
            if (timeSpan.Days > 0)
            {
                return $"{timeSpan:%d}d {timeSpan:hh}h {timeSpan:mm}m {timeSpan:ss}s";
            }

            if (timeSpan.Hours > 0)
            {
                return $"{timeSpan:hh}h {timeSpan:mm}m {timeSpan:ss}s";
            }

            if (timeSpan.Minutes > 0)
            {
                return $"{timeSpan:mm}m {timeSpan:ss}s";
            }

            if (timeSpan.Seconds > 0)
            {
                return $"{timeSpan:ss}s";
            }

            return "0s";
        }
    }
}
