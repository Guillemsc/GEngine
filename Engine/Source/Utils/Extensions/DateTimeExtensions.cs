using System;

namespace GEngine.Utils.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Year 1970, mont 1, day 1.
        /// The Unix epoch is the number of seconds that have elapsed since January 1, 1970 at midnight UTC.
        /// </summary>
        public static DateTime Epoch => new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Calls <see cref="DateTime.DaysInMonth"/> on this DateTime.
        /// </summary>
        public static int DaysInMonth(this DateTime dateTime)
            => DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        /// <summary>
        /// The unix time stamp is a way to track time as a running total of seconds. This count starts at the Unix
        /// Epoch on January 1st, 1970 at UTC. Therefore, the unix time stamp is merely the number of seconds between
        /// a particular date and the Unix Epoch.
        /// https://www.unixtimestamp.com/index.php
        /// </summary>
        public static TimeSpan TimeSpanSinceEpoch(this DateTime dateTime)
            => dateTime - Epoch;

        /// <summary>
        /// Checks if this DateTime is in between the provided start and end DateTimes.
        /// </summary>
        public static bool IsBetween(this DateTime dateTime, DateTime startDateTime, DateTime endDateTime)
        {
            return dateTime >= startDateTime && dateTime <= endDateTime;
        }

        /// <summary>
        /// Returns a string formated in a dd-MM-yyyy way. Example: 25-03-2023.
        /// </summary>
        public static string ToStringDMY(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static DateTime GetCurrentDateTimeByKind(DateTimeKind dateTimeKind)
        {
#pragma warning disable GK0001
            return dateTimeKind switch
            {
                DateTimeKind.Local => DateTime.Now,
                DateTimeKind.Utc => DateTime.UtcNow,
                _ => throw new ArgumentOutOfRangeException(nameof(dateTimeKind), dateTimeKind, null)
            };
#pragma warning restore GK0001


        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(unixTimeStamp);
        }

        public static double DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            return dateTime.Subtract(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            ).TotalSeconds;
        }
    }
}
