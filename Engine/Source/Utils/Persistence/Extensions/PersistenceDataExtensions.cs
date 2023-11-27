using System.Threading;
using GEngine.Utils.Persistence.Data;
using GEngine.Utils.Extensions;

namespace GEngine.Utils.Persistence.Extensions
{
    public static class PersistenceDataExtensions
    {
        public static void SaveAsync(this IPersistenceData persistenceData)
        {
            persistenceData.Save(CancellationToken.None).RunAsync();
        }
    }
}
