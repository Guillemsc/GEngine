using GEngine.Utils.Disposing.Disposables;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Loadables
{
    /// <summary>
    /// Adapter for returning something that is already loaded and is managed by some external loading/unloading system
    /// This can be used for example, when binding an Installer to a DiContext and the installer is already in the scene
    /// </summary>
    /// <typeparam name="T">Type of the object to be returned as a loadable</typeparam>
    public sealed class PreloadedLoadable<T> : ILoadable<T>
    {
        readonly T _instance;

        public PreloadedLoadable(T instance)
        {
            _instance = instance;
        }

        public IDisposable<T> Load()
        {
            return new CallbackDisposable<T>(_instance, DelegateExtensions.DoNothing);
        }
    }
}
