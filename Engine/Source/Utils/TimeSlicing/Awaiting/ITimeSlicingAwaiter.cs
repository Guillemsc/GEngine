using System.Threading.Tasks;

namespace GEngine.Utils.TimeSlicing.Awaiting
{
    /// <summary>
    /// Represents an awaiter that allows time-sliced execution.
    /// This is useful for doing frame-slice operations.
    /// For example, can be used to avoid going over the budget ms of our application.
    /// </summary>
    public interface ITimeSlicingAwaiter
    {
        /// <summary>
        /// Starts the awaiter internal logic.
        /// </summary>
        void Start();

        /// <summary>
        /// Attempts to execute a time slice.
        /// Depending on the implementation, this can happen after some time since start, for example.
        /// </summary>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
        ValueTask TryTimeSlice();
    }
}
