using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Delegates.Animation
{
    /// <summary>
    /// Generic delegate for things that get animated.
    /// </summary>
    /// <param name="instantly">If the animation should be performed instantly, on this same frame.</param>
    /// <param name="cancellationToken">For canceling the animation in progress at any time.</param>
    /// <returns>Task that lasts the duration of the animation.</returns>
    public delegate Task TaskAnimationEvent(bool instantly, CancellationToken cancellationToken);
}
