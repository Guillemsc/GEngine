using Newtonsoft.Json;

namespace GEngine.Utils.Persistence.Constants
{
    public static class PersistenceConstants
    {
        public static readonly JsonSerializerSettings DebugJsonSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented
        };

        public static readonly JsonSerializerSettings ReleaseJsonSettings = new()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None
        };
    }
}
