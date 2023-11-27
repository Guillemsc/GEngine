using System;

namespace GEngine.Utils.Time.Services
{
    /// <summary>
    /// Represents a time service that provides access to current time.
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// Gets the current Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; }

        /// <summary>
        /// Gets the current local time.
        /// </summary>
        DateTime LocalNow { get; }
    }
}
