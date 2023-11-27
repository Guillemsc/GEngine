using GEngine.Utils.Disposing.Disposables;

namespace GEngine.Utils.Loadables
{
    public interface ILoadable<out T>
    {
        IDisposable<T> Load();
    }
}
