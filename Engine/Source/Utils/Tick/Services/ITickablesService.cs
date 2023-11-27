using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Utils.Tick.Services
{
    /// <summary>
    /// Represents a service for ticking tickable objects.
    /// </summary>
    public interface ITickablesService
    {
        /// <summary>
        /// Adds a tickable object to the service.
        /// </summary>
        /// <param name="tickable">The tickable object to add.</param>
        /// <param name="tickType">When the tick is going to be performed.</param>
        void Add(ITickable tickable, TickType tickType);

        /// <summary>
        /// Removes a tickable object from the service.
        /// </summary>
        /// <param name="tickable">The tickable object to remove.</param>
        /// <param name="tickType">When the tick is was being performed.</param>
        void Remove(ITickable tickable, TickType tickType);

        /// <summary>
        /// Immediately removes a tickable object from the service (normally they are removed the next frame).
        /// </summary>
        /// <param name="tickable">The tickable object to remove.</param>
        /// <param name="tickType">When the tick is was being performed.</param>
        void RemoveNow(ITickable tickable, TickType tickType);
    }
}
