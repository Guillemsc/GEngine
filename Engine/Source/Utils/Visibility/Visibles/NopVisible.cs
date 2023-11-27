using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Visibility.Visibles
{
    public sealed class NopVisible : IVisible
    {
        public static readonly NopVisible Instance = new();

        NopVisible()
        {

        }

        public Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
