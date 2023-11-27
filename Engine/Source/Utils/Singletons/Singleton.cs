namespace GEngine.Utils.Singletons
{
    public abstract class Singleton<T> where T : class
    {
        static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Activator.CreateInstance<T>();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Resets the singleton instance.
        /// Needs to be called with [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        /// on inherited class to support disabled domain reload, since Unity does not support it on base classes.
        /// </summary>
        protected static void ClearInstance()
        {
            _instance = null;
        }
    }
}
