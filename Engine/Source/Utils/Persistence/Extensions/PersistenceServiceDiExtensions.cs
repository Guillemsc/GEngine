using GEngine.Utils.Di.Builder;
using GEngine.Utils.Di.Container;
using GEngine.Utils.Persistence.Data;
using GEngine.Utils.Persistence.Services;

namespace GEngine.Utils.Persistence.Extensions
{
    public static class PersistenceServiceDiExtensions
    {
        public static IDiBindingActionBuilder<T> FromPersistenceService<T>(
            this IDiBindingBuilder<T> diBindingBuilder
            ) where T : IPersistenceData
        {
            T FromFunction(IDiResolveContainer resolveContainer)
            {
                IPersistenceService persistenceService = resolveContainer.Resolve<IPersistenceService>();
                return persistenceService.Get<T>();
            }

            return diBindingBuilder.FromFunction(FromFunction);
        }
    }
}
