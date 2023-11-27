using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GEngine.Utils.Persistence.Data;
using GEngine.Utils.Persistence.Extensions;

namespace GEngine.Utils.Persistence.Services
{
    /// <inheritdoc />
    public sealed class PersistenceService : IPersistenceService
    {
        readonly Dictionary<Type, IPersistenceData> _persistenceDatas = new();

        public void Add<T>(T persistenceData) where T : IPersistenceData
        {
            Type type = typeof(T);

            Add(type, persistenceData);
        }

        public void Add(IPersistenceData persistenceData)
        {
            Type type = persistenceData.GetType();

            Add(type, persistenceData);
        }

        public async Task LoadAll(CancellationToken cancellationToken)
        {
            foreach (IPersistenceData serializableData in _persistenceDatas.Values)
            {
                if(cancellationToken.IsCancellationRequested) break;

                await serializableData.Load(cancellationToken);
            }
        }

        public T Get<T>() where T : IPersistenceData
        {
            Type type = typeof(T);

            bool found = _persistenceDatas.TryGetValue(type, out IPersistenceData persistenceData);

            if (!found)
            {
                throw new Exception($"Tried to get {type.Name}, but it has not been not added");
            }

            return (T)persistenceData;
        }

        public Task Save<T>(CancellationToken cancellationToken) where T : IPersistenceData
        {
            IPersistenceData persistenceData = Get<T>();
            return persistenceData.Save(cancellationToken);
        }

        public void SaveAsync<T>() where T : IPersistenceData
        {
            IPersistenceData persistenceData = Get<T>();
            persistenceData.SaveAsync();
        }

        void Add(Type type, IPersistenceData persistenceData)
        {
            bool alreadyAdded = _persistenceDatas.ContainsKey(type);

            if (alreadyAdded)
            {
                throw new Exception($"Tried to add {type.Name}, but this type is already present");
            }

            _persistenceDatas.Add(type, persistenceData);
        }
    }
}
