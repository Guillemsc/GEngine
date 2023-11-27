using GEngine.Utils.Time.TimeSources;

namespace GEngine.Utils.Time.TimeContexts
{
    /// <summary>
    /// Represents a complete time context that provides time-related properties and functionalities.
    /// </summary>
    public interface ITimeContext : ITimeSource
    {
        /// <summary>
        /// Gets or sets the time scale.
        /// </summary>
        float TimeScale { get; set; }

        /// <summary>
        /// Gets the delta time, which is the time elapsed since the last time update (usually last frame).
        /// </summary>
        float DeltaTime { get; }
    }
}
