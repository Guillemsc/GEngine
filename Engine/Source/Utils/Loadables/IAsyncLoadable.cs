using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Disposing.Disposables;

namespace GEngine.Utils.Loadables
{
    public interface IAsyncLoadable<T>
    {
        Task<IAsyncDisposable<T>> Load(CancellationToken ct);
    }
}
